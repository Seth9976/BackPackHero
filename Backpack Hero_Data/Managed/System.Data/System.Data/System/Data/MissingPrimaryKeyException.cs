using System;
using System.Runtime.Serialization;

namespace System.Data
{
	/// <summary>Represents the exception that is thrown when you try to access a row in a table that has no primary key.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000058 RID: 88
	[Serializable]
	public class MissingPrimaryKeyException : DataException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.MissingPrimaryKeyException" /> class with serialization information.</summary>
		/// <param name="info">The data that is required to serialize or deserialize an object. </param>
		/// <param name="context">A description of the source and destination of the specified serialized stream. </param>
		// Token: 0x060003E1 RID: 993 RVA: 0x000121CA File Offset: 0x000103CA
		protected MissingPrimaryKeyException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.MissingPrimaryKeyException" /> class.</summary>
		// Token: 0x060003E2 RID: 994 RVA: 0x00012319 File Offset: 0x00010519
		public MissingPrimaryKeyException()
			: base("Missing primary key.")
		{
			base.HResult = -2146232027;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.MissingPrimaryKeyException" /> class with the specified string.</summary>
		/// <param name="s">The string to display when the exception is thrown. </param>
		// Token: 0x060003E3 RID: 995 RVA: 0x00012331 File Offset: 0x00010531
		public MissingPrimaryKeyException(string s)
			: base(s)
		{
			base.HResult = -2146232027;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.MissingPrimaryKeyException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified. </param>
		// Token: 0x060003E4 RID: 996 RVA: 0x00012345 File Offset: 0x00010545
		public MissingPrimaryKeyException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2146232027;
		}
	}
}
