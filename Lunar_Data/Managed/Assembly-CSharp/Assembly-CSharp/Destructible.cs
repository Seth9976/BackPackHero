using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200002A RID: 42
public class Destructible : MonoBehaviour
{
	// Token: 0x0600013D RID: 317 RVA: 0x00007589 File Offset: 0x00005789
	private void OnEnable()
	{
		Destructible.destructibles.Add(this);
	}

	// Token: 0x0600013E RID: 318 RVA: 0x00007596 File Offset: 0x00005796
	private void OnDisable()
	{
		Destructible.destructibles.Remove(this);
	}

	// Token: 0x0600013F RID: 319 RVA: 0x000075A4 File Offset: 0x000057A4
	private void Start()
	{
		this.startingColor = this.spriteRenderer.color;
	}

	// Token: 0x06000140 RID: 320 RVA: 0x000075B8 File Offset: 0x000057B8
	private void Update()
	{
		if (this.isHighlighted)
		{
			this.spriteRenderer.color = Color.red;
			return;
		}
		if ((this.timeSinceLastHit += Time.deltaTime) > 0.3f)
		{
			this.spriteRenderer.color = this.GetColor();
			this.spriteRenderer.transform.localPosition = Vector3.zero;
			return;
		}
		this.timeSinceLastHit += Time.deltaTime;
		this.spriteRenderer.color = Color.Lerp(Color.red, this.GetColor(), this.timeSinceLastHit / 0.1f);
		this.spriteRenderer.transform.localPosition = Vector3.zero + Random.insideUnitSphere * Mathf.Sin(Time.time * 10f) * 0.1f;
	}

	// Token: 0x06000141 RID: 321 RVA: 0x00007698 File Offset: 0x00005898
	public Color GetColor()
	{
		return this.startingColor;
	}

	// Token: 0x06000142 RID: 322 RVA: 0x000076A0 File Offset: 0x000058A0
	public static void SetAllHighlighted(bool highlighted)
	{
		foreach (Destructible destructible in Destructible.destructibles)
		{
			destructible.SetHighlight(highlighted);
		}
	}

	// Token: 0x06000143 RID: 323 RVA: 0x000076F0 File Offset: 0x000058F0
	public void SetHighlight(bool highlighted)
	{
		this.isHighlighted = highlighted;
	}

	// Token: 0x06000144 RID: 324 RVA: 0x000076FC File Offset: 0x000058FC
	public void Heal(int amount, Vector2 positionOfDamage = default(Vector2))
	{
		Vector2 vector = base.transform.position;
		if (positionOfDamage != default(Vector2))
		{
			vector = positionOfDamage;
		}
		if (this.damageIndicatorHealingPrefab)
		{
			Object.Instantiate<GameObject>(this.damageIndicatorHealingPrefab, new Vector3(vector.x, vector.y, base.transform.position.z - 1f), Quaternion.identity).GetComponent<DamageIndicator>().ShowDamage(Mathf.RoundToInt((float)Mathf.Abs(amount)));
		}
		else if (this.damageIndicatorPrefab)
		{
			Object.Instantiate<GameObject>(this.damageIndicatorPrefab, new Vector3(vector.x, vector.y, base.transform.position.z - 1f), Quaternion.identity).GetComponent<DamageIndicator>().ShowDamage(Mathf.RoundToInt((float)Mathf.Abs(amount)));
		}
		this.health += amount;
		this.health = Mathf.Clamp(this.health, 0, this.maxHealth);
	}

