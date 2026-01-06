using System;
using System.Collections;
using System.Globalization;
using System.Security.Permissions;
using Unity;

namespace System.ComponentModel.Design
{
	/// <summary>Provides a base class for getting and setting option values for a designer.</summary>
	// Token: 0x02000790 RID: 1936
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public abstract class DesignerOptionService : IDesignerOptionService
	{
		/// <summary>Gets the options collection for this service.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.Design.DesignerOptionService.DesignerOptionCollection" /> populated with available designer options.</returns>
		// Token: 0x17000DE6 RID: 3558
		// (get) Token: 0x06003D3B RID: 15675 RVA: 0x000D8F75 File Offset: 0x000D7175
		public DesignerOptionService.DesignerOptionCollection Options
		{
			get
			{
				if (this._options == null)
				{
					this._options = new DesignerOptionService.DesignerOptionCollection(this, null, string.Empty, null);
				}
				return this._options;
			}
		}

		/// <summary>Creates a new <see cref="T:System.ComponentModel.Design.DesignerOptionService.DesignerOptionCollection" /> with the given name and adds it to the given parent. </summary>
		/// <returns>A new <see cref="T:System.ComponentModel.Design.DesignerOptionService.DesignerOptionCollection" /> with the given name. </returns>
		/// <param name="parent">The parent designer option collection. All collections have a parent except the root object collection.</param>
		/// <param name="name">The name of this collection.</param>
		/// <param name="value">The object providing properties for this collection. Can be null if the collection should not provide any properties.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="parent" /> or <paramref name="name" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is an empty string.</exception>
		// Token: 0x06003D3C RID: 15676 RVA: 0x000D8F98 File Offset: 0x000D7198
		protected DesignerOptionService.DesignerOptionCollection CreateOptionCollection(DesignerOptionService.DesignerOptionCollection parent, string name, object value)
		{
			if (parent == null)
			{
				throw new ArgumentNullException("parent");
			}
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException(SR.GetString("'{1}' is not a valid value for '{0}'.", new object[]
				{
					name.Length.ToString(CultureInfo.CurrentCulture),
					0.ToString(CultureInfo.CurrentCulture)
				}), "name.Length");
			}
			return new DesignerOptionService.DesignerOptionCollection(this, parent, name, value);
		}

		// Token: 0x06003D3D RID: 15677 RVA: 0x000D9014 File Offset: 0x000D7214
		private PropertyDescriptor GetOptionProperty(string pageName, string valueName)
		{
			if (pageName == null)
			{
				throw new ArgumentNullException("pageName");
			}
			if (valueName == null)
			{
				throw new ArgumentNullException("valueName");
			}
			string[] array = pageName.Split(new char[] { '\\' });
			DesignerOptionService.DesignerOptionCollection designerOptionCollection = this.Options;
			foreach (string text in array)
			{
				designerOptionCollection = designerOptionCollection[text];
				if (designerOptionCollection == null)
				{
					return null;
				}
			}
			return designerOptionCollection.Properties[valueName];
		}

		/// <summary>Populates a <see cref="T:System.ComponentModel.Design.DesignerOptionService.DesignerOptionCollection" />.</summary>
		/// <param name="options">The collection to populate.</param>
		// Token: 0x06003D3E RID: 15678 RVA: 0x00003917 File Offset: 0x00001B17
		protected virtual void PopulateOptionCollection(DesignerOptionService.DesignerOptionCollection options)
		{
		}

		/// <summary>Shows the options dialog box for the given object.</summary>
		/// <returns>true if the dialog box is shown; otherwise, false.</returns>
		/// <param name="options">The options collection containing the object to be invoked.</param>
		/// <param name="optionObject">The actual options object.</param>
		// Token: 0x06003D3F RID: 15679 RVA: 0x00003062 File Offset: 0x00001262
		protected virtual bool ShowDialog(DesignerOptionService.DesignerOptionCollection options, object optionObject)
		{
			return false;
		}

