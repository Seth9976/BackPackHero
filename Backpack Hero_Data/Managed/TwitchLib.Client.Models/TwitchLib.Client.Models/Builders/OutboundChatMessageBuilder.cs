using System;

namespace TwitchLib.Client.Models.Builders
{
	// Token: 0x02000038 RID: 56
	public sealed class OutboundChatMessageBuilder : IBuilder<OutboundChatMessage>
	{
		// Token: 0x060001EE RID: 494 RVA: 0x00008351 File Offset: 0x00006551
		private OutboundChatMessageBuilder()
		{
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00008359 File Offset: 0x00006559
		public static OutboundChatMessageBuilder Create()
		{
			return new OutboundChatMessageBuilder();
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00008360 File Offset: 0x00006560
		public OutboundChatMessageBuilder WithChannel(string channel)
		{
			this._channel = channel;
			return this;
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000836A File Offset: 0x0000656A
		public OutboundChatMessageBuilder WithMessage(string message)
		{
			this._message = message;
			return this;
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00008374 File Offset: 0x00006574
		public OutboundChatMessageBuilder WithUsername(string userName)
		{
			this._userName = userName;
			return this;
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000837E File Offset: 0x0000657E
		public OutboundChatMessage Build()
		{
			return new OutboundChatMessage
			{
				Channel = this._channel,
				Message = this._message,
				Username = this._userName
			};
		}

		// Token: 0x040001F0 RID: 496
		private string _channel;

		// Token: 0x040001F1 RID: 497
		private string _message;

		// Token: 0x040001F2 RID: 498
		private string _userName;
	}
}
