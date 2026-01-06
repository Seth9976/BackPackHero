using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Used by <see cref="M:System.AppDomain.DoCallBack(System.CrossAppDomainDelegate)" /> for cross-application domain calls.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000230 RID: 560
	// (Invoke) Token: 0x06001996 RID: 6550
	[ComVisible(true)]
	public delegate void CrossAppDomainDelegate();
}
