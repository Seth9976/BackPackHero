using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000095 RID: 149
public class ResearchUnlockBar : MonoBehaviour
{
	// Token: 0x06000330 RID: 816 RVA: 0x00012878 File Offset: 0x00010A78
	public void SetFavorite(bool favorite)
	{
		if (this.research.IsComplete())
		{
			this.favoriteImage.gameObject.SetActive(false);
			return;
		}
		if (favorite)
		{
			this.favoriteImage.color = Color.white;
			return;
		}
		this.favoriteImage.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
	}

	// Token: 0x06000331 RID: 817 RVA: 0x000128DC File Offset: 0x00010ADC
	public void MakeFavorite()
	{
		bool flag = !this.research.isFavorite;
		this.research.isFavorite = flag;
		MetaProgressSaveManager.main.AddNewResearch(this.research.GetName(), this.research.Stringify());
		this.SetFavorite(flag);
	}

	// Token: 0x06000332 RID: 818 RVA: 0x0001292C File Offset: 0x00010B2C
	public void SetLauncherSprite(bool hide = false)
	{
		if (this.research.launcherSprite && !hide)
		{
			this.launcherImage.sprite = this.research.launcherSprite;
			this.launcherImage.GetComponent<SimpleHoverText>().SetText(this.research.launcherName);
			this.launcherImage.transform.parent.gameObject.SetActive(true);
			return;
		}
		this.launcherImage.transform.parent.gameObject.SetActive(false);
	}

	// Token: 0x06000333 RID: 819 RVA: 0x000129B8 File Offset: 0x00010BB8
	public void Setup(Overworld_BuildingInterface.Research research, Overworld_BuildingInterface buildingInterface)
	{
		this.research = research;
		this.SetFavorite(research.isFavorite);
		this.SetLauncherSprite(false);
		Overworld_InventoryItemButton componentInParent = this.unlockImage.GetComponentInParent<Overworld_InventoryItemButton>();
		if (research.item)
		{
			componentInParent.Setup(research.item, -1);
		}
		else if (research.mission)
		{
			componentInParent.Setup(research.mission);
		}
		else if (research.type == Overworld_BuildingInterface.Research.Type.MetaProgressMarker)
		{
			componentInParent.Setup(research.name, research.sprite, research.hoverText);
		}
		else if (research.name != "")
		{
			componentInParent.Setup(research.name);
		}
		int num = 0;
		foreach (Overworld_ResourceManager.Resource resource in research.resourcesRequired)
		{
			ResearchUnlockBarCost component = Object.Instantiate<GameObject>(this.costPrefab, this.costParent).GetComponent<ResearchUnlockBarCost>();
			component.SetBar(this);
			component.Setup(resource, buildingInterface);
			component.SetNumber(num, research);
			num++;
		}
		for (int i = 0; i < research.itemTypesRequired.Count; i++)
		{
			Item2.ItemType itemType = research.itemTypesRequired[i];
			Item2.Rarity rarity = Item2.Rarity.Common;
			if (research.itemRaritiesRequired.Count > i)
			{
				rarity = research.itemRaritiesRequired[i];
			}
			ResearchUnlockBarCost component2 = Object.Instantiate<GameObject>(this.costPrefab, this.costParent).GetComponent<ResearchUnlockBarCost>();
			component2.SetBar(this);
			component2.Setup(itemType, rarity);
			component2.SetNumber(num, research);
			num++;
		}
		foreach (GameObject gameObject in research.itemsRequired)
		{
			ResearchUnlockBarCost component3 = Object.Instantiate<GameObject>(this.costPrefab, this.costParent).GetComponent<ResearchUnlockBarCost>();
			component3.SetBar(this);
			component3.Setup(gameObject);
			component3.SetNumber(num, research);
			num++;
		}
	}

	// Token: 0x06000334 RID: 820 RVA: 0x00012BC0 File Offset: 0x00010DC0
	public bool IsVisible()
	{
		if (!this.rectTransform || !this.parentRect)
		{
			return false;
		}
		Vector3[] array = new Vector3[4];
		this.parentRect.GetWorldCorners(array);
		return this.rectTransform.transform.position.y - 2f <= array[2].y && this.rectTransform.transform.position.y + 2f >= array[0].y;
	}

