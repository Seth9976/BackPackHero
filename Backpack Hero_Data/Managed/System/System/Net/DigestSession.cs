using System;
using System.Security.Cryptography;
using System.Text;

namespace System.Net
{
	// Token: 0x02000486 RID: 1158
	internal class DigestSession
	{
		// Token: 0x06002473 RID: 9331 RVA: 0x00086955 File Offset: 0x00084B55
		public DigestSession()
		{
			this._nc = 1;
			this.lastUse = DateTime.UtcNow;
		}

		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x06002474 RID: 9332 RVA: 0x0008696F File Offset: 0x00084B6F
		public string Algorithm
		{
			get
			{
				return this.parser.Algorithm;
			}
		}

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x06002475 RID: 9333 RVA: 0x0008697C File Offset: 0x00084B7C
		public string Realm
		{
			get
			{
				return this.parser.Realm;
			}
		}

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x06002476 RID: 9334 RVA: 0x00086989 File Offset: 0x00084B89
		public string Nonce
		{
			get
			{
				return this.parser.Nonce;
			}
		}

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x06002477 RID: 9335 RVA: 0x00086996 File Offset: 0x00084B96
		public string Opaque
		{
			get
			{
				return this.parser.Opaque;
			}
		}

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x06002478 RID: 9336 RVA: 0x000869A3 File Offset: 0x00084BA3
		public string QOP
		{
			get
			{
				return this.parser.QOP;
			}
		}

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x06002479 RID: 9337 RVA: 0x000869B0 File Offset: 0x00084BB0
		public string CNonce
		{
			get
			{
				if (this._cnonce == null)
				{
					byte[] array = new byte[15];
					DigestSession.rng.GetBytes(array);
					this._cnonce = Convert.ToBase64String(array);
					Array.Clear(array, 0, array.Length);
				}
				return this._cnonce;
			}
		}

		// Token: 0x0600247A RID: 9338 RVA: 0x000869F4 File Offset: 0x00084BF4
		public bool Parse(string challenge)
		{
			this.parser = new DigestHeaderParser(challenge);
			if (!this.parser.Parse())
			{
				return false;
			}
			if (this.parser.Algorithm == null || this.parser.Algorithm.ToUpper().StartsWith("MD5"))
			{
				this.hash = MD5.Create();
			}
			return true;
		}

		// Token: 0x0600247B RID: 9339 RVA: 0x00086A54 File Offset: 0x00084C54
		private string HashToHexString(string toBeHashed)
		{
			if (this.hash == null)
			{
				return null;
			}
			this.hash.Initialize();
			byte[] array = this.hash.ComputeHash(Encoding.ASCII.GetBytes(toBeHashed));
			StringBuilder stringBuilder = new StringBuilder();
			foreach (byte b in array)
			{
				stringBuilder.Append(b.ToString("x2"));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600247C RID: 9340 RVA: 0x00086AC0 File Offset: 0x00084CC0
		private string HA1(string username, string password)
		{
			string text = string.Format("{0}:{1}:{2}", username, this.Realm, password);
			if (this.Algorithm != null && this.Algorithm.ToLower() == "md5-sess")
			{
				text = string.Format("{0}:{1}:{2}", this.HashToHexString(text), this.Nonce, this.CNonce);
			}
			return this.HashToHexString(text);
		}

		// Token: 0x0600247D RID: 9341 RVA: 0x00086B24 File Offset: 0x00084D24
		private string HA2(HttpWebRequest webRequest)
		{
			string text = string.Format("{0}:{1}", webRequest.Method, webRequest.RequestUri.PathAndQuery);
			this.QOP == "auth-int";
			return this.HashToHexString(text);
		}

		// Token: 0x0600247E RID: 9342 RVA: 0x00086B68 File Offset: 0x00084D68
		private string Response(string username, string password, HttpWebRequest webRequest)
		{
			string text = string.Format("{0}:{1}:", this.HA1(username, password), this.Nonce);
			if (this.QOP != null)
			{
				text += string.Format("{0}:{1}:{2}:", this._nc.ToString("X8"), this.CNonce, this.QOP);
			}
			text += this.HA2(webRequest);
			return this.HashToHexString(text);
		}

		// Token: 0x0600247F RID: 9343 RVA: 0x00086BD8 File Offset: 0x00084DD8
		public Authorization Authenticate(WebRequest webRequest, ICredentials credentials)
		{
			if (this.parser == null)
			{
				throw new InvalidOperationException();
			}
			HttpWebRequest httpWebRequest = webRequest as HttpWebRequest;
			if (httpWebRequest == null)
			{
				return null;
			}
			this.lastUse = DateTime.UtcNow;
			NetworkCredential credential = credentials.GetCredential(httpWebRequest.RequestUri, "digest");
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
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("Digest username=\"{0}\", ", userName);
			stringBuilder.AppendFormat("realm=\"{0}\", ", this.Realm);
			stringBuilder.AppendFormat("nonce=\"{0}\", ", this.Nonce);
			stringBuilder.AppendFormat("uri=\"{0}\", ", httpWebRequest.Address.PathAndQuery);
			if (this.Algorithm != null)
			{
				stringBuilder.AppendFormat("algorithm=\"{0}\", ", this.Algorithm);
			}
			stringBuilder.AppendFormat("response=\"{0}\", ", this.Response(userName, password, httpWebRequest));
			if (this.QOP != null)
			{
				stringBuilder.AppendFormat("qop=\"{0}\", ", this.QOP);
			}
			lock (this)
			{
				if (this.QOP != null)
				{
					stringBuilder.AppendFormat("nc={0:X8}, ", this._nc);
					this._nc++;
				}
			}
			if (this.CNonce != null)
			{
				stringBuilder.AppendFormat("cnonce=\"{0}\", ", this.CNonce);
			}
			if (this.Opaque != null)
			{
				stringBuilder.AppendFormat("opaque=\"{0}\", ", this.Opaque);
			}
			stringBuilder.Length -= 2;
			return new Authorization(stringBuilder.ToString());
		}

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x06002480 RID: 9344 RVA: 0x00086D8C File Offset: 0x00084F8C
		public DateTime LastUse
		{
			get
			{
				return this.lastUse;
			}
		}

		// Token: 0x0400154B RID: 5451
		private static RandomNumberGenerator rng = RandomNumberGenerator.Create();

		// Token: 0x0400154C RID: 5452
		private DateTime lastUse;

		// Token: 0x0400154D RID: 5453
		private int _nc;

		// Token: 0x0400154E RID: 5454
		private HashAlgorithm hash;

		// Token: 0x0400154F RID: 5455
		private DigestHeaderParser parser;

		// Token: 0x04001550 RID: 5456
		private string _cnonce;
	}
}
