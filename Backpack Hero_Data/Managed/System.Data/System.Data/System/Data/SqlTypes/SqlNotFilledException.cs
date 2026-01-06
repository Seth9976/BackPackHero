using System;
using System.Runtime.Serialization;

namespace System.Data.SqlTypes
{
	/// <summary>The <see cref="T:System.Data.SqlTypes.SqlNotFilledException" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality.</summary>
	// Token: 0x020002CC RID: 716
	[Serializable]
	public sealed class SqlNotFilledException : SqlTypeException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlNotFilledException" /> class.</summary>
		// Token: 0x060021EC RID: 8684 RVA: 0x0009DA74 File Offset: 0x0009BC74
		public SqlNotFilledException()
			: this(SQLResource.NotFilledMessage, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlNotFilledException" /> class.</summary>
		/// <param name="message">The string to display when the exception is thrown.</param>
		// Token: 0x060021ED RID: 8685 RVA: 0x0009DA82 File Offset: 0x0009BC82
		public SqlNotFilledException(string message)
			: this(message, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlNotFilledException" /> class.</summary>
		/// <param name="message">The string to display when the exception is thrown.</param>
		/// <param name="e">A reference to an inner exception.</param>
		// Token: 0x060021EE RID: 8686 RVA: 0x0009D9C6 File Offset: 0x0009BBC6
		public SqlNotFilledException(string message, Exception e)
			: base(message, e)
		{
			base.HResult = -2146232015;
		}

		// Token: 0x060021EF RID: 8687 RVA: 0x0009DA8C File Offset: 0x0009BC8C
		private SqlNotFilledException(SerializationInfo si, StreamingContext sc)
			: base(si, sc)
		{
		}
	}
}
