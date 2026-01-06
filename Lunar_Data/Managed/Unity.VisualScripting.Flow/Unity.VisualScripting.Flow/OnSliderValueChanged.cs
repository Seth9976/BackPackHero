using System;
using UnityEngine.UI;

namespace Unity.VisualScripting
{
	// Token: 0x02000074 RID: 116
	[UnitCategory("Events/GUI")]
	[TypeIcon(typeof(Slider))]
	[UnitOrder(8)]
	public sealed class OnSliderValueChanged : GameObjectEventUnit<float>
	{
		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060003E4 RID: 996 RVA: 0x00009243 File Offset: 0x00007443
		public override Type MessageListenerType
		{
			get
			{
				return typeof(UnityOnSliderValueChangedMessageListener);
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x0000924F File Offset: 0x0000744F
		protected override string hookName
		{
			get
			{
				return "OnSliderValueChanged";
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060003E6 RID: 998 RVA: 0x00009256 File Offset: 0x00007456
		// (set) Token: 0x060003E7 RID: 999 RVA: 0x0000925E File Offset: 0x0000745E
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueOutput value { get; private set; }

		// Token: 0x060003E8 RID: 1000 RVA: 0x00009267 File Offset: 0x00007467
		protected override void Definition()
		{
			base.Definition();
			this.value = base.ValueOutput<float>("value");
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x00009280 File Offset: 0x00007480
		protected override void AssignArguments(Flow flow, float value)
		{
			flow.SetValue(this.value, value);
		}
	}
}
