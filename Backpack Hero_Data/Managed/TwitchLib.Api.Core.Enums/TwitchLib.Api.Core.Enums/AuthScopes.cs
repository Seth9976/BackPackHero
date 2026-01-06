using System;

namespace TwitchLib.Api.Core.Enums
{
	// Token: 0x02000003 RID: 3
	public enum AuthScopes
	{
		// Token: 0x04000006 RID: 6
		Any,
		// Token: 0x04000007 RID: 7
		Channel_Check_Subscription,
		// Token: 0x04000008 RID: 8
		Channel_Commercial,
		// Token: 0x04000009 RID: 9
		Channel_Editor,
		// Token: 0x0400000A RID: 10
		Channel_Feed_Edit,
		// Token: 0x0400000B RID: 11
		Channel_Feed_Read,
		// Token: 0x0400000C RID: 12
		Channel_Read,
		// Token: 0x0400000D RID: 13
		Channel_Stream,
		// Token: 0x0400000E RID: 14
		Channel_Subscriptions,
		// Token: 0x0400000F RID: 15
		Chat_Read,
		// Token: 0x04000010 RID: 16
		Chat_Edit,
		// Token: 0x04000011 RID: 17
		Chat_Moderate,
		// Token: 0x04000012 RID: 18
		Collections_Edit,
		// Token: 0x04000013 RID: 19
		Communities_Edit,
		// Token: 0x04000014 RID: 20
		Communities_Moderate,
		// Token: 0x04000015 RID: 21
		User_Blocks_Edit,
		// Token: 0x04000016 RID: 22
		User_Blocks_Read,
		// Token: 0x04000017 RID: 23
		User_Follows_Edit,
		// Token: 0x04000018 RID: 24
		User_Read,
		// Token: 0x04000019 RID: 25
		User_Subscriptions,
		// Token: 0x0400001A RID: 26
		Viewing_Activity_Read,
		// Token: 0x0400001B RID: 27
		OpenId,
		// Token: 0x0400001C RID: 28
		Helix_Analytics_Read_Extensions,
		// Token: 0x0400001D RID: 29
		Helix_Analytics_Read_Games,
		// Token: 0x0400001E RID: 30
		Helix_Bits_Read,
		// Token: 0x0400001F RID: 31
		Helix_Channel_Edit_Commercial,
		// Token: 0x04000020 RID: 32
		Helix_Channel_Manage_Broadcast,
		// Token: 0x04000021 RID: 33
		Helix_Channel_Manage_Extensions,
		// Token: 0x04000022 RID: 34
		Helix_Channel_Manage_Moderators,
		// Token: 0x04000023 RID: 35
		Helix_Channel_Manage_Polls,
		// Token: 0x04000024 RID: 36
		Helix_Channel_Manage_Predictions,
		// Token: 0x04000025 RID: 37
		Helix_Channel_Manage_Redemptions,
		// Token: 0x04000026 RID: 38
		Helix_Channel_Manage_Schedule,
		// Token: 0x04000027 RID: 39
		Helix_Channel_Manage_Videos,
		// Token: 0x04000028 RID: 40
		Helix_Channel_Manage_VIPs,
		// Token: 0x04000029 RID: 41
		Helix_Channel_Read_Charity,
		// Token: 0x0400002A RID: 42
		Helix_Channel_Read_Editors,
		// Token: 0x0400002B RID: 43
		Helix_Channel_Read_Goals,
		// Token: 0x0400002C RID: 44
		Helix_Channel_Read_Hype_Train,
		// Token: 0x0400002D RID: 45
		Helix_Channel_Read_Polls,
		// Token: 0x0400002E RID: 46
		Helix_Channel_Read_Predictions,
		// Token: 0x0400002F RID: 47
		Helix_Channel_Read_Redemptions,
		// Token: 0x04000030 RID: 48
		Helix_Channel_Read_Stream_Key,
		// Token: 0x04000031 RID: 49
		Helix_Channel_Read_Subscriptions,
		// Token: 0x04000032 RID: 50
		Helix_Channel_Read_VIPs,
		// Token: 0x04000033 RID: 51
		Helix_Clips_Edit,
		// Token: 0x04000034 RID: 52
		Helix_Moderation_Read,
		// Token: 0x04000035 RID: 53
		Helix_Moderator_Manage_Banned_Users,
		// Token: 0x04000036 RID: 54
		Helix_Moderator_Manage_Blocked_Terms,
		// Token: 0x04000037 RID: 55
		Helix_Moderator_Manage_Announcements,
		// Token: 0x04000038 RID: 56
		Helix_Moderator_Manage_Automod,
		// Token: 0x04000039 RID: 57
		Helix_Moderator_Manage_Automod_Settings,
		// Token: 0x0400003A RID: 58
		Helix_moderator_Manage_Chat_Messages,
		// Token: 0x0400003B RID: 59
		Helix_Moderator_Manage_Chat_Settings,
		// Token: 0x0400003C RID: 60
		Helix_Moderator_Read_Blocked_Terms,
		// Token: 0x0400003D RID: 61
		Helix_Moderator_Read_Automod_Settings,
		// Token: 0x0400003E RID: 62
		Helix_Moderator_Read_Chat_Settings,
		// Token: 0x0400003F RID: 63
		Helix_Moderator_Read_Chatters,
		// Token: 0x04000040 RID: 64
		Helix_User_Edit,
		// Token: 0x04000041 RID: 65
		Helix_User_Edit_Broadcast,
		// Token: 0x04000042 RID: 66
		Helix_User_Edit_Follows,
		// Token: 0x04000043 RID: 67
		Helix_User_Manage_BlockedUsers,
		// Token: 0x04000044 RID: 68
		Helix_User_Manage_Chat_Color,
		// Token: 0x04000045 RID: 69
		Helix_User_Manage_Whispers,
		// Token: 0x04000046 RID: 70
		Helix_User_Read_BlockedUsers,
		// Token: 0x04000047 RID: 71
		Helix_User_Read_Broadcast,
		// Token: 0x04000048 RID: 72
		Helix_User_Read_Email,
		// Token: 0x04000049 RID: 73
		Helix_User_Read_Follows,
		// Token: 0x0400004A RID: 74
		Helix_User_Read_Subscriptions,
		// Token: 0x0400004B RID: 75
		None
	}
}
