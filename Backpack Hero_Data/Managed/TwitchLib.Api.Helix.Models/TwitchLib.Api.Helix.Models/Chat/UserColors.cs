using System;

namespace TwitchLib.Api.Helix.Models.Chat
{
	// Token: 0x02000099 RID: 153
	public class UserColors
	{
		// Token: 0x06000517 RID: 1303 RVA: 0x00004C6B File Offset: 0x00002E6B
		private UserColors(string value)
		{
			this.Value = value;
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000518 RID: 1304 RVA: 0x00004C7A File Offset: 0x00002E7A
		// (set) Token: 0x06000519 RID: 1305 RVA: 0x00004C82 File Offset: 0x00002E82
		public string Value { get; private set; }

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x0600051A RID: 1306 RVA: 0x00004C8B File Offset: 0x00002E8B
		public static UserColors Blue
		{
			get
			{
				return new UserColors("blue");
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x0600051B RID: 1307 RVA: 0x00004C97 File Offset: 0x00002E97
		public static UserColors BlueVoilet
		{
			get
			{
				return new UserColors("blue_violet");
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x0600051C RID: 1308 RVA: 0x00004CA3 File Offset: 0x00002EA3
		public static UserColors CadetBlue
		{
			get
			{
				return new UserColors("cadet_blue");
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x0600051D RID: 1309 RVA: 0x00004CAF File Offset: 0x00002EAF
		public static UserColors Chocolate
		{
			get
			{
				return new UserColors("chocolate");
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x0600051E RID: 1310 RVA: 0x00004CBB File Offset: 0x00002EBB
		public static UserColors Coral
		{
			get
			{
				return new UserColors("coral");
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x0600051F RID: 1311 RVA: 0x00004CC7 File Offset: 0x00002EC7
		public static UserColors DodgerBlue
		{
			get
			{
				return new UserColors("dodger_blue");
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000520 RID: 1312 RVA: 0x00004CD3 File Offset: 0x00002ED3
		public static UserColors Firebrick
		{
			get
			{
				return new UserColors("firebrick");
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000521 RID: 1313 RVA: 0x00004CDF File Offset: 0x00002EDF
		public static UserColors GoldenRod
		{
			get
			{
				return new UserColors("golden_rod");
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000522 RID: 1314 RVA: 0x00004CEB File Offset: 0x00002EEB
		public static UserColors HotPink
		{
			get
			{
				return new UserColors("hot_pink");
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000523 RID: 1315 RVA: 0x00004CF7 File Offset: 0x00002EF7
		public static UserColors OrangeRed
		{
			get
			{
				return new UserColors("orange_red");
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000524 RID: 1316 RVA: 0x00004D03 File Offset: 0x00002F03
		public static UserColors Red
		{
			get
			{
				return new UserColors("red");
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000525 RID: 1317 RVA: 0x00004D0F File Offset: 0x00002F0F
		public static UserColors SeaGreen
		{
			get
			{
				return new UserColors("sea_green");
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000526 RID: 1318 RVA: 0x00004D1B File Offset: 0x00002F1B
		public static UserColors SpringGreen
		{
			get
			{
				return new UserColors("spring_green");
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000527 RID: 1319 RVA: 0x00004D27 File Offset: 0x00002F27
		public static UserColors YellowGreen
		{
			get
			{
				return new UserColors("yellow_green");
			}
		}
	}
}
