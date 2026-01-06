using System;

namespace System.ComponentModel
{
	/// <summary>Specifies a property that is offered by an extender provider. This class cannot be inherited.</summary>
	// Token: 0x020006C0 RID: 1728
	[AttributeUsage(AttributeTargets.All)]
	public sealed class ExtenderProvidedPropertyAttribute : Attribute
	{
		// Token: 0x06003747 RID: 14151 RVA: 0x000C372B File Offset: 0x000C192B
		internal static ExtenderProvidedPropertyAttribute Create(PropertyDescriptor extenderProperty, Type receiverType, IExtenderProvider provider)
		{
			return new ExtenderProvidedPropertyAttribute
			{
				ExtenderProperty = extenderProperty,
				ReceiverType = receiverType,
				Provider = provider
			};
		}

		/// <summary>Gets the property that is being provided.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptor" /> encapsulating the property that is being provided.</returns>
		// Token: 0x17000CBD RID: 3261
		// (get) Token: 0x06003749 RID: 14153 RVA: 0x000C3747 File Offset: 0x000C1947
		// (set) Token: 0x0600374A RID: 14154 RVA: 0x000C374F File Offset: 0x000C194F
		public PropertyDescriptor ExtenderProperty { get; private set; }

		/// <summary>Gets the extender provider that is providing the property.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.IExtenderProvider" /> that is providing the property.</returns>
		// Token: 0x17000CBE RID: 3262
		// (get) Token: 0x0600374B RID: 14155 RVA: 0x000C3758 File Offset: 0x000C1958
		// (set) Token: 0x0600374C RID: 14156 RVA: 0x000C3760 File Offset: 0x000C1960
		public IExtenderProvider Provider { get; private set; }

		/// <summary>Gets the type of object that can receive the property.</summary>
		/// <returns>A <see cref="T:System.Type" /> describing the type of object that can receive the property.</returns>
		// Token: 0x17000CBF RID: 3263
		// (get) Token: 0x0600374D RID: 14157 RVA: 0x000C3769 File Offset: 0x000C1969
		// (set) Token: 0x0600374E RID: 14158 RVA: 0x000C3771 File Offset: 0x000C1971
		public Type ReceiverType { get; private set; }

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />.</summary>
		/// <returns>true if the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />; otherwise, false.</returns>
		/// <param name="obj">An <see cref="T:System.Object" /> to compare with this instance or null. </param>
		// Token: 0x0600374F RID: 14159 RVA: 0x000C377C File Offset: 0x000C197C
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			ExtenderProvidedPropertyAttribute extenderProvidedPropertyAttribute = obj as ExtenderProvidedPropertyAttribute;
			return extenderProvidedPropertyAttribute != null && extenderProvidedPropertyAttribute.ExtenderProperty.Equals(this.ExtenderProperty) && extenderProvidedPropertyAttribute.Provider.Equals(this.Provider) && extenderProvidedPropertyAttribute.ReceiverType.Equals(this.ReceiverType);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06003750 RID: 14160 RVA: 0x000BE516 File Offset: 0x000BC716
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Provides an indication whether the value of this instance is the default value for the derived class.</summary>
		/// <returns>true if this instance is the default attribute for the class; otherwise, false.</returns>
		// Token: 0x06003751 RID: 14161 RVA: 0x000C37D2 File Offset: 0x000C19D2
		public override bool IsDefaultAttribute()
		{
			return this.ReceiverType == null;
		}
	}
}
