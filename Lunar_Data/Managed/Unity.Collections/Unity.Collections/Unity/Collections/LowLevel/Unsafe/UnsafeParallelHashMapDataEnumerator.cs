using System;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000135 RID: 309
	internal struct UnsafeParallelHashMapDataEnumerator
	{
		// Token: 0x06000B39 RID: 2873 RVA: 0x00021AC3 File Offset: 0x0001FCC3
		internal unsafe UnsafeParallelHashMapDataEnumerator(UnsafeParallelHashMapData* data)
		{
			this.m_Buffer = data;
			this.m_Index = -1;
			this.m_BucketIndex = 0;
			this.m_NextIndex = -1;
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x00021AE1 File Offset: 0x0001FCE1
		internal bool MoveNext()
		{
			return UnsafeParallelHashMapData.MoveNext(this.m_Buffer, ref this.m_BucketIndex, ref this.m_NextIndex, out this.m_Index);
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x00021B00 File Offset: 0x0001FD00
		internal void Reset()
		{
			this.m_Index = -1;
			this.m_BucketIndex = 0;
			this.m_NextIndex = -1;
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x00021B18 File Offset: 0x0001FD18
		internal KeyValue<TKey, TValue> GetCurrent<TKey, TValue>() where TKey : struct, IEquatable<TKey> where TValue : struct
		{
			return new KeyValue<TKey, TValue>
			{
				m_Buffer = this.m_Buffer,
				m_Index = this.m_Index
			};
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x00021B48 File Offset: 0x0001FD48
		internal unsafe TKey GetCurrentKey<TKey>() where TKey : struct, IEquatable<TKey>
		{
			if (this.m_Index != -1)
			{
				return UnsafeUtility.ReadArrayElement<TKey>((void*)this.m_Buffer->keys, this.m_Index);
			}
			return default(TKey);
		}

		// Token: 0x040003AD RID: 941
		[NativeDisableUnsafePtrRestriction]
		internal unsafe UnsafeParallelHashMapData* m_Buffer;

		// Token: 0x040003AE RID: 942
		internal int m_Index;

		// Token: 0x040003AF RID: 943
		internal int m_BucketIndex;

		// Token: 0x040003B0 RID: 944
		internal int m_NextIndex;
	}
}
