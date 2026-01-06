using System;
using System.Runtime.CompilerServices;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000148 RID: 328
	internal sealed class UnsafeRingQueueDebugView<[global::System.Runtime.CompilerServices.IsUnmanaged] T> where T : struct, ValueType
	{
		// Token: 0x06000BDD RID: 3037 RVA: 0x000236E7 File Offset: 0x000218E7
		public UnsafeRingQueueDebugView(UnsafeRingQueue<T> data)
		{
			this.Data = data;
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000BDE RID: 3038 RVA: 0x000236F8 File Offset: 0x000218F8
		public unsafe T[] Items
		{
			get
			{
				T[] array = new T[this.Data.Length];
				int read = this.Data.Control.Read;
				int capacity = this.Data.Control.Capacity;
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = this.Data.Ptr[(IntPtr)((read + i) % capacity) * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)];
				}
				return array;
			}
		}

		// Token: 0x040003D1 RID: 977
		private UnsafeRingQueue<T> Data;
	}
}
