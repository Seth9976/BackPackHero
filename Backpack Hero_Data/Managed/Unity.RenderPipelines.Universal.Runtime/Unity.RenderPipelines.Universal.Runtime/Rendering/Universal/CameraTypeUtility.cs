using System;
using System.Linq;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000D0 RID: 208
	internal static class CameraTypeUtility
	{
		// Token: 0x060005C9 RID: 1481 RVA: 0x00020AE8 File Offset: 0x0001ECE8
		public static string GetName(this CameraRenderType type)
		{
			int num = (int)type;
			if (num < 0 || num >= CameraTypeUtility.s_CameraTypeNames.Length)
			{
				num = 0;
			}
			return CameraTypeUtility.s_CameraTypeNames[num];
		}

		// Token: 0x040004C3 RID: 1219
		private static string[] s_CameraTypeNames = Enum.GetNames(typeof(CameraRenderType)).ToArray<string>();
	}
}
