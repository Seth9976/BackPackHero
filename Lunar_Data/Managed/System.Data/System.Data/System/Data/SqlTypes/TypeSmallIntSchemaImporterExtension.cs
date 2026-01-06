using System;

namespace System.Data.SqlTypes
{
	/// <summary>The <see cref="T:System.Data.SqlTypes.TypeSmallIntSchemaImporterExtension" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality. </summary>
	// Token: 0x020002E8 RID: 744
	public sealed class TypeSmallIntSchemaImporterExtension : SqlTypesSchemaImporterExtensionHelper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.TypeSmallIntSchemaImporterExtension" /> class.</summary>
		// Token: 0x06002241 RID: 8769 RVA: 0x0009E8AD File Offset: 0x0009CAAD
		public TypeSmallIntSchemaImporterExtension()
			: base("smallint", "System.Data.SqlTypes.SqlInt16")
		{
		}
	}
}
