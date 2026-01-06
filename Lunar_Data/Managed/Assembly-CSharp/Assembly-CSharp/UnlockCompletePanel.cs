using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000A3 RID: 163
public class UnlockCompletePanel : MonoBehaviour
{
	// Token: 0x06000461 RID: 1121 RVA: 0x00015847 File Offset: 0x00013A47
	private void Start()
	{
		this.startRunButtonHandler.SetActive(false);
		this.startRunButton.SetActive(false);
		this.lootBoxImage.gameObject.SetActive(true);
		this.unlockedItemPanel.SetActive(false);
	}

	// Token: 0x06000462 RID: 1122 RVA: 0x0001587E File Offset: 0x00013A7E
	public void SetRun()
	{
		if (!this.unlock)
		{
			return;
		}
		if (!this.unlock.runType)
		{
			return;
		}
		Singleton.instance.selectedRun = this.unlock.runType;
	}

	// Token: 0x06000463 RID: 1123 RVA: 0x000158B8 File Offset: 0x00013AB8
	public void UnlockComplete(Unlock unlock)
	{
		int num = 0;
		foreach (object obj in this.scrollviewContent)
		{
			Transform transform = (Transform)obj;
			if (num <= 3)
			{
				num++;
			}
			else
			{
				Object.Destroy(transform.gameObject);
			}
		}
		this.unlock = unlock;
		if (this.unlock.runType)
		{
			this.startRunButton.SetActive(true);
			this.startRunButtonHandler.SetActive(true);
		}
		if (unlock.unlockedItem)
		{
			if (unlock.unlockedItem.GetComponent<Relic>())
			{
				this.unlockedTypeText.SetKey("newRelic");
				this.unlockedTypeDescriptionText.SetKey("unlockDesc");
				this.unlockedItemImage.gameObject.SetActive(true);
				this.unlockedItemImage.sprite = unlock.unlockedItem.GetComponentInChildren<SpriteRenderer>().sprite;
			}
			if (unlock.unlockedItem.GetComponent<CardEffect>())
			{
				this.unlockedTypeText.SetKey("newCard");
				this.unlockedTypeDescriptionText.SetKey("unlockDesc");
				this.unlockedItemImage.gameObject.SetActive(true);
				this.unlockedItemImage.sprite = unlock.unlockedItem.GetComponentInChildren<SpriteRenderer>().sprite;
			}
			CardDescription component = unlock.unlockedItem.GetComponent<CardDescription>();
			if (component)
			{
				CardDescriptor component2 = Object.Instantiate<GameObject>(this.cardDescriptionPrefab, this.scrollviewContent).GetComponent<CardDescriptor>();
				component2.SetCardTexts(component.cardName, component.cardDescription);
				if (!component.cardEffect)
				{
					component2.DisableEnergyRequirement();
					component2.DisableUseAndClassTypes();
				}
				else
				{
					component2.SetEnergyRequirement(component.cardEffect.necessaryEnergy);
					component2.SetCardUseAndClassTypes(component.cardEffect.useType, component.cardEffect.lengthOfEffect, component.cardEffect.classType);
				}
			}
		}
		else if (unlock.runType)
		{
			this.unlockedTypeText.SetKey("newRun");
			this.unlockedTypeDescriptionText.SetKey("unlockDescRun");
			this.unlockedItemImage.gameObject.SetActive(false);
			Object.Instantiate<GameObject>(this.unlockedRunNameTextPrefab, this.scrollviewContent).GetComponent<ReplacementText>().SetKey(unlock.runType.runName);
			Object.Instantiate<GameObject>(this.runTypeDescriptorPrefab, this.scrollviewContent).GetComponent<RunTypeDescriptionWindow>().SetDescription(unlock.runType);
		}
		this.lootBoxImage.GetComponent<Animator>().enabled = true;
		if (this.unlockCompleteCoroutine != null)
		{
			base.StopCoroutine(this.unlockCompleteCoroutine);
		}
		this.unlockCompleteCoroutine = base.StartCoroutine(this.UnlockCompleteCoroutine());
	}

	// Token: 0x06000464 RID: 1124 RVA: 0x00015B84 File Offset: 0x00013D84
	private IEnumerator UnlockCompleteCoroutine()
	{
		yield return new WaitForSeconds(0.25f);
		this.lootBoxImage.gameObject.SetActive(false);
		this.unlockedItemPanel.SetActive(true);
		SoundManager.instance.PlaySFX("unlockComplete", double.PositiveInfinity);
		yield break;
	}

	// Token: 0x04000357 RID: 855
	[SerializeField]
	public Image lootBoxImage;

	// Token: 0x04000358 RID: 856
	private Coroutine unlockCompleteCoroutine;

	// Token: 0x04000359 RID: 857
	[SerializeField]
	public UnlockProgressBar unlockProgressBar;

	// Token: 0x0400035A RID: 858
	[SerializeField]
	public GameObject unlockedItemPanel;

	// Token: 0x0400035B RID: 859
	[SerializeField]
	public Image unlockedItemImage;

	// Token: 0x0400035C RID: 860
	[SerializeField]
	public ReplacementText unlockedTypeText;

	// Token: 0x0400035D RID: 861
	[SerializeField]
	public ReplacementText unlockedTypeDescriptionText;

	// Token: 0x0400035E RID: 862
	[SerializeField]
	private GameObject startRunButton;

	// Token: 0x0400035F RID: 863
	[SerializeField]
	private GameObject startRunButtonHandler;

	// Token: 0x04000360 RID: 864
	[SerializeField]
	private Transform scrollviewContent;

	// Token: 0x04000361 RID: 865
	[Header("----------------------Objects to Spawn----------------------")]
	[SerializeField]
	public GameObject cardDescriptionPrefab;

	// Token: 0x04000362 RID: 866
	[SerializeField]
	public GameObject runTypeDescriptorPrefab;

	// Token: 0x04000363 RID: 867
	[SerializeField]
	public GameObject unlockedRunNameTextPrefab;

	// Token: 0x04000364 RID: 868
	private Unlock unlock;
}
