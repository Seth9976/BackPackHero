using System;

namespace System.Data.SqlTypes
{
	/// <summary>The <see cref="T:System.Data.SqlTypes.TypeVarBinarySchemaImporterExtension" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality. </summary>
	// Token: 0x020002E1 RID: 737
	public sealed class TypeVarBinarySchemaImporterExtension : SqlTypesSchemaImporterExtensionHelper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.TypeVarBinarySchemaImporterExtension" /> class.</summary>
		// Token: 0x0600223A RID: 8762 RVA: 0x0009E82A File Offset: 0x0009CA2A
		public TypeVarBinarySchemaImporterExtension()
			: base("varbinary", "System.Data.SqlTypes.SqlBinary", false)
		{
		}
	}
}
