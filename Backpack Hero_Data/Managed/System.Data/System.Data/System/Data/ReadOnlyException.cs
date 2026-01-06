using System;
using System.Runtime.Serialization;

namespace System.Data
{
	/// <summary>Represents the exception that is thrown when you try to change the value of a read-only column.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200005A RID: 90
	[Serializable]
	public class ReadOnlyException : DataException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.ReadOnlyException" /> class with serialization information.</summary>
		/// <param name="info">The data that is required to serialize or deserialize an object. </param>
		/// <param name="context">Description of the source and destination of the specified serialized stream. </param>
		// Token: 0x060003E9 RID: 1001 RVA: 0x000121CA File Offset: 0x000103CA
		protected ReadOnlyException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.ReadOnlyException" /> class.</summary>
		// Token: 0x060003EA RID: 1002 RVA: 0x0001239B File Offset: 0x0001059B
		public ReadOnlyException()
			: base("Column is marked read only.")
		{
			base.HResult = -2146232025;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.ReadOnlyException" /> class with the specified string.</summary>
		/// <param name="s">The string to display when the exception is thrown. </param>
		// Token: 0x060003EB RID: 1003 RVA: 0x000123B3 File Offset: 0x000105B3
		public ReadOnlyException(string s)
			: base(s)
		{
			base.HResult = -2146232025;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.ReadOnlyException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified. </param>
		// Token: 0x060003EC RID: 1004 RVA: 0x000123C7 File Offset: 0x000105C7
		public ReadOnlyException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2146232025;
		}
	}
}
