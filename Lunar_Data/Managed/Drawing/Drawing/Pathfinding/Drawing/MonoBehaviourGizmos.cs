using System;
using UnityEngine;

namespace Pathfinding.Drawing
{
	// Token: 0x02000050 RID: 80
	public abstract class MonoBehaviourGizmos : MonoBehaviour, IDrawGizmos
	{
		// Token: 0x06000291 RID: 657 RVA: 0x0000E2E7 File Offset: 0x0000C4E7
		public MonoBehaviourGizmos()
		{
		}

		// Token: 0x06000292 RID: 658 RVA: 0x00002094 File Offset: 0x00000294
		private void OnDrawGizmosSelected()
		{
		}

		// Token: 0x06000293 RID: 659 RVA: 0x00002094 File Offset: 0x00000294
		public virtual void DrawGizmos()
		{
		}
	}
}
