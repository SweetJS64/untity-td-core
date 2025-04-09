//TODO: When we add spawn and path
// using System.Collections;
// using System.Collections.Generic;
// using Unity.Entities;
// using Unity.Transforms;
// using UnityEngine;
//
// public partial struct SpawnerSystem : ISystem
// {
//     private int enemyID;
//     public void OnCreate(ref SystemState state)
//     {
//     }
//
//     public void OnDestroy(ref SystemState state)
//     {
//     }
//
//     public void OnUpdate(ref SystemState state)
//     {
//         var ecb = SystemAPI.GetSingleton<BeginFixedStepSimulationEntityCommandBufferSystem.Singleton>()
//             .CreateCommandBuffer(state.WorldUnmanaged);
//         
//         
//         foreach(var (spawner, pos,path) in SystemAPI.Query
//                 <RefRW<SpawnerData>,
//                 LocalToWorldTransform,
//                 DynamicBuffer<WayPointsComponent>>())
//         {
//             spawner.ValueRW.TimeToNextSpawn -= SystemAPI.Time.DeltaTime;//schetchik timera
//             if(spawner.ValueRO.TimeToNextSpawn < 0)
//             {
//                 spawner.ValueRW.TimeToNextSpawn = spawner.ValueRO.Timer;//update timer
//                 Entity e = spawner.ValueRO.Prefab;//convertirovali prefab v entity 
//                 var setSpawnPosition = new LocalToWorldTransform { Value = pos.Value };//update position spawn
//                 var enemyIdComponent = new EnemyIdComponent{Id = enemyID};//update enemyID
//                 enemyID++;//update enemyID for nextEnemy
//                 ecb.SetComponent(e, enemyIdComponent);//update enemyID in entity
//                 ecb.SetComponent(e, setSpawnPosition);//update spawnPos in entity
//                 var buffer = ecb.AddBuffer<WayPointsComponent>(e);//add buffer waypoints
//                 
//                 buffer.AddRange(path.AsNativeArray());//hz vot, libo dlinna massiva, libo hz???
//                 
//                 ecb.AddComponent<NextPathIndexComponent>(e);// add component for next waypoint
//                 ecb.Instantiate(e);//spawn enemy
//             }
//         }
//     }
// }