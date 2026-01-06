using System;

namespace System.Data.SqlTypes
{
	/// <summary>The <see cref="T:System.Data.SqlTypes.TypeBigIntSchemaImporterExtension" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality. </summary>
	// Token: 0x020002E6 RID: 742
	public sealed class TypeBigIntSchemaImporterExtension : SqlTypesSchemaImporterExtensionHelper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.TypeBigIntSchemaImporterExtension" /> class.</summary>
		// Token: 0x0600223F RID: 8767 RVA: 0x0009E889 File Offset: 0x0009CA89
		public TypeBigIntSchemaImporterExtension()
			: base("bigint", "System.Data.SqlTypes.SqlInt64")
		{
		}
	}
}
