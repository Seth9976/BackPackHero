using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using SaveSystem.States;
using UnityEngine;

// Token: 0x0200002C RID: 44
public class Enemy : MonoBehaviour
{
	// Token: 0x0600014E RID: 334 RVA: 0x00007B26 File Offset: 0x00005D26
	private void OnEnable()
	{
		Enemy.enemies.Add(this);
		if (this.findPathCoroutine != null)
		{
			base.StopCoroutine(this.findPathCoroutine);
		}
		this.findPathCoroutine = base.StartCoroutine(this.FindPath());
	}

	// Token: 0x0600014F RID: 335 RVA: 0x00007B59 File Offset: 0x00005D59
	private void OnDisable()
	{
		Enemy.enemies.Remove(this);
		if (this.findPathCoroutine != null)
		{
			base.StopCoroutine(this.findPathCoroutine);
		}
		if (this.defeatMessageInstance)
		{
			Object.Destroy(this.defeatMessageInstance);
		}
	}

	// Token: 0x06000150 RID: 336 RVA: 0x00007B94 File Offset: 0x00005D94
	private void Start()
	{
		if (this.mustBeDefeated)
		{
			this.defeatMessageInstance = Object.Instantiate<GameObject>(this.defeatMessagePrefab, base.transform.position, Quaternion.identity, CanvasManager.instance.transform);
			this.defeatMessageInstance.transform.SetAsFirstSibling();
		}
		if (this.col)
		{
			this.startingRadius = this.col.radius;
		}
		else if (this.capsuleCollider2D)
		{
			this.startingCapsuleSize = this.capsuleCollider2D.size;
		}
		base.StartCoroutine(this.ExpandCollider());
	}

	// Token: 0x06000151 RID: 337 RVA: 0x00007C30 File Offset: 0x00005E30
	private void Update()
	{
		if (this.defeatMessageInstance)
		{
			if (this.defeatMessagePosition)
			{
				this.defeatMessageInstance.transform.position = Camera.main.WorldToScreenPoint(this.defeatMessagePosition.position);
			}
			else
			{
				this.defeatMessageInstance.transform.position = Camera.main.WorldToScreenPoint(base.transform.position + Vector3.up * 0.5f);
			}
		}
		Enemy.EntranceBehavior entranceBehavior = this.entranceBehavior;
		if (entranceBehavior != Enemy.EntranceBehavior.EnterLevel)
		{
			if (entranceBehavior == Enemy.EntranceBehavior.FollowBehavior)
			{
				this.FollowBehavior();
			}
		}
		else
		{
			this.EnterLevelBehavior();
		}
		this.DetermineDirection();
	}

	// Token: 0x06000152 RID: 338 RVA: 0x00007CDB File Offset: 0x00005EDB
	private IEnumerator ExpandCollider()
	{
		if (this.col)
		{
			this.col.radius = 0f;
		}
		else if (this.capsuleCollider2D)
		{
			this.capsuleCollider2D.size = Vector2.zero;
		}
		for (float i = 0f; i < 1f; i += Time.deltaTime)
		{
			if (this.col)
			{
				this.col.radius = this.startingRadius * i;
			}
			else if (this.capsuleCollider2D)
			{
				this.capsuleCollider2D.size = this.startingCapsuleSize * i;
			}
			yield return null;
		}
		yield break;
	}

	// Token: 0x06000153 RID: 339 RVA: 0x00007CEC File Offset: 0x00005EEC
	public static Enemy GetRandom(Vector2 origin, float radius)
	{
		List<Enemy> enemiesInRadius = Enemy.GetEnemiesInRadius(origin, radius);
		if (enemiesInRadius.Count == 0)
		{
			return null;
		}
		return enemiesInRadius[Random.Range(0, enemiesInRadius.Count)];
	}

	// Token: 0x06000154 RID: 340 RVA: 0x00007D1D File Offset: 0x00005F1D
	public static Enemy GetRandom()
	{
		if (Enemy.enemies.Count == 0)
		{
			return null;
		}
		return Enemy.enemies[Random.Range(0, Enemy.enemies.Count)];
	}

