using Unity.Entities;

public struct TimerComponent : IComponentData
{
    public float Time;
    public float Delay;
    public bool Trigger;
    public bool Condition;
}