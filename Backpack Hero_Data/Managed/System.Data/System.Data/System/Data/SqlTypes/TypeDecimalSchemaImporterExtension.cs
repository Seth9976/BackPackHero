using System;

namespace System.Data.SqlTypes
{
	/// <summary>The <see cref="T:System.Data.SqlTypes.TypeDecimalSchemaImporterExtension" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality. </summary>
	// Token: 0x020002E4 RID: 740
	public sealed class TypeDecimalSchemaImporterExtension : SqlTypesSchemaImporterExtensionHelper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.TypeDecimalSchemaImporterExtension" /> class.</summary>
		// Token: 0x0600223D RID: 8765 RVA: 0x0009E863 File Offset: 0x0009CA63
		public TypeDecimalSchemaImporterExtension()
			: base("decimal", "System.Data.SqlTypes.SqlDecimal", false)
		{
		}
	}
}