	// Token: 0x06000155 RID: 341 RVA: 0x00007D48 File Offset: 0x00005F48
	public static Enemy GetClosest(Vector2 pos, float closestDistance = float.PositiveInfinity)
	{
		Enemy enemy = null;
		foreach (Enemy enemy2 in Enemy.enemies)
		{
			if (enemy2)
			{
				float num = Vector2.Distance(enemy2.transform.position, pos);
				if (num < closestDistance)
				{
					enemy = enemy2;
					closestDistance = num;
				}
			}
		}
		return enemy;
	}

	// Token: 0x06000156 RID: 342 RVA: 0x00007DC0 File Offset: 0x00005FC0
	public static List<Enemy> GetEnemiesInRadius(Vector2 pos, float radius)
	{
		List<Enemy> list = new List<Enemy>();
		foreach (Enemy enemy in Enemy.enemies)
		{
			Vector2 vector = enemy.transform.position;
			if (enemy.capsuleCollider2D)
			{
				vector = enemy.capsuleCollider2D.ClosestPoint(pos);
			}
			if (enemy && Vector2.Distance(vector, pos) < radius)
			{
				list.Add(enemy);
			}
		}
		return list;
	}

	// Token: 0x06000157 RID: 343 RVA: 0x00007E58 File Offset: 0x00006058
	public static void KillAllEnemies()
	{
		foreach (Enemy enemy in new List<Enemy>(Enemy.enemies))
		{
			if (enemy)
			{
				Destructible component = enemy.GetComponent<Destructible>();
				if (!component)
				{
					Object.Destroy(enemy.gameObject);
				}
				component.SilentlyExplode();
			}
		}
	}

