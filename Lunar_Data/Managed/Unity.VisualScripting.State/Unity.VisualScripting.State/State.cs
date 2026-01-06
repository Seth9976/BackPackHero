using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000010 RID: 16
	public abstract class State : GraphElement<StateGraph>, IState, IGraphElementWithDebugData, IGraphElement, IGraphItem, INotifiedCollectionItem, IDisposable, IPrewarmable, IAotStubbable, IIdentifiable, IAnalyticsIdentifiable, IGraphElementWithData
	{
		// Token: 0x06000056 RID: 86 RVA: 0x00002741 File Offset: 0x00000941
		public IGraphElementData CreateData()
		{
			return new State.Data();
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002748 File Offset: 0x00000948
		public IGraphElementDebugData CreateDebugData()
		{
			return new State.DebugData();
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000058 RID: 88 RVA: 0x0000274F File Offset: 0x0000094F
		// (set) Token: 0x06000059 RID: 89 RVA: 0x00002757 File Offset: 0x00000957
		[Serialize]
		public bool isStart { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00002760 File Offset: 0x00000960
		[DoNotSerialize]
		public virtual bool canBeSource
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002763 File Offset: 0x00000963
		[DoNotSerialize]
		public virtual bool canBeDestination
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002766 File Offset: 0x00000966
		public override void BeforeRemove()
		{
			base.BeforeRemove();
			this.Disconnect();
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002774 File Offset: 0x00000974
		public override void Instantiate(GraphReference instance)
		{
			base.Instantiate(instance);
			State.Data elementData = instance.GetElementData<State.Data>(this);
			IGraphEventListener graphEventListener = this as IGraphEventListener;
			if (graphEventListener != null && elementData.isActive)
			{
				graphEventListener.StartListening(instance);
				return;
			}
			if (this.isStart && !elementData.hasEntered && base.graph.IsListening(instance))
			{
				using (Flow flow = Flow.New(instance))
				{
					this.OnEnter(flow, StateEnterReason.Start);
				}
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000027F4 File Offset: 0x000009F4
		public override void Uninstantiate(GraphReference instance)
		{
			IGraphEventListener graphEventListener = this as IGraphEventListener;
			if (graphEventListener != null)
			{
				graphEventListener.StopListening(instance);
			}
			base.Uninstantiate(instance);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002819 File Offset: 0x00000A19
		protected void CopyFrom(State source)
		{
			base.CopyFrom(source);
			this.isStart = source.isStart;
			this.width = source.width;
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000060 RID: 96 RVA: 0x0000283A File Offset: 0x00000A3A
		public IEnumerable<IStateTransition> outgoingTransitions
		{
			get
			{
				StateGraph graph = base.graph;
				return ((graph != null) ? graph.transitions.WithSource(this) : null) ?? Enumerable.Empty<IStateTransition>();
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000061 RID: 97 RVA: 0x0000285D File Offset: 0x00000A5D
		public IEnumerable<IStateTransition> incomingTransitions
		{
			get
			{
				StateGraph graph = base.graph;
				return ((graph != null) ? graph.transitions.WithDestination(this) : null) ?? Enumerable.Empty<IStateTransition>();
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00002880 File Offset: 0x00000A80
		protected List<IStateTransition> outgoingTransitionsNoAlloc
		{
			get
			{
				StateGraph graph = base.graph;
				return ((graph != null) ? graph.transitions.WithSourceNoAlloc(this) : null) ?? Empty<IStateTransition>.list;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000063 RID: 99 RVA: 0x000028A3 File Offset: 0x00000AA3
		public IEnumerable<IStateTransition> transitions
		{
			get
			{
				return LinqUtility.Concat<IStateTransition>(new IEnumerable[] { this.outgoingTransitions, this.incomingTransitions });
			}
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000028C4 File Offset: 0x00000AC4
		public void Disconnect()
		{
			foreach (IStateTransition stateTransition in this.transitions.ToArray<IStateTransition>())
			{
				base.graph.transitions.Remove(stateTransition);
			}
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002904 File Offset: 0x00000B04
		public virtual void OnEnter(Flow flow, StateEnterReason reason)
		{
			State.Data elementData = flow.stack.GetElementData<State.Data>(this);
			if (elementData.isActive)
			{
				return;
			}
			elementData.isActive = true;
			elementData.hasEntered = true;
			foreach (IStateTransition stateTransition in this.outgoingTransitionsNoAlloc)
			{
				IGraphEventListener graphEventListener = stateTransition as IGraphEventListener;
				if (graphEventListener != null)
				{
					graphEventListener.StartListening(flow.stack);
				}
			}
			if (flow.enableDebug)
			{
				flow.stack.GetElementDebugData<State.DebugData>(this).lastEnterFrame = EditorTimeBinding.frame;
			}
			this.OnEnterImplementation(flow);
			foreach (IStateTransition stateTransition2 in this.outgoingTransitionsNoAlloc)
			{
				try
				{
					stateTransition2.OnEnter(flow);
				}
				catch (Exception ex)
				{
					stateTransition2.HandleException(flow.stack, ex);
					throw;
				}
			}
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002A10 File Offset: 0x00000C10
		public virtual void OnExit(Flow flow, StateExitReason reason)
		{
			State.Data elementData = flow.stack.GetElementData<State.Data>(this);
			if (!elementData.isActive)
			{
				return;
			}
			this.OnExitImplementation(flow);
			elementData.isActive = false;
			if (flow.enableDebug)
			{
				flow.stack.GetElementDebugData<State.DebugData>(this).lastExitTime = EditorTimeBinding.time;
			}
			foreach (IStateTransition stateTransition in this.outgoingTransitionsNoAlloc)
			{
				try
				{
					stateTransition.OnExit(flow);
				}
				catch (Exception ex)
				{
					stateTransition.HandleException(flow.stack, ex);
					throw;
				}
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002AC4 File Offset: 0x00000CC4
		protected virtual void OnEnterImplementation(Flow flow)
		{
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002AC6 File Offset: 0x00000CC6
		protected virtual void UpdateImplementation(Flow flow)
		{
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002AC8 File Offset: 0x00000CC8
		protected virtual void FixedUpdateImplementation(Flow flow)
		{
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002ACA File Offset: 0x00000CCA
		protected virtual void LateUpdateImplementation(Flow flow)
		{
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002ACC File Offset: 0x00000CCC
		protected virtual void OnExitImplementation(Flow flow)
		{
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002ACE File Offset: 0x00000CCE
		public virtual void OnBranchTo(Flow flow, IState destination)
		{
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00002AD0 File Offset: 0x00000CD0
		// (set) Token: 0x0600006E RID: 110 RVA: 0x00002AD8 File Offset: 0x00000CD8
		[Serialize]
		public Vector2 position { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00002AE1 File Offset: 0x00000CE1
		// (set) Token: 0x06000070 RID: 112 RVA: 0x00002AE9 File Offset: 0x00000CE9
		[Serialize]
		public float width { get; set; } = 170f;

		// Token: 0x06000071 RID: 113 RVA: 0x00002AF2 File Offset: 0x00000CF2
		public override AnalyticsIdentifier GetAnalyticsIdentifier()
		{
			AnalyticsIdentifier analyticsIdentifier = new AnalyticsIdentifier();
			analyticsIdentifier.Identifier = base.GetType().FullName;
			analyticsIdentifier.Namespace = base.GetType().Namespace;
			analyticsIdentifier.Hashcode = analyticsIdentifier.Identifier.GetHashCode();
			return analyticsIdentifier;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002B3F File Offset: 0x00000D3F
		StateGraph IState.get_graph()
		{
			return base.graph;
		}

		// Token: 0x04000005 RID: 5
		public const float DefaultWidth = 170f;

		// Token: 0x02000022 RID: 34
		public class Data : IGraphElementData
		{
			// Token: 0x0400002D RID: 45
			public bool isActive;

			// Token: 0x0400002E RID: 46
			public bool hasEntered;
		}

		// Token: 0x02000023 RID: 35
		public class DebugData : IStateDebugData, IGraphElementDebugData
		{
			// Token: 0x17000037 RID: 55
			// (get) Token: 0x060000D0 RID: 208 RVA: 0x000035BC File Offset: 0x000017BC
			// (set) Token: 0x060000D1 RID: 209 RVA: 0x000035C4 File Offset: 0x000017C4
			public int lastEnterFrame { get; set; }

			// Token: 0x17000038 RID: 56
			// (get) Token: 0x060000D2 RID: 210 RVA: 0x000035CD File Offset: 0x000017CD
			// (set) Token: 0x060000D3 RID: 211 RVA: 0x000035D5 File Offset: 0x000017D5
			public float lastExitTime { get; set; }

			// Token: 0x17000039 RID: 57
			// (get) Token: 0x060000D4 RID: 212 RVA: 0x000035DE File Offset: 0x000017DE
			// (set) Token: 0x060000D5 RID: 213 RVA: 0x000035E6 File Offset: 0x000017E6
			public Exception runtimeException { get; set; }
		}
	}
}
