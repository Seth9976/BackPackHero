using System;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;

namespace Unity.Collections.NotBurstCompatible
{
	// Token: 0x020000FB RID: 251
	public static class Extensions
	{
		// Token: 0x06000959 RID: 2393 RVA: 0x0001CA50 File Offset: 0x0001AC50
		[NotBurstCompatible]
		public static T[] ToArray<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeParallelHashSet<T> set) where T : struct, ValueType, IEquatable<T>
		{
			NativeArray<T> nativeArray = set.ToNativeArray(Allocator.TempJob);
			T[] array = nativeArray.ToArray();
			nativeArray.Dispose();
			return array;
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x0001CA7C File Offset: 0x0001AC7C
		[NotBurstCompatible]
		public static T[] ToArrayNBC<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeList<T> list) where T : struct, ValueType
		{
			return list.AsArray().ToArray();
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x0001CA98 File Offset: 0x0001AC98
		[NotBurstCompatible]
		public static void CopyFromNBC<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeList<T> list, T[] array) where T : struct, ValueType
		{
			list.Clear();
			list.Resize(array.Length, NativeArrayOptions.UninitializedMemory);
			list.AsArray().CopyFrom(array);
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x0001CAC7 File Offset: 0x0001ACC7
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		[Obsolete("Burst now supports tuple, please use `GetUniqueKeyArray` method from `Unity.Collections.UnsafeParallelMultiHashMap` instead.", false)]
		public static ValueTuple<NativeArray<TKey>, int> GetUniqueKeyArrayNBC<TKey, TValue>(this UnsafeParallelMultiHashMap<TKey, TValue> hashmap, AllocatorManager.AllocatorHandle allocator) where TKey : struct, IEquatable<TKey>, IComparable<TKey> where TValue : struct
		{
			return hashmap.GetUniqueKeyArray(allocator);
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x0001CAD0 File Offset: 0x0001ACD0
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		[Obsolete("Burst now supports tuple, please use `GetUniqueKeyArray` method from `Unity.Collections.NativeParallelMultiHashMap` instead.", false)]
		public static ValueTuple<NativeArray<TKey>, int> GetUniqueKeyArrayNBC<TKey, TValue>(this NativeParallelMultiHashMap<TKey, TValue> hashmap, AllocatorManager.AllocatorHandle allocator) where TKey : struct, IEquatable<TKey>, IComparable<TKey> where TValue : struct
		{
			return hashmap.GetUniqueKeyArray(allocator);
		}
	}
}