	// Token: 0x06000158 RID: 344 RVA: 0x00007ED0 File Offset: 0x000060D0
	public static bool AnyEnemiesAliveThatMustBeDefeated()
	{
		foreach (Enemy enemy in Enemy.enemies)
		{
			if (enemy && enemy.mustBeDefeated)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000159 RID: 345 RVA: 0x00007F34 File Offset: 0x00006134
	private void DetermineDirection()
	{
		if (this.status.GetStatusEffect(Status.StatusEffect.Type.Freeze))
		{
			return;
		}
		if (this.entranceBehavior == Enemy.EntranceBehavior.EnterLevel)
		{
			this.FaceTarget(Vector2.zero);
			return;
		}
		switch (this.behavior)
		{
		case Enemy.Behavior.ChargePlayer:
		case Enemy.Behavior.CirclePlayer:
			this.FaceTarget(Player.instance.transform.position);
			return;
		case Enemy.Behavior.BullCharge:
			if (this.delayTime > 0f)
			{
				this.FaceTarget(Player.instance.transform.position);
				return;
			}
			this.FaceTarget(base.transform.position + this.storedDirection);
			return;
		case Enemy.Behavior.RunAway:
			this.FaceTarget(this.currentDestination);
			return;
		default:
			return;
		}
	}

	// Token: 0x0600015A RID: 346 RVA: 0x00007FF8 File Offset: 0x000061F8
	private void FaceTarget(Vector2 target)
	{
		if (TimeManager.instance.currentTimeScale <= 0.1f)
		{
			return;
		}
		int num = (int)Mathf.Sign(target.x - base.transform.position.x);
		if (num != this.facingDirection)
		{
			this.facingDirection = num;
			this.timeFacingDirection = 0f;
			return;
		}
		this.timeFacingDirection += Time.deltaTime * TimeManager.instance.currentTimeScale;
		if (this.timeFacingDirection > 0.1f)
		{
			this.spriteRenderer.flipX = this.facingDirection < 0;
		}
	}

	// Token: 0x0600015B RID: 347 RVA: 0x00008090 File Offset: 0x00006290
	private void EnterLevelBehavior()
	{
		this.FollowPath();
		if (RoomManager.instance.currentRoom && RoomManager.instance.currentRoom.ComfortablyInsideRoom(base.transform.position))
		{
			this.entranceBehavior = Enemy.EntranceBehavior.FollowBehavior;
			switch (this.behavior)
			{
			case Enemy.Behavior.ChargePlayer:
				break;
			case Enemy.Behavior.BullCharge:
				this.delayTime = Random.Range(0f, 1.75f);
				break;
			case Enemy.Behavior.RunAway:
				if (this.currentDestination == Vector2.zero)
				{
					this.currentDestination = SpawnPoint.GetRandomSpawnPoint(SpawnPoint.SpawnType.Door, base.transform.position, 6f);
				}
				this.seeker.StartPath(base.transform.position, this.currentDestination, new OnPathDelegate(this.OnPathComplete));
				return;
			default:
				return;
			}
		}
	}

	// Token: 0x0600015C RID: 348 RVA: 0x00008174 File Offset: 0x00006374
	private void FollowBehavior()
	{
		if (HordeRemainingDisplay.instance.wonLevel)
		{
			this.velocityMovement.SetMovementVelocity(Vector2.zero);
			return;
		}
		ZombieBait closestZombieBaitNearBy = ZombieBait.GetClosestZombieBaitNearBy(base.transform.position, 5f);
		if (closestZombieBaitNearBy)
		{
			Vector2 normalized = (closestZombieBaitNearBy.transform.position - base.transform.position).normalized;
			this.velocityMovement.SetMovementVelocity(normalized * this.GetMovementSpeed());
			return;
		}
		if (this.enemyBehaviorOverride)
		{
			this.enemyBehaviorOverride.FollowBehavior();
			return;
		}
		switch (this.behavior)
		{
		case Enemy.Behavior.ChargePlayer:
			this.ChargePlayer();
			return;
		case Enemy.Behavior.BullCharge:
			this.BullCharge();
			return;
		case Enemy.Behavior.RunAway:
			this.FollowPath();
			this.ConsiderLeaving();
			return;
		case Enemy.Behavior.CirclePlayer:
			this.CirclePlayer();
			return;
		default:
			return;
		}
	}

	// Token: 0x0600015D RID: 349 RVA: 0x00008260 File Offset: 0x00006460
	private void CirclePlayer()
	{
		Vector2 normalized = (Player.instance.transform.position - base.transform.position).normalized;
		Vector2 vector = new Vector2(-normalized.y, normalized.x);
		vector *= this.movementDirection;
		this.velocityMovement.SetMovementVelocity(vector * this.GetMovementSpeed());
		this.timeAtMovementDirection += Time.deltaTime;
		if (!RoomManager.instance.currentRoom.ComfortablyInsideRoom(base.transform.position) && this.timeAtMovementDirection > 0.3f)
		{
			this.timeAtMovementDirection = 0f;
			this.movementDirection *= -1f;
		}
	}

	// Token: 0x0600015E RID: 350 RVA: 0x00008334 File Offset: 0x00006534
	private void ChargePlayer()
	{
		DefendableObject closestDefendableObject = DefendableObject.GetClosestDefendableObject(base.transform.position, Mathf.Min(3f, Vector2.Distance(base.transform.position, Player.instance.transform.position)));
		if (Vector2.Distance(base.transform.position, closestDefendableObject ? closestDefendableObject.transform.position : Player.instance.transform.position) < 3f)
		{
			this.MoveDirectlyTowardsPlayer(closestDefendableObject ? closestDefendableObject.transform.position : Player.instance.transform.position);
			return;
		}
		this.FollowPath();
	}

	// Token: 0x0600015F RID: 351 RVA: 0x00008405 File Offset: 0x00006605
	private void ConsiderLeaving()
	{
		if (Vector2.Distance(base.transform.position, this.currentDestination) < 1f)
		{
			Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06000160 RID: 352 RVA: 0x00008434 File Offset: 0x00006634
	private void BullCharge()
	{
		if (this.delayTime == float.PositiveInfinity)
		{
			this.delayTime = (float)Random.Range(2, 4);
		}
		if (this.delayTime > 0f)
		{
			this.animator.PlayAnimation("idle");
			this.delayTime -= Time.deltaTime;
			this.storedDirection = (Player.instance.transform.position - base.transform.position).normalized;
			this.velocityMovement.SetMovementVelocity(Vector2.zero);
			return;
		}
		this.animator.PlayAnimation("charge");
		this.velocityMovement.SetMovementVelocity(this.storedDirection * this.GetMovementSpeed() * 2f);
		if (!RoomManager.instance.currentRoom.ComfortablyInsideRoom(base.transform.position) && Vector2.Dot(this.storedDirection, RoomManager.instance.currentRoom.GetCenter() - base.transform.position) < 0.5f)
		{
			this.delayTime = Random.Range(0.25f, 0.75f);
		}
	}

	// Token: 0x06000161 RID: 353 RVA: 0x00008575 File Offset: 0x00006775
	private IEnumerator FindPath()
	{
		for (;;)
		{
			if (RoomManager.instance == null || Player.instance == null)
			{
				yield return new WaitForSeconds(0.5f);
			}
			else if (this.entranceBehavior == Enemy.EntranceBehavior.EnterLevel)
			{
				if (RoomManager.instance.currentRoom)
				{
					this.seeker.StartPath(base.transform.position, RoomManager.instance.currentRoom.GetCenter(), new OnPathDelegate(this.OnPathComplete));
				}
				yield return new WaitForSeconds(0.5f);
			}
			else if (this.enemyBehaviorOverride)
			{
				this.enemyBehaviorOverride.FindPath();
				yield return new WaitForSeconds(0.5f);
			}
			else
			{
				switch (this.behavior)
				{
				case Enemy.Behavior.ChargePlayer:
				{
					DefendableObject closestDefendableObject = DefendableObject.GetClosestDefendableObject(base.transform.position, Mathf.Min(3f, Vector2.Distance(base.transform.position, Player.instance.transform.position)));
					this.seeker.StartPath(base.transform.position, closestDefendableObject ? closestDefendableObject.transform.position : Player.instance.transform.position, new OnPathDelegate(this.OnPathComplete));
					break;
				}
				case Enemy.Behavior.RunAway:
					if (this.currentDestination == Vector2.zero)
					{
						this.currentDestination = SpawnPoint.GetRandomSpawnPoint(SpawnPoint.SpawnType.Door, base.transform.position, 6f);
					}
					this.seeker.StartPath(base.transform.position, this.currentDestination, new OnPathDelegate(this.OnPathComplete));
					break;
				}
				yield return new WaitForSeconds(0.5f);
			}
		}
		yield break;
	}

	// Token: 0x06000162 RID: 354 RVA: 0x00008584 File Offset: 0x00006784
	private void OnPathComplete(Path p)
	{
		if (p == null || p.error)
		{
			return;
		}
		this.path = p.vectorPath;
		this.currentWaypoint = 2;
	}

	// Token: 0x06000163 RID: 355 RVA: 0x000085A8 File Offset: 0x000067A8
	private void MoveDirectlyTowardsPlayer(Vector2 destination)
	{
		Vector2 normalized = (destination - base.transform.position).normalized;
		this.velocityMovement.SetMovementVelocity(normalized * this.GetMovementSpeed());
	}

	// Token: 0x06000164 RID: 356 RVA: 0x000085EC File Offset: 0x000067EC
	private void FollowPath()
	{
		if (this.path == null)
		{
			return;
		}
		Vector2 pathDirection = this.GetPathDirection();
		this.velocityMovement.SetMovementVelocity(pathDirection * this.GetMovementSpeed());
		this.ConsiderNextPathPoint();
	}

	// Token: 0x06000165 RID: 357 RVA: 0x00008628 File Offset: 0x00006828
	public void SpawnEnemies(int summons)
	{
		if (HordeRemainingDisplay.instance.wonLevel)
		{
			return;
		}
		for (int i = 0; i < summons; i++)
		{
			Object.Instantiate<GameObject>(this.summonPrefab, base.transform.position + Random.insideUnitSphere * 0.1f, Quaternion.identity).GetComponent<Enemy>();
		}
	}

	// Token: 0x06000166 RID: 358 RVA: 0x00008684 File Offset: 0x00006884
	public float GetMovementSpeed()
	{
		float num = this.movementSpeed;
		if (this.status.GetStatusEffect(Status.StatusEffect.Type.Poison))
		{
			num *= 0.25f;
		}
		if (this.status.GetStatusEffect(Status.StatusEffect.Type.Freeze))
		{
			num = 0f;
		}
		num *= RunTypeManager.instance.GetRunTypeModifierPercentage(RunType.RunProperty.RunPropertyType.FasterEnemyMovement);
		return num * this.GetTimeScale();
	}

	// Token: 0x06000167 RID: 359 RVA: 0x000086E2 File Offset: 0x000068E2
	public float GetTimeScale()
	{
		return TimeManager.instance.currentTimeScale;
	}

	// Token: 0x06000168 RID: 360 RVA: 0x000086EE File Offset: 0x000068EE
	public bool AtEndOfPath()
	{
		return this.currentWaypoint >= this.path.Count;
	}

	// Token: 0x06000169 RID: 361 RVA: 0x00008708 File Offset: 0x00006908
	public Vector2 GetPathDirection()
	{
		if (this.path == null || this.path.Count == 0)
		{
			return Vector2.zero;
		}
		this.currentWaypoint = Mathf.Clamp(this.currentWaypoint, 0, this.path.Count - 1);
		return (this.path[this.currentWaypoint] - base.transform.position).normalized;
	}

	// Token: 0x0600016A RID: 362 RVA: 0x00008784 File Offset: 0x00006984
	public void ConsiderNextPathPoint()
	{
		if (this.path == null || this.currentWaypoint >= this.path.Count)
		{
			return;
		}
		this.timeOnThisWaypoint += Time.deltaTime * this.GetTimeScale();
		if (Vector2.Distance(base.transform.position, this.path[this.currentWaypoint]) < 0.25f || this.timeOnThisWaypoint > 0.5f)
		{
			this.timeOnThisWaypoint = 0f;
			this.currentWaypoint++;
		}
	}

	// Token: 0x0600016B RID: 363 RVA: 0x00008820 File Offset: 0x00006A20
	public void DieEffects()
	{
		if (HordeRemainingDisplay.instance.wonLevel)
		{
			return;
		}
		if (this.givesXP)
		{
			XPManager.instance.AddXP(1);
		}
		PickUpSpawner.instance.CreatePickUp(base.transform.position);
		Singleton.instance.AddAccomplishment(ProgressState.Accomplishment.EnemiesKilled, 1);
		foreach (ProgressState.Accomplishment accomplishment in this.accomplishmentToAddOnKill)
		{
			Singleton.instance.AddAccomplishment(accomplishment, 1);
		}
		if (this.status.GetStatusEffect(Status.StatusEffect.Type.Burn) && ModifierManager.instance.GetModifierRandomRoll(Modifier.ModifierEffect.Type.EnemiesThatDieByFireExplodePercent))
		{
			Object.Instantiate<GameObject>(this.explosionPrefab, base.transform.position, Quaternion.identity);
		}
		if (this.status.GetStatusEffect(Status.StatusEffect.Type.Freeze) && ModifierManager.instance.GetModifierRandomRoll(Modifier.ModifierEffect.Type.FroznEnemiesHaveAChanceToExplodeIntoIce))
		{
			for (int i = 0; i < 6; i++)
			{
				float num = (float)i * 3.1415927f / 3f;
				Vector2 vector = new Vector2(Mathf.Cos(num), Mathf.Sin(num));
				this.CreateFrozen(base.transform.position, vector);
			}
		}
	}

	// Token: 0x0600016C RID: 364 RVA: 0x00008964 File Offset: 0x00006B64
	private void CreateFrozen(Vector2 position, Vector2 direction)
	{
		Object.Instantiate<GameObject>(this.frozenEffectPrefab, position, Quaternion.identity).GetComponent<Bullet>().SetVelocity(direction);
	}

	// Token: 0x0600016D RID: 365 RVA: 0x00008987 File Offset: 0x00006B87
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (this.behavior == Enemy.Behavior.BullCharge && collision.gameObject.layer == LayerMask.NameToLayer("Level"))
		{
			this.delayTime = Random.Range(0f, 1.75f);
		}
	}

	// Token: 0x0600016E RID: 366 RVA: 0x000089C0 File Offset: 0x00006BC0
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			PlayerHitBox component = other.GetComponent<PlayerHitBox>();
			if (component)
			{
				component.AddEnemy(base.gameObject);
				return;
			}
		}
		else if (other.CompareTag("Defendable"))
		{
			PlayerHitBox component2 = other.GetComponent<PlayerHitBox>();
			if (component2)
			{
				component2.AddEnemy(base.gameObject);
			}
		}
	}

	// Token: 0x0600016F RID: 367 RVA: 0x00008A20 File Offset: 0x00006C20
	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			PlayerHitBox component = other.GetComponent<PlayerHitBox>();
			if (component)
			{
				component.RemoveEnemy(base.gameObject);
				return;
			}
		}
		else if (other.CompareTag("Defendable"))
		{
			PlayerHitBox component2 = other.GetComponent<PlayerHitBox>();
			if (component2)
			{
				component2.RemoveEnemy(base.gameObject);
			}
		}
	}

	// Token: 0x04000105 RID: 261
	public static List<Enemy> enemies = new List<Enemy>();

	// Token: 0x04000106 RID: 262
	[SerializeField]
	public List<ProgressState.Accomplishment> accomplishmentToAddOnKill = new List<ProgressState.Accomplishment>();

	// Token: 0x04000107 RID: 263
	[Header("--------------------Settings--------------------")]
	[SerializeField]
	private Enemy.EntranceBehavior entranceBehavior;

	// Token: 0x04000108 RID: 264
	public Enemy.Behavior behavior;

	// Token: 0x04000109 RID: 265
	[SerializeField]
	private EnemyBehavior enemyBehaviorOverride;

	// Token: 0x0400010A RID: 266
	[SerializeField]
	private GameObject summonPrefab;

	// Token: 0x0400010B RID: 267
	[SerializeField]
	private bool mustBeDefeated;

	// Token: 0x0400010C RID: 268
	[SerializeField]
	private bool givesXP = true;

	// Token: 0x0400010D RID: 269
	[SerializeField]
	public float movementSpeed = 5f;

	// Token: 0x0400010E RID: 270
	[Header("--------------------Effect References--------------------")]
	[SerializeField]
	private GameObject explosionPrefab;

	// Token: 0x0400010F RID: 271
	[SerializeField]
	private GameObject frozenEffectPrefab;

	// Token: 0x04000110 RID: 272
	[Header("--------------------References--------------------")]
	[SerializeField]
	private VelocityMovement velocityMovement;

	// Token: 0x04000111 RID: 273
	[SerializeField]
	private Rigidbody2D rb;

	// Token: 0x04000112 RID: 274
	[SerializeField]
	private Seeker seeker;

	// Token: 0x04000113 RID: 275
	[SerializeField]
	public List<Vector3> path;

	// Token: 0x04000114 RID: 276
	[SerializeField]
	private Status status;

	// Token: 0x04000115 RID: 277
	[SerializeField]
	private SimpleAnimator animator;

	// Token: 0x04000116 RID: 278
	[SerializeField]
	private SpriteRenderer spriteRenderer;

	// Token: 0x04000117 RID: 279
	[SerializeField]
	private CircleCollider2D col;

	// Token: 0x04000118 RID: 280
	[SerializeField]
	private CapsuleCollider2D capsuleCollider2D;

	// Token: 0x04000119 RID: 281
	private Coroutine findPathCoroutine;

	// Token: 0x0400011A RID: 282
	[SerializeField]
	public int currentWaypoint;

	// Token: 0x0400011B RID: 283
	[SerializeField]
	private GameObject defeatMessagePrefab;

	// Token: 0x0400011C RID: 284
	[SerializeField]
	private Transform defeatMessagePosition;

	// Token: 0x0400011D RID: 285
	private GameObject defeatMessageInstance;

	// Token: 0x0400011E RID: 286
	private float timeOnThisWaypoint;

	// Token: 0x0400011F RID: 287
	private int facingDirection = 1;

	// Token: 0x04000120 RID: 288
	private float timeFacingDirection;

	// Token: 0x04000121 RID: 289
	private float movementDirection = 1f;

	// Token: 0x04000122 RID: 290
	private float timeAtMovementDirection;

	// Token: 0x04000123 RID: 291
	private float delayTime = float.PositiveInfinity;

	// Token: 0x04000124 RID: 292
	private Vector2 storedDirection;

	// Token: 0x04000125 RID: 293
	private Vector2 currentDestination;

	// Token: 0x04000126 RID: 294
	private float startingRadius;

	// Token: 0x04000127 RID: 295
	private Vector2 startingCapsuleSize;

	// Token: 0x020000D3 RID: 211
	private enum EntranceBehavior
	{
		// Token: 0x0400041B RID: 1051
		EnterLevel,
		// Token: 0x0400041C RID: 1052
		FollowBehavior
	}

	// Token: 0x020000D4 RID: 212
	public enum Behavior
	{
		// Token: 0x0400041E RID: 1054
		ChargePlayer,
		// Token: 0x0400041F RID: 1055
		BullCharge,
		// Token: 0x04000420 RID: 1056
		RunAway,
		// Token: 0x04000421 RID: 1057
		CirclePlayer
	}
}
