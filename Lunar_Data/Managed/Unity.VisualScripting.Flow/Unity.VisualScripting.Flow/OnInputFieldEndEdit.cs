using System;
using UnityEngine.UI;

namespace Unity.VisualScripting
{
	// Token: 0x02000068 RID: 104
	[UnitCategory("Events/GUI")]
	[TypeIcon(typeof(InputField))]
	[UnitOrder(3)]
	public sealed class OnInputFieldEndEdit : GameObjectEventUnit<string>
	{
		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060003AC RID: 940 RVA: 0x00008FD8 File Offset: 0x000071D8
		public override Type MessageListenerType
		{
			get
			{
				return typeof(UnityOnInputFieldEndEditMessageListener);
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060003AD RID: 941 RVA: 0x00008FE4 File Offset: 0x000071E4
		protected override string hookName
		{
			get
			{
				return "OnInputFieldEndEdit";
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060003AE RID: 942 RVA: 0x00008FEB File Offset: 0x000071EB
		// (set) Token: 0x060003AF RID: 943 RVA: 0x00008FF3 File Offset: 0x000071F3
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueOutput value { get; private set; }

		// Token: 0x060003B0 RID: 944 RVA: 0x00008FFC File Offset: 0x000071FC
		protected override void Definition()
		{
			base.Definition();
			this.value = base.ValueOutput<string>("value");
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x00009015 File Offset: 0x00007215
		protected override void AssignArguments(Flow flow, string value)
		{
			flow.SetValue(this.value, value);
		}
	}
}
