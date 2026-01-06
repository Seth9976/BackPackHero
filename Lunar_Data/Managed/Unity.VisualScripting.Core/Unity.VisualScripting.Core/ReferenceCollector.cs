using System;
using UnityEngine.SceneManagement;

namespace Unity.VisualScripting
{
	// Token: 0x0200015F RID: 351
	public static class ReferenceCollector
	{
		// Token: 0x14000013 RID: 19
		// (add) Token: 0x06000954 RID: 2388 RVA: 0x000284A4 File Offset: 0x000266A4
		// (remove) Token: 0x06000955 RID: 2389 RVA: 0x000284D8 File Offset: 0x000266D8
		public static event Action onSceneUnloaded;

		// Token: 0x06000956 RID: 2390 RVA: 0x0002850B File Offset: 0x0002670B
		internal static void Initialize()
		{
			SceneManager.sceneUnloaded += delegate(Scene scene)
			{
				Action action = ReferenceCollector.onSceneUnloaded;
				if (action == null)
				{
					return;
				}
				action();
			};
		}
	}
}
