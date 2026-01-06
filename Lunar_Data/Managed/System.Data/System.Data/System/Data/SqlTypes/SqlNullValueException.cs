using System;
using System.Runtime.Serialization;

namespace System.Data.SqlTypes
{
	/// <summary>The exception that is thrown when the Value property of a <see cref="N:System.Data.SqlTypes" /> structure is set to null.</summary>
	// Token: 0x020002CA RID: 714
	[Serializable]
	public sealed class SqlNullValueException : SqlTypeException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlNullValueException" /> class with a system-supplied message that describes the error.</summary>
		// Token: 0x060021E2 RID: 8674 RVA: 0x0009D9AE File Offset: 0x0009BBAE
		public SqlNullValueException()
			: this(SQLResource.NullValueMessage, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlNullValueException" /> class with a specified message that describes the error.</summary>
		/// <param name="message">The message that describes the exception. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		// Token: 0x060021E3 RID: 8675 RVA: 0x0009D9BC File Offset: 0x0009BBBC
		public SqlNullValueException(string message)
			: this(message, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlNullValueException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The message that describes the exception. The caller of this constructor is required to ensure that this string has been localized for the current system culture. </param>
		/// <param name="e">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x060021E4 RID: 8676 RVA: 0x0009D9C6 File Offset: 0x0009BBC6
		public SqlNullValueException(string message, Exception e)
			: base(message, e)
		{
			base.HResult = -2146232015;
		}

		// Token: 0x060021E5 RID: 8677 RVA: 0x0009D9DB File Offset: 0x0009BBDB
		private SqlNullValueException(SerializationInfo si, StreamingContext sc)
			: base(SqlNullValueException.SqlNullValueExceptionSerialization(si, sc), sc)
		{
		}

		// Token: 0x060021E6 RID: 8678 RVA: 0x0009D9EB File Offset: 0x0009BBEB
		private static SerializationInfo SqlNullValueExceptionSerialization(SerializationInfo si, StreamingContext sc)
		{
			if (si != null && 1 == si.MemberCount)
			{
				new SqlNullValueException(si.GetString("SqlNullValueExceptionMessage")).GetObjectData(si, sc);
			}
			return si;
		}
	}
}
