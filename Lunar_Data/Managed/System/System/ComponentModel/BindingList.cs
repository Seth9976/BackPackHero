using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;

namespace System.ComponentModel
{
	/// <summary>Provides a generic collection that supports data binding.</summary>
	/// <typeparam name="T">The type of elements in the list.</typeparam>
	// Token: 0x0200069A RID: 1690
	[Serializable]
	public class BindingList<T> : Collection<T>, IBindingList, IList, ICollection, IEnumerable, ICancelAddNew, IRaiseItemChangedEvents
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.BindingList`1" /> class using default values.</summary>
		// Token: 0x0600360C RID: 13836 RVA: 0x000BFE2C File Offset: 0x000BE02C
		public BindingList()
		{
			this.Initialize();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.BindingList`1" /> class with the specified list.</summary>
		/// <param name="list">An <see cref="T:System.Collections.Generic.IList`1" /> of items to be contained in the <see cref="T:System.ComponentModel.BindingList`1" />.</param>
		// Token: 0x0600360D RID: 13837 RVA: 0x000BFE64 File Offset: 0x000BE064
		public BindingList(IList<T> list)
			: base(list)
		{
			this.Initialize();
		}

		// Token: 0x0600360E RID: 13838 RVA: 0x000BFEA0 File Offset: 0x000BE0A0
		private void Initialize()
		{
			this.allowNew = this.ItemTypeHasDefaultConstructor;
			if (typeof(INotifyPropertyChanged).IsAssignableFrom(typeof(T)))
			{
				this.raiseItemChangedEvents = true;
				foreach (T t in base.Items)
				{
					this.HookPropertyChanged(t);
				}
			}
		}

