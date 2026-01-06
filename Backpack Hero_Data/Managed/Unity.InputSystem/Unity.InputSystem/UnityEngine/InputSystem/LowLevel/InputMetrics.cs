using System;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000EB RID: 235
	[Serializable]
	public struct InputMetrics
	{
		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06000DE2 RID: 3554 RVA: 0x000449D9 File Offset: 0x00042BD9
		// (set) Token: 0x06000DE3 RID: 3555 RVA: 0x000449E1 File Offset: 0x00042BE1
		public int maxNumDevices { readonly get; set; }

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06000DE4 RID: 3556 RVA: 0x000449EA File Offset: 0x00042BEA
		// (set) Token: 0x06000DE5 RID: 3557 RVA: 0x000449F2 File Offset: 0x00042BF2
		public int currentNumDevices { readonly get; set; }

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06000DE6 RID: 3558 RVA: 0x000449FB File Offset: 0x00042BFB
		// (set) Token: 0x06000DE7 RID: 3559 RVA: 0x00044A03 File Offset: 0x00042C03
		public int maxStateSizeInBytes { readonly get; set; }

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06000DE8 RID: 3560 RVA: 0x00044A0C File Offset: 0x00042C0C
		// (set) Token: 0x06000DE9 RID: 3561 RVA: 0x00044A14 File Offset: 0x00042C14
		public int currentStateSizeInBytes { readonly get; set; }

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06000DEA RID: 3562 RVA: 0x00044A1D File Offset: 0x00042C1D
		// (set) Token: 0x06000DEB RID: 3563 RVA: 0x00044A25 File Offset: 0x00042C25
		public int currentControlCount { readonly get; set; }

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06000DEC RID: 3564 RVA: 0x00044A2E File Offset: 0x00042C2E
		// (set) Token: 0x06000DED RID: 3565 RVA: 0x00044A36 File Offset: 0x00042C36
		public int currentLayoutCount { readonly get; set; }

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06000DEE RID: 3566 RVA: 0x00044A3F File Offset: 0x00042C3F
		// (set) Token: 0x06000DEF RID: 3567 RVA: 0x00044A47 File Offset: 0x00042C47
		public int totalEventBytes { readonly get; set; }

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06000DF0 RID: 3568 RVA: 0x00044A50 File Offset: 0x00042C50
		// (set) Token: 0x06000DF1 RID: 3569 RVA: 0x00044A58 File Offset: 0x00042C58
		public int totalEventCount { readonly get; set; }

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06000DF2 RID: 3570 RVA: 0x00044A61 File Offset: 0x00042C61
		// (set) Token: 0x06000DF3 RID: 3571 RVA: 0x00044A69 File Offset: 0x00042C69
		public int totalUpdateCount { readonly get; set; }

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06000DF4 RID: 3572 RVA: 0x00044A72 File Offset: 0x00042C72
		// (set) Token: 0x06000DF5 RID: 3573 RVA: 0x00044A7A File Offset: 0x00042C7A
		public double totalEventProcessingTime { readonly get; set; }

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06000DF6 RID: 3574 RVA: 0x00044A83 File Offset: 0x00042C83
		// (set) Token: 0x06000DF7 RID: 3575 RVA: 0x00044A8B File Offset: 0x00042C8B
		public double totalEventLagTime { readonly get; set; }

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06000DF8 RID: 3576 RVA: 0x00044A94 File Offset: 0x00042C94
		public float averageEventBytesPerFrame
		{
			get
			{
				return (float)this.totalEventBytes / (float)this.totalUpdateCount;
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06000DF9 RID: 3577 RVA: 0x00044AA5 File Offset: 0x00042CA5
		public double averageProcessingTimePerEvent
		{
			get
			{
				return this.totalEventProcessingTime / (double)this.totalEventCount;
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06000DFA RID: 3578 RVA: 0x00044AB5 File Offset: 0x00042CB5
		public double averageLagTimePerEvent
		{
			get
			{
				return this.totalEventLagTime / (double)this.totalEventCount;
			}
		}
	}
}
