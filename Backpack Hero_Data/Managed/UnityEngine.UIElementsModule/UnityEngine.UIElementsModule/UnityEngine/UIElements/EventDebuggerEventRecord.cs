using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000239 RID: 569
	[Serializable]
	internal class EventDebuggerEventRecord
	{
		// Token: 0x170003AE RID: 942
		// (get) Token: 0x060010DF RID: 4319 RVA: 0x00041D56 File Offset: 0x0003FF56
		// (set) Token: 0x060010E0 RID: 4320 RVA: 0x00041D5E File Offset: 0x0003FF5E
		public string eventBaseName { get; private set; }

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x060010E1 RID: 4321 RVA: 0x00041D67 File Offset: 0x0003FF67
		// (set) Token: 0x060010E2 RID: 4322 RVA: 0x00041D6F File Offset: 0x0003FF6F
		public long eventTypeId { get; private set; }

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x060010E3 RID: 4323 RVA: 0x00041D78 File Offset: 0x0003FF78
		// (set) Token: 0x060010E4 RID: 4324 RVA: 0x00041D80 File Offset: 0x0003FF80
		public ulong eventId { get; private set; }

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x060010E5 RID: 4325 RVA: 0x00041D89 File Offset: 0x0003FF89
		// (set) Token: 0x060010E6 RID: 4326 RVA: 0x00041D91 File Offset: 0x0003FF91
		private ulong triggerEventId { get; set; }

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x060010E7 RID: 4327 RVA: 0x00041D9A File Offset: 0x0003FF9A
		// (set) Token: 0x060010E8 RID: 4328 RVA: 0x00041DA2 File Offset: 0x0003FFA2
		internal long timestamp { get; private set; }

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x060010E9 RID: 4329 RVA: 0x00041DAB File Offset: 0x0003FFAB
		// (set) Token: 0x060010EA RID: 4330 RVA: 0x00041DB3 File Offset: 0x0003FFB3
		public IEventHandler target { get; set; }

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x060010EB RID: 4331 RVA: 0x00041DBC File Offset: 0x0003FFBC
		// (set) Token: 0x060010EC RID: 4332 RVA: 0x00041DC4 File Offset: 0x0003FFC4
		private List<IEventHandler> skipElements { get; set; }

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x060010ED RID: 4333 RVA: 0x00041DCD File Offset: 0x0003FFCD
		// (set) Token: 0x060010EE RID: 4334 RVA: 0x00041DD5 File Offset: 0x0003FFD5
		public bool hasUnderlyingPhysicalEvent { get; private set; }

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x060010EF RID: 4335 RVA: 0x00041DDE File Offset: 0x0003FFDE
		// (set) Token: 0x060010F0 RID: 4336 RVA: 0x00041DE6 File Offset: 0x0003FFE6
		private bool isPropagationStopped { get; set; }

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x060010F1 RID: 4337 RVA: 0x00041DEF File Offset: 0x0003FFEF
		// (set) Token: 0x060010F2 RID: 4338 RVA: 0x00041DF7 File Offset: 0x0003FFF7
		private bool isImmediatePropagationStopped { get; set; }

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x060010F3 RID: 4339 RVA: 0x00041E00 File Offset: 0x00040000
		// (set) Token: 0x060010F4 RID: 4340 RVA: 0x00041E08 File Offset: 0x00040008
		private bool isDefaultPrevented { get; set; }

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x060010F5 RID: 4341 RVA: 0x00041E11 File Offset: 0x00040011
		// (set) Token: 0x060010F6 RID: 4342 RVA: 0x00041E19 File Offset: 0x00040019
		public PropagationPhase propagationPhase { get; private set; }

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x060010F7 RID: 4343 RVA: 0x00041E22 File Offset: 0x00040022
		// (set) Token: 0x060010F8 RID: 4344 RVA: 0x00041E2A File Offset: 0x0004002A
		private IEventHandler currentTarget { get; set; }

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x060010F9 RID: 4345 RVA: 0x00041E33 File Offset: 0x00040033
		// (set) Token: 0x060010FA RID: 4346 RVA: 0x00041E3B File Offset: 0x0004003B
		private bool dispatch { get; set; }

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x060010FB RID: 4347 RVA: 0x00041E44 File Offset: 0x00040044
		// (set) Token: 0x060010FC RID: 4348 RVA: 0x00041E4C File Offset: 0x0004004C
		private Vector2 originalMousePosition { get; set; }

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x060010FD RID: 4349 RVA: 0x00041E55 File Offset: 0x00040055
		// (set) Token: 0x060010FE RID: 4350 RVA: 0x00041E5D File Offset: 0x0004005D
		public EventModifiers modifiers { get; private set; }

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x060010FF RID: 4351 RVA: 0x00041E66 File Offset: 0x00040066
		// (set) Token: 0x06001100 RID: 4352 RVA: 0x00041E6E File Offset: 0x0004006E
		public Vector2 mousePosition { get; private set; }

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06001101 RID: 4353 RVA: 0x00041E77 File Offset: 0x00040077
		// (set) Token: 0x06001102 RID: 4354 RVA: 0x00041E7F File Offset: 0x0004007F
		public int clickCount { get; private set; }

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06001103 RID: 4355 RVA: 0x00041E88 File Offset: 0x00040088
		// (set) Token: 0x06001104 RID: 4356 RVA: 0x00041E90 File Offset: 0x00040090
		public int button { get; private set; }

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06001105 RID: 4357 RVA: 0x00041E99 File Offset: 0x00040099
		// (set) Token: 0x06001106 RID: 4358 RVA: 0x00041EA1 File Offset: 0x000400A1
		public int pressedButtons { get; private set; }

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06001107 RID: 4359 RVA: 0x00041EAA File Offset: 0x000400AA
		// (set) Token: 0x06001108 RID: 4360 RVA: 0x00041EB2 File Offset: 0x000400B2
		public Vector3 delta { get; private set; }

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06001109 RID: 4361 RVA: 0x00041EBB File Offset: 0x000400BB
		// (set) Token: 0x0600110A RID: 4362 RVA: 0x00041EC3 File Offset: 0x000400C3
		public char character { get; private set; }

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x0600110B RID: 4363 RVA: 0x00041ECC File Offset: 0x000400CC
		// (set) Token: 0x0600110C RID: 4364 RVA: 0x00041ED4 File Offset: 0x000400D4
		public KeyCode keyCode { get; private set; }

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x0600110D RID: 4365 RVA: 0x00041EDD File Offset: 0x000400DD
		// (set) Token: 0x0600110E RID: 4366 RVA: 0x00041EE5 File Offset: 0x000400E5
		public string commandName { get; private set; }

		// Token: 0x0600110F RID: 4367 RVA: 0x00041EF0 File Offset: 0x000400F0
		private void Init(EventBase evt)
		{
			Type type = evt.GetType();
			this.eventBaseName = EventDebugger.GetTypeDisplayName(type);
			this.eventTypeId = evt.eventTypeId;
			this.eventId = evt.eventId;
			this.triggerEventId = evt.triggerEventId;
			this.timestamp = evt.timestamp;
			this.target = evt.target;
			this.skipElements = evt.skipElements;
			this.isPropagationStopped = evt.isPropagationStopped;
			this.isImmediatePropagationStopped = evt.isImmediatePropagationStopped;
			this.isDefaultPrevented = evt.isDefaultPrevented;
			IMouseEvent mouseEvent = evt as IMouseEvent;
			IMouseEventInternal mouseEventInternal = evt as IMouseEventInternal;
			this.hasUnderlyingPhysicalEvent = mouseEvent != null && mouseEventInternal != null && mouseEventInternal.triggeredByOS;
			this.propagationPhase = evt.propagationPhase;
			this.originalMousePosition = evt.originalMousePosition;
			this.currentTarget = evt.currentTarget;
			this.dispatch = evt.dispatch;
			bool flag = mouseEvent != null;
			if (flag)
			{
				this.modifiers = mouseEvent.modifiers;
				this.mousePosition = mouseEvent.mousePosition;
				this.button = mouseEvent.button;
				this.pressedButtons = mouseEvent.pressedButtons;
				this.clickCount = mouseEvent.clickCount;
				WheelEvent wheelEvent = mouseEvent as WheelEvent;
				bool flag2 = wheelEvent != null;
				if (flag2)
				{
					this.delta = wheelEvent.delta;
				}
			}
			IPointerEvent pointerEvent = evt as IPointerEvent;
			bool flag3 = pointerEvent != null;
			if (flag3)
			{
				IPointerEventInternal pointerEventInternal = evt as IPointerEventInternal;
				this.hasUnderlyingPhysicalEvent = pointerEvent != null && pointerEventInternal != null && pointerEventInternal.triggeredByOS;
				this.modifiers = pointerEvent.modifiers;
				this.mousePosition = pointerEvent.position;
				this.button = pointerEvent.button;
				this.pressedButtons = pointerEvent.pressedButtons;
				this.clickCount = pointerEvent.clickCount;
			}
			IKeyboardEvent keyboardEvent = evt as IKeyboardEvent;
			bool flag4 = keyboardEvent != null;
			if (flag4)
			{
				this.modifiers = keyboardEvent.modifiers;
				this.character = keyboardEvent.character;
				this.keyCode = keyboardEvent.keyCode;
			}
			ICommandEvent commandEvent = evt as ICommandEvent;
			bool flag5 = commandEvent != null;
			if (flag5)
			{
				this.commandName = commandEvent.commandName;
			}
		}

		// Token: 0x06001110 RID: 4368 RVA: 0x0004212A File Offset: 0x0004032A
		public EventDebuggerEventRecord(EventBase evt)
		{
			this.Init(evt);
		}

		// Token: 0x06001111 RID: 4369 RVA: 0x0004213C File Offset: 0x0004033C
		public string TimestampString()
		{
			long num = (long)((float)this.timestamp / 1000f * 10000000f);
			return new DateTime(num).ToString("HH:mm:ss.ffffff");
		}
	}
}
