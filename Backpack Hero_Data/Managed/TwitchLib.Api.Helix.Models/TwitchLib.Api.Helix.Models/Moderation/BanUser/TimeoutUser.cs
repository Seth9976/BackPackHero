using System;

namespace TwitchLib.Api.Helix.Models.Moderation.BanUser
{
	// Token: 0x02000069 RID: 105
	public class TimeoutUser
	{
		// Token: 0x04000180 RID: 384
		public string UserId;

		// Token: 0x04000181 RID: 385
		public string Reason;

		// Token: 0x04000182 RID: 386
		public TimeSpan Duration;
	}
}
