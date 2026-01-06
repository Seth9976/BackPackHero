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
		// Token: 0x06000A99 RID: 2713 RVA: 0x00038E31 File Offset: 0x00037031
		public ExtendedPointerEventData(EventSystem eventSystem)
			: base(eventSystem)
		{
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000A9A RID: 2714 RVA: 0x00038E3A File Offset: 0x0003703A
		// (set) Token: 0x06000A9B RID: 2715 RVA: 0x00038E42 File Offset: 0x00037042
		public InputControl control { get; set; }

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000A9C RID: 2716 RVA: 0x00038E4B File Offset: 0x0003704B
		// (set) Token: 0x06000A9D RID: 2717 RVA: 0x00038E53 File Offset: 0x00037053
		public InputDevice device { get; set; }

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000A9E RID: 2718 RVA: 0x00038E5C File Offset: 0x0003705C
		// (set) Token: 0x06000A9F RID: 2719 RVA: 0x00038E64 File Offset: 0x00037064
		public int touchId { get; set; }

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000AA0 RID: 2720 RVA: 0x00038E6D File Offset: 0x0003706D
		// (set) Token: 0x06000AA1 RID: 2721 RVA: 0x00038E75 File Offset: 0x00037075
		public UIPointerType pointerType { get; set; }

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000AA2 RID: 2722 RVA: 0x00038E7E File Offset: 0x0003707E
		// (set) Token: 0x06000AA3 RID: 2723 RVA: 0x00038E86 File Offset: 0x00037086
		public int uiToolkitPointerId { get; set; }

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000AA4 RID: 2724 RVA: 0x00038E8F File Offset: 0x0003708F
		// (set) Token: 0x06000AA5 RID: 2725 RVA: 0x00038E97 File Offset: 0x00037097
		public Vector3 trackedDevicePosition { get; set; }

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000AA6 RID: 2726 RVA: 0x00038EA0 File Offset: 0x000370A0
		// (set) Token: 0x06000AA7 RID: 2727 RVA: 0x00038EA8 File Offset: 0x000370A8
		public Quaternion trackedDeviceOrientation { get; set; }

		// Token: 0x06000AA8 RID: 2728 RVA: 0x00038EB4 File Offset: 0x000370B4
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

		// Token: 0x06000AA9 RID: 2729 RVA: 0x000390B6 File Offset: 0x000372B6
		internal static int MakePointerIdForTouch(int deviceId, int touchId)
		{
			return (deviceId << 24) + touchId;
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x000390BE File Offset: 0x000372BE
		internal static int TouchIdFromPointerId(int pointerId)
		{
			return pointerId & 255;
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x000390C8 File Offset: 0x000372C8
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

		// Token: 0x06000AAC RID: 2732 RVA: 0x00039218 File Offset: 0x00037418
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

		// Token: 0x06000AAD RID: 2733 RVA: 0x00039298 File Offset: 0x00037498
		private static int GetTouchPointerId(TouchControl touchControl)
		{
			int num = ((Touchscreen)touchControl.device).touches.IndexOfReference(touchControl);
			return PointerId.touchPointerIdBase + Mathf.Clamp(num, 0, PointerId.touchPointerCount - 1);
		}
	}
}
