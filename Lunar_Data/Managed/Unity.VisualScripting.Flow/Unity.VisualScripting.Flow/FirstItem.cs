using System;
using System.Collections;
using System.Linq;

namespace Unity.VisualScripting
{
	// Token: 0x02000028 RID: 40
	[UnitCategory("Collections")]
	public sealed class FirstItem : Unit
	{
		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600018B RID: 395 RVA: 0x000058C4 File Offset: 0x00003AC4
		// (set) Token: 0x0600018C RID: 396 RVA: 0x000058CC File Offset: 0x00003ACC
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueInput collection { get; private set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600018D RID: 397 RVA: 0x000058D5 File Offset: 0x00003AD5
		// (set) Token: 0x0600018E RID: 398 RVA: 0x000058DD File Offset: 0x00003ADD
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueOutput firstItem { get; private set; }

		// Token: 0x0600018F RID: 399 RVA: 0x000058E8 File Offset: 0x00003AE8
		protected override void Definition()
		{
			this.collection = base.ValueInput<IEnumerable>("collection");
			this.firstItem = base.ValueOutput<object>("firstItem", new Func<Flow, object>(this.First));
			base.Requirement(this.collection, this.firstItem);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00005938 File Offset: 0x00003B38
		public object First(Flow flow)
		{
			IEnumerable value = flow.GetValue<IEnumerable>(this.collection);
			if (value is IList)
			{
				return ((IList)value)[0];
			}
			return value.Cast<object>().First<object>();
		}
	}
}
