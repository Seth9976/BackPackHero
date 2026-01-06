using System;

namespace System.ComponentModel
{
	/// <summary>Specifies a description for a property or event.</summary>
	// Token: 0x0200067B RID: 1659
	[AttributeUsage(AttributeTargets.All)]
	public class DescriptionAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DescriptionAttribute" /> class with no parameters.</summary>
		// Token: 0x06003562 RID: 13666 RVA: 0x000BEC1F File Offset: 0x000BCE1F
		public DescriptionAttribute()
			: this(string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DescriptionAttribute" /> class with a description.</summary>
		/// <param name="description">The description text. </param>
		// Token: 0x06003563 RID: 13667 RVA: 0x000BEC2C File Offset: 0x000BCE2C
		public DescriptionAttribute(string description)
		{
			this.DescriptionValue = description;
		}

		/// <summary>Gets the description stored in this attribute.</summary>
		/// <returns>The description stored in this attribute.</returns>
		// Token: 0x17000C53 RID: 3155
		// (get) Token: 0x06003564 RID: 13668 RVA: 0x000BEC3B File Offset: 0x000BCE3B
		public virtual string Description
		{
			get
			{
				return this.DescriptionValue;
			}
		}

		/// <summary>Gets or sets the string stored as the description.</summary>
		/// <returns>The string stored as the description. The default value is an empty string ("").</returns>
		// Token: 0x17000C54 RID: 3156
		// (get) Token: 0x06003565 RID: 13669 RVA: 0x000BEC43 File Offset: 0x000BCE43
		// (set) Token: 0x06003566 RID: 13670 RVA: 0x000BEC4B File Offset: 0x000BCE4B
		protected string DescriptionValue { get; set; }

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.DescriptionAttribute" />.</summary>
		/// <returns>true if the value of the given object is equal to that of the current; otherwise, false.</returns>
		/// <param name="obj">The object to test the value equality of. </param>
		// Token: 0x06003567 RID: 13671 RVA: 0x000BEC54 File Offset: 0x000BCE54
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DescriptionAttribute descriptionAttribute = obj as DescriptionAttribute;
			return descriptionAttribute != null && descriptionAttribute.Description == this.Description;
		}

		// Token: 0x06003568 RID: 13672 RVA: 0x000BEC84 File Offset: 0x000BCE84
		public override int GetHashCode()
		{
			return this.Description.GetHashCode();
		}

		/// <summary>Returns a value indicating whether this is the default <see cref="T:System.ComponentModel.DescriptionAttribute" /> instance.</summary>
		/// <returns>true, if this is the default <see cref="T:System.ComponentModel.DescriptionAttribute" /> instance; otherwise, false.</returns>
		// Token: 0x06003569 RID: 13673 RVA: 0x000BEC91 File Offset: 0x000BCE91
		public override bool IsDefaultAttribute()
		{
			return this.Equals(DescriptionAttribute.Default);
		}

		/// <summary>Specifies the default value for the <see cref="T:System.ComponentModel.DescriptionAttribute" />, which is an empty string (""). This static field is read-only.</summary>
		// Token: 0x0400200D RID: 8205
		public static readonly DescriptionAttribute Default = new DescriptionAttribute();
	}
}
