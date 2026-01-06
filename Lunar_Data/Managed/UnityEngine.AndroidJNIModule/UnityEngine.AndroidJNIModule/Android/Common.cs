using System;

namespace UnityEngine.Android
{
	// Token: 0x02000019 RID: 25
	internal static class Common
	{
		// Token: 0x060001BC RID: 444 RVA: 0x000081CC File Offset: 0x000063CC
		public static AndroidJavaObject GetActivity()
		{
			bool flag = Common.m_Activity != null;
			AndroidJavaObject androidJavaObject;
			if (flag)
			{
				androidJavaObject = Common.m_Activity;
			}
			else
			{
				using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
				{
					Common.m_Activity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
				}
				androidJavaObject = Common.m_Activity;
			}
			return androidJavaObject;
		}

		// Token: 0x04000049 RID: 73
		private static AndroidJavaObject m_Activity;
	}
}
