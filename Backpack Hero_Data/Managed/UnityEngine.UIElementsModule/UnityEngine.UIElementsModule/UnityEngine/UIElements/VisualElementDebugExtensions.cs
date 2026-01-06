using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020000E5 RID: 229
	internal static class VisualElementDebugExtensions
	{
		// Token: 0x06000741 RID: 1857 RVA: 0x0001A520 File Offset: 0x00018720
		public static string GetDisplayName(this VisualElement ve, bool withHashCode = true)
		{
			bool flag = ve == null;
			string text;
			if (flag)
			{
				text = string.Empty;
			}
			else
			{
				string text2 = ve.GetType().Name;
				bool flag2 = !string.IsNullOrEmpty(ve.name);
				if (flag2)
				{
					text2 = text2 + "#" + ve.name;
				}
				if (withHashCode)
				{
					text2 = text2 + " (" + ve.GetHashCode().ToString("x8") + ")";
				}
				text = text2;
			}
			return text;
		}
	}
}
