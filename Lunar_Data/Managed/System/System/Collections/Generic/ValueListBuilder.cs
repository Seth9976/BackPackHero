using System;
using System.Buffers;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
	// Token: 0x020007D8 RID: 2008
	internal ref struct ValueListBuilder<T>
	{
		// Token: 0x06004003 RID: 16387 RVA: 0x000DFB12 File Offset: 0x000DDD12
		public ValueListBuilder(Span<T> initialSpan)
		{
			this._span = initialSpan;
			this._arrayFromPool = null;
			this._pos = 0;
		}

		// Token: 0x17000E91 RID: 3729
		// (get) Token: 0x06004004 RID: 16388 RVA: 0x000DFB29 File Offset: 0x000DDD29
		public int Length
		{
			get
			{
				return this._pos;
			}
		}

		// Token: 0x17000E92 RID: 3730
		public ref T this[int index]
		{
			get
			{
				return this._span[index];
			}
		}

		// Token: 0x06004006 RID: 16390 RVA: 0x000DFB40 File Offset: 0x000DDD40
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe void Append(T item)
		{
			int pos = this._pos;
			if (pos >= this._span.Length)
			{
				this.Grow();
			}
			*this._span[pos] = item;
			this._pos = pos + 1;
		}

		// Token: 0x06004007 RID: 16391 RVA: 0x000DFB83 File Offset: 0x000DDD83
		public ReadOnlySpan<T> AsSpan()
		{
			return this._span.Slice(0, this._pos);
		}

		// Token: 0x06004008 RID: 16392 RVA: 0x000DFB9C File Offset: 0x000DDD9C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Dispose()
		{
			if (this._arrayFromPool != null)
			{
				ArrayPool<T>.Shared.Return(this._arrayFromPool, false);
				this._arrayFromPool = null;
			}
		}

		// Token: 0x06004009 RID: 16393 RVA: 0x000DFBC0 File Offset: 0x000DDDC0
		private void Grow()
		{
			T[] array = ArrayPool<T>.Shared.Rent(this._span.Length * 2);
			this._span.TryCopyTo(array);
			T[] arrayFromPool = this._arrayFromPool;
			this._span = (this._arrayFromPool = array);
			if (arrayFromPool != null)
			{
				ArrayPool<T>.Shared.Return(arrayFromPool, false);
			}
		}

		// Token: 0x0600400A RID: 16394 RVA: 0x000DFC22 File Offset: 0x000DDE22
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe T Pop()
		{
			this._pos--;
			return *this._span[this._pos];
		}

		// Token: 0x040026B8 RID: 9912
		private Span<T> _span;

		// Token: 0x040026B9 RID: 9913
		private T[] _arrayFromPool;

		// Token: 0x040026BA RID: 9914
		private int _pos;
	}
}
