using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Token: 0x02000027 RID: 39
public class DeckPanel : MonoBehaviour
{
	// Token: 0x0600011F RID: 287 RVA: 0x00006D14 File Offset: 0x00004F14
	private void OnEnable()
	{
		DeckPanel.instance = this;
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
				this.ConfirmSelection();
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
			this.ConfirmSelection();
		};
	}

	// Token: 0x06000120 RID: 288 RVA: 0x00006E29 File Offset: 0x00005029
	private void OnDisable()
	{
		DeckPanel.instance = null;
		this.inputActions.Disable();
	}

	// Token: 0x06000121 RID: 289 RVA: 0x00006E3C File Offset: 0x0000503C
	private void Start()
	{
	}

	// Token: 0x06000122 RID: 290 RVA: 0x00006E40 File Offset: 0x00005040
	public void ConfirmSelection()
	{
		if (!this.deckSelector.selectedObject)
		{
			return;
		}
		if (!EventManager.instance.eventResultPanel)
		{
			return;
		}
		DeckCard component = this.deckSelector.selectedObject.GetComponent<DeckCard>();
		if (!component)
		{
			return;
		}
		SingleUI component2 = base.GetComponent<SingleUI>();
		if (component2)
		{
			component2.CloseAndDestroyViaFade();
		}
		EventManager.instance.selectedCardFromEvent = component.cardReference;
		EventManager.instance.CreateEvent();
	}

	// Token: 0x06000123 RID: 291 RVA: 0x00006EBB File Offset: 0x000050BB
	public void SetDeckName(string deckName)
	{
		this.deckNameText.SetKey(deckName);
	}

	// Token: 0x06000124 RID: 292 RVA: 0x00006ECC File Offset: 0x000050CC
	public void ShowRelics()
	{
		foreach (Transform transform in new List<Transform> { RelicManager.instance.transform })
		{
			this.ShowCards(transform);
		}
	}

	// Token: 0x06000125 RID: 293 RVA: 0x00006F30 File Offset: 0x00005130
	public void ShowAllCards()
	{
		foreach (Transform transform in new List<Transform>
		{
			CardManager.instance.deck,
			CardManager.instance.cardsParent,
			CardManager.instance.discardPile
		})
		{
			this.ShowCards(transform);
		}
		this.SetDeckName("All Cards");
	}

	// Token: 0x06000126 RID: 294 RVA: 0x00006FC0 File Offset: 0x000051C0
	public void ShowCards(Transform origin)
	{
		DeckInfo component = origin.GetComponent<DeckInfo>();
		if (component)
		{
			this.SetDeckName(component.deckName);
		}
		foreach (object obj in origin)
		{
			Transform transform = (Transform)obj;
			Object.Instantiate<GameObject>(this.deckCardPrefab, this.deckContentParent).GetComponent<DeckCard>().cardReference = transform.gameObject;
		}
		if (this.deckContentParent.childCount > 0)
		{
			this.deckSelector.SetSelectedObject(this.deckContentParent.GetChild(0).gameObject);
		}
	}

	// Token: 0x06000127 RID: 295 RVA: 0x00007074 File Offset: 0x00005274
	private void Update()
	{
		this.timeSinceMove += Time.deltaTime;
		if (this.timeSinceMove < 0.1f)
		{
			return;
		}
		if (!this.deckSelector.selectedObject)
		{
			return;
		}
		int siblingIndex = this.deckSelector.selectedObject.transform.GetSiblingIndex();
		if (this.movementVector.x < -0.1f && Mathf.Abs(this.movementVector.x) > Mathf.Abs(this.movementVector.y) && siblingIndex > 0)
		{
			this.deckSelector.SetSelectedObject(this.deckContentParent.GetChild(siblingIndex - 1).gameObject);
			this.timeSinceMove = 0f;
			return;
		}
		if (this.movementVector.x > 0.1f && Mathf.Abs(this.movementVector.x) > Mathf.Abs(this.movementVector.y) && siblingIndex < this.deckContentParent.childCount - 1)
		{
			this.deckSelector.SetSelectedObject(this.deckContentParent.GetChild(siblingIndex + 1).gameObject);
			this.timeSinceMove = 0f;
			return;
		}
		if (this.movementVector.y < -0.1f && Mathf.Abs(this.movementVector.y) > Mathf.Abs(this.movementVector.x) && siblingIndex + this.rowLength < this.deckContentParent.childCount)
		{
			this.deckSelector.SetSelectedObject(this.deckContentParent.GetChild(siblingIndex + this.rowLength).gameObject);
			this.timeSinceMove = 0f;
			return;
		}
		if (this.movementVector.y > 0.1f && Mathf.Abs(this.movementVector.y) > Mathf.Abs(this.movementVector.x) && siblingIndex - this.rowLength >= 0)
		{
			this.deckSelector.SetSelectedObject(this.deckContentParent.GetChild(siblingIndex - this.rowLength).gameObject);
			this.timeSinceMove = 0f;
		}
	}

	// Token: 0x06000128 RID: 296 RVA: 0x00007280 File Offset: 0x00005480
	public void ShowCardInfo(DeckCard deckCard)
	{
		if (this.cardDescriptorReference)
		{
			Object.Destroy(this.cardDescriptorReference);
		}
		this.cardDescriptorReference = Object.Instantiate<GameObject>(this.cardDescriptorPrefab, this.cardDescriptorParentTransform);
		CardDescription component = deckCard.cardReference.GetComponent<CardDescription>();
		CardDescriptor component2 = this.cardDescriptorReference.GetComponent<CardDescriptor>();
		component2.SetCardTexts(component.cardName, component.cardDescription);
		if (component.cardEffect)
		{
			component2.SetEnergyRequirement(component.cardEffect.GetNecessaryEnergy());
			component2.SetCardUseAndClassTypes(component.cardEffect.useType, component.cardEffect.lengthOfEffect, component.cardEffect.classType);
			return;
		}
		component2.DisableEnergyRequirement();
		component2.DisableUseAndClassTypes();
	}

	// Token: 0x040000DB RID: 219
	public static DeckPanel instance;

	// Token: 0x040000DC RID: 220
	[SerializeField]
	private ReplacementText deckNameText;

	// Token: 0x040000DD RID: 221
	[SerializeField]
	private DeckSelector deckSelector;

	// Token: 0x040000DE RID: 222
	[SerializeField]
	private GameObject cardDescriptorPrefab;

	// Token: 0x040000DF RID: 223
	[SerializeField]
	private GameObject cardDescriptorReference;

	// Token: 0x040000E0 RID: 224
	[SerializeField]
	private Transform cardDescriptorParentTransform;

	// Token: 0x040000E1 RID: 225
	[SerializeField]
	private Transform deckContentParent;

	// Token: 0x040000E2 RID: 226
	[SerializeField]
	private int rowLength = 4;

	// Token: 0x040000E3 RID: 227
	[SerializeField]
	private GameObject deckCardPrefab;

	// Token: 0x040000E4 RID: 228
	[SerializeField]
	private GameObject doneButton;

	// Token: 0x040000E5 RID: 229
	private InputActions inputActions;

	// Token: 0x040000E6 RID: 230
	private Vector2 movementVector;

	// Token: 0x040000E7 RID: 231
	private float timeSinceMove;
}
