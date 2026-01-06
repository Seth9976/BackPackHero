using System;

namespace System.ComponentModel
{
	/// <summary>Specifies when a component property can be bound to an application setting.</summary>
	// Token: 0x02000700 RID: 1792
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class SettingsBindableAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.SettingsBindableAttribute" /> class. </summary>
		/// <param name="bindable">true to specify that a property is appropriate to bind settings to; otherwise, false.</param>
		// Token: 0x0600395D RID: 14685 RVA: 0x000C8449 File Offset: 0x000C6649
		public SettingsBindableAttribute(bool bindable)
		{
			this.Bindable = bindable;
		}

		/// <summary>Gets a value indicating whether a property is appropriate to bind settings to. </summary>
		/// <returns>true if the property is appropriate to bind settings to; otherwise, false.</returns>
		// Token: 0x17000D41 RID: 3393
		// (get) Token: 0x0600395E RID: 14686 RVA: 0x000C8458 File Offset: 0x000C6658
		public bool Bindable { get; }

		/// <summary>Determines whether two <see cref="T:System.ComponentModel.SettingsBindableAttribute" /> objects are equal.</summary>
		/// <returns>true if <paramref name="obj" /> equals the type and value of this instance; otherwise, false.</returns>
		/// <param name="obj">The value to compare to.</param>
		// Token: 0x0600395F RID: 14687 RVA: 0x000C8460 File Offset: 0x000C6660
		public override bool Equals(object obj)
		{
			return obj == this || (obj != null && obj is SettingsBindableAttribute && ((SettingsBindableAttribute)obj).Bindable == this.Bindable);
		}

		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06003960 RID: 14688 RVA: 0x000C8488 File Offset: 0x000C6688
		public override int GetHashCode()
		{
			return this.Bindable.GetHashCode();
		}

		/// <summary>Specifies that a property is appropriate to bind settings to.</summary>
		// Token: 0x04002151 RID: 8529
		public static readonly SettingsBindableAttribute Yes = new SettingsBindableAttribute(true);

		/// <summary>Specifies that a property is not appropriate to bind settings to.</summary>
		// Token: 0x04002152 RID: 8530
		public static readonly SettingsBindableAttribute No = new SettingsBindableAttribute(false);
	}
}
