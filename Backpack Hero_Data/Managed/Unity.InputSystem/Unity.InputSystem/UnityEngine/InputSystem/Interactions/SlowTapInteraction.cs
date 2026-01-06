using System;
using System.ComponentModel;
using UnityEngine.InputSystem.Controls;

namespace UnityEngine.InputSystem.Interactions
{
	// Token: 0x02000121 RID: 289
	[DisplayName("Long Tap")]
	public class SlowTapInteraction : IInputInteraction
	{
		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06001024 RID: 4132 RVA: 0x0004D1C5 File Offset: 0x0004B3C5
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

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06001025 RID: 4133 RVA: 0x0004D1E5 File Offset: 0x0004B3E5
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

		// Token: 0x06001026 RID: 4134 RVA: 0x0004D200 File Offset: 0x0004B400
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

		// Token: 0x06001027 RID: 4135 RVA: 0x0004D26F File Offset: 0x0004B46F
		public void Reset()
		{
			this.m_SlowTapStartTime = 0.0;
		}

		// Token: 0x040006A3 RID: 1699
		public float duration;

		// Token: 0x040006A4 RID: 1700
		public float pressPoint;

		// Token: 0x040006A5 RID: 1701
		private double m_SlowTapStartTime;
	}
}
