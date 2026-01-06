using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

// Token: 0x0200000B RID: 11
public class ButtonHandler : MonoBehaviour
{
	// Token: 0x06000038 RID: 56 RVA: 0x00003130 File Offset: 0x00001330
	private void OnEnable()
	{
		this.inputActions = new InputActions();
		this.inputActions.Enable();
		switch (this.key)
		{
		case ButtonHandler.Key.Confirm:
			this.inputActions.Default.Confirm.performed += delegate(InputAction.CallbackContext ctx)
			{
				this.Confirm();
			};
			this.inputActions.Default.Confirm.canceled += delegate(InputAction.CallbackContext ctx)
			{
				this.Cancel();
			};
			break;
		case ButtonHandler.Key.Cancel:
			this.inputActions.Default.Cancel.performed += delegate(InputAction.CallbackContext ctx)
			{
				this.Confirm();
			};
			this.inputActions.Default.Cancel.canceled += delegate(InputAction.CallbackContext ctx)
			{
				this.Cancel();
			};
			break;
		case ButtonHandler.Key.YButton:
			this.inputActions.Default.YButton.performed += delegate(InputAction.CallbackContext ctx)
			{
				this.Confirm();
			};
			this.inputActions.Default.YButton.canceled += delegate(InputAction.CallbackContext ctx)
			{
				this.Cancel();
			};
			break;
		}
		ControllerSpriteManager.instance.spriteSetChanged += this.SpriteSetChanged;
		this.SpriteSetChanged();
	}

	// Token: 0x06000039 RID: 57 RVA: 0x0000326C File Offset: 0x0000146C
	private void OnDisable()
	{
		ControllerSpriteManager.instance.spriteSetChanged -= this.SpriteSetChanged;
		this.inputActions.Disable();
	}

	// Token: 0x0600003A RID: 58 RVA: 0x0000328F File Offset: 0x0000148F
	private void Start()
	{
		this.overlayImage.sprite = this.firstImage.sprite;
	}

	// Token: 0x0600003B RID: 59 RVA: 0x000032A8 File Offset: 0x000014A8
	private void Update()
	{
		if (this.isFilling)
		{
			this.overlayImage.fillAmount = this.fillAmount;
			this.fillAmount += 1.75f * Time.deltaTime;
			if (this.fillAmount > 1f)
			{
				this.isFilling = false;
				this.overlayImage.fillAmount = 0f;
				this.fillAmount = 0f;
				if (this.button != null)
				{
					this.button.onClick.Invoke();
				}
				UnityEvent unityEvent = this.confirmEvent;
				if (unityEvent == null)
				{
					return;
				}
				unityEvent.Invoke();
			}
		}
	}

	// Token: 0x0600003C RID: 60 RVA: 0x00003346 File Offset: 0x00001546
	private void Confirm()
	{
		this.isFilling = true;
	}

	// Token: 0x0600003D RID: 61 RVA: 0x0000334F File Offset: 0x0000154F
	private void Cancel()
	{
		this.isFilling = false;
		this.overlayImage.fillAmount = 0f;
		this.fillAmount = 0f;
	}

	// Token: 0x0600003E RID: 62 RVA: 0x00003374 File Offset: 0x00001574
	private void SpriteSetChanged()
	{
		this.firstImage.enabled = ControllerSpriteManager.instance.isUsingController;
		this.overlayImage.enabled = ControllerSpriteManager.instance.isUsingController;
		this.firstImage.sprite = ControllerSpriteManager.instance.GetKeySprite(this.key);
	}

	// Token: 0x0400002B RID: 43
	[SerializeField]
	public ButtonHandler.Key key;

	// Token: 0x0400002C RID: 44
	[SerializeField]
	private Image firstImage;

	// Token: 0x0400002D RID: 45
	[SerializeField]
	private Image overlayImage;

	// Token: 0x0400002E RID: 46
	private InputActions inputActions;

	// Token: 0x0400002F RID: 47
	private bool isFilling;

	// Token: 0x04000030 RID: 48
	private float fillAmount;

	// Token: 0x04000031 RID: 49
	[Header("Events-------------------")]
	[SerializeField]
	private Button button;

	// Token: 0x04000032 RID: 50
	[SerializeField]
	private UnityEvent confirmEvent;

	// Token: 0x020000BC RID: 188
	public enum Key
	{
		// Token: 0x040003C9 RID: 969
		Confirm,
		// Token: 0x040003CA RID: 970
		Cancel,
		// Token: 0x040003CB RID: 971
		YButton
	}
}
