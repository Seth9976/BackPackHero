using System;
using UnityEngine.InputSystem.Controls;

namespace UnityEngine.InputSystem.Interactions
{
	// Token: 0x0200011E RID: 286
	public class MultiTapInteraction : IInputInteraction<float>, IInputInteraction
	{
		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x06001018 RID: 4120 RVA: 0x0004CDE6 File Offset: 0x0004AFE6
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

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x06001019 RID: 4121 RVA: 0x0004CE0B File Offset: 0x0004B00B
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

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x0600101A RID: 4122 RVA: 0x0004CE30 File Offset: 0x0004B030
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

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x0600101B RID: 4123 RVA: 0x0004CE4B File Offset: 0x0004B04B
		private float releasePointOrDefault
		{
			get
			{
				return this.pressPointOrDefault * ButtonControl.s_GlobalDefaultButtonReleaseThreshold;
			}
		}

		// Token: 0x0600101C RID: 4124 RVA: 0x0004CE5C File Offset: 0x0004B05C
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

		// Token: 0x0600101D RID: 4125 RVA: 0x0004CFA8 File Offset: 0x0004B1A8
		public void Reset()
		{
			this.m_CurrentTapPhase = MultiTapInteraction.TapPhase.None;
			this.m_CurrentTapCount = 0;
			this.m_CurrentTapStartTime = 0.0;
			this.m_LastTapReleaseTime = 0.0;
		}

		// Token: 0x04000694 RID: 1684
		[Tooltip("The maximum time (in seconds) allowed to elapse between pressing and releasing a control for it to register as a tap.")]
		public float tapTime;

		// Token: 0x04000695 RID: 1685
		[Tooltip("The maximum delay (in seconds) allowed between each tap. If this time is exceeded, the multi-tap is canceled.")]
		public float tapDelay;

		// Token: 0x04000696 RID: 1686
		[Tooltip("How many taps need to be performed in succession. Two means double-tap, three means triple-tap, and so on.")]
		public int tapCount = 2;

		// Token: 0x04000697 RID: 1687
		public float pressPoint;

		// Token: 0x04000698 RID: 1688
		private MultiTapInteraction.TapPhase m_CurrentTapPhase;

		// Token: 0x04000699 RID: 1689
		private int m_CurrentTapCount;

		// Token: 0x0400069A RID: 1690
		private double m_CurrentTapStartTime;

		// Token: 0x0400069B RID: 1691
		private double m_LastTapReleaseTime;

		// Token: 0x02000232 RID: 562
		private enum TapPhase
		{
			// Token: 0x04000BE8 RID: 3048
			None,
			// Token: 0x04000BE9 RID: 3049
			WaitingForNextRelease,
			// Token: 0x04000BEA RID: 3050
			WaitingForNextPress
		}
	}
}
