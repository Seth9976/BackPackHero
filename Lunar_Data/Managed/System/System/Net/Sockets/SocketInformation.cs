using System;
using System.Runtime.Serialization;

namespace System.Net.Sockets
{
	/// <summary>Encapsulates the information that is necessary to duplicate a <see cref="T:System.Net.Sockets.Socket" />.</summary>
	// Token: 0x020005C1 RID: 1473
	[Serializable]
	public struct SocketInformation
	{
		/// <summary>Gets or sets the protocol information for a <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <returns>An array of type <see cref="T:System.Byte" />.</returns>
		// Token: 0x17000B07 RID: 2823
		// (get) Token: 0x06002F38 RID: 12088 RVA: 0x000A7D9C File Offset: 0x000A5F9C
		// (set) Token: 0x06002F39 RID: 12089 RVA: 0x000A7DA4 File Offset: 0x000A5FA4
		public byte[] ProtocolInformation
		{
			get
			{
				return this.protocolInformation;
			}
			set
			{
				this.protocolInformation = value;
			}
		}

		/// <summary>Gets or sets the options for a <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <returns>A <see cref="T:System.Net.Sockets.SocketInformationOptions" /> instance.</returns>
		// Token: 0x17000B08 RID: 2824
		// (get) Token: 0x06002F3A RID: 12090 RVA: 0x000A7DAD File Offset: 0x000A5FAD
		// (set) Token: 0x06002F3B RID: 12091 RVA: 0x000A7DB5 File Offset: 0x000A5FB5
		public SocketInformationOptions Options
		{
			get
			{
				return this.options;
			}
			set
			{
				this.options = value;
			}
		}

		// Token: 0x17000B09 RID: 2825
		// (get) Token: 0x06002F3C RID: 12092 RVA: 0x000A7DBE File Offset: 0x000A5FBE
		// (set) Token: 0x06002F3D RID: 12093 RVA: 0x000A7DCB File Offset: 0x000A5FCB
		internal bool IsNonBlocking
		{
			get
			{
				return (this.options & SocketInformationOptions.NonBlocking) > (SocketInformationOptions)0;
			}
			set
			{
				if (value)
				{
					this.options |= SocketInformationOptions.NonBlocking;
					return;
				}
				this.options &= ~SocketInformationOptions.NonBlocking;
			}
		}

		// Token: 0x17000B0A RID: 2826
		// (get) Token: 0x06002F3E RID: 12094 RVA: 0x000A7DEE File Offset: 0x000A5FEE
		// (set) Token: 0x06002F3F RID: 12095 RVA: 0x000A7DFB File Offset: 0x000A5FFB
		internal bool IsConnected
		{
			get
			{
				return (this.options & SocketInformationOptions.Connected) > (SocketInformationOptions)0;
			}
			set
			{
				if (value)
				{
					this.options |= SocketInformationOptions.Connected;
					return;
				}
				this.options &= ~SocketInformationOptions.Connected;
			}
		}

		// Token: 0x17000B0B RID: 2827
		// (get) Token: 0x06002F40 RID: 12096 RVA: 0x000A7E1E File Offset: 0x000A601E
		// (set) Token: 0x06002F41 RID: 12097 RVA: 0x000A7E2B File Offset: 0x000A602B
		internal bool IsListening
		{
			get
			{
				return (this.options & SocketInformationOptions.Listening) > (SocketInformationOptions)0;
			}
			set
			{
				if (value)
				{
					this.options |= SocketInformationOptions.Listening;
					return;
				}
				this.options &= ~SocketInformationOptions.Listening;
			}
		}

		// Token: 0x17000B0C RID: 2828
		// (get) Token: 0x06002F42 RID: 12098 RVA: 0x000A7E4E File Offset: 0x000A604E
		// (set) Token: 0x06002F43 RID: 12099 RVA: 0x000A7E5B File Offset: 0x000A605B
		internal bool UseOnlyOverlappedIO
		{
			get
			{
				return (this.options & SocketInformationOptions.UseOnlyOverlappedIO) > (SocketInformationOptions)0;
			}
			set
			{
				if (value)
				{
					this.options |= SocketInformationOptions.UseOnlyOverlappedIO;
					return;
				}
				this.options &= ~SocketInformationOptions.UseOnlyOverlappedIO;
			}
		}

		// Token: 0x17000B0D RID: 2829
		// (get) Token: 0x06002F44 RID: 12100 RVA: 0x000A7E7E File Offset: 0x000A607E
		// (set) Token: 0x06002F45 RID: 12101 RVA: 0x000A7E86 File Offset: 0x000A6086
		internal EndPoint RemoteEndPoint
		{
			get
			{
				return this.remoteEndPoint;
			}
			set
			{
				this.remoteEndPoint = value;
			}
		}

		// Token: 0x04001C31 RID: 7217
		private byte[] protocolInformation;

		// Token: 0x04001C32 RID: 7218
		private SocketInformationOptions options;

		// Token: 0x04001C33 RID: 7219
		[OptionalField]
		private EndPoint remoteEndPoint;
	}
}
