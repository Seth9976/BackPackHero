using System;

namespace TwitchLib.Client.Models.Builders
{
	// Token: 0x0200002E RID: 46
	public sealed class CheerBadgeBuilder : IBuilder<CheerBadge>
	{
		// Token: 0x060001AA RID: 426 RVA: 0x00007FB0 File Offset: 0x000061B0
		public CheerBadgeBuilder WithCheerAmount(int cheerAmount)
		{
			this._cheerAmount = cheerAmount;
			return this;
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00007FBA File Offset: 0x000061BA
		public CheerBadge Build()
		{
			return new CheerBadge(this._cheerAmount);
		}

		// Token: 0x040001C8 RID: 456
		private int _cheerAmount;
	}
}
