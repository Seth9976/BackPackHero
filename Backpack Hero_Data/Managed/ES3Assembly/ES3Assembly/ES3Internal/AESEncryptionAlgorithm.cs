using System;
using System.IO;
using System.Security.Cryptography;

namespace ES3Internal
{
	// Token: 0x020000C5 RID: 197
	public class AESEncryptionAlgorithm : EncryptionAlgorithm
	{
		// Token: 0x060003DD RID: 989 RVA: 0x0001E9A8 File Offset: 0x0001CBA8
		public override byte[] Encrypt(byte[] bytes, string password, int bufferSize)
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream(bytes))
			{
				using (MemoryStream memoryStream2 = new MemoryStream())
				{
					this.Encrypt(memoryStream, memoryStream2, password, bufferSize);
					array = memoryStream2.ToArray();
				}
			}
			return array;
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0001EA08 File Offset: 0x0001CC08
		public override byte[] Decrypt(byte[] bytes, string password, int bufferSize)
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream(bytes))
			{
				using (MemoryStream memoryStream2 = new MemoryStream())
				{
					this.Decrypt(memoryStream, memoryStream2, password, bufferSize);
					array = memoryStream2.ToArray();
				}
			}
			return array;
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0001EA68 File Offset: 0x0001CC68
		public override void Encrypt(Stream input, Stream output, string password, int bufferSize)
		{
			input.Position = 0L;
			using (Aes aes = Aes.Create())
			{
				aes.Mode = CipherMode.CBC;
				aes.Padding = PaddingMode.PKCS7;
				aes.GenerateIV();
				Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, aes.IV, 100);
				aes.Key = rfc2898DeriveBytes.GetBytes(16);
				output.Write(aes.IV, 0, 16);
				using (ICryptoTransform cryptoTransform = aes.CreateEncryptor())
				{
					using (CryptoStream cryptoStream = new CryptoStream(output, cryptoTransform, CryptoStreamMode.Write))
					{
						EncryptionAlgorithm.CopyStream(input, cryptoStream, bufferSize);
					}
				}
			}
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x0001EB24 File Offset: 0x0001CD24
		public override void Decrypt(Stream input, Stream output, string password, int bufferSize)
		{
			using (Aes aes = Aes.Create())
			{
				byte[] array = new byte[16];
				input.Read(array, 0, 16);
				aes.IV = array;
				Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, aes.IV, 100);
				aes.Key = rfc2898DeriveBytes.GetBytes(16);
				using (ICryptoTransform cryptoTransform = aes.CreateDecryptor())
				{
					using (CryptoStream cryptoStream = new CryptoStream(input, cryptoTransform, CryptoStreamMode.Read))
					{
						EncryptionAlgorithm.CopyStream(cryptoStream, output, bufferSize);
					}
				}
			}
			output.Position = 0L;
		}

		// Token: 0x040000FC RID: 252
		private const int ivSize = 16;

		// Token: 0x040000FD RID: 253
		private const int keySize = 16;

		// Token: 0x040000FE RID: 254
		private const int pwIterations = 100;
	}
}