		/// <summary>Gets the value of an option defined in this package.</summary>
		/// <returns>The value of the option named <paramref name="valueName" />.</returns>
		/// <param name="pageName">The page to which the option is bound.</param>
		/// <param name="valueName">The name of the option value.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pageName" /> or <paramref name="valueName" /> is null.</exception>
		// Token: 0x06003D40 RID: 15680 RVA: 0x000D9084 File Offset: 0x000D7284
		object IDesignerOptionService.GetOptionValue(string pageName, string valueName)
		{
			PropertyDescriptor optionProperty = this.GetOptionProperty(pageName, valueName);
			if (optionProperty != null)
			{
				return optionProperty.GetValue(null);
			}
			return null;
		}

		/// <summary>Sets the value of an option defined in this package.</summary>
		/// <param name="pageName">The page to which the option is bound</param>
		/// <param name="valueName">The name of the option value.</param>
		/// <param name="value">The value of the option.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pageName" /> or <paramref name="valueName" /> is null.</exception>
		// Token: 0x06003D41 RID: 15681 RVA: 0x000D90A8 File Offset: 0x000D72A8
		void IDesignerOptionService.SetOptionValue(string pageName, string valueName, object value)
		{
			PropertyDescriptor optionProperty = this.GetOptionProperty(pageName, valueName);
			if (optionProperty != null)
			{
				optionProperty.SetValue(null, value);
			}
		}

		// Token: 0x040025F3 RID: 9715
		private DesignerOptionService.DesignerOptionCollection _options;

