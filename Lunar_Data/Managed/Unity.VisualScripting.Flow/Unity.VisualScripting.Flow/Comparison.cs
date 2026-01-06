using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x020000B7 RID: 183
	[UnitCategory("Logic")]
	[UnitTitle("Comparison")]
	[UnitShortTitle("Comparison")]
	[UnitOrder(99)]
	public sealed class Comparison : Unit
	{
		// Token: 0x1700020F RID: 527
		// (get) Token: 0x0600055D RID: 1373 RVA: 0x0000B2CF File Offset: 0x000094CF
		// (set) Token: 0x0600055E RID: 1374 RVA: 0x0000B2D7 File Offset: 0x000094D7
		[DoNotSerialize]
		public ValueInput a { get; private set; }

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x0600055F RID: 1375 RVA: 0x0000B2E0 File Offset: 0x000094E0
		// (set) Token: 0x06000560 RID: 1376 RVA: 0x0000B2E8 File Offset: 0x000094E8
		[DoNotSerialize]
		public ValueInput b { get; private set; }

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000561 RID: 1377 RVA: 0x0000B2F1 File Offset: 0x000094F1
		// (set) Token: 0x06000562 RID: 1378 RVA: 0x0000B2F9 File Offset: 0x000094F9
		[Serialize]
		[Inspectable]
		public bool numeric { get; set; } = true;

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000563 RID: 1379 RVA: 0x0000B302 File Offset: 0x00009502
		// (set) Token: 0x06000564 RID: 1380 RVA: 0x0000B30A File Offset: 0x0000950A
		[DoNotSerialize]
		[PortLabel("A < B")]
		public ValueOutput aLessThanB { get; private set; }

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000565 RID: 1381 RVA: 0x0000B313 File Offset: 0x00009513
		// (set) Token: 0x06000566 RID: 1382 RVA: 0x0000B31B File Offset: 0x0000951B
		[DoNotSerialize]
		[PortLabel("A ≤ B")]
		public ValueOutput aLessThanOrEqualToB { get; private set; }

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000567 RID: 1383 RVA: 0x0000B324 File Offset: 0x00009524
		// (set) Token: 0x06000568 RID: 1384 RVA: 0x0000B32C File Offset: 0x0000952C
		[DoNotSerialize]
		[PortLabel("A = B")]
		public ValueOutput aEqualToB { get; private set; }

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000569 RID: 1385 RVA: 0x0000B335 File Offset: 0x00009535
		// (set) Token: 0x0600056A RID: 1386 RVA: 0x0000B33D File Offset: 0x0000953D
		[DoNotSerialize]
		[PortLabel("A ≠ B")]
		public ValueOutput aNotEqualToB { get; private set; }

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x0600056B RID: 1387 RVA: 0x0000B346 File Offset: 0x00009546
		// (set) Token: 0x0600056C RID: 1388 RVA: 0x0000B34E File Offset: 0x0000954E
		[DoNotSerialize]
		[PortLabel("A ≥ B")]
		public ValueOutput aGreaterThanOrEqualToB { get; private set; }

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x0600056D RID: 1389 RVA: 0x0000B357 File Offset: 0x00009557
		// (set) Token: 0x0600056E RID: 1390 RVA: 0x0000B35F File Offset: 0x0000955F
		[DoNotSerialize]
		[PortLabel("A > B")]
		public ValueOutput aGreatherThanB { get; private set; }

		// Token: 0x0600056F RID: 1391 RVA: 0x0000B368 File Offset: 0x00009568
		protected override void Definition()
		{
			if (this.numeric)
			{
				this.a = base.ValueInput<float>("a");
				this.b = base.ValueInput<float>("b", 0f);
				this.aLessThanB = base.ValueOutput<bool>("aLessThanB", (Flow flow) => this.NumericLess(flow.GetValue<float>(this.a), flow.GetValue<float>(this.b))).Predictable();
				this.aLessThanOrEqualToB = base.ValueOutput<bool>("aLessThanOrEqualToB", (Flow flow) => this.NumericLessOrEqual(flow.GetValue<float>(this.a), flow.GetValue<float>(this.b))).Predictable();
				this.aEqualToB = base.ValueOutput<bool>("aEqualToB", (Flow flow) => this.NumericEqual(flow.GetValue<float>(this.a), flow.GetValue<float>(this.b))).Predictable();
				this.aNotEqualToB = base.ValueOutput<bool>("aNotEqualToB", (Flow flow) => this.NumericNotEqual(flow.GetValue<float>(this.a), flow.GetValue<float>(this.b))).Predictable();
				this.aGreaterThanOrEqualToB = base.ValueOutput<bool>("aGreaterThanOrEqualToB", (Flow flow) => this.NumericGreaterOrEqual(flow.GetValue<float>(this.a), flow.GetValue<float>(this.b))).Predictable();
				this.aGreatherThanB = base.ValueOutput<bool>("aGreatherThanB", (Flow flow) => this.NumericGreater(flow.GetValue<float>(this.a), flow.GetValue<float>(this.b))).Predictable();
			}
			else
			{
				this.a = base.ValueInput<object>("a").AllowsNull();
				this.b = base.ValueInput<object>("b").AllowsNull();
				this.aLessThanB = base.ValueOutput<bool>("aLessThanB", (Flow flow) => this.GenericLess(flow.GetValue(this.a), flow.GetValue(this.b)));
				this.aLessThanOrEqualToB = base.ValueOutput<bool>("aLessThanOrEqualToB", (Flow flow) => this.GenericLessOrEqual(flow.GetValue(this.a), flow.GetValue(this.b)));
				this.aEqualToB = base.ValueOutput<bool>("aEqualToB", (Flow flow) => this.GenericEqual(flow.GetValue(this.a), flow.GetValue(this.b)));
				this.aNotEqualToB = base.ValueOutput<bool>("aNotEqualToB", (Flow flow) => this.GenericNotEqual(flow.GetValue(this.a), flow.GetValue(this.b)));
				this.aGreaterThanOrEqualToB = base.ValueOutput<bool>("aGreaterThanOrEqualToB", (Flow flow) => this.GenericGreaterOrEqual(flow.GetValue(this.a), flow.GetValue(this.b)));
				this.aGreatherThanB = base.ValueOutput<bool>("aGreatherThanB", (Flow flow) => this.GenericGreater(flow.GetValue(this.a), flow.GetValue(this.b)));
			}
			base.Requirement(this.a, this.aLessThanB);
			base.Requirement(this.b, this.aLessThanB);
			base.Requirement(this.a, this.aLessThanOrEqualToB);
			base.Requirement(this.b, this.aLessThanOrEqualToB);
			base.Requirement(this.a, this.aEqualToB);
			base.Requirement(this.b, this.aEqualToB);
			base.Requirement(this.a, this.aNotEqualToB);
			base.Requirement(this.b, this.aNotEqualToB);
			base.Requirement(this.a, this.aGreaterThanOrEqualToB);
			base.Requirement(this.b, this.aGreaterThanOrEqualToB);
			base.Requirement(this.a, this.aGreatherThanB);
			base.Requirement(this.b, this.aGreatherThanB);
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x0000B62A File Offset: 0x0000982A
		private bool NumericLess(float a, float b)
		{
			return a < b;
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x0000B630 File Offset: 0x00009830
		private bool NumericLessOrEqual(float a, float b)
		{
			return a < b || Mathf.Approximately(a, b);
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x0000B63F File Offset: 0x0000983F
		private bool NumericEqual(float a, float b)
		{
			return Mathf.Approximately(a, b);
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x0000B648 File Offset: 0x00009848
		private bool NumericNotEqual(float a, float b)
		{
			return !Mathf.Approximately(a, b);
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x0000B654 File Offset: 0x00009854
		private bool NumericGreaterOrEqual(float a, float b)
		{
			return a > b || Mathf.Approximately(a, b);
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x0000B663 File Offset: 0x00009863
		private bool NumericGreater(float a, float b)
		{
			return a > b;
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x0000B669 File Offset: 0x00009869
		private bool GenericLess(object a, object b)
		{
			return OperatorUtility.LessThan(a, b);
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x0000B672 File Offset: 0x00009872
		private bool GenericLessOrEqual(object a, object b)
		{
			return OperatorUtility.LessThanOrEqual(a, b);
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x0000B67B File Offset: 0x0000987B
		private bool GenericEqual(object a, object b)
		{
			return OperatorUtility.Equal(a, b);
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x0000B684 File Offset: 0x00009884
		private bool GenericNotEqual(object a, object b)
		{
			return OperatorUtility.NotEqual(a, b);
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x0000B68D File Offset: 0x0000988D
		private bool GenericGreaterOrEqual(object a, object b)
		{
			return OperatorUtility.GreaterThanOrEqual(a, b);
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x0000B696 File Offset: 0x00009896
		private bool GenericGreater(object a, object b)
		{
			return OperatorUtility.GreaterThan(a, b);
		}
	}
}
