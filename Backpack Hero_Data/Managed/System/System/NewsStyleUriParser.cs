using System;

namespace System
{
	/// <summary>A customizable parser based on the news scheme using the Network News Transfer Protocol (NNTP).</summary>
	// Token: 0x02000161 RID: 353
	public class NewsStyleUriParser : UriParser
	{
		/// <summary>Create a customizable parser based on the news scheme using the Network News Transfer Protocol (NNTP).</summary>
		// Token: 0x0600098D RID: 2445 RVA: 0x0002A23F File Offset: 0x0002843F
		public NewsStyleUriParser()
			: base(UriParser.NewsUri.Flags)
		{
		}
	}
}
