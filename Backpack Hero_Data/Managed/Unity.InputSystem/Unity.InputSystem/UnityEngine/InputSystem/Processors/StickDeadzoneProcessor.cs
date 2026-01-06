using System;

namespace UnityEngine.InputSystem.Processors
{
	// Token: 0x02000105 RID: 261
	public class StickDeadzoneProcessor : InputProcessor<Vector2>
	{
		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06000EBD RID: 3773 RVA: 0x000479A8 File Offset: 0x00045BA8
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

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06000EBE RID: 3774 RVA: 0x000479C8 File Offset: 0x00045BC8
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

		// Token: 0x06000EBF RID: 3775 RVA: 0x000479E8 File Offset: 0x00045BE8
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

		// Token: 0x06000EC0 RID: 3776 RVA: 0x00047A24 File Offset: 0x00045C24
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

		// Token: 0x06000EC1 RID: 3777 RVA: 0x00047A69 File Offset: 0x00045C69
		public override string ToString()
		{
			return string.Format("StickDeadzone(min={0},max={1})", this.minOrDefault, this.maxOrDefault);
		}

		// Token: 0x0400060E RID: 1550
		public float min;

		// Token: 0x0400060F RID: 1551
		public float max;
	}
}
