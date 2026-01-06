using System;

namespace System.Data.SqlTypes
{
	/// <summary>The <see cref="T:System.Data.SqlTypes.TypeVarImageSchemaImporterExtension" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality. </summary>
	// Token: 0x020002E3 RID: 739
	public sealed class TypeVarImageSchemaImporterExtension : SqlTypesSchemaImporterExtensionHelper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.TypeVarImageSchemaImporterExtension" /> class.</summary>
		// Token: 0x0600223C RID: 8764 RVA: 0x0009E850 File Offset: 0x0009CA50
		public TypeVarImageSchemaImporterExtension()
			: base("image", "System.Data.SqlTypes.SqlBinary", false)
		{
		}
	}
}
