using System;

namespace UnityEngine.InputSystem.Layouts
{
	// Token: 0x02000109 RID: 265
	[AttributeUsage(AttributeTargets.Class, Inherited = false)]
	public sealed class InputControlLayoutAttribute : Attribute
	{
		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06000F26 RID: 3878 RVA: 0x00048F2F File Offset: 0x0004712F
		// (set) Token: 0x06000F27 RID: 3879 RVA: 0x00048F37 File Offset: 0x00047137
		public Type stateType { get; set; }

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06000F28 RID: 3880 RVA: 0x00048F40 File Offset: 0x00047140
		// (set) Token: 0x06000F29 RID: 3881 RVA: 0x00048F48 File Offset: 0x00047148
		public string stateFormat { get; set; }

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06000F2A RID: 3882 RVA: 0x00048F51 File Offset: 0x00047151
		// (set) Token: 0x06000F2B RID: 3883 RVA: 0x00048F59 File Offset: 0x00047159
		public string[] commonUsages { get; set; }

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06000F2C RID: 3884 RVA: 0x00048F62 File Offset: 0x00047162
		// (set) Token: 0x06000F2D RID: 3885 RVA: 0x00048F6A File Offset: 0x0004716A
		public string variants { get; set; }

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06000F2E RID: 3886 RVA: 0x00048F73 File Offset: 0x00047173
		// (set) Token: 0x06000F2F RID: 3887 RVA: 0x00048F7B File Offset: 0x0004717B
		public bool isNoisy { get; set; }

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06000F30 RID: 3888 RVA: 0x00048F84 File Offset: 0x00047184
		// (set) Token: 0x06000F31 RID: 3889 RVA: 0x00048F91 File Offset: 0x00047191
		public bool canRunInBackground
		{
			get
			{
				return this.canRunInBackgroundInternal.Value;
			}
			set
			{
				this.canRunInBackgroundInternal = new bool?(value);
			}
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06000F32 RID: 3890 RVA: 0x00048F9F File Offset: 0x0004719F
		// (set) Token: 0x06000F33 RID: 3891 RVA: 0x00048FAC File Offset: 0x000471AC
		public bool updateBeforeRender
		{
			get
			{
				return this.updateBeforeRenderInternal.Value;
			}
			set
			{
				this.updateBeforeRenderInternal = new bool?(value);
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06000F34 RID: 3892 RVA: 0x00048FBA File Offset: 0x000471BA
		// (set) Token: 0x06000F35 RID: 3893 RVA: 0x00048FC2 File Offset: 0x000471C2
		public bool isGenericTypeOfDevice { get; set; }

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06000F36 RID: 3894 RVA: 0x00048FCB File Offset: 0x000471CB
		// (set) Token: 0x06000F37 RID: 3895 RVA: 0x00048FD3 File Offset: 0x000471D3
		public string displayName { get; set; }

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06000F38 RID: 3896 RVA: 0x00048FDC File Offset: 0x000471DC
		// (set) Token: 0x06000F39 RID: 3897 RVA: 0x00048FE4 File Offset: 0x000471E4
		public string description { get; set; }

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06000F3A RID: 3898 RVA: 0x00048FED File Offset: 0x000471ED
		// (set) Token: 0x06000F3B RID: 3899 RVA: 0x00048FF5 File Offset: 0x000471F5
		public bool hideInUI { get; set; }

		// Token: 0x0400063E RID: 1598
		internal bool? canRunInBackgroundInternal;

		// Token: 0x0400063F RID: 1599
		internal bool? updateBeforeRenderInternal;
	}
}
