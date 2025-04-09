// TODO: fix this class for inheritance
// using System;
// using System.Reflection;
// using Unity.Entities;
//
// public class ConvertableConfig<T> : AbstractEffectConfig where T : unmanaged, IBufferElementData
// {
//     public override void AppendToBuffer(Entity entity, EntityCommandBuffer ecb)
//     {
//         var obj = new T();
//         
//         FieldInfo field = obj.GetType().GetField("Id");
//         
//         if (field != null)
//         {
//             field.SetValue(obj, Id);
//         }
//
//         ecb.AppendToBuffer(entity, obj);
//     }
// }

