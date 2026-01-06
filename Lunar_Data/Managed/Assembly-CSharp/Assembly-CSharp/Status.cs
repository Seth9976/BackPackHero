using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000097 RID: 151
public class Status : MonoBehaviour
{
	// Token: 0x06000411 RID: 1041 RVA: 0x00014518 File Offset: 0x00012718
	private void OnEnable()
	{
		Status.allStatuses.Add(this);
	}

	// Token: 0x06000412 RID: 1042 RVA: 0x00014525 File Offset: 0x00012725
	private void OnDisable()
	{
		Status.allStatuses.Remove(this);
	}

	// Token: 0x06000413 RID: 1043 RVA: 0x00014534 File Offset: 0x00012734
	private void Start()
	{
		this.startingMass = this.rb.mass;
		this.startingColor = this.spriteRenderer.color;
		this.startingFireEmission = this.fireParticles.emission.rateOverTime.constant;
		this.fireParticles.emission.rateOverTime = 0f;
		ParticleSystem.ShapeModule shape = this.fireParticles.shape;
		shape.sprite = this.spriteRenderer.sprite;
		shape.texture = this.spriteRenderer.sprite.texture;
		this.startingPoisonEmission = this.poisonParticles.emission.rateOverTime.constant;
		this.poisonParticles.emission.rateOverTime = 0f;
		ParticleSystem.ShapeModule shape2 = this.poisonParticles.shape;
		shape2.sprite = this.spriteRenderer.sprite;
		shape2.texture = this.spriteRenderer.sprite.texture;
	}

	// Token: 0x06000414 RID: 1044 RVA: 0x00014650 File Offset: 0x00012850
	private void Update()
	{
		if (this.animator && this.GetStatusEffect(Status.StatusEffect.Type.Freeze))
		{
			this.animator.enabled = false;
		}
		else if (this.animator && !this.animator.enabled)
		{
			this.animator.enabled = true;
		}
		if (this.GetStatusEffect(Status.StatusEffect.Type.Freeze))
		{
			this.rb.mass = this.startingMass * 10f;
		}
		else
		{
			this.rb.mass = this.startingMass;
		}
		this.destructible.startingColor = this.GetEffects();
		for (int i = 0; i < this.statusEffects.Count; i++)
		{
			Status.StatusEffect statusEffect = this.statusEffects[i];
			statusEffect.lastsFor -= Time.deltaTime * TimeManager.instance.currentTimeScale;
			statusEffect.lastPing -= Time.deltaTime * TimeManager.instance.currentTimeScale;
			if (statusEffect.lastPing <= 0f)
			{
				this.PingEffect(statusEffect);
				statusEffect.lastPing = statusEffect.pingDistance;
			}
			if (statusEffect.lastsFor <= 0f)
			{
				this.PingEffect(statusEffect);
				this.statusEffects.RemoveAt(i);
				i--;
			}
		}
	}

	// Token: 0x06000415 RID: 1045 RVA: 0x0001479C File Offset: 0x0001299C
	public static List<Status> GetStatusesWithEffect(Status.StatusEffect.Type type)
	{
		List<Status> list = new List<Status>();
		for (int i = 0; i < Status.allStatuses.Count; i++)
		{
			if (Status.allStatuses[i].GetStatusEffect(type))
			{
				list.Add(Status.allStatuses[i]);
			}
		}
		return list;
	}

	// Token: 0x06000416 RID: 1046 RVA: 0x000147F0 File Offset: 0x000129F0
	private void PingEffect(Status.StatusEffect effect)
	{
		switch (effect.type)
		{
		case Status.StatusEffect.Type.Burn:
			this.destructible.TakeDamage(2f, default(Vector2), true);
			return;
		case Status.StatusEffect.Type.Freeze:
			break;
		case Status.StatusEffect.Type.Poison:
			this.destructible.TakeDamage(1f, default(Vector2), true);
			if (ModifierManager.instance.GetModifierRandomRoll(Modifier.ModifierEffect.Type.PoisonEnemiesHaveAChanceToSpawnLocusts))
			{
				Object.Instantiate<GameObject>(this.locustPrefab, base.transform.position, Quaternion.identity, RoomManager.instance.roomContents);
			}
			break;
		default:
			return;
		}
	}

	// Token: 0x06000417 RID: 1047 RVA: 0x00014880 File Offset: 0x00012A80
	public void ApplyStatusEffect(Status.StatusEffect.Type type, float lastsFor)
	{
		foreach (Status.StatusEffect statusEffect in this.statusEffects)
		{
			if (statusEffect.type == type)
			{
				statusEffect.lastsFor = lastsFor;
				return;
			}
		}
		Status.StatusEffect statusEffect2 = new Status.StatusEffect(type, lastsFor);
		this.statusEffects.Add(statusEffect2);
		this.StartParticles(type);
	}

	// Token: 0x06000418 RID: 1048 RVA: 0x000148FC File Offset: 0x00012AFC
	public void ApplyStatusEffect(Status.StatusEffect effect)
	{
		foreach (Status.StatusEffect statusEffect in this.statusEffects)
		{
			if (statusEffect.type == effect.type)
			{
				statusEffect.lastsFor = effect.lastsFor;
				return;
			}
		}
		Status.StatusEffect statusEffect2 = effect.Copy();
		this.statusEffects.Add(statusEffect2);
		this.StartParticles(effect.type);
	}

