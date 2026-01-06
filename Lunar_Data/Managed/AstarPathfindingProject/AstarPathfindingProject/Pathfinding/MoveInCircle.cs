using System;
using Pathfinding.Drawing;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000010 RID: 16
	[UniqueComponent(tag = "ai.destination")]
	[AddComponentMenu("Pathfinding/AI/Behaviors/MoveInCircle")]
	[HelpURL("https://arongranberg.com/astar/documentation/stable/moveincircle.html")]
	public class MoveInCircle : VersionedMonoBehaviour
	{
		// Token: 0x0600007A RID: 122 RVA: 0x0000424B File Offset: 0x0000244B
		private void OnEnable()
		{
			this.ai = base.GetComponent<IAstarAI>();
		}

		// Token: 0x0600007B RID: 123 RVA: 0x0000425C File Offset: 0x0000245C
		private void Update()
		{
			Vector3 normalized = (this.ai.position - this.target.position).normalized;
			Vector3 vector = Vector3.Cross(normalized, this.target.up);
			this.ai.destination = this.target.position + normalized * this.radius + vector * this.offset;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000042D7 File Offset: 0x000024D7
		public override void DrawGizmos()
		{
			if (this.target)
			{
				Draw.Circle(this.target.position, this.target.up, this.radius, Color.white);
			}
		}

		// Token: 0x0400006B RID: 107
		public Transform target;

		// Token: 0x0400006C RID: 108
		public float radius = 5f;

		// Token: 0x0400006D RID: 109
		public float offset = 2f;

		// Token: 0x0400006E RID: 110
		private IAstarAI ai;
	}
}
