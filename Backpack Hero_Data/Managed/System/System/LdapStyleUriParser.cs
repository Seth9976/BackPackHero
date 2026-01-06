using System;

namespace System
{
	/// <summary>A customizable parser based on the Lightweight Directory Access Protocol (LDAP) scheme.</summary>
	// Token: 0x02000163 RID: 355
	public class LdapStyleUriParser : UriParser
	{
		/// <summary>Creates a customizable parser based on the Lightweight Directory Access Protocol (LDAP) scheme.</summary>
		// Token: 0x0600098F RID: 2447 RVA: 0x0002A263 File Offset: 0x00028463
		public LdapStyleUriParser()
			: base(UriParser.LdapUri.Flags)
		{
		}
	}
}
