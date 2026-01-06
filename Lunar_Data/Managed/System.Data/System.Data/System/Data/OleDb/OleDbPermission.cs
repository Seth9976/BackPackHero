using System;
using System.ComponentModel;
using System.Data.Common;
using System.Security;
using System.Security.Permissions;

namespace System.Data.OleDb
{
	/// <summary>Enables the .NET Framework Data Provider for OLE DB to help make sure that a user has a security level sufficient to access an OLE DB data source.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000109 RID: 265
	[Serializable]
	public sealed class OleDbPermission : DBDataPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbPermission" /> class.</summary>
		// Token: 0x06000E95 RID: 3733 RVA: 0x0004EEA2 File Offset: 0x0004D0A2
		[Obsolete("OleDbPermission() has been deprecated.  Use the OleDbPermission(PermissionState.None) constructor.  http://go.microsoft.com/fwlink/?linkid=14202", true)]
		public OleDbPermission()
			: this(PermissionState.None)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbPermission" /> class.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values. </param>
		// Token: 0x06000E96 RID: 3734 RVA: 0x0004EEAB File Offset: 0x0004D0AB
		public OleDbPermission(PermissionState state)
			: base(state)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbPermission" /> class.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values. </param>
		/// <param name="allowBlankPassword">Indicates whether a blank password is allowed. </param>
		// Token: 0x06000E97 RID: 3735 RVA: 0x0004EEB4 File Offset: 0x0004D0B4
		[Obsolete("OleDbPermission(PermissionState state, Boolean allowBlankPassword) has been deprecated.  Use the OleDbPermission(PermissionState.None) constructor.  http://go.microsoft.com/fwlink/?linkid=14202", true)]
		public OleDbPermission(PermissionState state, bool allowBlankPassword)
			: this(state)
		{
			base.AllowBlankPassword = allowBlankPassword;
		}

		// Token: 0x06000E98 RID: 3736 RVA: 0x0004EEC4 File Offset: 0x0004D0C4
		private OleDbPermission(OleDbPermission permission)
			: base(permission)
		{
		}

		// Token: 0x06000E99 RID: 3737 RVA: 0x0004EECD File Offset: 0x0004D0CD
		internal OleDbPermission(OleDbPermissionAttribute permissionAttribute)
			: base(permissionAttribute)
		{
		}

		// Token: 0x06000E9A RID: 3738 RVA: 0x0004EED6 File Offset: 0x0004D0D6
		internal OleDbPermission(OleDbConnectionString constr)
			: base(constr)
		{
			if (constr == null || constr.IsEmpty)
			{
				base.Add(ADP.StrEmpty, ADP.StrEmpty, KeyRestrictionBehavior.AllowOnly);
			}
		}

		/// <summary>This property has been marked as obsolete. Setting this property will have no effect.</summary>
		/// <returns>This property has been marked as obsolete. Setting this property will have no effect.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000E9B RID: 3739 RVA: 0x0004EEFC File Offset: 0x0004D0FC
		// (set) Token: 0x06000E9C RID: 3740 RVA: 0x0004EF4C File Offset: 0x0004D14C
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Provider property has been deprecated.  Use the Add method.  http://go.microsoft.com/fwlink/?linkid=14202")]
		public string Provider
		{
			get
			{
				string text = this._providers;
				if (text == null)
				{
					string[] providerRestriction = this._providerRestriction;
					if (providerRestriction != null && providerRestriction.Length != 0)
					{
						text = providerRestriction[0];
						for (int i = 1; i < providerRestriction.Length; i++)
						{
							text = text + ";" + providerRestriction[i];
						}
					}
				}
				if (text == null)
				{
					return ADP.StrEmpty;
				}
				return text;
			}
			set
			{
				string[] array = null;
				if (!ADP.IsEmpty(value))
				{
					array = value.Split(new char[] { ';' });
					array = DBConnectionString.RemoveDuplicates(array);
				}
				this._providerRestriction = array;
				this._providers = value;
			}
		}

		/// <summary>Returns the <see cref="T:System.Data.OleDb.OleDbPermission" /> as an <see cref="T:System.Security.IPermission" />.</summary>
		/// <returns>A copy of the current permission object.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000E9D RID: 3741 RVA: 0x0004EF8A File Offset: 0x0004D18A
		public override IPermission Copy()
		{
			return new OleDbPermission(this);
		}

		// Token: 0x04000997 RID: 2455
		private string[] _providerRestriction;

		// Token: 0x04000998 RID: 2456
		private string _providers;
	}
}
