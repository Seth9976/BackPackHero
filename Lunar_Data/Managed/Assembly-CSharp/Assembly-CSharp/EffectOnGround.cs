using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200002B RID: 43
public class EffectOnGround : MonoBehaviour
{
	// Token: 0x0600014A RID: 330 RVA: 0x00007A97 File Offset: 0x00005C97
	private void Start()
	{
		this.currentLifeSpan = this.lifeSpan;
	}

	// Token: 0x0600014B RID: 331 RVA: 0x00007AA8 File Offset: 0x00005CA8
	private void Update()
	{
		this.currentLifeSpan -= Time.deltaTime * TimeManager.instance.currentTimeScale;
		if (this.currentLifeSpan <= 0f && this.shrinkCoroutine == null)
		{
			this.shrinkCoroutine = base.StartCoroutine(this.Shrink());
		}
	}

	// Token: 0x0600014C RID: 332 RVA: 0x00007AF9 File Offset: 0x00005CF9
	private IEnumerator Shrink()
	{
		this.particles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
		this.particles.transform.SetParent(null, true);
		float time = 0f;
		while (time < 0.25f)
		{
			time += Time.deltaTime * TimeManager.instance.currentTimeScale;
			base.transform.localScale = Vector3.one * (1f - time / 0.25f);
			yield return null;
		}
		Object.Destroy(base.gameObject);
		yield break;
	}

	// Token: 0x04000100 RID: 256
	[SerializeField]
	public EffectOnGround.GroundEffectType groundEffectType;

	// Token: 0x04000101 RID: 257
	[SerializeField]
	private float lifeSpan = 1.25f;

	// Token: 0x04000102 RID: 258
	[SerializeField]
	private ParticleSystem particles;

	// Token: 0x04000103 RID: 259
	private float currentLifeSpan = 1.25f;

	// Token: 0x04000104 RID: 260
	private Coroutine shrinkCoroutine;

	// Token: 0x020000D1 RID: 209
	[SerializeField]
	public enum GroundEffectType
	{
		// Token: 0x04000413 RID: 1043
		Fire,
		// Token: 0x04000414 RID: 1044
		Poison,
		// Token: 0x04000415 RID: 1045
		Ice
	}
}
