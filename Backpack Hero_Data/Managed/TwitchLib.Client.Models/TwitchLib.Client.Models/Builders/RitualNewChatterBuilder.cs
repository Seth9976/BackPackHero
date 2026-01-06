using System;

namespace TwitchLib.Client.Models.Builders
{
	// Token: 0x0200003D RID: 61
	public sealed class RitualNewChatterBuilder : IFromIrcMessageBuilder<RitualNewChatter>
	{
		// Token: 0x0600021E RID: 542 RVA: 0x000086FE File Offset: 0x000068FE
		public RitualNewChatter BuildFromIrcMessage(FromIrcMessageBuilderDataObject fromIrcMessageBuilderDataObject)
		{
			return new RitualNewChatter(fromIrcMessageBuilderDataObject.Message);
		}
	}
}
