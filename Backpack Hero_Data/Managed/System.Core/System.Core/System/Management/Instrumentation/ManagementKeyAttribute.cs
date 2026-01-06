using System;
using System.Security.Permissions;

namespace System.Management.Instrumentation
{
	/// <summary>The ManagementKey attribute identifies the key properties of a WMI class.</summary>
	// Token: 0x02000383 RID: 899
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class ManagementKeyAttribute : ManagementMemberAttribute
	{
	}
}
