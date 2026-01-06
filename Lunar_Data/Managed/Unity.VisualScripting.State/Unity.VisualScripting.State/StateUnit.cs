using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000019 RID: 25
	[TypeIcon(typeof(StateGraph))]
	[UnitCategory("Nesting")]
	public sealed class StateUnit : NesterUnit<StateGraph, StateGraphAsset>
	{
		// Token: 0x060000A0 RID: 160 RVA: 0x0000323B File Offset: 0x0000143B
		public StateUnit()
		{
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003243 File Offset: 0x00001443
		public StateUnit(StateGraphAsset macro)
			: base(macro)
		{
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x0000324C File Offset: 0x0000144C
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x00003254 File Offset: 0x00001454
		[DoNotSerialize]
		public ControlInput start { get; private set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x0000325D File Offset: 0x0000145D
		// (set) Token: 0x060000A5 RID: 165 RVA: 0x00003265 File Offset: 0x00001465
		[DoNotSerialize]
		public ControlInput stop { get; private set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x0000326E File Offset: 0x0000146E
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x00003276 File Offset: 0x00001476
		[DoNotSerialize]
		public ControlOutput started { get; private set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x0000327F File Offset: 0x0000147F
		// (set) Token: 0x060000A9 RID: 169 RVA: 0x00003287 File Offset: 0x00001487
		[DoNotSerialize]
		public ControlOutput stopped { get; private set; }

		// Token: 0x060000AA RID: 170 RVA: 0x00003290 File Offset: 0x00001490
		public static StateUnit WithStart()
		{
			return new StateUnit
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

		// Token: 0x060000AB RID: 171 RVA: 0x000032B4 File Offset: 0x000014B4
		protected override void Definition()
		{
			this.start = base.ControlInput("start", new Func<Flow, ControlOutput>(this.Start));
			this.stop = base.ControlInput("stop", new Func<Flow, ControlOutput>(this.Stop));
			this.started = base.ControlOutput("started");
			this.stopped = base.ControlOutput("stopped");
			base.Succession(this.start, this.started);
			base.Succession(this.stop, this.stopped);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003341 File Offset: 0x00001541
		private ControlOutput Start(Flow flow)
		{
			flow.stack.EnterParentElement(this);
			base.nest.graph.Start(flow);
			flow.stack.ExitParentElement();
			return this.started;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003371 File Offset: 0x00001571
		private ControlOutput Stop(Flow flow)
		{
			flow.stack.EnterParentElement(this);
			base.nest.graph.Stop(flow);
			flow.stack.ExitParentElement();
			return this.stopped;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x000033A1 File Offset: 0x000015A1
		public override StateGraph DefaultGraph()
		{
			return StateGraph.WithStart();
		}
	}
}
