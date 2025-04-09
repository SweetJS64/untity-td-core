using Unity.Entities;
using UnityEngine;

public struct BurningComponent : IEnableableComponent, IComponentData
{
    public int BurningDamage;
    public float Timer;
}