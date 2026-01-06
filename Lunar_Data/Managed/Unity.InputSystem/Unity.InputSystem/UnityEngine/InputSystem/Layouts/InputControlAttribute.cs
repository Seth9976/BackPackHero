using System;

namespace UnityEngine.InputSystem.Layouts
{
	// Token: 0x02000106 RID: 262
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
	public sealed class InputControlAttribute : PropertyAttribute
	{
		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06000EBE RID: 3774 RVA: 0x00047A47 File Offset: 0x00045C47
		// (set) Token: 0x06000EBF RID: 3775 RVA: 0x00047A4F File Offset: 0x00045C4F
		public string layout { get; set; }

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06000EC0 RID: 3776 RVA: 0x00047A58 File Offset: 0x00045C58
		// (set) Token: 0x06000EC1 RID: 3777 RVA: 0x00047A60 File Offset: 0x00045C60
		public string variants { get; set; }

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06000EC2 RID: 3778 RVA: 0x00047A69 File Offset: 0x00045C69
		// (set) Token: 0x06000EC3 RID: 3779 RVA: 0x00047A71 File Offset: 0x00045C71
		public string name { get; set; }

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06000EC4 RID: 3780 RVA: 0x00047A7A File Offset: 0x00045C7A
		// (set) Token: 0x06000EC5 RID: 3781 RVA: 0x00047A82 File Offset: 0x00045C82
		public string format { get; set; }

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06000EC6 RID: 3782 RVA: 0x00047A8B File Offset: 0x00045C8B
		// (set) Token: 0x06000EC7 RID: 3783 RVA: 0x00047A93 File Offset: 0x00045C93
		public string usage { get; set; }

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06000EC8 RID: 3784 RVA: 0x00047A9C File Offset: 0x00045C9C
		// (set) Token: 0x06000EC9 RID: 3785 RVA: 0x00047AA4 File Offset: 0x00045CA4
		public string[] usages { get; set; }

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06000ECA RID: 3786 RVA: 0x00047AAD File Offset: 0x00045CAD
		// (set) Token: 0x06000ECB RID: 3787 RVA: 0x00047AB5 File Offset: 0x00045CB5
		public string parameters { get; set; }

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06000ECC RID: 3788 RVA: 0x00047ABE File Offset: 0x00045CBE
		// (set) Token: 0x06000ECD RID: 3789 RVA: 0x00047AC6 File Offset: 0x00045CC6
		public string processors { get; set; }

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06000ECE RID: 3790 RVA: 0x00047ACF File Offset: 0x00045CCF
		// (set) Token: 0x06000ECF RID: 3791 RVA: 0x00047AD7 File Offset: 0x00045CD7
		public string alias { get; set; }

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06000ED0 RID: 3792 RVA: 0x00047AE0 File Offset: 0x00045CE0
		// (set) Token: 0x06000ED1 RID: 3793 RVA: 0x00047AE8 File Offset: 0x00045CE8
		public string[] aliases { get; set; }

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06000ED2 RID: 3794 RVA: 0x00047AF1 File Offset: 0x00045CF1
		// (set) Token: 0x06000ED3 RID: 3795 RVA: 0x00047AF9 File Offset: 0x00045CF9
		public string useStateFrom { get; set; }

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06000ED4 RID: 3796 RVA: 0x00047B02 File Offset: 0x00045D02
		// (set) Token: 0x06000ED5 RID: 3797 RVA: 0x00047B0A File Offset: 0x00045D0A
		public uint bit { get; set; } = uint.MaxValue;

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06000ED6 RID: 3798 RVA: 0x00047B13 File Offset: 0x00045D13
		// (set) Token: 0x06000ED7 RID: 3799 RVA: 0x00047B1B File Offset: 0x00045D1B
		public uint offset { get; set; } = uint.MaxValue;

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06000ED8 RID: 3800 RVA: 0x00047B24 File Offset: 0x00045D24
		// (set) Token: 0x06000ED9 RID: 3801 RVA: 0x00047B2C File Offset: 0x00045D2C
		public uint sizeInBits { get; set; }

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06000EDA RID: 3802 RVA: 0x00047B35 File Offset: 0x00045D35
		// (set) Token: 0x06000EDB RID: 3803 RVA: 0x00047B3D File Offset: 0x00045D3D
		public int arraySize { get; set; }

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06000EDC RID: 3804 RVA: 0x00047B46 File Offset: 0x00045D46
		// (set) Token: 0x06000EDD RID: 3805 RVA: 0x00047B4E File Offset: 0x00045D4E
		public string displayName { get; set; }

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06000EDE RID: 3806 RVA: 0x00047B57 File Offset: 0x00045D57
		// (set) Token: 0x06000EDF RID: 3807 RVA: 0x00047B5F File Offset: 0x00045D5F
		public string shortDisplayName { get; set; }

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06000EE0 RID: 3808 RVA: 0x00047B68 File Offset: 0x00045D68
		// (set) Token: 0x06000EE1 RID: 3809 RVA: 0x00047B70 File Offset: 0x00045D70
		public bool noisy { get; set; }

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06000EE2 RID: 3810 RVA: 0x00047B79 File Offset: 0x00045D79
		// (set) Token: 0x06000EE3 RID: 3811 RVA: 0x00047B81 File Offset: 0x00045D81
		public bool synthetic { get; set; }

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06000EE4 RID: 3812 RVA: 0x00047B8A File Offset: 0x00045D8A
		// (set) Token: 0x06000EE5 RID: 3813 RVA: 0x00047B92 File Offset: 0x00045D92
		public bool dontReset { get; set; }

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06000EE6 RID: 3814 RVA: 0x00047B9B File Offset: 0x00045D9B
		// (set) Token: 0x06000EE7 RID: 3815 RVA: 0x00047BA3 File Offset: 0x00045DA3
		public object defaultState { get; set; }

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06000EE8 RID: 3816 RVA: 0x00047BAC File Offset: 0x00045DAC
		// (set) Token: 0x06000EE9 RID: 3817 RVA: 0x00047BB4 File Offset: 0x00045DB4
		public object minValue { get; set; }

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06000EEA RID: 3818 RVA: 0x00047BBD File Offset: 0x00045DBD
		// (set) Token: 0x06000EEB RID: 3819 RVA: 0x00047BC5 File Offset: 0x00045DC5
		public object maxValue { get; set; }
	}
}
