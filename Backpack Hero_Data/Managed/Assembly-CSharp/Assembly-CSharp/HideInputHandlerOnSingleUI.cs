using System;
using UnityEngine;

// Token: 0x0200005A RID: 90
public class HideInputHandlerOnSingleUI : MonoBehaviour
{
	// Token: 0x06000197 RID: 407 RVA: 0x0000A558 File Offset: 0x00008758
	private void FindInputHandler()
	{
		if (!this.inputHandler)
		{
			this.inputHandler = base.GetComponent<InputHandler>();
		}
	}

	// Token: 0x06000198 RID: 408 RVA: 0x0000A573 File Offset: 0x00008773
	private void OnEnable()
	{
		this.findInputHandler = true;
	}

	// Token: 0x06000199 RID: 409 RVA: 0x0000A57C File Offset: 0x0000877C
	private void Update()
	{
		if (SingleUI.IsViewingPopUp())
		{
			this.findInputHandler = true;
			if (!this.inputHandler)
			{
				return;
			}
			this.inputHandler.enabled = false;
			return;
		}
		else
		{
			if (this.findInputHandler)
			{
				this.FindInputHandler();
				this.findInputHandler = false;
				return;
			}
			if (!this.inputHandler)
			{
				return;
			}
			this.inputHandler.enabled = true;
			return;
		}
	}

	// Token: 0x04000105 RID: 261
	[SerializeField]
	private InputHandler inputHandler;

	// Token: 0x04000106 RID: 262
	private bool findInputHandler = true;
}
