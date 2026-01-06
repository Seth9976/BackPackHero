using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000151 RID: 337
public class Overworld_ParticleManager : MonoBehaviour
{
	// Token: 0x06000D4C RID: 3404 RVA: 0x00085B0F File Offset: 0x00083D0F
	private void Awake()
	{
		Overworld_ParticleManager.main = this;
	}

	// Token: 0x06000D4D RID: 3405 RVA: 0x00085B17 File Offset: 0x00083D17
	private void OnDestroy()
	{
		if (Overworld_ParticleManager.main == this)
		{
			Overworld_ParticleManager.main = null;
		}
	}

	// Token: 0x06000D4E RID: 3406 RVA: 0x00085B2C File Offset: 0x00083D2C
	private void Start()
	{
	}

	// Token: 0x06000D4F RID: 3407 RVA: 0x00085B2E File Offset: 0x00083D2E
	private void Update()
	{
	}

	// Token: 0x06000D50 RID: 3408 RVA: 0x00085B30 File Offset: 0x00083D30
	public void ShowParticles(Item2 item)
	{
		SpriteRenderer component = item.GetComponent<SpriteRenderer>();
		if (!component)
		{
			return;
		}
		this.ShowParticles(component.sprite, 1f);
	}

	// Token: 0x06000D51 RID: 3409 RVA: 0x00085B60 File Offset: 0x00083D60
	public void ShowParticles(List<Item2> items)
	{
		if (items.Count == 0)
		{
			return;
		}
		List<Sprite> list = items.Select((Item2 x) => x.GetComponent<SpriteRenderer>().sprite).ToList<Sprite>();
		this.ShowParticles(list);
	}

	// Token: 0x06000D52 RID: 3410 RVA: 0x00085BA8 File Offset: 0x00083DA8
	public void ShowParticles(List<Sprite> sprites)
	{
		foreach (Sprite sprite in sprites)
		{
			this.ShowParticles(sprite, (float)sprites.Count);
		}
	}

	// Token: 0x06000D53 RID: 3411 RVA: 0x00085C00 File Offset: 0x00083E00
	public void ShowParticles(Sprite sprite, float divisor = 1f)
	{
		ParticleSystem component = Object.Instantiate<GameObject>(this.newItemParticleSystemPrefab.gameObject, this.particlesTransform.transform.position, Quaternion.identity, this.particlesTransform).GetComponent<ParticleSystem>();
		this.particleSystems.Add(component);
		ParticleSystem.EmissionModule emission = component.emission;
		emission.rateOverTime = new ParticleSystem.MinMaxCurve(emission.rateOverTime.constant / divisor);
		component.textureSheetAnimation.SetSprite(0, sprite);
	}

	// Token: 0x06000D54 RID: 3412 RVA: 0x00085C80 File Offset: 0x00083E80
	public void HideParticles()
	{
		for (int i = 0; i < this.particleSystems.Count; i++)
		{
			if (!this.particleSystems[i])
			{
				this.particleSystems.RemoveAt(i);
				i--;
			}
			else
			{
				this.particleSystems[i].Stop(true, ParticleSystemStopBehavior.StopEmitting);
			}
		}
		this.particleSystems.Clear();
	}

	// Token: 0x04000AC5 RID: 2757
	public static Overworld_ParticleManager main;

	// Token: 0x04000AC6 RID: 2758
	[SerializeField]
	private Transform particlesTransform;

	// Token: 0x04000AC7 RID: 2759
	[SerializeField]
	private GameObject newItemParticleSystemPrefab;

	// Token: 0x04000AC8 RID: 2760
	private List<ParticleSystem> particleSystems = new List<ParticleSystem>();
}
