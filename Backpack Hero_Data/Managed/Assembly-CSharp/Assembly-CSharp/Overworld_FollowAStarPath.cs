using System;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

// Token: 0x02000141 RID: 321
public class Overworld_FollowAStarPath : MonoBehaviour
{
	// Token: 0x1700001E RID: 30
	// (get) Token: 0x06000C48 RID: 3144 RVA: 0x0007ED74 File Offset: 0x0007CF74
	// (set) Token: 0x06000C49 RID: 3145 RVA: 0x0007ED7C File Offset: 0x0007CF7C
	public Path path { get; private set; }

	// Token: 0x06000C4A RID: 3146 RVA: 0x0007ED85 File Offset: 0x0007CF85
	private void OnDestroy()
	{
		if (this.target && this.target.gameObject)
		{
			Object.Destroy(this.target.gameObject);
		}
	}

	// Token: 0x06000C4B RID: 3147 RVA: 0x0007EDB6 File Offset: 0x0007CFB6
	private void Start()
	{
		this.MakeReferences();
		this.thisNPC = base.GetComponent<Overworld_NPC>();
	}

	// Token: 0x06000C4C RID: 3148 RVA: 0x0007EDCA File Offset: 0x0007CFCA
	private void Update()
	{
		this.ConsiderIfCloseEnough();
		this.FollowPath();
	}

	// Token: 0x06000C4D RID: 3149 RVA: 0x0007EDD8 File Offset: 0x0007CFD8
	public bool IsMoving()
	{
		return this.path != null && this.currentWaypoint < this.path.vectorPath.Count && this.path.vectorPath.Count > 0;
	}

	// Token: 0x06000C4E RID: 3150 RVA: 0x0007EE10 File Offset: 0x0007D010
	private void ConsiderIfCloseEnough()
	{
		if (!this.destination_Overworld_InteractiveObject)
		{
			return;
		}
		if (Vector2.Distance(base.transform.position + this.offset, this.destination_Overworld_InteractiveObject.transform.position) < this.destination_Overworld_InteractiveObject.interactionRadius)
		{
			this.EndMove();
		}
	}

