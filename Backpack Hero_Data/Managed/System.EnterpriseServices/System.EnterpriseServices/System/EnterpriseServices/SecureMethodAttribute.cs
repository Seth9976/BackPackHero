using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices
{
	/// <summary>Ensures that the infrastructure calls through an interface for a method or for each method in a class when using the security service. Classes need to use interfaces to use security services. This class cannot be inherited.</summary>
	// Token: 0x02000041 RID: 65
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
	[ComVisible(false)]
	public sealed class SecureMethodAttribute : Attribute
	{
	}
}
