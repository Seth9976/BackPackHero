using System;

namespace System.Data.SqlTypes
{
	/// <summary>The <see cref="T:System.Data.SqlTypes.TypeTinyIntSchemaImporterExtension" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality. </summary>
	// Token: 0x020002E9 RID: 745
	public sealed class TypeTinyIntSchemaImporterExtension : SqlTypesSchemaImporterExtensionHelper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.TypeTinyIntSchemaImporterExtension" /> class.</summary>
		// Token: 0x06002242 RID: 8770 RVA: 0x0009E8BF File Offset: 0x0009CABF
		public TypeTinyIntSchemaImporterExtension()
			: base("tinyint", "System.Data.SqlTypes.SqlByte")
		{
		}
	}
}
