using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000147 RID: 327
public class Overworld_InventoryItemButton : CustomInputHandler, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
	// Token: 0x06000C87 RID: 3207 RVA: 0x000803E4 File Offset: 0x0007E5E4
	public void ShowAsNewUnlock()
	{
		this.newUnlockText.gameObject.SetActive(true);
	}

	// Token: 0x06000C88 RID: 3208 RVA: 0x000803F8 File Offset: 0x0007E5F8
	private void Start()
	{
		this.rectTransform = base.GetComponent<RectTransform>();
		this.layoutElement = base.GetComponent<LayoutElement>();
		this.images = base.GetComponentsInChildren<Image>();
		this.textMeshProUGUI = base.GetComponentsInChildren<TextMeshProUGUI>();
		this.graphicRaycaster = base.GetComponentInParent<GraphicRaycaster>();
		this.startingInventory = base.GetComponentInParent<OverworldInventory>();
		this.canvasGroup = base.GetComponent<CanvasGroup>();
	}

	// Token: 0x06000C89 RID: 3209 RVA: 0x0008045C File Offset: 0x0007E65C
	private void Update()
	{
		if (this.hovering && !this.layoutElement.ignoreLayout)
		{
			this.timeToDisplay += Time.deltaTime;
			if (this.timeToDisplay > 0.25f)
			{
				this.ShowCard();
			}
		}
		else
		{
			this.timeToDisplay = 0f;
		}
		if (this.dragging)
		{
			this.draggingTime += Time.deltaTime;
			this.Drag();
			if (this.draggingTime > 0.2f && (DigitalCursor.main.GetInputDown("confirm") || DigitalCursor.main.GetInputDown("cancel")))
			{
				this.EndDrag();
				return;
			}
		}
		else
		{
			this.draggingTime = 0f;
		}
	}

	// Token: 0x06000C8A RID: 3210 RVA: 0x00080514 File Offset: 0x0007E714
	public int GetSiblingIndex()
	{
		OverworldInventory componentInParent = base.GetComponentInParent<OverworldInventory>();
		if (componentInParent)
		{
			return base.transform.GetSiblingIndex() + componentInParent.GetItemNumber();
		}
		return base.transform.GetSiblingIndex();
	}

	// Token: 0x06000C8B RID: 3211 RVA: 0x0008054E File Offset: 0x0007E74E
	public void ShowCard()
	{
		if (this.layoutElement.ignoreLayout)
		{
			return;
		}
		this.ShowCard2();
	}

	// Token: 0x06000C8C RID: 3212 RVA: 0x00080564 File Offset: 0x0007E764
	public void ShowCard2()
	{
		if (this.card)
		{
			return;
		}
		if (Overworld_InventoryItemButton.cardShowingFromInventoryItemButton)
		{
			Overworld_InventoryItemButton.cardShowingFromInventoryItemButton.RemoveCard();
			Overworld_InventoryItemButton.cardShowingFromInventoryItemButton = null;
		}
		if (this.item)
		{
			GameObject gameObject = this.item.GetComponent<ItemMovement>().ShowCard(base.gameObject);
			if (gameObject)
			{
				this.card = gameObject.GetComponent<Card>();
			}
		}
		else if (this.mission)
		{
			Card component = Object.Instantiate<GameObject>(this.simpleCardMission, base.transform.position, Quaternion.identity, GameObject.FindGameObjectWithTag("SecondaryCanvas").GetComponent<Canvas>().transform).GetComponent<Card>();
			component.GetDescriptionMission(this.mission, base.gameObject);
			if (component)
			{
				this.card = component;
			}
		}
		else if (this.structure)
		{
			Card component2 = Overworld_CardManager.main.DisplayCard(this.structure, null).GetComponent<Card>();
			if (component2)
			{
				this.card = component2;
			}
		}
		else if (this.sellingTile)
		{
			Card component3 = Overworld_CardManager.main.DisplayCard(this.sellingTile).GetComponent<Card>();
			if (component3)
			{
				this.card = component3;
			}
		}
		else if (this.text != "")
		{
			Card component4 = Object.Instantiate<GameObject>(this.simpleCard, base.transform.position, Quaternion.identity, GameObject.FindGameObjectWithTag("SecondaryCanvas").GetComponent<Canvas>().transform).GetComponent<Card>();
			component4.GetDescriptionsSimple(new List<string> { this.text }, base.gameObject);
			if (component4)
			{
				this.card = component4;
			}
		}
		Overworld_InventoryItemButton.cardShowingFromInventoryItemButton = this;
	}

	// Token: 0x06000C8D RID: 3213 RVA: 0x00080735 File Offset: 0x0007E935
	public void RemoveCard()
	{
		if (!this.card)
		{
			return;
		}
		Object.Destroy(this.card.gameObject);
	}

	// Token: 0x06000C8E RID: 3214 RVA: 0x00080755 File Offset: 0x0007E955
	void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
	{
		this.hovering = true;
	}

	// Token: 0x06000C8F RID: 3215 RVA: 0x0008075E File Offset: 0x0007E95E
	private void SetupNavigation()
	{
	}

	// Token: 0x06000C90 RID: 3216 RVA: 0x00080760 File Offset: 0x0007E960
	private Selectable FindClosestToPosition(Vector2 position, List<Selectable> iibs)
	{
		float num = 100000f;
		Selectable selectable = null;
		foreach (Selectable selectable2 in iibs)
		{
			if (!(selectable2 == this))
			{
				float num2 = Vector3.Distance(selectable2.transform.position, position);
				if (num2 < num)
				{
					num = num2;
					selectable = selectable2;
				}
			}
		}
		return selectable;
	}

	// Token: 0x06000C91 RID: 3217 RVA: 0x000807DC File Offset: 0x0007E9DC
	public override void OnCursorStart()
	{
		if (DigitalCursor.main.GetCurrentUITop() != base.gameObject)
		{
			return;
		}
		this.hovering = true;
		this.SetupNavigation();
		DigitalCursor.main.UpdateContextControls();
	}

	// Token: 0x06000C92 RID: 3218 RVA: 0x0008080D File Offset: 0x0007EA0D
	void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
	{
		this.hovering = false;
		this.RemoveCard();
	}

	// Token: 0x06000C93 RID: 3219 RVA: 0x0008081C File Offset: 0x0007EA1C
	public override void OnCursorEnd()
	{
		this.hovering = false;
		this.RemoveCard();
		DigitalCursor.main.UpdateContextControls();
	}

	// Token: 0x06000C94 RID: 3220 RVA: 0x00080838 File Offset: 0x0007EA38
	public override void OnPressStart(string keyName, bool overrideKeyName)
	{
		base.OnPressStart(keyName, overrideKeyName);
		if (this.timeToDisplay <= 0.05f)
		{
			return;
		}
		if (keyName == "confirm")
		{
			this.MiddleClick();
		}
		if (keyName == "cancel")
		{
			this.EndDrag();
		}
		if (keyName == "contextualaction")
		{
			this.MiddleClick();
		}
		if (keyName == "contextmenu")
		{
			this.RightClick();
		}
	}

	// Token: 0x06000C95 RID: 3221 RVA: 0x000808A7 File Offset: 0x0007EAA7
	void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Middle || eventData.button == PointerEventData.InputButton.Left)
		{
			this.MiddleClick();
		}
		if (eventData.button == PointerEventData.InputButton.Right)
		{
			this.RightClick();
		}
	}

	// Token: 0x06000C96 RID: 3222 RVA: 0x000808CF File Offset: 0x0007EACF
	private void RightClick()
	{
		ContextMenuManager.main.ReceiveItem(this.item, ContextMenuManager.main.gameObject);
	}

	// Token: 0x06000C97 RID: 3223 RVA: 0x000808EC File Offset: 0x0007EAEC
	private void MiddleClick()
	{
		if (!this.draggable)
		{
			return;
		}
		OverworldInventory componentInParent = base.GetComponentInParent<OverworldInventory>();
		base.GetComponentInParent<ItemSlot>();
		OverworldInventory[] array = Object.FindObjectsOfType<OverworldInventory>();
		int i = 0;
		while (i < array.Length)
		{
			OverworldInventory overworldInventory = array[i];
			if (overworldInventory != componentInParent)
			{
				if (!overworldInventory.CanAcceptItem(this))
				{
					MessageManager.main.CreatePopUp(LangaugeManager.main.GetTextByKey("gm98"));
					SoundManager.main.PlaySFX("negative");
					return;
				}
				if (componentInParent)
				{
					componentInParent.RemoveItemButton(this, -1);
					DigitalCursor.main.SelectClosestSelectableInElement(componentInParent.transform);
				}
				overworldInventory.AddItemButton(this);
				OverworldInventory.UpdateAllPages();
				SoundManager.main.PlaySFX("putdown");
				return;
			}
			else
			{
				i++;
			}
		}
		ResearchUnlockBarCost[] array2 = Object.FindObjectsOfType<ResearchUnlockBarCost>();
		for (i = 0; i < array2.Length; i++)
		{
			if (array2[i].researchUnlockBar.IsVisible())
			{
				componentInParent.RemoveItemButton(this, -1);
				return;
			}
		}
	}

	// Token: 0x06000C98 RID: 3224 RVA: 0x000809D1 File Offset: 0x0007EBD1
	private void ToggleDrag()
	{
	}

	// Token: 0x06000C99 RID: 3225 RVA: 0x000809D3 File Offset: 0x0007EBD3
	void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
	{
	}

	// Token: 0x06000C9A RID: 3226 RVA: 0x000809D8 File Offset: 0x0007EBD8
	private void OnEnable()
	{
		if (Overworld_InventoryManager.main && Overworld_InventoryManager.main.chosenItem && this.isReceiving)
		{
			this.isReceiving = false;
			this.Setup(Overworld_InventoryManager.main.chosenItem.gameObject, -1);
			Overworld_InventoryManager.main.ClearItem();
			return;
		}
	}

	// Token: 0x06000C9B RID: 3227 RVA: 0x00080A34 File Offset: 0x0007EC34
	private void BeginDrag()
	{
	}

	// Token: 0x06000C9C RID: 3228 RVA: 0x00080A44 File Offset: 0x0007EC44
	private void Drag()
	{
		if (!this.draggable)
		{
			return;
		}
		this.layoutElement.ignoreLayout = true;
		Image[] array = this.images;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].maskable = false;
		}
		TextMeshProUGUI[] array2 = this.textMeshProUGUI;
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i].maskable = false;
		}
		this.rectTransform.position = DigitalCursor.main.transform.position;
		this.rectTransform.localPosition = new Vector3(this.rectTransform.localPosition.x, this.rectTransform.localPosition.y, 0f);
	}

	// Token: 0x06000C9D RID: 3229 RVA: 0x00080AF1 File Offset: 0x0007ECF1
	void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
	{
		this.EndDrag();
	}

	// Token: 0x06000C9E RID: 3230 RVA: 0x00080AFC File Offset: 0x0007ECFC
	private void EndDrag()
	{
		if (!this.draggable || !this.dragging)
		{
			return;
		}
		SoundManager.main.PlaySFX("putdown");
		if (Overworld_InventoryItemButton.draggingButton == this)
		{
			Overworld_InventoryItemButton.draggingButton = null;
		}
		Image[] array = this.images;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].maskable = true;
		}
		TextMeshProUGUI[] array2 = this.textMeshProUGUI;
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i].maskable = true;
		}
		this.layoutElement.ignoreLayout = false;
		PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
		pointerEventData.position = Camera.main.WorldToScreenPoint(DigitalCursor.main.transform.position);
		List<RaycastResult> list = new List<RaycastResult>();
		this.graphicRaycaster.Raycast(pointerEventData, list);
		foreach (RaycastResult raycastResult in list)
		{
			OverworldInventory componentInParent = raycastResult.gameObject.GetComponentInParent<OverworldInventory>();
			if (componentInParent)
			{
				if (componentInParent.CanAcceptItem(this))
				{
					componentInParent.AddItemButton(this);
					OverworldInventory.UpdateAllPages();
					return;
				}
				MessageManager.main.CreatePopUp(LangaugeManager.main.GetTextByKey("gm38"));
				SoundManager.main.PlaySFX("negative");
			}
			ResearchUnlockBarCost component = raycastResult.gameObject.GetComponent<ResearchUnlockBarCost>();
			if (component)
			{
				if (component.GetItem(this))
				{
					return;
				}
				SoundManager.main.PlaySFX("negative");
				MessageManager.main.CreatePopUp(LangaugeManager.main.GetTextByKey("gm38"));
			}
		}
		if (this.startingInventory)
		{
			this.startingInventory.AddItemButton(this);
		}
		Selectable component2 = base.GetComponent<Button>();
		this.canvasGroup.blocksRaycasts = true;
		this.canvasGroup.interactable = true;
		component2.interactable = true;
		this.dragging = false;
		OverworldInventory.UpdateAllPages();
	}

	// Token: 0x06000C9F RID: 3231 RVA: 0x00080D00 File Offset: 0x0007EF00
	public void Setup(SellingTile sellingTile)
	{
		if (!sellingTile)
		{
			return;
		}
		this.itemNameText.text = Item2.GetDisplayName(sellingTile.name);
		this.itemImage.sprite = sellingTile.GetComponentInChildren<SpriteRenderer>().sprite;
		this.sellingTile = sellingTile;
		this.itemImage.SetNativeSize();
		if (this.itemImage.rectTransform.rect.width > 200f || this.itemImage.rectTransform.rect.height > 200f)
		{
			float num = 180f / Mathf.Max(this.itemImage.rectTransform.rect.width, this.itemImage.rectTransform.rect.height);
			this.itemImage.rectTransform.sizeDelta = new Vector2(this.itemImage.rectTransform.rect.width * num, this.itemImage.rectTransform.rect.height * num);
		}
	}

	// Token: 0x06000CA0 RID: 3232 RVA: 0x00080E1C File Offset: 0x0007F01C
	public void Setup(Overworld_Structure structure)
	{
		if (!structure)
		{
			return;
		}
		this.itemNameText.text = Item2.GetDisplayName(structure.name);
		this.itemImage.sprite = structure.GetComponentInChildren<SpriteRenderer>().sprite;
		this.structure = structure;
		this.itemImage.SetNativeSize();
		if (this.itemImage.rectTransform.rect.width > 200f || this.itemImage.rectTransform.rect.height > 200f)
		{
			float num = 180f / Mathf.Max(this.itemImage.rectTransform.rect.width, this.itemImage.rectTransform.rect.height);
			this.itemImage.rectTransform.sizeDelta = new Vector2(this.itemImage.rectTransform.rect.width * num, this.itemImage.rectTransform.rect.height * num);
		}
	}

	// Token: 0x06000CA1 RID: 3233 RVA: 0x00080F38 File Offset: 0x0007F138
	public void SetupAsOptionItem()
	{
		this.optionItem = true;
		this.itemNameText.text = LangaugeManager.main.GetTextByKey("gm39");
		this.itemImage.sprite = null;
		this.item = null;
		this.structure = null;
		this.sellingTile = null;
	}

	// Token: 0x06000CA2 RID: 3234 RVA: 0x00080F88 File Offset: 0x0007F188
	public void Setup(GameObject item, int num = -1)
	{
		this.itemNumber = num;
		Overworld_Structure component = item.GetComponent<Overworld_Structure>();
		if (component)
		{
			if (this.colorImage)
			{
				this.colorImage.gameObject.SetActive(false);
			}
			this.Setup(component);
			return;
		}
		SellingTile component2 = item.GetComponent<SellingTile>();
		if (component2)
		{
			this.Setup(component2);
			return;
		}
		Item2 component3 = item.GetComponent<Item2>();
		SpriteRenderer component4 = item.GetComponent<SpriteRenderer>();
		if (!component3 || !component4)
		{
			if (this.colorImage)
			{
				this.colorImage.gameObject.SetActive(false);
			}
			return;
		}
		string displayName = Item2.GetDisplayName(component3.name);
		this.itemNameText.text = displayName;
		this.itemImage.sprite = component4.sprite;
		component3.displayName = displayName;
		this.item = component3;
		if (this.colorImage)
		{
			if (component3.rarity == Item2.Rarity.Common)
			{
				this.colorImage.color = Color.clear;
			}
			else if (component3.rarity == Item2.Rarity.Uncommon)
			{
				this.colorImage.color = new Color(0f, 0.5f, 0f, 0.3f);
			}
			else if (component3.rarity == Item2.Rarity.Rare)
			{
				this.colorImage.color = new Color(0.8f, 0.7f, 0.1f, 0.4f);
			}
			else if (component3.rarity == Item2.Rarity.Legendary)
			{
				this.colorImage.color = new Color(0.6f, 0f, 0.65f, 0.3f);
			}
		}
		this.itemImage.SetNativeSize();
		if (this.itemImage.rectTransform.rect.width > 200f || this.itemImage.rectTransform.rect.height > 200f)
		{
			float num2 = 180f / Mathf.Max(this.itemImage.rectTransform.rect.width, this.itemImage.rectTransform.rect.height);
			this.itemImage.rectTransform.sizeDelta = new Vector2(this.itemImage.rectTransform.rect.width * num2, this.itemImage.rectTransform.rect.height * num2);
		}
	}

	// Token: 0x06000CA3 RID: 3235 RVA: 0x000811F4 File Offset: 0x0007F3F4
	public void Setup(Missions mission)
	{
		if (this.colorImage)
		{
			this.colorImage.gameObject.SetActive(false);
		}
		this.itemNameText.text = mission.name;
		this.itemImage.sprite = this.missionIcon;
		this.itemImage.rectTransform.sizeDelta = new Vector2(100f, 100f);
		this.mission = mission;
	}

	// Token: 0x06000CA4 RID: 3236 RVA: 0x00081268 File Offset: 0x0007F468
	public void Setup(string text)
	{
		this.itemNameText.text = text;
		this.itemImage.sprite = this.loreIcon;
		this.itemImage.rectTransform.sizeDelta = new Vector2(100f, 100f);
		this.text = LangaugeManager.main.GetTextByKey("gm72b");
	}

	// Token: 0x06000CA5 RID: 3237 RVA: 0x000812C8 File Offset: 0x0007F4C8
	public void Setup(string text, Sprite sprite, string hoverText = "")
	{
		this.hoverText = hoverText;
		this.itemNameText.text = text;
		this.itemImage.sprite = sprite;
		this.itemImage.SetNativeSize();
		this.itemImage.rectTransform.sizeDelta = new Vector2(this.itemImage.rectTransform.rect.width * 125f / Mathf.Max(this.itemImage.rectTransform.rect.width, this.itemImage.rectTransform.rect.height), this.itemImage.rectTransform.rect.height * 125f / Mathf.Max(this.itemImage.rectTransform.rect.width, this.itemImage.rectTransform.rect.height));
		if (hoverText == "")
		{
			this.text = LangaugeManager.main.GetTextByKey("gm72e");
			return;
		}
		this.text = LangaugeManager.main.GetTextByKey(hoverText);
	}

	// Token: 0x06000CA6 RID: 3238 RVA: 0x000813F0 File Offset: 0x0007F5F0
	public void SetupAction(Overworld_InventoryManager.ClickAction clickAction)
	{
		Button component = base.GetComponent<Button>();
		if (component == null)
		{
			return;
		}
		if (clickAction == Overworld_InventoryManager.ClickAction.SELECTITEM)
		{
			this.isButtonEnabled = true;
			component.onClick.RemoveAllListeners();
			component.onClick.AddListener(delegate
			{
				Overworld_InventoryManager.main.SelectItem(this);
			});
			return;
		}
		if (clickAction != Overworld_InventoryManager.ClickAction.SELECTITEMtoTake)
		{
			return;
		}
		this.isButtonEnabled = true;
		component.onClick.RemoveAllListeners();
		component.onClick.AddListener(delegate
		{
			if (!this.item)
			{
				return;
			}
			if (this.item.oneOfAKindType == Item2.OneOfAKindType.OneTotal)
			{
				PopUpManager.main.CreatePopUp(LangaugeManager.main.GetTextByKey("gm25"), DigitalCursor.main.transform.position);
				return;
			}
			Overworld_InventoryManager.main.SelectItem(this);
			MetaProgressSaveManager.main.storedItems.RemoveAt(Overworld_InventoryManager.main.chosenItemNumber);
		});
	}

	// Token: 0x06000CA7 RID: 3239 RVA: 0x00081469 File Offset: 0x0007F669
	private void OnDestroy()
	{
		if (this.optionItem && Overworld_InventoryManager.main && this.item)
		{
			MetaProgressSaveManager.main.AddItem(this.item);
		}
		DigitalCursor.main.UpdateContextControls();
	}

	// Token: 0x06000CA8 RID: 3240 RVA: 0x000814A6 File Offset: 0x0007F6A6
	public void Select()
	{
		Overworld_InventoryManager.main.SelectItem(this);
	}

	// Token: 0x04000A18 RID: 2584
	public static Overworld_InventoryItemButton cardShowingFromInventoryItemButton;

	// Token: 0x04000A19 RID: 2585
	public bool optionItem;

	// Token: 0x04000A1A RID: 2586
	private string hoverText;

	// Token: 0x04000A1B RID: 2587
	[SerializeField]
	private Button b;

	// Token: 0x04000A1C RID: 2588
	[SerializeField]
	private TextMeshProUGUI newUnlockText;

	// Token: 0x04000A1D RID: 2589
	[SerializeField]
	private Image colorImage;

	// Token: 0x04000A1E RID: 2590
	[SerializeField]
	private GameObject simpleCard;

	// Token: 0x04000A1F RID: 2591
	[SerializeField]
	private GameObject simpleCardMission;

	// Token: 0x04000A20 RID: 2592
	[SerializeField]
	private Sprite missionIcon;

	// Token: 0x04000A21 RID: 2593
	[SerializeField]
	private Sprite loreIcon;

	// Token: 0x04000A22 RID: 2594
	[SerializeField]
	private Missions mission;

	// Token: 0x04000A23 RID: 2595
	[SerializeField]
	private string text;

	// Token: 0x04000A24 RID: 2596
	[SerializeField]
	private TextMeshProUGUI itemNameText;

	// Token: 0x04000A25 RID: 2597
	[SerializeField]
	public Image itemImage;

	// Token: 0x04000A26 RID: 2598
	public bool draggable = true;

	// Token: 0x04000A27 RID: 2599
	public Item2 item;

	// Token: 0x04000A28 RID: 2600
	public int itemNumber;

	// Token: 0x04000A29 RID: 2601
	public Overworld_Structure structure;

	// Token: 0x04000A2A RID: 2602
	public SellingTile sellingTile;

	// Token: 0x04000A2B RID: 2603
	private RectTransform rectTransform;

	// Token: 0x04000A2C RID: 2604
	private CanvasGroup canvasGroup;

	// Token: 0x04000A2D RID: 2605
	private LayoutElement layoutElement;

	// Token: 0x04000A2E RID: 2606
	private Image[] images;

	// Token: 0x04000A2F RID: 2607
	private TextMeshProUGUI[] textMeshProUGUI;

	// Token: 0x04000A30 RID: 2608
	public OverworldInventory startingInventory;

	// Token: 0x04000A31 RID: 2609
	private GraphicRaycaster graphicRaycaster;

	// Token: 0x04000A32 RID: 2610
	private Card card;

	// Token: 0x04000A33 RID: 2611
	private float timeToDisplay;

	// Token: 0x04000A34 RID: 2612
	private bool hovering;

	// Token: 0x04000A35 RID: 2613
	private float draggingTime;

	// Token: 0x04000A36 RID: 2614
	private bool dragging;

	// Token: 0x04000A37 RID: 2615
	public static Overworld_InventoryItemButton draggingButton;

	// Token: 0x04000A38 RID: 2616
	private bool isReceiving;

	// Token: 0x04000A39 RID: 2617
	private bool isButtonEnabled;
}
