using System;

namespace System
{
	/// <summary>A customizable parser based on the File scheme.</summary>
	// Token: 0x02000160 RID: 352
	public class FileStyleUriParser : UriParser
	{
		/// <summary>Creates a customizable parser based on the File scheme.</summary>
		// Token: 0x0600098C RID: 2444 RVA: 0x0002A22D File Offset: 0x0002842D
		public FileStyleUriParser()
			: base(UriParser.FileUri.Flags)
		{
		}
	}
}
