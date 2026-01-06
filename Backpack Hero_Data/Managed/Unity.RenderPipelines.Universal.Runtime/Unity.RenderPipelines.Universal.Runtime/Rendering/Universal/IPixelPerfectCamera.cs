using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200002F RID: 47
	internal interface IPixelPerfectCamera
	{
		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060001B3 RID: 435
		// (set) Token: 0x060001B4 RID: 436
		int assetsPPU { get; set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060001B5 RID: 437
		// (set) Token: 0x060001B6 RID: 438
		int refResolutionX { get; set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060001B7 RID: 439
		// (set) Token: 0x060001B8 RID: 440
		int refResolutionY { get; set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060001B9 RID: 441
		// (set) Token: 0x060001BA RID: 442
		bool upscaleRT { get; set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060001BB RID: 443
		// (set) Token: 0x060001BC RID: 444
		bool pixelSnapping { get; set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001BD RID: 445
		// (set) Token: 0x060001BE RID: 446
		bool cropFrameX { get; set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001BF RID: 447
		// (set) Token: 0x060001C0 RID: 448
		bool cropFrameY { get; set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060001C1 RID: 449
		// (set) Token: 0x060001C2 RID: 450
		bool stretchFill { get; set; }
	}
}
