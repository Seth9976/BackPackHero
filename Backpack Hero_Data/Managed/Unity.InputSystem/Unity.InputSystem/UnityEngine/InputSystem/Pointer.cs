using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputSystem
{
	// Token: 0x0200003F RID: 63
	[InputControlLayout(stateType = typeof(PointerState), isGenericTypeOfDevice = true)]
	public class Pointer : InputDevice, IInputStateCallbackReceiver
	{
		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x0600059D RID: 1437 RVA: 0x00014F20 File Offset: 0x00013120
		// (set) Token: 0x0600059E RID: 1438 RVA: 0x00014F28 File Offset: 0x00013128
		public Vector2Control position { get; protected set; }

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x0600059F RID: 1439 RVA: 0x00014F31 File Offset: 0x00013131
		// (set) Token: 0x060005A0 RID: 1440 RVA: 0x00014F39 File Offset: 0x00013139
		public DeltaControl delta { get; protected set; }

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x060005A1 RID: 1441 RVA: 0x00014F42 File Offset: 0x00013142
		// (set) Token: 0x060005A2 RID: 1442 RVA: 0x00014F4A File Offset: 0x0001314A
		public Vector2Control radius { get; protected set; }

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x060005A3 RID: 1443 RVA: 0x00014F53 File Offset: 0x00013153
		// (set) Token: 0x060005A4 RID: 1444 RVA: 0x00014F5B File Offset: 0x0001315B
		public AxisControl pressure { get; protected set; }

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x060005A5 RID: 1445 RVA: 0x00014F64 File Offset: 0x00013164
		// (set) Token: 0x060005A6 RID: 1446 RVA: 0x00014F6C File Offset: 0x0001316C
		public ButtonControl press { get; protected set; }

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x060005A7 RID: 1447 RVA: 0x00014F75 File Offset: 0x00013175
		// (set) Token: 0x060005A8 RID: 1448 RVA: 0x00014F7D File Offset: 0x0001317D
		public IntegerControl displayIndex { get; protected set; }

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x060005A9 RID: 1449 RVA: 0x00014F86 File Offset: 0x00013186
		// (set) Token: 0x060005AA RID: 1450 RVA: 0x00014F8D File Offset: 0x0001318D
		public static Pointer current { get; internal set; }

		// Token: 0x060005AB RID: 1451 RVA: 0x00014F95 File Offset: 0x00013195
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			Pointer.current = this;
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x00014FA3 File Offset: 0x000131A3
		protected override void OnRemoved()
		{
			base.OnRemoved();
			if (Pointer.current == this)
			{
				Pointer.current = null;
			}
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x00014FBC File Offset: 0x000131BC
		protected override void FinishSetup()
		{
			this.position = base.GetChildControl<Vector2Control>("position");
			this.delta = base.GetChildControl<DeltaControl>("delta");
			this.radius = base.GetChildControl<Vector2Control>("radius");
			this.pressure = base.GetChildControl<AxisControl>("pressure");
			this.press = base.GetChildControl<ButtonControl>("press");
			this.displayIndex = base.GetChildControl<IntegerControl>("displayIndex");
			base.FinishSetup();
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x00015038 File Offset: 0x00013238
		protected void OnNextUpdate()
		{
			InputState.Change<Vector2>(this.delta, Vector2.zero, InputUpdateType.None, default(InputEventPtr));
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x0001505F File Offset: 0x0001325F
		protected void OnStateEvent(InputEventPtr eventPtr)
		{
			this.delta.AccumulateValueInEvent(base.currentStatePtr, eventPtr);
			InputState.Change(this, eventPtr, InputUpdateType.None);
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x0001507B File Offset: 0x0001327B
		void IInputStateCallbackReceiver.OnNextUpdate()
		{
			this.OnNextUpdate();
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x00015083 File Offset: 0x00013283
		void IInputStateCallbackReceiver.OnStateEvent(InputEventPtr eventPtr)
		{
			this.OnStateEvent(eventPtr);
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x0001508C File Offset: 0x0001328C
		bool IInputStateCallbackReceiver.GetStateOffsetForEvent(InputControl control, InputEventPtr eventPtr, ref uint offset)
		{
			return false;
		}
	}
}
