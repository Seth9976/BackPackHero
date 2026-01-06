using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000062 RID: 98
	[UnitCategory("Events/GUI")]
	[UnitOrder(23)]
	public sealed class OnDeselect : GenericGuiEventUnit
	{
		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000395 RID: 917 RVA: 0x00008EBA File Offset: 0x000070BA
		public override Type MessageListenerType
		{
			get
			{
				return typeof(UnityOnDeselectMessageListener);
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000396 RID: 918 RVA: 0x00008EC6 File Offset: 0x000070C6
		protected override string hookName
		{
			get
			{
				return "OnDeselect";
			}
		}
	}
}
