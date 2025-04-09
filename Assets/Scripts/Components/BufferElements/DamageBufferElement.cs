using Unity.Entities;

[InternalBufferCapacity(99)]
public struct DamageBufferElement : IBufferElementData
{
    public int Id;
}