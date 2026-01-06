using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputSystem
{
	// Token: 0x0200003C RID: 60
	[InputControlLayout(stateType = typeof(MouseState), isGenericTypeOfDevice = true)]
	public class Mouse : Pointer, IInputStateCallbackReceiver
	{
		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000568 RID: 1384 RVA: 0x00014B04 File Offset: 0x00012D04
		// (set) Token: 0x06000569 RID: 1385 RVA: 0x00014B0C File Offset: 0x00012D0C
		public DeltaControl scroll { get; protected set; }

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x0600056A RID: 1386 RVA: 0x00014B15 File Offset: 0x00012D15
		// (set) Token: 0x0600056B RID: 1387 RVA: 0x00014B1D File Offset: 0x00012D1D
		public ButtonControl leftButton { get; protected set; }

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x0600056C RID: 1388 RVA: 0x00014B26 File Offset: 0x00012D26
		// (set) Token: 0x0600056D RID: 1389 RVA: 0x00014B2E File Offset: 0x00012D2E
		public ButtonControl middleButton { get; protected set; }

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x0600056E RID: 1390 RVA: 0x00014B37 File Offset: 0x00012D37
		// (set) Token: 0x0600056F RID: 1391 RVA: 0x00014B3F File Offset: 0x00012D3F
		public ButtonControl rightButton { get; protected set; }

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000570 RID: 1392 RVA: 0x00014B48 File Offset: 0x00012D48
		// (set) Token: 0x06000571 RID: 1393 RVA: 0x00014B50 File Offset: 0x00012D50
		public ButtonControl backButton { get; protected set; }

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000572 RID: 1394 RVA: 0x00014B59 File Offset: 0x00012D59
		// (set) Token: 0x06000573 RID: 1395 RVA: 0x00014B61 File Offset: 0x00012D61
		public ButtonControl forwardButton { get; protected set; }

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000574 RID: 1396 RVA: 0x00014B6A File Offset: 0x00012D6A
		// (set) Token: 0x06000575 RID: 1397 RVA: 0x00014B72 File Offset: 0x00012D72
		public IntegerControl clickCount { get; protected set; }

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000576 RID: 1398 RVA: 0x00014B7B File Offset: 0x00012D7B
		// (set) Token: 0x06000577 RID: 1399 RVA: 0x00014B82 File Offset: 0x00012D82
		public new static Mouse current { get; private set; }

		// Token: 0x06000578 RID: 1400 RVA: 0x00014B8A File Offset: 0x00012D8A
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			Mouse.current = this;
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x00014B98 File Offset: 0x00012D98
		protected override void OnAdded()
		{
			base.OnAdded();
			if (base.native && Mouse.s_PlatformMouseDevice == null)
			{
				Mouse.s_PlatformMouseDevice = this;
			}
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x00014BB5 File Offset: 0x00012DB5
		protected override void OnRemoved()
		{
			base.OnRemoved();
			if (Mouse.current == this)
			{
				Mouse.current = null;
			}
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x00014BCC File Offset: 0x00012DCC
		public void WarpCursorPosition(Vector2 position)
		{
			WarpMousePositionCommand warpMousePositionCommand = WarpMousePositionCommand.Create(position);
			base.ExecuteCommand<WarpMousePositionCommand>(ref warpMousePositionCommand);
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x00014BEC File Offset: 0x00012DEC
		protected override void FinishSetup()
		{
			this.scroll = base.GetChildControl<DeltaControl>("scroll");
			this.leftButton = base.GetChildControl<ButtonControl>("leftButton");
			this.middleButton = base.GetChildControl<ButtonControl>("middleButton");
			this.rightButton = base.GetChildControl<ButtonControl>("rightButton");
			this.forwardButton = base.GetChildControl<ButtonControl>("forwardButton");
			this.backButton = base.GetChildControl<ButtonControl>("backButton");
			base.displayIndex = base.GetChildControl<IntegerControl>("displayIndex");
			this.clickCount = base.GetChildControl<IntegerControl>("clickCount");
			base.FinishSetup();
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x00014C88 File Offset: 0x00012E88
		protected new void OnNextUpdate()
		{
			base.OnNextUpdate();
			InputState.Change<Vector2>(this.scroll, Vector2.zero, InputUpdateType.None, default(InputEventPtr));
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x00014CB5 File Offset: 0x00012EB5
		protected new void OnStateEvent(InputEventPtr eventPtr)
		{
			this.scroll.AccumulateValueInEvent(base.currentStatePtr, eventPtr);
			base.OnStateEvent(eventPtr);
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x00014CD0 File Offset: 0x00012ED0
		void IInputStateCallbackReceiver.OnNextUpdate()
		{
			this.OnNextUpdate();
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x00014CD8 File Offset: 0x00012ED8
		void IInputStateCallbackReceiver.OnStateEvent(InputEventPtr eventPtr)
		{
			this.OnStateEvent(eventPtr);
		}

		// Token: 0x04000206 RID: 518
		internal static Mouse s_PlatformMouseDevice;
	}
}
