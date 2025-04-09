using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;

//TODO: You can make a SO in which the positions of the spawner and castle will be stored.
//I think it should still store the points of the path that the wave follows,
//or you will have to make a separate castle prefab for each level.
//We will not do prefabs, so we will also need to prescribe that these points are read into the castle.
//Perhaps this will be revised when adding multiple mob paths.

[BurstCompile]
public partial struct StartFirstLevelSystem : ISystem
{
    private EntityQuery _queryStorage;

    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        _queryStorage = state.GetEntityQuery(
            ComponentType.ReadWrite<StorageStatusLevelComponent>());
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        if (_queryStorage.CalculateEntityCount() == 0) return;
        
        var entityStorage = _queryStorage.GetSingletonEntity();
        var statusStart = state.EntityManager.GetComponentData<StorageStatusLevelComponent>(entityStorage).Start;
        if (statusStart)
        {
            var ecb = new EntityCommandBuffer(Allocator.TempJob);
            new StartLevelJob
            {
                Ecb = ecb,
                EntityStorage = entityStorage,
            }.Run();
            state.Dependency.Complete();
            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }
    }
}

[BurstCompile]
partial struct StartLevelJob : IJobEntity
{
    public EntityCommandBuffer Ecb;
    public Entity EntityStorage;

    public void Execute(ref StorageEntitiesComponent storageEntities, ref StoragePositionCastleComponent castlePos,
        ref StoragePositionSpawnerComponent spawnerPos)
    {
        var spawnerEntity = Ecb.Instantiate(storageEntities.SpawnerPrefab);
        var setSpawnerPosition = new LocalTransform
            { Position = spawnerPos.Position, Scale = 0.6f };
        Ecb.SetComponent(spawnerEntity, setSpawnerPosition);

        var castleEntity = Ecb.Instantiate(storageEntities.CastlePrefab);
        var setCastlePosition = new LocalTransform
            { Position = castlePos.Position, Scale = 0.15f };
        Ecb.SetComponent(castleEntity, setCastlePosition);

        var statusStart = new StorageStatusLevelComponent { Start = false };
        Ecb.SetComponent(EntityStorage, statusStart);
    }
}