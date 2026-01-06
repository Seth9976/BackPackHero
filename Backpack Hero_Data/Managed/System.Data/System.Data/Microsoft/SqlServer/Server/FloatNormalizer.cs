using System;
using System.IO;
using System.Reflection;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x020003B8 RID: 952
	internal sealed class FloatNormalizer : Normalizer
	{
		// Token: 0x06002EBB RID: 11963 RVA: 0x000CA9F8 File Offset: 0x000C8BF8
		internal override void Normalize(FieldInfo fi, object obj, Stream s)
		{
			float num = (float)base.GetValue(fi, obj);
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
				else if (num < 0f)
				{
					base.FlipAllBits(bytes);
				}
			}
			s.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x06002EBC RID: 11964 RVA: 0x000CAA60 File Offset: 0x000C8C60
		internal override void DeNormalize(FieldInfo fi, object recvr, Stream s)
		{
			byte[] array = new byte[4];
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
			base.SetValue(fi, recvr, BitConverter.ToSingle(array, 0));
		}

		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x06002EBD RID: 11965 RVA: 0x000CA84F File Offset: 0x000C8A4F
		internal override int Size
		{
			get
			{
				return 4;
			}
		}
	}
}
