using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000015 RID: 21
public class CardManager : MonoBehaviour
{
	// Token: 0x0600008C RID: 140 RVA: 0x000043D8 File Offset: 0x000025D8
	private void OnEnable()
	{
		CardManager.instance = this;
	}

	// Token: 0x0600008D RID: 141 RVA: 0x000043E0 File Offset: 0x000025E0
	private void OnDisable()
	{
		if (CardManager.instance == this)
		{
			CardManager.instance = null;
		}
	}

	// Token: 0x0600008E RID: 142 RVA: 0x000043F5 File Offset: 0x000025F5
	private void Start()
	{
		this.CreateDeck();
	}

	// Token: 0x0600008F RID: 143 RVA: 0x00004400 File Offset: 0x00002600
	private void Update()
	{
		if (this.cardsParent.transform.childCount < 5)
		{
			this.DrawCard();
		}
		if (this.rotationDelay > 0f)
		{
			this.rotationDelay -= Time.deltaTime;
		}
		if (this.cardsLayoutParent.childCount % 2 == 0 && !this.additionaCardPositionLayoutElement)
		{
			this.additionaCardPositionLayoutElement = Object.Instantiate<GameObject>(this.cardPositionLayoutElementPrefab, this.cardsLayoutParent);
			this.additionaCardPositionLayoutElement.name = "AdditionalCardPositionLayoutElement";
		}
		else if (this.cardsParent.childCount % 2 == 1 && this.additionaCardPositionLayoutElement)
		{
			Object.Destroy(this.additionaCardPositionLayoutElement);
			this.additionaCardPositionLayoutElement = null;
		}
		if (InputManager.instance.inputType == InputManager.InputType.CardSelection)
		{
			if (!this.cardDescriptor)
			{
				this.cardDescriptor = Object.Instantiate<GameObject>(this.cardDescriptorPrefab, CanvasManager.instance.masterContentScaler);
				this.lastCardDescription = null;
			}
			GameObject gameObject = this.GetSelectedCard();
			if (gameObject)
			{
				CardDescription component = gameObject.GetComponent<CardDescription>();
				if (component && this.lastCardDescription != component)
				{
					this.lastCardDescription = component;
					CardDescriptor component2 = this.cardDescriptor.GetComponent<CardDescriptor>();
					component2.SetCardTexts(component.cardName, component.cardDescription);
					component2.SetEnergyRequirement(component.cardEffect.GetNecessaryEnergy());
					component2.SetCardUseAndClassTypes(component.cardEffect.useType, component.cardEffect.lengthOfEffect, component.cardEffect.classType);
					return;
				}
			}
		}
		else if (this.cardDescriptor)
		{
			Object.Destroy(this.cardDescriptor);
		}
	}

	// Token: 0x06000090 RID: 144 RVA: 0x000045A0 File Offset: 0x000027A0
	private void CreateDeck()
	{
		if (RunTypeManager.instance.GetRunTypeModifierExists(RunType.RunProperty.RunPropertyType.StartingDeck))
		{
			return;
		}
		List<GameObject> startingDeck = Singleton.instance.selectedCharacter.startingDeck;
		for (int i = 0; i < startingDeck.Count; i++)
		{
			GameObject gameObject = startingDeck[i];
			int num = Random.Range(i, startingDeck.Count);
			startingDeck[i] = startingDeck[num];
			startingDeck[num] = gameObject;
		}
		foreach (GameObject gameObject2 in startingDeck)
		{
			Object.Instantiate<GameObject>(gameObject2, this.deck).gameObject.SetActive(false);
		}
	}

	// Token: 0x06000091 RID: 145 RVA: 0x00004658 File Offset: 0x00002858
	public GameObject CreateCardLayoutElement()
	{
		return Object.Instantiate<GameObject>(this.cardPositionLayoutElementPrefab, this.cardsLayoutParent);
	}

	// Token: 0x06000092 RID: 146 RVA: 0x0000466C File Offset: 0x0000286C
	public GameObject GetSelectedCard()
	{
		if (this.cardsParent.childCount == 0)
		{
			return null;
		}
		if (this.selectedCard)
		{
			return this.selectedCard;
		}
		return this.cardsParent.GetChild(this.cardsParent.childCount / 2).gameObject;
	}

	// Token: 0x06000093 RID: 147 RVA: 0x000046BC File Offset: 0x000028BC
	public void RotateCards(int direction)
	{
		if (this.rotationDelay > 0f)
		{
			return;
		}
		if (InputManager.instance.controllerType == InputManager.ControllerType.Mouse)
		{
			return;
		}
		SoundManager.instance.PlaySFX("cardClick", -1.0);
		this.rotationDelay = 0.25f;
		List<Transform> list = new List<Transform>();
		for (int i = 0; i < this.cardsParent.childCount; i++)
		{
			list.Add(this.cardsParent.GetChild(i));
		}
		for (int j = 0; j < list.Count; j++)
		{
			list[j].SetSiblingIndex((j - direction + list.Count) % list.Count);
		}
	}

	// Token: 0x06000094 RID: 148 RVA: 0x00004764 File Offset: 0x00002964
	public void CearAllExhausts()
	{
		foreach (CardEffect cardEffect in this.GetAllCards())
		{
			cardEffect.isExhausted = false;
		}
	}

