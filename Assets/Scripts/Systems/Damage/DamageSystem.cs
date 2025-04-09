using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

[BurstCompile]
public partial struct DamageSystem : ISystem
{
    private EntityQuery _queryDamageComponent;

    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        var ecb = new EntityCommandBuffer(Allocator.TempJob);
        var queries = new NativeArray<ComponentType>(2, Allocator.Temp);
        queries[0] = ComponentType.ReadOnly<DamageComponent>();
        queries[1] = ComponentType.ReadOnly<EnemyHpComponent>();
        _queryDamageComponent = state.GetEntityQuery(queries);
        new OffDamageComponentJob { Ecb = ecb }.Run(_queryDamageComponent);
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var ecb = new EntityCommandBuffer(Allocator.TempJob);
        new DamageJob { Ecb = ecb }.Run(_queryDamageComponent);
        state.Dependency.Complete();
        ecb.Playback(state.EntityManager);
        ecb.Dispose();
    }

    [BurstCompile]
    public partial struct DamageJob : IJobEntity
    {
        public EntityCommandBuffer Ecb;

        [BurstCompile]
        private void Execute(Entity entity, ref DamageComponent damage, ref EnemyHpComponent hp)
        {
            var resHp = hp.Hp - damage.Damage;
            var hpResult = new EnemyHpComponent { Hp = resHp, MaxHp = hp.MaxHp };
            Ecb.SetComponent(entity, hpResult);
            Ecb.SetComponentEnabled<DamageComponent>(entity, false);
        }
    }

    [BurstCompile]
    public partial struct OffDamageComponentJob : IJobEntity
    {
        public EntityCommandBuffer Ecb;

        [BurstCompile]
        private void Execute(Entity entity, ref DamageComponent damage)
        {
            Ecb.SetComponentEnabled<DamageComponent>(entity, false); /*TODO: when a creep spawns*/
        }
    }
}