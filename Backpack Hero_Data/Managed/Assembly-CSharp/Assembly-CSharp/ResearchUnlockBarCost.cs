using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000096 RID: 150
public class ResearchUnlockBarCost : CustomInputHandler, IPointerClickHandler, IEventSystemHandler
{
	// Token: 0x06000339 RID: 825 RVA: 0x00012DE4 File Offset: 0x00010FE4
	private void OnEnable()
	{
		if (this.readyToReceive)
		{
			Item2 chosenItem = Overworld_InventoryManager.main.chosenItem;
			if (this.GetItem(chosenItem))
			{
				Overworld_InventoryManager.main.DestroyItem();
				ItemsHeld.main.ForceGetAllItems();
			}
		}
		else
		{
			this.setColor = true;
		}
		this.readyToReceive = false;
	}

	// Token: 0x0600033A RID: 826 RVA: 0x00012E31 File Offset: 0x00011031
	private void Start()
	{
		if (!this.researchUnlockBar)
		{
			this.researchUnlockBar = base.GetComponentInParent<ResearchUnlockBar>();
		}
	}

	// Token: 0x0600033B RID: 827 RVA: 0x00012E4C File Offset: 0x0001104C
	private void Update()
	{
		if (this.setColor)
		{
			this.setColor = false;
			this.SetColor();
		}
	}

	// Token: 0x0600033C RID: 828 RVA: 0x00012E63 File Offset: 0x00011063
	public void SetBar(ResearchUnlockBar researchUnlockBar)
	{
		this.researchUnlockBar = researchUnlockBar;
	}

	// Token: 0x0600033D RID: 829 RVA: 0x00012E6C File Offset: 0x0001106C
	private int GetCost(int cost)
	{
		float num = (float)cost;
		if (this.buildingInterface)
		{
			num /= this.buildingInterface.currentEfficiencyBonus / 100f;
		}
		return Mathf.CeilToInt(num);
	}

	// Token: 0x0600033E RID: 830 RVA: 0x00012EA4 File Offset: 0x000110A4
	public void Setup(Overworld_ResourceManager.Resource resource, Overworld_BuildingInterface buildingInterface)
	{
		this.buildingInterface = buildingInterface;
		this.requiredType = ResearchUnlockBarCost.RequiredType.resource;
		this.resourcesRequired = resource;
		this.spriteImage.gameObject.SetActive(true);
		this.spriteImage.sprite = this.resourceSprites[(int)resource.type];
		this.text.text = this.GetCost(resource.amount).ToString();
		this.inventoryItemButton.gameObject.SetActive(false);
	}

	// Token: 0x0600033F RID: 831 RVA: 0x00012F20 File Offset: 0x00011120
	public void Setup(Item2.ItemType itemType, Item2.Rarity rarity)
	{
		this.requiredType = ResearchUnlockBarCost.RequiredType.itemType;
		this.itemTypesRequired = itemType;
		this.itemRaritiesRequired = rarity;
		this.spriteImage.gameObject.SetActive(false);
		this.inventoryItemButton.gameObject.SetActive(false);
		if (itemType == Item2.ItemType.Any)
		{
			this.text.text = "";
		}
		else
		{
			this.text.text = LangaugeManager.main.GetTextByKey(itemType.ToString());
		}
		if (rarity != Item2.Rarity.Common)
		{
			TextMeshProUGUI textMeshProUGUI = this.text;
			textMeshProUGUI.text = textMeshProUGUI.text + "<br>" + LangaugeManager.main.GetTextByKey(rarity.ToString());
		}
		LangaugeManager.main.SetFont(base.transform);
	}

	// Token: 0x06000340 RID: 832 RVA: 0x00012FE0 File Offset: 0x000111E0
	public void Setup(GameObject item)
	{
		this.requiredType = ResearchUnlockBarCost.RequiredType.item;
		this.itemsRequired = item;
		this.inventoryItemButton.gameObject.SetActive(true);
		this.inventoryItemButton.Setup(item, -1);
		this.text.gameObject.SetActive(false);
	}

