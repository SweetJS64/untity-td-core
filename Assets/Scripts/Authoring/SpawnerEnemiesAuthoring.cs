using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

class SpawnerEnemiesAuthoring : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public float SpeedSpawn = 2;
}

class SpawnerBaker : Baker<SpawnerEnemiesAuthoring>
{
    public override void Bake(SpawnerEnemiesAuthoring enemiesAuthoring)
    {
        var spawnerEntity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(spawnerEntity, new SpawnerEnemiesComponent
        {
            EnemyPrefab = GetEntity(enemiesAuthoring.EnemyPrefab, TransformUsageFlags.Dynamic),
            SpeedSpawn = enemiesAuthoring.SpeedSpawn
        });
        AddComponent(spawnerEntity, new SpawnCountEnemiesComponent { Count = 0 });
        AddComponent<TimerComponent>(spawnerEntity);
        AddComponent<OffSceneComponent>(spawnerEntity);
    }
}