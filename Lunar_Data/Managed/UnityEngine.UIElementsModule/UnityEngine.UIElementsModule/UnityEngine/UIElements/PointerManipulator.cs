using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000063 RID: 99
	public abstract class PointerManipulator : MouseManipulator
	{
		// Token: 0x060002D5 RID: 725 RVA: 0x0000A8D0 File Offset: 0x00008AD0
		protected bool CanStartManipulation(IPointerEvent e)
		{
			foreach (ManipulatorActivationFilter manipulatorActivationFilter in base.activators)
			{
				bool flag = manipulatorActivationFilter.Matches(e);
				if (flag)
				{
					this.m_CurrentPointerId = e.pointerId;
					return true;
				}
			}
			return false;
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000A944 File Offset: 0x00008B44
		protected bool CanStopManipulation(IPointerEvent e)
		{
			bool flag = e == null;
			return !flag && e.pointerId == this.m_CurrentPointerId;
		}

		// Token: 0x0400014B RID: 331
		private int m_CurrentPointerId;
	}
}
