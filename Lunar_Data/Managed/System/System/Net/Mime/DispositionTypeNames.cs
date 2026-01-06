using System;

namespace System.Net.Mime
{
	/// <summary>Supplies the strings used to specify the disposition type for an e-mail attachment.</summary>
	// Token: 0x02000608 RID: 1544
	public static class DispositionTypeNames
	{
		/// <summary>Specifies that the attachment is to be displayed as part of the e-mail message body.</summary>
		// Token: 0x04001E57 RID: 7767
		public const string Inline = "inline";

		/// <summary>Specifies that the attachment is to be displayed as a file attached to the e-mail message.</summary>
		// Token: 0x04001E58 RID: 7768
		public const string Attachment = "attachment";
	}
}
