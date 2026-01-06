using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Timers;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using TwitchLib.Communication.Clients;
using TwitchLib.Communication.Events;
using TwitchLib.Communication.Models;
using TwitchLib.PubSub.Enums;
using TwitchLib.PubSub.Events;
using TwitchLib.PubSub.Interfaces;
using TwitchLib.PubSub.Models;
using TwitchLib.PubSub.Models.Responses;
using TwitchLib.PubSub.Models.Responses.Messages;
using TwitchLib.PubSub.Models.Responses.Messages.AutomodCaughtMessage;
using TwitchLib.PubSub.Models.Responses.Messages.Redemption;
using TwitchLib.PubSub.Models.Responses.Messages.UserModerationNotifications;
using TwitchLib.PubSub.Models.Responses.Messages.UserModerationNotificationsTypes;

namespace TwitchLib.PubSub
{
	// Token: 0x02000002 RID: 2
	public class TwitchPubSub : ITwitchPubSub
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		// (remove) Token: 0x06000002 RID: 2 RVA: 0x00002088 File Offset: 0x00000288
		public event EventHandler OnPubSubServiceConnected;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		// (remove) Token: 0x06000004 RID: 4 RVA: 0x000020F8 File Offset: 0x000002F8
		public event EventHandler<OnPubSubServiceErrorArgs> OnPubSubServiceError;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000005 RID: 5 RVA: 0x00002130 File Offset: 0x00000330
		// (remove) Token: 0x06000006 RID: 6 RVA: 0x00002168 File Offset: 0x00000368
		public event EventHandler OnPubSubServiceClosed;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000007 RID: 7 RVA: 0x000021A0 File Offset: 0x000003A0
		// (remove) Token: 0x06000008 RID: 8 RVA: 0x000021D8 File Offset: 0x000003D8
		public event EventHandler<OnListenResponseArgs> OnListenResponse;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000009 RID: 9 RVA: 0x00002210 File Offset: 0x00000410
		// (remove) Token: 0x0600000A RID: 10 RVA: 0x00002248 File Offset: 0x00000448
		public event EventHandler<OnTimeoutArgs> OnTimeout;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x0600000B RID: 11 RVA: 0x00002280 File Offset: 0x00000480
		// (remove) Token: 0x0600000C RID: 12 RVA: 0x000022B8 File Offset: 0x000004B8
		public event EventHandler<OnBanArgs> OnBan;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x0600000D RID: 13 RVA: 0x000022F0 File Offset: 0x000004F0
		// (remove) Token: 0x0600000E RID: 14 RVA: 0x00002328 File Offset: 0x00000528
		public event EventHandler<OnMessageDeletedArgs> OnMessageDeleted;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x0600000F RID: 15 RVA: 0x00002360 File Offset: 0x00000560
		// (remove) Token: 0x06000010 RID: 16 RVA: 0x00002398 File Offset: 0x00000598
		public event EventHandler<OnUnbanArgs> OnUnban;

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000011 RID: 17 RVA: 0x000023D0 File Offset: 0x000005D0
		// (remove) Token: 0x06000012 RID: 18 RVA: 0x00002408 File Offset: 0x00000608
		public event EventHandler<OnUntimeoutArgs> OnUntimeout;

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06000013 RID: 19 RVA: 0x00002440 File Offset: 0x00000640
		// (remove) Token: 0x06000014 RID: 20 RVA: 0x00002478 File Offset: 0x00000678
		public event EventHandler<OnHostArgs> OnHost;

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x06000015 RID: 21 RVA: 0x000024B0 File Offset: 0x000006B0
		// (remove) Token: 0x06000016 RID: 22 RVA: 0x000024E8 File Offset: 0x000006E8
		public event EventHandler<OnSubscribersOnlyArgs> OnSubscribersOnly;

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x06000017 RID: 23 RVA: 0x00002520 File Offset: 0x00000720
		// (remove) Token: 0x06000018 RID: 24 RVA: 0x00002558 File Offset: 0x00000758
		public event EventHandler<OnSubscribersOnlyOffArgs> OnSubscribersOnlyOff;

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x06000019 RID: 25 RVA: 0x00002590 File Offset: 0x00000790
		// (remove) Token: 0x0600001A RID: 26 RVA: 0x000025C8 File Offset: 0x000007C8
		public event EventHandler<OnClearArgs> OnClear;

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x0600001B RID: 27 RVA: 0x00002600 File Offset: 0x00000800
		// (remove) Token: 0x0600001C RID: 28 RVA: 0x00002638 File Offset: 0x00000838
		public event EventHandler<OnEmoteOnlyArgs> OnEmoteOnly;

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x0600001D RID: 29 RVA: 0x00002670 File Offset: 0x00000870
		// (remove) Token: 0x0600001E RID: 30 RVA: 0x000026A8 File Offset: 0x000008A8
		public event EventHandler<OnEmoteOnlyOffArgs> OnEmoteOnlyOff;

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x0600001F RID: 31 RVA: 0x000026E0 File Offset: 0x000008E0
		// (remove) Token: 0x06000020 RID: 32 RVA: 0x00002718 File Offset: 0x00000918
		public event EventHandler<OnR9kBetaArgs> OnR9kBeta;

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x06000021 RID: 33 RVA: 0x00002750 File Offset: 0x00000950
		// (remove) Token: 0x06000022 RID: 34 RVA: 0x00002788 File Offset: 0x00000988
		public event EventHandler<OnR9kBetaOffArgs> OnR9kBetaOff;

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x06000023 RID: 35 RVA: 0x000027C0 File Offset: 0x000009C0
		// (remove) Token: 0x06000024 RID: 36 RVA: 0x000027F8 File Offset: 0x000009F8
		public event EventHandler<OnBitsReceivedArgs> OnBitsReceived;

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x06000025 RID: 37 RVA: 0x00002830 File Offset: 0x00000A30
		// (remove) Token: 0x06000026 RID: 38 RVA: 0x00002868 File Offset: 0x00000A68
		public event EventHandler<OnBitsReceivedV2Args> OnBitsReceivedV2;

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x06000027 RID: 39 RVA: 0x000028A0 File Offset: 0x00000AA0
		// (remove) Token: 0x06000028 RID: 40 RVA: 0x000028D8 File Offset: 0x00000AD8
		public event EventHandler<OnStreamUpArgs> OnStreamUp;

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x06000029 RID: 41 RVA: 0x00002910 File Offset: 0x00000B10
		// (remove) Token: 0x0600002A RID: 42 RVA: 0x00002948 File Offset: 0x00000B48
		public event EventHandler<OnStreamDownArgs> OnStreamDown;

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x0600002B RID: 43 RVA: 0x00002980 File Offset: 0x00000B80
		// (remove) Token: 0x0600002C RID: 44 RVA: 0x000029B8 File Offset: 0x00000BB8
		public event EventHandler<OnViewCountArgs> OnViewCount;

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x0600002D RID: 45 RVA: 0x000029F0 File Offset: 0x00000BF0
		// (remove) Token: 0x0600002E RID: 46 RVA: 0x00002A28 File Offset: 0x00000C28
		public event EventHandler<OnWhisperArgs> OnWhisper;

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x0600002F RID: 47 RVA: 0x00002A60 File Offset: 0x00000C60
		// (remove) Token: 0x06000030 RID: 48 RVA: 0x00002A98 File Offset: 0x00000C98
		public event EventHandler<OnChannelSubscriptionArgs> OnChannelSubscription;

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x06000031 RID: 49 RVA: 0x00002AD0 File Offset: 0x00000CD0
		// (remove) Token: 0x06000032 RID: 50 RVA: 0x00002B08 File Offset: 0x00000D08
		public event EventHandler<OnChannelExtensionBroadcastArgs> OnChannelExtensionBroadcast;

