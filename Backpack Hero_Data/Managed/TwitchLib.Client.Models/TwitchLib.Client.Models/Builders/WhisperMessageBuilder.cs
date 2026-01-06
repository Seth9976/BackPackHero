using System;

namespace TwitchLib.Client.Models.Builders
{
	// Token: 0x02000046 RID: 70
	public sealed class WhisperMessageBuilder : IBuilder<WhisperMessage>, IFromIrcMessageBuilder<WhisperMessage>
	{
		// Token: 0x060002BD RID: 701 RVA: 0x00008F57 File Offset: 0x00007157
		private WhisperMessageBuilder()
		{
			this._twitchLibMessage = TwitchLibMessageBuilder.Create().Build();
		}

		// Token: 0x060002BE RID: 702 RVA: 0x00008F6F File Offset: 0x0000716F
		public static WhisperMessageBuilder Create()
		{
			return new WhisperMessageBuilder();
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00008F76 File Offset: 0x00007176
		public WhisperMessageBuilder WithTwitchLibMessage(TwitchLibMessage twitchLibMessage)
		{
			this._twitchLibMessage = twitchLibMessage;
			return this;
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x00008F80 File Offset: 0x00007180
		public WhisperMessageBuilder WithMessageId(string messageId)
		{
			this._messageId = messageId;
			return this;
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00008F8A File Offset: 0x0000718A
		public WhisperMessageBuilder WithThreadId(string threadId)
		{
			this._threadId = threadId;
			return this;
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00008F94 File Offset: 0x00007194
		public WhisperMessageBuilder WhihMessage(string message)
		{
			this._message = message;
			return this;
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00008FA0 File Offset: 0x000071A0
		public WhisperMessage BuildFromIrcMessage(FromIrcMessageBuilderDataObject fromIrcMessageBuilderDataObject)
		{
			string text = fromIrcMessageBuilderDataObject.AditionalData.ToString();
			return new WhisperMessage(fromIrcMessageBuilderDataObject.Message, text);
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00008FC8 File Offset: 0x000071C8
		public WhisperMessage Build()
		{
			return new WhisperMessage(this._twitchLibMessage.Badges, this._twitchLibMessage.ColorHex, this._twitchLibMessage.Color, this._twitchLibMessage.Username, this._twitchLibMessage.DisplayName, this._twitchLibMessage.EmoteSet, this._threadId, this._messageId, this._twitchLibMessage.UserId, this._twitchLibMessage.IsTurbo, this._twitchLibMessage.BotUsername, this._message, this._twitchLibMessage.UserType);
		}

		// Token: 0x0400024D RID: 589
		private TwitchLibMessage _twitchLibMessage;

		// Token: 0x0400024E RID: 590
		private string _messageId;

		// Token: 0x0400024F RID: 591
		private string _threadId;

		// Token: 0x04000250 RID: 592
		private string _message;
	}
}
