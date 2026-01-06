using System;
using System.ComponentModel;
using UnityEngine.InputSystem.Controls;

namespace UnityEngine.InputSystem.Interactions
{
	// Token: 0x0200011D RID: 285
	[DisplayName("Hold")]
	public class HoldInteraction : IInputInteraction
	{
		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06001013 RID: 4115 RVA: 0x0004CCDF File Offset: 0x0004AEDF
		private float durationOrDefault
		{
			get
			{
				if ((double)this.duration <= 0.0)
				{
					return InputSystem.settings.defaultHoldTime;
				}
				return this.duration;
			}
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06001014 RID: 4116 RVA: 0x0004CD04 File Offset: 0x0004AF04
		private float pressPointOrDefault
		{
			get
			{
				if ((double)this.pressPoint <= 0.0)
				{
					return ButtonControl.s_GlobalDefaultButtonPressPoint;
				}
				return this.pressPoint;
			}
		}

		// Token: 0x06001015 RID: 4117 RVA: 0x0004CD24 File Offset: 0x0004AF24
		public void Process(ref InputInteractionContext context)
		{
			if (context.timerHasExpired)
			{
				context.PerformedAndStayPerformed();
				return;
			}
			switch (context.phase)
			{
			case InputActionPhase.Waiting:
				if (context.ControlIsActuated(this.pressPointOrDefault))
				{
					this.m_TimePressed = context.time;
					context.Started();
					context.SetTimeout(this.durationOrDefault);
					return;
				}
				break;
			case InputActionPhase.Started:
				if (context.time - this.m_TimePressed >= (double)this.durationOrDefault)
				{
					context.PerformedAndStayPerformed();
				}
				if (!context.ControlIsActuated(0f))
				{
					context.Canceled();
					return;
				}
				break;
			case InputActionPhase.Performed:
				if (!context.ControlIsActuated(this.pressPointOrDefault))
				{
					context.Canceled();
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x06001016 RID: 4118 RVA: 0x0004CDCD File Offset: 0x0004AFCD
		public void Reset()
		{
			this.m_TimePressed = 0.0;
		}

		// Token: 0x04000691 RID: 1681
		public float duration;

		// Token: 0x04000692 RID: 1682
		public float pressPoint;

		// Token: 0x04000693 RID: 1683
		private double m_TimePressed;
	}
}
