using System;
using System.Collections;

namespace Unity.VisualScripting
{
	// Token: 0x02000036 RID: 54
	[UnitTitle("For Loop")]
	[UnitCategory("Control")]
	[UnitOrder(9)]
	public sealed class For : LoopUnit
	{
		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000216 RID: 534 RVA: 0x000067EF File Offset: 0x000049EF
		// (set) Token: 0x06000217 RID: 535 RVA: 0x000067F7 File Offset: 0x000049F7
		[PortLabel("First")]
		[DoNotSerialize]
		public ValueInput firstIndex { get; private set; }

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000218 RID: 536 RVA: 0x00006800 File Offset: 0x00004A00
		// (set) Token: 0x06000219 RID: 537 RVA: 0x00006808 File Offset: 0x00004A08
		[PortLabel("Last")]
		[DoNotSerialize]
		public ValueInput lastIndex { get; private set; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600021A RID: 538 RVA: 0x00006811 File Offset: 0x00004A11
		// (set) Token: 0x0600021B RID: 539 RVA: 0x00006819 File Offset: 0x00004A19
		[DoNotSerialize]
		public ValueInput step { get; private set; }

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600021C RID: 540 RVA: 0x00006822 File Offset: 0x00004A22
		// (set) Token: 0x0600021D RID: 541 RVA: 0x0000682A File Offset: 0x00004A2A
		[PortLabel("Index")]
		[DoNotSerialize]
		public ValueOutput currentIndex { get; private set; }

		// Token: 0x0600021E RID: 542 RVA: 0x00006834 File Offset: 0x00004A34
		protected override void Definition()
		{
			this.firstIndex = base.ValueInput<int>("firstIndex", 0);
			this.lastIndex = base.ValueInput<int>("lastIndex", 10);
			this.step = base.ValueInput<int>("step", 1);
			this.currentIndex = base.ValueOutput<int>("currentIndex");
			base.Definition();
			base.Requirement(this.firstIndex, base.enter);
			base.Requirement(this.lastIndex, base.enter);
			base.Requirement(this.step, base.enter);
			base.Assignment(base.enter, this.currentIndex);
		}

		// Token: 0x0600021F RID: 543 RVA: 0x000068D8 File Offset: 0x00004AD8
		private int Start(Flow flow, out int currentIndex, out int lastIndex, out bool ascending)
		{
			int value = flow.GetValue<int>(this.firstIndex);
			lastIndex = flow.GetValue<int>(this.lastIndex);
			ascending = value <= lastIndex;
			currentIndex = value;
			flow.SetValue(this.currentIndex, currentIndex);
			return flow.EnterLoop();
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00006927 File Offset: 0x00004B27
		private bool CanMoveNext(int currentIndex, int lastIndex, bool ascending)
		{
			if (ascending)
			{
				return currentIndex < lastIndex;
			}
			return currentIndex > lastIndex;
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00006935 File Offset: 0x00004B35
		private void MoveNext(Flow flow, ref int currentIndex)
		{
			currentIndex += flow.GetValue<int>(this.step);
			flow.SetValue(this.currentIndex, currentIndex);
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000695C File Offset: 0x00004B5C
		protected override ControlOutput Loop(Flow flow)
		{
			int num2;
			int num3;
			bool flag;
			int num = this.Start(flow, out num2, out num3, out flag);
			if (!this.IsStepValueZero())
			{
				GraphStack graphStack = flow.PreserveStack();
				while (flow.LoopIsNotBroken(num) && this.CanMoveNext(num2, num3, flag))
				{
					flow.Invoke(base.body);
					flow.RestoreStack(graphStack);
					this.MoveNext(flow, ref num2);
				}
				flow.DisposePreservedStack(graphStack);
			}
			flow.ExitLoop(num);
			return base.exit;
		}

		// Token: 0x06000223 RID: 547 RVA: 0x000069CF File Offset: 0x00004BCF
		protected override IEnumerator LoopCoroutine(Flow flow)
		{
			int currentIndex;
			int lastIndex;
			bool ascending;
			int loop = this.Start(flow, out currentIndex, out lastIndex, out ascending);
			GraphStack stack = flow.PreserveStack();
			while (flow.LoopIsNotBroken(loop) && this.CanMoveNext(currentIndex, lastIndex, ascending))
			{
				yield return base.body;
				flow.RestoreStack(stack);
				this.MoveNext(flow, ref currentIndex);
			}
			flow.DisposePreservedStack(stack);
			flow.ExitLoop(loop);
			yield return base.exit;
			yield break;
		}

		// Token: 0x06000224 RID: 548 RVA: 0x000069E8 File Offset: 0x00004BE8
		public bool IsStepValueZero()
		{
			bool flag = !this.step.hasValidConnection && (int)base.defaultValues[this.step.key] == 0;
			bool flag2 = false;
			if (this.step.hasValidConnection)
			{
				Literal literal = this.step.connection.source.unit as Literal;
				if (literal != null && Convert.ToInt32(literal.value) == 0)
				{
					flag2 = true;
				}
			}
			return flag || flag2;
		}
	}
}
