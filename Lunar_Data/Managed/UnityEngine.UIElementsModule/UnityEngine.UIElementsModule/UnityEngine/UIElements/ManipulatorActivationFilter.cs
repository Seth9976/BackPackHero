using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000048 RID: 72
	public struct ManipulatorActivationFilter : IEquatable<ManipulatorActivationFilter>
	{
		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060001BA RID: 442 RVA: 0x000084D9 File Offset: 0x000066D9
		// (set) Token: 0x060001BB RID: 443 RVA: 0x000084E1 File Offset: 0x000066E1
		public MouseButton button { readonly get; set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060001BC RID: 444 RVA: 0x000084EA File Offset: 0x000066EA
		// (set) Token: 0x060001BD RID: 445 RVA: 0x000084F2 File Offset: 0x000066F2
		public EventModifiers modifiers { readonly get; set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060001BE RID: 446 RVA: 0x000084FB File Offset: 0x000066FB
		// (set) Token: 0x060001BF RID: 447 RVA: 0x00008503 File Offset: 0x00006703
		public int clickCount { readonly get; set; }

		// Token: 0x060001C0 RID: 448 RVA: 0x0000850C File Offset: 0x0000670C
		public override bool Equals(object obj)
		{
			return obj is ManipulatorActivationFilter && this.Equals((ManipulatorActivationFilter)obj);
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00008538 File Offset: 0x00006738
		public bool Equals(ManipulatorActivationFilter other)
		{
			return this.button == other.button && this.modifiers == other.modifiers && this.clickCount == other.clickCount;
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0000857C File Offset: 0x0000677C
		public override int GetHashCode()
		{
			int num = 390957112;
			num = num * -1521134295 + this.button.GetHashCode();
			num = num * -1521134295 + this.modifiers.GetHashCode();
			return num * -1521134295 + this.clickCount.GetHashCode();
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x000085E8 File Offset: 0x000067E8
		public bool Matches(IMouseEvent e)
		{
			bool flag = e == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = this.clickCount == 0 || e.clickCount >= this.clickCount;
				flag2 = this.button == (MouseButton)e.button && this.HasModifiers(e) && flag3;
			}
			return flag2;
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00008640 File Offset: 0x00006840
		private bool HasModifiers(IMouseEvent e)
		{
			bool flag = e == null;
			return !flag && this.MatchModifiers(e.altKey, e.ctrlKey, e.shiftKey, e.commandKey);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0000867C File Offset: 0x0000687C
		public bool Matches(IPointerEvent e)
		{
			bool flag = e == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = this.clickCount == 0 || e.clickCount >= this.clickCount;
				flag2 = this.button == (MouseButton)e.button && this.HasModifiers(e) && flag3;
			}
			return flag2;
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x000086D4 File Offset: 0x000068D4
		private bool HasModifiers(IPointerEvent e)
		{
			bool flag = e == null;
			return !flag && this.MatchModifiers(e.altKey, e.ctrlKey, e.shiftKey, e.commandKey);
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00008710 File Offset: 0x00006910
		private bool MatchModifiers(bool alt, bool ctrl, bool shift, bool command)
		{
			bool flag = ((this.modifiers & EventModifiers.Alt) != EventModifiers.None && !alt) || ((this.modifiers & EventModifiers.Alt) == EventModifiers.None && alt);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = ((this.modifiers & EventModifiers.Control) != EventModifiers.None && !ctrl) || ((this.modifiers & EventModifiers.Control) == EventModifiers.None && ctrl);
				if (flag3)
				{
					flag2 = false;
				}
				else
				{
					bool flag4 = ((this.modifiers & EventModifiers.Shift) != EventModifiers.None && !shift) || ((this.modifiers & EventModifiers.Shift) == EventModifiers.None && shift);
					flag2 = !flag4 && ((this.modifiers & EventModifiers.Command) == EventModifiers.None || command) && ((this.modifiers & EventModifiers.Command) != EventModifiers.None || !command);
				}
			}
			return flag2;
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x000087BC File Offset: 0x000069BC
		public static bool operator ==(ManipulatorActivationFilter filter1, ManipulatorActivationFilter filter2)
		{
			return filter1.Equals(filter2);
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x000087D8 File Offset: 0x000069D8
		public static bool operator !=(ManipulatorActivationFilter filter1, ManipulatorActivationFilter filter2)
		{
			return !(filter1 == filter2);
		}
	}
}
