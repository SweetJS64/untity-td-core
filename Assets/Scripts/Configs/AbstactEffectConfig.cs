using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public abstract class AbstractEffectConfig : ScriptableObject
{
    public static readonly Dictionary<int, AbstractEffectConfig> Mapping = new();
    private static int _curId;
    public int Id { get; private set; }

    private void OnEnable()
    {
        Id = _curId;
        _curId++;
        Mapping[Id] = this;
    }

    public abstract void AppendToBuffer(Entity entity, EntityCommandBuffer ecb);
}