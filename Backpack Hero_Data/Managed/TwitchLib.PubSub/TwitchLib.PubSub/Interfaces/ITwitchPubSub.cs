using System;
using TwitchLib.PubSub.Events;

namespace TwitchLib.PubSub.Interfaces
{
	// Token: 0x0200002C RID: 44
	public interface ITwitchPubSub
	{
		// Token: 0x1400002A RID: 42
		// (add) Token: 0x060001BA RID: 442
		// (remove) Token: 0x060001BB RID: 443
		event EventHandler<OnBanArgs> OnBan;

		// Token: 0x1400002B RID: 43
		// (add) Token: 0x060001BC RID: 444
		// (remove) Token: 0x060001BD RID: 445
		event EventHandler<OnBitsReceivedArgs> OnBitsReceived;

		// Token: 0x1400002C RID: 44
		// (add) Token: 0x060001BE RID: 446
		// (remove) Token: 0x060001BF RID: 447
		event EventHandler<OnChannelExtensionBroadcastArgs> OnChannelExtensionBroadcast;

		// Token: 0x1400002D RID: 45
		// (add) Token: 0x060001C0 RID: 448
		// (remove) Token: 0x060001C1 RID: 449
		event EventHandler<OnChannelSubscriptionArgs> OnChannelSubscription;

		// Token: 0x1400002E RID: 46
		// (add) Token: 0x060001C2 RID: 450
		// (remove) Token: 0x060001C3 RID: 451
		event EventHandler<OnClearArgs> OnClear;

		// Token: 0x1400002F RID: 47
		// (add) Token: 0x060001C4 RID: 452
		// (remove) Token: 0x060001C5 RID: 453
		event EventHandler<OnEmoteOnlyArgs> OnEmoteOnly;

		// Token: 0x14000030 RID: 48
		// (add) Token: 0x060001C6 RID: 454
		// (remove) Token: 0x060001C7 RID: 455
		event EventHandler<OnEmoteOnlyOffArgs> OnEmoteOnlyOff;

		// Token: 0x14000031 RID: 49
		// (add) Token: 0x060001C8 RID: 456
		// (remove) Token: 0x060001C9 RID: 457
		event EventHandler<OnFollowArgs> OnFollow;

		// Token: 0x14000032 RID: 50
		// (add) Token: 0x060001CA RID: 458
		// (remove) Token: 0x060001CB RID: 459
		event EventHandler<OnHostArgs> OnHost;

		// Token: 0x14000033 RID: 51
		// (add) Token: 0x060001CC RID: 460
		// (remove) Token: 0x060001CD RID: 461
		event EventHandler<OnMessageDeletedArgs> OnMessageDeleted;

		// Token: 0x14000034 RID: 52
		// (add) Token: 0x060001CE RID: 462
		// (remove) Token: 0x060001CF RID: 463
		event EventHandler<OnListenResponseArgs> OnListenResponse;

		// Token: 0x14000035 RID: 53
		// (add) Token: 0x060001D0 RID: 464
		// (remove) Token: 0x060001D1 RID: 465
		event EventHandler OnPubSubServiceClosed;

		// Token: 0x14000036 RID: 54
		// (add) Token: 0x060001D2 RID: 466
		// (remove) Token: 0x060001D3 RID: 467
		event EventHandler OnPubSubServiceConnected;

		// Token: 0x14000037 RID: 55
		// (add) Token: 0x060001D4 RID: 468
		// (remove) Token: 0x060001D5 RID: 469
		event EventHandler<OnPubSubServiceErrorArgs> OnPubSubServiceError;

		// Token: 0x14000038 RID: 56
		// (add) Token: 0x060001D6 RID: 470
		// (remove) Token: 0x060001D7 RID: 471
		event EventHandler<OnR9kBetaArgs> OnR9kBeta;

		// Token: 0x14000039 RID: 57
		// (add) Token: 0x060001D8 RID: 472
		// (remove) Token: 0x060001D9 RID: 473
		event EventHandler<OnR9kBetaOffArgs> OnR9kBetaOff;

		// Token: 0x1400003A RID: 58
		// (add) Token: 0x060001DA RID: 474
		// (remove) Token: 0x060001DB RID: 475
		event EventHandler<OnStreamDownArgs> OnStreamDown;

		// Token: 0x1400003B RID: 59
		// (add) Token: 0x060001DC RID: 476
		// (remove) Token: 0x060001DD RID: 477
		event EventHandler<OnStreamUpArgs> OnStreamUp;

		// Token: 0x1400003C RID: 60
		// (add) Token: 0x060001DE RID: 478
		// (remove) Token: 0x060001DF RID: 479
		event EventHandler<OnSubscribersOnlyArgs> OnSubscribersOnly;

		// Token: 0x1400003D RID: 61
		// (add) Token: 0x060001E0 RID: 480
		// (remove) Token: 0x060001E1 RID: 481
		event EventHandler<OnSubscribersOnlyOffArgs> OnSubscribersOnlyOff;

		// Token: 0x1400003E RID: 62
		// (add) Token: 0x060001E2 RID: 482
		// (remove) Token: 0x060001E3 RID: 483
		event EventHandler<OnTimeoutArgs> OnTimeout;

		// Token: 0x1400003F RID: 63
		// (add) Token: 0x060001E4 RID: 484
		// (remove) Token: 0x060001E5 RID: 485
		event EventHandler<OnUnbanArgs> OnUnban;

		// Token: 0x14000040 RID: 64
		// (add) Token: 0x060001E6 RID: 486
		// (remove) Token: 0x060001E7 RID: 487
		event EventHandler<OnUntimeoutArgs> OnUntimeout;

