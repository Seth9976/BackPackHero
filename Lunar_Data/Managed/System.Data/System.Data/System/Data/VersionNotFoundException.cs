using System;
using System.Runtime.Serialization;

namespace System.Data
{
	/// <summary>Represents the exception that is thrown when you try to return a version of a <see cref="T:System.Data.DataRow" /> that has been deleted.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200005C RID: 92
	[Serializable]
	public class VersionNotFoundException : DataException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.VersionNotFoundException" /> class with serialization information.</summary>
		/// <param name="info">The data that is required to serialize or deserialize an object. </param>
		/// <param name="context">Description of the source and destination of the specified serialized stream. </param>
		// Token: 0x060003F1 RID: 1009 RVA: 0x000121CA File Offset: 0x000103CA
		protected VersionNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.VersionNotFoundException" /> class.</summary>
		// Token: 0x060003F2 RID: 1010 RVA: 0x0001241D File Offset: 0x0001061D
		public VersionNotFoundException()
			: base("Version not found.")
		{
			base.HResult = -2146232023;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.VersionNotFoundException" /> class with the specified string.</summary>
		/// <param name="s">The string to display when the exception is thrown. </param>
		// Token: 0x060003F3 RID: 1011 RVA: 0x00012435 File Offset: 0x00010635
		public VersionNotFoundException(string s)
			: base(s)
		{
			base.HResult = -2146232023;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.VersionNotFoundException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified. </param>
		// Token: 0x060003F4 RID: 1012 RVA: 0x00012449 File Offset: 0x00010649
		public VersionNotFoundException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2146232023;
		}
	}
}
