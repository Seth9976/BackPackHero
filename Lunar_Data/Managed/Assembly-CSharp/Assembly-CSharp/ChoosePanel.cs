using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

// Token: 0x0200001D RID: 29
public class ChoosePanel : MonoBehaviour
{
	// Token: 0x060000D0 RID: 208 RVA: 0x00005B94 File Offset: 0x00003D94
	private void OnEnable()
	{
		this.inputActions = new InputActions();
		this.inputActions.Enable();
		Singleton.ControlType controlType = Singleton.instance.controlType;
		if (controlType == Singleton.ControlType.Xbox)
		{
			this.inputActions.Default.Movement.performed += delegate(InputAction.CallbackContext ctx)
			{
				this.movementVector = ctx.ReadValue<Vector2>();
			};
			this.inputActions.Default.Movement.canceled += delegate(InputAction.CallbackContext ctx)
			{
				this.movementVector = Vector2.zero;
			};
			this.inputActions.Default.Confirm.performed += delegate(InputAction.CallbackContext ctx)
			{
				this.CardButton();
			};
			return;
		}
		if (controlType != Singleton.ControlType.Switch)
		{
			return;
		}
		this.inputActions.Switch.Movement.performed += delegate(InputAction.CallbackContext ctx)
		{
			this.movementVector = ctx.ReadValue<Vector2>();
		};
		this.inputActions.Switch.Movement.canceled += delegate(InputAction.CallbackContext ctx)
		{
			this.movementVector = Vector2.zero;
		};
		this.inputActions.Switch.Confirm.performed += delegate(InputAction.CallbackContext ctx)
		{
			this.CardButton();
		};
	}

	// Token: 0x060000D1 RID: 209 RVA: 0x00005CA3 File Offset: 0x00003EA3
	private void OnDisable()
	{
		this.inputActions.Disable();
	}

	// Token: 0x060000D2 RID: 210 RVA: 0x00005CB0 File Offset: 0x00003EB0
	private void Start()
	{
		ChoosePanel.Type type = this.type;
		if (type != ChoosePanel.Type.Card)
		{
			if (type == ChoosePanel.Type.Relic)
			{
				this.CreateRelicOptions();
			}
		}
		else
		{
			this.CreateCardOptions();
		}
		SoundManager.instance.PlaySFX("openDeck", -1.0);
	}

	// Token: 0x060000D3 RID: 211 RVA: 0x00005CF4 File Offset: 0x00003EF4
	private void Update()
	{
		this.timeOpen += Time.deltaTime;
		this.timeSinceLastInput += Time.deltaTime;
		if (this.timeSinceLastInput < 0.3f)
		{
			return;
		}
		if (this.movementVector.x > 0.1f)
		{
			this.GetCardInDirection(this.selectedCard.position, Vector2.right);
			return;
		}
		if (this.movementVector.x < -0.1f)
		{
			this.GetCardInDirection(this.selectedCard.position, Vector2.left);
		}
	}

	// Token: 0x060000D4 RID: 212 RVA: 0x00005D84 File Offset: 0x00003F84
	public void GetCardInDirection(Vector3 start, Vector2 direction)
	{
		float num = float.PositiveInfinity;
		ChooseCard chooseCard = null;
		foreach (ChooseCard chooseCard2 in ChooseCard.chooseCards)
		{
			if (!(chooseCard2 == this.selectedCard) && (double)Vector2.Dot(direction, (chooseCard2.transform.position - start).normalized) >= 0.5)
			{
				float num2 = Vector3.Distance(start, chooseCard2.transform.position);
				if (num2 < num)
				{
					num = num2;
					chooseCard = chooseCard2;
				}
			}
		}
		if (chooseCard)
		{
			this.timeSinceLastInput = 0f;
			this.selectedCard = chooseCard.transform;
			SoundManager.instance.PlaySFX("cardClick", -1.0);
		}
	}

	// Token: 0x060000D5 RID: 213 RVA: 0x00005E6C File Offset: 0x0000406C
	private void CardButton()
	{
		if (this.timeOpen <= 1f)
		{
			return;
		}
		this.selectedCard.GetComponent<ChooseCard>().SelectThisCard();
	}

