using System;

namespace System.Data.SqlClient
{
	/// <summary>Indicates the source of the notification received by the dependency event handler.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001C3 RID: 451
	public enum SqlNotificationSource
	{
		/// <summary>Data has changed; for example, an insert, update, delete, or truncate operation occurred.</summary>
		// Token: 0x04000EAE RID: 3758
		Data,
		/// <summary>The subscription time-out expired.</summary>
		// Token: 0x04000EAF RID: 3759
		Timeout,
		/// <summary>A database object changed; for example, an underlying object related to the query was dropped or modified.</summary>
		// Token: 0x04000EB0 RID: 3760
		Object,
		/// <summary>The database state changed; for example, the database related to the query was dropped or detached.</summary>
		// Token: 0x04000EB1 RID: 3761
		Database,
		/// <summary>A system-related event occurred. For example, there was an internal error, the server was restarted, or resource pressure caused the invalidation.</summary>
		// Token: 0x04000EB2 RID: 3762
		System,
		/// <summary>The Transact-SQL statement is not valid for notifications; for example, a SELECT statement that could not be notified or a non-SELECT statement was executed.</summary>
		// Token: 0x04000EB3 RID: 3763
		Statement,
		/// <summary>The run-time environment was not compatible with notifications; for example, the isolation level was set to snapshot, or one or more SET options are not compatible.</summary>
		// Token: 0x04000EB4 RID: 3764
		Environment,
		/// <summary>A run-time error occurred during execution.</summary>
		// Token: 0x04000EB5 RID: 3765
		Execution,
		/// <summary>Internal only; not intended to be used in your code.</summary>
		// Token: 0x04000EB6 RID: 3766
		Owner,
		/// <summary>Used when the source option sent by the server was not recognized by the client. </summary>
		// Token: 0x04000EB7 RID: 3767
		Unknown = -1,
		/// <summary>A client-initiated notification occurred, such as a client-side time-out or as a result of attempting to add a command to a dependency that has already fired.</summary>
		// Token: 0x04000EB8 RID: 3768
		Client = -2
	}
}
