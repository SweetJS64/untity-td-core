// using Unity.Collections;
// using Unity.Entities;
// using Unity.Mathematics;
// using Unity.Transforms;
// using UnityEngine;
//
// public readonly partial struct PathFollowerAspect : IAspect
// {
//     [Optional]
//     readonly RefRO<SpeedComponent> speed;
//     readonly RefRW<NextPathIndexComponent> pathIndex;
//     [ReadOnly]
//     readonly DynamicBuffer<WayPointsComponent> path;
//     readonly TransformAspect transform;
//
//     public void FollowPath(float time)
//     {
//         //Debug.Log("FollowPath");
//         float3 direction = path[pathIndex.ValueRO.Value].Value - transform.Position;
//         if (math.distance(transform.Position, path[pathIndex.ValueRO.Value].Value) < 0.1f)
//         {
//             pathIndex.ValueRW.Value = (pathIndex.ValueRO.Value + 1) % path.Length;
//         }
//         float movementSpeed = speed.IsValid ? speed.ValueRO.Value : 1;
//         transform.Position += math.normalize(direction) * time * movementSpeed;
//         
//
//     }
//
//     public bool HasReachedEndOfPath()
//     {
//         return math.distance(transform.Position, path[path.Length - 1].Value) < 0.1f;
//     }
//
// }