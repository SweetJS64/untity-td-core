using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class StorageDataAuthoring : MonoBehaviour
{
    public int Coins = 50;
    public int LevelHp = 5;
    public int WaveLength = 10;
    public float3 SpawnerPos = new (-7.6f, -3, 0);
    public float3 CastlePos = new (1, 3, 0);
    public GameObject SimpleTowerPrefab;
    public GameObject FireTowerPrefab;
    public GameObject EnemyPrefab;
    public GameObject CastlePrefab;
    public GameObject SpawnerPrefab;
}

class PayManagerBaker : Baker<StorageDataAuthoring>
{
    public override void Bake(StorageDataAuthoring authoring)
    {
        var storageEntity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(storageEntity, new StorageEntitiesComponent
        {
            SimpleTowerPrefab = GetEntity(authoring.SimpleTowerPrefab, TransformUsageFlags.Dynamic),
            FireTowerPrefab = GetEntity(authoring.FireTowerPrefab, TransformUsageFlags.Dynamic),
            EnemyPrefab = GetEntity(authoring.EnemyPrefab, TransformUsageFlags.Dynamic),
            SpawnerPrefab = GetEntity(authoring.SpawnerPrefab, TransformUsageFlags.Dynamic),
            CastlePrefab = GetEntity(authoring.CastlePrefab, TransformUsageFlags.Dynamic)
        });
        AddComponent(storageEntity, new StoragePositionCastleComponent
        { 
            Position = authoring.CastlePos
        });
        AddComponent(storageEntity, new StoragePositionSpawnerComponent
        { 
            Position = authoring.SpawnerPos
        });
        AddComponent(storageEntity, new StorageWaveDataComponent
        {
            WaveLength = authoring.WaveLength,
            StartWaveLength = authoring.WaveLength
        });
        AddComponent(storageEntity, new StorageLevelHpComponent
        {
            LevelHp = authoring.LevelHp
        });
        AddComponent(storageEntity, new StorageCoinsComponent
        {
            Coins = authoring.Coins
        });
        //AddComponent(storageEntity, new StorageStatusStartLevelComponent());
        AddComponent(storageEntity, new StorageStatusLevelComponent());
    }
}