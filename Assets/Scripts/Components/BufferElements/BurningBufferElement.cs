using Unity.Entities;
using UnityEngine;

[InternalBufferCapacity(99)]
public struct BurningBufferElement : IBufferElementData
{
    public int Id;
}