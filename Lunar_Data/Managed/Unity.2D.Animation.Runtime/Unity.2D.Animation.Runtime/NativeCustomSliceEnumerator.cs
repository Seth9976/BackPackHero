using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;

namespace UnityEngine.U2D.Animation
{
	// Token: 0x02000007 RID: 7
	internal struct NativeCustomSliceEnumerator<T> : IEnumerable<T>, IEnumerable, IEnumerator<T>, IEnumerator, IDisposable where T : struct
	{
		// Token: 0x06000022 RID: 34 RVA: 0x00002669 File Offset: 0x00000869
		internal NativeCustomSliceEnumerator(NativeSlice<byte> slice, int length, int stride)
		{
			this.nativeCustomSlice = new NativeCustomSlice<T>(slice, length, stride);
			this.index = -1;
			this.Reset();
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002686 File Offset: 0x00000886
		public IEnumerator<T> GetEnumerator()
		{
			return this;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002693 File Offset: 0x00000893
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000269C File Offset: 0x0000089C
		public bool MoveNext()
		{
			int num = this.index + 1;
			this.index = num;
			return num < this.nativeCustomSlice.length;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000026CA File Offset: 0x000008CA
		public void Reset()
		{
			this.index = -1;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000027 RID: 39 RVA: 0x000026D3 File Offset: 0x000008D3
		public T Current
		{
			get
			{
				return this.nativeCustomSlice[this.index];
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000028 RID: 40 RVA: 0x000026E6 File Offset: 0x000008E6
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000026F3 File Offset: 0x000008F3
		void IDisposable.Dispose()
		{
		}

		// Token: 0x0400000E RID: 14
		private NativeCustomSlice<T> nativeCustomSlice;

		// Token: 0x0400000F RID: 15
		private int index;
	}
}
