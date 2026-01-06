using System;
using System.Linq;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.HID
{
	// Token: 0x02000095 RID: 149
	public static class HIDSupport
	{
		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000BB2 RID: 2994 RVA: 0x0003ED24 File Offset: 0x0003CF24
		// (set) Token: 0x06000BB3 RID: 2995 RVA: 0x0003ED30 File Offset: 0x0003CF30
		public static ReadOnlyArray<HIDSupport.HIDPageUsage> supportedHIDUsages
		{
			get
			{
				return HIDSupport.s_SupportedHIDUsages;
			}
			set
			{
				HIDSupport.s_SupportedHIDUsages = value.ToArray();
				InputSystem.s_Manager.AddAvailableDevicesThatAreNowRecognized();
				for (int i = 0; i < InputSystem.devices.Count; i++)
				{
					InputDevice inputDevice = InputSystem.devices[i];
					HID hid = inputDevice as HID;
					if (hid != null && !HIDSupport.s_SupportedHIDUsages.Contains(new HIDSupport.HIDPageUsage(hid.hidDescriptor.usagePage, hid.hidDescriptor.usage)))
					{
						InputSystem.RemoveLayout(inputDevice.layout);
						i--;
					}
				}
			}
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x0003EDBC File Offset: 0x0003CFBC
		internal static void Initialize()
		{
			HIDSupport.s_SupportedHIDUsages = new HIDSupport.HIDPageUsage[]
			{
				new HIDSupport.HIDPageUsage(HID.GenericDesktop.Joystick),
				new HIDSupport.HIDPageUsage(HID.GenericDesktop.Gamepad),
				new HIDSupport.HIDPageUsage(HID.GenericDesktop.MultiAxisController)
			};
			InputSystem.RegisterLayout<HID>(null, null);
			InputSystem.onFindLayoutForDevice += HID.OnFindLayoutForDevice;
		}

		// Token: 0x04000430 RID: 1072
		private static HIDSupport.HIDPageUsage[] s_SupportedHIDUsages;

		// Token: 0x020001EA RID: 490
		public struct HIDPageUsage
		{
			// Token: 0x06001451 RID: 5201 RVA: 0x0005E915 File Offset: 0x0005CB15
			public HIDPageUsage(HID.UsagePage page, int usage)
			{
				this.page = page;
				this.usage = usage;
			}

			// Token: 0x06001452 RID: 5202 RVA: 0x0005E925 File Offset: 0x0005CB25
			public HIDPageUsage(HID.GenericDesktop usage)
			{
				this.page = HID.UsagePage.GenericDesktop;
				this.usage = (int)usage;
			}

			// Token: 0x04000ABA RID: 2746
			public HID.UsagePage page;

			// Token: 0x04000ABB RID: 2747
			public int usage;
		}
	}
}
