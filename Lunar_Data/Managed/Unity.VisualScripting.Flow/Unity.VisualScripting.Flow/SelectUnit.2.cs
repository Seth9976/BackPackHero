using System;
using System.Collections.Generic;

namespace Unity.VisualScripting
{
	// Token: 0x02000042 RID: 66
	[TypeIcon(typeof(ISelectUnit))]
	public abstract class SelectUnit<T> : Unit, ISelectUnit, IUnit, IGraphElementWithDebugData, IGraphElement, IGraphItem, INotifiedCollectionItem, IDisposable, IPrewarmable, IAotStubbable, IIdentifiable, IAnalyticsIdentifiable
	{
		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000285 RID: 645 RVA: 0x0000743A File Offset: 0x0000563A
		// (set) Token: 0x06000286 RID: 646 RVA: 0x00007442 File Offset: 0x00005642
		[DoNotSerialize]
		public List<KeyValuePair<T, ValueInput>> branches { get; private set; }

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000287 RID: 647 RVA: 0x0000744B File Offset: 0x0000564B
		// (set) Token: 0x06000288 RID: 648 RVA: 0x00007453 File Offset: 0x00005653
		[Inspectable]
		[Serialize]
		public List<T> options { get; set; } = new List<T>();

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000289 RID: 649 RVA: 0x0000745C File Offset: 0x0000565C
		// (set) Token: 0x0600028A RID: 650 RVA: 0x00007464 File Offset: 0x00005664
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueInput selector { get; private set; }

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600028B RID: 651 RVA: 0x0000746D File Offset: 0x0000566D
		// (set) Token: 0x0600028C RID: 652 RVA: 0x00007475 File Offset: 0x00005675
		[DoNotSerialize]
		public ValueInput @default { get; private set; }

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600028D RID: 653 RVA: 0x0000747E File Offset: 0x0000567E
		// (set) Token: 0x0600028E RID: 654 RVA: 0x00007486 File Offset: 0x00005686
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueOutput selection { get; private set; }

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600028F RID: 655 RVA: 0x0000748F File Offset: 0x0000568F
		public override bool canDefine
		{
			get
			{
				return this.options != null;
			}
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000749C File Offset: 0x0000569C
		protected override void Definition()
		{
			this.selection = base.ValueOutput<object>("selection", new Func<Flow, object>(this.Result)).Predictable();
			this.selector = base.ValueInput<T>("selector");
			base.Requirement(this.selector, this.selection);
			this.branches = new List<KeyValuePair<T, ValueInput>>();
			foreach (T t in this.options)
			{
				string text = "%";
				T t2 = t;
				string text2 = text + ((t2 != null) ? t2.ToString() : null);
				if (!base.valueInputs.Contains(text2))
				{
					ValueInput valueInput = base.ValueInput<object>(text2).AllowsNull();
					this.branches.Add(new KeyValuePair<T, ValueInput>(t, valueInput));
					base.Requirement(valueInput, this.selection);
				}
			}
			this.@default = base.ValueInput<object>("default");
			base.Requirement(this.@default, this.selection);
		}

		// Token: 0x06000291 RID: 657 RVA: 0x000075C0 File Offset: 0x000057C0
		protected virtual bool Matches(T a, T b)
		{
			return object.Equals(a, b);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x000075D4 File Offset: 0x000057D4
		public object Result(Flow flow)
		{
			T value = flow.GetValue<T>(this.selector);
			foreach (KeyValuePair<T, ValueInput> keyValuePair in this.branches)
			{
				if (this.Matches(keyValuePair.Key, value))
				{
					return flow.GetValue(keyValuePair.Value);
				}
			}
			return flow.GetValue(this.@default);
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000766F File Offset: 0x0000586F
		FlowGraph IUnit.get_graph()
		{
			return base.graph;
		}
	}
}
