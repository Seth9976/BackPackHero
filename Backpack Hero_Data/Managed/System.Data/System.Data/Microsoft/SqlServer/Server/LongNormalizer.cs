using System;
using System.IO;
using System.Reflection;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x020003B6 RID: 950
	internal sealed class LongNormalizer : Normalizer
	{
		// Token: 0x06002EB3 RID: 11955 RVA: 0x000CA8D4 File Offset: 0x000C8AD4
		internal override void Normalize(FieldInfo fi, object obj, Stream s)
		{
			byte[] bytes = BitConverter.GetBytes((long)base.GetValue(fi, obj));
			if (!this._skipNormalize)
			{
				Array.Reverse<byte>(bytes);
				byte[] array = bytes;
				int num = 0;
				array[num] ^= 128;
			}
			s.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x06002EB4 RID: 11956 RVA: 0x000CA920 File Offset: 0x000C8B20
		internal override void DeNormalize(FieldInfo fi, object recvr, Stream s)
		{
			byte[] array = new byte[8];
			s.Read(array, 0, array.Length);
			if (!this._skipNormalize)
			{
				byte[] array2 = array;
				int num = 0;
				array2[num] ^= 128;
				Array.Reverse<byte>(array);
			}
			base.SetValue(fi, recvr, BitConverter.ToInt64(array, 0));
		}

		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x06002EB5 RID: 11957 RVA: 0x000CA973 File Offset: 0x000C8B73
		internal override int Size
		{
			get
			{
				return 8;
			}
		}
	}
}
