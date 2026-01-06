using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Messaging
{
	/// <summary>Defines the method call message interface.</summary>
	// Token: 0x0200061E RID: 1566
	[ComVisible(true)]
	public interface IMethodCallMessage : IMethodMessage, IMessage
	{
		/// <summary>Gets the number of arguments in the call that are not marked as out parameters.</summary>
		/// <returns>The number of arguments in the call that are not marked as out parameters.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170008B6 RID: 2230
		// (get) Token: 0x06003AFD RID: 15101
		int InArgCount { get; }

		/// <summary>Gets an array of arguments that are not marked as out parameters.</summary>
		/// <returns>An array of arguments that are not marked as out parameters.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170008B7 RID: 2231
		// (get) Token: 0x06003AFE RID: 15102
		object[] InArgs { get; }

		/// <summary>Returns the specified argument that is not marked as an out parameter.</summary>
		/// <returns>The requested argument that is not marked as an out parameter.</returns>
		/// <param name="argNum">The number of the requested in argument. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission. </exception>
		// Token: 0x06003AFF RID: 15103
		object GetInArg(int argNum);

		/// <summary>Returns the name of the specified argument that is not marked as an out parameter.</summary>
		/// <returns>The name of a specific argument that is not marked as an out parameter.</returns>
		/// <param name="index">The number of the requested in argument. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission. </exception>
		// Token: 0x06003B00 RID: 15104
		string GetInArgName(int index);
	}
}
