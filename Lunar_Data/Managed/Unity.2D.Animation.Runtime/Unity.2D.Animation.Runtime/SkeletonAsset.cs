using System;

namespace UnityEngine.U2D.Animation
{
	// Token: 0x02000008 RID: 8
	public class SkeletonAsset : ScriptableObject
	{
		// Token: 0x0600002A RID: 42 RVA: 0x000026F5 File Offset: 0x000008F5
		public SpriteBone[] GetSpriteBones()
		{
			return this.m_SpriteBones;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000026FD File Offset: 0x000008FD
		public void SetSpriteBones(SpriteBone[] spriteBones)
		{
			this.m_SpriteBones = spriteBones;
		}

		// Token: 0x04000010 RID: 16
		[SerializeField]
		private SpriteBone[] m_SpriteBones;
	}
}
