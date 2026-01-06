using System;

namespace System
{
	/// <summary>A customizable parser based on the HTTP scheme.</summary>
	// Token: 0x0200015E RID: 350
	public class HttpStyleUriParser : UriParser
	{
		/// <summary>Create a customizable parser based on the HTTP scheme.</summary>
		// Token: 0x0600098A RID: 2442 RVA: 0x0002A209 File Offset: 0x00028409
		public HttpStyleUriParser()
			: base(UriParser.HttpUri.Flags)
		{
		}
	}
}
