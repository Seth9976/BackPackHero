using System;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000EB RID: 235
	[Serializable]
	public struct InputMetrics
	{
		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06000DDE RID: 3550 RVA: 0x00044991 File Offset: 0x00042B91
		// (set) Token: 0x06000DDF RID: 3551 RVA: 0x00044999 File Offset: 0x00042B99
		public int maxNumDevices { readonly get; set; }

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06000DE0 RID: 3552 RVA: 0x000449A2 File Offset: 0x00042BA2
		// (set) Token: 0x06000DE1 RID: 3553 RVA: 0x000449AA File Offset: 0x00042BAA
		public int currentNumDevices { readonly get; set; }

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06000DE2 RID: 3554 RVA: 0x000449B3 File Offset: 0x00042BB3
		// (set) Token: 0x06000DE3 RID: 3555 RVA: 0x000449BB File Offset: 0x00042BBB
		public int maxStateSizeInBytes { readonly get; set; }

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06000DE4 RID: 3556 RVA: 0x000449C4 File Offset: 0x00042BC4
		// (set) Token: 0x06000DE5 RID: 3557 RVA: 0x000449CC File Offset: 0x00042BCC
		public int currentStateSizeInBytes { readonly get; set; }

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06000DE6 RID: 3558 RVA: 0x000449D5 File Offset: 0x00042BD5
		// (set) Token: 0x06000DE7 RID: 3559 RVA: 0x000449DD File Offset: 0x00042BDD
		public int currentControlCount { readonly get; set; }

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06000DE8 RID: 3560 RVA: 0x000449E6 File Offset: 0x00042BE6
		// (set) Token: 0x06000DE9 RID: 3561 RVA: 0x000449EE File Offset: 0x00042BEE
		public int currentLayoutCount { readonly get; set; }

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06000DEA RID: 3562 RVA: 0x000449F7 File Offset: 0x00042BF7
		// (set) Token: 0x06000DEB RID: 3563 RVA: 0x000449FF File Offset: 0x00042BFF
		public int totalEventBytes { readonly get; set; }

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06000DEC RID: 3564 RVA: 0x00044A08 File Offset: 0x00042C08
		// (set) Token: 0x06000DED RID: 3565 RVA: 0x00044A10 File Offset: 0x00042C10
		public int totalEventCount { readonly get; set; }

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06000DEE RID: 3566 RVA: 0x00044A19 File Offset: 0x00042C19
		// (set) Token: 0x06000DEF RID: 3567 RVA: 0x00044A21 File Offset: 0x00042C21
		public int totalUpdateCount { readonly get; set; }

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06000DF0 RID: 3568 RVA: 0x00044A2A File Offset: 0x00042C2A
		// (set) Token: 0x06000DF1 RID: 3569 RVA: 0x00044A32 File Offset: 0x00042C32
		public double totalEventProcessingTime { readonly get; set; }

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06000DF2 RID: 3570 RVA: 0x00044A3B File Offset: 0x00042C3B
		// (set) Token: 0x06000DF3 RID: 3571 RVA: 0x00044A43 File Offset: 0x00042C43
		public double totalEventLagTime { readonly get; set; }

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06000DF4 RID: 3572 RVA: 0x00044A4C File Offset: 0x00042C4C
		public float averageEventBytesPerFrame
		{
			get
			{
				return (float)this.totalEventBytes / (float)this.totalUpdateCount;
			}
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06000DF5 RID: 3573 RVA: 0x00044A5D File Offset: 0x00042C5D
		public double averageProcessingTimePerEvent
		{
			get
			{
				return this.totalEventProcessingTime / (double)this.totalEventCount;
			}
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06000DF6 RID: 3574 RVA: 0x00044A6D File Offset: 0x00042C6D
		public double averageLagTimePerEvent
		{
			get
			{
				return this.totalEventLagTime / (double)this.totalEventCount;
			}
		}
	}
}
