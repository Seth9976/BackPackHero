using System;
using System.Collections.Generic;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000014 RID: 20
	internal static class MaterialManager
	{
		// Token: 0x060000A3 RID: 163 RVA: 0x00006190 File Offset: 0x00004390
		public static Material GetFallbackMaterial(Material sourceMaterial, Material targetMaterial)
		{
			int instanceID = sourceMaterial.GetInstanceID();
			Texture texture = targetMaterial.GetTexture(TextShaderUtilities.ID_MainTex);
			int instanceID2 = texture.GetInstanceID();
			long num = ((long)instanceID << 32) | (long)((ulong)instanceID2);
			Material material;
			bool flag = MaterialManager.s_FallbackMaterials.TryGetValue(num, ref material);
			Material material2;
			if (flag)
			{
				material2 = material;
			}
			else
			{
				bool flag2 = sourceMaterial.HasProperty(TextShaderUtilities.ID_GradientScale) && targetMaterial.HasProperty(TextShaderUtilities.ID_GradientScale);
				if (flag2)
				{
					material = new Material(sourceMaterial);
					material.hideFlags = HideFlags.HideAndDontSave;
					material.SetTexture(TextShaderUtilities.ID_MainTex, texture);
					material.SetFloat(TextShaderUtilities.ID_GradientScale, targetMaterial.GetFloat(TextShaderUtilities.ID_GradientScale));
					material.SetFloat(TextShaderUtilities.ID_TextureWidth, targetMaterial.GetFloat(TextShaderUtilities.ID_TextureWidth));
					material.SetFloat(TextShaderUtilities.ID_TextureHeight, targetMaterial.GetFloat(TextShaderUtilities.ID_TextureHeight));
					material.SetFloat(TextShaderUtilities.ID_WeightNormal, targetMaterial.GetFloat(TextShaderUtilities.ID_WeightNormal));
					material.SetFloat(TextShaderUtilities.ID_WeightBold, targetMaterial.GetFloat(TextShaderUtilities.ID_WeightBold));
				}
				else
				{
					material = new Material(targetMaterial);
				}
				MaterialManager.s_FallbackMaterials.Add(num, material);
				material2 = material;
			}
			return material2;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000062C0 File Offset: 0x000044C0
		public static Material GetFallbackMaterial(FontAsset fontAsset, Material sourceMaterial, int atlasIndex)
		{
			int instanceID = sourceMaterial.GetInstanceID();
			Texture texture = fontAsset.atlasTextures[atlasIndex];
			int instanceID2 = texture.GetInstanceID();
			long num = ((long)instanceID << 32) | (long)((ulong)instanceID2);
			Material material;
			bool flag = MaterialManager.s_FallbackMaterials.TryGetValue(num, ref material);
			Material material2;
			if (flag)
			{
				material2 = material;
			}
			else
			{
				Material material3 = new Material(sourceMaterial);
				material3.SetTexture(TextShaderUtilities.ID_MainTex, texture);
				material3.hideFlags = HideFlags.HideAndDontSave;
				MaterialManager.s_FallbackMaterials.Add(num, material3);
				material2 = material3;
			}
			return material2;
		}

		// Token: 0x04000088 RID: 136
		private static Dictionary<long, Material> s_FallbackMaterials = new Dictionary<long, Material>();
	}
}
