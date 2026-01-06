using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020002AA RID: 682
	[Serializable]
	internal struct StyleValueHandle
	{
		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x06001713 RID: 5907 RVA: 0x0005E324 File Offset: 0x0005C524
		// (set) Token: 0x06001714 RID: 5908 RVA: 0x0005E33C File Offset: 0x0005C53C
		public StyleValueType valueType
		{
			get
			{
				return this.m_ValueType;
			}
			internal set
			{
				this.m_ValueType = value;
			}
		}

		// Token: 0x06001715 RID: 5909 RVA: 0x0005E346 File Offset: 0x0005C546
		internal StyleValueHandle(int valueIndex, StyleValueType valueType)
		{
			this.valueIndex = valueIndex;
			this.m_ValueType = valueType;
		}

		// Token: 0x040009C2 RID: 2498
		[SerializeField]
		private StyleValueType m_ValueType;

		// Token: 0x040009C3 RID: 2499
		[SerializeField]
		internal int valueIndex;
	}
}
