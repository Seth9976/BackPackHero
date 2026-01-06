using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200040D RID: 1037
	public struct ShaderTagId : IEquatable<ShaderTagId>
	{
		// Token: 0x060023C4 RID: 9156 RVA: 0x0003C3F6 File Offset: 0x0003A5F6
		public ShaderTagId(string name)
		{
			this.m_Id = Shader.TagToID(name);
		}

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x060023C5 RID: 9157 RVA: 0x0003C408 File Offset: 0x0003A608
		// (set) Token: 0x060023C6 RID: 9158 RVA: 0x0003C420 File Offset: 0x0003A620
		internal int id
		{
			get
			{
				return this.m_Id;
			}
			set
			{
				this.m_Id = value;
			}
		}

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x060023C7 RID: 9159 RVA: 0x0003C42C File Offset: 0x0003A62C
		public string name
		{
			get
			{
				return Shader.IDToTag(this.id);
			}
		}

		// Token: 0x060023C8 RID: 9160 RVA: 0x0003C44C File Offset: 0x0003A64C
		public override bool Equals(object obj)
		{
			return obj is ShaderTagId && this.Equals((ShaderTagId)obj);
		}

		// Token: 0x060023C9 RID: 9161 RVA: 0x0003C478 File Offset: 0x0003A678
		public bool Equals(ShaderTagId other)
		{
			return this.m_Id == other.m_Id;
		}

		// Token: 0x060023CA RID: 9162 RVA: 0x0003C498 File Offset: 0x0003A698
		public override int GetHashCode()
		{
			int num = 2079669542;
			return num * -1521134295 + this.m_Id.GetHashCode();
		}

		// Token: 0x060023CB RID: 9163 RVA: 0x0003C4C8 File Offset: 0x0003A6C8
		public static bool operator ==(ShaderTagId tag1, ShaderTagId tag2)
		{
			return tag1.Equals(tag2);
		}

		// Token: 0x060023CC RID: 9164 RVA: 0x0003C4E4 File Offset: 0x0003A6E4
		public static bool operator !=(ShaderTagId tag1, ShaderTagId tag2)
		{
			return !(tag1 == tag2);
		}

		// Token: 0x060023CD RID: 9165 RVA: 0x0003C500 File Offset: 0x0003A700
		public static explicit operator ShaderTagId(string name)
		{
			return new ShaderTagId(name);
		}

		// Token: 0x060023CE RID: 9166 RVA: 0x0003C518 File Offset: 0x0003A718
		public static explicit operator string(ShaderTagId tagId)
		{
			return tagId.name;
		}

		// Token: 0x04000D30 RID: 3376
		public static readonly ShaderTagId none;

		// Token: 0x04000D31 RID: 3377
		private int m_Id;
	}
}
