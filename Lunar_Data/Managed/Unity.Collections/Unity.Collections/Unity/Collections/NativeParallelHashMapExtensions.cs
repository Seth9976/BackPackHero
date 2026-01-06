using System;
using Unity.Collections.LowLevel.Unsafe;

namespace Unity.Collections
{
	// Token: 0x020000BC RID: 188
	[BurstCompatible]
	public static class NativeParallelHashMapExtensions
	{
		// Token: 0x06000731 RID: 1841 RVA: 0x000163CC File Offset: 0x000145CC
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(int) })]
		public static int Unique<T>(this NativeArray<T> array) where T : struct, IEquatable<T>
		{
			if (array.Length == 0)
			{
				return 0;
			}
			int num = 0;
			int length = array.Length;
			int num2 = num;
			while (++num != length)
			{
				T t = array[num2];
				if (!t.Equals(array[num]))
				{
					array[++num2] = array[num];
				}
			}
			return num2 + 1;
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x00016434 File Offset: 0x00014634
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public static ValueTuple<NativeArray<TKey>, int> GetUniqueKeyArray<TKey, TValue>(this UnsafeParallelMultiHashMap<TKey, TValue> container, AllocatorManager.AllocatorHandle allocator) where TKey : struct, IEquatable<TKey>, IComparable<TKey> where TValue : struct
		{
			NativeArray<TKey> keyArray = container.GetKeyArray(allocator);
			keyArray.Sort<TKey>();
			int num = keyArray.Unique<TKey>();
			return new ValueTuple<NativeArray<TKey>, int>(keyArray, num);
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x0001645C File Offset: 0x0001465C
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public static ValueTuple<NativeArray<TKey>, int> GetUniqueKeyArray<TKey, TValue>(this NativeParallelMultiHashMap<TKey, TValue> container, AllocatorManager.AllocatorHandle allocator) where TKey : struct, IEquatable<TKey>, IComparable<TKey> where TValue : struct
		{
			NativeArray<TKey> keyArray = container.GetKeyArray(allocator);
			keyArray.Sort<TKey>();
			int num = keyArray.Unique<TKey>();
			return new ValueTuple<NativeArray<TKey>, int>(keyArray, num);
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x00016484 File Offset: 0x00014684
		[Obsolete("GetBucketData is deprecated, please use GetUnsafeBucketData instead. (RemovedAfter 2021-07-08) (UnityUpgradable) -> GetUnsafeBucketData<TKey,TValue>(*)", false)]
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public unsafe static UnsafeHashMapBucketData GetBucketData<TKey, TValue>(this NativeParallelHashMap<TKey, TValue> container) where TKey : struct, IEquatable<TKey> where TValue : struct
		{
			return container.m_HashMapData.m_Buffer->GetBucketData();
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x00016484 File Offset: 0x00014684
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public unsafe static UnsafeHashMapBucketData GetUnsafeBucketData<TKey, TValue>(this NativeParallelHashMap<TKey, TValue> container) where TKey : struct, IEquatable<TKey> where TValue : struct
		{
			return container.m_HashMapData.m_Buffer->GetBucketData();
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x00016496 File Offset: 0x00014696
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public unsafe static UnsafeHashMapBucketData GetUnsafeBucketData<TKey, TValue>(this NativeParallelMultiHashMap<TKey, TValue> container) where TKey : struct, IEquatable<TKey> where TValue : struct
		{
			return container.m_MultiHashMapData.m_Buffer->GetBucketData();
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x000164A8 File Offset: 0x000146A8
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public static void Remove<TKey, TValue>(this NativeParallelMultiHashMap<TKey, TValue> container, TKey key, TValue value) where TKey : struct, IEquatable<TKey> where TValue : struct, IEquatable<TValue>
		{
			container.m_MultiHashMapData.Remove<TValue>(key, value);
		}
	}
}