	// Token: 0x06000335 RID: 821 RVA: 0x00012C51 File Offset: 0x00010E51
	private void Start()
	{
		this.rectTransform = base.GetComponent<RectTransform>();
		this.parentRect = base.transform.parent.parent.GetComponent<RectTransform>();
	}

	// Token: 0x06000336 RID: 822 RVA: 0x00012C7C File Offset: 0x00010E7C
	private void Update()
	{
		if (!this.IsVisible())
		{
			if (this.isCurrentlyVisible)
			{
				foreach (object obj in base.transform)
				{
					Transform transform = (Transform)obj;
					if (transform.gameObject.activeSelf)
					{
						this.wasActives.Add(new ResearchUnlockBar.WasActive(true, transform.gameObject));
						transform.gameObject.SetActive(false);
					}
					else
					{
						this.wasActives.Add(new ResearchUnlockBar.WasActive(false, transform.gameObject));
					}
				}
				this.isCurrentlyVisible = false;
				return;
			}
		}
		else if (!this.isCurrentlyVisible)
		{
			foreach (ResearchUnlockBar.WasActive wasActive in this.wasActives)
			{
				if (wasActive.wasActive)
				{
					wasActive.gameObject.SetActive(true);
				}
			}
			this.wasActives.Clear();
			this.isCurrentlyVisible = true;
			if (!this.hasBeenSeenEver)
			{
				this.hasBeenSeenEver = true;
				base.StartCoroutine(this.ShowAsNewUnlock());
			}
		}
	}

	// Token: 0x06000337 RID: 823 RVA: 0x00012DC0 File Offset: 0x00010FC0
	private IEnumerator ShowAsNewUnlock()
	{
		if (this.research == null)
		{
			yield break;
		}
		if (this.research.HasBeenSeen())
		{
			this.newText.gameObject.SetActive(false);
			yield break;
		}
		this.newText.gameObject.SetActive(true);
		string text = this.research.Stringify();
		MetaProgressSaveManager.main.AddNewResearch(base.name, text);
		float time = 0f;
		while ((double)time < 0.25)
		{
			time += Time.deltaTime;
			this.newText.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one * 2f, time / 0.25f);
			yield return new WaitForEndOfFrame();
		}
		time = 0f;
		while (time < 1f)
		{
			time += Time.deltaTime;
			this.newText.color = Color.Lerp(Color.white, Color.clear, time);
			this.newText.transform.localScale = Vector3.Lerp(Vector3.one * 2f, Vector3.zero, time);
			yield return new WaitForEndOfFrame();
		}
		this.newText.gameObject.SetActive(false);
		yield break;
	}

	// Token: 0x04000238 RID: 568
	private Overworld_BuildingInterface.Research research;

	// Token: 0x04000239 RID: 569
	[SerializeField]
	private Image favoriteImage;

	// Token: 0x0400023A RID: 570
	[SerializeField]
	private Image launcherImage;

	// Token: 0x0400023B RID: 571
	[SerializeField]
	private Image unlockImage;

	// Token: 0x0400023C RID: 572
	[SerializeField]
	private GameObject costPrefab;

	// Token: 0x0400023D RID: 573
	[SerializeField]
	private Transform costParent;

	// Token: 0x0400023E RID: 574
	[SerializeField]
	private TextMeshProUGUI newText;

	// Token: 0x0400023F RID: 575
	private RectTransform rectTransform;

	// Token: 0x04000240 RID: 576
	private RectTransform parentRect;

	// Token: 0x04000241 RID: 577
	private bool isCurrentlyVisible;

	// Token: 0x04000242 RID: 578
	private bool hasBeenSeenEver;

	// Token: 0x04000243 RID: 579
	private List<ResearchUnlockBar.WasActive> wasActives = new List<ResearchUnlockBar.WasActive>();

	// Token: 0x0200029B RID: 667
	private class WasActive
	{
		// Token: 0x060013D4 RID: 5076 RVA: 0x000B16D0 File Offset: 0x000AF8D0
		public WasActive(bool wasActive, GameObject gameObject)
		{
			this.wasActive = wasActive;
			this.gameObject = gameObject;
		}

		// Token: 0x04000FD5 RID: 4053
		public bool wasActive;

		// Token: 0x04000FD6 RID: 4054
		public GameObject gameObject;
	}
}
