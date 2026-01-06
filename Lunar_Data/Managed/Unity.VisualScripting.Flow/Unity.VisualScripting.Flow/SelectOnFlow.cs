using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x0200003E RID: 62
	[UnitCategory("Control")]
	[UnitTitle("Select On Flow")]
	[UnitShortTitle("Select")]
	[UnitSubtitle("On Flow")]
	[UnitOrder(8)]
	[TypeIcon(typeof(ISelectUnit))]
	public sealed class SelectOnFlow : Unit, ISelectUnit, IUnit, IGraphElementWithDebugData, IGraphElement, IGraphItem, INotifiedCollectionItem, IDisposable, IPrewarmable, IAotStubbable, IIdentifiable, IAnalyticsIdentifiable
	{
		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000268 RID: 616 RVA: 0x0000717E File Offset: 0x0000537E
		// (set) Token: 0x06000269 RID: 617 RVA: 0x00007186 File Offset: 0x00005386
		[DoNotSerialize]
		[Inspectable]
		[UnitHeaderInspectable("Branches")]
		public int branchCount
		{
			get
			{
				return this._branchCount;
			}
			set
			{
				this._branchCount = Mathf.Clamp(value, 2, 10);
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600026A RID: 618 RVA: 0x00007197 File Offset: 0x00005397
		// (set) Token: 0x0600026B RID: 619 RVA: 0x0000719F File Offset: 0x0000539F
		[DoNotSerialize]
		public Dictionary<ControlInput, ValueInput> branches { get; private set; }

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600026C RID: 620 RVA: 0x000071A8 File Offset: 0x000053A8
		// (set) Token: 0x0600026D RID: 621 RVA: 0x000071B0 File Offset: 0x000053B0
		[DoNotSerialize]
		[PortLabelHidden]
		public ControlOutput exit { get; private set; }

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600026E RID: 622 RVA: 0x000071B9 File Offset: 0x000053B9
		// (set) Token: 0x0600026F RID: 623 RVA: 0x000071C1 File Offset: 0x000053C1
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueOutput selection { get; private set; }

		// Token: 0x06000270 RID: 624 RVA: 0x000071CC File Offset: 0x000053CC
		protected override void Definition()
		{
			this.branches = new Dictionary<ControlInput, ValueInput>();
			this.selection = base.ValueOutput<object>("selection");
			this.exit = base.ControlOutput("exit");
			for (int i = 0; i < this.branchCount; i++)
			{
				ValueInput branchValue = base.ValueInput<object>("value_" + i.ToString());
				ControlInput controlInput = base.ControlInput("enter_" + i.ToString(), (Flow flow) => this.Select(flow, branchValue));
				base.Requirement(branchValue, controlInput);
				base.Assignment(controlInput, this.selection);
				base.Succession(controlInput, this.exit);
				this.branches.Add(controlInput, branchValue);
			}
		}

		// Token: 0x06000271 RID: 625 RVA: 0x000072A4 File Offset: 0x000054A4
		public ControlOutput Select(Flow flow, ValueInput branchValue)
		{
			flow.SetValue(this.selection, flow.GetValue(branchValue));
			return this.exit;
		}

		// Token: 0x06000273 RID: 627 RVA: 0x000072CE File Offset: 0x000054CE
		FlowGraph IUnit.get_graph()
		{
			return base.graph;
		}

		// Token: 0x040000B7 RID: 183
		[SerializeAs("branchCount")]
		private int _branchCount = 2;
	}
}
