using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x02000045 RID: 69
	public class KeyboardNavigationManipulator : Manipulator
	{
		// Token: 0x060001AE RID: 430 RVA: 0x000081B0 File Offset: 0x000063B0
		public KeyboardNavigationManipulator(Action<KeyboardNavigationOperation, EventBase> action)
		{
			this.m_Action = action;
		}

		// Token: 0x060001AF RID: 431 RVA: 0x000081C4 File Offset: 0x000063C4
		protected override void RegisterCallbacksOnTarget()
		{
			base.target.RegisterCallback<NavigationMoveEvent>(new EventCallback<NavigationMoveEvent>(this.OnNavigationMove), TrickleDown.NoTrickleDown);
			base.target.RegisterCallback<NavigationSubmitEvent>(new EventCallback<NavigationSubmitEvent>(this.OnNavigationSubmit), TrickleDown.NoTrickleDown);
			base.target.RegisterCallback<NavigationCancelEvent>(new EventCallback<NavigationCancelEvent>(this.OnNavigationCancel), TrickleDown.NoTrickleDown);
			base.target.RegisterCallback<KeyDownEvent>(new EventCallback<KeyDownEvent>(this.OnKeyDown), TrickleDown.NoTrickleDown);
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00008238 File Offset: 0x00006438
		protected override void UnregisterCallbacksFromTarget()
		{
			base.target.UnregisterCallback<NavigationMoveEvent>(new EventCallback<NavigationMoveEvent>(this.OnNavigationMove), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<NavigationSubmitEvent>(new EventCallback<NavigationSubmitEvent>(this.OnNavigationSubmit), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<NavigationCancelEvent>(new EventCallback<NavigationCancelEvent>(this.OnNavigationCancel), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<KeyDownEvent>(new EventCallback<KeyDownEvent>(this.OnKeyDown), TrickleDown.NoTrickleDown);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x000082AC File Offset: 0x000064AC
		internal void OnKeyDown(KeyDownEvent evt)
		{
			IPanel panel = base.target.panel;
			bool flag = panel != null && panel.contextType == ContextType.Editor;
			if (flag)
			{
				this.OnEditorKeyDown(evt);
			}
			else
			{
				this.OnRuntimeKeyDown(evt);
			}
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x000082EC File Offset: 0x000064EC
		private void OnRuntimeKeyDown(KeyDownEvent evt)
		{
			KeyboardNavigationManipulator.<>c__DisplayClass5_0 CS$<>8__locals1;
			CS$<>8__locals1.evt = evt;
			this.Invoke(KeyboardNavigationManipulator.<OnRuntimeKeyDown>g__GetOperation|5_0(ref CS$<>8__locals1), CS$<>8__locals1.evt);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00008318 File Offset: 0x00006518
		private void OnEditorKeyDown(KeyDownEvent evt)
		{
			KeyboardNavigationManipulator.<>c__DisplayClass6_0 CS$<>8__locals1;
			CS$<>8__locals1.evt = evt;
			this.Invoke(KeyboardNavigationManipulator.<OnEditorKeyDown>g__GetOperation|6_0(ref CS$<>8__locals1), CS$<>8__locals1.evt);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00008343 File Offset: 0x00006543
		private void OnNavigationCancel(NavigationCancelEvent evt)
		{
			this.Invoke(KeyboardNavigationOperation.Cancel, evt);
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0000834F File Offset: 0x0000654F
		private void OnNavigationSubmit(NavigationSubmitEvent evt)
		{
			this.Invoke(KeyboardNavigationOperation.Submit, evt);
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000835C File Offset: 0x0000655C
		private void OnNavigationMove(NavigationMoveEvent evt)
		{
			NavigationMoveEvent.Direction direction = evt.direction;
			NavigationMoveEvent.Direction direction2 = direction;
			if (direction2 != NavigationMoveEvent.Direction.Up)
			{
				if (direction2 == NavigationMoveEvent.Direction.Down)
				{
					this.Invoke(KeyboardNavigationOperation.Next, evt);
				}
			}
			else
			{
				this.Invoke(KeyboardNavigationOperation.Previous, evt);
			}
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00008398 File Offset: 0x00006598
		private void Invoke(KeyboardNavigationOperation operation, EventBase evt)
		{
			bool flag = operation == KeyboardNavigationOperation.None;
			if (!flag)
			{
				Action<KeyboardNavigationOperation, EventBase> action = this.m_Action;
				if (action != null)
				{
					action.Invoke(operation, evt);
				}
			}
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x000083C4 File Offset: 0x000065C4
		[CompilerGenerated]
		internal static KeyboardNavigationOperation <OnRuntimeKeyDown>g__GetOperation|5_0(ref KeyboardNavigationManipulator.<>c__DisplayClass5_0 A_0)
		{
			KeyCode keyCode = A_0.evt.keyCode;
			KeyCode keyCode2 = keyCode;
			if (keyCode2 != KeyCode.A)
			{
				switch (keyCode2)
				{
				case KeyCode.Home:
					return KeyboardNavigationOperation.Begin;
				case KeyCode.End:
					return KeyboardNavigationOperation.End;
				case KeyCode.PageUp:
					return KeyboardNavigationOperation.PageUp;
				case KeyCode.PageDown:
					return KeyboardNavigationOperation.PageDown;
				}
			}
			else if (A_0.evt.actionKey)
			{
				return KeyboardNavigationOperation.SelectAll;
			}
			return KeyboardNavigationOperation.None;
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00008430 File Offset: 0x00006630
		[CompilerGenerated]
		internal static KeyboardNavigationOperation <OnEditorKeyDown>g__GetOperation|6_0(ref KeyboardNavigationManipulator.<>c__DisplayClass6_0 A_0)
		{
			KeyCode keyCode = A_0.evt.keyCode;
			KeyCode keyCode2 = keyCode;
			if (keyCode2 <= KeyCode.Escape)
			{
				if (keyCode2 != KeyCode.Return)
				{
					if (keyCode2 != KeyCode.Escape)
					{
						goto IL_0097;
					}
					return KeyboardNavigationOperation.Cancel;
				}
			}
			else if (keyCode2 != KeyCode.A)
			{
				switch (keyCode2)
				{
				case KeyCode.KeypadEnter:
					break;
				case KeyCode.KeypadEquals:
				case KeyCode.RightArrow:
				case KeyCode.LeftArrow:
				case KeyCode.Insert:
					goto IL_0097;
				case KeyCode.UpArrow:
					return KeyboardNavigationOperation.Previous;
				case KeyCode.DownArrow:
					return KeyboardNavigationOperation.Next;
				case KeyCode.Home:
					return KeyboardNavigationOperation.Begin;
				case KeyCode.End:
					return KeyboardNavigationOperation.End;
				case KeyCode.PageUp:
					return KeyboardNavigationOperation.PageUp;
				case KeyCode.PageDown:
					return KeyboardNavigationOperation.PageDown;
				default:
					goto IL_0097;
				}
			}
			else
			{
				if (!A_0.evt.actionKey)
				{
					goto IL_0097;
				}
				return KeyboardNavigationOperation.SelectAll;
			}
			return KeyboardNavigationOperation.Submit;
			IL_0097:
			return KeyboardNavigationOperation.None;
		}

		// Token: 0x040000C9 RID: 201
		private readonly Action<KeyboardNavigationOperation, EventBase> m_Action;
	}
}
