using System;

namespace Febucci.UI.Effects
{
	// Token: 0x0200002D RID: 45
	[Serializable]
	public struct TimeMode
	{
		// Token: 0x060000A7 RID: 167 RVA: 0x00004A64 File Offset: 0x00002C64
		public TimeMode(bool useUniformTime)
		{
			this.useUniformTime = useUniformTime;
			this.waveSize = 0f;
			this.timeSpeed = 1f;
			this.startDelay = 0f;
			this.tempTime = 0f;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00004A9C File Offset: 0x00002C9C
		public float GetTime(float animatorTime, float charTime, int charIndex)
		{
			this.tempTime = ((this.useUniformTime ? animatorTime : charTime) - this.startDelay) * this.timeSpeed - this.waveSize * (float)charIndex;
			if (this.tempTime < this.startDelay)
			{
				return -1f;
			}
			return this.tempTime;
		}

		// Token: 0x0400009D RID: 157
		public float startDelay;

		// Token: 0x0400009E RID: 158
		public bool useUniformTime;

		// Token: 0x0400009F RID: 159
		public float waveSize;

		// Token: 0x040000A0 RID: 160
		public float timeSpeed;

		// Token: 0x040000A1 RID: 161
		private float tempTime;
	}
}
