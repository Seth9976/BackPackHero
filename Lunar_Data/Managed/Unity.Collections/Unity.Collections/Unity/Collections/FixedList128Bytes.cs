using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;
using UnityEngine;

namespace Unity.Collections
{
	// Token: 0x0200005F RID: 95
	[DebuggerTypeProxy(typeof(FixedList128BytesDebugView<>))]
	[BurstCompatible(GenericTypeArguments = new Type[] { typeof(int) })]
	[Serializable]
	public struct FixedList128Bytes<[global::System.Runtime.CompilerServices.IsUnmanaged] T> : INativeList<T>, IIndexable<T>, IEnumerable<T>, IEnumerable, IEquatable<FixedList32Bytes<T>>, IComparable<FixedList32Bytes<T>>, IEquatable<FixedList64Bytes<T>>, IComparable<FixedList64Bytes<T>>, IEquatable<FixedList128Bytes<T>>, IComparable<FixedList128Bytes<T>>, IEquatable<FixedList512Bytes<T>>, IComparable<FixedList512Bytes<T>>, IEquatable<FixedList4096Bytes<T>>, IComparable<FixedList4096Bytes<T>> where T : struct, ValueType
	{
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000257 RID: 599 RVA: 0x00006A47 File Offset: 0x00004C47
		// (set) Token: 0x06000258 RID: 600 RVA: 0x00006A4F File Offset: 0x00004C4F
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

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000259 RID: 601 RVA: 0x00006A59 File Offset: 0x00004C59
		[CreateProperty]
		private IEnumerable<T> Elements
		{
			get
			{
				return this.ToArray();
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600025A RID: 602 RVA: 0x00006A61 File Offset: 0x00004C61
		public bool IsEmpty
		{
			get
			{
				return this.Length == 0;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600025B RID: 603 RVA: 0x00006A6C File Offset: 0x00004C6C
		internal int LengthInBytes
		{
			get
			{
				return this.Length * UnsafeUtility.SizeOf<T>();
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600025C RID: 604 RVA: 0x00006A7C File Offset: 0x00004C7C
		internal unsafe byte* Buffer
		{
			get
			{
				fixed (byte* ptr = &this.buffer.offset0000.byte0000)
				{
					return ptr + FixedList.PaddingBytes<T>();
				}
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600025D RID: 605 RVA: 0x00006AA2 File Offset: 0x00004CA2
		// (set) Token: 0x0600025E RID: 606 RVA: 0x00002C2B File Offset: 0x00000E2B
		public int Capacity
		{
			get
			{
				return FixedList.Capacity<FixedBytes126, T>();
			}
			set
			{
			}
		}

		// Token: 0x17000059 RID: 89
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

		// Token: 0x06000261 RID: 609 RVA: 0x00006AD0 File Offset: 0x00004CD0
		public unsafe ref T ElementAt(int index)
		{
			return UnsafeUtility.ArrayElementAsRef<T>((void*)this.Buffer, index);
		}

		// Token: 0x06000262 RID: 610 RVA: 0x00006ADE File Offset: 0x00004CDE
		public unsafe override int GetHashCode()
		{
			return (int)CollectionHelper.Hash((void*)this.Buffer, this.LengthInBytes);
		}

		// Token: 0x06000263 RID: 611 RVA: 0x00006AF4 File Offset: 0x00004CF4
		public void Add(in T item)
		{
			int num = this.Length;
			this.Length = num + 1;
			this[num] = item;
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00006B20 File Offset: 0x00004D20
		public unsafe void AddRange(void* ptr, int length)
		{
			for (int i = 0; i < length; i++)
			{
				int num = this.Length;
				this.Length = num + 1;
				this[num] = *(T*)((byte*)ptr + (IntPtr)i * (IntPtr)sizeof(T));
			}
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00006B62 File Offset: 0x00004D62
		public void AddNoResize(in T item)
		{
			this.Add(in item);
		}

		// Token: 0x06000266 RID: 614 RVA: 0x00006B6B File Offset: 0x00004D6B
		public unsafe void AddRangeNoResize(void* ptr, int length)
		{
			this.AddRange(ptr, length);
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00006B75 File Offset: 0x00004D75
		public void Clear()
		{
			this.Length = 0;
		}

		// Token: 0x06000268 RID: 616 RVA: 0x00006B80 File Offset: 0x00004D80
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

		// Token: 0x06000269 RID: 617 RVA: 0x00006BDE File Offset: 0x00004DDE
		public void Insert(int index, in T item)
		{
			this.InsertRangeWithBeginEnd(index, index + 1);
			this[index] = item;
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00006BF7 File Offset: 0x00004DF7
		public void RemoveAtSwapBack(int index)
		{
			this.RemoveRangeSwapBack(index, 1);
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00006C04 File Offset: 0x00004E04
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

		// Token: 0x0600026C RID: 620 RVA: 0x00006C62 File Offset: 0x00004E62
		[Obsolete("RemoveRangeSwapBackWithBeginEnd(begin, end) is deprecated, use RemoveRangeSwapBack(index, count) instead. (RemovedAfter 2021-06-02)", false)]
		public void RemoveRangeSwapBackWithBeginEnd(int begin, int end)
		{
			this.RemoveRangeSwapBack(begin, end - begin);
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00006C6E File Offset: 0x00004E6E
		public void RemoveAt(int index)
		{
			this.RemoveRange(index, 1);
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00006C78 File Offset: 0x00004E78
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

		// Token: 0x0600026F RID: 623 RVA: 0x00006CD4 File Offset: 0x00004ED4
		[Obsolete("RemoveRangeWithBeginEnd(begin, end) is deprecated, use RemoveRange(index, count) instead. (RemovedAfter 2021-06-02)", false)]
		public void RemoveRangeWithBeginEnd(int begin, int end)
		{
			this.RemoveRange(begin, end - begin);
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00006CE0 File Offset: 0x00004EE0
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

		// Token: 0x06000271 RID: 625 RVA: 0x00006D27 File Offset: 0x00004F27
		public unsafe NativeArray<T> ToNativeArray(AllocatorManager.AllocatorHandle allocator)
		{
			NativeArray<T> nativeArray = CollectionHelper.CreateNativeArray<T>(this.Length, allocator, NativeArrayOptions.UninitializedMemory);
			UnsafeUtility.MemCpy(nativeArray.GetUnsafePtr<T>(), (void*)this.Buffer, (long)this.LengthInBytes);
			return nativeArray;
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00006D50 File Offset: 0x00004F50
		public unsafe static bool operator ==(in FixedList128Bytes<T> a, in FixedList32Bytes<T> b)
		{
			if (a.length != b.length)
			{
				return false;
			}
			FixedList128Bytes<T> fixedList128Bytes = a;
			void* ptr = (void*)fixedList128Bytes.Buffer;
			FixedList32Bytes<T> fixedList32Bytes = b;
			void* ptr2 = (void*)fixedList32Bytes.Buffer;
			fixedList128Bytes = a;
			return UnsafeUtility.MemCmp(ptr, ptr2, (long)fixedList128Bytes.LengthInBytes) == 0;
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00006DA0 File Offset: 0x00004FA0
		public static bool operator !=(in FixedList128Bytes<T> a, in FixedList32Bytes<T> b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x06000274 RID: 628 RVA: 0x00006DAC File Offset: 0x00004FAC
		public unsafe int CompareTo(FixedList32Bytes<T> other)
		{
			fixed (byte* ptr = &this.buffer.offset0000.byte0000)
			{
				byte* ptr2 = ptr;
				byte* ptr3 = &other.buffer.offset0000.byte0000;
				byte* ptr4 = ptr2 + FixedList.PaddingBytes<T>();
				byte* ptr5 = ptr3 + FixedList.PaddingBytes<T>();
				int num = math.min(this.Length, other.Length);
				for (int i = 0; i < num; i++)
				{
					int num2 = UnsafeUtility.MemCmp((void*)(ptr4 + sizeof(T) * i), (void*)(ptr5 + sizeof(T) * i), (long)sizeof(T));
					if (num2 != 0)
					{
						return num2;
					}
				}
				return this.Length.CompareTo(other.Length);
			}
		}

		// Token: 0x06000275 RID: 629 RVA: 0x00006E54 File Offset: 0x00005054
		public bool Equals(FixedList32Bytes<T> other)
		{
			return this.CompareTo(other) == 0;
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00006E60 File Offset: 0x00005060
		public FixedList128Bytes(in FixedList32Bytes<T> other)
		{
			this = default(FixedList128Bytes<T>);
			this.Initialize(in other);
		}

		// Token: 0x06000277 RID: 631 RVA: 0x00006E74 File Offset: 0x00005074
		internal unsafe int Initialize(in FixedList32Bytes<T> other)
		{
			FixedList32Bytes<T> fixedList32Bytes = other;
			if (fixedList32Bytes.Length > this.Capacity)
			{
				return 1;
			}
			this.length = other.length;
			this.buffer = default(FixedBytes126);
			void* ptr = (void*)this.Buffer;
			fixedList32Bytes = other;
			UnsafeUtility.MemCpy(ptr, (void*)fixedList32Bytes.Buffer, (long)this.LengthInBytes);
			return 0;
		}

		// Token: 0x06000278 RID: 632 RVA: 0x00006ED2 File Offset: 0x000050D2
		public static implicit operator FixedList128Bytes<T>(in FixedList32Bytes<T> other)
		{
			return new FixedList128Bytes<T>(in other);
		}

		// Token: 0x06000279 RID: 633 RVA: 0x00006EDC File Offset: 0x000050DC
		public unsafe static bool operator ==(in FixedList128Bytes<T> a, in FixedList64Bytes<T> b)
		{
			if (a.length != b.length)
			{
				return false;
			}
			FixedList128Bytes<T> fixedList128Bytes = a;
			void* ptr = (void*)fixedList128Bytes.Buffer;
			FixedList64Bytes<T> fixedList64Bytes = b;
			void* ptr2 = (void*)fixedList64Bytes.Buffer;
			fixedList128Bytes = a;
			return UnsafeUtility.MemCmp(ptr, ptr2, (long)fixedList128Bytes.LengthInBytes) == 0;
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00006F2C File Offset: 0x0000512C
		public static bool operator !=(in FixedList128Bytes<T> a, in FixedList64Bytes<T> b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00006F38 File Offset: 0x00005138
		public unsafe int CompareTo(FixedList64Bytes<T> other)
		{
			fixed (byte* ptr = &this.buffer.offset0000.byte0000)
			{
				byte* ptr2 = ptr;
				byte* ptr3 = &other.buffer.offset0000.byte0000;
				byte* ptr4 = ptr2 + FixedList.PaddingBytes<T>();
				byte* ptr5 = ptr3 + FixedList.PaddingBytes<T>();
				int num = math.min(this.Length, other.Length);
				for (int i = 0; i < num; i++)
				{
					int num2 = UnsafeUtility.MemCmp((void*)(ptr4 + sizeof(T) * i), (void*)(ptr5 + sizeof(T) * i), (long)sizeof(T));
					if (num2 != 0)
					{
						return num2;
					}
				}
				return this.Length.CompareTo(other.Length);
			}
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00006FE0 File Offset: 0x000051E0
		public bool Equals(FixedList64Bytes<T> other)
		{
			return this.CompareTo(other) == 0;
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00006FEC File Offset: 0x000051EC
		public FixedList128Bytes(in FixedList64Bytes<T> other)
		{
			this = default(FixedList128Bytes<T>);
			this.Initialize(in other);
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00007000 File Offset: 0x00005200
		internal unsafe int Initialize(in FixedList64Bytes<T> other)
		{
			FixedList64Bytes<T> fixedList64Bytes = other;
			if (fixedList64Bytes.Length > this.Capacity)
			{
				return 1;
			}
			this.length = other.length;
			this.buffer = default(FixedBytes126);
			void* ptr = (void*)this.Buffer;
			fixedList64Bytes = other;
			UnsafeUtility.MemCpy(ptr, (void*)fixedList64Bytes.Buffer, (long)this.LengthInBytes);
			return 0;
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000705E File Offset: 0x0000525E
		public static implicit operator FixedList128Bytes<T>(in FixedList64Bytes<T> other)
		{
			return new FixedList128Bytes<T>(in other);
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00007068 File Offset: 0x00005268
		public unsafe static bool operator ==(in FixedList128Bytes<T> a, in FixedList128Bytes<T> b)
		{
			if (a.length != b.length)
			{
				return false;
			}
			FixedList128Bytes<T> fixedList128Bytes = a;
			void* ptr = (void*)fixedList128Bytes.Buffer;
			fixedList128Bytes = b;
			void* ptr2 = (void*)fixedList128Bytes.Buffer;
			fixedList128Bytes = a;
			return UnsafeUtility.MemCmp(ptr, ptr2, (long)fixedList128Bytes.LengthInBytes) == 0;
		}

		// Token: 0x06000281 RID: 641 RVA: 0x000070B8 File Offset: 0x000052B8
		public static bool operator !=(in FixedList128Bytes<T> a, in FixedList128Bytes<T> b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x06000282 RID: 642 RVA: 0x000070C4 File Offset: 0x000052C4
		public unsafe int CompareTo(FixedList128Bytes<T> other)
		{
			fixed (byte* ptr = &this.buffer.offset0000.byte0000)
			{
				byte* ptr2 = ptr;
				byte* ptr3 = &other.buffer.offset0000.byte0000;
				byte* ptr4 = ptr2 + FixedList.PaddingBytes<T>();
				byte* ptr5 = ptr3 + FixedList.PaddingBytes<T>();
				int num = math.min(this.Length, other.Length);
				for (int i = 0; i < num; i++)
				{
					int num2 = UnsafeUtility.MemCmp((void*)(ptr4 + sizeof(T) * i), (void*)(ptr5 + sizeof(T) * i), (long)sizeof(T));
					if (num2 != 0)
					{
						return num2;
					}
				}
				return this.Length.CompareTo(other.Length);
			}
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000716C File Offset: 0x0000536C
		public bool Equals(FixedList128Bytes<T> other)
		{
			return this.CompareTo(other) == 0;
		}

		// Token: 0x06000284 RID: 644 RVA: 0x00007178 File Offset: 0x00005378
		public unsafe static bool operator ==(in FixedList128Bytes<T> a, in FixedList512Bytes<T> b)
		{
			if (a.length != b.length)
			{
				return false;
			}
			FixedList128Bytes<T> fixedList128Bytes = a;
			void* ptr = (void*)fixedList128Bytes.Buffer;
			FixedList512Bytes<T> fixedList512Bytes = b;
			void* ptr2 = (void*)fixedList512Bytes.Buffer;
			fixedList128Bytes = a;
			return UnsafeUtility.MemCmp(ptr, ptr2, (long)fixedList128Bytes.LengthInBytes) == 0;
		}

		// Token: 0x06000285 RID: 645 RVA: 0x000071C8 File Offset: 0x000053C8
		public static bool operator !=(in FixedList128Bytes<T> a, in FixedList512Bytes<T> b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x06000286 RID: 646 RVA: 0x000071D4 File Offset: 0x000053D4
		public unsafe int CompareTo(FixedList512Bytes<T> other)
		{
			fixed (byte* ptr = &this.buffer.offset0000.byte0000)
			{
				byte* ptr2 = ptr;
				byte* ptr3 = &other.buffer.offset0000.byte0000;
				byte* ptr4 = ptr2 + FixedList.PaddingBytes<T>();
				byte* ptr5 = ptr3 + FixedList.PaddingBytes<T>();
				int num = math.min(this.Length, other.Length);
				for (int i = 0; i < num; i++)
				{
					int num2 = UnsafeUtility.MemCmp((void*)(ptr4 + sizeof(T) * i), (void*)(ptr5 + sizeof(T) * i), (long)sizeof(T));
					if (num2 != 0)
					{
						return num2;
					}
				}
				return this.Length.CompareTo(other.Length);
			}
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000727C File Offset: 0x0000547C
		public bool Equals(FixedList512Bytes<T> other)
		{
			return this.CompareTo(other) == 0;
		}

		// Token: 0x06000288 RID: 648 RVA: 0x00007288 File Offset: 0x00005488
		public FixedList128Bytes(in FixedList512Bytes<T> other)
		{
			this = default(FixedList128Bytes<T>);
			this.Initialize(in other);
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000729C File Offset: 0x0000549C
		internal unsafe int Initialize(in FixedList512Bytes<T> other)
		{
			FixedList512Bytes<T> fixedList512Bytes = other;
			if (fixedList512Bytes.Length > this.Capacity)
			{
				return 1;
			}
			this.length = other.length;
			this.buffer = default(FixedBytes126);
			void* ptr = (void*)this.Buffer;
			fixedList512Bytes = other;
			UnsafeUtility.MemCpy(ptr, (void*)fixedList512Bytes.Buffer, (long)this.LengthInBytes);
			return 0;
		}

		// Token: 0x0600028A RID: 650 RVA: 0x000072FA File Offset: 0x000054FA
		public static implicit operator FixedList128Bytes<T>(in FixedList512Bytes<T> other)
		{
			return new FixedList128Bytes<T>(in other);
		}

		// Token: 0x0600028B RID: 651 RVA: 0x00007304 File Offset: 0x00005504
		public unsafe static bool operator ==(in FixedList128Bytes<T> a, in FixedList4096Bytes<T> b)
		{
			if (a.length != b.length)
			{
				return false;
			}
			FixedList128Bytes<T> fixedList128Bytes = a;
			void* ptr = (void*)fixedList128Bytes.Buffer;
			FixedList4096Bytes<T> fixedList4096Bytes = b;
			void* ptr2 = (void*)fixedList4096Bytes.Buffer;
			fixedList128Bytes = a;
			return UnsafeUtility.MemCmp(ptr, ptr2, (long)fixedList128Bytes.LengthInBytes) == 0;
		}

		// Token: 0x0600028C RID: 652 RVA: 0x00007354 File Offset: 0x00005554
		public static bool operator !=(in FixedList128Bytes<T> a, in FixedList4096Bytes<T> b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00007360 File Offset: 0x00005560
		public unsafe int CompareTo(FixedList4096Bytes<T> other)
		{
			fixed (byte* ptr = &this.buffer.offset0000.byte0000)
			{
				byte* ptr2 = ptr;
				byte* ptr3 = &other.buffer.offset0000.byte0000;
				byte* ptr4 = ptr2 + FixedList.PaddingBytes<T>();
				byte* ptr5 = ptr3 + FixedList.PaddingBytes<T>();
				int num = math.min(this.Length, other.Length);
				for (int i = 0; i < num; i++)
				{
					int num2 = UnsafeUtility.MemCmp((void*)(ptr4 + sizeof(T) * i), (void*)(ptr5 + sizeof(T) * i), (long)sizeof(T));
					if (num2 != 0)
					{
						return num2;
					}
				}
				return this.Length.CompareTo(other.Length);
			}
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00007408 File Offset: 0x00005608
		public bool Equals(FixedList4096Bytes<T> other)
		{
			return this.CompareTo(other) == 0;
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00007414 File Offset: 0x00005614
		public FixedList128Bytes(in FixedList4096Bytes<T> other)
		{
			this = default(FixedList128Bytes<T>);
			this.Initialize(in other);
		}

		// Token: 0x06000290 RID: 656 RVA: 0x00007428 File Offset: 0x00005628
		internal unsafe int Initialize(in FixedList4096Bytes<T> other)
		{
			FixedList4096Bytes<T> fixedList4096Bytes = other;
			if (fixedList4096Bytes.Length > this.Capacity)
			{
				return 1;
			}
			this.length = other.length;
			this.buffer = default(FixedBytes126);
			void* ptr = (void*)this.Buffer;
			fixedList4096Bytes = other;
			UnsafeUtility.MemCpy(ptr, (void*)fixedList4096Bytes.Buffer, (long)this.LengthInBytes);
			return 0;
		}

		// Token: 0x06000291 RID: 657 RVA: 0x00007486 File Offset: 0x00005686
		public static implicit operator FixedList128Bytes<T>(in FixedList4096Bytes<T> other)
		{
			return new FixedList128Bytes<T>(in other);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x00007490 File Offset: 0x00005690
		[NotBurstCompatible]
		public override bool Equals(object obj)
		{
			if (obj is FixedList32Bytes<T>)
			{
				FixedList32Bytes<T> fixedList32Bytes = (FixedList32Bytes<T>)obj;
				return this.Equals(fixedList32Bytes);
			}
			if (obj is FixedList64Bytes<T>)
			{
				FixedList64Bytes<T> fixedList64Bytes = (FixedList64Bytes<T>)obj;
				return this.Equals(fixedList64Bytes);
			}
			if (obj is FixedList128Bytes<T>)
			{
				FixedList128Bytes<T> fixedList128Bytes = (FixedList128Bytes<T>)obj;
				return this.Equals(fixedList128Bytes);
			}
			if (obj is FixedList512Bytes<T>)
			{
				FixedList512Bytes<T> fixedList512Bytes = (FixedList512Bytes<T>)obj;
				return this.Equals(fixedList512Bytes);
			}
			if (obj is FixedList4096Bytes<T>)
			{
				FixedList4096Bytes<T> fixedList4096Bytes = (FixedList4096Bytes<T>)obj;
				return this.Equals(fixedList4096Bytes);
			}
			return false;
		}

		// Token: 0x06000293 RID: 659 RVA: 0x00007513 File Offset: 0x00005713
		public FixedList128Bytes<T>.Enumerator GetEnumerator()
		{
			return new FixedList128Bytes<T>.Enumerator(ref this);
		}

		// Token: 0x06000294 RID: 660 RVA: 0x00004A65 File Offset: 0x00002C65
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000295 RID: 661 RVA: 0x00004A65 File Offset: 0x00002C65
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x040000BB RID: 187
		[SerializeField]
		internal ushort length;

		// Token: 0x040000BC RID: 188
		[SerializeField]
		internal FixedBytes126 buffer;

		// Token: 0x02000060 RID: 96
		public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
		{
			// Token: 0x06000296 RID: 662 RVA: 0x0000751B File Offset: 0x0000571B
			public Enumerator(ref FixedList128Bytes<T> list)
			{
				this.m_List = list;
				this.m_Index = -1;
			}

			// Token: 0x06000297 RID: 663 RVA: 0x00002C2B File Offset: 0x00000E2B
			public void Dispose()
			{
			}

			// Token: 0x06000298 RID: 664 RVA: 0x00007530 File Offset: 0x00005730
			public bool MoveNext()
			{
				this.m_Index++;
				return this.m_Index < this.m_List.Length;
			}

			// Token: 0x06000299 RID: 665 RVA: 0x00007553 File Offset: 0x00005753
			public void Reset()
			{
				this.m_Index = -1;
			}

			// Token: 0x1700005A RID: 90
			// (get) Token: 0x0600029A RID: 666 RVA: 0x0000755C File Offset: 0x0000575C
			public T Current
			{
				get
				{
					return this.m_List[this.m_Index];
				}
			}

			// Token: 0x1700005B RID: 91
			// (get) Token: 0x0600029B RID: 667 RVA: 0x0000756F File Offset: 0x0000576F
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x040000BD RID: 189
			private FixedList128Bytes<T> m_List;

			// Token: 0x040000BE RID: 190
			private int m_Index;
		}
	}
}
