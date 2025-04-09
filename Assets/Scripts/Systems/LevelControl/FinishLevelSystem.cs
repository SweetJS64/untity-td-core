using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

[BurstCompile]
public partial struct FinishLevelSystem : ISystem
{
    private EntityQuery _queryStorage;
    private EntityQuery _queryEnemies;

    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        _queryStorage = state.GetEntityQuery(ComponentType.ReadWrite<StorageLevelHpComponent>());
        _queryEnemies = state.GetEntityQuery(ComponentType.ReadWrite<EnemyIdComponent>());
    }
    
    public void OnUpdate(ref SystemState state)
    {
        if (_queryStorage.CalculateEntityCount() == 0) return;
        
        var entityStorage = _queryStorage.GetSingletonEntity();
        var simulationSystemGroup = state.World.GetExistingSystemManaged<SimulationSystemGroup>();

        //victory
        var victoryStatusLevel = new StorageStatusLevelComponent { Victory = true, Stop = true };
        var enemiesArray = _queryEnemies.ToComponentDataArray<EnemyIdComponent>(Allocator.Temp);
        var statusWave = state.EntityManager.GetComponentData<StorageWaveDataComponent>(entityStorage).StopWave;
        if (statusWave && enemiesArray.Length == 0)
        {
            simulationSystemGroup.Enabled = false;
            state.EntityManager.SetComponentData(entityStorage, victoryStatusLevel);
        }


        //lose
        var loseStatusLevel = new StorageStatusLevelComponent { Lose = true, Stop = true };
        var levelHp = state.EntityManager.GetComponentData<StorageLevelHpComponent>(entityStorage).LevelHp;
        if (levelHp > 0) return;
        simulationSystemGroup.Enabled = false;
        state.EntityManager.SetComponentData(entityStorage, loseStatusLevel);
    }
}