using System;

namespace System.Data
{
	// Token: 0x02000049 RID: 73
	internal abstract class AutoIncrementValue
	{
		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000348 RID: 840 RVA: 0x000107DF File Offset: 0x0000E9DF
		// (set) Token: 0x06000349 RID: 841 RVA: 0x000107E7 File Offset: 0x0000E9E7
		internal bool Auto { get; set; }

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600034A RID: 842
		// (set) Token: 0x0600034B RID: 843
		internal abstract object Current { get; set; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600034C RID: 844
		// (set) Token: 0x0600034D RID: 845
		internal abstract long Seed { get; set; }

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600034E RID: 846
		// (set) Token: 0x0600034F RID: 847
		internal abstract long Step { get; set; }

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000350 RID: 848
		internal abstract Type DataType { get; }

		// Token: 0x06000351 RID: 849
		internal abstract void SetCurrent(object value, IFormatProvider formatProvider);

		// Token: 0x06000352 RID: 850
		internal abstract void SetCurrentAndIncrement(object value);

		// Token: 0x06000353 RID: 851
		internal abstract void MoveAfter();

		// Token: 0x06000354 RID: 852 RVA: 0x000107F0 File Offset: 0x0000E9F0
		internal AutoIncrementValue Clone()
		{
			AutoIncrementInt64 autoIncrementInt = ((this is AutoIncrementInt64) ? new AutoIncrementInt64() : new AutoIncrementBigInteger());
			autoIncrementInt.Auto = this.Auto;
			autoIncrementInt.Seed = this.Seed;
			autoIncrementInt.Step = this.Step;
			autoIncrementInt.Current = this.Current;
			return autoIncrementInt;
		}
	}
}
