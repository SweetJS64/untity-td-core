using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[BurstCompile]
public partial struct MoveEnemiesSystem : ISystem
{
    private EntityQuery _queryEnemies;
    private EntityQuery _queryCastles;
    private EntityQuery _queryStorage;

    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        var queryEnemies = new NativeArray<ComponentType>(4, Allocator.Temp);
        queryEnemies[0] = ComponentType.ReadOnly<LocalTransform>();
        queryEnemies[1] = ComponentType.ReadOnly<DirectionComponent>();
        queryEnemies[2] = ComponentType.ReadOnly<TargetIdComponent>();
        queryEnemies[3] = ComponentType.ReadOnly<SpeedComponent>();
        _queryEnemies = state.GetEntityQuery(queryEnemies);
        var queryCastles = new NativeArray<ComponentType>(3, Allocator.Temp);
        queryCastles[0] = ComponentType.ReadWrite<CastleComponent>();
        queryCastles[1] = ComponentType.ReadOnly<LocalTransform>();
        queryCastles[2] = ComponentType.ReadOnly<WayPointsBufferElements>();
        _queryCastles = state.GetEntityQuery(queryCastles);
        _queryStorage = state.GetEntityQuery(
            ComponentType.ReadWrite<StorageLevelHpComponent>());
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        if (_queryStorage.CalculateEntityCount() == 0) return;
        
        var entityStorage = _queryStorage.GetSingletonEntity();
        var castleEntity = _queryCastles.ToEntityArray(Allocator.TempJob); //TODO: Add many castles
        if (castleEntity.Length == 0) return;
        var dt = SystemAPI.Time.DeltaTime;
        var ecb = new EntityCommandBuffer(Allocator.TempJob);

        var dynamicBuffer = state.EntityManager.GetBuffer<WayPointsBufferElements>(castleEntity[0]);
        var floatArray = new NativeArray<float3>(dynamicBuffer.Length, Allocator.TempJob);
        for (var i = 0; i < dynamicBuffer.Length; i++)
        {
            floatArray[i] = dynamicBuffer[i].Value;
        }

        var levelHp = state.EntityManager.GetComponentData<StorageLevelHpComponent>(entityStorage).LevelHp;
        var startWaveLength = state.EntityManager.GetComponentData<StorageWaveDataComponent>(entityStorage)
            .StartWaveLength;
        var waveLength = state.EntityManager.GetComponentData<StorageWaveDataComponent>(entityStorage).WaveLength;
        var castleTransforms = state.EntityManager.GetComponentData<LocalTransform>(castleEntity[0]).Position;
        new MoveEnemyJob
        {
            Dt = dt,
            Ecb = ecb,
            PathArray = floatArray,
            CastleTransforms = castleTransforms,
            EntityStorage = entityStorage,
            LevelHp = levelHp,
            WaveLength = waveLength,
            StartWaveLength = startWaveLength
        }.Run(_queryEnemies);
        state.Dependency.Complete();
        ecb.Playback(state.EntityManager);
        ecb.Dispose();
    }
}

//[BurstCompile]
partial struct MoveEnemyJob : IJobEntity
{
    public float Dt;
    public EntityCommandBuffer Ecb;
    public NativeArray<float3> PathArray;
    public float3 CastleTransforms;

    public Entity EntityStorage;
    public int LevelHp;
    public int WaveLength;
    public int StartWaveLength;

    private void Execute(Entity entity, ref LocalTransform transform, ref DirectionComponent dir,
        ref TargetIdComponent target, ref SpeedComponent speed)
    {
        if (target.Id < PathArray.Length)
        {
            var direction = math.normalize(PathArray[target.Id] - transform.Position);
            transform.Position += direction * Dt * speed.Value;
            var distance = math.distancesq(transform.Position, PathArray[target.Id]);
            if (distance < 0.1) target.Id++;
        }
        else
        {
            var finishPosition = new float3(CastleTransforms.x, CastleTransforms.y - 0.5f,
                CastleTransforms.z);
            var direction = math.normalize(finishPosition - transform.Position);
            transform.Position += direction * Dt * speed.Value;
            var distance = math.distancesq(transform.Position, finishPosition);
            if (distance > 0.1) return;
            var newWaveLength = new StorageWaveDataComponent
            {
                WaveLength = WaveLength - 1,
                StartWaveLength = StartWaveLength
            };
            var levelHpResult = new StorageLevelHpComponent { LevelHp = LevelHp - 1 };
            Ecb.SetComponent(EntityStorage, levelHpResult);
            Ecb.SetComponent(EntityStorage, newWaveLength);
            Ecb.DestroyEntity(entity);
        }
        // for test Scene
        // if ((transform.Position.x > 5 && dir.Direction > 0) ||
        //     (transform.Position.x < -5 && dir.Direction < 0))
        // {
        //     dir.Direction *= -1;
        // }
    }
}
// old path system

// [BurstCompile]
// public partial struct MoveEnemySystem : ISystem
// {
//     public void OnCreate(ref SystemState state)
//     {
//     }
//
//     public void OnDestroy(ref SystemState state)
//     {
//     }
//
//     [BurstCompile]
//     public void OnUpdate(ref SystemState state)
//     {
//         foreach (var pathFollower in SystemAPI.Query<PathFollowerAspect>())
//         {
//             pathFollower.FollowPath(SystemAPI.Time.DeltaTime);
//         }
//     }
// }