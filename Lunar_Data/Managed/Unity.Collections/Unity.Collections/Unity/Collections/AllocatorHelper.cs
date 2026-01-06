using System;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;

namespace Unity.Collections
{
	// Token: 0x02000033 RID: 51
	[BurstCompatible(GenericTypeArguments = new Type[] { typeof(AllocatorManager.AllocatorHandle) })]
	public struct AllocatorHelper<[global::System.Runtime.CompilerServices.IsUnmanaged] T> : IDisposable where T : struct, ValueType, AllocatorManager.IAllocator
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x0000384E File Offset: 0x00001A4E
		public unsafe ref T Allocator
		{
			get
			{
				return UnsafeUtility.AsRef<T>((void*)this.m_allocator);
			}
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x0000385C File Offset: 0x00001A5C
		[NotBurstCompatible]
		public unsafe AllocatorHelper(AllocatorManager.AllocatorHandle backingAllocator)
		{
			ref T ptr = ref AllocatorManager.CreateAllocator<T>(backingAllocator);
			this.m_allocator = (T*)UnsafeUtility.AddressOf<T>(ref ptr);
			this.m_backingAllocator = backingAllocator;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00003883 File Offset: 0x00001A83
		[NotBurstCompatible]
		public unsafe void Dispose()
		{
			UnsafeUtility.AsRef<T>((void*)this.m_allocator).DestroyAllocator(this.m_backingAllocator);
		}

		// Token: 0x0400006C RID: 108
		private unsafe readonly T* m_allocator;

		// Token: 0x0400006D RID: 109
		private AllocatorManager.AllocatorHandle m_backingAllocator;
	}
}
