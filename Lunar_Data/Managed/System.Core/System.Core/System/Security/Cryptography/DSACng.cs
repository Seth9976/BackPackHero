using System;
using System.Security.Permissions;
using Unity;

namespace System.Security.Cryptography
{
	// Token: 0x0200036C RID: 876
	public sealed class DSACng : DSA
	{
		// Token: 0x06001AA7 RID: 6823 RVA: 0x0000235B File Offset: 0x0000055B
		public DSACng()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x06001AA8 RID: 6824 RVA: 0x0000235B File Offset: 0x0000055B
		public DSACng(int keySize)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x06001AA9 RID: 6825 RVA: 0x0000235B File Offset: 0x0000055B
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		public DSACng(CngKey key)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06001AAA RID: 6826 RVA: 0x0005A05A File Offset: 0x0005825A
		public CngKey Key
		{
			[SecuritySafeCritical]
			[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		// Token: 0x06001AAB RID: 6827 RVA: 0x0005A05A File Offset: 0x0005825A
		[SecuritySafeCritical]
		public override byte[] CreateSignature(byte[] rgbHash)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x06001AAC RID: 6828 RVA: 0x0005A064 File Offset: 0x00058264
		public override DSAParameters ExportParameters(bool includePrivateParameters)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(DSAParameters);
		}

		// Token: 0x06001AAD RID: 6829 RVA: 0x0000235B File Offset: 0x0000055B
		public override void ImportParameters(DSAParameters parameters)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x06001AAE RID: 6830 RVA: 0x0005A080 File Offset: 0x00058280
		[SecuritySafeCritical]
		public override bool VerifySignature(byte[] rgbHash, byte[] rgbSignature)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}
	}
}
