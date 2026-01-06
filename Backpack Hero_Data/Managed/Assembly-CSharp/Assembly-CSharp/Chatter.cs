using System;

// Token: 0x02000175 RID: 373
public struct Chatter
{
	// Token: 0x1700002B RID: 43
	// (get) Token: 0x06000F13 RID: 3859 RVA: 0x0009482A File Offset: 0x00092A2A
	// (set) Token: 0x06000F14 RID: 3860 RVA: 0x00094832 File Offset: 0x00092A32
	public string userName { readonly get; set; }

	// Token: 0x1700002C RID: 44
	// (get) Token: 0x06000F15 RID: 3861 RVA: 0x0009483B File Offset: 0x00092A3B
	// (set) Token: 0x06000F16 RID: 3862 RVA: 0x00094843 File Offset: 0x00092A43
	public string displayName { readonly get; set; }

	// Token: 0x1700002D RID: 45
	// (get) Token: 0x06000F17 RID: 3863 RVA: 0x0009484C File Offset: 0x00092A4C
	// (set) Token: 0x06000F18 RID: 3864 RVA: 0x00094854 File Offset: 0x00092A54
	public string color { readonly get; set; }

	// Token: 0x1700002E RID: 46
	// (get) Token: 0x06000F19 RID: 3865 RVA: 0x0009485D File Offset: 0x00092A5D
	// (set) Token: 0x06000F1A RID: 3866 RVA: 0x00094865 File Offset: 0x00092A65
	public bool isSubscriber { readonly get; set; }

	// Token: 0x1700002F RID: 47
	// (get) Token: 0x06000F1B RID: 3867 RVA: 0x0009486E File Offset: 0x00092A6E
	// (set) Token: 0x06000F1C RID: 3868 RVA: 0x00094876 File Offset: 0x00092A76
	public bool isModerator { readonly get; set; }

	// Token: 0x17000030 RID: 48
	// (get) Token: 0x06000F1D RID: 3869 RVA: 0x0009487F File Offset: 0x00092A7F
	// (set) Token: 0x06000F1E RID: 3870 RVA: 0x00094887 File Offset: 0x00092A87
	public bool isVip { readonly get; set; }

	// Token: 0x17000031 RID: 49
	// (get) Token: 0x06000F1F RID: 3871 RVA: 0x00094890 File Offset: 0x00092A90
	// (set) Token: 0x06000F20 RID: 3872 RVA: 0x00094898 File Offset: 0x00092A98
	public string lastMessageTimestamp { readonly get; set; }
}
