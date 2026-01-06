using System;
using UnityEngine;
using UnityEngine.Events;

namespace Unity.VisualScripting.FullSerializer.Internal.Converters
{
	// Token: 0x020001B2 RID: 434
	public class UnityEvent_Converter : fsConverter
	{
		// Token: 0x06000B99 RID: 2969 RVA: 0x000315BA File Offset: 0x0002F7BA
		public override bool CanProcess(Type type)
		{
			return typeof(UnityEvent).Resolve().IsAssignableFrom(type.Resolve()) && !type.Resolve().IsGenericType;
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x000315E8 File Offset: 0x0002F7E8
		public override bool RequestCycleSupport(Type storageType)
		{
			return false;
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x000315EC File Offset: 0x0002F7EC
		public override fsResult TryDeserialize(fsData data, ref object instance, Type storageType)
		{
			Type type = (Type)instance;
			fsResult success = fsResult.Success;
			instance = JsonUtility.FromJson(fsJsonPrinter.CompressedJson(data), type);
			return success;
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x00031614 File Offset: 0x0002F814
		public override fsResult TrySerialize(object instance, out fsData serialized, Type storageType)
		{
			fsResult success = fsResult.Success;
			serialized = fsJsonParser.Parse(JsonUtility.ToJson(instance));
			return success;
		}
	}
}
