using System;
using System.ComponentModel;
using UnityEngine.InputSystem.Controls;

namespace UnityEngine.InputSystem.Interactions
{
	// Token: 0x0200011F RID: 287
	[DisplayName("Press")]
	public class PressInteraction : IInputInteraction
	{
		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x0600101A RID: 4122 RVA: 0x0004CF99 File Offset: 0x0004B199
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
		// (get) Token: 0x0600101B RID: 4123 RVA: 0x0004CFB4 File Offset: 0x0004B1B4
		private float releasePointOrDefault
		{
			get
			{
				return this.pressPointOrDefault * ButtonControl.s_GlobalDefaultButtonReleaseThreshold;
			}
		}

		// Token: 0x0600101C RID: 4124 RVA: 0x0004CFC4 File Offset: 0x0004B1C4
		public void Process(ref InputInteractionContext context)
		{
			float num = context.ComputeMagnitude();
			switch (this.behavior)
			{
			case PressBehavior.PressOnly:
				if (this.m_WaitingForRelease)
				{
					if (num <= this.releasePointOrDefault)
					{
						this.m_WaitingForRelease = false;
						if (Mathf.Approximately(0f, num))
						{
							context.Canceled();
							return;
						}
						context.Started();
						return;
					}
				}
				else
				{
					if (num >= this.pressPointOrDefault)
					{
						this.m_WaitingForRelease = true;
						context.PerformedAndStayPerformed();
						return;
					}
					if (num > 0f && !context.isStarted)
					{
						context.Started();
						return;
					}
					if (Mathf.Approximately(0f, num) && context.isStarted)
					{
						context.Canceled();
						return;
					}
				}
				break;
			case PressBehavior.ReleaseOnly:
				if (this.m_WaitingForRelease)
				{
					if (num <= this.releasePointOrDefault)
					{
						this.m_WaitingForRelease = false;
						context.Performed();
						context.Canceled();
						return;
					}
				}
				else if (num >= this.pressPointOrDefault)
				{
					this.m_WaitingForRelease = true;
					if (!context.isStarted)
					{
						context.Started();
						return;
					}
				}
				else
				{
					bool isStarted = context.isStarted;
					if (num > 0f && !isStarted)
					{
						context.Started();
						return;
					}
					if (Mathf.Approximately(0f, num) && isStarted)
					{
						context.Canceled();
						return;
					}
				}
				break;
			case PressBehavior.PressAndRelease:
				if (this.m_WaitingForRelease)
				{
					if (num <= this.releasePointOrDefault)
					{
						this.m_WaitingForRelease = false;
						context.Performed();
						if (Mathf.Approximately(0f, num))
						{
							context.Canceled();
							return;
						}
					}
				}
				else
				{
					if (num >= this.pressPointOrDefault)
					{
						this.m_WaitingForRelease = true;
						context.PerformedAndStayPerformed();
						return;
					}
					bool isStarted2 = context.isStarted;
					if (num > 0f && !isStarted2)
					{
						context.Started();
						return;
					}
					if (Mathf.Approximately(0f, num) && isStarted2)
					{
						context.Canceled();
					}
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x0600101D RID: 4125 RVA: 0x0004D168 File Offset: 0x0004B368
		public void Reset()
		{
			this.m_WaitingForRelease = false;
		}

		// Token: 0x0400069B RID: 1691
		[Tooltip("The amount of actuation a control requires before being considered pressed. If not set, default to 'Default Press Point' in the global input settings.")]
		public float pressPoint;

		// Token: 0x0400069C RID: 1692
		[Tooltip("Determines how button presses trigger the action. By default (PressOnly), the action is performed on press. With ReleaseOnly, the action is performed on release. With PressAndRelease, the action is performed on press and release.")]
		public PressBehavior behavior;

		// Token: 0x0400069D RID: 1693
		private bool m_WaitingForRelease;
	}
}
