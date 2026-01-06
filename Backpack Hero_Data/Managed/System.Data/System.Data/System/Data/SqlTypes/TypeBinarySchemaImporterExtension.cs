using System;

namespace System.Data.SqlTypes
{
	/// <summary>The <see cref="T:System.Data.SqlTypes.TypeBinarySchemaImporterExtension" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality. </summary>
	// Token: 0x020002E2 RID: 738
	public sealed class TypeBinarySchemaImporterExtension : SqlTypesSchemaImporterExtensionHelper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.TypeBinarySchemaImporterExtension" /> class.</summary>
		// Token: 0x0600223B RID: 8763 RVA: 0x0009E83D File Offset: 0x0009CA3D
		public TypeBinarySchemaImporterExtension()
			: base("binary", "System.Data.SqlTypes.SqlBinary", false)
		{
		}
	}
}
