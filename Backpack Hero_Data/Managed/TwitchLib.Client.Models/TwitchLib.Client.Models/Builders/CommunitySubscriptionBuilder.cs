using System;

namespace TwitchLib.Client.Models.Builders
{
	// Token: 0x0200002F RID: 47
	public sealed class CommunitySubscriptionBuilder : IFromIrcMessageBuilder<CommunitySubscription>
	{
		// Token: 0x060001AD RID: 429 RVA: 0x00007FCF File Offset: 0x000061CF
		private CommunitySubscriptionBuilder()
		{
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00007FD7 File Offset: 0x000061D7
		public static CommunitySubscriptionBuilder Create()
		{
			return new CommunitySubscriptionBuilder();
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00007FDE File Offset: 0x000061DE
		public CommunitySubscription BuildFromIrcMessage(FromIrcMessageBuilderDataObject fromIrcMessageBuilderDataObject)
		{
			return new CommunitySubscription(fromIrcMessageBuilderDataObject.Message);
		}
	}
}
