using System;
using System.ComponentModel;
using Unity;

namespace System.Net
{
	/// <summary>Provides data for the <see cref="E:System.Net.WebClient.DownloadProgressChanged" /> event of a <see cref="T:System.Net.WebClient" />.</summary>
	// Token: 0x020003D0 RID: 976
	public class DownloadProgressChangedEventArgs : ProgressChangedEventArgs
	{
		// Token: 0x06002039 RID: 8249 RVA: 0x000765AB File Offset: 0x000747AB
		internal DownloadProgressChangedEventArgs(int progressPercentage, object userToken, long bytesReceived, long totalBytesToReceive)
			: base(progressPercentage, userToken)
		{
			this.BytesReceived = bytesReceived;
			this.TotalBytesToReceive = totalBytesToReceive;
		}

		/// <summary>Gets the number of bytes received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that indicates the number of bytes received.</returns>
		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x0600203A RID: 8250 RVA: 0x000765C4 File Offset: 0x000747C4
		public long BytesReceived { get; }

		/// <summary>Gets the total number of bytes in a <see cref="T:System.Net.WebClient" /> data download operation.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that indicates the number of bytes that will be received.</returns>
		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x0600203B RID: 8251 RVA: 0x000765CC File Offset: 0x000747CC
		public long TotalBytesToReceive { get; }

		// Token: 0x0600203C RID: 8252 RVA: 0x00013B26 File Offset: 0x00011D26
		internal DownloadProgressChangedEventArgs()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
