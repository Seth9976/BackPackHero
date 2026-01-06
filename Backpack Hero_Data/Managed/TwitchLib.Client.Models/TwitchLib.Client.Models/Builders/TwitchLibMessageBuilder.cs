using System;
using System.Collections.Generic;
using System.Drawing;
using TwitchLib.Client.Enums;

namespace TwitchLib.Client.Models.Builders
{
	// Token: 0x02000041 RID: 65
	public sealed class TwitchLibMessageBuilder : TwitchLibMessage, IBuilder<TwitchLibMessage>
	{
		// Token: 0x06000287 RID: 647 RVA: 0x00008C8C File Offset: 0x00006E8C
		private TwitchLibMessageBuilder()
		{
		}

		// Token: 0x06000288 RID: 648 RVA: 0x00008C94 File Offset: 0x00006E94
		public TwitchLibMessageBuilder WithBadges(List<KeyValuePair<string, string>> badges)
		{
			base.Badges = badges;
			return this;
		}

		// Token: 0x06000289 RID: 649 RVA: 0x00008C9E File Offset: 0x00006E9E
		public TwitchLibMessageBuilder WithColorHex(string colorHex)
		{
			base.ColorHex = colorHex;
			return this;
		}

		// Token: 0x0600028A RID: 650 RVA: 0x00008CA8 File Offset: 0x00006EA8
		public TwitchLibMessageBuilder WithColorHex(Color color)
		{
			base.Color = color;
			return this;
		}

		// Token: 0x0600028B RID: 651 RVA: 0x00008CB2 File Offset: 0x00006EB2
		public TwitchLibMessageBuilder WithUsername(string username)
		{
			base.Username = username;
			return this;
		}

		// Token: 0x0600028C RID: 652 RVA: 0x00008CBC File Offset: 0x00006EBC
		public TwitchLibMessageBuilder WithDisplayName(string displayName)
		{
			base.DisplayName = displayName;
			return this;
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00008CC6 File Offset: 0x00006EC6
		public TwitchLibMessageBuilder WithEmoteSet(EmoteSet emoteSet)
		{
			base.EmoteSet = emoteSet;
			return this;
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00008CD0 File Offset: 0x00006ED0
		public TwitchLibMessageBuilder WithUserId(string userId)
		{
			base.UserId = userId;
			return this;
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00008CDA File Offset: 0x00006EDA
		public TwitchLibMessageBuilder WithIsTurbo(bool isTurbo)
		{
			base.IsTurbo = isTurbo;
			return this;
		}

		// Token: 0x06000290 RID: 656 RVA: 0x00008CE4 File Offset: 0x00006EE4
		public TwitchLibMessageBuilder WithBotUserName(string botUserName)
		{
			base.BotUsername = botUserName;
			return this;
		}

		// Token: 0x06000291 RID: 657 RVA: 0x00008CEE File Offset: 0x00006EEE
		public TwitchLibMessageBuilder WithUserType(UserType userType)
		{
			base.UserType = userType;
			return this;
		}

		// Token: 0x06000292 RID: 658 RVA: 0x00008CF8 File Offset: 0x00006EF8
		public static TwitchLibMessageBuilder Create()
		{
			return new TwitchLibMessageBuilder();
		}

		// Token: 0x06000293 RID: 659 RVA: 0x00008CFF File Offset: 0x00006EFF
		public TwitchLibMessage Build()
		{
			return this;
		}
	}
}
