using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Computes the <see cref="T:System.Security.Cryptography.SHA1" /> hash value for the input data using the implementation provided by the cryptographic service provider (CSP). This class cannot be inherited. </summary>
	// Token: 0x020004D0 RID: 1232
	[ComVisible(true)]
	public sealed class SHA1CryptoServiceProvider : SHA1
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.SHA1CryptoServiceProvider" /> class.</summary>
		// Token: 0x0600314B RID: 12619 RVA: 0x000B6A10 File Offset: 0x000B4C10
		public SHA1CryptoServiceProvider()
		{
			this.sha = new SHA1Internal();
		}

		// Token: 0x0600314C RID: 12620 RVA: 0x000B6A24 File Offset: 0x000B4C24
		~SHA1CryptoServiceProvider()
		{
			this.Dispose(false);
		}

		// Token: 0x0600314D RID: 12621 RVA: 0x000B6A54 File Offset: 0x000B4C54
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		// Token: 0x0600314E RID: 12622 RVA: 0x000B6A5D File Offset: 0x000B4C5D
		protected override void HashCore(byte[] rgb, int ibStart, int cbSize)
		{
			this.State = 1;
			this.sha.HashCore(rgb, ibStart, cbSize);
		}

		// Token: 0x0600314F RID: 12623 RVA: 0x000B6A74 File Offset: 0x000B4C74
		protected override byte[] HashFinal()
		{
			this.State = 0;
			return this.sha.HashFinal();
		}

		/// <summary>Initializes an instance of <see cref="T:System.Security.Cryptography.SHA1CryptoServiceProvider" />.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003150 RID: 12624 RVA: 0x000B6A88 File Offset: 0x000B4C88
		public override void Initialize()
		{
			this.sha.Initialize();
		}

		// Token: 0x04002277 RID: 8823
		private SHA1Internal sha;
	}
}
