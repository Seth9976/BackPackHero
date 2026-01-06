using System;

namespace Unity.VisualScripting
{
	// Token: 0x020000B6 RID: 182
	[UnitCategory("Logic")]
	public abstract class BinaryComparisonUnit : Unit
	{
		// Token: 0x1700020A RID: 522
		// (get) Token: 0x0600054E RID: 1358 RVA: 0x0000B161 File Offset: 0x00009361
		// (set) Token: 0x0600054F RID: 1359 RVA: 0x0000B169 File Offset: 0x00009369
		[DoNotSerialize]
		public ValueInput a { get; private set; }

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000550 RID: 1360 RVA: 0x0000B172 File Offset: 0x00009372
		// (set) Token: 0x06000551 RID: 1361 RVA: 0x0000B17A File Offset: 0x0000937A
		[DoNotSerialize]
		public ValueInput b { get; private set; }

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000552 RID: 1362 RVA: 0x0000B183 File Offset: 0x00009383
		// (set) Token: 0x06000553 RID: 1363 RVA: 0x0000B18B File Offset: 0x0000938B
		[DoNotSerialize]
		public virtual ValueOutput comparison { get; private set; }

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000554 RID: 1364 RVA: 0x0000B194 File Offset: 0x00009394
		// (set) Token: 0x06000555 RID: 1365 RVA: 0x0000B19C File Offset: 0x0000939C
		[Serialize]
		[Inspectable]
		[InspectorToggleLeft]
		public bool numeric { get; set; } = true;

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000556 RID: 1366 RVA: 0x0000B1A5 File Offset: 0x000093A5
		protected virtual string outputKey
		{
			get
			{
				return "comparison";
			}
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x0000B1AC File Offset: 0x000093AC
		protected override void Definition()
		{
			if (this.numeric)
			{
				this.a = base.ValueInput<float>("a");
				this.b = base.ValueInput<float>("b", 0f);
				this.comparison = base.ValueOutput<bool>(this.outputKey, new Func<Flow, bool>(this.NumericComparison)).Predictable();
			}
			else
			{
				this.a = base.ValueInput<object>("a").AllowsNull();
				this.b = base.ValueInput<object>("b").AllowsNull();
				this.comparison = base.ValueOutput<bool>(this.outputKey, new Func<Flow, bool>(this.GenericComparison)).Predictable();
			}
			base.Requirement(this.a, this.comparison);
			base.Requirement(this.b, this.comparison);
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x0000B280 File Offset: 0x00009480
		private bool NumericComparison(Flow flow)
		{
			return this.NumericComparison(flow.GetValue<float>(this.a), flow.GetValue<float>(this.b));
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x0000B2A0 File Offset: 0x000094A0
		private bool GenericComparison(Flow flow)
		{
			return this.GenericComparison(flow.GetValue(this.a), flow.GetValue(this.b));
		}

		// Token: 0x0600055A RID: 1370
		protected abstract bool NumericComparison(float a, float b);

		// Token: 0x0600055B RID: 1371
		protected abstract bool GenericComparison(object a, object b);
	}
}
