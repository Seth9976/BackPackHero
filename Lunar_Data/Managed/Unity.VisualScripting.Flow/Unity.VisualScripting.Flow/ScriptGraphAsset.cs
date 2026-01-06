using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x0200017A RID: 378
	[TypeIcon(typeof(FlowGraph))]
	[CreateAssetMenu(menuName = "Visual Scripting/Script Graph", fileName = "New Script Graph", order = 81)]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.visualscripting@latest/index.html?subfolder=/manual/vs-script-graphs-intro.html")]
	public sealed class ScriptGraphAsset : Macro<FlowGraph>
	{
		// Token: 0x060009E8 RID: 2536 RVA: 0x00011A2B File Offset: 0x0000FC2B
		[ContextMenu("Show Data...")]
		protected override void ShowData()
		{
			base.ShowData();
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x00011A33 File Offset: 0x0000FC33
		public override FlowGraph DefaultGraph()
		{
			return FlowGraph.WithInputOutput();
		}
	}
}
