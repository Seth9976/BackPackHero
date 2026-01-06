using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.SceneManagement
{
	// Token: 0x020002E9 RID: 745
	[NativeHeader("Runtime/Export/SceneManager/SceneUtility.bindings.h")]
	public static class SceneUtility
	{
		// Token: 0x06001E73 RID: 7795
		[StaticAccessor("SceneUtilityBindings", StaticAccessorType.DoubleColon)]
		[MethodImpl(4096)]
		public static extern string GetScenePathByBuildIndex(int buildIndex);

		// Token: 0x06001E74 RID: 7796
		[StaticAccessor("SceneUtilityBindings", StaticAccessorType.DoubleColon)]
		[MethodImpl(4096)]
		public static extern int GetBuildIndexByScenePath(string scenePath);
	}
}
