using System;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200000E RID: 14
	[UniqueComponent(tag = "ai.destination")]
	[AddComponentMenu("Pathfinding/AI/Behaviors/AIDestinationSetter")]
	[HelpURL("https://arongranberg.com/astar/documentation/stable/aidestinationsetter.html")]
	public class AIDestinationSetter : VersionedMonoBehaviour
	{
		// Token: 0x0600006E RID: 110 RVA: 0x00003D1C File Offset: 0x00001F1C
		private void OnEnable()
		{
			this.ai = base.GetComponent<IAstarAI>();
			if (this.ai != null)
			{
				IAstarAI astarAI = this.ai;
				astarAI.onSearchPath = (Action)Delegate.Combine(astarAI.onSearchPath, new Action(this.UpdateDestination));
			}
			BatchedEvents.Add<AIDestinationSetter>(this, BatchedEvents.Event.Update, new Action<AIDestinationSetter[], int>(AIDestinationSetter.OnUpdate), 0);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003D78 File Offset: 0x00001F78
		private void OnDisable()
		{
			if (this.ai != null)
			{
				IAstarAI astarAI = this.ai;
				astarAI.onSearchPath = (Action)Delegate.Remove(astarAI.onSearchPath, new Action(this.UpdateDestination));
			}
			BatchedEvents.Remove<AIDestinationSetter>(this);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003DB0 File Offset: 0x00001FB0
		private static void OnUpdate(AIDestinationSetter[] components, int count)
		{
			for (int i = 0; i < count; i++)
			{
				components[i].UpdateDestination();
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003DD1 File Offset: 0x00001FD1
		private void UpdateDestination()
		{
			if (this.target != null && this.ai != null)
			{
				this.ai.destination = this.target.position;
			}
		}

		// Token: 0x04000067 RID: 103
		public Transform target;

		// Token: 0x04000068 RID: 104
		public bool useRotation;

		// Token: 0x04000069 RID: 105
		private IAstarAI ai;
	}
}
