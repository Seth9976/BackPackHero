using System;
using System.Collections;
using System.Linq;

namespace Unity.VisualScripting
{
	// Token: 0x02000029 RID: 41
	[UnitCategory("Collections")]
	public sealed class LastItem : Unit
	{
		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000192 RID: 402 RVA: 0x0000597A File Offset: 0x00003B7A
		// (set) Token: 0x06000193 RID: 403 RVA: 0x00005982 File Offset: 0x00003B82
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueInput collection { get; private set; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000194 RID: 404 RVA: 0x0000598B File Offset: 0x00003B8B
		// (set) Token: 0x06000195 RID: 405 RVA: 0x00005993 File Offset: 0x00003B93
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueOutput lastItem { get; private set; }

		// Token: 0x06000196 RID: 406 RVA: 0x0000599C File Offset: 0x00003B9C
		protected override void Definition()
		{
			this.collection = base.ValueInput<IEnumerable>("collection");
			this.lastItem = base.ValueOutput<object>("lastItem", new Func<Flow, object>(this.First));
			base.Requirement(this.collection, this.lastItem);
		}

		// Token: 0x06000197 RID: 407 RVA: 0x000059EC File Offset: 0x00003BEC
		public object First(Flow flow)
		{
			IEnumerable value = flow.GetValue<IEnumerable>(this.collection);
			if (value is IList)
			{
				IList list = (IList)value;
				return list[list.Count - 1];
			}
			return value.Cast<object>().Last<object>();
		}
	}
}
