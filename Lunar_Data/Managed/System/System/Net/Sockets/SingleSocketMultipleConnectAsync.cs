using System;

namespace System.Net.Sockets
{
	// Token: 0x0200059D RID: 1437
	internal sealed class SingleSocketMultipleConnectAsync : MultipleConnectAsync
	{
		// Token: 0x06002D5A RID: 11610 RVA: 0x000A07D6 File Offset: 0x0009E9D6
		public SingleSocketMultipleConnectAsync(Socket socket, bool userSocket)
		{
			this._socket = socket;
			this._userSocket = userSocket;
		}

		// Token: 0x06002D5B RID: 11611 RVA: 0x000A07EC File Offset: 0x0009E9EC
		protected override IPAddress GetNextAddress(out Socket attemptSocket)
		{
			this._socket.ReplaceHandleIfNecessaryAfterFailedConnect();
			while (this._nextAddress < this._addressList.Length)
			{
				IPAddress ipaddress = this._addressList[this._nextAddress];
				this._nextAddress++;
				if (this._socket.CanTryAddressFamily(ipaddress.AddressFamily))
				{
					attemptSocket = this._socket;
					return ipaddress;
				}
			}
			attemptSocket = null;
			return null;
		}

		// Token: 0x06002D5C RID: 11612 RVA: 0x000A0853 File Offset: 0x0009EA53
		protected override void OnFail(bool abortive)
		{
			if (abortive || !this._userSocket)
			{
				this._socket.Dispose();
			}
		}

		// Token: 0x06002D5D RID: 11613 RVA: 0x00003917 File Offset: 0x00001B17
		protected override void OnSucceed()
		{
		}

		// Token: 0x04001ADA RID: 6874
		private Socket _socket;

		// Token: 0x04001ADB RID: 6875
		private bool _userSocket;
	}
}
