using System;
using UnityEngine;

namespace Unity.Services.Analytics
{
	// Token: 0x0200001C RID: 28
	[Obsolete("Should not be public. Do not use this, it will be removed in an upcoming version.")]
	public static class ContainerObject
	{
		// Token: 0x06000062 RID: 98 RVA: 0x00003619 File Offset: 0x00001819
		internal static void Initialize()
		{
			if (!ContainerObject.s_Created)
			{
				ContainerObject.s_Container = new GameObject("AnalyticsContainer");
				ContainerObject.s_Container.AddComponent<AnalyticsLifetime>();
				ContainerObject.s_Created = true;
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003642 File Offset: 0x00001842
		[Obsolete("Should not be public. Do not use this, it will be removed in an upcoming version.")]
		public static void DestroyContainer()
		{
			Object.Destroy(ContainerObject.s_Container);
			ContainerObject.s_Created = false;
		}

		// Token: 0x04000096 RID: 150
		private static bool s_Created;

		// Token: 0x04000097 RID: 151
		private static GameObject s_Container;
	}
}
