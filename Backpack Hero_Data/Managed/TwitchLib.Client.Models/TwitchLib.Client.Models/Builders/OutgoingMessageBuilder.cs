using System;

namespace TwitchLib.Client.Models.Builders
{
	// Token: 0x0200003A RID: 58
	public sealed class OutgoingMessageBuilder : IBuilder<OutgoingMessage>
	{
		// Token: 0x060001FA RID: 506 RVA: 0x00008401 File Offset: 0x00006601
		private OutgoingMessageBuilder()
		{
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00008409 File Offset: 0x00006609
		public static OutgoingMessageBuilder Create()
		{
			return new OutgoingMessageBuilder();
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00008410 File Offset: 0x00006610
		public OutgoingMessageBuilder WithChannel(string channel)
		{
			this._channel = channel;
			return this;
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0000841A File Offset: 0x0000661A
		public OutgoingMessageBuilder WithMessage(string message)
		{
			this._message = message;
			return this;
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00008424 File Offset: 0x00006624
		public OutgoingMessageBuilder WithNonce(int nonce)
		{
			this._nonce = nonce;
			return this;
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000842E File Offset: 0x0000662E
		public OutgoingMessageBuilder WithSender(string sender)
		{
			this._sender = sender;
			return this;
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00008438 File Offset: 0x00006638
		public OutgoingMessageBuilder WithMessageState(MessageState messageState)
		{
			this._messageState = messageState;
			return this;
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00008444 File Offset: 0x00006644
		public OutgoingMessage Build()
		{
			return new OutgoingMessage
			{
				Channel = this._channel,
				Message = this._message,
				Nonce = this._nonce,
				Sender = this._sender,
				State = this._messageState
			};
		}

		// Token: 0x040001F6 RID: 502
		private string _channel;

		// Token: 0x040001F7 RID: 503
		private string _message;

		// Token: 0x040001F8 RID: 504
		private int _nonce;

		// Token: 0x040001F9 RID: 505
		private string _sender;

		// Token: 0x040001FA RID: 506
		private MessageState _messageState;
	}
}
