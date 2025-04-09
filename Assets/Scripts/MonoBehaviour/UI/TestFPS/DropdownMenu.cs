using TMPro;
using Unity.Entities;
using UnityEngine;
using Random = Unity.Mathematics.Random;
using Unity.Transforms;
using Unity.Mathematics;

public class DropdownMenu : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _quantityTowersMenu;
    private int _valueTowerMenu;
    [SerializeField] private TMP_Dropdown _quantityEnemiesMenu;
    private int _valueEnemiesMenu;
    [SerializeField] private TMP_Dropdown _speedShootMenu;
    private int _valueSpeedShootMenu;

    [SerializeField] private float _minSpawnX = -8;
    [SerializeField] private float _maxSpawnX = 8;
    [SerializeField] private float _minSpawnY = -4;
    [SerializeField] private float _maxSpawnY = 2;

    private EntityManager _entityManager;
    private Entity _entityStorage;
    private Entity _entityTower;
    private Entity _entityEnemy;

    private int _quantityTowers;
    private int _quantityEnemies;
    private float _speedShoot;

    private Random _random;

    public void Start()
    {
        _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
    }

    public void OnButtonSpawn()
    {
        _valueTowerMenu = _quantityTowersMenu.value;
        if (_valueTowerMenu == 0)
        {
            _quantityTowers = 0;
        }

        if (_valueTowerMenu == 1)
        {
            _quantityTowers = 10;
        }

        if (_valueTowerMenu == 2)
        {
            _quantityTowers = 100;
        }

        if (_valueTowerMenu == 3)
        {
            _quantityTowers = 500;
        }

        if (_valueTowerMenu == 4)
        {
            _quantityTowers = 1000;
        }

        _valueEnemiesMenu = _quantityEnemiesMenu.value;
        if (_valueEnemiesMenu == 0)
        {
            _quantityEnemies = 0;
        }

        if (_valueEnemiesMenu == 1)
        {
            _quantityEnemies = 10;
        }

        if (_valueEnemiesMenu == 2)
        {
            _quantityEnemies = 100;
        }

        if (_valueEnemiesMenu == 3)
        {
            _quantityEnemies = 500;
        }

        _valueSpeedShootMenu = _speedShootMenu.value;
        if (_valueSpeedShootMenu == 0)
        {
            _speedShoot = 0f;
        }

        if (_valueSpeedShootMenu == 1)
        {
            _speedShoot = 0.25f;
        }

        if (_valueSpeedShootMenu == 2)
        {
            _speedShoot = 0.5f;
        }

        if (_valueSpeedShootMenu == 3)
        {
            _speedShoot = 2f;
        }

        _entityStorage = _entityManager.CreateEntityQuery(typeof(StorageEntitiesComponent))
            .GetSingletonEntity();

        var storageComponent = _entityManager.GetComponentData<StorageEntitiesComponent>(_entityStorage);
        _entityTower = storageComponent.SimpleTowerPrefab;
        _entityEnemy = storageComponent.EnemyPrefab;
        var minimumPosition = new float3(_minSpawnX, _minSpawnY, 0);
        var maximumPosition = new float3(_maxSpawnX, _maxSpawnY, 0);

        var timeSeed = (uint)(Time.deltaTime * 100000);
        _random.InitState(timeSeed);

        for (var i = 1; i <= _quantityTowers; i++)
        {
            var posTowerSpawn = _random.NextFloat3(minimumPosition, maximumPosition);
            var setSpawnTowerPosition = new LocalTransform
                { Position = posTowerSpawn, Scale = 0.5f };
            var setSpeedAttack = new TowerSpeedAttack { Value = _speedShoot };
            _entityManager.SetComponentData(_entityTower, setSpawnTowerPosition);
            _entityManager.SetComponentData(_entityTower, setSpeedAttack);
            _entityManager.Instantiate(_entityTower);
        }

        for (var i = 1; i <= _quantityEnemies; i++)
        {
            var posEnemySpawn = _random.NextFloat3(minimumPosition, maximumPosition);
            _entityManager.SetComponentData(_entityEnemy, new LocalTransform
                { Position = posEnemySpawn, Scale = 0.5f });
            _entityManager.SetComponentData(_entityEnemy, new EnemyIdComponent { Id = i });
            _entityManager.Instantiate(_entityEnemy);
        }
    }
}