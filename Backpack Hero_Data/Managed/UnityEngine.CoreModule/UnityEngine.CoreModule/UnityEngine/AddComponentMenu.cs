using System;

namespace UnityEngine
{
	// Token: 0x020001EF RID: 495
	public sealed class AddComponentMenu : Attribute
	{
		// Token: 0x0600164C RID: 5708 RVA: 0x00023BD6 File Offset: 0x00021DD6
		public AddComponentMenu(string menuName)
		{
			this.m_AddComponentMenu = menuName;
			this.m_Ordering = 0;
		}

		// Token: 0x0600164D RID: 5709 RVA: 0x00023BEE File Offset: 0x00021DEE
		public AddComponentMenu(string menuName, int order)
		{
			this.m_AddComponentMenu = menuName;
			this.m_Ordering = order;
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x0600164E RID: 5710 RVA: 0x00023C08 File Offset: 0x00021E08
		public string componentMenu
		{
			get
			{
				return this.m_AddComponentMenu;
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x0600164F RID: 5711 RVA: 0x00023C20 File Offset: 0x00021E20
		public int componentOrder
		{
			get
			{
				return this.m_Ordering;
			}
		}

		// Token: 0x040007CB RID: 1995
		private string m_AddComponentMenu;

		// Token: 0x040007CC RID: 1996
		private int m_Ordering;
	}
}
