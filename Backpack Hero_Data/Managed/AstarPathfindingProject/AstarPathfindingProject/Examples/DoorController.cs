using System;
using UnityEngine;

namespace Pathfinding.Examples
{
	// Token: 0x020000DF RID: 223
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_examples_1_1_door_controller.php")]
	public class DoorController : MonoBehaviour
	{
		// Token: 0x060009A5 RID: 2469 RVA: 0x0003ED29 File Offset: 0x0003CF29
		public void Start()
		{
			this.bounds = base.GetComponent<Collider>().bounds;
			this.SetState(this.open);
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x0003ED48 File Offset: 0x0003CF48
		private void OnGUI()
		{
			if (GUI.Button(new Rect(5f, this.yOffset, 100f, 22f), "Toggle Door"))
			{
				this.SetState(!this.open);
			}
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x0003ED80 File Offset: 0x0003CF80
		public void SetState(bool open)
		{
			this.open = open;
			if (this.updateGraphsWithGUO)
			{
				GraphUpdateObject graphUpdateObject = new GraphUpdateObject(this.bounds);
				int num = (open ? this.opentag : this.closedtag);
				if (num > 31)
				{
					Debug.LogError("tag > 31");
					return;
				}
				graphUpdateObject.modifyTag = true;
				graphUpdateObject.setTag = num;
				graphUpdateObject.updatePhysics = false;
				AstarPath.active.UpdateGraphs(graphUpdateObject);
			}
			if (open)
			{
				base.GetComponent<Animation>().Play("Open");
				return;
			}
			base.GetComponent<Animation>().Play("Close");
		}

		// Token: 0x040005C5 RID: 1477
		private bool open;

		// Token: 0x040005C6 RID: 1478
		public int opentag = 1;

		// Token: 0x040005C7 RID: 1479
		public int closedtag = 1;

		// Token: 0x040005C8 RID: 1480
		public bool updateGraphsWithGUO = true;

		// Token: 0x040005C9 RID: 1481
		public float yOffset = 5f;

		// Token: 0x040005CA RID: 1482
		private Bounds bounds;
	}
}
