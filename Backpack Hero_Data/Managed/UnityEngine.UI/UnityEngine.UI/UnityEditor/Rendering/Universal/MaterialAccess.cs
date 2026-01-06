using System;
using UnityEngine;

namespace UnityEditor.Rendering.Universal
{
	// Token: 0x02000075 RID: 117
	internal static class MaterialAccess
	{
		// Token: 0x0600068B RID: 1675 RVA: 0x0001BA0E File Offset: 0x00019C0E
		internal static int ReadMaterialRawRenderQueue(Material mat)
		{
			return mat.rawRenderQueue;
		}
	}
}
