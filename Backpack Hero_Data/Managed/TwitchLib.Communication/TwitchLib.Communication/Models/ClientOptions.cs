using System;
using TwitchLib.Communication.Enums;
using TwitchLib.Communication.Interfaces;

namespace TwitchLib.Communication.Models
{
	// Token: 0x02000003 RID: 3
	public class ClientOptions : IClientOptions
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000012 RID: 18 RVA: 0x0000229C File Offset: 0x0000049C
		// (set) Token: 0x06000013 RID: 19 RVA: 0x000022A4 File Offset: 0x000004A4
		public int SendQueueCapacity { get; set; } = 10000;

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000014 RID: 20 RVA: 0x000022AD File Offset: 0x000004AD
		// (set) Token: 0x06000015 RID: 21 RVA: 0x000022B5 File Offset: 0x000004B5
		public TimeSpan SendCacheItemTimeout { get; set; } = TimeSpan.FromMinutes(30.0);

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000022BE File Offset: 0x000004BE
		// (set) Token: 0x06000017 RID: 23 RVA: 0x000022C6 File Offset: 0x000004C6
		public ushort SendDelay { get; set; } = 50;

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000022CF File Offset: 0x000004CF
		// (set) Token: 0x06000019 RID: 25 RVA: 0x000022D7 File Offset: 0x000004D7
		public ReconnectionPolicy ReconnectionPolicy { get; set; } = new ReconnectionPolicy(3000, new int?(10));

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001A RID: 26 RVA: 0x000022E0 File Offset: 0x000004E0
		// (set) Token: 0x0600001B RID: 27 RVA: 0x000022E8 File Offset: 0x000004E8
		public bool UseSsl { get; set; } = true;

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001C RID: 28 RVA: 0x000022F1 File Offset: 0x000004F1
		// (set) Token: 0x0600001D RID: 29 RVA: 0x000022F9 File Offset: 0x000004F9
		public int DisconnectWait { get; set; } = 20000;

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002302 File Offset: 0x00000502
		// (set) Token: 0x0600001F RID: 31 RVA: 0x0000230A File Offset: 0x0000050A
		public ClientType ClientType { get; set; } = ClientType.Chat;

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002313 File Offset: 0x00000513
		// (set) Token: 0x06000021 RID: 33 RVA: 0x0000231B File Offset: 0x0000051B
		public TimeSpan ThrottlingPeriod { get; set; } = TimeSpan.FromSeconds(30.0);

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002324 File Offset: 0x00000524
		// (set) Token: 0x06000023 RID: 35 RVA: 0x0000232C File Offset: 0x0000052C
		public int MessagesAllowedInPeriod { get; set; } = 100;

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002335 File Offset: 0x00000535
		// (set) Token: 0x06000025 RID: 37 RVA: 0x0000233D File Offset: 0x0000053D
		public TimeSpan WhisperThrottlingPeriod { get; set; } = TimeSpan.FromSeconds(60.0);

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002346 File Offset: 0x00000546
		// (set) Token: 0x06000027 RID: 39 RVA: 0x0000234E File Offset: 0x0000054E
		public int WhispersAllowedInPeriod { get; set; } = 100;

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002357 File Offset: 0x00000557
		// (set) Token: 0x06000029 RID: 41 RVA: 0x0000235F File Offset: 0x0000055F
		public int WhisperQueueCapacity { get; set; } = 10000;
	}
}
