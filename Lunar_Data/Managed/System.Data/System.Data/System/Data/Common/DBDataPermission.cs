using System;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Security;
using System.Security.Permissions;

namespace System.Data.Common
{
	/// <summary>Enables a .NET Framework data provider to help ensure that a user has a security level adequate for accessing data.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200037E RID: 894
	[SecurityPermission(SecurityAction.InheritanceDemand, ControlEvidence = true, ControlPolicy = true)]
	[Serializable]
	public abstract class DBDataPermission : CodeAccessPermission, IUnrestrictedPermission
	{
		/// <summary>Initializes a new instance of a DBDataPermission class.</summary>
		// Token: 0x06002B19 RID: 11033 RVA: 0x000BE63C File Offset: 0x000BC83C
		[Obsolete("DBDataPermission() has been deprecated.  Use the DBDataPermission(PermissionState.None) constructor.  http://go.microsoft.com/fwlink/?linkid=14202", true)]
		protected DBDataPermission()
			: this(PermissionState.None)
		{
		}

		/// <summary>Initializes a new instance of a DBDataPermission class with the specified <see cref="T:System.Security.Permissions.PermissionState" /> value.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values. </param>
		// Token: 0x06002B1A RID: 11034 RVA: 0x000BE645 File Offset: 0x000BC845
		protected DBDataPermission(PermissionState state)
		{
			this._keyvaluetree = NameValuePermission.Default;
			base..ctor();
			if (state == PermissionState.Unrestricted)
			{
				this._isUnrestricted = true;
				return;
			}
			if (state == PermissionState.None)
			{
				this._isUnrestricted = false;
				return;
			}
			throw ADP.InvalidPermissionState(state);
		}

		/// <summary>Initializes a new instance of a DBDataPermission class with the specified <see cref="T:System.Security.Permissions.PermissionState" /> value, and a value indicating whether a blank password is allowed.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values. </param>
		/// <param name="allowBlankPassword">Indicates whether a blank password is allowed. </param>
		// Token: 0x06002B1B RID: 11035 RVA: 0x000BE675 File Offset: 0x000BC875
		[Obsolete("DBDataPermission(PermissionState state,Boolean allowBlankPassword) has been deprecated.  Use the DBDataPermission(PermissionState.None) constructor.  http://go.microsoft.com/fwlink/?linkid=14202", true)]
		protected DBDataPermission(PermissionState state, bool allowBlankPassword)
			: this(state)
		{
			this.AllowBlankPassword = allowBlankPassword;
		}

		/// <summary>Initializes a new instance of a DBDataPermission class using an existing DBDataPermission.</summary>
		/// <param name="permission">An existing DBDataPermission used to create a new DBDataPermission. </param>
		// Token: 0x06002B1C RID: 11036 RVA: 0x000BE685 File Offset: 0x000BC885
		protected DBDataPermission(DBDataPermission permission)
		{
			this._keyvaluetree = NameValuePermission.Default;
			base..ctor();
			if (permission == null)
			{
				throw ADP.ArgumentNull("permissionAttribute");
			}
			this.CopyFrom(permission);
		}

		/// <summary>Initializes a new instance of a DBDataPermission class with the specified DBDataPermissionAttribute.</summary>
		/// <param name="permissionAttribute">A security action associated with a custom security attribute. </param>
		// Token: 0x06002B1D RID: 11037 RVA: 0x000BE6B0 File Offset: 0x000BC8B0
		protected DBDataPermission(DBDataPermissionAttribute permissionAttribute)
		{
			this._keyvaluetree = NameValuePermission.Default;
			base..ctor();
			if (permissionAttribute == null)
			{
				throw ADP.ArgumentNull("permissionAttribute");
			}
			this._isUnrestricted = permissionAttribute.Unrestricted;
			if (!this._isUnrestricted)
			{
				this._allowBlankPassword = permissionAttribute.AllowBlankPassword;
				if (permissionAttribute.ShouldSerializeConnectionString() || permissionAttribute.ShouldSerializeKeyRestrictions())
				{
					this.Add(permissionAttribute.ConnectionString, permissionAttribute.KeyRestrictions, permissionAttribute.KeyRestrictionBehavior);
				}
			}
		}

