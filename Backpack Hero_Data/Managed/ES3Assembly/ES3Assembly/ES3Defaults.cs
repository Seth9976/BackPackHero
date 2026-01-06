using System;
using UnityEngine;

// Token: 0x0200000E RID: 14
public class ES3Defaults : ScriptableObject
{
	// Token: 0x04000022 RID: 34
	[SerializeField]
	public ES3SerializableSettings settings = new ES3SerializableSettings();

	// Token: 0x04000023 RID: 35
	public bool addMgrToSceneAutomatically;

	// Token: 0x04000024 RID: 36
	public bool autoUpdateReferences = true;

	// Token: 0x04000025 RID: 37
	public bool addAllPrefabsToManager = true;

	// Token: 0x04000026 RID: 38
	public bool logDebugInfo;

	// Token: 0x04000027 RID: 39
	public bool logWarnings = true;

	// Token: 0x04000028 RID: 40
	public bool logErrors = true;
}
