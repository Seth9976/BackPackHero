using System;
using TwitchLib.Client.Enums;

namespace TwitchLib.Client.Models
{
	// Token: 0x02000007 RID: 7
	public class CheerBadge
	{
		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00003988 File Offset: 0x00001B88
		public int CheerAmount { get; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00003990 File Offset: 0x00001B90
		public BadgeColor Color { get; }

		// Token: 0x06000057 RID: 87 RVA: 0x00003998 File Offset: 0x00001B98
		public CheerBadge(int cheerAmount)
		{
			this.CheerAmount = cheerAmount;
			this.Color = this.GetColor(cheerAmount);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000039B4 File Offset: 0x00001BB4
		private BadgeColor GetColor(int cheerAmount)
		{
			if (cheerAmount >= 10000)
			{
				return BadgeColor.Red;
			}
			if (cheerAmount >= 5000)
			{
				return BadgeColor.Blue;
			}
			if (cheerAmount >= 1000)
			{
				return BadgeColor.Green;
			}
			if (cheerAmount < 100)
			{
				return BadgeColor.Gray;
			}
			return BadgeColor.Purple;
		}
	}
}
