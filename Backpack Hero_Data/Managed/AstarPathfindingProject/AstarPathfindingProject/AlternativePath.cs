using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000074 RID: 116
	[AddComponentMenu("Pathfinding/Modifiers/Alternative Path")]
	[HelpURL("http://arongranberg.com/astar/documentation/stable/class_pathfinding_1_1_alternative_path.php")]
	[Serializable]
	public class AlternativePath : MonoModifier
	{
		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000628 RID: 1576 RVA: 0x000244F3 File Offset: 0x000226F3
		public override int Order
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x000244F7 File Offset: 0x000226F7
		public override void Apply(Path p)
		{
			if (this == null)
			{
				return;
			}
			this.ApplyNow(p.path);
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x0002450F File Offset: 0x0002270F
		protected void OnDestroy()
		{
			this.destroyed = true;
			this.ClearOnDestroy();
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x0002451E File Offset: 0x0002271E
		private void ClearOnDestroy()
		{
			this.InversePrevious();
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x00024528 File Offset: 0x00022728
		private void InversePrevious()
		{
			if (this.prevNodes != null)
			{
				bool flag = false;
				for (int i = 0; i < this.prevNodes.Count; i++)
				{
					if ((ulong)this.prevNodes[i].Penalty < (ulong)((long)this.prevPenalty))
					{
						flag = true;
						this.prevNodes[i].Penalty = 0U;
					}
					else
					{
						this.prevNodes[i].Penalty = (uint)((ulong)this.prevNodes[i].Penalty - (ulong)((long)this.prevPenalty));
					}
				}
				if (flag)
				{
					Debug.LogWarning("Penalty for some nodes has been reset while the AlternativePath modifier was active (possibly because of a graph update). Some penalties might be incorrect (they may be lower than expected for the affected nodes)");
				}
			}
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x000245C4 File Offset: 0x000227C4
		private void ApplyNow(List<GraphNode> nodes)
		{
			this.InversePrevious();
			this.prevNodes.Clear();
			if (this.destroyed)
			{
				return;
			}
			if (nodes != null)
			{
				for (int i = this.rnd.Next(this.randomStep); i < nodes.Count; i += this.rnd.Next(1, this.randomStep))
				{
					nodes[i].Penalty = (uint)((ulong)nodes[i].Penalty + (ulong)((long)this.penalty));
					this.prevNodes.Add(nodes[i]);
				}
			}
			this.prevPenalty = this.penalty;
		}

		// Token: 0x0400037A RID: 890
		public int penalty = 1000;

		// Token: 0x0400037B RID: 891
		public int randomStep = 10;

		// Token: 0x0400037C RID: 892
		private List<GraphNode> prevNodes = new List<GraphNode>();

		// Token: 0x0400037D RID: 893
		private int prevPenalty;

		// Token: 0x0400037E RID: 894
		private readonly Random rnd = new Random();

		// Token: 0x0400037F RID: 895
		private bool destroyed;
	}
}
