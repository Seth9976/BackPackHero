using System;
using System.ComponentModel;
using Unity;

namespace System.Net
{
	/// <summary>Provides data for the <see cref="E:System.Net.WebClient.UploadProgressChanged" /> event of a <see cref="T:System.Net.WebClient" />.</summary>
	// Token: 0x020003D1 RID: 977
	public class UploadProgressChangedEventArgs : ProgressChangedEventArgs
	{
		// Token: 0x0600203D RID: 8253 RVA: 0x000765D4 File Offset: 0x000747D4
		internal UploadProgressChangedEventArgs(int progressPercentage, object userToken, long bytesSent, long totalBytesToSend, long bytesReceived, long totalBytesToReceive)
			: base(progressPercentage, userToken)
		{
			this.BytesReceived = bytesReceived;
			this.TotalBytesToReceive = totalBytesToReceive;
			this.BytesSent = bytesSent;
			this.TotalBytesToSend = totalBytesToSend;
		}

		/// <summary>Gets the number of bytes received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that indicates the number of bytes received.</returns>
		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x0600203E RID: 8254 RVA: 0x000765FD File Offset: 0x000747FD
		public long BytesReceived { get; }

		/// <summary>Gets the total number of bytes in a <see cref="T:System.Net.WebClient" /> data upload operation.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that indicates the number of bytes that will be received.</returns>
		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x0600203F RID: 8255 RVA: 0x00076605 File Offset: 0x00074805
		public long TotalBytesToReceive { get; }

		/// <summary>Gets the number of bytes sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that indicates the number of bytes sent.</returns>
		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x06002040 RID: 8256 RVA: 0x0007660D File Offset: 0x0007480D
		public long BytesSent { get; }

		/// <summary>Gets the total number of bytes to send.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that indicates the number of bytes that will be sent.</returns>
		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x06002041 RID: 8257 RVA: 0x00076615 File Offset: 0x00074815
		public long TotalBytesToSend { get; }

		// Token: 0x06002042 RID: 8258 RVA: 0x00013B26 File Offset: 0x00011D26
		internal UploadProgressChangedEventArgs()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
