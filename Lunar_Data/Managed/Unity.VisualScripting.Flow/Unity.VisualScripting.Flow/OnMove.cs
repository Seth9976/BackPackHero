using System;
using UnityEngine.EventSystems;

namespace Unity.VisualScripting
{
	// Token: 0x0200006A RID: 106
	[UnitCategory("Events/GUI")]
	[UnitOrder(21)]
	public sealed class OnMove : GameObjectEventUnit<AxisEventData>
	{
		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060003BA RID: 954 RVA: 0x00009080 File Offset: 0x00007280
		public override Type MessageListenerType
		{
			get
			{
				return typeof(UnityOnMoveMessageListener);
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060003BB RID: 955 RVA: 0x0000908C File Offset: 0x0000728C
		protected override string hookName
		{
			get
			{
				return "OnMove";
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060003BC RID: 956 RVA: 0x00009093 File Offset: 0x00007293
		// (set) Token: 0x060003BD RID: 957 RVA: 0x0000909B File Offset: 0x0000729B
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueOutput data { get; private set; }

		// Token: 0x060003BE RID: 958 RVA: 0x000090A4 File Offset: 0x000072A4
		protected override void Definition()
		{
			base.Definition();
			this.data = base.ValueOutput<AxisEventData>("data");
		}

		// Token: 0x060003BF RID: 959 RVA: 0x000090BD File Offset: 0x000072BD
		protected override void AssignArguments(Flow flow, AxisEventData data)
		{
			flow.SetValue(this.data, data);
		}
	}
}
