using System;
using Unity.Collections;
using Unity.Entities;

public struct BulletComponent : IComponentData
{
    public FixedList128Bytes<int> ListEffects;
}