using System;
using System.Collections;

namespace System.ComponentModel
{
	/// <summary>Provides an abstraction of a property on a class.</summary>
	// Token: 0x020006F4 RID: 1780
	public abstract class PropertyDescriptor : MemberDescriptor
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.PropertyDescriptor" /> class with the specified name and attributes.</summary>
		/// <param name="name">The name of the property. </param>
		/// <param name="attrs">An array of type <see cref="T:System.Attribute" /> that contains the property attributes. </param>
		// Token: 0x060038CD RID: 14541 RVA: 0x000C2EE7 File Offset: 0x000C10E7
		protected PropertyDescriptor(string name, Attribute[] attrs)
			: base(name, attrs)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.PropertyDescriptor" /> class with the name and attributes in the specified <see cref="T:System.ComponentModel.MemberDescriptor" />.</summary>
		/// <param name="descr">A <see cref="T:System.ComponentModel.MemberDescriptor" /> that contains the name of the property and its attributes. </param>
		// Token: 0x060038CE RID: 14542 RVA: 0x000C2EF1 File Offset: 0x000C10F1
		protected PropertyDescriptor(MemberDescriptor descr)
			: base(descr)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.PropertyDescriptor" /> class with the name in the specified <see cref="T:System.ComponentModel.MemberDescriptor" /> and the attributes in both the <see cref="T:System.ComponentModel.MemberDescriptor" /> and the <see cref="T:System.Attribute" /> array.</summary>
		/// <param name="descr">A <see cref="T:System.ComponentModel.MemberDescriptor" /> containing the name of the member and its attributes. </param>
		/// <param name="attrs">An <see cref="T:System.Attribute" /> array containing the attributes you want to associate with the property. </param>
		// Token: 0x060038CF RID: 14543 RVA: 0x000C2EFA File Offset: 0x000C10FA
		protected PropertyDescriptor(MemberDescriptor descr, Attribute[] attrs)
			: base(descr, attrs)
		{
		}

		/// <summary>When overridden in a derived class, gets the type of the component this property is bound to.</summary>
		/// <returns>A <see cref="T:System.Type" /> that represents the type of component this property is bound to. When the <see cref="M:System.ComponentModel.PropertyDescriptor.GetValue(System.Object)" /> or <see cref="M:System.ComponentModel.PropertyDescriptor.SetValue(System.Object,System.Object)" /> methods are invoked, the object specified might be an instance of this type.</returns>
		// Token: 0x17000D1D RID: 3357
		// (get) Token: 0x060038D0 RID: 14544
		public abstract Type ComponentType { get; }

		/// <summary>Gets the type converter for this property.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter" /> that is used to convert the <see cref="T:System.Type" /> of this property.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000D1E RID: 3358
		// (get) Token: 0x060038D1 RID: 14545 RVA: 0x000C6934 File Offset: 0x000C4B34
		public virtual TypeConverter Converter
		{
			get
			{
				AttributeCollection attributes = this.Attributes;
				if (this._converter == null)
				{
					TypeConverterAttribute typeConverterAttribute = (TypeConverterAttribute)attributes[typeof(TypeConverterAttribute)];
					if (typeConverterAttribute.ConverterTypeName != null && typeConverterAttribute.ConverterTypeName.Length > 0)
					{
						Type typeFromName = this.GetTypeFromName(typeConverterAttribute.ConverterTypeName);
						if (typeFromName != null && typeof(TypeConverter).IsAssignableFrom(typeFromName))
						{
							this._converter = (TypeConverter)this.CreateInstance(typeFromName);
						}
					}
					if (this._converter == null)
					{
						this._converter = TypeDescriptor.GetConverter(this.PropertyType);
					}
				}
				return this._converter;
			}
		}

