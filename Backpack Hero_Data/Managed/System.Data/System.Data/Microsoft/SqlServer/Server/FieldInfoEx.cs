using System;
using System.Reflection;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x020003AC RID: 940
	internal sealed class FieldInfoEx : IComparable
	{
		// Token: 0x06002E84 RID: 11908 RVA: 0x000CA048 File Offset: 0x000C8248
		internal FieldInfoEx(FieldInfo fi, int offset, Normalizer normalizer)
		{
			this.FieldInfo = fi;
			this.Offset = offset;
			this.Normalizer = normalizer;
		}

		// Token: 0x06002E85 RID: 11909 RVA: 0x000CA068 File Offset: 0x000C8268
		public int CompareTo(object other)
		{
			FieldInfoEx fieldInfoEx = other as FieldInfoEx;
			if (fieldInfoEx == null)
			{
				return -1;
			}
			return this.Offset.CompareTo(fieldInfoEx.Offset);
		}

		// Token: 0x04001BA3 RID: 7075
		internal readonly int Offset;

		// Token: 0x04001BA4 RID: 7076
		internal readonly FieldInfo FieldInfo;

		// Token: 0x04001BA5 RID: 7077
		internal readonly Normalizer Normalizer;
	}
}
