using System;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;

namespace Unity.Collections
{
	// Token: 0x020000C8 RID: 200
	[BurstCompatible]
	public static class NativeParallelMultiHashMapExtensions
	{
		// Token: 0x06000798 RID: 1944 RVA: 0x000173F0 File Offset: 0x000155F0
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int),
			typeof(AllocatorManager.AllocatorHandle)
		})]
		internal static void Initialize<TKey, TValue, [global::System.Runtime.CompilerServices.IsUnmanaged] U>(this NativeParallelMultiHashMap<TKey, TValue> container, int capacity, ref U allocator, int disposeSentinelStackDepth = 2) where TKey : struct, IEquatable<TKey> where TValue : struct where U : struct, ValueType, AllocatorManager.IAllocator
		{
			container.m_MultiHashMapData = new UnsafeParallelMultiHashMap<TKey, TValue>(capacity, allocator.Handle);
		}
	}
}
