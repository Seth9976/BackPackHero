using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Data.Common
{
	/// <summary>Associates a security action with a custom security attribute. </summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000380 RID: 896
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public abstract class DBDataPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Common.DBDataPermissionAttribute" />.</summary>
		/// <param name="action">One of the security action values representing an action that can be performed by declarative security.</param>
		// Token: 0x06002B30 RID: 11056 RVA: 0x000BEF14 File Offset: 0x000BD114
		protected DBDataPermissionAttribute(SecurityAction action)
			: base(action)
		{
		}

		/// <summary>Gets or sets a value indicating whether a blank password is allowed.</summary>
		/// <returns>true if a blank password is allowed; otherwise false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x06002B31 RID: 11057 RVA: 0x000BEF1D File Offset: 0x000BD11D
		// (set) Token: 0x06002B32 RID: 11058 RVA: 0x000BEF25 File Offset: 0x000BD125
		public bool AllowBlankPassword
		{
			get
			{
				return this._allowBlankPassword;
			}
			set
			{
				this._allowBlankPassword = value;
			}
		}

		/// <summary>Gets or sets a permitted connection string.</summary>
		/// <returns>A permitted connection string.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x06002B33 RID: 11059 RVA: 0x000BEF30 File Offset: 0x000BD130
		// (set) Token: 0x06002B34 RID: 11060 RVA: 0x000BEF4E File Offset: 0x000BD14E
		public string ConnectionString
		{
			get
			{
				string connectionString = this._connectionString;
				if (connectionString == null)
				{
					return string.Empty;
				}
				return connectionString;
			}
			set
			{
				this._connectionString = value;
			}
		}

		/// <summary>Identifies whether the list of connection string parameters identified by the <see cref="P:System.Data.Common.DBDataPermissionAttribute.KeyRestrictions" /> property are the only connection string parameters allowed.</summary>
		/// <returns>One of the <see cref="T:System.Data.KeyRestrictionBehavior" /> values.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x06002B35 RID: 11061 RVA: 0x000BEF57 File Offset: 0x000BD157
		// (set) Token: 0x06002B36 RID: 11062 RVA: 0x000BEF5F File Offset: 0x000BD15F
		public KeyRestrictionBehavior KeyRestrictionBehavior
		{
			get
			{
				return this._behavior;
			}
			set
			{
				if (value <= KeyRestrictionBehavior.PreventUsage)
				{
					this._behavior = value;
					return;
				}
				throw ADP.InvalidKeyRestrictionBehavior(value);
			}
		}

		/// <summary>Gets or sets connection string parameters that are allowed or disallowed.</summary>
		/// <returns>One or more connection string parameters that are allowed or disallowed.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x06002B37 RID: 11063 RVA: 0x000BEF74 File Offset: 0x000BD174
		// (set) Token: 0x06002B38 RID: 11064 RVA: 0x000BEF92 File Offset: 0x000BD192
		public string KeyRestrictions
		{
			get
			{
				string restrictions = this._restrictions;
				if (restrictions == null)
				{
					return ADP.StrEmpty;
				}
				return restrictions;
			}
			set
			{
				this._restrictions = value;
			}
		}

		/// <summary>Identifies whether the attribute should serialize the connection string.</summary>
		/// <returns>true if the attribute should serialize the connection string; otherwise false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002B39 RID: 11065 RVA: 0x000BEF9B File Offset: 0x000BD19B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeConnectionString()
		{
			return this._connectionString != null;
		}

		/// <summary>Identifies whether the attribute should serialize the set of key restrictions.</summary>
		/// <returns>true if the attribute should serialize the set of key restrictions; otherwise false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002B3A RID: 11066 RVA: 0x000BEFA6 File Offset: 0x000BD1A6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeKeyRestrictions()
		{
			return this._restrictions != null;
		}

		// Token: 0x04001A5E RID: 6750
		private bool _allowBlankPassword;

		// Token: 0x04001A5F RID: 6751
		private string _connectionString;

		// Token: 0x04001A60 RID: 6752
		private string _restrictions;

		// Token: 0x04001A61 RID: 6753
		private KeyRestrictionBehavior _behavior;
	}
}
