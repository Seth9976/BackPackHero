using System;
using System.Security.Principal;

namespace System.Net
{
	/// <summary>Holds the user name and password from a basic authentication request.</summary>
	// Token: 0x0200049B RID: 1179
	public class HttpListenerBasicIdentity : GenericIdentity
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.HttpListenerBasicIdentity" /> class using the specified user name and password.</summary>
		/// <param name="username">The user name.</param>
		/// <param name="password">The password.</param>
		// Token: 0x0600254F RID: 9551 RVA: 0x0008A381 File Offset: 0x00088581
		public HttpListenerBasicIdentity(string username, string password)
			: base(username, "Basic")
		{
			this.password = password;
		}

		/// <summary>Indicates the password from a basic authentication attempt.</summary>
		/// <returns>A <see cref="T:System.String" /> that holds the password.</returns>
		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x06002550 RID: 9552 RVA: 0x0008A396 File Offset: 0x00088596
		public virtual string Password
		{
			get
			{
				return this.password;
			}
		}

		// Token: 0x040015AE RID: 5550
		private string password;
	}
}
