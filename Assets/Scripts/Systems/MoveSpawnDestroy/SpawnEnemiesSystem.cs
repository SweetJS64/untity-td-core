using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[BurstCompile]
public partial struct SpawnEnemiesSystem : ISystem
{
    private EntityQuery _querySpawners;
    private EntityQuery _queryStorage;
    private Entity _entityStorage;

    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        var queries = new NativeArray<ComponentType>(4, Allocator.Temp);
        queries[0] = ComponentType.ReadOnly<SpawnerEnemiesComponent>();
        queries[1] = ComponentType.ReadOnly<SpawnCountEnemiesComponent>();
        queries[2] = ComponentType.ReadOnly<LocalTransform>();
        queries[3] = ComponentType.ReadOnly<TimerComponent>();
        _querySpawners = state.GetEntityQuery(queries);
        _queryStorage = state.GetEntityQuery(ComponentType.ReadWrite<StorageWaveDataComponent>());
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        if (_queryStorage.CalculateEntityCount() == 0) return;

        _entityStorage = _queryStorage.GetSingletonEntity();

        var waveData = state.EntityManager.GetComponentData<StorageWaveDataComponent>(_entityStorage);
        var statusLevel = state.EntityManager.GetComponentData<StorageStatusLevelComponent>(_entityStorage);

        if (waveData.StopWave || statusLevel.Stop) return;

        var ecb = new EntityCommandBuffer(Allocator.TempJob);

        new SpawnJob
        {
            Ecb = ecb,
            StartWaveLength = waveData.StartWaveLength,
            WaveLength = waveData.WaveLength,
            Storage = _entityStorage
        }.Run(_querySpawners);

        state.Dependency.Complete();
        ecb.Playback(state.EntityManager);
        ecb.Dispose();
    }
}

//[BurstCompile]
partial struct SpawnJob : IJobEntity
{
    public EntityCommandBuffer Ecb;
    public int StartWaveLength;
    public int WaveLength;
    public Entity Storage;

    private void Execute(Entity entity, ref SpawnerEnemiesComponent spawnerEnemies,
        ref SpawnCountEnemiesComponent countEnemiesComponent, ref TimerComponent timerComponent,
        ref LocalTransform spawnerTransform)
    {
        if (StartWaveLength <= countEnemiesComponent.Count)
        {
            var stopWave = new StorageWaveDataComponent
            {
                WaveLength = WaveLength, StartWaveLength = StartWaveLength,
                StopWave = true
            };
            Ecb.SetComponent(Storage, stopWave);
            return;
        }

        if (!timerComponent.Condition)
            Ecb.SetComponent(entity, new TimerComponent
            {
                Condition = true, Trigger = false, Time = spawnerEnemies.SpeedSpawn, Delay = spawnerEnemies.SpeedSpawn
            });
        if (!timerComponent.Trigger) return;

        var newEnemy = Ecb.Instantiate(spawnerEnemies.EnemyPrefab);
        var enemySpawnPosition = new float3(spawnerTransform.Position.x + 0.5f,
            spawnerTransform.Position.y - 0.3f,
            spawnerTransform.Position.z);
        var setSpawnPosition = new LocalTransform
            { Position = enemySpawnPosition, Scale = 0.5f };

        Ecb.SetComponent(newEnemy, setSpawnPosition);
        var enemyId = new EnemyIdComponent { Id = countEnemiesComponent.Count };
        Ecb.SetComponent(newEnemy, enemyId);
        countEnemiesComponent.Count++;
    }
}