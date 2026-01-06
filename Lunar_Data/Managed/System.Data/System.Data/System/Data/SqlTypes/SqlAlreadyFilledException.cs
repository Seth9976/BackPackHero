using System;
using System.Runtime.Serialization;

namespace System.Data.SqlTypes
{
	/// <summary>The <see cref="T:System.Data.SqlTypes.SqlAlreadyFilledException" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality. </summary>
	// Token: 0x020002CD RID: 717
	[Serializable]
	public sealed class SqlAlreadyFilledException : SqlTypeException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlAlreadyFilledException" /> class.</summary>
		// Token: 0x060021F0 RID: 8688 RVA: 0x0009DA96 File Offset: 0x0009BC96
		public SqlAlreadyFilledException()
			: this(SQLResource.AlreadyFilledMessage, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlAlreadyFilledException" /> class.</summary>
		/// <param name="message">The string to display when the exception is thrown.</param>
		// Token: 0x060021F1 RID: 8689 RVA: 0x0009DAA4 File Offset: 0x0009BCA4
		public SqlAlreadyFilledException(string message)
			: this(message, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlAlreadyFilledException" /> class.</summary>
		/// <param name="message">The string to display when the exception is thrown.</param>
		/// <param name="e">A reference to an inner exception.</param>
		// Token: 0x060021F2 RID: 8690 RVA: 0x0009D9C6 File Offset: 0x0009BBC6
		public SqlAlreadyFilledException(string message, Exception e)
			: base(message, e)
		{
			base.HResult = -2146232015;
		}

		// Token: 0x060021F3 RID: 8691 RVA: 0x0009DA8C File Offset: 0x0009BC8C
		private SqlAlreadyFilledException(SerializationInfo si, StreamingContext sc)
			: base(si, sc)
		{
		}
	}
}
