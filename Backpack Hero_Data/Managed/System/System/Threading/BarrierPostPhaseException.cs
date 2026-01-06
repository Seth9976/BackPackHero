using System;
using System.Runtime.Serialization;
using System.Security;

namespace System.Threading
{
	/// <summary>The exception that is thrown when the post-phase action of a <see cref="T:System.Threading.Barrier" /> fails</summary>
	// Token: 0x0200017C RID: 380
	[Serializable]
	public class BarrierPostPhaseException : Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.BarrierPostPhaseException" /> class with a system-supplied message that describes the error.</summary>
		// Token: 0x06000A1F RID: 2591 RVA: 0x0002C353 File Offset: 0x0002A553
		public BarrierPostPhaseException()
			: this(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.BarrierPostPhaseException" /> class with the specified inner exception.</summary>
		/// <param name="innerException">The exception that is the cause of the current exception.</param>
		// Token: 0x06000A20 RID: 2592 RVA: 0x0002C35C File Offset: 0x0002A55C
		public BarrierPostPhaseException(Exception innerException)
			: this(null, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.BarrierPostPhaseException" /> class with a specified message that describes the error.</summary>
		/// <param name="message">The message that describes the exception. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		// Token: 0x06000A21 RID: 2593 RVA: 0x0002C366 File Offset: 0x0002A566
		public BarrierPostPhaseException(string message)
			: this(message, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.BarrierPostPhaseException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The message that describes the exception. The caller of this constructor is required to ensure that this string has been localized for the current system culture. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06000A22 RID: 2594 RVA: 0x0002C370 File Offset: 0x0002A570
		public BarrierPostPhaseException(string message, Exception innerException)
			: base((message == null) ? SR.GetString("The postPhaseAction failed with an exception.") : message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.BarrierPostPhaseException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x06000A23 RID: 2595 RVA: 0x0002C389 File Offset: 0x0002A589
		[SecurityCritical]
		protected BarrierPostPhaseException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
