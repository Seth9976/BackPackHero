using System;

namespace System.Data.SqlClient
{
	/// <summary>Bitwise flag that specifies one or more options to use with an instance of <see cref="T:System.Data.SqlClient.SqlBulkCopy" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200015B RID: 347
	[Flags]
	public enum SqlBulkCopyOptions
	{
		/// <summary>Use the default values for all options.</summary>
		// Token: 0x04000B55 RID: 2901
		Default = 0,
		/// <summary>Preserve source identity values. When not specified, identity values are assigned by the destination.</summary>
		// Token: 0x04000B56 RID: 2902
		KeepIdentity = 1,
		/// <summary>Check constraints while data is being inserted. By default, constraints are not checked.</summary>
		// Token: 0x04000B57 RID: 2903
		CheckConstraints = 2,
		/// <summary>Obtain a bulk update lock for the duration of the bulk copy operation. When not specified, row locks are used.</summary>
		// Token: 0x04000B58 RID: 2904
		TableLock = 4,
		/// <summary>Preserve null values in the destination table regardless of the settings for default values. When not specified, null values are replaced by default values where applicable.</summary>
		// Token: 0x04000B59 RID: 2905
		KeepNulls = 8,
		/// <summary>When specified, cause the server to fire the insert triggers for the rows being inserted into the database.</summary>
		// Token: 0x04000B5A RID: 2906
		FireTriggers = 16,
		/// <summary>When specified, each batch of the bulk-copy operation will occur within a transaction. If you indicate this option and also provide a <see cref="T:System.Data.SqlClient.SqlTransaction" /> object to the constructor, an <see cref="T:System.ArgumentException" /> occurs.</summary>
		// Token: 0x04000B5B RID: 2907
		UseInternalTransaction = 32,
		// Token: 0x04000B5C RID: 2908
		AllowEncryptedValueModifications = 64
	}
}
