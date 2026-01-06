using System;

namespace Microsoft.SqlServer.Server
{
	/// <summary>Used to mark a method definition in an assembly as a trigger in SQL Server. The properties on the attribute reflect the physical attributes used when the type is registered with SQL Server. This class cannot be inherited.</summary>
	// Token: 0x020003C7 RID: 967
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	[Serializable]
	public sealed class SqlTriggerAttribute : Attribute
	{
		/// <summary>An attribute on a method definition in an assembly, used to mark the method as a trigger in SQL Server.</summary>
		// Token: 0x06002F10 RID: 12048 RVA: 0x000CB013 File Offset: 0x000C9213
		public SqlTriggerAttribute()
		{
			this.m_fName = null;
			this.m_fTarget = null;
			this.m_fEvent = null;
		}

		/// <summary>The name of the trigger.</summary>
		/// <returns>A <see cref="T:System.String" /> value representing the name of the trigger.</returns>
		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x06002F11 RID: 12049 RVA: 0x000CB030 File Offset: 0x000C9230
		// (set) Token: 0x06002F12 RID: 12050 RVA: 0x000CB038 File Offset: 0x000C9238
		public string Name
		{
			get
			{
				return this.m_fName;
			}
			set
			{
				this.m_fName = value;
			}
		}

		/// <summary>The table to which the trigger applies.</summary>
		/// <returns>A <see cref="T:System.String" /> value representing the table name.</returns>
		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x06002F13 RID: 12051 RVA: 0x000CB041 File Offset: 0x000C9241
		// (set) Token: 0x06002F14 RID: 12052 RVA: 0x000CB049 File Offset: 0x000C9249
		public string Target
		{
			get
			{
				return this.m_fTarget;
			}
			set
			{
				this.m_fTarget = value;
			}
		}

		/// <summary>The type of trigger and what data manipulation language (DML) action activates the trigger.</summary>
		/// <returns>A <see cref="T:System.String" /> value representing the type of trigger and what data manipulation language (DML) action activates the trigger.</returns>
		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x06002F15 RID: 12053 RVA: 0x000CB052 File Offset: 0x000C9252
		// (set) Token: 0x06002F16 RID: 12054 RVA: 0x000CB05A File Offset: 0x000C925A
		public string Event
		{
			get
			{
				return this.m_fEvent;
			}
			set
			{
				this.m_fEvent = value;
			}
		}

		// Token: 0x04001BCB RID: 7115
		private string m_fName;

		// Token: 0x04001BCC RID: 7116
		private string m_fTarget;

		// Token: 0x04001BCD RID: 7117
		private string m_fEvent;
	}
}
