using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x0200017B RID: 379
	[AddComponentMenu("Visual Scripting/Script Machine")]
	[RequireComponent(typeof(Variables))]
	[DisableAnnotation]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.visualscripting@latest/index.html?subfolder=/manual/vs-graphs-machines-macros.html")]
	[RenamedFrom("Bolt.FlowMachine")]
	[RenamedFrom("Unity.VisualScripting.FlowMachine")]
	public sealed class ScriptMachine : EventMachine<FlowGraph, ScriptGraphAsset>
	{
		// Token: 0x060009EB RID: 2539 RVA: 0x00011A42 File Offset: 0x0000FC42
		public override FlowGraph DefaultGraph()
		{
			return FlowGraph.WithStartUpdate();
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x00011A49 File Offset: 0x0000FC49
		protected override void OnEnable()
		{
			if (base.hasGraph)
			{
				base.graph.StartListening(base.reference);
			}
			base.OnEnable();
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x00011A6A File Offset: 0x0000FC6A
		protected override void OnInstantiateWhileEnabled()
		{
			if (base.hasGraph)
			{
				base.graph.StartListening(base.reference);
			}
			base.OnInstantiateWhileEnabled();
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x00011A8B File Offset: 0x0000FC8B
		protected override void OnUninstantiateWhileEnabled()
		{
			base.OnUninstantiateWhileEnabled();
			if (base.hasGraph)
			{
				base.graph.StopListening(base.reference);
			}
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x00011AAC File Offset: 0x0000FCAC
		protected override void OnDisable()
		{
			base.OnDisable();
			if (base.hasGraph)
			{
				base.graph.StopListening(base.reference);
			}
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x00011ACD File Offset: 0x0000FCCD
		[ContextMenu("Show Data...")]
		protected override void ShowData()
		{
			base.ShowData();
		}
	}
}
