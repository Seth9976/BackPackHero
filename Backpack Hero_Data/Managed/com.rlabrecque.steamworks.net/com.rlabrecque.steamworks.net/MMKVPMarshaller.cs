using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200017E RID: 382
	public class MMKVPMarshaller
	{
		// Token: 0x060008B8 RID: 2232 RVA: 0x0000CDA4 File Offset: 0x0000AFA4
		public MMKVPMarshaller(MatchMakingKeyValuePair_t[] filters)
		{
			if (filters == null)
			{
				return;
			}
			int num = Marshal.SizeOf(typeof(MatchMakingKeyValuePair_t));
			this.m_pNativeArray = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(IntPtr)) * filters.Length);
			this.m_pArrayEntries = Marshal.AllocHGlobal(num * filters.Length);
			for (int i = 0; i < filters.Length; i++)
			{
				Marshal.StructureToPtr<MatchMakingKeyValuePair_t>(filters[i], new IntPtr(this.m_pArrayEntries.ToInt64() + (long)(i * num)), false);
			}
			Marshal.WriteIntPtr(this.m_pNativeArray, this.m_pArrayEntries);
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x0000CE3C File Offset: 0x0000B03C
		~MMKVPMarshaller()
		{
			if (this.m_pArrayEntries != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(this.m_pArrayEntries);
			}
			if (this.m_pNativeArray != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(this.m_pNativeArray);
			}
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x0000CE9C File Offset: 0x0000B09C
		public static implicit operator IntPtr(MMKVPMarshaller that)
		{
			return that.m_pNativeArray;
		}

		// Token: 0x040009F1 RID: 2545
		private IntPtr m_pNativeArray;

		// Token: 0x040009F2 RID: 2546
		private IntPtr m_pArrayEntries;
	}
}
