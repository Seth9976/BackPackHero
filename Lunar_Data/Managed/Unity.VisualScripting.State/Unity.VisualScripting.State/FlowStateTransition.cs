using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000004 RID: 4
	[SerializationVersion("A", new Type[] { })]
	public sealed class FlowStateTransition : NesterStateTransition<FlowGraph, ScriptGraphAsset>, IGraphEventListener
	{
		// Token: 0x0600000F RID: 15 RVA: 0x00002309 File Offset: 0x00000509
		public FlowStateTransition()
		{
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002311 File Offset: 0x00000511
		public FlowStateTransition(IState source, IState destination)
			: base(source, destination)
		{
			if (!source.canBeSource)
			{
				throw new InvalidOperationException("Source state cannot emit transitions.");
			}
			if (!destination.canBeDestination)
			{
				throw new InvalidOperationException("Destination state cannot receive transitions.");
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002341 File Offset: 0x00000541
		public static FlowStateTransition WithDefaultTrigger(IState source, IState destination)
		{
			return new FlowStateTransition(source, destination)
			{
				nest = 
				{
					source = GraphSource.Embed
				},
				nest = 
				{
					embed = FlowStateTransition.GraphWithDefaultTrigger()
				}
			};
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002366 File Offset: 0x00000566
		public static FlowGraph GraphWithDefaultTrigger()
		{
			return new FlowGraph
			{
				units = 
				{
					new TriggerStateTransition
					{
						position = new Vector2(100f, -50f)
					}
				}
			};
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002394 File Offset: 0x00000594
		public override void OnEnter(Flow flow)
		{
			if (flow.stack.TryEnterParentElement(this))
			{
				flow.stack.TriggerEventHandler((EventHook hook) => hook == "OnEnterState", default(EmptyEventArgs), (IGraphParentElement parent) => parent is SubgraphUnit, false);
				flow.stack.ExitParentElement();
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002410 File Offset: 0x00000610
		public override void OnExit(Flow flow)
		{
			if (flow.stack.TryEnterParentElement(this))
			{
				flow.stack.TriggerEventHandler((EventHook hook) => hook == "OnExitState", default(EmptyEventArgs), (IGraphParentElement parent) => parent is SubgraphUnit, false);
				base.nest.graph.StopListening(flow.stack);
				flow.stack.ExitParentElement();
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000249F File Offset: 0x0000069F
		public void StartListening(GraphStack stack)
		{
			if (stack.TryEnterParentElement(this))
			{
				base.nest.graph.StartListening(stack);
				stack.ExitParentElement();
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000024C1 File Offset: 0x000006C1
		public void StopListening(GraphStack stack)
		{
			if (stack.TryEnterParentElement(this))
			{
				base.nest.graph.StopListening(stack);
				stack.ExitParentElement();
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000024E3 File Offset: 0x000006E3
		public bool IsListening(GraphPointer pointer)
		{
			return pointer.GetElementData<State.Data>(base.source).isActive;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000024F6 File Offset: 0x000006F6
		public override FlowGraph DefaultGraph()
		{
			return FlowStateTransition.GraphWithDefaultTrigger();
		}
	}
}
