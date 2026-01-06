using System;
using System.Runtime.InteropServices;
using UnityEngine.TextCore.Text;

namespace UnityEngine.UIElements
{
	// Token: 0x02000283 RID: 643
	public struct StyleFontDefinition : IStyleValue<FontDefinition>, IEquatable<StyleFontDefinition>
	{
		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x060014E2 RID: 5346 RVA: 0x0005A794 File Offset: 0x00058994
		// (set) Token: 0x060014E3 RID: 5347 RVA: 0x0005A7BF File Offset: 0x000589BF
		public FontDefinition value
		{
			get
			{
				return (this.m_Keyword == StyleKeyword.Undefined) ? this.m_Value : default(FontDefinition);
			}
			set
			{
				this.m_Value = value;
				this.m_Keyword = StyleKeyword.Undefined;
			}
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x060014E4 RID: 5348 RVA: 0x0005A7D0 File Offset: 0x000589D0
		// (set) Token: 0x060014E5 RID: 5349 RVA: 0x0005A7E8 File Offset: 0x000589E8
		public StyleKeyword keyword
		{
			get
			{
				return this.m_Keyword;
			}
			set
			{
				this.m_Keyword = value;
			}
		}

		// Token: 0x060014E6 RID: 5350 RVA: 0x0005A7F2 File Offset: 0x000589F2
		public StyleFontDefinition(FontDefinition f)
		{
			this = new StyleFontDefinition(f, StyleKeyword.Undefined);
		}

		// Token: 0x060014E7 RID: 5351 RVA: 0x0005A7FE File Offset: 0x000589FE
		public StyleFontDefinition(FontAsset f)
		{
			this = new StyleFontDefinition(f, StyleKeyword.Undefined);
		}

		// Token: 0x060014E8 RID: 5352 RVA: 0x0005A80A File Offset: 0x00058A0A
		public StyleFontDefinition(Font f)
		{
			this = new StyleFontDefinition(f, StyleKeyword.Undefined);
		}

		// Token: 0x060014E9 RID: 5353 RVA: 0x0005A818 File Offset: 0x00058A18
		public StyleFontDefinition(StyleKeyword keyword)
		{
			this = new StyleFontDefinition(default(FontDefinition), keyword);
		}

		// Token: 0x060014EA RID: 5354 RVA: 0x0005A837 File Offset: 0x00058A37
		internal StyleFontDefinition(object obj, StyleKeyword keyword)
		{
			this = new StyleFontDefinition(FontDefinition.FromObject(obj), keyword);
		}

		// Token: 0x060014EB RID: 5355 RVA: 0x0005A848 File Offset: 0x00058A48
		internal StyleFontDefinition(object obj)
		{
			this = new StyleFontDefinition(FontDefinition.FromObject(obj), StyleKeyword.Undefined);
		}

		// Token: 0x060014EC RID: 5356 RVA: 0x0005A859 File Offset: 0x00058A59
		internal StyleFontDefinition(FontAsset f, StyleKeyword keyword)
		{
			this = new StyleFontDefinition(FontDefinition.FromSDFFont(f), keyword);
		}

		// Token: 0x060014ED RID: 5357 RVA: 0x0005A86A File Offset: 0x00058A6A
		internal StyleFontDefinition(Font f, StyleKeyword keyword)
		{
			this = new StyleFontDefinition(FontDefinition.FromFont(f), keyword);
		}

		// Token: 0x060014EE RID: 5358 RVA: 0x0005A87C File Offset: 0x00058A7C
		internal StyleFontDefinition(GCHandle gcHandle, StyleKeyword keyword)
		{
			this = new StyleFontDefinition(gcHandle.IsAllocated ? FontDefinition.FromObject(gcHandle.Target) : default(FontDefinition), keyword);
		}

		// Token: 0x060014EF RID: 5359 RVA: 0x0005A8B2 File Offset: 0x00058AB2
		internal StyleFontDefinition(FontDefinition f, StyleKeyword keyword)
		{
			this.m_Keyword = keyword;
			this.m_Value = f;
		}

		// Token: 0x060014F0 RID: 5360 RVA: 0x0005A8C3 File Offset: 0x00058AC3
		internal StyleFontDefinition(StyleFontDefinition sfd)
		{
			this.m_Keyword = sfd.keyword;
			this.m_Value = sfd.value;
		}

		// Token: 0x060014F1 RID: 5361 RVA: 0x0005A8E0 File Offset: 0x00058AE0
		public static implicit operator StyleFontDefinition(StyleKeyword keyword)
		{
			return new StyleFontDefinition(keyword);
		}

		// Token: 0x060014F2 RID: 5362 RVA: 0x0005A8F8 File Offset: 0x00058AF8
		public static implicit operator StyleFontDefinition(FontDefinition f)
		{
			return new StyleFontDefinition(f);
		}

		// Token: 0x060014F3 RID: 5363 RVA: 0x0005A910 File Offset: 0x00058B10
		public bool Equals(StyleFontDefinition other)
		{
			return this.m_Keyword == other.m_Keyword && this.m_Value.Equals(other.m_Value);
		}

		// Token: 0x060014F4 RID: 5364 RVA: 0x0005A944 File Offset: 0x00058B44
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is StyleFontDefinition)
			{
				StyleFontDefinition styleFontDefinition = (StyleFontDefinition)obj;
				flag = this.Equals(styleFontDefinition);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060014F5 RID: 5365 RVA: 0x0005A970 File Offset: 0x00058B70
		public override int GetHashCode()
		{
			return (int)((this.m_Keyword * (StyleKeyword)397) ^ (StyleKeyword)this.m_Value.GetHashCode());
		}

		// Token: 0x060014F6 RID: 5366 RVA: 0x0005A9A4 File Offset: 0x00058BA4
		public static bool operator ==(StyleFontDefinition left, StyleFontDefinition right)
		{
			return left.Equals(right);
		}

		// Token: 0x060014F7 RID: 5367 RVA: 0x0005A9C0 File Offset: 0x00058BC0
		public static bool operator !=(StyleFontDefinition left, StyleFontDefinition right)
		{
			return !left.Equals(right);
		}

		// Token: 0x0400090C RID: 2316
		private StyleKeyword m_Keyword;

		// Token: 0x0400090D RID: 2317
		private FontDefinition m_Value;
	}
}
