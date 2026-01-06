using System;

namespace UnityEngine.EventSystems
{
	// Token: 0x02000068 RID: 104
	public class BaseInput : UIBehaviour
	{
		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x060005EF RID: 1519 RVA: 0x000192C3 File Offset: 0x000174C3
		public virtual string compositionString
		{
			get
			{
				return Input.compositionString;
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x060005F0 RID: 1520 RVA: 0x000192CA File Offset: 0x000174CA
		// (set) Token: 0x060005F1 RID: 1521 RVA: 0x000192D1 File Offset: 0x000174D1
		public virtual IMECompositionMode imeCompositionMode
		{
			get
			{
				return Input.imeCompositionMode;
			}
			set
			{
				Input.imeCompositionMode = value;
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x060005F2 RID: 1522 RVA: 0x000192D9 File Offset: 0x000174D9
		// (set) Token: 0x060005F3 RID: 1523 RVA: 0x000192E0 File Offset: 0x000174E0
		public virtual Vector2 compositionCursorPos
		{
			get
			{
				return Input.compositionCursorPos;
			}
			set
			{
				Input.compositionCursorPos = value;
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x060005F4 RID: 1524 RVA: 0x000192E8 File Offset: 0x000174E8
		public virtual bool mousePresent
		{
			get
			{
				return Input.mousePresent;
			}
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x000192EF File Offset: 0x000174EF
		public virtual bool GetMouseButtonDown(int button)
		{
			return Input.GetMouseButtonDown(button);
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x000192F7 File Offset: 0x000174F7
		public virtual bool GetMouseButtonUp(int button)
		{
			return Input.GetMouseButtonUp(button);
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x000192FF File Offset: 0x000174FF
		public virtual bool GetMouseButton(int button)
		{
			return Input.GetMouseButton(button);
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x060005F8 RID: 1528 RVA: 0x00019307 File Offset: 0x00017507
		public virtual Vector2 mousePosition
		{
			get
			{
				return Input.mousePosition;
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x060005F9 RID: 1529 RVA: 0x00019313 File Offset: 0x00017513
		public virtual Vector2 mouseScrollDelta
		{
			get
			{
				return Input.mouseScrollDelta;
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060005FA RID: 1530 RVA: 0x0001931A File Offset: 0x0001751A
		public virtual bool touchSupported
		{
			get
			{
				return Input.touchSupported;
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060005FB RID: 1531 RVA: 0x00019321 File Offset: 0x00017521
		public virtual int touchCount
		{
			get
			{
				return Input.touchCount;
			}
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x00019328 File Offset: 0x00017528
		public virtual Touch GetTouch(int index)
		{
			return Input.GetTouch(index);
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x00019330 File Offset: 0x00017530
		public virtual float GetAxisRaw(string axisName)
		{
			return Input.GetAxisRaw(axisName);
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x00019338 File Offset: 0x00017538
		public virtual bool GetButtonDown(string buttonName)
		{
			return Input.GetButtonDown(buttonName);
		}
	}
}
