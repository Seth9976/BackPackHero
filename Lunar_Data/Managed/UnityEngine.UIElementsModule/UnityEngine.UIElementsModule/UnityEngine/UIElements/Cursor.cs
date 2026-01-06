using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000014 RID: 20
	public struct Cursor : IEquatable<Cursor>
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00003F8F File Offset: 0x0000218F
		// (set) Token: 0x06000088 RID: 136 RVA: 0x00003F97 File Offset: 0x00002197
		public Texture2D texture { readonly get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00003FA0 File Offset: 0x000021A0
		// (set) Token: 0x0600008A RID: 138 RVA: 0x00003FA8 File Offset: 0x000021A8
		public Vector2 hotspot { readonly get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00003FB1 File Offset: 0x000021B1
		// (set) Token: 0x0600008C RID: 140 RVA: 0x00003FB9 File Offset: 0x000021B9
		internal int defaultCursorId { readonly get; set; }

		// Token: 0x0600008D RID: 141 RVA: 0x00003FC4 File Offset: 0x000021C4
		public override bool Equals(object obj)
		{
			return obj is Cursor && this.Equals((Cursor)obj);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003FF0 File Offset: 0x000021F0
		public bool Equals(Cursor other)
		{
			return EqualityComparer<Texture2D>.Default.Equals(this.texture, other.texture) && this.hotspot.Equals(other.hotspot) && this.defaultCursorId == other.defaultCursorId;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00004044 File Offset: 0x00002244
		public override int GetHashCode()
		{
			int num = 1500536833;
			num = num * -1521134295 + EqualityComparer<Texture2D>.Default.GetHashCode(this.texture);
			num = num * -1521134295 + EqualityComparer<Vector2>.Default.GetHashCode(this.hotspot);
			return num * -1521134295 + this.defaultCursorId.GetHashCode();
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000040A8 File Offset: 0x000022A8
		public static bool operator ==(Cursor style1, Cursor style2)
		{
			return style1.Equals(style2);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000040C4 File Offset: 0x000022C4
		public static bool operator !=(Cursor style1, Cursor style2)
		{
			return !(style1 == style2);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000040E0 File Offset: 0x000022E0
		public override string ToString()
		{
			return string.Format("texture={0}, hotspot={1}", this.texture, this.hotspot);
		}
	}
}
