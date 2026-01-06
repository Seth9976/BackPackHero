using System;
using TwitchLib.Client.Models.Internal;

namespace TwitchLib.Client.Models.Extractors
{
	// Token: 0x02000028 RID: 40
	public interface IExtractor<TResult>
	{
		// Token: 0x06000176 RID: 374
		TResult Extract(IrcMessage ircMessage);
	}
}
