using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200055C RID: 1372
	internal struct Win32_SOCKET_ADDRESS
	{
		// Token: 0x06002B99 RID: 11161 RVA: 0x0009D184 File Offset: 0x0009B384
		public IPAddress GetIPAddress()
		{
			Win32_SOCKADDR win32_SOCKADDR = (Win32_SOCKADDR)Marshal.PtrToStructure(this.Sockaddr, typeof(Win32_SOCKADDR));
			byte[] array;
			if (win32_SOCKADDR.AddressFamily == 23)
			{
				array = new byte[16];
				Array.Copy(win32_SOCKADDR.AddressData, 6, array, 0, 16);
			}
			else
			{
				array = new byte[4];
				Array.Copy(win32_SOCKADDR.AddressData, 2, array, 0, 4);
			}
			return new IPAddress(array);
		}

		// Token: 0x04001A0B RID: 6667
		public IntPtr Sockaddr;

		// Token: 0x04001A0C RID: 6668
		public int SockaddrLength;

		// Token: 0x04001A0D RID: 6669
		private const int AF_INET6 = 23;
	}
}
