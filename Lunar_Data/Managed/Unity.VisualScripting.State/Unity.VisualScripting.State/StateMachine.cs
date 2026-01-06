using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000017 RID: 23
	[AddComponentMenu("Visual Scripting/State Machine")]
	[RequireComponent(typeof(Variables))]
	[DisableAnnotation]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.visualscripting@latest/index.html?subfolder=/manual/vs-graphs-machines-macros.html")]
	public sealed class StateMachine : EventMachine<StateGraph, StateGraphAsset>
	{
		// Token: 0x0600008B RID: 139 RVA: 0x00002F40 File Offset: 0x00001140
		protected override void OnEnable()
		{
			if (base.hasGraph)
			{
				using (Flow flow = Flow.New(base.reference))
				{
					base.graph.Start(flow);
				}
			}
			base.OnEnable();
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00002F90 File Offset: 0x00001190
		protected override void OnInstantiateWhileEnabled()
		{
			if (base.hasGraph)
			{
				using (Flow flow = Flow.New(base.reference))
				{
					base.graph.Start(flow);
				}
			}
			base.OnInstantiateWhileEnabled();
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00002FE0 File Offset: 0x000011E0
		protected override void OnUninstantiateWhileEnabled()
		{
			base.OnUninstantiateWhileEnabled();
			if (base.hasGraph)
			{
				using (Flow flow = Flow.New(base.reference))
				{
					base.graph.Stop(flow);
				}
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003030 File Offset: 0x00001230
		protected override void OnDisable()
		{
			base.OnDisable();
			if (base.hasGraph)
			{
				using (Flow flow = Flow.New(base.reference))
				{
					base.graph.Stop(flow);
				}
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003080 File Offset: 0x00001280
		[ContextMenu("Show Data...")]
		protected override void ShowData()
		{
			base.ShowData();
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003088 File Offset: 0x00001288
		public override StateGraph DefaultGraph()
		{
			return StateGraph.WithStart();
		}
	}
}
