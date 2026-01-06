using System;

namespace UnityEngine.InputSystem.Users
{
	// Token: 0x02000081 RID: 129
	[Serializable]
	internal class InputUserSettings
	{
		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000A7B RID: 2683 RVA: 0x00038D20 File Offset: 0x00036F20
		// (set) Token: 0x06000A7C RID: 2684 RVA: 0x00038D28 File Offset: 0x00036F28
		public string customBindings { get; set; }

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000A7D RID: 2685 RVA: 0x00038D31 File Offset: 0x00036F31
		// (set) Token: 0x06000A7E RID: 2686 RVA: 0x00038D39 File Offset: 0x00036F39
		public bool invertMouseX { get; set; }

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000A7F RID: 2687 RVA: 0x00038D42 File Offset: 0x00036F42
		// (set) Token: 0x06000A80 RID: 2688 RVA: 0x00038D4A File Offset: 0x00036F4A
		public bool invertMouseY { get; set; }

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000A81 RID: 2689 RVA: 0x00038D53 File Offset: 0x00036F53
		// (set) Token: 0x06000A82 RID: 2690 RVA: 0x00038D5B File Offset: 0x00036F5B
		public float? mouseSmoothing { get; set; }

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000A83 RID: 2691 RVA: 0x00038D64 File Offset: 0x00036F64
		// (set) Token: 0x06000A84 RID: 2692 RVA: 0x00038D6C File Offset: 0x00036F6C
		public float? mouseSensitivity { get; set; }

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000A85 RID: 2693 RVA: 0x00038D75 File Offset: 0x00036F75
		// (set) Token: 0x06000A86 RID: 2694 RVA: 0x00038D7D File Offset: 0x00036F7D
		public bool invertStickX { get; set; }

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000A87 RID: 2695 RVA: 0x00038D86 File Offset: 0x00036F86
		// (set) Token: 0x06000A88 RID: 2696 RVA: 0x00038D8E File Offset: 0x00036F8E
		public bool invertStickY { get; set; }

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000A89 RID: 2697 RVA: 0x00038D97 File Offset: 0x00036F97
		// (set) Token: 0x06000A8A RID: 2698 RVA: 0x00038D9F File Offset: 0x00036F9F
		public bool swapSticks { get; set; }

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000A8B RID: 2699 RVA: 0x00038DA8 File Offset: 0x00036FA8
		// (set) Token: 0x06000A8C RID: 2700 RVA: 0x00038DB0 File Offset: 0x00036FB0
		public bool swapBumpers { get; set; }

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000A8D RID: 2701 RVA: 0x00038DB9 File Offset: 0x00036FB9
		// (set) Token: 0x06000A8E RID: 2702 RVA: 0x00038DC1 File Offset: 0x00036FC1
		public bool swapTriggers { get; set; }

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000A8F RID: 2703 RVA: 0x00038DCA File Offset: 0x00036FCA
		// (set) Token: 0x06000A90 RID: 2704 RVA: 0x00038DD2 File Offset: 0x00036FD2
		public bool swapDpadAndLeftStick { get; set; }

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000A91 RID: 2705 RVA: 0x00038DDB File Offset: 0x00036FDB
		// (set) Token: 0x06000A92 RID: 2706 RVA: 0x00038DE3 File Offset: 0x00036FE3
		public float vibrationStrength { get; set; }

		// Token: 0x06000A93 RID: 2707 RVA: 0x00038DEC File Offset: 0x00036FEC
		public virtual void Apply(IInputActionCollection actions)
		{
		}

		// Token: 0x040003A3 RID: 931
		[SerializeField]
		private string m_CustomBindings;
	}
}
