using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000015 RID: 21
	[TypeIcon(typeof(StateGraph))]
	[CreateAssetMenu(menuName = "Visual Scripting/State Graph", fileName = "New State Graph", order = 81)]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.visualscripting@latest/index.html?subfolder=/manual/vs-state-graphs-intro.html")]
	public sealed class StateGraphAsset : Macro<StateGraph>
	{
		// Token: 0x06000085 RID: 133 RVA: 0x00002F0D File Offset: 0x0000110D
		[ContextMenu("Show Data...")]
		protected override void ShowData()
		{
			base.ShowData();
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00002F15 File Offset: 0x00001115
		public override StateGraph DefaultGraph()
		{
			return StateGraph.WithStart();
		}
	}
}
