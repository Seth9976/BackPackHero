using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000011 RID: 17
	[UniqueComponent(tag = "ai.destination")]
	[AddComponentMenu("Pathfinding/AI/Behaviors/Patrol")]
	[HelpURL("https://arongranberg.com/astar/documentation/stable/patrol.html")]
	public class Patrol : VersionedMonoBehaviour
	{
		// Token: 0x0600007E RID: 126 RVA: 0x00004334 File Offset: 0x00002534
		protected override void Awake()
		{
			base.Awake();
			this.agent = base.GetComponent<IAstarAI>();
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00004348 File Offset: 0x00002548
		private void Update()
		{
			if (this.targets.Length == 0)
			{
				return;
			}
			if (this.agent.reachedEndOfPath && !this.agent.pathPending && float.IsPositiveInfinity(this.switchTime))
			{
				this.switchTime = Time.time + this.delay;
			}
			if (Time.time >= this.switchTime)
			{
				this.index++;
				this.switchTime = float.PositiveInfinity;
				this.index %= this.targets.Length;
				this.agent.destination = this.targets[this.index].position;
				this.agent.SearchPath();
				return;
			}
			if (this.updateDestinationEveryFrame)
			{
				this.index %= this.targets.Length;
				this.agent.destination = this.targets[this.index].position;
			}
		}

		// Token: 0x0400006F RID: 111
		public Transform[] targets;

		// Token: 0x04000070 RID: 112
		public float delay;

		// Token: 0x04000071 RID: 113
		public bool updateDestinationEveryFrame;

		// Token: 0x04000072 RID: 114
		private int index = -1;

		// Token: 0x04000073 RID: 115
		private IAstarAI agent;

		// Token: 0x04000074 RID: 116
		private float switchTime = float.NegativeInfinity;
	}
}
