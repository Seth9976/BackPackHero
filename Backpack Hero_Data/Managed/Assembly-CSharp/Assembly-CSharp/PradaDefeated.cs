using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200008F RID: 143
public class PradaDefeated : CustomInputHandler
{
	// Token: 0x0600031B RID: 795 RVA: 0x000122A8 File Offset: 0x000104A8
	private void Start()
	{
		this.player = Player.main;
		this.gameManager = GameManager.main;
		this.gameFlowManager = GameFlowManager.main;
		base.transform.parent.localPosition = new Vector3(base.transform.parent.localPosition.x, base.transform.parent.localPosition.y, 2f);
		using (List<PradaDefeated.ItemAndCharacter>.Enumerator enumerator = this.itemAndCharacters.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.characterName == this.player.characterName)
				{
					break;
				}
			}
		}
		GameObject gameObject = GameObject.FindGameObjectWithTag("ChaosParticles");
		if (gameObject)
		{
			gameObject.SetActive(false);
		}
	}

	// Token: 0x0600031C RID: 796 RVA: 0x00012388 File Offset: 0x00010588
	private void OnMouseUpAsButton()
	{
		if (DigitalCursor.main.controlStyle != DigitalCursor.ControlStyle.cursor)
		{
			return;
		}
		this.Click();
	}

	// Token: 0x0600031D RID: 797 RVA: 0x0001239D File Offset: 0x0001059D
	public override void OnPressStart(string keyName, bool overrideKeyName)
	{
		if (keyName == "confirm" || overrideKeyName)
		{
			this.Click();
		}
	}

	// Token: 0x0600031E RID: 798 RVA: 0x000123B4 File Offset: 0x000105B4
	public void Click()
	{
		if (this.gameManager.travelling || this.isOpen)
		{
			return;
		}
		this.isOpen = true;
		base.StartCoroutine(this.WalkToWinItem());
		Object.Destroy(this.digitalInputSelectOnButton);
	}

	// Token: 0x0600031F RID: 799 RVA: 0x000123EB File Offset: 0x000105EB
	public IEnumerator WalkToWinItem()
	{
		AchievementAbstractor.instance.ConsiderAchievement("DefeatedChaos");
		MetaProgressSaveManager.main.AddMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.wonTheWholeDangGame);
		MetaProgressSaveManager.main.CompleteMission(Singleton.Instance.mission);
		MetaProgressSaveManager.main.SaveAll();
		MetaProgressSaveManager.main.RemoveRunSave();
		if (this.gameManager)
		{
			this.gameManager.EndRunAnalytics("won");
		}
		if (Object.FindObjectOfType<TutorialManager>().playType != TutorialManager.PlayType.testing)
		{
			Object.FindObjectOfType<MetaProgressSaveManager>().AddNew();
		}
		Transform playerTransform = this.player.transform;
		Animator playerAnimator = playerTransform.GetComponentInChildren<Animator>();
		this.gameManager.travelling = true;
		playerAnimator.Play("Player_Run");
		playerAnimator.speed = 0.9f;
		while (playerTransform.position.x < base.transform.position.x - 1.2f)
		{
			playerTransform.position = Vector3.MoveTowards(playerTransform.position, new Vector3(base.transform.position.x - 0.35f, playerTransform.position.y, playerTransform.position.z), 4.2f * Time.deltaTime);
			yield return null;
		}
		yield return null;
		this.pradaSprite.enabled = false;
		playerTransform.position = new Vector3(base.transform.position.x - 1.14f, playerTransform.position.y, playerTransform.position.z);
		playerAnimator.Play("hugPrada");
		yield return new WaitForSeconds(3.7f);
		Object.Instantiate<GameObject>(this.endingCinematic, Vector3.zero, Quaternion.identity, GameObject.FindGameObjectWithTag("UI Canvas").transform);
		Object.Destroy(base.gameObject);
		yield break;
	}

	// Token: 0x0400021E RID: 542
	[SerializeField]
	private List<PradaDefeated.ItemAndCharacter> itemAndCharacters;

	// Token: 0x0400021F RID: 543
	[SerializeField]
	private SpriteRenderer pradaSprite;

	// Token: 0x04000220 RID: 544
	[SerializeField]
	private GameObject endingCinematic;

	// Token: 0x04000221 RID: 545
	[SerializeField]
	private InputHandler digitalInputSelectOnButton;

	// Token: 0x04000222 RID: 546
	private GameManager gameManager;

	// Token: 0x04000223 RID: 547
	private GameFlowManager gameFlowManager;

	// Token: 0x04000224 RID: 548
	private Player player;

	// Token: 0x04000225 RID: 549
	public bool isOpen;

	// Token: 0x04000226 RID: 550
	public int doorNumber;

	// Token: 0x02000297 RID: 663
	[Serializable]
	public class ItemAndCharacter
	{
		// Token: 0x04000FC5 RID: 4037
		public Character.CharacterName characterName;

		// Token: 0x04000FC6 RID: 4038
		public Sprite winItemSprite;
	}
}
