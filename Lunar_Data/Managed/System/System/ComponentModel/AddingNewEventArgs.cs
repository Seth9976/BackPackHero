using System;

namespace System.ComponentModel
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.BindingSource.AddingNew" /> event.</summary>
	// Token: 0x0200068D RID: 1677
	public class AddingNewEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AddingNewEventArgs" /> class using no parameters.</summary>
		// Token: 0x060035C0 RID: 13760 RVA: 0x0000C6B5 File Offset: 0x0000A8B5
		public AddingNewEventArgs()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AddingNewEventArgs" /> class using the specified object as the new item.</summary>
		/// <param name="newObject">An <see cref="T:System.Object" /> to use as the new item value.</param>
		// Token: 0x060035C1 RID: 13761 RVA: 0x000BF38A File Offset: 0x000BD58A
		public AddingNewEventArgs(object newObject)
		{
			this.NewObject = newObject;
		}

		/// <summary>Gets or sets the object to be added to the binding list. </summary>
		/// <returns>The <see cref="T:System.Object" /> to be added as a new item to the associated collection. </returns>
		// Token: 0x17000C67 RID: 3175
		// (get) Token: 0x060035C2 RID: 13762 RVA: 0x000BF399 File Offset: 0x000BD599
		// (set) Token: 0x060035C3 RID: 13763 RVA: 0x000BF3A1 File Offset: 0x000BD5A1
		public object NewObject { get; set; }
	}
}
