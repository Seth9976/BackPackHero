using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Runtime.Remoting
{
	/// <summary>The exception that is thrown to communicate errors to the client when the client connects to non-.NET Framework applications that cannot throw exceptions.</summary>
	// Token: 0x0200056E RID: 1390
	[ComVisible(true)]
	[Serializable]
	public class ServerException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.ServerException" /> class with default properties.</summary>
		// Token: 0x060036AC RID: 13996 RVA: 0x00092A1D File Offset: 0x00090C1D
		public ServerException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.ServerException" /> class with a specified message.</summary>
		/// <param name="message">The message that describes the exception </param>
		// Token: 0x060036AD RID: 13997 RVA: 0x0006E691 File Offset: 0x0006C891
		public ServerException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.ServerException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="InnerException">The exception that is the cause of the current exception. If the <paramref name="InnerException" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x060036AE RID: 13998 RVA: 0x0006E69A File Offset: 0x0006C89A
		public ServerException(string message, Exception InnerException)
			: base(message, InnerException)
		{
		}

		// Token: 0x060036AF RID: 13999 RVA: 0x00020A69 File Offset: 0x0001EC69
		internal ServerException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
