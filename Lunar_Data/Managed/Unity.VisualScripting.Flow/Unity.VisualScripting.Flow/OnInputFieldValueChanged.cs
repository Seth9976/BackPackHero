using System;
using UnityEngine.UI;

namespace Unity.VisualScripting
{
	// Token: 0x02000069 RID: 105
	[UnitCategory("Events/GUI")]
	[TypeIcon(typeof(InputField))]
	[UnitOrder(2)]
	public sealed class OnInputFieldValueChanged : GameObjectEventUnit<string>
	{
		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x0000902C File Offset: 0x0000722C
		public override Type MessageListenerType
		{
			get
			{
				return typeof(UnityOnInputFieldValueChangedMessageListener);
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x00009038 File Offset: 0x00007238
		protected override string hookName
		{
			get
			{
				return "OnInputFieldValueChanged";
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x0000903F File Offset: 0x0000723F
		// (set) Token: 0x060003B6 RID: 950 RVA: 0x00009047 File Offset: 0x00007247
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueOutput value { get; private set; }

		// Token: 0x060003B7 RID: 951 RVA: 0x00009050 File Offset: 0x00007250
		protected override void Definition()
		{
			base.Definition();
			this.value = base.ValueOutput<string>("value");
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x00009069 File Offset: 0x00007269
		protected override void AssignArguments(Flow flow, string value)
		{
			flow.SetValue(this.value, value);
		}
	}
}
