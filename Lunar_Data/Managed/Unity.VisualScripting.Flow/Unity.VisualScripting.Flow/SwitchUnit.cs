using System;
using System.Collections.Generic;

namespace Unity.VisualScripting
{
	// Token: 0x02000047 RID: 71
	[TypeIcon(typeof(IBranchUnit))]
	public abstract class SwitchUnit<T> : Unit, IBranchUnit, IUnit, IGraphElementWithDebugData, IGraphElement, IGraphItem, INotifiedCollectionItem, IDisposable, IPrewarmable, IAotStubbable, IIdentifiable, IAnalyticsIdentifiable
	{
		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x000079B9 File Offset: 0x00005BB9
		// (set) Token: 0x060002B3 RID: 691 RVA: 0x000079C1 File Offset: 0x00005BC1
		[DoNotSerialize]
		public List<KeyValuePair<T, ControlOutput>> branches { get; private set; }

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x000079CA File Offset: 0x00005BCA
		// (set) Token: 0x060002B5 RID: 693 RVA: 0x000079D2 File Offset: 0x00005BD2
		[Inspectable]
		[Serialize]
		public List<T> options { get; set; } = new List<T>();

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x000079DB File Offset: 0x00005BDB
		// (set) Token: 0x060002B7 RID: 695 RVA: 0x000079E3 File Offset: 0x00005BE3
		[DoNotSerialize]
		[PortLabelHidden]
		public ControlInput enter { get; private set; }

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x000079EC File Offset: 0x00005BEC
		// (set) Token: 0x060002B9 RID: 697 RVA: 0x000079F4 File Offset: 0x00005BF4
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueInput selector { get; private set; }

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060002BA RID: 698 RVA: 0x000079FD File Offset: 0x00005BFD
		// (set) Token: 0x060002BB RID: 699 RVA: 0x00007A05 File Offset: 0x00005C05
		[DoNotSerialize]
		public ControlOutput @default { get; private set; }

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060002BC RID: 700 RVA: 0x00007A0E File Offset: 0x00005C0E
		public override bool canDefine
		{
			get
			{
				return this.options != null;
			}
		}

		// Token: 0x060002BD RID: 701 RVA: 0x00007A1C File Offset: 0x00005C1C
		protected override void Definition()
		{
			this.enter = base.ControlInput("enter", new Func<Flow, ControlOutput>(this.Enter));
			this.selector = base.ValueInput<T>("selector");
			base.Requirement(this.selector, this.enter);
			this.branches = new List<KeyValuePair<T, ControlOutput>>();
			foreach (T t in this.options)
			{
				string text = "%";
				T t2 = t;
				string text2 = text + ((t2 != null) ? t2.ToString() : null);
				if (!base.controlOutputs.Contains(text2))
				{
					ControlOutput controlOutput = base.ControlOutput(text2);
					this.branches.Add(new KeyValuePair<T, ControlOutput>(t, controlOutput));
					base.Succession(this.enter, controlOutput);
				}
			}
			this.@default = base.ControlOutput("default");
			base.Succession(this.enter, this.@default);
		}

		// Token: 0x060002BE RID: 702 RVA: 0x00007B34 File Offset: 0x00005D34
		protected virtual bool Matches(T a, T b)
		{
			return object.Equals(a, b);
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00007B48 File Offset: 0x00005D48
		public ControlOutput Enter(Flow flow)
		{
			T value = flow.GetValue<T>(this.selector);
			foreach (KeyValuePair<T, ControlOutput> keyValuePair in this.branches)
			{
				if (this.Matches(keyValuePair.Key, value))
				{
					return keyValuePair.Value;
				}
			}
			return this.@default;
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00007BD7 File Offset: 0x00005DD7
		FlowGraph IUnit.get_graph()
		{
			return base.graph;
		}
	}
}
