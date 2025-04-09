//TODO: When we add spawn and path

using Unity.Entities;
using Unity.Mathematics;

[InternalBufferCapacity(10)]
public struct WayPointsBufferElements : IBufferElementData
{
    public float3 Value;
}