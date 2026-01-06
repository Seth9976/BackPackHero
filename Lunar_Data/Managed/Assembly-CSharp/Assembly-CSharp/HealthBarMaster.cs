using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200004A RID: 74
public class HealthBarMaster : MonoBehaviour
{
	// Token: 0x06000215 RID: 533 RVA: 0x0000AEC9 File Offset: 0x000090C9
	private void OnEnable()
	{
		if (HealthBarMaster.instance == null)
		{
			HealthBarMaster.instance = this;
		}
	}

	// Token: 0x06000216 RID: 534 RVA: 0x0000AEDE File Offset: 0x000090DE
	private void OnDisable()
	{
		if (HealthBarMaster.instance == this)
		{
			HealthBarMaster.instance = null;
		}
	}

	// Token: 0x06000217 RID: 535 RVA: 0x0000AEF3 File Offset: 0x000090F3
	private void Start()
	{
	}

	// Token: 0x06000218 RID: 536 RVA: 0x0000AEF5 File Offset: 0x000090F5
	private void Update()
	{
		this.displayedHealth = Mathf.Lerp(this.displayedHealth, this.health, Time.deltaTime * 5f);
		this.healthSlider.value = this.displayedHealth / this.maxHealth;
	}

	// Token: 0x06000219 RID: 537 RVA: 0x0000AF31 File Offset: 0x00009131
	public float GetHealthPercentage()
	{
		return this.health / this.maxHealth;
	}

	// Token: 0x0600021A RID: 538 RVA: 0x0000AF40 File Offset: 0x00009140
	public bool IsFullHealth()
	{
		return this.health >= this.maxHealth;
	}

	// Token: 0x0600021B RID: 539 RVA: 0x0000AF54 File Offset: 0x00009154
	public void Heal(float amount)
	{
		if (this.health <= 0f || Player.instance.isDead)
		{
			return;
		}
		this.health += amount;
		if (this.health > this.maxHealth)
		{
			this.health = this.maxHealth;
		}
	}

	// Token: 0x0600021C RID: 540 RVA: 0x0000AFA4 File Offset: 0x000091A4
	public void TakeDamage(float damage)
	{
		this.health -= damage;
		if (this.health <= 0f)
		{
			SoundManager.instance.PlaySFX("gameOver", double.PositiveInfinity);
			this.health = 0f;
			Player.instance.Die();
		}
	}

	// Token: 0x0600021D RID: 541 RVA: 0x0000AFF9 File Offset: 0x000091F9
	public bool IsMostlyHealed()
	{
		return this.health >= this.maxHealth * 0.9f;
	}

	// Token: 0x04000197 RID: 407
	public static HealthBarMaster instance;

	// Token: 0x04000198 RID: 408
	[SerializeField]
	private float health = 100f;

	// Token: 0x04000199 RID: 409
	private float displayedHealth = 100f;

	// Token: 0x0400019A RID: 410
	[SerializeField]
	private float maxHealth = 100f;

	// Token: 0x0400019B RID: 411
	[SerializeField]
	private Slider healthSlider;
}
