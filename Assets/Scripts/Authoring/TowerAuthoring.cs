using Unity.Entities;
using UnityEngine;

class TowerAuthoring : MonoBehaviour
{
    public GameObject BulletPrefab;
    public float TowerSpeedAttack = 1;
}

class TowerBaker : Baker<TowerAuthoring>
{
    public override void Bake(TowerAuthoring authoring)
    {
        var towerEntity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(towerEntity, new TowerComponent { BulletPrefab = GetEntity(authoring.BulletPrefab, TransformUsageFlags.Dynamic) });
        AddComponent(towerEntity, new TowerSpeedAttack { Value = authoring.TowerSpeedAttack });
        AddComponent<TimerComponent>(towerEntity);
        
        AddComponent<OffSceneComponent>(towerEntity);
    }
}