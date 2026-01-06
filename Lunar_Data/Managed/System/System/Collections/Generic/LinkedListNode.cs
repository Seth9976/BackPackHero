using System;

namespace System.Collections.Generic
{
	/// <summary>Represents a node in a <see cref="T:System.Collections.Generic.LinkedList`1" />. This class cannot be inherited.</summary>
	/// <typeparam name="T">Specifies the element type of the linked list.</typeparam>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020007E1 RID: 2017
	public sealed class LinkedListNode<T>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.LinkedListNode`1" /> class, containing the specified value.</summary>
		/// <param name="value">The value to contain in the <see cref="T:System.Collections.Generic.LinkedListNode`1" />.</param>
		// Token: 0x0600404F RID: 16463 RVA: 0x000E082F File Offset: 0x000DEA2F
		public LinkedListNode(T value)
		{
			this.item = value;
		}

		// Token: 0x06004050 RID: 16464 RVA: 0x000E083E File Offset: 0x000DEA3E
		internal LinkedListNode(LinkedList<T> list, T value)
		{
			this.list = list;
			this.item = value;
		}

		/// <summary>Gets the <see cref="T:System.Collections.Generic.LinkedList`1" /> that the <see cref="T:System.Collections.Generic.LinkedListNode`1" /> belongs to.</summary>
		/// <returns>A reference to the <see cref="T:System.Collections.Generic.LinkedList`1" /> that the <see cref="T:System.Collections.Generic.LinkedListNode`1" /> belongs to, or null if the <see cref="T:System.Collections.Generic.LinkedListNode`1" /> is not linked.</returns>
		// Token: 0x17000EA0 RID: 3744
		// (get) Token: 0x06004051 RID: 16465 RVA: 0x000E0854 File Offset: 0x000DEA54
		public LinkedList<T> List
		{
			get
			{
				return this.list;
			}
		}

		/// <summary>Gets the next node in the <see cref="T:System.Collections.Generic.LinkedList`1" />.</summary>
		/// <returns>A reference to the next node in the <see cref="T:System.Collections.Generic.LinkedList`1" />, or null if the current node is the last element (<see cref="P:System.Collections.Generic.LinkedList`1.Last" />) of the <see cref="T:System.Collections.Generic.LinkedList`1" />.</returns>
		// Token: 0x17000EA1 RID: 3745
		// (get) Token: 0x06004052 RID: 16466 RVA: 0x000E085C File Offset: 0x000DEA5C
		public LinkedListNode<T> Next
		{
			get
			{
				if (this.next != null && this.next != this.list.head)
				{
					return this.next;
				}
				return null;
			}
		}

		/// <summary>Gets the previous node in the <see cref="T:System.Collections.Generic.LinkedList`1" />.</summary>
		/// <returns>A reference to the previous node in the <see cref="T:System.Collections.Generic.LinkedList`1" />, or null if the current node is the first element (<see cref="P:System.Collections.Generic.LinkedList`1.First" />) of the <see cref="T:System.Collections.Generic.LinkedList`1" />.</returns>
		// Token: 0x17000EA2 RID: 3746
		// (get) Token: 0x06004053 RID: 16467 RVA: 0x000E0881 File Offset: 0x000DEA81
		public LinkedListNode<T> Previous
		{
			get
			{
				if (this.prev != null && this != this.list.head)
				{
					return this.prev;
				}
				return null;
			}
		}

		/// <summary>Gets the value contained in the node.</summary>
		/// <returns>The value contained in the node.</returns>
		// Token: 0x17000EA3 RID: 3747
		// (get) Token: 0x06004054 RID: 16468 RVA: 0x000E08A1 File Offset: 0x000DEAA1
		// (set) Token: 0x06004055 RID: 16469 RVA: 0x000E08A9 File Offset: 0x000DEAA9
		public T Value
		{
			get
			{
				return this.item;
			}
			set
			{
				this.item = value;
			}
		}

		// Token: 0x06004056 RID: 16470 RVA: 0x000E08B2 File Offset: 0x000DEAB2
		internal void Invalidate()
		{
			this.list = null;
			this.next = null;
			this.prev = null;
		}

		// Token: 0x040026D8 RID: 9944
		internal LinkedList<T> list;

		// Token: 0x040026D9 RID: 9945
		internal LinkedListNode<T> next;

		// Token: 0x040026DA RID: 9946
		internal LinkedListNode<T> prev;

		// Token: 0x040026DB RID: 9947
		internal T item;
	}
}
