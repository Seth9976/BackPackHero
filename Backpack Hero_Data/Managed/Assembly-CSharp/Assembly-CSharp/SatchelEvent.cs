using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200009B RID: 155
public class SatchelEvent : CustomEvent
{
	// Token: 0x06000367 RID: 871 RVA: 0x00013CF6 File Offset: 0x00011EF6
	public override IEnumerator Interact()
	{
		this.gameManager.travelling = false;
		this.player.transform.GetComponentInChildren<Animator>();
		if (this.dungeonEvent)
		{
			this.dungeonEvent.FinishEvent();
		}
		UnityEvent onInteract = this.onInteract;
		if (onInteract != null)
		{
			onInteract.Invoke();
		}
		yield return null;
		while (Overworld_ConversationManager.main.InLockedConversation())
		{
			yield return null;
		}
		yield return null;
		yield break;
	}

	// Token: 0x06000368 RID: 872 RVA: 0x00013D08 File Offset: 0x00011F08
	public void CompleteEventAndLaunchConversation(int num)
	{
		this.gameManager.travelling = false;
		if (!this.dungeonEvent)
		{
			DungeonRoom dungeonRoom = DungeonPlayer.main.FindClosestRoom();
			if (dungeonRoom)
			{
				DungeonEvent componentInChildren = dungeonRoom.GetComponentInChildren<DungeonEvent>();
				if (componentInChildren != null)
				{
					componentInChildren.FinishEvent();
				}
			}
		}
		if (this.dungeonEvent)
		{
			this.dungeonEvent.FinishEvent();
		}
		ConversationLauncher component = base.GetComponent<ConversationLauncher>();
		if (component == null)
		{
			return;
		}
		component.LaunchConversationAfterBattle(num);
	}

	// Token: 0x06000369 RID: 873 RVA: 0x00013D7C File Offset: 0x00011F7C
	public override void InteractFromDialogue(int x)
	{
		if (x == 0)
		{
			Object.Destroy(base.transform.parent.gameObject);
			Object.Instantiate<GameObject>(this.enemyToCreate, base.transform.position, Quaternion.identity, Player.main.transform.parent).GetComponentInChildren<Enemy>().PlayAnimation("enemyPos_buff");
			this.gameFlowManager.StartCombat();
		}
	}
}
