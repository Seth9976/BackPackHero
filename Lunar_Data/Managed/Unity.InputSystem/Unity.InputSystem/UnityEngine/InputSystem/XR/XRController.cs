using System;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.XR;

namespace UnityEngine.InputSystem.XR
{
	// Token: 0x02000063 RID: 99
	[InputControlLayout(commonUsages = new string[] { "LeftHand", "RightHand" }, isGenericTypeOfDevice = true, displayName = "XR Controller")]
	public class XRController : TrackedDevice
	{
		// Token: 0x1700026B RID: 619
		// (get) Token: 0x0600098E RID: 2446 RVA: 0x00034EC1 File Offset: 0x000330C1
		public static XRController leftHand
		{
			get
			{
				return InputSystem.GetDevice<XRController>(CommonUsages.LeftHand);
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x0600098F RID: 2447 RVA: 0x00034ECD File Offset: 0x000330CD
		public static XRController rightHand
		{
			get
			{
				return InputSystem.GetDevice<XRController>(CommonUsages.RightHand);
			}
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x00034EDC File Offset: 0x000330DC
		protected override void FinishSetup()
		{
			base.FinishSetup();
			XRDeviceDescriptor xrdeviceDescriptor = XRDeviceDescriptor.FromJson(base.description.capabilities);
			if (xrdeviceDescriptor != null)
			{
				if ((xrdeviceDescriptor.characteristics & InputDeviceCharacteristics.Left) != InputDeviceCharacteristics.None)
				{
					InputSystem.SetDeviceUsage(this, CommonUsages.LeftHand);
					return;
				}
				if ((xrdeviceDescriptor.characteristics & InputDeviceCharacteristics.Right) != InputDeviceCharacteristics.None)
				{
					InputSystem.SetDeviceUsage(this, CommonUsages.RightHand);
				}
			}
		}
	}
}
