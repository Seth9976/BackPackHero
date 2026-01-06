using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000004 RID: 4
	[UniqueComponent(tag = "ai.destination")]
	[HelpURL("http://arongranberg.com/astar/documentation/stable/class_pathfinding_1_1_a_i_destination_setter.php")]
	public class AIDestinationSetter : VersionedMonoBehaviour
	{
		// Token: 0x06000054 RID: 84 RVA: 0x000033C8 File Offset: 0x000015C8
		private void OnEnable()
		{
			this.ai = base.GetComponent<IAstarAI>();
			if (this.ai != null)
			{
				IAstarAI astarAI = this.ai;
				astarAI.onSearchPath = (Action)Delegate.Combine(astarAI.onSearchPath, new Action(this.Update));
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003405 File Offset: 0x00001605
		private void OnDisable()
		{
			if (this.ai != null)
			{
				IAstarAI astarAI = this.ai;
				astarAI.onSearchPath = (Action)Delegate.Remove(astarAI.onSearchPath, new Action(this.Update));
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003436 File Offset: 0x00001636
		private void Update()
		{
			if (this.target != null && this.ai != null)
			{
				this.ai.destination = this.target.position;
			}
		}

		// Token: 0x0400003D RID: 61
		public Transform target;

		// Token: 0x0400003E RID: 62
		private IAstarAI ai;
	}
}
