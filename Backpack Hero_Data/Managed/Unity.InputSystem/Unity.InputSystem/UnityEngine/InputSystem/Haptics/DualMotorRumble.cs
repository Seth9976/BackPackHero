using System;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputSystem.Haptics
{
	// Token: 0x020000A8 RID: 168
	internal struct DualMotorRumble
	{
		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000C6E RID: 3182 RVA: 0x000414D5 File Offset: 0x0003F6D5
		// (set) Token: 0x06000C6F RID: 3183 RVA: 0x000414DD File Offset: 0x0003F6DD
		public float lowFrequencyMotorSpeed { readonly get; private set; }

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000C70 RID: 3184 RVA: 0x000414E6 File Offset: 0x0003F6E6
		// (set) Token: 0x06000C71 RID: 3185 RVA: 0x000414EE File Offset: 0x0003F6EE
		public float highFrequencyMotorSpeed { readonly get; private set; }

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000C72 RID: 3186 RVA: 0x000414F7 File Offset: 0x0003F6F7
		public bool isRumbling
		{
			get
			{
				return !Mathf.Approximately(this.lowFrequencyMotorSpeed, 0f) || !Mathf.Approximately(this.highFrequencyMotorSpeed, 0f);
			}
		}

		// Token: 0x06000C73 RID: 3187 RVA: 0x00041520 File Offset: 0x0003F720
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

		// Token: 0x06000C74 RID: 3188 RVA: 0x0004155D File Offset: 0x0003F75D
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

		// Token: 0x06000C75 RID: 3189 RVA: 0x00041589 File Offset: 0x0003F789
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

		// Token: 0x06000C76 RID: 3190 RVA: 0x000415B4 File Offset: 0x0003F7B4
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
