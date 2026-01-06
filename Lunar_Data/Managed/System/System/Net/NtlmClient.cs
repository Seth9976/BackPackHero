using System;
using Mono.Http;

namespace System.Net
{
	// Token: 0x020004BB RID: 1211
	internal class NtlmClient : IAuthenticationModule
	{
		// Token: 0x060026F7 RID: 9975 RVA: 0x00090BFA File Offset: 0x0008EDFA
		public NtlmClient()
		{
			this.authObject = new NtlmClient();
		}

		// Token: 0x060026F8 RID: 9976 RVA: 0x00090C0D File Offset: 0x0008EE0D
		public Authorization Authenticate(string challenge, WebRequest webRequest, ICredentials credentials)
		{
			if (this.authObject == null)
			{
				return null;
			}
			return this.authObject.Authenticate(challenge, webRequest, credentials);
		}

		// Token: 0x060026F9 RID: 9977 RVA: 0x00002F6A File Offset: 0x0000116A
		public Authorization PreAuthenticate(WebRequest webRequest, ICredentials credentials)
		{
			return null;
		}

		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x060026FA RID: 9978 RVA: 0x0000C9C8 File Offset: 0x0000ABC8
		public string AuthenticationType
		{
			get
			{
				return "NTLM";
			}
		}

		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x060026FB RID: 9979 RVA: 0x00003062 File Offset: 0x00001262
		public bool CanPreAuthenticate
		{
			get
			{
				return false;
			}
		}

		// Token: 0x040016A5 RID: 5797
		private IAuthenticationModule authObject;
	}
}
