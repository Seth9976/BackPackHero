using System;
using System.ComponentModel;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Net.NetworkInformation
{
	/// <summary>The exception that is thrown when an error occurs while retrieving network information.</summary>
	// Token: 0x02000505 RID: 1285
	[Serializable]
	public class NetworkInformationException : Win32Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.NetworkInformationException" /> class.</summary>
		// Token: 0x060029B6 RID: 10678 RVA: 0x000770B5 File Offset: 0x000752B5
		public NetworkInformationException()
			: base(Marshal.GetLastWin32Error())
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.NetworkInformationException" /> class with the specified error code.</summary>
		/// <param name="errorCode">A Win32 error code. </param>
		// Token: 0x060029B7 RID: 10679 RVA: 0x000770C2 File Offset: 0x000752C2
		public NetworkInformationException(int errorCode)
			: base(errorCode)
		{
		}

		// Token: 0x060029B8 RID: 10680 RVA: 0x000770C2 File Offset: 0x000752C2
		internal NetworkInformationException(SocketError socketError)
			: base((int)socketError)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.NetworkInformationException" /> class with serialized data.</summary>
		/// <param name="serializationInfo">A SerializationInfo object that contains the serialized exception data. </param>
		/// <param name="streamingContext">A StreamingContext that contains contextual information about the serialized exception. </param>
		// Token: 0x060029B9 RID: 10681 RVA: 0x000770D5 File Offset: 0x000752D5
		protected NetworkInformationException(SerializationInfo serializationInfo, StreamingContext streamingContext)
			: base(serializationInfo, streamingContext)
		{
		}

		/// <summary>Gets the Win32 error code for this exception.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value that contains the Win32 error code.</returns>
		// Token: 0x17000931 RID: 2353
		// (get) Token: 0x060029BA RID: 10682 RVA: 0x000770DF File Offset: 0x000752DF
		public override int ErrorCode
		{
			get
			{
				return base.NativeErrorCode;
			}
		}
	}
}
