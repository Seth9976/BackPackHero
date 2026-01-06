using System;

namespace UnityEngine.InputSystem.Users
{
	// Token: 0x02000081 RID: 129
	[Serializable]
	internal class InputUserSettings
	{
		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000A7D RID: 2685 RVA: 0x00038D5C File Offset: 0x00036F5C
		// (set) Token: 0x06000A7E RID: 2686 RVA: 0x00038D64 File Offset: 0x00036F64
		public string customBindings { get; set; }

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000A7F RID: 2687 RVA: 0x00038D6D File Offset: 0x00036F6D
		// (set) Token: 0x06000A80 RID: 2688 RVA: 0x00038D75 File Offset: 0x00036F75
		public bool invertMouseX { get; set; }

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000A81 RID: 2689 RVA: 0x00038D7E File Offset: 0x00036F7E
		// (set) Token: 0x06000A82 RID: 2690 RVA: 0x00038D86 File Offset: 0x00036F86
		public bool invertMouseY { get; set; }

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000A83 RID: 2691 RVA: 0x00038D8F File Offset: 0x00036F8F
		// (set) Token: 0x06000A84 RID: 2692 RVA: 0x00038D97 File Offset: 0x00036F97
		public float? mouseSmoothing { get; set; }

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000A85 RID: 2693 RVA: 0x00038DA0 File Offset: 0x00036FA0
		// (set) Token: 0x06000A86 RID: 2694 RVA: 0x00038DA8 File Offset: 0x00036FA8
		public float? mouseSensitivity { get; set; }

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000A87 RID: 2695 RVA: 0x00038DB1 File Offset: 0x00036FB1
		// (set) Token: 0x06000A88 RID: 2696 RVA: 0x00038DB9 File Offset: 0x00036FB9
		public bool invertStickX { get; set; }

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000A89 RID: 2697 RVA: 0x00038DC2 File Offset: 0x00036FC2
		// (set) Token: 0x06000A8A RID: 2698 RVA: 0x00038DCA File Offset: 0x00036FCA
		public bool invertStickY { get; set; }

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000A8B RID: 2699 RVA: 0x00038DD3 File Offset: 0x00036FD3
		// (set) Token: 0x06000A8C RID: 2700 RVA: 0x00038DDB File Offset: 0x00036FDB
		public bool swapSticks { get; set; }

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000A8D RID: 2701 RVA: 0x00038DE4 File Offset: 0x00036FE4
		// (set) Token: 0x06000A8E RID: 2702 RVA: 0x00038DEC File Offset: 0x00036FEC
		public bool swapBumpers { get; set; }

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000A8F RID: 2703 RVA: 0x00038DF5 File Offset: 0x00036FF5
		// (set) Token: 0x06000A90 RID: 2704 RVA: 0x00038DFD File Offset: 0x00036FFD
		public bool swapTriggers { get; set; }

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000A91 RID: 2705 RVA: 0x00038E06 File Offset: 0x00037006
		// (set) Token: 0x06000A92 RID: 2706 RVA: 0x00038E0E File Offset: 0x0003700E
		public bool swapDpadAndLeftStick { get; set; }

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000A93 RID: 2707 RVA: 0x00038E17 File Offset: 0x00037017
		// (set) Token: 0x06000A94 RID: 2708 RVA: 0x00038E1F File Offset: 0x0003701F
		public float vibrationStrength { get; set; }

		// Token: 0x06000A95 RID: 2709 RVA: 0x00038E28 File Offset: 0x00037028
		public virtual void Apply(IInputActionCollection actions)
		{
		}

		// Token: 0x040003A3 RID: 931
		[SerializeField]
		private string m_CustomBindings;
	}
}
