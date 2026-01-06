using System;

namespace Febucci.UI.Core
{
	// Token: 0x0200003A RID: 58
	public class TypingInfo
	{
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000155 RID: 341 RVA: 0x000069F2 File Offset: 0x00004BF2
		// (set) Token: 0x06000156 RID: 342 RVA: 0x000069FA File Offset: 0x00004BFA
		public float timePassed { get; internal set; }

		// Token: 0x06000157 RID: 343 RVA: 0x00006A03 File Offset: 0x00004C03
		public TypingInfo()
		{
			this.speed = 1f;
			this.timePassed = 0f;
		}

		// Token: 0x040000E4 RID: 228
		public float speed = 1f;
	}
}
