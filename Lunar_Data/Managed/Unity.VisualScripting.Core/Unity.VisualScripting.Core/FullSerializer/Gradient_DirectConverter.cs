using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.VisualScripting.FullSerializer
{
	// Token: 0x02000187 RID: 391
	public class Gradient_DirectConverter : fsDirectConverter<Gradient>
	{
		// Token: 0x06000A65 RID: 2661 RVA: 0x0002B7A8 File Offset: 0x000299A8
		protected override fsResult DoSerialize(Gradient model, Dictionary<string, fsData> serialized)
		{
			fsResult fsResult = fsResult.Success;
			fsResult += base.SerializeMember<GradientAlphaKey[]>(serialized, null, "alphaKeys", model.alphaKeys);
			fsResult += base.SerializeMember<GradientColorKey[]>(serialized, null, "colorKeys", model.colorKeys);
			try
			{
				fsResult += base.SerializeMember<GradientMode>(serialized, null, "mode", model.mode);
			}
			catch (Exception)
			{
				Gradient_DirectConverter.LogWarning("serialized");
			}
			return fsResult;
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x0002B82C File Offset: 0x00029A2C
		protected override fsResult DoDeserialize(Dictionary<string, fsData> data, ref Gradient model)
		{
			fsResult fsResult = fsResult.Success;
			GradientAlphaKey[] alphaKeys = model.alphaKeys;
			fsResult += base.DeserializeMember<GradientAlphaKey[]>(data, null, "alphaKeys", out alphaKeys);
			model.alphaKeys = alphaKeys;
			GradientColorKey[] colorKeys = model.colorKeys;
			fsResult += base.DeserializeMember<GradientColorKey[]>(data, null, "colorKeys", out colorKeys);
			model.colorKeys = colorKeys;
			try
			{
				GradientMode mode = model.mode;
				fsResult += base.DeserializeMember<GradientMode>(data, null, "mode", out mode);
				model.mode = mode;
			}
			catch (Exception)
			{
				Gradient_DirectConverter.LogWarning("deserialized");
			}
			return fsResult;
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x0002B8D4 File Offset: 0x00029AD4
		private static void LogWarning(string phase)
		{
			string text = "2021.3.9f1";
			Debug.LogWarning(string.Concat(new string[] { "Gradient.mode could not be ", phase, ". Please use Unity ", text, " or newer to resolve this issue." }));
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x0002B917 File Offset: 0x00029B17
		public override object CreateInstance(fsData data, Type storageType)
		{
			return new Gradient();
		}
	}
}
