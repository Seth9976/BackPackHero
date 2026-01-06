using System;
using System.IO;

namespace ES3Internal
{
	// Token: 0x020000C4 RID: 196
	public abstract class EncryptionAlgorithm
	{
		// Token: 0x060003D7 RID: 983
		public abstract byte[] Encrypt(byte[] bytes, string password, int bufferSize);

		// Token: 0x060003D8 RID: 984
		public abstract byte[] Decrypt(byte[] bytes, string password, int bufferSize);

		// Token: 0x060003D9 RID: 985
		public abstract void Encrypt(Stream input, Stream output, string password, int bufferSize);

		// Token: 0x060003DA RID: 986
		public abstract void Decrypt(Stream input, Stream output, string password, int bufferSize);

		// Token: 0x060003DB RID: 987 RVA: 0x0001E970 File Offset: 0x0001CB70
		protected static void CopyStream(Stream input, Stream output, int bufferSize)
		{
			byte[] array = new byte[bufferSize];
			int num;
			while ((num = input.Read(array, 0, bufferSize)) > 0)
			{
				output.Write(array, 0, num);
			}
		}
	}
}
