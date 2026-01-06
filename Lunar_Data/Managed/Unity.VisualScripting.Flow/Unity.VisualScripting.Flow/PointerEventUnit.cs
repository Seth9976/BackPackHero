using System;
using UnityEngine.EventSystems;

namespace Unity.VisualScripting
{
	// Token: 0x02000077 RID: 119
	public abstract class PointerEventUnit : GameObjectEventUnit<PointerEventData>
	{
		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060003F5 RID: 1013 RVA: 0x00009310 File Offset: 0x00007510
		// (set) Token: 0x060003F6 RID: 1014 RVA: 0x00009318 File Offset: 0x00007518
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueOutput data { get; private set; }

		// Token: 0x060003F7 RID: 1015 RVA: 0x00009321 File Offset: 0x00007521
		protected override void Definition()
		{
			base.Definition();
			this.data = base.ValueOutput<PointerEventData>("data");
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0000933A File Offset: 0x0000753A
		protected override void AssignArguments(Flow flow, PointerEventData data)
		{
			flow.SetValue(this.data, data);
		}
	}
}
