using System;
using System.Runtime.Serialization;

namespace System.Data
{
	/// <summary>Represents the exception that is thrown when a duplicate database object name is encountered during an add operation in a <see cref="T:System.Data.DataSet" /> -related object.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000055 RID: 85
	[Serializable]
	public class DuplicateNameException : DataException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DuplicateNameException" /> class with serialization information.</summary>
		/// <param name="info">The data that is required to serialize or deserialize an object. </param>
		/// <param name="context">Description of the source and destination of the specified serialized stream. </param>
		// Token: 0x060003D5 RID: 981 RVA: 0x000121CA File Offset: 0x000103CA
		protected DuplicateNameException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DuplicateNameException" /> class.</summary>
		// Token: 0x060003D6 RID: 982 RVA: 0x00012256 File Offset: 0x00010456
		public DuplicateNameException()
			: base("Duplicate name not allowed.")
		{
			base.HResult = -2146232030;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DuplicateNameException" /> class with the specified string.</summary>
		/// <param name="s">The string to display when the exception is thrown. </param>
		// Token: 0x060003D7 RID: 983 RVA: 0x0001226E File Offset: 0x0001046E
		public DuplicateNameException(string s)
			: base(s)
		{
			base.HResult = -2146232030;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DuplicateNameException" /> class with the specified string and exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified. </param>
		// Token: 0x060003D8 RID: 984 RVA: 0x00012282 File Offset: 0x00010482
		public DuplicateNameException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2146232030;
		}
	}
}
