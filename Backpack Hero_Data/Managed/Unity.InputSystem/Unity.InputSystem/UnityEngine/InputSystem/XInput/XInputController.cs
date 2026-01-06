using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;

namespace UnityEngine.InputSystem.XInput
{
	// Token: 0x02000079 RID: 121
	[InputControlLayout(displayName = "Xbox Controller")]
	public class XInputController : Gamepad
	{
		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000A22 RID: 2594 RVA: 0x000369E5 File Offset: 0x00034BE5
		// (set) Token: 0x06000A23 RID: 2595 RVA: 0x000369ED File Offset: 0x00034BED
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

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000A24 RID: 2596 RVA: 0x000369F6 File Offset: 0x00034BF6
		// (set) Token: 0x06000A25 RID: 2597 RVA: 0x000369FE File Offset: 0x00034BFE
		public ButtonControl view { get; private set; }

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000A26 RID: 2598 RVA: 0x00036A07 File Offset: 0x00034C07
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

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000A27 RID: 2599 RVA: 0x00036A1D File Offset: 0x00034C1D
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

		// Token: 0x06000A28 RID: 2600 RVA: 0x00036A33 File Offset: 0x00034C33
		protected override void FinishSetup()
		{
			base.FinishSetup();
			this.menu = base.startButton;
			this.view = base.selectButton;
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x00036A54 File Offset: 0x00034C54
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
			// Token: 0x040008F5 RID: 2293
			Gamepad
		}

		// Token: 0x020001BE RID: 446
		public enum DeviceSubType
		{
			// Token: 0x040008F7 RID: 2295
			Unknown,
			// Token: 0x040008F8 RID: 2296
			Gamepad,
			// Token: 0x040008F9 RID: 2297
			Wheel,
			// Token: 0x040008FA RID: 2298
			ArcadeStick,
			// Token: 0x040008FB RID: 2299
			FlightStick,
			// Token: 0x040008FC RID: 2300
			DancePad,
			// Token: 0x040008FD RID: 2301
			Guitar,
			// Token: 0x040008FE RID: 2302
			GuitarAlternate,
			// Token: 0x040008FF RID: 2303
			DrumKit,
			// Token: 0x04000900 RID: 2304
			GuitarBass = 11,
			// Token: 0x04000901 RID: 2305
			ArcadePad = 19
		}

		// Token: 0x020001BF RID: 447
		[Flags]
		public new enum DeviceFlags
		{
			// Token: 0x04000903 RID: 2307
			ForceFeedbackSupported = 1,
			// Token: 0x04000904 RID: 2308
			Wireless = 2,
			// Token: 0x04000905 RID: 2309
			VoiceSupported = 4,
			// Token: 0x04000906 RID: 2310
			PluginModulesSupported = 8,
			// Token: 0x04000907 RID: 2311
			NoNavigation = 16
		}

		// Token: 0x020001C0 RID: 448
		[Serializable]
		internal struct Capabilities
		{
			// Token: 0x04000908 RID: 2312
			public XInputController.DeviceType type;

			// Token: 0x04000909 RID: 2313
			public XInputController.DeviceSubType subType;

			// Token: 0x0400090A RID: 2314
			public XInputController.DeviceFlags flags;
		}
	}
}
