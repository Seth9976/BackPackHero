using System;
using System.Security.Permissions;
using Unity;

namespace System.Management.Instrumentation
{
	/// <summary>The ManagementReferenceAttribute marks a class member, property or method parameter as a reference to another management object or class.</summary>
	// Token: 0x02000386 RID: 902
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class ManagementReferenceAttribute : Attribute
	{
		/// <summary>Gets or sets the name of the referenced type.</summary>
		/// <returns>A string containing the name of the referenced type.</returns>
		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06001B1A RID: 6938 RVA: 0x0005A05A File Offset: 0x0005825A
		// (set) Token: 0x06001B1B RID: 6939 RVA: 0x0000235B File Offset: 0x0000055B
		public string Type
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
