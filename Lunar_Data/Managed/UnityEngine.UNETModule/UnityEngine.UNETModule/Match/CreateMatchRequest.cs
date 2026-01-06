using System;
using System.Collections.Generic;

namespace UnityEngine.Networking.Match
{
	// Token: 0x02000025 RID: 37
	internal class CreateMatchRequest : Request
	{
		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x000057E7 File Offset: 0x000039E7
		// (set) Token: 0x060001A9 RID: 425 RVA: 0x000057EF File Offset: 0x000039EF
		public string name { get; set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001AA RID: 426 RVA: 0x000057F8 File Offset: 0x000039F8
		// (set) Token: 0x060001AB RID: 427 RVA: 0x00005800 File Offset: 0x00003A00
		public uint size { get; set; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001AC RID: 428 RVA: 0x00005809 File Offset: 0x00003A09
		// (set) Token: 0x060001AD RID: 429 RVA: 0x00005811 File Offset: 0x00003A11
		public string publicAddress { get; set; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001AE RID: 430 RVA: 0x0000581A File Offset: 0x00003A1A
		// (set) Token: 0x060001AF RID: 431 RVA: 0x00005822 File Offset: 0x00003A22
		public string privateAddress { get; set; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x0000582B File Offset: 0x00003A2B
		// (set) Token: 0x060001B1 RID: 433 RVA: 0x00005833 File Offset: 0x00003A33
		public int eloScore { get; set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x0000583C File Offset: 0x00003A3C
		// (set) Token: 0x060001B3 RID: 435 RVA: 0x00005844 File Offset: 0x00003A44
		public bool advertise { get; set; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x0000584D File Offset: 0x00003A4D
		// (set) Token: 0x060001B5 RID: 437 RVA: 0x00005855 File Offset: 0x00003A55
		public string password { get; set; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x0000585E File Offset: 0x00003A5E
		// (set) Token: 0x060001B7 RID: 439 RVA: 0x00005866 File Offset: 0x00003A66
		public Dictionary<string, long> matchAttributes { get; set; }

		// Token: 0x060001B8 RID: 440 RVA: 0x00005870 File Offset: 0x00003A70
		public override string ToString()
		{
			return UnityString.Format("[{0}]-name:{1},size:{2},publicAddress:{3},privateAddress:{4},eloScore:{5},advertise:{6},HasPassword:{7},matchAttributes.Count:{8}", new object[]
			{
				base.ToString(),
				this.name,
				this.size,
				this.publicAddress,
				this.privateAddress,
				this.eloScore,
				this.advertise,
				string.IsNullOrEmpty(this.password) ? "NO" : "YES",
				(this.matchAttributes == null) ? 0 : this.matchAttributes.Count
			});
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000591C File Offset: 0x00003B1C
		public override bool IsValid()
		{
			return base.IsValid() && this.size >= 2U && (this.matchAttributes == null || this.matchAttributes.Count <= 10);
		}
	}
}
