using System;

namespace System.ComponentModel.Design
{
	/// <summary>Provides an interface to add and remove the event handlers for events that add, change, remove or rename components, and provides methods to raise a <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentChanged" /> or <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentChanging" /> event.</summary>
	// Token: 0x0200076B RID: 1899
	public interface IComponentChangeService
	{
		/// <summary>Occurs when a component has been added.</summary>
		// Token: 0x1400004C RID: 76
		// (add) Token: 0x06003C7D RID: 15485
		// (remove) Token: 0x06003C7E RID: 15486
		event ComponentEventHandler ComponentAdded;

		/// <summary>Occurs when a component is in the process of being added.</summary>
		// Token: 0x1400004D RID: 77
		// (add) Token: 0x06003C7F RID: 15487
		// (remove) Token: 0x06003C80 RID: 15488
		event ComponentEventHandler ComponentAdding;

		/// <summary>Occurs when a component has been changed.</summary>
		// Token: 0x1400004E RID: 78
		// (add) Token: 0x06003C81 RID: 15489
		// (remove) Token: 0x06003C82 RID: 15490
		event ComponentChangedEventHandler ComponentChanged;

		/// <summary>Occurs when a component is in the process of being changed.</summary>
		// Token: 0x1400004F RID: 79
		// (add) Token: 0x06003C83 RID: 15491
		// (remove) Token: 0x06003C84 RID: 15492
		event ComponentChangingEventHandler ComponentChanging;

		/// <summary>Occurs when a component has been removed.</summary>
		// Token: 0x14000050 RID: 80
		// (add) Token: 0x06003C85 RID: 15493
		// (remove) Token: 0x06003C86 RID: 15494
		event ComponentEventHandler ComponentRemoved;

		/// <summary>Occurs when a component is in the process of being removed.</summary>
		// Token: 0x14000051 RID: 81
		// (add) Token: 0x06003C87 RID: 15495
		// (remove) Token: 0x06003C88 RID: 15496
		event ComponentEventHandler ComponentRemoving;

		/// <summary>Occurs when a component is renamed.</summary>
		// Token: 0x14000052 RID: 82
		// (add) Token: 0x06003C89 RID: 15497
		// (remove) Token: 0x06003C8A RID: 15498
		event ComponentRenameEventHandler ComponentRename;

		/// <summary>Announces to the component change service that a particular component has changed.</summary>
		/// <param name="component">The component that has changed. </param>
		/// <param name="member">The member that has changed. This is null if this change is not related to a single member. </param>
		/// <param name="oldValue">The old value of the member. This is valid only if the member is not null. </param>
		/// <param name="newValue">The new value of the member. This is valid only if the member is not null. </param>
		// Token: 0x06003C8B RID: 15499
		void OnComponentChanged(object component, MemberDescriptor member, object oldValue, object newValue);

		/// <summary>Announces to the component change service that a particular component is changing.</summary>
		/// <param name="component">The component that is about to change. </param>
		/// <param name="member">The member that is changing. This is null if this change is not related to a single member. </param>
		// Token: 0x06003C8C RID: 15500
		void OnComponentChanging(object component, MemberDescriptor member);
	}
}