		/// <summary>Contains a collection of designer options. This class cannot be inherited.</summary>
		// Token: 0x02000791 RID: 1937
		[Editor("", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[TypeConverter(typeof(DesignerOptionService.DesignerOptionConverter))]
		public sealed class DesignerOptionCollection : IList, ICollection, IEnumerable
		{
			// Token: 0x06003D43 RID: 15683 RVA: 0x000D90CC File Offset: 0x000D72CC
			internal DesignerOptionCollection(DesignerOptionService service, DesignerOptionService.DesignerOptionCollection parent, string name, object value)
			{
				this._service = service;
				this._parent = parent;
				this._name = name;
				this._value = value;
				if (this._parent != null)
				{
					if (this._parent._children == null)
					{
						this._parent._children = new ArrayList(1);
					}
					this._parent._children.Add(this);
				}
			}

			/// <summary>Gets the number of child option collections this <see cref="T:System.ComponentModel.Design.DesignerOptionService.DesignerOptionCollection" /> contains.</summary>
			/// <returns>The number of child option collections this <see cref="T:System.ComponentModel.Design.DesignerOptionService.DesignerOptionCollection" /> contains.</returns>
			// Token: 0x17000DE7 RID: 3559
			// (get) Token: 0x06003D44 RID: 15684 RVA: 0x000D9134 File Offset: 0x000D7334
			public int Count
			{
				get
				{
					this.EnsurePopulated();
					return this._children.Count;
				}
			}

			/// <summary>Gets the name of this <see cref="T:System.ComponentModel.Design.DesignerOptionService.DesignerOptionCollection" />.</summary>
			/// <returns>The name of this <see cref="T:System.ComponentModel.Design.DesignerOptionService.DesignerOptionCollection" />.</returns>
			// Token: 0x17000DE8 RID: 3560
			// (get) Token: 0x06003D45 RID: 15685 RVA: 0x000D9147 File Offset: 0x000D7347
			public string Name
			{
				get
				{
					return this._name;
				}
			}

			/// <summary>Gets the parent collection object.</summary>
			/// <returns>The parent collection object, or null if there is no parent.</returns>
			// Token: 0x17000DE9 RID: 3561
			// (get) Token: 0x06003D46 RID: 15686 RVA: 0x000D914F File Offset: 0x000D734F
			public DesignerOptionService.DesignerOptionCollection Parent
			{
				get
				{
					return this._parent;
				}
			}

			/// <summary>Gets the collection of properties offered by this <see cref="T:System.ComponentModel.Design.DesignerOptionService.DesignerOptionCollection" />, along with all of its children.</summary>
			/// <returns>The collection of properties offered by this <see cref="T:System.ComponentModel.Design.DesignerOptionService.DesignerOptionCollection" />, along with all of its children.</returns>
			// Token: 0x17000DEA RID: 3562
			// (get) Token: 0x06003D47 RID: 15687 RVA: 0x000D9158 File Offset: 0x000D7358
			public PropertyDescriptorCollection Properties
			{
				get
				{
					if (this._properties == null)
					{
						ArrayList arrayList;
						if (this._value != null)
						{
							PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this._value);
							arrayList = new ArrayList(properties.Count);
							using (IEnumerator enumerator = properties.GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									object obj = enumerator.Current;
									PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
									arrayList.Add(new DesignerOptionService.DesignerOptionCollection.WrappedPropertyDescriptor(propertyDescriptor, this._value));
								}
								goto IL_0076;
							}
						}
						arrayList = new ArrayList(1);
						IL_0076:
						this.EnsurePopulated();
						foreach (object obj2 in this._children)
						{
							DesignerOptionService.DesignerOptionCollection designerOptionCollection = (DesignerOptionService.DesignerOptionCollection)obj2;
							arrayList.AddRange(designerOptionCollection.Properties);
						}
						PropertyDescriptor[] array = (PropertyDescriptor[])arrayList.ToArray(typeof(PropertyDescriptor));
						this._properties = new PropertyDescriptorCollection(array, true);
					}
					return this._properties;
				}
			}

			/// <summary>Gets the child collection at the given index.</summary>
			/// <returns>The child collection at the specified index.</returns>
			/// <param name="index">The zero-based index of the child collection to get.</param>
			// Token: 0x17000DEB RID: 3563
			public DesignerOptionService.DesignerOptionCollection this[int index]
			{
				get
				{
					this.EnsurePopulated();
					if (index < 0 || index >= this._children.Count)
					{
						throw new IndexOutOfRangeException("index");
					}
					return (DesignerOptionService.DesignerOptionCollection)this._children[index];
				}
			}

			/// <summary>Gets the child collection at the given name.</summary>
			/// <returns>The child collection with the name specified by the <paramref name="name" /> parameter, or null if the name is not found.</returns>
			/// <param name="name">The name of the child collection.</param>
			// Token: 0x17000DEC RID: 3564
			public DesignerOptionService.DesignerOptionCollection this[string name]
			{
				get
				{
					this.EnsurePopulated();
					foreach (object obj in this._children)
					{
						DesignerOptionService.DesignerOptionCollection designerOptionCollection = (DesignerOptionService.DesignerOptionCollection)obj;
						if (string.Compare(designerOptionCollection.Name, name, true, CultureInfo.InvariantCulture) == 0)
						{
							return designerOptionCollection;
						}
					}
					return null;
				}
			}

			/// <summary>Copies the entire collection to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index of the target array.</summary>
			/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the collection. The <paramref name="array" /> must have zero-based indexing.</param>
			/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
			// Token: 0x06003D4A RID: 15690 RVA: 0x000D9318 File Offset: 0x000D7518
			public void CopyTo(Array array, int index)
			{
				this.EnsurePopulated();
				this._children.CopyTo(array, index);
			}

			// Token: 0x06003D4B RID: 15691 RVA: 0x000D932D File Offset: 0x000D752D
			private void EnsurePopulated()
			{
				if (this._children == null)
				{
					this._service.PopulateOptionCollection(this);
					if (this._children == null)
					{
						this._children = new ArrayList(1);
					}
				}
			}

			/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate this collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate this collection.</returns>
			// Token: 0x06003D4C RID: 15692 RVA: 0x000D9357 File Offset: 0x000D7557
			public IEnumerator GetEnumerator()
			{
				this.EnsurePopulated();
				return this._children.GetEnumerator();
			}

			/// <summary>Returns the index of the first occurrence of a given value in a range of this collection.</summary>
			/// <returns>The index of the first occurrence of value within the entire collection, if found; otherwise, the lower bound of the collection minus 1.</returns>
			/// <param name="value">The object to locate in the collection.</param>
			// Token: 0x06003D4D RID: 15693 RVA: 0x000D936A File Offset: 0x000D756A
			public int IndexOf(DesignerOptionService.DesignerOptionCollection value)
			{
				this.EnsurePopulated();
				return this._children.IndexOf(value);
			}

			// Token: 0x06003D4E RID: 15694 RVA: 0x000D9380 File Offset: 0x000D7580
			private static object RecurseFindValue(DesignerOptionService.DesignerOptionCollection options)
			{
				if (options._value != null)
				{
					return options._value;
				}
				foreach (object obj in options)
				{
					object obj2 = DesignerOptionService.DesignerOptionCollection.RecurseFindValue((DesignerOptionService.DesignerOptionCollection)obj);
					if (obj2 != null)
					{
						return obj2;
					}
				}
				return null;
			}

			/// <summary>Displays a dialog box user interface (UI) with which the user can configure the options in this <see cref="T:System.ComponentModel.Design.DesignerOptionService.DesignerOptionCollection" />.</summary>
			/// <returns>true if the dialog box can be displayed; otherwise, false.</returns>
			// Token: 0x06003D4F RID: 15695 RVA: 0x000D93EC File Offset: 0x000D75EC
			public bool ShowDialog()
			{
				object obj = DesignerOptionService.DesignerOptionCollection.RecurseFindValue(this);
				return obj != null && this._service.ShowDialog(this, obj);
			}

			/// <summary>Gets a value indicating whether access to the collection is synchronized and, therefore, thread safe.</summary>
			/// <returns>true if the access to the collection is synchronized; otherwise, false.</returns>
			// Token: 0x17000DED RID: 3565
			// (get) Token: 0x06003D50 RID: 15696 RVA: 0x00003062 File Offset: 0x00001262
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
			/// <returns>An object that can be used to synchronize access to the collection.</returns>
			// Token: 0x17000DEE RID: 3566
			// (get) Token: 0x06003D51 RID: 15697 RVA: 0x00007575 File Offset: 0x00005775
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			/// <summary>Gets a value indicating whether the collection has a fixed size.</summary>
			/// <returns>true if the collection has a fixed size; otherwise, false.</returns>
			// Token: 0x17000DEF RID: 3567
			// (get) Token: 0x06003D52 RID: 15698 RVA: 0x0000390E File Offset: 0x00001B0E
			bool IList.IsFixedSize
			{
				get
				{
					return true;
				}
			}

			/// <summary>Gets a value indicating whether the collection is read-only.</summary>
			/// <returns>true if the collection is read-only; otherwise, false.</returns>
			// Token: 0x17000DF0 RID: 3568
			// (get) Token: 0x06003D53 RID: 15699 RVA: 0x0000390E File Offset: 0x00001B0E
			bool IList.IsReadOnly
			{
				get
				{
					return true;
				}
			}

			/// <summary>Gets or sets the element at the specified index.</summary>
			/// <returns>The element at the specified index.</returns>
			/// <param name="index">The zero-based index of the element to get or set.</param>
			// Token: 0x17000DF1 RID: 3569
			object IList.this[int index]
			{
				get
				{
					return this[index];
				}
				set
				{
					throw new NotSupportedException();
				}
			}

			/// <summary>Adds an item to the <see cref="T:System.Collections.IList" />.</summary>
			/// <returns>The position into which the new element was inserted.</returns>
			/// <param name="value">The <see cref="T:System.Object" /> to add to the <see cref="T:System.Collections.IList" />.</param>
			// Token: 0x06003D56 RID: 15702 RVA: 0x000044FA File Offset: 0x000026FA
			int IList.Add(object value)
			{
				throw new NotSupportedException();
			}

			/// <summary>Removes all items from the collection.</summary>
			// Token: 0x06003D57 RID: 15703 RVA: 0x000044FA File Offset: 0x000026FA
			void IList.Clear()
			{
				throw new NotSupportedException();
			}

			/// <summary>Determines whether the collection contains a specific value.</summary>
			/// <returns>true if the <see cref="T:System.Object" /> is found in the collection; otherwise, false. </returns>
			/// <param name="value">The <see cref="T:System.Object" /> to locate in the collection</param>
			// Token: 0x06003D58 RID: 15704 RVA: 0x000D941B File Offset: 0x000D761B
			bool IList.Contains(object value)
			{
				this.EnsurePopulated();
				return this._children.Contains(value);
			}

			/// <summary>Determines the index of a specific item in the collection.</summary>
			/// <returns>The index of <paramref name="value" /> if found in the list; otherwise, -1.</returns>
			/// <param name="value">The <see cref="T:System.Object" /> to locate in the collection.</param>
			// Token: 0x06003D59 RID: 15705 RVA: 0x000D936A File Offset: 0x000D756A
			int IList.IndexOf(object value)
			{
				this.EnsurePopulated();
				return this._children.IndexOf(value);
			}

			/// <summary>Inserts an item into the collection at the specified index.</summary>
			/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
			/// <param name="value">The <see cref="T:System.Object" /> to insert into the collection.</param>
			// Token: 0x06003D5A RID: 15706 RVA: 0x000044FA File Offset: 0x000026FA
			void IList.Insert(int index, object value)
			{
				throw new NotSupportedException();
			}

			/// <summary>Removes the first occurrence of a specific object from the collection.</summary>
			/// <param name="value">The <see cref="T:System.Object" /> to remove from the collection.</param>
			// Token: 0x06003D5B RID: 15707 RVA: 0x000044FA File Offset: 0x000026FA
			void IList.Remove(object value)
			{
				throw new NotSupportedException();
			}

			/// <summary>Removes the collection item at the specified index.</summary>
			/// <param name="index">The zero-based index of the item to remove.</param>
			// Token: 0x06003D5C RID: 15708 RVA: 0x000044FA File Offset: 0x000026FA
			void IList.RemoveAt(int index)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06003D5D RID: 15709 RVA: 0x00013B26 File Offset: 0x00011D26
			internal DesignerOptionCollection()
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}

