using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Enables users to write activation code for managed objects that extend <see cref="T:System.MarshalByRefObject" />.</summary>
	// Token: 0x020006D8 RID: 1752
	public interface ICustomFactory
	{
		/// <summary>Creates a new instance of the specified type.</summary>
		/// <returns>A <see cref="T:System.MarshalByRefObject" /> associated with the specified type.</returns>
		/// <param name="serverType">The type to activate. </param>
		// Token: 0x06004033 RID: 16435
		MarshalByRefObject CreateInstance(Type serverType);
	}
}
