using System;

namespace System.Data.SqlTypes
{
	/// <summary>The <see cref="T:System.Data.SqlTypes.TypeBitSchemaImporterExtension" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality. </summary>
	// Token: 0x020002EA RID: 746
	public sealed class TypeBitSchemaImporterExtension : SqlTypesSchemaImporterExtensionHelper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.TypeBitSchemaImporterExtension" /> class. </summary>
		// Token: 0x06002243 RID: 8771 RVA: 0x0009E8D1 File Offset: 0x0009CAD1
		public TypeBitSchemaImporterExtension()
			: base("bit", "System.Data.SqlTypes.SqlBoolean")
		{
		}
	}
}
