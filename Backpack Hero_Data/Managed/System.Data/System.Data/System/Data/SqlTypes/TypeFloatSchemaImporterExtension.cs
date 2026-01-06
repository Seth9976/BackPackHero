using System;

namespace System.Data.SqlTypes
{
	/// <summary>The <see cref="T:System.Data.SqlTypes.TypeFloatSchemaImporterExtension" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality. </summary>
	// Token: 0x020002EB RID: 747
	public sealed class TypeFloatSchemaImporterExtension : SqlTypesSchemaImporterExtensionHelper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.TypeFloatSchemaImporterExtension" /> class.</summary>
		// Token: 0x06002244 RID: 8772 RVA: 0x0009E8E3 File Offset: 0x0009CAE3
		public TypeFloatSchemaImporterExtension()
			: base("float", "System.Data.SqlTypes.SqlDouble")
		{
		}
	}
}
