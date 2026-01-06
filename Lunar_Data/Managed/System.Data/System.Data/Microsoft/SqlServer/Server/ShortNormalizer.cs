using System;
using System.IO;
using System.Reflection;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x020003B2 RID: 946
	internal sealed class ShortNormalizer : Normalizer
	{
		// Token: 0x06002EA3 RID: 11939 RVA: 0x000CA68C File Offset: 0x000C888C
		internal override void Normalize(FieldInfo fi, object obj, Stream s)
		{
			byte[] bytes = BitConverter.GetBytes((short)base.GetValue(fi, obj));
			if (!this._skipNormalize)
			{
				Array.Reverse<byte>(bytes);
				byte[] array = bytes;
				int num = 0;
				array[num] ^= 128;
			}
			s.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x06002EA4 RID: 11940 RVA: 0x000CA6D8 File Offset: 0x000C88D8
		internal override void DeNormalize(FieldInfo fi, object recvr, Stream s)
		{
			byte[] array = new byte[2];
			s.Read(array, 0, array.Length);
			if (!this._skipNormalize)
			{
				byte[] array2 = array;
				int num = 0;
				array2[num] ^= 128;
				Array.Reverse<byte>(array);
			}
			base.SetValue(fi, recvr, BitConverter.ToInt16(array, 0));
		}

		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x06002EA5 RID: 11941 RVA: 0x000CA72B File Offset: 0x000C892B
		internal override int Size
		{
			get
			{
				return 2;
			}
		}
	}
}
