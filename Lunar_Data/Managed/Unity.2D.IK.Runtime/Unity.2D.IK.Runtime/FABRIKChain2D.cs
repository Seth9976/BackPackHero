using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.U2D.IK
{
	// Token: 0x0200000B RID: 11
	[MovedFrom("UnityEngine.Experimental.U2D.IK")]
	public struct FABRIKChain2D
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000063 RID: 99 RVA: 0x000031D8 File Offset: 0x000013D8
		public Vector2 first
		{
			get
			{
				return this.positions[0];
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000064 RID: 100 RVA: 0x000031E6 File Offset: 0x000013E6
		public Vector2 last
		{
			get
			{
				Vector2[] array = this.positions;
				return array[array.Length - 1];
			}
		}

		// Token: 0x04000027 RID: 39
		public Vector2 origin;

		// Token: 0x04000028 RID: 40
		public Vector2 target;

		// Token: 0x04000029 RID: 41
		public float sqrTolerance;

		// Token: 0x0400002A RID: 42
		public Vector2[] positions;

		// Token: 0x0400002B RID: 43
		public float[] lengths;

		// Token: 0x0400002C RID: 44
		public int[] subChainIndices;

		// Token: 0x0400002D RID: 45
		public Vector3[] worldPositions;
	}
}
