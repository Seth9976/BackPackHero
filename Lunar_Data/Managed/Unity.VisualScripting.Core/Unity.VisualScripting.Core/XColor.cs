using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000165 RID: 357
	public static class XColor
	{
		// Token: 0x06000990 RID: 2448 RVA: 0x00028F14 File Offset: 0x00027114
		public static string ToHexString(this Color color)
		{
			return ((byte)(color.r * 255f)).ToString("X2") + ((byte)(color.g * 255f)).ToString("X2") + ((byte)(color.b * 255f)).ToString("X2") + ((byte)(color.a * 255f)).ToString("X2");
		}
	}
}
