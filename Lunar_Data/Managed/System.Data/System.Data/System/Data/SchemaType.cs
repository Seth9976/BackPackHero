using System;

namespace System.Data
{
	/// <summary>Specifies how to handle existing schema mappings when performing a <see cref="M:System.Data.Common.DataAdapter.FillSchema(System.Data.DataSet,System.Data.SchemaType)" /> operation.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000D3 RID: 211
	public enum SchemaType
	{
		/// <summary>Ignore any table mappings on the DataAdapter. Configure the <see cref="T:System.Data.DataSet" /> using the incoming schema without applying any transformations.</summary>
		// Token: 0x040007D4 RID: 2004
		Source = 1,
		/// <summary>Apply any existing table mappings to the incoming schema. Configure the <see cref="T:System.Data.DataSet" /> with the transformed schema.</summary>
		// Token: 0x040007D5 RID: 2005
		Mapped
	}
}
