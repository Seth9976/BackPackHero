using System;
using System.Security.Permissions;
using Unity;

namespace System.Management.Instrumentation
{
	/// <summary>The ManagementRemoveAttribute is used to indicate that a method cleans up an instance of a managed entity.</summary>
	// Token: 0x02000387 RID: 903
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class ManagementRemoveAttribute : ManagementMemberAttribute
	{
		/// <summary>Gets or sets a value that defines the type of output that the object that is marked with the ManagementRemove attribute will output.</summary>
		/// <returns>A <see cref="T:System.Type" /> value that indicates the type of output that the object marked with the Remove attribute will output.</returns>
		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06001B1D RID: 6941 RVA: 0x0005A05A File Offset: 0x0005825A
		// (set) Token: 0x06001B1E RID: 6942 RVA: 0x0000235B File Offset: 0x0000055B
		public Type Schema
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}
	}
}
