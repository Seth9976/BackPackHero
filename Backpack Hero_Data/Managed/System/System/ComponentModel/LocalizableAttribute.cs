using System;

namespace System.ComponentModel
{
	/// <summary>Specifies whether a property should be localized. This class cannot be inherited.</summary>
	// Token: 0x0200068A RID: 1674
	[AttributeUsage(AttributeTargets.All)]
	public sealed class LocalizableAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.LocalizableAttribute" /> class.</summary>
		/// <param name="isLocalizable">true if a property should be localized; otherwise, false. </param>
		// Token: 0x060035AE RID: 13742 RVA: 0x000BF1BE File Offset: 0x000BD3BE
		public LocalizableAttribute(bool isLocalizable)
		{
			this.IsLocalizable = isLocalizable;
		}

		/// <summary>Gets a value indicating whether a property should be localized.</summary>
		/// <returns>true if a property should be localized; otherwise, false.</returns>
		// Token: 0x17000C64 RID: 3172
		// (get) Token: 0x060035AF RID: 13743 RVA: 0x000BF1CD File Offset: 0x000BD3CD
		public bool IsLocalizable { get; }

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.LocalizableAttribute" />.</summary>
		/// <returns>true if the value of the given object is equal to that of the current; otherwise, false.</returns>
		/// <param name="obj">The object to test the value equality of. </param>
		// Token: 0x060035B0 RID: 13744 RVA: 0x000BF1D8 File Offset: 0x000BD3D8
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			LocalizableAttribute localizableAttribute = obj as LocalizableAttribute;
			bool? flag = ((localizableAttribute != null) ? new bool?(localizableAttribute.IsLocalizable) : null);
			bool isLocalizable = this.IsLocalizable;
			return (flag.GetValueOrDefault() == isLocalizable) & (flag != null);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.LocalizableAttribute" />.</returns>
		// Token: 0x060035B1 RID: 13745 RVA: 0x000BE516 File Offset: 0x000BC716
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Determines if this attribute is the default.</summary>
		/// <returns>true if the attribute is the default value for this attribute class; otherwise, false.</returns>
		// Token: 0x060035B2 RID: 13746 RVA: 0x000BF224 File Offset: 0x000BD424
		public override bool IsDefaultAttribute()
		{
			return this.IsLocalizable == LocalizableAttribute.Default.IsLocalizable;
		}

		/// <summary>Specifies that a property should be localized. This static field is read-only.</summary>
		// Token: 0x0400202E RID: 8238
		public static readonly LocalizableAttribute Yes = new LocalizableAttribute(true);

		/// <summary>Specifies that a property should not be localized. This static field is read-only.</summary>
		// Token: 0x0400202F RID: 8239
		public static readonly LocalizableAttribute No = new LocalizableAttribute(false);

		/// <summary>Specifies the default value, which is <see cref="F:System.ComponentModel.LocalizableAttribute.No" />. This static field is read-only.</summary>
		// Token: 0x04002030 RID: 8240
		public static readonly LocalizableAttribute Default = LocalizableAttribute.No;
	}
}
