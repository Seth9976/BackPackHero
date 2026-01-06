using System;

namespace UnityEngine.U2D
{
	// Token: 0x02000004 RID: 4
	internal interface IPixelPerfectCamera
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001F RID: 31
		// (set) Token: 0x06000020 RID: 32
		int assetsPPU { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000021 RID: 33
		// (set) Token: 0x06000022 RID: 34
		int refResolutionX { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000023 RID: 35
		// (set) Token: 0x06000024 RID: 36
		int refResolutionY { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000025 RID: 37
		// (set) Token: 0x06000026 RID: 38
		bool upscaleRT { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000027 RID: 39
		// (set) Token: 0x06000028 RID: 40
		bool pixelSnapping { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000029 RID: 41
		// (set) Token: 0x0600002A RID: 42
		bool cropFrameX { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600002B RID: 43
		// (set) Token: 0x0600002C RID: 44
		bool cropFrameY { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600002D RID: 45
		// (set) Token: 0x0600002E RID: 46
		bool stretchFill { get; set; }
	}
}
