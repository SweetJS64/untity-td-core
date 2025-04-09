using Unity.Entities;
using UnityEngine;

public struct StorageEntitiesComponent : IComponentData
{
    public Entity SimpleTowerPrefab;
    public Entity FireTowerPrefab;
    public Entity EnemyPrefab;
    public Entity SpawnerPrefab;
    public Entity CastlePrefab;
}