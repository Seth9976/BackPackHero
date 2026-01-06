using System;

namespace System.Data.SqlTypes
{
	/// <summary>The <see cref="T:System.Data.SqlTypes.TypeCharSchemaImporterExtension" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality. </summary>
	// Token: 0x020002DB RID: 731
	public sealed class TypeCharSchemaImporterExtension : SqlTypesSchemaImporterExtensionHelper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.TypeCharSchemaImporterExtension" /> class.</summary>
		// Token: 0x06002234 RID: 8756 RVA: 0x0009E7B8 File Offset: 0x0009C9B8
		public TypeCharSchemaImporterExtension()
			: base("char", "System.Data.SqlTypes.SqlString", false)
		{
		}
	}
}
