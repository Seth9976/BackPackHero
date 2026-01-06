using System;

namespace System.Data
{
	// Token: 0x020000D6 RID: 214
	internal readonly struct IndexField
	{
		// Token: 0x06000C2E RID: 3118 RVA: 0x00038292 File Offset: 0x00036492
		internal IndexField(DataColumn column, bool isDescending)
		{
			this.Column = column;
			this.IsDescending = isDescending;
		}

		// Token: 0x06000C2F RID: 3119 RVA: 0x000382A2 File Offset: 0x000364A2
		public static bool operator ==(IndexField if1, IndexField if2)
		{
			return if1.Column == if2.Column && if1.IsDescending == if2.IsDescending;
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x000382C2 File Offset: 0x000364C2
		public static bool operator !=(IndexField if1, IndexField if2)
		{
			return !(if1 == if2);
		}

		// Token: 0x06000C31 RID: 3121 RVA: 0x000382CE File Offset: 0x000364CE
		public override bool Equals(object obj)
		{
			return obj is IndexField && this == (IndexField)obj;
		}

		// Token: 0x06000C32 RID: 3122 RVA: 0x000382EB File Offset: 0x000364EB
		public override int GetHashCode()
		{
			return this.Column.GetHashCode() ^ this.IsDescending.GetHashCode();
		}

		// Token: 0x040007E6 RID: 2022
		public readonly DataColumn Column;

		// Token: 0x040007E7 RID: 2023
		public readonly bool IsDescending;
	}
}
