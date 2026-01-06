using System;
using Pathfinding;
using UnityEngine;

// Token: 0x0200002E RID: 46
public class EnemyBehavior_Siren : EnemyBehavior
{
	// Token: 0x0600017D RID: 381 RVA: 0x00008B24 File Offset: 0x00006D24
	public override void FindPath()
	{
		if (this.findNextPointTimer > 1f)
		{
			this.seeker.StartPath(this.rb.position, RoomManager.instance.currentRoom.GetRandomPointComfortablyInsideRoom(), new OnPathDelegate(base.OnPathComplete));
		}
	}

	// Token: 0x0600017E RID: 382 RVA: 0x00008B7C File Offset: 0x00006D7C
	public override void FollowBehavior()
	{
		Debug.Log("FollowBehavior");
		if (this.cooldown > 0f)
		{
			this.cooldown -= Time.deltaTime * this.enemy.GetMovementSpeed();
			this.rb.velocity = Vector2.zero;
			if ((double)this.cooldown <= 0.5 && !this.startedSing)
			{
				this.startedSing = true;
				this.screamEffect.Play();
				this.animator.PlayAnimation("sing");
				SoundManager.instance.PlaySFX("siren", double.PositiveInfinity);
			}
			if (this.cooldown <= 0f)
			{
				this.SummonEnemies();
			}
			return;
		}
		this.screamEffect.Stop(true, ParticleSystemStopBehavior.StopEmitting);
		this.startedSing = false;
		this.animator.PlayAnimation("run");
		this.rb.velocity = this.enemy.GetPathDirection() * this.enemy.GetMovementSpeed();
		this.enemy.ConsiderNextPathPoint();
		if (this.enemy.AtEndOfPath())
		{
			this.findNextPointTimer += Time.deltaTime * this.enemy.GetMovementSpeed();
		}
		else
		{
			this.findNextPointTimer = 0f;
		}
		if (this.timeToNextScream > this.screamInterval)
		{
			this.timeToNextScream = 0f;
			this.cooldown = 3f;
			this.animator.PlayAnimation("charge");
			return;
		}
		this.timeToNextScream += Time.deltaTime * this.enemy.GetMovementSpeed();
	}

	// Token: 0x0600017F RID: 383 RVA: 0x00008D1C File Offset: 0x00006F1C
	public void SummonEnemies()
	{
		for (int i = 0; i < this.enmiesToSpawn; i++)
		{
			if (Enemy.enemies.Count > 100)
			{
				return;
			}
			Vector2 randomSpawnPoint = SpawnPoint.GetRandomSpawnPoint(SpawnPoint.SpawnType.Door);
			Object.Instantiate<GameObject>(this.enemiesToSpawn[Random.Range(0, this.enemiesToSpawn.Length)], randomSpawnPoint, Quaternion.identity).GetComponent<Enemy>();
		}
	}

	// Token: 0x0400012F RID: 303
	[SerializeField]
	private SpriteRenderer forcefieldSprite;

	// Token: 0x04000130 RID: 304
	[SerializeField]
	private GameObject[] enemiesToSpawn;

	// Token: 0x04000131 RID: 305
	[SerializeField]
	private ParticleSystem screamEffect;

	// Token: 0x04000132 RID: 306
	private int enmiesToSpawn = 6;

	// Token: 0x04000133 RID: 307
	private float findNextPointTimer;

	// Token: 0x04000134 RID: 308
	private float timeToNextScream;

	// Token: 0x04000135 RID: 309
	private float screamInterval = 3.5f;

	// Token: 0x04000136 RID: 310
	private float cooldown;

	// Token: 0x04000137 RID: 311
	private bool startedSing;
}
