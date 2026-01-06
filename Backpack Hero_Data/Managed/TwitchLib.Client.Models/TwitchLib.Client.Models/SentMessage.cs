using System;
using System.Collections.Generic;
using TwitchLib.Client.Enums;

namespace TwitchLib.Client.Models
{
	// Token: 0x0200001B RID: 27
	public class SentMessage
	{
		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600010D RID: 269 RVA: 0x00005FF8 File Offset: 0x000041F8
		public List<KeyValuePair<string, string>> Badges { get; }

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600010E RID: 270 RVA: 0x00006000 File Offset: 0x00004200
		public string Channel { get; }

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00006008 File Offset: 0x00004208
		public string ColorHex { get; }

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000110 RID: 272 RVA: 0x00006010 File Offset: 0x00004210
		public string DisplayName { get; }

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00006018 File Offset: 0x00004218
		public string EmoteSet { get; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000112 RID: 274 RVA: 0x00006020 File Offset: 0x00004220
		public bool IsModerator { get; }

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00006028 File Offset: 0x00004228
		public bool IsSubscriber { get; }

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000114 RID: 276 RVA: 0x00006030 File Offset: 0x00004230
		public string Message { get; }

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000115 RID: 277 RVA: 0x00006038 File Offset: 0x00004238
		public UserType UserType { get; }

		// Token: 0x06000116 RID: 278 RVA: 0x00006040 File Offset: 0x00004240
		public SentMessage(UserState state, string message)
		{
			this.Badges = state.Badges;
			this.Channel = state.Channel;
			this.ColorHex = state.ColorHex;
			this.DisplayName = state.DisplayName;
			this.EmoteSet = state.EmoteSet;
			this.IsModerator = state.IsModerator;
			this.IsSubscriber = state.IsSubscriber;
			this.UserType = state.UserType;
			this.Message = message;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x000060BC File Offset: 0x000042BC
		public SentMessage(List<KeyValuePair<string, string>> badges, string channel, string colorHex, string displayName, string emoteSet, bool isModerator, bool isSubscriber, UserType userType, string message)
		{
			this.Badges = badges;
			this.Channel = channel;
			this.ColorHex = colorHex;
			this.DisplayName = displayName;
			this.EmoteSet = emoteSet;
			this.IsModerator = isModerator;
			this.IsSubscriber = isSubscriber;
			this.UserType = userType;
			this.Message = message;
		}
	}
}
