using System;
using UnityEngine;

// Token: 0x0200015C RID: 348
public class ParticlesFollowSpriteRendererLayer : MonoBehaviour
{
	// Token: 0x06000DF1 RID: 3569 RVA: 0x0008AD77 File Offset: 0x00088F77
	private void Start()
	{
	}

	// Token: 0x06000DF2 RID: 3570 RVA: 0x0008AD7C File Offset: 0x00088F7C
	private void Update()
	{
		if (!this.myParticleSystem || !this.spriteRenderer)
		{
			this.myParticleSystem = base.GetComponent<ParticleSystemRenderer>();
			Follow component = base.GetComponent<Follow>();
			if (component && component.follow)
			{
				this.spriteRenderer = component.follow.GetComponent<SpriteRenderer>();
			}
			if (!this.myParticleSystem || !this.spriteRenderer)
			{
				return;
			}
		}
		this.myParticleSystem.sortingOrder = this.spriteRenderer.sortingOrder;
	}

	// Token: 0x04000B4C RID: 2892
	private ParticleSystemRenderer myParticleSystem;

	// Token: 0x04000B4D RID: 2893
	private SpriteRenderer spriteRenderer;
}
