using System;
using System.Collections.Generic;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000075 RID: 117
	[AddComponentMenu("Pathfinding/Modifiers/Funnel")]
	[HelpURL("http://arongranberg.com/astar/documentation/stable/class_pathfinding_1_1_funnel_modifier.php")]
	[Serializable]
	public class FunnelModifier : MonoModifier
	{
		// Token: 0x170000FD RID: 253
		// (get) Token: 0x0600062F RID: 1583 RVA: 0x00024691 File Offset: 0x00022891
		public override int Order
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x00024698 File Offset: 0x00022898
		public override void Apply(Path p)
		{
			if (p.path == null || p.path.Count == 0 || p.vectorPath == null || p.vectorPath.Count == 0)
			{
				return;
			}
			List<Vector3> list = ListPool<Vector3>.Claim();
			List<Funnel.PathPart> list2 = Funnel.SplitIntoParts(p);
			if (list2.Count == 0)
			{
				return;
			}
			for (int i = 0; i < list2.Count; i++)
			{
				Funnel.PathPart pathPart = list2[i];
				if (!pathPart.isLink)
				{
					Funnel.FunnelPortals funnelPortals = Funnel.ConstructFunnelPortals(p.path, pathPart);
					List<Vector3> list3 = Funnel.Calculate(funnelPortals, this.unwrap, this.splitAtEveryPortal);
					list.AddRange(list3);
					ListPool<Vector3>.Release(ref funnelPortals.left);
					ListPool<Vector3>.Release(ref funnelPortals.right);
					ListPool<Vector3>.Release(ref list3);
				}
				else
				{
					if (i == 0 || list2[i - 1].isLink)
					{
						list.Add(pathPart.startPoint);
					}
					if (i == list2.Count - 1 || list2[i + 1].isLink)
					{
						list.Add(pathPart.endPoint);
					}
				}
			}
			ListPool<Funnel.PathPart>.Release(ref list2);
			ListPool<Vector3>.Release(ref p.vectorPath);
			p.vectorPath = list;
		}

		// Token: 0x04000380 RID: 896
		public bool unwrap = true;

		// Token: 0x04000381 RID: 897
		public bool splitAtEveryPortal;
	}
}
