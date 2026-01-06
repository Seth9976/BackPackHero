using System;

namespace System.Data.SqlTypes
{
	/// <summary>The <see cref="T:System.Data.SqlTypes.TypeVarCharSchemaImporterExtension" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality. </summary>
	// Token: 0x020002DD RID: 733
	public sealed class TypeVarCharSchemaImporterExtension : SqlTypesSchemaImporterExtensionHelper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.TypeVarCharSchemaImporterExtension" /> class.</summary>
		// Token: 0x06002236 RID: 8758 RVA: 0x0009E7DE File Offset: 0x0009C9DE
		public TypeVarCharSchemaImporterExtension()
			: base("varchar", "System.Data.SqlTypes.SqlString", false)
		{
		}
	}
}
