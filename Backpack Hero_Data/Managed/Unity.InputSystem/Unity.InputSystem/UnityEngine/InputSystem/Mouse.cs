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
		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x0600056A RID: 1386 RVA: 0x00014B40 File Offset: 0x00012D40
		// (set) Token: 0x0600056B RID: 1387 RVA: 0x00014B48 File Offset: 0x00012D48
		public DeltaControl scroll { get; protected set; }

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x0600056C RID: 1388 RVA: 0x00014B51 File Offset: 0x00012D51
		// (set) Token: 0x0600056D RID: 1389 RVA: 0x00014B59 File Offset: 0x00012D59
		public ButtonControl leftButton { get; protected set; }

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x0600056E RID: 1390 RVA: 0x00014B62 File Offset: 0x00012D62
		// (set) Token: 0x0600056F RID: 1391 RVA: 0x00014B6A File Offset: 0x00012D6A
		public ButtonControl middleButton { get; protected set; }

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000570 RID: 1392 RVA: 0x00014B73 File Offset: 0x00012D73
		// (set) Token: 0x06000571 RID: 1393 RVA: 0x00014B7B File Offset: 0x00012D7B
		public ButtonControl rightButton { get; protected set; }

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000572 RID: 1394 RVA: 0x00014B84 File Offset: 0x00012D84
		// (set) Token: 0x06000573 RID: 1395 RVA: 0x00014B8C File Offset: 0x00012D8C
		public ButtonControl backButton { get; protected set; }

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000574 RID: 1396 RVA: 0x00014B95 File Offset: 0x00012D95
		// (set) Token: 0x06000575 RID: 1397 RVA: 0x00014B9D File Offset: 0x00012D9D
		public ButtonControl forwardButton { get; protected set; }

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000576 RID: 1398 RVA: 0x00014BA6 File Offset: 0x00012DA6
		// (set) Token: 0x06000577 RID: 1399 RVA: 0x00014BAE File Offset: 0x00012DAE
		public IntegerControl clickCount { get; protected set; }

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000578 RID: 1400 RVA: 0x00014BB7 File Offset: 0x00012DB7
		// (set) Token: 0x06000579 RID: 1401 RVA: 0x00014BBE File Offset: 0x00012DBE
		public new static Mouse current { get; private set; }

		// Token: 0x0600057A RID: 1402 RVA: 0x00014BC6 File Offset: 0x00012DC6
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			Mouse.current = this;
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x00014BD4 File Offset: 0x00012DD4
		protected override void OnAdded()
		{
			base.OnAdded();
			if (base.native && Mouse.s_PlatformMouseDevice == null)
			{
				Mouse.s_PlatformMouseDevice = this;
			}
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x00014BF1 File Offset: 0x00012DF1
		protected override void OnRemoved()
		{
			base.OnRemoved();
			if (Mouse.current == this)
			{
				Mouse.current = null;
			}
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x00014C08 File Offset: 0x00012E08
		public void WarpCursorPosition(Vector2 position)
		{
			WarpMousePositionCommand warpMousePositionCommand = WarpMousePositionCommand.Create(position);
			base.ExecuteCommand<WarpMousePositionCommand>(ref warpMousePositionCommand);
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x00014C28 File Offset: 0x00012E28
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

		// Token: 0x0600057F RID: 1407 RVA: 0x00014CC4 File Offset: 0x00012EC4
		protected new void OnNextUpdate()
		{
			base.OnNextUpdate();
			InputState.Change<Vector2>(this.scroll, Vector2.zero, InputUpdateType.None, default(InputEventPtr));
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x00014CF1 File Offset: 0x00012EF1
		protected new void OnStateEvent(InputEventPtr eventPtr)
		{
			this.scroll.AccumulateValueInEvent(base.currentStatePtr, eventPtr);
			base.OnStateEvent(eventPtr);
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x00014D0C File Offset: 0x00012F0C
		void IInputStateCallbackReceiver.OnNextUpdate()
		{
			this.OnNextUpdate();
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x00014D14 File Offset: 0x00012F14
		void IInputStateCallbackReceiver.OnStateEvent(InputEventPtr eventPtr)
		{
			this.OnStateEvent(eventPtr);
		}

		// Token: 0x04000206 RID: 518
		internal static Mouse s_PlatformMouseDevice;
	}
}
