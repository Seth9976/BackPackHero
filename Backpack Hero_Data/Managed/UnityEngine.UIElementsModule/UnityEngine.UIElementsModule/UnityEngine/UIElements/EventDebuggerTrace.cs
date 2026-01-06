using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200023A RID: 570
	internal class EventDebuggerTrace
	{
		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06001112 RID: 4370 RVA: 0x00042176 File Offset: 0x00040376
		public EventDebuggerEventRecord eventBase { get; }

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06001113 RID: 4371 RVA: 0x0004217E File Offset: 0x0004037E
		public IEventHandler focusedElement { get; }

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06001114 RID: 4372 RVA: 0x00042186 File Offset: 0x00040386
		public IEventHandler mouseCapture { get; }

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06001115 RID: 4373 RVA: 0x0004218E File Offset: 0x0004038E
		// (set) Token: 0x06001116 RID: 4374 RVA: 0x00042196 File Offset: 0x00040396
		public long duration { get; set; }

		// Token: 0x06001117 RID: 4375 RVA: 0x000421A0 File Offset: 0x000403A0
		public EventDebuggerTrace(IPanel panel, EventBase evt, long duration, IEventHandler mouseCapture)
		{
			this.eventBase = new EventDebuggerEventRecord(evt);
			IEventHandler eventHandler;
			if (panel == null)
			{
				eventHandler = null;
			}
			else
			{
				FocusController focusController = panel.focusController;
				eventHandler = ((focusController != null) ? focusController.focusedElement : null);
			}
			this.focusedElement = eventHandler;
			this.mouseCapture = mouseCapture;
			this.duration = duration;
		}
	}
}
