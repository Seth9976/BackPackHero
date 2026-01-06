using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.VisualScripting.FullSerializer
{
	// Token: 0x0200018C RID: 396
	public class LayerMask_DirectConverter : fsDirectConverter<LayerMask>
	{
		// Token: 0x06000A7B RID: 2683 RVA: 0x0002C33B File Offset: 0x0002A53B
		protected override fsResult DoSerialize(LayerMask model, Dictionary<string, fsData> serialized)
		{
			return fsResult.Success + base.SerializeMember<int>(serialized, null, "value", model.value);
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x0002C35C File Offset: 0x0002A55C
		protected override fsResult DoDeserialize(Dictionary<string, fsData> data, ref LayerMask model)
		{
			fsResult success = fsResult.Success;
			int value = model.value;
			fsResult fsResult = success + base.DeserializeMember<int>(data, null, "value", out value);
			model.value = value;
			return fsResult;
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x0002C390 File Offset: 0x0002A590
		public override object CreateInstance(fsData data, Type storageType)
		{
			return default(LayerMask);
		}
	}
}
