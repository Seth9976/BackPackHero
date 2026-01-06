using System;

namespace Unity.Services.Analytics.Internal
{
	// Token: 0x02000025 RID: 37
	internal interface IBuffer
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000073 RID: 115
		// (set) Token: 0x06000074 RID: 116
		string UserID { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000075 RID: 117
		// (set) Token: 0x06000076 RID: 118
		string InstallID { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000077 RID: 119
		// (set) Token: 0x06000078 RID: 120
		string PlayerID { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000079 RID: 121
		// (set) Token: 0x0600007A RID: 122
		string SessionID { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600007B RID: 123
		int Length { get; }

		// Token: 0x0600007C RID: 124
		byte[] Serialize();

		// Token: 0x0600007D RID: 125
		void PushStartEvent(string name, DateTime datetime, long? eventVersion, bool addPlayerIdsToEventBody = false);

		// Token: 0x0600007E RID: 126
		void PushEndEvent();

		// Token: 0x0600007F RID: 127
		void PushObjectStart(string name = null);

		// Token: 0x06000080 RID: 128
		void PushObjectEnd();

		// Token: 0x06000081 RID: 129
		void PushArrayStart(string name);

		// Token: 0x06000082 RID: 130
		void PushArrayEnd();

		// Token: 0x06000083 RID: 131
		void PushDouble(double val, string name = null);

		// Token: 0x06000084 RID: 132
		void PushFloat(float val, string name = null);

		// Token: 0x06000085 RID: 133
		void PushString(string val, string name = null);

		// Token: 0x06000086 RID: 134
		void PushInt64(long val, string name = null);

		// Token: 0x06000087 RID: 135
		void PushInt(int val, string name = null);

		// Token: 0x06000088 RID: 136
		void PushBool(bool val, string name = null);

		// Token: 0x06000089 RID: 137
		void PushTimestamp(DateTime val, string name = null);

		// Token: 0x0600008A RID: 138
		void FlushToDisk();

		// Token: 0x0600008B RID: 139
		void ClearDiskCache();

		// Token: 0x0600008C RID: 140
		void ClearBuffer();

		// Token: 0x0600008D RID: 141
		void ClearBuffer(long upTo);

		// Token: 0x0600008E RID: 142
		void LoadFromDisk();

		// Token: 0x0600008F RID: 143
		[Obsolete("This mechanism is no longer supported and will be removed in a future version. Use the new Core IAnalyticsStandardEventComponent API instead.")]
		void PushEvent(Event evt);
	}
}
