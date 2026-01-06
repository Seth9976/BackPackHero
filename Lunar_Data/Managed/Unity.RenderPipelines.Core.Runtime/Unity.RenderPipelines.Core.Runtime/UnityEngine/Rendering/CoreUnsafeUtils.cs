using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.Rendering
{
	// Token: 0x0200004B RID: 75
	public static class CoreUnsafeUtils
	{
		// Token: 0x0600027E RID: 638 RVA: 0x0000D30C File Offset: 0x0000B50C
		public unsafe static void CopyTo<T>(this List<T> list, void* dest, int count) where T : struct
		{
			int num = Mathf.Min(count, list.Count);
			for (int i = 0; i < num; i++)
			{
				UnsafeUtility.WriteArrayElement<T>(dest, i, list[i]);
			}
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000D340 File Offset: 0x0000B540
		public unsafe static void CopyTo<T>(this T[] list, void* dest, int count) where T : struct
		{
			int num = Mathf.Min(count, list.Length);
			for (int i = 0; i < num; i++)
			{
				UnsafeUtility.WriteArrayElement<T>(dest, i, list[i]);
			}
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000D371 File Offset: 0x0000B571
		private static void CalculateRadixParams(int radixBits, out int bitStates)
		{
			if (radixBits != 2 && radixBits != 4 && radixBits != 8)
			{
				throw new Exception("Radix bits must be 2, 4 or 8 for uint radix sort.");
			}
			bitStates = 1 << radixBits;
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000D392 File Offset: 0x0000B592
		private static int CalculateRadixSupportSize(int bitStates, int arrayLength)
		{
			return bitStates * 3 + arrayLength;
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000D399 File Offset: 0x0000B599
		private unsafe static void CalculateRadixSortSupportArrays(int bitStates, int arrayLength, uint* supportArray, out uint* bucketIndices, out uint* bucketSizes, out uint* bucketPrefix, out uint* arrayOutput)
		{
			bucketIndices = supportArray;
			bucketSizes = bucketIndices + (IntPtr)bitStates * 4;
			bucketPrefix = bucketSizes + (IntPtr)bitStates * 4;
			arrayOutput = bucketPrefix + (IntPtr)bitStates * 4;
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000D3C0 File Offset: 0x0000B5C0
		private unsafe static void MergeSort(uint* array, uint* support, int length)
		{
			for (int i = 1; i < length; i *= 2)
			{
				int num = 0;
				while (num + i < length)
				{
					int num2 = num + i;
					int num3 = num2 + i;
					if (num3 > length)
					{
						num3 = length;
					}
					int j = num;
					int k = num;
					int l = num2;
					while (k < num2)
					{
						if (l >= num3)
						{
							break;
						}
						if (array[k] <= array[l])
						{
							support[j] = array[k++];
						}
						else
						{
							support[j] = array[l++];
						}
						j++;
					}
					while (k < num2)
					{
						support[j] = array[k++];
						j++;
					}
					while (l < num3)
					{
						support[j] = array[l++];
						j++;
					}
					for (j = num; j < num3; j++)
					{
						array[j] = support[j];
					}
					num += i * 2;
				}
			}
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000D4C0 File Offset: 0x0000B6C0
		public unsafe static void MergeSort(uint[] arr, int sortSize, ref uint[] supportArray)
		{
			sortSize = Math.Min(sortSize, arr.Length);
			if (arr == null || sortSize == 0)
			{
				return;
			}
			if (supportArray == null || supportArray.Length < sortSize)
			{
				supportArray = new uint[sortSize];
			}
			fixed (uint[] array = arr)
			{
				uint* ptr;
				if (arr == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				uint[] array2;
				uint* ptr2;
				if ((array2 = supportArray) == null || array2.Length == 0)
				{
					ptr2 = null;
				}
				else
				{
					ptr2 = &array2[0];
				}
				CoreUnsafeUtils.MergeSort(ptr, ptr2, sortSize);
				array2 = null;
			}
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000D530 File Offset: 0x0000B730
		public unsafe static void MergeSort(NativeArray<uint> arr, int sortSize, ref NativeArray<uint> supportArray)
		{
			sortSize = Math.Min(sortSize, arr.Length);
			if (!arr.IsCreated || sortSize == 0)
			{
				return;
			}
			if (!supportArray.IsCreated || supportArray.Length < sortSize)
			{
				(ref supportArray).ResizeArray(arr.Length);
			}
			CoreUnsafeUtils.MergeSort((uint*)arr.GetUnsafePtr<uint>(), (uint*)supportArray.GetUnsafePtr<uint>(), sortSize);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000D590 File Offset: 0x0000B790
		private unsafe static void InsertionSort(uint* arr, int length)
		{
			for (int i = 0; i < length; i++)
			{
				int num = i;
				while (num >= 1 && arr[num] < arr[num - 1])
				{
					uint num2 = arr[num];
					arr[num] = arr[num - 1];
					arr[num - 1] = num2;
					num--;
				}
			}
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000D5EC File Offset: 0x0000B7EC
		public unsafe static void InsertionSort(uint[] arr, int sortSize)
		{
			sortSize = Math.Min(arr.Length, sortSize);
			if (arr == null || sortSize == 0)
			{
				return;
			}
			fixed (uint[] array = arr)
			{
				uint* ptr;
				if (arr == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				CoreUnsafeUtils.InsertionSort(ptr, sortSize);
			}
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000D62C File Offset: 0x0000B82C
		public unsafe static void InsertionSort(NativeArray<uint> arr, int sortSize)
		{
			sortSize = Math.Min(arr.Length, sortSize);
			if (!arr.IsCreated || sortSize == 0)
			{
				return;
			}
			CoreUnsafeUtils.InsertionSort((uint*)arr.GetUnsafePtr<uint>(), sortSize);
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000D658 File Offset: 0x0000B858
		private unsafe static void RadixSort(uint* array, uint* support, int radixBits, int bitStates, int length)
		{
			uint num = (uint)(bitStates - 1);
			uint* ptr;
			uint* ptr2;
			uint* ptr3;
			uint* ptr4;
			CoreUnsafeUtils.CalculateRadixSortSupportArrays(bitStates, length, support, out ptr, out ptr2, out ptr3, out ptr4);
			int num2 = 32 / radixBits;
			uint* ptr5 = ptr4;
			uint* ptr6 = array;
			for (int i = 0; i < num2; i++)
			{
				int num3 = i * radixBits;
				for (int j = 0; j < 3 * bitStates; j++)
				{
					ptr[j] = 0U;
				}
				for (int k = 0; k < length; k++)
				{
					ptr2[(ulong)((ptr6[k] >> num3) & num) * 4UL / 4UL] += 1U;
				}
				for (int l = 1; l < bitStates; l++)
				{
					ptr3[l] = ptr3[l - 1] + ptr2[l - 1];
				}
				for (int m = 0; m < length; m++)
				{
					uint num4 = ptr6[m];
					uint num5 = (num4 >> num3) & num;
					ref int ptr7 = ref *(int*)ptr5;
					uint num6 = ptr3[(ulong)num5 * 4UL / 4UL];
					uint* ptr8 = ptr + (ulong)num5 * 4UL / 4UL;
					uint num7 = *ptr8;
					*ptr8 = num7 + 1U;
					*((ref ptr7) + (IntPtr)((ulong)(num6 + num7) * 4UL)) = (int)num4;
				}
				uint* ptr9 = ptr6;
				ptr6 = ptr5;
				ptr5 = ptr9;
			}
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000D774 File Offset: 0x0000B974
		public unsafe static void RadixSort(uint[] arr, int sortSize, ref uint[] supportArray, int radixBits = 8)
		{
			sortSize = Math.Min(sortSize, arr.Length);
			int num;
			CoreUnsafeUtils.CalculateRadixParams(radixBits, out num);
			if (arr == null || sortSize == 0)
			{
				return;
			}
			int num2 = CoreUnsafeUtils.CalculateRadixSupportSize(num, sortSize);
			if (supportArray == null || supportArray.Length < num2)
			{
				supportArray = new uint[num2];
			}
			fixed (uint[] array = arr)
			{
				uint* ptr;
				if (arr == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				uint[] array2;
				uint* ptr2;
				if ((array2 = supportArray) == null || array2.Length == 0)
				{
					ptr2 = null;
				}
				else
				{
					ptr2 = &array2[0];
				}
				CoreUnsafeUtils.RadixSort(ptr, ptr2, radixBits, num, sortSize);
				array2 = null;
			}
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000D7FC File Offset: 0x0000B9FC
		public unsafe static void RadixSort(NativeArray<uint> array, int sortSize, ref NativeArray<uint> supportArray, int radixBits = 8)
		{
			sortSize = Math.Min(sortSize, array.Length);
			int num;
			CoreUnsafeUtils.CalculateRadixParams(radixBits, out num);
			if (!array.IsCreated || sortSize == 0)
			{
				return;
			}
			int num2 = CoreUnsafeUtils.CalculateRadixSupportSize(num, sortSize);
			if (!supportArray.IsCreated || supportArray.Length < num2)
			{
				(ref supportArray).ResizeArray(num2);
			}
			CoreUnsafeUtils.RadixSort((uint*)array.GetUnsafePtr<uint>(), (uint*)supportArray.GetUnsafePtr<uint>(), radixBits, num, sortSize);
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000D868 File Offset: 0x0000BA68
		public unsafe static void QuickSort(uint[] arr, int left, int right)
		{
			fixed (uint[] array = arr)
			{
				uint* ptr;
				if (arr == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				CoreUnsafeUtils.QuickSort<uint, uint, CoreUnsafeUtils.UintKeyGetter>((void*)ptr, left, right);
			}
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000D897 File Offset: 0x0000BA97
		public unsafe static void QuickSort<T>(int count, void* data) where T : struct, IComparable<T>
		{
			CoreUnsafeUtils.QuickSort<T, T, CoreUnsafeUtils.DefaultKeyGetter<T>>(data, 0, count - 1);
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000D8A3 File Offset: 0x0000BAA3
		public unsafe static void QuickSort<TValue, TKey, TGetter>(int count, void* data) where TValue : struct where TKey : struct, IComparable<TKey> where TGetter : struct, CoreUnsafeUtils.IKeyGetter<TValue, TKey>
		{
			CoreUnsafeUtils.QuickSort<TValue, TKey, TGetter>(data, 0, count - 1);
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000D8B0 File Offset: 0x0000BAB0
		public unsafe static void QuickSort<TValue, TKey, TGetter>(void* data, int left, int right) where TValue : struct where TKey : struct, IComparable<TKey> where TGetter : struct, CoreUnsafeUtils.IKeyGetter<TValue, TKey>
		{
			if (left < right)
			{
				int num = CoreUnsafeUtils.Partition<TValue, TKey, TGetter>(data, left, right);
				if (num >= 1)
				{
					CoreUnsafeUtils.QuickSort<TValue, TKey, TGetter>(data, left, num);
				}
				if (num + 1 < right)
				{
					CoreUnsafeUtils.QuickSort<TValue, TKey, TGetter>(data, num + 1, right);
				}
			}
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000D8E8 File Offset: 0x0000BAE8
		public unsafe static int IndexOf<T>(void* data, int count, T v) where T : struct, IEquatable<T>
		{
			for (int i = 0; i < count; i++)
			{
				T t = UnsafeUtility.ReadArrayElement<T>(data, i);
				if (t.Equals(v))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000D91C File Offset: 0x0000BB1C
		public unsafe static int CompareHashes<TOldValue, TOldGetter, TNewValue, TNewGetter>(int oldHashCount, void* oldHashes, int newHashCount, void* newHashes, int* addIndices, int* removeIndices, out int addCount, out int remCount) where TOldValue : struct where TOldGetter : struct, CoreUnsafeUtils.IKeyGetter<TOldValue, Hash128> where TNewValue : struct where TNewGetter : struct, CoreUnsafeUtils.IKeyGetter<TNewValue, Hash128>
		{
			TOldGetter toldGetter = new TOldGetter();
			TNewGetter tnewGetter = new TNewGetter();
			addCount = 0;
			remCount = 0;
			if (oldHashCount == newHashCount)
			{
				Hash128 hash = default(Hash128);
				Hash128 hash2 = default(Hash128);
				CoreUnsafeUtils.CombineHashes<TOldValue, TOldGetter>(oldHashCount, oldHashes, &hash);
				CoreUnsafeUtils.CombineHashes<TNewValue, TNewGetter>(newHashCount, newHashes, &hash2);
				if (hash == hash2)
				{
					return 0;
				}
			}
			int num = 0;
			int i = 0;
			int j = 0;
			while (i < oldHashCount || j < newHashCount)
			{
				if (i == oldHashCount)
				{
					while (j < newHashCount)
					{
						int num2 = addCount;
						addCount = num2 + 1;
						addIndices[num2] = j;
						num++;
						j++;
					}
				}
				else if (j == newHashCount)
				{
					while (i < oldHashCount)
					{
						int num2 = remCount;
						remCount = num2 + 1;
						removeIndices[num2] = i;
						num++;
						i++;
					}
				}
				else
				{
					TNewValue tnewValue = UnsafeUtility.ReadArrayElement<TNewValue>(newHashes, j);
					TOldValue toldValue = UnsafeUtility.ReadArrayElement<TOldValue>(oldHashes, i);
					Hash128 hash3 = tnewGetter.Get(ref tnewValue);
					Hash128 hash4 = toldGetter.Get(ref toldValue);
					if (hash3 == hash4)
					{
						j++;
						i++;
					}
					else if (hash3 < hash4)
					{
						while (j < newHashCount)
						{
							if (!(hash3 < hash4))
							{
								break;
							}
							int num2 = addCount;
							addCount = num2 + 1;
							addIndices[num2] = j;
							j++;
							num++;
							tnewValue = UnsafeUtility.ReadArrayElement<TNewValue>(newHashes, j);
							hash3 = tnewGetter.Get(ref tnewValue);
						}
					}
					else
					{
						while (i < oldHashCount && hash4 < hash3)
						{
							int num2 = remCount;
							remCount = num2 + 1;
							removeIndices[num2] = i;
							num++;
							i++;
						}
					}
				}
			}
			return num;
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000DAC4 File Offset: 0x0000BCC4
		public unsafe static int CompareHashes(int oldHashCount, Hash128* oldHashes, int newHashCount, Hash128* newHashes, int* addIndices, int* removeIndices, out int addCount, out int remCount)
		{
			return CoreUnsafeUtils.CompareHashes<Hash128, CoreUnsafeUtils.DefaultKeyGetter<Hash128>, Hash128, CoreUnsafeUtils.DefaultKeyGetter<Hash128>>(oldHashCount, (void*)oldHashes, newHashCount, (void*)newHashes, addIndices, removeIndices, out addCount, out remCount);
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000DAD8 File Offset: 0x0000BCD8
		public unsafe static void CombineHashes<TValue, TGetter>(int count, void* hashes, Hash128* outHash) where TValue : struct where TGetter : struct, CoreUnsafeUtils.IKeyGetter<TValue, Hash128>
		{
			TGetter tgetter = new TGetter();
			for (int i = 0; i < count; i++)
			{
				TValue tvalue = UnsafeUtility.ReadArrayElement<TValue>(hashes, i);
				Hash128 hash = tgetter.Get(ref tvalue);
				HashUtilities.AppendHash(ref hash, ref *outHash);
			}
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000DB17 File Offset: 0x0000BD17
		public unsafe static void CombineHashes(int count, Hash128* hashes, Hash128* outHash)
		{
			CoreUnsafeUtils.CombineHashes<Hash128, CoreUnsafeUtils.DefaultKeyGetter<Hash128>>(count, (void*)hashes, outHash);
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000DB24 File Offset: 0x0000BD24
		private unsafe static int Partition<TValue, TKey, TGetter>(void* data, int left, int right) where TValue : struct where TKey : struct, IComparable<TKey> where TGetter : struct, CoreUnsafeUtils.IKeyGetter<TValue, TKey>
		{
			TGetter tgetter = default(TGetter);
			TValue tvalue = UnsafeUtility.ReadArrayElement<TValue>(data, left);
			TKey tkey = tgetter.Get(ref tvalue);
			left--;
			right++;
			for (;;)
			{
				TValue tvalue2 = default(TValue);
				TKey tkey2 = default(TKey);
				int num;
				do
				{
					left++;
					tvalue2 = UnsafeUtility.ReadArrayElement<TValue>(data, left);
					tkey2 = tgetter.Get(ref tvalue2);
					num = tkey2.CompareTo(tkey);
				}
				while (num < 0);
				TValue tvalue3 = default(TValue);
				TKey tkey3 = default(TKey);
				do
				{
					right--;
					tvalue3 = UnsafeUtility.ReadArrayElement<TValue>(data, right);
					tkey3 = tgetter.Get(ref tvalue3);
					num = tkey3.CompareTo(tkey);
				}
				while (num > 0);
				if (left >= right)
				{
					break;
				}
				UnsafeUtility.WriteArrayElement<TValue>(data, right, tvalue2);
				UnsafeUtility.WriteArrayElement<TValue>(data, left, tvalue3);
			}
			return right;
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000DC00 File Offset: 0x0000BE00
		public unsafe static bool HaveDuplicates(int[] arr)
		{
			int* ptr;
			checked
			{
				ptr = stackalloc int[unchecked((UIntPtr)arr.Length) * 4];
				arr.CopyTo((void*)ptr, arr.Length);
				CoreUnsafeUtils.QuickSort<int>(arr.Length, (void*)ptr);
			}
			for (int i = arr.Length - 1; i > 0; i--)
			{
				if (UnsafeUtility.ReadArrayElement<int>((void*)ptr, i).CompareTo(UnsafeUtility.ReadArrayElement<int>((void*)ptr, i - 1)) == 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x02000139 RID: 313
		public struct FixedBufferStringQueue
		{
			// Token: 0x170000F2 RID: 242
			// (get) Token: 0x06000832 RID: 2098 RVA: 0x00022F69 File Offset: 0x00021169
			// (set) Token: 0x06000833 RID: 2099 RVA: 0x00022F71 File Offset: 0x00021171
			public int Count { readonly get; private set; }

			// Token: 0x06000834 RID: 2100 RVA: 0x00022F7C File Offset: 0x0002117C
			public unsafe FixedBufferStringQueue(byte* ptr, int length)
			{
				this.m_BufferStart = ptr;
				this.m_BufferLength = length;
				this.m_BufferEnd = this.m_BufferStart + this.m_BufferLength;
				this.m_ReadCursor = this.m_BufferStart;
				this.m_WriteCursor = this.m_BufferStart;
				this.Count = 0;
				this.Clear();
			}

			// Token: 0x06000835 RID: 2101 RVA: 0x00022FD0 File Offset: 0x000211D0
			public unsafe bool TryPush(string v)
			{
				int num = v.Length * 2 + 4;
				if (this.m_WriteCursor + num >= this.m_BufferEnd)
				{
					return false;
				}
				*(int*)this.m_WriteCursor = v.Length;
				this.m_WriteCursor += 4;
				char* ptr = (char*)this.m_WriteCursor;
				int i = 0;
				while (i < v.Length)
				{
					*ptr = v[i];
					i++;
					ptr++;
				}
				this.m_WriteCursor += 2 * v.Length;
				int num2 = this.Count + 1;
				this.Count = num2;
				return true;
			}

			// Token: 0x06000836 RID: 2102 RVA: 0x00023060 File Offset: 0x00021260
			public unsafe bool TryPop(out string v)
			{
				int num = *(int*)this.m_ReadCursor;
				if (num != 0)
				{
					this.m_ReadCursor += 4;
					v = new string((char*)this.m_ReadCursor, 0, num);
					this.m_ReadCursor += num * 2;
					return true;
				}
				v = null;
				return false;
			}

			// Token: 0x06000837 RID: 2103 RVA: 0x000230AB File Offset: 0x000212AB
			public unsafe void Clear()
			{
				this.m_WriteCursor = this.m_BufferStart;
				this.m_ReadCursor = this.m_BufferStart;
				this.Count = 0;
				UnsafeUtility.MemClear((void*)this.m_BufferStart, (long)this.m_BufferLength);
			}

			// Token: 0x040004F5 RID: 1269
			private unsafe byte* m_ReadCursor;

			// Token: 0x040004F6 RID: 1270
			private unsafe byte* m_WriteCursor;

			// Token: 0x040004F7 RID: 1271
			private unsafe readonly byte* m_BufferEnd;

			// Token: 0x040004F8 RID: 1272
			private unsafe readonly byte* m_BufferStart;

			// Token: 0x040004F9 RID: 1273
			private readonly int m_BufferLength;
		}

		// Token: 0x0200013A RID: 314
		public interface IKeyGetter<TValue, TKey>
		{
			// Token: 0x06000838 RID: 2104
			TKey Get(ref TValue v);
		}

		// Token: 0x0200013B RID: 315
		internal struct DefaultKeyGetter<T> : CoreUnsafeUtils.IKeyGetter<T, T>
		{
			// Token: 0x06000839 RID: 2105 RVA: 0x000230DE File Offset: 0x000212DE
			public T Get(ref T v)
			{
				return v;
			}
		}

		// Token: 0x0200013C RID: 316
		internal struct UintKeyGetter : CoreUnsafeUtils.IKeyGetter<uint, uint>
		{
			// Token: 0x0600083A RID: 2106 RVA: 0x000230E6 File Offset: 0x000212E6
			public uint Get(ref uint v)
			{
				return v;
			}
		}
	}
}
