using System;

namespace UnityEngine.InputSystem.Processors
{
	// Token: 0x020000F8 RID: 248
	public class AxisDeadzoneProcessor : InputProcessor<float>
	{
		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06000E90 RID: 3728 RVA: 0x0004742D File Offset: 0x0004562D
		private float minOrDefault
		{
			get
			{
				if (this.min != 0f)
				{
					return this.min;
				}
				return InputSystem.settings.defaultDeadzoneMin;
			}
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06000E91 RID: 3729 RVA: 0x0004744D File Offset: 0x0004564D
		private float maxOrDefault
		{
			get
			{
				if (this.max != 0f)
				{
					return this.max;
				}
				return InputSystem.settings.defaultDeadzoneMax;
			}
		}

		// Token: 0x06000E92 RID: 3730 RVA: 0x00047470 File Offset: 0x00045670
		public override float Process(float value, InputControl control = null)
		{
			float minOrDefault = this.minOrDefault;
			float maxOrDefault = this.maxOrDefault;
			float num = Mathf.Abs(value);
			if (num < minOrDefault)
			{
				return 0f;
			}
			if (num > maxOrDefault)
			{
				return Mathf.Sign(value);
			}
			return Mathf.Sign(value) * ((num - minOrDefault) / (maxOrDefault - minOrDefault));
		}

		// Token: 0x06000E93 RID: 3731 RVA: 0x000474B5 File Offset: 0x000456B5
		public override string ToString()
		{
			return string.Format("AxisDeadzone(min={0},max={1})", this.minOrDefault, this.maxOrDefault);
		}

		// Token: 0x040005FC RID: 1532
		public float min;

		// Token: 0x040005FD RID: 1533
		public float max;
	}
}
