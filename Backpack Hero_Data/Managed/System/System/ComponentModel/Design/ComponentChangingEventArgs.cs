using System;

namespace System.ComponentModel.Design
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentChanging" /> event. This class cannot be inherited.</summary>
	// Token: 0x02000757 RID: 1879
	public sealed class ComponentChangingEventArgs : EventArgs
	{
		/// <summary>Gets the component that is about to be changed or the component that is the parent container of the member that is about to be changed.</summary>
		/// <returns>The component that is about to have a member changed.</returns>
		// Token: 0x17000DB5 RID: 3509
		// (get) Token: 0x06003C17 RID: 15383 RVA: 0x000D7D53 File Offset: 0x000D5F53
		public object Component { get; }

		/// <summary>Gets the member that is about to be changed.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.MemberDescriptor" /> indicating the member that is about to be changed, if known, or null otherwise.</returns>
		// Token: 0x17000DB6 RID: 3510
		// (get) Token: 0x06003C18 RID: 15384 RVA: 0x000D7D5B File Offset: 0x000D5F5B
		public MemberDescriptor Member { get; }

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.ComponentChangingEventArgs" /> class.</summary>
		/// <param name="component">The component that is about to be changed. </param>
		/// <param name="member">A <see cref="T:System.ComponentModel.MemberDescriptor" /> indicating the member of the component that is about to be changed. </param>
		// Token: 0x06003C19 RID: 15385 RVA: 0x000D7D63 File Offset: 0x000D5F63
		public ComponentChangingEventArgs(object component, MemberDescriptor member)
		{
			this.Component = component;
			this.Member = member;
		}
	}
}
