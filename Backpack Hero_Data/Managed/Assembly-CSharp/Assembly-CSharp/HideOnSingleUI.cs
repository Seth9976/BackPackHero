using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200005C RID: 92
public class HideOnSingleUI : MonoBehaviour
{
	// Token: 0x0600019E RID: 414 RVA: 0x0000A624 File Offset: 0x00008824
	private void Start()
	{
		this.localPosition = base.transform.localPosition;
		if (!this.canvasGroup)
		{
			this.canvasGroup = base.GetComponent<CanvasGroup>();
		}
		if (!this.animator)
		{
			this.animator = base.GetComponent<Animator>();
		}
	}

	// Token: 0x0600019F RID: 415 RVA: 0x0000A674 File Offset: 0x00008874
	private void Update()
	{
		bool flag = SingleUI.IsViewingPopUp();
		if (this.delay > 0f && !flag)
		{
			this.delay -= Time.deltaTime;
			return;
		}
		if (!flag)
		{
			if (this.wasHidden)
			{
				if (this.changePosition)
				{
					base.transform.localPosition = this.localPosition;
				}
				this.wasHidden = false;
				this.customEventForReenable.Invoke();
				if (this.animator && this.showState == SingleUI.State.Animation)
				{
					this.animator.SetTrigger("Open");
				}
				InputHandler[] array = base.GetComponentsInChildren<InputHandler>();
				for (int i = 0; i < array.Length; i++)
				{
					array[i].enabled = true;
				}
			}
			this.localPosition = base.transform.localPosition;
		}
		else
		{
			this.delay = 0.1f;
			if (!this.wasHidden && this.animator && this.hideState == SingleUI.State.Animation)
			{
				this.animator.SetTrigger("Close");
			}
			if (this.changePosition)
			{
				base.transform.localPosition = Vector3.one * -9999f;
			}
			InputHandler[] array = base.GetComponentsInChildren<InputHandler>();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].enabled = false;
			}
			this.wasHidden = true;
		}
		if (!this.canvasGroup)
		{
			return;
		}
		if (flag)
		{
			if (this.hideState == SingleUI.State.Instant)
			{
				this.canvasGroup.alpha = 0f;
			}
			else if (this.hideState == SingleUI.State.Fade)
			{
				this.canvasGroup.alpha = Mathf.MoveTowards(this.canvasGroup.alpha, 0f, Time.deltaTime * 2f);
			}
			this.canvasGroup.interactable = false;
			this.canvasGroup.blocksRaycasts = false;
			return;
		}
		if (this.showState == SingleUI.State.Instant)
		{
			this.canvasGroup.alpha = 1f;
		}
		else if (this.showState == SingleUI.State.Fade)
		{
			this.canvasGroup.alpha = Mathf.MoveTowards(this.canvasGroup.alpha, 1f, Time.deltaTime * 2f);
		}
		this.canvasGroup.interactable = true;
		this.canvasGroup.blocksRaycasts = true;
	}

	// Token: 0x04000108 RID: 264
	[Header("Options")]
	[SerializeField]
	private SingleUI.State hideState;

	// Token: 0x04000109 RID: 265
	[SerializeField]
	private SingleUI.State showState;

	// Token: 0x0400010A RID: 266
	[Header("Optional References")]
	[SerializeField]
	private CanvasGroup canvasGroup;

	// Token: 0x0400010B RID: 267
	[SerializeField]
	private Animator animator;

	// Token: 0x0400010C RID: 268
	[SerializeField]
	private UnityEvent customEventForReenable;

	// Token: 0x0400010D RID: 269
	public bool changePosition = true;

	// Token: 0x0400010E RID: 270
	private Vector3 localPosition;

	// Token: 0x0400010F RID: 271
	private bool wasHidden;

	// Token: 0x04000110 RID: 272
	private float delay;

	// Token: 0x04000111 RID: 273
	private const float currentDelay = 0.1f;
}
