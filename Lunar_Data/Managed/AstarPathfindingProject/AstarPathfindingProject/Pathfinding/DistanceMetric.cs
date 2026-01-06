using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000024 RID: 36
	public struct DistanceMetric
	{
		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x0000991E File Offset: 0x00007B1E
		public bool isProjectedDistance
		{
			get
			{
				return this.projectionAxis != Vector3.zero;
			}
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00009930 File Offset: 0x00007B30
		public static DistanceMetric ClosestAsSeenFromAboveSoft()
		{
			return new DistanceMetric
			{
				projectionAxis = Vector3.positiveInfinity,
				distanceScaleAlongProjectionDirection = 0.2f
			};
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00009960 File Offset: 0x00007B60
		public static DistanceMetric ClosestAsSeenFromAboveSoft(Vector3 up)
		{
			return new DistanceMetric
			{
				projectionAxis = up,
				distanceScaleAlongProjectionDirection = 0.2f
			};
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000998C File Offset: 0x00007B8C
		public static DistanceMetric ClosestAsSeenFromAbove()
		{
			return new DistanceMetric
			{
				projectionAxis = Vector3.positiveInfinity,
				distanceScaleAlongProjectionDirection = 0f
			};
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x000099BC File Offset: 0x00007BBC
		public static DistanceMetric ClosestAsSeenFromAbove(Vector3 up)
		{
			return new DistanceMetric
			{
				projectionAxis = up,
				distanceScaleAlongProjectionDirection = 0f
			};
		}

		// Token: 0x0400012F RID: 303
		public Vector3 projectionAxis;

		// Token: 0x04000130 RID: 304
		public float distanceScaleAlongProjectionDirection;

		// Token: 0x04000131 RID: 305
		public static readonly DistanceMetric Euclidean = new DistanceMetric
		{
			projectionAxis = Vector3.zero,
			distanceScaleAlongProjectionDirection = 0f
		};
	}
}
