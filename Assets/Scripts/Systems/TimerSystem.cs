using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

[BurstCompile]
public partial struct TimerSystem : ISystem
{
    private EntityQuery _queryTimerComponent;

    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        _queryTimerComponent = state.GetEntityQuery(ComponentType.ReadOnly<TimerComponent>());
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var ecb = new EntityCommandBuffer(Allocator.TempJob);
        var dt = SystemAPI.Time.DeltaTime;
        new TimerJob { Dt = dt, Ecb = ecb }.Run(_queryTimerComponent);
        state.Dependency.Complete();
        ecb.Playback(state.EntityManager);
        ecb.Dispose();
    }
}

[BurstCompile]
public partial struct TimerJob : IJobEntity
{
    public float Dt;
    public EntityCommandBuffer Ecb;

    [BurstCompile]
    private void Execute(Entity entity, ref TimerComponent timer)
    {
        if (!timer.Condition) return;
        var timeout = timer.Time - Dt;
        var delay = timer.Delay;
        if (timeout <= 0)
        {
            SetTime(delay, delay, true, entity);
        }
        else
        {
            SetTime(timeout, delay, false, entity);
        }
    }

    [BurstCompile]
    void SetTime(float time, float delay, bool result, Entity entity)
    {
        var updateTime = new TimerComponent { Time = time, Trigger = result, Condition = true, Delay = delay };
        Ecb.SetComponent(entity, updateTime);
    }
}