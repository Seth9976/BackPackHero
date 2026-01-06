using System;
using System.Data.Common;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.SqlServer.Server
{
	/// <summary>Thrown when SQL Server or the ADO.NET <see cref="N:System.Data.SqlClient" /> provider detects an invalid user-defined type (UDT). </summary>
	// Token: 0x020003CD RID: 973
	[Serializable]
	public sealed class InvalidUdtException : SystemException
	{
		// Token: 0x06002F31 RID: 12081 RVA: 0x000CB1C5 File Offset: 0x000C93C5
		internal InvalidUdtException()
		{
			base.HResult = -2146232009;
		}

		// Token: 0x06002F32 RID: 12082 RVA: 0x000CB1D8 File Offset: 0x000C93D8
		internal InvalidUdtException(string message)
			: base(message)
		{
			base.HResult = -2146232009;
		}

		// Token: 0x06002F33 RID: 12083 RVA: 0x000CB1EC File Offset: 0x000C93EC
		internal InvalidUdtException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2146232009;
		}

		// Token: 0x06002F34 RID: 12084 RVA: 0x0000E5A6 File Offset: 0x0000C7A6
		private InvalidUdtException(SerializationInfo si, StreamingContext sc)
			: base(si, sc)
		{
		}

		/// <summary>Streams all the <see cref="T:Microsoft.SqlServer.Server.InvalidUdtException" /> properties into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> class for the given <see cref="T:System.Runtime.Serialization.StreamingContext" />.</summary>
		/// <param name="si">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> object.</param>
		// Token: 0x06002F35 RID: 12085 RVA: 0x0000E5B0 File Offset: 0x0000C7B0
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo si, StreamingContext context)
		{
			base.GetObjectData(si, context);
		}

		// Token: 0x06002F36 RID: 12086 RVA: 0x000CB204 File Offset: 0x000C9404
		internal static InvalidUdtException Create(Type udtType, string resourceReason)
		{
			string @string = Res.GetString(resourceReason);
			InvalidUdtException ex = new InvalidUdtException(Res.GetString("'{0}' is an invalid user defined type, reason: {1}.", new object[] { udtType.FullName, @string }));
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x020003CE RID: 974
		private class HResults
		{
			// Token: 0x04001C8C RID: 7308
			internal const int InvalidUdt = -2146232009;
		}
	}
}
