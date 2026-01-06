using System;
using System.Collections.Generic;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200010D RID: 269
	[AddComponentMenu("Pathfinding/Modifiers/Funnel Modifier")]
	[HelpURL("https://arongranberg.com/astar/documentation/stable/funnelmodifier.html")]
	[Serializable]
	public class FunnelModifier : MonoModifier
	{
		// Token: 0x17000160 RID: 352
		// (get) Token: 0x0600088E RID: 2190 RVA: 0x0002D3D5 File Offset: 0x0002B5D5
		public override int Order
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x0002D574 File Offset: 0x0002B774
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
			if (this.quality == FunnelModifier.FunnelQuality.High)
			{
				Funnel.Simplify(list2, ref p.path);
			}
			for (int i = 0; i < list2.Count; i++)
			{
				Funnel.PathPart pathPart = list2[i];
				if (pathPart.type == Funnel.PartType.NodeSequence)
				{
					GridGraph gridGraph = p.path[pathPart.startIndex].Graph as GridGraph;
					if (gridGraph != null && gridGraph.neighbours != NumNeighbours.Six)
					{
						Func<GraphNode, uint> func = null;
						if (this.accountForGridPenalties)
						{
							func = new Func<GraphNode, uint>(p.GetTraversalCost);
						}
						Func<GraphNode, bool> func2 = new Func<GraphNode, bool>(p.CanTraverse);
						List<Vector3> list3 = GridStringPulling.Calculate(p.path, pathPart.startIndex, pathPart.endIndex, pathPart.startPoint, pathPart.endPoint, func, func2, int.MaxValue);
						list.AddRange(list3);
						ListPool<Vector3>.Release(ref list3);
					}
					else
					{
						Funnel.FunnelPortals funnelPortals = Funnel.ConstructFunnelPortals(p.path, pathPart);
						List<Vector3> list4 = Funnel.Calculate(funnelPortals, this.splitAtEveryPortal);
						list.AddRange(list4);
						ListPool<Vector3>.Release(ref funnelPortals.left);
						ListPool<Vector3>.Release(ref funnelPortals.right);
						ListPool<Vector3>.Release(ref list4);
					}
				}
				else
				{
					if (i == 0 || list2[i - 1].type == Funnel.PartType.OffMeshLink)
					{
						list.Add(pathPart.startPoint);
					}
					if (i == list2.Count - 1 || list2[i + 1].type == Funnel.PartType.OffMeshLink)
					{
						list.Add(pathPart.endPoint);
					}
				}
			}
			ListPool<Funnel.PathPart>.Release(ref list2);
			ListPool<Vector3>.Release(ref p.vectorPath);
			p.vectorPath = list;
		}

		// Token: 0x040005A5 RID: 1445
		public FunnelModifier.FunnelQuality quality;

		// Token: 0x040005A6 RID: 1446
		public bool splitAtEveryPortal;

		// Token: 0x040005A7 RID: 1447
		public bool accountForGridPenalties;

		// Token: 0x0200010E RID: 270
		public enum FunnelQuality
		{
			// Token: 0x040005A9 RID: 1449
			Medium,
			// Token: 0x040005AA RID: 1450
			High
		}
	}
}
