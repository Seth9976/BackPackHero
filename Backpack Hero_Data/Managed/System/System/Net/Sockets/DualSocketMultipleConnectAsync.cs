using System;

namespace System.Net.Sockets
{
	// Token: 0x0200059E RID: 1438
	internal sealed class DualSocketMultipleConnectAsync : MultipleConnectAsync
	{
		// Token: 0x06002D5E RID: 11614 RVA: 0x000A086B File Offset: 0x0009EA6B
		public DualSocketMultipleConnectAsync(SocketType socketType, ProtocolType protocolType)
		{
			if (Socket.OSSupportsIPv4)
			{
				this._socket4 = new Socket(AddressFamily.InterNetwork, socketType, protocolType);
			}
			if (Socket.OSSupportsIPv6)
			{
				this._socket6 = new Socket(AddressFamily.InterNetworkV6, socketType, protocolType);
			}
		}

		// Token: 0x06002D5F RID: 11615 RVA: 0x000A08A0 File Offset: 0x0009EAA0
		protected override IPAddress GetNextAddress(out Socket attemptSocket)
		{
			IPAddress ipaddress = null;
			attemptSocket = null;
			while (attemptSocket == null)
			{
				if (this._nextAddress >= this._addressList.Length)
				{
					return null;
				}
				ipaddress = this._addressList[this._nextAddress];
				this._nextAddress++;
				if (ipaddress.AddressFamily == AddressFamily.InterNetworkV6)
				{
					attemptSocket = this._socket6;
				}
				else if (ipaddress.AddressFamily == AddressFamily.InterNetwork)
				{
					attemptSocket = this._socket4;
				}
			}
			Socket socket = attemptSocket;
			if (socket != null)
			{
				socket.ReplaceHandleIfNecessaryAfterFailedConnect();
			}
			return ipaddress;
		}

		// Token: 0x06002D60 RID: 11616 RVA: 0x000A091C File Offset: 0x0009EB1C
		protected override void OnSucceed()
		{
			if (this._socket4 != null && !this._socket4.Connected)
			{
				this._socket4.Dispose();
			}
			if (this._socket6 != null && !this._socket6.Connected)
			{
				this._socket6.Dispose();
			}
		}

		// Token: 0x06002D61 RID: 11617 RVA: 0x000A0969 File Offset: 0x0009EB69
		protected override void OnFail(bool abortive)
		{
			Socket socket = this._socket4;
			if (socket != null)
			{
				socket.Dispose();
			}
			Socket socket2 = this._socket6;
			if (socket2 == null)
			{
				return;
			}
			socket2.Dispose();
		}

		// Token: 0x04001ADC RID: 6876
		private Socket _socket4;

		// Token: 0x04001ADD RID: 6877
		private Socket _socket6;
	}
}
