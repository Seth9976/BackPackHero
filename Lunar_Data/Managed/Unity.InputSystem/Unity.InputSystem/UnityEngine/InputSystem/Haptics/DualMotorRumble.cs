using System;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputSystem.Haptics
{
	// Token: 0x020000A8 RID: 168
	internal struct DualMotorRumble
	{
		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000C6B RID: 3179 RVA: 0x0004148D File Offset: 0x0003F68D
		// (set) Token: 0x06000C6C RID: 3180 RVA: 0x00041495 File Offset: 0x0003F695
		public float lowFrequencyMotorSpeed { readonly get; private set; }

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000C6D RID: 3181 RVA: 0x0004149E File Offset: 0x0003F69E
		// (set) Token: 0x06000C6E RID: 3182 RVA: 0x000414A6 File Offset: 0x0003F6A6
		public float highFrequencyMotorSpeed { readonly get; private set; }

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000C6F RID: 3183 RVA: 0x000414AF File Offset: 0x0003F6AF
		public bool isRumbling
		{
			get
			{
				return !Mathf.Approximately(this.lowFrequencyMotorSpeed, 0f) || !Mathf.Approximately(this.highFrequencyMotorSpeed, 0f);
			}
		}

		// Token: 0x06000C70 RID: 3184 RVA: 0x000414D8 File Offset: 0x0003F6D8
		public void PauseHaptics(InputDevice device)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			if (!this.isRumbling)
			{
				return;
			}
			DualMotorRumbleCommand dualMotorRumbleCommand = DualMotorRumbleCommand.Create(0f, 0f);
			device.ExecuteCommand<DualMotorRumbleCommand>(ref dualMotorRumbleCommand);
		}

		// Token: 0x06000C71 RID: 3185 RVA: 0x00041515 File Offset: 0x0003F715
		public void ResumeHaptics(InputDevice device)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			if (!this.isRumbling)
			{
				return;
			}
			this.SetMotorSpeeds(device, this.lowFrequencyMotorSpeed, this.highFrequencyMotorSpeed);
		}

		// Token: 0x06000C72 RID: 3186 RVA: 0x00041541 File Offset: 0x0003F741
		public void ResetHaptics(InputDevice device)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			if (!this.isRumbling)
			{
				return;
			}
			this.SetMotorSpeeds(device, 0f, 0f);
		}

		// Token: 0x06000C73 RID: 3187 RVA: 0x0004156C File Offset: 0x0003F76C
		public void SetMotorSpeeds(InputDevice device, float lowFrequency, float highFrequency)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			this.lowFrequencyMotorSpeed = Mathf.Clamp(lowFrequency, 0f, 1f);
			this.highFrequencyMotorSpeed = Mathf.Clamp(highFrequency, 0f, 1f);
			DualMotorRumbleCommand dualMotorRumbleCommand = DualMotorRumbleCommand.Create(this.lowFrequencyMotorSpeed, this.highFrequencyMotorSpeed);
			device.ExecuteCommand<DualMotorRumbleCommand>(ref dualMotorRumbleCommand);
		}
	}
}
