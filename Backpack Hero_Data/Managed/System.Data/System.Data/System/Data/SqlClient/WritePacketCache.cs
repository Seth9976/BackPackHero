using System;
using System.Collections.Generic;

namespace System.Data.SqlClient
{
	// Token: 0x0200021D RID: 541
	internal sealed class WritePacketCache : IDisposable
	{
		// Token: 0x06001900 RID: 6400 RVA: 0x0007DF67 File Offset: 0x0007C167
		public WritePacketCache()
		{
			this._disposed = false;
			this._packets = new Stack<SNIPacket>();
		}

		// Token: 0x06001901 RID: 6401 RVA: 0x0007DF84 File Offset: 0x0007C184
		public SNIPacket Take(SNIHandle sniHandle)
		{
			SNIPacket snipacket;
			if (this._packets.Count > 0)
			{
				snipacket = this._packets.Pop();
				SNINativeMethodWrapper.SNIPacketReset(sniHandle, SNINativeMethodWrapper.IOType.WRITE, snipacket, SNINativeMethodWrapper.ConsumerNumber.SNI_Consumer_SNI);
			}
			else
			{
				snipacket = new SNIPacket(sniHandle);
			}
			return snipacket;
		}

		// Token: 0x06001902 RID: 6402 RVA: 0x0007DFBE File Offset: 0x0007C1BE
		public void Add(SNIPacket packet)
		{
			if (!this._disposed)
			{
				this._packets.Push(packet);
				return;
			}
			packet.Dispose();
		}

		// Token: 0x06001903 RID: 6403 RVA: 0x0007DFDB File Offset: 0x0007C1DB
		public void Clear()
		{
			while (this._packets.Count > 0)
			{
				this._packets.Pop().Dispose();
			}
		}

		// Token: 0x06001904 RID: 6404 RVA: 0x0007DFFD File Offset: 0x0007C1FD
		public void Dispose()
		{
			if (!this._disposed)
			{
				this._disposed = true;
				this.Clear();
			}
		}

		// Token: 0x04001216 RID: 4630
		private bool _disposed;

		// Token: 0x04001217 RID: 4631
		private Stack<SNIPacket> _packets;
	}
}
