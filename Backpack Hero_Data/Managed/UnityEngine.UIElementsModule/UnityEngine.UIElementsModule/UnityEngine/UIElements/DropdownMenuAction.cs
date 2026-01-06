using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000021 RID: 33
	public class DropdownMenuAction : DropdownMenuItem
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00004FB4 File Offset: 0x000031B4
		public string name { get; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00004FBC File Offset: 0x000031BC
		// (set) Token: 0x060000D8 RID: 216 RVA: 0x00004FC4 File Offset: 0x000031C4
		public DropdownMenuAction.Status status { get; private set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00004FCD File Offset: 0x000031CD
		// (set) Token: 0x060000DA RID: 218 RVA: 0x00004FD5 File Offset: 0x000031D5
		public DropdownMenuEventInfo eventInfo { get; private set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00004FDE File Offset: 0x000031DE
		// (set) Token: 0x060000DC RID: 220 RVA: 0x00004FE6 File Offset: 0x000031E6
		public object userData { get; private set; }

		// Token: 0x060000DD RID: 221 RVA: 0x00004FF0 File Offset: 0x000031F0
		public static DropdownMenuAction.Status AlwaysEnabled(DropdownMenuAction a)
		{
			return DropdownMenuAction.Status.Normal;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00005004 File Offset: 0x00003204
		public static DropdownMenuAction.Status AlwaysDisabled(DropdownMenuAction a)
		{
			return DropdownMenuAction.Status.Disabled;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00005017 File Offset: 0x00003217
		public DropdownMenuAction(string actionName, Action<DropdownMenuAction> actionCallback, Func<DropdownMenuAction, DropdownMenuAction.Status> actionStatusCallback, object userData = null)
		{
			this.name = actionName;
			this.actionCallback = actionCallback;
			this.actionStatusCallback = actionStatusCallback;
			this.userData = userData;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x0000503F File Offset: 0x0000323F
		public void UpdateActionStatus(DropdownMenuEventInfo eventInfo)
		{
			this.eventInfo = eventInfo;
			Func<DropdownMenuAction, DropdownMenuAction.Status> func = this.actionStatusCallback;
			this.status = ((func != null) ? func.Invoke(this) : DropdownMenuAction.Status.Hidden);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00005064 File Offset: 0x00003264
		public void Execute()
		{
			Action<DropdownMenuAction> action = this.actionCallback;
			if (action != null)
			{
				action.Invoke(this);
			}
		}

		// Token: 0x0400005C RID: 92
		private readonly Action<DropdownMenuAction> actionCallback;

		// Token: 0x0400005D RID: 93
		private readonly Func<DropdownMenuAction, DropdownMenuAction.Status> actionStatusCallback;

		// Token: 0x02000022 RID: 34
		[Flags]
		public enum Status
		{
			// Token: 0x0400005F RID: 95
			None = 0,
			// Token: 0x04000060 RID: 96
			Normal = 1,
			// Token: 0x04000061 RID: 97
			Disabled = 2,
			// Token: 0x04000062 RID: 98
			Checked = 4,
			// Token: 0x04000063 RID: 99
			Hidden = 8
		}
	}
}
