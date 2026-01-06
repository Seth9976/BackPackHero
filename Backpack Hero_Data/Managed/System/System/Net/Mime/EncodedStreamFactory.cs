using System;
using System.IO;
using System.Text;

namespace System.Net.Mime
{
	// Token: 0x0200060A RID: 1546
	internal class EncodedStreamFactory
	{
		// Token: 0x060031B8 RID: 12728 RVA: 0x000B2276 File Offset: 0x000B0476
		internal IEncodableStream GetEncoder(TransferEncoding encoding, Stream stream)
		{
			if (encoding == TransferEncoding.Base64)
			{
				return new Base64Stream(stream, new Base64WriteStateInfo());
			}
			if (encoding == TransferEncoding.QuotedPrintable)
			{
				return new QuotedPrintableStream(stream, true);
			}
			if (encoding == TransferEncoding.SevenBit || encoding == TransferEncoding.EightBit)
			{
				return new EightBitStream(stream);
			}
			throw new NotSupportedException();
		}

		// Token: 0x060031B9 RID: 12729 RVA: 0x000B22A8 File Offset: 0x000B04A8
		internal IEncodableStream GetEncoderForHeader(Encoding encoding, bool useBase64Encoding, int headerTextLength)
		{
			byte[] array = this.CreateHeader(encoding, useBase64Encoding);
			byte[] array2 = this.CreateFooter();
			if (useBase64Encoding)
			{
				return new Base64Stream((Base64WriteStateInfo)new Base64WriteStateInfo(1024, array, array2, 70, headerTextLength));
			}
			return new QEncodedStream(new WriteStateInfoBase(1024, array, array2, 70, headerTextLength));
		}

		// Token: 0x060031BA RID: 12730 RVA: 0x000B22F6 File Offset: 0x000B04F6
		protected byte[] CreateHeader(Encoding encoding, bool useBase64Encoding)
		{
			return Encoding.ASCII.GetBytes("=?" + encoding.HeaderName + "?" + (useBase64Encoding ? "B?" : "Q?"));
		}

		// Token: 0x060031BB RID: 12731 RVA: 0x000B2326 File Offset: 0x000B0526
		protected byte[] CreateFooter()
		{
			return new byte[] { 63, 61 };
		}

		// Token: 0x04001E5B RID: 7771
		internal const int DefaultMaxLineLength = 70;

		// Token: 0x04001E5C RID: 7772
		private const int InitialBufferSize = 1024;
	}
}
