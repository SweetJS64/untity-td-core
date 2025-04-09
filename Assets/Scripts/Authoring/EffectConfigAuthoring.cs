using Unity.Entities;
using UnityEngine;
using System.Collections.Generic;

public class EffectConfigAuthoring : MonoBehaviour
{
    public List<AbstractEffectConfig> Configs;
}

public class EffectConfigBaker : Baker<EffectConfigAuthoring>
{
    public override void Bake(EffectConfigAuthoring authoring)
    {
        foreach (var config in authoring.Configs)
        {
            // Ты можешь создать буфер и записать туда ID
            var entity = GetEntity(TransformUsageFlags.None);
            AddBuffer<EffectConfigBuffer>(entity).Add(new EffectConfigBuffer { Id = config.Id });
        }
    }
}
