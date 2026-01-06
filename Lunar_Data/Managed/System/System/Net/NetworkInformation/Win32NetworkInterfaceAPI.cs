using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200054A RID: 1354
	internal class Win32NetworkInterfaceAPI : NetworkInterfaceFactory
	{
		// Token: 0x06002B77 RID: 11127
		[DllImport("iphlpapi.dll", SetLastError = true)]
		private static extern int GetAdaptersAddresses(uint family, uint flags, IntPtr reserved, IntPtr info, ref int size);

		// Token: 0x06002B78 RID: 11128
		[DllImport("iphlpapi.dll")]
		private static extern uint GetBestInterfaceEx(byte[] ipAddress, out int index);

		// Token: 0x06002B79 RID: 11129 RVA: 0x0009CD7C File Offset: 0x0009AF7C
		private static Win32_IP_ADAPTER_ADDRESSES[] GetAdaptersAddresses()
		{
			IntPtr intPtr = IntPtr.Zero;
			int num = 0;
			uint num2 = 192U;
			Win32NetworkInterfaceAPI.GetAdaptersAddresses(0U, num2, IntPtr.Zero, intPtr, ref num);
			if (Marshal.SizeOf(typeof(Win32_IP_ADAPTER_ADDRESSES)) > num)
			{
				throw new NetworkInformationException();
			}
			intPtr = Marshal.AllocHGlobal(num);
			int adaptersAddresses = Win32NetworkInterfaceAPI.GetAdaptersAddresses(0U, num2, IntPtr.Zero, intPtr, ref num);
			if (adaptersAddresses != 0)
			{
				throw new NetworkInformationException(adaptersAddresses);
			}
			List<Win32_IP_ADAPTER_ADDRESSES> list = new List<Win32_IP_ADAPTER_ADDRESSES>();
			IntPtr intPtr2 = intPtr;
			while (intPtr2 != IntPtr.Zero)
			{
				Win32_IP_ADAPTER_ADDRESSES win32_IP_ADAPTER_ADDRESSES = Marshal.PtrToStructure<Win32_IP_ADAPTER_ADDRESSES>(intPtr2);
				list.Add(win32_IP_ADAPTER_ADDRESSES);
				intPtr2 = win32_IP_ADAPTER_ADDRESSES.Next;
			}
			return list.ToArray();
		}

		// Token: 0x06002B7A RID: 11130 RVA: 0x0009CE1C File Offset: 0x0009B01C
		public override NetworkInterface[] GetAllNetworkInterfaces()
		{
			Win32_IP_ADAPTER_ADDRESSES[] adaptersAddresses = Win32NetworkInterfaceAPI.GetAdaptersAddresses();
			NetworkInterface[] array = new NetworkInterface[adaptersAddresses.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new Win32NetworkInterface2(adaptersAddresses[i]);
			}
			return array;
		}

		// Token: 0x06002B7B RID: 11131 RVA: 0x0009CE58 File Offset: 0x0009B058
		private static int GetBestInterfaceForAddress(IPAddress addr)
		{
			int num;
			int bestInterfaceEx = (int)Win32NetworkInterfaceAPI.GetBestInterfaceEx(new SocketAddress(addr).m_Buffer, out num);
			if (bestInterfaceEx != 0)
			{
				throw new NetworkInformationException(bestInterfaceEx);
			}
			return num;
		}

		// Token: 0x06002B7C RID: 11132 RVA: 0x0009CE83 File Offset: 0x0009B083
		public override int GetLoopbackInterfaceIndex()
		{
			return Win32NetworkInterfaceAPI.GetBestInterfaceForAddress(IPAddress.Loopback);
		}

		// Token: 0x06002B7D RID: 11133 RVA: 0x0000822E File Offset: 0x0000642E
		public override IPAddress GetNetMask(IPAddress address)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04001975 RID: 6517
		private const string IPHLPAPI = "iphlpapi.dll";
	}
}
