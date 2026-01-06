using System;

namespace System.ComponentModel.Design
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentChanged" /> event. This class cannot be inherited.</summary>
	// Token: 0x02000755 RID: 1877
	public sealed class ComponentChangedEventArgs : EventArgs
	{
		/// <summary>Gets the component that was modified.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the component that was modified.</returns>
		// Token: 0x17000DB1 RID: 3505
		// (get) Token: 0x06003C0E RID: 15374 RVA: 0x000D7D0E File Offset: 0x000D5F0E
		public object Component { get; }

		/// <summary>Gets the member that has been changed.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.MemberDescriptor" /> that indicates the member that has been changed.</returns>
		// Token: 0x17000DB2 RID: 3506
		// (get) Token: 0x06003C0F RID: 15375 RVA: 0x000D7D16 File Offset: 0x000D5F16
		public MemberDescriptor Member { get; }

		/// <summary>Gets the new value of the changed member.</summary>
		/// <returns>The new value of the changed member. This property can be null.</returns>
		// Token: 0x17000DB3 RID: 3507
		// (get) Token: 0x06003C10 RID: 15376 RVA: 0x000D7D1E File Offset: 0x000D5F1E
		public object NewValue { get; }

		/// <summary>Gets the old value of the changed member.</summary>
		/// <returns>The old value of the changed member. This property can be null.</returns>
		// Token: 0x17000DB4 RID: 3508
		// (get) Token: 0x06003C11 RID: 15377 RVA: 0x000D7D26 File Offset: 0x000D5F26
		public object OldValue { get; }

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.ComponentChangedEventArgs" /> class.</summary>
		/// <param name="component">The component that was changed. </param>
		/// <param name="member">A <see cref="T:System.ComponentModel.MemberDescriptor" /> that represents the member that was changed. </param>
		/// <param name="oldValue">The old value of the changed member. </param>
		/// <param name="newValue">The new value of the changed member. </param>
		// Token: 0x06003C12 RID: 15378 RVA: 0x000D7D2E File Offset: 0x000D5F2E
		public ComponentChangedEventArgs(object component, MemberDescriptor member, object oldValue, object newValue)
		{
			this.Component = component;
			this.Member = member;
			this.OldValue = oldValue;
			this.NewValue = newValue;
		}
	}
}
