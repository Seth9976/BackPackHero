using System;
using System.Runtime.Serialization;

namespace System.Data
{
	/// <summary>Represents the exception that is thrown when errors are generated using ADO.NET components.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000052 RID: 82
	[Serializable]
	public class DataException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataException" /> class with the specified serialization information and context.</summary>
		/// <param name="info">The data necessary to serialize or deserialize an object. </param>
		/// <param name="context">Description of the source and destination of the specified serialized stream. </param>
		// Token: 0x060003C9 RID: 969 RVA: 0x0000E5A6 File Offset: 0x0000C7A6
		protected DataException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataException" /> class. This is the default constructor.</summary>
		// Token: 0x060003CA RID: 970 RVA: 0x00012194 File Offset: 0x00010394
		public DataException()
			: base("Data Exception.")
		{
			base.HResult = -2146232032;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataException" /> class with the specified string.</summary>
		/// <param name="s">The string to display when the exception is thrown. </param>
		// Token: 0x060003CB RID: 971 RVA: 0x000121AC File Offset: 0x000103AC
		public DataException(string s)
			: base(s)
		{
			base.HResult = -2146232032;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataException" /> class with the specified string and inner exception.</summary>
		/// <param name="s">The string to display when the exception is thrown. </param>
		/// <param name="innerException">A reference to an inner exception. </param>
		// Token: 0x060003CC RID: 972 RVA: 0x000121C0 File Offset: 0x000103C0
		public DataException(string s, Exception innerException)
			: base(s, innerException)
		{
		}
	}
}
