using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Messaging
{
	/// <summary>Defines the method call return message interface.</summary>
	// Token: 0x02000620 RID: 1568
	[ComVisible(true)]
	public interface IMethodReturnMessage : IMethodMessage, IMessage
	{
		/// <summary>Gets the exception thrown during the method call.</summary>
		/// <returns>The exception object for the method call, or null if the method did not throw an exception.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170008C1 RID: 2241
		// (get) Token: 0x06003B0C RID: 15116
		Exception Exception { get; }

		/// <summary>Gets the number of arguments in the method call marked as ref or out parameters.</summary>
		/// <returns>The number of arguments in the method call marked as ref or out parameters.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170008C2 RID: 2242
		// (get) Token: 0x06003B0D RID: 15117
		int OutArgCount { get; }

		/// <summary>Returns the specified argument marked as a ref or an out parameter.</summary>
		/// <returns>The specified argument marked as a ref or an out parameter.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170008C3 RID: 2243
		// (get) Token: 0x06003B0E RID: 15118
		object[] OutArgs { get; }

		/// <summary>Gets the return value of the method call.</summary>
		/// <returns>The return value of the method call.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x06003B0F RID: 15119
		object ReturnValue { get; }

		/// <summary>Returns the specified argument marked as a ref or an out parameter.</summary>
		/// <returns>The specified argument marked as a ref or an out parameter.</returns>
		/// <param name="argNum">The number of the requested argument. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission. </exception>
		// Token: 0x06003B10 RID: 15120
		object GetOutArg(int argNum);

		/// <summary>Returns the name of the specified argument marked as a ref or an out parameter.</summary>
		/// <returns>The argument name, or null if the current method is not implemented.</returns>
		/// <param name="index">The number of the requested argument name. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission. </exception>
		// Token: 0x06003B11 RID: 15121
		string GetOutArgName(int index);
	}
}
