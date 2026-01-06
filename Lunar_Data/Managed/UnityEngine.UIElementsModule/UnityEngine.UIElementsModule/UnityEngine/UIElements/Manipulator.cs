using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200004A RID: 74
	public abstract class Manipulator : IManipulator
	{
		// Token: 0x060001CC RID: 460
		protected abstract void RegisterCallbacksOnTarget();

		// Token: 0x060001CD RID: 461
		protected abstract void UnregisterCallbacksFromTarget();

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060001CE RID: 462 RVA: 0x000087F4 File Offset: 0x000069F4
		// (set) Token: 0x060001CF RID: 463 RVA: 0x0000880C File Offset: 0x00006A0C
		public VisualElement target
		{
			get
			{
				return this.m_Target;
			}
			set
			{
				bool flag = this.target != null;
				if (flag)
				{
					this.UnregisterCallbacksFromTarget();
				}
				this.m_Target = value;
				bool flag2 = this.target != null;
				if (flag2)
				{
					this.RegisterCallbacksOnTarget();
				}
			}
		}

		// Token: 0x040000CF RID: 207
		private VisualElement m_Target;
	}
}
