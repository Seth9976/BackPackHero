using System;
using System.Runtime.Serialization;

namespace System.Net.NetworkInformation
{
	/// <summary>The exception that is thrown when a <see cref="Overload:System.Net.NetworkInformation.Ping.Send" /> or <see cref="Overload:System.Net.NetworkInformation.Ping.SendAsync" /> method calls a method that throws an exception.</summary>
	// Token: 0x0200051E RID: 1310
	[Serializable]
	public class PingException : InvalidOperationException
	{
		// Token: 0x06002A39 RID: 10809 RVA: 0x000785BA File Offset: 0x000767BA
		internal PingException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.PingException" /> class with serialized data. </summary>
		/// <param name="serializationInfo">The object that holds the serialized object data. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that specifies the contextual information about the source or destination for this serialization.</param>
		// Token: 0x06002A3A RID: 10810 RVA: 0x000785CB File Offset: 0x000767CB
		protected PingException(SerializationInfo serializationInfo, StreamingContext streamingContext)
			: base(serializationInfo, streamingContext)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.PingException" /> class using the specified message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error.</param>
		// Token: 0x06002A3B RID: 10811 RVA: 0x000785C2 File Offset: 0x000767C2
		public PingException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.PingException" />  class using the specified message and inner exception.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error.</param>
		/// <param name="innerException">The exception that causes the current exception.</param>
		// Token: 0x06002A3C RID: 10812 RVA: 0x0009A2E3 File Offset: 0x000984E3
		public PingException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
