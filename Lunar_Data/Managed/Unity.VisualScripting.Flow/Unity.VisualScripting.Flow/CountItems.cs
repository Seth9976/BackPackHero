using System;
using System.Collections;
using System.Linq;

namespace Unity.VisualScripting
{
	// Token: 0x0200001F RID: 31
	[UnitCategory("Collections")]
	public sealed class CountItems : Unit
	{
		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00004FC9 File Offset: 0x000031C9
		// (set) Token: 0x06000135 RID: 309 RVA: 0x00004FD1 File Offset: 0x000031D1
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueInput collection { get; private set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00004FDA File Offset: 0x000031DA
		// (set) Token: 0x06000137 RID: 311 RVA: 0x00004FE2 File Offset: 0x000031E2
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueOutput count { get; private set; }

		// Token: 0x06000138 RID: 312 RVA: 0x00004FEC File Offset: 0x000031EC
		protected override void Definition()
		{
			this.collection = base.ValueInput<IEnumerable>("collection");
			this.count = base.ValueOutput<int>("count", new Func<Flow, int>(this.Count));
			base.Requirement(this.collection, this.count);
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0000503C File Offset: 0x0000323C
		public int Count(Flow flow)
		{
			IEnumerable value = flow.GetValue<IEnumerable>(this.collection);
			if (value is ICollection)
			{
				return ((ICollection)value).Count;
			}
			return value.Cast<object>().Count<object>();
		}
	}
}
