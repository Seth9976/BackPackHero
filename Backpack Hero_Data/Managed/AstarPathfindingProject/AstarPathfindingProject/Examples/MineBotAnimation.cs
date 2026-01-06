using System;
using UnityEngine;

namespace Pathfinding.Examples
{
	// Token: 0x020000E1 RID: 225
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_examples_1_1_mine_bot_animation.php")]
	public class MineBotAnimation : VersionedMonoBehaviour
	{
		// Token: 0x060009AA RID: 2474 RVA: 0x0003EE56 File Offset: 0x0003D056
		protected override void Awake()
		{
			base.Awake();
			this.ai = base.GetComponent<IAstarAI>();
			this.tr = base.GetComponent<Transform>();
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x0003EE78 File Offset: 0x0003D078
		private void OnTargetReached()
		{
			if (this.endOfPathEffect != null && Vector3.Distance(this.tr.position, this.lastTarget) > 1f)
			{
				Object.Instantiate<GameObject>(this.endOfPathEffect, this.tr.position, this.tr.rotation);
				this.lastTarget = this.tr.position;
			}
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x0003EEE4 File Offset: 0x0003D0E4
		protected void Update()
		{
			if (this.ai.reachedEndOfPath)
			{
				if (!this.isAtDestination)
				{
					this.OnTargetReached();
				}
				this.isAtDestination = true;
			}
			else
			{
				this.isAtDestination = false;
			}
			Vector3 vector = this.tr.InverseTransformDirection(this.ai.velocity);
			vector.y = 0f;
			this.anim.SetFloat("NormalizedSpeed", vector.magnitude / this.anim.transform.lossyScale.x);
		}

		// Token: 0x040005CF RID: 1487
		public Animator anim;

		// Token: 0x040005D0 RID: 1488
		public GameObject endOfPathEffect;

		// Token: 0x040005D1 RID: 1489
		private bool isAtDestination;

		// Token: 0x040005D2 RID: 1490
		private IAstarAI ai;

		// Token: 0x040005D3 RID: 1491
		private Transform tr;

		// Token: 0x040005D4 RID: 1492
		protected Vector3 lastTarget;
	}
}
