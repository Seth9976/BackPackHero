using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200001E RID: 30
	public class DropdownMenuEventInfo
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00004ECB File Offset: 0x000030CB
		public EventModifiers modifiers { get; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00004ED3 File Offset: 0x000030D3
		public Vector2 mousePosition { get; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00004EDB File Offset: 0x000030DB
		public Vector2 localMousePosition { get; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00004EE3 File Offset: 0x000030E3
		private char character { get; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00004EEB File Offset: 0x000030EB
		private KeyCode keyCode { get; }

		// Token: 0x060000D2 RID: 210 RVA: 0x00004EF4 File Offset: 0x000030F4
		public DropdownMenuEventInfo(EventBase e)
		{
			IMouseEvent mouseEvent = e as IMouseEvent;
			bool flag = mouseEvent != null;
			if (flag)
			{
				this.mousePosition = mouseEvent.mousePosition;
				this.localMousePosition = mouseEvent.localMousePosition;
				this.modifiers = mouseEvent.modifiers;
				this.character = '\0';
				this.keyCode = KeyCode.None;
			}
			else
			{
				IKeyboardEvent keyboardEvent = e as IKeyboardEvent;
				bool flag2 = keyboardEvent != null;
				if (flag2)
				{
					this.character = keyboardEvent.character;
					this.keyCode = keyboardEvent.keyCode;
					this.modifiers = keyboardEvent.modifiers;
					this.mousePosition = Vector2.zero;
					this.localMousePosition = Vector2.zero;
				}
			}
		}
	}
}
