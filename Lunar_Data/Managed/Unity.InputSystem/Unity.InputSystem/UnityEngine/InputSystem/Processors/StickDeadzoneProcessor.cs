using System;

namespace UnityEngine.InputSystem.Processors
{
	// Token: 0x02000105 RID: 261
	public class StickDeadzoneProcessor : InputProcessor<Vector2>
	{
		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06000EB8 RID: 3768 RVA: 0x0004795C File Offset: 0x00045B5C
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

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06000EB9 RID: 3769 RVA: 0x0004797C File Offset: 0x00045B7C
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

		// Token: 0x06000EBA RID: 3770 RVA: 0x0004799C File Offset: 0x00045B9C
		public override Vector2 Process(Vector2 value, InputControl control = null)
		{
			float magnitude = value.magnitude;
			float deadZoneAdjustedValue = this.GetDeadZoneAdjustedValue(magnitude);
			if (deadZoneAdjustedValue == 0f)
			{
				value = Vector2.zero;
			}
			else
			{
				value *= deadZoneAdjustedValue / magnitude;
			}
			return value;
		}

		// Token: 0x06000EBB RID: 3771 RVA: 0x000479D8 File Offset: 0x00045BD8
		private float GetDeadZoneAdjustedValue(float value)
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

		// Token: 0x06000EBC RID: 3772 RVA: 0x00047A1D File Offset: 0x00045C1D
		public override string ToString()
		{
			return string.Format("StickDeadzone(min={0},max={1})", this.minOrDefault, this.maxOrDefault);
		}

		// Token: 0x0400060D RID: 1549
		public float min;

		// Token: 0x0400060E RID: 1550
		public float max;
	}
}
