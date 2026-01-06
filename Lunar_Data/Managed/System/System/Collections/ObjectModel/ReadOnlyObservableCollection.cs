using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;

namespace System.Collections.ObjectModel
{
	/// <summary>Represents a read-only <see cref="T:System.Collections.ObjectModel.ObservableCollection`1" />.</summary>
	/// <typeparam name="T">The type of elements in the collection.</typeparam>
	// Token: 0x020007AD RID: 1965
	[DebuggerTypeProxy(typeof(global::System.Collections.Generic.CollectionDebugView<>))]
	[DebuggerDisplay("Count = {Count}")]
	[Serializable]
	public class ReadOnlyObservableCollection<T> : ReadOnlyCollection<T>, INotifyCollectionChanged, INotifyPropertyChanged
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.ObjectModel.ReadOnlyObservableCollection`1" /> class that serves as a wrapper around the specified <see cref="T:System.Collections.ObjectModel.ObservableCollection`1" />.</summary>
		/// <param name="list">The <see cref="T:System.Collections.ObjectModel.ObservableCollection`1" /> with which to create this instance of the <see cref="T:System.Collections.ObjectModel.ReadOnlyObservableCollection`1" /> class.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="list" /> is null.</exception>
		// Token: 0x06003E06 RID: 15878 RVA: 0x000DA3C4 File Offset: 0x000D85C4
		public ReadOnlyObservableCollection(ObservableCollection<T> list)
			: base(list)
		{
			((INotifyCollectionChanged)base.Items).CollectionChanged += this.HandleCollectionChanged;
			((INotifyPropertyChanged)base.Items).PropertyChanged += this.HandlePropertyChanged;
		}

		/// <summary>Occurs when the collection changes.</summary>
		// Token: 0x14000066 RID: 102
		// (add) Token: 0x06003E07 RID: 15879 RVA: 0x000DA410 File Offset: 0x000D8610
		// (remove) Token: 0x06003E08 RID: 15880 RVA: 0x000DA419 File Offset: 0x000D8619
		event NotifyCollectionChangedEventHandler INotifyCollectionChanged.CollectionChanged
		{
			add
			{
				this.CollectionChanged += value;
			}
			remove
			{
				this.CollectionChanged -= value;
			}
		}

		/// <summary>Occurs when an item is added or removed.</summary>
		// Token: 0x14000067 RID: 103
		// (add) Token: 0x06003E09 RID: 15881 RVA: 0x000DA424 File Offset: 0x000D8624
		// (remove) Token: 0x06003E0A RID: 15882 RVA: 0x000DA45C File Offset: 0x000D865C
		[field: NonSerialized]
		protected virtual event NotifyCollectionChangedEventHandler CollectionChanged;

		/// <summary>Raises the <see cref="E:System.Collections.ObjectModel.ReadOnlyObservableCollection`1.CollectionChanged" /> event using the provided arguments.</summary>
		/// <param name="args">Arguments of the event being raised.</param>
		// Token: 0x06003E0B RID: 15883 RVA: 0x000DA491 File Offset: 0x000D8691
		protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs args)
		{
			if (this.CollectionChanged != null)
			{
				this.CollectionChanged(this, args);
			}
		}

		/// <summary>Occurs when a property value changes.</summary>
		// Token: 0x14000068 RID: 104
		// (add) Token: 0x06003E0C RID: 15884 RVA: 0x000DA4A8 File Offset: 0x000D86A8
		// (remove) Token: 0x06003E0D RID: 15885 RVA: 0x000DA4B1 File Offset: 0x000D86B1
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

		/// <summary>Occurs when a property value changes.</summary>
		// Token: 0x14000069 RID: 105
		// (add) Token: 0x06003E0E RID: 15886 RVA: 0x000DA4BC File Offset: 0x000D86BC
		// (remove) Token: 0x06003E0F RID: 15887 RVA: 0x000DA4F4 File Offset: 0x000D86F4
		[field: NonSerialized]
		protected virtual event PropertyChangedEventHandler PropertyChanged;

		/// <summary>Raises the <see cref="E:System.Collections.ObjectModel.ReadOnlyObservableCollection`1.PropertyChanged" /> event using the provided arguments.</summary>
		/// <param name="args">Arguments of the event being raised.</param>
		// Token: 0x06003E10 RID: 15888 RVA: 0x000DA529 File Offset: 0x000D8729
		protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, args);
			}
		}

		// Token: 0x06003E11 RID: 15889 RVA: 0x000DA540 File Offset: 0x000D8740
		private void HandleCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			this.OnCollectionChanged(e);
		}

		// Token: 0x06003E12 RID: 15890 RVA: 0x000DA549 File Offset: 0x000D8749
		private void HandlePropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			this.OnPropertyChanged(e);
		}
	}
}
