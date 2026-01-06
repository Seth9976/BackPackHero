using System;
using UnityEngine;

namespace Febucci.UI.Effects
{
	// Token: 0x02000028 RID: 40
	[Serializable]
	public struct ColorCurve
	{
		// Token: 0x0600009D RID: 157 RVA: 0x000047C4 File Offset: 0x000029C4
		public ColorCurve(float waveSize)
		{
			this.enabled = false;
			this.waveSize = waveSize;
			this.duration = 1f;
			this.colorOverTime = new Gradient();
			this.colorOverTime.SetKeys(new GradientColorKey[]
			{
				new GradientColorKey(Color.white, 0f),
				new GradientColorKey(Color.cyan, 0.5f),
				new GradientColorKey(Color.white, 1f)
			}, new GradientAlphaKey[]
			{
				new GradientAlphaKey(1f, 0f),
				new GradientAlphaKey(1f, 1f)
			});
		}

		// Token: 0x0600009E RID: 158 RVA: 0x0000487A File Offset: 0x00002A7A
		public Color32 Evaluate(float time, int charIndex)
		{
			time = Mathf.Repeat(time + (float)charIndex * this.waveSize, this.duration);
			return this.colorOverTime.Evaluate(time);
		}

		// Token: 0x04000091 RID: 145
		public bool enabled;

		// Token: 0x04000092 RID: 146
		public Gradient colorOverTime;

		// Token: 0x04000093 RID: 147
		public float waveSize;

		// Token: 0x04000094 RID: 148
		public float duration;
	}
}
