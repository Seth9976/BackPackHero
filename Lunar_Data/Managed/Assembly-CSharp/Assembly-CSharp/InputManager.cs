using System;
using UnityEngine;
using UnityEngine.InputSystem;

// Token: 0x0200004C RID: 76
public class InputManager : MonoBehaviour
{
	// Token: 0x0600022B RID: 555 RVA: 0x0000B320 File Offset: 0x00009520
	private void OnEnable()
	{
		if (InputManager.instance == null)
		{
			InputManager.instance = this;
		}
		else if (InputManager.instance != this)
		{
			Object.Destroy(base.gameObject);
		}
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
			this.inputActions.Default.Confirm.started += delegate(InputAction.CallbackContext ctx)
			{
				this.CardButton();
			};
			this.inputActions.Default.Cancel.started += delegate(InputAction.CallbackContext ctx)
			{
				this.CancelButton();
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
		this.inputActions.Switch.Confirm.started += delegate(InputAction.CallbackContext ctx)
		{
			this.CardButton();
		};
		this.inputActions.Switch.Cancel.started += delegate(InputAction.CallbackContext ctx)
		{
			this.CancelButton();
		};
	}

	// Token: 0x0600022C RID: 556 RVA: 0x0000B4A7 File Offset: 0x000096A7
	private void OnDisable()
	{
		if (InputManager.instance == this)
		{
			InputManager.instance = null;
		}
		this.inputActions.Disable();
	}

	// Token: 0x0600022D RID: 557 RVA: 0x0000B4C7 File Offset: 0x000096C7
	private void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(this.cardPosition, 0.1f);
	}

	// Token: 0x0600022E RID: 558 RVA: 0x0000B4DE File Offset: 0x000096DE
	private void Start()
	{
	}

	// Token: 0x0600022F RID: 559 RVA: 0x0000B4E0 File Offset: 0x000096E0
	private void Update()
	{
		if (!this.IsGameInput())
		{
			return;
		}
		if (Input.GetMouseButtonDown(0))
		{
			this.controllerType = InputManager.ControllerType.Mouse;
		}
		switch (this.inputType)
		{
		case InputManager.InputType.PlayerMovement:
			this.cardPosition = Player.instance.transform.position;
			return;
		case InputManager.InputType.CardSelection:
			this.cardPosition = Player.instance.transform.position + Vector3.down * 0.1f;
			this.chosenCardEffect = null;
			if (Mathf.Abs(Mathf.RoundToInt(this.movementVector.x)) >= 1)
			{
				CardManager.instance.RotateCards(Mathf.RoundToInt(this.movementVector.x));
				return;
			}
			break;
		case InputManager.InputType.CardPlacement:
			this.PreviewAim();
			break;
		default:
			return;
		}
	}