		// Token: 0x1400001A RID: 26
		// (add) Token: 0x06000033 RID: 51 RVA: 0x00002B40 File Offset: 0x00000D40
		// (remove) Token: 0x06000034 RID: 52 RVA: 0x00002B78 File Offset: 0x00000D78
		public event EventHandler<OnFollowArgs> OnFollow;

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x06000035 RID: 53 RVA: 0x00002BB0 File Offset: 0x00000DB0
		// (remove) Token: 0x06000036 RID: 54 RVA: 0x00002BE8 File Offset: 0x00000DE8
		[Obsolete("This event fires on an undocumented/retired/obsolete topic.", false)]
		public event EventHandler<OnCustomRewardCreatedArgs> OnCustomRewardCreated;

		// Token: 0x1400001C RID: 28
		// (add) Token: 0x06000037 RID: 55 RVA: 0x00002C20 File Offset: 0x00000E20
		// (remove) Token: 0x06000038 RID: 56 RVA: 0x00002C58 File Offset: 0x00000E58
		[Obsolete("This event fires on an undocumented/retired/obsolete topic.", false)]
		public event EventHandler<OnCustomRewardUpdatedArgs> OnCustomRewardUpdated;

		// Token: 0x1400001D RID: 29
		// (add) Token: 0x06000039 RID: 57 RVA: 0x00002C90 File Offset: 0x00000E90
		// (remove) Token: 0x0600003A RID: 58 RVA: 0x00002CC8 File Offset: 0x00000EC8
		[Obsolete("This event fires on an undocumented/retired/obsolete topic.", false)]
		public event EventHandler<OnCustomRewardDeletedArgs> OnCustomRewardDeleted;

		// Token: 0x1400001E RID: 30
		// (add) Token: 0x0600003B RID: 59 RVA: 0x00002D00 File Offset: 0x00000F00
		// (remove) Token: 0x0600003C RID: 60 RVA: 0x00002D38 File Offset: 0x00000F38
		[Obsolete("This event fires on an undocumented/retired/obsolete topic. Consider using OnChannelPointsRewardRedeemed", false)]
		public event EventHandler<OnRewardRedeemedArgs> OnRewardRedeemed;

		// Token: 0x1400001F RID: 31
		// (add) Token: 0x0600003D RID: 61 RVA: 0x00002D70 File Offset: 0x00000F70
		// (remove) Token: 0x0600003E RID: 62 RVA: 0x00002DA8 File Offset: 0x00000FA8
		public event EventHandler<OnChannelPointsRewardRedeemedArgs> OnChannelPointsRewardRedeemed;

		// Token: 0x14000020 RID: 32
		// (add) Token: 0x0600003F RID: 63 RVA: 0x00002DE0 File Offset: 0x00000FE0
		// (remove) Token: 0x06000040 RID: 64 RVA: 0x00002E18 File Offset: 0x00001018
		public event EventHandler<OnLeaderboardEventArgs> OnLeaderboardSubs;

		// Token: 0x14000021 RID: 33
		// (add) Token: 0x06000041 RID: 65 RVA: 0x00002E50 File Offset: 0x00001050
		// (remove) Token: 0x06000042 RID: 66 RVA: 0x00002E88 File Offset: 0x00001088
		public event EventHandler<OnLeaderboardEventArgs> OnLeaderboardBits;

		// Token: 0x14000022 RID: 34
		// (add) Token: 0x06000043 RID: 67 RVA: 0x00002EC0 File Offset: 0x000010C0
		// (remove) Token: 0x06000044 RID: 68 RVA: 0x00002EF8 File Offset: 0x000010F8
		public event EventHandler<OnRaidUpdateArgs> OnRaidUpdate;

		// Token: 0x14000023 RID: 35
		// (add) Token: 0x06000045 RID: 69 RVA: 0x00002F30 File Offset: 0x00001130
		// (remove) Token: 0x06000046 RID: 70 RVA: 0x00002F68 File Offset: 0x00001168
		public event EventHandler<OnRaidUpdateV2Args> OnRaidUpdateV2;

		// Token: 0x14000024 RID: 36
		// (add) Token: 0x06000047 RID: 71 RVA: 0x00002FA0 File Offset: 0x000011A0
		// (remove) Token: 0x06000048 RID: 72 RVA: 0x00002FD8 File Offset: 0x000011D8
		public event EventHandler<OnRaidGoArgs> OnRaidGo;

		// Token: 0x14000025 RID: 37
		// (add) Token: 0x06000049 RID: 73 RVA: 0x00003010 File Offset: 0x00001210
		// (remove) Token: 0x0600004A RID: 74 RVA: 0x00003048 File Offset: 0x00001248
		public event EventHandler<OnLogArgs> OnLog;

		// Token: 0x14000026 RID: 38
		// (add) Token: 0x0600004B RID: 75 RVA: 0x00003080 File Offset: 0x00001280
		// (remove) Token: 0x0600004C RID: 76 RVA: 0x000030B8 File Offset: 0x000012B8
		public event EventHandler<OnCommercialArgs> OnCommercial;

		// Token: 0x14000027 RID: 39
		// (add) Token: 0x0600004D RID: 77 RVA: 0x000030F0 File Offset: 0x000012F0
		// (remove) Token: 0x0600004E RID: 78 RVA: 0x00003128 File Offset: 0x00001328
		public event EventHandler<OnPredictionArgs> OnPrediction;

		// Token: 0x14000028 RID: 40
		// (add) Token: 0x0600004F RID: 79 RVA: 0x00003160 File Offset: 0x00001360
		// (remove) Token: 0x06000050 RID: 80 RVA: 0x00003198 File Offset: 0x00001398
		public event EventHandler<OnAutomodCaughtMessageArgs> OnAutomodCaughtMessage;

		// Token: 0x14000029 RID: 41
		// (add) Token: 0x06000051 RID: 81 RVA: 0x000031D0 File Offset: 0x000013D0
		// (remove) Token: 0x06000052 RID: 82 RVA: 0x00003208 File Offset: 0x00001408
		public event EventHandler<OnAutomodCaughtUserMessage> OnAutomodCaughtUserMessage;

