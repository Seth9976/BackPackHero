using System;
using System.Drawing;

namespace TwitchLib.Client.Models.Extensions.NetCore
{
	// Token: 0x02000029 RID: 41
	public static class ColorTranslator
	{
		// Token: 0x06000177 RID: 375 RVA: 0x00007A38 File Offset: 0x00005C38
		public static Color FromHtml(string hexColor)
		{
			hexColor += 0.ToString();
			return Color.FromArgb(int.Parse(hexColor.Replace("#", ""), 515));
		}
	}
}
