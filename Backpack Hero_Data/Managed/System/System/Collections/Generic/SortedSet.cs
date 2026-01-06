using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Security;
using System.Threading;

namespace System.Collections.Generic
{
	/// <summary>Represents a collection of objects that is maintained in sorted order.</summary>
	/// <typeparam name="T">The type of elements in the set.</typeparam>
	// Token: 0x020007F6 RID: 2038
	[DebuggerTypeProxy(typeof(ICollectionDebugView<>))]
	[DebuggerDisplay("Count = {Count}")]
	[Serializable]
	public class SortedSet<T> : ISet<T>, ICollection<T>, IEnumerable<T>, IEnumerable, ICollection, IReadOnlyCollection<T>, ISerializable, IDeserializationCallback
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedSet`1" /> class. </summary>
		// Token: 0x06004137 RID: 16695 RVA: 0x000E2A16 File Offset: 0x000E0C16
		public SortedSet()
		{
			this.comparer = Comparer<T>.Default;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedSet`1" /> class that uses a specified comparer.</summary>
		/// <param name="comparer">The default comparer to use for comparing objects. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="comparer" /> is null.</exception>
		// Token: 0x06004138 RID: 16696 RVA: 0x000E2A29 File Offset: 0x000E0C29
		public SortedSet(IComparer<T> comparer)
		{
			this.comparer = comparer ?? Comparer<T>.Default;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedSet`1" /> class that contains elements copied from a specified enumerable collection.</summary>
		/// <param name="collection">The enumerable collection to be copied. </param>
		// Token: 0x06004139 RID: 16697 RVA: 0x000E2A41 File Offset: 0x000E0C41
		public SortedSet(IEnumerable<T> collection)
			: this(collection, Comparer<T>.Default)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedSet`1" /> class that contains elements copied from a specified enumerable collection and that uses a specified comparer.</summary>
		/// <param name="collection">The enumerable collection to be copied. </param>
		/// <param name="comparer">The default comparer to use for comparing objects. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="collection" /> is null.</exception>
		// Token: 0x0600413A RID: 16698 RVA: 0x000E2A50 File Offset: 0x000E0C50
		public SortedSet(IEnumerable<T> collection, IComparer<T> comparer)
			: this(comparer)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			SortedSet<T> sortedSet = collection as SortedSet<T>;
			if (sortedSet != null && !(sortedSet is SortedSet<T>.TreeSubSet) && this.HasEqualComparer(sortedSet))
			{
				if (sortedSet.Count > 0)
				{
					this.count = sortedSet.count;
					this.root = sortedSet.root.DeepClone(this.count);
				}
				return;
			}
			int num;
			T[] array = EnumerableHelpers.ToArray<T>(collection, out num);
			if (num > 0)
			{
				comparer = this.comparer;
				Array.Sort<T>(array, 0, num, comparer);
				int num2 = 1;
				for (int i = 1; i < num; i++)
				{
					if (comparer.Compare(array[i], array[i - 1]) != 0)
					{
						array[num2++] = array[i];
					}
				}
				num = num2;
				this.root = SortedSet<T>.ConstructRootFromSortedArray(array, 0, num - 1, null);
				this.count = num;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedSet`1" /> class that contains serialized data.</summary>
		/// <param name="info">The object that contains the information that is required to serialize the <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</param>
		/// <param name="context">The structure that contains the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</param>
		// Token: 0x0600413B RID: 16699 RVA: 0x000E2B31 File Offset: 0x000E0D31
		protected SortedSet(SerializationInfo info, StreamingContext context)
		{
			this.siInfo = info;
		}

		// Token: 0x0600413C RID: 16700 RVA: 0x000E2B40 File Offset: 0x000E0D40
		private void AddAllElements(IEnumerable<T> collection)
		{
			foreach (T t in collection)
			{
				if (!this.Contains(t))
				{
					this.Add(t);
				}
			}
		}

		// Token: 0x0600413D RID: 16701 RVA: 0x000E2B94 File Offset: 0x000E0D94
		private void RemoveAllElements(IEnumerable<T> collection)
		{
			T min = this.Min;
			T max = this.Max;
			foreach (T t in collection)
			{
				if (this.comparer.Compare(t, min) >= 0 && this.comparer.Compare(t, max) <= 0 && this.Contains(t))
				{
					this.Remove(t);
				}
			}
		}

		// Token: 0x0600413E RID: 16702 RVA: 0x000E2C14 File Offset: 0x000E0E14
		private bool ContainsAllElements(IEnumerable<T> collection)
		{
			foreach (T t in collection)
			{
				if (!this.Contains(t))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600413F RID: 16703 RVA: 0x000E2C68 File Offset: 0x000E0E68
		internal virtual bool InOrderTreeWalk(TreeWalkPredicate<T> action)
		{
			if (this.root == null)
			{
				return true;
			}
			Stack<SortedSet<T>.Node> stack = new Stack<SortedSet<T>.Node>(2 * SortedSet<T>.Log2(this.Count + 1));
			for (SortedSet<T>.Node node = this.root; node != null; node = node.Left)
			{
				stack.Push(node);
			}
			while (stack.Count != 0)
			{
				SortedSet<T>.Node node = stack.Pop();
				if (!action(node))
				{
					return false;
				}
				for (SortedSet<T>.Node node2 = node.Right; node2 != null; node2 = node2.Left)
				{
					stack.Push(node2);
				}
			}
			return true;
		}

		// Token: 0x06004140 RID: 16704 RVA: 0x000E2CE8 File Offset: 0x000E0EE8
		internal virtual bool BreadthFirstTreeWalk(TreeWalkPredicate<T> action)
		{
			if (this.root == null)
			{
				return true;
			}
			Queue<SortedSet<T>.Node> queue = new Queue<SortedSet<T>.Node>();
			queue.Enqueue(this.root);
			while (queue.Count != 0)
			{
				SortedSet<T>.Node node = queue.Dequeue();
				if (!action(node))
				{
					return false;
				}
				if (node.Left != null)
				{
					queue.Enqueue(node.Left);
				}
				if (node.Right != null)
				{
					queue.Enqueue(node.Right);
				}
			}
			return true;
		}

		/// <summary>Gets the number of elements in the <see cref="T:System.Collections.Generic.SortedSet`1" />.</summary>
		/// <returns>The number of elements in the <see cref="T:System.Collections.Generic.SortedSet`1" />.</returns>
		// Token: 0x17000EEC RID: 3820
		// (get) Token: 0x06004141 RID: 16705 RVA: 0x000E2D56 File Offset: 0x000E0F56
		public int Count
		{
			get
			{
				this.VersionCheck();
				return this.count;
			}
		}

		/// <summary>Gets the <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> object that is used to determine equality for the values in the <see cref="T:System.Collections.Generic.SortedSet`1" />.</summary>
		/// <returns>The comparer that is used to determine equality for the values in the <see cref="T:System.Collections.Generic.SortedSet`1" />.</returns>
		// Token: 0x17000EED RID: 3821
		// (get) Token: 0x06004142 RID: 16706 RVA: 0x000E2D64 File Offset: 0x000E0F64
		public IComparer<T> Comparer
		{
			get
			{
				return this.comparer;
			}
		}

		/// <summary>Gets a value that indicates whether a <see cref="T:System.Collections.ICollection" /> is read-only.</summary>
		/// <returns>true if the collection is read-only; otherwise, false.</returns>
		// Token: 0x17000EEE RID: 3822
		// (get) Token: 0x06004143 RID: 16707 RVA: 0x00003062 File Offset: 0x00001262
		bool ICollection<T>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value that indicates whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized; otherwise, false.</returns>
		// Token: 0x17000EEF RID: 3823
		// (get) Token: 0x06004144 RID: 16708 RVA: 0x00003062 File Offset: 0x00001262
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />. In the default implementation of <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" />, this property always returns the current instance.</returns>
		// Token: 0x17000EF0 RID: 3824
		// (get) Token: 0x06004145 RID: 16709 RVA: 0x000E2D6C File Offset: 0x000E0F6C
		object ICollection.SyncRoot
		{
			get
			{
				if (this._syncRoot == null)
				{
					Interlocked.CompareExchange(ref this._syncRoot, new object(), null);
				}
				return this._syncRoot;
			}
		}

		// Token: 0x06004146 RID: 16710 RVA: 0x00003917 File Offset: 0x00001B17
		internal virtual void VersionCheck()
		{
		}

		// Token: 0x06004147 RID: 16711 RVA: 0x0000390E File Offset: 0x00001B0E
		internal virtual bool IsWithinRange(T item)
		{
			return true;
		}

		/// <summary>Adds an element to the set and returns a value that indicates if it was successfully added.</summary>
		/// <returns>true if <paramref name="item" /> is added to the set; otherwise, false. </returns>
		/// <param name="item">The element to add to the set.</param>
		// Token: 0x06004148 RID: 16712 RVA: 0x000E2D8E File Offset: 0x000E0F8E
		public bool Add(T item)
		{
			return this.AddIfNotPresent(item);
		}

		/// <summary>Adds an item to an <see cref="T:System.Collections.Generic.ICollection`1" /> object.</summary>
		/// <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" /> object.</param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only.</exception>
		// Token: 0x06004149 RID: 16713 RVA: 0x000E2D97 File Offset: 0x000E0F97
		void ICollection<T>.Add(T item)
		{
			this.Add(item);
		}

		// Token: 0x0600414A RID: 16714 RVA: 0x000E2DA4 File Offset: 0x000E0FA4
		internal virtual bool AddIfNotPresent(T item)
		{
			if (this.root == null)
			{
				this.root = new SortedSet<T>.Node(item, NodeColor.Black);
				this.count = 1;
				this.version++;
				return true;
			}
			SortedSet<T>.Node node = this.root;
			SortedSet<T>.Node node2 = null;
			SortedSet<T>.Node node3 = null;
			SortedSet<T>.Node node4 = null;
			this.version++;
			int num = 0;
			while (node != null)
			{
				num = this.comparer.Compare(item, node.Item);
				if (num == 0)
				{
					this.root.ColorBlack();
					return false;
				}
				if (node.Is4Node)
				{
					node.Split4Node();
					if (SortedSet<T>.Node.IsNonNullRed(node2))
					{
						this.InsertionBalance(node, ref node2, node3, node4);
					}
				}
				node4 = node3;
				node3 = node2;
				node2 = node;
				node = ((num < 0) ? node.Left : node.Right);
			}
			SortedSet<T>.Node node5 = new SortedSet<T>.Node(item, NodeColor.Red);
			if (num > 0)
			{
				node2.Right = node5;
			}
			else
			{
				node2.Left = node5;
			}
			if (node2.IsRed)
			{
				this.InsertionBalance(node5, ref node2, node3, node4);
			}
			this.root.ColorBlack();
			this.count++;
			return true;
		}

		/// <summary>Removes a specified item from the <see cref="T:System.Collections.Generic.SortedSet`1" />.</summary>
		/// <returns>true if the element is found and successfully removed; otherwise, false. </returns>
		/// <param name="item">The element to remove.</param>
		// Token: 0x0600414B RID: 16715 RVA: 0x000E2EAE File Offset: 0x000E10AE
		public bool Remove(T item)
		{
			return this.DoRemove(item);
		}

		// Token: 0x0600414C RID: 16716 RVA: 0x000E2EB8 File Offset: 0x000E10B8
		internal virtual bool DoRemove(T item)
		{
			if (this.root == null)
			{
				return false;
			}
			this.version++;
			SortedSet<T>.Node node = this.root;
			SortedSet<T>.Node node2 = null;
			SortedSet<T>.Node node3 = null;
			SortedSet<T>.Node node4 = null;
			SortedSet<T>.Node node5 = null;
			bool flag = false;
			while (node != null)
			{
				if (node.Is2Node)
				{
					if (node2 == null)
					{
						node.ColorRed();
					}
					else
					{
						SortedSet<T>.Node node6 = node2.GetSibling(node);
						if (node6.IsRed)
						{
							if (node2.Right == node6)
							{
								node2.RotateLeft();
							}
							else
							{
								node2.RotateRight();
							}
							node2.ColorRed();
							node6.ColorBlack();
							this.ReplaceChildOrRoot(node3, node2, node6);
							node3 = node6;
							if (node2 == node4)
							{
								node5 = node6;
							}
							node6 = node2.GetSibling(node);
						}
						if (node6.Is2Node)
						{
							node2.Merge2Nodes();
						}
						else
						{
							SortedSet<T>.Node node7 = node2.Rotate(node2.GetRotation(node, node6));
							node7.Color = node2.Color;
							node2.ColorBlack();
							node.ColorRed();
							this.ReplaceChildOrRoot(node3, node2, node7);
							if (node2 == node4)
							{
								node5 = node7;
							}
						}
					}
				}
				int num = (flag ? (-1) : this.comparer.Compare(item, node.Item));
				if (num == 0)
				{
					flag = true;
					node4 = node;
					node5 = node2;
				}
				node3 = node2;
				node2 = node;
				node = ((num < 0) ? node.Left : node.Right);
			}
			if (node4 != null)
			{
				this.ReplaceNode(node4, node5, node2, node3);
				this.count--;
			}
			SortedSet<T>.Node node8 = this.root;
			if (node8 != null)
			{
				node8.ColorBlack();
			}
			return flag;
		}

		/// <summary>Removes all elements from the set.</summary>
		// Token: 0x0600414D RID: 16717 RVA: 0x000E3024 File Offset: 0x000E1224
		public virtual void Clear()
		{
			this.root = null;
			this.count = 0;
			this.version++;
		}

		/// <summary>Determines whether the set contains a specific element.</summary>
		/// <returns>true if the set contains <paramref name="item" />; otherwise, false.</returns>
		/// <param name="item">The element to locate in the set.</param>
		// Token: 0x0600414E RID: 16718 RVA: 0x000E3042 File Offset: 0x000E1242
		public virtual bool Contains(T item)
		{
			return this.FindNode(item) != null;
		}

		/// <summary>Copies the complete <see cref="T:System.Collections.Generic.SortedSet`1" /> to a compatible one-dimensional array, starting at the beginning of the target array.</summary>
		/// <param name="array">A one-dimensional array that is the destination of the elements copied from the <see cref="T:System.Collections.Generic.SortedSet`1" />.</param>
		/// <exception cref="T:System.ArgumentException">The number of elements in the source <see cref="T:System.Collections.Generic.SortedSet`1" /> exceeds the number of elements that the destination array can contain. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		// Token: 0x0600414F RID: 16719 RVA: 0x000E304E File Offset: 0x000E124E
		public void CopyTo(T[] array)
		{
			this.CopyTo(array, 0, this.Count);
		}

		/// <summary>Copies the complete <see cref="T:System.Collections.Generic.SortedSet`1" /> to a compatible one-dimensional array, starting at the specified array index.</summary>
		/// <param name="array">A one-dimensional array that is the destination of the elements copied from the <see cref="T:System.Collections.Generic.SortedSet`1" />. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentException">The number of elements in the source array is greater than the available space from <paramref name="index" /> to the end of the destination array.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		// Token: 0x06004150 RID: 16720 RVA: 0x000E305E File Offset: 0x000E125E
		public void CopyTo(T[] array, int index)
		{
			this.CopyTo(array, index, this.Count);
		}

		/// <summary>Copies a specified number of elements from <see cref="T:System.Collections.Generic.SortedSet`1" /> to a compatible one-dimensional array, starting at the specified array index.</summary>
		/// <param name="array">A one-dimensional array that is the destination of the elements copied from the <see cref="T:System.Collections.Generic.SortedSet`1" />. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <param name="count">The number of elements to copy.</param>
		/// <exception cref="T:System.ArgumentException">The number of elements in the source array is greater than the available space from <paramref name="index" /> to the end of the destination array.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or-<paramref name="count" /> is less than zero.</exception>
		// Token: 0x06004151 RID: 16721 RVA: 0x000E3070 File Offset: 0x000E1270
		public void CopyTo(T[] array, int index, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", index, "Non-negative number required.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Non-negative number required.");
			}
			if (count > array.Length - index)
			{
				throw new ArgumentException("Destination array is not long enough to copy all the items in the collection. Check array index and length.");
			}
			count += index;
			this.InOrderTreeWalk(delegate(SortedSet<T>.Node node)
			{
				if (index >= count)
				{
					return false;
				}
				T[] array2 = array;
				int index2 = index;
				index = index2 + 1;
				array2[index2] = node.Item;
				return true;
			});
		}

		/// <summary>Copies the complete <see cref="T:System.Collections.Generic.SortedSet`1" /> to a compatible one-dimensional array, starting at the specified array index.</summary>
		/// <param name="array">A one-dimensional array that is the destination of the elements copied from the <see cref="T:System.Collections.Generic.SortedSet`1" />. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentException">The number of elements in the source array is greater than the available space from <paramref name="index" /> to the end of the destination array. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		// Token: 0x06004152 RID: 16722 RVA: 0x000E3130 File Offset: 0x000E1330
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException("Only single dimensional arrays are supported for the requested action.", "array");
			}
			if (array.GetLowerBound(0) != 0)
			{
				throw new ArgumentException("The lower bound of target array must be zero.", "array");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", index, "Non-negative number required.");
			}
			if (array.Length - index < this.Count)
			{
				throw new ArgumentException("Destination array is not long enough to copy all the items in the collection. Check array index and length.");
			}
			T[] array2 = array as T[];
			if (array2 != null)
			{
				this.CopyTo(array2, index);
				return;
			}
			object[] objects = array as object[];
			if (objects == null)
			{
				throw new ArgumentException("Target array type is not compatible with the type of items in the collection.", "array");
			}
			try
			{
				this.InOrderTreeWalk(delegate(SortedSet<T>.Node node)
				{
					object[] objects2 = objects;
					int index2 = index;
					index = index2 + 1;
					objects2[index2] = node.Item;
					return true;
				});
			}
			catch (ArrayTypeMismatchException)
			{
				throw new ArgumentException("Target array type is not compatible with the type of items in the collection.", "array");
			}
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.Generic.SortedSet`1" />.</summary>
		/// <returns>An enumerator that iterates through the <see cref="T:System.Collections.Generic.SortedSet`1" /> in sorted order.</returns>
		// Token: 0x06004153 RID: 16723 RVA: 0x000E3244 File Offset: 0x000E1444
		public SortedSet<T>.Enumerator GetEnumerator()
		{
			return new SortedSet<T>.Enumerator(this);
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An enumerator that can be used to iterate through the collection.</returns>
		// Token: 0x06004154 RID: 16724 RVA: 0x000E324C File Offset: 0x000E144C
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An enumerator that can be used to iterate through the collection.</returns>
		// Token: 0x06004155 RID: 16725 RVA: 0x000E324C File Offset: 0x000E144C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06004156 RID: 16726 RVA: 0x000E325C File Offset: 0x000E145C
		private void InsertionBalance(SortedSet<T>.Node current, ref SortedSet<T>.Node parent, SortedSet<T>.Node grandParent, SortedSet<T>.Node greatGrandParent)
		{
			bool flag = grandParent.Right == parent;
			bool flag2 = parent.Right == current;
			SortedSet<T>.Node node;
			if (flag == flag2)
			{
				node = (flag2 ? grandParent.RotateLeft() : grandParent.RotateRight());
			}
			else
			{
				node = (flag2 ? grandParent.RotateLeftRight() : grandParent.RotateRightLeft());
				parent = greatGrandParent;
			}
			grandParent.ColorRed();
			node.ColorBlack();
			this.ReplaceChildOrRoot(greatGrandParent, grandParent, node);
		}

		// Token: 0x06004157 RID: 16727 RVA: 0x000E32C1 File Offset: 0x000E14C1
		private void ReplaceChildOrRoot(SortedSet<T>.Node parent, SortedSet<T>.Node child, SortedSet<T>.Node newChild)
		{
			if (parent != null)
			{
				parent.ReplaceChild(child, newChild);
				return;
			}
			this.root = newChild;
		}

		// Token: 0x06004158 RID: 16728 RVA: 0x000E32D8 File Offset: 0x000E14D8
		private void ReplaceNode(SortedSet<T>.Node match, SortedSet<T>.Node parentOfMatch, SortedSet<T>.Node successor, SortedSet<T>.Node parentOfSuccessor)
		{
			if (successor == match)
			{
				successor = match.Left;
			}
			else
			{
				SortedSet<T>.Node right = successor.Right;
				if (right != null)
				{
					right.ColorBlack();
				}
				if (parentOfSuccessor != match)
				{
					parentOfSuccessor.Left = successor.Right;
					successor.Right = match.Right;
				}
				successor.Left = match.Left;
			}
			if (successor != null)
			{
				successor.Color = match.Color;
			}
			this.ReplaceChildOrRoot(parentOfMatch, match, successor);
		}

		// Token: 0x06004159 RID: 16729 RVA: 0x000E3348 File Offset: 0x000E1548
		internal virtual SortedSet<T>.Node FindNode(T item)
		{
			int num;
			for (SortedSet<T>.Node node = this.root; node != null; node = ((num < 0) ? node.Left : node.Right))
			{
				num = this.comparer.Compare(item, node.Item);
				if (num == 0)
				{
					return node;
				}
			}
			return null;
		}

		// Token: 0x0600415A RID: 16730 RVA: 0x000E3390 File Offset: 0x000E1590
		internal virtual int InternalIndexOf(T item)
		{
			SortedSet<T>.Node node = this.root;
			int num = 0;
			while (node != null)
			{
				int num2 = this.comparer.Compare(item, node.Item);
				if (num2 == 0)
				{
					return num;
				}
				node = ((num2 < 0) ? node.Left : node.Right);
				num = ((num2 < 0) ? (2 * num + 1) : (2 * num + 2));
			}
			return -1;
		}

		// Token: 0x0600415B RID: 16731 RVA: 0x000E33E8 File Offset: 0x000E15E8
		internal SortedSet<T>.Node FindRange(T from, T to)
		{
			return this.FindRange(from, to, true, true);
		}

		// Token: 0x0600415C RID: 16732 RVA: 0x000E33F4 File Offset: 0x000E15F4
		internal SortedSet<T>.Node FindRange(T from, T to, bool lowerBoundActive, bool upperBoundActive)
		{
			SortedSet<T>.Node node = this.root;
			while (node != null)
			{
				if (lowerBoundActive && this.comparer.Compare(from, node.Item) > 0)
				{
					node = node.Right;
				}
				else
				{
					if (!upperBoundActive || this.comparer.Compare(to, node.Item) >= 0)
					{
						return node;
					}
					node = node.Left;
				}
			}
			return null;
		}

		// Token: 0x0600415D RID: 16733 RVA: 0x000E3453 File Offset: 0x000E1653
		internal void UpdateVersion()
		{
			this.version++;
		}

		/// <summary>Returns an <see cref="T:System.Collections.IEqualityComparer" /> object that can be used to create a collection that contains individual sets.</summary>
		/// <returns>A comparer for creating a collection of sets.</returns>
		// Token: 0x0600415E RID: 16734 RVA: 0x000E3463 File Offset: 0x000E1663
		public static IEqualityComparer<SortedSet<T>> CreateSetComparer()
		{
			return SortedSet<T>.CreateSetComparer(null);
		}

		/// <summary>Returns an <see cref="T:System.Collections.IEqualityComparer" /> object, according to a specified comparer, that can be used to create a collection that contains individual sets.</summary>
		/// <returns>A comparer for creating a collection of sets.</returns>
		/// <param name="memberEqualityComparer">The comparer to use for creating the returned comparer.</param>
		// Token: 0x0600415F RID: 16735 RVA: 0x000E346B File Offset: 0x000E166B
		public static IEqualityComparer<SortedSet<T>> CreateSetComparer(IEqualityComparer<T> memberEqualityComparer)
		{
			return new SortedSetEqualityComparer<T>(memberEqualityComparer);
		}

		// Token: 0x06004160 RID: 16736 RVA: 0x000E3474 File Offset: 0x000E1674
		internal static bool SortedSetEquals(SortedSet<T> set1, SortedSet<T> set2, IComparer<T> comparer)
		{
			if (set1 == null)
			{
				return set2 == null;
			}
			if (set2 == null)
			{
				return false;
			}
			if (set1.HasEqualComparer(set2))
			{
				return set1.Count == set2.Count && set1.SetEquals(set2);
			}
			bool flag = false;
			foreach (T t in set1)
			{
				flag = false;
				foreach (T t2 in set2)
				{
					if (comparer.Compare(t, t2) == 0)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06004161 RID: 16737 RVA: 0x000E3540 File Offset: 0x000E1740
		private bool HasEqualComparer(SortedSet<T> other)
		{
			return this.Comparer == other.Comparer || this.Comparer.Equals(other.Comparer);
		}

		/// <summary>Modifies the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object so that it contains all elements that are present in either the current object or the specified collection. </summary>
		/// <param name="other">The collection to compare to the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is null.</exception>
		// Token: 0x06004162 RID: 16738 RVA: 0x000E3564 File Offset: 0x000E1764
		public void UnionWith(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			SortedSet<T> sortedSet = other as SortedSet<T>;
			SortedSet<T>.TreeSubSet treeSubSet = this as SortedSet<T>.TreeSubSet;
			if (treeSubSet != null)
			{
				this.VersionCheck();
			}
			if (sortedSet != null && treeSubSet == null && this.count == 0)
			{
				SortedSet<T> sortedSet2 = new SortedSet<T>(sortedSet, this.comparer);
				this.root = sortedSet2.root;
				this.count = sortedSet2.count;
				this.version++;
				return;
			}
			if (sortedSet != null && treeSubSet == null && this.HasEqualComparer(sortedSet) && sortedSet.Count > this.Count / 2)
			{
				T[] array = new T[sortedSet.Count + this.Count];
				int num = 0;
				SortedSet<T>.Enumerator enumerator = this.GetEnumerator();
				SortedSet<T>.Enumerator enumerator2 = sortedSet.GetEnumerator();
				bool flag = !enumerator.MoveNext();
				bool flag2 = !enumerator2.MoveNext();
				while (!flag && !flag2)
				{
					int num2 = this.Comparer.Compare(enumerator.Current, enumerator2.Current);
					if (num2 < 0)
					{
						array[num++] = enumerator.Current;
						flag = !enumerator.MoveNext();
					}
					else if (num2 == 0)
					{
						array[num++] = enumerator2.Current;
						flag = !enumerator.MoveNext();
						flag2 = !enumerator2.MoveNext();
					}
					else
					{
						array[num++] = enumerator2.Current;
						flag2 = !enumerator2.MoveNext();
					}
				}
				if (!flag || !flag2)
				{
					SortedSet<T>.Enumerator enumerator3 = (flag ? enumerator2 : enumerator);
					do
					{
						array[num++] = enumerator3.Current;
					}
					while (enumerator3.MoveNext());
				}
				this.root = null;
				this.root = SortedSet<T>.ConstructRootFromSortedArray(array, 0, num - 1, null);
				this.count = num;
				this.version++;
				return;
			}
			this.AddAllElements(other);
		}

		// Token: 0x06004163 RID: 16739 RVA: 0x000E3750 File Offset: 0x000E1950
		private static SortedSet<T>.Node ConstructRootFromSortedArray(T[] arr, int startIndex, int endIndex, SortedSet<T>.Node redNode)
		{
			int num = endIndex - startIndex + 1;
			SortedSet<T>.Node node;
			switch (num)
			{
			case 0:
				return null;
			case 1:
				node = new SortedSet<T>.Node(arr[startIndex], NodeColor.Black);
				if (redNode != null)
				{
					node.Left = redNode;
				}
				break;
			case 2:
				node = new SortedSet<T>.Node(arr[startIndex], NodeColor.Black);
				node.Right = new SortedSet<T>.Node(arr[endIndex], NodeColor.Black);
				node.Right.ColorRed();
				if (redNode != null)
				{
					node.Left = redNode;
				}
				break;
			case 3:
				node = new SortedSet<T>.Node(arr[startIndex + 1], NodeColor.Black);
				node.Left = new SortedSet<T>.Node(arr[startIndex], NodeColor.Black);
				node.Right = new SortedSet<T>.Node(arr[endIndex], NodeColor.Black);
				if (redNode != null)
				{
					node.Left.Left = redNode;
				}
				break;
			default:
			{
				int num2 = (startIndex + endIndex) / 2;
				node = new SortedSet<T>.Node(arr[num2], NodeColor.Black);
				node.Left = SortedSet<T>.ConstructRootFromSortedArray(arr, startIndex, num2 - 1, redNode);
				node.Right = ((num % 2 == 0) ? SortedSet<T>.ConstructRootFromSortedArray(arr, num2 + 2, endIndex, new SortedSet<T>.Node(arr[num2 + 1], NodeColor.Red)) : SortedSet<T>.ConstructRootFromSortedArray(arr, num2 + 1, endIndex, null));
				break;
			}
			}
			return node;
		}

		/// <summary>Modifies the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object so that it contains only elements that are also in a specified collection.</summary>
		/// <param name="other">The collection to compare to the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is null.</exception>
		// Token: 0x06004164 RID: 16740 RVA: 0x000E387C File Offset: 0x000E1A7C
		public virtual void IntersectWith(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (this.Count == 0)
			{
				return;
			}
			if (other == this)
			{
				return;
			}
			SortedSet<T> sortedSet = other as SortedSet<T>;
			SortedSet<T>.TreeSubSet treeSubSet = this as SortedSet<T>.TreeSubSet;
			if (treeSubSet != null)
			{
				this.VersionCheck();
			}
			if (sortedSet != null && treeSubSet == null && this.HasEqualComparer(sortedSet))
			{
				T[] array = new T[this.Count];
				int num = 0;
				SortedSet<T>.Enumerator enumerator = this.GetEnumerator();
				SortedSet<T>.Enumerator enumerator2 = sortedSet.GetEnumerator();
				bool flag = !enumerator.MoveNext();
				bool flag2 = !enumerator2.MoveNext();
				T max = this.Max;
				T min = this.Min;
				while (!flag && !flag2 && this.Comparer.Compare(enumerator2.Current, max) <= 0)
				{
					int num2 = this.Comparer.Compare(enumerator.Current, enumerator2.Current);
					if (num2 < 0)
					{
						flag = !enumerator.MoveNext();
					}
					else if (num2 == 0)
					{
						array[num++] = enumerator2.Current;
						flag = !enumerator.MoveNext();
						flag2 = !enumerator2.MoveNext();
					}
					else
					{
						flag2 = !enumerator2.MoveNext();
					}
				}
				this.root = null;
				this.root = SortedSet<T>.ConstructRootFromSortedArray(array, 0, num - 1, null);
				this.count = num;
				this.version++;
				return;
			}
			this.IntersectWithEnumerable(other);
		}

		// Token: 0x06004165 RID: 16741 RVA: 0x000E39DC File Offset: 0x000E1BDC
		internal virtual void IntersectWithEnumerable(IEnumerable<T> other)
		{
			List<T> list = new List<T>(this.Count);
			foreach (T t in other)
			{
				if (this.Contains(t))
				{
					list.Add(t);
				}
			}
			this.Clear();
			foreach (T t2 in list)
			{
				this.Add(t2);
			}
		}

		/// <summary>Removes all elements that are in a specified collection from the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</summary>
		/// <param name="other">The collection of items to remove from the <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is null.</exception>
		// Token: 0x06004166 RID: 16742 RVA: 0x000E3A80 File Offset: 0x000E1C80
		public void ExceptWith(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (this.count == 0)
			{
				return;
			}
			if (other == this)
			{
				this.Clear();
				return;
			}
			SortedSet<T> sortedSet = other as SortedSet<T>;
			if (sortedSet != null && this.HasEqualComparer(sortedSet))
			{
				if (this.comparer.Compare(sortedSet.Max, this.Min) < 0 || this.comparer.Compare(sortedSet.Min, this.Max) > 0)
				{
					return;
				}
				T min = this.Min;
				T max = this.Max;
				using (IEnumerator<T> enumerator = other.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						T t = enumerator.Current;
						if (this.comparer.Compare(t, min) >= 0)
						{
							if (this.comparer.Compare(t, max) > 0)
							{
								break;
							}
							this.Remove(t);
						}
					}
					return;
				}
			}
			this.RemoveAllElements(other);
		}

		/// <summary>Modifies the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object so that it contains only elements that are present either in the current object or in the specified collection, but not both.</summary>
		/// <param name="other">The collection to compare to the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is null.</exception>
		// Token: 0x06004167 RID: 16743 RVA: 0x000E3B78 File Offset: 0x000E1D78
		public void SymmetricExceptWith(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (this.Count == 0)
			{
				this.UnionWith(other);
				return;
			}
			if (other == this)
			{
				this.Clear();
				return;
			}
			SortedSet<T> sortedSet = other as SortedSet<T>;
			if (sortedSet != null && this.HasEqualComparer(sortedSet))
			{
				this.SymmetricExceptWithSameComparer(sortedSet);
				return;
			}
			int num;
			T[] array = EnumerableHelpers.ToArray<T>(other, out num);
			Array.Sort<T>(array, 0, num, this.Comparer);
			this.SymmetricExceptWithSameComparer(array, num);
		}

		// Token: 0x06004168 RID: 16744 RVA: 0x000E3BE8 File Offset: 0x000E1DE8
		private void SymmetricExceptWithSameComparer(SortedSet<T> other)
		{
			foreach (T t in other)
			{
				if (!this.Contains(t))
				{
					this.Add(t);
				}
				else
				{
					this.Remove(t);
				}
			}
		}

		// Token: 0x06004169 RID: 16745 RVA: 0x000E3C4C File Offset: 0x000E1E4C
		private void SymmetricExceptWithSameComparer(T[] other, int count)
		{
			if (count == 0)
			{
				return;
			}
			T t = other[0];
			for (int i = 0; i < count; i++)
			{
				while (i < count && i != 0 && this.comparer.Compare(other[i], t) == 0)
				{
					i++;
				}
				if (i >= count)
				{
					break;
				}
				T t2 = other[i];
				if (!this.Contains(t2))
				{
					this.Add(t2);
				}
				else
				{
					this.Remove(t2);
				}
				t = t2;
			}
		}

		/// <summary>Determines whether a <see cref="T:System.Collections.Generic.SortedSet`1" /> object is a subset of the specified collection.</summary>
		/// <returns>true if the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object is a subset of <paramref name="other" />; otherwise, false.</returns>
		/// <param name="other">The collection to compare to the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is null.</exception>
		// Token: 0x0600416A RID: 16746 RVA: 0x000E3CBC File Offset: 0x000E1EBC
		[SecuritySafeCritical]
		public bool IsSubsetOf(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (this.Count == 0)
			{
				return true;
			}
			SortedSet<T> sortedSet = other as SortedSet<T>;
			if (sortedSet != null && this.HasEqualComparer(sortedSet))
			{
				return this.Count <= sortedSet.Count && this.IsSubsetOfSortedSetWithSameComparer(sortedSet);
			}
			SortedSet<T>.ElementCount elementCount = this.CheckUniqueAndUnfoundElements(other, false);
			return elementCount.UniqueCount == this.Count && elementCount.UnfoundCount >= 0;
		}

		// Token: 0x0600416B RID: 16747 RVA: 0x000E3D34 File Offset: 0x000E1F34
		private bool IsSubsetOfSortedSetWithSameComparer(SortedSet<T> asSorted)
		{
			SortedSet<T> viewBetween = asSorted.GetViewBetween(this.Min, this.Max);
			foreach (T t in this)
			{
				if (!viewBetween.Contains(t))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Determines whether a <see cref="T:System.Collections.Generic.SortedSet`1" /> object is a proper subset of the specified collection.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Generic.SortedSet`1" /> object is a proper subset of <paramref name="other" />; otherwise, false.</returns>
		/// <param name="other">The collection to compare to the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is null.</exception>
		// Token: 0x0600416C RID: 16748 RVA: 0x000E3DA0 File Offset: 0x000E1FA0
		[SecuritySafeCritical]
		public bool IsProperSubsetOf(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (other is ICollection && this.Count == 0)
			{
				return (other as ICollection).Count > 0;
			}
			SortedSet<T> sortedSet = other as SortedSet<T>;
			if (sortedSet != null && this.HasEqualComparer(sortedSet))
			{
				return this.Count < sortedSet.Count && this.IsSubsetOfSortedSetWithSameComparer(sortedSet);
			}
			SortedSet<T>.ElementCount elementCount = this.CheckUniqueAndUnfoundElements(other, false);
			return elementCount.UniqueCount == this.Count && elementCount.UnfoundCount > 0;
		}

		/// <summary>Determines whether a <see cref="T:System.Collections.Generic.SortedSet`1" /> object is a superset of the specified collection.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Generic.SortedSet`1" /> object is a superset of <paramref name="other" />; otherwise, false.</returns>
		/// <param name="other">The collection to compare to the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is null.</exception>
		// Token: 0x0600416D RID: 16749 RVA: 0x000E3E28 File Offset: 0x000E2028
		public bool IsSupersetOf(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (other is ICollection && (other as ICollection).Count == 0)
			{
				return true;
			}
			SortedSet<T> sortedSet = other as SortedSet<T>;
			if (sortedSet == null || !this.HasEqualComparer(sortedSet))
			{
				return this.ContainsAllElements(other);
			}
			if (this.Count < sortedSet.Count)
			{
				return false;
			}
			SortedSet<T> viewBetween = this.GetViewBetween(sortedSet.Min, sortedSet.Max);
			foreach (T t in sortedSet)
			{
				if (!viewBetween.Contains(t))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Determines whether a <see cref="T:System.Collections.Generic.SortedSet`1" /> object is a proper superset of the specified collection.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Generic.SortedSet`1" /> object is a proper superset of <paramref name="other" />; otherwise, false.</returns>
		/// <param name="other">The collection to compare to the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is null.</exception>
		// Token: 0x0600416E RID: 16750 RVA: 0x000E3EE4 File Offset: 0x000E20E4
		[SecuritySafeCritical]
		public bool IsProperSupersetOf(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (this.Count == 0)
			{
				return false;
			}
			if (other is ICollection && (other as ICollection).Count == 0)
			{
				return true;
			}
			SortedSet<T> sortedSet = other as SortedSet<T>;
			if (sortedSet == null || !this.HasEqualComparer(sortedSet))
			{
				SortedSet<T>.ElementCount elementCount = this.CheckUniqueAndUnfoundElements(other, true);
				return elementCount.UniqueCount < this.Count && elementCount.UnfoundCount == 0;
			}
			if (sortedSet.Count >= this.Count)
			{
				return false;
			}
			SortedSet<T> viewBetween = this.GetViewBetween(sortedSet.Min, sortedSet.Max);
			foreach (T t in sortedSet)
			{
				if (!viewBetween.Contains(t))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Determines whether the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object and the specified collection contain the same elements.</summary>
		/// <returns>true if the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object is equal to <paramref name="other" />; otherwise, false.</returns>
		/// <param name="other">The collection to compare to the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is null.</exception>
		// Token: 0x0600416F RID: 16751 RVA: 0x000E3FC8 File Offset: 0x000E21C8
		[SecuritySafeCritical]
		public bool SetEquals(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			SortedSet<T> sortedSet = other as SortedSet<T>;
			if (sortedSet != null && this.HasEqualComparer(sortedSet))
			{
				SortedSet<T>.Enumerator enumerator = this.GetEnumerator();
				SortedSet<T>.Enumerator enumerator2 = sortedSet.GetEnumerator();
				bool flag = !enumerator.MoveNext();
				bool flag2 = !enumerator2.MoveNext();
				while (!flag && !flag2)
				{
					if (this.Comparer.Compare(enumerator.Current, enumerator2.Current) != 0)
					{
						return false;
					}
					flag = !enumerator.MoveNext();
					flag2 = !enumerator2.MoveNext();
				}
				return flag && flag2;
			}
			SortedSet<T>.ElementCount elementCount = this.CheckUniqueAndUnfoundElements(other, true);
			return elementCount.UniqueCount == this.Count && elementCount.UnfoundCount == 0;
		}

		/// <summary>Determines whether the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object and a specified collection share common elements.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Generic.SortedSet`1" /> object and <paramref name="other" /> share at least one common element; otherwise, false.</returns>
		/// <param name="other">The collection to compare to the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is null.</exception>
		// Token: 0x06004170 RID: 16752 RVA: 0x000E4084 File Offset: 0x000E2284
		public bool Overlaps(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (this.Count == 0)
			{
				return false;
			}
			if (other is ICollection<T> && (other as ICollection<T>).Count == 0)
			{
				return false;
			}
			SortedSet<T> sortedSet = other as SortedSet<T>;
			if (sortedSet != null && this.HasEqualComparer(sortedSet) && (this.comparer.Compare(this.Min, sortedSet.Max) > 0 || this.comparer.Compare(this.Max, sortedSet.Min) < 0))
			{
				return false;
			}
			foreach (T t in other)
			{
				if (this.Contains(t))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06004171 RID: 16753 RVA: 0x000E4150 File Offset: 0x000E2350
		private unsafe SortedSet<T>.ElementCount CheckUniqueAndUnfoundElements(IEnumerable<T> other, bool returnIfUnfound)
		{
			SortedSet<T>.ElementCount elementCount;
			if (this.Count == 0)
			{
				int num = 0;
				using (IEnumerator<T> enumerator = other.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						T t = enumerator.Current;
						num++;
					}
				}
				elementCount.UniqueCount = 0;
				elementCount.UnfoundCount = num;
				return elementCount;
			}
			int num2 = BitHelper.ToIntArrayLength(this.Count);
			BitHelper bitHelper;
			int num3;
			int num4;
			checked
			{
				if (num2 <= 100)
				{
					bitHelper = new BitHelper(stackalloc int[unchecked((UIntPtr)num2) * 4], num2);
				}
				else
				{
					bitHelper = new BitHelper(new int[num2], num2);
				}
				num3 = 0;
				num4 = 0;
			}
			foreach (T t2 in other)
			{
				int num5 = this.InternalIndexOf(t2);
				if (num5 >= 0)
				{
					if (!bitHelper.IsMarked(num5))
					{
						bitHelper.MarkBit(num5);
						num4++;
					}
				}
				else
				{
					num3++;
					if (returnIfUnfound)
					{
						break;
					}
				}
			}
			elementCount.UniqueCount = num4;
			elementCount.UnfoundCount = num3;
			return elementCount;
		}

		/// <summary>Removes all elements that match the conditions defined by the specified predicate from a <see cref="T:System.Collections.Generic.SortedSet`1" />.</summary>
		/// <returns>The number of elements that were removed from the <see cref="T:System.Collections.Generic.SortedSet`1" /> collection.. </returns>
		/// <param name="match">The delegate that defines the conditions of the elements to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="match" /> is null.</exception>
		// Token: 0x06004172 RID: 16754 RVA: 0x000E4268 File Offset: 0x000E2468
		public int RemoveWhere(Predicate<T> match)
		{
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			List<T> matches = new List<T>(this.Count);
			this.BreadthFirstTreeWalk(delegate(SortedSet<T>.Node n)
			{
				if (match(n.Item))
				{
					matches.Add(n.Item);
				}
				return true;
			});
			int num = 0;
			for (int i = matches.Count - 1; i >= 0; i--)
			{
				if (this.Remove(matches[i]))
				{
					num++;
				}
			}
			return num;
		}

		/// <summary>Gets the minimum value in the <see cref="T:System.Collections.Generic.SortedSet`1" />, as defined by the comparer.</summary>
		/// <returns>The minimum value in the set.</returns>
		// Token: 0x17000EF1 RID: 3825
		// (get) Token: 0x06004173 RID: 16755 RVA: 0x000E42EC File Offset: 0x000E24EC
		public T Min
		{
			get
			{
				return this.MinInternal;
			}
		}

		// Token: 0x17000EF2 RID: 3826
		// (get) Token: 0x06004174 RID: 16756 RVA: 0x000E42F4 File Offset: 0x000E24F4
		internal virtual T MinInternal
		{
			get
			{
				if (this.root == null)
				{
					return default(T);
				}
				SortedSet<T>.Node left = this.root;
				while (left.Left != null)
				{
					left = left.Left;
				}
				return left.Item;
			}
		}

		/// <summary>Gets the maximum value in the <see cref="T:System.Collections.Generic.SortedSet`1" />, as defined by the comparer.</summary>
		/// <returns>The maximum value in the set.</returns>
		// Token: 0x17000EF3 RID: 3827
		// (get) Token: 0x06004175 RID: 16757 RVA: 0x000E4331 File Offset: 0x000E2531
		public T Max
		{
			get
			{
				return this.MaxInternal;
			}
		}

		// Token: 0x17000EF4 RID: 3828
		// (get) Token: 0x06004176 RID: 16758 RVA: 0x000E433C File Offset: 0x000E253C
		internal virtual T MaxInternal
		{
			get
			{
				if (this.root == null)
				{
					return default(T);
				}
				SortedSet<T>.Node right = this.root;
				while (right.Right != null)
				{
					right = right.Right;
				}
				return right.Item;
			}
		}

		/// <summary>Returns an <see cref="T:System.Collections.Generic.IEnumerable`1" /> that iterates over the <see cref="T:System.Collections.Generic.SortedSet`1" /> in reverse order.</summary>
		/// <returns>An enumerator that iterates over the <see cref="T:System.Collections.Generic.SortedSet`1" /> in reverse order.</returns>
		// Token: 0x06004177 RID: 16759 RVA: 0x000E4379 File Offset: 0x000E2579
		public IEnumerable<T> Reverse()
		{
			SortedSet<T>.Enumerator e = new SortedSet<T>.Enumerator(this, true);
			while (e.MoveNext())
			{
				T t = e.Current;
				yield return t;
			}
			yield break;
		}

		/// <summary>Returns a view of a subset in a <see cref="T:System.Collections.Generic.SortedSet`1" />.</summary>
		/// <returns>A subset view that contains only the values in the specified range.</returns>
		/// <param name="lowerValue">The lowest desired value in the view.</param>
		/// <param name="upperValue">The highest desired value in the view. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="lowerValue" /> is more than <paramref name="upperValue" /> according to the comparer.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">A tried operation on the view was outside the range specified by <paramref name="lowerValue" /> and <paramref name="upperValue" />.</exception>
		// Token: 0x06004178 RID: 16760 RVA: 0x000E4389 File Offset: 0x000E2589
		public virtual SortedSet<T> GetViewBetween(T lowerValue, T upperValue)
		{
			if (this.Comparer.Compare(lowerValue, upperValue) > 0)
			{
				throw new ArgumentException("Must be less than or equal to upperValue.", "lowerValue");
			}
			return new SortedSet<T>.TreeSubSet(this, lowerValue, upperValue, true, true);
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface, and returns the data that you need to serialize the <see cref="T:System.Collections.Generic.SortedSet`1" /> instance.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains the information that is required to serialize the <see cref="T:System.Collections.Generic.SortedSet`1" /> instance.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> structure that contains the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Generic.SortedSet`1" /> instance.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null.</exception>
		// Token: 0x06004179 RID: 16761 RVA: 0x000E43B5 File Offset: 0x000E25B5
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			this.GetObjectData(info, context);
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and returns the data that you must have to serialize a <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains the information that is required to serialize the <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> structure that contains the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null.</exception>
		// Token: 0x0600417A RID: 16762 RVA: 0x000E43C0 File Offset: 0x000E25C0
		protected virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("Count", this.count);
			info.AddValue("Comparer", this.comparer, typeof(IComparer<T>));
			info.AddValue("Version", this.version);
			if (this.root != null)
			{
				T[] array = new T[this.Count];
				this.CopyTo(array, 0);
				info.AddValue("Items", array, typeof(T[]));
			}
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.IDeserializationCallback" /> interface, and raises the deserialization event when the deserialization is completed.</summary>
		/// <param name="sender">The source of the deserialization event.</param>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object associated with the current <see cref="T:System.Collections.Generic.SortedSet`1" /> instance is invalid.</exception>
		// Token: 0x0600417B RID: 16763 RVA: 0x000E444A File Offset: 0x000E264A
		void IDeserializationCallback.OnDeserialization(object sender)
		{
			this.OnDeserialization(sender);
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface, and raises the deserialization event when the deserialization is completed.</summary>
		/// <param name="sender">The source of the deserialization event.</param>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object associated with the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object is invalid.</exception>
		// Token: 0x0600417C RID: 16764 RVA: 0x000E4454 File Offset: 0x000E2654
		protected virtual void OnDeserialization(object sender)
		{
			if (this.comparer != null)
			{
				return;
			}
			if (this.siInfo == null)
			{
				throw new SerializationException("OnDeserialization method was called while the object was not being deserialized.");
			}
			this.comparer = (IComparer<T>)this.siInfo.GetValue("Comparer", typeof(IComparer<T>));
			int @int = this.siInfo.GetInt32("Count");
			if (@int != 0)
			{
				T[] array = (T[])this.siInfo.GetValue("Items", typeof(T[]));
				if (array == null)
				{
					throw new SerializationException("The values for this dictionary are missing.");
				}
				for (int i = 0; i < array.Length; i++)
				{
					this.Add(array[i]);
				}
			}
			this.version = this.siInfo.GetInt32("Version");
			if (this.count != @int)
			{
				throw new SerializationException("The serialized Count information doesn't match the number of items.");
			}
			this.siInfo = null;
		}

		// Token: 0x0600417D RID: 16765 RVA: 0x000E4534 File Offset: 0x000E2734
		public bool TryGetValue(T equalValue, out T actualValue)
		{
			SortedSet<T>.Node node = this.FindNode(equalValue);
			if (node != null)
			{
				actualValue = node.Item;
				return true;
			}
			actualValue = default(T);
			return false;
		}

		// Token: 0x0600417E RID: 16766 RVA: 0x000E4564 File Offset: 0x000E2764
		private static int Log2(int value)
		{
			int num = 0;
			while (value > 0)
			{
				num++;
				value >>= 1;
			}
			return num;
		}

		// Token: 0x04002710 RID: 10000
		private SortedSet<T>.Node root;

		// Token: 0x04002711 RID: 10001
		private IComparer<T> comparer;

		// Token: 0x04002712 RID: 10002
		private int count;

		// Token: 0x04002713 RID: 10003
		private int version;

		// Token: 0x04002714 RID: 10004
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x04002715 RID: 10005
		private SerializationInfo siInfo;

		// Token: 0x04002716 RID: 10006
		private const string ComparerName = "Comparer";

		// Token: 0x04002717 RID: 10007
		private const string CountName = "Count";

		// Token: 0x04002718 RID: 10008
		private const string ItemsName = "Items";

		// Token: 0x04002719 RID: 10009
		private const string VersionName = "Version";

		// Token: 0x0400271A RID: 10010
		private const string TreeName = "Tree";

		// Token: 0x0400271B RID: 10011
		private const string NodeValueName = "Item";

		// Token: 0x0400271C RID: 10012
		private const string EnumStartName = "EnumStarted";

		// Token: 0x0400271D RID: 10013
		private const string ReverseName = "Reverse";

		// Token: 0x0400271E RID: 10014
		private const string EnumVersionName = "EnumVersion";

		// Token: 0x0400271F RID: 10015
		private const string MinName = "Min";

		// Token: 0x04002720 RID: 10016
		private const string MaxName = "Max";

		// Token: 0x04002721 RID: 10017
		private const string LowerBoundActiveName = "lBoundActive";

		// Token: 0x04002722 RID: 10018
		private const string UpperBoundActiveName = "uBoundActive";

		// Token: 0x04002723 RID: 10019
		internal const int StackAllocThreshold = 100;

		// Token: 0x020007F7 RID: 2039
		[Serializable]
		internal sealed class TreeSubSet : SortedSet<T>, ISerializable, IDeserializationCallback
		{
			// Token: 0x0600417F RID: 16767 RVA: 0x000E4584 File Offset: 0x000E2784
			public TreeSubSet(SortedSet<T> Underlying, T Min, T Max, bool lowerBoundActive, bool upperBoundActive)
				: base(Underlying.Comparer)
			{
				this._underlying = Underlying;
				this._min = Min;
				this._max = Max;
				this._lBoundActive = lowerBoundActive;
				this._uBoundActive = upperBoundActive;
				this.root = this._underlying.FindRange(this._min, this._max, this._lBoundActive, this._uBoundActive);
				this.count = 0;
				this.version = -1;
				this.VersionCheckImpl();
			}

			// Token: 0x06004180 RID: 16768 RVA: 0x000E45FF File Offset: 0x000E27FF
			internal override bool AddIfNotPresent(T item)
			{
				if (!this.IsWithinRange(item))
				{
					throw new ArgumentOutOfRangeException("item");
				}
				bool flag = this._underlying.AddIfNotPresent(item);
				this.VersionCheck();
				return flag;
			}

			// Token: 0x06004181 RID: 16769 RVA: 0x000E4627 File Offset: 0x000E2827
			public override bool Contains(T item)
			{
				this.VersionCheck();
				return base.Contains(item);
			}

			// Token: 0x06004182 RID: 16770 RVA: 0x000E4636 File Offset: 0x000E2836
			internal override bool DoRemove(T item)
			{
				if (!this.IsWithinRange(item))
				{
					return false;
				}
				bool flag = this._underlying.Remove(item);
				this.VersionCheck();
				return flag;
			}

			// Token: 0x06004183 RID: 16771 RVA: 0x000E4658 File Offset: 0x000E2858
			public override void Clear()
			{
				if (this.count == 0)
				{
					return;
				}
				List<T> toRemove = new List<T>();
				this.BreadthFirstTreeWalk(delegate(SortedSet<T>.Node n)
				{
					toRemove.Add(n.Item);
					return true;
				});
				while (toRemove.Count != 0)
				{
					this._underlying.Remove(toRemove[toRemove.Count - 1]);
					toRemove.RemoveAt(toRemove.Count - 1);
				}
				this.root = null;
				this.count = 0;
				this.version = this._underlying.version;
			}

			// Token: 0x06004184 RID: 16772 RVA: 0x000E46FC File Offset: 0x000E28FC
			internal override bool IsWithinRange(T item)
			{
				return (this._lBoundActive ? base.Comparer.Compare(this._min, item) : (-1)) <= 0 && (this._uBoundActive ? base.Comparer.Compare(this._max, item) : 1) >= 0;
			}

			// Token: 0x17000EF5 RID: 3829
			// (get) Token: 0x06004185 RID: 16773 RVA: 0x000E4750 File Offset: 0x000E2950
			internal override T MinInternal
			{
				get
				{
					SortedSet<T>.Node node = this.root;
					T t = default(T);
					while (node != null)
					{
						int num = (this._lBoundActive ? base.Comparer.Compare(this._min, node.Item) : (-1));
						if (num == 1)
						{
							node = node.Right;
						}
						else
						{
							t = node.Item;
							if (num == 0)
							{
								break;
							}
							node = node.Left;
						}
					}
					return t;
				}
			}

			// Token: 0x17000EF6 RID: 3830
			// (get) Token: 0x06004186 RID: 16774 RVA: 0x000E47B4 File Offset: 0x000E29B4
			internal override T MaxInternal
			{
				get
				{
					SortedSet<T>.Node node = this.root;
					T t = default(T);
					while (node != null)
					{
						int num = (this._uBoundActive ? base.Comparer.Compare(this._max, node.Item) : 1);
						if (num == -1)
						{
							node = node.Left;
						}
						else
						{
							t = node.Item;
							if (num == 0)
							{
								break;
							}
							node = node.Right;
						}
					}
					return t;
				}
			}

			// Token: 0x06004187 RID: 16775 RVA: 0x000E4818 File Offset: 0x000E2A18
			internal override bool InOrderTreeWalk(TreeWalkPredicate<T> action)
			{
				this.VersionCheck();
				if (this.root == null)
				{
					return true;
				}
				Stack<SortedSet<T>.Node> stack = new Stack<SortedSet<T>.Node>(2 * SortedSet<T>.Log2(this.count + 1));
				SortedSet<T>.Node node = this.root;
				while (node != null)
				{
					if (this.IsWithinRange(node.Item))
					{
						stack.Push(node);
						node = node.Left;
					}
					else if (this._lBoundActive && base.Comparer.Compare(this._min, node.Item) > 0)
					{
						node = node.Right;
					}
					else
					{
						node = node.Left;
					}
				}
				while (stack.Count != 0)
				{
					node = stack.Pop();
					if (!action(node))
					{
						return false;
					}
					SortedSet<T>.Node node2 = node.Right;
					while (node2 != null)
					{
						if (this.IsWithinRange(node2.Item))
						{
							stack.Push(node2);
							node2 = node2.Left;
						}
						else if (this._lBoundActive && base.Comparer.Compare(this._min, node2.Item) > 0)
						{
							node2 = node2.Right;
						}
						else
						{
							node2 = node2.Left;
						}
					}
				}
				return true;
			}

			// Token: 0x06004188 RID: 16776 RVA: 0x000E4920 File Offset: 0x000E2B20
			internal override bool BreadthFirstTreeWalk(TreeWalkPredicate<T> action)
			{
				this.VersionCheck();
				if (this.root == null)
				{
					return true;
				}
				Queue<SortedSet<T>.Node> queue = new Queue<SortedSet<T>.Node>();
				queue.Enqueue(this.root);
				while (queue.Count != 0)
				{
					SortedSet<T>.Node node = queue.Dequeue();
					if (this.IsWithinRange(node.Item) && !action(node))
					{
						return false;
					}
					if (node.Left != null && (!this._lBoundActive || base.Comparer.Compare(this._min, node.Item) < 0))
					{
						queue.Enqueue(node.Left);
					}
					if (node.Right != null && (!this._uBoundActive || base.Comparer.Compare(this._max, node.Item) > 0))
					{
						queue.Enqueue(node.Right);
					}
				}
				return true;
			}

			// Token: 0x06004189 RID: 16777 RVA: 0x000E49EC File Offset: 0x000E2BEC
			internal override SortedSet<T>.Node FindNode(T item)
			{
				if (!this.IsWithinRange(item))
				{
					return null;
				}
				this.VersionCheck();
				return base.FindNode(item);
			}

			// Token: 0x0600418A RID: 16778 RVA: 0x000E4A08 File Offset: 0x000E2C08
			internal override int InternalIndexOf(T item)
			{
				int num = -1;
				foreach (T t in this)
				{
					num++;
					if (base.Comparer.Compare(item, t) == 0)
					{
						return num;
					}
				}
				return -1;
			}

			// Token: 0x0600418B RID: 16779 RVA: 0x000E4A6C File Offset: 0x000E2C6C
			internal override void VersionCheck()
			{
				this.VersionCheckImpl();
			}

			// Token: 0x0600418C RID: 16780 RVA: 0x000E4A74 File Offset: 0x000E2C74
			private void VersionCheckImpl()
			{
				if (this.version != this._underlying.version)
				{
					this.root = this._underlying.FindRange(this._min, this._max, this._lBoundActive, this._uBoundActive);
					this.version = this._underlying.version;
					this.count = 0;
					this.InOrderTreeWalk(delegate(SortedSet<T>.Node n)
					{
						this.count++;
						return true;
					});
				}
			}

			// Token: 0x0600418D RID: 16781 RVA: 0x000E4AE8 File Offset: 0x000E2CE8
			public override SortedSet<T> GetViewBetween(T lowerValue, T upperValue)
			{
				if (this._lBoundActive && base.Comparer.Compare(this._min, lowerValue) > 0)
				{
					throw new ArgumentOutOfRangeException("lowerValue");
				}
				if (this._uBoundActive && base.Comparer.Compare(this._max, upperValue) < 0)
				{
					throw new ArgumentOutOfRangeException("upperValue");
				}
				return (SortedSet<T>.TreeSubSet)this._underlying.GetViewBetween(lowerValue, upperValue);
			}

			// Token: 0x0600418E RID: 16782 RVA: 0x000E43B5 File Offset: 0x000E25B5
			void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
			{
				this.GetObjectData(info, context);
			}

			// Token: 0x0600418F RID: 16783 RVA: 0x00011EB0 File Offset: 0x000100B0
			protected override void GetObjectData(SerializationInfo info, StreamingContext context)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06004190 RID: 16784 RVA: 0x00011EB0 File Offset: 0x000100B0
			void IDeserializationCallback.OnDeserialization(object sender)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06004191 RID: 16785 RVA: 0x00011EB0 File Offset: 0x000100B0
			protected override void OnDeserialization(object sender)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x04002724 RID: 10020
			private SortedSet<T> _underlying;

			// Token: 0x04002725 RID: 10021
			private T _min;

			// Token: 0x04002726 RID: 10022
			private T _max;

			// Token: 0x04002727 RID: 10023
			private bool _lBoundActive;

			// Token: 0x04002728 RID: 10024
			private bool _uBoundActive;
		}

		// Token: 0x020007F9 RID: 2041
		[Serializable]
		internal sealed class Node
		{
			// Token: 0x06004195 RID: 16789 RVA: 0x000E4B7C File Offset: 0x000E2D7C
			public Node(T item, NodeColor color)
			{
				this.Item = item;
				this.Color = color;
			}

			// Token: 0x06004196 RID: 16790 RVA: 0x000E4B92 File Offset: 0x000E2D92
			public static bool IsNonNullBlack(SortedSet<T>.Node node)
			{
				return node != null && node.IsBlack;
			}

			// Token: 0x06004197 RID: 16791 RVA: 0x000E4B9F File Offset: 0x000E2D9F
			public static bool IsNonNullRed(SortedSet<T>.Node node)
			{
				return node != null && node.IsRed;
			}

			// Token: 0x06004198 RID: 16792 RVA: 0x000E4BAC File Offset: 0x000E2DAC
			public static bool IsNullOrBlack(SortedSet<T>.Node node)
			{
				return node == null || node.IsBlack;
			}

			// Token: 0x17000EF7 RID: 3831
			// (get) Token: 0x06004199 RID: 16793 RVA: 0x000E4BB9 File Offset: 0x000E2DB9
			// (set) Token: 0x0600419A RID: 16794 RVA: 0x000E4BC1 File Offset: 0x000E2DC1
			public T Item { get; set; }

			// Token: 0x17000EF8 RID: 3832
			// (get) Token: 0x0600419B RID: 16795 RVA: 0x000E4BCA File Offset: 0x000E2DCA
			// (set) Token: 0x0600419C RID: 16796 RVA: 0x000E4BD2 File Offset: 0x000E2DD2
			public SortedSet<T>.Node Left { get; set; }

			// Token: 0x17000EF9 RID: 3833
			// (get) Token: 0x0600419D RID: 16797 RVA: 0x000E4BDB File Offset: 0x000E2DDB
			// (set) Token: 0x0600419E RID: 16798 RVA: 0x000E4BE3 File Offset: 0x000E2DE3
			public SortedSet<T>.Node Right { get; set; }

			// Token: 0x17000EFA RID: 3834
			// (get) Token: 0x0600419F RID: 16799 RVA: 0x000E4BEC File Offset: 0x000E2DEC
			// (set) Token: 0x060041A0 RID: 16800 RVA: 0x000E4BF4 File Offset: 0x000E2DF4
			public NodeColor Color { get; set; }

			// Token: 0x17000EFB RID: 3835
			// (get) Token: 0x060041A1 RID: 16801 RVA: 0x000E4BFD File Offset: 0x000E2DFD
			public bool IsBlack
			{
				get
				{
					return this.Color == NodeColor.Black;
				}
			}

			// Token: 0x17000EFC RID: 3836
			// (get) Token: 0x060041A2 RID: 16802 RVA: 0x000E4C08 File Offset: 0x000E2E08
			public bool IsRed
			{
				get
				{
					return this.Color == NodeColor.Red;
				}
			}

			// Token: 0x17000EFD RID: 3837
			// (get) Token: 0x060041A3 RID: 16803 RVA: 0x000E4C13 File Offset: 0x000E2E13
			public bool Is2Node
			{
				get
				{
					return this.IsBlack && SortedSet<T>.Node.IsNullOrBlack(this.Left) && SortedSet<T>.Node.IsNullOrBlack(this.Right);
				}
			}

			// Token: 0x17000EFE RID: 3838
			// (get) Token: 0x060041A4 RID: 16804 RVA: 0x000E4C37 File Offset: 0x000E2E37
			public bool Is4Node
			{
				get
				{
					return SortedSet<T>.Node.IsNonNullRed(this.Left) && SortedSet<T>.Node.IsNonNullRed(this.Right);
				}
			}

			// Token: 0x060041A5 RID: 16805 RVA: 0x000E4C53 File Offset: 0x000E2E53
			public void ColorBlack()
			{
				this.Color = NodeColor.Black;
			}

			// Token: 0x060041A6 RID: 16806 RVA: 0x000E4C5C File Offset: 0x000E2E5C
			public void ColorRed()
			{
				this.Color = NodeColor.Red;
			}

			// Token: 0x060041A7 RID: 16807 RVA: 0x000E4C68 File Offset: 0x000E2E68
			public SortedSet<T>.Node DeepClone(int count)
			{
				Stack<SortedSet<T>.Node> stack = new Stack<SortedSet<T>.Node>(2 * SortedSet<T>.Log2(count) + 2);
				Stack<SortedSet<T>.Node> stack2 = new Stack<SortedSet<T>.Node>(2 * SortedSet<T>.Log2(count) + 2);
				SortedSet<T>.Node node = this.ShallowClone();
				SortedSet<T>.Node node2 = this;
				SortedSet<T>.Node node3 = node;
				while (node2 != null)
				{
					stack.Push(node2);
					stack2.Push(node3);
					SortedSet<T>.Node node4 = node3;
					SortedSet<T>.Node left = node2.Left;
					node4.Left = ((left != null) ? left.ShallowClone() : null);
					node2 = node2.Left;
					node3 = node3.Left;
				}
				while (stack.Count != 0)
				{
					node2 = stack.Pop();
					node3 = stack2.Pop();
					SortedSet<T>.Node node5 = node2.Right;
					SortedSet<T>.Node node6 = ((node5 != null) ? node5.ShallowClone() : null);
					node3.Right = node6;
					while (node5 != null)
					{
						stack.Push(node5);
						stack2.Push(node6);
						SortedSet<T>.Node node7 = node6;
						SortedSet<T>.Node left2 = node5.Left;
						node7.Left = ((left2 != null) ? left2.ShallowClone() : null);
						node5 = node5.Left;
						node6 = node6.Left;
					}
				}
				return node;
			}

			// Token: 0x060041A8 RID: 16808 RVA: 0x000E4D5C File Offset: 0x000E2F5C
			public TreeRotation GetRotation(SortedSet<T>.Node current, SortedSet<T>.Node sibling)
			{
				bool flag = this.Left == current;
				if (!SortedSet<T>.Node.IsNonNullRed(sibling.Left))
				{
					if (!flag)
					{
						return TreeRotation.LeftRight;
					}
					return TreeRotation.Left;
				}
				else
				{
					if (!flag)
					{
						return TreeRotation.Right;
					}
					return TreeRotation.RightLeft;
				}
			}

			// Token: 0x060041A9 RID: 16809 RVA: 0x000E4D8D File Offset: 0x000E2F8D
			public SortedSet<T>.Node GetSibling(SortedSet<T>.Node node)
			{
				if (node != this.Left)
				{
					return this.Left;
				}
				return this.Right;
			}

			// Token: 0x060041AA RID: 16810 RVA: 0x000E4DA5 File Offset: 0x000E2FA5
			public SortedSet<T>.Node ShallowClone()
			{
				return new SortedSet<T>.Node(this.Item, this.Color);
			}

			// Token: 0x060041AB RID: 16811 RVA: 0x000E4DB8 File Offset: 0x000E2FB8
			public void Split4Node()
			{
				this.ColorRed();
				this.Left.ColorBlack();
				this.Right.ColorBlack();
			}

			// Token: 0x060041AC RID: 16812 RVA: 0x000E4DD8 File Offset: 0x000E2FD8
			public SortedSet<T>.Node Rotate(TreeRotation rotation)
			{
				switch (rotation)
				{
				case TreeRotation.Left:
					this.Right.Right.ColorBlack();
					return this.RotateLeft();
				case TreeRotation.LeftRight:
					return this.RotateLeftRight();
				case TreeRotation.Right:
					this.Left.Left.ColorBlack();
					return this.RotateRight();
				case TreeRotation.RightLeft:
					return this.RotateRightLeft();
				default:
					return null;
				}
			}

			// Token: 0x060041AD RID: 16813 RVA: 0x000E4E3C File Offset: 0x000E303C
			public SortedSet<T>.Node RotateLeft()
			{
				SortedSet<T>.Node right = this.Right;
				this.Right = right.Left;
				right.Left = this;
				return right;
			}

			// Token: 0x060041AE RID: 16814 RVA: 0x000E4E64 File Offset: 0x000E3064
			public SortedSet<T>.Node RotateLeftRight()
			{
				SortedSet<T>.Node left = this.Left;
				SortedSet<T>.Node right = left.Right;
				this.Left = right.Right;
				right.Right = this;
				left.Right = right.Left;
				right.Left = left;
				return right;
			}

			// Token: 0x060041AF RID: 16815 RVA: 0x000E4EA8 File Offset: 0x000E30A8
			public SortedSet<T>.Node RotateRight()
			{
				SortedSet<T>.Node left = this.Left;
				this.Left = left.Right;
				left.Right = this;
				return left;
			}

			// Token: 0x060041B0 RID: 16816 RVA: 0x000E4ED0 File Offset: 0x000E30D0
			public SortedSet<T>.Node RotateRightLeft()
			{
				SortedSet<T>.Node right = this.Right;
				SortedSet<T>.Node left = right.Left;
				this.Right = left.Left;
				left.Left = this;
				right.Left = left.Right;
				left.Right = right;
				return left;
			}

			// Token: 0x060041B1 RID: 16817 RVA: 0x000E4F12 File Offset: 0x000E3112
			public void Merge2Nodes()
			{
				this.ColorBlack();
				this.Left.ColorRed();
				this.Right.ColorRed();
			}

			// Token: 0x060041B2 RID: 16818 RVA: 0x000E4F30 File Offset: 0x000E3130
			public void ReplaceChild(SortedSet<T>.Node child, SortedSet<T>.Node newChild)
			{
				if (this.Left == child)
				{
					this.Left = newChild;
					return;
				}
				this.Right = newChild;
			}
		}

		/// <summary>Enumerates the elements of a <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</summary>
		// Token: 0x020007FA RID: 2042
		[Serializable]
		public struct Enumerator : IEnumerator<T>, IDisposable, IEnumerator, ISerializable, IDeserializationCallback
		{
			// Token: 0x060041B3 RID: 16819 RVA: 0x000E4F4A File Offset: 0x000E314A
			internal Enumerator(SortedSet<T> set)
			{
				this = new SortedSet<T>.Enumerator(set, false);
			}

			// Token: 0x060041B4 RID: 16820 RVA: 0x000E4F54 File Offset: 0x000E3154
			internal Enumerator(SortedSet<T> set, bool reverse)
			{
				this._tree = set;
				set.VersionCheck();
				this._version = set.version;
				this._stack = new Stack<SortedSet<T>.Node>(2 * SortedSet<T>.Log2(set.Count + 1));
				this._current = null;
				this._reverse = reverse;
				this.Initialize();
			}

			/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and returns the data needed to serialize the <see cref="T:System.Collections.Generic.SortedSet`1" /> instance.</summary>
			/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains the information required to serialize the <see cref="T:System.Collections.Generic.SortedSet`1" /> instance.</param>
			/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Generic.SortedSet`1" /> instance.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="info" /> is null.</exception>
			// Token: 0x060041B5 RID: 16821 RVA: 0x00011EB0 File Offset: 0x000100B0
			void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
			{
				throw new PlatformNotSupportedException();
			}

			/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and raises the deserialization event when the deserialization is complete.</summary>
			/// <param name="sender">The source of the deserialization event.</param>
			/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object associated with the current <see cref="T:System.Collections.Generic.SortedSet`1" /> instance is invalid.</exception>
			// Token: 0x060041B6 RID: 16822 RVA: 0x00011EB0 File Offset: 0x000100B0
			void IDeserializationCallback.OnDeserialization(object sender)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x060041B7 RID: 16823 RVA: 0x000E4FA8 File Offset: 0x000E31A8
			private void Initialize()
			{
				this._current = null;
				SortedSet<T>.Node node = this._tree.root;
				while (node != null)
				{
					SortedSet<T>.Node node2 = (this._reverse ? node.Right : node.Left);
					SortedSet<T>.Node node3 = (this._reverse ? node.Left : node.Right);
					if (this._tree.IsWithinRange(node.Item))
					{
						this._stack.Push(node);
						node = node2;
					}
					else if (node2 == null || !this._tree.IsWithinRange(node2.Item))
					{
						node = node3;
					}
					else
					{
						node = node2;
					}
				}
			}

			/// <summary>Advances the enumerator to the next element of the <see cref="T:System.Collections.Generic.SortedSet`1" /> collection.</summary>
			/// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
			/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
			// Token: 0x060041B8 RID: 16824 RVA: 0x000E5040 File Offset: 0x000E3240
			public bool MoveNext()
			{
				this._tree.VersionCheck();
				if (this._version != this._tree.version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				if (this._stack.Count == 0)
				{
					this._current = null;
					return false;
				}
				this._current = this._stack.Pop();
				SortedSet<T>.Node node = (this._reverse ? this._current.Left : this._current.Right);
				while (node != null)
				{
					SortedSet<T>.Node node2 = (this._reverse ? node.Right : node.Left);
					SortedSet<T>.Node node3 = (this._reverse ? node.Left : node.Right);
					if (this._tree.IsWithinRange(node.Item))
					{
						this._stack.Push(node);
						node = node2;
					}
					else if (node3 == null || !this._tree.IsWithinRange(node3.Item))
					{
						node = node2;
					}
					else
					{
						node = node3;
					}
				}
				return true;
			}

			/// <summary>Releases all resources used by the <see cref="T:System.Collections.Generic.SortedSet`1.Enumerator" />. </summary>
			// Token: 0x060041B9 RID: 16825 RVA: 0x00003917 File Offset: 0x00001B17
			public void Dispose()
			{
			}

			/// <summary>Gets the element at the current position of the enumerator.</summary>
			/// <returns>The element in the collection at the current position of the enumerator.</returns>
			// Token: 0x17000EFF RID: 3839
			// (get) Token: 0x060041BA RID: 16826 RVA: 0x000E5138 File Offset: 0x000E3338
			public T Current
			{
				get
				{
					if (this._current != null)
					{
						return this._current.Item;
					}
					return default(T);
				}
			}

			/// <summary>Gets the element at the current position of the enumerator.</summary>
			/// <returns>The element in the collection at the current position of the enumerator.</returns>
			/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
			// Token: 0x17000F00 RID: 3840
			// (get) Token: 0x060041BB RID: 16827 RVA: 0x000E5162 File Offset: 0x000E3362
			object IEnumerator.Current
			{
				get
				{
					if (this._current == null)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return this._current.Item;
				}
			}

			// Token: 0x17000F01 RID: 3841
			// (get) Token: 0x060041BC RID: 16828 RVA: 0x000E5187 File Offset: 0x000E3387
			internal bool NotStartedOrEnded
			{
				get
				{
					return this._current == null;
				}
			}

			// Token: 0x060041BD RID: 16829 RVA: 0x000E5192 File Offset: 0x000E3392
			internal void Reset()
			{
				if (this._version != this._tree.version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				this._stack.Clear();
				this.Initialize();
			}

			/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
			/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
			// Token: 0x060041BE RID: 16830 RVA: 0x000E51C3 File Offset: 0x000E33C3
			void IEnumerator.Reset()
			{
				this.Reset();
			}

			// Token: 0x0400272E RID: 10030
			private static readonly SortedSet<T>.Node s_dummyNode = new SortedSet<T>.Node(default(T), NodeColor.Red);

			// Token: 0x0400272F RID: 10031
			private SortedSet<T> _tree;

			// Token: 0x04002730 RID: 10032
			private int _version;

			// Token: 0x04002731 RID: 10033
			private Stack<SortedSet<T>.Node> _stack;

			// Token: 0x04002732 RID: 10034
			private SortedSet<T>.Node _current;

			// Token: 0x04002733 RID: 10035
			private bool _reverse;
		}

		// Token: 0x020007FB RID: 2043
		internal struct ElementCount
		{
			// Token: 0x04002734 RID: 10036
			internal int UniqueCount;

			// Token: 0x04002735 RID: 10037
			internal int UnfoundCount;
		}
	}
}
