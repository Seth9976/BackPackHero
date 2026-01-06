using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.VisualScripting.FullSerializer
{
	// Token: 0x02000188 RID: 392
	public class GUIStyleState_DirectConverter : fsDirectConverter<GUIStyleState>
	{
		// Token: 0x06000A6A RID: 2666 RVA: 0x0002B926 File Offset: 0x00029B26
		protected override fsResult DoSerialize(GUIStyleState model, Dictionary<string, fsData> serialized)
		{
			return fsResult.Success + base.SerializeMember<Texture2D>(serialized, null, "background", model.background) + base.SerializeMember<Color>(serialized, null, "textColor", model.textColor);
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x0002B960 File Offset: 0x00029B60
		protected override fsResult DoDeserialize(Dictionary<string, fsData> data, ref GUIStyleState model)
		{
			fsResult success = fsResult.Success;
			Texture2D background = model.background;
			fsResult fsResult = success + base.DeserializeMember<Texture2D>(data, null, "background", out background);
			model.background = background;
			Color textColor = model.textColor;
			fsResult fsResult2 = fsResult + base.DeserializeMember<Color>(data, null, "textColor", out textColor);
			model.textColor = textColor;
			return fsResult2;
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x0002B9BA File Offset: 0x00029BBA
		public override object CreateInstance(fsData data, Type storageType)
		{
			return new GUIStyleState();
		}
	}
}
