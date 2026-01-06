using System;

namespace TwitchLib.Client.Models.Builders
{
	// Token: 0x0200003C RID: 60
	public sealed class ReSubscriberBuilder : SubscriberBaseBuilder, IBuilder<ReSubscriber>, IFromIrcMessageBuilder<ReSubscriber>
	{
		// Token: 0x06000219 RID: 537 RVA: 0x0000861B File Offset: 0x0000681B
		private ReSubscriberBuilder()
		{
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00008623 File Offset: 0x00006823
		public new static ReSubscriberBuilder Create()
		{
			return new ReSubscriberBuilder();
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000862A File Offset: 0x0000682A
		public ReSubscriber BuildFromIrcMessage(FromIrcMessageBuilderDataObject fromIrcMessageBuilderDataObject)
		{
			return new ReSubscriber(fromIrcMessageBuilderDataObject.Message);
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00008637 File Offset: 0x00006837
		ReSubscriber IBuilder<ReSubscriber>.Build()
		{
			return (ReSubscriber)this.Build();
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00008644 File Offset: 0x00006844
		public override SubscriberBase Build()
		{
			return new ReSubscriber(base.Badges, base.BadgeInfo, base.ColorHex, base.Color, base.DisplayName, base.EmoteSet, base.Id, base.Login, base.SystemMessage, base.MessageId, base.MsgParamCumulativeMonths, base.MsgParamStreakMonths, base.MsgParamShouldShareStreak, base.ParsedSystemMessage, base.ResubMessage, base.SubscriptionPlan, base.SubscriptionPlanName, base.RoomId, base.UserId, base.IsModerator, base.IsTurbo, base.IsSubscriber, base.IsPartner, base.TmiSentTs, base.UserType, base.RawIrc, base.Channel, base.Months);
		}
	}
}
