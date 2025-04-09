using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class CastleAuthoring : MonoBehaviour
{
    public List<float3> Path;
}

class CastleAuthoringBaker : Baker<CastleAuthoring>
{
    public override void Bake(CastleAuthoring authoring)
    {
        var castleEntity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent<CastleComponent>(castleEntity);
        AddComponent<OffSceneComponent>(castleEntity);

        DynamicBuffer<WayPointsBufferElements> path = AddBuffer<WayPointsBufferElements>(castleEntity);
        foreach (var point in authoring.Path)
        {
            WayPointsBufferElements wp = default;
            wp.Value = point;
            path.Add(wp);
        }
    }
}