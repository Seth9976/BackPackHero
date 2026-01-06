using System;

namespace TwitchLib.Client.Models.Builders
{
	// Token: 0x02000040 RID: 64
	public sealed class SubscriberBuilder : SubscriberBaseBuilder, IBuilder<Subscriber>, IFromIrcMessageBuilder<Subscriber>
	{
		// Token: 0x06000282 RID: 642 RVA: 0x00008BAE File Offset: 0x00006DAE
		private SubscriberBuilder()
		{
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00008BB6 File Offset: 0x00006DB6
		public new static SubscriberBuilder Create()
		{
			return new SubscriberBuilder();
		}

		// Token: 0x06000284 RID: 644 RVA: 0x00008BBD File Offset: 0x00006DBD
		public Subscriber BuildFromIrcMessage(FromIrcMessageBuilderDataObject fromIrcMessageBuilderDataObject)
		{
			return new Subscriber(fromIrcMessageBuilderDataObject.Message);
		}

		// Token: 0x06000285 RID: 645 RVA: 0x00008BCA File Offset: 0x00006DCA
		Subscriber IBuilder<Subscriber>.Build()
		{
			return (Subscriber)this.Build();
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00008BD8 File Offset: 0x00006DD8
		public override SubscriberBase Build()
		{
			return new Subscriber(base.Badges, base.BadgeInfo, base.ColorHex, base.Color, base.DisplayName, base.EmoteSet, base.Id, base.Login, base.SystemMessage, base.MessageId, base.MsgParamCumulativeMonths, base.MsgParamStreakMonths, base.MsgParamShouldShareStreak, base.ParsedSystemMessage, base.ResubMessage, base.SubscriptionPlan, base.SubscriptionPlanName, base.RoomId, base.UserId, base.IsModerator, base.IsTurbo, base.IsSubscriber, base.IsPartner, base.TmiSentTs, base.UserType, base.RawIrc, base.Channel);
		}
	}
}
