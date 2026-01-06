using System;

namespace System.ComponentModel
{
	/// <summary>Identifies the type of data operation performed by a method, as specified by the <see cref="T:System.ComponentModel.DataObjectMethodAttribute" /> applied to the method.</summary>
	// Token: 0x020006AC RID: 1708
	public enum DataObjectMethodType
	{
		/// <summary>Indicates that a method is used for a data operation that fills a <see cref="T:System.Data.DataSet" /> object.</summary>
		// Token: 0x0400207C RID: 8316
		Fill,
		/// <summary>Indicates that a method is used for a data operation that retrieves data.</summary>
		// Token: 0x0400207D RID: 8317
		Select,
		/// <summary>Indicates that a method is used for a data operation that updates data.</summary>
		// Token: 0x0400207E RID: 8318
		Update,
		/// <summary>Indicates that a method is used for a data operation that inserts data.</summary>
		// Token: 0x0400207F RID: 8319
		Insert,
		/// <summary>Indicates that a method is used for a data operation that deletes data.</summary>
		// Token: 0x04002080 RID: 8320
		Delete
	}
}
