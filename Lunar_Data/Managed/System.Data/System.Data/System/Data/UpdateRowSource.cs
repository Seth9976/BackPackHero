using System;

namespace System.Data
{
	/// <summary>Specifies how query command results are applied to the row being updated.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000E7 RID: 231
	public enum UpdateRowSource
	{
		/// <summary>Any returned parameters or rows are ignored.</summary>
		// Token: 0x04000840 RID: 2112
		None,
		/// <summary>Output parameters are mapped to the changed row in the <see cref="T:System.Data.DataSet" />.</summary>
		// Token: 0x04000841 RID: 2113
		OutputParameters,
		/// <summary>The data in the first returned row is mapped to the changed row in the <see cref="T:System.Data.DataSet" />.</summary>
		// Token: 0x04000842 RID: 2114
		FirstReturnedRecord,
		/// <summary>Both the output parameters and the first returned row are mapped to the changed row in the <see cref="T:System.Data.DataSet" />.</summary>
		// Token: 0x04000843 RID: 2115
		Both
	}
}
