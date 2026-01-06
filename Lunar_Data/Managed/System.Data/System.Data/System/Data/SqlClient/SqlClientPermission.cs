using System;
using System.Data.Common;
using System.Security;
using System.Security.Permissions;

namespace System.Data.SqlClient
{
	/// <summary>Enables the .NET Framework Data Provider for SQL Server to help make sure that a user has a security level sufficient to access a data source. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200022E RID: 558
	[Serializable]
	public sealed class SqlClientPermission : DBDataPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlClientPermission" /> class.</summary>
		// Token: 0x06001A1C RID: 6684 RVA: 0x000832D2 File Offset: 0x000814D2
		[Obsolete("SqlClientPermission() has been deprecated.  Use the SqlClientPermission(PermissionState.None) constructor.  http://go.microsoft.com/fwlink/?linkid=14202", true)]
		public SqlClientPermission()
			: this(PermissionState.None)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlClientPermission" /> class.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values. </param>
		// Token: 0x06001A1D RID: 6685 RVA: 0x0004EEAB File Offset: 0x0004D0AB
		public SqlClientPermission(PermissionState state)
			: base(state)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlClientPermission" /> class.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values. </param>
		/// <param name="allowBlankPassword">Indicates whether a blank password is allowed. </param>
		// Token: 0x06001A1E RID: 6686 RVA: 0x000832DB File Offset: 0x000814DB
		[Obsolete("SqlClientPermission(PermissionState state, Boolean allowBlankPassword) has been deprecated.  Use the SqlClientPermission(PermissionState.None) constructor.  http://go.microsoft.com/fwlink/?linkid=14202", true)]
		public SqlClientPermission(PermissionState state, bool allowBlankPassword)
			: this(state)
		{
			base.AllowBlankPassword = allowBlankPassword;
		}

		// Token: 0x06001A1F RID: 6687 RVA: 0x0004EEC4 File Offset: 0x0004D0C4
		private SqlClientPermission(SqlClientPermission permission)
			: base(permission)
		{
		}

		// Token: 0x06001A20 RID: 6688 RVA: 0x0004EECD File Offset: 0x0004D0CD
		internal SqlClientPermission(SqlClientPermissionAttribute permissionAttribute)
			: base(permissionAttribute)
		{
		}

		// Token: 0x06001A21 RID: 6689 RVA: 0x0004EED6 File Offset: 0x0004D0D6
		internal SqlClientPermission(SqlConnectionString constr)
			: base(constr)
		{
			if (constr == null || constr.IsEmpty)
			{
				base.Add(ADP.StrEmpty, ADP.StrEmpty, KeyRestrictionBehavior.AllowOnly);
			}
		}

		/// <summary>Adds a new connection string and a set of restricted keywords to the <see cref="T:System.Data.SqlClient.SqlClientPermission" /> object.</summary>
		/// <param name="connectionString">The connection string.</param>
		/// <param name="restrictions">The key restrictions.</param>
		/// <param name="behavior">One of the <see cref="T:System.Data.KeyRestrictionBehavior" /> enumerations.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001A22 RID: 6690 RVA: 0x000832EC File Offset: 0x000814EC
		public override void Add(string connectionString, string restrictions, KeyRestrictionBehavior behavior)
		{
			DBConnectionString dbconnectionString = new DBConnectionString(connectionString, restrictions, behavior, SqlConnectionString.GetParseSynonyms(), false);
			base.AddPermissionEntry(dbconnectionString);
		}

		/// <summary>Returns the <see cref="T:System.Data.SqlClient.SqlClientPermission" /> as an <see cref="T:System.Security.IPermission" />.</summary>
		/// <returns>A copy of the current permission object.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06001A23 RID: 6691 RVA: 0x0008330F File Offset: 0x0008150F
		public override IPermission Copy()
		{
			return new SqlClientPermission(this);
		}
	}
}
