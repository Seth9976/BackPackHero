using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000038 RID: 56
	internal class DefaultGroupManager : IGroupManager
	{
		// Token: 0x06000155 RID: 341 RVA: 0x000069C4 File Offset: 0x00004BC4
		public IGroupBoxOption GetSelectedOption()
		{
			return this.m_SelectedOption;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x000069DC File Offset: 0x00004BDC
		public void OnOptionSelectionChanged(IGroupBoxOption selectedOption)
		{
			bool flag = this.m_SelectedOption == selectedOption;
			if (!flag)
			{
				this.m_SelectedOption = selectedOption;
				foreach (IGroupBoxOption groupBoxOption in this.m_GroupOptions)
				{
					groupBoxOption.SetSelected(groupBoxOption == this.m_SelectedOption);
				}
			}
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00006A54 File Offset: 0x00004C54
		public void RegisterOption(IGroupBoxOption option)
		{
			bool flag = !this.m_GroupOptions.Contains(option);
			if (flag)
			{
				this.m_GroupOptions.Add(option);
			}
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00006A84 File Offset: 0x00004C84
		public void UnregisterOption(IGroupBoxOption option)
		{
			this.m_GroupOptions.Remove(option);
		}

		// Token: 0x04000090 RID: 144
		private List<IGroupBoxOption> m_GroupOptions = new List<IGroupBoxOption>();

		// Token: 0x04000091 RID: 145
		private IGroupBoxOption m_SelectedOption;
	}
}
