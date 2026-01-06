using System;
using System.ComponentModel;
using UnityEngine.Networking.PlayerConnection;

namespace UnityEngine.Diagnostics
{
	// Token: 0x0200044D RID: 1101
	public static class PlayerConnection
	{
		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x060026DE RID: 9950 RVA: 0x00040CEC File Offset: 0x0003EEEC
		[Obsolete("Use UnityEngine.Networking.PlayerConnection.PlayerConnection.instance.isConnected instead.")]
		public static bool connected
		{
			get
			{
				return PlayerConnection.instance.isConnected;
			}
		}

		// Token: 0x060026DF RID: 9951 RVA: 0x00004557 File Offset: 0x00002757
		[Obsolete("PlayerConnection.SendFile is no longer supported.", true)]
		[EditorBrowsable(1)]
		public static void SendFile(string remoteFilePath, byte[] data)
		{
		}
	}
}