	// Token: 0x06000341 RID: 833 RVA: 0x0001301F File Offset: 0x0001121F
	public void SetNumber(int number, Overworld_BuildingInterface.Research research)
	{
		this.numberRequired = number;
		this.research = research;
		this.SetColor();
	}

	// Token: 0x06000342 RID: 834 RVA: 0x00013038 File Offset: 0x00011238
	private void SetColor()
	{
		if (this.research == null || !this.colorFill)
		{
			return;
		}
		if (this.research.IsComplete(this.numberRequired))
		{
			this.colorFill.fillAmount = 1f;
			return;
		}
		if ((this.requiredType == ResearchUnlockBarCost.RequiredType.itemType || this.requiredType == ResearchUnlockBarCost.RequiredType.itemRarity) && this.IfPlayerHasMatchingItem(new List<Item2.ItemType> { this.itemTypesRequired }, this.itemRaritiesRequired))
		{
			this.colorFill.fillAmount = 0.25f;
			return;
		}
		if (this.requiredType == ResearchUnlockBarCost.RequiredType.item && this.IfPlayerHasMatchingItem(this.itemsRequired.name))
		{
			this.colorFill.fillAmount = 0.25f;
			return;
		}
		this.colorFill.fillAmount = 0f;
	}

	// Token: 0x06000343 RID: 835 RVA: 0x000130FE File Offset: 0x000112FE
	private IEnumerator FillColor()
	{
		SoundManager.main.PlaySFX("research");
		float fillAmount = 0f;
		float totalTime = 1f;
		while (fillAmount < totalTime)
		{
			fillAmount += Time.deltaTime;
			this.colorFill.fillAmount = Mathf.Lerp(0f, 1f, fillAmount / totalTime);
			yield return null;
		}
		yield break;
	}

	// Token: 0x06000344 RID: 836 RVA: 0x0001310D File Offset: 0x0001130D
	public bool GetItem(Overworld_InventoryItemButton button)
	{
		return base.GetComponentInParent<Overworld_BuildingInterface>() && !this.research.IsComplete(this.numberRequired) && this.GetItem(button.item);
	}

	// Token: 0x06000345 RID: 837 RVA: 0x00013140 File Offset: 0x00011340
	public bool GetItem(Item2 item)
	{
		if (!item)
		{
			return false;
		}
		if (this.requiredType == ResearchUnlockBarCost.RequiredType.itemType && (item.itemType.Contains(this.itemTypesRequired) || this.itemTypesRequired == Item2.ItemType.Any) && item.rarity >= this.itemRaritiesRequired && item.oneOfAKindType != Item2.OneOfAKindType.OneTotal)
		{
			this.research.SubmitCost(this.numberRequired);
			base.StartCoroutine(this.FillColor());
			return true;
		}
		if (this.requiredType == ResearchUnlockBarCost.RequiredType.item && Item2.GetDisplayName(this.itemsRequired.name) == Item2.GetDisplayName(item.name))
		{
			this.research.SubmitCost(this.numberRequired);
			base.StartCoroutine(this.FillColor());
			return true;
		}
		return false;
	}

