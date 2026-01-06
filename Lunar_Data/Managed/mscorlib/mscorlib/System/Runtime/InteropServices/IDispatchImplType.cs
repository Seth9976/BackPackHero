using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates which IDispatch implementation to use for a particular class.</summary>
	// Token: 0x020006F2 RID: 1778
	[ComVisible(true)]
	[Obsolete("The IDispatchImplAttribute is deprecated.", false)]
	[Serializable]
	public enum IDispatchImplType
	{
		/// <summary>Specifies that the common language runtime decides which IDispatch implementation to use.</summary>
		// Token: 0x04002A40 RID: 10816
		SystemDefinedImpl,
		/// <summary>Specifies that the IDispatch implemenation is supplied by the runtime.</summary>
		// Token: 0x04002A41 RID: 10817
		InternalImpl,
		/// <summary>Specifies that the IDispatch implementation is supplied by passing the type information for the object to the COM CreateStdDispatch API method.</summary>
		// Token: 0x04002A42 RID: 10818
		CompatibleImpl
	}
}
