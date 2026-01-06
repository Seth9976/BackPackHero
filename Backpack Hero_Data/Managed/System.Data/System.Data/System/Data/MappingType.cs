using System;

namespace System.Data
{
	/// <summary>Specifies how a <see cref="T:System.Data.DataColumn" /> is mapped.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000BC RID: 188
	public enum MappingType
	{
		/// <summary>The column is mapped to an XML element.</summary>
		// Token: 0x0400076B RID: 1899
		Element = 1,
		/// <summary>The column is mapped to an XML attribute.</summary>
		// Token: 0x0400076C RID: 1900
		Attribute,
		/// <summary>The column is mapped to an <see cref="T:System.Xml.XmlText" /> node.</summary>
		// Token: 0x0400076D RID: 1901
		SimpleContent,
		/// <summary>The column is mapped to an internal structure.</summary>
		// Token: 0x0400076E RID: 1902
		Hidden
	}
}
