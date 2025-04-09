using Unity.Entities;

public struct SpawnerEnemiesComponent : IComponentData
{
    public Entity EnemyPrefab;
    public float SpeedSpawn;
}