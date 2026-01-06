using System;

namespace UnityEngine.NVIDIA
{
	// Token: 0x02000009 RID: 9
	public struct DLSSTextureTable
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001C RID: 28 RVA: 0x0000221E File Offset: 0x0000041E
		// (set) Token: 0x0600001B RID: 27 RVA: 0x00002215 File Offset: 0x00000415
		public Texture colorInput { readonly get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001E RID: 30 RVA: 0x0000222F File Offset: 0x0000042F
		// (set) Token: 0x0600001D RID: 29 RVA: 0x00002226 File Offset: 0x00000426
		public Texture colorOutput { readonly get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002240 File Offset: 0x00000440
		// (set) Token: 0x0600001F RID: 31 RVA: 0x00002237 File Offset: 0x00000437
		public Texture depth { readonly get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002251 File Offset: 0x00000451
		// (set) Token: 0x06000021 RID: 33 RVA: 0x00002248 File Offset: 0x00000448
		public Texture motionVectors { readonly get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002262 File Offset: 0x00000462
		// (set) Token: 0x06000023 RID: 35 RVA: 0x00002259 File Offset: 0x00000459
		public Texture transparencyMask { readonly get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002273 File Offset: 0x00000473
		// (set) Token: 0x06000025 RID: 37 RVA: 0x0000226A File Offset: 0x0000046A
		public Texture exposureTexture { readonly get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002284 File Offset: 0x00000484
		// (set) Token: 0x06000027 RID: 39 RVA: 0x0000227B File Offset: 0x0000047B
		public Texture biasColorMask { readonly get; set; }
	}
}
