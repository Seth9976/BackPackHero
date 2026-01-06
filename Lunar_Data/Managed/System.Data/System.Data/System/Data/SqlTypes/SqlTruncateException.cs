using System;
using System.Runtime.Serialization;

namespace System.Data.SqlTypes
{
	/// <summary>The exception that is thrown when you set a value into a <see cref="N:System.Data.SqlTypes" /> structure would truncate that value.</summary>
	// Token: 0x020002CB RID: 715
	[Serializable]
	public sealed class SqlTruncateException : SqlTypeException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlTruncateException" /> class.</summary>
		// Token: 0x060021E7 RID: 8679 RVA: 0x0009DA11 File Offset: 0x0009BC11
		public SqlTruncateException()
			: this(SQLResource.TruncationMessage, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlTruncateException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		// Token: 0x060021E8 RID: 8680 RVA: 0x0009DA1F File Offset: 0x0009BC1F
		public SqlTruncateException(string message)
			: this(message, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlTruncateException" /> class with a specified error message and a reference to the <see cref="T:System.Exception" />.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="e">A reference to an inner <see cref="T:System.Exception" />. </param>
		// Token: 0x060021E9 RID: 8681 RVA: 0x0009DA29 File Offset: 0x0009BC29
		public SqlTruncateException(string message, Exception e)
			: base(message, e)
		{
			base.HResult = -2146232014;
		}

		// Token: 0x060021EA RID: 8682 RVA: 0x0009DA3E File Offset: 0x0009BC3E
		private SqlTruncateException(SerializationInfo si, StreamingContext sc)
			: base(SqlTruncateException.SqlTruncateExceptionSerialization(si, sc), sc)
		{
		}

		// Token: 0x060021EB RID: 8683 RVA: 0x0009DA4E File Offset: 0x0009BC4E
		private static SerializationInfo SqlTruncateExceptionSerialization(SerializationInfo si, StreamingContext sc)
		{
			if (si != null && 1 == si.MemberCount)
			{
				new SqlTruncateException(si.GetString("SqlTruncateExceptionMessage")).GetObjectData(si, sc);
			}
			return si;
		}
	}
}
