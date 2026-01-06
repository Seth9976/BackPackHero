using System;
using UnityEngine.UI;

namespace Unity.VisualScripting
{
	// Token: 0x02000076 RID: 118
	[UnitCategory("Events/GUI")]
	[TypeIcon(typeof(Toggle))]
	[UnitOrder(5)]
	public sealed class OnToggleValueChanged : GameObjectEventUnit<bool>
	{
		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x000092B7 File Offset: 0x000074B7
		public override Type MessageListenerType
		{
			get
			{
				return typeof(UnityOnToggleValueChangedMessageListener);
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x000092C3 File Offset: 0x000074C3
		protected override string hookName
		{
			get
			{
				return "OnToggleValueChanged";
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x000092CA File Offset: 0x000074CA
		// (set) Token: 0x060003F1 RID: 1009 RVA: 0x000092D2 File Offset: 0x000074D2
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueOutput value { get; private set; }

		// Token: 0x060003F2 RID: 1010 RVA: 0x000092DB File Offset: 0x000074DB
		protected override void Definition()
		{
			base.Definition();
			this.value = base.ValueOutput<bool>("value");
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x000092F4 File Offset: 0x000074F4
		protected override void AssignArguments(Flow flow, bool value)
		{
			flow.SetValue(this.value, value);
		}
	}
}
