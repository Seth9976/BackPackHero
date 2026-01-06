using System;
using System.ComponentModel;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000003 RID: 3
	[SerializationVersion("A", new Type[] { })]
	[TypeIcon(typeof(FlowGraph))]
	[DisplayName("Script State")]
	public sealed class FlowState : NesterState<FlowGraph, ScriptGraphAsset>, IGraphEventListener
	{
		// Token: 0x06000005 RID: 5 RVA: 0x000020D8 File Offset: 0x000002D8
		public FlowState()
		{
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020E0 File Offset: 0x000002E0
		public FlowState(ScriptGraphAsset macro)
			: base(macro)
		{
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020EC File Offset: 0x000002EC
		protected override void OnEnterImplementation(Flow flow)
		{
			if (flow.stack.TryEnterParentElement(this))
			{
				base.nest.graph.StartListening(flow.stack);
				flow.stack.TriggerEventHandler((EventHook hook) => hook == "OnEnterState", default(EmptyEventArgs), (IGraphParentElement parent) => parent is SubgraphUnit, false);
				flow.stack.ExitParentElement();
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000217C File Offset: 0x0000037C
		protected override void OnExitImplementation(Flow flow)
		{
			if (flow.stack.TryEnterParentElement(this))
			{
				flow.stack.TriggerEventHandler((EventHook hook) => hook == "OnExitState", default(EmptyEventArgs), (IGraphParentElement parent) => parent is SubgraphUnit, false);
				base.nest.graph.StopListening(flow.stack);
				flow.stack.ExitParentElement();
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000220B File Offset: 0x0000040B
		public void StartListening(GraphStack stack)
		{
			if (stack.TryEnterParentElement(this))
			{
				base.nest.graph.StartListening(stack);
				stack.ExitParentElement();
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000222D File Offset: 0x0000042D
		public void StopListening(GraphStack stack)
		{
			if (stack.TryEnterParentElement(this))
			{
				base.nest.graph.StopListening(stack);
				stack.ExitParentElement();
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000224F File Offset: 0x0000044F
		public bool IsListening(GraphPointer pointer)
		{
			return pointer.GetElementData<State.Data>(this).isActive;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000225D File Offset: 0x0000045D
		public override FlowGraph DefaultGraph()
		{
			return FlowState.GraphWithEnterUpdateExit();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002264 File Offset: 0x00000464
		public static FlowState WithEnterUpdateExit()
		{
			return new FlowState
			{
				nest = 
				{
					source = GraphSource.Embed
				},
				nest = 
				{
					embed = FlowState.GraphWithEnterUpdateExit()
				}
			};
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002288 File Offset: 0x00000488
		public static FlowGraph GraphWithEnterUpdateExit()
		{
			return new FlowGraph
			{
				units = 
				{
					new OnEnterState
					{
						position = new Vector2(-205f, -215f)
					},
					new Update
					{
						position = new Vector2(-161f, -38f)
					},
					new OnExitState
					{
						position = new Vector2(-205f, 145f)
					}
				}
			};
		}
	}
}