	// Token: 0x06000095 RID: 149 RVA: 0x000047B8 File Offset: 0x000029B8
	public List<CardEffect> GetAllCards()
	{
		List<CardEffect> list = new List<CardEffect>();
		foreach (object obj in this.cardsParent)
		{
			CardEffect component = ((Transform)obj).GetComponent<CardEffect>();
			list.Add(component);
		}
		foreach (object obj2 in this.deck)
		{
			CardEffect component2 = ((Transform)obj2).GetComponent<CardEffect>();
			list.Add(component2);
		}
		foreach (object obj3 in this.discardPile)
		{
			CardEffect component3 = ((Transform)obj3).GetComponent<CardEffect>();
			list.Add(component3);
		}
		return list;
	}

	// Token: 0x06000096 RID: 150 RVA: 0x000048BC File Offset: 0x00002ABC
	private void ReshuffleDiscardIntoDeck()
	{
		List<Transform> list = new List<Transform>();
		foreach (object obj in this.discardPile)
		{
			Transform transform = (Transform)obj;
			list.Add(transform);
		}
		foreach (Transform transform2 in list)
		{
			transform2.gameObject.SetActive(true);
			if (!transform2.GetComponent<CardEffect>().isExhausted)
			{
				transform2.GetComponent<CardPlacement>().ReturnToDeck();
			}
		}
	}

	// Token: 0x06000097 RID: 151 RVA: 0x0000497C File Offset: 0x00002B7C
	public void DeleteAllCards()
	{
		this.DeleteAllCardsInTransform(this.cardsParent);
		this.DeleteAllCardsInTransform(this.deck);
		this.DeleteAllCardsInTransform(this.discardPile);
	}

	// Token: 0x06000098 RID: 152 RVA: 0x000049A4 File Offset: 0x00002BA4
	public void DeleteAllCardsInTransform(Transform t)
	{
		foreach (object obj in t)
		{
			Object.Destroy(((Transform)obj).gameObject);
		}
	}

	// Token: 0x06000099 RID: 153 RVA: 0x000049FC File Offset: 0x00002BFC
	public void ShuffleDeck()
	{
		List<Transform> list = new List<Transform>();
		for (int i = 0; i < this.deck.childCount; i++)
		{
			list.Add(this.deck.GetChild(i));
		}
		for (int j = 0; j < list.Count; j++)
		{
			Transform transform = list[j];
			int num = Random.Range(j, list.Count);
			list[j] = list[num];
			list[num] = transform;
		}
	}

	// Token: 0x0600009A RID: 154 RVA: 0x00004A78 File Offset: 0x00002C78
	public void ReshuffleAllCards()
	{
		foreach (object obj in this.cardsParent)
		{
			((Transform)obj).GetComponent<CardPlacement>().ReturnToDeck();
		}
		this.ReshuffleDiscardIntoDeck();
		this.ShuffleDeck();
	}

	// Token: 0x0600009B RID: 155 RVA: 0x00004AE0 File Offset: 0x00002CE0
	public void DrawCard()
	{
		if (CardPlacement.IsAnythingMoving())
		{
			return;
		}
		if (!this.isAllowedToDraw)
		{
			return;
		}
		if (this.deck.transform.childCount == 0)
		{
			this.ReshuffleDiscardIntoDeck();
			this.ShuffleDeck();
		}
		if (this.deck.transform.childCount == 0)
		{
			return;
		}
		GameObject gameObject = this.deck.transform.GetChild(0).gameObject;
		gameObject.GetComponent<CardPlacement>().DrawCard();
		gameObject.transform.position = this.deck.position;
		gameObject.transform.SetParent(this.cardsParent);
		gameObject.transform.SetAsLastSibling();
		gameObject.gameObject.SetActive(true);
	}

	// Token: 0x0600009C RID: 156 RVA: 0x00004B90 File Offset: 0x00002D90
	public void DiscardAll()
	{
		List<Transform> list = new List<Transform>();
		for (int i = 0; i < this.cardsParent.childCount; i++)
		{
			list.Add(this.cardsParent.GetChild(i));
		}
		foreach (Transform transform in list)
		{
			transform.GetComponent<CardPlacement>().Discard();
		}
	}

	// Token: 0x0400006B RID: 107
	public static CardManager instance;

	// Token: 0x0400006C RID: 108
	[SerializeField]
	public Transform cardsParent;

	// Token: 0x0400006D RID: 109
	[SerializeField]
	private Transform cardsLayoutParent;

	// Token: 0x0400006E RID: 110
	[SerializeField]
	private GameObject cardPositionLayoutElementPrefab;

	// Token: 0x0400006F RID: 111
	[SerializeField]
	private GameObject cardDescriptorPrefab;

	// Token: 0x04000070 RID: 112
	[SerializeField]
	private GameObject cardDescriptor;

	// Token: 0x04000071 RID: 113
	[SerializeField]
	private GameObject cardPrefab;

	// Token: 0x04000072 RID: 114
	[SerializeField]
	public Transform deck;

	// Token: 0x04000073 RID: 115
	[SerializeField]
	public Transform discardPile;

	// Token: 0x04000074 RID: 116
	[SerializeField]
	private CardDescription lastCardDescription;

	// Token: 0x04000075 RID: 117
	[SerializeField]
	public GameObject selectedCard;

	// Token: 0x04000076 RID: 118
	private GameObject additionaCardPositionLayoutElement;

	// Token: 0x04000077 RID: 119
	public bool isAllowedToDraw = true;

	// Token: 0x04000078 RID: 120
	private float rotationDelay = 0.5f;
}
