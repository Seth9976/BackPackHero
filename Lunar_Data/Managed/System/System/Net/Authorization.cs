using System;

namespace System.Net
{
	/// <summary>Contains an authentication message for an Internet server.</summary>
	// Token: 0x020003D5 RID: 981
	public class Authorization
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Net.Authorization" /> class with the specified authorization message.</summary>
		/// <param name="token">The encrypted authorization message expected by the server. </param>
		// Token: 0x06002049 RID: 8265 RVA: 0x0007661D File Offset: 0x0007481D
		public Authorization(string token)
		{
			this.m_Message = ValidationHelper.MakeStringNull(token);
			this.m_Complete = true;
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Net.Authorization" /> class with the specified authorization message and completion status.</summary>
		/// <param name="token">The encrypted authorization message expected by the server. </param>
		/// <param name="finished">The completion status of the authorization attempt. true if the authorization attempt is complete; otherwise, false. </param>
		// Token: 0x0600204A RID: 8266 RVA: 0x00076638 File Offset: 0x00074838
		public Authorization(string token, bool finished)
		{
			this.m_Message = ValidationHelper.MakeStringNull(token);
			this.m_Complete = finished;
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Net.Authorization" /> class with the specified authorization message, completion status, and connection group identifier.</summary>
		/// <param name="token">The encrypted authorization message expected by the server. </param>
		/// <param name="finished">The completion status of the authorization attempt. true if the authorization attempt is complete; otherwise, false. </param>
		/// <param name="connectionGroupId">A unique identifier that can be used to create private client-server connections that are bound only to this authentication scheme. </param>
		// Token: 0x0600204B RID: 8267 RVA: 0x00076653 File Offset: 0x00074853
		public Authorization(string token, bool finished, string connectionGroupId)
			: this(token, finished, connectionGroupId, false)
		{
		}

		// Token: 0x0600204C RID: 8268 RVA: 0x0007665F File Offset: 0x0007485F
		internal Authorization(string token, bool finished, string connectionGroupId, bool mutualAuth)
		{
			this.m_Message = ValidationHelper.MakeStringNull(token);
			this.m_ConnectionGroupId = ValidationHelper.MakeStringNull(connectionGroupId);
			this.m_Complete = finished;
			this.m_MutualAuth = mutualAuth;
		}

		/// <summary>Gets the message returned to the server in response to an authentication challenge.</summary>
		/// <returns>The message that will be returned to the server in response to an authentication challenge.</returns>
		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x0600204D RID: 8269 RVA: 0x0007668E File Offset: 0x0007488E
		public string Message
		{
			get
			{
				return this.m_Message;
			}
		}

		/// <summary>Gets a unique identifier for user-specific connections.</summary>
		/// <returns>A unique string that associates a connection with an authenticating entity.</returns>
		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x0600204E RID: 8270 RVA: 0x00076696 File Offset: 0x00074896
		public string ConnectionGroupId
		{
			get
			{
				return this.m_ConnectionGroupId;
			}
		}

		/// <summary>Gets the completion status of the authorization.</summary>
		/// <returns>true if the authentication process is complete; otherwise, false.</returns>
		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x0600204F RID: 8271 RVA: 0x0007669E File Offset: 0x0007489E
		public bool Complete
		{
			get
			{
				return this.m_Complete;
			}
		}

		// Token: 0x06002050 RID: 8272 RVA: 0x000766A6 File Offset: 0x000748A6
		internal void SetComplete(bool complete)
		{
			this.m_Complete = complete;
		}

		/// <summary>Gets or sets the prefix for Uniform Resource Identifiers (URIs) that can be authenticated with the <see cref="P:System.Net.Authorization.Message" /> property.</summary>
		/// <returns>An array of strings that contains URI prefixes.</returns>
		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x06002051 RID: 8273 RVA: 0x000766AF File Offset: 0x000748AF
		// (set) Token: 0x06002052 RID: 8274 RVA: 0x000766B8 File Offset: 0x000748B8
		public string[] ProtectionRealm
		{
			get
			{
				return this.m_ProtectionRealm;
			}
			set
			{
				string[] array = ValidationHelper.MakeEmptyArrayNull(value);
				this.m_ProtectionRealm = array;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that indicates whether mutual authentication occurred.</summary>
		/// <returns>true if both client and server were authenticated; otherwise, false.</returns>
		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x06002053 RID: 8275 RVA: 0x000766D3 File Offset: 0x000748D3
		// (set) Token: 0x06002054 RID: 8276 RVA: 0x000766E5 File Offset: 0x000748E5
		public bool MutuallyAuthenticated
		{
			get
			{
				return this.Complete && this.m_MutualAuth;
			}
			set
			{
				this.m_MutualAuth = value;
			}
		}

		// Token: 0x0400112A RID: 4394
		private string m_Message;

		// Token: 0x0400112B RID: 4395
		private bool m_Complete;

		// Token: 0x0400112C RID: 4396
		private string[] m_ProtectionRealm;

		// Token: 0x0400112D RID: 4397
		private string m_ConnectionGroupId;

		// Token: 0x0400112E RID: 4398
		private bool m_MutualAuth;

		// Token: 0x0400112F RID: 4399
		internal string ModuleAuthenticationType;
	}
}
