using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

[BurstCompile]
public partial struct BurningSystem : ISystem
{
    private EntityQuery _queryBurningComponent;

    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        var ecb = new EntityCommandBuffer(Allocator.TempJob);
        var queries = new NativeArray<ComponentType>(3, Allocator.Temp);
        queries[0] = ComponentType.ReadOnly<BurningComponent>();
        queries[1] = ComponentType.ReadOnly<EnemyHpComponent>();
        queries[2] = ComponentType.ReadOnly<TimerComponent>();
        _queryBurningComponent = state.GetEntityQuery(queries);
        new OffBurningComponentJob { Ecb = ecb }.Run(_queryBurningComponent);
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var ecb = new EntityCommandBuffer(Allocator.TempJob);
        new BurningJob { Ecb = ecb }.Run(_queryBurningComponent);
        state.Dependency.Complete();
        ecb.Playback(state.EntityManager);
        ecb.Dispose();
    }
}

//[BurstCompile]
public partial struct BurningJob : IJobEntity
{
    public EntityCommandBuffer Ecb;

    //[BurstCompile]
    private void Execute(Entity entity, ref BurningComponent damage, ref EnemyHpComponent hp, ref TimerComponent timer)
    {
        var timerBurning = damage.Timer;

        if (!timer.Trigger)
        {
            return;
        }

        if (timerBurning >= 0)
        {
            var timerResult = new BurningComponent { BurningDamage = damage.BurningDamage, Timer = timerBurning - 1 };
            var resHp = hp.Hp - damage.BurningDamage;
            var hpResult = new EnemyHpComponent { Hp = resHp, MaxHp = hp.MaxHp };
            Ecb.SetComponent(entity, timerResult);
            Ecb.SetComponent(entity, hpResult);
        }
        else
        {
            Ecb.SetComponentEnabled<BurningComponent>(entity, false);
        }
    }
}

[BurstCompile]
public partial struct OffBurningComponentJob : IJobEntity
{
    public EntityCommandBuffer Ecb;

    private void Execute(Entity entity, ref BurningComponent damage)
    {
        Ecb.SetComponentEnabled<BurningComponent>(entity, false); /*TODO: when a creep spawns*/
    }
}