using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.VisualScripting.FullSerializer
{
	// Token: 0x0200018D RID: 397
	public class RectOffset_DirectConverter : fsDirectConverter<RectOffset>
	{
		// Token: 0x06000A7F RID: 2687 RVA: 0x0002C3B4 File Offset: 0x0002A5B4
		protected override fsResult DoSerialize(RectOffset model, Dictionary<string, fsData> serialized)
		{
			return fsResult.Success + base.SerializeMember<int>(serialized, null, "bottom", model.bottom) + base.SerializeMember<int>(serialized, null, "left", model.left) + base.SerializeMember<int>(serialized, null, "right", model.right) + base.SerializeMember<int>(serialized, null, "top", model.top);
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x0002C428 File Offset: 0x0002A628
		protected override fsResult DoDeserialize(Dictionary<string, fsData> data, ref RectOffset model)
		{
			fsResult success = fsResult.Success;
			int bottom = model.bottom;
			fsResult fsResult = success + base.DeserializeMember<int>(data, null, "bottom", out bottom);
			model.bottom = bottom;
			int left = model.left;
			fsResult fsResult2 = fsResult + base.DeserializeMember<int>(data, null, "left", out left);
			model.left = left;
			int right = model.right;
			fsResult fsResult3 = fsResult2 + base.DeserializeMember<int>(data, null, "right", out right);
			model.right = right;
			int top = model.top;
			fsResult fsResult4 = fsResult3 + base.DeserializeMember<int>(data, null, "top", out top);
			model.top = top;
			return fsResult4;
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x0002C4CA File Offset: 0x0002A6CA
		public override object CreateInstance(fsData data, Type storageType)
		{
			return new RectOffset();
		}
	}
}
