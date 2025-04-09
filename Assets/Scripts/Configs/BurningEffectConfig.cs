using Unity.Entities;
using UnityEngine;

[CreateAssetMenu(fileName = "Burn", menuName = "ScriptableObjects/BurningElementConfig", order = 1)]
public class BurningEffectConfig : AbstractEffectConfig
{
    public int Damage;
    public float Timer;

    public override void AppendToBuffer(Entity entity, EntityCommandBuffer ecb)
    {
        ecb.AppendToBuffer(entity, new BurningBufferElement { Id = Id });
    }
}