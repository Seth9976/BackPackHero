using System;

namespace UnityEngine.UI
{
	// Token: 0x0200002D RID: 45
	internal static class Misc
	{
		// Token: 0x06000301 RID: 769 RVA: 0x0000FDC2 File Offset: 0x0000DFC2
		public static void Destroy(Object obj)
		{
			if (obj != null)
			{
				if (Application.isPlaying)
				{
					if (obj is GameObject)
					{
						(obj as GameObject).transform.parent = null;
					}
					Object.Destroy(obj);
					return;
				}
				Object.DestroyImmediate(obj);
			}
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000FDFA File Offset: 0x0000DFFA
		public static void DestroyImmediate(Object obj)
		{
			if (obj != null)
			{
				if (Application.isEditor)
				{
					Object.DestroyImmediate(obj);
					return;
				}
				Object.Destroy(obj);
			}
		}
	}
}
