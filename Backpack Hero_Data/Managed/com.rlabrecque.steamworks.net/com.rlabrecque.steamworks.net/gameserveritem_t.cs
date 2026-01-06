using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Steamworks
{
	// Token: 0x0200018A RID: 394
	[Serializable]
	[StructLayout(LayoutKind.Sequential, Pack = 4, Size = 372)]
	public class gameserveritem_t
	{
		// Token: 0x06000919 RID: 2329 RVA: 0x0000E01D File Offset: 0x0000C21D
		public string GetGameDir()
		{
			return Encoding.UTF8.GetString(this.m_szGameDir, 0, Array.IndexOf<byte>(this.m_szGameDir, 0));
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x0000E03C File Offset: 0x0000C23C
		public void SetGameDir(string dir)
		{
			this.m_szGameDir = Encoding.UTF8.GetBytes(dir + "\0");
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x0000E059 File Offset: 0x0000C259
		public string GetMap()
		{
			return Encoding.UTF8.GetString(this.m_szMap, 0, Array.IndexOf<byte>(this.m_szMap, 0));
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x0000E078 File Offset: 0x0000C278
		public void SetMap(string map)
		{
			this.m_szMap = Encoding.UTF8.GetBytes(map + "\0");
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x0000E095 File Offset: 0x0000C295
		public string GetGameDescription()
		{
			return Encoding.UTF8.GetString(this.m_szGameDescription, 0, Array.IndexOf<byte>(this.m_szGameDescription, 0));
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x0000E0B4 File Offset: 0x0000C2B4
		public void SetGameDescription(string desc)
		{
			this.m_szGameDescription = Encoding.UTF8.GetBytes(desc + "\0");
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x0000E0D1 File Offset: 0x0000C2D1
		public string GetServerName()
		{
			if (this.m_szServerName[0] == 0)
			{
				return this.m_NetAdr.GetConnectionAddressString();
			}
			return Encoding.UTF8.GetString(this.m_szServerName, 0, Array.IndexOf<byte>(this.m_szServerName, 0));
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x0000E106 File Offset: 0x0000C306
		public void SetServerName(string name)
		{
			this.m_szServerName = Encoding.UTF8.GetBytes(name + "\0");
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x0000E123 File Offset: 0x0000C323
		public string GetGameTags()
		{
			return Encoding.UTF8.GetString(this.m_szGameTags, 0, Array.IndexOf<byte>(this.m_szGameTags, 0));
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x0000E142 File Offset: 0x0000C342
		public void SetGameTags(string tags)
		{
			this.m_szGameTags = Encoding.UTF8.GetBytes(tags + "\0");
		}

		// Token: 0x04000A32 RID: 2610
		public servernetadr_t m_NetAdr;

		// Token: 0x04000A33 RID: 2611
		public int m_nPing;

		// Token: 0x04000A34 RID: 2612
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bHadSuccessfulResponse;

		// Token: 0x04000A35 RID: 2613
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bDoNotRefresh;

		// Token: 0x04000A36 RID: 2614
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
		private byte[] m_szGameDir;

		// Token: 0x04000A37 RID: 2615
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
		private byte[] m_szMap;

		// Token: 0x04000A38 RID: 2616
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
		private byte[] m_szGameDescription;

		// Token: 0x04000A39 RID: 2617
		public uint m_nAppID;

		// Token: 0x04000A3A RID: 2618
		public int m_nPlayers;

		// Token: 0x04000A3B RID: 2619
		public int m_nMaxPlayers;

		// Token: 0x04000A3C RID: 2620
		public int m_nBotPlayers;

		// Token: 0x04000A3D RID: 2621
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bPassword;

		// Token: 0x04000A3E RID: 2622
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bSecure;

		// Token: 0x04000A3F RID: 2623
		public uint m_ulTimeLastPlayed;

		// Token: 0x04000A40 RID: 2624
		public int m_nServerVersion;

		// Token: 0x04000A41 RID: 2625
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
		private byte[] m_szServerName;

		// Token: 0x04000A42 RID: 2626
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
		private byte[] m_szGameTags;

		// Token: 0x04000A43 RID: 2627
		public CSteamID m_steamID;
	}
}
