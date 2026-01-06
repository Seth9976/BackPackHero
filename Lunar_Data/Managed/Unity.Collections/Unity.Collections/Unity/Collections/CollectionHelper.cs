using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Burst;
using Unity.Burst.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;

namespace Unity.Collections
{
	// Token: 0x0200003E RID: 62
	[BurstCompatible]
	public static class CollectionHelper
	{
		// Token: 0x0600010E RID: 270 RVA: 0x000041D3 File Offset: 0x000023D3
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		internal static void CheckAllocator(AllocatorManager.AllocatorHandle allocator)
		{
			if (!CollectionHelper.ShouldDeallocate(allocator))
			{
				throw new ArgumentException(string.Format("Allocator {0} must not be None or Invalid", allocator));
			}
		}

		// Token: 0x0600010F RID: 271 RVA: 0x000041F3 File Offset: 0x000023F3
		public static int Log2Floor(int value)
		{
			return 31 - math.lzcnt((uint)value);
		}

		// Token: 0x06000110 RID: 272 RVA: 0x000041FE File Offset: 0x000023FE
		public static int Log2Ceil(int value)
		{
			return 32 - math.lzcnt((uint)(value - 1));
		}

		// Token: 0x06000111 RID: 273 RVA: 0x0000420B File Offset: 0x0000240B
		public static int Align(int size, int alignmentPowerOfTwo)
		{
			if (alignmentPowerOfTwo == 0)
			{
				return size;
			}
			return (size + alignmentPowerOfTwo - 1) & ~(alignmentPowerOfTwo - 1);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x0000421C File Offset: 0x0000241C
		public static ulong Align(ulong size, ulong alignmentPowerOfTwo)
		{
			if (alignmentPowerOfTwo == 0UL)
			{
				return size;
			}
			return (size + alignmentPowerOfTwo - 1UL) & ~(alignmentPowerOfTwo - 1UL);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x0000422F File Offset: 0x0000242F
		public unsafe static bool IsAligned(void* p, int alignmentPowerOfTwo)
		{
			return ((byte*)p & ((byte*)((long)alignmentPowerOfTwo) - 1L)) == null;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x0000423D File Offset: 0x0000243D
		public static bool IsAligned(ulong offset, int alignmentPowerOfTwo)
		{
			return (offset & (ulong)((long)alignmentPowerOfTwo - 1L)) == 0UL;
		}

		// Token: 0x06000115 RID: 277 RVA: 0x0000424A File Offset: 0x0000244A
		public static bool IsPowerOfTwo(int value)
		{
			return (value & (value - 1)) == 0;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00004254 File Offset: 0x00002454
		public unsafe static uint Hash(void* ptr, int bytes)
		{
			ulong num = 5381UL;
			while (bytes > 0)
			{
				int num2 = --bytes;
				ulong num3 = (ulong)((byte*)ptr)[num2];
				num = (num << 5) + num + num3;
			}
			return (uint)num;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00004288 File Offset: 0x00002488
		[NotBurstCompatible]
		internal static void WriteLayout(Type type)
		{
			Console.WriteLine(string.Format("   Offset | Bytes  | Name     Layout: {0}", 0), type.Name);
			foreach (FieldInfo fieldInfo in type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
			{
				Console.WriteLine("   {0, 6} | {1, 6} | {2}", Marshal.OffsetOf(type, fieldInfo.Name), Marshal.SizeOf(fieldInfo.FieldType), fieldInfo.Name);
			}
		}

		// Token: 0x06000118 RID: 280 RVA: 0x000042FC File Offset: 0x000024FC
		internal static bool ShouldDeallocate(AllocatorManager.AllocatorHandle allocator)
		{
			return allocator.ToAllocator > Allocator.None;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00004308 File Offset: 0x00002508
		[return: AssumeRange(0L, 2147483647L)]
		internal static int AssumePositive(int value)
		{
			return value;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x0000430B File Offset: 0x0000250B
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[BurstDiscard]
		[NotBurstCompatible]
		internal static void CheckIsUnmanaged<T>()
		{
			if (!UnsafeUtility.IsValidNativeContainerElementType<T>())
			{
				throw new ArgumentException(string.Format("{0} used in native collection is not blittable, not primitive, or contains a type tagged as NativeContainer", typeof(T)));
			}
		}

		// Token: 0x0600011B RID: 283 RVA: 0x0000432E File Offset: 0x0000252E
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		internal static void CheckIntPositivePowerOfTwo(int value)
		{
			if (value <= 0 || (value & (value - 1)) != 0)
			{
				throw new ArgumentException(string.Format("Alignment requested: {0} is not a non-zero, positive power of two.", value));
			}
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00004357 File Offset: 0x00002557
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		internal static void CheckUlongPositivePowerOfTwo(ulong value)
		{
			if (value <= 0UL || (value & (value - 1UL)) != 0UL)
			{
				throw new ArgumentException(string.Format("Alignment requested: {0} is not a non-zero, positive power of two.", value));
			}
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00004383 File Offset: 0x00002583
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		internal static void CheckIndexInRange(int index, int length)
		{
			if (index < 0)
			{
				throw new IndexOutOfRangeException(string.Format("Index {0} must be positive.", index));
			}
			if (index >= length)
			{
				throw new IndexOutOfRangeException(string.Format("Index {0} is out of range in container of '{1}' Length.", index, length));
			}
		}

		// Token: 0x0600011E RID: 286 RVA: 0x000043BF File Offset: 0x000025BF
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		internal static void CheckCapacityInRange(int capacity, int length)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException(string.Format("Capacity {0} must be positive.", capacity));
			}
			if (capacity < length)
			{
				throw new ArgumentOutOfRangeException(string.Format("Capacity {0} is out of range in container of '{1}' Length.", capacity, length));
			}
		}

		// Token: 0x0600011F RID: 287 RVA: 0x000043FC File Offset: 0x000025FC
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(AllocatorManager.AllocatorHandle)
		})]
		public static NativeArray<T> CreateNativeArray<T, [global::System.Runtime.CompilerServices.IsUnmanaged] U>(int length, ref U allocator, NativeArrayOptions options = NativeArrayOptions.ClearMemory) where T : struct where U : struct, ValueType, AllocatorManager.IAllocator
		{
			NativeArray<T> nativeArray;
			if (!allocator.IsCustomAllocator)
			{
				nativeArray = new NativeArray<T>(length, allocator.ToAllocator, options);
			}
			else
			{
				nativeArray = default(NativeArray<T>);
				(ref nativeArray).Initialize(length, ref allocator, options);
			}
			return nativeArray;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00004444 File Offset: 0x00002644
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(int) })]
		public static NativeArray<T> CreateNativeArray<T>(int length, AllocatorManager.AllocatorHandle allocator, NativeArrayOptions options = NativeArrayOptions.ClearMemory) where T : struct
		{
			NativeArray<T> nativeArray;
			if (!AllocatorManager.IsCustomAllocator(allocator))
			{
				nativeArray = new NativeArray<T>(length, allocator.ToAllocator, options);
			}
			else
			{
				nativeArray = default(NativeArray<T>);
				(ref nativeArray).Initialize(length, allocator, options);
			}
			return nativeArray;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00004480 File Offset: 0x00002680
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(int) })]
		public static NativeArray<T> CreateNativeArray<T>(NativeArray<T> array, AllocatorManager.AllocatorHandle allocator) where T : struct
		{
			NativeArray<T> nativeArray;
			if (!AllocatorManager.IsCustomAllocator(allocator))
			{
				nativeArray = new NativeArray<T>(array, allocator.ToAllocator);
			}
			else
			{
				nativeArray = default(NativeArray<T>);
				(ref nativeArray).Initialize(array.Length, allocator, NativeArrayOptions.UninitializedMemory);
				nativeArray.CopyFrom(array);
			}
			return nativeArray;
		}

		// Token: 0x06000122 RID: 290 RVA: 0x000044C8 File Offset: 0x000026C8
		[NotBurstCompatible]
		public static NativeArray<T> CreateNativeArray<T>(T[] array, AllocatorManager.AllocatorHandle allocator) where T : struct
		{
			NativeArray<T> nativeArray;
			if (!AllocatorManager.IsCustomAllocator(allocator))
			{
				nativeArray = new NativeArray<T>(array, allocator.ToAllocator);
			}
			else
			{
				nativeArray = default(NativeArray<T>);
				(ref nativeArray).Initialize(array.Length, allocator, NativeArrayOptions.UninitializedMemory);
				nativeArray.CopyFrom(array);
			}
			return nativeArray;
		}

		// Token: 0x06000123 RID: 291 RVA: 0x0000450C File Offset: 0x0000270C
		[NotBurstCompatible]
		public static NativeArray<T> CreateNativeArray<T, [global::System.Runtime.CompilerServices.IsUnmanaged] U>(T[] array, ref U allocator) where T : struct where U : struct, ValueType, AllocatorManager.IAllocator
		{
			NativeArray<T> nativeArray;
			if (!allocator.IsCustomAllocator)
			{
				nativeArray = new NativeArray<T>(array, allocator.ToAllocator);
			}
			else
			{
				nativeArray = default(NativeArray<T>);
				(ref nativeArray).Initialize(array.Length, ref allocator, NativeArrayOptions.ClearMemory);
				nativeArray.CopyFrom(array);
			}
			return nativeArray;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x0000455C File Offset: 0x0000275C
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int),
			typeof(AllocatorManager.AllocatorHandle)
		})]
		public static NativeParallelHashMap<TKey, TValue> CreateNativeParallelHashMap<TKey, TValue, [global::System.Runtime.CompilerServices.IsUnmanaged] U>(int length, ref U allocator) where TKey : struct, IEquatable<TKey> where TValue : struct where U : struct, ValueType, AllocatorManager.IAllocator
		{
			return default(NativeParallelHashMap<TKey, TValue>);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00004574 File Offset: 0x00002774
		[Obsolete("CreateNativeMultiHashMap is renamed to CreateNativeParallelHashMap. (UnityUpgradable) -> CreateNativeParallelHashMap<TKey, TValue, U>(*)", true)]
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int),
			typeof(AllocatorManager.AllocatorHandle)
		})]
		public static NativeHashMap<TKey, TValue> CreateNativeMultiHashMap<TKey, TValue, [global::System.Runtime.CompilerServices.IsUnmanaged] U>(int length, ref U allocator) where TKey : struct, IEquatable<TKey> where TValue : struct where U : struct, ValueType, AllocatorManager.IAllocator
		{
			return default(NativeHashMap<TKey, TValue>);
		}

		// Token: 0x04000079 RID: 121
		public const int CacheLineSize = 64;

		// Token: 0x0200003F RID: 63
		[StructLayout(2)]
		internal struct LongDoubleUnion
		{
			// Token: 0x0400007A RID: 122
			[FieldOffset(0)]
			internal long longValue;

			// Token: 0x0400007B RID: 123
			[FieldOffset(0)]
			internal double doubleValue;
		}
	}
}
