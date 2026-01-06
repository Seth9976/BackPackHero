using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000027 RID: 39
	public readonly struct NNInfo
	{
		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x00009B72 File Offset: 0x00007D72
		[Obsolete("This field has been renamed to 'position'")]
		public Vector3 clampedPosition
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00009B7A File Offset: 0x00007D7A
		[MethodImpl(256)]
		public NNInfo(GraphNode node, Vector3 position, float distanceCostSqr)
		{
			this.node = node;
			if (node == null)
			{
				this.position = Vector3.positiveInfinity;
				this.distanceCostSqr = float.PositiveInfinity;
				return;
			}
			this.position = position;
			this.distanceCostSqr = distanceCostSqr;
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00009B72 File Offset: 0x00007D72
		public static explicit operator Vector3(NNInfo ob)
		{
			return ob.position;
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00009BAB File Offset: 0x00007DAB
		public static explicit operator GraphNode(NNInfo ob)
		{
			return ob.node;
		}

		// Token: 0x0400013B RID: 315
		public readonly GraphNode node;

		// Token: 0x0400013C RID: 316
		public readonly Vector3 position;

		// Token: 0x0400013D RID: 317
		public readonly float distanceCostSqr;

		// Token: 0x0400013E RID: 318
		public static readonly NNInfo Empty = new NNInfo(null, Vector3.positiveInfinity, float.PositiveInfinity);
	}
}
