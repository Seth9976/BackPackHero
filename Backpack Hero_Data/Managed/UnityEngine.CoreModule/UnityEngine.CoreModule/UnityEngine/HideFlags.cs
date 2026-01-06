using System;

namespace UnityEngine
{
	// Token: 0x02000223 RID: 547
	[Flags]
	public enum HideFlags
	{
		// Token: 0x04000813 RID: 2067
		None = 0,
		// Token: 0x04000814 RID: 2068
		HideInHierarchy = 1,
		// Token: 0x04000815 RID: 2069
		HideInInspector = 2,
		// Token: 0x04000816 RID: 2070
		DontSaveInEditor = 4,
		// Token: 0x04000817 RID: 2071
		NotEditable = 8,
		// Token: 0x04000818 RID: 2072
		DontSaveInBuild = 16,
		// Token: 0x04000819 RID: 2073
		DontUnloadUnusedAsset = 32,
		// Token: 0x0400081A RID: 2074
		DontSave = 52,
		// Token: 0x0400081B RID: 2075
		HideAndDontSave = 61
	}
}