	// Token: 0x06000C4F RID: 3151 RVA: 0x0007EE78 File Offset: 0x0007D078
	public static void DisableAll()
	{
		Overworld_FollowAStarPath[] array = Object.FindObjectsOfType<Overworld_FollowAStarPath>();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].enabled = false;
		}
	}

	// Token: 0x06000C50 RID: 3152 RVA: 0x0007EEA4 File Offset: 0x0007D0A4
	public static void EnableAll()
	{
		Overworld_Manager.main.UpdateMap();
		Overworld_FollowAStarPath[] array = Object.FindObjectsOfType<Overworld_FollowAStarPath>();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].enabled = true;
		}
	}

	// Token: 0x06000C51 RID: 3153 RVA: 0x0007EED8 File Offset: 0x0007D0D8
	private void MakeReferences()
	{
		if (!this.collider)
		{
			this.collider = base.GetComponentInChildren<Collider2D>();
		}
		if (!this.animator)
		{
			this.animator = base.GetComponentInChildren<Animator>();
		}
		if (!this.overworld_NPC_CostumeSelector)
		{
			this.overworld_NPC_CostumeSelector = base.GetComponentInChildren<Overworld_NPC_CostumeSelector>();
		}
		if (!this.seeker)
		{
			this.seeker = base.GetComponentInChildren<Seeker>();
		}
	}

	// Token: 0x06000C52 RID: 3154 RVA: 0x0007EF49 File Offset: 0x0007D149
	public void SetSpeed(float speed)
	{
		this.speed = speed;
	}

	// Token: 0x06000C53 RID: 3155 RVA: 0x0007EF52 File Offset: 0x0007D152
	public void ChangeSpeed(float speed)
	{
		this.speed += speed;
	}

	// Token: 0x06000C54 RID: 3156 RVA: 0x0007EF62 File Offset: 0x0007D162
	public void SetInteraction(Overworld_InteractiveObject x)
	{
		this.destination_Overworld_InteractiveObject = x;
	}

	// Token: 0x06000C55 RID: 3157 RVA: 0x0007EF6B File Offset: 0x0007D16B
	public void SetNewPath(Vector2 destination, int earlyEnd = 0)
	{
		if (!this.target)
		{
			return;
		}
		this.earlyEnd = earlyEnd;
		this.target.position = destination;
		this.UpdatePath();
	}

	// Token: 0x06000C56 RID: 3158 RVA: 0x0007EF99 File Offset: 0x0007D199
	public void ResetEarlyEnd()
	{
		this.earlyEnd = 0;
	}

	// Token: 0x06000C57 RID: 3159 RVA: 0x0007EFA4 File Offset: 0x0007D1A4
	public void UpdatePath()
	{
		this.MakeReferences();
		if (this.thisNPC && this.thisNPC.isWrecked)
		{
			return;
		}
		this.doneSearch = false;
		this.pathSuccess = false;
		this.reachedDestination = false;
		this.path = null;
		this.skippedPoints = 0;
		if (this.seeker.IsDone())
		{
			this.seeker.StartPath(base.transform.position + this.offset, this.target.position, new OnPathDelegate(this.OnPathComplete));
		}
	}

	// Token: 0x06000C58 RID: 3160 RVA: 0x0007F040 File Offset: 0x0007D240
	public void FindPathwayComplete(Vector2 dest)
	{
		this.earlyEnd = 0;
		this.target.position = dest;
		this.destination = dest;
		this.doneSearch = false;
		this.pathSuccess = false;
		if (this.seeker.IsDone())
		{
			this.seeker.StartPath(base.transform.position + this.offset, this.target.position, new OnPathDelegate(this.OnPathBoolComplete));
		}
	}

	// Token: 0x06000C59 RID: 3161 RVA: 0x0007F0C8 File Offset: 0x0007D2C8
	private void OnPathBoolComplete(Path p)
	{
		this.doneSearch = true;
		if (Vector2.Distance(p.vectorPath[p.vectorPath.Count - 1], this.destination) < 1.5f)
		{
			this.pathSuccess = true;
			return;
		}
		this.pathSuccess = false;
	}

	// Token: 0x06000C5A RID: 3162 RVA: 0x0007F11C File Offset: 0x0007D31C
	private void OnPathComplete(Path p)
	{
		this.doneSearch = true;
		if (!p.error)
		{
			if (this.storeVecs)
			{
				this.vecs = new List<Vector3>(p.vectorPath);
			}
			uint num = 0U;
			for (int i = 0; i < p.vectorPath.Count - 1; i++)
			{
				num += AstarPath.active.GetNearest(p.vectorPath[i]).node.Penalty;
			}
			this.currentWaypoint = 0;
			this.path = p;
			this.timeSinceLastDirectionChange = 0f;
			Overworld_NPC component = base.GetComponent<Overworld_NPC>();
			if (component)
			{
				component.ChangePhase(Overworld_NPC.Phase.Moving);
				return;
			}
		}
		else
		{
			Overworld_NPC component2 = base.GetComponent<Overworld_NPC>();
			if (component2)
			{
				component2.backupDestinations.RemoveAt(0);
				if (component2.backupDestinations.Count > 0)
				{
					OVerworld_NPCDestination overworld_NPCDestination = component2.backupDestinations[0];
					component2.SetCurrentDestination(component2.backupDestinations[0]);
					this.target.position = overworld_NPCDestination.transform.position - this.offset + Random.insideUnitCircle * overworld_NPCDestination.radius;
					this.UpdatePath();
				}
			}
		}
	}

	// Token: 0x06000C5B RID: 3163 RVA: 0x0007F258 File Offset: 0x0007D458
	private void FollowPath()
	{
		if (this.path == null)
		{
			return;
		}
		if (this.currentWaypoint >= this.path.vectorPath.Count - this.earlyEnd)
		{
			this.EndMove();
			return;
		}
		if (this.thisNPC && this.thisNPC.isWrecked)
		{
			return;
		}
		Vector2 vector = this.path.vectorPath[this.currentWaypoint];
		Vector2 vector2 = (vector - (base.transform.position + this.offset)).normalized * this.speed * Time.deltaTime;
		this.timeOnThisWayPoint += Time.deltaTime;
		if (this.timeOnThisWayPoint > 1.5f)
		{
			this.skippedPoints++;
			this.timeOnThisWayPoint = 0f;
			this.currentWaypoint++;
			if (this.currentWaypoint >= this.path.vectorPath.Count)
			{
				this.EndMove();
				return;
			}
			if (this.skippedPoints >= 2)
			{
				this.EndMove();
				return;
			}
		}
		if (this.timeSinceLastDirectionChange >= 0.1f)
		{
			if (this.animator)
			{
				if (Mathf.Abs(vector2.x) > Mathf.Abs(vector2.y + 0.01f))
				{
					if (vector2.x > 0f)
					{
						this.animator.Play("move_right");
					}
					else
					{
						this.animator.Play("move_left");
					}
					if (this.playSFX)
					{
						SoundManager.main.PlaySFXPitched("footstep", Random.Range(0.9f, 1.1f), 0.4f, false);
					}
				}
				else
				{
					if (vector2.y > 0f)
					{
						this.animator.Play("move_up");
					}
					else
					{
						this.animator.Play("move_down");
					}
					if (this.playSFX)
					{
						SoundManager.main.PlaySFXPitched("footstep", Random.Range(0.9f, 1.1f), 0.4f, false);
					}
				}
			}
			else if (this.overworld_NPC_CostumeSelector)
			{
				if (Mathf.Abs(vector2.x) > Mathf.Abs(vector2.y + 0.01f))
				{
					if (vector2.x > 0f)
					{
						this.overworld_NPC_CostumeSelector.SetMovement(npcOutfit.Direction.Right, npcOutfit.Animation.Walk);
					}
					else
					{
						this.overworld_NPC_CostumeSelector.SetMovement(npcOutfit.Direction.Left, npcOutfit.Animation.Walk);
					}
				}
				else if (vector2.y > 0f)
				{
					this.overworld_NPC_CostumeSelector.SetMovement(npcOutfit.Direction.Back, npcOutfit.Animation.Walk);
				}
				else
				{
					this.overworld_NPC_CostumeSelector.SetMovement(npcOutfit.Direction.Front, npcOutfit.Animation.Walk);
				}
			}
			this.timeSinceLastDirectionChange = 0f;
		}
		this.timeSinceLastDirectionChange += Time.deltaTime;
		float num = 0f;
		if (this.path != null && this.currentWaypoint < this.path.vectorPath.Count)
		{
			num = Vector2.Distance(base.transform.position + this.offset, vector);
		}
		base.transform.position += vector2;
		if (num < 0.25f)
		{
			this.skippedPoints = 0;
			this.timeOnThisWayPoint = 0f;
			this.currentWaypoint++;
			if (this.currentWaypoint >= this.path.vectorPath.Count)
			{
				this.EndMove();
			}
		}
	}

	// Token: 0x06000C5C RID: 3164 RVA: 0x0007F5CC File Offset: 0x0007D7CC
	private void OnCollisionEnter2D(Collision2D other)
	{
		if (this.destination_Overworld_InteractiveObject && this.destination_Overworld_InteractiveObject.gameObject == other.gameObject)
		{
			this.EndMove();
			return;
		}
		if (!this.allowToBePushedOffPath)
		{
			return;
		}
		if (this.path != null && this.currentWaypoint < this.path.vectorPath.Count)
		{
			Vector2 vector = other.contacts[0].normal;
			Vector2 normalized = (this.path.vectorPath[this.currentWaypoint] - (base.transform.position + this.offset)).normalized;
			if (Vector2.Distance(vector, normalized) < 0.1f || Vector2.Distance(vector, -normalized) < 0.1f)
			{
				vector = Quaternion.Euler(0f, 0f, -90f) * vector;
			}
			List<Vector3> vectorPath = this.path.vectorPath;
			int num = this.currentWaypoint;
			vectorPath[num] += vector * 0.5f;
		}
	}

	// Token: 0x06000C5D RID: 3165 RVA: 0x0007F70C File Offset: 0x0007D90C
	public void EndMove()
	{
		if (this.destination_Overworld_InteractiveObject)
		{
			this.destination_Overworld_InteractiveObject.Interact();
			this.destination_Overworld_InteractiveObject = null;
		}
		Overworld_Purse component = base.GetComponent<Overworld_Purse>();
		if (component)
		{
			component.EndMove();
		}
		this.reachedDestination = true;
		this.IdleAnimation();
		this.path = null;
	}

	// Token: 0x06000C5E RID: 3166 RVA: 0x0007F761 File Offset: 0x0007D961
	public void IdleAnimation()
	{
		if (this.animator)
		{
			this.animator.SetTrigger("idle");
			return;
		}
		if (this.overworld_NPC_CostumeSelector)
		{
			this.overworld_NPC_CostumeSelector.SetMovement(npcOutfit.Animation.Idle);
		}
	}

	// Token: 0x06000C5F RID: 3167 RVA: 0x0007F79C File Offset: 0x0007D99C
	public void FaceTowards(Vector3 target)
	{
		if (this.thisNPC && this.thisNPC.isWrecked)
		{
			return;
		}
		Vector3 vector = target - base.transform.position;
		if (!this.animator)
		{
			if (this.overworld_NPC_CostumeSelector)
			{
				if (Mathf.Abs(vector.x) > Mathf.Abs(vector.y))
				{
					if (vector.x > 0f)
					{
						this.overworld_NPC_CostumeSelector.SetMovement(npcOutfit.Direction.Right, npcOutfit.Animation.Idle);
						return;
					}
					this.overworld_NPC_CostumeSelector.SetMovement(npcOutfit.Direction.Left, npcOutfit.Animation.Idle);
					return;
				}
				else
				{
					if (vector.y > 0f)
					{
						this.overworld_NPC_CostumeSelector.SetMovement(npcOutfit.Direction.Back, npcOutfit.Animation.Idle);
						return;
					}
					this.overworld_NPC_CostumeSelector.SetMovement(npcOutfit.Direction.Front, npcOutfit.Animation.Idle);
				}
			}
			return;
		}
		if (Mathf.Abs(vector.x) > Mathf.Abs(vector.y))
		{
			if (vector.x > 0f)
			{
				this.animator.Play("idle_right");
				return;
			}
			this.animator.Play("idle_left");
			return;
		}
		else
		{
			if (vector.y > 0f)
			{
				this.animator.Play("idle_up");
				return;
			}
			this.animator.Play("idle_down");
			return;
		}
	}

	// Token: 0x040009ED RID: 2541
	[SerializeField]
	private List<Vector3> vecs;

	// Token: 0x040009EE RID: 2542
	[SerializeField]
	private bool storeVecs;

	// Token: 0x040009EF RID: 2543
	[SerializeField]
	private bool playSFX;

	// Token: 0x040009F0 RID: 2544
	[SerializeField]
	private Transform target;

	// Token: 0x040009F1 RID: 2545
	[SerializeField]
	public float speed = 200f;

	// Token: 0x040009F3 RID: 2547
	private Seeker seeker;

	// Token: 0x040009F4 RID: 2548
	private float timeOnThisWayPoint;

	// Token: 0x040009F5 RID: 2549
	private bool reachedEndOfPath;

	// Token: 0x040009F6 RID: 2550
	private int currentWaypoint;

	// Token: 0x040009F7 RID: 2551
	private int earlyEnd;

	// Token: 0x040009F8 RID: 2552
	public bool reachedDestination;

	// Token: 0x040009F9 RID: 2553
	private Animator animator;

	// Token: 0x040009FA RID: 2554
	private Overworld_NPC_CostumeSelector overworld_NPC_CostumeSelector;

	// Token: 0x040009FB RID: 2555
	private Collider2D collider;

	// Token: 0x040009FC RID: 2556
	private float timeSinceLastDirectionChange;

	// Token: 0x040009FD RID: 2557
	[SerializeField]
	private Overworld_InteractiveObject destination_Overworld_InteractiveObject;

	// Token: 0x040009FE RID: 2558
	[SerializeField]
	private bool allowToBePushedOffPath = true;

	// Token: 0x040009FF RID: 2559
	private int skippedPoints;

	// Token: 0x04000A00 RID: 2560
	private Overworld_NPC thisNPC;

	// Token: 0x04000A01 RID: 2561
	[SerializeField]
	private Vector2 offset = new Vector2(0f, 0f);

	// Token: 0x04000A02 RID: 2562
	public bool doneSearch;

	// Token: 0x04000A03 RID: 2563
	public bool pathSuccess;

	// Token: 0x04000A04 RID: 2564
	private Vector2 destination;
}
