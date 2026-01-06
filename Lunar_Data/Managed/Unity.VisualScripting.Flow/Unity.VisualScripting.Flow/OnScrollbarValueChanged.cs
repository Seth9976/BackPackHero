using System;
using UnityEngine.UI;

namespace Unity.VisualScripting
{
	// Token: 0x02000071 RID: 113
	[UnitCategory("Events/GUI")]
	[TypeIcon(typeof(Scrollbar))]
	[UnitOrder(6)]
	public sealed class OnScrollbarValueChanged : GameObjectEventUnit<float>
	{
		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060003D3 RID: 979 RVA: 0x00009176 File Offset: 0x00007376
		public override Type MessageListenerType
		{
			get
			{
				return typeof(UnityOnScrollbarValueChangedMessageListener);
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x00009182 File Offset: 0x00007382
		protected override string hookName
		{
			get
			{
				return "OnScrollbarValueChanged";
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060003D5 RID: 981 RVA: 0x00009189 File Offset: 0x00007389
		// (set) Token: 0x060003D6 RID: 982 RVA: 0x00009191 File Offset: 0x00007391
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueOutput value { get; private set; }

		// Token: 0x060003D7 RID: 983 RVA: 0x0000919A File Offset: 0x0000739A
		protected override void Definition()
		{
			base.Definition();
			this.value = base.ValueOutput<float>("value");
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x000091B3 File Offset: 0x000073B3
		protected override void AssignArguments(Flow flow, float value)
		{
			flow.SetValue(this.value, value);
		}
	}
}
