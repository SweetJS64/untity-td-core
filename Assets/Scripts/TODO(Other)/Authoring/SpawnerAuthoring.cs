// TODO: When we add spawn and path
//  using System.Collections;
//  using System.Collections.Generic;
//  using System.Linq;
//  using Unity.Entities;
//  using UnityEngine;
//
//  public class SpawnerAuthoring : MonoBehaviour
//  {
//
//      public GameObject Prefab;
//      public float Timer;
//      public List<Transform> Path = GetComponentsInChildren<Transform>();
//  }
//
//  public class SpawnerBaker : Baker<SpawnerAuthoring>
//  {
//      public override void Bake(SpawnerAuthoring authoring)
//      {
//          var Path = authoring.Prefab.GetComponentsInChildren<Transform>();
//          
//          DynamicBuffer<WayPointsComponent> path = AddBuffer<WayPointsComponent>();
//          foreach(var point in authoring.Path)
//          {
//              WayPointsComponent wp = default;
//              wp.Value = point.position;
//              path.Add(wp);
//          }
//
//          SpawnerData sd = default;
//          sd.Prefab = GetEntity(authoring.Prefab);
//          sd.Timer = authoring.Timer;
//          sd.TimeToNextSpawn = authoring.Timer;
//          AddComponent(sd);
//      }
//  }