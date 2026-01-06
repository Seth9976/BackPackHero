using System;
using System.ComponentModel;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputSystem
{
	// Token: 0x0200003E RID: 62
	[InputControlLayout(stateType = typeof(PenState), isGenericTypeOfDevice = true)]
	public class Pen : Pointer
	{
		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000582 RID: 1410 RVA: 0x00014CE9 File Offset: 0x00012EE9
		// (set) Token: 0x06000583 RID: 1411 RVA: 0x00014CF1 File Offset: 0x00012EF1
		public ButtonControl tip { get; protected set; }

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000584 RID: 1412 RVA: 0x00014CFA File Offset: 0x00012EFA
		// (set) Token: 0x06000585 RID: 1413 RVA: 0x00014D02 File Offset: 0x00012F02
		public ButtonControl eraser { get; protected set; }

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000586 RID: 1414 RVA: 0x00014D0B File Offset: 0x00012F0B
		// (set) Token: 0x06000587 RID: 1415 RVA: 0x00014D13 File Offset: 0x00012F13
		public ButtonControl firstBarrelButton { get; protected set; }

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000588 RID: 1416 RVA: 0x00014D1C File Offset: 0x00012F1C
		// (set) Token: 0x06000589 RID: 1417 RVA: 0x00014D24 File Offset: 0x00012F24
		public ButtonControl secondBarrelButton { get; protected set; }

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x0600058A RID: 1418 RVA: 0x00014D2D File Offset: 0x00012F2D
		// (set) Token: 0x0600058B RID: 1419 RVA: 0x00014D35 File Offset: 0x00012F35
		public ButtonControl thirdBarrelButton { get; protected set; }

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x0600058C RID: 1420 RVA: 0x00014D3E File Offset: 0x00012F3E
		// (set) Token: 0x0600058D RID: 1421 RVA: 0x00014D46 File Offset: 0x00012F46
		public ButtonControl fourthBarrelButton { get; protected set; }

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x0600058E RID: 1422 RVA: 0x00014D4F File Offset: 0x00012F4F
		// (set) Token: 0x0600058F RID: 1423 RVA: 0x00014D57 File Offset: 0x00012F57
		public ButtonControl inRange { get; protected set; }

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000590 RID: 1424 RVA: 0x00014D60 File Offset: 0x00012F60
		// (set) Token: 0x06000591 RID: 1425 RVA: 0x00014D68 File Offset: 0x00012F68
		public Vector2Control tilt { get; protected set; }

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000592 RID: 1426 RVA: 0x00014D71 File Offset: 0x00012F71
		// (set) Token: 0x06000593 RID: 1427 RVA: 0x00014D79 File Offset: 0x00012F79
		public AxisControl twist { get; protected set; }

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000594 RID: 1428 RVA: 0x00014D82 File Offset: 0x00012F82
		// (set) Token: 0x06000595 RID: 1429 RVA: 0x00014D89 File Offset: 0x00012F89
		public new static Pen current { get; internal set; }

		// Token: 0x170001E1 RID: 481
		public ButtonControl this[PenButton button]
		{
			get
			{
				switch (button)
				{
				case PenButton.Tip:
					return this.tip;
				case PenButton.Eraser:
					return this.eraser;
				case PenButton.BarrelFirst:
					return this.firstBarrelButton;
				case PenButton.BarrelSecond:
					return this.secondBarrelButton;
				case PenButton.InRange:
					return this.inRange;
				case PenButton.BarrelThird:
					return this.thirdBarrelButton;
				case PenButton.BarrelFourth:
					return this.fourthBarrelButton;
				default:
					throw new InvalidEnumArgumentException("button", (int)button, typeof(PenButton));
				}
			}
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x00014E0B File Offset: 0x0001300B
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			Pen.current = this;
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x00014E19 File Offset: 0x00013019
		protected override void OnRemoved()
		{
			base.OnRemoved();
			if (Pen.current == this)
			{
				Pen.current = null;
			}
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x00014E30 File Offset: 0x00013030
		protected override void FinishSetup()
		{
			this.tip = base.GetChildControl<ButtonControl>("tip");
			this.eraser = base.GetChildControl<ButtonControl>("eraser");
			this.firstBarrelButton = base.GetChildControl<ButtonControl>("barrel1");
			this.secondBarrelButton = base.GetChildControl<ButtonControl>("barrel2");
			this.thirdBarrelButton = base.GetChildControl<ButtonControl>("barrel3");
			this.fourthBarrelButton = base.GetChildControl<ButtonControl>("barrel4");
			this.inRange = base.GetChildControl<ButtonControl>("inRange");
			this.tilt = base.GetChildControl<Vector2Control>("tilt");
			this.twist = base.GetChildControl<AxisControl>("twist");
			base.FinishSetup();
		}
	}
}
