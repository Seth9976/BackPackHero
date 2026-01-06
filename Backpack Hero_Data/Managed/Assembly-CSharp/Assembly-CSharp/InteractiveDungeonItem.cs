using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000103 RID: 259
public class InteractiveDungeonItem : CustomInputHandler
{
	// Token: 0x060008E3 RID: 2275 RVA: 0x0005D63C File Offset: 0x0005B83C
	private void Start()
	{
		this.player = Player.main;
		this.gameManager = GameManager.main;
		this.gameFlowManager = GameFlowManager.main;
		base.transform.parent.localPosition = new Vector3(base.transform.parent.localPosition.x, base.transform.parent.localPosition.y, 2f);
		foreach (InteractiveDungeonItem.ItemAndCharacter itemAndCharacter in this.itemAndCharacters)
		{
			if (itemAndCharacter.characterName == this.player.characterName)
			{
				this.winItem.sprite = itemAndCharacter.winItemSprite;
				break;
			}
		}
	}

	// Token: 0x060008E4 RID: 2276 RVA: 0x0005D714 File Offset: 0x0005B914
	private void OnMouseUpAsButton()
	{
		if (DigitalCursor.main.controlStyle != DigitalCursor.ControlStyle.cursor)
		{
			return;
		}
		this.Click();
	}

	// Token: 0x060008E5 RID: 2277 RVA: 0x0005D729 File Offset: 0x0005B929
	public override void OnPressStart(string keyName, bool overrideKeyName)
	{
		if (keyName == "confirm" || overrideKeyName)
		{
			this.Click();
		}
	}

	// Token: 0x060008E6 RID: 2278 RVA: 0x0005D740 File Offset: 0x0005B940
	private void Click()
	{
		if (this.gameManager.travelling || this.isOpen)
		{
			return;
		}
		DigitalInputSelectOnButton componentInParent = base.GetComponentInParent<DigitalInputSelectOnButton>();
		if (componentInParent)
		{
			componentInParent.RemoveSymbol();
		}
		this.isOpen = true;
		base.StartCoroutine(this.WalkToWinItem());
	}

	// Token: 0x060008E7 RID: 2279 RVA: 0x0005D78C File Offset: 0x0005B98C
	public IEnumerator WalkToWinItem()
	{
		if (this.gameManager)
		{
			this.gameManager.EndRunAnalytics("won");
		}
		AchievementAbstractor.instance.ConsiderAchievement("WonAQuickRun");
		if (Singleton.Instance.character == Character.CharacterName.Purse)
		{
			AchievementAbstractor.instance.ConsiderAchievement("WonAQuickRunAsPurse");
		}
		else if (Singleton.Instance.character == Character.CharacterName.Tote)
		{
			AchievementAbstractor.instance.ConsiderAchievement("WonAQuickRunAsTote");
		}
		else if (Singleton.Instance.character == Character.CharacterName.Satchel)
		{
			AchievementAbstractor.instance.ConsiderAchievement("WonAQuickRunAsSatchel");
		}
		else if (Singleton.Instance.character == Character.CharacterName.Pochette)
		{
			AchievementAbstractor.instance.ConsiderAchievement("WonAQuickRunAsPochette");
		}
		else if (Singleton.Instance.character == Character.CharacterName.CR8)
		{
			AchievementAbstractor.instance.ConsiderAchievement("WonAQuickRunAsCR8");
		}
		MetaProgressSaveManager.main.AddNew();
		Transform playerTransform = this.player.transform;
		Animator playerAnimator = playerTransform.GetComponentInChildren<Animator>();
		this.gameManager.travelling = true;
		playerAnimator.Play("Player_Run");
		playerAnimator.speed = 0.9f;
		while (playerTransform.position.x < base.transform.position.x - 1.46f)
		{
			playerTransform.position = Vector3.MoveTowards(playerTransform.position, new Vector3(base.transform.position.x - 0.35f, playerTransform.position.y, playerTransform.position.z), 4.2f * Time.deltaTime);
			yield return null;
		}
		this.gameFlowManager.actionFinished = false;
		this.player.itemToInteractWith.sprite = this.winItem.sprite;
		this.winItem.gameObject.SetActive(false);
		playerAnimator.speed = 0.3f;
		playerAnimator.Play("Player_UseItem");
		while (!this.gameFlowManager.actionFinished)
		{
			yield return null;
		}
		playerAnimator.speed = 1f;
		Object.Instantiate<GameObject>(this.theEndEventPrefab, Vector3.zero, Quaternion.identity, this.gameManager.eventsParent);
		yield break;
	}

	// Token: 0x04000706 RID: 1798
	[SerializeField]
	private List<InteractiveDungeonItem.ItemAndCharacter> itemAndCharacters;

	// Token: 0x04000707 RID: 1799
	[SerializeField]
	private GameObject theEndEventPrefab;

	// Token: 0x04000708 RID: 1800
	[SerializeField]
	private SpriteRenderer winItem;

	// Token: 0x04000709 RID: 1801
	private GameManager gameManager;

	// Token: 0x0400070A RID: 1802
	private GameFlowManager gameFlowManager;

	// Token: 0x0400070B RID: 1803
	private Player player;

	// Token: 0x0400070C RID: 1804
	public bool isOpen;

	// Token: 0x0400070D RID: 1805
	public int doorNumber;

	// Token: 0x0200037B RID: 891
	[Serializable]
	public class ItemAndCharacter
	{
		// Token: 0x04001514 RID: 5396
		public Character.CharacterName characterName;

		// Token: 0x04001515 RID: 5397
		public Sprite winItemSprite;
	}
}
