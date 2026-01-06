using System;

namespace System.ComponentModel.Design
{
	/// <summary>Provides an interface for managing designer transactions and components.</summary>
	// Token: 0x02000771 RID: 1905
	public interface IDesignerHost : IServiceContainer, IServiceProvider
	{
		/// <summary>Gets a value indicating whether the designer host is currently loading the document.</summary>
		/// <returns>true if the designer host is currently loading the document; otherwise, false.</returns>
		// Token: 0x17000DCF RID: 3535
		// (get) Token: 0x06003CA4 RID: 15524
		bool Loading { get; }

		/// <summary>Gets a value indicating whether the designer host is currently in a transaction.</summary>
		/// <returns>true if a transaction is in progress; otherwise, false.</returns>
		// Token: 0x17000DD0 RID: 3536
		// (get) Token: 0x06003CA5 RID: 15525
		bool InTransaction { get; }

		/// <summary>Gets the container for this designer host.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.IContainer" /> for this host.</returns>
		// Token: 0x17000DD1 RID: 3537
		// (get) Token: 0x06003CA6 RID: 15526
		IContainer Container { get; }

		/// <summary>Gets the instance of the base class used as the root component for the current design.</summary>
		/// <returns>The instance of the root component class.</returns>
		// Token: 0x17000DD2 RID: 3538
		// (get) Token: 0x06003CA7 RID: 15527
		IComponent RootComponent { get; }

		/// <summary>Gets the fully qualified name of the class being designed.</summary>
		/// <returns>The fully qualified name of the base component class.</returns>
		// Token: 0x17000DD3 RID: 3539
		// (get) Token: 0x06003CA8 RID: 15528
		string RootComponentClassName { get; }

		/// <summary>Gets the description of the current transaction.</summary>
		/// <returns>A description of the current transaction.</returns>
		// Token: 0x17000DD4 RID: 3540
		// (get) Token: 0x06003CA9 RID: 15529
		string TransactionDescription { get; }

		/// <summary>Occurs when this designer is activated.</summary>
		// Token: 0x14000057 RID: 87
		// (add) Token: 0x06003CAA RID: 15530
		// (remove) Token: 0x06003CAB RID: 15531
		event EventHandler Activated;

		/// <summary>Occurs when this designer is deactivated.</summary>
		// Token: 0x14000058 RID: 88
		// (add) Token: 0x06003CAC RID: 15532
		// (remove) Token: 0x06003CAD RID: 15533
		event EventHandler Deactivated;

		/// <summary>Occurs when this designer completes loading its document.</summary>
		// Token: 0x14000059 RID: 89
		// (add) Token: 0x06003CAE RID: 15534
		// (remove) Token: 0x06003CAF RID: 15535
		event EventHandler LoadComplete;

		/// <summary>Adds an event handler for the <see cref="E:System.ComponentModel.Design.IDesignerHost.TransactionClosed" /> event.</summary>
		// Token: 0x1400005A RID: 90
		// (add) Token: 0x06003CB0 RID: 15536
		// (remove) Token: 0x06003CB1 RID: 15537
		event DesignerTransactionCloseEventHandler TransactionClosed;

		/// <summary>Adds an event handler for the <see cref="E:System.ComponentModel.Design.IDesignerHost.TransactionClosing" /> event.</summary>
		// Token: 0x1400005B RID: 91
		// (add) Token: 0x06003CB2 RID: 15538
		// (remove) Token: 0x06003CB3 RID: 15539
		event DesignerTransactionCloseEventHandler TransactionClosing;

		/// <summary>Adds an event handler for the <see cref="E:System.ComponentModel.Design.IDesignerHost.TransactionOpened" /> event.</summary>
		// Token: 0x1400005C RID: 92
		// (add) Token: 0x06003CB4 RID: 15540
		// (remove) Token: 0x06003CB5 RID: 15541
		event EventHandler TransactionOpened;

		/// <summary>Adds an event handler for the <see cref="E:System.ComponentModel.Design.IDesignerHost.TransactionOpening" /> event.</summary>
		// Token: 0x1400005D RID: 93
		// (add) Token: 0x06003CB6 RID: 15542
		// (remove) Token: 0x06003CB7 RID: 15543
		event EventHandler TransactionOpening;

		/// <summary>Activates the designer that this host is hosting.</summary>
		// Token: 0x06003CB8 RID: 15544
		void Activate();

		/// <summary>Creates a component of the specified type and adds it to the design document.</summary>
		/// <returns>The newly created component.</returns>
		/// <param name="componentClass">The type of the component to create. </param>
		// Token: 0x06003CB9 RID: 15545
		IComponent CreateComponent(Type componentClass);

		/// <summary>Creates a component of the specified type and name, and adds it to the design document.</summary>
		/// <returns>The newly created component.</returns>
		/// <param name="componentClass">The type of the component to create. </param>
		/// <param name="name">The name for the component. </param>
		// Token: 0x06003CBA RID: 15546
		IComponent CreateComponent(Type componentClass, string name);

		/// <summary>Creates a <see cref="T:System.ComponentModel.Design.DesignerTransaction" /> that can encapsulate event sequences to improve performance and enable undo and redo support functionality.</summary>
		/// <returns>A new instance of <see cref="T:System.ComponentModel.Design.DesignerTransaction" />. When you complete the steps in your transaction, you should call <see cref="M:System.ComponentModel.Design.DesignerTransaction.Commit" /> on this object.</returns>
		// Token: 0x06003CBB RID: 15547
		DesignerTransaction CreateTransaction();

		/// <summary>Creates a <see cref="T:System.ComponentModel.Design.DesignerTransaction" /> that can encapsulate event sequences to improve performance and enable undo and redo support functionality, using the specified transaction description.</summary>
		/// <returns>A new <see cref="T:System.ComponentModel.Design.DesignerTransaction" />. When you have completed the steps in your transaction, you should call <see cref="M:System.ComponentModel.Design.DesignerTransaction.Commit" /> on this object.</returns>
		/// <param name="description">A title or description for the newly created transaction. </param>
		// Token: 0x06003CBC RID: 15548
		DesignerTransaction CreateTransaction(string description);

		/// <summary>Destroys the specified component and removes it from the designer container.</summary>
		/// <param name="component">The component to destroy. </param>
		// Token: 0x06003CBD RID: 15549
		void DestroyComponent(IComponent component);

		/// <summary>Gets the designer instance that contains the specified component.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.Design.IDesigner" />, or null if there is no designer for the specified component.</returns>
		/// <param name="component">The <see cref="T:System.ComponentModel.IComponent" /> to retrieve the designer for. </param>
		// Token: 0x06003CBE RID: 15550
		IDesigner GetDesigner(IComponent component);

		/// <summary>Gets an instance of the specified, fully qualified type name.</summary>
		/// <returns>The type object for the specified type name, or null if the type cannot be found.</returns>
		/// <param name="typeName">The name of the type to load. </param>
		// Token: 0x06003CBF RID: 15551
		Type GetType(string typeName);
	}
}
