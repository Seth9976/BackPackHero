using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020002A0 RID: 672
	[Serializable]
	internal class StyleRule
	{
		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x060016CE RID: 5838 RVA: 0x0005D608 File Offset: 0x0005B808
		// (set) Token: 0x060016CF RID: 5839 RVA: 0x0005D620 File Offset: 0x0005B820
		public StyleProperty[] properties
		{
			get
			{
				return this.m_Properties;
			}
			internal set
			{
				this.m_Properties = value;
			}
		}

		// Token: 0x0400098D RID: 2445
		[SerializeField]
		private StyleProperty[] m_Properties;

		// Token: 0x0400098E RID: 2446
		[SerializeField]
		internal int line;

		// Token: 0x0400098F RID: 2447
		[NonSerialized]
		internal int customPropertiesCount;
	}
}
