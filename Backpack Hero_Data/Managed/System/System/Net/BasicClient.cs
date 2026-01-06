using System;

namespace System.Net
{
	// Token: 0x0200047B RID: 1147
	internal class BasicClient : IAuthenticationModule
	{
		// Token: 0x06002447 RID: 9287 RVA: 0x00085D62 File Offset: 0x00083F62
		public Authorization Authenticate(string challenge, WebRequest webRequest, ICredentials credentials)
		{
			if (credentials == null || challenge == null)
			{
				return null;
			}
			if (challenge.Trim().ToLower().IndexOf("basic", StringComparison.Ordinal) == -1)
			{
				return null;
			}
			return BasicClient.InternalAuthenticate(webRequest, credentials);
		}

		// Token: 0x06002448 RID: 9288 RVA: 0x00085D90 File Offset: 0x00083F90
		private static byte[] GetBytes(string str)
		{
			int i = str.Length;
			byte[] array = new byte[i];
			for (i--; i >= 0; i--)
			{
				array[i] = (byte)str[i];
			}
			return array;
		}

		// Token: 0x06002449 RID: 9289 RVA: 0x00085DC8 File Offset: 0x00083FC8
		private static Authorization InternalAuthenticate(WebRequest webRequest, ICredentials credentials)
		{
			HttpWebRequest httpWebRequest = webRequest as HttpWebRequest;
			if (httpWebRequest == null || credentials == null)
			{
				return null;
			}
			NetworkCredential credential = credentials.GetCredential(httpWebRequest.AuthUri, "basic");
			if (credential == null)
			{
				return null;
			}
			string userName = credential.UserName;
			if (userName == null || userName == "")
			{
				return null;
			}
			string password = credential.Password;
			string domain = credential.Domain;
			byte[] array;
			if (domain == null || domain == "" || domain.Trim() == "")
			{
				array = BasicClient.GetBytes(userName + ":" + password);
			}
			else
			{
				array = BasicClient.GetBytes(string.Concat(new string[] { domain, "\\", userName, ":", password }));
			}
			return new Authorization("Basic " + Convert.ToBase64String(array));
		}

		// Token: 0x0600244A RID: 9290 RVA: 0x00085EA2 File Offset: 0x000840A2
		public Authorization PreAuthenticate(WebRequest webRequest, ICredentials credentials)
		{
			return BasicClient.InternalAuthenticate(webRequest, credentials);
		}

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x0600244B RID: 9291 RVA: 0x00085EAB File Offset: 0x000840AB
		public string AuthenticationType
		{
			get
			{
				return "Basic";
			}
		}

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x0600244C RID: 9292 RVA: 0x0000390E File Offset: 0x00001B0E
		public bool CanPreAuthenticate
		{
			get
			{
				return true;
			}
		}
	}
}
