using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020000BB RID: 187
	public struct TextShadow : IEquatable<TextShadow>
	{
		// Token: 0x06000630 RID: 1584 RVA: 0x000170B0 File Offset: 0x000152B0
		public override bool Equals(object obj)
		{
			return obj is TextShadow && this.Equals((TextShadow)obj);
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x000170DC File Offset: 0x000152DC
		public bool Equals(TextShadow other)
		{
			return other.offset == this.offset && other.blurRadius == this.blurRadius && other.color == this.color;
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x00017124 File Offset: 0x00015324
		public override int GetHashCode()
		{
			int num = 1500536833;
			num = num * -1521134295 + this.offset.GetHashCode();
			num = num * -1521134295 + this.blurRadius.GetHashCode();
			return num * -1521134295 + this.color.GetHashCode();
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x00017188 File Offset: 0x00015388
		public static bool operator ==(TextShadow style1, TextShadow style2)
		{
			return style1.Equals(style2);
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x000171A4 File Offset: 0x000153A4
		public static bool operator !=(TextShadow style1, TextShadow style2)
		{
			return !(style1 == style2);
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x000171C0 File Offset: 0x000153C0
		public override string ToString()
		{
			return string.Format("offset={0}, blurRadius={1}, color={2}", this.offset, this.blurRadius, this.color);
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x00017200 File Offset: 0x00015400
		internal static TextShadow LerpUnclamped(TextShadow a, TextShadow b, float t)
		{
			return new TextShadow
			{
				offset = Vector2.LerpUnclamped(a.offset, b.offset, t),
				blurRadius = Mathf.LerpUnclamped(a.blurRadius, b.blurRadius, t),
				color = Color.LerpUnclamped(a.color, b.color, t)
			};
		}

		// Token: 0x04000278 RID: 632
		public Vector2 offset;

		// Token: 0x04000279 RID: 633
		public float blurRadius;

		// Token: 0x0400027A RID: 634
		public Color color;
	}
}
