using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200004C RID: 76
	public class DynamicArray<T> where T : new()
	{
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000297 RID: 663 RVA: 0x0000DC56 File Offset: 0x0000BE56
		// (set) Token: 0x06000298 RID: 664 RVA: 0x0000DC5E File Offset: 0x0000BE5E
		public int size { get; private set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000299 RID: 665 RVA: 0x0000DC67 File Offset: 0x0000BE67
		public int capacity
		{
			get
			{
				return this.m_Array.Length;
			}
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000DC71 File Offset: 0x0000BE71
		public DynamicArray()
		{
			this.m_Array = new T[32];
			this.size = 0;
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000DC8D File Offset: 0x0000BE8D
		public DynamicArray(int size)
		{
			this.m_Array = new T[size];
			this.size = size;
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000DCA8 File Offset: 0x0000BEA8
		public void Clear()
		{
			this.size = 0;
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000DCB1 File Offset: 0x0000BEB1
		public bool Contains(T item)
		{
			return this.IndexOf(item) != -1;
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000DCC0 File Offset: 0x0000BEC0
		public int Add(in T value)
		{
			int size = this.size;
			if (size >= this.m_Array.Length)
			{
				T[] array = new T[this.m_Array.Length * 2];
				Array.Copy(this.m_Array, array, this.m_Array.Length);
				this.m_Array = array;
			}
			this.m_Array[size] = value;
			int size2 = this.size;
			this.size = size2 + 1;
			return size;
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000DD30 File Offset: 0x0000BF30
		public unsafe void AddRange(DynamicArray<T> array)
		{
			this.Reserve(this.size + array.size, true);
			for (int i = 0; i < array.size; i++)
			{
				T[] array2 = this.m_Array;
				int size = this.size;
				this.size = size + 1;
				array2[size] = *array[i];
			}
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000DD8C File Offset: 0x0000BF8C
		public bool Remove(T item)
		{
			int num = this.IndexOf(item);
			if (num != -1)
			{
				this.RemoveAt(num);
				return true;
			}
			return false;
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000DDB0 File Offset: 0x0000BFB0
		public void RemoveAt(int index)
		{
			if (index < 0 || index >= this.size)
			{
				throw new IndexOutOfRangeException();
			}
			if (index != this.size - 1)
			{
				Array.Copy(this.m_Array, index + 1, this.m_Array, index, this.size - index - 1);
			}
			int size = this.size;
			this.size = size - 1;
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000DE0C File Offset: 0x0000C00C
		public void RemoveRange(int index, int count)
		{
			if (index < 0 || index >= this.size || count < 0 || index + count > this.size)
			{
				throw new ArgumentOutOfRangeException();
			}
			Array.Copy(this.m_Array, index + count, this.m_Array, index, this.size - index - count);
			this.size -= count;
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000DE68 File Offset: 0x0000C068
		public int FindIndex(int startIndex, int count, Predicate<T> match)
		{
			for (int i = startIndex; i < this.size; i++)
			{
				if (match(this.m_Array[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000DEA0 File Offset: 0x0000C0A0
		public int IndexOf(T item, int index, int count)
		{
			int num = index;
			while (num < this.size && count > 0)
			{
				if (this.m_Array[num].Equals(item))
				{
					return num;
				}
				num++;
				count--;
			}
			return -1;
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000DEEC File Offset: 0x0000C0EC
		public int IndexOf(T item, int index)
		{
			for (int i = index; i < this.size; i++)
			{
				if (this.m_Array[i].Equals(item))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000DF2E File Offset: 0x0000C12E
		public int IndexOf(T item)
		{
			return this.IndexOf(item, 0);
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000DF38 File Offset: 0x0000C138
		public void Resize(int newSize, bool keepContent = false)
		{
			this.Reserve(newSize, keepContent);
			this.size = newSize;
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000DF4C File Offset: 0x0000C14C
		public void Reserve(int newCapacity, bool keepContent = false)
		{
			if (newCapacity > this.m_Array.Length)
			{
				if (keepContent)
				{
					T[] array = new T[newCapacity];
					Array.Copy(this.m_Array, array, this.m_Array.Length);
					this.m_Array = array;
					return;
				}
				this.m_Array = new T[newCapacity];
			}
		}

		// Token: 0x17000042 RID: 66
		public ref T this[int index]
		{
			get
			{
				return ref this.m_Array[index];
			}
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000DFA4 File Offset: 0x0000C1A4
		public static implicit operator T[](DynamicArray<T> array)
		{
			return array.m_Array;
		}

		// Token: 0x040001B3 RID: 435
		private T[] m_Array;
	}
}