		/// <summary>Gets a value indicating whether this property should be localized, as specified in the <see cref="T:System.ComponentModel.LocalizableAttribute" />.</summary>
		/// <returns>true if the member is marked with the <see cref="T:System.ComponentModel.LocalizableAttribute" /> set to true; otherwise, false.</returns>
		// Token: 0x17000D1F RID: 3359
		// (get) Token: 0x060038D2 RID: 14546 RVA: 0x000C69D5 File Offset: 0x000C4BD5
		public virtual bool IsLocalizable
		{
			get
			{
				return LocalizableAttribute.Yes.Equals(this.Attributes[typeof(LocalizableAttribute)]);
			}
		}

		/// <summary>When overridden in a derived class, gets a value indicating whether this property is read-only.</summary>
		/// <returns>true if the property is read-only; otherwise, false.</returns>
		// Token: 0x17000D20 RID: 3360
		// (get) Token: 0x060038D3 RID: 14547
		public abstract bool IsReadOnly { get; }

		/// <summary>Gets a value indicating whether this property should be serialized, as specified in the <see cref="T:System.ComponentModel.DesignerSerializationVisibilityAttribute" />.</summary>
		/// <returns>One of the <see cref="T:System.ComponentModel.DesignerSerializationVisibility" /> enumeration values that specifies whether this property should be serialized.</returns>
		// Token: 0x17000D21 RID: 3361
		// (get) Token: 0x060038D4 RID: 14548 RVA: 0x000C69F6 File Offset: 0x000C4BF6
		public DesignerSerializationVisibility SerializationVisibility
		{
			get
			{
				return ((DesignerSerializationVisibilityAttribute)this.Attributes[typeof(DesignerSerializationVisibilityAttribute)]).Visibility;
			}
		}

		/// <summary>When overridden in a derived class, gets the type of the property.</summary>
		/// <returns>A <see cref="T:System.Type" /> that represents the type of the property.</returns>
		// Token: 0x17000D22 RID: 3362
		// (get) Token: 0x060038D5 RID: 14549
		public abstract Type PropertyType { get; }

		/// <summary>Enables other objects to be notified when this property changes.</summary>
		/// <param name="component">The component to add the handler for. </param>
		/// <param name="handler">The delegate to add as a listener. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> or <paramref name="handler" /> is null.</exception>
		// Token: 0x060038D6 RID: 14550 RVA: 0x000C6A18 File Offset: 0x000C4C18
		public virtual void AddValueChanged(object component, EventHandler handler)
		{
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			if (this._valueChangedHandlers == null)
			{
				this._valueChangedHandlers = new Hashtable();
			}
			EventHandler eventHandler = (EventHandler)this._valueChangedHandlers[component];
			this._valueChangedHandlers[component] = Delegate.Combine(eventHandler, handler);
		}

		/// <summary>When overridden in a derived class, returns whether resetting an object changes its value.</summary>
		/// <returns>true if resetting the component changes its value; otherwise, false.</returns>
		/// <param name="component">The component to test for reset capability. </param>
		// Token: 0x060038D7 RID: 14551
		public abstract bool CanResetValue(object component);

		/// <summary>Compares this to another object to see if they are equivalent.</summary>
		/// <returns>true if the values are equivalent; otherwise, false.</returns>
		/// <param name="obj">The object to compare to this <see cref="T:System.ComponentModel.PropertyDescriptor" />. </param>
		// Token: 0x060038D8 RID: 14552 RVA: 0x000C6A7C File Offset: 0x000C4C7C
		public override bool Equals(object obj)
		{
			try
			{
				if (obj == this)
				{
					return true;
				}
				if (obj == null)
				{
					return false;
				}
				PropertyDescriptor propertyDescriptor = obj as PropertyDescriptor;
				if (propertyDescriptor != null && propertyDescriptor.NameHashCode == this.NameHashCode && propertyDescriptor.PropertyType == this.PropertyType && propertyDescriptor.Name.Equals(this.Name))
				{
					return true;
				}
			}
			catch
			{
			}
			return false;
		}

