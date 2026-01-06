using System;

namespace System.ComponentModel
{
	/// <summary>Specifies whether a member is typically used for binding. This class cannot be inherited.</summary>
	// Token: 0x02000697 RID: 1687
	[AttributeUsage(AttributeTargets.All)]
	public sealed class BindableAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.BindableAttribute" /> class with a Boolean value.</summary>
		/// <param name="bindable">true to use property for binding; otherwise, false.</param>
		// Token: 0x06003602 RID: 13826 RVA: 0x000BFD51 File Offset: 0x000BDF51
		public BindableAttribute(bool bindable)
			: this(bindable, BindingDirection.OneWay)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.BindableAttribute" /> class.</summary>
		/// <param name="bindable">true to use property for binding; otherwise, false.</param>
		/// <param name="direction">One of the <see cref="T:System.ComponentModel.BindingDirection" /> values.</param>
		// Token: 0x06003603 RID: 13827 RVA: 0x000BFD5B File Offset: 0x000BDF5B
		public BindableAttribute(bool bindable, BindingDirection direction)
		{
			this.Bindable = bindable;
			this.Direction = direction;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.BindableAttribute" /> class with one of the <see cref="T:System.ComponentModel.BindableSupport" /> values.</summary>
		/// <param name="flags">One of the <see cref="T:System.ComponentModel.BindableSupport" /> values. </param>
		// Token: 0x06003604 RID: 13828 RVA: 0x000BFD71 File Offset: 0x000BDF71
		public BindableAttribute(BindableSupport flags)
			: this(flags, BindingDirection.OneWay)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.BindableAttribute" /> class.</summary>
		/// <param name="flags">One of the <see cref="T:System.ComponentModel.BindableSupport" /> values. </param>
		/// <param name="direction">One of the <see cref="T:System.ComponentModel.BindingDirection" /> values.</param>
		// Token: 0x06003605 RID: 13829 RVA: 0x000BFD7B File Offset: 0x000BDF7B
		public BindableAttribute(BindableSupport flags, BindingDirection direction)
		{
			this.Bindable = flags > BindableSupport.No;
			this._isDefault = flags == BindableSupport.Default;
			this.Direction = direction;
		}

		/// <summary>Gets a value indicating that a property is typically used for binding.</summary>
		/// <returns>true if the property is typically used for binding; otherwise, false.</returns>
		// Token: 0x17000C74 RID: 3188
		// (get) Token: 0x06003606 RID: 13830 RVA: 0x000BFD9E File Offset: 0x000BDF9E
		public bool Bindable { get; }

		/// <summary>Gets a value indicating the direction or directions of this property's data binding.</summary>
		/// <returns>The direction of this property’s data binding.</returns>
		// Token: 0x17000C75 RID: 3189
		// (get) Token: 0x06003607 RID: 13831 RVA: 0x000BFDA6 File Offset: 0x000BDFA6
		public BindingDirection Direction { get; }

		/// <summary>Determines whether two <see cref="T:System.ComponentModel.BindableAttribute" /> objects are equal.</summary>
		/// <returns>true if the specified <see cref="T:System.ComponentModel.BindableAttribute" /> is equal to the current <see cref="T:System.ComponentModel.BindableAttribute" />; false if it is not equal.</returns>
		/// <param name="obj">The object to compare.</param>
		// Token: 0x06003608 RID: 13832 RVA: 0x000BFDAE File Offset: 0x000BDFAE
		public override bool Equals(object obj)
		{
			return obj == this || (obj != null && obj is BindableAttribute && ((BindableAttribute)obj).Bindable == this.Bindable);
		}

		/// <summary>Serves as a hash function for the <see cref="T:System.ComponentModel.BindableAttribute" /> class.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.BindableAttribute" />.</returns>
		// Token: 0x06003609 RID: 13833 RVA: 0x000BFDD8 File Offset: 0x000BDFD8
		public override int GetHashCode()
		{
			return this.Bindable.GetHashCode();
		}

		/// <summary>Determines if this attribute is the default.</summary>
		/// <returns>true if the attribute is the default value for this attribute class; otherwise, false.</returns>
		// Token: 0x0600360A RID: 13834 RVA: 0x000BFDF3 File Offset: 0x000BDFF3
		public override bool IsDefaultAttribute()
		{
			return this.Equals(BindableAttribute.Default) || this._isDefault;
		}

		/// <summary>Specifies that a property is typically used for binding. This field is read-only.</summary>
		// Token: 0x04002047 RID: 8263
		public static readonly BindableAttribute Yes = new BindableAttribute(true);

		/// <summary>Specifies that a property is not typically used for binding. This field is read-only.</summary>
		// Token: 0x04002048 RID: 8264
		public static readonly BindableAttribute No = new BindableAttribute(false);

		/// <summary>Specifies the default value for the <see cref="T:System.ComponentModel.BindableAttribute" />, which is <see cref="F:System.ComponentModel.BindableAttribute.No" />. This field is read-only.</summary>
		// Token: 0x04002049 RID: 8265
		public static readonly BindableAttribute Default = BindableAttribute.No;

		// Token: 0x0400204A RID: 8266
		private bool _isDefault;
	}
}
