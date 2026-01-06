using System;
using Pathfinding;
using UnityEngine;

// Token: 0x02000003 RID: 3
[ExecuteInEditMode]
[HelpURL("http://arongranberg.com/astar/docs/class_snap_to_node.php")]
public class SnapToNode : MonoBehaviour
{
	// Token: 0x06000052 RID: 82 RVA: 0x00003354 File Offset: 0x00001554
	private void Update()
	{
		if (base.transform.hasChanged && AstarPath.active != null)
		{
			GraphNode node = AstarPath.active.GetNearest(base.transform.position, NNConstraint.None).node;
			if (node != null)
			{
				base.transform.position = (Vector3)node.position;
				base.transform.hasChanged = false;
			}
		}
	}
}
