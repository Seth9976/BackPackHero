using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Net.Sockets
{
	/// <summary>The exception that is thrown when a socket error occurs.</summary>
	// Token: 0x020005B1 RID: 1457
	[Serializable]
	public class SocketException : Win32Exception
	{
		// Token: 0x06002F05 RID: 12037
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int WSAGetLastError_icall();

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.SocketException" /> class with the last operating system error code.</summary>
		// Token: 0x06002F06 RID: 12038 RVA: 0x000A79A9 File Offset: 0x000A5BA9
		public SocketException()
			: base(SocketException.WSAGetLastError_icall())
		{
		}

		// Token: 0x06002F07 RID: 12039 RVA: 0x000770CB File Offset: 0x000752CB
		internal SocketException(int error, string message)
			: base(error, message)
		{
		}

		// Token: 0x06002F08 RID: 12040 RVA: 0x000A79B6 File Offset: 0x000A5BB6
		internal SocketException(EndPoint endPoint)
			: base(Marshal.GetLastWin32Error())
		{
			this.m_EndPoint = endPoint;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.SocketException" /> class with the specified error code.</summary>
		/// <param name="errorCode">The error code that indicates the error that occurred. </param>
		// Token: 0x06002F09 RID: 12041 RVA: 0x000770C2 File Offset: 0x000752C2
		public SocketException(int errorCode)
			: base(errorCode)
		{
		}

		// Token: 0x06002F0A RID: 12042 RVA: 0x000A79CA File Offset: 0x000A5BCA
		internal SocketException(int errorCode, EndPoint endPoint)
			: base(errorCode)
		{
			this.m_EndPoint = endPoint;
		}

		// Token: 0x06002F0B RID: 12043 RVA: 0x000770C2 File Offset: 0x000752C2
		internal SocketException(SocketError socketError)
			: base((int)socketError)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.SocketException" /> class from the specified instances of the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> classes.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> instance that contains the information that is required to serialize the new <see cref="T:System.Net.Sockets.SocketException" /> instance. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the source of the serialized stream that is associated with the new <see cref="T:System.Net.Sockets.SocketException" /> instance. </param>
		// Token: 0x06002F0C RID: 12044 RVA: 0x000770D5 File Offset: 0x000752D5
		protected SocketException(SerializationInfo serializationInfo, StreamingContext streamingContext)
			: base(serializationInfo, streamingContext)
		{
		}

		/// <summary>Gets the error code that is associated with this exception.</summary>
		/// <returns>An integer error code that is associated with this exception.</returns>
		// Token: 0x17000AF6 RID: 2806
		// (get) Token: 0x06002F0D RID: 12045 RVA: 0x000770DF File Offset: 0x000752DF
		public override int ErrorCode
		{
			get
			{
				return base.NativeErrorCode;
			}
		}

		/// <summary>Gets the error message that is associated with this exception.</summary>
		/// <returns>A string that contains the error message. </returns>
		// Token: 0x17000AF7 RID: 2807
		// (get) Token: 0x06002F0E RID: 12046 RVA: 0x000A79DA File Offset: 0x000A5BDA
		public override string Message
		{
			get
			{
				if (this.m_EndPoint == null)
				{
					return base.Message;
				}
				return base.Message + " " + this.m_EndPoint.ToString();
			}
		}

		/// <summary>Gets the error code that is associated with this exception.</summary>
		/// <returns>An integer error code that is associated with this exception.</returns>
		// Token: 0x17000AF8 RID: 2808
		// (get) Token: 0x06002F0F RID: 12047 RVA: 0x000770DF File Offset: 0x000752DF
		public SocketError SocketErrorCode
		{
			get
			{
				return (SocketError)base.NativeErrorCode;
			}
		}

		// Token: 0x04001B53 RID: 6995
		[NonSerialized]
		private EndPoint m_EndPoint;
	}
}
