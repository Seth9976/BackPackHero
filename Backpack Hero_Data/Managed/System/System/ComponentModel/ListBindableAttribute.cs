using System;

namespace System.ComponentModel
{
	/// <summary>Specifies that a list can be used as a data source. A visual designer should use this attribute to determine whether to display a particular list in a data-binding picker. This class cannot be inherited.</summary>
	// Token: 0x020006E1 RID: 1761
	[AttributeUsage(AttributeTargets.All)]
	public sealed class ListBindableAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ListBindableAttribute" /> class using a value to indicate whether the list is bindable.</summary>
		/// <param name="listBindable">true if the list is bindable; otherwise, false. </param>
		// Token: 0x060037ED RID: 14317 RVA: 0x000C40DA File Offset: 0x000C22DA
		public ListBindableAttribute(bool listBindable)
		{
			this.ListBindable = listBindable;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ListBindableAttribute" /> class using <see cref="T:System.ComponentModel.BindableSupport" /> to indicate whether the list is bindable.</summary>
		/// <param name="flags">A <see cref="T:System.ComponentModel.BindableSupport" /> that indicates whether the list is bindable. </param>
		// Token: 0x060037EE RID: 14318 RVA: 0x000C40E9 File Offset: 0x000C22E9
		public ListBindableAttribute(BindableSupport flags)
		{
			this.ListBindable = flags > BindableSupport.No;
			this._isDefault = flags == BindableSupport.Default;
		}

		/// <summary>Gets whether the list is bindable.</summary>
		/// <returns>true if the list is bindable; otherwise, false.</returns>
		// Token: 0x17000CE5 RID: 3301
		// (get) Token: 0x060037EF RID: 14319 RVA: 0x000C4105 File Offset: 0x000C2305
		public bool ListBindable { get; }

		/// <summary>Returns whether the object passed is equal to this <see cref="T:System.ComponentModel.ListBindableAttribute" />.</summary>
		/// <returns>true if the object passed is equal to this <see cref="T:System.ComponentModel.ListBindableAttribute" />; otherwise, false.</returns>
		/// <param name="obj">The object to test equality with. </param>
		// Token: 0x060037F0 RID: 14320 RVA: 0x000C4110 File Offset: 0x000C2310
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			ListBindableAttribute listBindableAttribute = obj as ListBindableAttribute;
			return listBindableAttribute != null && listBindableAttribute.ListBindable == this.ListBindable;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.ListBindableAttribute" />.</returns>
		// Token: 0x060037F1 RID: 14321 RVA: 0x000BE516 File Offset: 0x000BC716
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Returns whether <see cref="P:System.ComponentModel.ListBindableAttribute.ListBindable" /> is set to the default value.</summary>
		/// <returns>true if <see cref="P:System.ComponentModel.ListBindableAttribute.ListBindable" /> is set to the default value; otherwise, false.</returns>
		// Token: 0x060037F2 RID: 14322 RVA: 0x000C413D File Offset: 0x000C233D
		public override bool IsDefaultAttribute()
		{
			return this.Equals(ListBindableAttribute.Default) || this._isDefault;
		}

		/// <summary>Specifies that the list is bindable. This static field is read-only.</summary>
		// Token: 0x040020C4 RID: 8388
		public static readonly ListBindableAttribute Yes = new ListBindableAttribute(true);

		/// <summary>Specifies that the list is not bindable. This static field is read-only.</summary>
		// Token: 0x040020C5 RID: 8389
		public static readonly ListBindableAttribute No = new ListBindableAttribute(false);

		/// <summary>Represents the default value for <see cref="T:System.ComponentModel.ListBindableAttribute" />.</summary>
		// Token: 0x040020C6 RID: 8390
		public static readonly ListBindableAttribute Default = ListBindableAttribute.Yes;

		// Token: 0x040020C7 RID: 8391
		private bool _isDefault;
	}
}
