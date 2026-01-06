using System;
using System.Collections.Generic;

namespace Unity.VisualScripting
{
	// Token: 0x0200003D RID: 61
	[UnitCategory("Control")]
	[UnitTitle("Select On Enum")]
	[UnitShortTitle("Select")]
	[UnitSubtitle("On Enum")]
	[UnitOrder(7)]
	[TypeIcon(typeof(ISelectUnit))]
	public sealed class SelectOnEnum : Unit, ISelectUnit, IUnit, IGraphElementWithDebugData, IGraphElement, IGraphItem, INotifiedCollectionItem, IDisposable, IPrewarmable, IAotStubbable, IIdentifiable, IAnalyticsIdentifiable
	{
		// Token: 0x170000CF RID: 207
		// (get) Token: 0x0600025B RID: 603 RVA: 0x00006FE3 File Offset: 0x000051E3
		// (set) Token: 0x0600025C RID: 604 RVA: 0x00006FEB File Offset: 0x000051EB
		[DoNotSerialize]
		public Dictionary<object, ValueInput> branches { get; private set; }

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600025D RID: 605 RVA: 0x00006FF4 File Offset: 0x000051F4
		// (set) Token: 0x0600025E RID: 606 RVA: 0x00006FFC File Offset: 0x000051FC
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueInput selector { get; private set; }

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600025F RID: 607 RVA: 0x00007005 File Offset: 0x00005205
		// (set) Token: 0x06000260 RID: 608 RVA: 0x0000700D File Offset: 0x0000520D
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueOutput selection { get; private set; }

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000261 RID: 609 RVA: 0x00007016 File Offset: 0x00005216
		// (set) Token: 0x06000262 RID: 610 RVA: 0x0000701E File Offset: 0x0000521E
		[Serialize]
		[Inspectable]
		[UnitHeaderInspectable]
		[TypeFilter(new Type[] { }, Enums = true, Classes = false, Interfaces = false, Structs = false, Primitives = false)]
		public Type enumType { get; set; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000263 RID: 611 RVA: 0x00007027 File Offset: 0x00005227
		public override bool canDefine
		{
			get
			{
				return this.enumType != null && this.enumType.IsEnum;
			}
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00007044 File Offset: 0x00005244
		protected override void Definition()
		{
			this.branches = new Dictionary<object, ValueInput>();
			this.selection = base.ValueOutput<object>("selection", new Func<Flow, object>(this.Branch)).Predictable();
			this.selector = base.ValueInput(this.enumType, "selector");
			base.Requirement(this.selector, this.selection);
			foreach (KeyValuePair<string, Enum> keyValuePair in EnumUtility.ValuesByNames(this.enumType, false))
			{
				Enum value = keyValuePair.Value;
				if (!this.branches.ContainsKey(value))
				{
					ValueInput valueInput = base.ValueInput<object>("%" + keyValuePair.Key).AllowsNull();
					this.branches.Add(value, valueInput);
					base.Requirement(valueInput, this.selection);
				}
			}
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000713C File Offset: 0x0000533C
		public object Branch(Flow flow)
		{
			object value = flow.GetValue(this.selector, this.enumType);
			return flow.GetValue(this.branches[value]);
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00007176 File Offset: 0x00005376
		FlowGraph IUnit.get_graph()
		{
			return base.graph;
		}
	}
}
