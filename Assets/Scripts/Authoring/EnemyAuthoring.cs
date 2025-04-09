using Unity.Entities;
using UnityEngine;

public class EnemyAuthoring : MonoBehaviour
{
    public int MaxHp = 500;
    public int Id = 0;
    public float Direction = 5;

    public float Speed = 4;
    //TODO: Add animationPrefab
    //public GameObject Prefab;
}

public class EnemyBaker : Baker<EnemyAuthoring>
{
    public override void Bake(EnemyAuthoring authoring)
    {
        var enemyEntity = GetEntity(TransformUsageFlags.Dynamic);
        
        AddComponent<OffSceneComponent>(enemyEntity);
        AddComponent(enemyEntity, new EnemyHpComponent { Hp = authoring.MaxHp, MaxHp = authoring.MaxHp });
        AddComponent(enemyEntity, new DirectionComponent { Direction = authoring.Direction });
        AddComponent(enemyEntity, new EnemyIdComponent { Id = authoring.Id });
        AddComponent(enemyEntity, new TargetIdComponent { Id = authoring.Id });
        AddComponent<TimerComponent>(enemyEntity);
        AddComponent<DamageComponent>(enemyEntity);
        AddComponent<BurningComponent>(enemyEntity);
        AddBuffer<DamageBufferElement>(enemyEntity);
        AddBuffer<BurningBufferElement>(enemyEntity);
        if (authoring.Speed > 0)
        {
            SpeedComponent speed = default;
            speed.Value = authoring.Speed;
            AddComponent(enemyEntity, speed);
        }

        //TODO: Add animationPrefab
        // PresentationGoComponent pgo = new PresentationGoComponent();
        // pgo.Prefab = authoring.Prefab;
        // AddComponentObject(pgo);
    }
}