			// Token: 0x040025F4 RID: 9716
			private DesignerOptionService _service;

			// Token: 0x040025F5 RID: 9717
			private DesignerOptionService.DesignerOptionCollection _parent;

			// Token: 0x040025F6 RID: 9718
			private string _name;

			// Token: 0x040025F7 RID: 9719
			private object _value;

			// Token: 0x040025F8 RID: 9720
			private ArrayList _children;

			// Token: 0x040025F9 RID: 9721
			private PropertyDescriptorCollection _properties;

			// Token: 0x02000792 RID: 1938
			private sealed class WrappedPropertyDescriptor : PropertyDescriptor
			{
				// Token: 0x06003D5E RID: 15710 RVA: 0x000D942F File Offset: 0x000D762F
				internal WrappedPropertyDescriptor(PropertyDescriptor property, object target)
					: base(property.Name, null)
				{
					this.property = property;
					this.target = target;
				}

				// Token: 0x17000DF2 RID: 3570
				// (get) Token: 0x06003D5F RID: 15711 RVA: 0x000D944C File Offset: 0x000D764C
				public override AttributeCollection Attributes
				{
					get
					{
						return this.property.Attributes;
					}
				}

				// Token: 0x17000DF3 RID: 3571
				// (get) Token: 0x06003D60 RID: 15712 RVA: 0x000D9459 File Offset: 0x000D7659
				public override Type ComponentType
				{
					get
					{
						return this.property.ComponentType;
					}
				}

