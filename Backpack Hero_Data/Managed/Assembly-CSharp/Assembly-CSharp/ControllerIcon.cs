using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200001A RID: 26
public class ControllerIcon : MonoBehaviour
{
	// Token: 0x060000B3 RID: 179 RVA: 0x00006530 File Offset: 0x00004730
	public void FollowTransformCenter(Transform t, RectTransform parent)
	{
		Debug.Log(string.Format("AAA: parent: {0}", parent));
		this.followType = ControllerIcon.FollowType.FollowCenter;
		this.followTransform = t;
		if (parent != null)
		{
			base.transform.parent = parent;
		}
		base.transform.position = this.followTransform.position;
	}

	// Token: 0x060000B4 RID: 180 RVA: 0x00006588 File Offset: 0x00004788
	public void FollowTransformUpperRight(RectTransform t, RectTransform parent)
	{
		if (!t)
		{
			return;
		}
		if (!this.rectTransform)
		{
			this.rectTransform = base.GetComponent<RectTransform>();
		}
		this.followType = ControllerIcon.FollowType.FollowUpperRight;
		this.followTransform = t.transform;
		this.followRectTransform = t;
		Debug.Log(string.Format("AAA: parent: {0}", parent));
		if (parent != null)
		{
			base.transform.parent = parent;
		}
		base.transform.position = this.followRectTransform.TransformPoint(this.followRectTransform.rect.max);
	}

	// Token: 0x060000B5 RID: 181 RVA: 0x00006624 File Offset: 0x00004824
	public void SetAsInstant(bool requiresHold = false)
	{
		this.requiresHold = requiresHold;
		if (this.barFillUI)
		{
			Object.Destroy(this.barFillUI.gameObject);
		}
	}

	// Token: 0x060000B6 RID: 182 RVA: 0x0000664C File Offset: 0x0000484C
	public void ShowIcon()
	{
		if (this.image)
		{
			this.image.enabled = true;
		}
		if (this.barFillUI)
		{
			this.barFillUI.enabled = true;
		}
		if (this.holdText)
		{
			this.holdText.enabled = true;
		}
	}

	// Token: 0x060000B7 RID: 183 RVA: 0x000066A4 File Offset: 0x000048A4
	public void ConsiderHiding()
	{
		if (!this.inputHandler)
		{
			return;
		}
		if (this.inputHandler.showIcon == InputHandler.ShowIcon.Never || (DigitalCursor.main.controlStyle == DigitalCursor.ControlStyle.cursor && this.inputHandler.showIcon == InputHandler.ShowIcon.ControllerOnly))
		{
			this.HideIcon();
			return;
		}
		this.ShowIcon();
	}

	// Token: 0x060000B8 RID: 184 RVA: 0x000066F4 File Offset: 0x000048F4
	public void HideIcon()
	{
		if (this.image)
		{
			this.image.enabled = false;
		}
		if (this.barFillUI)
		{
			this.barFillUI.enabled = false;
		}
		if (this.holdText)
		{
			this.holdText.enabled = false;
		}
	}

	// Token: 0x060000B9 RID: 185 RVA: 0x0000674C File Offset: 0x0000494C
	public void Launch()
	{
		if (this.inputStage != ControllerIcon.InputStage.CompleteCycle)
		{
			return;
		}
		this.inputStage = ControllerIcon.InputStage.WaitingForRelease;
		if (this.inputHandler)
		{
			this.inputHandler.Launch();
		}
	}

	// Token: 0x060000BA RID: 186 RVA: 0x00006777 File Offset: 0x00004977
	public void Press()
	{
		if (this.inputStage == ControllerIcon.InputStage.WaitingForPress)
		{
			this.inputStage = ControllerIcon.InputStage.CompleteCycle;
			this.isFilling = true;
		}
	}

	// Token: 0x060000BB RID: 187 RVA: 0x00006790 File Offset: 0x00004990
	public void Release()
	{
		if (!this.requiresHold && this.fillAmount <= 1f)
		{
			this.Launch();
		}
		if (this.inputStage == ControllerIcon.InputStage.WaitingForRelease || this.inputStage == ControllerIcon.InputStage.CompleteCycle)
		{
			this.inputStage = ControllerIcon.InputStage.WaitingForPress;
		}
		this.isFilling = false;
	}

