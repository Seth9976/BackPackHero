using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000072 RID: 114
	public interface IGraphNest : IAotStubbable
	{
		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000390 RID: 912
		// (set) Token: 0x06000391 RID: 913
		IGraphNester nester { get; set; }

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000392 RID: 914
		// (set) Token: 0x06000393 RID: 915
		GraphSource source { get; set; }

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000394 RID: 916
		// (set) Token: 0x06000395 RID: 917
		IGraph embed { get; set; }

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000396 RID: 918
		// (set) Token: 0x06000397 RID: 919
		IMacro macro { get; set; }

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000398 RID: 920
		IGraph graph { get; }

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000399 RID: 921
		Type graphType { get; }

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600039A RID: 922
		Type macroType { get; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x0600039B RID: 923
		bool hasBackgroundEmbed { get; }
	}
}
