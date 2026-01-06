using System;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x0200012F RID: 303
	public class RayConverter : fsDirectConverter<Ray>
	{
		// Token: 0x06000843 RID: 2115 RVA: 0x000254FB File Offset: 0x000236FB
		protected override fsResult DoSerialize(Ray model, Dictionary<string, fsData> serialized)
		{
			return fsResult.Success + base.SerializeMember<Vector3>(serialized, null, "origin", model.origin) + base.SerializeMember<Vector3>(serialized, null, "direction", model.direction);
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x00025534 File Offset: 0x00023734
		protected override fsResult DoDeserialize(Dictionary<string, fsData> data, ref Ray model)
		{
			fsResult success = fsResult.Success;
			Vector3 origin = model.origin;
			fsResult fsResult = success + base.DeserializeMember<Vector3>(data, null, "origin", out origin);
			model.origin = origin;
			Vector3 direction = model.direction;
			fsResult fsResult2 = fsResult + base.DeserializeMember<Vector3>(data, null, "direction", out direction);
			model.direction = direction;
			return fsResult2;
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x0002558C File Offset: 0x0002378C
		public override object CreateInstance(fsData data, Type storageType)
		{
			return default(Ray);
		}
	}
}
