using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.VisualScripting.FullSerializer
{
	// Token: 0x02000185 RID: 389
	public class AnimationCurve_DirectConverter : fsDirectConverter<AnimationCurve>
	{
		// Token: 0x06000A5D RID: 2653 RVA: 0x0002B608 File Offset: 0x00029808
		protected override fsResult DoSerialize(AnimationCurve model, Dictionary<string, fsData> serialized)
		{
			return fsResult.Success + base.SerializeMember<Keyframe[]>(serialized, null, "keys", model.keys) + base.SerializeMember<WrapMode>(serialized, null, "preWrapMode", model.preWrapMode) + base.SerializeMember<WrapMode>(serialized, null, "postWrapMode", model.postWrapMode);
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x0002B664 File Offset: 0x00029864
		protected override fsResult DoDeserialize(Dictionary<string, fsData> data, ref AnimationCurve model)
		{
			fsResult success = fsResult.Success;
			Keyframe[] keys = model.keys;
			fsResult fsResult = success + base.DeserializeMember<Keyframe[]>(data, null, "keys", out keys);
			model.keys = keys;
			WrapMode preWrapMode = model.preWrapMode;
			fsResult fsResult2 = fsResult + base.DeserializeMember<WrapMode>(data, null, "preWrapMode", out preWrapMode);
			model.preWrapMode = preWrapMode;
			WrapMode postWrapMode = model.postWrapMode;
			fsResult fsResult3 = fsResult2 + base.DeserializeMember<WrapMode>(data, null, "postWrapMode", out postWrapMode);
			model.postWrapMode = postWrapMode;
			return fsResult3;
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x0002B6E2 File Offset: 0x000298E2
		public override object CreateInstance(fsData data, Type storageType)
		{
			return new AnimationCurve();
		}
	}
}
