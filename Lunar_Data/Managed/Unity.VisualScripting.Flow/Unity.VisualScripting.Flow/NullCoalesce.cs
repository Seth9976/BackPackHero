using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x0200012C RID: 300
	[UnitCategory("Nulls")]
	[TypeIcon(typeof(Null))]
	public sealed class NullCoalesce : Unit
	{
		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x060007C8 RID: 1992 RVA: 0x0000E76A File Offset: 0x0000C96A
		// (set) Token: 0x060007C9 RID: 1993 RVA: 0x0000E772 File Offset: 0x0000C972
		[DoNotSerialize]
		public ValueInput input { get; private set; }

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x060007CA RID: 1994 RVA: 0x0000E77B File Offset: 0x0000C97B
		// (set) Token: 0x060007CB RID: 1995 RVA: 0x0000E783 File Offset: 0x0000C983
		[DoNotSerialize]
		public ValueInput fallback { get; private set; }

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x060007CC RID: 1996 RVA: 0x0000E78C File Offset: 0x0000C98C
		// (set) Token: 0x060007CD RID: 1997 RVA: 0x0000E794 File Offset: 0x0000C994
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueOutput result { get; private set; }

		// Token: 0x060007CE RID: 1998 RVA: 0x0000E7A0 File Offset: 0x0000C9A0
		protected override void Definition()
		{
			this.input = base.ValueInput<object>("input").AllowsNull();
			this.fallback = base.ValueInput<object>("fallback");
			this.result = base.ValueOutput<object>("result", new Func<Flow, object>(this.Coalesce)).Predictable();
			base.Requirement(this.input, this.result);
			base.Requirement(this.fallback, this.result);
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x0000E81C File Offset: 0x0000CA1C
		public object Coalesce(Flow flow)
		{
			object value = flow.GetValue(this.input);
			bool flag;
			if (value is Object)
			{
				flag = (Object)value == null;
			}
			else
			{
				flag = value == null;
			}
			if (!flag)
			{
				return value;
			}
			return flow.GetValue(this.fallback);
		}
	}
}
