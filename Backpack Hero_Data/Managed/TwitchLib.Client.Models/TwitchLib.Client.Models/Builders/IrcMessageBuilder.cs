using System;
using System.Collections.Generic;
using TwitchLib.Client.Enums.Internal;
using TwitchLib.Client.Models.Internal;

namespace TwitchLib.Client.Models.Builders
{
	// Token: 0x02000037 RID: 55
	public sealed class IrcMessageBuilder : IBuilder<IrcMessage>
	{
		// Token: 0x060001E5 RID: 485 RVA: 0x000082C7 File Offset: 0x000064C7
		public static IrcMessageBuilder Create()
		{
			return new IrcMessageBuilder();
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x000082CE File Offset: 0x000064CE
		private IrcMessageBuilder()
		{
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x000082E1 File Offset: 0x000064E1
		public IrcMessageBuilder WithCommand(IrcCommand ircCommand)
		{
			this._ircCommand = ircCommand;
			return this.Builder();
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x000082F0 File Offset: 0x000064F0
		public IrcMessageBuilder WithParameter(params string[] parameters)
		{
			this._parameters.AddRange(parameters);
			return this.Builder();
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00008304 File Offset: 0x00006504
		private IrcMessageBuilder Builder()
		{
			return this;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00008307 File Offset: 0x00006507
		public IrcMessage BuildWithUserOnly(string user)
		{
			return new IrcMessage(user);
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000830F File Offset: 0x0000650F
		public IrcMessageBuilder WithHostMask(string hostMask)
		{
			this._hostmask = hostMask;
			return this.Builder();
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000831E File Offset: 0x0000651E
		public IrcMessageBuilder WithTags(Dictionary<string, string> tags)
		{
			this._tags = tags;
			return this.Builder();
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000832D File Offset: 0x0000652D
		public IrcMessage Build()
		{
			return new IrcMessage(this._ircCommand, this._parameters.ToArray(), this._hostmask, this._tags);
		}

		// Token: 0x040001EC RID: 492
		private IrcCommand _ircCommand;

		// Token: 0x040001ED RID: 493
		private readonly List<string> _parameters = new List<string>();

		// Token: 0x040001EE RID: 494
		private string _hostmask;

		// Token: 0x040001EF RID: 495
		private Dictionary<string, string> _tags;
	}
}
