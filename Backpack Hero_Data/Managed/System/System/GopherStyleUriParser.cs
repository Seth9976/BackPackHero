using System;

namespace System
{
	/// <summary>A customizable parser based on the Gopher scheme.</summary>
	// Token: 0x02000162 RID: 354
	public class GopherStyleUriParser : UriParser
	{
		/// <summary>Creates a customizable parser based on the Gopher scheme.</summary>
		// Token: 0x0600098E RID: 2446 RVA: 0x0002A251 File Offset: 0x00028451
		public GopherStyleUriParser()
			: base(UriParser.GopherUri.Flags)
		{
		}
	}
}
