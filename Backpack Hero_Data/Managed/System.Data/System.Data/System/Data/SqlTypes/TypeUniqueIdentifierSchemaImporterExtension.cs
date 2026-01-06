using System;

namespace System.Data.SqlTypes
{
	/// <summary>The <see cref="T:System.Data.SqlTypes.TypeUniqueIdentifierSchemaImporterExtension" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality. </summary>
	// Token: 0x020002F1 RID: 753
	public sealed class TypeUniqueIdentifierSchemaImporterExtension : SqlTypesSchemaImporterExtensionHelper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.TypeUniqueIdentifierSchemaImporterExtension" /> class.</summary>
		// Token: 0x0600224A RID: 8778 RVA: 0x0009E94F File Offset: 0x0009CB4F
		public TypeUniqueIdentifierSchemaImporterExtension()
			: base("uniqueidentifier", "System.Data.SqlTypes.SqlGuid")
		{
		}
	}
}
