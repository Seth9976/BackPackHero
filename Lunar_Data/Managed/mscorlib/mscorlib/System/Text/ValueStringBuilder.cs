using System;
using System.Buffers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Text
{
	// Token: 0x020003BA RID: 954
	internal ref struct ValueStringBuilder
	{
		// Token: 0x0600277D RID: 10109 RVA: 0x0008FE0A File Offset: 0x0008E00A
		public ValueStringBuilder(Span<char> initialBuffer)
		{
			this._arrayToReturnToPool = null;
			this._chars = initialBuffer;
			this._pos = 0;
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x0600277E RID: 10110 RVA: 0x0008FE21 File Offset: 0x0008E021
		// (set) Token: 0x0600277F RID: 10111 RVA: 0x0008FE29 File Offset: 0x0008E029
		public int Length
		{
			get
			{
				return this._pos;
			}
			set
			{
				this._pos = value;
			}
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06002780 RID: 10112 RVA: 0x0008FE32 File Offset: 0x0008E032
		public int Capacity
		{
			get
			{
				return this._chars.Length;
			}
		}

		// Token: 0x06002781 RID: 10113 RVA: 0x0008FE3F File Offset: 0x0008E03F
		public void EnsureCapacity(int capacity)
		{
			if (capacity > this._chars.Length)
			{
				this.Grow(capacity - this._chars.Length);
			}
		}

		// Token: 0x06002782 RID: 10114 RVA: 0x0008FE62 File Offset: 0x0008E062
		public unsafe ref char GetPinnableReference(bool terminate = false)
		{
			if (terminate)
			{
				this.EnsureCapacity(this.Length + 1);
				*this._chars[this.Length] = '\0';
			}
			return MemoryMarshal.GetReference<char>(this._chars);
		}

		// Token: 0x170004CA RID: 1226
		public ref char this[int index]
		{
			get
			{
				return this._chars[index];
			}
		}

		// Token: 0x06002784 RID: 10116 RVA: 0x0008FEA1 File Offset: 0x0008E0A1
		public override string ToString()
		{
			string text = new string(this._chars.Slice(0, this._pos));
			this.Dispose();
			return text;
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06002785 RID: 10117 RVA: 0x0008FEC5 File Offset: 0x0008E0C5
		public Span<char> RawChars
		{
			get
			{
				return this._chars;
			}
		}

		// Token: 0x06002786 RID: 10118 RVA: 0x0008FECD File Offset: 0x0008E0CD
		public unsafe ReadOnlySpan<char> AsSpan(bool terminate)
		{
			if (terminate)
			{
				this.EnsureCapacity(this.Length + 1);
				*this._chars[this.Length] = '\0';
			}
			return this._chars.Slice(0, this._pos);
		}

		// Token: 0x06002787 RID: 10119 RVA: 0x0008FF0A File Offset: 0x0008E10A
		public ReadOnlySpan<char> AsSpan()
		{
			return this._chars.Slice(0, this._pos);
		}

		// Token: 0x06002788 RID: 10120 RVA: 0x0008FF23 File Offset: 0x0008E123
		public ReadOnlySpan<char> AsSpan(int start)
		{
			return this._chars.Slice(start, this._pos - start);
		}

		// Token: 0x06002789 RID: 10121 RVA: 0x0008FF3E File Offset: 0x0008E13E
		public ReadOnlySpan<char> AsSpan(int start, int length)
		{
			return this._chars.Slice(start, length);
		}

		// Token: 0x0600278A RID: 10122 RVA: 0x0008FF54 File Offset: 0x0008E154
		public bool TryCopyTo(Span<char> destination, out int charsWritten)
		{
			if (this._chars.Slice(0, this._pos).TryCopyTo(destination))
			{
				charsWritten = this._pos;
				this.Dispose();
				return true;
			}
			charsWritten = 0;
			this.Dispose();
			return false;
		}

		// Token: 0x0600278B RID: 10123 RVA: 0x0008FF98 File Offset: 0x0008E198
		public void Insert(int index, char value, int count)
		{
			if (this._pos > this._chars.Length - count)
			{
				this.Grow(count);
			}
			int num = this._pos - index;
			this._chars.Slice(index, num).CopyTo(this._chars.Slice(index + count));
			this._chars.Slice(index, count).Fill(value);
			this._pos += count;
		}

		// Token: 0x0600278C RID: 10124 RVA: 0x00090014 File Offset: 0x0008E214
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe void Append(char c)
		{
			int pos = this._pos;
			if (pos < this._chars.Length)
			{
				*this._chars[pos] = c;
				this._pos = pos + 1;
				return;
			}
			this.GrowAndAppend(c);
		}

		// Token: 0x0600278D RID: 10125 RVA: 0x00090058 File Offset: 0x0008E258
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe void Append(string s)
		{
			int pos = this._pos;
			if (s.Length == 1 && pos < this._chars.Length)
			{
				*this._chars[pos] = s[0];
				this._pos = pos + 1;
				return;
			}
			this.AppendSlow(s);
		}

		// Token: 0x0600278E RID: 10126 RVA: 0x000900A8 File Offset: 0x0008E2A8
		private void AppendSlow(string s)
		{
			int pos = this._pos;
			if (pos > this._chars.Length - s.Length)
			{
				this.Grow(s.Length);
			}
			s.AsSpan().CopyTo(this._chars.Slice(pos));
			this._pos += s.Length;
		}

		// Token: 0x0600278F RID: 10127 RVA: 0x0009010C File Offset: 0x0008E30C
		public unsafe void Append(char c, int count)
		{
			if (this._pos > this._chars.Length - count)
			{
				this.Grow(count);
			}
			Span<char> span = this._chars.Slice(this._pos, count);
			for (int i = 0; i < span.Length; i++)
			{
				*span[i] = c;
			}
			this._pos += count;
		}

		// Token: 0x06002790 RID: 10128 RVA: 0x00090174 File Offset: 0x0008E374
		public unsafe void Append(char* value, int length)
		{
			if (this._pos > this._chars.Length - length)
			{
				this.Grow(length);
			}
			Span<char> span = this._chars.Slice(this._pos, length);
			for (int i = 0; i < span.Length; i++)
			{
				*span[i] = *(value++);
			}
			this._pos += length;
		}

		// Token: 0x06002791 RID: 10129 RVA: 0x000901E0 File Offset: 0x0008E3E0
		public void Append(ReadOnlySpan<char> value)
		{
			if (this._pos > this._chars.Length - value.Length)
			{
				this.Grow(value.Length);
			}
			value.CopyTo(this._chars.Slice(this._pos));
			this._pos += value.Length;
		}

		// Token: 0x06002792 RID: 10130 RVA: 0x00090244 File Offset: 0x0008E444
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Span<char> AppendSpan(int length)
		{
			int pos = this._pos;
			if (pos > this._chars.Length - length)
			{
				this.Grow(length);
			}
			this._pos = pos + length;
			return this._chars.Slice(pos, length);
		}

		// Token: 0x06002793 RID: 10131 RVA: 0x00090285 File Offset: 0x0008E485
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void GrowAndAppend(char c)
		{
			this.Grow(1);
			this.Append(c);
		}

		// Token: 0x06002794 RID: 10132 RVA: 0x00090298 File Offset: 0x0008E498
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void Grow(int requiredAdditionalCapacity)
		{
			char[] array = ArrayPool<char>.Shared.Rent(Math.Max(this._pos + requiredAdditionalCapacity, this._chars.Length * 2));
			this._chars.CopyTo(array);
			char[] arrayToReturnToPool = this._arrayToReturnToPool;
			this._chars = (this._arrayToReturnToPool = array);
			if (arrayToReturnToPool != null)
			{
				ArrayPool<char>.Shared.Return(arrayToReturnToPool, false);
			}
		}

		// Token: 0x06002795 RID: 10133 RVA: 0x00090308 File Offset: 0x0008E508
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Dispose()
		{
			char[] arrayToReturnToPool = this._arrayToReturnToPool;
			this = default(ValueStringBuilder);
			if (arrayToReturnToPool != null)
			{
				ArrayPool<char>.Shared.Return(arrayToReturnToPool, false);
			}
		}

		// Token: 0x04001E0F RID: 7695
		private char[] _arrayToReturnToPool;

		// Token: 0x04001E10 RID: 7696
		private Span<char> _chars;

		// Token: 0x04001E11 RID: 7697
		private int _pos;
	}
}
