using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x0200013F RID: 319
	internal sealed class UnsafeHashSetDebuggerTypeProxy<[global::System.Runtime.CompilerServices.IsUnmanaged] T> where T : struct, ValueType, IEquatable<T>
	{
		// Token: 0x06000B77 RID: 2935 RVA: 0x0002205D File Offset: 0x0002025D
		public UnsafeHashSetDebuggerTypeProxy(UnsafeParallelHashSet<T> data)
		{
			this.Data = data;
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000B78 RID: 2936 RVA: 0x0002206C File Offset: 0x0002026C
		public List<T> Items
		{
			get
			{
				List<T> list = new List<T>();
				using (NativeArray<T> nativeArray = this.Data.ToNativeArray(Allocator.Temp))
				{
					for (int i = 0; i < nativeArray.Length; i++)
					{
						list.Add(nativeArray[i]);
					}
				}
				return list;
			}
		}

		// Token: 0x040003BE RID: 958
		private UnsafeParallelHashSet<T> Data;
	}
}
