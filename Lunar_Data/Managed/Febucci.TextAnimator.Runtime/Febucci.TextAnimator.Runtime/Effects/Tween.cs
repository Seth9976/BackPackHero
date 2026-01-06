using System;
using UnityEngine;

namespace Febucci.UI.Effects
{
	// Token: 0x02000030 RID: 48
	public static class Tween
	{
		// Token: 0x060000B5 RID: 181 RVA: 0x00004B33 File Offset: 0x00002D33
		public static float EaseIn(float t)
		{
			return t * t;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00004B38 File Offset: 0x00002D38
		public static float Flip(float x)
		{
			return 1f - x;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00004B41 File Offset: 0x00002D41
		public static float Square(float t)
		{
			return t * t;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00004B46 File Offset: 0x00002D46
		public static float EaseOut(float t)
		{
			return Tween.Flip(Tween.Square(Tween.Flip(t)));
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00004B58 File Offset: 0x00002D58
		public static float EaseInOut(float t)
		{
			return Mathf.Lerp(Tween.EaseIn(t), Tween.EaseOut(t), t);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00004B6C File Offset: 0x00002D6C
		public static float BounceOut(float t)
		{
			if (t < 0.36363637f)
			{
				return 7.5625f * t * t;
			}
			if (t < 0.72727275f)
			{
				return 7.5625f * (t -= 0.54545456f) * t + 0.75f;
			}
			if (t < 0.90909094f)
			{
				return 7.5625f * (t -= 0.8181818f) * t + 0.9375f;
			}
			return 7.5625f * (t -= 0.95454544f) * t + 0.984375f;
		}
	}
}
