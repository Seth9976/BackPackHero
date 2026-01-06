using System;
using System.ComponentModel;
using UnityEngine.InputSystem.Controls;

namespace UnityEngine.InputSystem.Interactions
{
	// Token: 0x0200011D RID: 285
	[DisplayName("Hold")]
	public class HoldInteraction : IInputInteraction
	{
		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x0600100E RID: 4110 RVA: 0x0004CC93 File Offset: 0x0004AE93
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

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x0600100F RID: 4111 RVA: 0x0004CCB8 File Offset: 0x0004AEB8
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

		// Token: 0x06001010 RID: 4112 RVA: 0x0004CCD8 File Offset: 0x0004AED8
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

		// Token: 0x06001011 RID: 4113 RVA: 0x0004CD81 File Offset: 0x0004AF81
		public void Reset()
		{
			this.m_TimePressed = 0.0;
		}

		// Token: 0x04000690 RID: 1680
		public float duration;

		// Token: 0x04000691 RID: 1681
		public float pressPoint;

		// Token: 0x04000692 RID: 1682
		private double m_TimePressed;
	}
}
