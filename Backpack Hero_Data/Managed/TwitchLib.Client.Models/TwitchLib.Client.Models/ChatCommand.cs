using System;
using System.Collections.Generic;
using System.Linq;
using TwitchLib.Client.Models.Common;

namespace TwitchLib.Client.Models
{
	// Token: 0x02000004 RID: 4
	public class ChatCommand
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002996 File Offset: 0x00000B96
		public List<string> ArgumentsAsList { get; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000026 RID: 38 RVA: 0x0000299E File Offset: 0x00000B9E
		public string ArgumentsAsString { get; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000027 RID: 39 RVA: 0x000029A6 File Offset: 0x00000BA6
		public ChatMessage ChatMessage { get; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000028 RID: 40 RVA: 0x000029AE File Offset: 0x00000BAE
		public char CommandIdentifier { get; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000029B6 File Offset: 0x00000BB6
		public string CommandText { get; }

		// Token: 0x0600002A RID: 42 RVA: 0x000029C0 File Offset: 0x00000BC0
		public ChatCommand(ChatMessage chatMessage)
		{
			ChatCommand <>4__this = this;
			this.ChatMessage = chatMessage;
			string[] array = chatMessage.Message.Split(new char[] { ' ' });
			this.CommandText = ((array != null) ? array[0].Substring(1, chatMessage.Message.Split(new char[] { ' ' })[0].Length - 1) : null) ?? chatMessage.Message.Substring(1, chatMessage.Message.Length - 1);
			string text;
			if (!chatMessage.Message.Contains(" "))
			{
				text = "";
			}
			else
			{
				string message = chatMessage.Message;
				string[] array2 = chatMessage.Message.Split(new char[] { ' ' });
				text = message.Replace(((array2 != null) ? array2[0] : null) + " ", "");
			}
			this.ArgumentsAsString = text;
			if (chatMessage.Message.Contains("\""))
			{
				if (Enumerable.Count<char>(chatMessage.Message, (char x) => x == '"') % 2 != 1)
				{
					this.ArgumentsAsList = Helpers.ParseQuotesAndNonQuotes(this.ArgumentsAsString);
					goto IL_01AD;
				}
			}
			string[] array3 = chatMessage.Message.Split(new char[] { ' ' });
			this.ArgumentsAsList = ((array3 != null) ? Enumerable.ToList<string>(Enumerable.Where<string>(array3, (string arg) => arg != chatMessage.Message.get_Chars(0).ToString() + <>4__this.CommandText)) : null) ?? new List<string>();
			IL_01AD:
			this.CommandIdentifier = chatMessage.Message.get_Chars(0);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002B91 File Offset: 0x00000D91
		public ChatCommand(ChatMessage chatMessage, string commandText, string argumentsAsString, List<string> argumentsAsList, char commandIdentifier)
		{
			this.ChatMessage = chatMessage;
			this.CommandText = commandText;
			this.ArgumentsAsString = argumentsAsString;
			this.ArgumentsAsList = argumentsAsList;
			this.CommandIdentifier = commandIdentifier;
		}
	}
}
