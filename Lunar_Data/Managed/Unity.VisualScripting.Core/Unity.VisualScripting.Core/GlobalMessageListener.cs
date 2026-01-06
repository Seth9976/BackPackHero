using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x0200007D RID: 125
	[Singleton(Name = "VisualScripting GlobalEventListener", Automatic = true, Persistent = true)]
	[DisableAnnotation]
	[AddComponentMenu("")]
	[IncludeInSettings(false)]
	[TypeIcon(typeof(MessageListener))]
	public sealed class GlobalMessageListener : MonoBehaviour, ISingleton
	{
		// Token: 0x060003BC RID: 956 RVA: 0x000092E4 File Offset: 0x000074E4
		private void OnGUI()
		{
			EventBus.Trigger("OnGUI");
		}

		// Token: 0x060003BD RID: 957 RVA: 0x000092F5 File Offset: 0x000074F5
		private void OnApplicationFocus(bool focus)
		{
			if (focus)
			{
				EventBus.Trigger("OnApplicationFocus");
				return;
			}
			EventBus.Trigger("OnApplicationLostFocus");
		}

		// Token: 0x060003BE RID: 958 RVA: 0x00009319 File Offset: 0x00007519
		private void OnApplicationPause(bool paused)
		{
			if (paused)
			{
				EventBus.Trigger("OnApplicationPause");
				return;
			}
			EventBus.Trigger("OnApplicationResume");
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0000933D File Offset: 0x0000753D
		private void OnApplicationQuit()
		{
			EventBus.Trigger("OnApplicationQuit");
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0000934E File Offset: 0x0000754E
		public static void Require()
		{
			GlobalMessageListener instance = Singleton<GlobalMessageListener>.instance;
		}
	}
}
