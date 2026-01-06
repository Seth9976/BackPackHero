using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Timers;
using Microsoft.Extensions.Logging;
using TwitchLib.Client.Enums;
using TwitchLib.Client.Enums.Internal;
using TwitchLib.Client.Events;
using TwitchLib.Client.Exceptions;
using TwitchLib.Client.Interfaces;
using TwitchLib.Client.Internal;
using TwitchLib.Client.Internal.Parsing;
using TwitchLib.Client.Manager;
using TwitchLib.Client.Models;
using TwitchLib.Client.Models.Internal;
using TwitchLib.Communication.Clients;
using TwitchLib.Communication.Events;
using TwitchLib.Communication.Interfaces;

namespace TwitchLib.Client
{
	// Token: 0x02000002 RID: 2
	public class TwitchClient : ITwitchClient
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public Version Version
		{
			get
			{
				return Assembly.GetEntryAssembly().GetName().Version;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002061 File Offset: 0x00000261
		public bool IsInitialized
		{
			get
			{
				return this._client != null;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000003 RID: 3 RVA: 0x0000206C File Offset: 0x0000026C
		public IReadOnlyList<JoinedChannel> JoinedChannels
		{
			get
			{
				return this._joinedChannelManager.GetJoinedChannels();
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002079 File Offset: 0x00000279
		// (set) Token: 0x06000005 RID: 5 RVA: 0x00002081 File Offset: 0x00000281
		public string TwitchUsername { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000006 RID: 6 RVA: 0x0000208A File Offset: 0x0000028A
		// (set) Token: 0x06000007 RID: 7 RVA: 0x00002092 File Offset: 0x00000292
		public WhisperMessage PreviousWhisper { get; private set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000008 RID: 8 RVA: 0x0000209B File Offset: 0x0000029B
		public bool IsConnected
		{
			get
			{
				return this.IsInitialized && this._client != null && this._client.IsConnected;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000020BA File Offset: 0x000002BA
		public MessageEmoteCollection ChannelEmotes
		{
			get
			{
				return this._channelEmotes;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000020C2 File Offset: 0x000002C2
		// (set) Token: 0x0600000B RID: 11 RVA: 0x000020CA File Offset: 0x000002CA
		public bool DisableAutoPong { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000020D3 File Offset: 0x000002D3
		// (set) Token: 0x0600000D RID: 13 RVA: 0x000020DB File Offset: 0x000002DB
		public bool WillReplaceEmotes { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000020E4 File Offset: 0x000002E4
		// (set) Token: 0x0600000F RID: 15 RVA: 0x000020EC File Offset: 0x000002EC
		public ConnectionCredentials ConnectionCredentials { get; private set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000020F5 File Offset: 0x000002F5
		// (set) Token: 0x06000011 RID: 17 RVA: 0x000020FD File Offset: 0x000002FD
		public bool AutoReListenOnException { get; set; }

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000012 RID: 18 RVA: 0x00002108 File Offset: 0x00000308
		// (remove) Token: 0x06000013 RID: 19 RVA: 0x00002140 File Offset: 0x00000340
		public event EventHandler<OnAnnouncementArgs> OnAnnouncement;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000014 RID: 20 RVA: 0x00002178 File Offset: 0x00000378
		// (remove) Token: 0x06000015 RID: 21 RVA: 0x000021B0 File Offset: 0x000003B0
		public event EventHandler<OnVIPsReceivedArgs> OnVIPsReceived;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000016 RID: 22 RVA: 0x000021E8 File Offset: 0x000003E8
		// (remove) Token: 0x06000017 RID: 23 RVA: 0x00002220 File Offset: 0x00000420
		public event EventHandler<OnLogArgs> OnLog;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000018 RID: 24 RVA: 0x00002258 File Offset: 0x00000458
		// (remove) Token: 0x06000019 RID: 25 RVA: 0x00002290 File Offset: 0x00000490
		public event EventHandler<OnConnectedArgs> OnConnected;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x0600001A RID: 26 RVA: 0x000022C8 File Offset: 0x000004C8
		// (remove) Token: 0x0600001B RID: 27 RVA: 0x00002300 File Offset: 0x00000500
		public event EventHandler<OnJoinedChannelArgs> OnJoinedChannel;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x0600001C RID: 28 RVA: 0x00002338 File Offset: 0x00000538
		// (remove) Token: 0x0600001D RID: 29 RVA: 0x00002370 File Offset: 0x00000570
		public event EventHandler<OnIncorrectLoginArgs> OnIncorrectLogin;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x0600001E RID: 30 RVA: 0x000023A8 File Offset: 0x000005A8
		// (remove) Token: 0x0600001F RID: 31 RVA: 0x000023E0 File Offset: 0x000005E0
		public event EventHandler<OnChannelStateChangedArgs> OnChannelStateChanged;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000020 RID: 32 RVA: 0x00002418 File Offset: 0x00000618
		// (remove) Token: 0x06000021 RID: 33 RVA: 0x00002450 File Offset: 0x00000650
		public event EventHandler<OnUserStateChangedArgs> OnUserStateChanged;

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000022 RID: 34 RVA: 0x00002488 File Offset: 0x00000688
		// (remove) Token: 0x06000023 RID: 35 RVA: 0x000024C0 File Offset: 0x000006C0
		public event EventHandler<OnMessageReceivedArgs> OnMessageReceived;

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06000024 RID: 36 RVA: 0x000024F8 File Offset: 0x000006F8
		// (remove) Token: 0x06000025 RID: 37 RVA: 0x00002530 File Offset: 0x00000730
		public event EventHandler<OnWhisperReceivedArgs> OnWhisperReceived;

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x06000026 RID: 38 RVA: 0x00002568 File Offset: 0x00000768
		// (remove) Token: 0x06000027 RID: 39 RVA: 0x000025A0 File Offset: 0x000007A0
		public event EventHandler<OnMessageSentArgs> OnMessageSent;

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x06000028 RID: 40 RVA: 0x000025D8 File Offset: 0x000007D8
		// (remove) Token: 0x06000029 RID: 41 RVA: 0x00002610 File Offset: 0x00000810
		public event EventHandler<OnWhisperSentArgs> OnWhisperSent;

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x0600002A RID: 42 RVA: 0x00002648 File Offset: 0x00000848
		// (remove) Token: 0x0600002B RID: 43 RVA: 0x00002680 File Offset: 0x00000880
		public event EventHandler<OnChatCommandReceivedArgs> OnChatCommandReceived;

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x0600002C RID: 44 RVA: 0x000026B8 File Offset: 0x000008B8
		// (remove) Token: 0x0600002D RID: 45 RVA: 0x000026F0 File Offset: 0x000008F0
		public event EventHandler<OnWhisperCommandReceivedArgs> OnWhisperCommandReceived;

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x0600002E RID: 46 RVA: 0x00002728 File Offset: 0x00000928
		// (remove) Token: 0x0600002F RID: 47 RVA: 0x00002760 File Offset: 0x00000960
		public event EventHandler<OnUserJoinedArgs> OnUserJoined;

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x06000030 RID: 48 RVA: 0x00002798 File Offset: 0x00000998
		// (remove) Token: 0x06000031 RID: 49 RVA: 0x000027D0 File Offset: 0x000009D0
		public event EventHandler<OnModeratorJoinedArgs> OnModeratorJoined;

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x06000032 RID: 50 RVA: 0x00002808 File Offset: 0x00000A08
		// (remove) Token: 0x06000033 RID: 51 RVA: 0x00002840 File Offset: 0x00000A40
		public event EventHandler<OnModeratorLeftArgs> OnModeratorLeft;

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x06000034 RID: 52 RVA: 0x00002878 File Offset: 0x00000A78
		// (remove) Token: 0x06000035 RID: 53 RVA: 0x000028B0 File Offset: 0x00000AB0
		public event EventHandler<OnMessageClearedArgs> OnMessageCleared;

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x06000036 RID: 54 RVA: 0x000028E8 File Offset: 0x00000AE8
		// (remove) Token: 0x06000037 RID: 55 RVA: 0x00002920 File Offset: 0x00000B20
		public event EventHandler<OnNewSubscriberArgs> OnNewSubscriber;

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x06000038 RID: 56 RVA: 0x00002958 File Offset: 0x00000B58
		// (remove) Token: 0x06000039 RID: 57 RVA: 0x00002990 File Offset: 0x00000B90
		public event EventHandler<OnReSubscriberArgs> OnReSubscriber;

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x0600003A RID: 58 RVA: 0x000029C8 File Offset: 0x00000BC8
		// (remove) Token: 0x0600003B RID: 59 RVA: 0x00002A00 File Offset: 0x00000C00
		public event EventHandler<OnPrimePaidSubscriberArgs> OnPrimePaidSubscriber;

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x0600003C RID: 60 RVA: 0x00002A38 File Offset: 0x00000C38
		// (remove) Token: 0x0600003D RID: 61 RVA: 0x00002A70 File Offset: 0x00000C70
		public event EventHandler<OnExistingUsersDetectedArgs> OnExistingUsersDetected;

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x0600003E RID: 62 RVA: 0x00002AA8 File Offset: 0x00000CA8
		// (remove) Token: 0x0600003F RID: 63 RVA: 0x00002AE0 File Offset: 0x00000CE0
		public event EventHandler<OnUserLeftArgs> OnUserLeft;

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x06000040 RID: 64 RVA: 0x00002B18 File Offset: 0x00000D18
		// (remove) Token: 0x06000041 RID: 65 RVA: 0x00002B50 File Offset: 0x00000D50
		public event EventHandler<OnDisconnectedEventArgs> OnDisconnected;

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x06000042 RID: 66 RVA: 0x00002B88 File Offset: 0x00000D88
		// (remove) Token: 0x06000043 RID: 67 RVA: 0x00002BC0 File Offset: 0x00000DC0
		public event EventHandler<OnConnectionErrorArgs> OnConnectionError;

		// Token: 0x1400001A RID: 26
		// (add) Token: 0x06000044 RID: 68 RVA: 0x00002BF8 File Offset: 0x00000DF8
		// (remove) Token: 0x06000045 RID: 69 RVA: 0x00002C30 File Offset: 0x00000E30
		public event EventHandler<OnChatClearedArgs> OnChatCleared;

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x06000046 RID: 70 RVA: 0x00002C68 File Offset: 0x00000E68
		// (remove) Token: 0x06000047 RID: 71 RVA: 0x00002CA0 File Offset: 0x00000EA0
		public event EventHandler<OnUserTimedoutArgs> OnUserTimedout;

		// Token: 0x1400001C RID: 28
		// (add) Token: 0x06000048 RID: 72 RVA: 0x00002CD8 File Offset: 0x00000ED8
		// (remove) Token: 0x06000049 RID: 73 RVA: 0x00002D10 File Offset: 0x00000F10
		public event EventHandler<OnLeftChannelArgs> OnLeftChannel;

		// Token: 0x1400001D RID: 29
		// (add) Token: 0x0600004A RID: 74 RVA: 0x00002D48 File Offset: 0x00000F48
		// (remove) Token: 0x0600004B RID: 75 RVA: 0x00002D80 File Offset: 0x00000F80
		public event EventHandler<OnUserBannedArgs> OnUserBanned;

		// Token: 0x1400001E RID: 30
		// (add) Token: 0x0600004C RID: 76 RVA: 0x00002DB8 File Offset: 0x00000FB8
		// (remove) Token: 0x0600004D RID: 77 RVA: 0x00002DF0 File Offset: 0x00000FF0
		public event EventHandler<OnModeratorsReceivedArgs> OnModeratorsReceived;

		// Token: 0x1400001F RID: 31
		// (add) Token: 0x0600004E RID: 78 RVA: 0x00002E28 File Offset: 0x00001028
		// (remove) Token: 0x0600004F RID: 79 RVA: 0x00002E60 File Offset: 0x00001060
		public event EventHandler<OnChatColorChangedArgs> OnChatColorChanged;

		// Token: 0x14000020 RID: 32
		// (add) Token: 0x06000050 RID: 80 RVA: 0x00002E98 File Offset: 0x00001098
		// (remove) Token: 0x06000051 RID: 81 RVA: 0x00002ED0 File Offset: 0x000010D0
		public event EventHandler<OnSendReceiveDataArgs> OnSendReceiveData;

		// Token: 0x14000021 RID: 33
		// (add) Token: 0x06000052 RID: 82 RVA: 0x00002F08 File Offset: 0x00001108
		// (remove) Token: 0x06000053 RID: 83 RVA: 0x00002F40 File Offset: 0x00001140
		public event EventHandler<OnRaidNotificationArgs> OnRaidNotification;

		// Token: 0x14000022 RID: 34
		// (add) Token: 0x06000054 RID: 84 RVA: 0x00002F78 File Offset: 0x00001178
		// (remove) Token: 0x06000055 RID: 85 RVA: 0x00002FB0 File Offset: 0x000011B0
		public event EventHandler<OnGiftedSubscriptionArgs> OnGiftedSubscription;

		// Token: 0x14000023 RID: 35
		// (add) Token: 0x06000056 RID: 86 RVA: 0x00002FE8 File Offset: 0x000011E8
		// (remove) Token: 0x06000057 RID: 87 RVA: 0x00003020 File Offset: 0x00001220
		public event EventHandler<OnCommunitySubscriptionArgs> OnCommunitySubscription;

		// Token: 0x14000024 RID: 36
		// (add) Token: 0x06000058 RID: 88 RVA: 0x00003058 File Offset: 0x00001258
		// (remove) Token: 0x06000059 RID: 89 RVA: 0x00003090 File Offset: 0x00001290
		public event EventHandler<OnContinuedGiftedSubscriptionArgs> OnContinuedGiftedSubscription;

		// Token: 0x14000025 RID: 37
		// (add) Token: 0x0600005A RID: 90 RVA: 0x000030C8 File Offset: 0x000012C8
		// (remove) Token: 0x0600005B RID: 91 RVA: 0x00003100 File Offset: 0x00001300
		public event EventHandler<OnMessageThrottledEventArgs> OnMessageThrottled;

		// Token: 0x14000026 RID: 38
		// (add) Token: 0x0600005C RID: 92 RVA: 0x00003138 File Offset: 0x00001338
		// (remove) Token: 0x0600005D RID: 93 RVA: 0x00003170 File Offset: 0x00001370
		public event EventHandler<OnWhisperThrottledEventArgs> OnWhisperThrottled;

		// Token: 0x14000027 RID: 39
		// (add) Token: 0x0600005E RID: 94 RVA: 0x000031A8 File Offset: 0x000013A8
		// (remove) Token: 0x0600005F RID: 95 RVA: 0x000031E0 File Offset: 0x000013E0
		public event EventHandler<OnErrorEventArgs> OnError;

		// Token: 0x14000028 RID: 40
		// (add) Token: 0x06000060 RID: 96 RVA: 0x00003218 File Offset: 0x00001418
		// (remove) Token: 0x06000061 RID: 97 RVA: 0x00003250 File Offset: 0x00001450
		public event EventHandler<OnReconnectedEventArgs> OnReconnected;

		// Token: 0x14000029 RID: 41
		// (add) Token: 0x06000062 RID: 98 RVA: 0x00003288 File Offset: 0x00001488
		// (remove) Token: 0x06000063 RID: 99 RVA: 0x000032C0 File Offset: 0x000014C0
		public event EventHandler<OnRequiresVerifiedEmailArgs> OnRequiresVerifiedEmail;

		// Token: 0x1400002A RID: 42
		// (add) Token: 0x06000064 RID: 100 RVA: 0x000032F8 File Offset: 0x000014F8
		// (remove) Token: 0x06000065 RID: 101 RVA: 0x00003330 File Offset: 0x00001530
		public event EventHandler<OnRequiresVerifiedPhoneNumberArgs> OnRequiresVerifiedPhoneNumber;

		// Token: 0x1400002B RID: 43
		// (add) Token: 0x06000066 RID: 102 RVA: 0x00003368 File Offset: 0x00001568
		// (remove) Token: 0x06000067 RID: 103 RVA: 0x000033A0 File Offset: 0x000015A0
		public event EventHandler<OnRateLimitArgs> OnRateLimit;

		// Token: 0x1400002C RID: 44
		// (add) Token: 0x06000068 RID: 104 RVA: 0x000033D8 File Offset: 0x000015D8
		// (remove) Token: 0x06000069 RID: 105 RVA: 0x00003410 File Offset: 0x00001610
		public event EventHandler<OnDuplicateArgs> OnDuplicate;

		// Token: 0x1400002D RID: 45
		// (add) Token: 0x0600006A RID: 106 RVA: 0x00003448 File Offset: 0x00001648
		// (remove) Token: 0x0600006B RID: 107 RVA: 0x00003480 File Offset: 0x00001680
		public event EventHandler<OnBannedEmailAliasArgs> OnBannedEmailAlias;

		// Token: 0x1400002E RID: 46
		// (add) Token: 0x0600006C RID: 108 RVA: 0x000034B8 File Offset: 0x000016B8
		// (remove) Token: 0x0600006D RID: 109 RVA: 0x000034F0 File Offset: 0x000016F0
		public event EventHandler OnSelfRaidError;

		// Token: 0x1400002F RID: 47
		// (add) Token: 0x0600006E RID: 110 RVA: 0x00003528 File Offset: 0x00001728
		// (remove) Token: 0x0600006F RID: 111 RVA: 0x00003560 File Offset: 0x00001760
		public event EventHandler OnNoPermissionError;

		// Token: 0x14000030 RID: 48
		// (add) Token: 0x06000070 RID: 112 RVA: 0x00003598 File Offset: 0x00001798
		// (remove) Token: 0x06000071 RID: 113 RVA: 0x000035D0 File Offset: 0x000017D0
		public event EventHandler OnRaidedChannelIsMatureAudience;

		// Token: 0x14000031 RID: 49
		// (add) Token: 0x06000072 RID: 114 RVA: 0x00003608 File Offset: 0x00001808
		// (remove) Token: 0x06000073 RID: 115 RVA: 0x00003640 File Offset: 0x00001840
		public event EventHandler<OnFailureToReceiveJoinConfirmationArgs> OnFailureToReceiveJoinConfirmation;

		// Token: 0x14000032 RID: 50
		// (add) Token: 0x06000074 RID: 116 RVA: 0x00003678 File Offset: 0x00001878
		// (remove) Token: 0x06000075 RID: 117 RVA: 0x000036B0 File Offset: 0x000018B0
		public event EventHandler<OnFollowersOnlyArgs> OnFollowersOnly;

		// Token: 0x14000033 RID: 51
		// (add) Token: 0x06000076 RID: 118 RVA: 0x000036E8 File Offset: 0x000018E8
		// (remove) Token: 0x06000077 RID: 119 RVA: 0x00003720 File Offset: 0x00001920
		public event EventHandler<OnSubsOnlyArgs> OnSubsOnly;

		// Token: 0x14000034 RID: 52
		// (add) Token: 0x06000078 RID: 120 RVA: 0x00003758 File Offset: 0x00001958
		// (remove) Token: 0x06000079 RID: 121 RVA: 0x00003790 File Offset: 0x00001990
		public event EventHandler<OnEmoteOnlyArgs> OnEmoteOnly;

		// Token: 0x14000035 RID: 53
		// (add) Token: 0x0600007A RID: 122 RVA: 0x000037C8 File Offset: 0x000019C8
		// (remove) Token: 0x0600007B RID: 123 RVA: 0x00003800 File Offset: 0x00001A00
		public event EventHandler<OnSuspendedArgs> OnSuspended;

		// Token: 0x14000036 RID: 54
		// (add) Token: 0x0600007C RID: 124 RVA: 0x00003838 File Offset: 0x00001A38
		// (remove) Token: 0x0600007D RID: 125 RVA: 0x00003870 File Offset: 0x00001A70
		public event EventHandler<OnBannedArgs> OnBanned;

		// Token: 0x14000037 RID: 55
		// (add) Token: 0x0600007E RID: 126 RVA: 0x000038A8 File Offset: 0x00001AA8
		// (remove) Token: 0x0600007F RID: 127 RVA: 0x000038E0 File Offset: 0x00001AE0
		public event EventHandler<OnSlowModeArgs> OnSlowMode;

		// Token: 0x14000038 RID: 56
		// (add) Token: 0x06000080 RID: 128 RVA: 0x00003918 File Offset: 0x00001B18
		// (remove) Token: 0x06000081 RID: 129 RVA: 0x00003950 File Offset: 0x00001B50
		public event EventHandler<OnR9kModeArgs> OnR9kMode;

		// Token: 0x14000039 RID: 57
		// (add) Token: 0x06000082 RID: 130 RVA: 0x00003988 File Offset: 0x00001B88
		// (remove) Token: 0x06000083 RID: 131 RVA: 0x000039C0 File Offset: 0x00001BC0
		public event EventHandler<OnUserIntroArgs> OnUserIntro;

		// Token: 0x1400003A RID: 58
		// (add) Token: 0x06000084 RID: 132 RVA: 0x000039F8 File Offset: 0x00001BF8
		// (remove) Token: 0x06000085 RID: 133 RVA: 0x00003A30 File Offset: 0x00001C30
		public event EventHandler<OnUnaccountedForArgs> OnUnaccountedFor;

		// Token: 0x06000086 RID: 134 RVA: 0x00003A68 File Offset: 0x00001C68
		public TwitchClient(IClient client = null, ClientProtocol protocol = ClientProtocol.WebSocket, ILogger<TwitchClient> logger = null)
		{
			this._logger = logger;
			this._client = client;
			this._protocol = protocol;
			this._joinedChannelManager = new JoinedChannelManager();
			this._ircParser = new IrcParser();
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003ADD File Offset: 0x00001CDD
		public void Initialize(ConnectionCredentials credentials, string channel = null, char chatCommandIdentifier = '!', char whisperCommandIdentifier = '!', bool autoReListenOnExceptions = true)
		{
			if (channel != null && channel.get_Chars(0) == '#')
			{
				channel = channel.Substring(1);
			}
			List<string> list = new List<string>();
			list.Add(channel);
			this.initializeHelper(credentials, list, chatCommandIdentifier, whisperCommandIdentifier, autoReListenOnExceptions);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003B0E File Offset: 0x00001D0E
		public void Initialize(ConnectionCredentials credentials, List<string> channels, char chatCommandIdentifier = '!', char whisperCommandIdentifier = '!', bool autoReListenOnExceptions = true)
		{
			channels = Enumerable.ToList<string>(Enumerable.Select<string, string>(channels, delegate(string x)
			{
				if (x.get_Chars(0) != '#')
				{
					return x;
				}
				return x.Substring(1);
			}));
			this.initializeHelper(credentials, channels, chatCommandIdentifier, whisperCommandIdentifier, autoReListenOnExceptions);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003B4C File Offset: 0x00001D4C
		private void initializeHelper(ConnectionCredentials credentials, List<string> channels, char chatCommandIdentifier = '!', char whisperCommandIdentifier = '!', bool autoReListenOnExceptions = true)
		{
			this.Log(string.Format("TwitchLib-TwitchClient initialized, assembly version: {0}", Assembly.GetExecutingAssembly().GetName().Version), false, false);
			this.ConnectionCredentials = credentials;
			this.TwitchUsername = this.ConnectionCredentials.TwitchUsername;
			if (chatCommandIdentifier != '\0')
			{
				this._chatCommandIdentifiers.Add(chatCommandIdentifier);
			}
			if (whisperCommandIdentifier != '\0')
			{
				this._whisperCommandIdentifiers.Add(whisperCommandIdentifier);
			}
			this.AutoReListenOnException = autoReListenOnExceptions;
			if (channels != null && channels.Count > 0)
			{
				int i;
				Func<JoinedChannel, bool> <>9__0;
				int j;
				for (i = 0; i < channels.Count; i = j + 1)
				{
					if (!string.IsNullOrEmpty(channels[i]))
					{
						IEnumerable<JoinedChannel> joinedChannels = this.JoinedChannels;
						Func<JoinedChannel, bool> func;
						if ((func = <>9__0) == null)
						{
							func = (<>9__0 = (JoinedChannel x) => x.Channel.ToLower() == channels[i]);
						}
						if (Enumerable.FirstOrDefault<JoinedChannel>(joinedChannels, func) != null)
						{
							return;
						}
						this._joinChannelQueue.Enqueue(new JoinedChannel(channels[i]));
					}
					j = i;
				}
			}
			this.InitializeClient();
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003C80 File Offset: 0x00001E80
		private void InitializeClient()
		{
			if (this._client == null)
			{
				ClientProtocol protocol = this._protocol;
				if (protocol != ClientProtocol.TCP)
				{
					if (protocol == ClientProtocol.WebSocket)
					{
						this._client = new WebSocketClient(null);
					}
				}
				else
				{
					this._client = new TcpClient(null);
				}
			}
			this._client.OnConnected += new EventHandler<OnConnectedEventArgs>(this._client_OnConnected);
			this._client.OnMessage += new EventHandler<OnMessageEventArgs>(this._client_OnMessage);
			this._client.OnDisconnected += new EventHandler<OnDisconnectedEventArgs>(this._client_OnDisconnected);
			this._client.OnFatality += new EventHandler<OnFatalErrorEventArgs>(this._client_OnFatality);
			this._client.OnMessageThrottled += new EventHandler<OnMessageThrottledEventArgs>(this._client_OnMessageThrottled);
			this._client.OnWhisperThrottled += new EventHandler<OnWhisperThrottledEventArgs>(this._client_OnWhisperThrottled);
			this._client.OnReconnected += new EventHandler<OnReconnectedEventArgs>(this._client_OnReconnected);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003D60 File Offset: 0x00001F60
		internal void RaiseEvent(string eventName, object args = null)
		{
			foreach (Delegate @delegate in (base.GetType().GetField(eventName, 36).GetValue(this) as MulticastDelegate).GetInvocationList())
			{
				MethodBase method = @delegate.Method;
				object target = @delegate.Target;
				object[] array2;
				if (args != null)
				{
					object[] array = new object[2];
					array[0] = this;
					array2 = array;
					array[1] = args;
				}
				else
				{
					object[] array3 = new object[2];
					array3[0] = this;
					array2 = array3;
					array3[1] = new EventArgs();
				}
				method.Invoke(target, array2);
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003DD8 File Offset: 0x00001FD8
		public void SendRaw(string message)
		{
			if (!this.IsInitialized)
			{
				TwitchClient.HandleNotInitialized();
			}
			this.Log("Writing: " + message, false, false);
			this._client.Send(message);
			EventHandler<OnSendReceiveDataArgs> onSendReceiveData = this.OnSendReceiveData;
			if (onSendReceiveData == null)
			{
				return;
			}
			onSendReceiveData.Invoke(this, new OnSendReceiveDataArgs
			{
				Direction = SendReceiveDirection.Sent,
				Data = message
			});
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003E38 File Offset: 0x00002038
		private void SendTwitchMessage(JoinedChannel channel, string message, string replyToId = null, bool dryRun = false)
		{
			if (!this.IsInitialized)
			{
				TwitchClient.HandleNotInitialized();
			}
			if (channel == null || message == null || dryRun)
			{
				return;
			}
			if (message.Length > 500)
			{
				this.LogError("Message length has exceeded the maximum character count. (500)", false, false);
				return;
			}
			OutboundChatMessage outboundChatMessage = new OutboundChatMessage
			{
				Channel = channel.Channel,
				Username = this.ConnectionCredentials.TwitchUsername,
				Message = message
			};
			if (replyToId != null)
			{
				outboundChatMessage.ReplyToId = replyToId;
			}
			this._lastMessageSent = message;
			this._client.Send(outboundChatMessage.ToString());
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003ECA File Offset: 0x000020CA
		public void SendMessage(JoinedChannel channel, string message, bool dryRun = false)
		{
			this.SendTwitchMessage(channel, message, null, dryRun);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003ED6 File Offset: 0x000020D6
		public void SendMessage(string channel, string message, bool dryRun = false)
		{
			this.SendMessage(this.GetJoinedChannel(channel), message, dryRun);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003EE7 File Offset: 0x000020E7
		public void SendReply(JoinedChannel channel, string replyToId, string message, bool dryRun = false)
		{
			this.SendTwitchMessage(channel, message, replyToId, dryRun);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003EF4 File Offset: 0x000020F4
		public void SendReply(string channel, string replyToId, string message, bool dryRun = false)
		{
			this.SendReply(this.GetJoinedChannel(channel), replyToId, message, dryRun);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003F08 File Offset: 0x00002108
		public void SendWhisper(string receiver, string message, bool dryRun = false)
		{
			if (!this.IsInitialized)
			{
				TwitchClient.HandleNotInitialized();
			}
			if (dryRun)
			{
				return;
			}
			OutboundWhisperMessage outboundWhisperMessage = new OutboundWhisperMessage
			{
				Receiver = receiver,
				Username = this.ConnectionCredentials.TwitchUsername,
				Message = message
			};
			this._client.SendWhisper(outboundWhisperMessage.ToString());
			EventHandler<OnWhisperSentArgs> onWhisperSent = this.OnWhisperSent;
			if (onWhisperSent == null)
			{
				return;
			}
			onWhisperSent.Invoke(this, new OnWhisperSentArgs
			{
				Receiver = receiver,
				Message = message
			});
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003F84 File Offset: 0x00002184
		public bool Connect()
		{
			if (!this.IsInitialized)
			{
				TwitchClient.HandleNotInitialized();
			}
			this.Log("Connecting to: " + this.ConnectionCredentials.TwitchWebsocketURI, false, false);
			this._joinedChannelManager.Clear();
			if (this._client.Open())
			{
				this.Log("Should be connected!", false, false);
				return true;
			}
			return false;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003FE3 File Offset: 0x000021E3
		public void Disconnect()
		{
			this.Log("Disconnect Twitch Chat Client...", false, false);
			if (!this.IsInitialized)
			{
				TwitchClient.HandleNotInitialized();
			}
			this._client.Close(true);
			this._joinedChannelManager.Clear();
			this.PreviousWhisper = null;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x0000401D File Offset: 0x0000221D
		public void Reconnect()
		{
			if (!this.IsInitialized)
			{
				TwitchClient.HandleNotInitialized();
			}
			this.Log("Reconnecting to Twitch", false, false);
			this._client.Reconnect();
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00004044 File Offset: 0x00002244
		public void AddChatCommandIdentifier(char identifier)
		{
			if (!this.IsInitialized)
			{
				TwitchClient.HandleNotInitialized();
			}
			this._chatCommandIdentifiers.Add(identifier);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x0000405F File Offset: 0x0000225F
		public void RemoveChatCommandIdentifier(char identifier)
		{
			if (!this.IsInitialized)
			{
				TwitchClient.HandleNotInitialized();
			}
			this._chatCommandIdentifiers.Remove(identifier);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x0000407B File Offset: 0x0000227B
		public void AddWhisperCommandIdentifier(char identifier)
		{
			if (!this.IsInitialized)
			{
				TwitchClient.HandleNotInitialized();
			}
			this._whisperCommandIdentifiers.Add(identifier);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00004096 File Offset: 0x00002296
		public void RemoveWhisperCommandIdentifier(char identifier)
		{
			if (!this.IsInitialized)
			{
				TwitchClient.HandleNotInitialized();
			}
			this._whisperCommandIdentifiers.Remove(identifier);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000040B2 File Offset: 0x000022B2
		public void SetConnectionCredentials(ConnectionCredentials credentials)
		{
			if (!this.IsInitialized)
			{
				TwitchClient.HandleNotInitialized();
			}
			if (this.IsConnected)
			{
				throw new IllegalAssignmentException("While the client is connected, you are unable to change the connection credentials. Please disconnect first and then change them.");
			}
			this.ConnectionCredentials = credentials;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000040DC File Offset: 0x000022DC
		public void JoinChannel(string channel, bool overrideCheck = false)
		{
			if (!this.IsInitialized)
			{
				TwitchClient.HandleNotInitialized();
			}
			if (!this.IsConnected)
			{
				TwitchClient.HandleNotConnected();
			}
			if (Enumerable.FirstOrDefault<JoinedChannel>(this.JoinedChannels, (JoinedChannel x) => x.Channel.ToLower() == channel && !overrideCheck) != null)
			{
				return;
			}
			if (channel.get_Chars(0) == '#')
			{
				channel = channel.Substring(1);
			}
			this._joinChannelQueue.Enqueue(new JoinedChannel(channel));
			if (!this._currentlyJoiningChannels)
			{
				this.QueueingJoinCheck();
			}
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00004178 File Offset: 0x00002378
		public JoinedChannel GetJoinedChannel(string channel)
		{
			if (!this.IsInitialized)
			{
				TwitchClient.HandleNotInitialized();
			}
			if (this.JoinedChannels.Count == 0)
			{
				throw new BadStateException("Must be connected to at least one channel.");
			}
			if (channel.get_Chars(0) == '#')
			{
				channel = channel.Substring(1);
			}
			return this._joinedChannelManager.GetJoinedChannel(channel);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000041CC File Offset: 0x000023CC
		public void LeaveChannel(string channel)
		{
			if (!this.IsInitialized)
			{
				TwitchClient.HandleNotInitialized();
			}
			channel = channel.ToLower();
			if (channel.get_Chars(0) == '#')
			{
				channel = channel.Substring(1);
			}
			this.Log("Leaving channel: " + channel, false, false);
			if (this._joinedChannelManager.GetJoinedChannel(channel) != null)
			{
				this._client.Send(Rfc2812.Part("#" + channel));
			}
		}

		// Token: 0x0600009E RID: 158 RVA: 0x0000423F File Offset: 0x0000243F
		public void LeaveChannel(JoinedChannel channel)
		{
			if (!this.IsInitialized)
			{
				TwitchClient.HandleNotInitialized();
			}
			this.LeaveChannel(channel.Channel);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x0000425A File Offset: 0x0000245A
		public void OnReadLineTest(string rawIrc)
		{
			if (!this.IsInitialized)
			{
				TwitchClient.HandleNotInitialized();
			}
			this.HandleIrcMessage(this._ircParser.ParseIrcMessage(rawIrc));
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x0000427B File Offset: 0x0000247B
		private void _client_OnWhisperThrottled(object sender, OnWhisperThrottledEventArgs e)
		{
			EventHandler<OnWhisperThrottledEventArgs> onWhisperThrottled = this.OnWhisperThrottled;
			if (onWhisperThrottled == null)
			{
				return;
			}
			onWhisperThrottled.Invoke(sender, e);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x0000428F File Offset: 0x0000248F
		private void _client_OnMessageThrottled(object sender, OnMessageThrottledEventArgs e)
		{
			EventHandler<OnMessageThrottledEventArgs> onMessageThrottled = this.OnMessageThrottled;
			if (onMessageThrottled == null)
			{
				return;
			}
			onMessageThrottled.Invoke(sender, e);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000042A3 File Offset: 0x000024A3
		private void _client_OnFatality(object sender, OnFatalErrorEventArgs e)
		{
			EventHandler<OnConnectionErrorArgs> onConnectionError = this.OnConnectionError;
			if (onConnectionError == null)
			{
				return;
			}
			onConnectionError.Invoke(this, new OnConnectionErrorArgs
			{
				BotUsername = this.TwitchUsername,
				Error = new ErrorEvent
				{
					Message = e.Reason
				}
			});
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000042DE File Offset: 0x000024DE
		private void _client_OnDisconnected(object sender, OnDisconnectedEventArgs e)
		{
			EventHandler<OnDisconnectedEventArgs> onDisconnected = this.OnDisconnected;
			if (onDisconnected == null)
			{
				return;
			}
			onDisconnected.Invoke(sender, e);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000042F4 File Offset: 0x000024F4
		private void _client_OnReconnected(object sender, OnReconnectedEventArgs e)
		{
			foreach (JoinedChannel joinedChannel in this._joinedChannelManager.GetJoinedChannels())
			{
				if (!string.Equals(joinedChannel.Channel, this.TwitchUsername, 1))
				{
					this._joinChannelQueue.Enqueue(joinedChannel);
				}
			}
			this._joinedChannelManager.Clear();
			EventHandler<OnReconnectedEventArgs> onReconnected = this.OnReconnected;
			if (onReconnected == null)
			{
				return;
			}
			onReconnected.Invoke(sender, e);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x0000437C File Offset: 0x0000257C
		private void _client_OnMessage(object sender, OnMessageEventArgs e)
		{
			string[] array = new string[] { "\r\n" };
			foreach (string text in e.Message.Split(array, 0))
			{
				if (text.Length > 1)
				{
					this.Log("Received: " + text, false, false);
					EventHandler<OnSendReceiveDataArgs> onSendReceiveData = this.OnSendReceiveData;
					if (onSendReceiveData != null)
					{
						onSendReceiveData.Invoke(this, new OnSendReceiveDataArgs
						{
							Direction = SendReceiveDirection.Received,
							Data = text
						});
					}
					this.HandleIrcMessage(this._ircParser.ParseIrcMessage(text));
				}
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x0000440C File Offset: 0x0000260C
		private void _client_OnConnected(object sender, object e)
		{
			this._client.Send(Rfc2812.Pass(this.ConnectionCredentials.TwitchOAuth));
			this._client.Send(Rfc2812.Nick(this.ConnectionCredentials.TwitchUsername));
			this._client.Send(Rfc2812.User(this.ConnectionCredentials.TwitchUsername, 0, this.ConnectionCredentials.TwitchUsername));
			if (this.ConnectionCredentials.Capabilities.Membership)
			{
				this._client.Send("CAP REQ twitch.tv/membership");
			}
			if (this.ConnectionCredentials.Capabilities.Commands)
			{
				this._client.Send("CAP REQ twitch.tv/commands");
			}
			if (this.ConnectionCredentials.Capabilities.Tags)
			{
				this._client.Send("CAP REQ twitch.tv/tags");
			}
			if (this._joinChannelQueue != null && this._joinChannelQueue.Count > 0)
			{
				this.QueueingJoinCheck();
			}
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00004500 File Offset: 0x00002700
		private void QueueingJoinCheck()
		{
			if (this._joinChannelQueue.Count > 0)
			{
				this._currentlyJoiningChannels = true;
				JoinedChannel joinedChannel = this._joinChannelQueue.Dequeue();
				this.Log("Joining channel: " + joinedChannel.Channel, false, false);
				this._client.Send(Rfc2812.Join("#" + joinedChannel.Channel.ToLower()));
				this._joinedChannelManager.AddJoinedChannel(new JoinedChannel(joinedChannel.Channel));
				this.StartJoinedChannelTimer(joinedChannel.Channel);
				return;
			}
			this.Log("Finished channel joining queue.", false, false);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x0000459C File Offset: 0x0000279C
		private void StartJoinedChannelTimer(string channel)
		{
			if (this._joinTimer == null)
			{
				this._joinTimer = new Timer(1000.0);
				this._joinTimer.Elapsed += new ElapsedEventHandler(this.JoinChannelTimeout);
				this._awaitingJoins = new List<KeyValuePair<string, DateTime>>();
			}
			this._awaitingJoins.Add(new KeyValuePair<string, DateTime>(channel.ToLower(), DateTime.Now));
			if (!this._joinTimer.Enabled)
			{
				this._joinTimer.Start();
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x0000461C File Offset: 0x0000281C
		private void JoinChannelTimeout(object sender, ElapsedEventArgs e)
		{
			if (Enumerable.Any<KeyValuePair<string, DateTime>>(this._awaitingJoins))
			{
				List<KeyValuePair<string, DateTime>> list = Enumerable.ToList<KeyValuePair<string, DateTime>>(Enumerable.Where<KeyValuePair<string, DateTime>>(this._awaitingJoins, (KeyValuePair<string, DateTime> x) => (DateTime.Now - x.Value).TotalSeconds > 5.0));
				if (!Enumerable.Any<KeyValuePair<string, DateTime>>(list))
				{
					return;
				}
				this._awaitingJoins.RemoveAll((KeyValuePair<string, DateTime> x) => (DateTime.Now - x.Value).TotalSeconds > 5.0);
				using (List<KeyValuePair<string, DateTime>>.Enumerator enumerator = list.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<string, DateTime> keyValuePair = enumerator.Current;
						this._joinedChannelManager.RemoveJoinedChannel(keyValuePair.Key.ToLowerInvariant());
						EventHandler<OnFailureToReceiveJoinConfirmationArgs> onFailureToReceiveJoinConfirmation = this.OnFailureToReceiveJoinConfirmation;
						if (onFailureToReceiveJoinConfirmation != null)
						{
							onFailureToReceiveJoinConfirmation.Invoke(this, new OnFailureToReceiveJoinConfirmationArgs
							{
								Exception = new FailureToReceiveJoinConfirmationException(keyValuePair.Key, null)
							});
						}
					}
					return;
				}
			}
			this._joinTimer.Stop();
			this._currentlyJoiningChannels = false;
			this.QueueingJoinCheck();
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00004734 File Offset: 0x00002934
		private void HandleIrcMessage(IrcMessage ircMessage)
		{
			if (!ircMessage.Message.Contains("Login authentication failed"))
			{
				switch (ircMessage.Command)
				{
				case IrcCommand.PrivMsg:
					this.HandlePrivMsg(ircMessage);
					return;
				case IrcCommand.Notice:
					this.HandleNotice(ircMessage);
					return;
				case IrcCommand.Ping:
					if (!this.DisableAutoPong)
					{
						this.SendRaw("PONG");
					}
					return;
				case IrcCommand.Pong:
					return;
				case IrcCommand.Join:
					this.HandleJoin(ircMessage);
					return;
				case IrcCommand.Part:
					this.HandlePart(ircMessage);
					return;
				case IrcCommand.ClearChat:
					this.HandleClearChat(ircMessage);
					return;
				case IrcCommand.ClearMsg:
					this.HandleClearMsg(ircMessage);
					return;
				case IrcCommand.UserState:
					this.HandleUserState(ircMessage);
					return;
				case IrcCommand.GlobalUserState:
				case IrcCommand.RPL_001:
				case IrcCommand.RPL_002:
				case IrcCommand.RPL_003:
				case IrcCommand.RPL_372:
				case IrcCommand.RPL_375:
				case IrcCommand.RPL_376:
					return;
				case IrcCommand.Cap:
					this.HandleCap(ircMessage);
					return;
				case IrcCommand.RPL_004:
					this.Handle004();
					return;
				case IrcCommand.RPL_353:
					this.Handle353(ircMessage);
					return;
				case IrcCommand.RPL_366:
					this.Handle366();
					return;
				case IrcCommand.Whisper:
					this.HandleWhisper(ircMessage);
					return;
				case IrcCommand.RoomState:
					this.HandleRoomState(ircMessage);
					return;
				case IrcCommand.Reconnect:
					this.Reconnect();
					return;
				case IrcCommand.UserNotice:
					this.HandleUserNotice(ircMessage);
					return;
				case IrcCommand.Mode:
					this.HandleMode(ircMessage);
					return;
				}
				EventHandler<OnUnaccountedForArgs> onUnaccountedFor = this.OnUnaccountedFor;
				if (onUnaccountedFor != null)
				{
					onUnaccountedFor.Invoke(this, new OnUnaccountedForArgs
					{
						BotUsername = this.TwitchUsername,
						Channel = null,
						Location = "HandleIrcMessage",
						RawIRC = ircMessage.ToString()
					});
				}
				this.UnaccountedFor(ircMessage.ToString());
				return;
			}
			EventHandler<OnIncorrectLoginArgs> onIncorrectLogin = this.OnIncorrectLogin;
			if (onIncorrectLogin == null)
			{
				return;
			}
			onIncorrectLogin.Invoke(this, new OnIncorrectLoginArgs
			{
				Exception = new ErrorLoggingInException(ircMessage.ToString(), this.TwitchUsername)
			});
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000048E8 File Offset: 0x00002AE8
		private void HandlePrivMsg(IrcMessage ircMessage)
		{
			ChatMessage chatMessage = new ChatMessage(this.TwitchUsername, ircMessage, ref this._channelEmotes, this.WillReplaceEmotes);
			IEnumerable<JoinedChannel> joinedChannels = this.JoinedChannels;
			Func<JoinedChannel, bool> <>9__0;
			Func<JoinedChannel, bool> func;
			if ((func = <>9__0) == null)
			{
				func = (<>9__0 = (JoinedChannel x) => string.Equals(x.Channel, ircMessage.Channel, 3));
			}
			foreach (JoinedChannel joinedChannel in Enumerable.Where<JoinedChannel>(joinedChannels, func))
			{
				joinedChannel.HandleMessage(chatMessage);
			}
			EventHandler<OnMessageReceivedArgs> onMessageReceived = this.OnMessageReceived;
			if (onMessageReceived != null)
			{
				onMessageReceived.Invoke(this, new OnMessageReceivedArgs
				{
					ChatMessage = chatMessage
				});
			}
			string text;
			if (ircMessage.Tags.TryGetValue("msg-id", ref text) && text == "user-intro")
			{
				EventHandler<OnUserIntroArgs> onUserIntro = this.OnUserIntro;
				if (onUserIntro != null)
				{
					onUserIntro.Invoke(this, new OnUserIntroArgs
					{
						ChatMessage = chatMessage
					});
				}
			}
			if (this._chatCommandIdentifiers == null || this._chatCommandIdentifiers.Count == 0 || string.IsNullOrEmpty(chatMessage.Message) || !this._chatCommandIdentifiers.Contains(chatMessage.Message.get_Chars(0)))
			{
				return;
			}
			ChatCommand chatCommand = new ChatCommand(chatMessage);
			EventHandler<OnChatCommandReceivedArgs> onChatCommandReceived = this.OnChatCommandReceived;
			if (onChatCommandReceived == null)
			{
				return;
			}
			onChatCommandReceived.Invoke(this, new OnChatCommandReceivedArgs
			{
				Command = chatCommand
			});
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00004A48 File Offset: 0x00002C48
		private void HandleNotice(IrcMessage ircMessage)
		{
			if (!ircMessage.Message.Contains("Improperly formatted auth"))
			{
				string text;
				if (!ircMessage.Tags.TryGetValue("msg-id", ref text))
				{
					EventHandler<OnUnaccountedForArgs> onUnaccountedFor = this.OnUnaccountedFor;
					if (onUnaccountedFor != null)
					{
						onUnaccountedFor.Invoke(this, new OnUnaccountedForArgs
						{
							BotUsername = this.TwitchUsername,
							Channel = ircMessage.Channel,
							Location = "NoticeHandling",
							RawIRC = ircMessage.ToString()
						});
					}
					this.UnaccountedFor(ircMessage.ToString());
				}
				if (text != null)
				{
					uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
					if (num <= 1238003276U)
					{
						if (num <= 886780891U)
						{
							if (num <= 472134992U)
							{
								if (num != 2101108U)
								{
									if (num == 472134992U)
									{
										if (text == "msg_channel_suspended")
										{
											this._awaitingJoins.RemoveAll((KeyValuePair<string, DateTime> x) => x.Key.ToLower() == ircMessage.Channel);
											this._joinedChannelManager.RemoveJoinedChannel(ircMessage.Channel);
											this.QueueingJoinCheck();
											EventHandler<OnFailureToReceiveJoinConfirmationArgs> onFailureToReceiveJoinConfirmation = this.OnFailureToReceiveJoinConfirmation;
											if (onFailureToReceiveJoinConfirmation == null)
											{
												return;
											}
											onFailureToReceiveJoinConfirmation.Invoke(this, new OnFailureToReceiveJoinConfirmationArgs
											{
												Exception = new FailureToReceiveJoinConfirmationException(ircMessage.Channel, ircMessage.Message)
											});
											return;
										}
									}
								}
								else if (text == "msg_suspended")
								{
									EventHandler<OnSuspendedArgs> onSuspended = this.OnSuspended;
									if (onSuspended == null)
									{
										return;
									}
									onSuspended.Invoke(this, new OnSuspendedArgs
									{
										Channel = ircMessage.Channel,
										Message = ircMessage.Message
									});
									return;
								}
							}
							else if (num != 494171482U)
							{
								if (num != 765544783U)
								{
									if (num == 886780891U)
									{
										if (text == "color_changed")
										{
											EventHandler<OnChatColorChangedArgs> onChatColorChanged = this.OnChatColorChanged;
											if (onChatColorChanged == null)
											{
												return;
											}
											onChatColorChanged.Invoke(this, new OnChatColorChangedArgs
											{
												Channel = ircMessage.Channel
											});
											return;
										}
									}
								}
								else if (text == "msg_emoteonly")
								{
									EventHandler<OnEmoteOnlyArgs> onEmoteOnly = this.OnEmoteOnly;
									if (onEmoteOnly == null)
									{
										return;
									}
									onEmoteOnly.Invoke(this, new OnEmoteOnlyArgs
									{
										Channel = ircMessage.Channel,
										Message = ircMessage.Message
									});
									return;
								}
							}
							else if (text == "msg_subsonly")
							{
								EventHandler<OnSubsOnlyArgs> onSubsOnly = this.OnSubsOnly;
								if (onSubsOnly == null)
								{
									return;
								}
								onSubsOnly.Invoke(this, new OnSubsOnlyArgs
								{
									Channel = ircMessage.Channel,
									Message = ircMessage.Message
								});
								return;
							}
						}
						else if (num <= 1123387807U)
						{
							if (num != 1099195418U)
							{
								if (num == 1123387807U)
								{
									if (text == "raid_notice_mature")
									{
										EventHandler onRaidedChannelIsMatureAudience = this.OnRaidedChannelIsMatureAudience;
										if (onRaidedChannelIsMatureAudience == null)
										{
											return;
										}
										onRaidedChannelIsMatureAudience.Invoke(this, null);
										return;
									}
								}
							}
							else if (text == "room_mods")
							{
								EventHandler<OnModeratorsReceivedArgs> onModeratorsReceived = this.OnModeratorsReceived;
								if (onModeratorsReceived == null)
								{
									return;
								}
								onModeratorsReceived.Invoke(this, new OnModeratorsReceivedArgs
								{
									Channel = ircMessage.Channel,
									Moderators = Enumerable.ToList<string>(ircMessage.Message.Replace(" ", "").Split(new char[] { ':' })[1].Split(new char[] { ',' }))
								});
								return;
							}
						}
						else if (num != 1178906860U)
						{
							if (num != 1233238656U)
							{
								if (num == 1238003276U)
								{
									if (text == "msg_ratelimit")
									{
										EventHandler<OnRateLimitArgs> onRateLimit = this.OnRateLimit;
										if (onRateLimit == null)
										{
											return;
										}
										onRateLimit.Invoke(this, new OnRateLimitArgs
										{
											Channel = ircMessage.Channel,
											Message = ircMessage.Message
										});
										return;
									}
								}
							}
							else if (text == "no_permission")
							{
								EventHandler onNoPermissionError = this.OnNoPermissionError;
								if (onNoPermissionError == null)
								{
									return;
								}
								onNoPermissionError.Invoke(this, null);
								return;
							}
						}
						else if (text == "msg_followersonly")
						{
							EventHandler<OnFollowersOnlyArgs> onFollowersOnly = this.OnFollowersOnly;
							if (onFollowersOnly == null)
							{
								return;
							}
							onFollowersOnly.Invoke(this, new OnFollowersOnlyArgs
							{
								Channel = ircMessage.Channel,
								Message = ircMessage.Message
							});
							return;
						}
					}
					else if (num <= 1916945449U)
					{
						if (num <= 1772928154U)
						{
							if (num != 1537425015U)
							{
								if (num == 1772928154U)
								{
									if (text == "no_mods")
									{
										EventHandler<OnModeratorsReceivedArgs> onModeratorsReceived2 = this.OnModeratorsReceived;
										if (onModeratorsReceived2 == null)
										{
											return;
										}
										onModeratorsReceived2.Invoke(this, new OnModeratorsReceivedArgs
										{
											Channel = ircMessage.Channel,
											Moderators = new List<string>()
										});
										return;
									}
								}
							}
							else if (text == "msg_banned")
							{
								EventHandler<OnBannedArgs> onBanned = this.OnBanned;
								if (onBanned == null)
								{
									return;
								}
								onBanned.Invoke(this, new OnBannedArgs
								{
									Channel = ircMessage.Channel,
									Message = ircMessage.Message
								});
								return;
							}
						}
						else if (num != 1796599443U)
						{
							if (num != 1804490261U)
							{
								if (num == 1916945449U)
								{
									if (text == "msg_requires_verified_phone_number")
									{
										EventHandler<OnRequiresVerifiedPhoneNumberArgs> onRequiresVerifiedPhoneNumber = this.OnRequiresVerifiedPhoneNumber;
										if (onRequiresVerifiedPhoneNumber == null)
										{
											return;
										}
										onRequiresVerifiedPhoneNumber.Invoke(this, new OnRequiresVerifiedPhoneNumberArgs
										{
											Channel = ircMessage.Channel,
											Message = ircMessage.Message
										});
										return;
									}
								}
							}
							else if (text == "vips_success")
							{
								EventHandler<OnVIPsReceivedArgs> onVIPsReceived = this.OnVIPsReceived;
								if (onVIPsReceived == null)
								{
									return;
								}
								onVIPsReceived.Invoke(this, new OnVIPsReceivedArgs
								{
									Channel = ircMessage.Channel,
									VIPs = Enumerable.ToList<string>(ircMessage.Message.Replace(" ", "").Replace(".", "").Split(new char[] { ':' })[1].Split(new char[] { ',' }))
								});
								return;
							}
						}
						else if (text == "raid_error_self")
						{
							EventHandler onSelfRaidError = this.OnSelfRaidError;
							if (onSelfRaidError == null)
							{
								return;
							}
							onSelfRaidError.Invoke(this, null);
							return;
						}
					}
					else if (num <= 3609663096U)
					{
						if (num != 2182358485U)
						{
							if (num != 2674761391U)
							{
								if (num == 3609663096U)
								{
									if (text == "msg_duplicate")
									{
										EventHandler<OnDuplicateArgs> onDuplicate = this.OnDuplicate;
										if (onDuplicate == null)
										{
											return;
										}
										onDuplicate.Invoke(this, new OnDuplicateArgs
										{
											Channel = ircMessage.Channel,
											Message = ircMessage.Message
										});
										return;
									}
								}
							}
							else if (text == "msg_banned_email_alias")
							{
								EventHandler<OnBannedEmailAliasArgs> onBannedEmailAlias = this.OnBannedEmailAlias;
								if (onBannedEmailAlias == null)
								{
									return;
								}
								onBannedEmailAlias.Invoke(this, new OnBannedEmailAliasArgs
								{
									Channel = ircMessage.Channel,
									Message = ircMessage.Message
								});
								return;
							}
						}
						else if (text == "msg_slowmode")
						{
							EventHandler<OnSlowModeArgs> onSlowMode = this.OnSlowMode;
							if (onSlowMode == null)
							{
								return;
							}
							onSlowMode.Invoke(this, new OnSlowModeArgs
							{
								Channel = ircMessage.Channel,
								Message = ircMessage.Message
							});
							return;
						}
					}
					else if (num != 3706078324U)
					{
						if (num != 4285928011U)
						{
							if (num == 4288818883U)
							{
								if (text == "no_vips")
								{
									EventHandler<OnVIPsReceivedArgs> onVIPsReceived2 = this.OnVIPsReceived;
									if (onVIPsReceived2 == null)
									{
										return;
									}
									onVIPsReceived2.Invoke(this, new OnVIPsReceivedArgs
									{
										Channel = ircMessage.Channel,
										VIPs = new List<string>()
									});
									return;
								}
							}
						}
						else if (text == "msg_r9k")
						{
							EventHandler<OnR9kModeArgs> onR9kMode = this.OnR9kMode;
							if (onR9kMode == null)
							{
								return;
							}
							onR9kMode.Invoke(this, new OnR9kModeArgs
							{
								Channel = ircMessage.Channel,
								Message = ircMessage.Message
							});
							return;
						}
					}
					else if (text == "msg_verified_email")
					{
						EventHandler<OnRequiresVerifiedEmailArgs> onRequiresVerifiedEmail = this.OnRequiresVerifiedEmail;
						if (onRequiresVerifiedEmail == null)
						{
							return;
						}
						onRequiresVerifiedEmail.Invoke(this, new OnRequiresVerifiedEmailArgs
						{
							Channel = ircMessage.Channel,
							Message = ircMessage.Message
						});
						return;
					}
				}
				EventHandler<OnUnaccountedForArgs> onUnaccountedFor2 = this.OnUnaccountedFor;
				if (onUnaccountedFor2 != null)
				{
					onUnaccountedFor2.Invoke(this, new OnUnaccountedForArgs
					{
						BotUsername = this.TwitchUsername,
						Channel = ircMessage.Channel,
						Location = "NoticeHandling",
						RawIRC = ircMessage.ToString()
					});
				}
				this.UnaccountedFor(ircMessage.ToString());
				return;
			}
			EventHandler<OnIncorrectLoginArgs> onIncorrectLogin = this.OnIncorrectLogin;
			if (onIncorrectLogin == null)
			{
				return;
			}
			onIncorrectLogin.Invoke(this, new OnIncorrectLoginArgs
			{
				Exception = new ErrorLoggingInException(ircMessage.ToString(), this.TwitchUsername)
			});
		}

		// Token: 0x060000AD RID: 173 RVA: 0x0000536B File Offset: 0x0000356B
		private void HandleJoin(IrcMessage ircMessage)
		{
			EventHandler<OnUserJoinedArgs> onUserJoined = this.OnUserJoined;
			if (onUserJoined == null)
			{
				return;
			}
			onUserJoined.Invoke(this, new OnUserJoinedArgs
			{
				Channel = ircMessage.Channel,
				Username = ircMessage.User
			});
		}

		// Token: 0x060000AE RID: 174 RVA: 0x0000539C File Offset: 0x0000359C
		private void HandlePart(IrcMessage ircMessage)
		{
			if (string.Equals(this.TwitchUsername, ircMessage.User, 3))
			{
				this._joinedChannelManager.RemoveJoinedChannel(ircMessage.Channel);
				this._hasSeenJoinedChannels.Remove(ircMessage.Channel);
				EventHandler<OnLeftChannelArgs> onLeftChannel = this.OnLeftChannel;
				if (onLeftChannel == null)
				{
					return;
				}
				onLeftChannel.Invoke(this, new OnLeftChannelArgs
				{
					BotUsername = this.TwitchUsername,
					Channel = ircMessage.Channel
				});
				return;
			}
			else
			{
				EventHandler<OnUserLeftArgs> onUserLeft = this.OnUserLeft;
				if (onUserLeft == null)
				{
					return;
				}
				onUserLeft.Invoke(this, new OnUserLeftArgs
				{
					Channel = ircMessage.Channel,
					Username = ircMessage.User
				});
				return;
			}
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00005440 File Offset: 0x00003640
		private void HandleClearChat(IrcMessage ircMessage)
		{
			string text;
			if (string.IsNullOrWhiteSpace(ircMessage.Message))
			{
				EventHandler<OnChatClearedArgs> onChatCleared = this.OnChatCleared;
				if (onChatCleared == null)
				{
					return;
				}
				onChatCleared.Invoke(this, new OnChatClearedArgs
				{
					Channel = ircMessage.Channel
				});
				return;
			}
			else if (ircMessage.Tags.TryGetValue("ban-duration", ref text))
			{
				UserTimeout userTimeout = new UserTimeout(ircMessage);
				EventHandler<OnUserTimedoutArgs> onUserTimedout = this.OnUserTimedout;
				if (onUserTimedout == null)
				{
					return;
				}
				onUserTimedout.Invoke(this, new OnUserTimedoutArgs
				{
					UserTimeout = userTimeout
				});
				return;
			}
			else
			{
				UserBan userBan = new UserBan(ircMessage);
				EventHandler<OnUserBannedArgs> onUserBanned = this.OnUserBanned;
				if (onUserBanned == null)
				{
					return;
				}
				onUserBanned.Invoke(this, new OnUserBannedArgs
				{
					UserBan = userBan
				});
				return;
			}
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000054DC File Offset: 0x000036DC
		private void HandleClearMsg(IrcMessage ircMessage)
		{
			EventHandler<OnMessageClearedArgs> onMessageCleared = this.OnMessageCleared;
			if (onMessageCleared == null)
			{
				return;
			}
			onMessageCleared.Invoke(this, new OnMessageClearedArgs
			{
				Channel = ircMessage.Channel,
				Message = ircMessage.Message,
				TargetMessageId = ircMessage.ToString().Split(new char[] { '=' })[3].Split(new char[] { ';' })[0],
				TmiSentTs = ircMessage.ToString().Split(new char[] { '=' })[4].Split(new char[] { ' ' })[0]
			});
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x0000557C File Offset: 0x0000377C
		private void HandleUserState(IrcMessage ircMessage)
		{
			UserState userState = new UserState(ircMessage);
			if (!this._hasSeenJoinedChannels.Contains(userState.Channel.ToLowerInvariant()))
			{
				this._hasSeenJoinedChannels.Add(userState.Channel.ToLowerInvariant());
				EventHandler<OnUserStateChangedArgs> onUserStateChanged = this.OnUserStateChanged;
				if (onUserStateChanged == null)
				{
					return;
				}
				onUserStateChanged.Invoke(this, new OnUserStateChangedArgs
				{
					UserState = userState
				});
				return;
			}
			else
			{
				EventHandler<OnMessageSentArgs> onMessageSent = this.OnMessageSent;
				if (onMessageSent == null)
				{
					return;
				}
				onMessageSent.Invoke(this, new OnMessageSentArgs
				{
					SentMessage = new SentMessage(userState, this._lastMessageSent)
				});
				return;
			}
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00005604 File Offset: 0x00003804
		private void Handle004()
		{
			EventHandler<OnConnectedArgs> onConnected = this.OnConnected;
			if (onConnected == null)
			{
				return;
			}
			onConnected.Invoke(this, new OnConnectedArgs
			{
				BotUsername = this.TwitchUsername
			});
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00005628 File Offset: 0x00003828
		private void Handle353(IrcMessage ircMessage)
		{
			EventHandler<OnExistingUsersDetectedArgs> onExistingUsersDetected = this.OnExistingUsersDetected;
			if (onExistingUsersDetected == null)
			{
				return;
			}
			onExistingUsersDetected.Invoke(this, new OnExistingUsersDetectedArgs
			{
				Channel = ircMessage.Channel,
				Users = Enumerable.ToList<string>(ircMessage.Message.Split(new char[] { ' ' }))
			});
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x0000567A File Offset: 0x0000387A
		private void Handle366()
		{
			this._currentlyJoiningChannels = false;
			this.QueueingJoinCheck();
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x0000568C File Offset: 0x0000388C
		private void HandleWhisper(IrcMessage ircMessage)
		{
			WhisperMessage whisperMessage = new WhisperMessage(ircMessage, this.TwitchUsername);
			this.PreviousWhisper = whisperMessage;
			EventHandler<OnWhisperReceivedArgs> onWhisperReceived = this.OnWhisperReceived;
			if (onWhisperReceived != null)
			{
				onWhisperReceived.Invoke(this, new OnWhisperReceivedArgs
				{
					WhisperMessage = whisperMessage
				});
			}
			if (this._whisperCommandIdentifiers == null || this._whisperCommandIdentifiers.Count == 0 || string.IsNullOrEmpty(whisperMessage.Message) || !this._whisperCommandIdentifiers.Contains(whisperMessage.Message.get_Chars(0)))
			{
				EventHandler<OnUnaccountedForArgs> onUnaccountedFor = this.OnUnaccountedFor;
				if (onUnaccountedFor != null)
				{
					onUnaccountedFor.Invoke(this, new OnUnaccountedForArgs
					{
						BotUsername = this.TwitchUsername,
						Channel = ircMessage.Channel,
						Location = "WhispergHandling",
						RawIRC = ircMessage.ToString()
					});
				}
				this.UnaccountedFor(ircMessage.ToString());
				return;
			}
			WhisperCommand whisperCommand = new WhisperCommand(whisperMessage);
			EventHandler<OnWhisperCommandReceivedArgs> onWhisperCommandReceived = this.OnWhisperCommandReceived;
			if (onWhisperCommandReceived == null)
			{
				return;
			}
			onWhisperCommandReceived.Invoke(this, new OnWhisperCommandReceivedArgs
			{
				Command = whisperCommand
			});
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00005780 File Offset: 0x00003980
		private void HandleRoomState(IrcMessage ircMessage)
		{
			if (ircMessage.Tags.Count > 2)
			{
				KeyValuePair<string, DateTime> keyValuePair = Enumerable.FirstOrDefault<KeyValuePair<string, DateTime>>(this._awaitingJoins, (KeyValuePair<string, DateTime> x) => x.Key == ircMessage.Channel);
				this._awaitingJoins.Remove(keyValuePair);
				EventHandler<OnJoinedChannelArgs> onJoinedChannel = this.OnJoinedChannel;
				if (onJoinedChannel != null)
				{
					onJoinedChannel.Invoke(this, new OnJoinedChannelArgs
					{
						BotUsername = this.TwitchUsername,
						Channel = ircMessage.Channel
					});
				}
			}
			EventHandler<OnChannelStateChangedArgs> onChannelStateChanged = this.OnChannelStateChanged;
			if (onChannelStateChanged == null)
			{
				return;
			}
			onChannelStateChanged.Invoke(this, new OnChannelStateChangedArgs
			{
				ChannelState = new ChannelState(ircMessage),
				Channel = ircMessage.Channel
			});
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00005840 File Offset: 0x00003A40
		private void HandleUserNotice(IrcMessage ircMessage)
		{
			string text;
			if (!ircMessage.Tags.TryGetValue("msg-id", ref text))
			{
				EventHandler<OnUnaccountedForArgs> onUnaccountedFor = this.OnUnaccountedFor;
				if (onUnaccountedFor != null)
				{
					onUnaccountedFor.Invoke(this, new OnUnaccountedForArgs
					{
						BotUsername = this.TwitchUsername,
						Channel = ircMessage.Channel,
						Location = "UserNoticeHandling",
						RawIRC = ircMessage.ToString()
					});
				}
				this.UnaccountedFor(ircMessage.ToString());
				return;
			}
			if (text != null)
			{
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 2476983697U)
				{
					if (num <= 1525027986U)
					{
						if (num != 137481631U)
						{
							if (num == 1525027986U)
							{
								if (text == "resub")
								{
									ReSubscriber reSubscriber = new ReSubscriber(ircMessage);
									EventHandler<OnReSubscriberArgs> onReSubscriber = this.OnReSubscriber;
									if (onReSubscriber == null)
									{
										return;
									}
									onReSubscriber.Invoke(this, new OnReSubscriberArgs
									{
										ReSubscriber = reSubscriber,
										Channel = ircMessage.Channel
									});
									return;
								}
							}
						}
						else if (text == "subgift")
						{
							GiftedSubscription giftedSubscription = new GiftedSubscription(ircMessage);
							EventHandler<OnGiftedSubscriptionArgs> onGiftedSubscription = this.OnGiftedSubscription;
							if (onGiftedSubscription == null)
							{
								return;
							}
							onGiftedSubscription.Invoke(this, new OnGiftedSubscriptionArgs
							{
								GiftedSubscription = giftedSubscription,
								Channel = ircMessage.Channel
							});
							return;
						}
					}
					else if (num != 2314189198U)
					{
						if (num == 2476983697U)
						{
							if (text == "raid")
							{
								RaidNotification raidNotification = new RaidNotification(ircMessage);
								EventHandler<OnRaidNotificationArgs> onRaidNotification = this.OnRaidNotification;
								if (onRaidNotification == null)
								{
									return;
								}
								onRaidNotification.Invoke(this, new OnRaidNotificationArgs
								{
									Channel = ircMessage.Channel,
									RaidNotification = raidNotification
								});
								return;
							}
						}
					}
					else if (text == "primepaidupgrade")
					{
						PrimePaidSubscriber primePaidSubscriber = new PrimePaidSubscriber(ircMessage);
						EventHandler<OnPrimePaidSubscriberArgs> onPrimePaidSubscriber = this.OnPrimePaidSubscriber;
						if (onPrimePaidSubscriber == null)
						{
							return;
						}
						onPrimePaidSubscriber.Invoke(this, new OnPrimePaidSubscriberArgs
						{
							PrimePaidSubscriber = primePaidSubscriber,
							Channel = ircMessage.Channel
						});
						return;
					}
				}
				else if (num <= 3085127283U)
				{
					if (num != 2709801988U)
					{
						if (num == 3085127283U)
						{
							if (text == "giftpaidupgrade")
							{
								ContinuedGiftedSubscription continuedGiftedSubscription = new ContinuedGiftedSubscription(ircMessage);
								EventHandler<OnContinuedGiftedSubscriptionArgs> onContinuedGiftedSubscription = this.OnContinuedGiftedSubscription;
								if (onContinuedGiftedSubscription == null)
								{
									return;
								}
								onContinuedGiftedSubscription.Invoke(this, new OnContinuedGiftedSubscriptionArgs
								{
									ContinuedGiftedSubscription = continuedGiftedSubscription,
									Channel = ircMessage.Channel
								});
								return;
							}
						}
					}
					else if (text == "submysterygift")
					{
						CommunitySubscription communitySubscription = new CommunitySubscription(ircMessage);
						EventHandler<OnCommunitySubscriptionArgs> onCommunitySubscription = this.OnCommunitySubscription;
						if (onCommunitySubscription == null)
						{
							return;
						}
						onCommunitySubscription.Invoke(this, new OnCommunitySubscriptionArgs
						{
							GiftedSubscription = communitySubscription,
							Channel = ircMessage.Channel
						});
						return;
					}
				}
				else if (num != 3696113941U)
				{
					if (num == 4131980058U)
					{
						if (text == "announcement")
						{
							Announcement announcement = new Announcement(ircMessage);
							EventHandler<OnAnnouncementArgs> onAnnouncement = this.OnAnnouncement;
							if (onAnnouncement == null)
							{
								return;
							}
							onAnnouncement.Invoke(this, new OnAnnouncementArgs
							{
								Announcement = announcement,
								Channel = ircMessage.Channel
							});
							return;
						}
					}
				}
				else if (text == "sub")
				{
					Subscriber subscriber = new Subscriber(ircMessage);
					EventHandler<OnNewSubscriberArgs> onNewSubscriber = this.OnNewSubscriber;
					if (onNewSubscriber == null)
					{
						return;
					}
					onNewSubscriber.Invoke(this, new OnNewSubscriberArgs
					{
						Subscriber = subscriber,
						Channel = ircMessage.Channel
					});
					return;
				}
			}
			EventHandler<OnUnaccountedForArgs> onUnaccountedFor2 = this.OnUnaccountedFor;
			if (onUnaccountedFor2 != null)
			{
				onUnaccountedFor2.Invoke(this, new OnUnaccountedForArgs
				{
					BotUsername = this.TwitchUsername,
					Channel = ircMessage.Channel,
					Location = "UserNoticeHandling",
					RawIRC = ircMessage.ToString()
				});
			}
			this.UnaccountedFor(ircMessage.ToString());
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00005BD4 File Offset: 0x00003DD4
		private void HandleMode(IrcMessage ircMessage)
		{
			if (!ircMessage.Message.StartsWith("+o"))
			{
				if (ircMessage.Message.StartsWith("-o"))
				{
					EventHandler<OnModeratorLeftArgs> onModeratorLeft = this.OnModeratorLeft;
					if (onModeratorLeft == null)
					{
						return;
					}
					onModeratorLeft.Invoke(this, new OnModeratorLeftArgs
					{
						Channel = ircMessage.Channel,
						Username = ircMessage.Message.Split(new char[] { ' ' })[1]
					});
				}
				return;
			}
			EventHandler<OnModeratorJoinedArgs> onModeratorJoined = this.OnModeratorJoined;
			if (onModeratorJoined == null)
			{
				return;
			}
			onModeratorJoined.Invoke(this, new OnModeratorJoinedArgs
			{
				Channel = ircMessage.Channel,
				Username = ircMessage.Message.Split(new char[] { ' ' })[1]
			});
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00005C8A File Offset: 0x00003E8A
		private void HandleCap(IrcMessage ircMessage)
		{
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00005C8C File Offset: 0x00003E8C
		private void UnaccountedFor(string ircString)
		{
			this.Log("Unaccounted for: " + ircString + " (please create a TwitchLib GitHub issue :P)", false, false);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00005CA8 File Offset: 0x00003EA8
		private void Log(string message, bool includeDate = false, bool includeTime = false)
		{
			string text;
			if (includeDate && includeTime)
			{
				text = string.Format("{0}", DateTime.UtcNow);
			}
			else if (includeDate)
			{
				text = DateTime.UtcNow.ToShortDateString() ?? "";
			}
			else
			{
				text = DateTime.UtcNow.ToShortTimeString() ?? "";
			}
			if (includeDate || includeTime)
			{
				ILogger<TwitchClient> logger = this._logger;
				if (logger != null)
				{
					logger.LogInformation(string.Format("[TwitchLib, {0} - {1}] {2}", Assembly.GetExecutingAssembly().GetName().Version, text, message), Array.Empty<object>());
				}
			}
			else
			{
				ILogger<TwitchClient> logger2 = this._logger;
				if (logger2 != null)
				{
					logger2.LogInformation(string.Format("[TwitchLib, {0}] {1}", Assembly.GetExecutingAssembly().GetName().Version, message), Array.Empty<object>());
				}
			}
			EventHandler<OnLogArgs> onLog = this.OnLog;
			if (onLog == null)
			{
				return;
			}
			OnLogArgs onLogArgs = new OnLogArgs();
			ConnectionCredentials connectionCredentials = this.ConnectionCredentials;
			onLogArgs.BotUsername = ((connectionCredentials != null) ? connectionCredentials.TwitchUsername : null);
			onLogArgs.Data = message;
			onLogArgs.DateTime = DateTime.UtcNow;
			onLog.Invoke(this, onLogArgs);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00005DAC File Offset: 0x00003FAC
		private void LogError(string message, bool includeDate = false, bool includeTime = false)
		{
			string text;
			if (includeDate && includeTime)
			{
				text = string.Format("{0}", DateTime.UtcNow);
			}
			else if (includeDate)
			{
				text = DateTime.UtcNow.ToShortDateString() ?? "";
			}
			else
			{
				text = DateTime.UtcNow.ToShortTimeString() ?? "";
			}
			if (includeDate || includeTime)
			{
				ILogger<TwitchClient> logger = this._logger;
				if (logger != null)
				{
					logger.LogError(string.Format("[TwitchLib, {0} - {1}] {2}", Assembly.GetExecutingAssembly().GetName().Version, text, message), Array.Empty<object>());
				}
			}
			else
			{
				ILogger<TwitchClient> logger2 = this._logger;
				if (logger2 != null)
				{
					logger2.LogError(string.Format("[TwitchLib, {0}] {1}", Assembly.GetExecutingAssembly().GetName().Version, message), Array.Empty<object>());
				}
			}
			EventHandler<OnLogArgs> onLog = this.OnLog;
			if (onLog == null)
			{
				return;
			}
			OnLogArgs onLogArgs = new OnLogArgs();
			ConnectionCredentials connectionCredentials = this.ConnectionCredentials;
			onLogArgs.BotUsername = ((connectionCredentials != null) ? connectionCredentials.TwitchUsername : null);
			onLogArgs.Data = message;
			onLogArgs.DateTime = DateTime.UtcNow;
			onLog.Invoke(this, onLogArgs);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00005EB0 File Offset: 0x000040B0
		public void SendQueuedItem(string message)
		{
			if (!this.IsInitialized)
			{
				TwitchClient.HandleNotInitialized();
			}
			this._client.Send(message);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00005ECC File Offset: 0x000040CC
		protected static void HandleNotInitialized()
		{
			throw new ClientNotInitializedException("The twitch client has not been initialized and cannot be used. Please call Initialize();");
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00005ED8 File Offset: 0x000040D8
		protected static void HandleNotConnected()
		{
			throw new ClientNotConnectedException("In order to perform this action, the client must be connected to Twitch. To confirm connection, try performing this action in or after the OnConnected event has been fired.");
		}

		// Token: 0x04000001 RID: 1
		private IClient _client;

		// Token: 0x04000002 RID: 2
		private MessageEmoteCollection _channelEmotes = new MessageEmoteCollection();

		// Token: 0x04000003 RID: 3
		private readonly ICollection<char> _chatCommandIdentifiers = new HashSet<char>();

		// Token: 0x04000004 RID: 4
		private readonly ICollection<char> _whisperCommandIdentifiers = new HashSet<char>();

		// Token: 0x04000005 RID: 5
		private readonly Queue<JoinedChannel> _joinChannelQueue = new Queue<JoinedChannel>();

		// Token: 0x04000006 RID: 6
		private readonly ILogger<TwitchClient> _logger;

		// Token: 0x04000007 RID: 7
		private readonly ClientProtocol _protocol;

		// Token: 0x04000008 RID: 8
		private bool _currentlyJoiningChannels;

		// Token: 0x04000009 RID: 9
		private Timer _joinTimer;

		// Token: 0x0400000A RID: 10
		private List<KeyValuePair<string, DateTime>> _awaitingJoins;

		// Token: 0x0400000B RID: 11
		private readonly IrcParser _ircParser;

		// Token: 0x0400000C RID: 12
		private readonly JoinedChannelManager _joinedChannelManager;

		// Token: 0x0400000D RID: 13
		private readonly List<string> _hasSeenJoinedChannels = new List<string>();

		// Token: 0x0400000E RID: 14
		private string _lastMessageSent;
	}
}
