using System;

namespace UnityEngine.Tilemaps
{
	// Token: 0x0200000A RID: 10
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.2d.tilemap.extras@latest/index.html?subfolder=/manual/AnimatedTile.html")]
	[Serializable]
	public class AnimatedTile : TileBase
	{
		// Token: 0x06000040 RID: 64 RVA: 0x00003B80 File Offset: 0x00001D80
		public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
		{
			tileData.transform = Matrix4x4.identity;
			tileData.color = Color.white;
			if (this.m_AnimatedSprites != null && this.m_AnimatedSprites.Length != 0)
			{
				tileData.sprite = this.m_AnimatedSprites[this.m_AnimatedSprites.Length - 1];
				tileData.colliderType = this.m_TileColliderType;
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00003BD8 File Offset: 0x00001DD8
		public override bool GetTileAnimationData(Vector3Int position, ITilemap tilemap, ref TileAnimationData tileAnimationData)
		{
			if (this.m_AnimatedSprites.Length != 0)
			{
				tileAnimationData.animatedSprites = this.m_AnimatedSprites;
				tileAnimationData.animationSpeed = Random.Range(this.m_MinSpeed, this.m_MaxSpeed);
				tileAnimationData.animationStartTime = this.m_AnimationStartTime;
				if (0 < this.m_AnimationStartFrame && this.m_AnimationStartFrame <= this.m_AnimatedSprites.Length)
				{
					Tilemap component = tilemap.GetComponent<Tilemap>();
					if (component != null && component.animationFrameRate > 0f)
					{
						tileAnimationData.animationStartTime = (float)(this.m_AnimationStartFrame - 1) / component.animationFrameRate;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x04000022 RID: 34
		public Sprite[] m_AnimatedSprites;

		// Token: 0x04000023 RID: 35
		public float m_MinSpeed = 1f;

		// Token: 0x04000024 RID: 36
		public float m_MaxSpeed = 1f;

		// Token: 0x04000025 RID: 37
		public float m_AnimationStartTime;

		// Token: 0x04000026 RID: 38
		public int m_AnimationStartFrame;

		// Token: 0x04000027 RID: 39
		public Tile.ColliderType m_TileColliderType;
	}
}
