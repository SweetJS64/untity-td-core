using Unity.Collections;
using Unity.Entities;
using Unity.Burst;

[BurstCompile]
public partial struct DestroySystem : ISystem
{
    private EntityQuery _queryDestroy;

    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        _queryDestroy = state.GetEntityQuery(ComponentType.ReadOnly<DestroyComponent>());
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var ecb = new EntityCommandBuffer(Allocator.TempJob);
        new DestroyJob
        {
            Ecb = ecb,
        }.Run(_queryDestroy);
        state.Dependency.Complete();
        ecb.Playback(state.EntityManager);
        ecb.Dispose();
        
    }
}

[BurstCompile]
public partial struct DestroyJob : IJobEntity
{
    public EntityCommandBuffer Ecb;
    [BurstCompile]
    private void Execute(in Entity entity)
    {
        Ecb.DestroyEntity(entity);
    }
}