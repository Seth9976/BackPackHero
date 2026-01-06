using System;
using System.Collections;
using System.Collections.Generic;
using CleverCrow.Fluid.Dialogues.Graphs;
using UnityEngine;

// Token: 0x0200001B RID: 27
public class ConversationLauncher : MonoBehaviour
{
	// Token: 0x060000BF RID: 191 RVA: 0x00006A0D File Offset: 0x00004C0D
	private void Start()
	{
	}

	// Token: 0x060000C0 RID: 192 RVA: 0x00006A0F File Offset: 0x00004C0F
	private void Update()
	{
	}

	// Token: 0x060000C1 RID: 193 RVA: 0x00006A11 File Offset: 0x00004C11
	public void LaunchConversationAfterBattle(int num)
	{
		if (this.launchConversationAfterBattleCoroutine != null)
		{
			base.StopCoroutine(this.launchConversationAfterBattleCoroutine);
		}
		this.launchConversationAfterBattleCoroutine = base.StartCoroutine(this.LaunchConversationAfterBattleCoroutine(num));
	}

	// Token: 0x060000C2 RID: 194 RVA: 0x00006A3A File Offset: 0x00004C3A
	private IEnumerator LaunchConversationAfterBattleCoroutine(int num)
	{
		yield return null;
		while (GameFlowManager.main && (GameFlowManager.main.battlePhase != GameFlowManager.BattlePhase.outOfBattle || GameFlowManager.main.isCheckingEffects))
		{
			yield return new WaitForSeconds(0.1f);
		}
		this.LaunchConversation(num);
		yield break;
	}

	// Token: 0x060000C3 RID: 195 RVA: 0x00006A50 File Offset: 0x00004C50
	public void LaunchConversation(int num)
	{
		if (num > this.conversations.Count)
		{
			return;
		}
		Overworld_ConversationManager.main.SetActorInGameScene(this.conversations[num].portrait, this.conversations[num].name, this.conversations[num].pitch);
		Overworld_ConversationManager.main.ShowConversation(this.conversations[num].dialogue, base.transform);
	}

	// Token: 0x0400006B RID: 107
	[SerializeField]
	private List<ConversationLauncher.Conversation> conversations = new List<ConversationLauncher.Conversation>();

	// Token: 0x0400006C RID: 108
	private Coroutine launchConversationAfterBattleCoroutine;

	// Token: 0x0200024D RID: 589
	[Serializable]
	public class Conversation
	{
		// Token: 0x04000ED5 RID: 3797
		public DialogueGraph dialogue;

		// Token: 0x04000ED6 RID: 3798
		public Sprite portrait;

		// Token: 0x04000ED7 RID: 3799
		public string name;

		// Token: 0x04000ED8 RID: 3800
		public float pitch = 1f;
	}
}
