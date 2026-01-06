using System;
using UnityEngine;

namespace PlayEveryWare.EpicOnlineServices
{
	// Token: 0x02000013 RID: 19
	public class EOSManagerPlatformSpecifics
	{
		// Token: 0x06000046 RID: 70 RVA: 0x00002634 File Offset: 0x00000834
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void InitOnPlayMode()
		{
			EOSManagerPlatformSpecifics.s_platformSpecifics = null;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x0000263C File Offset: 0x0000083C
		public static void SetEOSManagerPlatformSpecificsInterface(IEOSManagerPlatformSpecifics platformSpecifics)
		{
			if (EOSManagerPlatformSpecifics.s_platformSpecifics != null)
			{
				throw new Exception(string.Format("Trying to set the EOSManagerPlatformSpecifics twice: {0} => {1}", EOSManagerPlatformSpecifics.s_platformSpecifics.GetType().Name, (platformSpecifics == null) ? "NULL" : platformSpecifics.GetType().Name));
			}
			EOSManagerPlatformSpecifics.s_platformSpecifics = platformSpecifics;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000048 RID: 72 RVA: 0x0000268A File Offset: 0x0000088A
		public static IEOSManagerPlatformSpecifics Instance
		{
			get
			{
				return EOSManagerPlatformSpecifics.s_platformSpecifics;
			}
		}

		// Token: 0x0400002E RID: 46
		private static IEOSManagerPlatformSpecifics s_platformSpecifics;
	}
}
