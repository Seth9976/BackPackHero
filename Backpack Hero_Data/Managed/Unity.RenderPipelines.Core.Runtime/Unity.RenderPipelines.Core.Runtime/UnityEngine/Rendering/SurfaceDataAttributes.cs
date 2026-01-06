using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000087 RID: 135
	[AttributeUsage(AttributeTargets.Field)]
	public class SurfaceDataAttributes : Attribute
	{
		// Token: 0x06000407 RID: 1031 RVA: 0x000147F4 File Offset: 0x000129F4
		public SurfaceDataAttributes(string displayName = "", bool isDirection = false, bool sRGBDisplay = false, FieldPrecision precision = FieldPrecision.Default, bool checkIsNormalized = false, string preprocessor = "")
		{
			this.displayNames = new string[1];
			this.displayNames[0] = displayName;
			this.isDirection = isDirection;
			this.sRGBDisplay = sRGBDisplay;
			this.precision = precision;
			this.checkIsNormalized = checkIsNormalized;
			this.preprocessor = preprocessor;
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x00014842 File Offset: 0x00012A42
		public SurfaceDataAttributes(string[] displayNames, bool isDirection = false, bool sRGBDisplay = false, FieldPrecision precision = FieldPrecision.Default, bool checkIsNormalized = false, string preprocessor = "")
		{
			this.displayNames = displayNames;
			this.isDirection = isDirection;
			this.sRGBDisplay = sRGBDisplay;
			this.precision = precision;
			this.checkIsNormalized = checkIsNormalized;
			this.preprocessor = preprocessor;
		}

		// Token: 0x040002C4 RID: 708
		public string[] displayNames;

		// Token: 0x040002C5 RID: 709
		public bool isDirection;

		// Token: 0x040002C6 RID: 710
		public bool sRGBDisplay;

		// Token: 0x040002C7 RID: 711
		public FieldPrecision precision;

		// Token: 0x040002C8 RID: 712
		public bool checkIsNormalized;

		// Token: 0x040002C9 RID: 713
		public string preprocessor;
	}
}
