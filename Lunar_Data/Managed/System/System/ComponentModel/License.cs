using System;

namespace System.ComponentModel
{
	/// <summary>Provides the abstract base class for all licenses. A license is granted to a specific instance of a component.</summary>
	// Token: 0x020006DB RID: 1755
	public abstract class License : IDisposable
	{
		/// <summary>When overridden in a derived class, gets the license key granted to this component.</summary>
		/// <returns>A license key granted to this component.</returns>
		// Token: 0x17000CDF RID: 3295
		// (get) Token: 0x060037C7 RID: 14279
		public abstract string LicenseKey { get; }

		/// <summary>When overridden in a derived class, disposes of the resources used by the license.</summary>
		// Token: 0x060037C8 RID: 14280
		public abstract void Dispose();
	}
}
