using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x020000B5 RID: 181
	public interface IMachine : IGraphRoot, IGraphParent, IGraphNester, IAotStubbable
	{
		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x0600046E RID: 1134
		// (set) Token: 0x0600046F RID: 1135
		IGraphData graphData { get; set; }

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000470 RID: 1136
		GameObject threadSafeGameObject { get; }
	}
}
