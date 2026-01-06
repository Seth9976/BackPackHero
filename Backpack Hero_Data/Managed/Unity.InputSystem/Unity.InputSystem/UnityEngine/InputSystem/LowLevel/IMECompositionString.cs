using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000DE RID: 222
	[StructLayout(LayoutKind.Explicit, Size = 132)]
	public struct IMECompositionString : IEnumerable<char>, IEnumerable
	{
		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06000D2E RID: 3374 RVA: 0x0004275C File Offset: 0x0004095C
		public int Count
		{
			get
			{
				return this.size;
			}
		}

		// Token: 0x170003BB RID: 955
		public unsafe char this[int index]
		{
			get
			{
				if (index >= this.Count || index < 0)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				fixed (char* ptr = &this.buffer.FixedElementField)
				{
					return ptr[index];
				}
			}
		}

		// Token: 0x06000D30 RID: 3376 RVA: 0x000427A0 File Offset: 0x000409A0
		public unsafe IMECompositionString(string characters)
		{
			if (string.IsNullOrEmpty(characters))
			{
				this.size = 0;
				return;
			}
			this.size = characters.Length;
			for (int i = 0; i < this.size; i++)
			{
				*((ref this.buffer.FixedElementField) + (IntPtr)i * 2) = characters[i];
			}
		}

		// Token: 0x06000D31 RID: 3377 RVA: 0x000427F4 File Offset: 0x000409F4
		public unsafe override string ToString()
		{
			fixed (char* ptr = &this.buffer.FixedElementField)
			{
				return new string(ptr, 0, this.size);
			}
		}

		// Token: 0x06000D32 RID: 3378 RVA: 0x0004281B File Offset: 0x00040A1B
		public IEnumerator<char> GetEnumerator()
		{
			return new IMECompositionString.Enumerator(this);
		}

		// Token: 0x06000D33 RID: 3379 RVA: 0x0004282D File Offset: 0x00040A2D
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000560 RID: 1376
		[FieldOffset(0)]
		private int size;

		// Token: 0x04000561 RID: 1377
		[FixedBuffer(typeof(char), 64)]
		[FieldOffset(4)]
		private IMECompositionString.<buffer>e__FixedBuffer buffer;

		// Token: 0x02000207 RID: 519
		internal struct Enumerator : IEnumerator<char>, IEnumerator, IDisposable
		{
			// Token: 0x06001470 RID: 5232 RVA: 0x0005F317 File Offset: 0x0005D517
			public Enumerator(IMECompositionString compositionString)
			{
				this.m_CompositionString = compositionString;
				this.m_CurrentCharacter = '\0';
				this.m_CurrentIndex = -1;
			}

			// Token: 0x06001471 RID: 5233 RVA: 0x0005F330 File Offset: 0x0005D530
			public unsafe bool MoveNext()
			{
				int count = this.m_CompositionString.Count;
				this.m_CurrentIndex++;
				if (this.m_CurrentIndex == count)
				{
					return false;
				}
				fixed (char* ptr = &this.m_CompositionString.buffer.FixedElementField)
				{
					char* ptr2 = ptr;
					this.m_CurrentCharacter = ptr2[this.m_CurrentIndex];
				}
				return true;
			}

			// Token: 0x06001472 RID: 5234 RVA: 0x0005F38C File Offset: 0x0005D58C
			public void Reset()
			{
				this.m_CurrentIndex = -1;
			}

			// Token: 0x06001473 RID: 5235 RVA: 0x0005F395 File Offset: 0x0005D595
			public void Dispose()
			{
			}

			// Token: 0x17000580 RID: 1408
			// (get) Token: 0x06001474 RID: 5236 RVA: 0x0005F397 File Offset: 0x0005D597
			public char Current
			{
				get
				{
					return this.m_CurrentCharacter;
				}
			}

			// Token: 0x17000581 RID: 1409
			// (get) Token: 0x06001475 RID: 5237 RVA: 0x0005F39F File Offset: 0x0005D59F
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x04000B21 RID: 2849
			private IMECompositionString m_CompositionString;

			// Token: 0x04000B22 RID: 2850
			private char m_CurrentCharacter;

			// Token: 0x04000B23 RID: 2851
			private int m_CurrentIndex;
		}

		// Token: 0x02000208 RID: 520
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 128)]
		public struct <buffer>e__FixedBuffer
		{
			// Token: 0x04000B24 RID: 2852
			public char FixedElementField;
		}
	}
}
