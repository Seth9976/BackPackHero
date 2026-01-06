using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

namespace Unity.Collections
{
	// Token: 0x020000AF RID: 175
	[NativeContainer]
	[DebuggerDisplay("Length = {Length}, IsCreated = {IsCreated}")]
	[BurstCompatible]
	public struct NativeBitArray : INativeDisposable, IDisposable
	{
		// Token: 0x060006A5 RID: 1701 RVA: 0x0001588A File Offset: 0x00013A8A
		public NativeBitArray(int numBits, AllocatorManager.AllocatorHandle allocator, NativeArrayOptions options = NativeArrayOptions.ClearMemory)
		{
			this = new NativeBitArray(numBits, allocator, options, 2);
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x00015896 File Offset: 0x00013A96
		private NativeBitArray(int numBits, AllocatorManager.AllocatorHandle allocator, NativeArrayOptions options, int disposeSentinelStackDepth)
		{
			this.m_BitArray = new UnsafeBitArray(numBits, allocator, options);
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060006A7 RID: 1703 RVA: 0x000158A6 File Offset: 0x00013AA6
		public bool IsCreated
		{
			get
			{
				return this.m_BitArray.IsCreated;
			}
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x000158B3 File Offset: 0x00013AB3
		public void Dispose()
		{
			this.m_BitArray.Dispose();
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x000158C0 File Offset: 0x00013AC0
		[NotBurstCompatible]
		public JobHandle Dispose(JobHandle inputDeps)
		{
			return this.m_BitArray.Dispose(inputDeps);
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060006AA RID: 1706 RVA: 0x000158CE File Offset: 0x00013ACE
		public int Length
		{
			get
			{
				return CollectionHelper.AssumePositive(this.m_BitArray.Length);
			}
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x000158E0 File Offset: 0x00013AE0
		public void Clear()
		{
			this.m_BitArray.Clear();
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x000158F0 File Offset: 0x00013AF0
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(int) })]
		public unsafe NativeArray<T> AsNativeArray<[global::System.Runtime.CompilerServices.IsUnmanaged] T>() where T : struct, ValueType
		{
			int num = UnsafeUtility.SizeOf<T>() * 8;
			int num2 = this.m_BitArray.Length / num;
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>((void*)this.m_BitArray.Ptr, num2, Allocator.None);
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x00015925 File Offset: 0x00013B25
		public void Set(int pos, bool value)
		{
			this.m_BitArray.Set(pos, value);
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x00015934 File Offset: 0x00013B34
		public void SetBits(int pos, bool value, int numBits)
		{
			this.m_BitArray.SetBits(pos, value, numBits);
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x00015944 File Offset: 0x00013B44
		public void SetBits(int pos, ulong value, int numBits = 1)
		{
			this.m_BitArray.SetBits(pos, value, numBits);
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x00015954 File Offset: 0x00013B54
		public ulong GetBits(int pos, int numBits = 1)
		{
			return this.m_BitArray.GetBits(pos, numBits);
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x00015963 File Offset: 0x00013B63
		public bool IsSet(int pos)
		{
			return this.m_BitArray.IsSet(pos);
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x00015971 File Offset: 0x00013B71
		public void Copy(int dstPos, int srcPos, int numBits)
		{
			this.m_BitArray.Copy(dstPos, srcPos, numBits);
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x00015981 File Offset: 0x00013B81
		public void Copy(int dstPos, ref NativeBitArray srcBitArray, int srcPos, int numBits)
		{
			this.m_BitArray.Copy(dstPos, ref srcBitArray.m_BitArray, srcPos, numBits);
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x00015998 File Offset: 0x00013B98
		public int Find(int pos, int numBits)
		{
			return this.m_BitArray.Find(pos, numBits);
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x000159A7 File Offset: 0x00013BA7
		public int Find(int pos, int count, int numBits)
		{
			return this.m_BitArray.Find(pos, count, numBits);
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x000159B7 File Offset: 0x00013BB7
		public bool TestNone(int pos, int numBits = 1)
		{
			return this.m_BitArray.TestNone(pos, numBits);
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x000159C6 File Offset: 0x00013BC6
		public bool TestAny(int pos, int numBits = 1)
		{
			return this.m_BitArray.TestAny(pos, numBits);
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x000159D5 File Offset: 0x00013BD5
		public bool TestAll(int pos, int numBits = 1)
		{
			return this.m_BitArray.TestAll(pos, numBits);
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x000159E4 File Offset: 0x00013BE4
		public int CountBits(int pos, int numBits = 1)
		{
			return this.m_BitArray.CountBits(pos, numBits);
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x00002C2B File Offset: 0x00000E2B
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckRead()
		{
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x000159F4 File Offset: 0x00013BF4
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckReadBounds<[global::System.Runtime.CompilerServices.IsUnmanaged] T>() where T : struct, ValueType
		{
			int num = UnsafeUtility.SizeOf<T>() * 8;
			int num2 = this.m_BitArray.Length / num;
			if (num2 == 0)
			{
				throw new InvalidOperationException(string.Format("Number of bits in the NativeBitArray {0} is not sufficient to cast to NativeArray<T> {1}.", this.m_BitArray.Length, UnsafeUtility.SizeOf<T>() * 8));
			}
			if (this.m_BitArray.Length != num * num2)
			{
				throw new InvalidOperationException(string.Format("Number of bits in the NativeBitArray {0} couldn't hold multiple of T {1}. Output array would be truncated.", this.m_BitArray.Length, UnsafeUtility.SizeOf<T>()));
			}
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x00002C2B File Offset: 0x00000E2B
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckWrite()
		{
		}

		// Token: 0x0400027E RID: 638
		[NativeDisableUnsafePtrRestriction]
		internal UnsafeBitArray m_BitArray;
	}
}
