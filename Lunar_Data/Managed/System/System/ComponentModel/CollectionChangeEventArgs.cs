using System;

namespace System.ComponentModel
{
	/// <summary>Provides data for the <see cref="E:System.Data.DataColumnCollection.CollectionChanged" /> event.</summary>
	// Token: 0x020006A0 RID: 1696
	public class CollectionChangeEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.CollectionChangeEventArgs" /> class.</summary>
		/// <param name="action">One of the <see cref="T:System.ComponentModel.CollectionChangeAction" /> values that specifies how the collection changed. </param>
		/// <param name="element">An <see cref="T:System.Object" /> that specifies the instance of the collection where the change occurred. </param>
		// Token: 0x0600365A RID: 13914 RVA: 0x000C0648 File Offset: 0x000BE848
		public CollectionChangeEventArgs(CollectionChangeAction action, object element)
		{
			this.Action = action;
			this.Element = element;
		}

		/// <summary>Gets an action that specifies how the collection changed.</summary>
		/// <returns>One of the <see cref="T:System.ComponentModel.CollectionChangeAction" /> values.</returns>
		// Token: 0x17000C8D RID: 3213
		// (get) Token: 0x0600365B RID: 13915 RVA: 0x000C065E File Offset: 0x000BE85E
		public virtual CollectionChangeAction Action { get; }

		/// <summary>Gets the instance of the collection with the change.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the instance of the collection with the change, or null if you refresh the collection.</returns>
		// Token: 0x17000C8E RID: 3214
		// (get) Token: 0x0600365C RID: 13916 RVA: 0x000C0666 File Offset: 0x000BE866
		public virtual object Element { get; }
	}
}
