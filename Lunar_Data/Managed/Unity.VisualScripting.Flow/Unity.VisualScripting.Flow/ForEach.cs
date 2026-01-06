using System;
using System.Collections;

namespace Unity.VisualScripting
{
	// Token: 0x02000037 RID: 55
	[UnitTitle("For Each Loop")]
	[UnitCategory("Control")]
	[UnitOrder(10)]
	public class ForEach : LoopUnit
	{
		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000226 RID: 550 RVA: 0x00006A69 File Offset: 0x00004C69
		// (set) Token: 0x06000227 RID: 551 RVA: 0x00006A71 File Offset: 0x00004C71
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueInput collection { get; private set; }

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000228 RID: 552 RVA: 0x00006A7A File Offset: 0x00004C7A
		// (set) Token: 0x06000229 RID: 553 RVA: 0x00006A82 File Offset: 0x00004C82
		[DoNotSerialize]
		[PortLabel("Index")]
		public ValueOutput currentIndex { get; private set; }

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600022A RID: 554 RVA: 0x00006A8B File Offset: 0x00004C8B
		// (set) Token: 0x0600022B RID: 555 RVA: 0x00006A93 File Offset: 0x00004C93
		[DoNotSerialize]
		[PortLabel("Key")]
		public ValueOutput currentKey { get; private set; }

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600022C RID: 556 RVA: 0x00006A9C File Offset: 0x00004C9C
		// (set) Token: 0x0600022D RID: 557 RVA: 0x00006AA4 File Offset: 0x00004CA4
		[DoNotSerialize]
		[PortLabel("Item")]
		public ValueOutput currentItem { get; private set; }

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600022E RID: 558 RVA: 0x00006AAD File Offset: 0x00004CAD
		// (set) Token: 0x0600022F RID: 559 RVA: 0x00006AB5 File Offset: 0x00004CB5
		[Serialize]
		[Inspectable]
		[UnitHeaderInspectable("Dictionary")]
		[InspectorToggleLeft]
		public bool dictionary { get; set; }

		// Token: 0x06000230 RID: 560 RVA: 0x00006AC0 File Offset: 0x00004CC0
		protected override void Definition()
		{
			base.Definition();
			if (this.dictionary)
			{
				this.collection = base.ValueInput<IDictionary>("collection");
			}
			else
			{
				this.collection = base.ValueInput<IEnumerable>("collection");
			}
			this.currentIndex = base.ValueOutput<int>("currentIndex");
			if (this.dictionary)
			{
				this.currentKey = base.ValueOutput<object>("currentKey");
			}
			this.currentItem = base.ValueOutput<object>("currentItem");
			base.Requirement(this.collection, base.enter);
			base.Assignment(base.enter, this.currentIndex);
			base.Assignment(base.enter, this.currentItem);
			if (this.dictionary)
			{
				base.Assignment(base.enter, this.currentKey);
			}
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00006B8C File Offset: 0x00004D8C
		private int Start(Flow flow, out IEnumerator enumerator, out IDictionaryEnumerator dictionaryEnumerator, out int currentIndex)
		{
			if (this.dictionary)
			{
				dictionaryEnumerator = flow.GetValue<IDictionary>(this.collection).GetEnumerator();
				enumerator = dictionaryEnumerator;
			}
			else
			{
				enumerator = flow.GetValue<IEnumerable>(this.collection).GetEnumerator();
				dictionaryEnumerator = null;
			}
			currentIndex = -1;
			return flow.EnterLoop();
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00006BDC File Offset: 0x00004DDC
		private bool MoveNext(Flow flow, IEnumerator enumerator, IDictionaryEnumerator dictionaryEnumerator, ref int currentIndex)
		{
			bool flag = enumerator.MoveNext();
			if (flag)
			{
				if (this.dictionary)
				{
					flow.SetValue(this.currentKey, dictionaryEnumerator.Key);
					flow.SetValue(this.currentItem, dictionaryEnumerator.Value);
				}
				else
				{
					flow.SetValue(this.currentItem, enumerator.Current);
				}
				currentIndex++;
				flow.SetValue(this.currentIndex, currentIndex);
			}
			return flag;
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00006C50 File Offset: 0x00004E50
		protected override ControlOutput Loop(Flow flow)
		{
			IEnumerator enumerator;
			IDictionaryEnumerator dictionaryEnumerator;
			int num2;
			int num = this.Start(flow, out enumerator, out dictionaryEnumerator, out num2);
			GraphStack graphStack = flow.PreserveStack();
			try
			{
				while (flow.LoopIsNotBroken(num) && this.MoveNext(flow, enumerator, dictionaryEnumerator, ref num2))
				{
					flow.Invoke(base.body);
					flow.RestoreStack(graphStack);
				}
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
			flow.DisposePreservedStack(graphStack);
			flow.ExitLoop(num);
			return base.exit;
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00006CD8 File Offset: 0x00004ED8
		protected override IEnumerator LoopCoroutine(Flow flow)
		{
			IEnumerator enumerator;
			IDictionaryEnumerator dictionaryEnumerator;
			int currentIndex;
			int loop = this.Start(flow, out enumerator, out dictionaryEnumerator, out currentIndex);
			GraphStack stack = flow.PreserveStack();
			try
			{
				while (flow.LoopIsNotBroken(loop) && this.MoveNext(flow, enumerator, dictionaryEnumerator, ref currentIndex))
				{
					yield return base.body;
					flow.RestoreStack(stack);
				}
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
			flow.DisposePreservedStack(stack);
			flow.ExitLoop(loop);
			yield return base.exit;
			yield break;
			yield break;
		}
	}
}
