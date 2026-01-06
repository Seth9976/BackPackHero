using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Creates a COM object.</summary>
	/// <returns>An <see cref="T:System.IntPtr" /> object that represents the IUnknown interface of the COM object.</returns>
	/// <param name="aggregator">A pointer to the managed object's IUnknown interface. </param>
	// Token: 0x0200071B RID: 1819
	// (Invoke) Token: 0x060040E1 RID: 16609
	[ComVisible(true)]
	public delegate IntPtr ObjectCreationDelegate(IntPtr aggregator);
}
