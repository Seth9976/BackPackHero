using System;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x0200002E RID: 46
	[Serializable]
	internal struct MeshExtents
	{
		// Token: 0x0600013E RID: 318 RVA: 0x000181BB File Offset: 0x000163BB
		public MeshExtents(Vector2 min, Vector2 max)
		{
			this.min = min;
			this.max = max;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x000181CC File Offset: 0x000163CC
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"Min (",
				this.min.x.ToString("f2"),
				", ",
				this.min.y.ToString("f2"),
				")   Max (",
				this.max.x.ToString("f2"),
				", ",
				this.max.y.ToString("f2"),
				")"
			});
		}

		// Token: 0x04000212 RID: 530
		public Vector2 min;

		// Token: 0x04000213 RID: 531
		public Vector2 max;
	}
}
