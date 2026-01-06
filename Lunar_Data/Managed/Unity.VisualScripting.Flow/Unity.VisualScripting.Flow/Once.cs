using System;

namespace Unity.VisualScripting
{
	// Token: 0x0200003C RID: 60
	[UnitCategory("Control")]
	[UnitOrder(14)]
	public sealed class Once : Unit, IGraphElementWithData, IGraphElement, IGraphItem, INotifiedCollectionItem, IDisposable, IPrewarmable, IAotStubbable, IIdentifiable, IAnalyticsIdentifiable
	{
		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600024E RID: 590 RVA: 0x00006EB6 File Offset: 0x000050B6
		// (set) Token: 0x0600024F RID: 591 RVA: 0x00006EBE File Offset: 0x000050BE
		[DoNotSerialize]
		[PortLabelHidden]
		public ControlInput enter { get; private set; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000250 RID: 592 RVA: 0x00006EC7 File Offset: 0x000050C7
		// (set) Token: 0x06000251 RID: 593 RVA: 0x00006ECF File Offset: 0x000050CF
		[DoNotSerialize]
		public ControlInput reset { get; private set; }

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000252 RID: 594 RVA: 0x00006ED8 File Offset: 0x000050D8
		// (set) Token: 0x06000253 RID: 595 RVA: 0x00006EE0 File Offset: 0x000050E0
		[DoNotSerialize]
		public ControlOutput once { get; private set; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000254 RID: 596 RVA: 0x00006EE9 File Offset: 0x000050E9
		// (set) Token: 0x06000255 RID: 597 RVA: 0x00006EF1 File Offset: 0x000050F1
		[DoNotSerialize]
		public ControlOutput after { get; private set; }

		// Token: 0x06000256 RID: 598 RVA: 0x00006EFC File Offset: 0x000050FC
		protected override void Definition()
		{
			this.enter = base.ControlInput("enter", new Func<Flow, ControlOutput>(this.Enter));
			this.reset = base.ControlInput("reset", new Func<Flow, ControlOutput>(this.Reset));
			this.once = base.ControlOutput("once");
			this.after = base.ControlOutput("after");
			base.Succession(this.enter, this.once);
			base.Succession(this.enter, this.after);
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00006F89 File Offset: 0x00005189
		public IGraphElementData CreateData()
		{
			return new Once.Data();
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00006F90 File Offset: 0x00005190
		public ControlOutput Enter(Flow flow)
		{
			Once.Data elementData = flow.stack.GetElementData<Once.Data>(this);
			if (!elementData.executed)
			{
				elementData.executed = true;
				return this.once;
			}
			return this.after;
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00006FC6 File Offset: 0x000051C6
		public ControlOutput Reset(Flow flow)
		{
			flow.stack.GetElementData<Once.Data>(this).executed = false;
			return null;
		}

		// Token: 0x020001AE RID: 430
		public sealed class Data : IGraphElementData
		{
			// Token: 0x04000391 RID: 913
			public bool executed;
		}
	}
}
