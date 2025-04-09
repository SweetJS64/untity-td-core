// using TMPro;
// using Unity.Entities;
// using UnityEngine;
// using Random = Unity.Mathematics.Random;
// using Unity.Transforms;

// public abstract class AbstractTestConfig : ScriptableObject
// {
//     
// }
//
//
// public class FirstTestConfig : AbstractTestConfig
// {
//     
// }
//
// public class SecondTestConfig : AbstractTestConfig
// {
//     
// }

// // public class CreateEnemySystem : MonoBehaviour
// // {
// //     public Entity Enemy;
// //     //public GameObject EnemyObject;
// //     
// //     [SerializeField]
// //     private int EnemyMaxHp = 500;
// //     [SerializeField]
// //     private int EnemyDirection = 5;
// //     [SerializeField]
// //     private int EnemySpeed = 4;
// //     private int enemyId =0;
// //     [SerializeField]
// //     private GameObject AnimPrefab;
// //     
// //     private EntityManager _entityManager;
// //     private Entity _entityStorage;
// //     private Entity _entityTower;
// //     private Entity _entityEnemy;
// //
// //     public void CreateEnemy(Entity entityEnemy)
// //     {
// //         Debug.Log("StartOnCreate");
// //         var EnemyPrefab = _entityManager.CreateEntity();
// //         
// //         _entityManager.AddComponentData(EnemyPrefab, new EnemyHpComponent { Hp = EnemyMaxHp, MaxHp = EnemyMaxHp });
// //         _entityManager.AddComponentData(EnemyPrefab, new DirectionComponent { Direction = EnemyDirection });
// //         _entityManager.AddComponentData(EnemyPrefab, new EnemyIdComponent { Id = enemyId });
// //         _entityManager.AddComponent<TimerComponent>(EnemyPrefab);
// //
// //         var te = new EnemyIdComponent { Id = enemyId };
// //         _entityManager.AddComponent<EnemyIdComponent>(EnemyPrefab);
// //         _entityManager.AddComponent<DamageComponent>(EnemyPrefab);
// //         _entityManager.AddComponent<BurningComponent>(EnemyPrefab);
// //         _entityManager.AddBuffer<DamageBufferElement>(EnemyPrefab);
// //         _entityManager.AddBuffer<BurningBufferElement>(EnemyPrefab);
// //         if (EnemySpeed > 0)
// //         {
// //             SpeedComponent speed = default;
// //             speed.Value = EnemySpeed;
// //             _entityManager.AddComponentData(EnemyPrefab, speed);
// //         }
// //
// //         PresentationGoComponent pgo = new PresentationGoComponent();
// //         pgo.Prefab = AnimPrefab;
// //         _entityManager.AddComponentObject(EnemyPrefab, pgo);
// //
// //         Enemy = EnemyPrefab;
// //         entityEnemy = EnemyPrefab;
// //     }
// // }