		/// <summary>Creates an instance of the specified type.</summary>
		/// <returns>A new instance of the type.</returns>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the type to create. </param>
		// Token: 0x060038D9 RID: 14553 RVA: 0x000C6AF4 File Offset: 0x000C4CF4
		protected object CreateInstance(Type type)
		{
			Type[] array = new Type[] { typeof(Type) };
			if (type.GetConstructor(array) != null)
			{
				return TypeDescriptor.CreateInstance(null, type, array, new object[] { this.PropertyType });
			}
			return TypeDescriptor.CreateInstance(null, type, null, null);
		}

		/// <summary>Adds the attributes of the <see cref="T:System.ComponentModel.PropertyDescriptor" /> to the specified list of attributes in the parent class.</summary>
		/// <param name="attributeList">An <see cref="T:System.Collections.IList" /> that lists the attributes in the parent class. Initially, this is empty.</param>
		// Token: 0x060038DA RID: 14554 RVA: 0x000C6B45 File Offset: 0x000C4D45
		protected override void FillAttributes(IList attributeList)
		{
			this._converter = null;
			this._editors = null;
			this._editorTypes = null;
			this._editorCount = 0;
			base.FillAttributes(attributeList);
		}

		/// <summary>Returns the default <see cref="T:System.ComponentModel.PropertyDescriptorCollection" />.</summary>
		/// <returns>A collection of property descriptor.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060038DB RID: 14555 RVA: 0x000C6B6A File Offset: 0x000C4D6A
		public PropertyDescriptorCollection GetChildProperties()
		{
			return this.GetChildProperties(null, null);
		}

		/// <summary>Returns a <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> using a specified array of attributes as a filter.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> with the properties that match the specified attributes.</returns>
		/// <param name="filter">An array of type <see cref="T:System.Attribute" /> to use as a filter. </param>
		// Token: 0x060038DC RID: 14556 RVA: 0x000C6B74 File Offset: 0x000C4D74
		public PropertyDescriptorCollection GetChildProperties(Attribute[] filter)
		{
			return this.GetChildProperties(null, filter);
		}

		/// <summary>Returns a <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> for a given object.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> with the properties for the specified component.</returns>
		/// <param name="instance">A component to get the properties for. </param>
		// Token: 0x060038DD RID: 14557 RVA: 0x000C6B7E File Offset: 0x000C4D7E
		public PropertyDescriptorCollection GetChildProperties(object instance)
		{
			return this.GetChildProperties(instance, null);
		}

		/// <summary>Returns a <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> for a given object using a specified array of attributes as a filter.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> with the properties that match the specified attributes for the specified component.</returns>
		/// <param name="instance">A component to get the properties for. </param>
		/// <param name="filter">An array of type <see cref="T:System.Attribute" /> to use as a filter. </param>
		// Token: 0x060038DE RID: 14558 RVA: 0x000C6B88 File Offset: 0x000C4D88
		public virtual PropertyDescriptorCollection GetChildProperties(object instance, Attribute[] filter)
		{
			if (instance == null)
			{
				return TypeDescriptor.GetProperties(this.PropertyType, filter);
			}
			return TypeDescriptor.GetProperties(instance, filter);
		}

