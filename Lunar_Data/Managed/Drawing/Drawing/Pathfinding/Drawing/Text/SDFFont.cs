using System;
using UnityEngine;

namespace Pathfinding.Drawing.Text
{
	// Token: 0x02000059 RID: 89
	internal struct SDFFont
	{
		// Token: 0x0400016D RID: 365
		public string name;

		// Token: 0x0400016E RID: 366
		public int size;

		// Token: 0x0400016F RID: 367
		public int width;

		// Token: 0x04000170 RID: 368
		public int height;

		// Token: 0x04000171 RID: 369
		public bool bold;

		// Token: 0x04000172 RID: 370
		public bool italic;

		// Token: 0x04000173 RID: 371
		public SDFCharacter[] characters;

		// Token: 0x04000174 RID: 372
		public Material material;
	}
}
