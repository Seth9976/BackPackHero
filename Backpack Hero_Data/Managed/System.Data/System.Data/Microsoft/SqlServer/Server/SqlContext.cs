using System;
using System.Security.Principal;

namespace Microsoft.SqlServer.Server
{
	/// <summary>Represents an abstraction of the caller's context, which provides access to the <see cref="T:Microsoft.SqlServer.Server.SqlPipe" />, <see cref="T:Microsoft.SqlServer.Server.SqlTriggerContext" />, and <see cref="T:System.Security.Principal.WindowsIdentity" /> objects. This class cannot be inherited.</summary>
	// Token: 0x020003CF RID: 975
	public sealed class SqlContext
	{
		/// <summary>Specifies whether the calling code is running within SQL Server, and if the context connection can be accessed.</summary>
		/// <returns>True if the context connection is available and the other <see cref="T:Microsoft.SqlServer.Server.SqlContext" /> members can be accessed.</returns>
		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x06002F38 RID: 12088 RVA: 0x00005AE9 File Offset: 0x00003CE9
		public static bool IsAvailable
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the pipe object that allows the caller to send result sets, messages, and the results of executing commands back to the client.</summary>
		/// <returns>An instance of <see cref="T:Microsoft.SqlServer.Server.SqlPipe" /> if a pipe is available, or null if called in a context where pipe is not available (for example, in a user-defined function).</returns>
		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x06002F39 RID: 12089 RVA: 0x00003DF6 File Offset: 0x00001FF6
		public static SqlPipe Pipe
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets the trigger context used to provide the caller with information about what caused the trigger to fire, and a map of the columns that were updated.</summary>
		/// <returns>An instance of <see cref="T:Microsoft.SqlServer.Server.SqlTriggerContext" /> if a trigger context is available, or null if called outside of a trigger invocation.</returns>
		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x06002F3A RID: 12090 RVA: 0x00003DF6 File Offset: 0x00001FF6
		public static SqlTriggerContext TriggerContext
		{
			get
			{
				return null;
			}
		}

		/// <summary>The Microsoft Windows identity of the caller.</summary>
		/// <returns>A <see cref="T:System.Security.Principal.WindowsIdentity" /> instance representing the Windows identity of the caller, or null if the client was authenticated using SQL Server Authentication. </returns>
		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x06002F3B RID: 12091 RVA: 0x00003DF6 File Offset: 0x00001FF6
		public static WindowsIdentity WindowsIdentity
		{
			get
			{
				return null;
			}
		}
	}
}