		/// <summary>Gets an editor of the specified type.</summary>
		/// <returns>An instance of the requested editor type, or null if an editor cannot be found.</returns>
		/// <param name="editorBaseType">The base type of editor, which is used to differentiate between multiple editors that a property supports. </param>
		// Token: 0x060038DF RID: 14559 RVA: 0x000C6BA4 File Offset: 0x000C4DA4
		public virtual object GetEditor(Type editorBaseType)
		{
			object obj = null;
			AttributeCollection attributes = this.Attributes;
			if (this._editorTypes != null)
			{
				for (int i = 0; i < this._editorCount; i++)
				{
					if (this._editorTypes[i] == editorBaseType)
					{
						return this._editors[i];
					}
				}
			}
			if (obj == null)
			{
				for (int j = 0; j < attributes.Count; j++)
				{
					EditorAttribute editorAttribute = attributes[j] as EditorAttribute;
					if (editorAttribute != null)
					{
						Type typeFromName = this.GetTypeFromName(editorAttribute.EditorBaseTypeName);
						if (editorBaseType == typeFromName)
						{
							Type typeFromName2 = this.GetTypeFromName(editorAttribute.EditorTypeName);
							if (typeFromName2 != null)
							{
								obj = this.CreateInstance(typeFromName2);
								break;
							}
						}
					}
				}
				if (obj == null)
				{
					obj = TypeDescriptor.GetEditor(this.PropertyType, editorBaseType);
				}
				if (this._editorTypes == null)
				{
					this._editorTypes = new Type[5];
					this._editors = new object[5];
				}
				if (this._editorCount >= this._editorTypes.Length)
				{
					Type[] array = new Type[this._editorTypes.Length * 2];
					object[] array2 = new object[this._editors.Length * 2];
					Array.Copy(this._editorTypes, array, this._editorTypes.Length);
					Array.Copy(this._editors, array2, this._editors.Length);
					this._editorTypes = array;
					this._editors = array2;
				}
				this._editorTypes[this._editorCount] = editorBaseType;
				object[] editors = this._editors;
				int editorCount = this._editorCount;
				this._editorCount = editorCount + 1;
				editors[editorCount] = obj;
			}
			return obj;
		}

		/// <summary>Returns the hash code for this object.</summary>
		/// <returns>The hash code for this object.</returns>
		// Token: 0x060038E0 RID: 14560 RVA: 0x000C6D19 File Offset: 0x000C4F19
		public override int GetHashCode()
		{
			return this.NameHashCode ^ this.PropertyType.GetHashCode();
		}

		/// <summary>This method returns the object that should be used during invocation of members.</summary>
		/// <returns>The <see cref="T:System.Object" /> that should be used during invocation of members.</returns>
		/// <param name="type">The <see cref="T:System.Type" /> of the invocation target.</param>
		/// <param name="instance">The potential invocation target.</param>
		// Token: 0x060038E1 RID: 14561 RVA: 0x000C6D30 File Offset: 0x000C4F30
		protected override object GetInvocationTarget(Type type, object instance)
		{
			object obj = base.GetInvocationTarget(type, instance);
			ICustomTypeDescriptor customTypeDescriptor = obj as ICustomTypeDescriptor;
			if (customTypeDescriptor != null)
			{
				obj = customTypeDescriptor.GetPropertyOwner(this);
			}
			return obj;
		}

		/// <summary>Returns a type using its name.</summary>
		/// <returns>A <see cref="T:System.Type" /> that matches the given type name, or null if a match cannot be found.</returns>
		/// <param name="typeName">The assembly-qualified name of the type to retrieve. </param>
		// Token: 0x060038E2 RID: 14562 RVA: 0x000C6D5C File Offset: 0x000C4F5C
		protected Type GetTypeFromName(string typeName)
		{
			if (typeName == null || typeName.Length == 0)
			{
				return null;
			}
			Type type = Type.GetType(typeName);
			Type type2 = null;
			if (this.ComponentType != null && (type == null || this.ComponentType.Assembly.FullName.Equals(type.Assembly.FullName)))
			{
				int num = typeName.IndexOf(',');
				if (num != -1)
				{
					typeName = typeName.Substring(0, num);
				}
				type2 = this.ComponentType.Assembly.GetType(typeName);
			}
			return type2 ?? type;
		}

		/// <summary>When overridden in a derived class, gets the current value of the property on a component.</summary>
		/// <returns>The value of a property for a given component.</returns>
		/// <param name="component">The component with the property for which to retrieve the value. </param>
		// Token: 0x060038E3 RID: 14563
		public abstract object GetValue(object component);