		// Token: 0x17000C76 RID: 3190
		// (get) Token: 0x0600360F RID: 13839 RVA: 0x000BFF1C File Offset: 0x000BE11C
		private bool ItemTypeHasDefaultConstructor
		{
			get
			{
				Type typeFromHandle = typeof(T);
				return typeFromHandle.IsPrimitive || typeFromHandle.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, null, Array.Empty<Type>(), null) != null;
			}
		}

		/// <summary>Occurs before an item is added to the list.</summary>
		// Token: 0x14000041 RID: 65
		// (add) Token: 0x06003610 RID: 13840 RVA: 0x000BFF56 File Offset: 0x000BE156
		// (remove) Token: 0x06003611 RID: 13841 RVA: 0x000BFF85 File Offset: 0x000BE185
		public event AddingNewEventHandler AddingNew
		{
			add
			{
				bool flag = this.AllowNew;
				this._onAddingNew = (AddingNewEventHandler)Delegate.Combine(this._onAddingNew, value);
				if (flag != this.AllowNew)
				{
					this.FireListChanged(ListChangedType.Reset, -1);
				}
			}
			remove
			{
				bool flag = this.AllowNew;
				this._onAddingNew = (AddingNewEventHandler)Delegate.Remove(this._onAddingNew, value);
				if (flag != this.AllowNew)
				{
					this.FireListChanged(ListChangedType.Reset, -1);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.BindingList`1.AddingNew" /> event.</summary>
		/// <param name="e">An <see cref="T:System.ComponentModel.AddingNewEventArgs" /> that contains the event data. </param>
		// Token: 0x06003612 RID: 13842 RVA: 0x000BFFB4 File Offset: 0x000BE1B4
		protected virtual void OnAddingNew(AddingNewEventArgs e)
		{
			AddingNewEventHandler onAddingNew = this._onAddingNew;
			if (onAddingNew == null)
			{
				return;
			}
			onAddingNew(this, e);
		}

		// Token: 0x06003613 RID: 13843 RVA: 0x000BFFC8 File Offset: 0x000BE1C8
		private object FireAddingNew()
		{
			AddingNewEventArgs addingNewEventArgs = new AddingNewEventArgs(null);
			this.OnAddingNew(addingNewEventArgs);
			return addingNewEventArgs.NewObject;
		}

		/// <summary>Occurs when the list or an item in the list changes.</summary>
		// Token: 0x14000042 RID: 66
		// (add) Token: 0x06003614 RID: 13844 RVA: 0x000BFFE9 File Offset: 0x000BE1E9
		// (remove) Token: 0x06003615 RID: 13845 RVA: 0x000C0002 File Offset: 0x000BE202
		public event ListChangedEventHandler ListChanged
		{
			add
			{
				this._onListChanged = (ListChangedEventHandler)Delegate.Combine(this._onListChanged, value);
			}
			remove
			{
				this._onListChanged = (ListChangedEventHandler)Delegate.Remove(this._onListChanged, value);
			}
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.BindingList`1.ListChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.ListChangedEventArgs" /> that contains the event data. </param>
		// Token: 0x06003616 RID: 13846 RVA: 0x000C001B File Offset: 0x000BE21B
		protected virtual void OnListChanged(ListChangedEventArgs e)
		{
			ListChangedEventHandler onListChanged = this._onListChanged;
			if (onListChanged == null)
			{
				return;
			}
			onListChanged(this, e);
		}

		/// <summary>Gets or sets a value indicating whether adding or removing items within the list raises <see cref="E:System.ComponentModel.BindingList`1.ListChanged" /> events.</summary>
		/// <returns>true if adding or removing items raises <see cref="E:System.ComponentModel.BindingList`1.ListChanged" /> events; otherwise, false. The default is true.</returns>
		// Token: 0x17000C77 RID: 3191
		// (get) Token: 0x06003617 RID: 13847 RVA: 0x000C002F File Offset: 0x000BE22F
		// (set) Token: 0x06003618 RID: 13848 RVA: 0x000C0037 File Offset: 0x000BE237
		public bool RaiseListChangedEvents
		{
			get
			{
				return this.raiseListChangedEvents;
			}
			set
			{
				this.raiseListChangedEvents = value;
			}
		}

		/// <summary>Raises a <see cref="E:System.ComponentModel.BindingList`1.ListChanged" /> event of type <see cref="F:System.ComponentModel.ListChangedType.Reset" />.</summary>
		// Token: 0x06003619 RID: 13849 RVA: 0x000C0040 File Offset: 0x000BE240
		public void ResetBindings()
		{
			this.FireListChanged(ListChangedType.Reset, -1);
		}

		/// <summary>Raises a <see cref="E:System.ComponentModel.BindingList`1.ListChanged" /> event of type <see cref="F:System.ComponentModel.ListChangedType.ItemChanged" /> for the item at the specified position.</summary>
		/// <param name="position">A zero-based index of the item to be reset.</param>
		// Token: 0x0600361A RID: 13850 RVA: 0x000C004A File Offset: 0x000BE24A
		public void ResetItem(int position)
		{
			this.FireListChanged(ListChangedType.ItemChanged, position);
		}

		// Token: 0x0600361B RID: 13851 RVA: 0x000C0054 File Offset: 0x000BE254
		private void FireListChanged(ListChangedType type, int index)
		{
			if (this.raiseListChangedEvents)
			{
				this.OnListChanged(new ListChangedEventArgs(type, index));
			}
		}

		/// <summary>Removes all elements from the collection.</summary>
		// Token: 0x0600361C RID: 13852 RVA: 0x000C006C File Offset: 0x000BE26C
		protected override void ClearItems()
		{
			this.EndNew(this.addNewPos);
			if (this.raiseItemChangedEvents)
			{
				foreach (T t in base.Items)
				{
					this.UnhookPropertyChanged(t);
				}
			}
			base.ClearItems();
			this.FireListChanged(ListChangedType.Reset, -1);
		}

		/// <summary>Inserts the specified item in the list at the specified index.</summary>
		/// <param name="index">The zero-based index where the item is to be inserted.</param>
		/// <param name="item">The item to insert in the list.</param>
		// Token: 0x0600361D RID: 13853 RVA: 0x000C00DC File Offset: 0x000BE2DC
		protected override void InsertItem(int index, T item)
		{
			this.EndNew(this.addNewPos);
			base.InsertItem(index, item);
			if (this.raiseItemChangedEvents)
			{
				this.HookPropertyChanged(item);
			}
			this.FireListChanged(ListChangedType.ItemAdded, index);
		}

		/// <summary>Removes the item at the specified index.</summary>
		/// <param name="index">The zero-based index of the item to remove. </param>
		/// <exception cref="T:System.NotSupportedException">You are removing a newly added item and <see cref="P:System.ComponentModel.IBindingList.AllowRemove" /> is set to false. </exception>
		// Token: 0x0600361E RID: 13854 RVA: 0x000C010C File Offset: 0x000BE30C
		protected override void RemoveItem(int index)
		{
			if (!this.allowRemove && (this.addNewPos < 0 || this.addNewPos != index))
			{
				throw new NotSupportedException();
			}
			this.EndNew(this.addNewPos);
			if (this.raiseItemChangedEvents)
			{
				this.UnhookPropertyChanged(base[index]);
			}
			base.RemoveItem(index);
			this.FireListChanged(ListChangedType.ItemDeleted, index);
		}

		/// <summary>Replaces the item at the specified index with the specified item.</summary>
		/// <param name="index">The zero-based index of the item to replace.</param>
		/// <param name="item">The new value for the item at the specified index. The value can be null for reference types.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or-<paramref name="index" /> is greater than <see cref="P:System.Collections.ObjectModel.Collection`1.Count" />.</exception>
		// Token: 0x0600361F RID: 13855 RVA: 0x000C0169 File Offset: 0x000BE369
		protected override void SetItem(int index, T item)
		{
			if (this.raiseItemChangedEvents)
			{
				this.UnhookPropertyChanged(base[index]);
			}
			base.SetItem(index, item);
			if (this.raiseItemChangedEvents)
			{
				this.HookPropertyChanged(item);
			}
			this.FireListChanged(ListChangedType.ItemChanged, index);
		}

		/// <summary>Discards a pending new item.</summary>
		/// <param name="itemIndex">The index of the of the new item to be added </param>
		// Token: 0x06003620 RID: 13856 RVA: 0x000C019F File Offset: 0x000BE39F
		public virtual void CancelNew(int itemIndex)
		{
			if (this.addNewPos >= 0 && this.addNewPos == itemIndex)
			{
				this.RemoveItem(this.addNewPos);
				this.addNewPos = -1;
			}
		}

		/// <summary>Commits a pending new item to the collection.</summary>
		/// <param name="itemIndex">The index of the new item to be added.</param>
		// Token: 0x06003621 RID: 13857 RVA: 0x000C01C6 File Offset: 0x000BE3C6
		public virtual void EndNew(int itemIndex)
		{
			if (this.addNewPos >= 0 && this.addNewPos == itemIndex)
			{
				this.addNewPos = -1;
			}
		}

		/// <summary>Adds a new item to the collection.</summary>
		/// <returns>The item added to the list.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Windows.Forms.BindingSource.AllowNew" /> property is set to false. -or-A public default constructor could not be found for the current item type.</exception>
		// Token: 0x06003622 RID: 13858 RVA: 0x000C01E1 File Offset: 0x000BE3E1
		public T AddNew()
		{
			return (T)((object)((IBindingList)this).AddNew());
		}

		/// <summary>Adds a new item to the list. For more information, see <see cref="M:System.ComponentModel.IBindingList.AddNew" />.</summary>
		/// <returns>The item added to the list.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not supported. </exception>
		// Token: 0x06003623 RID: 13859 RVA: 0x000C01F0 File Offset: 0x000BE3F0
		object IBindingList.AddNew()
		{
			object obj = this.AddNewCore();
			this.addNewPos = ((obj != null) ? base.IndexOf((T)((object)obj)) : (-1));
			return obj;
		}

		// Token: 0x17000C78 RID: 3192
		// (get) Token: 0x06003624 RID: 13860 RVA: 0x000C021D File Offset: 0x000BE41D
		private bool AddingNewHandled
		{
			get
			{
				return this._onAddingNew != null && this._onAddingNew.GetInvocationList().Length != 0;
			}
		}

		/// <summary>Adds a new item to the end of the collection.</summary>
		/// <returns>The item that was added to the collection.</returns>
		/// <exception cref="T:System.InvalidCastException">The new item is not the same type as the objects contained in the <see cref="T:System.ComponentModel.BindingList`1" />.</exception>
		// Token: 0x06003625 RID: 13861 RVA: 0x000C0238 File Offset: 0x000BE438
		protected virtual object AddNewCore()
		{
			object obj = this.FireAddingNew();
			if (obj == null)
			{
				obj = SecurityUtils.SecureCreateInstance(typeof(T));
			}
			base.Add((T)((object)obj));
			return obj;
		}

		/// <summary>Gets or sets a value indicating whether you can add items to the list using the <see cref="M:System.ComponentModel.BindingList`1.AddNew" /> method.</summary>
		/// <returns>true if you can add items to the list with the <see cref="M:System.ComponentModel.BindingList`1.AddNew" /> method; otherwise, false. The default depends on the underlying type contained in the list.</returns>
		// Token: 0x17000C79 RID: 3193
		// (get) Token: 0x06003626 RID: 13862 RVA: 0x000C026C File Offset: 0x000BE46C
		// (set) Token: 0x06003627 RID: 13863 RVA: 0x000C028B File Offset: 0x000BE48B
		public bool AllowNew
		{
			get
			{
				if (this.userSetAllowNew || this.allowNew)
				{
					return this.allowNew;
				}
				return this.AddingNewHandled;
			}
			set
			{
				bool flag = this.AllowNew;
				this.userSetAllowNew = true;
				this.allowNew = value;
				if (flag != value)
				{
					this.FireListChanged(ListChangedType.Reset, -1);
				}
			}
		}

		/// <summary>Gets a value indicating whether new items can be added to the list using the <see cref="M:System.ComponentModel.BindingList`1.AddNew" /> method.</summary>
		/// <returns>true if you can add items to the list with the <see cref="M:System.ComponentModel.BindingList`1.AddNew" /> method; otherwise, false. The default depends on the underlying type contained in the list.</returns>
		// Token: 0x17000C7A RID: 3194
		// (get) Token: 0x06003628 RID: 13864 RVA: 0x000C02AC File Offset: 0x000BE4AC
		bool IBindingList.AllowNew
		{
			get
			{
				return this.AllowNew;
			}
		}

		/// <summary>Gets or sets a value indicating whether items in the list can be edited.</summary>
		/// <returns>true if list items can be edited; otherwise, false. The default is true.</returns>
		// Token: 0x17000C7B RID: 3195
		// (get) Token: 0x06003629 RID: 13865 RVA: 0x000C02B4 File Offset: 0x000BE4B4
		// (set) Token: 0x0600362A RID: 13866 RVA: 0x000C02BC File Offset: 0x000BE4BC
		public bool AllowEdit
		{
			get
			{
				return this.allowEdit;
			}
			set
			{
				if (this.allowEdit != value)
				{
					this.allowEdit = value;
					this.FireListChanged(ListChangedType.Reset, -1);
				}
			}
		}

		/// <summary>Gets a value indicating whether items in the list can be edited.</summary>
		/// <returns>true if list items can be edited; otherwise, false. The default is true.</returns>
		// Token: 0x17000C7C RID: 3196
		// (get) Token: 0x0600362B RID: 13867 RVA: 0x000C02D6 File Offset: 0x000BE4D6
		bool IBindingList.AllowEdit
		{
			get
			{
				return this.AllowEdit;
			}
		}

		/// <summary>Gets or sets a value indicating whether you can remove items from the collection. </summary>
		/// <returns>true if you can remove items from the list with the <see cref="M:System.ComponentModel.BindingList`1.RemoveItem(System.Int32)" /> method otherwise, false. The default is true.</returns>
		// Token: 0x17000C7D RID: 3197
		// (get) Token: 0x0600362C RID: 13868 RVA: 0x000C02DE File Offset: 0x000BE4DE
		// (set) Token: 0x0600362D RID: 13869 RVA: 0x000C02E6 File Offset: 0x000BE4E6
		public bool AllowRemove
		{
			get
			{
				return this.allowRemove;
			}
			set
			{
				if (this.allowRemove != value)
				{
					this.allowRemove = value;
					this.FireListChanged(ListChangedType.Reset, -1);
				}
			}
		}

		/// <summary>Gets a value indicating whether items can be removed from the list.</summary>
		/// <returns>true if you can remove items from the list with the <see cref="M:System.ComponentModel.BindingList`1.RemoveItem(System.Int32)" /> method; otherwise, false. The default is true.</returns>
		// Token: 0x17000C7E RID: 3198
		// (get) Token: 0x0600362E RID: 13870 RVA: 0x000C0300 File Offset: 0x000BE500
		bool IBindingList.AllowRemove
		{
			get
			{
				return this.AllowRemove;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SupportsChangeNotification" />.</summary>
		/// <returns>true if a <see cref="E:System.ComponentModel.IBindingList.ListChanged" /> event is raised when the list changes or when an item changes; otherwise, false.</returns>
		// Token: 0x17000C7F RID: 3199
		// (get) Token: 0x0600362F RID: 13871 RVA: 0x000C0308 File Offset: 0x000BE508
		bool IBindingList.SupportsChangeNotification
		{
			get
			{
				return this.SupportsChangeNotificationCore;
			}
		}

		/// <summary>Gets a value indicating whether <see cref="E:System.ComponentModel.BindingList`1.ListChanged" /> events are enabled.</summary>
		/// <returns>true if <see cref="E:System.ComponentModel.BindingList`1.ListChanged" /> events are supported; otherwise, false. The default is true.</returns>
		// Token: 0x17000C80 RID: 3200
		// (get) Token: 0x06003630 RID: 13872 RVA: 0x0000390E File Offset: 0x00001B0E
		protected virtual bool SupportsChangeNotificationCore
		{
			get
			{
				return true;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SupportsSearching" />.</summary>
		/// <returns>true if the list supports searching using the <see cref="M:System.ComponentModel.IBindingList.Find(System.ComponentModel.PropertyDescriptor,System.Object)" /> method; otherwise, false.</returns>
		// Token: 0x17000C81 RID: 3201
		// (get) Token: 0x06003631 RID: 13873 RVA: 0x000C0310 File Offset: 0x000BE510
		bool IBindingList.SupportsSearching
		{
			get
			{
				return this.SupportsSearchingCore;
			}
		}

		/// <summary>Gets a value indicating whether the list supports searching.</summary>
		/// <returns>true if the list supports searching; otherwise, false. The default is false.</returns>
		// Token: 0x17000C82 RID: 3202
		// (get) Token: 0x06003632 RID: 13874 RVA: 0x00003062 File Offset: 0x00001262
		protected virtual bool SupportsSearchingCore
		{
			get
			{
				return false;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SupportsSorting" />.</summary>
		/// <returns>true if the list supports sorting; otherwise, false.</returns>
		// Token: 0x17000C83 RID: 3203
		// (get) Token: 0x06003633 RID: 13875 RVA: 0x000C0318 File Offset: 0x000BE518
		bool IBindingList.SupportsSorting
		{
			get
			{
				return this.SupportsSortingCore;
			}
		}

		/// <summary>Gets a value indicating whether the list supports sorting.</summary>
		/// <returns>true if the list supports sorting; otherwise, false. The default is false.</returns>
		// Token: 0x17000C84 RID: 3204
		// (get) Token: 0x06003634 RID: 13876 RVA: 0x00003062 File Offset: 0x00001262
		protected virtual bool SupportsSortingCore
		{
			get
			{
				return false;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.IsSorted" />.</summary>
		/// <returns>true if <see cref="M:System.ComponentModel.IBindingListView.ApplySort(System.ComponentModel.ListSortDescriptionCollection)" /> has been called and <see cref="M:System.ComponentModel.IBindingList.RemoveSort" /> has not been called; otherwise, false.</returns>
		// Token: 0x17000C85 RID: 3205
		// (get) Token: 0x06003635 RID: 13877 RVA: 0x000C0320 File Offset: 0x000BE520
		bool IBindingList.IsSorted
		{
			get
			{
				return this.IsSortedCore;
			}
		}

		/// <summary>Gets a value indicating whether the list is sorted. </summary>
		/// <returns>true if the list is sorted; otherwise, false. The default is false.</returns>
		// Token: 0x17000C86 RID: 3206
		// (get) Token: 0x06003636 RID: 13878 RVA: 0x00003062 File Offset: 0x00001262
		protected virtual bool IsSortedCore
		{
			get
			{
				return false;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SortProperty" />.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.PropertyDescriptor" /> that is being used for sorting.</returns>
		// Token: 0x17000C87 RID: 3207
		// (get) Token: 0x06003637 RID: 13879 RVA: 0x000C0328 File Offset: 0x000BE528
		PropertyDescriptor IBindingList.SortProperty
		{
			get
			{
				return this.SortPropertyCore;
			}
		}

		/// <summary>Gets the property descriptor that is used for sorting the list if sorting is implemented in a derived class; otherwise, returns null. </summary>
		/// <returns>The <see cref="T:System.ComponentModel.PropertyDescriptor" /> used for sorting the list.</returns>
		// Token: 0x17000C88 RID: 3208
		// (get) Token: 0x06003638 RID: 13880 RVA: 0x00002F6A File Offset: 0x0000116A
		protected virtual PropertyDescriptor SortPropertyCore
		{
			get
			{
				return null;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SortDirection" />.</summary>
		/// <returns>One of the <see cref="T:System.ComponentModel.ListSortDirection" /> values.</returns>
		// Token: 0x17000C89 RID: 3209
		// (get) Token: 0x06003639 RID: 13881 RVA: 0x000C0330 File Offset: 0x000BE530
		ListSortDirection IBindingList.SortDirection
		{
			get
			{
				return this.SortDirectionCore;
			}
		}

		/// <summary>Gets the direction the list is sorted.</summary>
		/// <returns>One of the <see cref="T:System.ComponentModel.ListSortDirection" /> values. The default is <see cref="F:System.ComponentModel.ListSortDirection.Ascending" />. </returns>
		// Token: 0x17000C8A RID: 3210
		// (get) Token: 0x0600363A RID: 13882 RVA: 0x00003062 File Offset: 0x00001262
		protected virtual ListSortDirection SortDirectionCore
		{
			get
			{
				return ListSortDirection.Ascending;
			}
		}

		/// <summary>Sorts the list based on a <see cref="T:System.ComponentModel.PropertyDescriptor" /> and a <see cref="T:System.ComponentModel.ListSortDirection" />. For a complete description of this member, see <see cref="M:System.ComponentModel.IBindingList.ApplySort(System.ComponentModel.PropertyDescriptor,System.ComponentModel.ListSortDirection)" />. </summary>
		/// <param name="prop">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to sort by.</param>
		/// <param name="direction">One of the <see cref="T:System.ComponentModel.ListSortDirection" /> values.</param>
		// Token: 0x0600363B RID: 13883 RVA: 0x000C0338 File Offset: 0x000BE538
		void IBindingList.ApplySort(PropertyDescriptor prop, ListSortDirection direction)
		{
			this.ApplySortCore(prop, direction);
		}

		/// <summary>Sorts the items if overridden in a derived class; otherwise, throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <param name="prop">A <see cref="T:System.ComponentModel.PropertyDescriptor" /> that specifies the property to sort on.</param>
		/// <param name="direction">One of the <see cref="T:System.ComponentModel.ListSortDirection" />  values.</param>
		/// <exception cref="T:System.NotSupportedException">Method is not overridden in a derived class. </exception>
		// Token: 0x0600363C RID: 13884 RVA: 0x000044FA File Offset: 0x000026FA
		protected virtual void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
		{
			throw new NotSupportedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.IBindingList.RemoveSort" /></summary>
		// Token: 0x0600363D RID: 13885 RVA: 0x000C0342 File Offset: 0x000BE542
		void IBindingList.RemoveSort()
		{
			this.RemoveSortCore();
		}

		/// <summary>Removes any sort applied with <see cref="M:System.ComponentModel.BindingList`1.ApplySortCore(System.ComponentModel.PropertyDescriptor,System.ComponentModel.ListSortDirection)" /> if sorting is implemented in a derived class; otherwise, raises <see cref="T:System.NotSupportedException" />.</summary>
		/// <exception cref="T:System.NotSupportedException">Method is not overridden in a derived class. </exception>
		// Token: 0x0600363E RID: 13886 RVA: 0x000044FA File Offset: 0x000026FA
		protected virtual void RemoveSortCore()
		{
			throw new NotSupportedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.IBindingList.Find(System.ComponentModel.PropertyDescriptor,System.Object)" />.</summary>
		/// <returns>The index of the row that has the given <see cref="T:System.ComponentModel.PropertyDescriptor" /> .</returns>
		/// <param name="prop">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to search on.</param>
		/// <param name="key">The value of the <paramref name="property" /> parameter to search for.</param>
		// Token: 0x0600363F RID: 13887 RVA: 0x000C034A File Offset: 0x000BE54A
		int IBindingList.Find(PropertyDescriptor prop, object key)
		{
			return this.FindCore(prop, key);
		}

		/// <summary>Searches for the index of the item that has the specified property descriptor with the specified value, if searching is implemented in a derived class; otherwise, a <see cref="T:System.NotSupportedException" />.</summary>
		/// <returns>The zero-based index of the item that matches the property descriptor and contains the specified value.</returns>
		/// <param name="prop">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to search for.</param>
		/// <param name="key">The value of <paramref name="property" /> to match.</param>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="M:System.ComponentModel.BindingList`1.FindCore(System.ComponentModel.PropertyDescriptor,System.Object)" /> is not overridden in a derived class.</exception>
		// Token: 0x06003640 RID: 13888 RVA: 0x000044FA File Offset: 0x000026FA
		protected virtual int FindCore(PropertyDescriptor prop, object key)
		{
			throw new NotSupportedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.IBindingList.AddIndex(System.ComponentModel.PropertyDescriptor)" />.</summary>
		/// <param name="prop">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to add as a search criteria. </param>
		// Token: 0x06003641 RID: 13889 RVA: 0x00003917 File Offset: 0x00001B17
		void IBindingList.AddIndex(PropertyDescriptor prop)
		{
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.IBindingList.RemoveIndex(System.ComponentModel.PropertyDescriptor)" />.</summary>
		/// <param name="prop">A <see cref="T:System.ComponentModel.PropertyDescriptor" /> to remove from the indexes used for searching.</param>
		// Token: 0x06003642 RID: 13890 RVA: 0x00003917 File Offset: 0x00001B17
		void IBindingList.RemoveIndex(PropertyDescriptor prop)
		{
		}

		// Token: 0x06003643 RID: 13891 RVA: 0x000C0354 File Offset: 0x000BE554
		private void HookPropertyChanged(T item)
		{
			INotifyPropertyChanged notifyPropertyChanged = item as INotifyPropertyChanged;
			if (notifyPropertyChanged != null)
			{
				if (this._propertyChangedEventHandler == null)
				{
					this._propertyChangedEventHandler = new PropertyChangedEventHandler(this.Child_PropertyChanged);
				}
				notifyPropertyChanged.PropertyChanged += this._propertyChangedEventHandler;
			}
		}

		// Token: 0x06003644 RID: 13892 RVA: 0x000C0398 File Offset: 0x000BE598
		private void UnhookPropertyChanged(T item)
		{
			INotifyPropertyChanged notifyPropertyChanged = item as INotifyPropertyChanged;
			if (notifyPropertyChanged != null && this._propertyChangedEventHandler != null)
			{
				notifyPropertyChanged.PropertyChanged -= this._propertyChangedEventHandler;
			}
		}

		// Token: 0x06003645 RID: 13893 RVA: 0x000C03C8 File Offset: 0x000BE5C8
		private void Child_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (this.RaiseListChangedEvents)
			{
				if (sender == null || e == null || string.IsNullOrEmpty(e.PropertyName))
				{
					this.ResetBindings();
					return;
				}
				T t;
				try
				{
					t = (T)((object)sender);
				}
				catch (InvalidCastException)
				{
					this.ResetBindings();
					return;
				}
				int num = this._lastChangeIndex;
				if (num >= 0 && num < base.Count)
				{
					T t2 = base[num];
					if (t2.Equals(t))
					{
						goto IL_007B;
					}
				}
				num = base.IndexOf(t);
				this._lastChangeIndex = num;
				IL_007B:
				if (num == -1)
				{
					this.UnhookPropertyChanged(t);
					this.ResetBindings();
					return;
				}
				if (this._itemTypeProperties == null)
				{
					this._itemTypeProperties = TypeDescriptor.GetProperties(typeof(T));
				}
				PropertyDescriptor propertyDescriptor = this._itemTypeProperties.Find(e.PropertyName, true);
				ListChangedEventArgs listChangedEventArgs = new ListChangedEventArgs(ListChangedType.ItemChanged, num, propertyDescriptor);
				this.OnListChanged(listChangedEventArgs);
			}
		}

		/// <summary>Gets a value indicating whether item property value changes raise <see cref="E:System.ComponentModel.BindingList`1.ListChanged" /> events of type <see cref="F:System.ComponentModel.ListChangedType.ItemChanged" />. This member cannot be overridden in a derived class.</summary>
		/// <returns>true if the list type implements <see cref="T:System.ComponentModel.INotifyPropertyChanged" />, otherwise, false. The default is false.</returns>
		// Token: 0x17000C8B RID: 3211
		// (get) Token: 0x06003646 RID: 13894 RVA: 0x000C04B4 File Offset: 0x000BE6B4
		bool IRaiseItemChangedEvents.RaisesItemChangedEvents
		{
			get
			{
				return this.raiseItemChangedEvents;
			}
		}

		// Token: 0x04002054 RID: 8276
		private int addNewPos = -1;

		// Token: 0x04002055 RID: 8277
		private bool raiseListChangedEvents = true;

		// Token: 0x04002056 RID: 8278
		private bool raiseItemChangedEvents;

		// Token: 0x04002057 RID: 8279
		[NonSerialized]
		private PropertyDescriptorCollection _itemTypeProperties;

		// Token: 0x04002058 RID: 8280
		[NonSerialized]
		private PropertyChangedEventHandler _propertyChangedEventHandler;

		// Token: 0x04002059 RID: 8281
		[NonSerialized]
		private AddingNewEventHandler _onAddingNew;

		// Token: 0x0400205A RID: 8282
		[NonSerialized]
		private ListChangedEventHandler _onListChanged;

		// Token: 0x0400205B RID: 8283
		[NonSerialized]
		private int _lastChangeIndex = -1;

		// Token: 0x0400205C RID: 8284
		private bool allowNew = true;

		// Token: 0x0400205D RID: 8285
		private bool allowEdit = true;

		// Token: 0x0400205E RID: 8286
		private bool allowRemove = true;

		// Token: 0x0400205F RID: 8287
		private bool userSetAllowNew;
	}
}
