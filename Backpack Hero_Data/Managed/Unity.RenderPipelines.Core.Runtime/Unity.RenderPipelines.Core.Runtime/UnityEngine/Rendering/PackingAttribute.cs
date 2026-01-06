using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000089 RID: 137
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
	public class PackingAttribute : Attribute
	{
		// Token: 0x0600040A RID: 1034 RVA: 0x00014890 File Offset: 0x00012A90
		public PackingAttribute(string[] displayNames, FieldPacking packingScheme = FieldPacking.NoPacking, int bitSize = 32, int offsetInSource = 0, float minValue = 0f, float maxValue = 1f, bool isDirection = false, bool sRGBDisplay = false, bool checkIsNormalized = false, string preprocessor = "")
		{
			this.displayNames = displayNames;
			this.packingScheme = packingScheme;
			this.offsetInSource = offsetInSource;
			this.isDirection = isDirection;
			this.sRGBDisplay = sRGBDisplay;
			this.checkIsNormalized = checkIsNormalized;
			this.sizeInBits = bitSize;
			this.range = new float[] { minValue, maxValue };
			this.preprocessor = preprocessor;
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x000148F8 File Offset: 0x00012AF8
		public PackingAttribute(string displayName = "", FieldPacking packingScheme = FieldPacking.NoPacking, int bitSize = 0, int offsetInSource = 0, float minValue = 0f, float maxValue = 1f, bool isDirection = false, bool sRGBDisplay = false, bool checkIsNormalized = false, string preprocessor = "")
		{
			this.displayNames = new string[1];
			this.displayNames[0] = displayName;
			this.packingScheme = packingScheme;
			this.offsetInSource = offsetInSource;
			this.isDirection = isDirection;
			this.sRGBDisplay = sRGBDisplay;
			this.checkIsNormalized = checkIsNormalized;
			this.sizeInBits = bitSize;
			this.range = new float[] { minValue, maxValue };
			this.preprocessor = preprocessor;
		}

		// Token: 0x040002CC RID: 716
		public string[] displayNames;

		// Token: 0x040002CD RID: 717
		public float[] range;

		// Token: 0x040002CE RID: 718
		public FieldPacking packingScheme;

		// Token: 0x040002CF RID: 719
		public int offsetInSource;

		// Token: 0x040002D0 RID: 720
		public int sizeInBits;

		// Token: 0x040002D1 RID: 721
		public bool isDirection;

		// Token: 0x040002D2 RID: 722
		public bool sRGBDisplay;

		// Token: 0x040002D3 RID: 723
		public bool checkIsNormalized;

		// Token: 0x040002D4 RID: 724
		public string preprocessor;
	}
}