		// Token: 0x14000041 RID: 65
		// (add) Token: 0x060001E8 RID: 488
		// (remove) Token: 0x060001E9 RID: 489
		event EventHandler<OnViewCountArgs> OnViewCount;

		// Token: 0x14000042 RID: 66
		// (add) Token: 0x060001EA RID: 490
		// (remove) Token: 0x060001EB RID: 491
		event EventHandler<OnWhisperArgs> OnWhisper;

		// Token: 0x14000043 RID: 67
		// (add) Token: 0x060001EC RID: 492
		// (remove) Token: 0x060001ED RID: 493
		[Obsolete("This event fires on an undocumented/retired/obsolete topic.", false)]
		event EventHandler<OnCustomRewardCreatedArgs> OnCustomRewardCreated;

		// Token: 0x14000044 RID: 68
		// (add) Token: 0x060001EE RID: 494
		// (remove) Token: 0x060001EF RID: 495
		[Obsolete("This event fires on an undocumented/retired/obsolete topic.", false)]
		event EventHandler<OnCustomRewardUpdatedArgs> OnCustomRewardUpdated;

		// Token: 0x14000045 RID: 69
		// (add) Token: 0x060001F0 RID: 496
		// (remove) Token: 0x060001F1 RID: 497
		[Obsolete("This event fires on an undocumented/retired/obsolete topic.", false)]
		event EventHandler<OnCustomRewardDeletedArgs> OnCustomRewardDeleted;

		// Token: 0x14000046 RID: 70
		// (add) Token: 0x060001F2 RID: 498
		// (remove) Token: 0x060001F3 RID: 499
		[Obsolete("This event fires on an undocumented/retired/obsolete topic.", false)]
		event EventHandler<OnRewardRedeemedArgs> OnRewardRedeemed;

		// Token: 0x14000047 RID: 71
		// (add) Token: 0x060001F4 RID: 500
		// (remove) Token: 0x060001F5 RID: 501
		event EventHandler<OnChannelPointsRewardRedeemedArgs> OnChannelPointsRewardRedeemed;

		// Token: 0x14000048 RID: 72
		// (add) Token: 0x060001F6 RID: 502
		// (remove) Token: 0x060001F7 RID: 503
		event EventHandler<OnLeaderboardEventArgs> OnLeaderboardSubs;

		// Token: 0x14000049 RID: 73
		// (add) Token: 0x060001F8 RID: 504
		// (remove) Token: 0x060001F9 RID: 505
		event EventHandler<OnLeaderboardEventArgs> OnLeaderboardBits;

		// Token: 0x1400004A RID: 74
		// (add) Token: 0x060001FA RID: 506
		// (remove) Token: 0x060001FB RID: 507
		event EventHandler<OnRaidUpdateArgs> OnRaidUpdate;

		// Token: 0x1400004B RID: 75
		// (add) Token: 0x060001FC RID: 508
		// (remove) Token: 0x060001FD RID: 509
		event EventHandler<OnRaidUpdateV2Args> OnRaidUpdateV2;

		// Token: 0x1400004C RID: 76
		// (add) Token: 0x060001FE RID: 510
		// (remove) Token: 0x060001FF RID: 511
		event EventHandler<OnRaidGoArgs> OnRaidGo;

		// Token: 0x1400004D RID: 77
		// (add) Token: 0x06000200 RID: 512
		// (remove) Token: 0x06000201 RID: 513
		event EventHandler<OnLogArgs> OnLog;

		// Token: 0x1400004E RID: 78
		// (add) Token: 0x06000202 RID: 514
		// (remove) Token: 0x06000203 RID: 515
		event EventHandler<OnCommercialArgs> OnCommercial;

		// Token: 0x1400004F RID: 79
		// (add) Token: 0x06000204 RID: 516
		// (remove) Token: 0x06000205 RID: 517
		event EventHandler<OnPredictionArgs> OnPrediction;

		// Token: 0x06000206 RID: 518
		void Connect();

		// Token: 0x06000207 RID: 519
		void Disconnect();

		// Token: 0x06000208 RID: 520
		[Obsolete("This topic is deprecated by Twitch. Please use ListenToBitsEventsV2()", false)]
		void ListenToBitsEvents(string channelTwitchId);

		// Token: 0x06000209 RID: 521
		void ListenToChannelExtensionBroadcast(string channelId, string extensionId);

		// Token: 0x0600020A RID: 522
		void ListenToChatModeratorActions(string myTwitchId, string channelTwitchId);

		// Token: 0x0600020B RID: 523
		void ListenToFollows(string channelId);

		// Token: 0x0600020C RID: 524
		void ListenToSubscriptions(string channelId);

		// Token: 0x0600020D RID: 525
		void ListenToVideoPlayback(string channelName);

		// Token: 0x0600020E RID: 526
		void ListenToWhispers(string channelTwitchId);

		// Token: 0x0600020F RID: 527
		[Obsolete("This method listens to an undocumented/retired/obsolete topic. Consider using ListenToChannelPoints()", false)]
		void ListenToRewards(string channelTwitchId);

		// Token: 0x06000210 RID: 528
		void ListenToChannelPoints(string channelTwitchId);

		// Token: 0x06000211 RID: 529
		void ListenToLeaderboards(string channelTwitchId);

		// Token: 0x06000212 RID: 530
		void ListenToRaid(string channelTwitchId);

		// Token: 0x06000213 RID: 531
		void ListenToPredictions(string channelTwitchId);

		// Token: 0x06000214 RID: 532
		void SendTopics(string oauth = null, bool unlisten = false);

		// Token: 0x06000215 RID: 533
		void TestMessageParser(string testJsonString);
	}
}
