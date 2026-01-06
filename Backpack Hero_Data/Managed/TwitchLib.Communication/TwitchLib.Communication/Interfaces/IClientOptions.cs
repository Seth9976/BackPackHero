using System;
using TwitchLib.Communication.Enums;
using TwitchLib.Communication.Models;

namespace TwitchLib.Communication.Interfaces
{
	// Token: 0x02000006 RID: 6
	public interface IClientOptions
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600005C RID: 92
		// (set) Token: 0x0600005D RID: 93
		ClientType ClientType { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600005E RID: 94
		// (set) Token: 0x0600005F RID: 95
		int DisconnectWait { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000060 RID: 96
		// (set) Token: 0x06000061 RID: 97
		int MessagesAllowedInPeriod { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000062 RID: 98
		// (set) Token: 0x06000063 RID: 99
		ReconnectionPolicy ReconnectionPolicy { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000064 RID: 100
		// (set) Token: 0x06000065 RID: 101
		TimeSpan SendCacheItemTimeout { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000066 RID: 102
		// (set) Token: 0x06000067 RID: 103
		ushort SendDelay { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000068 RID: 104
		// (set) Token: 0x06000069 RID: 105
		int SendQueueCapacity { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600006A RID: 106
		// (set) Token: 0x0600006B RID: 107
		TimeSpan ThrottlingPeriod { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600006C RID: 108
		// (set) Token: 0x0600006D RID: 109
		bool UseSsl { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600006E RID: 110
		// (set) Token: 0x0600006F RID: 111
		TimeSpan WhisperThrottlingPeriod { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000070 RID: 112
		// (set) Token: 0x06000071 RID: 113
		int WhispersAllowedInPeriod { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000072 RID: 114
		// (set) Token: 0x06000073 RID: 115
		int WhisperQueueCapacity { get; set; }
	}
}
