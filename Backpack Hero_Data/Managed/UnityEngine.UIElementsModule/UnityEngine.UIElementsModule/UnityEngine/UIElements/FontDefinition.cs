using System;
using UnityEngine.TextCore.Text;

namespace UnityEngine.UIElements
{
	// Token: 0x02000271 RID: 625
	public struct FontDefinition : IEquatable<FontDefinition>
	{
		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x0600134D RID: 4941 RVA: 0x00052D20 File Offset: 0x00050F20
		// (set) Token: 0x0600134E RID: 4942 RVA: 0x00052D38 File Offset: 0x00050F38
		public Font font
		{
			get
			{
				return this.m_Font;
			}
			set
			{
				bool flag = value != null && this.fontAsset != null;
				if (flag)
				{
					throw new InvalidOperationException("Cannot set both Font and FontAsset on FontDefinition");
				}
				this.m_Font = value;
			}
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x0600134F RID: 4943 RVA: 0x00052D74 File Offset: 0x00050F74
		// (set) Token: 0x06001350 RID: 4944 RVA: 0x00052D8C File Offset: 0x00050F8C
		public FontAsset fontAsset
		{
			get
			{
				return this.m_FontAsset;
			}
			set
			{
				bool flag = value != null && this.font != null;
				if (flag)
				{
					throw new InvalidOperationException("Cannot set both Font and FontAsset on FontDefinition");
				}
				this.m_FontAsset = value;
			}
		}

		// Token: 0x06001351 RID: 4945 RVA: 0x00052DC8 File Offset: 0x00050FC8
		public static FontDefinition FromFont(Font f)
		{
			return new FontDefinition
			{
				m_Font = f
			};
		}

		// Token: 0x06001352 RID: 4946 RVA: 0x00052DEC File Offset: 0x00050FEC
		public static FontDefinition FromSDFFont(FontAsset f)
		{
			return new FontDefinition
			{
				m_FontAsset = f
			};
		}

		// Token: 0x06001353 RID: 4947 RVA: 0x00052E10 File Offset: 0x00051010
		internal static FontDefinition FromObject(object obj)
		{
			Font font = obj as Font;
			bool flag = font != null;
			FontDefinition fontDefinition;
			if (flag)
			{
				fontDefinition = FontDefinition.FromFont(font);
			}
			else
			{
				FontAsset fontAsset = obj as FontAsset;
				bool flag2 = fontAsset != null;
				if (flag2)
				{
					fontDefinition = FontDefinition.FromSDFFont(fontAsset);
				}
				else
				{
					fontDefinition = default(FontDefinition);
				}
			}
			return fontDefinition;
		}

		// Token: 0x06001354 RID: 4948 RVA: 0x00052E64 File Offset: 0x00051064
		internal bool IsEmpty()
		{
			return this.m_Font == null && this.m_FontAsset == null;
		}

		// Token: 0x06001355 RID: 4949 RVA: 0x00052E94 File Offset: 0x00051094
		public override string ToString()
		{
			bool flag = this.font != null;
			string text;
			if (flag)
			{
				text = string.Format("{0}", this.font);
			}
			else
			{
				text = string.Format("{0}", this.fontAsset);
			}
			return text;
		}

		// Token: 0x06001356 RID: 4950 RVA: 0x00052EDC File Offset: 0x000510DC
		public bool Equals(FontDefinition other)
		{
			return object.Equals(this.m_Font, other.m_Font) && object.Equals(this.m_FontAsset, other.m_FontAsset);
		}

		// Token: 0x06001357 RID: 4951 RVA: 0x00052F18 File Offset: 0x00051118
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is FontDefinition)
			{
				FontDefinition fontDefinition = (FontDefinition)obj;
				flag = this.Equals(fontDefinition);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06001358 RID: 4952 RVA: 0x00052F44 File Offset: 0x00051144
		public override int GetHashCode()
		{
			return (((this.m_Font != null) ? this.m_Font.GetHashCode() : 0) * 397) ^ ((this.m_FontAsset != null) ? this.m_FontAsset.GetHashCode() : 0);
		}

		// Token: 0x06001359 RID: 4953 RVA: 0x00052F98 File Offset: 0x00051198
		public static bool operator ==(FontDefinition left, FontDefinition right)
		{
			return left.Equals(right);
		}

		// Token: 0x0600135A RID: 4954 RVA: 0x00052FB4 File Offset: 0x000511B4
		public static bool operator !=(FontDefinition left, FontDefinition right)
		{
			return !left.Equals(right);
		}

		// Token: 0x040008D1 RID: 2257
		private Font m_Font;

		// Token: 0x040008D2 RID: 2258
		private FontAsset m_FontAsset;
	}
}
