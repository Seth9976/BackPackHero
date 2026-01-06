using System;

namespace TwitchLib.Client.Models.Builders
{
	// Token: 0x0200002B RID: 43
	public sealed class ChannelStateBuilder : IBuilder<ChannelState>, IFromIrcMessageBuilder<ChannelState>
	{
		// Token: 0x0600017C RID: 380 RVA: 0x00007C80 File Offset: 0x00005E80
		private ChannelStateBuilder()
		{
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00007C88 File Offset: 0x00005E88
		public ChannelStateBuilder WithBroadcasterLanguage(string broadcasterLanguage)
		{
			this._broadcasterLanguage = broadcasterLanguage;
			return this;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00007C92 File Offset: 0x00005E92
		public ChannelStateBuilder WithChannel(string channel)
		{
			this._channel = channel;
			return this;
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00007C9C File Offset: 0x00005E9C
		public ChannelStateBuilder WithEmoteOnly(bool emoteOnly)
		{
			this._emoteOnly = emoteOnly;
			return this;
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00007CA6 File Offset: 0x00005EA6
		public ChannelStateBuilder WIthFollowersOnly(TimeSpan followersOnly)
		{
			this._followersOnly = followersOnly;
			return this;
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00007CB0 File Offset: 0x00005EB0
		public ChannelStateBuilder WithMercury(bool mercury)
		{
			this._mercury = mercury;
			return this;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00007CBA File Offset: 0x00005EBA
		public ChannelStateBuilder WithR9K(bool r9k)
		{
			this._r9K = r9k;
			return this;
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00007CC4 File Offset: 0x00005EC4
		public ChannelStateBuilder WithRituals(bool rituals)
		{
			this._rituals = rituals;
			return this;
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00007CCE File Offset: 0x00005ECE
		public ChannelStateBuilder WithRoomId(string roomId)
		{
			this._roomId = roomId;
			return this;
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00007CD8 File Offset: 0x00005ED8
		public ChannelStateBuilder WithSlowMode(int slowMode)
		{
			this._slowMode = slowMode;
			return this;
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00007CE2 File Offset: 0x00005EE2
		public ChannelStateBuilder WithSubOnly(bool subOnly)
		{
			this._subOnly = subOnly;
			return this;
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00007CEC File Offset: 0x00005EEC
		public static ChannelStateBuilder Create()
		{
			return new ChannelStateBuilder();
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00007CF4 File Offset: 0x00005EF4
		public ChannelState Build()
		{
			return new ChannelState(this._r9K, this._rituals, this._subOnly, this._slowMode, this._emoteOnly, this._broadcasterLanguage, this._channel, this._followersOnly, this._mercury, this._roomId);
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00007D42 File Offset: 0x00005F42
		public ChannelState BuildFromIrcMessage(FromIrcMessageBuilderDataObject fromIrcMessageBuilderDataObject)
		{
			return new ChannelState(fromIrcMessageBuilderDataObject.Message);
		}

		// Token: 0x040001A5 RID: 421
		private string _broadcasterLanguage;

		// Token: 0x040001A6 RID: 422
		private string _channel;

		// Token: 0x040001A7 RID: 423
		private bool _emoteOnly;

		// Token: 0x040001A8 RID: 424
		private TimeSpan _followersOnly;

		// Token: 0x040001A9 RID: 425
		private bool _mercury;

		// Token: 0x040001AA RID: 426
		private bool _r9K;

		// Token: 0x040001AB RID: 427
		private bool _rituals;

		// Token: 0x040001AC RID: 428
		private string _roomId;

		// Token: 0x040001AD RID: 429
		private int _slowMode;

		// Token: 0x040001AE RID: 430
		private bool _subOnly;
	}
}
