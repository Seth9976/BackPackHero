using System;
using UnityEngine;

namespace Febucci.UI.Core
{
	// Token: 0x0200003B RID: 59
	public struct CharacterData
	{
		// Token: 0x06000158 RID: 344 RVA: 0x00006A2C File Offset: 0x00004C2C
		public void ResetInfo(int i, bool resetVisibility = true)
		{
			this.index = i;
			this.wordIndex = -1;
			if (resetVisibility)
			{
				this.isVisible = true;
			}
			if (!this.info.initialized)
			{
				this.source.positions = new Vector3[4];
				this.source.colors = new Color32[4];
				this.current.positions = new Vector3[4];
				this.current.colors = new Color32[4];
			}
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00006AA4 File Offset: 0x00004CA4
		public void ResetAnimation()
		{
			for (int i = 0; i < this.source.positions.Length; i++)
			{
				this.current.positions[i] = this.source.positions[i];
				this.current.colors[i] = this.source.colors[i];
			}
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00006B10 File Offset: 0x00004D10
		public void Hide()
		{
			byte b = 0;
			while ((int)b < this.source.positions.Length)
			{
				this.current.positions[(int)b] = Vector3.zero;
				b += 1;
			}
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00006B4C File Offset: 0x00004D4C
		public void UpdateIntensity(float referenceFontSize)
		{
			this.uniformIntensity = this.info.pointSize / referenceFontSize;
		}

		// Token: 0x040000E6 RID: 230
		public CharInfo info;

		// Token: 0x040000E7 RID: 231
		public int index;

		// Token: 0x040000E8 RID: 232
		public int wordIndex;

		// Token: 0x040000E9 RID: 233
		public bool isVisible;

		// Token: 0x040000EA RID: 234
		public float passedTime;

		// Token: 0x040000EB RID: 235
		public float uniformIntensity;

		// Token: 0x040000EC RID: 236
		public MeshData source;

		// Token: 0x040000ED RID: 237
		public MeshData current;
	}
}
