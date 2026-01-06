using System;
using UnityEngine;

namespace Pathfinding.Util
{
	// Token: 0x0200026D RID: 621
	public class MutableGraphTransform : GraphTransform
	{
		// Token: 0x06000EB9 RID: 3769 RVA: 0x0005B1B0 File Offset: 0x000593B0
		public MutableGraphTransform(Matrix4x4 matrix)
			: base(matrix)
		{
		}

		// Token: 0x06000EBA RID: 3770 RVA: 0x0005B1B9 File Offset: 0x000593B9
		public void SetMatrix(Matrix4x4 matrix)
		{
			base.Set(matrix);
		}
	}
}