	// Token: 0x06000346 RID: 838 RVA: 0x00013200 File Offset: 0x00011400
	public void Click()
	{
		if (!base.GetComponentInParent<Overworld_BuildingInterface>())
		{
			return;
		}
		if (this.research.IsComplete(this.numberRequired))
		{
			return;
		}
		if (this.requiredType == ResearchUnlockBarCost.RequiredType.resource)
		{
			Overworld_ResourceManager.Resource resource = new Overworld_ResourceManager.Resource
			{
				type = this.resourcesRequired.type,
				amount = this.GetCost(this.resourcesRequired.amount)
			};
			if (Overworld_ResourceManager.main.HasEnoughResources(new List<Overworld_ResourceManager.Resource> { resource }, -1))
			{
				Overworld_ResourceManager.ChangeResourceAmountBy(MetaProgressSaveManager.main.resources, new List<Overworld_ResourceManager.Resource> { resource }, -1);
				this.research.SubmitCost(this.numberRequired);
				base.StartCoroutine(this.FillColor());
			}
			else
			{
				MessageManager.main.CreatePopUp(LangaugeManager.main.GetTextByKey("gm35"));
				SoundManager.main.PlaySFX("negative");
			}
		}
		if ((this.requiredType == ResearchUnlockBarCost.RequiredType.itemType || this.requiredType == ResearchUnlockBarCost.RequiredType.itemRarity) && this.IfPlayerHasMatchingItem(new List<Item2.ItemType> { this.itemTypesRequired }, this.itemRaritiesRequired))
		{
			this.readyToReceive = true;
			Overworld_InventoryManager.main.ShowPlayerInventoryWithType(new List<Item2.ItemType> { this.itemTypesRequired }, new List<Item2.Rarity> { this.itemRaritiesRequired });
			return;
		}
		if (this.requiredType == ResearchUnlockBarCost.RequiredType.item && this.IfPlayerHasMatchingItem(this.itemsRequired.name))
		{
			this.readyToReceive = true;
			Overworld_InventoryManager.main.ShowPlayerInventoryWithItem(this.itemsRequired.name);
		}
	}