				// Token: 0x17000DF4 RID: 3572
				// (get) Token: 0x06003D61 RID: 15713 RVA: 0x000D9466 File Offset: 0x000D7666
				public override bool IsReadOnly
				{
					get
					{
						return this.property.IsReadOnly;
					}
				}

				// Token: 0x17000DF5 RID: 3573
				// (get) Token: 0x06003D62 RID: 15714 RVA: 0x000D9473 File Offset: 0x000D7673
				public override Type PropertyType
				{
					get
					{
						return this.property.PropertyType;
					}
				}

				// Token: 0x06003D63 RID: 15715 RVA: 0x000D9480 File Offset: 0x000D7680
				public override bool CanResetValue(object component)
				{
					return this.property.CanResetValue(this.target);
				}

				// Token: 0x06003D64 RID: 15716 RVA: 0x000D9493 File Offset: 0x000D7693
				public override object GetValue(object component)
				{
					return this.property.GetValue(this.target);
				}

				// Token: 0x06003D65 RID: 15717 RVA: 0x000D94A6 File Offset: 0x000D76A6
				public override void ResetValue(object component)
				{
					this.property.ResetValue(this.target);
				}

				// Token: 0x06003D66 RID: 15718 RVA: 0x000D94B9 File Offset: 0x000D76B9
				public override void SetValue(object component, object value)
				{
					this.property.SetValue(this.target, value);
				}

				// Token: 0x06003D67 RID: 15719 RVA: 0x000D94CD File Offset: 0x000D76CD
				public override bool ShouldSerializeValue(object component)
				{
					return this.property.ShouldSerializeValue(this.target);
				}