	// Token: 0x06000145 RID: 325 RVA: 0x0000780C File Offset: 0x00005A0C
	public void TakeDamage(float amount, Vector2 positionOfDamage = default(Vector2), bool showDamageIndicator = true)
	{
		amount -= this.armor;
		if (amount < 0f)
		{
			amount = 0f;
		}
		this.timeSinceLastHit = 0f;
		Vector2 vector = base.transform.position;
		if (positionOfDamage != default(Vector2))
		{
			vector = positionOfDamage;
		}
		vector += Random.insideUnitCircle * 0.1f;
		if (this.damageIndicatorPrefab && showDamageIndicator)
		{
			Object.Instantiate<GameObject>(this.damageIndicatorPrefab, new Vector3(vector.x, vector.y, base.transform.position.z - 1f), Quaternion.identity).GetComponent<DamageIndicator>().ShowDamage(Mathf.RoundToInt(Mathf.Abs(amount)));
		}
		SoundManager.instance.PlaySFX("enemyHit", double.PositiveInfinity);
		if (this.maxHealth == -1)
		{
			return;
		}
		this.health -= Mathf.RoundToInt(amount);
		this.health = Mathf.Clamp(this.health, 0, this.maxHealth);
		foreach (Destructible.OnHealthChangedEvent onHealthChangedEvent in this.onHealthChangedEvents)
		{
			if (this.health <= onHealthChangedEvent.healthReached && !onHealthChangedEvent.hasBeenTriggered)
			{
				onHealthChangedEvent.onHealthChanged.Invoke();
				onHealthChangedEvent.hasBeenTriggered = true;
			}
		}
		if (this.health <= 0)
		{
			this.isDestroyed = true;
			this.health = 0;
			UnityEvent unityEvent = this.whenDestroyed;
			if (unityEvent == null)
			{
				return;
			}
			unityEvent.Invoke();
		}
	}

	// Token: 0x06000146 RID: 326 RVA: 0x000079B0 File Offset: 0x00005BB0
	public void Explode()
	{
		SoundManager.instance.PlaySFX("enemyDie", double.PositiveInfinity);
		this.destroyParticles.gameObject.SetActive(true);
		this.destroyParticles.main.startColor = this.spriteRenderer.color;
		this.destroyParticles.transform.SetParent(base.transform.parent);
		Object.Destroy(base.gameObject);
	}

	// Token: 0x06000147 RID: 327 RVA: 0x00007A2F File Offset: 0x00005C2F
	public void SilentlyExplode()
	{
		this.destroyParticles.gameObject.SetActive(true);
		this.destroyParticles.transform.SetParent(base.transform.parent);
		Object.Destroy(base.gameObject);
	}

	// Token: 0x040000EF RID: 239
	private static List<Destructible> destructibles = new List<Destructible>();

	// Token: 0x040000F0 RID: 240
	[SerializeField]
	private GameObject damageIndicatorPrefab;

	// Token: 0x040000F1 RID: 241
	[SerializeField]
	private GameObject damageIndicatorHealingPrefab;

	// Token: 0x040000F2 RID: 242
	[SerializeField]
	private SpriteRenderer spriteRenderer;

	// Token: 0x040000F3 RID: 243
	[SerializeField]
	private ParticleSystem destroyParticles;

	// Token: 0x040000F4 RID: 244
	[SerializeField]
	private List<Collider2D> colliders;

	// Token: 0x040000F5 RID: 245
	[SerializeField]
	public int maxHealth = 100;

	// Token: 0x040000F6 RID: 246
	[SerializeField]
	public int health = 100;

	// Token: 0x040000F7 RID: 247
	public Color startingColor;

	// Token: 0x040000F8 RID: 248
	private float timeSinceLastHit = 1f;

	// Token: 0x040000F9 RID: 249
	[SerializeField]
	private UnityEvent whenDestroyed;

	// Token: 0x040000FA RID: 250
	[SerializeField]
	private List<Destructible.OnHealthChangedEvent> onHealthChangedEvents;

	// Token: 0x040000FB RID: 251
	private bool isHighlighted;

	// Token: 0x040000FC RID: 252
	public bool isDestroyed;

	// Token: 0x040000FD RID: 253
	[SerializeField]
	private string takeDamageSFX;

	// Token: 0x040000FE RID: 254
	[SerializeField]
	private string destroySFX;

	// Token: 0x040000FF RID: 255
	public float armor;

	// Token: 0x020000D0 RID: 208
	[Serializable]
	private class OnHealthChangedEvent
	{
		// Token: 0x0400040F RID: 1039
		public int healthReached;

		// Token: 0x04000410 RID: 1040
		public UnityEvent onHealthChanged;

		// Token: 0x04000411 RID: 1041
		[NonSerialized]
		public bool hasBeenTriggered;
	}
}
