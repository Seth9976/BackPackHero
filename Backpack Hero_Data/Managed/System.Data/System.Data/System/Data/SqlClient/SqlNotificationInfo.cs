using System;

namespace System.Data.SqlClient
{
	/// <summary>This enumeration provides additional information about the different notifications that can be received by the dependency event handler. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001C2 RID: 450
	public enum SqlNotificationInfo
	{
		/// <summary>One or more tables were truncated.</summary>
		// Token: 0x04000E9A RID: 3738
		Truncate,
		/// <summary>Data was changed by an INSERT statement.</summary>
		// Token: 0x04000E9B RID: 3739
		Insert,
		/// <summary>Data was changed by an UPDATE statement.</summary>
		// Token: 0x04000E9C RID: 3740
		Update,
		/// <summary>Data was changed by a DELETE statement.</summary>
		// Token: 0x04000E9D RID: 3741
		Delete,
		/// <summary>An underlying object related to the query was dropped.</summary>
		// Token: 0x04000E9E RID: 3742
		Drop,
		/// <summary>An underlying server object related to the query was modified.</summary>
		// Token: 0x04000E9F RID: 3743
		Alter,
		/// <summary>The server was restarted (notifications are sent during restart.).</summary>
		// Token: 0x04000EA0 RID: 3744
		Restart,
		/// <summary>An internal server error occurred.</summary>
		// Token: 0x04000EA1 RID: 3745
		Error,
		/// <summary>A SELECT statement that cannot be notified or was provided.</summary>
		// Token: 0x04000EA2 RID: 3746
		Query,
		/// <summary>A statement was provided that cannot be notified (for example, an UPDATE statement).</summary>
		// Token: 0x04000EA3 RID: 3747
		Invalid,
		/// <summary>The SET options were not set appropriately at subscription time.</summary>
		// Token: 0x04000EA4 RID: 3748
		Options,
		/// <summary>The statement was executed under an isolation mode that was not valid (for example, Snapshot).</summary>
		// Token: 0x04000EA5 RID: 3749
		Isolation,
		/// <summary>The SqlDependency object has expired.</summary>
		// Token: 0x04000EA6 RID: 3750
		Expired,
		/// <summary>Fires as a result of server resource pressure.</summary>
		// Token: 0x04000EA7 RID: 3751
		Resource,
		/// <summary>A previous statement has caused query notifications to fire under the current transaction.</summary>
		// Token: 0x04000EA8 RID: 3752
		PreviousFire,
		/// <summary>The subscribing query causes the number of templates on one of the target tables to exceed the maximum allowable limit.</summary>
		// Token: 0x04000EA9 RID: 3753
		TemplateLimit,
		/// <summary>Used to distinguish the server-side cause for a query notification firing.</summary>
		// Token: 0x04000EAA RID: 3754
		Merge,
		/// <summary>Used when the info option sent by the server was not recognized by the client.</summary>
		// Token: 0x04000EAB RID: 3755
		Unknown = -1,
		/// <summary>The SqlDependency object already fired, and new commands cannot be added to it.</summary>
		// Token: 0x04000EAC RID: 3756
		AlreadyChanged = -2
	}
}
