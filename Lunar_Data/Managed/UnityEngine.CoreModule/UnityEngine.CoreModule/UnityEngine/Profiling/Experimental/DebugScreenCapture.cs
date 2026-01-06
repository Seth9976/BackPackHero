using System;
using Unity.Collections;

namespace UnityEngine.Profiling.Experimental
{
	// Token: 0x0200027B RID: 635
	public struct DebugScreenCapture
	{
		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x06001BB8 RID: 7096 RVA: 0x0002C74F File Offset: 0x0002A94F
		// (set) Token: 0x06001BB9 RID: 7097 RVA: 0x0002C757 File Offset: 0x0002A957
		public NativeArray<byte> rawImageDataReference { readonly get; set; }

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x06001BBA RID: 7098 RVA: 0x0002C760 File Offset: 0x0002A960
		// (set) Token: 0x06001BBB RID: 7099 RVA: 0x0002C768 File Offset: 0x0002A968
		public TextureFormat imageFormat { readonly get; set; }

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x06001BBC RID: 7100 RVA: 0x0002C771 File Offset: 0x0002A971
		// (set) Token: 0x06001BBD RID: 7101 RVA: 0x0002C779 File Offset: 0x0002A979
		public int width { readonly get; set; }

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x06001BBE RID: 7102 RVA: 0x0002C782 File Offset: 0x0002A982
		// (set) Token: 0x06001BBF RID: 7103 RVA: 0x0002C78A File Offset: 0x0002A98A
		public int height { readonly get; set; }
	}
}
