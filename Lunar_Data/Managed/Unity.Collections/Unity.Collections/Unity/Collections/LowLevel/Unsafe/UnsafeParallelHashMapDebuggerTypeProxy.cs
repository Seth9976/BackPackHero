using System;
using System.Collections.Generic;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x0200013A RID: 314
	internal sealed class UnsafeParallelHashMapDebuggerTypeProxy<TKey, TValue> where TKey : struct, IEquatable<TKey> where TValue : struct
	{
		// Token: 0x06000B5D RID: 2909 RVA: 0x00021E71 File Offset: 0x00020071
		public UnsafeParallelHashMapDebuggerTypeProxy(UnsafeParallelHashMap<TKey, TValue> target)
		{
			this.m_Target = target;
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000B5E RID: 2910 RVA: 0x00021E80 File Offset: 0x00020080
		public List<Pair<TKey, TValue>> Items
		{
			get
			{
				List<Pair<TKey, TValue>> list = new List<Pair<TKey, TValue>>();
				using (NativeKeyValueArrays<TKey, TValue> keyValueArrays = this.m_Target.GetKeyValueArrays(Allocator.Temp))
				{
					for (int i = 0; i < keyValueArrays.Length; i++)
					{
						List<Pair<TKey, TValue>> list2 = list;
						NativeArray<TKey> keys = keyValueArrays.Keys;
						TKey tkey = keys[i];
						NativeArray<TValue> values = keyValueArrays.Values;
						list2.Add(new Pair<TKey, TValue>(tkey, values[i]));
					}
				}
				return list;
			}
		}

		// Token: 0x040003B8 RID: 952
		private UnsafeParallelHashMap<TKey, TValue> m_Target;
	}
}
