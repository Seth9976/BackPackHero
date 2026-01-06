using System;
using System.Data.Common;
using System.Security;
using System.Security.Permissions;

namespace System.Data.Odbc
{
	/// <summary>Enables the .NET Framework Data Provider for ODBC to help make sure that a user has a security level sufficient to access an ODBC data source. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002B0 RID: 688
	[Serializable]
	public sealed class OdbcPermission : DBDataPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcPermission" /> class.</summary>
		// Token: 0x06001E24 RID: 7716 RVA: 0x00093139 File Offset: 0x00091339
		[Obsolete("OdbcPermission() has been deprecated.  Use the OdbcPermission(PermissionState.None) constructor.  http://go.microsoft.com/fwlink/?linkid=14202", true)]
		public OdbcPermission()
			: this(PermissionState.None)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcPermission" /> class with one of the <see cref="T:System.Security.Permissions.PermissionState" /> values.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values. </param>
		// Token: 0x06001E25 RID: 7717 RVA: 0x0004EEAB File Offset: 0x0004D0AB
		public OdbcPermission(PermissionState state)
			: base(state)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcPermission" /> class.</summary>
		/// <param name="state">One of the System.Security.Permissions.PermissionState values. </param>
		/// <param name="allowBlankPassword">Indicates whether a blank password is allowed. </param>
		// Token: 0x06001E26 RID: 7718 RVA: 0x00093142 File Offset: 0x00091342
		[Obsolete("OdbcPermission(PermissionState state, Boolean allowBlankPassword) has been deprecated.  Use the OdbcPermission(PermissionState.None) constructor.  http://go.microsoft.com/fwlink/?linkid=14202", true)]
		public OdbcPermission(PermissionState state, bool allowBlankPassword)
			: this(state)
		{
			base.AllowBlankPassword = allowBlankPassword;
		}

		// Token: 0x06001E27 RID: 7719 RVA: 0x0004EEC4 File Offset: 0x0004D0C4
		private OdbcPermission(OdbcPermission permission)
			: base(permission)
		{
		}

		// Token: 0x06001E28 RID: 7720 RVA: 0x0004EECD File Offset: 0x0004D0CD
		internal OdbcPermission(OdbcPermissionAttribute permissionAttribute)
			: base(permissionAttribute)
		{
		}

		// Token: 0x06001E29 RID: 7721 RVA: 0x0004EED6 File Offset: 0x0004D0D6
		internal OdbcPermission(OdbcConnectionString constr)
			: base(constr)
		{
			if (constr == null || constr.IsEmpty)
			{
				base.Add(ADP.StrEmpty, ADP.StrEmpty, KeyRestrictionBehavior.AllowOnly);
			}
		}

		/// <summary>Adds access for the specified connection string to the existing state of the permission.</summary>
		/// <param name="connectionString">A permitted connection string. </param>
		/// <param name="restrictions">String that identifies connection string parameters that are allowed or disallowed. </param>
		/// <param name="behavior">One of the <see cref="T:System.Data.KeyRestrictionBehavior" /> values. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001E2A RID: 7722 RVA: 0x00093154 File Offset: 0x00091354
		public override void Add(string connectionString, string restrictions, KeyRestrictionBehavior behavior)
		{
			DBConnectionString dbconnectionString = new DBConnectionString(connectionString, restrictions, behavior, null, true);
			base.AddPermissionEntry(dbconnectionString);
		}

		/// <summary>Returns the <see cref="T:System.Data.Odbc.OdbcPermission" /> as an <see cref="T:System.Security.IPermission" />.</summary>
		/// <returns>A copy of the current permission object.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06001E2B RID: 7723 RVA: 0x00093173 File Offset: 0x00091373
		public override IPermission Copy()
		{
			return new OdbcPermission(this);
		}
	}
}
