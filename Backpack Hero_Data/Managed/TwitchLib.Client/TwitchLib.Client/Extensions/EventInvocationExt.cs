using System;
using System.Collections.Generic;
using System.Drawing;
using TwitchLib.Client.Enums;
using TwitchLib.Client.Events;
using TwitchLib.Client.Exceptions;
using TwitchLib.Client.Models;

namespace TwitchLib.Client.Extensions
{
	// Token: 0x0200000E RID: 14
	public static class EventInvocationExt
	{
		// Token: 0x060001BB RID: 443 RVA: 0x00007314 File Offset: 0x00005514
		public static void InvokeChannelStateChanged(this TwitchClient client, string channel, bool r9k, bool rituals, bool subOnly, int slowMode, bool emoteOnly, string broadcasterLanguage, TimeSpan followersOnly, bool mercury, string roomId)
		{
			ChannelState channelState = new ChannelState(r9k, rituals, subOnly, slowMode, emoteOnly, broadcasterLanguage, channel, followersOnly, mercury, roomId);
			OnChannelStateChangedArgs onChannelStateChangedArgs = new OnChannelStateChangedArgs
			{
				Channel = channel,
				ChannelState = channelState
			};
			client.RaiseEvent("OnChannelStateChanged", onChannelStateChangedArgs);
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00007358 File Offset: 0x00005558
		public static void InvokeChatCleared(this TwitchClient client, string channel)
		{
			OnChatClearedArgs onChatClearedArgs = new OnChatClearedArgs
			{
				Channel = channel
			};
			client.RaiseEvent("OnChatCleared", onChatClearedArgs);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00007380 File Offset: 0x00005580
		public static void InvokeChatCommandsReceived(this TwitchClient client, string botUsername, string userId, string userName, string displayName, string colorHex, Color color, EmoteSet emoteSet, string message, UserType userType, string channel, string id, bool isSubscriber, int subscribedMonthCount, string roomId, bool isTurbo, bool isModerator, bool isMe, bool isBroadcaster, bool isVip, bool isPartner, bool isStaff, Noisy noisy, string rawIrcMessage, string emoteReplacedMessage, List<KeyValuePair<string, string>> badges, CheerBadge cheerBadge, int bits, double bitsInDollars, string commandText, string argumentsAsString, List<string> argumentsAsList, char commandIdentifier)
		{
			ChatMessage chatMessage = new ChatMessage(botUsername, userId, userName, displayName, colorHex, color, emoteSet, message, userType, channel, id, isSubscriber, subscribedMonthCount, roomId, isTurbo, isModerator, isMe, isBroadcaster, isVip, isPartner, isStaff, noisy, rawIrcMessage, emoteReplacedMessage, badges, cheerBadge, bits, bitsInDollars);
			OnChatCommandReceivedArgs onChatCommandReceivedArgs = new OnChatCommandReceivedArgs
			{
				Command = new ChatCommand(chatMessage, commandText, argumentsAsString, argumentsAsList, commandIdentifier)
			};
			client.RaiseEvent("OnChatCommandReceived", onChatCommandReceivedArgs);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x000073F0 File Offset: 0x000055F0
		public static void InvokeConnected(this TwitchClient client, string autoJoinChannel, string botUsername)
		{
			OnConnectedArgs onConnectedArgs = new OnConnectedArgs
			{
				AutoJoinChannel = autoJoinChannel,
				BotUsername = botUsername
			};
			client.RaiseEvent("OnConnected", onConnectedArgs);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00007420 File Offset: 0x00005620
		public static void InvokeConnectionError(this TwitchClient client, string botUsername, ErrorEvent errorEvent)
		{
			OnConnectionErrorArgs onConnectionErrorArgs = new OnConnectionErrorArgs
			{
				BotUsername = botUsername,
				Error = errorEvent
			};
			client.RaiseEvent("OnConnectionError", onConnectionErrorArgs);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00007450 File Offset: 0x00005650
		public static void InvokeDisconnected(this TwitchClient client, string botUsername)
		{
			OnDisconnectedArgs onDisconnectedArgs = new OnDisconnectedArgs
			{
				BotUsername = botUsername
			};
			client.RaiseEvent("OnDisconnected", onDisconnectedArgs);
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00007478 File Offset: 0x00005678
		public static void InvokeExistingUsersDetected(this TwitchClient client, string channel, List<string> users)
		{
			OnExistingUsersDetectedArgs onExistingUsersDetectedArgs = new OnExistingUsersDetectedArgs
			{
				Channel = channel,
				Users = users
			};
			client.RaiseEvent("OnExistingUsersDetected", onExistingUsersDetectedArgs);
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x000074A8 File Offset: 0x000056A8
		public static void InvokeGiftedSubscription(this TwitchClient client, List<KeyValuePair<string, string>> badges, List<KeyValuePair<string, string>> badgeInfo, string color, string displayName, string emotes, string id, string login, bool isModerator, string msgId, string msgParamMonths, string msgParamRecipientDisplayName, string msgParamRecipientId, string msgParamRecipientUserName, string msgParamSubPlanName, string msgMultiMonthGiftDuration, SubscriptionPlan msgParamSubPlan, string roomId, bool isSubscriber, string systemMsg, string systemMsgParsed, string tmiSentTs, bool isTurbo, UserType userType, string userId)
		{
			OnGiftedSubscriptionArgs onGiftedSubscriptionArgs = new OnGiftedSubscriptionArgs
			{
				GiftedSubscription = new GiftedSubscription(badges, badgeInfo, color, displayName, emotes, id, login, isModerator, msgId, msgParamMonths, msgParamRecipientDisplayName, msgParamRecipientId, msgParamRecipientUserName, msgParamSubPlanName, msgMultiMonthGiftDuration, msgParamSubPlan, roomId, isSubscriber, systemMsg, systemMsgParsed, tmiSentTs, isTurbo, userType, userId)
			};
			client.RaiseEvent("OnGiftedSubscription", onGiftedSubscriptionArgs);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00007500 File Offset: 0x00005700
		public static void InvokeIncorrectLogin(this TwitchClient client, ErrorLoggingInException ex)
		{
			OnIncorrectLoginArgs onIncorrectLoginArgs = new OnIncorrectLoginArgs
			{
				Exception = ex
			};
			client.RaiseEvent("OnIncorrectLogin", onIncorrectLoginArgs);
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00007528 File Offset: 0x00005728
		public static void InvokeJoinedChannel(this TwitchClient client, string botUsername, string channel)
		{
			OnJoinedChannelArgs onJoinedChannelArgs = new OnJoinedChannelArgs
			{
				BotUsername = botUsername,
				Channel = channel
			};
			client.RaiseEvent("OnJoinedChannel", onJoinedChannelArgs);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00007558 File Offset: 0x00005758
		public static void InvokeLeftChannel(this TwitchClient client, string botUsername, string channel)
		{
			OnLeftChannelArgs onLeftChannelArgs = new OnLeftChannelArgs
			{
				BotUsername = botUsername,
				Channel = channel
			};
			client.RaiseEvent("OnLeftChannel", onLeftChannelArgs);
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00007588 File Offset: 0x00005788
		public static void InvokeLog(this TwitchClient client, string botUsername, string data, DateTime dateTime)
		{
			OnLogArgs onLogArgs = new OnLogArgs
			{
				BotUsername = botUsername,
				Data = data,
				DateTime = dateTime
			};
			client.RaiseEvent("OnLog", onLogArgs);
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x000075BC File Offset: 0x000057BC
		public static void InvokeMessageReceived(this TwitchClient client, string botUsername, string userId, string userName, string displayName, string colorHex, Color color, EmoteSet emoteSet, string message, UserType userType, string channel, string id, bool isSubscriber, int subscribedMonthCount, string roomId, bool isTurbo, bool isModerator, bool isMe, bool isBroadcaster, bool isVip, bool isPartner, bool isStaff, Noisy noisy, string rawIrcMessage, string emoteReplacedMessage, List<KeyValuePair<string, string>> badges, CheerBadge cheerBadge, int bits, double bitsInDollars)
		{
			OnMessageReceivedArgs onMessageReceivedArgs = new OnMessageReceivedArgs
			{
				ChatMessage = new ChatMessage(botUsername, userId, userName, displayName, colorHex, color, emoteSet, message, userType, channel, id, isSubscriber, subscribedMonthCount, roomId, isTurbo, isModerator, isMe, isBroadcaster, isVip, isPartner, isStaff, noisy, rawIrcMessage, emoteReplacedMessage, badges, cheerBadge, bits, bitsInDollars)
			};
			client.RaiseEvent("OnMessageReceived", onMessageReceivedArgs);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000761C File Offset: 0x0000581C
		public static void InvokeMessageSent(this TwitchClient client, List<KeyValuePair<string, string>> badges, string channel, string colorHex, string displayName, string emoteSet, bool isModerator, bool isSubscriber, UserType userType, string message)
		{
			OnMessageSentArgs onMessageSentArgs = new OnMessageSentArgs
			{
				SentMessage = new SentMessage(badges, channel, colorHex, displayName, emoteSet, isModerator, isSubscriber, userType, message)
			};
			client.RaiseEvent("OnMessageSent", onMessageSentArgs);
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00007658 File Offset: 0x00005858
		public static void InvokeModeratorJoined(this TwitchClient client, string channel, string username)
		{
			OnModeratorJoinedArgs onModeratorJoinedArgs = new OnModeratorJoinedArgs
			{
				Channel = channel,
				Username = username
			};
			client.RaiseEvent("OnModeratorJoined", onModeratorJoinedArgs);
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00007688 File Offset: 0x00005888
		public static void InvokeModeratorLeft(this TwitchClient client, string channel, string username)
		{
			OnModeratorLeftArgs onModeratorLeftArgs = new OnModeratorLeftArgs
			{
				Channel = channel,
				Username = username
			};
			client.RaiseEvent("OnModeratorLeft", onModeratorLeftArgs);
		}

		// Token: 0x060001CB RID: 459 RVA: 0x000076B8 File Offset: 0x000058B8
		public static void InvokeModeratorsReceived(this TwitchClient client, string channel, List<string> moderators)
		{
			OnModeratorsReceivedArgs onModeratorsReceivedArgs = new OnModeratorsReceivedArgs
			{
				Channel = channel,
				Moderators = moderators
			};
			client.RaiseEvent("OnModeratorsReceived", onModeratorsReceivedArgs);
		}

		// Token: 0x060001CC RID: 460 RVA: 0x000076E8 File Offset: 0x000058E8
		public static void InvokeNewSubscriber(this TwitchClient client, List<KeyValuePair<string, string>> badges, List<KeyValuePair<string, string>> badgeInfo, string colorHex, Color color, string displayName, string emoteSet, string id, string login, string systemMessage, string msgId, string msgParamCumulativeMonths, string msgParamStreakMonths, bool msgParamShouldShareStreak, string systemMessageParsed, string resubMessage, SubscriptionPlan subscriptionPlan, string subscriptionPlanName, string roomId, string userId, bool isModerator, bool isTurbo, bool isSubscriber, bool isPartner, string tmiSentTs, UserType userType, string rawIrc, string channel)
		{
			OnNewSubscriberArgs onNewSubscriberArgs = new OnNewSubscriberArgs
			{
				Subscriber = new Subscriber(badges, badgeInfo, colorHex, color, displayName, emoteSet, id, login, systemMessage, msgId, msgParamCumulativeMonths, msgParamStreakMonths, msgParamShouldShareStreak, systemMessageParsed, resubMessage, subscriptionPlan, subscriptionPlanName, roomId, userId, isModerator, isTurbo, isSubscriber, isPartner, tmiSentTs, userType, rawIrc, channel)
			};
			client.RaiseEvent("OnNewSubscriber", onNewSubscriberArgs);
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00007748 File Offset: 0x00005948
		public static void InvokeRaidNotification(this TwitchClient client, string channel, List<KeyValuePair<string, string>> badges, List<KeyValuePair<string, string>> badgeInfo, string color, string displayName, string emotes, string id, string login, bool moderator, string msgId, string msgParamDisplayName, string msgParamLogin, string msgParamViewerCount, string roomId, bool subscriber, string systemMsg, string systemMsgParsed, string tmiSentTs, bool turbo, UserType userType, string userId)
		{
			OnRaidNotificationArgs onRaidNotificationArgs = new OnRaidNotificationArgs
			{
				Channel = channel,
				RaidNotification = new RaidNotification(badges, badgeInfo, color, displayName, emotes, id, login, moderator, msgId, msgParamDisplayName, msgParamLogin, msgParamViewerCount, roomId, subscriber, systemMsg, systemMsgParsed, tmiSentTs, turbo, userType, userId)
			};
			client.RaiseEvent("OnRaidNotification", onRaidNotificationArgs);
		}

		// Token: 0x060001CE RID: 462 RVA: 0x000077A0 File Offset: 0x000059A0
		public static void InvokeReSubscriber(this TwitchClient client, List<KeyValuePair<string, string>> badges, List<KeyValuePair<string, string>> badgeInfo, string colorHex, Color color, string displayName, string emoteSet, string id, string login, string systemMessage, string msgId, string msgParamCumulativeMonths, string msgParamStreakMonths, bool msgParamShouldShareStreak, string systemMessageParsed, string resubMessage, SubscriptionPlan subscriptionPlan, string subscriptionPlanName, string roomId, string userId, bool isModerator, bool isTurbo, bool isSubscriber, bool isPartner, string tmiSentTs, UserType userType, string rawIrc, string channel)
		{
			OnReSubscriberArgs onReSubscriberArgs = new OnReSubscriberArgs
			{
				ReSubscriber = new ReSubscriber(badges, badgeInfo, colorHex, color, displayName, emoteSet, id, login, systemMessage, msgId, msgParamCumulativeMonths, msgParamStreakMonths, msgParamShouldShareStreak, systemMessageParsed, resubMessage, subscriptionPlan, subscriptionPlanName, roomId, userId, isModerator, isTurbo, isSubscriber, isPartner, tmiSentTs, userType, rawIrc, channel, 0)
			};
			client.RaiseEvent("OnReSubscriber", onReSubscriberArgs);
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00007800 File Offset: 0x00005A00
		public static void InvokeSendReceiveData(this TwitchClient client, string data, SendReceiveDirection direction)
		{
			OnSendReceiveDataArgs onSendReceiveDataArgs = new OnSendReceiveDataArgs
			{
				Data = data,
				Direction = direction
			};
			client.RaiseEvent("OnSendReceiveData", onSendReceiveDataArgs);
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00007830 File Offset: 0x00005A30
		public static void InvokeUserBanned(this TwitchClient client, string channel, string username, string banReason, string roomId, string targetUserId)
		{
			OnUserBannedArgs onUserBannedArgs = new OnUserBannedArgs
			{
				UserBan = new UserBan(channel, username, banReason, roomId, targetUserId)
			};
			client.RaiseEvent("OnUserBanned", onUserBannedArgs);
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00007864 File Offset: 0x00005A64
		public static void InvokeUserJoined(this TwitchClient client, string channel, string username)
		{
			OnUserJoinedArgs onUserJoinedArgs = new OnUserJoinedArgs
			{
				Channel = channel,
				Username = username
			};
			client.RaiseEvent("OnUserJoined", onUserJoinedArgs);
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00007894 File Offset: 0x00005A94
		public static void InvokeUserLeft(this TwitchClient client, string channel, string username)
		{
			OnUserLeftArgs onUserLeftArgs = new OnUserLeftArgs
			{
				Channel = channel,
				Username = username
			};
			client.RaiseEvent("OnUserLeft", onUserLeftArgs);
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x000078C4 File Offset: 0x00005AC4
		public static void InvokeUserStateChanged(this TwitchClient client, List<KeyValuePair<string, string>> badges, List<KeyValuePair<string, string>> badgeInfo, string colorHex, string displayName, string emoteSet, string channel, string id, bool isSubscriber, bool isModerator, UserType userType)
		{
			OnUserStateChangedArgs onUserStateChangedArgs = new OnUserStateChangedArgs
			{
				UserState = new UserState(badges, badgeInfo, colorHex, displayName, emoteSet, channel, id, isSubscriber, isModerator, userType)
			};
			client.RaiseEvent("OnUserStateChanged", onUserStateChangedArgs);
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00007900 File Offset: 0x00005B00
		public static void InvokeUserTimedout(this TwitchClient client, string channel, string username, string targetUserId, int timeoutDuration, string timeoutReason)
		{
			OnUserTimedoutArgs onUserTimedoutArgs = new OnUserTimedoutArgs
			{
				UserTimeout = new UserTimeout(channel, username, targetUserId, timeoutDuration, timeoutReason)
			};
			client.RaiseEvent("OnUserTimedout", onUserTimedoutArgs);
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00007934 File Offset: 0x00005B34
		public static void InvokeWhisperCommandReceived(this TwitchClient client, List<KeyValuePair<string, string>> badges, string colorHex, Color color, string username, string displayName, EmoteSet emoteSet, string threadId, string messageId, string userId, bool isTurbo, string botUsername, string message, UserType userType, string commandText, string argumentsAsString, List<string> argumentsAsList, char commandIdentifier)
		{
			WhisperMessage whisperMessage = new WhisperMessage(badges, colorHex, color, username, displayName, emoteSet, threadId, messageId, userId, isTurbo, botUsername, message, userType);
			OnWhisperCommandReceivedArgs onWhisperCommandReceivedArgs = new OnWhisperCommandReceivedArgs
			{
				Command = new WhisperCommand(whisperMessage, commandText, argumentsAsString, argumentsAsList, commandIdentifier)
			};
			client.RaiseEvent("OnWhisperCommandReceived", onWhisperCommandReceivedArgs);
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00007984 File Offset: 0x00005B84
		public static void InvokeWhisperReceived(this TwitchClient client, List<KeyValuePair<string, string>> badges, string colorHex, Color color, string username, string displayName, EmoteSet emoteSet, string threadId, string messageId, string userId, bool isTurbo, string botUsername, string message, UserType userType)
		{
			OnWhisperReceivedArgs onWhisperReceivedArgs = new OnWhisperReceivedArgs
			{
				WhisperMessage = new WhisperMessage(badges, colorHex, color, username, displayName, emoteSet, threadId, messageId, userId, isTurbo, botUsername, message, userType)
			};
			client.RaiseEvent("OnWhisperReceived", onWhisperReceivedArgs);
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x000079C8 File Offset: 0x00005BC8
		public static void InvokeWhisperSent(this TwitchClient client, string username, string receiver, string message)
		{
			OnWhisperSentArgs onWhisperSentArgs = new OnWhisperSentArgs
			{
				Message = message,
				Receiver = receiver,
				Username = username
			};
			client.RaiseEvent("OnWhisperSent", onWhisperSentArgs);
		}
	}
}
