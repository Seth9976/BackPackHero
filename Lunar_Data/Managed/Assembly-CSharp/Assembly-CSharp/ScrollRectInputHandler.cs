using System;
using UnityEngine;
using UnityEngine.InputSystem;

// Token: 0x0200008D RID: 141
public class ScrollRectInputHandler : MonoBehaviour
{
	// Token: 0x060003B0 RID: 944 RVA: 0x00012588 File Offset: 0x00010788
	private void OnEnable()
	{
		this.inputActions = new InputActions();
		this.inputActions.Enable();
		this.inputActions.Default.Movement.performed += delegate(InputAction.CallbackContext ctx)
		{
			this.movementVector = ctx.ReadValue<Vector2>();
		};
		this.inputActions.Default.Movement.canceled += delegate(InputAction.CallbackContext ctx)
		{
			this.movementVector = Vector2.zero;
		};
	}

	// Token: 0x060003B1 RID: 945 RVA: 0x000125F4 File Offset: 0x000107F4
	private void Update()
	{
		if (this.movementVector != Vector2.zero)
		{
			Vector2 vector = new Vector2(0f, this.movementVector.y);
			this.scrollRectContent.anchoredPosition += vector * -500f * Time.deltaTime;
		}
	}

	// Token: 0x040002D2 RID: 722
	private InputActions inputActions;

	// Token: 0x040002D3 RID: 723
	[SerializeField]
	private RectTransform scrollRectContent;

	// Token: 0x040002D4 RID: 724
	private Vector2 movementVector;
}
