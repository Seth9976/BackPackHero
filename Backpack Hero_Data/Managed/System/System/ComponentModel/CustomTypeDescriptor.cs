using System;

namespace System.ComponentModel
{
	/// <summary>Provides a simple default implementation of the <see cref="T:System.ComponentModel.ICustomTypeDescriptor" /> interface.</summary>
	// Token: 0x020006A8 RID: 1704
	public abstract class CustomTypeDescriptor : ICustomTypeDescriptor
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.CustomTypeDescriptor" /> class.</summary>
		// Token: 0x06003680 RID: 13952 RVA: 0x0000219B File Offset: 0x0000039B
		protected CustomTypeDescriptor()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.CustomTypeDescriptor" /> class using a parent custom type descriptor.</summary>
		/// <param name="parent">The parent custom type descriptor.</param>
		// Token: 0x06003681 RID: 13953 RVA: 0x000C1FD3 File Offset: 0x000C01D3
		protected CustomTypeDescriptor(ICustomTypeDescriptor parent)
		{
			this._parent = parent;
		}

		/// <summary>Returns a collection of custom attributes for the type represented by this type descriptor.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.AttributeCollection" /> containing the attributes for the type. The default is <see cref="F:System.ComponentModel.AttributeCollection.Empty" />.</returns>
		// Token: 0x06003682 RID: 13954 RVA: 0x000C1FE2 File Offset: 0x000C01E2
		public virtual AttributeCollection GetAttributes()
		{
			if (this._parent != null)
			{
				return this._parent.GetAttributes();
			}
			return AttributeCollection.Empty;
		}

		/// <summary>Returns the fully qualified name of the class represented by this type descriptor.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the fully qualified class name of the type this type descriptor is describing. The default is null.</returns>
		// Token: 0x06003683 RID: 13955 RVA: 0x000C1FFD File Offset: 0x000C01FD
		public virtual string GetClassName()
		{
			ICustomTypeDescriptor parent = this._parent;
			if (parent == null)
			{
				return null;
			}
			return parent.GetClassName();
		}

		/// <summary>Returns the name of the class represented by this type descriptor.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the name of the component instance this type descriptor is describing. The default is null.</returns>
		// Token: 0x06003684 RID: 13956 RVA: 0x000C2010 File Offset: 0x000C0210
		public virtual string GetComponentName()
		{
			ICustomTypeDescriptor parent = this._parent;
			if (parent == null)
			{
				return null;
			}
			return parent.GetComponentName();
		}

		/// <summary>Returns a type converter for the type represented by this type descriptor.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter" /> for the type represented by this type descriptor. The default is a newly created <see cref="T:System.ComponentModel.TypeConverter" />.</returns>
		// Token: 0x06003685 RID: 13957 RVA: 0x000C2023 File Offset: 0x000C0223
		public virtual TypeConverter GetConverter()
		{
			if (this._parent != null)
			{
				return this._parent.GetConverter();
			}
			return new TypeConverter();
		}

		/// <summary>Returns the event descriptor for the default event of the object represented by this type descriptor.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.EventDescriptor" /> for the default event on the object represented by this type descriptor. The default is null.</returns>
		// Token: 0x06003686 RID: 13958 RVA: 0x000C203E File Offset: 0x000C023E
		public virtual EventDescriptor GetDefaultEvent()
		{
			ICustomTypeDescriptor parent = this._parent;
			if (parent == null)
			{
				return null;
			}
			return parent.GetDefaultEvent();
		}

		/// <summary>Returns the property descriptor for the default property of the object represented by this type descriptor.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptor" /> for the default property on the object represented by this type descriptor. The default is null.</returns>
		// Token: 0x06003687 RID: 13959 RVA: 0x000C2051 File Offset: 0x000C0251
		public virtual PropertyDescriptor GetDefaultProperty()
		{
			ICustomTypeDescriptor parent = this._parent;
			if (parent == null)
			{
				return null;
			}
			return parent.GetDefaultProperty();
		}

