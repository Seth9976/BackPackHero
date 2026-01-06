using System;
using System.Runtime.Serialization;

namespace System.IO
{
	/// <summary>The exception thrown when the internal buffer overflows.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200082D RID: 2093
	[Serializable]
	public class InternalBufferOverflowException : SystemException
	{
		/// <summary>Initializes a new default instance of the <see cref="T:System.IO.InternalBufferOverflowException" /> class.</summary>
		// Token: 0x060042CD RID: 17101 RVA: 0x000E84D1 File Offset: 0x000E66D1
		public InternalBufferOverflowException()
			: base("Internal buffer overflow occurred.")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.InternalBufferOverflowException" /> class with the error message to be displayed specified.</summary>
		/// <param name="message">The message to be given for the exception. </param>
		// Token: 0x060042CE RID: 17102 RVA: 0x0002F0B8 File Offset: 0x0002D2B8
		public InternalBufferOverflowException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new, empty instance of the <see cref="T:System.IO.InternalBufferOverflowException" /> class that is serializable using the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> objects.</summary>
		/// <param name="info">The information required to serialize the T:System.IO.InternalBufferOverflowException object.</param>
		/// <param name="context">The source and destination of the serialized stream associated with the T:System.IO.InternalBufferOverflowException object.</param>
		// Token: 0x060042CF RID: 17103 RVA: 0x0005569B File Offset: 0x0005389B
		protected InternalBufferOverflowException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.InternalBufferOverflowException" /> class with the message to be displayed and the generated inner exception specified.</summary>
		/// <param name="message">The message to be given for the exception. </param>
		/// <param name="inner">The inner exception. </param>
		// Token: 0x060042D0 RID: 17104 RVA: 0x0002F0ED File Offset: 0x0002D2ED
		public InternalBufferOverflowException(string message, Exception inner)
			: base(message, inner)
		{
		}
	}
}
