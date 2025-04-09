using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Burst;
using UnityEngine;

[BurstCompile]
public partial struct MoveBulletSystem : ISystem
{
    private EntityQuery _queryTargetId;
    private EntityQuery _queryEnemy;

    public void OnCreate(ref SystemState state)
    {
        var queryBullet = new NativeArray<ComponentType>(4, Allocator.Temp);
        queryBullet[0] = ComponentType.ReadOnly<LocalTransform>();
        queryBullet[1] = ComponentType.ReadOnly<TargetIdComponent>();
        queryBullet[2] = ComponentType.ReadOnly<BulletComponent>();
        queryBullet[3] = ComponentType.ReadOnly<SpeedComponent>();
        _queryTargetId = state.GetEntityQuery(queryBullet);
        var queryEnemies = new NativeArray<ComponentType>(4, Allocator.Temp);
        queryEnemies[0] = ComponentType.ReadOnly<EnemyIdComponent>();
        queryEnemies[1] = ComponentType.ReadOnly<LocalTransform>();
        queryEnemies[2] = ComponentType.ReadOnly<DamageBufferElement>();
        queryEnemies[3] = ComponentType.ReadOnly<BurningBufferElement>();
        _queryEnemy = state.GetEntityQuery(queryEnemies);
    }

    //[BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var enemyIds = _queryEnemy.ToComponentDataArray<EnemyIdComponent>(Allocator.TempJob);
        var ecb = new EntityCommandBuffer(Allocator.TempJob);
        var dt = SystemAPI.Time.DeltaTime;
        if (enemyIds.Length <= 0) return;
        new MoveBulletJob
        {
            Dt = dt,
            Ecb = ecb,
            EnemyIds = enemyIds,
            EnemyTransforms = _queryEnemy.ToComponentDataArray<LocalTransform>(Allocator.TempJob),
            EnemyEntityArray = _queryEnemy.ToEntityArray(Allocator.TempJob)
        }.Run(_queryTargetId);
        state.Dependency.Complete();
        ecb.Playback(state.EntityManager);
        ecb.Dispose();
    }
}

public partial struct MoveBulletJob : IJobEntity
{
    public float Dt;
    public EntityCommandBuffer Ecb;
    public NativeArray<EnemyIdComponent> EnemyIds;
    public NativeArray<LocalTransform> EnemyTransforms;
    public NativeArray<Entity> EnemyEntityArray;

    private void Execute(Entity entity, ref LocalTransform bulletTransform, in TargetIdComponent bullet,
        in BulletComponent bulletInfo, ref SpeedComponent bulletSpeed)
    {
        var mapping = AbstractEffectConfig.Mapping;
        var enemyIndex = IndexOf(EnemyIds, bullet.Id);
        if (enemyIndex != -1)
        {
            var enemyTransform = EnemyTransforms[enemyIndex];
            var enemyEntity = EnemyEntityArray[enemyIndex];
            var direction = math.normalize(enemyTransform.Position - bulletTransform.Position);
            bulletTransform.Position += direction * Dt * bulletSpeed.Value;
            var distance = math.distancesq(bulletTransform.Position, enemyTransform.Position);
            if (!(distance < 0.1f)) return;
            Ecb.AddComponent(entity, new DestroyComponent());
            foreach (var effect in bulletInfo.ListEffects)
            {
                if (!mapping.ContainsKey(effect)) return;
                mapping[effect].AppendToBuffer(enemyEntity, Ecb);
            }
        }
        else
        {
            Ecb.AddComponent(entity, new DestroyComponent());
        }
    }

    private int IndexOf(NativeArray<EnemyIdComponent> array, int id)
    {
        for (var i = 0; i < array.Length; i++)
        {
            if (array[i].Id == id) return i;
        }

        return -1;
    }
}