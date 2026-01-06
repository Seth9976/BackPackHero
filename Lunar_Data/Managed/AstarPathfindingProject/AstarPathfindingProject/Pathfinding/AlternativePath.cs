using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200010C RID: 268
	[AddComponentMenu("Pathfinding/Modifiers/Alternative Path Modifier")]
	[HelpURL("https://arongranberg.com/astar/documentation/stable/alternativepath.html")]
	[Serializable]
	public class AlternativePath : MonoModifier
	{
		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000887 RID: 2183 RVA: 0x0002D3D5 File Offset: 0x0002B5D5
		public override int Order
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x0002D3D9 File Offset: 0x0002B5D9
		public override void Apply(Path p)
		{
			if (this == null)
			{
				return;
			}
			this.ApplyNow(p.path);
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x0002D3F1 File Offset: 0x0002B5F1
		protected void OnDestroy()
		{
			this.destroyed = true;
			this.ClearOnDestroy();
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x0002D400 File Offset: 0x0002B600
		private void ClearOnDestroy()
		{
			this.InversePrevious();
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x0002D408 File Offset: 0x0002B608
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

		// Token: 0x0600088C RID: 2188 RVA: 0x0002D4A4 File Offset: 0x0002B6A4
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

		// Token: 0x0400059F RID: 1439
		public int penalty = 1000;

		// Token: 0x040005A0 RID: 1440
		public int randomStep = 10;

		// Token: 0x040005A1 RID: 1441
		private List<GraphNode> prevNodes = new List<GraphNode>();

		// Token: 0x040005A2 RID: 1442
		private int prevPenalty;

		// Token: 0x040005A3 RID: 1443
		private readonly Random rnd = new Random();

		// Token: 0x040005A4 RID: 1444
		private bool destroyed;
	}
}
