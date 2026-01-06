using System;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200006E RID: 110
	[HelpURL("https://arongranberg.com/astar/documentation/stable/localspacegraph.html")]
	public class LocalSpaceGraph : VersionedMonoBehaviour
	{
		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060003BA RID: 954 RVA: 0x00012CBD File Offset: 0x00010EBD
		public GraphTransform transformation
		{
			get
			{
				return this.graphTransform;
			}
		}

		// Token: 0x060003BB RID: 955 RVA: 0x00012CC5 File Offset: 0x00010EC5
		private void Start()
		{
			this.originalMatrix = base.transform.worldToLocalMatrix;
			base.transform.hasChanged = true;
			this.Refresh();
		}

		// Token: 0x060003BC RID: 956 RVA: 0x00012CEA File Offset: 0x00010EEA
		public void Refresh()
		{
			if (base.transform.hasChanged)
			{
				this.graphTransform.SetMatrix(base.transform.localToWorldMatrix * this.originalMatrix);
				base.transform.hasChanged = false;
			}
		}

		// Token: 0x04000274 RID: 628
		private Matrix4x4 originalMatrix;

		// Token: 0x04000275 RID: 629
		private MutableGraphTransform graphTransform = new MutableGraphTransform(Matrix4x4.identity);
	}
}
