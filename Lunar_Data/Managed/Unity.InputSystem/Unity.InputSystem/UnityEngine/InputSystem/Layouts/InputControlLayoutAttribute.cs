using System;

namespace UnityEngine.InputSystem.Layouts
{
	// Token: 0x02000109 RID: 265
	[AttributeUsage(AttributeTargets.Class, Inherited = false)]
	public sealed class InputControlLayoutAttribute : Attribute
	{
		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06000F21 RID: 3873 RVA: 0x00048EE3 File Offset: 0x000470E3
		// (set) Token: 0x06000F22 RID: 3874 RVA: 0x00048EEB File Offset: 0x000470EB
		public Type stateType { get; set; }

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06000F23 RID: 3875 RVA: 0x00048EF4 File Offset: 0x000470F4
		// (set) Token: 0x06000F24 RID: 3876 RVA: 0x00048EFC File Offset: 0x000470FC
		public string stateFormat { get; set; }

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06000F25 RID: 3877 RVA: 0x00048F05 File Offset: 0x00047105
		// (set) Token: 0x06000F26 RID: 3878 RVA: 0x00048F0D File Offset: 0x0004710D
		public string[] commonUsages { get; set; }

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06000F27 RID: 3879 RVA: 0x00048F16 File Offset: 0x00047116
		// (set) Token: 0x06000F28 RID: 3880 RVA: 0x00048F1E File Offset: 0x0004711E
		public string variants { get; set; }

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06000F29 RID: 3881 RVA: 0x00048F27 File Offset: 0x00047127
		// (set) Token: 0x06000F2A RID: 3882 RVA: 0x00048F2F File Offset: 0x0004712F
		public bool isNoisy { get; set; }

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06000F2B RID: 3883 RVA: 0x00048F38 File Offset: 0x00047138
		// (set) Token: 0x06000F2C RID: 3884 RVA: 0x00048F45 File Offset: 0x00047145
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

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06000F2D RID: 3885 RVA: 0x00048F53 File Offset: 0x00047153
		// (set) Token: 0x06000F2E RID: 3886 RVA: 0x00048F60 File Offset: 0x00047160
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

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06000F2F RID: 3887 RVA: 0x00048F6E File Offset: 0x0004716E
		// (set) Token: 0x06000F30 RID: 3888 RVA: 0x00048F76 File Offset: 0x00047176
		public bool isGenericTypeOfDevice { get; set; }

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06000F31 RID: 3889 RVA: 0x00048F7F File Offset: 0x0004717F
		// (set) Token: 0x06000F32 RID: 3890 RVA: 0x00048F87 File Offset: 0x00047187
		public string displayName { get; set; }

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06000F33 RID: 3891 RVA: 0x00048F90 File Offset: 0x00047190
		// (set) Token: 0x06000F34 RID: 3892 RVA: 0x00048F98 File Offset: 0x00047198
		public string description { get; set; }

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06000F35 RID: 3893 RVA: 0x00048FA1 File Offset: 0x000471A1
		// (set) Token: 0x06000F36 RID: 3894 RVA: 0x00048FA9 File Offset: 0x000471A9
		public bool hideInUI { get; set; }

		// Token: 0x0400063D RID: 1597
		internal bool? canRunInBackgroundInternal;

		// Token: 0x0400063E RID: 1598
		internal bool? updateBeforeRenderInternal;
	}
}
