using System;

namespace System.Data.SqlTypes
{
	/// <summary>The <see cref="T:System.Data.SqlTypes.TypeRealSchemaImporterExtension" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality. </summary>
	// Token: 0x020002EC RID: 748
	public sealed class TypeRealSchemaImporterExtension : SqlTypesSchemaImporterExtensionHelper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.TypeRealSchemaImporterExtension" /> class.</summary>
		// Token: 0x06002245 RID: 8773 RVA: 0x0009E8F5 File Offset: 0x0009CAF5
		public TypeRealSchemaImporterExtension()
			: base("real", "System.Data.SqlTypes.SqlSingle")
		{
		}
	}
}
