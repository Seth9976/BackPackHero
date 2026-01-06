using System;
using System.IO;
using System.Reflection;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x020003AF RID: 943
	internal sealed class BooleanNormalizer : Normalizer
	{
		// Token: 0x06002E97 RID: 11927 RVA: 0x000CA57C File Offset: 0x000C877C
		internal override void Normalize(FieldInfo fi, object obj, Stream s)
		{
			s.WriteByte(((bool)base.GetValue(fi, obj)) ? 1 : 0);
		}

		// Token: 0x06002E98 RID: 11928 RVA: 0x000CA5A8 File Offset: 0x000C87A8
		internal override void DeNormalize(FieldInfo fi, object recvr, Stream s)
		{
			byte b = (byte)s.ReadByte();
			base.SetValue(fi, recvr, b == 1);
		}

		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x06002E99 RID: 11929 RVA: 0x0000CD07 File Offset: 0x0000AF07
		internal override int Size
		{
			get
			{
				return 1;
			}
		}
	}
}
