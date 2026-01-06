using System;
using UnityEngine.EventSystems;

namespace Unity.VisualScripting
{
	// Token: 0x0200005E RID: 94
	public abstract class GenericGuiEventUnit : GameObjectEventUnit<BaseEventData>
	{
		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000387 RID: 903 RVA: 0x00008E28 File Offset: 0x00007028
		// (set) Token: 0x06000388 RID: 904 RVA: 0x00008E30 File Offset: 0x00007030
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueOutput data { get; private set; }

		// Token: 0x06000389 RID: 905 RVA: 0x00008E39 File Offset: 0x00007039
		protected override void Definition()
		{
			base.Definition();
			this.data = base.ValueOutput<BaseEventData>("data");
		}

		// Token: 0x0600038A RID: 906 RVA: 0x00008E52 File Offset: 0x00007052
		protected override void AssignArguments(Flow flow, BaseEventData data)
		{
			flow.SetValue(this.data, data);
		}
	}
}
