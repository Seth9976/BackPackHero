using System;
using System.ComponentModel;
using UnityEngine.InputSystem.Controls;

namespace UnityEngine.InputSystem.Interactions
{
	// Token: 0x02000122 RID: 290
	[DisplayName("Tap")]
	public class TapInteraction : IInputInteraction
	{
		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06001029 RID: 4137 RVA: 0x0004D288 File Offset: 0x0004B488
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

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x0600102A RID: 4138 RVA: 0x0004D2AD File Offset: 0x0004B4AD
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

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x0600102B RID: 4139 RVA: 0x0004D2C8 File Offset: 0x0004B4C8
		private float releasePointOrDefault
		{
			get
			{
				return this.pressPointOrDefault * ButtonControl.s_GlobalDefaultButtonReleaseThreshold;
			}
		}

		// Token: 0x0600102C RID: 4140 RVA: 0x0004D2D8 File Offset: 0x0004B4D8
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

		// Token: 0x0600102D RID: 4141 RVA: 0x0004D368 File Offset: 0x0004B568
		public void Reset()
		{
			this.m_TapStartTime = 0.0;
		}

		// Token: 0x040006A6 RID: 1702
		public float duration;

		// Token: 0x040006A7 RID: 1703
		public float pressPoint;

		// Token: 0x040006A8 RID: 1704
		private double m_TapStartTime;
	}
}
