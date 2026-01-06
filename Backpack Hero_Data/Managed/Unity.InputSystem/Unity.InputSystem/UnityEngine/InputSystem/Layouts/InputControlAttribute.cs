using System;

namespace UnityEngine.InputSystem.Layouts
{
	// Token: 0x02000106 RID: 262
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
	public sealed class InputControlAttribute : PropertyAttribute
	{
		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06000EC3 RID: 3779 RVA: 0x00047A93 File Offset: 0x00045C93
		// (set) Token: 0x06000EC4 RID: 3780 RVA: 0x00047A9B File Offset: 0x00045C9B
		public string layout { get; set; }

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06000EC5 RID: 3781 RVA: 0x00047AA4 File Offset: 0x00045CA4
		// (set) Token: 0x06000EC6 RID: 3782 RVA: 0x00047AAC File Offset: 0x00045CAC
		public string variants { get; set; }

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06000EC7 RID: 3783 RVA: 0x00047AB5 File Offset: 0x00045CB5
		// (set) Token: 0x06000EC8 RID: 3784 RVA: 0x00047ABD File Offset: 0x00045CBD
		public string name { get; set; }

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06000EC9 RID: 3785 RVA: 0x00047AC6 File Offset: 0x00045CC6
		// (set) Token: 0x06000ECA RID: 3786 RVA: 0x00047ACE File Offset: 0x00045CCE
		public string format { get; set; }

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06000ECB RID: 3787 RVA: 0x00047AD7 File Offset: 0x00045CD7
		// (set) Token: 0x06000ECC RID: 3788 RVA: 0x00047ADF File Offset: 0x00045CDF
		public string usage { get; set; }

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06000ECD RID: 3789 RVA: 0x00047AE8 File Offset: 0x00045CE8
		// (set) Token: 0x06000ECE RID: 3790 RVA: 0x00047AF0 File Offset: 0x00045CF0
		public string[] usages { get; set; }

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06000ECF RID: 3791 RVA: 0x00047AF9 File Offset: 0x00045CF9
		// (set) Token: 0x06000ED0 RID: 3792 RVA: 0x00047B01 File Offset: 0x00045D01
		public string parameters { get; set; }

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06000ED1 RID: 3793 RVA: 0x00047B0A File Offset: 0x00045D0A
		// (set) Token: 0x06000ED2 RID: 3794 RVA: 0x00047B12 File Offset: 0x00045D12
		public string processors { get; set; }

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06000ED3 RID: 3795 RVA: 0x00047B1B File Offset: 0x00045D1B
		// (set) Token: 0x06000ED4 RID: 3796 RVA: 0x00047B23 File Offset: 0x00045D23
		public string alias { get; set; }

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06000ED5 RID: 3797 RVA: 0x00047B2C File Offset: 0x00045D2C
		// (set) Token: 0x06000ED6 RID: 3798 RVA: 0x00047B34 File Offset: 0x00045D34
		public string[] aliases { get; set; }

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06000ED7 RID: 3799 RVA: 0x00047B3D File Offset: 0x00045D3D
		// (set) Token: 0x06000ED8 RID: 3800 RVA: 0x00047B45 File Offset: 0x00045D45
		public string useStateFrom { get; set; }

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06000ED9 RID: 3801 RVA: 0x00047B4E File Offset: 0x00045D4E
		// (set) Token: 0x06000EDA RID: 3802 RVA: 0x00047B56 File Offset: 0x00045D56
		public uint bit { get; set; } = uint.MaxValue;

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06000EDB RID: 3803 RVA: 0x00047B5F File Offset: 0x00045D5F
		// (set) Token: 0x06000EDC RID: 3804 RVA: 0x00047B67 File Offset: 0x00045D67
		public uint offset { get; set; } = uint.MaxValue;

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06000EDD RID: 3805 RVA: 0x00047B70 File Offset: 0x00045D70
		// (set) Token: 0x06000EDE RID: 3806 RVA: 0x00047B78 File Offset: 0x00045D78
		public uint sizeInBits { get; set; }

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06000EDF RID: 3807 RVA: 0x00047B81 File Offset: 0x00045D81
		// (set) Token: 0x06000EE0 RID: 3808 RVA: 0x00047B89 File Offset: 0x00045D89
		public int arraySize { get; set; }

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06000EE1 RID: 3809 RVA: 0x00047B92 File Offset: 0x00045D92
		// (set) Token: 0x06000EE2 RID: 3810 RVA: 0x00047B9A File Offset: 0x00045D9A
		public string displayName { get; set; }

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06000EE3 RID: 3811 RVA: 0x00047BA3 File Offset: 0x00045DA3
		// (set) Token: 0x06000EE4 RID: 3812 RVA: 0x00047BAB File Offset: 0x00045DAB
		public string shortDisplayName { get; set; }

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06000EE5 RID: 3813 RVA: 0x00047BB4 File Offset: 0x00045DB4
		// (set) Token: 0x06000EE6 RID: 3814 RVA: 0x00047BBC File Offset: 0x00045DBC
		public bool noisy { get; set; }

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06000EE7 RID: 3815 RVA: 0x00047BC5 File Offset: 0x00045DC5
		// (set) Token: 0x06000EE8 RID: 3816 RVA: 0x00047BCD File Offset: 0x00045DCD
		public bool synthetic { get; set; }

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06000EE9 RID: 3817 RVA: 0x00047BD6 File Offset: 0x00045DD6
		// (set) Token: 0x06000EEA RID: 3818 RVA: 0x00047BDE File Offset: 0x00045DDE
		public bool dontReset { get; set; }

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06000EEB RID: 3819 RVA: 0x00047BE7 File Offset: 0x00045DE7
		// (set) Token: 0x06000EEC RID: 3820 RVA: 0x00047BEF File Offset: 0x00045DEF
		public object defaultState { get; set; }

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06000EED RID: 3821 RVA: 0x00047BF8 File Offset: 0x00045DF8
		// (set) Token: 0x06000EEE RID: 3822 RVA: 0x00047C00 File Offset: 0x00045E00
		public object minValue { get; set; }

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06000EEF RID: 3823 RVA: 0x00047C09 File Offset: 0x00045E09
		// (set) Token: 0x06000EF0 RID: 3824 RVA: 0x00047C11 File Offset: 0x00045E11
		public object maxValue { get; set; }
	}
}
