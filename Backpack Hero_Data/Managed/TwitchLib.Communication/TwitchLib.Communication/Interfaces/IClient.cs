using System;
using TwitchLib.Communication.Events;

namespace TwitchLib.Communication.Interfaces
{
	// Token: 0x02000005 RID: 5
	public interface IClient
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000036 RID: 54
		// (set) Token: 0x06000037 RID: 55
		TimeSpan DefaultKeepAliveInterval { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000038 RID: 56
		int SendQueueLength { get; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000039 RID: 57
		int WhisperQueueLength { get; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600003A RID: 58
		bool IsConnected { get; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600003B RID: 59
		IClientOptions Options { get; }

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600003C RID: 60
		// (remove) Token: 0x0600003D RID: 61
		event EventHandler<OnConnectedEventArgs> OnConnected;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600003E RID: 62
		// (remove) Token: 0x0600003F RID: 63
		event EventHandler<OnDataEventArgs> OnData;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000040 RID: 64
		// (remove) Token: 0x06000041 RID: 65
		event EventHandler<OnDisconnectedEventArgs> OnDisconnected;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000042 RID: 66
		// (remove) Token: 0x06000043 RID: 67
		event EventHandler<OnErrorEventArgs> OnError;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000044 RID: 68
		// (remove) Token: 0x06000045 RID: 69
		event EventHandler<OnFatalErrorEventArgs> OnFatality;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000046 RID: 70
		// (remove) Token: 0x06000047 RID: 71
		event EventHandler<OnMessageEventArgs> OnMessage;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000048 RID: 72
		// (remove) Token: 0x06000049 RID: 73
		event EventHandler<OnMessageThrottledEventArgs> OnMessageThrottled;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x0600004A RID: 74
		// (remove) Token: 0x0600004B RID: 75
		event EventHandler<OnWhisperThrottledEventArgs> OnWhisperThrottled;

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x0600004C RID: 76
		// (remove) Token: 0x0600004D RID: 77
		event EventHandler<OnSendFailedEventArgs> OnSendFailed;

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x0600004E RID: 78
		// (remove) Token: 0x0600004F RID: 79
		event EventHandler<OnStateChangedEventArgs> OnStateChanged;

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x06000050 RID: 80
		// (remove) Token: 0x06000051 RID: 81
		event EventHandler<OnReconnectedEventArgs> OnReconnected;

		// Token: 0x06000052 RID: 82
		void Close(bool callDisconnect = true);

		// Token: 0x06000053 RID: 83
		void Dispose();

		// Token: 0x06000054 RID: 84
		bool Open();

		// Token: 0x06000055 RID: 85
		bool Send(string message);

		// Token: 0x06000056 RID: 86
		bool SendWhisper(string message);

		// Token: 0x06000057 RID: 87
		void Reconnect();

		// Token: 0x06000058 RID: 88
		void MessageThrottled(OnMessageThrottledEventArgs eventArgs);

		// Token: 0x06000059 RID: 89
		void SendFailed(OnSendFailedEventArgs eventArgs);

		// Token: 0x0600005A RID: 90
		void Error(OnErrorEventArgs eventArgs);

		// Token: 0x0600005B RID: 91
		void WhisperThrottled(OnWhisperThrottledEventArgs eventArgs);
	}
}
