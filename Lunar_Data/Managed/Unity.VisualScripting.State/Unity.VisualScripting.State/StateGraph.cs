using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000014 RID: 20
	[SerializationVersion("A", new Type[] { })]
	public sealed class StateGraph : Graph, IGraphEventListener
	{
		// Token: 0x06000074 RID: 116 RVA: 0x00002B48 File Offset: 0x00000D48
		public StateGraph()
		{
			this.states = new GraphElementCollection<IState>(this);
			this.transitions = new GraphConnectionCollection<IStateTransition, IState, IState>(this);
			this.groups = new GraphElementCollection<GraphGroup>(this);
			this.sticky = new GraphElementCollection<StickyNote>(this);
			base.elements.Include<IState>(this.states);
			base.elements.Include<IStateTransition>(this.transitions);
			base.elements.Include<GraphGroup>(this.groups);
			base.elements.Include<StickyNote>(this.sticky);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002BCF File Offset: 0x00000DCF
		public override IGraphData CreateData()
		{
			return new StateGraphData(this);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00002BD8 File Offset: 0x00000DD8
		public void StartListening(GraphStack stack)
		{
			stack.GetGraphData<StateGraphData>().isListening = true;
			HashSet<IState> activeStatesNoAlloc = this.GetActiveStatesNoAlloc(stack);
			foreach (IState state in activeStatesNoAlloc)
			{
				IGraphEventListener graphEventListener = state as IGraphEventListener;
				if (graphEventListener != null)
				{
					graphEventListener.StartListening(stack);
				}
			}
			activeStatesNoAlloc.Free<IState>();
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00002C4C File Offset: 0x00000E4C
		public void StopListening(GraphStack stack)
		{
			HashSet<IState> activeStatesNoAlloc = this.GetActiveStatesNoAlloc(stack);
			foreach (IState state in activeStatesNoAlloc)
			{
				IGraphEventListener graphEventListener = state as IGraphEventListener;
				if (graphEventListener != null)
				{
					graphEventListener.StopListening(stack);
				}
			}
			activeStatesNoAlloc.Free<IState>();
			stack.GetGraphData<StateGraphData>().isListening = false;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00002CC0 File Offset: 0x00000EC0
		public bool IsListening(GraphPointer pointer)
		{
			return pointer.GetGraphData<StateGraphData>().isListening;
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00002CCD File Offset: 0x00000ECD
		// (set) Token: 0x0600007A RID: 122 RVA: 0x00002CD5 File Offset: 0x00000ED5
		[DoNotSerialize]
		public GraphElementCollection<IState> states { get; internal set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00002CDE File Offset: 0x00000EDE
		// (set) Token: 0x0600007C RID: 124 RVA: 0x00002CE6 File Offset: 0x00000EE6
		[DoNotSerialize]
		public GraphConnectionCollection<IStateTransition, IState, IState> transitions { get; internal set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00002CEF File Offset: 0x00000EEF
		// (set) Token: 0x0600007E RID: 126 RVA: 0x00002CF7 File Offset: 0x00000EF7
		[DoNotSerialize]
		public GraphElementCollection<GraphGroup> groups { get; internal set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00002D00 File Offset: 0x00000F00
		// (set) Token: 0x06000080 RID: 128 RVA: 0x00002D08 File Offset: 0x00000F08
		[DoNotSerialize]
		public GraphElementCollection<StickyNote> sticky { get; private set; }

		// Token: 0x06000081 RID: 129 RVA: 0x00002D14 File Offset: 0x00000F14
		private HashSet<IState> GetActiveStatesNoAlloc(GraphPointer pointer)
		{
			HashSet<IState> hashSet = HashSetPool<IState>.New();
			foreach (IState state in this.states)
			{
				if (pointer.GetElementData<State.Data>(state).isActive)
				{
					hashSet.Add(state);
				}
			}
			return hashSet;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00002D80 File Offset: 0x00000F80
		public void Start(Flow flow)
		{
			flow.stack.GetGraphData<StateGraphData>().isListening = true;
			foreach (IState state in this.states.Where((IState s) => s.isStart))
			{
				try
				{
					state.OnEnter(flow, StateEnterReason.Start);
				}
				catch (Exception ex)
				{
					state.HandleException(flow.stack, ex);
					throw;
				}
			}
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00002E24 File Offset: 0x00001024
		public void Stop(Flow flow)
		{
			HashSet<IState> activeStatesNoAlloc = this.GetActiveStatesNoAlloc(flow.stack);
			foreach (IState state in activeStatesNoAlloc)
			{
				try
				{
					state.OnExit(flow, StateExitReason.Stop);
				}
				catch (Exception ex)
				{
					state.HandleException(flow.stack, ex);
					throw;
				}
			}
			activeStatesNoAlloc.Free<IState>();
			flow.stack.GetGraphData<StateGraphData>().isListening = false;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00002EB8 File Offset: 0x000010B8
		public static StateGraph WithStart()
		{
			StateGraph stateGraph = new StateGraph();
			FlowState flowState = FlowState.WithEnterUpdateExit();
			flowState.isStart = true;
			flowState.nest.embed.title = "Start";
			flowState.position = new Vector2(-86f, -15f);
			stateGraph.states.Add(flowState);
			return stateGraph;
		}
	}
}
