using System;

namespace System.Security.Cryptography
{
	// Token: 0x02000055 RID: 85
	public sealed class AesCng : Aes
	{
		// Token: 0x060001B5 RID: 437 RVA: 0x00003F0D File Offset: 0x0000210D
		public AesCng()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00003F0D File Offset: 0x0000210D
		public AesCng(string keyName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00003F0D File Offset: 0x0000210D
		public AesCng(string keyName, CngProvider provider)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00003F0D File Offset: 0x0000210D
		public AesCng(string keyName, CngProvider provider, CngKeyOpenOptions openOptions)
		{
			throw new NotImplementedException();
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x000023CA File Offset: 0x000005CA
		// (set) Token: 0x060001BA RID: 442 RVA: 0x000023CA File Offset: 0x000005CA
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

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001BB RID: 443 RVA: 0x000023CA File Offset: 0x000005CA
		// (set) Token: 0x060001BC RID: 444 RVA: 0x000023CA File Offset: 0x000005CA
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

		// Token: 0x060001BD RID: 445 RVA: 0x000023CA File Offset: 0x000005CA
		public override ICryptoTransform CreateDecryptor()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001BE RID: 446 RVA: 0x000023CA File Offset: 0x000005CA
		public override ICryptoTransform CreateDecryptor(byte[] rgbKey, byte[] rgbIV)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001BF RID: 447 RVA: 0x000023CA File Offset: 0x000005CA
		public override ICryptoTransform CreateEncryptor()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0000392D File Offset: 0x00001B2D
		public override ICryptoTransform CreateEncryptor(byte[] rgbKey, byte[] rgbIV)
		{
			return null;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x000023CA File Offset: 0x000005CA
		protected override void Dispose(bool disposing)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x000023CA File Offset: 0x000005CA
		public override void GenerateIV()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x000023CA File Offset: 0x000005CA
		public override void GenerateKey()
		{
			throw new NotImplementedException();
		}
	}
}
