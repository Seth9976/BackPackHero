using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.VisualScripting.FullSerializer
{
	// Token: 0x0200018B RID: 395
	public class Keyframe_DirectConverter : fsDirectConverter<Keyframe>
	{
		// Token: 0x06000A77 RID: 2679 RVA: 0x0002C1C8 File Offset: 0x0002A3C8
		protected override fsResult DoSerialize(Keyframe model, Dictionary<string, fsData> serialized)
		{
			return fsResult.Success + base.SerializeMember<float>(serialized, null, "time", model.time) + base.SerializeMember<float>(serialized, null, "value", model.value) + base.SerializeMember<int>(serialized, null, "tangentMode", model.tangentMode) + base.SerializeMember<float>(serialized, null, "inTangent", model.inTangent) + base.SerializeMember<float>(serialized, null, "outTangent", model.outTangent);
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x0002C258 File Offset: 0x0002A458
		protected override fsResult DoDeserialize(Dictionary<string, fsData> data, ref Keyframe model)
		{
			fsResult success = fsResult.Success;
			float time = model.time;
			fsResult fsResult = success + base.DeserializeMember<float>(data, null, "time", out time);
			model.time = time;
			float value = model.value;
			fsResult fsResult2 = fsResult + base.DeserializeMember<float>(data, null, "value", out value);
			model.value = value;
			int tangentMode = model.tangentMode;
			fsResult fsResult3 = fsResult2 + base.DeserializeMember<int>(data, null, "tangentMode", out tangentMode);
			model.tangentMode = tangentMode;
			float inTangent = model.inTangent;
			fsResult fsResult4 = fsResult3 + base.DeserializeMember<float>(data, null, "inTangent", out inTangent);
			model.inTangent = inTangent;
			float outTangent = model.outTangent;
			fsResult fsResult5 = fsResult4 + base.DeserializeMember<float>(data, null, "outTangent", out outTangent);
			model.outTangent = outTangent;
			return fsResult5;
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x0002C318 File Offset: 0x0002A518
		public override object CreateInstance(fsData data, Type storageType)
		{
			return default(Keyframe);
		}
	}
}
