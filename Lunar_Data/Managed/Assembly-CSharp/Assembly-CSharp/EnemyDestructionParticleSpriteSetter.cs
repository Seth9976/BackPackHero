using System;
using UnityEngine;

// Token: 0x02000030 RID: 48
public class EnemyDestructionParticleSpriteSetter : MonoBehaviour
{
	// Token: 0x06000189 RID: 393 RVA: 0x00008EE8 File Offset: 0x000070E8
	private void Start()
	{
		ParticleSystem.ShapeModule shape = this.destroyParticles.shape;
		shape.sprite = this.sprite.sprite;
		shape.texture = this.sprite.sprite.texture;
	}

	// Token: 0x0400013B RID: 315
	[SerializeField]
	private SpriteRenderer sprite;

	// Token: 0x0400013C RID: 316
	[SerializeField]
	private ParticleSystem destroyParticles;
}
