using System;
using System.Runtime.Serialization;

namespace System.Data
{
	/// <summary>Represents the exception that is thrown when attempting an action that violates a constraint.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000053 RID: 83
	[Serializable]
	public class ConstraintException : DataException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.ConstraintException" /> class using the specified serialization and stream context.</summary>
		/// <param name="info">The data necessary to serialize or deserialize an object. </param>
		/// <param name="context">Description of the source and destination of the specified serialized stream. </param>
		// Token: 0x060003CD RID: 973 RVA: 0x000121CA File Offset: 0x000103CA
		protected ConstraintException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.ConstraintException" /> class. This is the default constructor.</summary>
		// Token: 0x060003CE RID: 974 RVA: 0x000121D4 File Offset: 0x000103D4
		public ConstraintException()
			: base("Constraint Exception.")
		{
			base.HResult = -2146232022;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.ConstraintException" /> class with the specified string.</summary>
		/// <param name="s">The string to display when the exception is thrown. </param>
		// Token: 0x060003CF RID: 975 RVA: 0x000121EC File Offset: 0x000103EC
		public ConstraintException(string s)
			: base(s)
		{
			base.HResult = -2146232022;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.ConstraintException" /> class using the specified string and inner exception.</summary>
		/// <param name="message">The string to display when the exception is thrown. </param>
		/// <param name="innerException">Gets the Exception instance that caused the current exception.</param>
		// Token: 0x060003D0 RID: 976 RVA: 0x00012200 File Offset: 0x00010400
		public ConstraintException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2146232022;
		}
	}
}
