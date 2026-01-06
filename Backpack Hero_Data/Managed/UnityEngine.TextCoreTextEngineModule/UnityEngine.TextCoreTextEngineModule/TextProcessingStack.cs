using System;
using System.Diagnostics;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x0200003B RID: 59
	[DebuggerDisplay("Item count = {m_Count}")]
	internal struct TextProcessingStack<T>
	{
		// Token: 0x06000165 RID: 357 RVA: 0x0001A8F3 File Offset: 0x00018AF3
		public TextProcessingStack(T[] stack)
		{
			this.itemStack = stack;
			this.m_Capacity = stack.Length;
			this.index = 0;
			this.m_RolloverSize = 0;
			this.m_DefaultItem = default(T);
			this.m_Count = 0;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x0001A927 File Offset: 0x00018B27
		public TextProcessingStack(int capacity)
		{
			this.itemStack = new T[capacity];
			this.m_Capacity = capacity;
			this.index = 0;
			this.m_RolloverSize = 0;
			this.m_DefaultItem = default(T);
			this.m_Count = 0;
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0001A95E File Offset: 0x00018B5E
		public TextProcessingStack(int capacity, int rolloverSize)
		{
			this.itemStack = new T[capacity];
			this.m_Capacity = capacity;
			this.index = 0;
			this.m_RolloverSize = rolloverSize;
			this.m_DefaultItem = default(T);
			this.m_Count = 0;
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000168 RID: 360 RVA: 0x0001A998 File Offset: 0x00018B98
		public int Count
		{
			get
			{
				return this.m_Count;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000169 RID: 361 RVA: 0x0001A9B0 File Offset: 0x00018BB0
		public T current
		{
			get
			{
				bool flag = this.index > 0;
				T t;
				if (flag)
				{
					t = this.itemStack[this.index - 1];
				}
				else
				{
					t = this.itemStack[0];
				}
				return t;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600016A RID: 362 RVA: 0x0001A9F4 File Offset: 0x00018BF4
		// (set) Token: 0x0600016B RID: 363 RVA: 0x0001AA0C File Offset: 0x00018C0C
		public int rolloverSize
		{
			get
			{
				return this.m_RolloverSize;
			}
			set
			{
				this.m_RolloverSize = value;
			}
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0001AA18 File Offset: 0x00018C18
		internal static void SetDefault(TextProcessingStack<T>[] stack, T item)
		{
			for (int i = 0; i < stack.Length; i++)
			{
				stack[i].SetDefault(item);
			}
		}

		// Token: 0x0600016D RID: 365 RVA: 0x0001AA46 File Offset: 0x00018C46
		public void Clear()
		{
			this.index = 0;
			this.m_Count = 0;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x0001AA58 File Offset: 0x00018C58
		public void SetDefault(T item)
		{
			bool flag = this.itemStack == null;
			if (flag)
			{
				this.m_Capacity = 4;
				this.itemStack = new T[this.m_Capacity];
				this.m_DefaultItem = default(T);
			}
			this.itemStack[0] = item;
			this.index = 1;
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0001AAB0 File Offset: 0x00018CB0
		public void Add(T item)
		{
			bool flag = this.index < this.itemStack.Length;
			if (flag)
			{
				this.itemStack[this.index] = item;
				this.index++;
			}
		}

		// Token: 0x06000170 RID: 368 RVA: 0x0001AAF4 File Offset: 0x00018CF4
		public T Remove()
		{
			this.index--;
			bool flag = this.index <= 0;
			T t;
			if (flag)
			{
				this.index = 1;
				t = this.itemStack[0];
			}
			else
			{
				t = this.itemStack[this.index - 1];
			}
			return t;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0001AB50 File Offset: 0x00018D50
		public void Push(T item)
		{
			bool flag = this.index == this.m_Capacity;
			if (flag)
			{
				this.m_Capacity *= 2;
				bool flag2 = this.m_Capacity == 0;
				if (flag2)
				{
					this.m_Capacity = 4;
				}
				Array.Resize<T>(ref this.itemStack, this.m_Capacity);
			}
			this.itemStack[this.index] = item;
			bool flag3 = this.m_RolloverSize == 0;
			if (flag3)
			{
				this.index++;
				this.m_Count++;
			}
			else
			{
				this.index = (this.index + 1) % this.m_RolloverSize;
				this.m_Count = ((this.m_Count < this.m_RolloverSize) ? (this.m_Count + 1) : this.m_RolloverSize);
			}
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0001AC20 File Offset: 0x00018E20
		public T Pop()
		{
			bool flag = this.index == 0 && this.m_RolloverSize == 0;
			T t;
			if (flag)
			{
				t = default(T);
			}
			else
			{
				bool flag2 = this.m_RolloverSize == 0;
				if (flag2)
				{
					this.index--;
				}
				else
				{
					this.index = (this.index - 1) % this.m_RolloverSize;
					this.index = ((this.index < 0) ? (this.index + this.m_RolloverSize) : this.index);
				}
				T t2 = this.itemStack[this.index];
				this.itemStack[this.index] = this.m_DefaultItem;
				this.m_Count = ((this.m_Count > 0) ? (this.m_Count - 1) : 0);
				t = t2;
			}
			return t;
		}

		// Token: 0x06000173 RID: 371 RVA: 0x0001ACF8 File Offset: 0x00018EF8
		public T Peek()
		{
			bool flag = this.index == 0;
			T t;
			if (flag)
			{
				t = this.m_DefaultItem;
			}
			else
			{
				t = this.itemStack[this.index - 1];
			}
			return t;
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0001AD34 File Offset: 0x00018F34
		public T CurrentItem()
		{
			bool flag = this.index > 0;
			T t;
			if (flag)
			{
				t = this.itemStack[this.index - 1];
			}
			else
			{
				t = this.itemStack[0];
			}
			return t;
		}

		// Token: 0x06000175 RID: 373 RVA: 0x0001AD78 File Offset: 0x00018F78
		public T PreviousItem()
		{
			bool flag = this.index > 1;
			T t;
			if (flag)
			{
				t = this.itemStack[this.index - 2];
			}
			else
			{
				t = this.itemStack[0];
			}
			return t;
		}

		// Token: 0x04000318 RID: 792
		public T[] itemStack;

		// Token: 0x04000319 RID: 793
		public int index;

		// Token: 0x0400031A RID: 794
		private T m_DefaultItem;

		// Token: 0x0400031B RID: 795
		private int m_Capacity;

		// Token: 0x0400031C RID: 796
		private int m_RolloverSize;

		// Token: 0x0400031D RID: 797
		private int m_Count;

		// Token: 0x0400031E RID: 798
		private const int k_DefaultCapacity = 4;
	}
}
