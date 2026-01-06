using System;

namespace Unity.VisualScripting
{
	// Token: 0x0200001A RID: 26
	[TypeIcon(typeof(StateGraph))]
	public sealed class SuperState : NesterState<StateGraph, StateGraphAsset>, IGraphEventListener
	{
		// Token: 0x060000AF RID: 175 RVA: 0x000033A8 File Offset: 0x000015A8
		public SuperState()
		{
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000033B0 File Offset: 0x000015B0
		public SuperState(StateGraphAsset macro)
			: base(macro)
		{
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000033B9 File Offset: 0x000015B9
		public static SuperState WithStart()
		{
			return new SuperState
			{
				nest = 
				{
					source = GraphSource.Embed
				},
				nest = 
				{
					embed = StateGraph.WithStart()
				}
			};
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000033DC File Offset: 0x000015DC
		protected override void OnEnterImplementation(Flow flow)
		{
			if (flow.stack.TryEnterParentElement(this))
			{
				base.nest.graph.Start(flow);
				flow.stack.ExitParentElement();
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00003408 File Offset: 0x00001608
		protected override void OnExitImplementation(Flow flow)
		{
			if (flow.stack.TryEnterParentElement(this))
			{
				base.nest.graph.Stop(flow);
				flow.stack.ExitParentElement();
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003434 File Offset: 0x00001634
		public void StartListening(GraphStack stack)
		{
			if (stack.TryEnterParentElement(this))
			{
				base.nest.graph.StartListening(stack);
				stack.ExitParentElement();
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00003456 File Offset: 0x00001656
		public void StopListening(GraphStack stack)
		{
			if (stack.TryEnterParentElement(this))
			{
				base.nest.graph.StopListening(stack);
				stack.ExitParentElement();
			}
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00003478 File Offset: 0x00001678
		public bool IsListening(GraphPointer pointer)
		{
			return pointer.GetElementData<State.Data>(this).isActive;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00003486 File Offset: 0x00001686
		public override StateGraph DefaultGraph()
		{
			return StateGraph.WithStart();
		}
	}
}
