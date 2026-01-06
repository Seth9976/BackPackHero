using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.SqlClient.SNI
{
	// Token: 0x02000250 RID: 592
	internal class SSRP
	{
		// Token: 0x06001AF9 RID: 6905 RVA: 0x00086A44 File Offset: 0x00084C44
		internal static int GetPortByInstanceName(string browserHostName, string instanceName)
		{
			byte[] array = SSRP.CreateInstanceInfoRequest(instanceName);
			byte[] array2 = null;
			try
			{
				array2 = SSRP.SendUDPRequest(browserHostName, 1434, array);
			}
			catch (SocketException ex)
			{
				throw new Exception(SQLMessage.SqlServerBrowserNotAccessible(), ex);
			}
			if (array2 == null || array2.Length <= 3 || array2[0] != 5 || (int)BitConverter.ToUInt16(array2, 1) != array2.Length - 3)
			{
				throw new SocketException();
			}
			string[] array3 = Encoding.ASCII.GetString(array2, 3, array2.Length - 3).Split(';', StringSplitOptions.None);
			int num = Array.IndexOf<string>(array3, "tcp");
			if (num < 0 || num == array3.Length - 1)
			{
				throw new SocketException();
			}
			return (int)ushort.Parse(array3[num + 1]);
		}

		// Token: 0x06001AFA RID: 6906 RVA: 0x00086AF0 File Offset: 0x00084CF0
		private static byte[] CreateInstanceInfoRequest(string instanceName)
		{
			instanceName += "\0";
			byte[] array = new byte[Encoding.ASCII.GetByteCount(instanceName) + 1];
			array[0] = 4;
			Encoding.ASCII.GetBytes(instanceName, 0, instanceName.Length, array, 1);
			return array;
		}

		// Token: 0x06001AFB RID: 6907 RVA: 0x00086B38 File Offset: 0x00084D38
		internal static int GetDacPortByInstanceName(string browserHostName, string instanceName)
		{
			byte[] array = SSRP.CreateDacPortInfoRequest(instanceName);
			byte[] array2 = SSRP.SendUDPRequest(browserHostName, 1434, array);
			if (array2 == null || array2.Length <= 4 || array2[0] != 5 || BitConverter.ToUInt16(array2, 1) != 6 || array2[3] != 1)
			{
				throw new SocketException();
			}
			return (int)BitConverter.ToUInt16(array2, 4);
		}

		// Token: 0x06001AFC RID: 6908 RVA: 0x00086B88 File Offset: 0x00084D88
		private static byte[] CreateDacPortInfoRequest(string instanceName)
		{
			instanceName += "\0";
			byte[] array = new byte[Encoding.ASCII.GetByteCount(instanceName) + 2];
			array[0] = 15;
			array[1] = 1;
			Encoding.ASCII.GetBytes(instanceName, 0, instanceName.Length, array, 2);
			return array;
		}

		// Token: 0x06001AFD RID: 6909 RVA: 0x00086BD4 File Offset: 0x00084DD4
		private static byte[] SendUDPRequest(string browserHostname, int port, byte[] requestPacket)
		{
			IPAddress ipaddress = null;
			bool flag = IPAddress.TryParse(browserHostname, out ipaddress);
			byte[] array = null;
			using (UdpClient udpClient = new UdpClient((!flag) ? AddressFamily.InterNetwork : ipaddress.AddressFamily))
			{
				Task<UdpReceiveResult> task;
				if (udpClient.SendAsync(requestPacket, requestPacket.Length, browserHostname, port).Wait(1000) && (task = udpClient.ReceiveAsync()).Wait(1000))
				{
					array = task.Result.Buffer;
				}
			}
			return array;
		}

		// Token: 0x0400136D RID: 4973
		private const char SemicolonSeparator = ';';

		// Token: 0x0400136E RID: 4974
		private const int SqlServerBrowserPort = 1434;
	}
}
