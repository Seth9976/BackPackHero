using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200000A RID: 10
public class Bullet : MonoBehaviour
{
	// Token: 0x0600002A RID: 42 RVA: 0x00002948 File Offset: 0x00000B48
	private void OnEnable()
	{
		Bullet.bullets.Add(this);
	}

	// Token: 0x0600002B RID: 43 RVA: 0x00002955 File Offset: 0x00000B55
	private void OnDisable()
	{
		Bullet.bullets.Remove(this);
	}

	// Token: 0x0600002C RID: 44 RVA: 0x00002963 File Offset: 0x00000B63
	private void Start()
	{
		this.currentSpawnTime = Random.Range(0f, this.spawnTime);
		this.currentTimeLerp = Random.Range(0.75f, 1.25f);
	}

	// Token: 0x0600002D RID: 45 RVA: 0x00002990 File Offset: 0x00000B90
	private void FixedUpdate()
	{
		float num = 1f;
		if (this.timeType == Bullet.TimeType.TimeManagerScaled)
		{
			num = TimeManager.instance.currentTimeScale;
		}
		else if (this.timeType == Bullet.TimeType.TimeManagerUnscaled)
		{
			num = 1f;
		}
		else if (this.timeType == Bullet.TimeType.temporaryUnscaled)
		{
			num = this.currentTimeLerp;
			this.currentTimeLerp = Mathf.Lerp(this.currentTimeLerp, TimeManager.instance.currentTimeScale, Time.fixedDeltaTime * 10f);
			if (Mathf.Abs(this.currentTimeLerp - TimeManager.instance.currentTimeScale) < 0.01f)
			{
				this.timeType = Bullet.TimeType.TimeManagerScaled;
			}
		}
		this.ConsiderSpawning(num);
		this.ConsiderShrink(num);
		this.ConsiderLifeTime(num);
		this.PullBulletTowadsPlayer(num);
		this.HomeTowardsEnemies(num);
		this.rb.velocity = this.movementVector * this.speed * num;
	}

	// Token: 0x0600002E RID: 46 RVA: 0x00002A68 File Offset: 0x00000C68
	private void ConsiderSpawning(float time)
	{
		if (this.objectToSpawn == null)
		{
			return;
		}
		this.currentSpawnTime += time * Time.deltaTime;
		if (this.currentSpawnTime >= this.spawnTime)
		{
			this.currentSpawnTime = 0f;
			Object.Instantiate<GameObject>(this.objectToSpawn, base.transform.position, Quaternion.identity);
		}
	}

	// Token: 0x0600002F RID: 47 RVA: 0x00002AD0 File Offset: 0x00000CD0
	private void ConsiderShrink(float time)
	{
		if (this.shrinkSpeed == 0f)
		{
			return;
		}
		base.transform.localScale = Vector3.MoveTowards(base.transform.localScale, Vector3.zero, this.shrinkSpeed * time * Time.deltaTime);
		if (base.transform.localScale.x < 0.1f)
		{
			this.DestroyBullet(base.transform.position);
		}
	}

	// Token: 0x06000030 RID: 48 RVA: 0x00002B48 File Offset: 0x00000D48
	private void ConsiderLifeTime(float time)
	{
		if (this.lifetime == float.PositiveInfinity)
		{
			return;
		}
		this.lifetime -= Time.deltaTime * time;
		if (this.lifetime <= 0f)
		{
			this.DestroyBullet(base.transform.position);
		}
	}

	// Token: 0x06000031 RID: 49 RVA: 0x00002B9C File Offset: 0x00000D9C
	private void HomeTowardsEnemies(float time)
	{
		if (!this.homesInOnEnemies)
		{
			return;
		}
		float num = 10f;
		Enemy closest = Enemy.GetClosest(base.transform.position, 12f);
		if (closest)
		{
			Vector2 vector = closest.transform.position - base.transform.position;
			this.movementVector = Vector2.Lerp(this.movementVector, vector.normalized, time * this.homingPower * num * Time.deltaTime).normalized;
			float num2 = Mathf.Atan2(this.movementVector.y, this.movementVector.x) * 57.29578f;
			base.transform.rotation = Quaternion.AngleAxis(num2, Vector3.forward);
		}
	}

	// Token: 0x06000032 RID: 50 RVA: 0x00002C70 File Offset: 0x00000E70
	private void PullBulletTowadsPlayer(float time)
	{
		if (this.enemyBullet)
		{
			return;
		}
		float modifierValue = ModifierManager.instance.GetModifierValue(Modifier.ModifierEffect.Type.Boomerang);
		if (modifierValue == 0f)
		{
			return;
		}
		Vector2 vector = this.movementVector * this.speed;
		Vector2 vector2 = Player.instance.transform.position - base.transform.position;
		float num = Mathf.Atan2(vector.y, vector.x) * 57.29578f;
		float num2 = Mathf.Atan2(vector2.y, vector2.x) * 57.29578f;
		num = Mathf.LerpAngle(num, num2, time * modifierValue * Time.deltaTime * 10f);
		vector = new Vector2(Mathf.Cos(num * 0.017453292f), Mathf.Sin(num * 0.017453292f));
		this.movementVector = vector.normalized;
		float num3 = Mathf.Atan2(this.movementVector.y, this.movementVector.x) * 57.29578f;
		base.transform.rotation = Quaternion.AngleAxis(num3, Vector3.forward);
	}