	// Token: 0x06000230 RID: 560 RVA: 0x0000B5A8 File Offset: 0x000097A8
	public void PreviewAim()
	{
		if (!this.chosenCardEffect)
		{
			return;
		}
		if (this.controllerType == InputManager.ControllerType.Mouse)
		{
			this.cardPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		}
		if (this.chosenCardEffect.showRange)
		{
			this.rangeIndicator.gameObject.SetActive(true);
			this.rangeIndicator.DrawCircle(Player.instance.transform.position, this.chosenCardEffect.rangeOfPlacement);
		}
		else
		{
			this.rangeIndicator.gameObject.SetActive(false);
		}
		switch (this.chosenCardEffect.placementType)
		{
		case CardEffect.PlacementType.NoLocation:
			this.actionIndicator.ClearLine();
			this.cardPosition = Player.instance.transform.position;
			return;
		case CardEffect.PlacementType.Local:
			this.actionIndicator.DrawCircle(Player.instance.transform.position, this.chosenCardEffect.GetRangeOfEffect());
			this.cardPosition = Player.instance.transform.position;
			return;
		case CardEffect.PlacementType.Arc:
			this.cardPosition += this.movementVector * Time.deltaTime * this.cardPositionSpeed;
			if (Vector2.Distance(this.cardPosition, Player.instance.transform.position) < 0.75f)
			{
				Vector2 vector = this.cardPosition - Player.instance.transform.position;
				vector.Normalize();
				this.cardPosition = Player.instance.transform.position + vector * 0.75f;
			}
			if (Vector2.Distance(this.cardPosition, Player.instance.transform.position) > 2f)
			{
				Vector2 vector2 = this.cardPosition - Player.instance.transform.position;
				vector2.Normalize();
				this.cardPosition = Player.instance.transform.position + vector2 * 2f;
			}
			this.actionIndicator.DrawArc(this.cardPosition, this.chosenCardEffect.GetRangeOfEffect());
			return;
		case CardEffect.PlacementType.PositionWithinRange:
			this.cardPosition += this.movementVector * Time.deltaTime * this.cardPositionSpeed;
			if (Vector2.Distance(this.cardPosition, Player.instance.transform.position) > this.chosenCardEffect.rangeOfPlacement)
			{
				Vector2 vector3 = this.cardPosition - Player.instance.transform.position;
				vector3.Normalize();
				this.cardPosition = Player.instance.transform.position + vector3 * this.chosenCardEffect.rangeOfPlacement;
			}
			this.actionIndicator.DrawCircle(this.cardPosition, this.chosenCardEffect.GetRangeOfEffect());
			return;
		case CardEffect.PlacementType.Line:
		{
			this.cardPosition += this.movementVector * Time.deltaTime * this.cardPositionSpeed;
			if (Vector2.Distance(this.cardPosition, Player.instance.transform.position) < 0.75f)
			{
				Vector2 vector4 = this.cardPosition - Player.instance.transform.position;
				vector4.Normalize();
				this.cardPosition = Player.instance.transform.position + vector4 * 0.75f;
			}
			if (Vector2.Distance(this.cardPosition, Player.instance.transform.position) > 2f)
			{
				Vector2 vector5 = this.cardPosition - Player.instance.transform.position;
				vector5.Normalize();
				this.cardPosition = Player.instance.transform.position + vector5 * 2f;
			}
			RaycastHit2D raycastHit2D = Physics2D.Raycast(Player.instance.transform.position, this.cardPosition - Player.instance.transform.position, 1000f, LayerMask.GetMask(new string[] { "Level" }));
			if (raycastHit2D.collider != null)
			{
				this.actionIndicator.DrawLine(Player.instance.transform.position, raycastHit2D.point);
				return;
			}
			this.actionIndicator.DrawLine(Player.instance.transform.position, Player.instance.transform.position + (this.cardPosition - Player.instance.transform.position).normalized * 1000f);
			return;
		}
		default:
			return;
		}
	}

	// Token: 0x06000231 RID: 561 RVA: 0x0000BAEB File Offset: 0x00009CEB
	public Vector2 GetCardPosition()
	{
		return this.cardPosition;
	}

	// Token: 0x06000232 RID: 562 RVA: 0x0000BAF3 File Offset: 0x00009CF3
	public Vector3[] GetAimLocations()
	{
		return this.actionIndicator.GetPositions();
	}

	// Token: 0x06000233 RID: 563 RVA: 0x0000BB00 File Offset: 0x00009D00
	public bool IsGameInput()
	{
		return !SingleUI.IsViewingPopUp() && Player.instance && !Player.instance.isDead;
	}

	// Token: 0x06000234 RID: 564 RVA: 0x0000BB26 File Offset: 0x00009D26
	public bool IsPlayerMovementMode()
	{
		return this.inputType == InputManager.InputType.PlayerMovement;
	}

	// Token: 0x06000235 RID: 565 RVA: 0x0000BB34 File Offset: 0x00009D34
	public void MouseRelease()
	{
		if (Input.mousePosition.y <= (float)Screen.height * 0.1f)
		{
			this.CancelPlacement();
			return;
		}
		if (!this.chosenCardEffect)
		{
			return;
		}
		if (!this.chosenCardEffect.ConsiderActivate())
		{
			this.CancelPlacement();
			return;
		}
		this.actionIndicator.gameObject.SetActive(false);
		this.rangeIndicator.gameObject.SetActive(false);
		this.inputType = InputManager.InputType.PlayerMovement;
		Player.instance.EndConsiderUseItem();
	}

	// Token: 0x06000236 RID: 566 RVA: 0x0000BBB8 File Offset: 0x00009DB8
	private void CardButton()
	{
		this.controllerType = InputManager.ControllerType.Buttons;
		if (!this.IsGameInput())
		{
			return;
		}
		switch (this.inputType)
		{
		case InputManager.InputType.PlayerMovement:
			if (CardManager.instance.cardsParent.childCount != 0)
			{
				this.inputType = InputManager.InputType.CardSelection;
				SoundManager.instance.PlaySFX("openDeck", -1.0);
				return;
			}
			break;
		case InputManager.InputType.CardSelection:
			this.SelectCard();
			return;
		case InputManager.InputType.CardPlacement:
		{
			GameObject selectedCard = CardManager.instance.GetSelectedCard();
			if (selectedCard && selectedCard.GetComponent<CardEffect>().ConsiderActivate())
			{
				this.actionIndicator.gameObject.SetActive(false);
				this.rangeIndicator.gameObject.SetActive(false);
				this.inputType = InputManager.InputType.PlayerMovement;
				Player.instance.EndConsiderUseItem();
			}
			break;
		}
		default:
			return;
		}
	}

