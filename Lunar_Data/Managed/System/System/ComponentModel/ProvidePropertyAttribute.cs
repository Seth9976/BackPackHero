using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the name of the property that an implementer of <see cref="T:System.ComponentModel.IExtenderProvider" /> offers to other components. This class cannot be inherited</summary>
	// Token: 0x020006F7 RID: 1783
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public sealed class ProvidePropertyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ProvidePropertyAttribute" /> class with the name of the property and its <see cref="T:System.Type" />.</summary>
		/// <param name="propertyName">The name of the property extending to an object of the specified type. </param>
		/// <param name="receiverType">The <see cref="T:System.Type" /> of the data type of the object that can receive the property. </param>
		// Token: 0x06003926 RID: 14630 RVA: 0x000C7844 File Offset: 0x000C5A44
		public ProvidePropertyAttribute(string propertyName, Type receiverType)
		{
			this.PropertyName = propertyName;
			this.ReceiverTypeName = receiverType.AssemblyQualifiedName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ProvidePropertyAttribute" /> class with the name of the property and the type of its receiver.</summary>
		/// <param name="propertyName">The name of the property extending to an object of the specified type. </param>
		/// <param name="receiverTypeName">The name of the data type this property can extend. </param>
		// Token: 0x06003927 RID: 14631 RVA: 0x000C785F File Offset: 0x000C5A5F
		public ProvidePropertyAttribute(string propertyName, string receiverTypeName)
		{
			this.PropertyName = propertyName;
			this.ReceiverTypeName = receiverTypeName;
		}

		/// <summary>Gets the name of a property that this class provides.</summary>
		/// <returns>The name of a property that this class provides.</returns>
		// Token: 0x17000D36 RID: 3382
		// (get) Token: 0x06003928 RID: 14632 RVA: 0x000C7875 File Offset: 0x000C5A75
		public string PropertyName { get; }

		/// <summary>Gets the name of the data type this property can extend.</summary>
		/// <returns>The name of the data type this property can extend.</returns>
		// Token: 0x17000D37 RID: 3383
		// (get) Token: 0x06003929 RID: 14633 RVA: 0x000C787D File Offset: 0x000C5A7D
		public string ReceiverTypeName { get; }

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.ProvidePropertyAttribute" />.</summary>
		/// <returns>true if the value of the given object is equal to that of the current; otherwise, false.</returns>
		/// <param name="obj">The object to test the value equality of. </param>
		// Token: 0x0600392A RID: 14634 RVA: 0x000C7888 File Offset: 0x000C5A88
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			ProvidePropertyAttribute providePropertyAttribute = obj as ProvidePropertyAttribute;
			return providePropertyAttribute != null && providePropertyAttribute.PropertyName == this.PropertyName && providePropertyAttribute.ReceiverTypeName == this.ReceiverTypeName;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.ProvidePropertyAttribute" />.</returns>
		// Token: 0x0600392B RID: 14635 RVA: 0x000C78CB File Offset: 0x000C5ACB
		public override int GetHashCode()
		{
			return this.PropertyName.GetHashCode() ^ this.ReceiverTypeName.GetHashCode();
		}

		/// <summary>Gets a unique identifier for this attribute.</summary>
		/// <returns>An <see cref="T:System.Object" /> that is a unique identifier for the attribute.</returns>
		// Token: 0x17000D38 RID: 3384
		// (get) Token: 0x0600392C RID: 14636 RVA: 0x000C78E4 File Offset: 0x000C5AE4
		public override object TypeId
		{
			get
			{
				return base.GetType().FullName + this.PropertyName;
			}
		}
	}
}