		// Token: 0x06000053 RID: 83 RVA: 0x00003240 File Offset: 0x00001440
		public TwitchPubSub(ILogger<TwitchPubSub> logger = null)
		{
			this._logger = logger;
			ClientOptions clientOptions = new ClientOptions
			{
				ClientType = 1
			};
			this._socket = new WebSocketClient(clientOptions);
			this._socket.OnConnected += new EventHandler<OnConnectedEventArgs>(this.Socket_OnConnected);
			this._socket.OnError += new EventHandler<OnErrorEventArgs>(this.OnError);
			this._socket.OnMessage += new EventHandler<OnMessageEventArgs>(this.OnMessage);
			this._socket.OnDisconnected += new EventHandler<OnDisconnectedEventArgs>(this.Socket_OnDisconnected);
			this._pongTimer.Interval = 15000.0;
			this._pongTimer.Elapsed += new ElapsedEventHandler(this.PongTimerTick);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003340 File Offset: 0x00001540
		private void OnError(object sender, OnErrorEventArgs e)
		{
			ILogger<TwitchPubSub> logger = this._logger;
			if (logger != null)
			{
				logger.LogError(string.Format("OnError in PubSub Websocket connection occured! Exception: {0}", e.Exception), Array.Empty<object>());
			}
			EventHandler<OnPubSubServiceErrorArgs> onPubSubServiceError = this.OnPubSubServiceError;
			if (onPubSubServiceError == null)
			{
				return;
			}
			onPubSubServiceError.Invoke(this, new OnPubSubServiceErrorArgs
			{
				Exception = e.Exception
			});
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003398 File Offset: 0x00001598
		private void OnMessage(object sender, OnMessageEventArgs e)
		{
			ILogger<TwitchPubSub> logger = this._logger;
			if (logger != null)
			{
				logger.LogDebug("Received Websocket OnMessage: " + e.Message, Array.Empty<object>());
			}
			EventHandler<OnLogArgs> onLog = this.OnLog;
			if (onLog != null)
			{
				onLog.Invoke(this, new OnLogArgs
				{
					Data = e.Message
				});
			}
			this.ParseMessage(e.Message);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000033FC File Offset: 0x000015FC
		private void Socket_OnDisconnected(object sender, EventArgs e)
		{
			ILogger<TwitchPubSub> logger = this._logger;
			if (logger != null)
			{
				logger.LogWarning("PubSub Websocket connection closed", Array.Empty<object>());
			}
			this._pingTimer.Stop();
			this._pongTimer.Stop();
			EventHandler onPubSubServiceClosed = this.OnPubSubServiceClosed;
			if (onPubSubServiceClosed == null)
			{
				return;
			}
			onPubSubServiceClosed.Invoke(this, null);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x0000344C File Offset: 0x0000164C
		private void Socket_OnConnected(object sender, EventArgs e)
		{
			ILogger<TwitchPubSub> logger = this._logger;
			if (logger != null)
			{
				logger.LogInformation("PubSub Websocket connection established", Array.Empty<object>());
			}
			this._pingTimer.Interval = 180000.0;
			this._pingTimer.Elapsed += new ElapsedEventHandler(this.PingTimerTick);
			this._pingTimer.Start();
			EventHandler onPubSubServiceConnected = this.OnPubSubServiceConnected;
			if (onPubSubServiceConnected == null)
			{
				return;
			}
			onPubSubServiceConnected.Invoke(this, null);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000034BC File Offset: 0x000016BC
		private void PingTimerTick(object sender, ElapsedEventArgs e)
		{
			this._pongReceived = false;
			JObject jobject = new JObject(new JProperty("type", "PING"));
			this._socket.Send(jobject.ToString());
			this._pongTimer.Start();
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003502 File Offset: 0x00001702
		private void PongTimerTick(object sender, ElapsedEventArgs e)
		{
			this._pongTimer.Stop();
			if (this._pongReceived)
			{
				this._pongReceived = false;
				return;
			}
			this._socket.Close(true);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x0000352C File Offset: 0x0000172C
		private void ParseMessage(string message)
		{
			JToken jtoken = JObject.Parse(message).SelectToken("type");
			string text = ((jtoken != null) ? jtoken.ToString() : null);
			string text2 = ((text != null) ? text.ToLower() : null);
			if (text2 != null)
			{
				if (!(text2 == "response"))
				{
					if (!(text2 == "message"))
					{
						if (text2 == "pong")
						{
							this._pongReceived = true;
							return;
						}
						if (text2 == "reconnect")
						{
							this._socket.Close(true);
						}
					}
					else
					{
						TwitchLib.PubSub.Models.Responses.Message message2 = new TwitchLib.PubSub.Models.Responses.Message(message);
						string text3;
						this._topicToChannelId.TryGetValue(message2.Topic, ref text3);
						text3 = text3 ?? "";
						string text4 = message2.Topic.Split(new char[] { '.' })[0];
						if (text4 != null)
						{
							uint num = <PrivateImplementationDetails>.ComputeStringHash(text4);
							if (num <= 1266735245U)
							{
								if (num <= 539594034U)
								{
									if (num != 450000440U)
									{
										if (num != 522816415U)
										{
											if (num == 539594034U)
											{
												if (text4 == "channel-bits-events-v2")
												{
													ChannelBitsEventsV2 channelBitsEventsV = message2.MessageData as ChannelBitsEventsV2;
													if (channelBitsEventsV != null)
													{
														EventHandler<OnBitsReceivedV2Args> onBitsReceivedV = this.OnBitsReceivedV2;
														if (onBitsReceivedV == null)
														{
															return;
														}
														onBitsReceivedV.Invoke(this, new OnBitsReceivedV2Args
														{
															IsAnonymous = channelBitsEventsV.IsAnonymous,
															BitsUsed = channelBitsEventsV.BitsUsed,
															ChannelId = channelBitsEventsV.ChannelId,
															ChannelName = channelBitsEventsV.ChannelName,
															ChatMessage = channelBitsEventsV.ChatMessage,
															Context = channelBitsEventsV.Context,
															Time = channelBitsEventsV.Time,
															TotalBitsUsed = channelBitsEventsV.TotalBitsUsed,
															UserId = channelBitsEventsV.UserId,
															UserName = channelBitsEventsV.UserName
														});
														return;
													}
												}
											}
										}
										else if (text4 == "channel-bits-events-v1")
										{
											ChannelBitsEvents channelBitsEvents = message2.MessageData as ChannelBitsEvents;
											if (channelBitsEvents != null)
											{
												EventHandler<OnBitsReceivedArgs> onBitsReceived = this.OnBitsReceived;
												if (onBitsReceived == null)
												{
													return;
												}
												onBitsReceived.Invoke(this, new OnBitsReceivedArgs
												{
													BitsUsed = channelBitsEvents.BitsUsed,
													ChannelId = channelBitsEvents.ChannelId,
													ChannelName = channelBitsEvents.ChannelName,
													ChatMessage = channelBitsEvents.ChatMessage,
													Context = channelBitsEvents.Context,
													Time = channelBitsEvents.Time,
													TotalBitsUsed = channelBitsEvents.TotalBitsUsed,
													UserId = channelBitsEvents.UserId,
													Username = channelBitsEvents.Username
												});
												return;
											}
										}
									}
									else if (text4 == "automod-queue")
									{
										AutomodQueue automodQueue = message2.MessageData as AutomodQueue;
										AutomodQueueType type = automodQueue.Type;
										if (type != AutomodQueueType.CaughtMessage)
										{
											if (type != AutomodQueueType.Unknown)
											{
												return;
											}
											this.UnaccountedFor("Unknown automod queue type. Msg: " + automodQueue.RawData);
											return;
										}
										else
										{
											TwitchLib.PubSub.Models.Responses.Messages.AutomodCaughtMessage.AutomodCaughtMessage automodCaughtMessage = automodQueue.Data as TwitchLib.PubSub.Models.Responses.Messages.AutomodCaughtMessage.AutomodCaughtMessage;
											EventHandler<OnAutomodCaughtMessageArgs> onAutomodCaughtMessage = this.OnAutomodCaughtMessage;
											if (onAutomodCaughtMessage == null)
											{
												return;
											}
											onAutomodCaughtMessage.Invoke(this, new OnAutomodCaughtMessageArgs
											{
												ChannelId = text3,
												AutomodCaughtMessage = automodCaughtMessage
											});
											return;
										}
									}
								}
								else if (num <= 1212123455U)
								{
									if (num != 778451386U)
									{
										if (num == 1212123455U)
										{
											if (text4 == "predictions-channel-v1")
											{
												PredictionEvents predictionEvents = message2.MessageData as PredictionEvents;
												PredictionType? predictionType = ((predictionEvents != null) ? new PredictionType?(predictionEvents.Type) : default(PredictionType?));
												if (predictionType == null)
												{
													this.UnaccountedFor("Prediction Type: null");
													return;
												}
												PredictionType valueOrDefault = predictionType.GetValueOrDefault();
												if (valueOrDefault != PredictionType.EventCreated)
												{
													if (valueOrDefault != PredictionType.EventUpdated)
													{
														this.UnaccountedFor(string.Format("Prediction Type: {0}", predictionEvents.Type));
														return;
													}
													EventHandler<OnPredictionArgs> onPrediction = this.OnPrediction;
													if (onPrediction == null)
													{
														return;
													}
													onPrediction.Invoke(this, new OnPredictionArgs
													{
														CreatedAt = predictionEvents.CreatedAt,
														Title = predictionEvents.Title,
														ChannelId = predictionEvents.ChannelId,
														EndedAt = predictionEvents.EndedAt,
														Id = predictionEvents.Id,
														Outcomes = predictionEvents.Outcomes,
														LockedAt = predictionEvents.LockedAt,
														PredictionTime = predictionEvents.PredictionTime,
														Status = predictionEvents.Status,
														WinningOutcomeId = predictionEvents.WinningOutcomeId,
														Type = predictionEvents.Type
													});
													return;
												}
												else
												{
													EventHandler<OnPredictionArgs> onPrediction2 = this.OnPrediction;
													if (onPrediction2 == null)
													{
														return;
													}
													onPrediction2.Invoke(this, new OnPredictionArgs
													{
														CreatedAt = predictionEvents.CreatedAt,
														Title = predictionEvents.Title,
														ChannelId = predictionEvents.ChannelId,
														EndedAt = predictionEvents.EndedAt,
														Id = predictionEvents.Id,
														Outcomes = predictionEvents.Outcomes,
														LockedAt = predictionEvents.LockedAt,
														PredictionTime = predictionEvents.PredictionTime,
														Status = predictionEvents.Status,
														WinningOutcomeId = predictionEvents.WinningOutcomeId,
														Type = predictionEvents.Type
													});
													return;
												}
											}
										}
									}
									else if (text4 == "whispers")
									{
										Whisper whisper = (Whisper)message2.MessageData;
										EventHandler<OnWhisperArgs> onWhisper = this.OnWhisper;
										if (onWhisper == null)
										{
											return;
										}
										onWhisper.Invoke(this, new OnWhisperArgs
										{
											Whisper = whisper,
											ChannelId = text3
										});
										return;
									}
								}
								else if (num != 1248430604U)
								{
									if (num == 1266735245U)
									{
										if (text4 == "channel-subscribe-events-v1")
										{
											ChannelSubscription channelSubscription = message2.MessageData as ChannelSubscription;
											EventHandler<OnChannelSubscriptionArgs> onChannelSubscription = this.OnChannelSubscription;
											if (onChannelSubscription == null)
											{
												return;
											}
											onChannelSubscription.Invoke(this, new OnChannelSubscriptionArgs
											{
												Subscription = channelSubscription,
												ChannelId = text3
											});
											return;
										}
									}
								}
								else if (text4 == "leaderboard-events-v1")
								{
									LeaderboardEvents leaderboardEvents = message2.MessageData as LeaderboardEvents;
									LeaderBoardType? leaderBoardType = ((leaderboardEvents != null) ? new LeaderBoardType?(leaderboardEvents.Type) : default(LeaderBoardType?));
									if (leaderBoardType == null)
									{
										return;
									}
									LeaderBoardType valueOrDefault2 = leaderBoardType.GetValueOrDefault();
									if (valueOrDefault2 != LeaderBoardType.BitsUsageByChannel)
									{
										if (valueOrDefault2 != LeaderBoardType.SubGiftSent)
										{
											return;
										}
										EventHandler<OnLeaderboardEventArgs> onLeaderboardSubs = this.OnLeaderboardSubs;
										if (onLeaderboardSubs == null)
										{
											return;
										}
										onLeaderboardSubs.Invoke(this, new OnLeaderboardEventArgs
										{
											ChannelId = leaderboardEvents.ChannelId,
											TopList = leaderboardEvents.Top
										});
										return;
									}
									else
									{
										EventHandler<OnLeaderboardEventArgs> onLeaderboardBits = this.OnLeaderboardBits;
										if (onLeaderboardBits == null)
										{
											return;
										}
										onLeaderboardBits.Invoke(this, new OnLeaderboardEventArgs
										{
											ChannelId = leaderboardEvents.ChannelId,
											TopList = leaderboardEvents.Top
										});
										return;
									}
								}
							}
							else if (num <= 2476983697U)
							{
								if (num <= 2101714332U)
								{
									if (num != 1970825802U)
									{
										if (num == 2101714332U)
										{
											if (text4 == "channel-points-channel-v1")
											{
												ChannelPointsChannel channelPointsChannel = message2.MessageData as ChannelPointsChannel;
												ChannelPointsChannelType type2 = channelPointsChannel.Type;
												if (type2 != ChannelPointsChannelType.RewardRedeemed)
												{
													if (type2 != ChannelPointsChannelType.Unknown)
													{
														return;
													}
													this.UnaccountedFor("Unknown channel points type. Msg: " + channelPointsChannel.RawData);
													return;
												}
												else
												{
													RewardRedeemed rewardRedeemed = channelPointsChannel.Data as RewardRedeemed;
													EventHandler<OnChannelPointsRewardRedeemedArgs> onChannelPointsRewardRedeemed = this.OnChannelPointsRewardRedeemed;
													if (onChannelPointsRewardRedeemed == null)
													{
														return;
													}
													onChannelPointsRewardRedeemed.Invoke(this, new OnChannelPointsRewardRedeemedArgs
													{
														ChannelId = rewardRedeemed.Redemption.ChannelId,
														RewardRedeemed = rewardRedeemed
													});
													return;
												}
											}
										}
									}
									else if (text4 == "video-playback-by-id")
									{
										VideoPlayback videoPlayback = message2.MessageData as VideoPlayback;
										VideoPlaybackType? videoPlaybackType = ((videoPlayback != null) ? new VideoPlaybackType?(videoPlayback.Type) : default(VideoPlaybackType?));
										if (videoPlaybackType != null)
										{
											switch (videoPlaybackType.GetValueOrDefault())
											{
											case VideoPlaybackType.StreamUp:
											{
												EventHandler<OnStreamUpArgs> onStreamUp = this.OnStreamUp;
												if (onStreamUp == null)
												{
													return;
												}
												onStreamUp.Invoke(this, new OnStreamUpArgs
												{
													PlayDelay = videoPlayback.PlayDelay,
													ServerTime = videoPlayback.ServerTime,
													ChannelId = text3
												});
												return;
											}
											case VideoPlaybackType.StreamDown:
											{
												EventHandler<OnStreamDownArgs> onStreamDown = this.OnStreamDown;
												if (onStreamDown == null)
												{
													return;
												}
												onStreamDown.Invoke(this, new OnStreamDownArgs
												{
													ServerTime = videoPlayback.ServerTime,
													ChannelId = text3
												});
												return;
											}
											case VideoPlaybackType.ViewCount:
											{
												EventHandler<OnViewCountArgs> onViewCount = this.OnViewCount;
												if (onViewCount == null)
												{
													return;
												}
												onViewCount.Invoke(this, new OnViewCountArgs
												{
													ServerTime = videoPlayback.ServerTime,
													Viewers = videoPlayback.Viewers,
													ChannelId = text3
												});
												return;
											}
											case VideoPlaybackType.Commercial:
											{
												EventHandler<OnCommercialArgs> onCommercial = this.OnCommercial;
												if (onCommercial == null)
												{
													return;
												}
												onCommercial.Invoke(this, new OnCommercialArgs
												{
													ServerTime = videoPlayback.ServerTime,
													Length = videoPlayback.Length,
													ChannelId = text3
												});
												return;
											}
											}
										}
									}
								}
								else if (num != 2157984858U)
								{
									if (num == 2476983697U)
									{
										if (text4 == "raid")
										{
											RaidEvents raidEvents = message2.MessageData as RaidEvents;
											RaidType? raidType = ((raidEvents != null) ? new RaidType?(raidEvents.Type) : default(RaidType?));
											if (raidType == null)
											{
												return;
											}
											switch (raidType.GetValueOrDefault())
											{
											case RaidType.RaidUpdate:
											{
												EventHandler<OnRaidUpdateArgs> onRaidUpdate = this.OnRaidUpdate;
												if (onRaidUpdate == null)
												{
													return;
												}
												onRaidUpdate.Invoke(this, new OnRaidUpdateArgs
												{
													Id = raidEvents.Id,
													ChannelId = raidEvents.ChannelId,
													TargetChannelId = raidEvents.TargetChannelId,
													AnnounceTime = raidEvents.AnnounceTime,
													RaidTime = raidEvents.RaidTime,
													RemainingDurationSeconds = raidEvents.RemainigDurationSeconds,
													ViewerCount = raidEvents.ViewerCount
												});
												return;
											}
											case RaidType.RaidUpdateV2:
											{
												EventHandler<OnRaidUpdateV2Args> onRaidUpdateV = this.OnRaidUpdateV2;
												if (onRaidUpdateV == null)
												{
													return;
												}
												onRaidUpdateV.Invoke(this, new OnRaidUpdateV2Args
												{
													Id = raidEvents.Id,
													ChannelId = raidEvents.ChannelId,
													TargetChannelId = raidEvents.TargetChannelId,
													TargetLogin = raidEvents.TargetLogin,
													TargetDisplayName = raidEvents.TargetDisplayName,
													TargetProfileImage = raidEvents.TargetProfileImage,
													ViewerCount = raidEvents.ViewerCount
												});
												return;
											}
											case RaidType.RaidGo:
											{
												EventHandler<OnRaidGoArgs> onRaidGo = this.OnRaidGo;
												if (onRaidGo == null)
												{
													return;
												}
												onRaidGo.Invoke(this, new OnRaidGoArgs
												{
													Id = raidEvents.Id,
													ChannelId = raidEvents.ChannelId,
													TargetChannelId = raidEvents.TargetChannelId,
													TargetLogin = raidEvents.TargetLogin,
													TargetDisplayName = raidEvents.TargetDisplayName,
													TargetProfileImage = raidEvents.TargetProfileImage,
													ViewerCount = raidEvents.ViewerCount
												});
												return;
											}
											default:
												return;
											}
										}
									}
								}
								else if (text4 == "community-points-channel-v1")
								{
									CommunityPointsChannel communityPointsChannel = message2.MessageData as CommunityPointsChannel;
									CommunityPointsChannelType? communityPointsChannelType = ((communityPointsChannel != null) ? new CommunityPointsChannelType?(communityPointsChannel.Type) : default(CommunityPointsChannelType?));
									if (communityPointsChannelType == null)
									{
										return;
									}
									switch (communityPointsChannelType.GetValueOrDefault())
									{
									case CommunityPointsChannelType.RewardRedeemed:
									{
										EventHandler<OnRewardRedeemedArgs> onRewardRedeemed = this.OnRewardRedeemed;
										if (onRewardRedeemed == null)
										{
											return;
										}
										onRewardRedeemed.Invoke(this, new OnRewardRedeemedArgs
										{
											TimeStamp = communityPointsChannel.TimeStamp,
											ChannelId = communityPointsChannel.ChannelId,
											Login = communityPointsChannel.Login,
											DisplayName = communityPointsChannel.DisplayName,
											Message = communityPointsChannel.Message,
											RewardId = communityPointsChannel.RewardId,
											RewardTitle = communityPointsChannel.RewardTitle,
											RewardPrompt = communityPointsChannel.RewardPrompt,
											RewardCost = communityPointsChannel.RewardCost,
											Status = communityPointsChannel.Status,
											RedemptionId = communityPointsChannel.RedemptionId
										});
										return;
									}
									case CommunityPointsChannelType.CustomRewardUpdated:
									{
										EventHandler<OnCustomRewardUpdatedArgs> onCustomRewardUpdated = this.OnCustomRewardUpdated;
										if (onCustomRewardUpdated == null)
										{
											return;
										}
										onCustomRewardUpdated.Invoke(this, new OnCustomRewardUpdatedArgs
										{
											TimeStamp = communityPointsChannel.TimeStamp,
											ChannelId = communityPointsChannel.ChannelId,
											RewardId = communityPointsChannel.RewardId,
											RewardTitle = communityPointsChannel.RewardTitle,
											RewardPrompt = communityPointsChannel.RewardPrompt,
											RewardCost = communityPointsChannel.RewardCost
										});
										return;
									}
									case CommunityPointsChannelType.CustomRewardCreated:
									{
										EventHandler<OnCustomRewardCreatedArgs> onCustomRewardCreated = this.OnCustomRewardCreated;
										if (onCustomRewardCreated == null)
										{
											return;
										}
										onCustomRewardCreated.Invoke(this, new OnCustomRewardCreatedArgs
										{
											TimeStamp = communityPointsChannel.TimeStamp,
											ChannelId = communityPointsChannel.ChannelId,
											RewardId = communityPointsChannel.RewardId,
											RewardTitle = communityPointsChannel.RewardTitle,
											RewardPrompt = communityPointsChannel.RewardPrompt,
											RewardCost = communityPointsChannel.RewardCost
										});
										return;
									}
									case CommunityPointsChannelType.CustomRewardDeleted:
									{
										EventHandler<OnCustomRewardDeletedArgs> onCustomRewardDeleted = this.OnCustomRewardDeleted;
										if (onCustomRewardDeleted == null)
										{
											return;
										}
										onCustomRewardDeleted.Invoke(this, new OnCustomRewardDeletedArgs
										{
											TimeStamp = communityPointsChannel.TimeStamp,
											ChannelId = communityPointsChannel.ChannelId,
											RewardId = communityPointsChannel.RewardId,
											RewardTitle = communityPointsChannel.RewardTitle,
											RewardPrompt = communityPointsChannel.RewardPrompt
										});
										return;
									}
									default:
										return;
									}
								}
							}
							else if (num <= 2643987228U)
							{
								if (num != 2535512472U)
								{
									if (num == 2643987228U)
									{
										if (text4 == "user-moderation-notifications")
										{
											UserModerationNotifications userModerationNotifications = message2.MessageData as UserModerationNotifications;
											UserModerationNotificationsType type3 = userModerationNotifications.Type;
											if (type3 != UserModerationNotificationsType.AutomodCaughtMessage)
											{
												return;
											}
											TwitchLib.PubSub.Models.Responses.Messages.UserModerationNotificationsTypes.AutomodCaughtMessage automodCaughtMessage2 = userModerationNotifications.Data as TwitchLib.PubSub.Models.Responses.Messages.UserModerationNotificationsTypes.AutomodCaughtMessage;
											EventHandler<OnAutomodCaughtUserMessage> onAutomodCaughtUserMessage = this.OnAutomodCaughtUserMessage;
											if (onAutomodCaughtUserMessage == null)
											{
												return;
											}
											onAutomodCaughtUserMessage.Invoke(this, new OnAutomodCaughtUserMessage
											{
												ChannelId = text3,
												UserId = message2.Topic.Split(new char[] { '.' })[2],
												AutomodCaughtMessage = automodCaughtMessage2
											});
											return;
										}
									}
								}
								else if (text4 == "following")
								{
									Following following = (Following)message2.MessageData;
									following.FollowedChannelId = message2.Topic.Split(new char[] { '.' })[1];
									EventHandler<OnFollowArgs> onFollow = this.OnFollow;
									if (onFollow == null)
									{
										return;
									}
									onFollow.Invoke(this, new OnFollowArgs
									{
										FollowedChannelId = following.FollowedChannelId,
										DisplayName = following.DisplayName,
										UserId = following.UserId,
										Username = following.Username
									});
									return;
								}
							}
							else if (num != 3075833323U)
							{
								if (num == 3729941884U)
								{
									if (text4 == "channel-ext-v1")
									{
										ChannelExtensionBroadcast channelExtensionBroadcast = message2.MessageData as ChannelExtensionBroadcast;
										EventHandler<OnChannelExtensionBroadcastArgs> onChannelExtensionBroadcast = this.OnChannelExtensionBroadcast;
										if (onChannelExtensionBroadcast == null)
										{
											return;
										}
										onChannelExtensionBroadcast.Invoke(this, new OnChannelExtensionBroadcastArgs
										{
											Messages = channelExtensionBroadcast.Messages,
											ChannelId = text3
										});
										return;
									}
								}
							}
							else if (text4 == "chat_moderator_actions")
							{
								ChatModeratorActions chatModeratorActions = message2.MessageData as ChatModeratorActions;
								string text5 = "";
								string text6 = ((chatModeratorActions != null) ? chatModeratorActions.ModerationAction.ToLower() : null);
								if (text6 != null)
								{
									num = <PrivateImplementationDetails>.ComputeStringHash(text6);
									if (num <= 1914476589U)
									{
										if (num <= 1521963270U)
										{
											if (num != 1255794741U)
											{
												if (num != 1507622623U)
												{
													if (num == 1521963270U)
													{
														if (text6 == "ban")
														{
															if (chatModeratorActions.Args.Count > 1)
															{
																text5 = chatModeratorActions.Args[1];
															}
															EventHandler<OnBanArgs> onBan = this.OnBan;
															if (onBan == null)
															{
																return;
															}
															onBan.Invoke(this, new OnBanArgs
															{
																BannedBy = chatModeratorActions.CreatedBy,
																BannedByUserId = chatModeratorActions.CreatedByUserId,
																BannedUserId = chatModeratorActions.TargetUserId,
																BanReason = text5,
																BannedUser = chatModeratorActions.Args[0],
																ChannelId = text3
															});
															return;
														}
													}
												}
												else if (text6 == "r9kbeta")
												{
													EventHandler<OnR9kBetaArgs> onR9kBeta = this.OnR9kBeta;
													if (onR9kBeta == null)
													{
														return;
													}
													onR9kBeta.Invoke(this, new OnR9kBetaArgs
													{
														Moderator = chatModeratorActions.CreatedBy,
														ChannelId = text3
													});
													return;
												}
											}
											else if (text6 == "subscribersoff")
											{
												EventHandler<OnSubscribersOnlyOffArgs> onSubscribersOnlyOff = this.OnSubscribersOnlyOff;
												if (onSubscribersOnlyOff == null)
												{
													return;
												}
												onSubscribersOnlyOff.Invoke(this, new OnSubscribersOnlyOffArgs
												{
													Moderator = chatModeratorActions.CreatedBy,
													ChannelId = text3
												});
												return;
											}
										}
										else if (num != 1550717474U)
										{
											if (num != 1740784714U)
											{
												if (num == 1914476589U)
												{
													if (text6 == "untimeout")
													{
														EventHandler<OnUntimeoutArgs> onUntimeout = this.OnUntimeout;
														if (onUntimeout == null)
														{
															return;
														}
														onUntimeout.Invoke(this, new OnUntimeoutArgs
														{
															UntimeoutedBy = chatModeratorActions.CreatedBy,
															UntimeoutedByUserId = chatModeratorActions.CreatedByUserId,
															UntimeoutedUserId = chatModeratorActions.TargetUserId,
															UntimeoutedUser = chatModeratorActions.Args[0],
															ChannelId = text3
														});
														return;
													}
												}
											}
											else if (text6 == "delete")
											{
												EventHandler<OnMessageDeletedArgs> onMessageDeleted = this.OnMessageDeleted;
												if (onMessageDeleted == null)
												{
													return;
												}
												onMessageDeleted.Invoke(this, new OnMessageDeletedArgs
												{
													DeletedBy = chatModeratorActions.CreatedBy,
													DeletedByUserId = chatModeratorActions.CreatedByUserId,
													TargetUserId = chatModeratorActions.TargetUserId,
													TargetUser = chatModeratorActions.Args[0],
													Message = chatModeratorActions.Args[1],
													MessageId = chatModeratorActions.Args[2],
													ChannelId = text3
												});
												return;
											}
										}
										else if (text6 == "clear")
										{
											EventHandler<OnClearArgs> onClear = this.OnClear;
											if (onClear == null)
											{
												return;
											}
											onClear.Invoke(this, new OnClearArgs
											{
												Moderator = chatModeratorActions.CreatedBy,
												ChannelId = text3
											});
											return;
										}
									}
									else if (num <= 2549515144U)
									{
										if (num != 2037245794U)
										{
											if (num != 2135416752U)
											{
												if (num == 2549515144U)
												{
													if (text6 == "timeout")
													{
														if (chatModeratorActions.Args.Count > 2)
														{
															text5 = chatModeratorActions.Args[2];
														}
														EventHandler<OnTimeoutArgs> onTimeout = this.OnTimeout;
														if (onTimeout == null)
														{
															return;
														}
														onTimeout.Invoke(this, new OnTimeoutArgs
														{
															TimedoutBy = chatModeratorActions.CreatedBy,
															TimedoutById = chatModeratorActions.CreatedByUserId,
															TimedoutUserId = chatModeratorActions.TargetUserId,
															TimeoutDuration = TimeSpan.FromSeconds((double)int.Parse(chatModeratorActions.Args[1])),
															TimeoutReason = text5,
															TimedoutUser = chatModeratorActions.Args[0],
															ChannelId = text3
														});
														return;
													}
												}
											}
											else if (text6 == "subscribers")
											{
												EventHandler<OnSubscribersOnlyArgs> onSubscribersOnly = this.OnSubscribersOnly;
												if (onSubscribersOnly == null)
												{
													return;
												}
												onSubscribersOnly.Invoke(this, new OnSubscribersOnlyArgs
												{
													Moderator = chatModeratorActions.CreatedBy,
													ChannelId = text3
												});
												return;
											}
										}
										else if (text6 == "emoteonlyoff")
										{
											EventHandler<OnEmoteOnlyOffArgs> onEmoteOnlyOff = this.OnEmoteOnlyOff;
											if (onEmoteOnlyOff == null)
											{
												return;
											}
											onEmoteOnlyOff.Invoke(this, new OnEmoteOnlyOffArgs
											{
												Moderator = chatModeratorActions.CreatedBy,
												ChannelId = text3
											});
											return;
										}
									}
									else if (num <= 2952701295U)
									{
										if (num != 2597424669U)
										{
											if (num == 2952701295U)
											{
												if (text6 == "host")
												{
													EventHandler<OnHostArgs> onHost = this.OnHost;
													if (onHost == null)
													{
														return;
													}
													onHost.Invoke(this, new OnHostArgs
													{
														HostedChannel = chatModeratorActions.Args[0],
														Moderator = chatModeratorActions.CreatedBy,
														ChannelId = text3
													});
													return;
												}
											}
										}
										else if (text6 == "emoteonly")
										{
											EventHandler<OnEmoteOnlyArgs> onEmoteOnly = this.OnEmoteOnly;
											if (onEmoteOnly == null)
											{
												return;
											}
											onEmoteOnly.Invoke(this, new OnEmoteOnlyArgs
											{
												Moderator = chatModeratorActions.CreatedBy,
												ChannelId = text3
											});
											return;
										}
									}
									else if (num != 3741867359U)
									{
										if (num == 4182426668U)
										{
											if (text6 == "r9kbetaoff")
											{
												EventHandler<OnR9kBetaOffArgs> onR9kBetaOff = this.OnR9kBetaOff;
												if (onR9kBetaOff == null)
												{
													return;
												}
												onR9kBetaOff.Invoke(this, new OnR9kBetaOffArgs
												{
													Moderator = chatModeratorActions.CreatedBy,
													ChannelId = text3
												});
												return;
											}
										}
									}
									else if (text6 == "unban")
									{
										EventHandler<OnUnbanArgs> onUnban = this.OnUnban;
										if (onUnban == null)
										{
											return;
										}
										onUnban.Invoke(this, new OnUnbanArgs
										{
											UnbannedBy = chatModeratorActions.CreatedBy,
											UnbannedByUserId = chatModeratorActions.CreatedByUserId,
											UnbannedUserId = chatModeratorActions.TargetUserId,
											UnbannedUser = chatModeratorActions.Args[0],
											ChannelId = text3
										});
										return;
									}
								}
							}
						}
					}
				}
				else
				{
					Response response = new Response(message);
					if (this._previousRequests.Count != 0)
					{
						bool flag = false;
						this._previousRequestsSemaphore.WaitOne();
						try
						{
							int i = 0;
							while (i < this._previousRequests.Count)
							{
								PreviousRequest previousRequest = this._previousRequests[i];
								if (string.Equals(previousRequest.Nonce, response.Nonce, 0))
								{
									this._previousRequests.RemoveAt(i);
									string text7;
									this._topicToChannelId.TryGetValue(previousRequest.Topic, ref text7);
									EventHandler<OnListenResponseArgs> onListenResponse = this.OnListenResponse;
									if (onListenResponse != null)
									{
										onListenResponse.Invoke(this, new OnListenResponseArgs
										{
											Response = response,
											Topic = previousRequest.Topic,
											Successful = response.Successful,
											ChannelId = text7
										});
									}
									flag = true;
								}
								else
								{
									i++;
								}
							}
						}
						finally
						{
							this._previousRequestsSemaphore.Release();
						}
						if (flag)
						{
							return;
						}
					}
				}
			}
			this.UnaccountedFor(message);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00004A64 File Offset: 0x00002C64
		private static string GenerateNonce()
		{
			return new string(Enumerable.ToArray<char>(Enumerable.Select<string, char>(Enumerable.Repeat<string>("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 8), (string s) => s.get_Chars(TwitchPubSub.Random.Next(s.Length)))));
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00004A9F File Offset: 0x00002C9F
		private void ListenToTopic(string topic)
		{
			this._topicList.Add(topic);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00004AB0 File Offset: 0x00002CB0
		private void ListenToTopics(params string[] topics)
		{
			foreach (string text in topics)
			{
				this._topicList.Add(text);
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00004AE0 File Offset: 0x00002CE0
		public void SendTopics(string oauth = null, bool unlisten = false)
		{
			if (oauth != null && oauth.Contains("oauth:"))
			{
				oauth = oauth.Replace("oauth:", "");
			}
			string text = TwitchPubSub.GenerateNonce();
			JArray jarray = new JArray();
			this._previousRequestsSemaphore.WaitOne();
			try
			{
				foreach (string text2 in this._topicList)
				{
					this._previousRequests.Add(new PreviousRequest(text, PubSubRequestType.ListenToTopic, text2));
					jarray.Add(new JValue(text2));
				}
			}
			finally
			{
				this._previousRequestsSemaphore.Release();
			}
			JObject jobject = new JObject(new object[]
			{
				new JProperty("type", (!unlisten) ? "LISTEN" : "UNLISTEN"),
				new JProperty("nonce", text),
				new JProperty("data", new JObject(new JProperty("topics", jarray)))
			});
			if (oauth != null)
			{
				JObject jobject2 = (JObject)jobject.SelectToken("data");
				if (jobject2 != null)
				{
					jobject2.Add(new JProperty("auth_token", oauth));
				}
			}
			this._socket.Send(jobject.ToString());
			this._topicList.Clear();
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00004C3C File Offset: 0x00002E3C
		private void UnaccountedFor(string message)
		{
			ILogger<TwitchPubSub> logger = this._logger;
			if (logger == null)
			{
				return;
			}
			logger.LogInformation("[TwitchPubSub] " + message, Array.Empty<object>());
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00004C60 File Offset: 0x00002E60
		public void ListenToFollows(string channelId)
		{
			string text = "following." + channelId;
			this._topicToChannelId[text] = channelId;
			this.ListenToTopic(text);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00004C90 File Offset: 0x00002E90
		public void ListenToChatModeratorActions(string userId, string channelId)
		{
			string text = "chat_moderator_actions." + userId + "." + channelId;
			this._topicToChannelId[text] = channelId;
			this.ListenToTopic(text);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00004CC4 File Offset: 0x00002EC4
		public void ListenToUserModerationNotifications(string myTwitchId, string channelTwitchId)
		{
			string text = "user-moderation-notifications." + myTwitchId + "." + channelTwitchId;
			this._topicToChannelId[text] = channelTwitchId;
			this.ListenToTopic(text);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00004CF8 File Offset: 0x00002EF8
		public void ListenToAutomodQueue(string userTwitchId, string channelTwitchId)
		{
			string text = "automod-queue." + userTwitchId + "." + channelTwitchId;
			this._topicToChannelId[text] = channelTwitchId;
			this.ListenToTopic(text);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00004D2C File Offset: 0x00002F2C
		public void ListenToChannelExtensionBroadcast(string channelId, string extensionId)
		{
			string text = string.Concat(new string[] { "channel-ext-v1.", channelId, "-", extensionId, "-broadcast" });
			this._topicToChannelId[text] = channelId;
			this.ListenToTopic(text);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00004D7C File Offset: 0x00002F7C
		[Obsolete("This topic is deprecated by Twitch. Please use ListenToBitsEventsV2()", false)]
		public void ListenToBitsEvents(string channelTwitchId)
		{
			string text = "channel-bits-events-v1." + channelTwitchId;
			this._topicToChannelId[text] = channelTwitchId;
			this.ListenToTopic(text);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00004DAC File Offset: 0x00002FAC
		public void ListenToBitsEventsV2(string channelTwitchId)
		{
			string text = "channel-bits-events-v2." + channelTwitchId;
			this._topicToChannelId[text] = channelTwitchId;
			this.ListenToTopic(text);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00004DDC File Offset: 0x00002FDC
		public void ListenToVideoPlayback(string channelTwitchId)
		{
			string text = "video-playback-by-id." + channelTwitchId;
			this._topicToChannelId[text] = channelTwitchId;
			this.ListenToTopic(text);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00004E0C File Offset: 0x0000300C
		public void ListenToWhispers(string channelTwitchId)
		{
			string text = "whispers." + channelTwitchId;
			this._topicToChannelId[text] = channelTwitchId;
			this.ListenToTopic(text);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00004E3C File Offset: 0x0000303C
		[Obsolete("This method listens to an undocumented/retired/obsolete topic. Consider using ListenToChannelPoints()", false)]
		public void ListenToRewards(string channelTwitchId)
		{
			string text = "community-points-channel-v1." + channelTwitchId;
			this._topicToChannelId[text] = channelTwitchId;
			this.ListenToTopic(text);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00004E6C File Offset: 0x0000306C
		public void ListenToChannelPoints(string channelTwitchId)
		{
			string text = "channel-points-channel-v1." + channelTwitchId;
			this._topicToChannelId[text] = channelTwitchId;
			this.ListenToTopic(text);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00004E9C File Offset: 0x0000309C
		public void ListenToLeaderboards(string channelTwitchId)
		{
			string text = "leaderboard-events-v1.bits-usage-by-channel-v1-" + channelTwitchId + "-WEEK";
			string text2 = "leaderboard-events-v1.sub-gift-sent-" + channelTwitchId + "-WEEK";
			this._topicToChannelId[text] = channelTwitchId;
			this._topicToChannelId[text2] = channelTwitchId;
			this.ListenToTopics(new string[] { text, text2 });
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00004EFC File Offset: 0x000030FC
		public void ListenToRaid(string channelTwitchId)
		{
			string text = "raid." + channelTwitchId;
			this._topicToChannelId[text] = channelTwitchId;
			this.ListenToTopic(text);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00004F2C File Offset: 0x0000312C
		public void ListenToSubscriptions(string channelId)
		{
			string text = "channel-subscribe-events-v1." + channelId;
			this._topicToChannelId[text] = channelId;
			this.ListenToTopic(text);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00004F5C File Offset: 0x0000315C
		public void ListenToPredictions(string channelTwitchId)
		{
			string text = "predictions-channel-v1." + channelTwitchId;
			this._topicToChannelId[text] = channelTwitchId;
			this.ListenToTopic(text);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00004F89 File Offset: 0x00003189
		public void Connect()
		{
			this._socket.Open();
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00004F97 File Offset: 0x00003197
		public void Disconnect()
		{
			this._socket.Close(true);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00004FA5 File Offset: 0x000031A5
		public void TestMessageParser(string testJsonString)
		{
			this.ParseMessage(testJsonString);
		}

		// Token: 0x04000001 RID: 1
		private readonly WebSocketClient _socket;

		// Token: 0x04000002 RID: 2
		private readonly List<PreviousRequest> _previousRequests = new List<PreviousRequest>();

		// Token: 0x04000003 RID: 3
		private readonly Semaphore _previousRequestsSemaphore = new Semaphore(1, 1);

		// Token: 0x04000004 RID: 4
		private readonly ILogger<TwitchPubSub> _logger;

		// Token: 0x04000005 RID: 5
		private readonly Timer _pingTimer = new Timer();

		// Token: 0x04000006 RID: 6
		private readonly Timer _pongTimer = new Timer();

		// Token: 0x04000007 RID: 7
		private bool _pongReceived;

		// Token: 0x04000008 RID: 8
		private readonly List<string> _topicList = new List<string>();

		// Token: 0x04000009 RID: 9
		private readonly Dictionary<string, string> _topicToChannelId = new Dictionary<string, string>();

		// Token: 0x04000033 RID: 51
		private static readonly Random Random = new Random();
	}
}
