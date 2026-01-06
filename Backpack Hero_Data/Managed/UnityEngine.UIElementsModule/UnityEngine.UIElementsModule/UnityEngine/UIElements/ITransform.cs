using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000043 RID: 67
	public interface ITransform
	{
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060001A7 RID: 423
		// (set) Token: 0x060001A8 RID: 424
		Vector3 position { get; set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060001A9 RID: 425
		// (set) Token: 0x060001AA RID: 426
		Quaternion rotation { get; set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060001AB RID: 427
		// (set) Token: 0x060001AC RID: 428
		Vector3 scale { get; set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060001AD RID: 429
		Matrix4x4 matrix { get; }
	}
}
