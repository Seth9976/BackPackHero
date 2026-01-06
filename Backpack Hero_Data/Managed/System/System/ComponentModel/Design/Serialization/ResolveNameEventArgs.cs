using System;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.Design.Serialization.IDesignerSerializationManager.ResolveName" /> event.</summary>
	// Token: 0x020007A5 RID: 1957
	public class ResolveNameEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.ResolveNameEventArgs" /> class.</summary>
		/// <param name="name">The name to resolve. </param>
		// Token: 0x06003DD0 RID: 15824 RVA: 0x000D9E88 File Offset: 0x000D8088
		public ResolveNameEventArgs(string name)
		{
			this.Name = name;
			this.Value = null;
		}

		/// <summary>Gets the name of the object to resolve.</summary>
		/// <returns>The name of the object to resolve.</returns>
		// Token: 0x17000E0D RID: 3597
		// (get) Token: 0x06003DD1 RID: 15825 RVA: 0x000D9E9E File Offset: 0x000D809E
		public string Name { get; }

		/// <summary>Gets or sets the object that matches the name.</summary>
		/// <returns>The object that the name is associated with.</returns>
		// Token: 0x17000E0E RID: 3598
		// (get) Token: 0x06003DD2 RID: 15826 RVA: 0x000D9EA6 File Offset: 0x000D80A6
		// (set) Token: 0x06003DD3 RID: 15827 RVA: 0x000D9EAE File Offset: 0x000D80AE
		public object Value { get; set; }
	}
}
