using System;

namespace System.Data.SqlTypes
{
	/// <summary>The <see cref="T:System.Data.SqlTypes.TypeDateTimeSchemaImporterExtension" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality. </summary>
	// Token: 0x020002ED RID: 749
	public sealed class TypeDateTimeSchemaImporterExtension : SqlTypesSchemaImporterExtensionHelper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.TypeDateTimeSchemaImporterExtension" /> class.</summary>
		// Token: 0x06002246 RID: 8774 RVA: 0x0009E907 File Offset: 0x0009CB07
		public TypeDateTimeSchemaImporterExtension()
			: base("datetime", "System.Data.SqlTypes.SqlDateTime")
		{
		}
	}
}
