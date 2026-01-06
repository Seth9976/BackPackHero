using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x0200031D RID: 797
	internal struct TextCoreSettings : IEquatable<TextCoreSettings>
	{
		// Token: 0x060019D5 RID: 6613 RVA: 0x0006DAC0 File Offset: 0x0006BCC0
		public override bool Equals(object obj)
		{
			return obj is TextCoreSettings && this.Equals((TextCoreSettings)obj);
		}

		// Token: 0x060019D6 RID: 6614 RVA: 0x0006DAEC File Offset: 0x0006BCEC
		public bool Equals(TextCoreSettings other)
		{
			return other.faceColor == this.faceColor && other.outlineColor == this.outlineColor && other.outlineWidth == this.outlineWidth && other.underlayColor == this.underlayColor && other.underlayOffset == this.underlayOffset && other.underlaySoftness == this.underlaySoftness;
		}

		// Token: 0x060019D7 RID: 6615 RVA: 0x0006DB6C File Offset: 0x0006BD6C
		public override int GetHashCode()
		{
			int num = 75905159;
			num = num * -1521134295 + this.faceColor.GetHashCode();
			num = num * -1521134295 + this.outlineColor.GetHashCode();
			num = num * -1521134295 + this.outlineWidth.GetHashCode();
			num = num * -1521134295 + this.underlayColor.GetHashCode();
			num = num * -1521134295 + this.underlayOffset.x.GetHashCode();
			num = num * -1521134295 + this.underlayOffset.y.GetHashCode();
			return num * -1521134295 + this.underlaySoftness.GetHashCode();
		}

		// Token: 0x04000BB3 RID: 2995
		public Color faceColor;

		// Token: 0x04000BB4 RID: 2996
		public Color outlineColor;

		// Token: 0x04000BB5 RID: 2997
		public float outlineWidth;

		// Token: 0x04000BB6 RID: 2998
		public Color underlayColor;

		// Token: 0x04000BB7 RID: 2999
		public Vector2 underlayOffset;

		// Token: 0x04000BB8 RID: 3000
		public float underlaySoftness;
	}
}
