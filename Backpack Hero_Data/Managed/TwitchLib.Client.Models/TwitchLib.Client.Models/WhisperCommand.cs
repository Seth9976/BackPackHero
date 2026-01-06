using System;
using System.Collections.Generic;
using System.Linq;

namespace TwitchLib.Client.Models
{
	// Token: 0x02000022 RID: 34
	public class WhisperCommand
	{
		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000160 RID: 352 RVA: 0x00007081 File Offset: 0x00005281
		public List<string> ArgumentsAsList { get; }

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00007089 File Offset: 0x00005289
		public string ArgumentsAsString { get; }

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000162 RID: 354 RVA: 0x00007091 File Offset: 0x00005291
		public char CommandIdentifier { get; }

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00007099 File Offset: 0x00005299
		public string CommandText { get; }

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000164 RID: 356 RVA: 0x000070A1 File Offset: 0x000052A1
		public WhisperMessage WhisperMessage { get; }

		// Token: 0x06000165 RID: 357 RVA: 0x000070AC File Offset: 0x000052AC
		public WhisperCommand(WhisperMessage whisperMessage)
		{
			WhisperCommand <>4__this = this;
			this.WhisperMessage = whisperMessage;
			string[] array = whisperMessage.Message.Split(new char[] { ' ' });
			this.CommandText = ((array != null) ? array[0].Substring(1, whisperMessage.Message.Split(new char[] { ' ' })[0].Length - 1) : null) ?? whisperMessage.Message.Substring(1, whisperMessage.Message.Length - 1);
			string message = whisperMessage.Message;
			string[] array2 = whisperMessage.Message.Split(new char[] { ' ' });
			this.ArgumentsAsString = message.Replace(((array2 != null) ? array2[0] : null) + " ", "");
			string[] array3 = whisperMessage.Message.Split(new char[] { ' ' });
			this.ArgumentsAsList = ((array3 != null) ? Enumerable.ToList<string>(Enumerable.Where<string>(array3, (string arg) => arg != whisperMessage.Message.get_Chars(0).ToString() + <>4__this.CommandText)) : null) ?? new List<string>();
			this.CommandIdentifier = whisperMessage.Message.get_Chars(0);
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00007201 File Offset: 0x00005401
		public WhisperCommand(WhisperMessage whisperMessage, string commandText, string argumentsAsString, List<string> argumentsAsList, char commandIdentifier)
		{
			this.WhisperMessage = whisperMessage;
			this.CommandText = commandText;
			this.ArgumentsAsString = argumentsAsString;
			this.ArgumentsAsList = argumentsAsList;
			this.CommandIdentifier = commandIdentifier;
		}
	}
}
