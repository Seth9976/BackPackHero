using System;
using System.Runtime.Serialization;

namespace System.Data
{
	/// <summary>Represents the exception that is thrown when you try to perform an operation on a <see cref="T:System.Data.DataRow" /> that is not in a <see cref="T:System.Data.DataTable" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200005B RID: 91
	[Serializable]
	public class RowNotInTableException : DataException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.RowNotInTableException" /> class with serialization information.</summary>
		/// <param name="info">The data that is required to serialize or deserialize an object. </param>
		/// <param name="context">Description of the source and destination of the specified serialized stream. </param>
		// Token: 0x060003ED RID: 1005 RVA: 0x000121CA File Offset: 0x000103CA
		protected RowNotInTableException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.RowNotInTableException" /> class.</summary>
		// Token: 0x060003EE RID: 1006 RVA: 0x000123DC File Offset: 0x000105DC
		public RowNotInTableException()
			: base("Row not found in table.")
		{
			base.HResult = -2146232024;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.RowNotInTableException" /> class with the specified string.</summary>
		/// <param name="s">The string to display when the exception is thrown. </param>
		// Token: 0x060003EF RID: 1007 RVA: 0x000123F4 File Offset: 0x000105F4
		public RowNotInTableException(string s)
			: base(s)
		{
			base.HResult = -2146232024;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.RowNotInTableException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified. </param>
		// Token: 0x060003F0 RID: 1008 RVA: 0x00012408 File Offset: 0x00010608
		public RowNotInTableException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2146232024;
		}
	}
}
