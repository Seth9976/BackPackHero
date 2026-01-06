using System;
using System.IO;
using System.Reflection;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x020003B0 RID: 944
	internal sealed class SByteNormalizer : Normalizer
	{
		// Token: 0x06002E9B RID: 11931 RVA: 0x000CA5D8 File Offset: 0x000C87D8
		internal override void Normalize(FieldInfo fi, object obj, Stream s)
		{
			byte b = (byte)((sbyte)base.GetValue(fi, obj));
			if (!this._skipNormalize)
			{
				b ^= 128;
			}
			s.WriteByte(b);
		}

		// Token: 0x06002E9C RID: 11932 RVA: 0x000CA60C File Offset: 0x000C880C
		internal override void DeNormalize(FieldInfo fi, object recvr, Stream s)
		{
			byte b = (byte)s.ReadByte();
			if (!this._skipNormalize)
			{
				b ^= 128;
			}
			sbyte b2 = (sbyte)b;
			base.SetValue(fi, recvr, b2);
		}

		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x06002E9D RID: 11933 RVA: 0x0000CD07 File Offset: 0x0000AF07
		internal override int Size
		{
			get
			{
				return 1;
			}
		}
	}
}
