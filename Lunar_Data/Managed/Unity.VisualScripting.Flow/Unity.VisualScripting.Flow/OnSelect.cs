using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000073 RID: 115
	[UnitCategory("Events/GUI")]
	[UnitOrder(22)]
	public sealed class OnSelect : GenericGuiEventUnit
	{
		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x00009228 File Offset: 0x00007428
		public override Type MessageListenerType
		{
			get
			{
				return typeof(UnityOnSelectMessageListener);
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060003E2 RID: 994 RVA: 0x00009234 File Offset: 0x00007434
		protected override string hookName
		{
			get
			{
				return "OnSelect";
			}
		}
	}
}
