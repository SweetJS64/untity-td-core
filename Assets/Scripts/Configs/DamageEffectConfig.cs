using Unity.Entities;
using UnityEngine;

[CreateAssetMenu(fileName = "Damage", menuName = "ScriptableObjects/DamageElementConfig", order = 1)]
public class DamageEffectConfig : AbstractEffectConfig
{
    public int Damage;

    public override void AppendToBuffer(Entity entity, EntityCommandBuffer ecb)
    {
        ecb.AppendToBuffer(entity, new DamageBufferElement { Id = Id });
    }
}