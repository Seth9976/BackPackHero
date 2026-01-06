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
		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06000D2B RID: 3371 RVA: 0x00042714 File Offset: 0x00040914
		public int Count
		{
			get
			{
				return this.size;
			}
		}

		// Token: 0x170003B9 RID: 953
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

		// Token: 0x06000D2D RID: 3373 RVA: 0x00042758 File Offset: 0x00040958
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

		// Token: 0x06000D2E RID: 3374 RVA: 0x000427AC File Offset: 0x000409AC
		public unsafe override string ToString()
		{
			fixed (char* ptr = &this.buffer.FixedElementField)
			{
				return new string(ptr, 0, this.size);
			}
		}

		// Token: 0x06000D2F RID: 3375 RVA: 0x000427D3 File Offset: 0x000409D3
		public IEnumerator<char> GetEnumerator()
		{
			return new IMECompositionString.Enumerator(this);
		}

		// Token: 0x06000D30 RID: 3376 RVA: 0x000427E5 File Offset: 0x000409E5
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
			// Token: 0x06001469 RID: 5225 RVA: 0x0005F103 File Offset: 0x0005D303
			public Enumerator(IMECompositionString compositionString)
			{
				this.m_CompositionString = compositionString;
				this.m_CurrentCharacter = '\0';
				this.m_CurrentIndex = -1;
			}

			// Token: 0x0600146A RID: 5226 RVA: 0x0005F11C File Offset: 0x0005D31C
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

			// Token: 0x0600146B RID: 5227 RVA: 0x0005F178 File Offset: 0x0005D378
			public void Reset()
			{
				this.m_CurrentIndex = -1;
			}

			// Token: 0x0600146C RID: 5228 RVA: 0x0005F181 File Offset: 0x0005D381
			public void Dispose()
			{
			}

			// Token: 0x1700057E RID: 1406
			// (get) Token: 0x0600146D RID: 5229 RVA: 0x0005F183 File Offset: 0x0005D383
			public char Current
			{
				get
				{
					return this.m_CurrentCharacter;
				}
			}

			// Token: 0x1700057F RID: 1407
			// (get) Token: 0x0600146E RID: 5230 RVA: 0x0005F18B File Offset: 0x0005D38B
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x04000B20 RID: 2848
			private IMECompositionString m_CompositionString;

			// Token: 0x04000B21 RID: 2849
			private char m_CurrentCharacter;

			// Token: 0x04000B22 RID: 2850
			private int m_CurrentIndex;
		}

		// Token: 0x02000208 RID: 520
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 128)]
		public struct <buffer>e__FixedBuffer
		{
			// Token: 0x04000B23 RID: 2851
			public char FixedElementField;
		}
	}
}
