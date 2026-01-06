using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.ComponentModel.Design
{
	/// <summary>The exception that is thrown when an attempt to check out a file that is checked into a source code management program is canceled or fails.</summary>
	// Token: 0x02000753 RID: 1875
	[Serializable]
	public class CheckoutException : ExternalException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.CheckoutException" /> class with no associated message or error code.</summary>
		// Token: 0x06003C02 RID: 15362 RVA: 0x000D7BF3 File Offset: 0x000D5DF3
		public CheckoutException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.CheckoutException" /> class with the specified message.</summary>
		/// <param name="message">A message describing the exception. </param>
		// Token: 0x06003C03 RID: 15363 RVA: 0x000D7BFB File Offset: 0x000D5DFB
		public CheckoutException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.CheckoutException" /> class with the specified message and error code.</summary>
		/// <param name="message">A message describing the exception. </param>
		/// <param name="errorCode">The error code to pass. </param>
		// Token: 0x06003C04 RID: 15364 RVA: 0x000D7C04 File Offset: 0x000D5E04
		public CheckoutException(string message, int errorCode)
			: base(message, errorCode)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.CheckoutException" /> class using the specified serialization data and context. </summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to be used for deserialization.</param>
		/// <param name="context">The destination to be used for deserialization.</param>
		// Token: 0x06003C05 RID: 15365 RVA: 0x000D7C0E File Offset: 0x000D5E0E
		protected CheckoutException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.CheckoutException" /> class with the specified detailed description and the specified exception. </summary>
		/// <param name="message">A detailed description of the error.</param>
		/// <param name="innerException">A reference to the inner exception that is the cause of this exception.</param>
		// Token: 0x06003C06 RID: 15366 RVA: 0x000D7C18 File Offset: 0x000D5E18
		public CheckoutException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x04002217 RID: 8727
		private const int E_ABORT = -2147467260;

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.CheckoutException" /> class that specifies that the check out was canceled. This field is read-only.</summary>
		// Token: 0x04002218 RID: 8728
		public static readonly CheckoutException Canceled = new CheckoutException("The checkout was canceled by the user.", -2147467260);
	}
}