		/// <summary>Raises the ValueChanged event that you implemented.</summary>
		/// <param name="component">The object that raises the event. </param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060038E4 RID: 14564 RVA: 0x000C6DE7 File Offset: 0x000C4FE7
		protected virtual void OnValueChanged(object component, EventArgs e)
		{
			if (component != null)
			{
				Hashtable valueChangedHandlers = this._valueChangedHandlers;
				EventHandler eventHandler = (EventHandler)((valueChangedHandlers != null) ? valueChangedHandlers[component] : null);
				if (eventHandler == null)
				{
					return;
				}
				eventHandler(component, e);
			}
		}

		/// <summary>Enables other objects to be notified when this property changes.</summary>
		/// <param name="component">The component to remove the handler for. </param>
		/// <param name="handler">The delegate to remove as a listener. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> or <paramref name="handler" /> is null.</exception>
		// Token: 0x060038E5 RID: 14565 RVA: 0x000C6E10 File Offset: 0x000C5010
		public virtual void RemoveValueChanged(object component, EventHandler handler)
		{
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			if (this._valueChangedHandlers != null)
			{
				EventHandler eventHandler = (EventHandler)this._valueChangedHandlers[component];
				eventHandler = (EventHandler)Delegate.Remove(eventHandler, handler);
				if (eventHandler != null)
				{
					this._valueChangedHandlers[component] = eventHandler;
					return;
				}
				this._valueChangedHandlers.Remove(component);
			}
		}

		/// <summary>Retrieves the current set of ValueChanged event handlers for a specific component</summary>
		/// <returns>A combined multicast event handler, or null if no event handlers are currently assigned to <paramref name="component" />.</returns>
		/// <param name="component">The component for which to retrieve event handlers.</param>
		// Token: 0x060038E6 RID: 14566 RVA: 0x000C6E7D File Offset: 0x000C507D
		protected internal EventHandler GetValueChangedHandler(object component)
		{
			if (component != null && this._valueChangedHandlers != null)
			{
				return (EventHandler)this._valueChangedHandlers[component];
			}
			return null;
		}

		/// <summary>When overridden in a derived class, resets the value for this property of the component to the default value.</summary>
		/// <param name="component">The component with the property value that is to be reset to the default value. </param>
		// Token: 0x060038E7 RID: 14567
		public abstract void ResetValue(object component);

		/// <summary>When overridden in a derived class, sets the value of the component to a different value.</summary>
		/// <param name="component">The component with the property value that is to be set. </param>
		/// <param name="value">The new value. </param>
		// Token: 0x060038E8 RID: 14568
		public abstract void SetValue(object component, object value);

		/// <summary>When overridden in a derived class, determines a value indicating whether the value of this property needs to be persisted.</summary>
		/// <returns>true if the property should be persisted; otherwise, false.</returns>
		/// <param name="component">The component with the property to be examined for persistence. </param>
		// Token: 0x060038E9 RID: 14569
		public abstract bool ShouldSerializeValue(object component);

		/// <summary>Gets a value indicating whether value change notifications for this property may originate from outside the property descriptor.</summary>
		/// <returns>true if value change notifications may originate from outside the property descriptor; otherwise, false.</returns>
		// Token: 0x17000D23 RID: 3363
		// (get) Token: 0x060038EA RID: 14570 RVA: 0x00003062 File Offset: 0x00001262
		public virtual bool SupportsChangeEvents
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0400212A RID: 8490
		private TypeConverter _converter;

		// Token: 0x0400212B RID: 8491
		private Hashtable _valueChangedHandlers;

		// Token: 0x0400212C RID: 8492
		private object[] _editors;

		// Token: 0x0400212D RID: 8493
		private Type[] _editorTypes;

		// Token: 0x0400212E RID: 8494
		private int _editorCount;
	}
}
