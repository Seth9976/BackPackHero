using System;
using UnityEngine.UI;

namespace Unity.VisualScripting
{
	// Token: 0x02000060 RID: 96
	[UnitCategory("Events/GUI")]
	[TypeIcon(typeof(Button))]
	[UnitOrder(1)]
	public sealed class OnButtonClick : GameObjectEventUnit<EmptyEventArgs>
	{
		// Token: 0x1700013B RID: 315
		// (get) Token: 0x0600038F RID: 911 RVA: 0x00008E84 File Offset: 0x00007084
		protected override string hookName
		{
			get
			{
				return "OnButtonClick";
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000390 RID: 912 RVA: 0x00008E8B File Offset: 0x0000708B
		public override Type MessageListenerType
		{
			get
			{
				return typeof(UnityOnButtonClickMessageListener);
			}
		}
	}
}
