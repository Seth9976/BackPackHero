using System;

namespace System.Data.SqlTypes
{
	/// <summary>The <see cref="T:System.Data.SqlTypes.TypeNVarCharSchemaImporterExtension" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality. </summary>
	// Token: 0x020002DE RID: 734
	public sealed class TypeNVarCharSchemaImporterExtension : SqlTypesSchemaImporterExtensionHelper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.TypeNVarCharSchemaImporterExtension" /> class.</summary>
		// Token: 0x06002237 RID: 8759 RVA: 0x0009E7F1 File Offset: 0x0009C9F1
		public TypeNVarCharSchemaImporterExtension()
			: base("nvarchar", "System.Data.SqlTypes.SqlString", false)
		{
		}
	}
}
