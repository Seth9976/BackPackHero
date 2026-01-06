using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace System.Collections.ObjectModel
{
	/// <summary>Represents a dynamic data collection that provides notifications when items get added, removed, or when the whole list is refreshed.</summary>
	/// <typeparam name="T">The type of elements in the collection.</typeparam>
	// Token: 0x020007AA RID: 1962
	[DebuggerTypeProxy(typeof(global::System.Collections.Generic.CollectionDebugView<>))]
	[DebuggerDisplay("Count = {Count}")]
	[Serializable]
	public class ObservableCollection<T> : Collection<T>, INotifyCollectionChanged, INotifyPropertyChanged
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.ObjectModel.ObservableCollection`1" /> class.</summary>
		// Token: 0x06003DE6 RID: 15846 RVA: 0x000D9FC2 File Offset: 0x000D81C2
		public ObservableCollection()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.ObjectModel.ObservableCollection`1" /> class that contains elements copied from the specified collection.</summary>
		/// <param name="collection">The collection from which the elements are copied.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="collection" /> parameter cannot be null.</exception>
		// Token: 0x06003DE7 RID: 15847 RVA: 0x000D9FCA File Offset: 0x000D81CA
		public ObservableCollection(IEnumerable<T> collection)
			: base(ObservableCollection<T>.CreateCopy(collection, "collection"))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.ObjectModel.ObservableCollection`1" /> class that contains elements copied from the specified list.</summary>
		/// <param name="list">The list from which the elements are copied.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="list" /> parameter cannot be null.</exception>
		// Token: 0x06003DE8 RID: 15848 RVA: 0x000D9FDD File Offset: 0x000D81DD
		public ObservableCollection(List<T> list)
			: base(ObservableCollection<T>.CreateCopy(list, "list"))
		{
		}

		// Token: 0x06003DE9 RID: 15849 RVA: 0x000D9FF0 File Offset: 0x000D81F0
		private static List<T> CreateCopy(IEnumerable<T> collection, string paramName)
		{
			if (collection == null)
			{
				throw new ArgumentNullException(paramName);
			}
			return new List<T>(collection);
		}

		/// <summary>Moves the item at the specified index to a new location in the collection.</summary>
		/// <param name="oldIndex">The zero-based index specifying the location of the item to be moved.</param>
		/// <param name="newIndex">The zero-based index specifying the new location of the item.</param>
		// Token: 0x06003DEA RID: 15850 RVA: 0x000DA002 File Offset: 0x000D8202
		public void Move(int oldIndex, int newIndex)
		{
			this.MoveItem(oldIndex, newIndex);
		}

		/// <summary>Occurs when a property value changes.</summary>
		// Token: 0x14000063 RID: 99
		// (add) Token: 0x06003DEB RID: 15851 RVA: 0x000DA00C File Offset: 0x000D820C
		// (remove) Token: 0x06003DEC RID: 15852 RVA: 0x000DA015 File Offset: 0x000D8215
		event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
		{
			add
			{
				this.PropertyChanged += value;
			}
			remove
			{
				this.PropertyChanged -= value;
			}
		}

		/// <summary>Occurs when an item is added, removed, changed, moved, or the entire list is refreshed.</summary>
		// Token: 0x14000064 RID: 100
		// (add) Token: 0x06003DED RID: 15853 RVA: 0x000DA020 File Offset: 0x000D8220
		// (remove) Token: 0x06003DEE RID: 15854 RVA: 0x000DA058 File Offset: 0x000D8258
		[field: NonSerialized]
		public virtual event NotifyCollectionChangedEventHandler CollectionChanged;

		/// <summary>Removes all items from the collection.</summary>
		// Token: 0x06003DEF RID: 15855 RVA: 0x000DA08D File Offset: 0x000D828D
		protected override void ClearItems()
		{
			this.CheckReentrancy();
			base.ClearItems();
			this.OnCountPropertyChanged();
			this.OnIndexerPropertyChanged();
			this.OnCollectionReset();
		}

		/// <summary>Removes the item at the specified index of the collection.</summary>
		/// <param name="index">The zero-based index of the element to remove.</param>
		// Token: 0x06003DF0 RID: 15856 RVA: 0x000DA0B0 File Offset: 0x000D82B0
		protected override void RemoveItem(int index)
		{
			this.CheckReentrancy();
			T t = base[index];
			base.RemoveItem(index);
			this.OnCountPropertyChanged();
			this.OnIndexerPropertyChanged();
			this.OnCollectionChanged(NotifyCollectionChangedAction.Remove, t, index);
		}

		/// <summary>Inserts an item into the collection at the specified index.</summary>
		/// <param name="index">The zero-based index at which <paramref name="item" /> should be inserted.</param>
		/// <param name="item">The object to insert.</param>
		// Token: 0x06003DF1 RID: 15857 RVA: 0x000DA0EC File Offset: 0x000D82EC
		protected override void InsertItem(int index, T item)
		{
			this.CheckReentrancy();
			base.InsertItem(index, item);
			this.OnCountPropertyChanged();
			this.OnIndexerPropertyChanged();
			this.OnCollectionChanged(NotifyCollectionChangedAction.Add, item, index);
		}

		/// <summary>Replaces the element at the specified index.</summary>
		/// <param name="index">The zero-based index of the element to replace.</param>
		/// <param name="item">The new value for the element at the specified index.</param>
		// Token: 0x06003DF2 RID: 15858 RVA: 0x000DA118 File Offset: 0x000D8318
		protected override void SetItem(int index, T item)
		{
			this.CheckReentrancy();
			T t = base[index];
			base.SetItem(index, item);
			this.OnIndexerPropertyChanged();
			this.OnCollectionChanged(NotifyCollectionChangedAction.Replace, t, item, index);
		}

		/// <summary>Moves the item at the specified index to a new location in the collection.</summary>
		/// <param name="oldIndex">The zero-based index specifying the location of the item to be moved.</param>
		/// <param name="newIndex">The zero-based index specifying the new location of the item.</param>
		// Token: 0x06003DF3 RID: 15859 RVA: 0x000DA158 File Offset: 0x000D8358
		protected virtual void MoveItem(int oldIndex, int newIndex)
		{
			this.CheckReentrancy();
			T t = base[oldIndex];
			base.RemoveItem(oldIndex);
			base.InsertItem(newIndex, t);
			this.OnIndexerPropertyChanged();
			this.OnCollectionChanged(NotifyCollectionChangedAction.Move, t, newIndex, oldIndex);
		}

		/// <summary>Raises the <see cref="E:System.Collections.ObjectModel.ObservableCollection`1.PropertyChanged" /> event with the provided arguments.</summary>
		/// <param name="e">Arguments of the event being raised.</param>
		// Token: 0x06003DF4 RID: 15860 RVA: 0x000DA197 File Offset: 0x000D8397
		protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged == null)
			{
				return;
			}
			propertyChanged(this, e);
		}

		/// <summary>Occurs when a property value changes.</summary>
		// Token: 0x14000065 RID: 101
		// (add) Token: 0x06003DF5 RID: 15861 RVA: 0x000DA1AC File Offset: 0x000D83AC
		// (remove) Token: 0x06003DF6 RID: 15862 RVA: 0x000DA1E4 File Offset: 0x000D83E4
		[field: NonSerialized]
		protected virtual event PropertyChangedEventHandler PropertyChanged;

		/// <summary>Raises the <see cref="E:System.Collections.ObjectModel.ObservableCollection`1.CollectionChanged" /> event with the provided arguments.</summary>
		/// <param name="e">Arguments of the event being raised.</param>
		// Token: 0x06003DF7 RID: 15863 RVA: 0x000DA21C File Offset: 0x000D841C
		protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
		{
			NotifyCollectionChangedEventHandler collectionChanged = this.CollectionChanged;
			if (collectionChanged != null)
			{
				this._blockReentrancyCount++;
				try
				{
					collectionChanged(this, e);
				}
				finally
				{
					this._blockReentrancyCount--;
				}
			}
		}

		/// <summary>Disallows reentrant attempts to change this collection.</summary>
		/// <returns>An <see cref="T:System.IDisposable" /> object that can be used to dispose of the object.</returns>
		// Token: 0x06003DF8 RID: 15864 RVA: 0x000DA26C File Offset: 0x000D846C
		protected IDisposable BlockReentrancy()
		{
			this._blockReentrancyCount++;
			return this.EnsureMonitorInitialized();
		}

		/// <summary>Checks for reentrant attempts to change this collection.</summary>
		/// <exception cref="T:System.InvalidOperationException">If there was a call to <see cref="M:System.Collections.ObjectModel.ObservableCollection`1.BlockReentrancy" /> of which the <see cref="T:System.IDisposable" /> return value has not yet been disposed of. Typically, this means when there are additional attempts to change this collection during a <see cref="E:System.Collections.ObjectModel.ObservableCollection`1.CollectionChanged" /> event. However, it depends on when derived classes choose to call <see cref="M:System.Collections.ObjectModel.ObservableCollection`1.BlockReentrancy" />.</exception>
		// Token: 0x06003DF9 RID: 15865 RVA: 0x000DA282 File Offset: 0x000D8482
		protected void CheckReentrancy()
		{
			if (this._blockReentrancyCount > 0)
			{
				NotifyCollectionChangedEventHandler collectionChanged = this.CollectionChanged;
				if (collectionChanged != null && collectionChanged.GetInvocationList().Length > 1)
				{
					throw new InvalidOperationException("Cannot change ObservableCollection during a CollectionChanged event.");
				}
			}
		}

		// Token: 0x06003DFA RID: 15866 RVA: 0x000DA2B1 File Offset: 0x000D84B1
		private void OnCountPropertyChanged()
		{
			this.OnPropertyChanged(EventArgsCache.CountPropertyChanged);
		}

		// Token: 0x06003DFB RID: 15867 RVA: 0x000DA2BE File Offset: 0x000D84BE
		private void OnIndexerPropertyChanged()
		{
			this.OnPropertyChanged(EventArgsCache.IndexerPropertyChanged);
		}

		// Token: 0x06003DFC RID: 15868 RVA: 0x000DA2CB File Offset: 0x000D84CB
		private void OnCollectionChanged(NotifyCollectionChangedAction action, object item, int index)
		{
			this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, item, index));
		}

		// Token: 0x06003DFD RID: 15869 RVA: 0x000DA2DB File Offset: 0x000D84DB
		private void OnCollectionChanged(NotifyCollectionChangedAction action, object item, int index, int oldIndex)
		{
			this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, item, index, oldIndex));
		}

		// Token: 0x06003DFE RID: 15870 RVA: 0x000DA2ED File Offset: 0x000D84ED
		private void OnCollectionChanged(NotifyCollectionChangedAction action, object oldItem, object newItem, int index)
		{
			this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, newItem, oldItem, index));
		}

		// Token: 0x06003DFF RID: 15871 RVA: 0x000DA2FF File Offset: 0x000D84FF
		private void OnCollectionReset()
		{
			this.OnCollectionChanged(EventArgsCache.ResetCollectionChanged);
		}

		// Token: 0x06003E00 RID: 15872 RVA: 0x000DA30C File Offset: 0x000D850C
		private ObservableCollection<T>.SimpleMonitor EnsureMonitorInitialized()
		{
			ObservableCollection<T>.SimpleMonitor simpleMonitor;
			if ((simpleMonitor = this._monitor) == null)
			{
				simpleMonitor = (this._monitor = new ObservableCollection<T>.SimpleMonitor(this));
			}
			return simpleMonitor;
		}

		// Token: 0x06003E01 RID: 15873 RVA: 0x000DA332 File Offset: 0x000D8532
		[OnSerializing]
		private void OnSerializing(StreamingContext context)
		{
			this.EnsureMonitorInitialized();
			this._monitor._busyCount = this._blockReentrancyCount;
		}

		// Token: 0x06003E02 RID: 15874 RVA: 0x000DA34C File Offset: 0x000D854C
		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			if (this._monitor != null)
			{
				this._blockReentrancyCount = this._monitor._busyCount;
				this._monitor._collection = this;
			}
		}

		// Token: 0x04002612 RID: 9746
		private ObservableCollection<T>.SimpleMonitor _monitor;

		// Token: 0x04002613 RID: 9747
		[NonSerialized]
		private int _blockReentrancyCount;

		// Token: 0x020007AB RID: 1963
		[Serializable]
		private sealed class SimpleMonitor : IDisposable
		{
			// Token: 0x06003E03 RID: 15875 RVA: 0x000DA373 File Offset: 0x000D8573
			public SimpleMonitor(ObservableCollection<T> collection)
			{
				this._collection = collection;
			}

			// Token: 0x06003E04 RID: 15876 RVA: 0x000DA382 File Offset: 0x000D8582
			public void Dispose()
			{
				this._collection._blockReentrancyCount--;
			}

			// Token: 0x04002616 RID: 9750
			internal int _busyCount;

			// Token: 0x04002617 RID: 9751
			[NonSerialized]
			internal ObservableCollection<T> _collection;
		}
	}
}
