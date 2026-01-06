using System;
using System.Text;

namespace System.Data.SqlClient
{
	// Token: 0x020001D8 RID: 472
	internal sealed class SqlUnicodeEncoding : UnicodeEncoding
	{
		// Token: 0x060016B1 RID: 5809 RVA: 0x0006F404 File Offset: 0x0006D604
		private SqlUnicodeEncoding()
			: base(false, false, false)
		{
		}

		// Token: 0x060016B2 RID: 5810 RVA: 0x0006F40F File Offset: 0x0006D60F
		public override Decoder GetDecoder()
		{
			return new SqlUnicodeEncoding.SqlUnicodeDecoder();
		}

		// Token: 0x060016B3 RID: 5811 RVA: 0x0006F416 File Offset: 0x0006D616
		public override int GetMaxByteCount(int charCount)
		{
			return charCount * 2;
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x060016B4 RID: 5812 RVA: 0x0006F41B File Offset: 0x0006D61B
		public static Encoding SqlUnicodeEncodingInstance
		{
			get
			{
				return SqlUnicodeEncoding.s_singletonEncoding;
			}
		}

		// Token: 0x04000F00 RID: 3840
		private static SqlUnicodeEncoding s_singletonEncoding = new SqlUnicodeEncoding();

		// Token: 0x020001D9 RID: 473
		private sealed class SqlUnicodeDecoder : Decoder
		{
			// Token: 0x060016B6 RID: 5814 RVA: 0x0006F42E File Offset: 0x0006D62E
			public override int GetCharCount(byte[] bytes, int index, int count)
			{
				return count / 2;
			}

			// Token: 0x060016B7 RID: 5815 RVA: 0x0006F434 File Offset: 0x0006D634
			public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
			{
				int num;
				int num2;
				bool flag;
				this.Convert(bytes, byteIndex, byteCount, chars, charIndex, chars.Length - charIndex, true, out num, out num2, out flag);
				return num2;
			}

			// Token: 0x060016B8 RID: 5816 RVA: 0x0006F45D File Offset: 0x0006D65D
			public override void Convert(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex, int charCount, bool flush, out int bytesUsed, out int charsUsed, out bool completed)
			{
				charsUsed = Math.Min(charCount, byteCount / 2);
				bytesUsed = charsUsed * 2;
				completed = bytesUsed == byteCount;
				Buffer.BlockCopy(bytes, byteIndex, chars, charIndex * 2, bytesUsed);
			}
		}
	}
}
