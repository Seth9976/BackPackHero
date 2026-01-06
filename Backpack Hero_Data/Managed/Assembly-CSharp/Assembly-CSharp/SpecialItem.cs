using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000EA RID: 234
public abstract class SpecialItem : MonoBehaviour
{
	// Token: 0x0600084E RID: 2126 RVA: 0x00056FBF File Offset: 0x000551BF
	protected virtual void Start()
	{
		this.item = base.GetComponentInParent<Item2>();
		this.itemMovement = base.GetComponentInParent<ItemMovement>();
		this.gameFlowManager = GameFlowManager.main;
		this.gameManager = GameManager.main;
		this.player = Player.main;
	}

	// Token: 0x0600084F RID: 2127 RVA: 0x00056FFA File Offset: 0x000551FA
	private void Update()
	{
	}

	// Token: 0x06000850 RID: 2128 RVA: 0x00056FFC File Offset: 0x000551FC
	public virtual string GetDescription()
	{
		return this.description;
	}

	// Token: 0x06000851 RID: 2129 RVA: 0x00057004 File Offset: 0x00055204
	public virtual IEnumerator ApplySpecialEffect(List<Enemy> effectedEnemies)
	{
		foreach (Status status in this.GetTargets(effectedEnemies))
		{
			this.UseSpecialEffect(status);
		}
		GameFlowManager.main.isWaitingForItemRoutine = false;
		yield break;
	}

	// Token: 0x06000852 RID: 2130 RVA: 0x0005701A File Offset: 0x0005521A
	public virtual void UseSpecialEffect(Status stat)
	{
		Debug.Log("Debuggig for virtual class. Did you mean to override this with a special item script?");
	}

	// Token: 0x06000853 RID: 2131 RVA: 0x00057026 File Offset: 0x00055226
	public virtual void ShowHighlights()
	{
	}

	// Token: 0x06000854 RID: 2132 RVA: 0x00057028 File Offset: 0x00055228
	public virtual bool ConsiderPlacement(Vector2 endPosition)
	{
		return true;
	}

	// Token: 0x06000855 RID: 2133 RVA: 0x0005702C File Offset: 0x0005522C
	public List<Status> GetTargets(List<Enemy> enemiesEffected)
	{
		List<Status> list = new List<Status>();
		if (this.target == Item2.Effect.Target.player || this.target == Item2.Effect.Target.unspecified)
		{
			List<Status> list2 = new List<Status>();
			list2.Add(Player.main.stats);
			list = list2;
			list = list2;
		}
		else if (this.target == Item2.Effect.Target.enemy)
		{
			if (!this.gameManager.targetedEnemy || this.gameManager.targetedEnemy.dead)
			{
				return list;
			}
			list = new List<Status> { this.gameManager.targetedEnemy.stats };
		}
		else if (this.target == Item2.Effect.Target.allEnemies)
		{
			foreach (Enemy enemy in Enemy.allEnemies)
			{
				if (enemy && !enemy.dead)
				{
					list.Add(enemy.stats);
				}
			}
		}
		return list;
	}

	// Token: 0x0400066F RID: 1647
	[SerializeField]
	public Item2.Trigger trigger;

	// Token: 0x04000670 RID: 1648
	[SerializeField]
	public Item2.Effect.Target target;

	// Token: 0x04000671 RID: 1649
	[SerializeField]
	public string description;

	// Token: 0x04000672 RID: 1650
	[HideInInspector]
	protected GameFlowManager gameFlowManager;

	// Token: 0x04000673 RID: 1651
	[HideInInspector]
	protected GameManager gameManager;

	// Token: 0x04000674 RID: 1652
	[HideInInspector]
	protected Player player;

	// Token: 0x04000675 RID: 1653
	public float value;

	// Token: 0x04000676 RID: 1654
	public Item2 item;

	// Token: 0x04000677 RID: 1655
	public ItemMovement itemMovement;
}
