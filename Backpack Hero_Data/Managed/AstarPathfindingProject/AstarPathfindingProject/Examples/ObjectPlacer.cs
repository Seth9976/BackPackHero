using System;
using System.Collections;
using UnityEngine;

namespace Pathfinding.Examples
{
	// Token: 0x020000E2 RID: 226
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_examples_1_1_object_placer.php")]
	public class ObjectPlacer : MonoBehaviour
	{
		// Token: 0x060009AE RID: 2478 RVA: 0x0003EF74 File Offset: 0x0003D174
		private void Update()
		{
			if (Input.GetKeyDown("p"))
			{
				this.PlaceObject();
			}
			if (Input.GetKeyDown("r"))
			{
				base.StartCoroutine(this.RemoveObject());
			}
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x0003EFA4 File Offset: 0x0003D1A4
		public void PlaceObject()
		{
			RaycastHit raycastHit;
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out raycastHit, float.PositiveInfinity))
			{
				Vector3 point = raycastHit.point;
				GameObject gameObject = Object.Instantiate<GameObject>(this.go, point, Quaternion.identity);
				if (this.issueGUOs)
				{
					GraphUpdateObject graphUpdateObject = new GraphUpdateObject(gameObject.GetComponent<Collider>().bounds);
					AstarPath.active.UpdateGraphs(graphUpdateObject);
					if (this.direct)
					{
						AstarPath.active.FlushGraphUpdates();
					}
				}
			}
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x0003F01E File Offset: 0x0003D21E
		public IEnumerator RemoveObject()
		{
			RaycastHit raycastHit;
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out raycastHit, float.PositiveInfinity))
			{
				if (raycastHit.collider.isTrigger || raycastHit.transform.gameObject.name == "Ground")
				{
					yield break;
				}
				Bounds b = raycastHit.collider.bounds;
				Object.Destroy(raycastHit.collider);
				Object.Destroy(raycastHit.collider.gameObject);
				if (this.issueGUOs)
				{
					yield return new WaitForEndOfFrame();
					GraphUpdateObject graphUpdateObject = new GraphUpdateObject(b);
					AstarPath.active.UpdateGraphs(graphUpdateObject);
					if (this.direct)
					{
						AstarPath.active.FlushGraphUpdates();
					}
				}
				b = default(Bounds);
			}
			yield break;
		}

		// Token: 0x040005D5 RID: 1493
		public GameObject go;

		// Token: 0x040005D6 RID: 1494
		public bool direct;

		// Token: 0x040005D7 RID: 1495
		public bool issueGUOs = true;
	}
}