		/// <summary>Returns an editor of the specified type that is to be associated with the class represented by this type descriptor.</summary>
		/// <returns>An editor of the given type that is to be associated with the class represented by this type descriptor. The default is null.</returns>
		/// <param name="editorBaseType">The base type of the editor to retrieve.</param>
		// Token: 0x06003688 RID: 13960 RVA: 0x000C2064 File Offset: 0x000C0264
		public virtual object GetEditor(Type editorBaseType)
		{
			ICustomTypeDescriptor parent = this._parent;
			if (parent == null)
			{
				return null;
			}
			return parent.GetEditor(editorBaseType);
		}

		/// <summary>Returns a collection of event descriptors for the object represented by this type descriptor.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EventDescriptorCollection" /> containing the event descriptors for the object represented by this type descriptor. The default is <see cref="F:System.ComponentModel.EventDescriptorCollection.Empty" />.</returns>
		// Token: 0x06003689 RID: 13961 RVA: 0x000C2078 File Offset: 0x000C0278
		public virtual EventDescriptorCollection GetEvents()
		{
			if (this._parent != null)
			{
				return this._parent.GetEvents();
			}
			return EventDescriptorCollection.Empty;
		}

		/// <summary>Returns a filtered collection of event descriptors for the object represented by this type descriptor.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EventDescriptorCollection" /> containing the event descriptions for the object represented by this type descriptor. The default is <see cref="F:System.ComponentModel.EventDescriptorCollection.Empty" />.</returns>
		/// <param name="attributes">An array of attributes to use as a filter. This can be null.</param>
		// Token: 0x0600368A RID: 13962 RVA: 0x000C2093 File Offset: 0x000C0293
		public virtual EventDescriptorCollection GetEvents(Attribute[] attributes)
		{
			if (this._parent != null)
			{
				return this._parent.GetEvents(attributes);
			}
			return EventDescriptorCollection.Empty;
		}

		/// <summary>Returns a collection of property descriptors for the object represented by this type descriptor.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> containing the property descriptions for the object represented by this type descriptor. The default is <see cref="F:System.ComponentModel.PropertyDescriptorCollection.Empty" />.</returns>
		// Token: 0x0600368B RID: 13963 RVA: 0x000C20AF File Offset: 0x000C02AF
		public virtual PropertyDescriptorCollection GetProperties()
		{
			if (this._parent != null)
			{
				return this._parent.GetProperties();
			}
			return PropertyDescriptorCollection.Empty;
		}

		/// <summary>Returns a filtered collection of property descriptors for the object represented by this type descriptor.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> containing the property descriptions for the object represented by this type descriptor. The default is <see cref="F:System.ComponentModel.PropertyDescriptorCollection.Empty" />.</returns>
		/// <param name="attributes">An array of attributes to use as a filter. This can be null.</param>
		// Token: 0x0600368C RID: 13964 RVA: 0x000C20CA File Offset: 0x000C02CA
		public virtual PropertyDescriptorCollection GetProperties(Attribute[] attributes)
		{
			if (this._parent != null)
			{
				return this._parent.GetProperties(attributes);
			}
			return PropertyDescriptorCollection.Empty;
		}

		/// <summary>Returns an object that contains the property described by the specified property descriptor.</summary>
		/// <returns>An <see cref="T:System.Object" /> that owns the given property specified by the type descriptor. The default is null.</returns>
		/// <param name="pd">The property descriptor for which to retrieve the owning object.</param>
		// Token: 0x0600368D RID: 13965 RVA: 0x000C20E6 File Offset: 0x000C02E6
		public virtual object GetPropertyOwner(PropertyDescriptor pd)
		{
			ICustomTypeDescriptor parent = this._parent;
			if (parent == null)
			{
				return null;
			}
			return parent.GetPropertyOwner(pd);
		}

		// Token: 0x04002070 RID: 8304
		private readonly ICustomTypeDescriptor _parent;
	}
}
