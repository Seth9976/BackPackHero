using System;
using System.Diagnostics;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.U2D.Common.UTess
{
	// Token: 0x02000002 RID: 2
	[DebuggerDisplay("Length = {Length}")]
	[DebuggerTypeProxy(typeof(ArrayDebugView<>))]
	internal struct Array<T> : IDisposable where T : struct
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public Array(int length, int maxSize, Allocator allocMode, NativeArrayOptions options)
		{
			this.m_Array = new NativeArray<T>(length, allocMode, options);
			this.m_AllocLabel = allocMode;
			this.m_Options = options;
			this.m_MaxSize = maxSize;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002078 File Offset: 0x00000278
		private void ResizeIfRequired(int index)
		{
			if (index >= this.m_MaxSize || index < 0)
			{
				throw new IndexOutOfRangeException(string.Format("Trying to access beyond allowed size. {0} is out of range of '{1}' MaxSize.", index, this.m_MaxSize));
			}
			if (index < this.m_Array.Length)
			{
				return;
			}
			int i;
			for (i = this.Length; i <= index; i *= 2)
			{
			}
			i = ((i > this.m_MaxSize) ? this.m_MaxSize : i);
			NativeArray<T> nativeArray = new NativeArray<T>(i, this.m_AllocLabel, this.m_Options);
			NativeArray<T>.Copy(this.m_Array, nativeArray, this.Length);
			this.m_Array.Dispose();
			this.m_Array = nativeArray;
		}

		// Token: 0x17000001 RID: 1
		public T this[int index]
		{
			get
			{
				return this.m_Array[index];
			}
			set
			{
				this.ResizeIfRequired(index);
				this.m_Array[index] = value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002142 File Offset: 0x00000342
		public bool IsCreated
		{
			get
			{
				return this.m_Array.IsCreated;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000006 RID: 6 RVA: 0x0000214F File Offset: 0x0000034F
		public int Length
		{
			get
			{
				if (this.m_MaxSize == 0)
				{
					return 0;
				}
				return this.m_Array.Length;
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002166 File Offset: 0x00000366
		public void Dispose()
		{
			this.m_Array.Dispose();
			this.m_MaxSize = 0;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000008 RID: 8 RVA: 0x0000217A File Offset: 0x0000037A
		public unsafe void* UnsafePtr
		{
			get
			{
				return this.m_Array.GetUnsafePtr<T>();
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002187 File Offset: 0x00000387
		public unsafe void* UnsafeReadOnlyPtr
		{
			get
			{
				return this.m_Array.GetUnsafeReadOnlyPtr<T>();
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002194 File Offset: 0x00000394
		public void CopyTo(T[] array)
		{
			this.m_Array.CopyTo(array);
		}

		// Token: 0x04000001 RID: 1
		internal NativeArray<T> m_Array;

		// Token: 0x04000002 RID: 2
		internal int m_MaxSize;

		// Token: 0x04000003 RID: 3
		internal Allocator m_AllocLabel;

		// Token: 0x04000004 RID: 4
		internal NativeArrayOptions m_Options;
	}
}
