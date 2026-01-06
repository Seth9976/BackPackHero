using System;

namespace Unity.Services.Analytics.Internal
{
	// Token: 0x02000026 RID: 38
	internal class BufferRevoked : IBuffer
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000090 RID: 144 RVA: 0x000038A3 File Offset: 0x00001AA3
		// (set) Token: 0x06000091 RID: 145 RVA: 0x000038AB File Offset: 0x00001AAB
		public string UserID { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000092 RID: 146 RVA: 0x000038B4 File Offset: 0x00001AB4
		// (set) Token: 0x06000093 RID: 147 RVA: 0x000038BC File Offset: 0x00001ABC
		public string InstallID { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000094 RID: 148 RVA: 0x000038C5 File Offset: 0x00001AC5
		// (set) Token: 0x06000095 RID: 149 RVA: 0x000038CD File Offset: 0x00001ACD
		public string PlayerID { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000096 RID: 150 RVA: 0x000038D6 File Offset: 0x00001AD6
		// (set) Token: 0x06000097 RID: 151 RVA: 0x000038DE File Offset: 0x00001ADE
		public string SessionID { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000098 RID: 152 RVA: 0x000038E7 File Offset: 0x00001AE7
		public int Length
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000038EA File Offset: 0x00001AEA
		public void ClearBuffer()
		{
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000038EC File Offset: 0x00001AEC
		public void ClearBuffer(long upTo)
		{
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000038EE File Offset: 0x00001AEE
		public void ClearDiskCache()
		{
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000038F0 File Offset: 0x00001AF0
		public void FlushToDisk()
		{
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000038F2 File Offset: 0x00001AF2
		public void LoadFromDisk()
		{
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000038F4 File Offset: 0x00001AF4
		public void PushArrayEnd()
		{
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000038F6 File Offset: 0x00001AF6
		public void PushArrayStart(string name = null)
		{
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000038F8 File Offset: 0x00001AF8
		public void PushBool(bool val, string name = null)
		{
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000038FA File Offset: 0x00001AFA
		public void PushDouble(double val, string name = null)
		{
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000038FC File Offset: 0x00001AFC
		public void PushEndEvent()
		{
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000038FE File Offset: 0x00001AFE
		[Obsolete("This mechanism is no longer supported and will be removed in a future version. Use the new Core IAnalyticsStandardEventComponent API instead.")]
		public void PushEvent(Event evt)
		{
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003900 File Offset: 0x00001B00
		public void PushFloat(float val, string name = null)
		{
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003902 File Offset: 0x00001B02
		public void PushInt(int val, string name = null)
		{
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003904 File Offset: 0x00001B04
		public void PushInt64(long val, string name = null)
		{
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003906 File Offset: 0x00001B06
		public void PushObjectEnd()
		{
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003908 File Offset: 0x00001B08
		public void PushObjectStart(string name = null)
		{
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x0000390A File Offset: 0x00001B0A
		public void PushStartEvent(string name, DateTime datetime, long? eventVersion, bool addPlayerIdsToEventBody = false)
		{
		}

		// Token: 0x060000AA RID: 170 RVA: 0x0000390C File Offset: 0x00001B0C
		public void PushString(string val, string name = null)
		{
		}

		// Token: 0x060000AB RID: 171 RVA: 0x0000390E File Offset: 0x00001B0E
		public void PushTimestamp(DateTime val, string name = null)
		{
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003910 File Offset: 0x00001B10
		public byte[] Serialize()
		{
			return null;
		}
	}
}
