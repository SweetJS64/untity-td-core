using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[BurstCompile]
public partial struct SpawnBulletSystem : ISystem
{
    private EntityQuery _queryEnemies;
    private EntityQuery _queryTower;

    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        var towersQuery = new NativeArray<ComponentType>(4, Allocator.Temp);
        towersQuery[0] = ComponentType.ReadOnly<TimerComponent>();
        towersQuery[1] = ComponentType.ReadOnly<LocalTransform>();
        towersQuery[2] = ComponentType.ReadOnly<TowerComponent>();
        towersQuery[3] = ComponentType.ReadOnly<TowerSpeedAttack>();
        _queryTower = state.GetEntityQuery(towersQuery);
        var enemiesQuery = new NativeArray<ComponentType>(2, Allocator.Temp);
        enemiesQuery[0] = ComponentType.ReadOnly<EnemyIdComponent>();
        enemiesQuery[1] = ComponentType.ReadOnly<LocalTransform>();
        _queryEnemies = state.GetEntityQuery(enemiesQuery);
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var enemyIds = _queryEnemies.ToComponentDataArray<EnemyIdComponent>(Allocator.TempJob);

        if (enemyIds.Length == 0) return;

        var ecb = new EntityCommandBuffer(Allocator.TempJob);
        var enemyTransforms = _queryEnemies.ToComponentDataArray<LocalTransform>(Allocator.TempJob);
        new SpawnBulletJob { Transforms = enemyTransforms, EnemyIds = enemyIds, Ecb = ecb }.Run(_queryTower);
        state.Dependency.Complete();
        ecb.Playback(state.EntityManager);
        ecb.Dispose();
    }
}

[BurstCompile]
public partial struct SpawnBulletJob : IJobEntity
{
    public NativeArray<LocalTransform> Transforms;
    public NativeArray<EnemyIdComponent> EnemyIds;
    public EntityCommandBuffer Ecb;

    [BurstCompile]
    private void Execute(Entity entity, ref TimerComponent timerComponent,
        in LocalTransform towerTransform, in TowerComponent towerComponent, in TowerSpeedAttack towerSpeedAttack)
    {
        if (!timerComponent.Condition)
            Ecb.SetComponent(entity, new TimerComponent
            {
                Condition = true, Trigger = false, Time = towerSpeedAttack.Value, Delay = towerSpeedAttack.Value
            });
        if (!timerComponent.Trigger) return;

        var newBullet = Ecb.Instantiate(towerComponent.BulletPrefab);
        var towerFirePosition = new float3(towerTransform.Position.x,
            towerTransform.Position.y + 0.5f,
            towerTransform.Position.z);
        var setSpawnPosition = new LocalTransform
            { Position = towerFirePosition, Scale = 0.2f };
        Ecb.SetComponent(newBullet, setSpawnPosition);
        var minDist = float.MaxValue;
        var enemyId = -1;
        for (var i = 0; i < Transforms.Length; i++)
        {
            var localToWorldTransform = Transforms[i];
            var dist = math.distancesq(towerTransform.Position, localToWorldTransform.Position);
            if (dist < minDist)
            {
                minDist = dist;
                enemyId = EnemyIds[i].Id;
            }
        }

        Ecb.AddComponent(newBullet, new TargetIdComponent { Id = enemyId });
    }
}