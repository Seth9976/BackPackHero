using System;
using System.Threading.Tasks;
using Unity;

namespace System.Data.SqlClient
{
	// Token: 0x020003E4 RID: 996
	public abstract class SqlAuthenticationProvider
	{
		// Token: 0x06002F69 RID: 12137 RVA: 0x0000E24C File Offset: 0x0000C44C
		protected SqlAuthenticationProvider()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x06002F6A RID: 12138
		public abstract Task<SqlAuthenticationToken> AcquireTokenAsync(SqlAuthenticationParameters parameters);

		// Token: 0x06002F6B RID: 12139 RVA: 0x0000E24C File Offset: 0x0000C44C
		public virtual void BeforeLoad(SqlAuthenticationMethod authenticationMethod)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x06002F6C RID: 12140 RVA: 0x0000E24C File Offset: 0x0000C44C
		public virtual void BeforeUnload(SqlAuthenticationMethod authenticationMethod)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x06002F6D RID: 12141 RVA: 0x0005503D File Offset: 0x0005323D
		public static SqlAuthenticationProvider GetProvider(SqlAuthenticationMethod authenticationMethod)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x06002F6E RID: 12142
		public abstract bool IsSupported(SqlAuthenticationMethod authenticationMethod);

		// Token: 0x06002F6F RID: 12143 RVA: 0x000CB3B0 File Offset: 0x000C95B0
		public static bool SetProvider(SqlAuthenticationMethod authenticationMethod, SqlAuthenticationProvider provider)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}
	}
}
