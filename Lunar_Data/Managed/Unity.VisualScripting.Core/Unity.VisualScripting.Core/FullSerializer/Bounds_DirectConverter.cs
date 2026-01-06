using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.VisualScripting.FullSerializer
{
	// Token: 0x02000186 RID: 390
	public class Bounds_DirectConverter : fsDirectConverter<Bounds>
	{
		// Token: 0x06000A61 RID: 2657 RVA: 0x0002B6F1 File Offset: 0x000298F1
		protected override fsResult DoSerialize(Bounds model, Dictionary<string, fsData> serialized)
		{
			return fsResult.Success + base.SerializeMember<Vector3>(serialized, null, "center", model.center) + base.SerializeMember<Vector3>(serialized, null, "size", model.size);
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x0002B72C File Offset: 0x0002992C
		protected override fsResult DoDeserialize(Dictionary<string, fsData> data, ref Bounds model)
		{
			fsResult success = fsResult.Success;
			Vector3 center = model.center;
			fsResult fsResult = success + base.DeserializeMember<Vector3>(data, null, "center", out center);
			model.center = center;
			Vector3 size = model.size;
			fsResult fsResult2 = fsResult + base.DeserializeMember<Vector3>(data, null, "size", out size);
			model.size = size;
			return fsResult2;
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x0002B784 File Offset: 0x00029984
		public override object CreateInstance(fsData data, Type storageType)
		{
			return default(Bounds);
		}
	}
}
