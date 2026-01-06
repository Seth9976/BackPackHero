using System;

namespace System.Data.SqlTypes
{
	/// <summary>The <see cref="T:System.Data.SqlTypes.TypeTextSchemaImporterExtension" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality. </summary>
	// Token: 0x020002DF RID: 735
	public sealed class TypeTextSchemaImporterExtension : SqlTypesSchemaImporterExtensionHelper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.TypeTextSchemaImporterExtension" /> class.</summary>
		// Token: 0x06002238 RID: 8760 RVA: 0x0009E804 File Offset: 0x0009CA04
		public TypeTextSchemaImporterExtension()
			: base("text", "System.Data.SqlTypes.SqlString", false)
		{
		}
	}
}
