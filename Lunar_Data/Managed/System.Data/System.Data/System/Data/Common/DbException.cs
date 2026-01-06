using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Data.Common
{
	/// <summary>The base class for all exceptions thrown on behalf of the data source.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000344 RID: 836
	[Serializable]
	public abstract class DbException : ExternalException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Common.DbException" /> class.</summary>
		// Token: 0x0600289C RID: 10396 RVA: 0x000B22E1 File Offset: 0x000B04E1
		protected DbException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Common.DbException" /> class with the specified error message.</summary>
		/// <param name="message">The message to display for this exception.</param>
		// Token: 0x0600289D RID: 10397 RVA: 0x000B22E9 File Offset: 0x000B04E9
		protected DbException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Common.DbException" /> class with the specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message string.</param>
		/// <param name="innerException">The inner exception reference.</param>
		// Token: 0x0600289E RID: 10398 RVA: 0x000B22F2 File Offset: 0x000B04F2
		protected DbException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Common.DbException" /> class with the specified error message and error code.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="errorCode">The error code for the exception.</param>
		// Token: 0x0600289F RID: 10399 RVA: 0x000B22FC File Offset: 0x000B04FC
		protected DbException(string message, int errorCode)
			: base(message, errorCode)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Common.DbException" /> class with the specified serialization information and context.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
		// Token: 0x060028A0 RID: 10400 RVA: 0x000B2306 File Offset: 0x000B0506
		protected DbException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
