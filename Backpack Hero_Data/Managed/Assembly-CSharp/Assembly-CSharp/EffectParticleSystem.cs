using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200004E RID: 78
public class EffectParticleSystem : MonoBehaviour
{
	// Token: 0x06000164 RID: 356 RVA: 0x000098D8 File Offset: 0x00007AD8
	private void RemoveRoutinesForThisType(EffectParticleSystem.ParticleType particleType)
	{
		for (int i = 0; i < this.runningRoutines.Count; i++)
		{
			if (this.runningRoutines[i].particleType == particleType)
			{
				base.StopCoroutine(this.runningRoutines[i].coroutine);
				this.runningRoutines.RemoveAt(i);
				i--;
			}
		}
	}

	// Token: 0x06000165 RID: 357 RVA: 0x00009938 File Offset: 0x00007B38
	private void AddToRunningRoutinesAndRemoveDupesForThisType(Transform t, EffectParticleSystem.ParticleType particleType)
	{
		this.RemoveRoutinesForThisType(particleType);
		EffectParticleSystem.RunningRoutines runningRoutines = new EffectParticleSystem.RunningRoutines();
		runningRoutines.particleType = particleType;
		runningRoutines.coroutine = base.StartCoroutine(this.FollowSpriteRoutine(t, particleType));
		this.runningRoutines.Add(runningRoutines);
	}

	// Token: 0x06000166 RID: 358 RVA: 0x00009979 File Offset: 0x00007B79
	private void Awake()
	{
		if (EffectParticleSystem.Instance == null)
		{
			EffectParticleSystem.Instance = this;
			return;
		}
		Object.Destroy(base.gameObject);
	}

	// Token: 0x06000167 RID: 359 RVA: 0x0000999A File Offset: 0x00007B9A
	private void OnDestroy()
	{
		if (EffectParticleSystem.Instance == this)
		{
			EffectParticleSystem.Instance = null;
		}
	}

	// Token: 0x06000168 RID: 360 RVA: 0x000099AF File Offset: 0x00007BAF
	private void Start()
	{
	}

	// Token: 0x06000169 RID: 361 RVA: 0x000099B4 File Offset: 0x00007BB4
	private ParticleSystem GetParticleSystemFromType(EffectParticleSystem.ParticleType p)
	{
		switch (p)
		{
		case EffectParticleSystem.ParticleType.forge:
			return this.forgeParticleSystem;
		case EffectParticleSystem.ParticleType.fire:
			return this.fireParticleSystem;
		case EffectParticleSystem.ParticleType.poison:
			return this.poisonParticleSystem;
		case EffectParticleSystem.ParticleType.curse:
			return this.curseParticleSystem;
		case EffectParticleSystem.ParticleType.cleanse:
			return this.cleansingParticleSystem;
		default:
			return null;
		}
	}

	// Token: 0x0600016A RID: 362 RVA: 0x00009A04 File Offset: 0x00007C04
	public void CopySprite(SpriteRenderer spriteRenderer, EffectParticleSystem.ParticleType particleType)
	{
		if (!spriteRenderer)
		{
			return;
		}
		ParticleSystem particleSystemFromType = this.GetParticleSystemFromType(particleType);
		if (!particleSystemFromType)
		{
			return;
		}
		ParticleSystem.ShapeModule shape = particleSystemFromType.shape;
		shape.texture = spriteRenderer.sprite.texture;
		shape.sprite = spriteRenderer.sprite;
		particleSystemFromType.transform.position = spriteRenderer.transform.position;
		particleSystemFromType.transform.rotation = spriteRenderer.transform.rotation;
		particleSystemFromType.Play();
	}

	// Token: 0x0600016B RID: 363 RVA: 0x00009A85 File Offset: 0x00007C85
	public void FollowSprite(Transform t, EffectParticleSystem.ParticleType particleType)
	{
		this.AddToRunningRoutinesAndRemoveDupesForThisType(t, particleType);
	}

	// Token: 0x0600016C RID: 364 RVA: 0x00009A8F File Offset: 0x00007C8F
	private IEnumerator FollowSpriteRoutine(Transform t, EffectParticleSystem.ParticleType particleType)
	{
		ParticleSystem particleSystem = this.GetParticleSystemFromType(particleType);
		yield return null;
		yield return null;
		while (t && particleSystem && particleSystem.IsAlive())
		{
			particleSystem.transform.position = t.position;
			particleSystem.transform.rotation = t.rotation;
			yield return null;
		}
		this.RemoveRoutinesForThisType(particleType);
		yield break;
	}

	// Token: 0x040000E7 RID: 231
	public static EffectParticleSystem Instance;

	// Token: 0x040000E8 RID: 232
	[SerializeField]
	private ParticleSystem forgeParticleSystem;

	// Token: 0x040000E9 RID: 233
	[SerializeField]
	private ParticleSystem fireParticleSystem;

	// Token: 0x040000EA RID: 234
	[SerializeField]
	private ParticleSystem poisonParticleSystem;

	// Token: 0x040000EB RID: 235
	[SerializeField]
	private ParticleSystem curseParticleSystem;

	// Token: 0x040000EC RID: 236
	[SerializeField]
	private ParticleSystem cleansingParticleSystem;

	// Token: 0x040000ED RID: 237
	private List<EffectParticleSystem.RunningRoutines> runningRoutines = new List<EffectParticleSystem.RunningRoutines>();

	// Token: 0x02000268 RID: 616
	public enum ParticleType
	{
		// Token: 0x04000F0E RID: 3854
		forge,
		// Token: 0x04000F0F RID: 3855
		fire,
		// Token: 0x04000F10 RID: 3856
		poison,
		// Token: 0x04000F11 RID: 3857
		curse,
		// Token: 0x04000F12 RID: 3858
		cleanse
	}

	// Token: 0x02000269 RID: 617
	private class RunningRoutines
	{
		// Token: 0x04000F13 RID: 3859
		public EffectParticleSystem.ParticleType particleType;

		// Token: 0x04000F14 RID: 3860
		public Coroutine coroutine;
	}
}
