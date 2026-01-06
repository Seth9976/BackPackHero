using System;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x0200010B RID: 267
	[Obsolete("This storage will no longer be used. (RemovedAfter 2021-06-01)")]
	public struct Words
	{
		// Token: 0x060009E5 RID: 2533 RVA: 0x0001DE37 File Offset: 0x0001C037
		public void ToFixedString<T>(ref T value) where T : IUTF8Bytes, INativeList<byte>
		{
			WordStorage.Instance.GetFixedString<T>(this.Index, ref value);
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x0001DE4C File Offset: 0x0001C04C
		public override string ToString()
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			this.ToFixedString<FixedString512Bytes>(ref fixedString512Bytes);
			return fixedString512Bytes.ToString();
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x0001DE76 File Offset: 0x0001C076
		public void SetFixedString<T>(ref T value) where T : IUTF8Bytes, INativeList<byte>
		{
			this.Index = WordStorage.Instance.GetOrCreateIndex<T>(ref value);
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x0001DE8C File Offset: 0x0001C08C
		public void SetString(string value)
		{
			FixedString512Bytes fixedString512Bytes = value;
			this.SetFixedString<FixedString512Bytes>(ref fixedString512Bytes);
		}

		// Token: 0x0400034B RID: 843
		private int Index;
	}
}
