using System;
using System.ComponentModel;
using UnityEngine.InputSystem.Controls;

namespace UnityEngine.InputSystem.Interactions
{
	// Token: 0x02000122 RID: 290
	[DisplayName("Tap")]
	public class TapInteraction : IInputInteraction
	{
		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06001024 RID: 4132 RVA: 0x0004D23C File Offset: 0x0004B43C
		private float durationOrDefault
		{
			get
			{
				if ((double)this.duration <= 0.0)
				{
					return InputSystem.settings.defaultTapTime;
				}
				return this.duration;
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06001025 RID: 4133 RVA: 0x0004D261 File Offset: 0x0004B461
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

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06001026 RID: 4134 RVA: 0x0004D27C File Offset: 0x0004B47C
		private float releasePointOrDefault
		{
			get
			{
				return this.pressPointOrDefault * ButtonControl.s_GlobalDefaultButtonReleaseThreshold;
			}
		}

		// Token: 0x06001027 RID: 4135 RVA: 0x0004D28C File Offset: 0x0004B48C
		public void Process(ref InputInteractionContext context)
		{
			if (context.timerHasExpired)
			{
				context.Canceled();
				return;
			}
			if (context.isWaiting && context.ControlIsActuated(this.pressPointOrDefault))
			{
				this.m_TapStartTime = context.time;
				context.Started();
				context.SetTimeout(this.durationOrDefault + 1E-05f);
				return;
			}
			if (context.isStarted && !context.ControlIsActuated(this.releasePointOrDefault))
			{
				if (context.time - this.m_TapStartTime <= (double)this.durationOrDefault)
				{
					context.Performed();
					return;
				}
				context.Canceled();
			}
		}

		// Token: 0x06001028 RID: 4136 RVA: 0x0004D31C File Offset: 0x0004B51C
		public void Reset()
		{
			this.m_TapStartTime = 0.0;
		}

		// Token: 0x040006A5 RID: 1701
		public float duration;

		// Token: 0x040006A6 RID: 1702
		public float pressPoint;

		// Token: 0x040006A7 RID: 1703
		private double m_TapStartTime;
	}
}
