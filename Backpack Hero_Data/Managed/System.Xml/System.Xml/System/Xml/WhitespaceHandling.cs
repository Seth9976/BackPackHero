using System;

namespace System.Xml
{
	/// <summary>Specifies how white space is handled.</summary>
	// Token: 0x02000060 RID: 96
	public enum WhitespaceHandling
	{
		/// <summary>Return Whitespace and SignificantWhitespace nodes. This is the default.</summary>
		// Token: 0x040006AA RID: 1706
		All,
		/// <summary>Return SignificantWhitespace nodes only.</summary>
		// Token: 0x040006AB RID: 1707
		Significant,
		/// <summary>Return no Whitespace and no SignificantWhitespace nodes.</summary>
		// Token: 0x040006AC RID: 1708
		None
	}
}