	// Token: 0x06000237 RID: 567 RVA: 0x0000BC7C File Offset: 0x00009E7C
	public void SelectCard()
	{
		if (!this.IsGameInput())
		{
			return;
		}
		GameObject selectedCard = CardManager.instance.GetSelectedCard();
		if (!selectedCard)
		{
			return;
		}
		CardEffect component = selectedCard.GetComponent<CardEffect>();
		if (!component.CanActivate())
		{
			return;
		}
		SoundManager.instance.PlaySFX("selectCard", double.PositiveInfinity);
		this.chosenCardEffect = component;
		CardPlacement component2 = selectedCard.GetComponent<CardPlacement>();
		this.inputType = InputManager.InputType.CardPlacement;
		this.actionIndicator.gameObject.SetActive(true);
		this.actionIndicator.SetSortingOrder(component.actionIndicatorSortingOrder);
		if (component2)
		{
			Player.instance.ShowConsiderUseItem(component2.GetSprite());
		}
	}

	// Token: 0x06000238 RID: 568 RVA: 0x0000BD20 File Offset: 0x00009F20
	public void OpenDeck()
	{
		if (!InputManager.instance.IsGameInput())
		{
			return;
		}
		if (CardManager.instance.cardsParent.childCount == 0)
		{
			return;
		}
		if (this.inputType != InputManager.InputType.CardSelection)
		{
			SoundManager.instance.PlaySFX("openDeck", -1.0);
		}
		this.inputType = InputManager.InputType.CardSelection;
	}

	// Token: 0x06000239 RID: 569 RVA: 0x0000BD74 File Offset: 0x00009F74
	public void CloseDeck()
	{
		if (this.inputType == InputManager.InputType.CardSelection)
		{
			SoundManager.instance.PlaySFX("closeDeck", -1.0);
			this.inputType = InputManager.InputType.PlayerMovement;
		}
	}

	// Token: 0x0600023A RID: 570 RVA: 0x0000BDA0 File Offset: 0x00009FA0
	private void CancelButton()
	{
		this.controllerType = InputManager.ControllerType.Buttons;
		if (!this.IsGameInput())
		{
			return;
		}
		InputManager.InputType inputType = this.inputType;
		if (inputType == InputManager.InputType.CardSelection)
		{
			this.inputType = InputManager.InputType.PlayerMovement;
			SoundManager.instance.PlaySFX("closeDeck", -1.0);
			return;
		}
		if (inputType != InputManager.InputType.CardPlacement)
		{
			return;
		}
		this.CancelPlacement();
	}

	// Token: 0x0600023B RID: 571 RVA: 0x0000BDF4 File Offset: 0x00009FF4
	public void CancelPlacement()
	{
		this.chosenCardEffect = null;
		this.actionIndicator.gameObject.SetActive(false);
		this.rangeIndicator.gameObject.SetActive(false);
		this.inputType = InputManager.InputType.CardSelection;
		Player.instance.EndConsiderUseItem();
		SoundManager.instance.PlaySFX("closeDeck", -1.0);
	}

	// Token: 0x040001A4 RID: 420
	[SerializeField]
	public InputManager.ControllerType controllerType;

	// Token: 0x040001A5 RID: 421
	[SerializeField]
	public static InputManager instance;

	// Token: 0x040001A6 RID: 422
	public InputManager.InputType inputType;

	// Token: 0x040001A7 RID: 423
	[SerializeField]
	private Vector2 movementVector;

	// Token: 0x040001A8 RID: 424
	[SerializeField]
	public Vector2 cardPosition;

	// Token: 0x040001A9 RID: 425
	[SerializeField]
	private ActionIndicator actionIndicator;

	// Token: 0x040001AA RID: 426
	[SerializeField]
	private ActionIndicator rangeIndicator;

	// Token: 0x040001AB RID: 427
	[SerializeField]
	public float cardPositionSpeed = 5f;

	// Token: 0x040001AC RID: 428
	private CardEffect chosenCardEffect;

	// Token: 0x040001AD RID: 429
	private InputActions inputActions;

	// Token: 0x020000E3 RID: 227
	public enum ControllerType
	{
		// Token: 0x04000453 RID: 1107
		Mouse,
		// Token: 0x04000454 RID: 1108
		Buttons
	}

	// Token: 0x020000E4 RID: 228
	public enum InputType
	{
		// Token: 0x04000456 RID: 1110
		PlayerMovement,
		// Token: 0x04000457 RID: 1111
		CardSelection,
		// Token: 0x04000458 RID: 1112
		CardPlacement
	}
}
