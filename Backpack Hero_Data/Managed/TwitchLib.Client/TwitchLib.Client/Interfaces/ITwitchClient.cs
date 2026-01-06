using System;
using System.Collections.Generic;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Events;

namespace TwitchLib.Client.Interfaces
{
	// Token: 0x02000006 RID: 6
	public interface ITwitchClient
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000132 RID: 306
		// (set) Token: 0x06000133 RID: 307
		bool AutoReListenOnException { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000134 RID: 308
		MessageEmoteCollection ChannelEmotes { get; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000135 RID: 309
		ConnectionCredentials ConnectionCredentials { get; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000136 RID: 310
		// (set) Token: 0x06000137 RID: 311
		bool DisableAutoPong { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000138 RID: 312
		bool IsConnected { get; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000139 RID: 313
		bool IsInitialized { get; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600013A RID: 314
		IReadOnlyList<JoinedChannel> JoinedChannels { get; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600013B RID: 315
		WhisperMessage PreviousWhisper { get; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600013C RID: 316
		string TwitchUsername { get; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600013D RID: 317
		// (set) Token: 0x0600013E RID: 318
		bool WillReplaceEmotes { get; set; }

		// Token: 0x1400003B RID: 59
		// (add) Token: 0x0600013F RID: 319
		// (remove) Token: 0x06000140 RID: 320
		event EventHandler<OnChannelStateChangedArgs> OnChannelStateChanged;

		// Token: 0x1400003C RID: 60
		// (add) Token: 0x06000141 RID: 321
		// (remove) Token: 0x06000142 RID: 322
		event EventHandler<OnChatClearedArgs> OnChatCleared;

		// Token: 0x1400003D RID: 61
		// (add) Token: 0x06000143 RID: 323
		// (remove) Token: 0x06000144 RID: 324
		event EventHandler<OnChatColorChangedArgs> OnChatColorChanged;

		// Token: 0x1400003E RID: 62
		// (add) Token: 0x06000145 RID: 325
		// (remove) Token: 0x06000146 RID: 326
		event EventHandler<OnChatCommandReceivedArgs> OnChatCommandReceived;

		// Token: 0x1400003F RID: 63
		// (add) Token: 0x06000147 RID: 327
		// (remove) Token: 0x06000148 RID: 328
		event EventHandler<OnConnectedArgs> OnConnected;

		// Token: 0x14000040 RID: 64
		// (add) Token: 0x06000149 RID: 329
		// (remove) Token: 0x0600014A RID: 330
		event EventHandler<OnConnectionErrorArgs> OnConnectionError;

		// Token: 0x14000041 RID: 65
		// (add) Token: 0x0600014B RID: 331
		// (remove) Token: 0x0600014C RID: 332
		event EventHandler<OnDisconnectedEventArgs> OnDisconnected;

		// Token: 0x14000042 RID: 66
		// (add) Token: 0x0600014D RID: 333
		// (remove) Token: 0x0600014E RID: 334
		event EventHandler<OnExistingUsersDetectedArgs> OnExistingUsersDetected;

		// Token: 0x14000043 RID: 67
		// (add) Token: 0x0600014F RID: 335
		// (remove) Token: 0x06000150 RID: 336
		event EventHandler<OnGiftedSubscriptionArgs> OnGiftedSubscription;

		// Token: 0x14000044 RID: 68
		// (add) Token: 0x06000151 RID: 337
		// (remove) Token: 0x06000152 RID: 338
		event EventHandler<OnIncorrectLoginArgs> OnIncorrectLogin;

		// Token: 0x14000045 RID: 69
		// (add) Token: 0x06000153 RID: 339
		// (remove) Token: 0x06000154 RID: 340
		event EventHandler<OnJoinedChannelArgs> OnJoinedChannel;

		// Token: 0x14000046 RID: 70
		// (add) Token: 0x06000155 RID: 341
		// (remove) Token: 0x06000156 RID: 342
		event EventHandler<OnLeftChannelArgs> OnLeftChannel;

		// Token: 0x14000047 RID: 71
		// (add) Token: 0x06000157 RID: 343
		// (remove) Token: 0x06000158 RID: 344
		event EventHandler<OnLogArgs> OnLog;

		// Token: 0x14000048 RID: 72
		// (add) Token: 0x06000159 RID: 345
		// (remove) Token: 0x0600015A RID: 346
		event EventHandler<OnMessageReceivedArgs> OnMessageReceived;

		// Token: 0x14000049 RID: 73
		// (add) Token: 0x0600015B RID: 347
		// (remove) Token: 0x0600015C RID: 348
		event EventHandler<OnMessageSentArgs> OnMessageSent;

		// Token: 0x1400004A RID: 74
		// (add) Token: 0x0600015D RID: 349
		// (remove) Token: 0x0600015E RID: 350
		event EventHandler<OnModeratorJoinedArgs> OnModeratorJoined;

		// Token: 0x1400004B RID: 75
		// (add) Token: 0x0600015F RID: 351
		// (remove) Token: 0x06000160 RID: 352
		event EventHandler<OnModeratorLeftArgs> OnModeratorLeft;

		// Token: 0x1400004C RID: 76
		// (add) Token: 0x06000161 RID: 353
		// (remove) Token: 0x06000162 RID: 354
		event EventHandler<OnModeratorsReceivedArgs> OnModeratorsReceived;

		// Token: 0x1400004D RID: 77
		// (add) Token: 0x06000163 RID: 355
		// (remove) Token: 0x06000164 RID: 356
		event EventHandler<OnNewSubscriberArgs> OnNewSubscriber;

		// Token: 0x1400004E RID: 78
		// (add) Token: 0x06000165 RID: 357
		// (remove) Token: 0x06000166 RID: 358
		event EventHandler<OnRaidNotificationArgs> OnRaidNotification;

		// Token: 0x1400004F RID: 79
		// (add) Token: 0x06000167 RID: 359
		// (remove) Token: 0x06000168 RID: 360
		event EventHandler<OnReSubscriberArgs> OnReSubscriber;

		// Token: 0x14000050 RID: 80
		// (add) Token: 0x06000169 RID: 361
		// (remove) Token: 0x0600016A RID: 362
		event EventHandler<OnSendReceiveDataArgs> OnSendReceiveData;

		// Token: 0x14000051 RID: 81
		// (add) Token: 0x0600016B RID: 363
		// (remove) Token: 0x0600016C RID: 364
		event EventHandler<OnUserBannedArgs> OnUserBanned;

		// Token: 0x14000052 RID: 82
		// (add) Token: 0x0600016D RID: 365
		// (remove) Token: 0x0600016E RID: 366
		event EventHandler<OnUserJoinedArgs> OnUserJoined;

		// Token: 0x14000053 RID: 83
		// (add) Token: 0x0600016F RID: 367
		// (remove) Token: 0x06000170 RID: 368
		event EventHandler<OnUserLeftArgs> OnUserLeft;

		// Token: 0x14000054 RID: 84
		// (add) Token: 0x06000171 RID: 369
		// (remove) Token: 0x06000172 RID: 370
		event EventHandler<OnUserStateChangedArgs> OnUserStateChanged;

		// Token: 0x14000055 RID: 85
		// (add) Token: 0x06000173 RID: 371
		// (remove) Token: 0x06000174 RID: 372
		event EventHandler<OnUserTimedoutArgs> OnUserTimedout;

		// Token: 0x14000056 RID: 86
		// (add) Token: 0x06000175 RID: 373
		// (remove) Token: 0x06000176 RID: 374
		event EventHandler<OnWhisperCommandReceivedArgs> OnWhisperCommandReceived;

		// Token: 0x14000057 RID: 87
		// (add) Token: 0x06000177 RID: 375
		// (remove) Token: 0x06000178 RID: 376
		event EventHandler<OnWhisperReceivedArgs> OnWhisperReceived;

		// Token: 0x14000058 RID: 88
		// (add) Token: 0x06000179 RID: 377
		// (remove) Token: 0x0600017A RID: 378
		event EventHandler<OnWhisperSentArgs> OnWhisperSent;

		// Token: 0x14000059 RID: 89
		// (add) Token: 0x0600017B RID: 379
		// (remove) Token: 0x0600017C RID: 380
		event EventHandler<OnMessageThrottledEventArgs> OnMessageThrottled;

		// Token: 0x1400005A RID: 90
		// (add) Token: 0x0600017D RID: 381
		// (remove) Token: 0x0600017E RID: 382
		event EventHandler<OnWhisperThrottledEventArgs> OnWhisperThrottled;

		// Token: 0x1400005B RID: 91
		// (add) Token: 0x0600017F RID: 383
		// (remove) Token: 0x06000180 RID: 384
		event EventHandler<OnErrorEventArgs> OnError;

		// Token: 0x1400005C RID: 92
		// (add) Token: 0x06000181 RID: 385
		// (remove) Token: 0x06000182 RID: 386
		event EventHandler<OnReconnectedEventArgs> OnReconnected;

		// Token: 0x1400005D RID: 93
		// (add) Token: 0x06000183 RID: 387
		// (remove) Token: 0x06000184 RID: 388
		event EventHandler<OnVIPsReceivedArgs> OnVIPsReceived;

		// Token: 0x1400005E RID: 94
		// (add) Token: 0x06000185 RID: 389
		// (remove) Token: 0x06000186 RID: 390
		event EventHandler<OnCommunitySubscriptionArgs> OnCommunitySubscription;

		// Token: 0x1400005F RID: 95
		// (add) Token: 0x06000187 RID: 391
		// (remove) Token: 0x06000188 RID: 392
		event EventHandler<OnMessageClearedArgs> OnMessageCleared;

		// Token: 0x14000060 RID: 96
		// (add) Token: 0x06000189 RID: 393
		// (remove) Token: 0x0600018A RID: 394
		event EventHandler<OnRequiresVerifiedEmailArgs> OnRequiresVerifiedEmail;

		// Token: 0x14000061 RID: 97
		// (add) Token: 0x0600018B RID: 395
		// (remove) Token: 0x0600018C RID: 396
		event EventHandler<OnRequiresVerifiedPhoneNumberArgs> OnRequiresVerifiedPhoneNumber;

		// Token: 0x14000062 RID: 98
		// (add) Token: 0x0600018D RID: 397
		// (remove) Token: 0x0600018E RID: 398
		event EventHandler<OnBannedEmailAliasArgs> OnBannedEmailAlias;

		// Token: 0x14000063 RID: 99
		// (add) Token: 0x0600018F RID: 399
		// (remove) Token: 0x06000190 RID: 400
		event EventHandler<OnUserIntroArgs> OnUserIntro;

		// Token: 0x14000064 RID: 100
		// (add) Token: 0x06000191 RID: 401
		// (remove) Token: 0x06000192 RID: 402
		event EventHandler<OnAnnouncementArgs> OnAnnouncement;

		// Token: 0x06000193 RID: 403
		void Initialize(ConnectionCredentials credentials, string channel = null, char chatCommandIdentifier = '!', char whisperCommandIdentifier = '!', bool autoReListenOnExceptions = true);

		// Token: 0x06000194 RID: 404
		void Initialize(ConnectionCredentials credentials, List<string> channels, char chatCommandIdentifier = '!', char whisperCommandIdentifier = '!', bool autoReListenOnExceptions = true);

		// Token: 0x06000195 RID: 405
		void SetConnectionCredentials(ConnectionCredentials credentials);

		// Token: 0x06000196 RID: 406
		void AddChatCommandIdentifier(char identifier);

		// Token: 0x06000197 RID: 407
		void AddWhisperCommandIdentifier(char identifier);

		// Token: 0x06000198 RID: 408
		void RemoveChatCommandIdentifier(char identifier);

		// Token: 0x06000199 RID: 409
		void RemoveWhisperCommandIdentifier(char identifier);

		// Token: 0x0600019A RID: 410
		bool Connect();

		// Token: 0x0600019B RID: 411
		void Disconnect();

		// Token: 0x0600019C RID: 412
		void Reconnect();

		// Token: 0x0600019D RID: 413
		JoinedChannel GetJoinedChannel(string channel);

		// Token: 0x0600019E RID: 414
		void JoinChannel(string channel, bool overrideCheck = false);

		// Token: 0x0600019F RID: 415
		void LeaveChannel(JoinedChannel channel);

		// Token: 0x060001A0 RID: 416
		void LeaveChannel(string channel);

		// Token: 0x060001A1 RID: 417
		void OnReadLineTest(string rawIrc);

		// Token: 0x060001A2 RID: 418
		void SendMessage(JoinedChannel channel, string message, bool dryRun = false);

		// Token: 0x060001A3 RID: 419
		void SendMessage(string channel, string message, bool dryRun = false);

		// Token: 0x060001A4 RID: 420
		void SendReply(JoinedChannel channel, string replyToId, string message, bool dryRun = false);

		// Token: 0x060001A5 RID: 421
		void SendReply(string channel, string replyToId, string message, bool dryRun = false);

		// Token: 0x060001A6 RID: 422
		void SendQueuedItem(string message);

		// Token: 0x060001A7 RID: 423
		void SendRaw(string message);

		// Token: 0x060001A8 RID: 424
		void SendWhisper(string receiver, string message, bool dryRun = false);
	}
}
