using System;

namespace System.ComponentModel
{
	/// <summary>Allows coordination of initialization for a component and its dependent properties.</summary>
	// Token: 0x020006D0 RID: 1744
	public interface ISupportInitializeNotification : ISupportInitialize
	{
		/// <summary>Gets a value indicating whether the component is initialized.</summary>
		/// <returns>true to indicate the component has completed initialization; otherwise, false. </returns>
		// Token: 0x17000CD5 RID: 3285
		// (get) Token: 0x0600379B RID: 14235
		bool IsInitialized { get; }

		/// <summary>Occurs when initialization of the component is completed.</summary>
		// Token: 0x14000044 RID: 68
		// (add) Token: 0x0600379C RID: 14236
		// (remove) Token: 0x0600379D RID: 14237
		event EventHandler Initialized;
	}
}
