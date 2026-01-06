using System;

namespace UnityEngine.InputSystem.Processors
{
	// Token: 0x020000FF RID: 255
	public class NormalizeProcessor : InputProcessor<float>
	{
		// Token: 0x06000EA9 RID: 3753 RVA: 0x00047794 File Offset: 0x00045994
		public override float Process(float value, InputControl control)
		{
			return NormalizeProcessor.Normalize(value, this.min, this.max, this.zero);
		}

		// Token: 0x06000EAA RID: 3754 RVA: 0x000477B0 File Offset: 0x000459B0
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

		// Token: 0x06000EAB RID: 3755 RVA: 0x000477F8 File Offset: 0x000459F8
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

		// Token: 0x06000EAC RID: 3756 RVA: 0x0004782A File Offset: 0x00045A2A
		public override string ToString()
		{
			return string.Format("Normalize(min={0},max={1},zero={2})", this.min, this.max, this.zero);
		}

		// Token: 0x04000605 RID: 1541
		public float min;

		// Token: 0x04000606 RID: 1542
		public float max;

		// Token: 0x04000607 RID: 1543
		public float zero;
	}
}
