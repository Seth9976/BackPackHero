using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020000B7 RID: 183
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class VolumeComponentMenu : Attribute
	{
		// Token: 0x06000617 RID: 1559 RVA: 0x0001CCC4 File Offset: 0x0001AEC4
		public VolumeComponentMenu(string menu)
		{
			this.menu = menu;
		}

		// Token: 0x04000392 RID: 914
		public readonly string menu;
	}
}
