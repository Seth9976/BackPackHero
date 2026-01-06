using System;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Data.SqlClient
{
	// Token: 0x020003E3 RID: 995
	public class SqlAuthenticationParameters
	{
		// Token: 0x06002F60 RID: 12128 RVA: 0x0000E24C File Offset: 0x0000C44C
		protected SqlAuthenticationParameters(SqlAuthenticationMethod authenticationMethod, string serverName, string databaseName, string resource, string authority, string userId, string password, Guid connectionId)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x06002F61 RID: 12129 RVA: 0x000CB378 File Offset: 0x000C9578
		public SqlAuthenticationMethod AuthenticationMethod
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return SqlAuthenticationMethod.NotSpecified;
			}
		}

		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x06002F62 RID: 12130 RVA: 0x0005503D File Offset: 0x0005323D
		public string Authority
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x06002F63 RID: 12131 RVA: 0x000CB394 File Offset: 0x000C9594
		public Guid ConnectionId
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(Guid);
			}
		}

		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x06002F64 RID: 12132 RVA: 0x0005503D File Offset: 0x0005323D
		public string DatabaseName
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x06002F65 RID: 12133 RVA: 0x0005503D File Offset: 0x0005323D
		public string Password
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x06002F66 RID: 12134 RVA: 0x0005503D File Offset: 0x0005323D
		public string Resource
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x06002F67 RID: 12135 RVA: 0x0005503D File Offset: 0x0005323D
		public string ServerName
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x06002F68 RID: 12136 RVA: 0x0005503D File Offset: 0x0005323D
		public string UserId
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}
	}
}
