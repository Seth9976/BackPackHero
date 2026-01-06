using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000017 RID: 23
public class CardPlacement : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
	// Token: 0x060000A0 RID: 160 RVA: 0x00004C80 File Offset: 0x00002E80
	private void OnEnable()
	{
		if (this.spriteRenderer)
		{
			this.cardImage.sprite = this.spriteRenderer.sprite;
			Object.Destroy(this.spriteRenderer);
		}
		CardPlacement.cards.Add(this);
	}

	// Token: 0x060000A1 RID: 161 RVA: 0x00004CBB File Offset: 0x00002EBB
	private void OnDisable()
	{
		CardPlacement.cards.Remove(this);
	}

	// Token: 0x060000A2 RID: 162 RVA: 0x00004CC9 File Offset: 0x00002EC9
	private void OnDestroy()
	{
		if (this.cardPositionLayoutElement)
		{
			Object.Destroy(this.cardPositionLayoutElement);
		}
	}

	// Token: 0x060000A3 RID: 163 RVA: 0x00004CE3 File Offset: 0x00002EE3
	private void Start()
	{
		if (!base.transform.IsChildOf(CanvasManager.instance.transform))
		{
			base.transform.SetParent(CardManager.instance.cardsParent);
			this.DrawCard();
		}
	}

	// Token: 0x060000A4 RID: 164 RVA: 0x00004D18 File Offset: 0x00002F18
	public static bool IsAnythingMoving()
	{
		using (List<CardPlacement>.Enumerator enumerator = CardPlacement.cards.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.IsMoving())
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x060000A5 RID: 165 RVA: 0x00004D70 File Offset: 0x00002F70
	private bool IsMoving()
	{
		return this.state == CardPlacement.State.Discarding || this.state == CardPlacement.State.ReturningToDeck;
	}

	// Token: 0x060000A6 RID: 166 RVA: 0x00004D88 File Offset: 0x00002F88
	private void Update()
	{
		if (this.cardEffect)
		{
			if (EnergyBarMaster.instance.GetEnergy() < this.cardEffect.GetNecessaryEnergy())
			{
				this.cardImage.color = new Color(0.75f, 0.5f, 0.5f, 1f);
			}
			else
			{
				this.cardImage.color = Color.white;
			}
		}
		switch (this.state)
		{
		case CardPlacement.State.Drawing:
			break;
		case CardPlacement.State.Discarding:
			this.Discarding();
			return;
		case CardPlacement.State.InHand:
			this.StayInHand();
			return;
		case CardPlacement.State.ReturningToDeck:
			this.ReturningToDeck();
			break;
		default:
			return;
		}
	}

	// Token: 0x060000A7 RID: 167 RVA: 0x00004E21 File Offset: 0x00003021
	public Sprite GetSprite()
	{
		return this.cardImage.sprite;
	}

	// Token: 0x060000A8 RID: 168 RVA: 0x00004E30 File Offset: 0x00003030
	private void StayInHand()
	{
		if (!this.cardPositionLayoutElement)
		{
			this.cardPositionLayoutElement = CardManager.instance.CreateCardLayoutElement();
			return;
		}
		if (!InputManager.instance.IsGameInput())
		{
			this.isDragging = false;
		}
		if (this.isDragging)
		{
			base.transform.position = Input.mousePosition;
			Vector2 vector = Vector2.one;
			if (Input.mousePosition.y > (float)Screen.height * 0.1f)
			{
				vector = Vector2.one * 0.5f;
			}
			base.transform.localScale = Vector2.Lerp(base.transform.localScale, vector, 10f * Time.deltaTime);
			return;
		}
		base.transform.localScale = Vector2.Lerp(base.transform.localScale, Vector2.one, 10f * Time.deltaTime);
		this.cardPositionLayoutElement.transform.SetSiblingIndex(base.transform.GetSiblingIndex());
		this.destination = this.cardPositionLayoutElement.transform.position;
		this.destination.y = this.destination.y - Mathf.Abs(this.destination.x - CanvasManager.instance.GetCenterOfCanvas().x) * 0.1f;
		if (InputManager.instance.inputType == InputManager.InputType.CardSelection)
		{
			this.destination.y = this.destination.y + 25f;
			if (CardManager.instance.GetSelectedCard() == base.gameObject)
			{
				this.destination.y = this.destination.y + 15f;
			}
		}
		base.transform.position = Vector2.Lerp(base.transform.position, this.destination, 10f * Time.deltaTime);
		base.transform.rotation = Quaternion.Lerp(base.transform.rotation, Quaternion.Euler(0f, 0f, this.destinationRotation), 10f * Time.deltaTime);
		this.rect.sizeDelta = Vector2.Lerp(this.rect.sizeDelta, this.defaultSize, 10f * Time.deltaTime);
	}

	// Token: 0x060000A9 RID: 169 RVA: 0x0000506F File Offset: 0x0000326F
	public void DrawCard()
	{
		this.state = CardPlacement.State.InHand;
	}

	// Token: 0x060000AA RID: 170 RVA: 0x00005078 File Offset: 0x00003278
	public void ReturnToDeck()
	{
		this.state = CardPlacement.State.ReturningToDeck;
	}

	// Token: 0x060000AB RID: 171 RVA: 0x00005084 File Offset: 0x00003284
	private void ReturningToDeck()
	{
		if (this.cardPositionLayoutElement)
		{
			Object.Destroy(this.cardPositionLayoutElement);
		}
		base.transform.SetParent(CanvasManager.instance.transform);
		base.transform.SetAsLastSibling();
		this.destination = CardManager.instance.deck.position;
		this.destinationRotation = 0f;
		base.transform.position = Vector2.Lerp(base.transform.position, this.destination, 10f * Time.deltaTime);
		base.transform.rotation = Quaternion.Lerp(base.transform.rotation, Quaternion.Euler(0f, 0f, this.destinationRotation), 10f * Time.deltaTime);
		this.rect.sizeDelta = Vector2.Lerp(this.rect.sizeDelta, new Vector2(0f, 0f), 10f * Time.deltaTime);
		if (this.rect.sizeDelta.x < 0.1f)
		{
			base.transform.SetParent(CardManager.instance.deck);
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x060000AC RID: 172 RVA: 0x000051CC File Offset: 0x000033CC
	private void Discarding()
	{
		if (this.cardPositionLayoutElement)
		{
			Object.Destroy(this.cardPositionLayoutElement);
		}
		base.transform.SetParent(CanvasManager.instance.transform);
		base.transform.SetAsLastSibling();
		this.destination = CardManager.instance.discardPile.position;
		this.destinationRotation = 0f;
		base.transform.position = Vector2.Lerp(base.transform.position, this.destination, 10f * Time.deltaTime);
		base.transform.rotation = Quaternion.Lerp(base.transform.rotation, Quaternion.Euler(0f, 0f, this.destinationRotation), 10f * Time.deltaTime);
		this.rect.sizeDelta = Vector2.Lerp(this.rect.sizeDelta, new Vector2(0f, 0f), 10f * Time.deltaTime);
		if (this.rect.sizeDelta.x < 0.1f)
		{
			this.rect.sizeDelta = this.defaultSize;
			base.transform.SetParent(CardManager.instance.discardPile);
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x060000AD RID: 173 RVA: 0x00005325 File Offset: 0x00003525
	public void Discard()
	{
		this.state = CardPlacement.State.Discarding;
	}

	// Token: 0x060000AE RID: 174 RVA: 0x00005330 File Offset: 0x00003530
	public void OnPointerDown(PointerEventData eventData)
	{
		if (!InputManager.instance.IsGameInput())
		{
			return;
		}
		CardEffect component = base.GetComponent<CardEffect>();
		if (!component || !component.CanActivate())
		{
			return;
		}
		this.isDragging = true;
		CardPlacement.isDraggingCard = true;
		CardManager.instance.selectedCard = base.gameObject;
		InputManager.instance.SelectCard();
	}

	// Token: 0x060000AF RID: 175 RVA: 0x00005389 File Offset: 0x00003589
	public void OnPointerUp(PointerEventData eventData)
	{
		if (!InputManager.instance.IsGameInput())
		{
			return;
		}
		InputManager.instance.MouseRelease();
		this.isDragging = false;
		CardPlacement.isDraggingCard = false;
	}

	// Token: 0x060000B0 RID: 176 RVA: 0x000053AF File Offset: 0x000035AF
	public void OnPointerEnter(PointerEventData eventData)
	{
		if (CardPlacement.isDraggingCard)
		{
			return;
		}
		CardManager.instance.selectedCard = base.gameObject;
		InputManager.instance.OpenDeck();
	}

	// Token: 0x060000B1 RID: 177 RVA: 0x000053D3 File Offset: 0x000035D3
	public void OnPointerExit(PointerEventData eventData)
	{
		if (CardPlacement.isDraggingCard)
		{
			return;
		}
		if (CardManager.instance.selectedCard == base.gameObject)
		{
			CardManager.instance.selectedCard = null;
		}
		InputManager.instance.CloseDeck();
	}

	// Token: 0x060000B2 RID: 178 RVA: 0x00005409 File Offset: 0x00003609
	public void OnPointerClick(PointerEventData eventData)
	{
	}

	// Token: 0x0400007D RID: 125
	private static List<CardPlacement> cards = new List<CardPlacement>();

	// Token: 0x0400007E RID: 126
	public Vector2 destination;

	// Token: 0x0400007F RID: 127
	public float destinationRotation;

	// Token: 0x04000080 RID: 128
	private GameObject cardPositionLayoutElement;

	// Token: 0x04000081 RID: 129
	[SerializeField]
	private RectTransform rect;

	// Token: 0x04000082 RID: 130
	[SerializeField]
	private SpriteRenderer spriteRenderer;

	// Token: 0x04000083 RID: 131
	[SerializeField]
	private Image cardImage;

	// Token: 0x04000084 RID: 132
	private Vector2 defaultSize = new Vector2(75f, 75f);

	// Token: 0x04000085 RID: 133
	public CardPlacement.State state = CardPlacement.State.InHand;

	// Token: 0x04000086 RID: 134
	public bool isDragging;

	// Token: 0x04000087 RID: 135
	public static bool isDraggingCard = false;

	// Token: 0x04000088 RID: 136
	[SerializeField]
	private CardEffect cardEffect;

	// Token: 0x020000C5 RID: 197
	public enum State
	{
		// Token: 0x040003F2 RID: 1010
		Drawing,
		// Token: 0x040003F3 RID: 1011
		Discarding,
		// Token: 0x040003F4 RID: 1012
		InHand,
		// Token: 0x040003F5 RID: 1013
		ReturningToDeck
	}
}
