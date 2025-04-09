using Unity.Entities;

public struct DamageComponent : IEnableableComponent, IComponentData
{
    public int Damage;
}