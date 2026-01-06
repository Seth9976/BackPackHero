using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001AA RID: 426
public class UICarvingIndicator : MonoBehaviour
{
	// Token: 0x060010EB RID: 4331 RVA: 0x000A09F2 File Offset: 0x0009EBF2
	private void Start()
	{
	}

	// Token: 0x060010EC RID: 4332 RVA: 0x000A09F4 File Offset: 0x0009EBF4
	private void Update()
	{
	}

	// Token: 0x060010ED RID: 4333 RVA: 0x000A09F8 File Offset: 0x0009EBF8
	public void OnPressStart()
	{
		if (!this.myItem)
		{
			return;
		}
		if (this.typeOfButton == UICarvingIndicator.TypeOfButton.clickCarving)
		{
			(this.myItem.itemMovement ? this.myItem.itemMovement : this.myItem.GetComponentInParent<ItemMovement>()).ChooseItemForEvent();
		}
		else if (this.typeOfButton == UICarvingIndicator.TypeOfButton.chooseFromLimitedMenu)
		{
			SelectItemDialogue selectItemDialogue = Object.FindObjectOfType<SelectItemDialogue>();
			if (selectItemDialogue)
			{
				selectItemDialogue.ChooseItem(this.myItem.gameObject);
			}
		}
		if (GameManager.main && GameManager.main.dead)
		{
			SaveAnItemOnLoss saveAnItemOnLoss = Object.FindObjectOfType<SaveAnItemOnLoss>();
			if (saveAnItemOnLoss)
			{
				saveAnItemOnLoss.GetItem(this.myItem);
			}
		}
	}

	// Token: 0x060010EE RID: 4334 RVA: 0x000A0AA6 File Offset: 0x0009ECA6
	private void OnEnable()
	{
		if (this.myItem)
		{
			this.myItem.gameObject.SetActive(true);
		}
	}

	// Token: 0x060010EF RID: 4335 RVA: 0x000A0AC8 File Offset: 0x0009ECC8
	private void OnDisable()
	{
		if (base.GetComponentInParent<SaveAnItemOnLoss>())
		{
			return;
		}
		if (this.myItem && Tote.main && Tote.main.CardIsOwned(this.myItem.gameObject) && !Tote.main.IsDrawn(this.myItem.gameObject))
		{
			this.myItem.gameObject.SetActive(false);
		}
	}

	// Token: 0x060010F0 RID: 4336 RVA: 0x000A0B3B File Offset: 0x0009ED3B
	public Item2 GetItem()
	{
		if (this.myItem)
		{
			return this.myItem;
		}
		return null;
	}

	// Token: 0x060010F1 RID: 4337 RVA: 0x000A0B54 File Offset: 0x0009ED54
	public void Setup(Item2 item)
	{
		this.carvingImage.sprite = item.GetComponent<SpriteRenderer>().sprite;
		this.carvingImage.rectTransform.sizeDelta = this.carvingImage.rectTransform.sizeDelta.normalized * 170f;
		this.carvingImage.rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
		this.carvingImage.rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
		this.carvingImage.rectTransform.offsetMax = new Vector2(-50f, -50f);
		this.carvingImage.rectTransform.offsetMin = new Vector2(50f, 50f);
		this.carvingImage.SetNativeSize();
		RectTransform component = base.GetComponent<RectTransform>();
		float num = Mathf.Min(component.rect.width, component.rect.height) - 25f;
		if (this.carvingImage.rectTransform.rect.width > num || this.carvingImage.rectTransform.rect.height > num)
		{
			float num2 = num / Mathf.Max(this.carvingImage.rectTransform.rect.width, this.carvingImage.rectTransform.rect.height);
			this.carvingImage.rectTransform.sizeDelta = new Vector2(this.carvingImage.rectTransform.rect.width * num2, this.carvingImage.rectTransform.rect.height * num2);
		}
		this.myItem = item;
	}

	// Token: 0x060010F2 RID: 4338 RVA: 0x000A0D23 File Offset: 0x0009EF23
	public void OnSelect()
	{
		if (DigitalCursor.main.controlStyle == DigitalCursor.ControlStyle.controller)
		{
			this.OnCursorStart();
		}
	}

	// Token: 0x060010F3 RID: 4339 RVA: 0x000A0D38 File Offset: 0x0009EF38
	public void OnDeselect()
	{
		if (DigitalCursor.main.controlStyle == DigitalCursor.ControlStyle.controller)
		{
			this.OnCursorEnd();
		}
	}

	// Token: 0x060010F4 RID: 4340 RVA: 0x000A0D50 File Offset: 0x0009EF50
	public void OnCursorStart()
	{
		if (!this.myItem)
		{
			return;
		}
		ItemMovement component = this.myItem.GetComponent<ItemMovement>();
		if (component)
		{
			component.ShowCard(base.gameObject);
		}
	}

	// Token: 0x060010F5 RID: 4341 RVA: 0x000A0D8C File Offset: 0x0009EF8C
	public void OnCursorEnd()
	{
		if (!this.myItem)
		{
			return;
		}
		ItemMovement component = this.myItem.GetComponent<ItemMovement>();
		if (component)
		{
			component.RemoveCard();
		}
	}

	// Token: 0x04000DD0 RID: 3536
	[SerializeField]
	private UICarvingIndicator.TypeOfButton typeOfButton;

	// Token: 0x04000DD1 RID: 3537
	[SerializeField]
	private Image carvingImage;

	// Token: 0x04000DD2 RID: 3538
	private Item2 myItem;

	// Token: 0x02000481 RID: 1153
	public enum TypeOfButton
	{
		// Token: 0x04001A70 RID: 6768
		clickCarving,
		// Token: 0x04001A71 RID: 6769
		chooseFromLimitedMenu
	}
}
