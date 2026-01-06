using System;
using System.IO;
using System.Reflection;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x020003B9 RID: 953
	internal sealed class DoubleNormalizer : Normalizer
	{
		// Token: 0x06002EBF RID: 11967 RVA: 0x000CAAC8 File Offset: 0x000C8CC8
		internal override void Normalize(FieldInfo fi, object obj, Stream s)
		{
			double num = (double)base.GetValue(fi, obj);
			byte[] bytes = BitConverter.GetBytes(num);
			if (!this._skipNormalize)
			{
				Array.Reverse<byte>(bytes);
				if ((bytes[0] & 128) == 0)
				{
					byte[] array = bytes;
					int num2 = 0;
					array[num2] ^= 128;
				}
				else if (num < 0.0)
				{
					base.FlipAllBits(bytes);
				}
			}
			s.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x06002EC0 RID: 11968 RVA: 0x000CAB34 File Offset: 0x000C8D34
		internal override void DeNormalize(FieldInfo fi, object recvr, Stream s)
		{
			byte[] array = new byte[8];
			s.Read(array, 0, array.Length);
			if (!this._skipNormalize)
			{
				if ((array[0] & 128) > 0)
				{
					byte[] array2 = array;
					int num = 0;
					array2[num] ^= 128;
				}
				else
				{
					base.FlipAllBits(array);
				}
				Array.Reverse<byte>(array);
			}
			base.SetValue(fi, recvr, BitConverter.ToDouble(array, 0));
		}

		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x06002EC1 RID: 11969 RVA: 0x000CA973 File Offset: 0x000C8B73
		internal override int Size
		{
			get
			{
				return 8;
			}
		}
	}
}
