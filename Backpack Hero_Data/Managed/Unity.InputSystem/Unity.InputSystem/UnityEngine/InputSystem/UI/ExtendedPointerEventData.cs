using System;
using System.Text;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.UIElements;

namespace UnityEngine.InputSystem.UI
{
	// Token: 0x02000084 RID: 132
	public class ExtendedPointerEventData : PointerEventData
	{
		// Token: 0x06000A9B RID: 2715 RVA: 0x00038E6D File Offset: 0x0003706D
		public ExtendedPointerEventData(EventSystem eventSystem)
			: base(eventSystem)
		{
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000A9C RID: 2716 RVA: 0x00038E76 File Offset: 0x00037076
		// (set) Token: 0x06000A9D RID: 2717 RVA: 0x00038E7E File Offset: 0x0003707E
		public InputControl control { get; set; }

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000A9E RID: 2718 RVA: 0x00038E87 File Offset: 0x00037087
		// (set) Token: 0x06000A9F RID: 2719 RVA: 0x00038E8F File Offset: 0x0003708F
		public InputDevice device { get; set; }

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000AA0 RID: 2720 RVA: 0x00038E98 File Offset: 0x00037098
		// (set) Token: 0x06000AA1 RID: 2721 RVA: 0x00038EA0 File Offset: 0x000370A0
		public int touchId { get; set; }

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000AA2 RID: 2722 RVA: 0x00038EA9 File Offset: 0x000370A9
		// (set) Token: 0x06000AA3 RID: 2723 RVA: 0x00038EB1 File Offset: 0x000370B1
		public UIPointerType pointerType { get; set; }

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000AA4 RID: 2724 RVA: 0x00038EBA File Offset: 0x000370BA
		// (set) Token: 0x06000AA5 RID: 2725 RVA: 0x00038EC2 File Offset: 0x000370C2
		public int uiToolkitPointerId { get; set; }

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000AA6 RID: 2726 RVA: 0x00038ECB File Offset: 0x000370CB
		// (set) Token: 0x06000AA7 RID: 2727 RVA: 0x00038ED3 File Offset: 0x000370D3
		public Vector3 trackedDevicePosition { get; set; }

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000AA8 RID: 2728 RVA: 0x00038EDC File Offset: 0x000370DC
		// (set) Token: 0x06000AA9 RID: 2729 RVA: 0x00038EE4 File Offset: 0x000370E4
		public Quaternion trackedDeviceOrientation { get; set; }

		// Token: 0x06000AAA RID: 2730 RVA: 0x00038EF0 File Offset: 0x000370F0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(base.ToString());
			stringBuilder.AppendLine("button: " + base.button.ToString());
			stringBuilder.AppendLine("clickTime: " + base.clickTime.ToString());
			stringBuilder.AppendLine("clickCount: " + base.clickCount.ToString());
			string text = "device: ";
			InputDevice device = this.device;
			stringBuilder.AppendLine(text + ((device != null) ? device.ToString() : null));
			stringBuilder.AppendLine("pointerType: " + this.pointerType.ToString());
			stringBuilder.AppendLine("touchId: " + this.touchId.ToString());
			stringBuilder.AppendLine("pressPosition: " + base.pressPosition.ToString());
			stringBuilder.AppendLine("trackedDevicePosition: " + this.trackedDevicePosition.ToString());
			stringBuilder.AppendLine("trackedDeviceOrientation: " + this.trackedDeviceOrientation.ToString());
			stringBuilder.AppendLine("pressure" + base.pressure.ToString());
			stringBuilder.AppendLine("radius: " + base.radius.ToString());
			stringBuilder.AppendLine("azimuthAngle: " + base.azimuthAngle.ToString());
			stringBuilder.AppendLine("altitudeAngle: " + base.altitudeAngle.ToString());
			stringBuilder.AppendLine("twist: " + base.twist.ToString());
			return stringBuilder.ToString();
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x000390F2 File Offset: 0x000372F2
		internal static int MakePointerIdForTouch(int deviceId, int touchId)
		{
			return (deviceId << 24) + touchId;
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x000390FA File Offset: 0x000372FA
		internal static int TouchIdFromPointerId(int pointerId)
		{
			return pointerId & 255;
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x00039104 File Offset: 0x00037304
		internal unsafe void ReadDeviceState()
		{
			Pen pen = this.control.parent as Pen;
			if (pen != null)
			{
				this.uiToolkitPointerId = ExtendedPointerEventData.GetPenPointerId(pen);
				base.pressure = pen.pressure.magnitude;
				base.azimuthAngle = (pen.tilt.value.x + 1f) * 3.1415927f / 2f;
				base.altitudeAngle = (pen.tilt.value.y + 1f) * 3.1415927f / 2f;
				base.twist = *pen.twist.value * 3.1415927f * 2f;
				return;
			}
			TouchControl touchControl = this.control.parent as TouchControl;
			if (touchControl != null)
			{
				this.uiToolkitPointerId = ExtendedPointerEventData.GetTouchPointerId(touchControl);
				base.pressure = touchControl.pressure.magnitude;
				base.radius = *touchControl.radius.value;
				return;
			}
			Touchscreen touchscreen = this.control.parent as Touchscreen;
			if (touchscreen != null)
			{
				this.uiToolkitPointerId = ExtendedPointerEventData.GetTouchPointerId(touchscreen.primaryTouch);
				base.pressure = touchscreen.pressure.magnitude;
				base.radius = *touchscreen.radius.value;
				return;
			}
			this.uiToolkitPointerId = PointerId.mousePointerId;
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x00039254 File Offset: 0x00037454
		private static int GetPenPointerId(Pen pen)
		{
			int num = 0;
			foreach (InputDevice inputDevice in InputSystem.devices)
			{
				Pen pen2 = inputDevice as Pen;
				if (pen2 != null)
				{
					if (pen == pen2)
					{
						return PointerId.penPointerIdBase + Mathf.Min(num, PointerId.penPointerCount - 1);
					}
					num++;
				}
			}
			return PointerId.penPointerIdBase;
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x000392D4 File Offset: 0x000374D4
		private static int GetTouchPointerId(TouchControl touchControl)
		{
			int num = ((Touchscreen)touchControl.device).touches.IndexOfReference(touchControl);
			return PointerId.touchPointerIdBase + Mathf.Clamp(num, 0, PointerId.touchPointerCount - 1);
		}
	}
}
