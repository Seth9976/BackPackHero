using System;
using System.Linq;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000054 RID: 84
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_target_mover.php")]
	public class TargetMover : MonoBehaviour
	{
		// Token: 0x06000419 RID: 1049 RVA: 0x00014B00 File Offset: 0x00012D00
		public void Start()
		{
			this.cam = Camera.main;
			this.ais = Object.FindObjectsOfType<MonoBehaviour>().OfType<IAstarAI>().ToArray<IAstarAI>();
			base.useGUILayout = false;
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x00014B29 File Offset: 0x00012D29
		public void OnGUI()
		{
			if (this.onlyOnDoubleClick && this.cam != null && Event.current.type == EventType.MouseDown && Event.current.clickCount == 2)
			{
				this.UpdateTargetPosition();
			}
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x00014B60 File Offset: 0x00012D60
		private void Update()
		{
			if (!this.onlyOnDoubleClick && this.cam != null)
			{
				this.UpdateTargetPosition();
			}
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x00014B80 File Offset: 0x00012D80
		public void UpdateTargetPosition()
		{
			Vector3 vector = Vector3.zero;
			bool flag = false;
			RaycastHit raycastHit;
			if (this.use2D)
			{
				vector = this.cam.ScreenToWorldPoint(Input.mousePosition);
				vector.z = 0f;
				flag = true;
			}
			else if (Physics.Raycast(this.cam.ScreenPointToRay(Input.mousePosition), out raycastHit, float.PositiveInfinity, this.mask))
			{
				vector = raycastHit.point;
				flag = true;
			}
			if (flag && vector != this.target.position)
			{
				this.target.position = vector;
				if (this.onlyOnDoubleClick)
				{
					for (int i = 0; i < this.ais.Length; i++)
					{
						if (this.ais[i] != null)
						{
							this.ais[i].SearchPath();
						}
					}
				}
			}
		}

		// Token: 0x04000275 RID: 629
		public LayerMask mask;

		// Token: 0x04000276 RID: 630
		public Transform target;

		// Token: 0x04000277 RID: 631
		private IAstarAI[] ais;

		// Token: 0x04000278 RID: 632
		public bool onlyOnDoubleClick;

		// Token: 0x04000279 RID: 633
		public bool use2D;

		// Token: 0x0400027A RID: 634
		private Camera cam;
	}
}
