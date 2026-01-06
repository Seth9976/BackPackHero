using System;

namespace System.ComponentModel
{
	/// <summary>Specifies that this object supports a simple, transacted notification for batch initialization.</summary>
	// Token: 0x02000685 RID: 1669
	public interface ISupportInitialize
	{
		/// <summary>Signals the object that initialization is starting.</summary>
		// Token: 0x0600359B RID: 13723
		void BeginInit();

		/// <summary>Signals the object that initialization is complete.</summary>
		// Token: 0x0600359C RID: 13724
		void EndInit();
	}
}
