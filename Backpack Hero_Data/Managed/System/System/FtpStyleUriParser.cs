using System;

namespace System
{
	/// <summary>A customizable parser based on the File Transfer Protocol (FTP) scheme.</summary>
	// Token: 0x0200015F RID: 351
	public class FtpStyleUriParser : UriParser
	{
		/// <summary>Creates a customizable parser based on the File Transfer Protocol (FTP) scheme.</summary>
		// Token: 0x0600098B RID: 2443 RVA: 0x0002A21B File Offset: 0x0002841B
		public FtpStyleUriParser()
			: base(UriParser.FtpUri.Flags)
		{
		}
	}
}
