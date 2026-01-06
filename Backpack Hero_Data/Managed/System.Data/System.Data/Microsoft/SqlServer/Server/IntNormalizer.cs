using System;
using System.IO;
using System.Reflection;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x020003B4 RID: 948
	internal sealed class IntNormalizer : Normalizer
	{
		// Token: 0x06002EAB RID: 11947 RVA: 0x000CA7B0 File Offset: 0x000C89B0
		internal override void Normalize(FieldInfo fi, object obj, Stream s)
		{
			byte[] bytes = BitConverter.GetBytes((int)base.GetValue(fi, obj));
			if (!this._skipNormalize)
			{
				Array.Reverse<byte>(bytes);
				byte[] array = bytes;
				int num = 0;
				array[num] ^= 128;
			}
			s.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x06002EAC RID: 11948 RVA: 0x000CA7FC File Offset: 0x000C89FC
		internal override void DeNormalize(FieldInfo fi, object recvr, Stream s)
		{
			byte[] array = new byte[4];
			s.Read(array, 0, array.Length);
			if (!this._skipNormalize)
			{
				byte[] array2 = array;
				int num = 0;
				array2[num] ^= 128;
				Array.Reverse<byte>(array);
			}
			base.SetValue(fi, recvr, BitConverter.ToInt32(array, 0));
		}

		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x06002EAD RID: 11949 RVA: 0x000CA84F File Offset: 0x000C8A4F
		internal override int Size
		{
			get
			{
				return 4;
			}
		}
	}
}
