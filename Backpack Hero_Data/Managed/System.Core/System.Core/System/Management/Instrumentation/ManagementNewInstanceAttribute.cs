using System;
using System.Security.Permissions;

namespace System.Management.Instrumentation
{
	/// <summary>The base class for management attributes that have only run-time functionality and no schema representation. The management attribute classes ManagementBindAttribute, ManagementCreateAttribute and ManagementEnumeratorAttribute are all derived from this class.</summary>
	// Token: 0x0200037A RID: 890
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = false)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public abstract class ManagementNewInstanceAttribute : ManagementMemberAttribute
	{
	}
}