	// Token: 0x060000BC RID: 188 RVA: 0x000067CC File Offset: 0x000049CC
	private void Start()
	{
		this.canvasGroup = base.GetComponent<CanvasGroup>();
		if (this.barFillUI)
		{
			this.barFillUI.sprite = base.GetComponent<Image>().sprite;
		}
		if (!this.requiresHold)
		{
			Object.Destroy(this.holdText.gameObject);
		}
	}

	// Token: 0x060000BD RID: 189 RVA: 0x00006820 File Offset: 0x00004A20
	private void Update()
	{
		this.ConsiderHiding();
		if (this.canvasGroup && this.parentCanvasGroup.Count > 0)
		{
			float num = 1f;
			foreach (CanvasGroup canvasGroup in this.parentCanvasGroup)
			{
				if (canvasGroup.alpha < num)
				{
					num = canvasGroup.alpha;
				}
			}
			this.canvasGroup.alpha = num;
		}
		else if (this.spriteRenderer)
		{
			this.canvasGroup.alpha = this.spriteRenderer.color.a;
		}
		if (this.followTransform)
		{
			if (this.followType == ControllerIcon.FollowType.FollowUpperRight)
			{
				if (this.followRectTransform)
				{
					base.transform.position = this.followRectTransform.TransformPoint(this.followRectTransform.rect.max);
				}
			}
			else if (this.followType == ControllerIcon.FollowType.FollowCenter)
			{
				base.transform.position = this.followTransform.position;
			}
			base.transform.localScale = Vector3.one;
		}
		if (this.isFilling)
		{
			this.fillAmount += Time.deltaTime * 3f;
			if (this.fillAmount >= 1.2f && (this.requiresHold || (this.inputHandler && this.inputHandler.pressType == InputHandler.PressType.InstantOrHold)))
			{
				this.Launch();
			}
		}
		else
		{
			this.fillAmount = 0f;
		}
		if (this.barFillUI)
		{
			this.barFillUI.fillAmount = Mathf.Clamp(this.fillAmount, 0f, 1f);
		}
	}

	// Token: 0x0400005C RID: 92
	[Header("--------------References--------------")]
	[SerializeField]
	private Image image;

	// Token: 0x0400005D RID: 93
	[SerializeField]
	private Image barFillUI;

	// Token: 0x0400005E RID: 94
	[SerializeField]
	private TextMeshProUGUI holdText;

	// Token: 0x0400005F RID: 95
	private float fillAmount;

	// Token: 0x04000060 RID: 96
	private bool isFilling;

	// Token: 0x04000061 RID: 97
	private bool requiresHold = true;

	// Token: 0x04000062 RID: 98
	[Header("--------------Debuggin'--------------")]
	[SerializeField]
	private ControllerIcon.InputStage inputStage;

	// Token: 0x04000063 RID: 99
	public InputHandler inputHandler;

	// Token: 0x04000064 RID: 100
	public Transform followTransform;

	// Token: 0x04000065 RID: 101
	public RectTransform followRectTransform;

	// Token: 0x04000066 RID: 102
	private RectTransform rectTransform;

	// Token: 0x04000067 RID: 103
	[SerializeField]
	private ControllerIcon.FollowType followType = ControllerIcon.FollowType.FollowCenter;

	// Token: 0x04000068 RID: 104
	private CanvasGroup canvasGroup;

	// Token: 0x04000069 RID: 105
	[SerializeField]
	public List<CanvasGroup> parentCanvasGroup = new List<CanvasGroup>();

	// Token: 0x0400006A RID: 106
	[SerializeField]
	public SpriteRenderer spriteRenderer;

	// Token: 0x0200024B RID: 587
	private enum InputStage
	{
		// Token: 0x04000ECE RID: 3790
		WaitingForRelease,
		// Token: 0x04000ECF RID: 3791
		WaitingForPress,
		// Token: 0x04000ED0 RID: 3792
		CompleteCycle
	}

	// Token: 0x0200024C RID: 588
	private enum FollowType
	{
		// Token: 0x04000ED2 RID: 3794
		None,
		// Token: 0x04000ED3 RID: 3795
		FollowCenter,
		// Token: 0x04000ED4 RID: 3796
		FollowUpperRight
	}
}
