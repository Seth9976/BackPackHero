using System;

namespace UnityEngine.Search
{
	// Token: 0x020002D0 RID: 720
	[Flags]
	public enum SearchViewFlags
	{
		// Token: 0x040009B1 RID: 2481
		None = 0,
		// Token: 0x040009B2 RID: 2482
		Debug = 16,
		// Token: 0x040009B3 RID: 2483
		NoIndexing = 32,
		// Token: 0x040009B4 RID: 2484
		Packages = 256,
		// Token: 0x040009B5 RID: 2485
		OpenLeftSidePanel = 2048,
		// Token: 0x040009B6 RID: 2486
		OpenInspectorPreview = 4096,
		// Token: 0x040009B7 RID: 2487
		Centered = 8192,
		// Token: 0x040009B8 RID: 2488
		HideSearchBar = 16384,
		// Token: 0x040009B9 RID: 2489
		CompactView = 32768,
		// Token: 0x040009BA RID: 2490
		ListView = 65536,
		// Token: 0x040009BB RID: 2491
		GridView = 131072,
		// Token: 0x040009BC RID: 2492
		TableView = 262144,
		// Token: 0x040009BD RID: 2493
		EnableSearchQuery = 524288,
		// Token: 0x040009BE RID: 2494
		DisableInspectorPreview = 1048576,
		// Token: 0x040009BF RID: 2495
		DisableSavedSearchQuery = 2097152,
		// Token: 0x040009C0 RID: 2496
		OpenInBuilderMode = 4194304,
		// Token: 0x040009C1 RID: 2497
		OpenInTextMode = 8388608,
		// Token: 0x040009C2 RID: 2498
		DisableBuilderModeToggle = 16777216,
		// Token: 0x040009C3 RID: 2499
		Borderless = 33554432
	}
}
