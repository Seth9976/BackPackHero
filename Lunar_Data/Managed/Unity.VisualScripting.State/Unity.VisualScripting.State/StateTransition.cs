using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000018 RID: 24
	public abstract class StateTransition : GraphElement<StateGraph>, IStateTransition, IGraphElementWithDebugData, IGraphElement, IGraphItem, INotifiedCollectionItem, IDisposable, IPrewarmable, IAotStubbable, IIdentifiable, IAnalyticsIdentifiable, IConnection<IState, IState>
	{
		// Token: 0x06000092 RID: 146 RVA: 0x00003097 File Offset: 0x00001297
		protected StateTransition()
		{
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000030A0 File Offset: 0x000012A0
		protected StateTransition(IState source, IState destination)
		{
			Ensure.That("source").IsNotNull<IState>(source);
			Ensure.That("destination").IsNotNull<IState>(destination);
			if (source.graph != destination.graph)
			{
				throw new NotSupportedException("Cannot create transitions across state graphs.");
			}
			this.source = source;
			this.destination = destination;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000030FA File Offset: 0x000012FA
		public IGraphElementDebugData CreateDebugData()
		{
			return new StateTransition.DebugData();
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00003101 File Offset: 0x00001301
		public override int dependencyOrder
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00003104 File Offset: 0x00001304
		// (set) Token: 0x06000097 RID: 151 RVA: 0x0000310C File Offset: 0x0000130C
		[Serialize]
		public IState source { get; internal set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00003115 File Offset: 0x00001315
		// (set) Token: 0x06000099 RID: 153 RVA: 0x0000311D File Offset: 0x0000131D
		[Serialize]
		public IState destination { get; internal set; }

		// Token: 0x0600009A RID: 154 RVA: 0x00003128 File Offset: 0x00001328
		public override void Instantiate(GraphReference instance)
		{
			base.Instantiate(instance);
			IGraphEventListener graphEventListener = this as IGraphEventListener;
			if (graphEventListener != null && instance.GetElementData<State.Data>(this.source).isActive)
			{
				graphEventListener.StartListening(instance);
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003160 File Offset: 0x00001360
		public override void Uninstantiate(GraphReference instance)
		{
			IGraphEventListener graphEventListener = this as IGraphEventListener;
			if (graphEventListener != null)
			{
				graphEventListener.StopListening(instance);
			}
			base.Uninstantiate(instance);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003188 File Offset: 0x00001388
		public void Branch(Flow flow)
		{
			if (flow.enableDebug)
			{
				StateTransition.DebugData elementDebugData = flow.stack.GetElementDebugData<StateTransition.DebugData>(this);
				elementDebugData.lastBranchFrame = EditorTimeBinding.frame;
				elementDebugData.lastBranchTime = EditorTimeBinding.time;
			}
			try
			{
				this.source.OnExit(flow, StateExitReason.Branch);
			}
			catch (Exception ex)
			{
				this.source.HandleException(flow.stack, ex);
				throw;
			}
			this.source.OnBranchTo(flow, this.destination);
			try
			{
				this.destination.OnEnter(flow, StateEnterReason.Branch);
			}
			catch (Exception ex2)
			{
				this.destination.HandleException(flow.stack, ex2);
				throw;
			}
		}

		// Token: 0x0600009D RID: 157
		public abstract void OnEnter(Flow flow);

		// Token: 0x0600009E RID: 158
		public abstract void OnExit(Flow flow);

		// Token: 0x0600009F RID: 159 RVA: 0x00003238 File Offset: 0x00001438
		public override AnalyticsIdentifier GetAnalyticsIdentifier()
		{
			return null;
		}

		// Token: 0x02000025 RID: 37
		public class DebugData : IStateTransitionDebugData, IGraphElementDebugData
		{
			// Token: 0x1700003A RID: 58
			// (get) Token: 0x060000DA RID: 218 RVA: 0x00003613 File Offset: 0x00001813
			// (set) Token: 0x060000DB RID: 219 RVA: 0x0000361B File Offset: 0x0000181B
			public Exception runtimeException { get; set; }

			// Token: 0x1700003B RID: 59
			// (get) Token: 0x060000DC RID: 220 RVA: 0x00003624 File Offset: 0x00001824
			// (set) Token: 0x060000DD RID: 221 RVA: 0x0000362C File Offset: 0x0000182C
			public int lastBranchFrame { get; set; }

			// Token: 0x1700003C RID: 60
			// (get) Token: 0x060000DE RID: 222 RVA: 0x00003635 File Offset: 0x00001835
			// (set) Token: 0x060000DF RID: 223 RVA: 0x0000363D File Offset: 0x0000183D
			public float lastBranchTime { get; set; }
		}
	}
}
