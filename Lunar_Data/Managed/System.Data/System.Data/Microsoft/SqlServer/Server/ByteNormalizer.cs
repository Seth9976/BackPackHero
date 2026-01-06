using System;
using System.IO;
using System.Reflection;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x020003B1 RID: 945
	internal sealed class ByteNormalizer : Normalizer
	{
		// Token: 0x06002E9F RID: 11935 RVA: 0x000CA644 File Offset: 0x000C8844
		internal override void Normalize(FieldInfo fi, object obj, Stream s)
		{
			byte b = (byte)base.GetValue(fi, obj);
			s.WriteByte(b);
		}

		// Token: 0x06002EA0 RID: 11936 RVA: 0x000CA668 File Offset: 0x000C8868
		internal override void DeNormalize(FieldInfo fi, object recvr, Stream s)
		{
			byte b = (byte)s.ReadByte();
			base.SetValue(fi, recvr, b);
		}

		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x06002EA1 RID: 11937 RVA: 0x0000CD07 File Offset: 0x0000AF07
		internal override int Size
		{
			get
			{
				return 1;
			}
		}
	}
}
