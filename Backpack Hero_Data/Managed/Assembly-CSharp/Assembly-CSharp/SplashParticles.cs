using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// Token: 0x0200019A RID: 410
public class SplashParticles : MonoBehaviour
{
	// Token: 0x0600106E RID: 4206 RVA: 0x0009D156 File Offset: 0x0009B356
	private void Awake()
	{
		SplashParticles.splashParticles.Add(this);
	}

	// Token: 0x0600106F RID: 4207 RVA: 0x0009D163 File Offset: 0x0009B363
	private void OnDestroy()
	{
		SplashParticles.splashParticles.Remove(this);
	}

	// Token: 0x06001070 RID: 4208 RVA: 0x0009D171 File Offset: 0x0009B371
	private void Start()
	{
	}

	// Token: 0x06001071 RID: 4209 RVA: 0x0009D173 File Offset: 0x0009B373
	private void Update()
	{
	}

	// Token: 0x06001072 RID: 4210 RVA: 0x0009D175 File Offset: 0x0009B375
	private void MakeReferences()
	{
		if (!this.particleSystem)
		{
			this.particleSystem = base.GetComponent<ParticleSystem>();
		}
	}

	// Token: 0x06001073 RID: 4211 RVA: 0x0009D190 File Offset: 0x0009B390
	public static SplashParticles GetSplashParticle(SplashParticles.Type type)
	{
		foreach (SplashParticles splashParticles in SplashParticles.splashParticles)
		{
			if (splashParticles.type == type)
			{
				return splashParticles;
			}
		}
		return null;
	}

	// Token: 0x06001074 RID: 4212 RVA: 0x0009D1EC File Offset: 0x0009B3EC
	public static void CopySpriteAndMoveToPosition(SpriteRenderer spriteRenderer, float volume)
	{
		SplashParticles.splashParticles[0].CopySpriteAndMoveToPositionPrivate(spriteRenderer, volume);
	}

	// Token: 0x06001075 RID: 4213 RVA: 0x0009D200 File Offset: 0x0009B400
	public static void CopySpriteAndMoveToPosition(TileBase tile, Sprite sprite, Vector2 position, float volume, SplashParticles.Type type)
	{
		SplashParticles splashParticle = SplashParticles.GetSplashParticle(type);
		if (splashParticle)
		{
			splashParticle.MakeReferences();
			splashParticle.particleSystem.emission.rateOverTime = volume;
			ParticleSystem.ShapeModule shape = splashParticle.particleSystem.shape;
			shape.sprite = sprite;
			shape.texture = sprite.texture;
			splashParticle.MoveToPosition(position);
		}
	}

	// Token: 0x06001076 RID: 4214 RVA: 0x0009D26C File Offset: 0x0009B46C
	public static void CopySpriteAndMoveToPosition(SpriteRenderer spriteRenderer, float volume, SplashParticles.Type type)
	{
		SplashParticles splashParticle = SplashParticles.GetSplashParticle(type);
		if (splashParticle)
		{
			splashParticle.CopySpriteAndMoveToPositionPrivate(spriteRenderer, volume);
		}
	}

	// Token: 0x06001077 RID: 4215 RVA: 0x0009D290 File Offset: 0x0009B490
	private void CopySpriteAndMoveToPositionPrivate(SpriteRenderer spriteRenderer, float volume)
	{
		this.MakeReferences();
		this.particleSystem.emission.rateOverTime = volume;
		this.CopySprite(spriteRenderer);
		this.MoveToPosition(spriteRenderer.transform.position);
	}

	// Token: 0x06001078 RID: 4216 RVA: 0x0009D2D4 File Offset: 0x0009B4D4
	private void CopySprite(SpriteRenderer spriteRenderer)
	{
		ParticleSystem.ShapeModule shape = this.particleSystem.shape;
		if (!spriteRenderer || !this.particleSystem || !spriteRenderer.sprite)
		{
			return;
		}
		shape.sprite = spriteRenderer.sprite;
		shape.texture = spriteRenderer.sprite.texture;
	}

	// Token: 0x06001079 RID: 4217 RVA: 0x0009D32F File Offset: 0x0009B52F
	private void MoveToPosition(Vector3 position)
	{
		base.transform.position = position;
		this.particleSystem.Play();
	}

	// Token: 0x04000D61 RID: 3425
	public SplashParticles.Type type;

	// Token: 0x04000D62 RID: 3426
	private static List<SplashParticles> splashParticles = new List<SplashParticles>();

	// Token: 0x04000D63 RID: 3427
	private ParticleSystem particleSystem;

	// Token: 0x02000475 RID: 1141
	public enum Type
	{
		// Token: 0x04001A46 RID: 6726
		Destroy,
		// Token: 0x04001A47 RID: 6727
		Create
	}
}
