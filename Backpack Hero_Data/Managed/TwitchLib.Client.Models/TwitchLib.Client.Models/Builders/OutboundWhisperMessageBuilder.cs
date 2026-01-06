using System;

namespace TwitchLib.Client.Models.Builders
{
	// Token: 0x02000039 RID: 57
	public sealed class OutboundWhisperMessageBuilder : IBuilder<OutboundWhisperMessage>
	{
		// Token: 0x060001F4 RID: 500 RVA: 0x000083A9 File Offset: 0x000065A9
		private OutboundWhisperMessageBuilder()
		{
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x000083B1 File Offset: 0x000065B1
		public OutboundWhisperMessageBuilder WithUsername(string username)
		{
			this._username = username;
			return this;
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x000083BB File Offset: 0x000065BB
		public OutboundWhisperMessageBuilder WithReceiver(string receiver)
		{
			this._receiver = receiver;
			return this;
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x000083C5 File Offset: 0x000065C5
		public OutboundWhisperMessageBuilder WithMessage(string message)
		{
			this._message = message;
			return this;
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x000083CF File Offset: 0x000065CF
		public static OutboundWhisperMessageBuilder Create()
		{
			return new OutboundWhisperMessageBuilder();
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x000083D6 File Offset: 0x000065D6
		public OutboundWhisperMessage Build()
		{
			return new OutboundWhisperMessage
			{
				Message = this._message,
				Receiver = this._receiver,
				Username = this._username
			};
		}

		// Token: 0x040001F3 RID: 499
		private string _username;

		// Token: 0x040001F4 RID: 500
		private string _receiver;

		// Token: 0x040001F5 RID: 501
		private string _message;
	}
}
