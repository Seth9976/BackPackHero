using System;
using System.Collections.Generic;

namespace Unity.VisualScripting
{
	// Token: 0x02000044 RID: 68
	[UnitCategory("Control")]
	[UnitTitle("Switch On Enum")]
	[UnitShortTitle("Switch")]
	[UnitSubtitle("On Enum")]
	[UnitOrder(3)]
	[TypeIcon(typeof(IBranchUnit))]
	public sealed class SwitchOnEnum : Unit, IBranchUnit, IUnit, IGraphElementWithDebugData, IGraphElement, IGraphItem, INotifiedCollectionItem, IDisposable, IPrewarmable, IAotStubbable, IIdentifiable, IAnalyticsIdentifiable
	{
		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x000077CE File Offset: 0x000059CE
		// (set) Token: 0x060002A1 RID: 673 RVA: 0x000077D6 File Offset: 0x000059D6
		[DoNotSerialize]
		public Dictionary<Enum, ControlOutput> branches { get; private set; }

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x000077DF File Offset: 0x000059DF
		// (set) Token: 0x060002A3 RID: 675 RVA: 0x000077E7 File Offset: 0x000059E7
		[Serialize]
		[Inspectable]
		[UnitHeaderInspectable]
		[TypeFilter(new Type[] { }, Enums = true, Classes = false, Interfaces = false, Structs = false, Primitives = false)]
		public Type enumType { get; set; }

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x000077F0 File Offset: 0x000059F0
		// (set) Token: 0x060002A5 RID: 677 RVA: 0x000077F8 File Offset: 0x000059F8
		[DoNotSerialize]
		[PortLabelHidden]
		public ControlInput enter { get; private set; }

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x00007801 File Offset: 0x00005A01
		// (set) Token: 0x060002A7 RID: 679 RVA: 0x00007809 File Offset: 0x00005A09
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueInput @enum { get; private set; }

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x00007812 File Offset: 0x00005A12
		public override bool canDefine
		{
			get
			{
				return this.enumType != null && this.enumType.IsEnum;
			}
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x00007830 File Offset: 0x00005A30
		protected override void Definition()
		{
			this.branches = new Dictionary<Enum, ControlOutput>();
			this.enter = base.ControlInput("enter", new Func<Flow, ControlOutput>(this.Enter));
			this.@enum = base.ValueInput(this.enumType, "enum");
			base.Requirement(this.@enum, this.enter);
			foreach (KeyValuePair<string, Enum> keyValuePair in EnumUtility.ValuesByNames(this.enumType, false))
			{
				string key = keyValuePair.Key;
				Enum value = keyValuePair.Value;
				if (!this.branches.ContainsKey(value))
				{
					ControlOutput controlOutput = base.ControlOutput("%" + key);
					this.branches.Add(value, controlOutput);
					base.Succession(this.enter, controlOutput);
				}
			}
		}

		// Token: 0x060002AA RID: 682 RVA: 0x00007920 File Offset: 0x00005B20
		public ControlOutput Enter(Flow flow)
		{
			Enum @enum = (Enum)flow.GetValue(this.@enum, this.enumType);
			if (this.branches.ContainsKey(@enum))
			{
				return this.branches[@enum];
			}
			return null;
		}

		// Token: 0x060002AC RID: 684 RVA: 0x00007969 File Offset: 0x00005B69
		FlowGraph IUnit.get_graph()
		{
			return base.graph;
		}
	}
}
