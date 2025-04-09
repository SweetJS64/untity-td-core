// using Unity.Collections;
// using Unity.Entities;
// using Unity.Transforms;
// using UnityEngine;
//
// public partial struct PresentationGOSystem : ISystem
// {
//     private EntityQuery _querySpawnerPosition;
//     
//     public void OnCreate(ref SystemState state)
//     {
//         //TODO: When we add spawn and path
//         //var queries = new NativeArray<ComponentType>(2, Allocator.Temp);
//         //queries[0] = ComponentType.ReadOnly<SpawnerData>();
//         //queries[1] = ComponentType.ReadOnly<LocalToWorldTransform>();
//         //_querySpawnerPosition = state.GetEntityQuery(queries);
//     } 
//     public void OnDestroy(ref SystemState state) { }
//     public void OnUpdate(ref SystemState state)
//     {
//         var ecbBOS = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>()
//             .CreateCommandBuffer(state.WorldUnmanaged);
//         //TODO: When we add spawn and path
//         //var positionSpawn = _querySpawnerPosition.ToComponentDataArray<LocalToWorldTransform>
//              //(Allocator.Temp)[0];
//         
//         foreach(var (pgo,entity) in SystemAPI.Query<PresentationGoComponent>().WithEntityAccess())
//         {
//             //TODO: When we add spawn and path
//             //pgo.Prefab.transform.position = positionSpawn.Value.Position;
//             GameObject go = GameObject.Instantiate(pgo.Prefab);
//             
//             go.AddComponent<EntityGameObjectAuthoring>().AssignEntity(entity, state.World);
//
//             ecbBOS.AddComponent(entity, new TransformGoComponent() { Transform = go.transform });
//             ecbBOS.AddComponent(entity, new AnimatorGoComponent() { Animator = go.GetComponent<Animator>() });
//             ecbBOS.RemoveComponent<PresentationGoComponent>(entity);
//         }
//         //TODO: When we add spawn and path
//         //var ecbEOS = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>()
//           //  .CreateCommandBuffer(state.WorldUnmanaged);
//
//         foreach (var (goTransform,goAnimator,tranform,speed) in SystemAPI.Query<
//                      TransformGoComponent, 
//                      AnimatorGoComponent, 
//                      TransformAspect, 
//                      RefRO<SpeedComponent>>())
//         {
//             goTransform.Transform.position = tranform.Position;
//             //goAnimator.Animator.SetFloat("speed", speed.ValueRO.Value);
//         }
//         //TODO: When we Destroy enemy
//         // foreach(var (goTransform,entity) in SystemAPI.Query<TransformGO>().WithNone<LocalToWorld>().WithEntityAccess())
//         // {
//         //     if (goTransform.Transform != null)
//         //     {
//         //         GameObject.Destroy(goTransform.Transform.gameObject);
//         //     }
//         //     ecbEOS.RemoveComponent<TransformGO>(entity);
//         // }
//     }
// }