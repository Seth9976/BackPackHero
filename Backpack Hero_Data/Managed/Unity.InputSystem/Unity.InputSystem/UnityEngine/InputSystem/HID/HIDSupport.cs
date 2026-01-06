using System;
using System.Linq;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.HID
{
	// Token: 0x02000095 RID: 149
	public static class HIDSupport
	{
		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000BB4 RID: 2996 RVA: 0x0003ED60 File Offset: 0x0003CF60
		// (set) Token: 0x06000BB5 RID: 2997 RVA: 0x0003ED6C File Offset: 0x0003CF6C
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

		// Token: 0x06000BB6 RID: 2998 RVA: 0x0003EDF8 File Offset: 0x0003CFF8
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
			// Token: 0x06001458 RID: 5208 RVA: 0x0005EB29 File Offset: 0x0005CD29
			public HIDPageUsage(HID.UsagePage page, int usage)
			{
				this.page = page;
				this.usage = usage;
			}

			// Token: 0x06001459 RID: 5209 RVA: 0x0005EB39 File Offset: 0x0005CD39
			public HIDPageUsage(HID.GenericDesktop usage)
			{
				this.page = HID.UsagePage.GenericDesktop;
				this.usage = (int)usage;
			}

			// Token: 0x04000ABB RID: 2747
			public HID.UsagePage page;

			// Token: 0x04000ABC RID: 2748
			public int usage;
		}
	}
}
