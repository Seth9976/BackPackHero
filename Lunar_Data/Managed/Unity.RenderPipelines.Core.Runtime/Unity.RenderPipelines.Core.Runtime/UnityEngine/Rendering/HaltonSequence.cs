using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020000AB RID: 171
	public static class HaltonSequence
	{
		// Token: 0x060005DE RID: 1502 RVA: 0x0001BE44 File Offset: 0x0001A044
		public static float Get(int index, int radix)
		{
			float num = 0f;
			float num2 = 1f / (float)radix;
			while (index > 0)
			{
				num += (float)(index % radix) * num2;
				index /= radix;
				num2 /= (float)radix;
			}
			return num;
		}
	}
}
