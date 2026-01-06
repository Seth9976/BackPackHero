using System;

namespace System.Security.Cryptography
{
	// Token: 0x02000060 RID: 96
	public sealed class TripleDESCng : TripleDES
	{
		// Token: 0x0600020F RID: 527 RVA: 0x00006184 File Offset: 0x00004384
		public TripleDESCng()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00006184 File Offset: 0x00004384
		public TripleDESCng(string keyName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00006184 File Offset: 0x00004384
		public TripleDESCng(string keyName, CngProvider provider)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00006184 File Offset: 0x00004384
		public TripleDESCng(string keyName, CngProvider provider, CngKeyOpenOptions openOptions)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000213 RID: 531 RVA: 0x000023CA File Offset: 0x000005CA
		// (set) Token: 0x06000214 RID: 532 RVA: 0x000023CA File Offset: 0x000005CA
		public override byte[] Key
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000215 RID: 533 RVA: 0x000023CA File Offset: 0x000005CA
		// (set) Token: 0x06000216 RID: 534 RVA: 0x000023CA File Offset: 0x000005CA
		public override int KeySize
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06000217 RID: 535 RVA: 0x000023CA File Offset: 0x000005CA
		public override ICryptoTransform CreateDecryptor()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000218 RID: 536 RVA: 0x000023CA File Offset: 0x000005CA
		public override ICryptoTransform CreateDecryptor(byte[] rgbKey, byte[] rgbIV)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000219 RID: 537 RVA: 0x000023CA File Offset: 0x000005CA
		public override ICryptoTransform CreateEncryptor()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000392D File Offset: 0x00001B2D
		public override ICryptoTransform CreateEncryptor(byte[] rgbKey, byte[] rgbIV)
		{
			return null;
		}

		// Token: 0x0600021B RID: 539 RVA: 0x000023CA File Offset: 0x000005CA
		protected override void Dispose(bool disposing)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600021C RID: 540 RVA: 0x000023CA File Offset: 0x000005CA
		public override void GenerateIV()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600021D RID: 541 RVA: 0x000023CA File Offset: 0x000005CA
		public override void GenerateKey()
		{
			throw new NotImplementedException();
		}
	}
}
