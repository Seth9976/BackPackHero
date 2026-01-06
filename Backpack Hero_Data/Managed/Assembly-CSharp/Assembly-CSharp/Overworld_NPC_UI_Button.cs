using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000081 RID: 129
public class Overworld_NPC_UI_Button : MonoBehaviour
{
	// Token: 0x060002CF RID: 719 RVA: 0x00010C32 File Offset: 0x0000EE32
	private void Start()
	{
	}

	// Token: 0x060002D0 RID: 720 RVA: 0x00010C34 File Offset: 0x0000EE34
	private void Update()
	{
	}

	// Token: 0x060002D1 RID: 721 RVA: 0x00010C38 File Offset: 0x0000EE38
	public void Setup(Overworld_NPC npc)
	{
		this.npc = npc;
		this.npcImage.sprite = npc.portraitSprite;
		if (npc.HasSomethingToSay())
		{
			this.npcImage.color = new Color(1f, 1f, 1f, 1f);
			return;
		}
		this.npcImage.color = new Color(0.35f, 0.35f, 0.35f, 1f);
	}

	// Token: 0x060002D2 RID: 722 RVA: 0x00010CAE File Offset: 0x0000EEAE
	private IEnumerator FindPath()
	{
		this.npc.transform.position;
		if (this.npc.transform.position.x < -23.5f)
		{
			SoundManager.main.PlaySFX("negative");
			PopUpManager.main.CreatePopUp(LangaugeManager.main.GetTextByKey("gm39"), base.transform.position);
			yield break;
		}
		Overworld_Purse.main.overworld_FollowAStarPath.FindPathwayComplete(this.npc.transform.position);
		while (!Overworld_Purse.main.overworld_FollowAStarPath.doneSearch)
		{
			yield return new WaitForEndOfFrame();
		}
		if (Overworld_Purse.main.overworld_FollowAStarPath.pathSuccess)
		{
			this.MoveToNPC();
		}
		else
		{
			SoundManager.main.PlaySFX("negative");
			PopUpManager.main.CreatePopUp(LangaugeManager.main.GetTextByKey("gm39"), base.transform.position);
		}
		yield break;
	}

	// Token: 0x060002D3 RID: 723 RVA: 0x00010CBD File Offset: 0x0000EEBD
	public void PressButton()
	{
		if (!this.npc)
		{
			return;
		}
		if (this.findPathCoroutine != null)
		{
			base.StopCoroutine(this.findPathCoroutine);
		}
		this.findPathCoroutine = base.StartCoroutine(this.FindPath());
	}

	// Token: 0x060002D4 RID: 724 RVA: 0x00010CF4 File Offset: 0x0000EEF4
	private void MoveToNPC()
	{
		BuildNPCsUIList componentInParent = base.GetComponentInParent<BuildNPCsUIList>();
		if (componentInParent)
		{
			componentInParent.DisableList();
		}
		this.npc.WaitForInteraction();
		Overworld_Purse.main.MoveToAndInteractWithNPC(this.npc);
	}

	// Token: 0x040001D7 RID: 471
	[SerializeField]
	private Overworld_NPC npc;

	// Token: 0x040001D8 RID: 472
	[SerializeField]
	private Image npcImage;

	// Token: 0x040001D9 RID: 473
	private Coroutine findPathCoroutine;
}
