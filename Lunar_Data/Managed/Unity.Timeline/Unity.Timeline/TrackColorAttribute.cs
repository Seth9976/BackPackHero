using System;

namespace UnityEngine.Timeline
{
	// Token: 0x02000012 RID: 18
	[AttributeUsage(AttributeTargets.Class)]
	public class TrackColorAttribute : Attribute
	{
		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000188 RID: 392 RVA: 0x000065DB File Offset: 0x000047DB
		public Color color
		{
			get
			{
				return this.m_Color;
			}
		}

		// Token: 0x06000189 RID: 393 RVA: 0x000065E3 File Offset: 0x000047E3
		public TrackColorAttribute(float r, float g, float b)
		{
			this.m_Color = new Color(r, g, b);
		}

		// Token: 0x04000085 RID: 133
		private Color m_Color;
	}
}