		// Token: 0x06002B1E RID: 11038 RVA: 0x000BE724 File Offset: 0x000BC924
		internal DBDataPermission(DbConnectionOptions connectionOptions)
		{
			this._keyvaluetree = NameValuePermission.Default;
			base..ctor();
			if (connectionOptions != null)
			{
				this._allowBlankPassword = connectionOptions.HasBlankPassword;
				this.AddPermissionEntry(new DBConnectionString(connectionOptions));
			}
		}

		/// <summary>Gets a value indicating whether a blank password is allowed.</summary>
		/// <returns>true if a blank password is allowed, otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x06002B1F RID: 11039 RVA: 0x000BE752 File Offset: 0x000BC952
		// (set) Token: 0x06002B20 RID: 11040 RVA: 0x000BE75A File Offset: 0x000BC95A
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

		/// <summary>Adds access for the specified connection string to the existing state of the DBDataPermission. </summary>
		/// <param name="connectionString">A permitted connection string.</param>
		/// <param name="restrictions">String that identifies connection string parameters that are allowed or disallowed.</param>
		/// <param name="behavior">One of the <see cref="T:System.Data.KeyRestrictionBehavior" /> properties.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002B21 RID: 11041 RVA: 0x000BE764 File Offset: 0x000BC964
		public virtual void Add(string connectionString, string restrictions, KeyRestrictionBehavior behavior)
		{
			DBConnectionString dbconnectionString = new DBConnectionString(connectionString, restrictions, behavior, null, false);
			this.AddPermissionEntry(dbconnectionString);
		}

		// Token: 0x06002B22 RID: 11042 RVA: 0x000BE784 File Offset: 0x000BC984
		internal void AddPermissionEntry(DBConnectionString entry)
		{
			if (this._keyvaluetree == null)
			{
				this._keyvaluetree = new NameValuePermission();
			}
			if (this._keyvalues == null)
			{
				this._keyvalues = new ArrayList();
			}
			NameValuePermission.AddEntry(this._keyvaluetree, this._keyvalues, entry);
			this._isUnrestricted = false;
		}

		/// <summary>Removes all permissions that were previous added using the <see cref="M:System.Data.Common.DBDataPermission.Add(System.String,System.String,System.Data.KeyRestrictionBehavior)" /> method.</summary>
		// Token: 0x06002B23 RID: 11043 RVA: 0x000BE7D0 File Offset: 0x000BC9D0
		protected void Clear()
		{
			this._keyvaluetree = null;
			this._keyvalues = null;
		}

		/// <summary>Creates and returns an identical copy of the current permission object.</summary>
		/// <returns>A copy of the current permission object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002B24 RID: 11044 RVA: 0x000BE7E0 File Offset: 0x000BC9E0
		public override IPermission Copy()
		{
			DBDataPermission dbdataPermission = this.CreateInstance();
			dbdataPermission.CopyFrom(this);
			return dbdataPermission;
		}

		// Token: 0x06002B25 RID: 11045 RVA: 0x000BE7F0 File Offset: 0x000BC9F0
		private void CopyFrom(DBDataPermission permission)
		{
			this._isUnrestricted = permission.IsUnrestricted();
			if (!this._isUnrestricted)
			{
				this._allowBlankPassword = permission.AllowBlankPassword;
				if (permission._keyvalues != null)
				{
					this._keyvalues = (ArrayList)permission._keyvalues.Clone();
					if (permission._keyvaluetree != null)
					{
						this._keyvaluetree = permission._keyvaluetree.CopyNameValue();
					}
				}
			}
		}

