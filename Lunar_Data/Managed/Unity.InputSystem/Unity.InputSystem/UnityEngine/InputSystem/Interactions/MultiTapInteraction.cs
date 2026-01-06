using System;
using UnityEngine.InputSystem.Controls;

namespace UnityEngine.InputSystem.Interactions
{
	// Token: 0x0200011E RID: 286
	public class MultiTapInteraction : IInputInteraction<float>, IInputInteraction
	{
		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06001013 RID: 4115 RVA: 0x0004CD9A File Offset: 0x0004AF9A
		private float tapTimeOrDefault
		{
			get
			{
				if ((double)this.tapTime <= 0.0)
				{
					return InputSystem.settings.defaultTapTime;
				}
				return this.tapTime;
			}
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06001014 RID: 4116 RVA: 0x0004CDBF File Offset: 0x0004AFBF
		internal float tapDelayOrDefault
		{
			get
			{
				if ((double)this.tapDelay <= 0.0)
				{
					return InputSystem.settings.multiTapDelayTime;
				}
				return this.tapDelay;
			}
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x06001015 RID: 4117 RVA: 0x0004CDE4 File Offset: 0x0004AFE4
		private float pressPointOrDefault
		{
			get
			{
				if (this.pressPoint <= 0f)
				{
					return ButtonControl.s_GlobalDefaultButtonPressPoint;
				}
				return this.pressPoint;
			}
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x06001016 RID: 4118 RVA: 0x0004CDFF File Offset: 0x0004AFFF
		private float releasePointOrDefault
		{
			get
			{
				return this.pressPointOrDefault * ButtonControl.s_GlobalDefaultButtonReleaseThreshold;
			}
		}

		// Token: 0x06001017 RID: 4119 RVA: 0x0004CE10 File Offset: 0x0004B010
		public void Process(ref InputInteractionContext context)
		{
			if (context.timerHasExpired)
			{
				context.Canceled();
				return;
			}
			switch (this.m_CurrentTapPhase)
			{
			case MultiTapInteraction.TapPhase.None:
				if (context.ControlIsActuated(this.pressPointOrDefault))
				{
					this.m_CurrentTapPhase = MultiTapInteraction.TapPhase.WaitingForNextRelease;
					this.m_CurrentTapStartTime = context.time;
					context.Started();
					float tapTimeOrDefault = this.tapTimeOrDefault;
					float tapDelayOrDefault = this.tapDelayOrDefault;
					context.SetTimeout(tapTimeOrDefault);
					context.SetTotalTimeoutCompletionTime(tapTimeOrDefault * (float)this.tapCount + (float)(this.tapCount - 1) * tapDelayOrDefault);
					return;
				}
				break;
			case MultiTapInteraction.TapPhase.WaitingForNextRelease:
				if (!context.ControlIsActuated(this.releasePointOrDefault))
				{
					if (context.time - this.m_CurrentTapStartTime > (double)this.tapTimeOrDefault)
					{
						context.Canceled();
						return;
					}
					this.m_CurrentTapCount++;
					if (this.m_CurrentTapCount >= this.tapCount)
					{
						context.Performed();
						return;
					}
					this.m_CurrentTapPhase = MultiTapInteraction.TapPhase.WaitingForNextPress;
					this.m_LastTapReleaseTime = context.time;
					context.SetTimeout(this.tapDelayOrDefault);
					return;
				}
				break;
			case MultiTapInteraction.TapPhase.WaitingForNextPress:
				if (context.ControlIsActuated(this.pressPointOrDefault))
				{
					if (context.time - this.m_LastTapReleaseTime <= (double)this.tapDelayOrDefault)
					{
						this.m_CurrentTapPhase = MultiTapInteraction.TapPhase.WaitingForNextRelease;
						this.m_CurrentTapStartTime = context.time;
						context.SetTimeout(this.tapTimeOrDefault);
						return;
					}
					context.Canceled();
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x06001018 RID: 4120 RVA: 0x0004CF5C File Offset: 0x0004B15C
		public void Reset()
		{
			this.m_CurrentTapPhase = MultiTapInteraction.TapPhase.None;
			this.m_CurrentTapCount = 0;
			this.m_CurrentTapStartTime = 0.0;
			this.m_LastTapReleaseTime = 0.0;
		}

		// Token: 0x04000693 RID: 1683
		[Tooltip("The maximum time (in seconds) allowed to elapse between pressing and releasing a control for it to register as a tap.")]
		public float tapTime;

		// Token: 0x04000694 RID: 1684
		[Tooltip("The maximum delay (in seconds) allowed between each tap. If this time is exceeded, the multi-tap is canceled.")]
		public float tapDelay;

		// Token: 0x04000695 RID: 1685
		[Tooltip("How many taps need to be performed in succession. Two means double-tap, three means triple-tap, and so on.")]
		public int tapCount = 2;

		// Token: 0x04000696 RID: 1686
		public float pressPoint;

		// Token: 0x04000697 RID: 1687
		private MultiTapInteraction.TapPhase m_CurrentTapPhase;

		// Token: 0x04000698 RID: 1688
		private int m_CurrentTapCount;

		// Token: 0x04000699 RID: 1689
		private double m_CurrentTapStartTime;

		// Token: 0x0400069A RID: 1690
		private double m_LastTapReleaseTime;

		// Token: 0x02000232 RID: 562
		private enum TapPhase
		{
			// Token: 0x04000BE7 RID: 3047
			None,
			// Token: 0x04000BE8 RID: 3048
			WaitingForNextRelease,
			// Token: 0x04000BE9 RID: 3049
			WaitingForNextPress
		}
	}
}
