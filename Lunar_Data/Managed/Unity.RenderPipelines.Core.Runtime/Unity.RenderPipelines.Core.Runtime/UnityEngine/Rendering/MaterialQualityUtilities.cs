using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Rendering
{
	// Token: 0x020000AD RID: 173
	[MovedFrom("Utilities")]
	public static class MaterialQualityUtilities
	{
		// Token: 0x060005DF RID: 1503 RVA: 0x0001BE7C File Offset: 0x0001A07C
		public static MaterialQuality GetHighestQuality(this MaterialQuality levels)
		{
			for (int i = MaterialQualityUtilities.Keywords.Length - 1; i >= 0; i--)
			{
				MaterialQuality materialQuality = (MaterialQuality)(1 << i);
				if ((levels & materialQuality) != (MaterialQuality)0)
				{
					return materialQuality;
				}
			}
			return (MaterialQuality)0;
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x0001BEAC File Offset: 0x0001A0AC
		public static MaterialQuality GetClosestQuality(this MaterialQuality availableLevels, MaterialQuality requestedLevel)
		{
			if (availableLevels == (MaterialQuality)0)
			{
				return MaterialQuality.Low;
			}
			int num = requestedLevel.ToFirstIndex();
			MaterialQuality materialQuality = (MaterialQuality)0;
			for (int i = num; i >= 0; i--)
			{
				MaterialQuality materialQuality2 = MaterialQualityUtilities.FromIndex(i);
				if ((materialQuality2 & availableLevels) != (MaterialQuality)0)
				{
					materialQuality = materialQuality2;
					break;
				}
			}
			if (materialQuality != (MaterialQuality)0)
			{
				return materialQuality;
			}
			for (int j = num + 1; j < MaterialQualityUtilities.Keywords.Length; j++)
			{
				MaterialQuality materialQuality3 = MaterialQualityUtilities.FromIndex(j);
				Math.Abs(requestedLevel - materialQuality3);
				if ((materialQuality3 & availableLevels) != (MaterialQuality)0)
				{
					materialQuality = materialQuality3;
					break;
				}
			}
			return materialQuality;
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x0001BF20 File Offset: 0x0001A120
		public static void SetGlobalShaderKeywords(this MaterialQuality level)
		{
			for (int i = 0; i < MaterialQualityUtilities.KeywordNames.Length; i++)
			{
				if ((level & (MaterialQuality)(1 << i)) != (MaterialQuality)0)
				{
					Shader.EnableKeyword(MaterialQualityUtilities.KeywordNames[i]);
				}
				else
				{
					Shader.DisableKeyword(MaterialQualityUtilities.KeywordNames[i]);
				}
			}
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x0001BF64 File Offset: 0x0001A164
		public static void SetGlobalShaderKeywords(this MaterialQuality level, CommandBuffer cmd)
		{
			for (int i = 0; i < MaterialQualityUtilities.KeywordNames.Length; i++)
			{
				if ((level & (MaterialQuality)(1 << i)) != (MaterialQuality)0)
				{
					cmd.EnableShaderKeyword(MaterialQualityUtilities.KeywordNames[i]);
				}
				else
				{
					cmd.DisableShaderKeyword(MaterialQualityUtilities.KeywordNames[i]);
				}
			}
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x0001BFAC File Offset: 0x0001A1AC
		public static int ToFirstIndex(this MaterialQuality level)
		{
			for (int i = 0; i < MaterialQualityUtilities.KeywordNames.Length; i++)
			{
				if ((level & (MaterialQuality)(1 << i)) != (MaterialQuality)0)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x0001BFD8 File Offset: 0x0001A1D8
		public static MaterialQuality FromIndex(int index)
		{
			return (MaterialQuality)(1 << index);
		}

		// Token: 0x04000373 RID: 883
		public static string[] KeywordNames = new string[] { "MATERIAL_QUALITY_LOW", "MATERIAL_QUALITY_MEDIUM", "MATERIAL_QUALITY_HIGH" };

		// Token: 0x04000374 RID: 884
		public static string[] EnumNames = Enum.GetNames(typeof(MaterialQuality));

		// Token: 0x04000375 RID: 885
		public static ShaderKeyword[] Keywords = new ShaderKeyword[]
		{
			new ShaderKeyword(MaterialQualityUtilities.KeywordNames[0]),
			new ShaderKeyword(MaterialQualityUtilities.KeywordNames[1]),
			new ShaderKeyword(MaterialQualityUtilities.KeywordNames[2])
		};
	}
}
