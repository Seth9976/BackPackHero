using System;
using UnityEngine;
using UnityEngine.InputSystem;

// Token: 0x0200000C RID: 12
public class ButtonPopUp : MonoBehaviour
{
	// Token: 0x06000046 RID: 70 RVA: 0x00003400 File Offset: 0x00001600
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
			this.inputActions.Default.ViewDecks.performed += delegate(InputAction.CallbackContext ctx)
			{
				this.ViewButtonDown();
			};
			this.inputActions.Default.ViewDecks.canceled += delegate(InputAction.CallbackContext ctx)
			{
				this.ViewButtonUp();
			};
			return;
		}
		if (controlType != Singleton.ControlType.Switch)
		{
			return;
		}
		this.inputActions.Default.Movement.performed += delegate(InputAction.CallbackContext ctx)
		{
			this.movementVector = ctx.ReadValue<Vector2>();
		};
		this.inputActions.Default.Movement.canceled += delegate(InputAction.CallbackContext ctx)
		{
			this.movementVector = Vector2.zero;
		};
		this.inputActions.Default.ViewDecks.performed += delegate(InputAction.CallbackContext ctx)
		{
			this.ViewButtonDown();
		};
		this.inputActions.Default.ViewDecks.canceled += delegate(InputAction.CallbackContext ctx)
		{
			this.ViewButtonUp();
		};
	}

	// Token: 0x06000047 RID: 71 RVA: 0x0000355A File Offset: 0x0000175A
	private void Start()
	{
	}

	// Token: 0x06000048 RID: 72 RVA: 0x0000355C File Offset: 0x0000175C
	private void Update()
	{
		if (!ButtonPopUp.viewing)
		{
			this.buttonSymbol.SetActive(false);
			return;
		}
		this.buttonSymbol.SetActive(true);
		if (Vector2.Distance(this.mySetDirection, this.movementVector) < 0.1f)
		{
			ButtonPopUp.viewing = false;
			this.buttonSymbol.SetActive(false);
			GameManager.instance.ToggleDeck(this.transformToView);
		}
	}

	// Token: 0x06000049 RID: 73 RVA: 0x000035C3 File Offset: 0x000017C3
	private void ViewButtonDown()
	{
		ButtonPopUp.viewing = true;
	}

	// Token: 0x0600004A RID: 74 RVA: 0x000035CB File Offset: 0x000017CB
	private void ViewButtonUp()
	{
		ButtonPopUp.viewing = false;
	}

	// Token: 0x04000033 RID: 51
	[SerializeField]
	private Transform transformToView;

	// Token: 0x04000034 RID: 52
	[SerializeField]
	private GameObject buttonSymbol;

	// Token: 0x04000035 RID: 53
	private Vector2 movementVector;

	// Token: 0x04000036 RID: 54
	[SerializeField]
	private Vector2 mySetDirection;

	// Token: 0x04000037 RID: 55
	private InputActions inputActions;

	// Token: 0x04000038 RID: 56
	private static bool viewing;
}
