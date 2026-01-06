using System;

namespace UnityEngine.InputSystem.Processors
{
	// Token: 0x020000FF RID: 255
	public class NormalizeProcessor : InputProcessor<float>
	{
		// Token: 0x06000EA4 RID: 3748 RVA: 0x00047748 File Offset: 0x00045948
		public override float Process(float value, InputControl control)
		{
			return NormalizeProcessor.Normalize(value, this.min, this.max, this.zero);
		}

		// Token: 0x06000EA5 RID: 3749 RVA: 0x00047764 File Offset: 0x00045964
		public static float Normalize(float value, float min, float max, float zero)
		{
			if (zero < min)
			{
				zero = min;
			}
			if (Mathf.Approximately(value, min))
			{
				if (min < zero)
				{
					return -1f;
				}
				return 0f;
			}
			else
			{
				float num = (value - min) / (max - min);
				if (min < zero)
				{
					return 2f * num - 1f;
				}
				return num;
			}
		}

		// Token: 0x06000EA6 RID: 3750 RVA: 0x000477AC File Offset: 0x000459AC
		internal static float Denormalize(float value, float min, float max, float zero)
		{
			if (zero < min)
			{
				zero = min;
			}
			if (min >= zero)
			{
				return min + (max - min) * value;
			}
			if (value < 0f)
			{
				return min + (zero - min) * (value * -1f);
			}
			return zero + (max - zero) * value;
		}

		// Token: 0x06000EA7 RID: 3751 RVA: 0x000477DE File Offset: 0x000459DE
		public override string ToString()
		{
			return string.Format("Normalize(min={0},max={1},zero={2})", this.min, this.max, this.zero);
		}

		// Token: 0x04000604 RID: 1540
		public float min;

		// Token: 0x04000605 RID: 1541
		public float max;

		// Token: 0x04000606 RID: 1542
		public float zero;
	}
}
