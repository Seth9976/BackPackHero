using System;
using System.Diagnostics;
using Unity.Jobs;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000155 RID: 341
	[BurstCompatible]
	[DebuggerDisplay("Length = {Length}, Capacity = {Capacity}, IsCreated = {IsCreated}, IsEmpty = {IsEmpty}")]
	public struct UnsafeText : INativeDisposable, IDisposable, IUTF8Bytes, INativeList<byte>, IIndexable<byte>
	{
		// Token: 0x06000C07 RID: 3079 RVA: 0x000240F4 File Offset: 0x000222F4
		public unsafe UnsafeText(int capacity, AllocatorManager.AllocatorHandle allocator)
		{
			this.m_UntypedListData = default(UntypedUnsafeList);
			*(ref this).AsUnsafeListOfBytes() = new UnsafeList<byte>(capacity + 1, allocator, NativeArrayOptions.UninitializedMemory);
			this.Length = 0;
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000C08 RID: 3080 RVA: 0x0002411E File Offset: 0x0002231E
		public bool IsCreated
		{
			get
			{
				return (ref this).AsUnsafeListOfBytes().IsCreated;
			}
		}

		// Token: 0x06000C09 RID: 3081 RVA: 0x0002412B File Offset: 0x0002232B
		public void Dispose()
		{
			(ref this).AsUnsafeListOfBytes().Dispose();
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x00024138 File Offset: 0x00022338
		[NotBurstCompatible]
		public JobHandle Dispose(JobHandle inputDeps)
		{
			return (ref this).AsUnsafeListOfBytes().Dispose(inputDeps);
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000C0B RID: 3083 RVA: 0x00024146 File Offset: 0x00022346
		public bool IsEmpty
		{
			get
			{
				return !this.IsCreated || this.Length == 0;
			}
		}

		// Token: 0x1700015E RID: 350
		public byte this[int index]
		{
			get
			{
				return UnsafeUtility.ReadArrayElement<byte>(this.m_UntypedListData.Ptr, index);
			}
			set
			{
				UnsafeUtility.WriteArrayElement<byte>(this.m_UntypedListData.Ptr, index, value);
			}
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x00024182 File Offset: 0x00022382
		public ref byte ElementAt(int index)
		{
			return UnsafeUtility.ArrayElementAsRef<byte>(this.m_UntypedListData.Ptr, index);
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x00024195 File Offset: 0x00022395
		public void Clear()
		{
			this.Length = 0;
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x0002419E File Offset: 0x0002239E
		public unsafe byte* GetUnsafePtr()
		{
			return (byte*)this.m_UntypedListData.Ptr;
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x000241AB File Offset: 0x000223AB
		public bool TryResize(int newLength, NativeArrayOptions clearOptions = NativeArrayOptions.ClearMemory)
		{
			(ref this).AsUnsafeListOfBytes().Resize(newLength + 1, clearOptions);
			(ref this).AsUnsafeListOfBytes()[newLength] = 0;
			return true;
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000C12 RID: 3090 RVA: 0x000241CA File Offset: 0x000223CA
		// (set) Token: 0x06000C13 RID: 3091 RVA: 0x000241D9 File Offset: 0x000223D9
		public int Capacity
		{
			get
			{
				return (ref this).AsUnsafeListOfBytes().Capacity - 1;
			}
			set
			{
				(ref this).AsUnsafeListOfBytes().SetCapacity(value + 1);
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000C14 RID: 3092 RVA: 0x000241E9 File Offset: 0x000223E9
		// (set) Token: 0x06000C15 RID: 3093 RVA: 0x000241F8 File Offset: 0x000223F8
		public int Length
		{
			get
			{
				return (ref this).AsUnsafeListOfBytes().Length - 1;
			}
			set
			{
				(ref this).AsUnsafeListOfBytes().Resize(value + 1, NativeArrayOptions.UninitializedMemory);
				(ref this).AsUnsafeListOfBytes()[value] = 0;
			}
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x00024216 File Offset: 0x00022416
		[NotBurstCompatible]
		public override string ToString()
		{
			if (!this.IsCreated)
			{
				return "";
			}
			return (ref this).ConvertToString<UnsafeText>();
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x0002422C File Offset: 0x0002242C
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckIndexInRange(int index)
		{
			if (index < 0)
			{
				throw new IndexOutOfRangeException(string.Format("Index {0} must be positive.", index));
			}
			if (index >= this.Length)
			{
				throw new IndexOutOfRangeException(string.Format("Index {0} is out of range in UnsafeText of {1} length.", index, this.Length));
			}
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x0002427D File Offset: 0x0002247D
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void ThrowCopyError(CopyError error, string source)
		{
			throw new ArgumentException(string.Format("UnsafeText: {0} while copying \"{1}\"", error, source));
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x00024295 File Offset: 0x00022495
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckCapacityInRange(int value, int length)
		{
			if (value < 0)
			{
				throw new ArgumentOutOfRangeException(string.Format("Value {0} must be positive.", value));
			}
			if (value < length)
			{
				throw new ArgumentOutOfRangeException(string.Format("Value {0} is out of range in NativeList of '{1}' Length.", value, length));
			}
		}

		// Token: 0x040003FB RID: 1019
		internal UntypedUnsafeList m_UntypedListData;
	}
}