				// Token: 0x040025FA RID: 9722
				private object target;

				// Token: 0x040025FB RID: 9723
				private PropertyDescriptor property;
			}
		}

		// Token: 0x02000793 RID: 1939
		internal sealed class DesignerOptionConverter : TypeConverter
		{
			// Token: 0x06003D68 RID: 15720 RVA: 0x0000390E File Offset: 0x00001B0E
			public override bool GetPropertiesSupported(ITypeDescriptorContext cxt)
			{
				return true;
			}

			// Token: 0x06003D69 RID: 15721 RVA: 0x000D94E0 File Offset: 0x000D76E0
			public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext cxt, object value, Attribute[] attributes)
			{
				PropertyDescriptorCollection propertyDescriptorCollection = new PropertyDescriptorCollection(null);
				DesignerOptionService.DesignerOptionCollection designerOptionCollection = value as DesignerOptionService.DesignerOptionCollection;
				if (designerOptionCollection == null)
				{
					return propertyDescriptorCollection;
				}
				foreach (object obj in designerOptionCollection)
				{
					DesignerOptionService.DesignerOptionCollection designerOptionCollection2 = (DesignerOptionService.DesignerOptionCollection)obj;
					propertyDescriptorCollection.Add(new DesignerOptionService.DesignerOptionConverter.OptionPropertyDescriptor(designerOptionCollection2));
				}
				foreach (object obj2 in designerOptionCollection.Properties)
				{
					PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj2;
					propertyDescriptorCollection.Add(propertyDescriptor);
				}
				return propertyDescriptorCollection;
			}

			// Token: 0x06003D6A RID: 15722 RVA: 0x000D95A0 File Offset: 0x000D77A0
			public override object ConvertTo(ITypeDescriptorContext cxt, CultureInfo culture, object value, Type destinationType)
			{
				if (destinationType == typeof(string))
				{
					return SR.GetString("(Collection)");
				}
				return base.ConvertTo(cxt, culture, value, destinationType);
			}

			// Token: 0x02000794 RID: 1940
			private class OptionPropertyDescriptor : PropertyDescriptor
			{
				// Token: 0x06003D6C RID: 15724 RVA: 0x000D95CB File Offset: 0x000D77CB
				internal OptionPropertyDescriptor(DesignerOptionService.DesignerOptionCollection option)
					: base(option.Name, null)
				{
					this._option = option;
				}

				// Token: 0x17000DF6 RID: 3574
				// (get) Token: 0x06003D6D RID: 15725 RVA: 0x000D95E1 File Offset: 0x000D77E1
				public override Type ComponentType
				{
					get
					{
						return this._option.GetType();
					}
				}

				// Token: 0x17000DF7 RID: 3575
				// (get) Token: 0x06003D6E RID: 15726 RVA: 0x0000390E File Offset: 0x00001B0E
				public override bool IsReadOnly
				{
					get
					{
						return true;
					}
				}

				// Token: 0x17000DF8 RID: 3576
				// (get) Token: 0x06003D6F RID: 15727 RVA: 0x000D95E1 File Offset: 0x000D77E1
				public override Type PropertyType
				{
					get
					{
						return this._option.GetType();
					}
				}

				// Token: 0x06003D70 RID: 15728 RVA: 0x00003062 File Offset: 0x00001262
				public override bool CanResetValue(object component)
				{
					return false;
				}

				// Token: 0x06003D71 RID: 15729 RVA: 0x000D95EE File Offset: 0x000D77EE
				public override object GetValue(object component)
				{
					return this._option;
				}

				// Token: 0x06003D72 RID: 15730 RVA: 0x00003917 File Offset: 0x00001B17
				public override void ResetValue(object component)
				{
				}

				// Token: 0x06003D73 RID: 15731 RVA: 0x00003917 File Offset: 0x00001B17
				public override void SetValue(object component, object value)
				{
				}

				// Token: 0x06003D74 RID: 15732 RVA: 0x00003062 File Offset: 0x00001262
				public override bool ShouldSerializeValue(object component)
				{
					return false;
				}

				// Token: 0x040025FC RID: 9724
				private DesignerOptionService.DesignerOptionCollection _option;
			}
		}
	}
}
