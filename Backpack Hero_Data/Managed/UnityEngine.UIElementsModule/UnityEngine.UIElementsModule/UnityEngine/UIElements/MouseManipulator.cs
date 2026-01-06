using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x0200004D RID: 77
	public abstract class MouseManipulator : Manipulator
	{
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x0000894D File Offset: 0x00006B4D
		// (set) Token: 0x060001D7 RID: 471 RVA: 0x00008955 File Offset: 0x00006B55
		public List<ManipulatorActivationFilter> activators { get; private set; }

		// Token: 0x060001D8 RID: 472 RVA: 0x0000895E File Offset: 0x00006B5E
		protected MouseManipulator()
		{
			this.activators = new List<ManipulatorActivationFilter>();
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00008974 File Offset: 0x00006B74
		protected bool CanStartManipulation(IMouseEvent e)
		{
			foreach (ManipulatorActivationFilter manipulatorActivationFilter in this.activators)
			{
				bool flag = manipulatorActivationFilter.Matches(e);
				if (flag)
				{
					this.m_currentActivator = manipulatorActivationFilter;
					return true;
				}
			}
			return false;
		}

		// Token: 0x060001DA RID: 474 RVA: 0x000089E4 File Offset: 0x00006BE4
		protected bool CanStopManipulation(IMouseEvent e)
		{
			bool flag = e == null;
			return !flag && e.button == (int)this.m_currentActivator.button;
		}

		// Token: 0x040000D7 RID: 215
		private ManipulatorActivationFilter m_currentActivator;
	}
}
