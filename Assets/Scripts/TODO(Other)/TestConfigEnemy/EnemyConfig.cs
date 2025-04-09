// using Unity.Entities;
// using Unity.Mathematics;
// using Unity.Transforms;
// using UnityEngine;
// using UnityEngine.SceneManagement;
// using UnityEngine.Serialization;
//
// [CreateAssetMenu(fileName = "EnemyTest", menuName = "ScriptableObjects/TestEnemyConfig", order = 1)]
// public class EnemyConfig : ScriptableObject
// {
//     public int EnemyMaxHp = 500;
//     public int EnemyDirection = 5;
//     public int EnemySpeed = 4;
//     public GameObject AnimPrefab;
//     private EnemyAuthoring enemyAuthoring;
//     public Scene scene;
//     public Entity test;
//     
//     private Unity.Mathematics.Random _random;
//     
//     public void CreateEnemy(EntityManager entityManager, int id, float3 posEnemySpawn)
//     {
//         var enemyUniformScaleTransform = new UniformScaleTransform
//             { Position = posEnemySpawn, Scale = 0.5f };
//         
//         var enemyEntity = entityManager.CreateEntity();
//
//         entityManager.AddComponentData(enemyEntity, new LocalToWorldTransform
//             { Value = enemyUniformScaleTransform });
//          entityManager.AddComponentData(enemyEntity, new EnemyHpComponent
//          {
//              Hp = EnemyMaxHp, MaxHp = EnemyMaxHp
//          });
//          entityManager.AddComponentData(enemyEntity, new DirectionComponent
//          {
//              Direction = EnemyDirection
//          });
//          entityManager.AddComponentData(enemyEntity, new EnemyIdComponent { Id =  id});
//          //entityManager.AddComponentData(enemyEntity, new SceneSectionComponent {} );
//          entityManager.AddComponent<TimerComponent>(enemyEntity);
//          entityManager.AddComponent<DamageComponent>(enemyEntity);
//          entityManager.AddComponent<BurningComponent>(enemyEntity);
//          entityManager.AddBuffer<DamageBufferElement>(enemyEntity);
//          entityManager.AddBuffer<BurningBufferElement>(enemyEntity);
//          //entityManager.AddComponentData(enemyEntity, new SceneTag { SceneEntity = test});
//          //entityManager.AddComponent<SceneTag>(enemyEntity);
//          entityManager.AddComponent<SceneSection>(enemyEntity);
//          //var test = new SceneTag();
//             
//          if (EnemySpeed > 0)
//          {
//              SpeedComponent speed = default;
//              speed.Value = EnemySpeed;
//              entityManager.AddComponentData(enemyEntity, speed);
//          }
//         
//          PresentationGoComponent pgo = new PresentationGoComponent();
//          pgo.Prefab = AnimPrefab;
//          entityManager.AddComponentObject(enemyEntity, pgo);
//     }
// }
