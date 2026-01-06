using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000175 RID: 373
	[Singleton(Name = "VisualScripting SavedVariablesSerializer", Automatic = true, Persistent = true)]
	[AddComponentMenu("")]
	[DisableAnnotation]
	[IncludeInSettings(false)]
	public class VariablesSaver : MonoBehaviour, ISingleton
	{
		// Token: 0x060009F8 RID: 2552 RVA: 0x0002991D File Offset: 0x00027B1D
		private void Awake()
		{
			Singleton<VariablesSaver>.Awake(this);
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x00029925 File Offset: 0x00027B25
		private void OnDestroy()
		{
			Singleton<VariablesSaver>.OnDestroy(this);
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x0002992D File Offset: 0x00027B2D
		private void OnApplicationQuit()
		{
			SavedVariables.OnExitPlayMode();
			ApplicationVariables.OnExitPlayMode();
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x00029939 File Offset: 0x00027B39
		private void OnApplicationPause(bool isPaused)
		{
			if (!isPaused)
			{
				return;
			}
			SavedVariables.OnExitPlayMode();
			ApplicationVariables.OnExitPlayMode();
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060009FC RID: 2556 RVA: 0x00029949 File Offset: 0x00027B49
		public static VariablesSaver instance
		{
			get
			{
				return Singleton<VariablesSaver>.instance;
			}
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x00029950 File Offset: 0x00027B50
		public static void Instantiate()
		{
			Singleton<VariablesSaver>.Instantiate();
		}
	}
}
