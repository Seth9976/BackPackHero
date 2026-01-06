using System;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000120 RID: 288
	internal sealed class UnsafeBitArrayDebugView
	{
		// Token: 0x06000A9A RID: 2714 RVA: 0x0001FA65 File Offset: 0x0001DC65
		public UnsafeBitArrayDebugView(UnsafeBitArray data)
		{
			this.Data = data;
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000A9B RID: 2715 RVA: 0x0001FA74 File Offset: 0x0001DC74
		public bool[] Bits
		{
			get
			{
				bool[] array = new bool[this.Data.Length];
				for (int i = 0; i < this.Data.Length; i++)
				{
					array[i] = this.Data.IsSet(i);
				}
				return array;
			}
		}

		// Token: 0x04000377 RID: 887
		private UnsafeBitArray Data;
	}
}
