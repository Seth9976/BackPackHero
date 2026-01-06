using System;
using Pathfinding;
using UnityEngine;

// Token: 0x0200002D RID: 45
[Serializable]
public class EnemyBehavior : MonoBehaviour
{
	// Token: 0x06000172 RID: 370 RVA: 0x00008AD9 File Offset: 0x00006CD9
	public static implicit operator bool(EnemyBehavior v)
	{
		return v != null;
	}

	// Token: 0x06000173 RID: 371 RVA: 0x00008AE2 File Offset: 0x00006CE2
	public virtual void StartBehavior()
	{
	}

	// Token: 0x06000174 RID: 372 RVA: 0x00008AE4 File Offset: 0x00006CE4
	public virtual void EnterLevelBehavior()
	{
	}

	// Token: 0x06000175 RID: 373 RVA: 0x00008AE6 File Offset: 0x00006CE6
	public virtual void FindPath()
	{
	}

	// Token: 0x06000176 RID: 374 RVA: 0x00008AE8 File Offset: 0x00006CE8
	protected void OnPathComplete(Path p)
	{
		if (!p.error)
		{
			this.enemy.path = p.vectorPath;
			this.enemy.currentWaypoint = 2;
		}
	}

	// Token: 0x06000177 RID: 375 RVA: 0x00008B0F File Offset: 0x00006D0F
	public virtual void DetermineFacingDirection()
	{
	}

	// Token: 0x06000178 RID: 376 RVA: 0x00008B11 File Offset: 0x00006D11
	public virtual void FollowBehavior()
	{
	}

	// Token: 0x06000179 RID: 377 RVA: 0x00008B13 File Offset: 0x00006D13
	public virtual void OnCollision()
	{
	}

	// Token: 0x0600017A RID: 378 RVA: 0x00008B15 File Offset: 0x00006D15
	public virtual void OnTakeDamage()
	{
	}

	// Token: 0x0600017B RID: 379 RVA: 0x00008B17 File Offset: 0x00006D17
	public virtual void OnDefeat()
	{
	}

	// Token: 0x04000128 RID: 296
	[SerializeField]
	protected Rigidbody2D rb;

	// Token: 0x04000129 RID: 297
	[SerializeField]
	protected Seeker seeker;

	// Token: 0x0400012A RID: 298
	[SerializeField]
	protected Enemy enemy;

	// Token: 0x0400012B RID: 299
	[SerializeField]
	protected Destructible destructible;

	// Token: 0x0400012C RID: 300
	[SerializeField]
	protected SimpleAnimator animator;

	// Token: 0x0400012D RID: 301
	public bool customEnterLevelBehavior;

	// Token: 0x0400012E RID: 302
	public bool customStandardBehavior;
}
