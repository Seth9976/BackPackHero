using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000162 RID: 354
	public static class RuntimeVSUsageUtility
	{
		// Token: 0x06000961 RID: 2401 RVA: 0x00028594 File Offset: 0x00026794
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void RuntimeInitializeOnLoadBeforeSceneLoad()
		{
			UnityThread.RuntimeInitialize();
			Ensure.OnRuntimeMethodLoad();
			Recursion.OnRuntimeMethodLoad();
			OptimizedReflection.OnRuntimeMethodLoad();
			SavedVariables.OnEnterPlayMode();
			ApplicationVariables.OnEnterPlayMode();
			ReferenceCollector.Initialize();
		}
	}
}
