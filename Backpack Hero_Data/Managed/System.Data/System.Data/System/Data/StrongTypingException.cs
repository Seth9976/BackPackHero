using System;
using System.Runtime.Serialization;

namespace System.Data
{
	/// <summary>The exception that is thrown by a strongly typed <see cref="T:System.Data.DataSet" /> when the user accesses a DBNull value.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020000E5 RID: 229
	[Serializable]
	public class StrongTypingException : DataException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.StrongTypingException" /> class using the specified serialization information and streaming context.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object. </param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> structure. </param>
		// Token: 0x06000CB3 RID: 3251 RVA: 0x000121CA File Offset: 0x000103CA
		protected StrongTypingException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.StrongTypingException" /> class.</summary>
		// Token: 0x06000CB4 RID: 3252 RVA: 0x0003A003 File Offset: 0x00038203
		public StrongTypingException()
		{
			base.HResult = -2146232021;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.StrongTypingException" /> class with the specified string.</summary>
		/// <param name="message">The string to display when the exception is thrown. </param>
		// Token: 0x06000CB5 RID: 3253 RVA: 0x0003A016 File Offset: 0x00038216
		public StrongTypingException(string message)
			: base(message)
		{
			base.HResult = -2146232021;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.StrongTypingException" /> class with the specified string and inner exception.</summary>
		/// <param name="s">The string to display when the exception is thrown. </param>
		/// <param name="innerException">A reference to an inner exception. </param>
		// Token: 0x06000CB6 RID: 3254 RVA: 0x0003A02A File Offset: 0x0003822A
		public StrongTypingException(string s, Exception innerException)
			: base(s, innerException)
		{
			base.HResult = -2146232021;
		}
	}
}
