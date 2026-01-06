using System;
using System.Collections.Generic;
using System.Text;
using TwitchLib.Client.Enums.Internal;

namespace TwitchLib.Client.Models.Internal
{
	// Token: 0x02000024 RID: 36
	public class IrcMessage
	{
		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600016C RID: 364 RVA: 0x00007658 File Offset: 0x00005858
		public string Channel
		{
			get
			{
				if (!this.Params.StartsWith("#"))
				{
					return this.Params;
				}
				return this.Params.Remove(0, 1);
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x0600016D RID: 365 RVA: 0x00007680 File Offset: 0x00005880
		public string Params
		{
			get
			{
				if (this._parameters == null || this._parameters.Length == 0)
				{
					return "";
				}
				return this._parameters[0];
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x0600016E RID: 366 RVA: 0x000076A1 File Offset: 0x000058A1
		public string Message
		{
			get
			{
				return this.Trailing;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x0600016F RID: 367 RVA: 0x000076A9 File Offset: 0x000058A9
		public string Trailing
		{
			get
			{
				if (this._parameters == null || this._parameters.Length <= 1)
				{
					return "";
				}
				return this._parameters[this._parameters.Length - 1];
			}
		}

		// Token: 0x06000170 RID: 368 RVA: 0x000076D5 File Offset: 0x000058D5
		public IrcMessage(string user)
		{
			this._parameters = null;
			this.User = user;
			this.Hostmask = null;
			this.Command = IrcCommand.Unknown;
			this.Tags = null;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00007700 File Offset: 0x00005900
		public IrcMessage(IrcCommand command, string[] parameters, string hostmask, Dictionary<string, string> tags = null)
		{
			int num = hostmask.IndexOf('!');
			this.User = ((num != -1) ? hostmask.Substring(0, num) : hostmask);
			this.Hostmask = hostmask;
			this._parameters = parameters;
			this.Command = command;
			this.Tags = tags;
			if (command == IrcCommand.RPL_353 && this.Params.Length > 0 && this.Params.Contains("#"))
			{
				this._parameters[0] = "#" + this._parameters[0].Split(new char[] { '#' })[1];
			}
		}

		// Token: 0x06000172 RID: 370 RVA: 0x000077A0 File Offset: 0x000059A0
		public string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(32);
			if (this.Tags != null)
			{
				string[] array = new string[this.Tags.Count];
				int num = 0;
				foreach (KeyValuePair<string, string> keyValuePair in this.Tags)
				{
					array[num] = keyValuePair.Key + "=" + keyValuePair.Value;
					num++;
				}
				if (array.Length != 0)
				{
					stringBuilder.Append("@").Append(string.Join(";", array)).Append(" ");
				}
			}
			if (!string.IsNullOrEmpty(this.Hostmask))
			{
				stringBuilder.Append(":").Append(this.Hostmask).Append(" ");
			}
			stringBuilder.Append(this.Command.ToString().ToUpper().Replace("RPL_", ""));
			if (this._parameters.Length == 0)
			{
				return stringBuilder.ToString();
			}
			if (this._parameters[0] != null && this._parameters[0].Length > 0)
			{
				stringBuilder.Append(" ").Append(this._parameters[0]);
			}
			if (this._parameters.Length > 1 && this._parameters[1] != null && this._parameters[1].Length > 0)
			{
				stringBuilder.Append(" :").Append(this._parameters[1]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000130 RID: 304
		private readonly string[] _parameters;

		// Token: 0x04000131 RID: 305
		public readonly string User;

		// Token: 0x04000132 RID: 306
		public readonly string Hostmask;

		// Token: 0x04000133 RID: 307
		public readonly IrcCommand Command;

		// Token: 0x04000134 RID: 308
		public readonly Dictionary<string, string> Tags;
	}
}
