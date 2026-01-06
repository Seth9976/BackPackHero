using System;

namespace System.Data.SqlTypes
{
	/// <summary>The <see cref="T:System.Data.SqlTypes.TypeSmallMoneySchemaImporterExtension" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality. </summary>
	// Token: 0x020002F0 RID: 752
	public sealed class TypeSmallMoneySchemaImporterExtension : SqlTypesSchemaImporterExtensionHelper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.TypeSmallMoneySchemaImporterExtension" /> class.</summary>
		// Token: 0x06002249 RID: 8777 RVA: 0x0009E93D File Offset: 0x0009CB3D
		public TypeSmallMoneySchemaImporterExtension()
			: base("smallmoney", "System.Data.SqlTypes.SqlMoney")
		{
		}
	}
}
