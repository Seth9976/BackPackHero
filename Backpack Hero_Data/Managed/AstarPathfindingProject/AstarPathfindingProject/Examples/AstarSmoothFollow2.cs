using System;
using UnityEngine;

namespace Pathfinding.Examples
{
	// Token: 0x020000DE RID: 222
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_examples_1_1_astar_smooth_follow2.php")]
	public class AstarSmoothFollow2 : MonoBehaviour
	{
		// Token: 0x060009A3 RID: 2467 RVA: 0x0003EBB8 File Offset: 0x0003CDB8
		private void LateUpdate()
		{
			Vector3 vector;
			if (this.staticOffset)
			{
				vector = this.target.position + new Vector3(0f, this.height, this.distance);
			}
			else if (this.followBehind)
			{
				vector = this.target.TransformPoint(0f, this.height, -this.distance);
			}
			else
			{
				vector = this.target.TransformPoint(0f, this.height, this.distance);
			}
			base.transform.position = Vector3.Lerp(base.transform.position, vector, Time.deltaTime * this.damping);
			if (this.smoothRotation)
			{
				Quaternion quaternion = Quaternion.LookRotation(this.target.position - base.transform.position, this.target.up);
				base.transform.rotation = Quaternion.Slerp(base.transform.rotation, quaternion, Time.deltaTime * this.rotationDamping);
				return;
			}
			base.transform.LookAt(this.target, this.target.up);
		}

		// Token: 0x040005BD RID: 1469
		public Transform target;

		// Token: 0x040005BE RID: 1470
		public float distance = 3f;

		// Token: 0x040005BF RID: 1471
		public float height = 3f;

		// Token: 0x040005C0 RID: 1472
		public float damping = 5f;

		// Token: 0x040005C1 RID: 1473
		public bool smoothRotation = true;

		// Token: 0x040005C2 RID: 1474
		public bool followBehind = true;

		// Token: 0x040005C3 RID: 1475
		public float rotationDamping = 10f;

		// Token: 0x040005C4 RID: 1476
		public bool staticOffset;
	}
}
