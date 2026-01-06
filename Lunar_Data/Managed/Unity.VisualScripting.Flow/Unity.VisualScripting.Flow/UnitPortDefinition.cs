using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000174 RID: 372
	public abstract class UnitPortDefinition : IUnitPortDefinition
	{
		// Token: 0x1700035A RID: 858
		// (get) Token: 0x060009A5 RID: 2469 RVA: 0x0001127C File Offset: 0x0000F47C
		// (set) Token: 0x060009A6 RID: 2470 RVA: 0x00011284 File Offset: 0x0000F484
		[Serialize]
		[Inspectable]
		[InspectorDelayed]
		[WarnBeforeEditing("Edit Port Key", "Changing the key of this definition will break any existing connection to this port. Are you sure you want to continue?", new object[] { null, "" })]
		public string key { get; set; }

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x060009A7 RID: 2471 RVA: 0x0001128D File Offset: 0x0000F48D
		// (set) Token: 0x060009A8 RID: 2472 RVA: 0x00011295 File Offset: 0x0000F495
		[Serialize]
		[Inspectable]
		public string label { get; set; }

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x060009A9 RID: 2473 RVA: 0x0001129E File Offset: 0x0000F49E
		// (set) Token: 0x060009AA RID: 2474 RVA: 0x000112A6 File Offset: 0x0000F4A6
		[Serialize]
		[Inspectable]
		[InspectorTextArea]
		public string summary { get; set; }

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x060009AB RID: 2475 RVA: 0x000112AF File Offset: 0x0000F4AF
		// (set) Token: 0x060009AC RID: 2476 RVA: 0x000112B7 File Offset: 0x0000F4B7
		[Serialize]
		[Inspectable]
		public bool hideLabel { get; set; }

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x060009AD RID: 2477 RVA: 0x000112C0 File Offset: 0x0000F4C0
		[DoNotSerialize]
		public virtual bool isValid
		{
			get
			{
				return !string.IsNullOrEmpty(this.key);
			}
		}
	}
}
