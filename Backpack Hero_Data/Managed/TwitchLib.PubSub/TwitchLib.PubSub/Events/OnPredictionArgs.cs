using System;
using System.Collections.Generic;
using TwitchLib.PubSub.Enums;
using TwitchLib.PubSub.Models;

namespace TwitchLib.PubSub.Events
{
	// Token: 0x02000043 RID: 67
	public class OnPredictionArgs : EventArgs
	{
		// Token: 0x04000141 RID: 321
		public PredictionType Type;

		// Token: 0x04000142 RID: 322
		public Guid Id;

		// Token: 0x04000143 RID: 323
		public string ChannelId;

		// Token: 0x04000144 RID: 324
		public DateTime? CreatedAt;

		// Token: 0x04000145 RID: 325
		public DateTime? LockedAt;

		// Token: 0x04000146 RID: 326
		public DateTime? EndedAt;

		// Token: 0x04000147 RID: 327
		public ICollection<Outcome> Outcomes;

		// Token: 0x04000148 RID: 328
		public PredictionStatus Status;

		// Token: 0x04000149 RID: 329
		public string Title;

		// Token: 0x0400014A RID: 330
		public Guid? WinningOutcomeId;

		// Token: 0x0400014B RID: 331
		public int PredictionTime;
	}
}
