using System;
using System.Security.Permissions;

namespace System.Management.Instrumentation
{
	/// <summary>The ManagementCommit attribute marks a method that is called when it is necessary to update a set of read-write properties in one, atomic operation.</summary>
	// Token: 0x0200037C RID: 892
	[AttributeUsage(AttributeTargets.Method)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class ManagementCommitAttribute : ManagementMemberAttribute
	{
	}
}
