using System;

namespace System.ComponentModel
{
	/// <summary>Provides information about an event.</summary>
	// Token: 0x020006BB RID: 1723
	public abstract class EventDescriptor : MemberDescriptor
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.EventDescriptor" /> class with the specified name and attribute array.</summary>
		/// <param name="name">The name of the event. </param>
		/// <param name="attrs">An array of type <see cref="T:System.Attribute" /> that contains the event attributes. </param>
		// Token: 0x06003704 RID: 14084 RVA: 0x000C2EE7 File Offset: 0x000C10E7
		protected EventDescriptor(string name, Attribute[] attrs)
			: base(name, attrs)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.EventDescriptor" /> class with the name and attributes in the specified <see cref="T:System.ComponentModel.MemberDescriptor" />.</summary>
		/// <param name="descr">A <see cref="T:System.ComponentModel.MemberDescriptor" /> that contains the name of the event and its attributes. </param>
		// Token: 0x06003705 RID: 14085 RVA: 0x000C2EF1 File Offset: 0x000C10F1
		protected EventDescriptor(MemberDescriptor descr)
			: base(descr)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.EventDescriptor" /> class with the name in the specified <see cref="T:System.ComponentModel.MemberDescriptor" /> and the attributes in both the <see cref="T:System.ComponentModel.MemberDescriptor" /> and the <see cref="T:System.Attribute" /> array.</summary>
		/// <param name="descr">A <see cref="T:System.ComponentModel.MemberDescriptor" /> that has the name of the member and its attributes. </param>
		/// <param name="attrs">An <see cref="T:System.Attribute" /> array with the attributes you want to add to this event description. </param>
		// Token: 0x06003706 RID: 14086 RVA: 0x000C2EFA File Offset: 0x000C10FA
		protected EventDescriptor(MemberDescriptor descr, Attribute[] attrs)
			: base(descr, attrs)
		{
		}

		/// <summary>When overridden in a derived class, gets the type of component this event is bound to.</summary>
		/// <returns>A <see cref="T:System.Type" /> that represents the type of component the event is bound to.</returns>
		// Token: 0x17000CAC RID: 3244
		// (get) Token: 0x06003707 RID: 14087
		public abstract Type ComponentType { get; }

		/// <summary>When overridden in a derived class, gets the type of delegate for the event.</summary>
		/// <returns>A <see cref="T:System.Type" /> that represents the type of delegate for the event.</returns>
		// Token: 0x17000CAD RID: 3245
		// (get) Token: 0x06003708 RID: 14088
		public abstract Type EventType { get; }

		/// <summary>When overridden in a derived class, gets a value indicating whether the event delegate is a multicast delegate.</summary>
		/// <returns>true if the event delegate is multicast; otherwise, false.</returns>
		// Token: 0x17000CAE RID: 3246
		// (get) Token: 0x06003709 RID: 14089
		public abstract bool IsMulticast { get; }

		/// <summary>When overridden in a derived class, binds the event to the component.</summary>
		/// <param name="component">A component that provides events to the delegate. </param>
		/// <param name="value">A delegate that represents the method that handles the event. </param>
		// Token: 0x0600370A RID: 14090
		public abstract void AddEventHandler(object component, Delegate value);

		/// <summary>When overridden in a derived class, unbinds the delegate from the component so that the delegate will no longer receive events from the component.</summary>
		/// <param name="component">The component that the delegate is bound to. </param>
		/// <param name="value">The delegate to unbind from the component. </param>
		// Token: 0x0600370B RID: 14091
		public abstract void RemoveEventHandler(object component, Delegate value);
	}
}
