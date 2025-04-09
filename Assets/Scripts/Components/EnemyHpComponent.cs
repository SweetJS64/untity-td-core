using Unity.Entities;

public struct EnemyHpComponent : IComponentData
{
    public int Hp;
    public int MaxHp;
}