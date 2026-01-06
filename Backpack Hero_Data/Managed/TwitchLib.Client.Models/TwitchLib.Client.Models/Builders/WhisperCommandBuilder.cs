using System;
using System.Collections.Generic;

namespace TwitchLib.Client.Models.Builders
{
	// Token: 0x02000045 RID: 69
	public sealed class WhisperCommandBuilder : IBuilder<WhisperCommand>
	{
		// Token: 0x060002B4 RID: 692 RVA: 0x00008ED9 File Offset: 0x000070D9
		private WhisperCommandBuilder()
		{
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x00008EEC File Offset: 0x000070EC
		public WhisperCommandBuilder WithWhisperMessage(WhisperMessage whisperMessage)
		{
			this._whisperMessage = whisperMessage;
			return this;
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x00008EF6 File Offset: 0x000070F6
		public WhisperCommandBuilder WithCommandText(string commandText)
		{
			this._commandText = commandText;
			return this;
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x00008F00 File Offset: 0x00007100
		public WhisperCommandBuilder WithCommandIdentifier(char commandIdentifier)
		{
			this._commandIdentifier = commandIdentifier;
			return this;
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x00008F0A File Offset: 0x0000710A
		public WhisperCommandBuilder WithArgumentAsString(string argumentAsString)
		{
			this._argumentsAsString = argumentAsString;
			return this;
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x00008F14 File Offset: 0x00007114
		public WhisperCommandBuilder WithArguments(params string[] arguments)
		{
			this._argumentsAsList.AddRange(arguments);
			return this;
		}

		// Token: 0x060002BA RID: 698 RVA: 0x00008F23 File Offset: 0x00007123
		public static WhisperCommandBuilder Create()
		{
			return new WhisperCommandBuilder();
		}

		// Token: 0x060002BB RID: 699 RVA: 0x00008F2A File Offset: 0x0000712A
		public WhisperCommand BuildFromWhisperMessage(WhisperMessage whisperMessage)
		{
			return new WhisperCommand(whisperMessage);
		}

		// Token: 0x060002BC RID: 700 RVA: 0x00008F32 File Offset: 0x00007132
		public WhisperCommand Build()
		{
			return new WhisperCommand(this._whisperMessage, this._commandText, this._argumentsAsString, this._argumentsAsList, this._commandIdentifier);
		}

		// Token: 0x04000248 RID: 584
		private readonly List<string> _argumentsAsList = new List<string>();

		// Token: 0x04000249 RID: 585
		private string _argumentsAsString;

		// Token: 0x0400024A RID: 586
		private char _commandIdentifier;

		// Token: 0x0400024B RID: 587
		private string _commandText;

		// Token: 0x0400024C RID: 588
		private WhisperMessage _whisperMessage;
	}
}
