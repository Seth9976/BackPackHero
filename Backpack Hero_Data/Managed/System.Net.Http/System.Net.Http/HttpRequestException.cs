using System;

namespace System.Net.Http
{
	/// <summary>A base class for exceptions thrown by the <see cref="T:System.Net.Http.HttpClient" /> and <see cref="T:System.Net.Http.HttpMessageHandler" /> classes.</summary>
	// Token: 0x02000028 RID: 40
	[Serializable]
	public class HttpRequestException : Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.HttpRequestException" /> class.</summary>
		// Token: 0x06000140 RID: 320 RVA: 0x00005896 File Offset: 0x00003A96
		public HttpRequestException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.HttpRequestException" /> class with a specific message that describes the current exception.</summary>
		/// <param name="message">A message that describes the current exception.</param>
		// Token: 0x06000141 RID: 321 RVA: 0x0000589E File Offset: 0x00003A9E
		public HttpRequestException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.HttpRequestException" /> class with a specific message that describes the current exception and an inner exception.</summary>
		/// <param name="message">A message that describes the current exception.</param>
		/// <param name="inner">The inner exception.</param>
		// Token: 0x06000142 RID: 322 RVA: 0x000058A7 File Offset: 0x00003AA7
		public HttpRequestException(string message, Exception inner)
			: base(message, inner)
		{
		}
	}
}
