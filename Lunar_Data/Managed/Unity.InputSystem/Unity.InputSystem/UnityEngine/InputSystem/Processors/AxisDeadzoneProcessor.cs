using System;

namespace UnityEngine.InputSystem.Processors
{
	// Token: 0x020000F8 RID: 248
	public class AxisDeadzoneProcessor : InputProcessor<float>
	{
		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06000E8B RID: 3723 RVA: 0x000473E1 File Offset: 0x000455E1
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

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06000E8C RID: 3724 RVA: 0x00047401 File Offset: 0x00045601
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

		// Token: 0x06000E8D RID: 3725 RVA: 0x00047424 File Offset: 0x00045624
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

		// Token: 0x06000E8E RID: 3726 RVA: 0x00047469 File Offset: 0x00045669
		public override string ToString()
		{
			return string.Format("AxisDeadzone(min={0},max={1})", this.minOrDefault, this.maxOrDefault);
		}

		// Token: 0x040005FB RID: 1531
		public float min;

		// Token: 0x040005FC RID: 1532
		public float max;
	}
}
