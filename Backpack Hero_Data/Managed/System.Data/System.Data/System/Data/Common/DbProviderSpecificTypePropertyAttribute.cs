using System;

namespace System.Data.Common
{
	/// <summary>Identifies which provider-specific property in the strongly typed parameter classes is to be used when setting a provider-specific type.</summary>
	// Token: 0x0200034A RID: 842
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	[Serializable]
	public sealed class DbProviderSpecificTypePropertyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of a <see cref="T:System.Data.Common.DbProviderSpecificTypePropertyAttribute" /> class.</summary>
		/// <param name="isProviderSpecificTypeProperty">Specifies whether this property is a provider-specific property.</param>
		// Token: 0x060028EA RID: 10474 RVA: 0x000B2608 File Offset: 0x000B0808
		public DbProviderSpecificTypePropertyAttribute(bool isProviderSpecificTypeProperty)
		{
			this.IsProviderSpecificTypeProperty = isProviderSpecificTypeProperty;
		}

		/// <summary>Indicates whether the attributed property is a provider-specific type.</summary>
		/// <returns>true if the property that this attribute is applied to is a provider-specific type property; otherwise false.</returns>
		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x060028EB RID: 10475 RVA: 0x000B2617 File Offset: 0x000B0817
		public bool IsProviderSpecificTypeProperty { get; }
	}
}
