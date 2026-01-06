using System;
using UnityEngine;

namespace Pathfinding.RVO
{
	// Token: 0x020002AD RID: 685
	[AddComponentMenu("")]
	[HelpURL("https://arongranberg.com/astar/documentation/stable/rvosquareobstacle.html")]
	public class RVOSquareObstacle : RVOObstacle
	{
		// Token: 0x1700025F RID: 607
		// (get) Token: 0x0600104F RID: 4175 RVA: 0x00018013 File Offset: 0x00016213
		protected override bool StaticObstacle
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06001050 RID: 4176 RVA: 0x00016F22 File Offset: 0x00015122
		protected override bool ExecuteInEditor
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06001051 RID: 4177 RVA: 0x00016F22 File Offset: 0x00015122
		protected override bool LocalCoordinates
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06001052 RID: 4178 RVA: 0x000665F3 File Offset: 0x000647F3
		protected override float Height
		{
			get
			{
				return this.height;
			}
		}

		// Token: 0x04000C76 RID: 3190
		public float height = 1f;

		// Token: 0x04000C77 RID: 3191
		public Vector2 size = Vector3.one;

		// Token: 0x04000C78 RID: 3192
		public Vector2 center = Vector3.zero;
	}
}
