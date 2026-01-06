using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;

namespace UnityEngine.InputSystem.XInput
{
	// Token: 0x02000079 RID: 121
	[InputControlLayout(displayName = "Xbox Controller")]
	public class XInputController : Gamepad
	{
		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000A20 RID: 2592 RVA: 0x000369A9 File Offset: 0x00034BA9
		// (set) Token: 0x06000A21 RID: 2593 RVA: 0x000369B1 File Offset: 0x00034BB1
		[InputControl(name = "buttonSouth", displayName = "A")]
		[InputControl(name = "buttonEast", displayName = "B")]
		[InputControl(name = "buttonWest", displayName = "X")]
		[InputControl(name = "buttonNorth", displayName = "Y")]
		[InputControl(name = "leftShoulder", displayName = "Left Bumper", shortDisplayName = "LB")]
		[InputControl(name = "rightShoulder", displayName = "Right Bumper", shortDisplayName = "RB")]
		[InputControl(name = "leftTrigger", shortDisplayName = "LT")]
		[InputControl(name = "rightTrigger", shortDisplayName = "RT")]
		[InputControl(name = "start", displayName = "Menu", alias = "menu")]
		[InputControl(name = "select", displayName = "View", alias = "view")]
		public ButtonControl menu { get; private set; }

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000A22 RID: 2594 RVA: 0x000369BA File Offset: 0x00034BBA
		// (set) Token: 0x06000A23 RID: 2595 RVA: 0x000369C2 File Offset: 0x00034BC2
		public ButtonControl view { get; private set; }

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000A24 RID: 2596 RVA: 0x000369CB File Offset: 0x00034BCB
		public XInputController.DeviceSubType subType
		{
			get
			{
				if (!this.m_HaveParsedCapabilities)
				{
					this.ParseCapabilities();
				}
				return this.m_SubType;
			}
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000A25 RID: 2597 RVA: 0x000369E1 File Offset: 0x00034BE1
		public XInputController.DeviceFlags flags
		{
			get
			{
				if (!this.m_HaveParsedCapabilities)
				{
					this.ParseCapabilities();
				}
				return this.m_Flags;
			}
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x000369F7 File Offset: 0x00034BF7
		protected override void FinishSetup()
		{
			base.FinishSetup();
			this.menu = base.startButton;
			this.view = base.selectButton;
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x00036A18 File Offset: 0x00034C18
		private void ParseCapabilities()
		{
			if (!string.IsNullOrEmpty(base.description.capabilities))
			{
				XInputController.Capabilities capabilities = JsonUtility.FromJson<XInputController.Capabilities>(base.description.capabilities);
				this.m_SubType = capabilities.subType;
				this.m_Flags = capabilities.flags;
			}
			this.m_HaveParsedCapabilities = true;
		}

		// Token: 0x04000375 RID: 885
		private bool m_HaveParsedCapabilities;

		// Token: 0x04000376 RID: 886
		private XInputController.DeviceSubType m_SubType;

		// Token: 0x04000377 RID: 887
		private XInputController.DeviceFlags m_Flags;

		// Token: 0x020001BD RID: 445
		internal enum DeviceType
		{
			// Token: 0x040008F4 RID: 2292
			Gamepad
		}

		// Token: 0x020001BE RID: 446
		public enum DeviceSubType
		{
			// Token: 0x040008F6 RID: 2294
			Unknown,
			// Token: 0x040008F7 RID: 2295
			Gamepad,
			// Token: 0x040008F8 RID: 2296
			Wheel,
			// Token: 0x040008F9 RID: 2297
			ArcadeStick,
			// Token: 0x040008FA RID: 2298
			FlightStick,
			// Token: 0x040008FB RID: 2299
			DancePad,
			// Token: 0x040008FC RID: 2300
			Guitar,
			// Token: 0x040008FD RID: 2301
			GuitarAlternate,
			// Token: 0x040008FE RID: 2302
			DrumKit,
			// Token: 0x040008FF RID: 2303
			GuitarBass = 11,
			// Token: 0x04000900 RID: 2304
			ArcadePad = 19
		}

		// Token: 0x020001BF RID: 447
		[Flags]
		public new enum DeviceFlags
		{
			// Token: 0x04000902 RID: 2306
			ForceFeedbackSupported = 1,
			// Token: 0x04000903 RID: 2307
			Wireless = 2,
			// Token: 0x04000904 RID: 2308
			VoiceSupported = 4,
			// Token: 0x04000905 RID: 2309
			PluginModulesSupported = 8,
			// Token: 0x04000906 RID: 2310
			NoNavigation = 16
		}

		// Token: 0x020001C0 RID: 448
		[Serializable]
		internal struct Capabilities
		{
			// Token: 0x04000907 RID: 2311
			public XInputController.DeviceType type;

			// Token: 0x04000908 RID: 2312
			public XInputController.DeviceSubType subType;

			// Token: 0x04000909 RID: 2313
			public XInputController.DeviceFlags flags;
		}
	}
}
