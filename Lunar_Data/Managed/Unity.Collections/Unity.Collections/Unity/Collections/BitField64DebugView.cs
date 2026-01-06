using System;

namespace Unity.Collections
{
	// Token: 0x02000039 RID: 57
	internal sealed class BitField64DebugView
	{
		// Token: 0x06000107 RID: 263 RVA: 0x00004181 File Offset: 0x00002381
		public BitField64DebugView(BitField64 data)
		{
			this.Data = data;
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00004190 File Offset: 0x00002390
		public bool[] Bits
		{
			get
			{
				bool[] array = new bool[64];
				for (int i = 0; i < 64; i++)
				{
					array[i] = this.Data.IsSet(i);
				}
				return array;
			}
		}

		// Token: 0x04000071 RID: 113
		private BitField64 Data;
	}
}
