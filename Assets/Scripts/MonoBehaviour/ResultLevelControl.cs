using Unity.Entities;
using UnityEngine;

public class ResultLevelControl : MonoBehaviour
{
    public GameObject LoseLevel;
    public GameObject VictoryLevel;

    private EntityManager _entityManager;
    private EntityQuery _queryStorage;
    private Entity _entityStorage;

    private int _startLevelHp;
    private int _startWaveLength;
    private int _startCoins;

    private bool _ready;

    void Start()
    {
        _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        _queryStorage = _entityManager.CreateEntityQuery(typeof(StorageStatusLevelComponent));
    }

    void Update()
    {
        if (!_ready)
        {
            if (_queryStorage.CalculateEntityCount() > 0)
            {
                _entityStorage = _queryStorage.GetSingletonEntity();
                _startLevelHp = _entityManager.GetComponentData<StorageLevelHpComponent>(_entityStorage).LevelHp;
                _startWaveLength = _entityManager.GetComponentData<StorageWaveDataComponent>(_entityStorage).StartWaveLength;
                _startCoins = _entityManager.GetComponentData<StorageCoinsComponent>(_entityStorage).Coins;

                _ready = true;
            }
            else return;
        }

        var realHp = _entityManager.GetComponentData<StorageLevelHpComponent>(_entityStorage).LevelHp;
        var status = _entityManager.GetComponentData<StorageStatusLevelComponent>(_entityStorage);
        if (status.Reset) return;

        if (status.Lose)
        {
            LoseLevel.SetActive(true);
        }

        if (status.Victory && realHp > 0)
        {
            VictoryLevel.SetActive(true);
        }
    }

    public void GoToMenu()
    {
        if (!_ready) return;

        _entityManager.World.GetExistingSystemManaged<SimulationSystemGroup>().Enabled = true;

        var conditionComponent = new StorageLevelHpComponent { LevelHp = _startLevelHp };
        var waveLength = new StorageWaveDataComponent
            { WaveLength = _startWaveLength, StartWaveLength = _startWaveLength };
        var coins = new StorageCoinsComponent { Coins = _startCoins };
        var statusLevel = new StorageStatusLevelComponent { Reset = true, Stop = true };

        _entityManager.SetComponentData(_entityStorage, conditionComponent);
        _entityManager.SetComponentData(_entityStorage, waveLength);
        _entityManager.SetComponentData(_entityStorage, coins);
        _entityManager.SetComponentData(_entityStorage, statusLevel);
    }
}