using System;
using UnityEngine;

namespace Pathfinding.RVO
{
	// Token: 0x020000DA RID: 218
	[AddComponentMenu("Pathfinding/Local Avoidance/Square Obstacle")]
	[HelpURL("http://arongranberg.com/astar/documentation/stable/class_pathfinding_1_1_r_v_o_1_1_r_v_o_square_obstacle.php")]
	public class RVOSquareObstacle : RVOObstacle
	{
		// Token: 0x1700014D RID: 333
		// (get) Token: 0x0600095E RID: 2398 RVA: 0x0003D6FC File Offset: 0x0003B8FC
		protected override bool StaticObstacle
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x0600095F RID: 2399 RVA: 0x0003D6FF File Offset: 0x0003B8FF
		protected override bool ExecuteInEditor
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000960 RID: 2400 RVA: 0x0003D702 File Offset: 0x0003B902
		protected override bool LocalCoordinates
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000961 RID: 2401 RVA: 0x0003D705 File Offset: 0x0003B905
		protected override float Height
		{
			get
			{
				return this.height;
			}
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x0003D70D File Offset: 0x0003B90D
		protected override bool AreGizmosDirty()
		{
			return false;
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x0003D710 File Offset: 0x0003B910
		protected override void CreateObstacles()
		{
			this.size.x = Mathf.Abs(this.size.x);
			this.size.y = Mathf.Abs(this.size.y);
			this.height = Mathf.Abs(this.height);
			Vector3[] array = new Vector3[]
			{
				new Vector3(1f, 0f, -1f),
				new Vector3(1f, 0f, 1f),
				new Vector3(-1f, 0f, 1f),
				new Vector3(-1f, 0f, -1f)
			};
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Scale(new Vector3(this.size.x * 0.5f, 0f, this.size.y * 0.5f));
				array[i] += new Vector3(this.center.x, 0f, this.center.y);
			}
			base.AddObstacle(array, this.height);
		}

		// Token: 0x0400057E RID: 1406
		public float height = 1f;

		// Token: 0x0400057F RID: 1407
		public Vector2 size = Vector3.one;

		// Token: 0x04000580 RID: 1408
		public Vector2 center = Vector3.zero;
	}
}
