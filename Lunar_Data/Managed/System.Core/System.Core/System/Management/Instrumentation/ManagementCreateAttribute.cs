using System;
using System.Security.Permissions;

namespace System.Management.Instrumentation
{
	/// <summary>The ManagementCreateAttribute is used to indicate that a method creates a new instance of a managed entity.</summary>
	// Token: 0x0200037F RID: 895
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = false)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class ManagementCreateAttribute : ManagementNewInstanceAttribute
	{
	}
}
