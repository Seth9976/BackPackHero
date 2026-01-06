using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x0200014E RID: 334
	[Singleton(Name = "VisualScripting CoroutineRunner", Automatic = true, Persistent = true)]
	[AddComponentMenu("")]
	[DisableAnnotation]
	[IncludeInSettings(false)]
	public sealed class CoroutineRunner : MonoBehaviour, ISingleton
	{
		// Token: 0x06000903 RID: 2307 RVA: 0x0002717C File Offset: 0x0002537C
		private void Awake()
		{
			Singleton<CoroutineRunner>.Awake(this);
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x00027184 File Offset: 0x00025384
		private void OnDestroy()
		{
			base.StopAllCoroutines();
			Singleton<CoroutineRunner>.OnDestroy(this);
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000905 RID: 2309 RVA: 0x00027192 File Offset: 0x00025392
		public static CoroutineRunner instance
		{
			get
			{
				return Singleton<CoroutineRunner>.instance;
			}
		}
	}
}