	// Token: 0x06000419 RID: 1049 RVA: 0x00014984 File Offset: 0x00012B84
	private void StartParticles(Status.StatusEffect.Type type)
	{
		if (type == Status.StatusEffect.Type.Poison)
		{
			this.poisonParticles.gameObject.SetActive(true);
			this.poisonParticlesTimeScaler.timeType = ParticleSystemTimeScaler.TimeType.TimeManagerLerp;
			return;
		}
		if (type == Status.StatusEffect.Type.Burn)
		{
			this.fireParticles.gameObject.SetActive(true);
			this.fireParticlesTimeScaler.timeType = ParticleSystemTimeScaler.TimeType.TimeManagerLerp;
		}
	}

	// Token: 0x0600041A RID: 1050 RVA: 0x000149D4 File Offset: 0x00012BD4
	public Status.StatusEffect GetStatusEffect(Status.StatusEffect.Type type)
	{
		for (int i = 0; i < this.statusEffects.Count; i++)
		{
			if (this.statusEffects[i].type == type)
			{
				return this.statusEffects[i];
			}
		}
		return null;
	}

	// Token: 0x0600041B RID: 1051 RVA: 0x00014A1C File Offset: 0x00012C1C
	public Color GetEffects()
	{
		Status.StatusEffect statusEffect = this.GetStatusEffect(Status.StatusEffect.Type.Freeze);
		if (statusEffect)
		{
			float num = Mathf.Clamp01(statusEffect.lastsFor / 0.25f);
			return Color.Lerp(this.startingColor, new Color(0f, 0.54f, 1f, 1f), num);
		}
		Status.StatusEffect statusEffect2 = this.GetStatusEffect(Status.StatusEffect.Type.Burn);
		if (statusEffect2)
		{
			this.fireParticles.emission.rateOverTime = Mathf.Lerp(0f, this.startingFireEmission, Mathf.Clamp01(statusEffect2.lastsFor / 0.5f));
		}
		else
		{
			this.fireParticles.emission.rateOverTime = 0f;
		}
		Status.StatusEffect statusEffect3 = this.GetStatusEffect(Status.StatusEffect.Type.Poison);
		if (statusEffect3)
		{
			this.poisonParticles.emission.rateOverTime = Mathf.Lerp(0f, this.startingPoisonEmission, Mathf.Clamp01(statusEffect3.lastsFor / 0.5f));
			if (!statusEffect)
			{
				float num2 = Mathf.Clamp01(statusEffect3.lastsFor / 0.5f);
				return Color.Lerp(this.startingColor, Color.green, num2);
			}
		}
		else
		{
			this.poisonParticles.emission.rateOverTime = 0f;
		}
		return this.startingColor;
	}

	// Token: 0x0400031D RID: 797
	private static List<Status> allStatuses = new List<Status>();

	// Token: 0x0400031E RID: 798
	[SerializeField]
	private Destructible destructible;

	// Token: 0x0400031F RID: 799
	[SerializeField]
	private SimpleAnimator animator;

	// Token: 0x04000320 RID: 800
	[SerializeField]
	private SpriteRenderer spriteRenderer;

	// Token: 0x04000321 RID: 801
	[SerializeField]
	private Rigidbody2D rb;

	// Token: 0x04000322 RID: 802
	[Header("Particles")]
	[SerializeField]
	private ParticleSystem fireParticles;

	// Token: 0x04000323 RID: 803
	[SerializeField]
	private ParticleSystemTimeScaler fireParticlesTimeScaler;

	// Token: 0x04000324 RID: 804
	private float startingFireEmission;

	// Token: 0x04000325 RID: 805
	[SerializeField]
	private ParticleSystem poisonParticles;

	// Token: 0x04000326 RID: 806
	[SerializeField]
	private ParticleSystemTimeScaler poisonParticlesTimeScaler;

	// Token: 0x04000327 RID: 807
	private float startingPoisonEmission;

	// Token: 0x04000328 RID: 808
	private float startingMass = 1f;

	// Token: 0x04000329 RID: 809
	public List<Status.StatusEffect> statusEffects = new List<Status.StatusEffect>();

	// Token: 0x0400032A RID: 810
	private Color startingColor;

	// Token: 0x0400032B RID: 811
	[Header("Status Effects")]
	[SerializeField]
	private GameObject locustPrefab;

	// Token: 0x0200011D RID: 285
	[Serializable]
	public class StatusEffect
	{
		// Token: 0x060005DD RID: 1501 RVA: 0x0001A0E8 File Offset: 0x000182E8
		public static implicit operator bool(Status.StatusEffect effect)
		{
			return effect != null;
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x0001A0EE File Offset: 0x000182EE
		public StatusEffect(Status.StatusEffect.Type type, float lastsFor)
		{
			this.type = type;
			this.lastsFor = lastsFor;
			this.lastPing = this.pingDistance;
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x0001A126 File Offset: 0x00018326
		public Status.StatusEffect Copy()
		{
			return new Status.StatusEffect(this.type, this.lastsFor);
		}

		// Token: 0x0400050E RID: 1294
		public Status.StatusEffect.Type type;

		// Token: 0x0400050F RID: 1295
		public float lastsFor = 1f;

		// Token: 0x04000510 RID: 1296
		[NonSerialized]
		public float lastPing;

		// Token: 0x04000511 RID: 1297
		[NonSerialized]
		public float pingDistance = 0.25f;

		// Token: 0x0200012F RID: 303
		public enum Type
		{
			// Token: 0x0400056D RID: 1389
			Burn,
			// Token: 0x0400056E RID: 1390
			Freeze,
			// Token: 0x0400056F RID: 1391
			Poison
		}
	}
}
