using System;
using System.Runtime.CompilerServices;

namespace Unity.Collections
{
	// Token: 0x020000E5 RID: 229
	internal struct UnmanagedArray<[global::System.Runtime.CompilerServices.IsUnmanaged] T> : IDisposable where T : struct, ValueType
	{
		// Token: 0x060008BA RID: 2234 RVA: 0x00019E36 File Offset: 0x00018036
		public unsafe UnmanagedArray(int length, AllocatorManager.AllocatorHandle allocator)
		{
			this.m_pointer = (IntPtr)((void*)Memory.Unmanaged.Array.Allocate<T>((long)length, allocator));
			this.m_length = length;
			this.m_allocator = allocator;
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x00019E59 File Offset: 0x00018059
		public unsafe void Dispose()
		{
			Memory.Unmanaged.Free<T>((T*)(void*)this.m_pointer, Allocator.Persistent);
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x00019E71 File Offset: 0x00018071
		public unsafe T* GetUnsafePointer()
		{
			return (T*)(void*)this.m_pointer;
		}

		// Token: 0x170000F2 RID: 242
		public unsafe ref T this[int index]
		{
			get
			{
				return ref *(T*)((byte*)(void*)this.m_pointer + (IntPtr)index * (IntPtr)sizeof(T));
			}
		}

		// Token: 0x040002D9 RID: 729
		private IntPtr m_pointer;

		// Token: 0x040002DA RID: 730
		private int m_length;

		// Token: 0x040002DB RID: 731
		private AllocatorManager.AllocatorHandle m_allocator;
	}
}
