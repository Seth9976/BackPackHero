using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;
using UnityEngine;

namespace Unity.Collections
{
	// Token: 0x02000052 RID: 82
	[BurstCompatible(GenericTypeArguments = new Type[]
	{
		typeof(int),
		typeof(FixedBytes30)
	})]
	[Serializable]
	internal struct FixedList<[global::System.Runtime.CompilerServices.IsUnmanaged] T, [global::System.Runtime.CompilerServices.IsUnmanaged] U> : INativeList<T>, IIndexable<T> where T : struct, ValueType where U : struct, ValueType
	{
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x00004F62 File Offset: 0x00003162
		// (set) Token: 0x060001A3 RID: 419 RVA: 0x00004F6A File Offset: 0x0000316A
		[CreateProperty]
		public int Length
		{
			get
			{
				return (int)this.length;
			}
			set
			{
				this.length = (ushort)value;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00004F74 File Offset: 0x00003174
		[CreateProperty]
		private IEnumerable<T> Elements
		{
			get
			{
				return this.ToArray();
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x00004F7C File Offset: 0x0000317C
		public bool IsEmpty
		{
			get
			{
				return this.Length == 0;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x00004F87 File Offset: 0x00003187
		internal int LengthInBytes
		{
			get
			{
				return this.Length * UnsafeUtility.SizeOf<T>();
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x00004F98 File Offset: 0x00003198
		internal unsafe byte* Buffer
		{
			get
			{
				fixed (U* ptr = &this.buffer)
				{
					return (byte*)ptr + FixedList.PaddingBytes<T>();
				}
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x00004FB4 File Offset: 0x000031B4
		// (set) Token: 0x060001A9 RID: 425 RVA: 0x00002C2B File Offset: 0x00000E2B
		public int Capacity
		{
			get
			{
				return FixedList.Capacity<U, T>();
			}
			set
			{
			}
		}

		// Token: 0x1700003E RID: 62
		public unsafe T this[int index]
		{
			get
			{
				return UnsafeUtility.ReadArrayElement<T>((void*)this.Buffer, CollectionHelper.AssumePositive(index));
			}
			set
			{
				UnsafeUtility.WriteArrayElement<T>((void*)this.Buffer, CollectionHelper.AssumePositive(index), value);
			}
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00004FE2 File Offset: 0x000031E2
		public unsafe ref T ElementAt(int index)
		{
			return UnsafeUtility.ArrayElementAsRef<T>((void*)this.Buffer, index);
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00004FF0 File Offset: 0x000031F0
		public unsafe override int GetHashCode()
		{
			return (int)CollectionHelper.Hash((void*)this.Buffer, this.LengthInBytes);
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00005004 File Offset: 0x00003204
		public void Add(in T item)
		{
			int num = this.Length;
			this.Length = num + 1;
			this[num] = item;
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00005030 File Offset: 0x00003230
		public unsafe void AddRange(void* ptr, int length)
		{
			for (int i = 0; i < length; i++)
			{
				int num = this.Length;
				this.Length = num + 1;
				this[num] = *(T*)((byte*)ptr + (IntPtr)i * (IntPtr)sizeof(T));
			}
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00005072 File Offset: 0x00003272
		public void AddNoResize(in T item)
		{
			this.Add(in item);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000507B File Offset: 0x0000327B
		public unsafe void AddRangeNoResize(void* ptr, int length)
		{
			this.AddRange(ptr, length);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00005085 File Offset: 0x00003285
		public void Clear()
		{
			this.Length = 0;
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00005090 File Offset: 0x00003290
		public unsafe void InsertRangeWithBeginEnd(int begin, int end)
		{
			int num = end - begin;
			if (num < 1)
			{
				return;
			}
			int num2 = (int)this.length - begin;
			this.Length += num;
			if (num2 < 1)
			{
				return;
			}
			int num3 = num2 * UnsafeUtility.SizeOf<T>();
			byte* ptr = this.Buffer;
			byte* ptr2 = ptr + end * UnsafeUtility.SizeOf<T>();
			byte* ptr3 = ptr + begin * UnsafeUtility.SizeOf<T>();
			UnsafeUtility.MemMove((void*)ptr2, (void*)ptr3, (long)num3);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x000050EE File Offset: 0x000032EE
		public void Insert(int index, in T item)
		{
			this.InsertRangeWithBeginEnd(index, index + 1);
			this[index] = item;
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00005107 File Offset: 0x00003307
		public void RemoveAtSwapBack(int index)
		{
			this.RemoveRangeSwapBack(index, 1);
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00005114 File Offset: 0x00003314
		public unsafe void RemoveRangeSwapBack(int index, int count)
		{
			if (count > 0)
			{
				int num = math.max(this.Length - count, index + count);
				int num2 = UnsafeUtility.SizeOf<T>();
				void* ptr = (void*)(this.Buffer + index * num2);
				void* ptr2 = (void*)(this.Buffer + num * num2);
				UnsafeUtility.MemCpy(ptr, ptr2, (long)((this.Length - num) * num2));
				this.Length -= count;
			}
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00005172 File Offset: 0x00003372
		[Obsolete("RemoveRangeSwapBackWithBeginEnd(begin, end) is deprecated, use RemoveRangeSwapBack(index, count) instead. (RemovedAfter 2021-06-02)", false)]
		public void RemoveRangeSwapBackWithBeginEnd(int begin, int end)
		{
			this.RemoveRangeSwapBack(begin, end - begin);
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0000517E File Offset: 0x0000337E
		public void RemoveAt(int index)
		{
			this.RemoveRange(index, 1);
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00005188 File Offset: 0x00003388
		public unsafe void RemoveRange(int index, int count)
		{
			if (count > 0)
			{
				int num = math.min(index + count, this.Length);
				int num2 = UnsafeUtility.SizeOf<T>();
				void* ptr = (void*)(this.Buffer + index * num2);
				void* ptr2 = (void*)(this.Buffer + num * num2);
				UnsafeUtility.MemCpy(ptr, ptr2, (long)((this.Length - num) * num2));
				this.Length -= count;
			}
		}

		// Token: 0x060001BA RID: 442 RVA: 0x000051E4 File Offset: 0x000033E4
		[Obsolete("RemoveRangeWithBeginEnd(begin, end) is deprecated, use RemoveRange(index, count) instead. (RemovedAfter 2021-06-02)", false)]
		public void RemoveRangeWithBeginEnd(int begin, int end)
		{
			this.RemoveRange(begin, end - begin);
		}

		// Token: 0x060001BB RID: 443 RVA: 0x000051F0 File Offset: 0x000033F0
		[NotBurstCompatible]
		public unsafe T[] ToArray()
		{
			T[] array = new T[this.Length];
			byte* ptr = this.Buffer;
			fixed (T[] array2 = array)
			{
				T* ptr2;
				if (array == null || array2.Length == 0)
				{
					ptr2 = null;
				}
				else
				{
					ptr2 = &array2[0];
				}
				UnsafeUtility.MemCpy((void*)ptr2, (void*)ptr, (long)this.LengthInBytes);
			}
			return array;
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00005237 File Offset: 0x00003437
		public unsafe NativeArray<T> ToNativeArray(AllocatorManager.AllocatorHandle allocator)
		{
			NativeArray<T> nativeArray = CollectionHelper.CreateNativeArray<T>(this.Length, allocator, NativeArrayOptions.UninitializedMemory);
			UnsafeUtility.MemCpy(nativeArray.GetUnsafePtr<T>(), (void*)this.Buffer, (long)this.LengthInBytes);
			return nativeArray;
		}

		// Token: 0x040000AF RID: 175
		[SerializeField]
		internal ushort length;

		// Token: 0x040000B0 RID: 176
		[SerializeField]
		internal U buffer;
	}
}
