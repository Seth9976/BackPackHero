using System;

namespace System.Net
{
	// Token: 0x02000463 RID: 1123
	internal struct HeaderVariantInfo
	{
		// Token: 0x06002361 RID: 9057 RVA: 0x000823AD File Offset: 0x000805AD
		internal HeaderVariantInfo(string name, CookieVariant variant)
		{
			this.m_name = name;
			this.m_variant = variant;
		}

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x06002362 RID: 9058 RVA: 0x000823BD File Offset: 0x000805BD
		internal string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x06002363 RID: 9059 RVA: 0x000823C5 File Offset: 0x000805C5
		internal CookieVariant Variant
		{
			get
			{
				return this.m_variant;
			}
		}

		// Token: 0x040014BC RID: 5308
		private string m_name;

		// Token: 0x040014BD RID: 5309
		private CookieVariant m_variant;
	}
}
