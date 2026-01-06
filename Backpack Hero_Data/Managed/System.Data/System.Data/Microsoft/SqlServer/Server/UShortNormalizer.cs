using System;
using System.IO;
using System.Reflection;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x020003B3 RID: 947
	internal sealed class UShortNormalizer : Normalizer
	{
		// Token: 0x06002EA7 RID: 11943 RVA: 0x000CA730 File Offset: 0x000C8930
		internal override void Normalize(FieldInfo fi, object obj, Stream s)
		{
			byte[] bytes = BitConverter.GetBytes((ushort)base.GetValue(fi, obj));
			if (!this._skipNormalize)
			{
				Array.Reverse<byte>(bytes);
			}
			s.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x06002EA8 RID: 11944 RVA: 0x000CA76C File Offset: 0x000C896C
		internal override void DeNormalize(FieldInfo fi, object recvr, Stream s)
		{
			byte[] array = new byte[2];
			s.Read(array, 0, array.Length);
			if (!this._skipNormalize)
			{
				Array.Reverse<byte>(array);
			}
			base.SetValue(fi, recvr, BitConverter.ToUInt16(array, 0));
		}

		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x06002EA9 RID: 11945 RVA: 0x000CA72B File Offset: 0x000C892B
		internal override int Size
		{
			get
			{
				return 2;
			}
		}
	}
}
