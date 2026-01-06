using System;

namespace Microsoft.SqlServer.Server
{
	/// <summary>Used to mark a method definition in an assembly as a stored procedure. The properties on the attribute reflect the physical characteristics used when the type is registered with SQL Server. This class cannot be inherited.</summary>
	// Token: 0x020003C6 RID: 966
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	[Serializable]
	public sealed class SqlProcedureAttribute : Attribute
	{
		/// <summary>An attribute on a method definition in an assembly, used to indicate that the given method should be registered as a stored procedure in SQL Server.</summary>
		// Token: 0x06002F0D RID: 12045 RVA: 0x000CAFF3 File Offset: 0x000C91F3
		public SqlProcedureAttribute()
		{
			this.m_fName = null;
		}

		/// <summary>The name of the stored procedure.</summary>
		/// <returns>A <see cref="T:System.String" /> representing the name of the stored procedure.</returns>
		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x06002F0E RID: 12046 RVA: 0x000CB002 File Offset: 0x000C9202
		// (set) Token: 0x06002F0F RID: 12047 RVA: 0x000CB00A File Offset: 0x000C920A
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

		// Token: 0x04001BCA RID: 7114
		private string m_fName;
	}
}
