using System;

namespace Unity.SpriteShape.External.LibTessDotNet
{
	// Token: 0x02000010 RID: 16
	internal struct ContourVertex
	{
		// Token: 0x06000086 RID: 134 RVA: 0x00006478 File Offset: 0x00004678
		public override string ToString()
		{
			return string.Format("{0}, {1}", this.Position, this.Data);
		}

		// Token: 0x04000051 RID: 81
		public Vec3 Position;

		// Token: 0x04000052 RID: 82
		public object Data;
	}
}
