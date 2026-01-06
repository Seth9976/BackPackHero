using System;
using System.Collections;
using UnityEngine;

namespace PlayEveryWare.EpicOnlineServices
{
	// Token: 0x0200000B RID: 11
	public class EOSManager : MonoBehaviour, IEOSCoroutineOwner
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600002F RID: 47 RVA: 0x0000249C File Offset: 0x0000069C
		public static EOSManager.EOSSingleton Instance
		{
			get
			{
				if (EOSManager.s_instance == null)
				{
					EOSManager.s_instance = new EOSManager.EOSSingleton();
				}
				return EOSManager.s_instance;
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000024B4 File Offset: 0x000006B4
		void IEOSCoroutineOwner.StartCoroutine(IEnumerator routine)
		{
			base.StartCoroutine(routine);
		}

		// Token: 0x04000025 RID: 37
		public bool InitializeOnAwake = true;

		// Token: 0x04000026 RID: 38
		public bool ShouldShutdownOnApplicationQuit = true;

		// Token: 0x04000027 RID: 39
		private static EOSManager.EOSSingleton s_instance;

		// Token: 0x02000024 RID: 36
		public class EOSSingleton
		{
		}
	}
}
