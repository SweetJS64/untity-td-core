using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

class BulletAuthoring : MonoBehaviour
{
    public float Speed = 10;
    public List<AbstractEffectConfig> ListSo = new();
}

class BulletBaker : Baker<BulletAuthoring>
{
        
    public override void Bake(BulletAuthoring authoring)
    {
        var bulletEntity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent<TargetIdComponent>(bulletEntity);
        AddComponent<OffSceneComponent>(bulletEntity);
        var list = new FixedList128Bytes<int>();
        foreach (var effect in authoring.ListSo)
        {
            list.Add(effect.Id);
        }

        AddComponent(bulletEntity, new BulletComponent { ListEffects = list });
        AddComponent(bulletEntity, new SpeedComponent { Value = authoring.Speed });
    }
}