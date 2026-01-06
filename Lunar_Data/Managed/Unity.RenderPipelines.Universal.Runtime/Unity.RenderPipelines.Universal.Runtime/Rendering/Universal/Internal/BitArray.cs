using System;
using Unity.Collections;

namespace UnityEngine.Rendering.Universal.Internal
{
	// Token: 0x02000102 RID: 258
	internal struct BitArray : IDisposable
	{
		// Token: 0x060007D1 RID: 2001 RVA: 0x0002F2C1 File Offset: 0x0002D4C1
		public BitArray(int bitCount, Allocator allocator, NativeArrayOptions options = NativeArrayOptions.ClearMemory)
		{
			this.m_BitCount = bitCount;
			this.m_IntCount = bitCount + 31 >> 5;
			this.m_Mem = new NativeArray<uint>(this.m_IntCount, allocator, options);
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x0002F2E9 File Offset: 0x0002D4E9
		public void Dispose()
		{
			this.m_Mem.Dispose();
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x0002F2F8 File Offset: 0x0002D4F8
		public void Clear()
		{
			for (int i = 0; i < this.m_IntCount; i++)
			{
				this.m_Mem[i] = 0U;
			}
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x0002F323 File Offset: 0x0002D523
		public bool IsSet(int bitIndex)
		{
			return (this.m_Mem[bitIndex >> 5] & (1U << bitIndex)) > 0U;
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x0002F340 File Offset: 0x0002D540
		public void Set(int bitIndex, bool val)
		{
			ref NativeArray<uint> ptr;
			int num;
			if (val)
			{
				ptr = ref this.m_Mem;
				num = bitIndex >> 5;
				ptr[num] |= 1U << bitIndex;
				return;
			}
			ptr = ref this.m_Mem;
			num = bitIndex >> 5;
			ptr[num] &= ~(1U << bitIndex);
		}

		// Token: 0x04000734 RID: 1844
		private NativeArray<uint> m_Mem;

		// Token: 0x04000735 RID: 1845
		private int m_BitCount;

		// Token: 0x04000736 RID: 1846
		private int m_IntCount;
	}
}
