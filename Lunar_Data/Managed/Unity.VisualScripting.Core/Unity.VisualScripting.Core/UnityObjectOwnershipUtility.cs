using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x0200014B RID: 331
	public static class UnityObjectOwnershipUtility
	{
		// Token: 0x060008EB RID: 2283 RVA: 0x00026DBC File Offset: 0x00024FBC
		public static void CopyOwner(object source, object destination)
		{
			IUnityObjectOwnable unityObjectOwnable = destination as IUnityObjectOwnable;
			if (unityObjectOwnable != null)
			{
				unityObjectOwnable.owner = UnityObjectOwnershipUtility.GetOwner(source);
			}
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x00026DE0 File Offset: 0x00024FE0
		public static void RemoveOwner(object o)
		{
			IUnityObjectOwnable unityObjectOwnable = o as IUnityObjectOwnable;
			if (unityObjectOwnable != null)
			{
				unityObjectOwnable.owner = null;
			}
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x00026DFE File Offset: 0x00024FFE
		public static Object GetOwner(object o)
		{
			Component component = o as Component;
			GameObject gameObject;
			if ((gameObject = ((component != null) ? component.gameObject : null)) == null)
			{
				IUnityObjectOwnable unityObjectOwnable = o as IUnityObjectOwnable;
				if (unityObjectOwnable == null)
				{
					return null;
				}
				gameObject = unityObjectOwnable.owner;
			}
			return gameObject;
		}
	}
}
