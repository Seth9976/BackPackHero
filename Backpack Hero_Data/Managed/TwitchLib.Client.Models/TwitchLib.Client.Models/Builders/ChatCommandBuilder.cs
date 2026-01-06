using System;
using System.Collections.Generic;

namespace TwitchLib.Client.Models.Builders
{
	// Token: 0x0200002C RID: 44
	public sealed class ChatCommandBuilder : IBuilder<ChatCommand>
	{
		// Token: 0x0600018A RID: 394 RVA: 0x00007D4F File Offset: 0x00005F4F
		private ChatCommandBuilder()
		{
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00007D62 File Offset: 0x00005F62
		public ChatCommandBuilder WithArgumentsAsList(params string[] argumentsList)
		{
			this._argumentsAsList.AddRange(argumentsList);
			return this;
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00007D71 File Offset: 0x00005F71
		public ChatCommandBuilder WithArgumentsAsString(string argumentsAsString)
		{
			this._argumentsAsString = argumentsAsString;
			return this;
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00007D7B File Offset: 0x00005F7B
		public ChatCommandBuilder WithChatMessage(ChatMessage chatMessage)
		{
			this._chatMessage = chatMessage;
			return this;
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00007D85 File Offset: 0x00005F85
		public ChatCommandBuilder WithCommandIdentifier(char commandIdentifier)
		{
			this._commandIdentifier = commandIdentifier;
			return this;
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00007D8F File Offset: 0x00005F8F
		public ChatCommandBuilder WithCommandText(string commandText)
		{
			this._commandText = commandText;
			return this;
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00007D99 File Offset: 0x00005F99
		public static ChatCommandBuilder Create()
		{
			return new ChatCommandBuilder();
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00007DA0 File Offset: 0x00005FA0
		public ChatCommand Build()
		{
			return new ChatCommand(this._chatMessage, this._commandText, this._argumentsAsString, this._argumentsAsList, this._commandIdentifier);
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00007DC5 File Offset: 0x00005FC5
		public ChatCommand BuildFromChatMessage(ChatMessage chatMessage)
		{
			return new ChatCommand(chatMessage);
		}

		// Token: 0x040001AF RID: 431
		private readonly List<string> _argumentsAsList = new List<string>();

		// Token: 0x040001B0 RID: 432
		private string _argumentsAsString;

		// Token: 0x040001B1 RID: 433
		private ChatMessage _chatMessage;

		// Token: 0x040001B2 RID: 434
		private char _commandIdentifier;

		// Token: 0x040001B3 RID: 435
		private string _commandText;
	}
}
