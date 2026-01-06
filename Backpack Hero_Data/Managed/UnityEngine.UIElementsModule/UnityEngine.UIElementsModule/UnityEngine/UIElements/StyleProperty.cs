using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200029F RID: 671
	[Serializable]
	internal class StyleProperty
	{
		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x060016C7 RID: 5831 RVA: 0x0005D59C File Offset: 0x0005B79C
		// (set) Token: 0x060016C8 RID: 5832 RVA: 0x0005D5B4 File Offset: 0x0005B7B4
		public string name
		{
			get
			{
				return this.m_Name;
			}
			internal set
			{
				this.m_Name = value;
			}
		}

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x060016C9 RID: 5833 RVA: 0x0005D5C0 File Offset: 0x0005B7C0
		// (set) Token: 0x060016CA RID: 5834 RVA: 0x0005D5D8 File Offset: 0x0005B7D8
		public int line
		{
			get
			{
				return this.m_Line;
			}
			internal set
			{
				this.m_Line = value;
			}
		}

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x060016CB RID: 5835 RVA: 0x0005D5E4 File Offset: 0x0005B7E4
		// (set) Token: 0x060016CC RID: 5836 RVA: 0x0005D5FC File Offset: 0x0005B7FC
		public StyleValueHandle[] values
		{
			get
			{
				return this.m_Values;
			}
			internal set
			{
				this.m_Values = value;
			}
		}

		// Token: 0x04000988 RID: 2440
		[SerializeField]
		private string m_Name;

		// Token: 0x04000989 RID: 2441
		[SerializeField]
		private int m_Line;

		// Token: 0x0400098A RID: 2442
		[SerializeField]
		private StyleValueHandle[] m_Values;

		// Token: 0x0400098B RID: 2443
		[NonSerialized]
		internal bool isCustomProperty;

		// Token: 0x0400098C RID: 2444
		[NonSerialized]
		internal bool requireVariableResolve;
	}
}
