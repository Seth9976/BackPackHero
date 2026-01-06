using System;

namespace Unity.VisualScripting.InputSystem
{
	// Token: 0x02000185 RID: 389
	public class OnInputSystemEventButton : OnInputSystemEvent
	{
		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06000A6D RID: 2669 RVA: 0x00012FB1 File Offset: 0x000111B1
		protected override OutputType OutputType
		{
			get
			{
				return OutputType.Button;
			}
		}
	}
}
