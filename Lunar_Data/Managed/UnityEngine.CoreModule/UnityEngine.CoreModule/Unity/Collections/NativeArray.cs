using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Unity.Burst;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using UnityEngine.Internal;

namespace Unity.Collections
{
	// Token: 0x02000091 RID: 145
	[DebuggerTypeProxy(typeof(NativeArrayDebugView<>))]
	[DebuggerDisplay("Length = {Length}")]
	[NativeContainerSupportsDeallocateOnJobCompletion]
	[NativeContainerSupportsMinMaxWriteRestriction]
	[NativeContainer]
	[NativeContainerSupportsDeferredConvertListToArray]
	public struct NativeArray<T> : IDisposable, IEnumerable<T>, IEnumerable, IEquatable<NativeArray<T>> where T : struct
	{
		// Token: 0x06000257 RID: 599 RVA: 0x000043F8 File Offset: 0x000025F8
		public NativeArray(int length, Allocator allocator, NativeArrayOptions options = NativeArrayOptions.ClearMemory)
		{
			NativeArray<T>.Allocate(length, allocator, out this);
			bool flag = (options & NativeArrayOptions.ClearMemory) == NativeArrayOptions.ClearMemory;
			if (flag)
			{
				UnsafeUtility.MemClear(this.m_Buffer, (long)this.Length * (long)UnsafeUtility.SizeOf<T>());
			}
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00004433 File Offset: 0x00002633
		public NativeArray(T[] array, Allocator allocator)
		{
			NativeArray<T>.Allocate(array.Length, allocator, out this);
			NativeArray<T>.Copy(array, this);
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000444E File Offset: 0x0000264E
		public NativeArray(NativeArray<T> array, Allocator allocator)
		{
			NativeArray<T>.Allocate(array.Length, allocator, out this);
			NativeArray<T>.Copy(array, 0, this, 0, array.Length);
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00004478 File Offset: 0x00002678
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckAllocateArguments(int length, Allocator allocator, long totalSize)
		{
			bool flag = allocator <= Allocator.None;
			if (flag)
			{
				throw new ArgumentException("Allocator must be Temp, TempJob or Persistent", "allocator");
			}
			bool flag2 = length < 0;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("length", "Length must be >= 0");
			}
			NativeArray<T>.IsUnmanagedAndThrow();
		}

		// Token: 0x0600025B RID: 603 RVA: 0x000044C0 File Offset: 0x000026C0
		private static void Allocate(int length, Allocator allocator, out NativeArray<T> array)
		{
			long num = (long)UnsafeUtility.SizeOf<T>() * (long)length;
			array = default(NativeArray<T>);
			array.m_Buffer = UnsafeUtility.Malloc(num, UnsafeUtility.AlignOf<T>(), allocator);
			array.m_Length = length;
			array.m_AllocatorLabel = allocator;
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600025C RID: 604 RVA: 0x00004500 File Offset: 0x00002700
		public int Length
		{
			get
			{
				return this.m_Length;
			}
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00004518 File Offset: 0x00002718
		[BurstDiscard]
		internal static void IsUnmanagedAndThrow()
		{
			bool flag = !UnsafeUtility.IsValidNativeContainerElementType<T>();
			if (flag)
			{
				throw new InvalidOperationException(string.Format("{0} used in NativeArray<{1}> must be unmanaged (contain no managed types) and cannot itself be a native container type.", typeof(T), typeof(T)));
			}
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00004557 File Offset: 0x00002757
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckElementReadAccess(int index)
		{
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00004557 File Offset: 0x00002757
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckElementWriteAccess(int index)
		{
		}

		// Token: 0x1700006D RID: 109
		public T this[int index]
		{
			get
			{
				return UnsafeUtility.ReadArrayElement<T>(this.m_Buffer, index);
			}
			[WriteAccessRequired]
			set
			{
				UnsafeUtility.WriteArrayElement<T>(this.m_Buffer, index, value);
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000262 RID: 610 RVA: 0x0000458B File Offset: 0x0000278B
		public bool IsCreated
		{
			get
			{
				return this.m_Buffer != null;
			}
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000459C File Offset: 0x0000279C
		[WriteAccessRequired]
		public void Dispose()
		{
			bool flag = this.m_Buffer == null;
			if (flag)
			{
				throw new ObjectDisposedException("The NativeArray is already disposed.");
			}
			bool flag2 = this.m_AllocatorLabel == Allocator.Invalid;
			if (flag2)
			{
				throw new InvalidOperationException("The NativeArray can not be Disposed because it was not allocated with a valid allocator.");
			}
			bool flag3 = this.m_AllocatorLabel > Allocator.None;
			if (flag3)
			{
				UnsafeUtility.Free(this.m_Buffer, this.m_AllocatorLabel);
				this.m_AllocatorLabel = Allocator.Invalid;
			}
			this.m_Buffer = null;
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00004610 File Offset: 0x00002810
		public JobHandle Dispose(JobHandle inputDeps)
		{
			bool flag = this.m_AllocatorLabel == Allocator.Invalid;
			if (flag)
			{
				throw new InvalidOperationException("The NativeArray can not be Disposed because it was not allocated with a valid allocator.");
			}
			bool flag2 = this.m_Buffer == null;
			if (flag2)
			{
				throw new InvalidOperationException("The NativeArray is already disposed.");
			}
			bool flag3 = this.m_AllocatorLabel > Allocator.None;
			JobHandle jobHandle2;
			if (flag3)
			{
				JobHandle jobHandle = new NativeArrayDisposeJob
				{
					Data = new NativeArrayDispose
					{
						m_Buffer = this.m_Buffer,
						m_AllocatorLabel = this.m_AllocatorLabel
					}
				}.Schedule(inputDeps);
				this.m_Buffer = null;
				this.m_AllocatorLabel = Allocator.Invalid;
				jobHandle2 = jobHandle;
			}
			else
			{
				this.m_Buffer = null;
				jobHandle2 = inputDeps;
			}
			return jobHandle2;
		}

		// Token: 0x06000265 RID: 613 RVA: 0x000046BE File Offset: 0x000028BE
		[WriteAccessRequired]
		public void CopyFrom(T[] array)
		{
			NativeArray<T>.Copy(array, this);
		}

		// Token: 0x06000266 RID: 614 RVA: 0x000046CE File Offset: 0x000028CE
		[WriteAccessRequired]
		public void CopyFrom(NativeArray<T> array)
		{
			NativeArray<T>.Copy(array, this);
		}

		// Token: 0x06000267 RID: 615 RVA: 0x000046DE File Offset: 0x000028DE
		public void CopyTo(T[] array)
		{
			NativeArray<T>.Copy(this, array);
		}

		// Token: 0x06000268 RID: 616 RVA: 0x000046EE File Offset: 0x000028EE
		public void CopyTo(NativeArray<T> array)
		{
			NativeArray<T>.Copy(this, array);
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00004700 File Offset: 0x00002900
		public T[] ToArray()
		{
			T[] array = new T[this.Length];
			NativeArray<T>.Copy(this, array, this.Length);
			return array;
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00004734 File Offset: 0x00002934
		public NativeArray<T>.Enumerator GetEnumerator()
		{
			return new NativeArray<T>.Enumerator(ref this);
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000474C File Offset: 0x0000294C
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return new NativeArray<T>.Enumerator(ref this);
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000476C File Offset: 0x0000296C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000478C File Offset: 0x0000298C
		public bool Equals(NativeArray<T> other)
		{
			return this.m_Buffer == other.m_Buffer && this.m_Length == other.m_Length;
		}

		// Token: 0x0600026E RID: 622 RVA: 0x000047C0 File Offset: 0x000029C0
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is NativeArray<T> && this.Equals((NativeArray<T>)obj);
		}

		// Token: 0x0600026F RID: 623 RVA: 0x000047F8 File Offset: 0x000029F8
		public override int GetHashCode()
		{
			return (this.m_Buffer * 397) ^ this.m_Length;
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00004820 File Offset: 0x00002A20
		public static bool operator ==(NativeArray<T> left, NativeArray<T> right)
		{
			return left.Equals(right);
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000483C File Offset: 0x00002A3C
		public static bool operator !=(NativeArray<T> left, NativeArray<T> right)
		{
			return !left.Equals(right);
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000485C File Offset: 0x00002A5C
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckCopyLengths(int srcLength, int dstLength)
		{
			bool flag = srcLength != dstLength;
			if (flag)
			{
				throw new ArgumentException("source and destination length must be the same");
			}
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00004880 File Offset: 0x00002A80
		public static void Copy(NativeArray<T> src, NativeArray<T> dst)
		{
			NativeArray<T>.Copy(src, 0, dst, 0, src.Length);
		}

		// Token: 0x06000274 RID: 628 RVA: 0x00004894 File Offset: 0x00002A94
		public static void Copy(NativeArray<T>.ReadOnly src, NativeArray<T> dst)
		{
			NativeArray<T>.Copy(src, 0, dst, 0, src.Length);
		}

		// Token: 0x06000275 RID: 629 RVA: 0x000048A8 File Offset: 0x00002AA8
		public static void Copy(T[] src, NativeArray<T> dst)
		{
			NativeArray<T>.Copy(src, 0, dst, 0, src.Length);
		}

		// Token: 0x06000276 RID: 630 RVA: 0x000048B8 File Offset: 0x00002AB8
		public static void Copy(NativeArray<T> src, T[] dst)
		{
			NativeArray<T>.Copy(src, 0, dst, 0, src.Length);
		}

		// Token: 0x06000277 RID: 631 RVA: 0x000048CC File Offset: 0x00002ACC
		public static void Copy(NativeArray<T>.ReadOnly src, T[] dst)
		{
			NativeArray<T>.Copy(src, 0, dst, 0, src.Length);
		}

		// Token: 0x06000278 RID: 632 RVA: 0x000048E0 File Offset: 0x00002AE0
		public static void Copy(NativeArray<T> src, NativeArray<T> dst, int length)
		{
			NativeArray<T>.Copy(src, 0, dst, 0, length);
		}

		// Token: 0x06000279 RID: 633 RVA: 0x000048EE File Offset: 0x00002AEE
		public static void Copy(NativeArray<T>.ReadOnly src, NativeArray<T> dst, int length)
		{
			NativeArray<T>.Copy(src, 0, dst, 0, length);
		}

		// Token: 0x0600027A RID: 634 RVA: 0x000048FC File Offset: 0x00002AFC
		public static void Copy(T[] src, NativeArray<T> dst, int length)
		{
			NativeArray<T>.Copy(src, 0, dst, 0, length);
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000490A File Offset: 0x00002B0A
		public static void Copy(NativeArray<T> src, T[] dst, int length)
		{
			NativeArray<T>.Copy(src, 0, dst, 0, length);
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00004918 File Offset: 0x00002B18
		public static void Copy(NativeArray<T>.ReadOnly src, T[] dst, int length)
		{
			NativeArray<T>.Copy(src, 0, dst, 0, length);
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00004928 File Offset: 0x00002B28
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckCopyArguments(int srcLength, int srcIndex, int dstLength, int dstIndex, int length)
		{
			bool flag = length < 0;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("length", "length must be equal or greater than zero.");
			}
			bool flag2 = srcIndex < 0 || srcIndex > srcLength || (srcIndex == srcLength && srcLength > 0);
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("srcIndex", "srcIndex is outside the range of valid indexes for the source NativeArray.");
			}
			bool flag3 = dstIndex < 0 || dstIndex > dstLength || (dstIndex == dstLength && dstLength > 0);
			if (flag3)
			{
				throw new ArgumentOutOfRangeException("dstIndex", "dstIndex is outside the range of valid indexes for the destination NativeArray.");
			}
			bool flag4 = srcIndex + length > srcLength;
			if (flag4)
			{
				throw new ArgumentException("length is greater than the number of elements from srcIndex to the end of the source NativeArray.", "length");
			}
			bool flag5 = srcIndex + length < 0;
			if (flag5)
			{
				throw new ArgumentException("srcIndex + length causes an integer overflow");
			}
			bool flag6 = dstIndex + length > dstLength;
			if (flag6)
			{
				throw new ArgumentException("length is greater than the number of elements from dstIndex to the end of the destination NativeArray.", "length");
			}
			bool flag7 = dstIndex + length < 0;
			if (flag7)
			{
				throw new ArgumentException("dstIndex + length causes an integer overflow");
			}
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00004A0B File Offset: 0x00002C0B
		public unsafe static void Copy(NativeArray<T> src, int srcIndex, NativeArray<T> dst, int dstIndex, int length)
		{
			UnsafeUtility.MemCpy((void*)((byte*)dst.m_Buffer + dstIndex * UnsafeUtility.SizeOf<T>()), (void*)((byte*)src.m_Buffer + srcIndex * UnsafeUtility.SizeOf<T>()), (long)(length * UnsafeUtility.SizeOf<T>()));
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00004A39 File Offset: 0x00002C39
		public unsafe static void Copy(NativeArray<T>.ReadOnly src, int srcIndex, NativeArray<T> dst, int dstIndex, int length)
		{
			UnsafeUtility.MemCpy((void*)((byte*)dst.m_Buffer + dstIndex * UnsafeUtility.SizeOf<T>()), (void*)((byte*)src.m_Buffer + srcIndex * UnsafeUtility.SizeOf<T>()), (long)(length * UnsafeUtility.SizeOf<T>()));
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00004A68 File Offset: 0x00002C68
		public unsafe static void Copy(T[] src, int srcIndex, NativeArray<T> dst, int dstIndex, int length)
		{
			GCHandle gchandle = GCHandle.Alloc(src, 3);
			IntPtr intPtr = gchandle.AddrOfPinnedObject();
			UnsafeUtility.MemCpy((void*)((byte*)dst.m_Buffer + dstIndex * UnsafeUtility.SizeOf<T>()), (void*)((byte*)(void*)intPtr + srcIndex * UnsafeUtility.SizeOf<T>()), (long)(length * UnsafeUtility.SizeOf<T>()));
			gchandle.Free();
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00004ABC File Offset: 0x00002CBC
		public unsafe static void Copy(NativeArray<T> src, int srcIndex, T[] dst, int dstIndex, int length)
		{
			GCHandle gchandle = GCHandle.Alloc(dst, 3);
			IntPtr intPtr = gchandle.AddrOfPinnedObject();
			UnsafeUtility.MemCpy((void*)((byte*)(void*)intPtr + dstIndex * UnsafeUtility.SizeOf<T>()), (void*)((byte*)src.m_Buffer + srcIndex * UnsafeUtility.SizeOf<T>()), (long)(length * UnsafeUtility.SizeOf<T>()));
			gchandle.Free();
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00004B10 File Offset: 0x00002D10
		public unsafe static void Copy(NativeArray<T>.ReadOnly src, int srcIndex, T[] dst, int dstIndex, int length)
		{
			GCHandle gchandle = GCHandle.Alloc(dst, 3);
			IntPtr intPtr = gchandle.AddrOfPinnedObject();
			UnsafeUtility.MemCpy((void*)((byte*)(void*)intPtr + dstIndex * UnsafeUtility.SizeOf<T>()), (void*)((byte*)src.m_Buffer + srcIndex * UnsafeUtility.SizeOf<T>()), (long)(length * UnsafeUtility.SizeOf<T>()));
			gchandle.Free();
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00004557 File Offset: 0x00002757
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckReinterpretLoadRange<U>(int sourceIndex) where U : struct
		{
		}

		// Token: 0x06000284 RID: 644 RVA: 0x00004557 File Offset: 0x00002757
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckReinterpretStoreRange<U>(int destIndex) where U : struct
		{
		}

		// Token: 0x06000285 RID: 645 RVA: 0x00004B64 File Offset: 0x00002D64
		public unsafe U ReinterpretLoad<U>(int sourceIndex) where U : struct
		{
			byte* ptr = (byte*)this.m_Buffer + (long)UnsafeUtility.SizeOf<T>() * (long)sourceIndex;
			return UnsafeUtility.ReadArrayElement<U>((void*)ptr, 0);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00004B90 File Offset: 0x00002D90
		public unsafe void ReinterpretStore<U>(int destIndex, U data) where U : struct
		{
			byte* ptr = (byte*)this.m_Buffer + (long)UnsafeUtility.SizeOf<T>() * (long)destIndex;
			UnsafeUtility.WriteArrayElement<U>((void*)ptr, 0, data);
		}

		// Token: 0x06000287 RID: 647 RVA: 0x00004BBC File Offset: 0x00002DBC
		private NativeArray<U> InternalReinterpret<U>(int length) where U : struct
		{
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<U>(this.m_Buffer, length, this.m_AllocatorLabel);
		}

		// Token: 0x06000288 RID: 648 RVA: 0x00004BE4 File Offset: 0x00002DE4
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckReinterpretSize<U>() where U : struct
		{
			bool flag = UnsafeUtility.SizeOf<T>() != UnsafeUtility.SizeOf<U>();
			if (flag)
			{
				throw new InvalidOperationException(string.Format("Types {0} and {1} are different sizes - direct reinterpretation is not possible. If this is what you intended, use Reinterpret(<type size>)", typeof(T), typeof(U)));
			}
		}

		// Token: 0x06000289 RID: 649 RVA: 0x00004C2C File Offset: 0x00002E2C
		public NativeArray<U> Reinterpret<U>() where U : struct
		{
			return this.InternalReinterpret<U>(this.Length);
		}

		// Token: 0x0600028A RID: 650 RVA: 0x00004C4C File Offset: 0x00002E4C
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckReinterpretSize<U>(long tSize, long uSize, int expectedTypeSize, long byteLen, long uLen)
		{
			bool flag = tSize != (long)expectedTypeSize;
			if (flag)
			{
				throw new InvalidOperationException(string.Format("Type {0} was expected to be {1} but is {2} bytes", typeof(T), expectedTypeSize, tSize));
			}
			bool flag2 = uLen * uSize != byteLen;
			if (flag2)
			{
				throw new InvalidOperationException(string.Format("Types {0} (array length {1}) and {2} cannot be aliased due to size constraints. The size of the types and lengths involved must line up.", typeof(T), this.Length, typeof(U)));
			}
		}

		// Token: 0x0600028B RID: 651 RVA: 0x00004CCC File Offset: 0x00002ECC
		public NativeArray<U> Reinterpret<U>(int expectedTypeSize) where U : struct
		{
			long num = (long)UnsafeUtility.SizeOf<T>();
			long num2 = (long)UnsafeUtility.SizeOf<U>();
			long num3 = (long)this.Length * num;
			long num4 = num3 / num2;
			return this.InternalReinterpret<U>((int)num4);
		}

		// Token: 0x0600028C RID: 652 RVA: 0x00004D04 File Offset: 0x00002F04
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckGetSubArrayArguments(int start, int length)
		{
			bool flag = start < 0;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("start", "start must be >= 0");
			}
			bool flag2 = start + length > this.Length;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("length", string.Format("sub array range {0}-{1} is outside the range of the native array 0-{2}", start, start + length - 1, this.Length - 1));
			}
			bool flag3 = start + length < 0;
			if (flag3)
			{
				throw new ArgumentException(string.Format("sub array range {0}-{1} caused an integer overflow and is outside the range of the native array 0-{2}", start, start + length - 1, this.Length - 1));
			}
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00004DA8 File Offset: 0x00002FA8
		public unsafe NativeArray<T> GetSubArray(int start, int length)
		{
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>((void*)((byte*)this.m_Buffer + (long)UnsafeUtility.SizeOf<T>() * (long)start), length, Allocator.None);
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00004DD4 File Offset: 0x00002FD4
		public NativeArray<T>.ReadOnly AsReadOnly()
		{
			return new NativeArray<T>.ReadOnly(this.m_Buffer, this.m_Length);
		}

		// Token: 0x04000224 RID: 548
		[NativeDisableUnsafePtrRestriction]
		internal unsafe void* m_Buffer;

		// Token: 0x04000225 RID: 549
		internal int m_Length;

		// Token: 0x04000226 RID: 550
		internal Allocator m_AllocatorLabel;

		// Token: 0x02000092 RID: 146
		[ExcludeFromDocs]
		public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
		{
			// Token: 0x0600028F RID: 655 RVA: 0x00004DF7 File Offset: 0x00002FF7
			public Enumerator(ref NativeArray<T> array)
			{
				this.m_Array = array;
				this.m_Index = -1;
			}

			// Token: 0x06000290 RID: 656 RVA: 0x00004557 File Offset: 0x00002757
			public void Dispose()
			{
			}

			// Token: 0x06000291 RID: 657 RVA: 0x00004E10 File Offset: 0x00003010
			public bool MoveNext()
			{
				this.m_Index++;
				return this.m_Index < this.m_Array.Length;
			}

			// Token: 0x06000292 RID: 658 RVA: 0x00004E43 File Offset: 0x00003043
			public void Reset()
			{
				this.m_Index = -1;
			}

			// Token: 0x1700006F RID: 111
			// (get) Token: 0x06000293 RID: 659 RVA: 0x00004E4D File Offset: 0x0000304D
			public T Current
			{
				get
				{
					return this.m_Array[this.m_Index];
				}
			}

			// Token: 0x17000070 RID: 112
			// (get) Token: 0x06000294 RID: 660 RVA: 0x00004E60 File Offset: 0x00003060
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x04000227 RID: 551
			private NativeArray<T> m_Array;

			// Token: 0x04000228 RID: 552
			private int m_Index;
		}

		// Token: 0x02000093 RID: 147
		[DebuggerDisplay("Length = {Length}")]
		[NativeContainerIsReadOnly]
		[NativeContainer]
		[DebuggerTypeProxy(typeof(NativeArrayReadOnlyDebugView<>))]
		public struct ReadOnly : IEnumerable<T>, IEnumerable
		{
			// Token: 0x06000295 RID: 661 RVA: 0x00004E6D File Offset: 0x0000306D
			internal unsafe ReadOnly(void* buffer, int length)
			{
				this.m_Buffer = buffer;
				this.m_Length = length;
			}

			// Token: 0x17000071 RID: 113
			// (get) Token: 0x06000296 RID: 662 RVA: 0x00004E80 File Offset: 0x00003080
			public int Length
			{
				get
				{
					return this.m_Length;
				}
			}

			// Token: 0x06000297 RID: 663 RVA: 0x00004E98 File Offset: 0x00003098
			public void CopyTo(T[] array)
			{
				NativeArray<T>.Copy(this, array);
			}

			// Token: 0x06000298 RID: 664 RVA: 0x00004EA7 File Offset: 0x000030A7
			public void CopyTo(NativeArray<T> array)
			{
				NativeArray<T>.Copy(this, array);
			}

			// Token: 0x06000299 RID: 665 RVA: 0x00004EB8 File Offset: 0x000030B8
			public T[] ToArray()
			{
				T[] array = new T[this.m_Length];
				NativeArray<T>.Copy(this, array, this.m_Length);
				return array;
			}

			// Token: 0x0600029A RID: 666 RVA: 0x00004EEC File Offset: 0x000030EC
			public NativeArray<U>.ReadOnly Reinterpret<U>() where U : struct
			{
				return new NativeArray<U>.ReadOnly(this.m_Buffer, this.m_Length);
			}

			// Token: 0x17000072 RID: 114
			public T this[int index]
			{
				get
				{
					return UnsafeUtility.ReadArrayElement<T>(this.m_Buffer, index);
				}
			}

			// Token: 0x0600029C RID: 668 RVA: 0x00004F30 File Offset: 0x00003130
			[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			private void CheckElementReadAccess(int index)
			{
				bool flag = index < 0 || index >= this.m_Length;
				if (flag)
				{
					throw new IndexOutOfRangeException(string.Format("Index {0} is out of range (must be between 0 and {1}).", index, this.m_Length - 1));
				}
			}

			// Token: 0x17000073 RID: 115
			// (get) Token: 0x0600029D RID: 669 RVA: 0x00004F79 File Offset: 0x00003179
			public bool IsCreated
			{
				get
				{
					return this.m_Buffer != null;
				}
			}

			// Token: 0x0600029E RID: 670 RVA: 0x00004F88 File Offset: 0x00003188
			public NativeArray<T>.ReadOnly.Enumerator GetEnumerator()
			{
				return new NativeArray<T>.ReadOnly.Enumerator(in this);
			}

			// Token: 0x0600029F RID: 671 RVA: 0x00004FA0 File Offset: 0x000031A0
			IEnumerator<T> IEnumerable<T>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x060002A0 RID: 672 RVA: 0x00004FC0 File Offset: 0x000031C0
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x04000229 RID: 553
			[NativeDisableUnsafePtrRestriction]
			internal unsafe void* m_Buffer;

			// Token: 0x0400022A RID: 554
			internal int m_Length;

			// Token: 0x02000094 RID: 148
			[ExcludeFromDocs]
			public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
			{
				// Token: 0x060002A1 RID: 673 RVA: 0x00004FDD File Offset: 0x000031DD
				public Enumerator(in NativeArray<T>.ReadOnly array)
				{
					this.m_Array = array;
					this.m_Index = -1;
				}

				// Token: 0x060002A2 RID: 674 RVA: 0x00004557 File Offset: 0x00002757
				public void Dispose()
				{
				}

				// Token: 0x060002A3 RID: 675 RVA: 0x00004FF4 File Offset: 0x000031F4
				public bool MoveNext()
				{
					this.m_Index++;
					return this.m_Index < this.m_Array.Length;
				}

				// Token: 0x060002A4 RID: 676 RVA: 0x00005027 File Offset: 0x00003227
				public void Reset()
				{
					this.m_Index = -1;
				}

				// Token: 0x17000074 RID: 116
				// (get) Token: 0x060002A5 RID: 677 RVA: 0x00005031 File Offset: 0x00003231
				public T Current
				{
					get
					{
						return this.m_Array[this.m_Index];
					}
				}

				// Token: 0x17000075 RID: 117
				// (get) Token: 0x060002A6 RID: 678 RVA: 0x00005044 File Offset: 0x00003244
				object IEnumerator.Current
				{
					get
					{
						return this.Current;
					}
				}

				// Token: 0x0400022B RID: 555
				private NativeArray<T>.ReadOnly m_Array;

				// Token: 0x0400022C RID: 556
				private int m_Index;
			}
		}
	}
}