	// Token: 0x06000033 RID: 51 RVA: 0x00002D8C File Offset: 0x00000F8C
	public void SetVelocity(Vector2 velocity)
	{
		this.movementVector = velocity.normalized;
		float num = Mathf.Atan2(this.movementVector.y, this.movementVector.x) * 57.29578f;
		base.transform.rotation = Quaternion.AngleAxis(num, Vector3.forward);
	}

	// Token: 0x06000034 RID: 52 RVA: 0x00002DE0 File Offset: 0x00000FE0
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (this.isUsed)
		{
			return;
		}
		if (this.enemyBullet && other.gameObject.CompareTag("Player"))
		{
			return;
		}
		if (other.GetComponent<Destructible>())
		{
			this.damageDealer.ApplyEffects(other.gameObject);
			if (!this.penetratesForever)
			{
				ModifierManager.instance.GetAsymptoticModifierValue(Modifier.ModifierEffect.Type.PenetrationAsymptote);
				this.damageDealer.SetDamage(this.damageDealer.damage * ModifierManager.instance.GetAsymptoticModifierValue(Modifier.ModifierEffect.Type.PenetrationAsymptote) - 1f);
				if (this.damageDealer.damage <= 0f)
				{
					RaycastHit2D raycastHit2D = Physics2D.Raycast(base.transform.position, this.rb.velocity, 0.1f);
					this.DestroyBullet(raycastHit2D.collider ? raycastHit2D.point : (base.transform.position + this.rb.velocity.normalized * 0.1f));
					return;
				}
			}
		}
		else
		{
			if ((other.gameObject.layer == LayerMask.NameToLayer("Level") || other.gameObject.layer == LayerMask.NameToLayer("PortalForceField")) && (this.bouncesForever || ModifierManager.instance.GetModifierValue(Modifier.ModifierEffect.Type.StakesBounce) > (float)this.timesBounced))
			{
				this.timesBounced++;
				Vector2 vector = other.ClosestPoint(base.transform.position) - base.transform.position;
				vector.Normalize();
				this.movementVector = Vector2.Reflect(this.movementVector, vector);
				float num = Mathf.Atan2(this.movementVector.y, this.movementVector.x) * 57.29578f;
				base.transform.rotation = Quaternion.AngleAxis(num, Vector3.forward);
				return;
			}
			RaycastHit2D raycastHit2D2 = Physics2D.Raycast(base.transform.position, this.rb.velocity, 0.1f);
			this.DestroyBullet(raycastHit2D2.collider ? raycastHit2D2.point : (base.transform.position + this.rb.velocity.normalized * 0.1f));
		}
	}

	// Token: 0x06000035 RID: 53 RVA: 0x00003054 File Offset: 0x00001254
	private void DestroyBullet(Vector2 position)
	{
		SoundManager.instance.PlaySFX("bulletHit", double.PositiveInfinity);
		this.isUsed = true;
		if (this.bulletParticles == null)
		{
			Object.Destroy(base.gameObject);
			return;
		}
		this.bulletParticles.gameObject.transform.position = position;
		this.bulletParticles.gameObject.SetActive(true);
		this.bulletParticles.transform.SetParent(null);
		Object.Destroy(base.gameObject);
	}

	// Token: 0x04000017 RID: 23
	private static List<Bullet> bullets = new List<Bullet>();

	// Token: 0x04000018 RID: 24
	[SerializeField]
	private Bullet.TimeType timeType;

	// Token: 0x04000019 RID: 25
	private bool isUsed;

	// Token: 0x0400001A RID: 26
	[SerializeField]
	private EnemyBullet enemyBullet;

	// Token: 0x0400001B RID: 27
	[SerializeField]
	private DamageDealer damageDealer;

	// Token: 0x0400001C RID: 28
	[SerializeField]
	private GameObject bulletParticles;

	// Token: 0x0400001D RID: 29
	[SerializeField]
	private Rigidbody2D rb;

	// Token: 0x0400001E RID: 30
	[SerializeField]
	private float speed = 10f;

	// Token: 0x0400001F RID: 31
	[SerializeField]
	private float shrinkSpeed;

	// Token: 0x04000020 RID: 32
	[SerializeField]
	private Vector2 movementVector;

	// Token: 0x04000021 RID: 33
	[SerializeField]
	private float lifetime = float.PositiveInfinity;

	// Token: 0x04000022 RID: 34
	[SerializeField]
	private bool bouncesForever;

	// Token: 0x04000023 RID: 35
	[SerializeField]
	private bool penetratesForever;

	// Token: 0x04000024 RID: 36
	[SerializeField]
	private bool homesInOnEnemies;

	// Token: 0x04000025 RID: 37
	[SerializeField]
	private float homingPower = 1f;

	// Token: 0x04000026 RID: 38
	private float currentTimeLerp = 1f;

	// Token: 0x04000027 RID: 39
	private int timesBounced;

	// Token: 0x04000028 RID: 40
	[Header("-------------------------Spawning-------------------------")]
	[SerializeField]
	private GameObject objectToSpawn;

	// Token: 0x04000029 RID: 41
	[SerializeField]
	private float spawnTime = 0.25f;

	// Token: 0x0400002A RID: 42
	private float currentSpawnTime;

	// Token: 0x020000BB RID: 187
	public enum TimeType
	{
		// Token: 0x040003C5 RID: 965
		TimeManagerScaled,
		// Token: 0x040003C6 RID: 966
		TimeManagerUnscaled,
		// Token: 0x040003C7 RID: 967
		temporaryUnscaled
	}
}
