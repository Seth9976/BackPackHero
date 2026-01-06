using System;

namespace System.Data.SqlTypes
{
	/// <summary>The TypeSmallDateTimeSchemaImporterExtension class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality. </summary>
	// Token: 0x020002EE RID: 750
	public sealed class TypeSmallDateTimeSchemaImporterExtension : SqlTypesSchemaImporterExtensionHelper
	{
		/// <summary>Initializes a new instance of the TypeSmallDateTimeSchemaImporterExtension class.</summary>
		// Token: 0x06002247 RID: 8775 RVA: 0x0009E919 File Offset: 0x0009CB19
		public TypeSmallDateTimeSchemaImporterExtension()
			: base("smalldatetime", "System.Data.SqlTypes.SqlDateTime")
		{
		}
	}
}
