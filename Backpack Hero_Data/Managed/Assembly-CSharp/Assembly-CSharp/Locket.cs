using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200006D RID: 109
public class Locket : CustomEvent
{
	// Token: 0x0600022B RID: 555 RVA: 0x0000DA6C File Offset: 0x0000BC6C
	public override IEnumerator Interact()
	{
		this.gameManager.travelling = false;
		this.parentAnimator.enabled = false;
		this.player.transform.GetComponentInChildren<Animator>().Play("Player_GetLocket");
		Player.main.itemToInteractWith.sprite = this.locketSprite;
		this.animator.Play("GetsSnatched");
		GameManager.main.ShowInventory();
		yield return new WaitForSeconds(2.35f);
		Object.Instantiate<GameObject>(this.enemyToCreate, base.transform.position, Quaternion.identity, Player.main.transform.parent).GetComponentInChildren<Enemy>().PlayAnimation("enemyPos_buff");
		this.gameFlowManager.StartCombat();
		this.gameManager.travelling = false;
		base.transform.position = new Vector3(-999f, -999f, -999f);
		this.dungeonEvent.FinishEvent();
		UnityEvent onCombatStart = this.onCombatStart;
		if (onCombatStart != null)
		{
			onCombatStart.Invoke();
		}
		yield return new WaitForSeconds(0.5f);
		Object.Destroy(base.transform.parent.gameObject);
		yield break;
	}
}