	// Token: 0x060000D6 RID: 214 RVA: 0x00005E8C File Offset: 0x0000408C
	private void CancelButton()
	{
		Object.Destroy(base.gameObject);
	}

	// Token: 0x060000D7 RID: 215 RVA: 0x00005E9C File Offset: 0x0000409C
	private void CreateCardOptions()
	{
		foreach (GameObject gameObject in Object.FindObjectOfType<CardSpawner>().GetRandomCards(3))
		{
			this.CreateCard(gameObject);
		}
		this.selectedCard = this.cardsParent.GetChild(1);
	}

	// Token: 0x060000D8 RID: 216 RVA: 0x00005F08 File Offset: 0x00004108
	private void CreateRelicOptions()
	{
		foreach (GameObject gameObject in Object.FindObjectOfType<CardSpawner>().GetRandomRelics(3))
		{
			this.CreateCard(gameObject);
		}
		this.selectedCard = this.cardsParent.GetChild(1);
	}

	// Token: 0x060000D9 RID: 217 RVA: 0x00005F74 File Offset: 0x00004174
	private GameObject CreateCard(GameObject card)
	{
		GameObject gameObject = Object.Instantiate<GameObject>(this.chooseCardLayoutPrefab, this.cardsLayoutParent);
		GameObject gameObject2 = Object.Instantiate<GameObject>(this.fullCardPrefab, this.cardsParent);
		CardDescriptor component = gameObject2.GetComponent<CardDescriptor>();
		gameObject2.GetComponent<FollowObject>().objectToFollow = gameObject.transform;
		CardDescription component2 = card.GetComponent<CardDescription>();
		SpriteRenderer component3 = card.GetComponent<SpriteRenderer>();
		Image component4 = card.GetComponent<Image>();
		if (component3)
		{
			component.SetCardImage(component3.sprite);
		}
		else if (component4)
		{
			component.SetCardImage(component4.sprite);
		}
		component.SetCardTexts(component2.cardName, component2.cardDescription);
		CardEffect component5 = card.GetComponent<CardEffect>();
		if (component5)
		{
			component.SetEnergyRequirement(component5.GetNecessaryEnergy());
			component.SetCardUseAndClassTypes(component5.useType, component5.lengthOfEffect, component5.classType);
		}
		else
		{
			component.DisableEnergyRequirement();
		}
		gameObject2.GetComponent<ChooseCard>().cardPrefab = card;
		return gameObject2;
	}

	// Token: 0x060000DA RID: 218 RVA: 0x0000605D File Offset: 0x0000425D
	public bool HasChoseAlready()
	{
		return this.hasChosen;
	}

	// Token: 0x060000DB RID: 219 RVA: 0x00006065 File Offset: 0x00004265
	public void SetHasChosen(bool hasChosen)
	{
		this.hasChosen = hasChosen;
	}

	// Token: 0x0400009A RID: 154
	[SerializeField]
	private ChoosePanel.Type type;

	// Token: 0x0400009B RID: 155
	private bool hasChosen;

	// Token: 0x0400009C RID: 156
	[SerializeField]
	private Vector2 movementVector;

	// Token: 0x0400009D RID: 157
	[SerializeField]
	private Transform cardsLayoutParent;

	// Token: 0x0400009E RID: 158
	[SerializeField]
	private Transform cardsParent;

	// Token: 0x0400009F RID: 159
	[SerializeField]
	private GameObject chooseCardLayoutPrefab;

	// Token: 0x040000A0 RID: 160
	[SerializeField]
	private GameObject fullCardPrefab;

	// Token: 0x040000A1 RID: 161
	[SerializeField]
	public Transform selectedCard;

	// Token: 0x040000A2 RID: 162
	private float timeSinceLastInput;

	// Token: 0x040000A3 RID: 163
	public float timeOpen;

	// Token: 0x040000A4 RID: 164
	private InputActions inputActions;

	// Token: 0x020000C7 RID: 199
	public enum Type
	{
		// Token: 0x04000400 RID: 1024
		Card,
		// Token: 0x04000401 RID: 1025
		Relic
	}
}
