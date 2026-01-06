using System;
using System.Data.SqlClient;
using System.Security.Principal;

namespace System.Data.ProviderBase
{
	// Token: 0x02000310 RID: 784
	[Serializable]
	internal sealed class DbConnectionPoolIdentity
	{
		// Token: 0x060023AB RID: 9131 RVA: 0x000A4931 File Offset: 0x000A2B31
		internal static DbConnectionPoolIdentity GetCurrent()
		{
			if (!TdsParserStateObjectFactory.UseManagedSNI)
			{
				return DbConnectionPoolIdentity.GetCurrentNative();
			}
			return DbConnectionPoolIdentity.GetCurrentManaged();
		}

		// Token: 0x060023AC RID: 9132 RVA: 0x000A4948 File Offset: 0x000A2B48
		private static DbConnectionPoolIdentity GetCurrentNative()
		{
			DbConnectionPoolIdentity dbConnectionPoolIdentity2;
			using (WindowsIdentity current = WindowsIdentity.GetCurrent())
			{
				IntPtr intPtr = current.AccessToken.DangerousGetHandle();
				bool flag = current.User.IsWellKnown(WellKnownSidType.NetworkSid);
				string value = current.User.Value;
				bool flag2 = Win32NativeMethods.IsTokenRestrictedWrapper(intPtr);
				DbConnectionPoolIdentity dbConnectionPoolIdentity = DbConnectionPoolIdentity.s_lastIdentity;
				if (dbConnectionPoolIdentity != null && dbConnectionPoolIdentity._sidString == value && dbConnectionPoolIdentity._isRestricted == flag2 && dbConnectionPoolIdentity._isNetwork == flag)
				{
					dbConnectionPoolIdentity2 = dbConnectionPoolIdentity;
				}
				else
				{
					dbConnectionPoolIdentity2 = new DbConnectionPoolIdentity(value, flag2, flag);
				}
			}
			DbConnectionPoolIdentity.s_lastIdentity = dbConnectionPoolIdentity2;
			return dbConnectionPoolIdentity2;
		}

		// Token: 0x060023AD RID: 9133 RVA: 0x000A49E8 File Offset: 0x000A2BE8
		private DbConnectionPoolIdentity(string sidString, bool isRestricted, bool isNetwork)
		{
			this._sidString = sidString;
			this._isRestricted = isRestricted;
			this._isNetwork = isNetwork;
			this._hashCode = ((sidString == null) ? 0 : sidString.GetHashCode());
		}

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x060023AE RID: 9134 RVA: 0x000A4A17 File Offset: 0x000A2C17
		internal bool IsRestricted
		{
			get
			{
				return this._isRestricted;
			}
		}

		// Token: 0x060023AF RID: 9135 RVA: 0x000A4A20 File Offset: 0x000A2C20
		public override bool Equals(object value)
		{
			bool flag = this == DbConnectionPoolIdentity.NoIdentity || this == value;
			if (!flag && value != null)
			{
				DbConnectionPoolIdentity dbConnectionPoolIdentity = (DbConnectionPoolIdentity)value;
				flag = this._sidString == dbConnectionPoolIdentity._sidString && this._isRestricted == dbConnectionPoolIdentity._isRestricted && this._isNetwork == dbConnectionPoolIdentity._isNetwork;
			}
			return flag;
		}

		// Token: 0x060023B0 RID: 9136 RVA: 0x000A4A7E File Offset: 0x000A2C7E
		public override int GetHashCode()
		{
			return this._hashCode;
		}

		// Token: 0x060023B1 RID: 9137 RVA: 0x000A4A88 File Offset: 0x000A2C88
		internal static DbConnectionPoolIdentity GetCurrentManaged()
		{
			string text = ((!string.IsNullOrWhiteSpace(Environment.UserDomainName)) ? (Environment.UserDomainName + "\\") : "") + Environment.UserName;
			bool flag = false;
			bool flag2 = false;
			return new DbConnectionPoolIdentity(text, flag2, flag);
		}

		// Token: 0x040017B4 RID: 6068
		private static DbConnectionPoolIdentity s_lastIdentity = null;

		// Token: 0x040017B5 RID: 6069
		public static readonly DbConnectionPoolIdentity NoIdentity = new DbConnectionPoolIdentity(string.Empty, false, true);

		// Token: 0x040017B6 RID: 6070
		private readonly string _sidString;

		// Token: 0x040017B7 RID: 6071
		private readonly bool _isRestricted;

		// Token: 0x040017B8 RID: 6072
		private readonly bool _isNetwork;

		// Token: 0x040017B9 RID: 6073
		private readonly int _hashCode;
	}
}
