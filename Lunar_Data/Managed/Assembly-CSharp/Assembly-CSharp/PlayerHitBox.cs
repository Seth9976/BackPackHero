using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000070 RID: 112
public class PlayerHitBox : MonoBehaviour
{
	// Token: 0x06000334 RID: 820 RVA: 0x00010241 File Offset: 0x0000E441
	public void AddEnemy(GameObject enemy)
	{
		this.enemiesHitting.Add(enemy);
	}

	// Token: 0x06000335 RID: 821 RVA: 0x0001024F File Offset: 0x0000E44F
	public void RemoveEnemy(GameObject enemy)
	{
		this.enemiesHitting.Remove(enemy);
	}

	// Token: 0x06000336 RID: 822 RVA: 0x0001025E File Offset: 0x0000E45E
	private void Start()
	{
	}

	// Token: 0x06000337 RID: 823 RVA: 0x00010260 File Offset: 0x0000E460
	private void Update()
	{
		for (int i = 0; i < this.enemiesHitting.Count; i++)
		{
			if (this.enemiesHitting[i] == null || !this.enemiesHitting[i].activeInHierarchy)
			{
				this.enemiesHitting.RemoveAt(i);
				i--;
			}
		}
		List<GameObject> list = new List<GameObject>();
		foreach (GameObject gameObject in this.enemiesHitting)
		{
			Status component = gameObject.GetComponent<Status>();
			if (!component)
			{
				list.Add(gameObject);
			}
			if (component && !component.GetStatusEffect(Status.StatusEffect.Type.Freeze))
			{
				list.Add(gameObject);
			}
		}
		if (list.Count > 0 && !Player.instance.isDead && !HordeRemainingDisplay.instance.wonLevel && !Player.instance.isWinning)
		{
			PlayerHitBox.Damage damage = this.damageType;
			if (damage == PlayerHitBox.Damage.Player)
			{
				this.spriteRenderer.color = Color.red;
				HealthBarMaster.instance.TakeDamage(Time.deltaTime * this.GetDamageAmount(list) * 0.25f * TimeManager.instance.currentTimeScale);
				if (this.timeSinceLastHurtSFX > 0.15f && TimeManager.instance.setTimeScale > 0f)
				{
					SoundManager.instance.PlaySFX("hurt", double.PositiveInfinity);
					this.timeSinceLastHurtSFX = 0f;
				}
				this.timeSinceLastHurtSFX += Time.deltaTime;
				return;
			}
			if (damage != PlayerHitBox.Damage.DefendableObject)
			{
				return;
			}
			this.spriteRenderer.color = Color.red;
			if (this.defendableObject)
			{
				this.defendableObject.TakeDamage(Time.deltaTime * this.GetDamageAmount(list) * 0.25f * TimeManager.instance.currentTimeScale);
				return;
			}
		}
		else
		{
			this.spriteRenderer.color = Color.white;
			this.timeSinceLastHurtSFX = 99999f;
		}
	}

	// Token: 0x06000338 RID: 824 RVA: 0x00010474 File Offset: 0x0000E674
	private float GetDamageAmount(List<GameObject> enemiesHitting)
	{
		float num = 0f;
		foreach (GameObject gameObject in enemiesHitting)
		{
			float num2 = Vector2.Distance(gameObject.transform.position, base.transform.position);
			num2 = Mathf.Max(0.1f, num2);
			num += 50f / num2;
		}
		num = Mathf.Clamp(num, 0f, 200f);
		return num;
	}

	// Token: 0x0400026B RID: 619
	[SerializeField]
	private PlayerHitBox.Damage damageType;

	// Token: 0x0400026C RID: 620
	[SerializeField]
	private DefendableObject defendableObject;

	// Token: 0x0400026D RID: 621
	[SerializeField]
	private SpriteRenderer spriteRenderer;

	// Token: 0x0400026E RID: 622
	[SerializeField]
	private List<GameObject> enemiesHitting = new List<GameObject>();

	// Token: 0x0400026F RID: 623
	private float timeSinceLastHurtSFX = 9999f;

	// Token: 0x020000FA RID: 250
	public enum Damage
	{
		// Token: 0x040004A0 RID: 1184
		Player,
		// Token: 0x040004A1 RID: 1185
		DefendableObject
	}
}
