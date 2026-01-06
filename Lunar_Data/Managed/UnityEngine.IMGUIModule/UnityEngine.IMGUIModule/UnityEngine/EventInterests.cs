using System;

namespace UnityEngine
{
	// Token: 0x02000009 RID: 9
	internal struct EventInterests
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600004B RID: 75 RVA: 0x0000373E File Offset: 0x0000193E
		// (set) Token: 0x0600004C RID: 76 RVA: 0x00003746 File Offset: 0x00001946
		public bool wantsMouseMove { readonly get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600004D RID: 77 RVA: 0x0000374F File Offset: 0x0000194F
		// (set) Token: 0x0600004E RID: 78 RVA: 0x00003757 File Offset: 0x00001957
		public bool wantsMouseEnterLeaveWindow { readonly get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00003760 File Offset: 0x00001960
		// (set) Token: 0x06000050 RID: 80 RVA: 0x00003768 File Offset: 0x00001968
		public bool wantsLessLayoutEvents { readonly get; set; }

		// Token: 0x06000051 RID: 81 RVA: 0x00003774 File Offset: 0x00001974
		public bool WantsEvent(EventType type)
		{
			bool flag;
			if (type != EventType.MouseMove)
			{
				flag = type - EventType.MouseEnterWindow > 1 || this.wantsMouseEnterLeaveWindow;
			}
			else
			{
				flag = this.wantsMouseMove;
			}
			return flag;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000037AC File Offset: 0x000019AC
		public bool WantsLayoutPass(EventType type)
		{
			bool flag = !this.wantsLessLayoutEvents;
			bool flag2;
			if (flag)
			{
				flag2 = true;
			}
			else
			{
				switch (type)
				{
				case EventType.MouseDown:
				case EventType.MouseUp:
					return this.wantsMouseMove;
				case EventType.MouseMove:
				case EventType.MouseDrag:
				case EventType.ScrollWheel:
					goto IL_006C;
				case EventType.KeyDown:
				case EventType.KeyUp:
					return GUIUtility.textFieldInput;
				case EventType.Repaint:
					break;
				default:
					if (type != EventType.ExecuteCommand)
					{
						if (type - EventType.MouseEnterWindow > 1)
						{
							goto IL_006C;
						}
						return this.wantsMouseEnterLeaveWindow;
					}
					break;
				}
				return true;
				IL_006C:
				flag2 = false;
			}
			return flag2;
		}
	}
}