		/// <summary>Creates a new instance of the DBDataPermission class.</summary>
		/// <returns>A new DBDataPermission object.</returns>
		// Token: 0x06002B26 RID: 11046 RVA: 0x000BE854 File Offset: 0x000BCA54
		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		protected virtual DBDataPermission CreateInstance()
		{
			return Activator.CreateInstance(base.GetType(), BindingFlags.Instance | BindingFlags.Public, null, null, CultureInfo.InvariantCulture, null) as DBDataPermission;
		}

		/// <summary>Returns a new permission object representing the intersection of the current permission object and the specified permission object.</summary>
		/// <returns>A new permission object that represents the intersection of the current permission object and the specified permission object. This new permission object is a null reference (Nothing in Visual Basic) if the intersection is empty.</returns>
		/// <param name="target">A permission object to intersect with the current permission object. It must be of the same type as the current permission object. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not a null reference (Nothing in Visual Basic) and is not an instance of the same class as the current permission object. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002B27 RID: 11047 RVA: 0x000BE870 File Offset: 0x000BCA70
		public override IPermission Intersect(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			if (target.GetType() != base.GetType())
			{
				throw ADP.PermissionTypeMismatch();
			}
			if (this.IsUnrestricted())
			{
				return target.Copy();
			}
			DBDataPermission dbdataPermission = (DBDataPermission)target;
			if (dbdataPermission.IsUnrestricted())
			{
				return this.Copy();
			}
			DBDataPermission dbdataPermission2 = (DBDataPermission)dbdataPermission.Copy();
			dbdataPermission2._allowBlankPassword &= this.AllowBlankPassword;
			if (this._keyvalues != null && dbdataPermission2._keyvalues != null)
			{
				dbdataPermission2._keyvalues.Clear();
				dbdataPermission2._keyvaluetree.Intersect(dbdataPermission2._keyvalues, this._keyvaluetree);
			}
			else
			{
				dbdataPermission2._keyvalues = null;
				dbdataPermission2._keyvaluetree = null;
			}
			if (dbdataPermission2.IsEmpty())
			{
				dbdataPermission2 = null;
			}
			return dbdataPermission2;
		}

		// Token: 0x06002B28 RID: 11048 RVA: 0x000BE92C File Offset: 0x000BCB2C
		private bool IsEmpty()
		{
			ArrayList keyvalues = this._keyvalues;
			return !this.IsUnrestricted() && !this.AllowBlankPassword && (keyvalues == null || keyvalues.Count == 0);
		}

