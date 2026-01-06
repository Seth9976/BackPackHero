using System;
using System.IO;
using System.Reflection;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x020003B5 RID: 949
	internal sealed class UIntNormalizer : Normalizer
	{
		// Token: 0x06002EAF RID: 11951 RVA: 0x000CA854 File Offset: 0x000C8A54
		internal override void Normalize(FieldInfo fi, object obj, Stream s)
		{
			byte[] bytes = BitConverter.GetBytes((uint)base.GetValue(fi, obj));
			if (!this._skipNormalize)
			{
				Array.Reverse<byte>(bytes);
			}
			s.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x06002EB0 RID: 11952 RVA: 0x000CA890 File Offset: 0x000C8A90
		internal override void DeNormalize(FieldInfo fi, object recvr, Stream s)
		{
			byte[] array = new byte[4];
			s.Read(array, 0, array.Length);
			if (!this._skipNormalize)
			{
				Array.Reverse<byte>(array);
			}
			base.SetValue(fi, recvr, BitConverter.ToUInt32(array, 0));
		}

		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x06002EB1 RID: 11953 RVA: 0x000CA84F File Offset: 0x000C8A4F
		internal override int Size
		{
			get
			{
				return 4;
			}
		}
	}
}
