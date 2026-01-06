using System;
using System.IO;
using System.Reflection;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x020003B7 RID: 951
	internal sealed class ULongNormalizer : Normalizer
	{
		// Token: 0x06002EB7 RID: 11959 RVA: 0x000CA978 File Offset: 0x000C8B78
		internal override void Normalize(FieldInfo fi, object obj, Stream s)
		{
			byte[] bytes = BitConverter.GetBytes((ulong)base.GetValue(fi, obj));
			if (!this._skipNormalize)
			{
				Array.Reverse<byte>(bytes);
			}
			s.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x06002EB8 RID: 11960 RVA: 0x000CA9B4 File Offset: 0x000C8BB4
		internal override void DeNormalize(FieldInfo fi, object recvr, Stream s)
		{
			byte[] array = new byte[8];
			s.Read(array, 0, array.Length);
			if (!this._skipNormalize)
			{
				Array.Reverse<byte>(array);
			}
			base.SetValue(fi, recvr, BitConverter.ToUInt64(array, 0));
		}

		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x06002EB9 RID: 11961 RVA: 0x000CA973 File Offset: 0x000C8B73
		internal override int Size
		{
			get
			{
				return 8;
			}
		}
	}
}
