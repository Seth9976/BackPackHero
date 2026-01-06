using System;
using System.Runtime.Serialization;

namespace System.IO
{
	/// <summary>The exception that is thrown when a data stream is in an invalid format.</summary>
	// Token: 0x0200082E RID: 2094
	[Serializable]
	public sealed class InvalidDataException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.InvalidDataException" /> class.</summary>
		// Token: 0x060042D1 RID: 17105 RVA: 0x000E84DE File Offset: 0x000E66DE
		public InvalidDataException()
			: base(global::Locale.GetText("Invalid data format."))
		{
			base.HResult = -2146233085;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.InvalidDataException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		// Token: 0x060042D2 RID: 17106 RVA: 0x000E84FB File Offset: 0x000E66FB
		public InvalidDataException(string message)
			: base(message)
		{
			base.HResult = -2146233085;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.InvalidDataException" /> class with a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception.</param>
		// Token: 0x060042D3 RID: 17107 RVA: 0x000E850F File Offset: 0x000E670F
		public InvalidDataException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2146233085;
		}

		// Token: 0x060042D4 RID: 17108 RVA: 0x0005569B File Offset: 0x0005389B
		private InvalidDataException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x040027E5 RID: 10213
		private const int Result = -2146233085;
	}
}
