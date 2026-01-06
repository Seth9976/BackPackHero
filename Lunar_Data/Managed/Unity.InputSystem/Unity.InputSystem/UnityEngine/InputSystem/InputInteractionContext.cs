using System;

namespace UnityEngine.InputSystem
{
	// Token: 0x0200002B RID: 43
	public struct InputInteractionContext
	{
		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060002FF RID: 767 RVA: 0x0000DEC0 File Offset: 0x0000C0C0
		public InputAction action
		{
			get
			{
				return this.m_State.GetActionOrNull(ref this.m_TriggerState);
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000300 RID: 768 RVA: 0x0000DED3 File Offset: 0x0000C0D3
		public InputControl control
		{
			get
			{
				return this.m_State.GetControl(ref this.m_TriggerState);
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000301 RID: 769 RVA: 0x0000DEE6 File Offset: 0x0000C0E6
		public InputActionPhase phase
		{
			get
			{
				return this.m_TriggerState.phase;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000302 RID: 770 RVA: 0x0000DEF3 File Offset: 0x0000C0F3
		public double time
		{
			get
			{
				return this.m_TriggerState.time;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000303 RID: 771 RVA: 0x0000DF00 File Offset: 0x0000C100
		public double startTime
		{
			get
			{
				return this.m_TriggerState.startTime;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000304 RID: 772 RVA: 0x0000DF0D File Offset: 0x0000C10D
		// (set) Token: 0x06000305 RID: 773 RVA: 0x0000DF1A File Offset: 0x0000C11A
		public bool timerHasExpired
		{
			get
			{
				return (this.m_Flags & InputInteractionContext.Flags.TimerHasExpired) > (InputInteractionContext.Flags)0;
			}
			internal set
			{
				if (value)
				{
					this.m_Flags |= InputInteractionContext.Flags.TimerHasExpired;
					return;
				}
				this.m_Flags &= ~InputInteractionContext.Flags.TimerHasExpired;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000306 RID: 774 RVA: 0x0000DF3D File Offset: 0x0000C13D
		public bool isWaiting
		{
			get
			{
				return this.phase == InputActionPhase.Waiting;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000307 RID: 775 RVA: 0x0000DF48 File Offset: 0x0000C148
		public bool isStarted
		{
			get
			{
				return this.phase == InputActionPhase.Started;
			}
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000DF53 File Offset: 0x0000C153
		public float ComputeMagnitude()
		{
			return this.m_TriggerState.magnitude;
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000DF60 File Offset: 0x0000C160
		public bool ControlIsActuated(float threshold = 0f)
		{
			return InputActionState.IsActuated(ref this.m_TriggerState, threshold);
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000DF6E File Offset: 0x0000C16E
		public void Started()
		{
			this.m_TriggerState.startTime = this.time;
			this.m_State.ChangePhaseOfInteraction(InputActionPhase.Started, ref this.m_TriggerState, InputActionPhase.Waiting, true);
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000DF95 File Offset: 0x0000C195
		public void Performed()
		{
			if (this.m_TriggerState.phase == InputActionPhase.Waiting)
			{
				this.m_TriggerState.startTime = this.time;
			}
			this.m_State.ChangePhaseOfInteraction(InputActionPhase.Performed, ref this.m_TriggerState, InputActionPhase.Waiting, true);
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000DFCA File Offset: 0x0000C1CA
		public void PerformedAndStayStarted()
		{
			if (this.m_TriggerState.phase == InputActionPhase.Waiting)
			{
				this.m_TriggerState.startTime = this.time;
			}
			this.m_State.ChangePhaseOfInteraction(InputActionPhase.Performed, ref this.m_TriggerState, InputActionPhase.Started, true);
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000DFFF File Offset: 0x0000C1FF
		public void PerformedAndStayPerformed()
		{
			if (this.m_TriggerState.phase == InputActionPhase.Waiting)
			{
				this.m_TriggerState.startTime = this.time;
			}
			this.m_State.ChangePhaseOfInteraction(InputActionPhase.Performed, ref this.m_TriggerState, InputActionPhase.Performed, true);
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000E034 File Offset: 0x0000C234
		public void Canceled()
		{
			if (this.m_TriggerState.phase != InputActionPhase.Canceled)
			{
				this.m_State.ChangePhaseOfInteraction(InputActionPhase.Canceled, ref this.m_TriggerState, InputActionPhase.Waiting, true);
			}
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000E058 File Offset: 0x0000C258
		public void Waiting()
		{
			if (this.m_TriggerState.phase != InputActionPhase.Waiting)
			{
				this.m_State.ChangePhaseOfInteraction(InputActionPhase.Waiting, ref this.m_TriggerState, InputActionPhase.Waiting, true);
			}
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000E07C File Offset: 0x0000C27C
		public void SetTimeout(float seconds)
		{
			this.m_State.StartTimeout(seconds, ref this.m_TriggerState);
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000E090 File Offset: 0x0000C290
		public void SetTotalTimeoutCompletionTime(float seconds)
		{
			if (seconds <= 0f)
			{
				throw new ArgumentException("Seconds must be a positive value", "seconds");
			}
			this.m_State.SetTotalTimeoutCompletionTime(seconds, ref this.m_TriggerState);
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000E0BC File Offset: 0x0000C2BC
		public TValue ReadValue<TValue>() where TValue : struct
		{
			return this.m_State.ReadValue<TValue>(this.m_TriggerState.bindingIndex, this.m_TriggerState.controlIndex, false);
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000313 RID: 787 RVA: 0x0000E0E0 File Offset: 0x0000C2E0
		internal int mapIndex
		{
			get
			{
				return this.m_TriggerState.mapIndex;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000314 RID: 788 RVA: 0x0000E0ED File Offset: 0x0000C2ED
		internal int controlIndex
		{
			get
			{
				return this.m_TriggerState.controlIndex;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000315 RID: 789 RVA: 0x0000E0FA File Offset: 0x0000C2FA
		internal int bindingIndex
		{
			get
			{
				return this.m_TriggerState.bindingIndex;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000316 RID: 790 RVA: 0x0000E107 File Offset: 0x0000C307
		internal int interactionIndex
		{
			get
			{
				return this.m_TriggerState.interactionIndex;
			}
		}

		// Token: 0x040000F6 RID: 246
		internal InputActionState m_State;

		// Token: 0x040000F7 RID: 247
		internal InputInteractionContext.Flags m_Flags;

		// Token: 0x040000F8 RID: 248
		internal InputActionState.TriggerState m_TriggerState;

		// Token: 0x0200017E RID: 382
		[Flags]
		internal enum Flags
		{
			// Token: 0x0400082B RID: 2091
			TimerHasExpired = 2
		}
	}
}
