using System;

namespace System.Text.RegularExpressions
{
	// Token: 0x0200020A RID: 522
	internal readonly struct RegexPrefix
	{
		// Token: 0x06000F05 RID: 3845 RVA: 0x00043290 File Offset: 0x00041490
		internal RegexPrefix(string prefix, bool ci)
		{
			this.Prefix = prefix;
			this.CaseInsensitive = ci;
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000F06 RID: 3846 RVA: 0x000432A0 File Offset: 0x000414A0
		internal bool CaseInsensitive { get; }

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000F07 RID: 3847 RVA: 0x000432A8 File Offset: 0x000414A8
		internal static RegexPrefix Empty { get; } = new RegexPrefix(string.Empty, false);

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000F08 RID: 3848 RVA: 0x000432AF File Offset: 0x000414AF
		internal string Prefix { get; }
	}
}
