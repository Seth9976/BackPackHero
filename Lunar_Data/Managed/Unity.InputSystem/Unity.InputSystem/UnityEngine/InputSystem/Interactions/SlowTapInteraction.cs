using System;
using System.ComponentModel;
using UnityEngine.InputSystem.Controls;

namespace UnityEngine.InputSystem.Interactions
{
	// Token: 0x02000121 RID: 289
	[DisplayName("Long Tap")]
	public class SlowTapInteraction : IInputInteraction
	{
		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x0600101F RID: 4127 RVA: 0x0004D179 File Offset: 0x0004B379
		private float durationOrDefault
		{
			get
			{
				if (this.duration <= 0f)
				{
					return InputSystem.settings.defaultSlowTapTime;
				}
				return this.duration;
			}
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06001020 RID: 4128 RVA: 0x0004D199 File Offset: 0x0004B399
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

		// Token: 0x06001021 RID: 4129 RVA: 0x0004D1B4 File Offset: 0x0004B3B4
		public void Process(ref InputInteractionContext context)
		{
			if (context.isWaiting && context.ControlIsActuated(this.pressPointOrDefault))
			{
				this.m_SlowTapStartTime = context.time;
				context.Started();
				return;
			}
			if (context.isStarted && !context.ControlIsActuated(this.pressPointOrDefault))
			{
				if (context.time - this.m_SlowTapStartTime >= (double)this.durationOrDefault)
				{
					context.Performed();
					return;
				}
				context.Canceled();
			}
		}

		// Token: 0x06001022 RID: 4130 RVA: 0x0004D223 File Offset: 0x0004B423
		public void Reset()
		{
			this.m_SlowTapStartTime = 0.0;
		}

		// Token: 0x040006A2 RID: 1698
		public float duration;

		// Token: 0x040006A3 RID: 1699
		public float pressPoint;

		// Token: 0x040006A4 RID: 1700
		private double m_SlowTapStartTime;
	}
}
