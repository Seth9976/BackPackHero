using System;

namespace Unity.VisualScripting
{
	// Token: 0x0200016E RID: 366
	public interface IUnitPortDefinition
	{
		// Token: 0x17000344 RID: 836
		// (get) Token: 0x0600096E RID: 2414
		string key { get; }

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x0600096F RID: 2415
		string label { get; }

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000970 RID: 2416
		string summary { get; }

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000971 RID: 2417
		bool hideLabel { get; }

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000972 RID: 2418
		bool isValid { get; }
	}
}
