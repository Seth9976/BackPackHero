using System;
using UnityEngine;

namespace Extensions
{
	// Token: 0x020001AD RID: 429
	public static class ExtensionMethods
	{
		// Token: 0x06001102 RID: 4354 RVA: 0x000A0FC0 File Offset: 0x0009F1C0
		public static GameObject FindRecursive(this GameObject parent, string name)
		{
			Transform transform = parent.transform.Find(name);
			GameObject gameObject = ((transform != null) ? transform.gameObject : null);
			if (gameObject != null)
			{
				return gameObject;
			}
			foreach (object obj in parent.transform)
			{
				gameObject = ((Transform)obj).gameObject.FindRecursive(name);
				if (gameObject != null)
				{
					return gameObject;
				}
			}
			return null;
		}
	}
}
