using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;

namespace Pathfinding.Util
{
	// Token: 0x02000245 RID: 581
	public static class Memory
	{
		// Token: 0x06000D83 RID: 3459 RVA: 0x000567D8 File Offset: 0x000549D8
		public static T[] ShrinkArray<T>(T[] arr, int newLength)
		{
			newLength = Math.Min(newLength, arr.Length);
			T[] array = new T[newLength];
			Array.Copy(arr, array, newLength);
			return array;
		}

		// Token: 0x06000D84 RID: 3460 RVA: 0x00056800 File Offset: 0x00054A00
		[MethodImpl(256)]
		public static void Swap<T>(ref T a, ref T b)
		{
			T t = a;
			a = b;
			b = t;
		}

		// Token: 0x06000D85 RID: 3461 RVA: 0x00056828 File Offset: 0x00054A28
		public static void Realloc<T>(ref NativeArray<T> arr, int newSize, Allocator allocator, NativeArrayOptions options = NativeArrayOptions.ClearMemory) where T : struct
		{
			if (arr.IsCreated && arr.Length >= newSize)
			{
				return;
			}
			NativeArray<T> nativeArray = new NativeArray<T>(newSize, allocator, options);
			if (arr.IsCreated)
			{
				NativeArray<T>.Copy(arr, nativeArray, arr.Length);
				arr.Dispose();
			}
			arr = nativeArray;
		}

		// Token: 0x06000D86 RID: 3462 RVA: 0x00056878 File Offset: 0x00054A78
		public static void Realloc<T>(ref T[] arr, int newSize)
		{
			if (arr == null)
			{
				arr = new T[newSize];
				return;
			}
			if (newSize > arr.Length)
			{
				T[] array = new T[newSize];
				arr.CopyTo(array, 0);
				arr = array;
			}
		}

		// Token: 0x06000D87 RID: 3463 RVA: 0x000568AC File Offset: 0x00054AAC
		public unsafe static T[] UnsafeAppendBufferToArray<[IsUnmanaged] T>(UnsafeAppendBuffer src) where T : struct, ValueType
		{
			int num = src.Length / UnsafeUtility.SizeOf<T>();
			T[] array = new T[num];
			GCHandle gchandle = GCHandle.Alloc(array, GCHandleType.Pinned);
			UnsafeUtility.MemCpy((void*)gchandle.AddrOfPinnedObject(), (void*)src.Ptr, (long)num * (long)UnsafeUtility.SizeOf<T>());
			gchandle.Free();
			return array;
		}

		// Token: 0x06000D88 RID: 3464 RVA: 0x000568FC File Offset: 0x00054AFC
		public static void Rotate3DArray<T>(T[] arr, int3 size, int dx, int dz)
		{
			int x = size.x;
			int y = size.y;
			int z = size.z;
			dx %= x;
			dz %= z;
			if (dx != 0)
			{
				if (dx < 0)
				{
					dx = x + dx;
				}
				T[] array = ArrayPool<T>.Claim(dx);
				for (int i = 0; i < y; i++)
				{
					int num = i * x * z;
					for (int j = 0; j < z; j++)
					{
						Array.Copy(arr, num + j * x + x - dx, array, 0, dx);
						Array.Copy(arr, num + j * x, arr, num + j * x + dx, x - dx);
						Array.Copy(array, 0, arr, num + j * x, dx);
					}
				}
				ArrayPool<T>.Release(ref array, false);
			}
			if (dz != 0)
			{
				if (dz < 0)
				{
					dz = z + dz;
				}
				T[] array2 = ArrayPool<T>.Claim(dz * x);
				for (int k = 0; k < y; k++)
				{
					int num2 = k * x * z;
					Array.Copy(arr, num2 + (z - dz) * x, array2, 0, dz * x);
					Array.Copy(arr, num2, arr, num2 + dz * x, (z - dz) * x);
					Array.Copy(array2, 0, arr, num2, dz * x);
				}
				ArrayPool<T>.Release(ref array2, false);
			}
		}
	}
}
