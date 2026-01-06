using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the <see cref="T:System.ComponentModel.LicenseProvider" /> to use with a class. This class cannot be inherited.</summary>
	// Token: 0x020006DF RID: 1759
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public sealed class LicenseProviderAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.LicenseProviderAttribute" /> class without a license provider.</summary>
		// Token: 0x060037E5 RID: 14309 RVA: 0x000C3FE4 File Offset: 0x000C21E4
		public LicenseProviderAttribute()
			: this(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.LicenseProviderAttribute" /> class with the specified type.</summary>
		/// <param name="typeName">The fully qualified name of the license provider class. </param>
		// Token: 0x060037E6 RID: 14310 RVA: 0x000C3FED File Offset: 0x000C21ED
		public LicenseProviderAttribute(string typeName)
		{
			this._licenseProviderName = typeName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.LicenseProviderAttribute" /> class with the specified type of license provider.</summary>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the type of the license provider class. </param>
		// Token: 0x060037E7 RID: 14311 RVA: 0x000C3FFC File Offset: 0x000C21FC
		public LicenseProviderAttribute(Type type)
		{
			this._licenseProviderType = type;
		}

		/// <summary>Gets the license provider that must be used with the associated class.</summary>
		/// <returns>A <see cref="T:System.Type" /> that represents the type of the license provider. The default value is null.</returns>
		// Token: 0x17000CE3 RID: 3299
		// (get) Token: 0x060037E8 RID: 14312 RVA: 0x000C400B File Offset: 0x000C220B
		public Type LicenseProvider
		{
			get
			{
				if (this._licenseProviderType == null && this._licenseProviderName != null)
				{
					this._licenseProviderType = Type.GetType(this._licenseProviderName);
				}
				return this._licenseProviderType;
			}
		}

		/// <summary>Indicates a unique ID for this attribute type.</summary>
		/// <returns>A unique ID for this attribute type.</returns>
		// Token: 0x17000CE4 RID: 3300
		// (get) Token: 0x060037E9 RID: 14313 RVA: 0x000C403C File Offset: 0x000C223C
		public override object TypeId
		{
			get
			{
				string text = this._licenseProviderName;
				if (text == null && this._licenseProviderType != null)
				{
					text = this._licenseProviderType.FullName;
				}
				return base.GetType().FullName + text;
			}
		}

		/// <summary>Indicates whether this instance and a specified object are equal.</summary>
		/// <returns>true if <paramref name="value" /> is equal to this instance; otherwise, false.</returns>
		/// <param name="value">Another object to compare to. </param>
		// Token: 0x060037EA RID: 14314 RVA: 0x000C4080 File Offset: 0x000C2280
		public override bool Equals(object value)
		{
			if (value is LicenseProviderAttribute && value != null)
			{
				Type licenseProvider = ((LicenseProviderAttribute)value).LicenseProvider;
				if (licenseProvider == this.LicenseProvider)
				{
					return true;
				}
				if (licenseProvider != null && licenseProvider.Equals(this.LicenseProvider))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.LicenseProviderAttribute" />.</returns>
		// Token: 0x060037EB RID: 14315 RVA: 0x000BE516 File Offset: 0x000BC716
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Specifies the default value, which is no provider. This static field is read-only.</summary>
		// Token: 0x040020BE RID: 8382
		public static readonly LicenseProviderAttribute Default = new LicenseProviderAttribute();

		// Token: 0x040020BF RID: 8383
		private Type _licenseProviderType;

		// Token: 0x040020C0 RID: 8384
		private string _licenseProviderName;
	}
}
