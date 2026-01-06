using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.VisualScripting.FullSerializer
{
	// Token: 0x0200018E RID: 398
	public class Rect_DirectConverter : fsDirectConverter<Rect>
	{
		// Token: 0x06000A83 RID: 2691 RVA: 0x0002C4DC File Offset: 0x0002A6DC
		protected override fsResult DoSerialize(Rect model, Dictionary<string, fsData> serialized)
		{
			return fsResult.Success + base.SerializeMember<float>(serialized, null, "xMin", model.xMin) + base.SerializeMember<float>(serialized, null, "yMin", model.yMin) + base.SerializeMember<float>(serialized, null, "xMax", model.xMax) + base.SerializeMember<float>(serialized, null, "yMax", model.yMax);
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x0002C554 File Offset: 0x0002A754
		protected override fsResult DoDeserialize(Dictionary<string, fsData> data, ref Rect model)
		{
			fsResult success = fsResult.Success;
			float xMin = model.xMin;
			fsResult fsResult = success + base.DeserializeMember<float>(data, null, "xMin", out xMin);
			model.xMin = xMin;
			float yMin = model.yMin;
			fsResult fsResult2 = fsResult + base.DeserializeMember<float>(data, null, "yMin", out yMin);
			model.yMin = yMin;
			float xMax = model.xMax;
			fsResult fsResult3 = fsResult2 + base.DeserializeMember<float>(data, null, "xMax", out xMax);
			model.xMax = xMax;
			float yMax = model.yMax;
			fsResult fsResult4 = fsResult3 + base.DeserializeMember<float>(data, null, "yMax", out yMax);
			model.yMax = yMax;
			return fsResult4;
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x0002C5F0 File Offset: 0x0002A7F0
		public override object CreateInstance(fsData data, Type storageType)
		{
			return default(Rect);
		}
	}
}
