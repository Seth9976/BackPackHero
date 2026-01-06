using System;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x0200012E RID: 302
	public class Ray2DConverter : fsDirectConverter<Ray2D>
	{
		// Token: 0x0600083F RID: 2111 RVA: 0x00025447 File Offset: 0x00023647
		protected override fsResult DoSerialize(Ray2D model, Dictionary<string, fsData> serialized)
		{
			return fsResult.Success + base.SerializeMember<Vector2>(serialized, null, "origin", model.origin) + base.SerializeMember<Vector2>(serialized, null, "direction", model.direction);
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x00025480 File Offset: 0x00023680
		protected override fsResult DoDeserialize(Dictionary<string, fsData> data, ref Ray2D model)
		{
			fsResult success = fsResult.Success;
			Vector2 origin = model.origin;
			fsResult fsResult = success + base.DeserializeMember<Vector2>(data, null, "origin", out origin);
			model.origin = origin;
			Vector2 direction = model.direction;
			fsResult fsResult2 = fsResult + base.DeserializeMember<Vector2>(data, null, "direction", out direction);
			model.direction = direction;
			return fsResult2;
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x000254D8 File Offset: 0x000236D8
		public override object CreateInstance(fsData data, Type storageType)
		{
			return default(Ray2D);
		}
	}
}
