using System;
using UnityEngine;

// Token: 0x020000A0 RID: 160
[Serializable]
public class UDictionary
{
	// Token: 0x02000120 RID: 288
	public class SplitAttribute : PropertyAttribute
	{
		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060005E0 RID: 1504 RVA: 0x0001A139 File Offset: 0x00018339
		// (set) Token: 0x060005E1 RID: 1505 RVA: 0x0001A141 File Offset: 0x00018341
		public float Key { get; protected set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060005E2 RID: 1506 RVA: 0x0001A14A File Offset: 0x0001834A
		// (set) Token: 0x060005E3 RID: 1507 RVA: 0x0001A152 File Offset: 0x00018352
		public float Value { get; protected set; }

		// Token: 0x060005E4 RID: 1508 RVA: 0x0001A15B File Offset: 0x0001835B
		public SplitAttribute(float key, float value)
		{
			this.Key = key;
			this.Value = value;
		}
	}
}