	// Token: 0x06000347 RID: 839 RVA: 0x00013384 File Offset: 0x00011584
	public bool IfPlayerHasMatchingItem(string nameToSearchFor)
	{
		foreach (string text in MetaProgressSaveManager.main.storedItems)
		{
			if (Item2.GetDisplayName(nameToSearchFor) == Item2.GetDisplayName(text))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000348 RID: 840 RVA: 0x000133F0 File Offset: 0x000115F0
	public bool IfPlayerHasMatchingItem(List<Item2.ItemType> types, Item2.Rarity rarityRequired)
	{
		if (!MetaProgressSaveManager.main)
		{
			return false;
		}
		ItemsHeld.main.GetAllItemsIfNotAlready();
		foreach (Item2 item in ItemsHeld.main.storedItems)
		{
			if (item && Item2.ShareItemTypes(types, item.itemType) && item.rarity >= rarityRequired)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000349 RID: 841 RVA: 0x00013480 File Offset: 0x00011680
	public void Hover()
	{
		if (this.showingCard)
		{
			return;
		}
		this.showingCard = true;
		if (this.research.IsComplete(this.numberRequired))
		{
			Overworld_CardManager.main.DisplayCardSimple(LangaugeManager.main.GetTextByKey("gm64"), base.gameObject, 550f);
			return;
		}
		if (this.requiredType == ResearchUnlockBarCost.RequiredType.item)
		{
			this.inventoryItemButton.ShowCard2();
			return;
		}
		if (this.requiredType == ResearchUnlockBarCost.RequiredType.itemType)
		{
			string text = LangaugeManager.main.GetTextByKey("gm84");
			if (this.itemTypesRequired == Item2.ItemType.Any)
			{
				text = text.Replace("/y", "");
			}
			else
			{
				text = LangaugeManager.main.GetTextByKey("gm84").Replace("/y", LangaugeManager.main.GetTextByKey(this.itemTypesRequired.ToString()));
			}
			if (this.itemRaritiesRequired == Item2.Rarity.Legendary)
			{
				text = text.Replace("/x", LangaugeManager.main.GetTextByKey(this.itemRaritiesRequired.ToString()));
			}
			else if (this.itemRaritiesRequired == Item2.Rarity.Rare)
			{
				text = text.Replace("/x", LangaugeManager.main.GetTextByKey("rarity3"));
			}
			else if (this.itemRaritiesRequired == Item2.Rarity.Uncommon)
			{
				text = text.Replace("/x", LangaugeManager.main.GetTextByKey("rarity2"));
			}
			else
			{
				text = text.Replace("/x ", LangaugeManager.main.GetTextByKey("rarity1"));
			}
			Overworld_CardManager.main.DisplayCardSimple(text, base.gameObject, 700f);
			return;
		}
		if (this.requiredType == ResearchUnlockBarCost.RequiredType.resource)
		{
			string text2 = LangaugeManager.main.GetTextByKey("gm85");
			if (this.resourcesRequired.type == Overworld_ResourceManager.Resource.Type.BuildingMaterial)
			{
				text2 = text2.Replace("/y", LangaugeManager.main.GetTextByKey("material"));
			}
			else if (this.resourcesRequired.type == Overworld_ResourceManager.Resource.Type.Food)
			{
				text2 = text2.Replace("/y", LangaugeManager.main.GetTextByKey("food"));
			}
			else if (this.resourcesRequired.type == Overworld_ResourceManager.Resource.Type.Treasure)
			{
				text2 = text2.Replace("/y", LangaugeManager.main.GetTextByKey("treasure"));
			}
			text2 = text2.Replace("/x", this.GetCost(this.resourcesRequired.amount).ToString());
			Overworld_CardManager.main.DisplayCardSimple(text2, base.gameObject, 550f);
		}
	}

	// Token: 0x0600034A RID: 842 RVA: 0x000136DA File Offset: 0x000118DA
	private void Clear()
	{
		this.showingCard = false;
		if (this.requiredType == ResearchUnlockBarCost.RequiredType.item)
		{
			this.inventoryItemButton.RemoveCard();
		}
		Overworld_CardManager.main.RemoveCardIfParent(base.gameObject);
	}

	// Token: 0x0600034B RID: 843 RVA: 0x00013707 File Offset: 0x00011907
	public override void OnPressStart(string x, bool xx)
	{
		if (x == "confirm")
		{
			this.Click();
		}
	}

	// Token: 0x0600034C RID: 844 RVA: 0x0001371C File Offset: 0x0001191C
	public override void OnCursorHold()
	{
		this.Hover();
	}

	// Token: 0x0600034D RID: 845 RVA: 0x00013724 File Offset: 0x00011924
	public override void OnCursorEnd()
	{
		this.Clear();
	}

	// Token: 0x0600034E RID: 846 RVA: 0x0001372C File Offset: 0x0001192C
	void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
	{
		Debug.Log("click");
		this.Click();
	}

	// Token: 0x04000244 RID: 580
	private ResearchUnlockBarCost.RequiredType requiredType;

	// Token: 0x04000245 RID: 581
	public Overworld_ResourceManager.Resource resourcesRequired;

	// Token: 0x04000246 RID: 582
	public Item2.ItemType itemTypesRequired;

	// Token: 0x04000247 RID: 583
	public Item2.Rarity itemRaritiesRequired;

	// Token: 0x04000248 RID: 584
	public GameObject itemsRequired;

	// Token: 0x04000249 RID: 585
	[Header("UI")]
	[SerializeField]
	private TextMeshProUGUI text;

	// Token: 0x0400024A RID: 586
	[SerializeField]
	private Image spriteImage;

	// Token: 0x0400024B RID: 587
	[SerializeField]
	private Image colorFill;

	// Token: 0x0400024C RID: 588
	[SerializeField]
	private Overworld_InventoryItemButton inventoryItemButton;

	// Token: 0x0400024D RID: 589
	private int numberRequired;

	// Token: 0x0400024E RID: 590
	private Overworld_BuildingInterface.Research research;

	// Token: 0x0400024F RID: 591
	public ResearchUnlockBar researchUnlockBar;

	// Token: 0x04000250 RID: 592
	[SerializeField]
	private Sprite[] resourceSprites;

	// Token: 0x04000251 RID: 593
	private Overworld_BuildingInterface buildingInterface;

	// Token: 0x04000252 RID: 594
	private bool readyToReceive;

	// Token: 0x04000253 RID: 595
	private bool setColor;

	// Token: 0x04000254 RID: 596
	public bool showingCard;

	// Token: 0x0200029D RID: 669
	private enum RequiredType
	{
		// Token: 0x04000FDC RID: 4060
		resource,
		// Token: 0x04000FDD RID: 4061
		itemType,
		// Token: 0x04000FDE RID: 4062
		itemRarity,
		// Token: 0x04000FDF RID: 4063
		item
	}
}