		/// <summary>Returns a value indicating whether the current permission object is a subset of the specified permission object.</summary>
		/// <returns>true if the current permission object is a subset of the specified permission object, otherwise false.</returns>
		/// <param name="target">A permission object that is to be tested for the subset relationship. This object must be of the same type as the current permission object. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is an object that is not of the same type as the current permission object. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002B29 RID: 11049 RVA: 0x000BE960 File Offset: 0x000BCB60
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return this.IsEmpty();
			}
			if (target.GetType() != base.GetType())
			{
				throw ADP.PermissionTypeMismatch();
			}
			DBDataPermission dbdataPermission = target as DBDataPermission;
			bool flag = dbdataPermission.IsUnrestricted();
			if (!flag && !this.IsUnrestricted() && (!this.AllowBlankPassword || dbdataPermission.AllowBlankPassword) && (this._keyvalues == null || dbdataPermission._keyvaluetree != null))
			{
				flag = true;
				if (this._keyvalues != null)
				{
					foreach (object obj in this._keyvalues)
					{
						DBConnectionString dbconnectionString = (DBConnectionString)obj;
						if (!dbdataPermission._keyvaluetree.CheckValueForKeyPermit(dbconnectionString))
						{
							flag = false;
							break;
						}
					}
				}
			}
			return flag;
		}

		/// <summary>Returns a value indicating whether the permission can be represented as unrestricted without any knowledge of the permission semantics.</summary>
		/// <returns>true if the permission can be represented as unrestricted.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002B2A RID: 11050 RVA: 0x000BEA30 File Offset: 0x000BCC30
		public bool IsUnrestricted()
		{
			return this._isUnrestricted;
		}

		/// <summary>Returns a new permission object that is the union of the current and specified permission objects.</summary>
		/// <returns>A new permission object that represents the union of the current permission object and the specified permission object.</returns>
		/// <param name="target">A permission object to combine with the current permission object. It must be of the same type as the current permission object. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> object is not the same type as the current permission object.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002B2B RID: 11051 RVA: 0x000BEA38 File Offset: 0x000BCC38
		public override IPermission Union(IPermission target)
		{
			if (target == null)
			{
				return this.Copy();
			}
			if (target.GetType() != base.GetType())
			{
				throw ADP.PermissionTypeMismatch();
			}
			if (this.IsUnrestricted())
			{
				return this.Copy();
			}
			DBDataPermission dbdataPermission = (DBDataPermission)target.Copy();
			if (!dbdataPermission.IsUnrestricted())
			{
				dbdataPermission._allowBlankPassword |= this.AllowBlankPassword;
				if (this._keyvalues != null)
				{
					foreach (object obj in this._keyvalues)
					{
						DBConnectionString dbconnectionString = (DBConnectionString)obj;
						dbdataPermission.AddPermissionEntry(dbconnectionString);
					}
				}
			}
			if (!dbdataPermission.IsEmpty())
			{
				return dbdataPermission;
			}
			return null;
		}

		// Token: 0x06002B2C RID: 11052 RVA: 0x000BEB00 File Offset: 0x000BCD00
		private string DecodeXmlValue(string value)
		{
			if (value != null && 0 < value.Length)
			{
				value = value.Replace("&quot;", "\"");
				value = value.Replace("&apos;", "'");
				value = value.Replace("&lt;", "<");
				value = value.Replace("&gt;", ">");
				value = value.Replace("&amp;", "&");
			}
			return value;
		}

		// Token: 0x06002B2D RID: 11053 RVA: 0x000BEB74 File Offset: 0x000BCD74
		private string EncodeXmlValue(string value)
		{
			if (value != null && 0 < value.Length)
			{
				value = value.Replace('\0', ' ');
				value = value.Trim();
				value = value.Replace("&", "&amp;");
				value = value.Replace(">", "&gt;");
				value = value.Replace("<", "&lt;");
				value = value.Replace("'", "&apos;");
				value = value.Replace("\"", "&quot;");
			}
			return value;
		}

		/// <summary>Reconstructs a security object with a specified state from an XML encoding.</summary>
		/// <param name="securityElement">The XML encoding to use to reconstruct the security object. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002B2E RID: 11054 RVA: 0x000BEBFC File Offset: 0x000BCDFC
		public override void FromXml(SecurityElement securityElement)
		{
			if (securityElement == null)
			{
				throw ADP.ArgumentNull("securityElement");
			}
			string text = securityElement.Tag;
			if (!text.Equals("Permission") && !text.Equals("IPermission"))
			{
				throw ADP.NotAPermissionElement();
			}
			string text2 = securityElement.Attribute("version");
			if (text2 != null && !text2.Equals("1"))
			{
				throw ADP.InvalidXMLBadVersion();
			}
			string text3 = securityElement.Attribute("Unrestricted");
			this._isUnrestricted = text3 != null && bool.Parse(text3);
			this.Clear();
			if (!this._isUnrestricted)
			{
				string text4 = securityElement.Attribute("AllowBlankPassword");
				this._allowBlankPassword = text4 != null && bool.Parse(text4);
				ArrayList children = securityElement.Children;
				if (children == null)
				{
					return;
				}
				using (IEnumerator enumerator = children.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						SecurityElement securityElement2 = (SecurityElement)obj;
						text = securityElement2.Tag;
						if ("add" == text || (text != null && "add" == text.ToLower(CultureInfo.InvariantCulture)))
						{
							string text5 = securityElement2.Attribute("ConnectionString");
							string text6 = securityElement2.Attribute("KeyRestrictions");
							string text7 = securityElement2.Attribute("KeyRestrictionBehavior");
							KeyRestrictionBehavior keyRestrictionBehavior = KeyRestrictionBehavior.AllowOnly;
							if (text7 != null)
							{
								keyRestrictionBehavior = (KeyRestrictionBehavior)Enum.Parse(typeof(KeyRestrictionBehavior), text7, true);
							}
							text5 = this.DecodeXmlValue(text5);
							text6 = this.DecodeXmlValue(text6);
							this.Add(text5, text6, keyRestrictionBehavior);
						}
					}
					return;
				}
			}
			this._allowBlankPassword = false;
		}

		/// <summary>Creates an XML encoding of the security object and its current state.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002B2F RID: 11055 RVA: 0x000BEDB0 File Offset: 0x000BCFB0
		public override SecurityElement ToXml()
		{
			Type type = base.GetType();
			SecurityElement securityElement = new SecurityElement("IPermission");
			securityElement.AddAttribute("class", type.AssemblyQualifiedName.Replace('"', '\''));
			securityElement.AddAttribute("version", "1");
			if (this.IsUnrestricted())
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			else
			{
				securityElement.AddAttribute("AllowBlankPassword", this._allowBlankPassword.ToString(CultureInfo.InvariantCulture));
				if (this._keyvalues != null)
				{
					foreach (object obj in this._keyvalues)
					{
						DBConnectionString dbconnectionString = (DBConnectionString)obj;
						SecurityElement securityElement2 = new SecurityElement("add");
						string text = dbconnectionString.ConnectionString;
						text = this.EncodeXmlValue(text);
						if (!ADP.IsEmpty(text))
						{
							securityElement2.AddAttribute("ConnectionString", text);
						}
						text = dbconnectionString.Restrictions;
						text = this.EncodeXmlValue(text);
						if (text == null)
						{
							text = ADP.StrEmpty;
						}
						securityElement2.AddAttribute("KeyRestrictions", text);
						text = dbconnectionString.Behavior.ToString();
						securityElement2.AddAttribute("KeyRestrictionBehavior", text);
						securityElement.AddChild(securityElement2);
					}
				}
			}
			return securityElement;
		}

		// Token: 0x04001A4E RID: 6734
		private bool _isUnrestricted;

		// Token: 0x04001A4F RID: 6735
		private bool _allowBlankPassword;

		// Token: 0x04001A50 RID: 6736
		private NameValuePermission _keyvaluetree;

		// Token: 0x04001A51 RID: 6737
		private ArrayList _keyvalues;

		// Token: 0x0200037F RID: 895
		private static class XmlStr
		{
			// Token: 0x04001A52 RID: 6738
			internal const string _class = "class";

			// Token: 0x04001A53 RID: 6739
			internal const string _IPermission = "IPermission";

			// Token: 0x04001A54 RID: 6740
			internal const string _Permission = "Permission";

			// Token: 0x04001A55 RID: 6741
			internal const string _Unrestricted = "Unrestricted";

			// Token: 0x04001A56 RID: 6742
			internal const string _AllowBlankPassword = "AllowBlankPassword";

			// Token: 0x04001A57 RID: 6743
			internal const string _true = "true";

			// Token: 0x04001A58 RID: 6744
			internal const string _Version = "version";

			// Token: 0x04001A59 RID: 6745
			internal const string _VersionNumber = "1";

			// Token: 0x04001A5A RID: 6746
			internal const string _add = "add";

			// Token: 0x04001A5B RID: 6747
			internal const string _ConnectionString = "ConnectionString";

			// Token: 0x04001A5C RID: 6748
			internal const string _KeyRestrictions = "KeyRestrictions";

			// Token: 0x04001A5D RID: 6749
			internal const string _KeyRestrictionBehavior = "KeyRestrictionBehavior";
		}
	}
}
