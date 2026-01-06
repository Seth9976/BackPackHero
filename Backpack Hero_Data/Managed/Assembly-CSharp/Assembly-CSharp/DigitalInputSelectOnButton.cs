using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020000DE RID: 222
public class DigitalInputSelectOnButton : MonoBehaviour
{
	// Token: 0x060006D9 RID: 1753 RVA: 0x00042D69 File Offset: 0x00040F69
	public void SelectMeOnCancel()
	{
		this.keyNames = new string[] { "cancel" };
		this.selectOnButtonPress = true;
		this.confirmOnButtonPressWhenSelected = true;
		this.showIcon = true;
	}

	// Token: 0x060006DA RID: 1754 RVA: 0x00042D94 File Offset: 0x00040F94
	private void Start()
	{
		this.UpdateIcon();
	}

	// Token: 0x060006DB RID: 1755 RVA: 0x00042D9C File Offset: 0x00040F9C
	private void OnEnable()
	{
		if (!DigitalCursor.main)
		{
			return;
		}
		if (GameManager.main && GameManager.main.inSpecialReorg && this.disabledDuringSpecialOrginization)
		{
			return;
		}
		if (this.selectOnEnable)
		{
			DigitalCursor.main.SelectUIElement(base.gameObject);
		}
		if (!DigitalInputSelectOnButton.all.Contains(this))
		{
			DigitalInputSelectOnButton.all.Add(this);
		}
		this.UpdateIcon();
	}

	// Token: 0x060006DC RID: 1756 RVA: 0x00042E0D File Offset: 0x0004100D
	private void OnDisable()
	{
		DigitalInputSelectOnButton.all.Remove(this);
	}

	// Token: 0x060006DD RID: 1757 RVA: 0x00042E1B File Offset: 0x0004101B
	private void OnDestroy()
	{
		if (this.createdButton)
		{
			Object.Destroy(this.createdButton);
		}
	}

	// Token: 0x060006DE RID: 1758 RVA: 0x00042E38 File Offset: 0x00041038
	public void UpdateIcon()
	{
		if (this.createdButton)
		{
			Object.Destroy(this.createdButton);
		}
		if (this.disabledButton)
		{
			return;
		}
		if (this.showIcon && this.keyNames.Length != 0)
		{
			RectTransform component = base.GetComponent<RectTransform>();
			if (component)
			{
				this.type = DigitalInputSelectOnButton.Type.UI;
				this.createdButton = DigitalCursor.main.GetControllerImage(this.keyNames[0], component, this.showIconWhenOnMouse);
				if (this.buttonPosition && this.createdButton)
				{
					this.createdButton.transform.position = this.buttonPosition.position;
				}
				if (this.createdButton && !this.animated)
				{
					this.createdButton.GetComponent<Animator>().enabled = false;
				}
				this.sortingOrder = base.GetComponentInParent<Canvas>().sortingOrder;
				return;
			}
			this.spriteRenderer = base.GetComponentInChildren<SpriteRenderer>();
			if (this.spriteRenderer)
			{
				this.type = DigitalInputSelectOnButton.Type.Sprite;
				this.createdButton = DigitalCursor.main.GetControllerImage(this.keyNames[0], this.spriteRenderer, this.showIconWhenOnMouse);
				if (this.buttonPosition && this.createdButton)
				{
					this.createdButton.transform.position = this.buttonPosition.position;
				}
				if (this.createdButton && !this.animated)
				{
					this.createdButton.GetComponent<Animator>().enabled = false;
				}
				this.sortingOrder = this.spriteRenderer.sortingOrder;
				return;
			}
		}
	}

	// Token: 0x060006DF RID: 1759 RVA: 0x00042FD6 File Offset: 0x000411D6
	public void RemoveSymbolAndDisable()
	{
		this.keyNames = new string[0];
		this.RemoveSymbol();
		this.showIcon = false;
	}

	// Token: 0x060006E0 RID: 1760 RVA: 0x00042FF1 File Offset: 0x000411F1
	public void RemoveSymbol()
	{
		this.disabledButton = true;
		if (this.createdButton)
		{
			Object.Destroy(this.createdButton);
		}
	}

	// Token: 0x060006E1 RID: 1761 RVA: 0x00043014 File Offset: 0x00041214
	private void Update()
	{
		if (this.type == DigitalInputSelectOnButton.Type.Sprite && this.spriteRenderer)
		{
			this.sortingOrder = this.spriteRenderer.sortingOrder;
		}
		if (this.disabledDuringSpecialOrginization && GameManager.main && (GameManager.main.inSpecialReorg || GameManager.main.inventoryPhase == GameManager.InventoryPhase.choose))
		{
			if (DigitalCursor.main.UIObjectSelected == base.gameObject)
			{
				DigitalCursor.main.ClearUIElement();
			}
			return;
		}
		if (this.forceSelectIfNothingSelected)
		{
			DigitalCursor.main.SelectUIElementIfNothingSelected(base.gameObject);
		}
		if (this.forceSelectIfLowerLayerIsSelectedd)
		{
			GameObject uiobjectSelected = DigitalCursor.main.UIObjectSelected;
			if (uiobjectSelected && uiobjectSelected.GetComponentInParent<Canvas>().sortingOrder < this.sortingOrder)
			{
				DigitalCursor.main.SelectUIElement(base.gameObject);
			}
		}
		if (this.escapeFromViewedObjectOnCancel && GameManager.main.viewingEventThroughObject && base.GetComponentInParent<CanvasGroup>().interactable && DigitalCursor.main.GetInputDown("cancel"))
		{
			GameManager.main.ClearEvent();
			DigitalCursor.main.ClearUIElement();
			DigitalCursor.main.transform.position = Vector3.zero;
		}
		if (DigitalCursor.main.GetInputDown("cancel") && this.recenterOnB && DigitalCursor.main.UIObjectSelected == base.gameObject)
		{
			DigitalCursor.main.ClearUIElement();
			if (GameManager.main)
			{
				GameManager.main.ClearEvent();
				GameManager.main.RecenterCursor();
			}
		}
		foreach (string text in this.keyNames)
		{
			if (DigitalCursor.main.GetInputDown(text))
			{
				if (this.type == DigitalInputSelectOnButton.Type.UI)
				{
					if (this.confirmOnButtonPress || (this.confirmOnButtonPressWhenSelected && DigitalCursor.main.UIObjectSelected == base.gameObject))
					{
						Button componentInChildren = base.GetComponentInChildren<Button>();
						if (componentInChildren)
						{
							if (EventSystem.current)
							{
								componentInChildren.OnSubmit(new BaseEventData(EventSystem.current));
							}
							else
							{
								componentInChildren.OnSubmit(new BaseEventData(null));
							}
						}
						Toggle componentInChildren2 = base.GetComponentInChildren<Toggle>();
						if (componentInChildren2)
						{
							componentInChildren2.isOn = !componentInChildren2.isOn;
						}
					}
					if (!GameManager.main || !GameManager.main.draggingItem)
					{
						if (this.selectFirstButtonNotThisElement)
						{
							Button componentInChildren3 = base.GetComponentInChildren<Button>();
							if (componentInChildren3)
							{
								if (this.selectOnButtonPress)
								{
									DigitalCursor.main.SelectUIElement(componentInChildren3.gameObject);
								}
								if (this.selectOnButtonPressIfNothingSelected)
								{
									DigitalCursor.main.SelectUIElementIfNothingSelected(componentInChildren3.gameObject);
								}
							}
						}
						else
						{
							if (this.selectOnButtonPress)
							{
								DigitalCursor.main.SelectUIElement(base.gameObject);
							}
							if (this.selectOnButtonPressIfNothingSelected)
							{
								DigitalCursor.main.SelectUIElementIfNothingSelected(base.gameObject);
							}
						}
					}
				}
				else
				{
					if (this.confirmOnButtonPress || (this.confirmOnButtonPressWhenSelected && DigitalCursor.main.ObjsHere().Contains(base.gameObject)))
					{
						CustomInputHandler componentInChildren4 = base.GetComponentInChildren<CustomInputHandler>();
						if (componentInChildren4 != null)
						{
							componentInChildren4.OnPressStart("confirm", false);
						}
					}
					if (this.selectOnButtonPress)
					{
						DigitalCursor.main.FollowGameElement(base.transform, false);
					}
				}
			}
		}
		if (EventSystem.current && EventSystem.current.currentSelectedGameObject == base.gameObject && this.escapable)
		{
			this.escapeTime -= Time.deltaTime;
			if (this.escapeTime <= 0f)
			{
				DigitalCursor.main.isEscapable = true;
				return;
			}
			DigitalCursor.main.isEscapable = false;
			return;
		}
		else
		{
			if (EventSystem.current && EventSystem.current.currentSelectedGameObject == base.gameObject)
			{
				DigitalCursor.main.isEscapable = false;
				this.escapeTime = 0.25f;
				return;
			}
			this.escapeTime = 0.25f;
			return;
		}
	}

	// Token: 0x04000572 RID: 1394
	private DigitalInputSelectOnButton.Type type;

	// Token: 0x04000573 RID: 1395
	[SerializeField]
	private string[] keyNames = new string[] { "cancel" };

	// Token: 0x04000574 RID: 1396
	[SerializeField]
	private string[] groupNames = new string[0];

	// Token: 0x04000575 RID: 1397
	[Header("---------Properties---------")]
	[SerializeField]
	public bool cannotBeSelected;

	// Token: 0x04000576 RID: 1398
	[SerializeField]
	public bool selectOnEnable;

	// Token: 0x04000577 RID: 1399
	[SerializeField]
	public bool isRaycastable;

	// Token: 0x04000578 RID: 1400
	[SerializeField]
	public bool selectCanvasGroupAsViewedObject;

	// Token: 0x04000579 RID: 1401
	[SerializeField]
	public bool escapeFromViewedObjectOnCancel;

	// Token: 0x0400057A RID: 1402
	[SerializeField]
	public bool recordAndReselectIfNothingSelected;

	// Token: 0x0400057B RID: 1403
	[SerializeField]
	private bool forceSelectIfNothingSelected;

	// Token: 0x0400057C RID: 1404
	[SerializeField]
	private bool forceSelectIfLowerLayerIsSelectedd;

	// Token: 0x0400057D RID: 1405
	[SerializeField]
	private bool selectOnButtonPress;

	// Token: 0x0400057E RID: 1406
	[SerializeField]
	public bool selectOnButtonPressIfNothingSelected;

	// Token: 0x0400057F RID: 1407
	[SerializeField]
	private bool selectFirstButtonNotThisElement;

	// Token: 0x04000580 RID: 1408
	[SerializeField]
	private bool confirmOnButtonPress = true;

	// Token: 0x04000581 RID: 1409
	[SerializeField]
	private bool confirmOnButtonPressWhenSelected = true;

	// Token: 0x04000582 RID: 1410
	[SerializeField]
	private bool showIcon = true;

	// Token: 0x04000583 RID: 1411
	[SerializeField]
	private bool showIconWhenOnMouse = true;

	// Token: 0x04000584 RID: 1412
	[SerializeField]
	private bool recenterOnB;

	// Token: 0x04000585 RID: 1413
	[SerializeField]
	public bool escapable;

	// Token: 0x04000586 RID: 1414
	[SerializeField]
	public bool locksEntirely;

	// Token: 0x04000587 RID: 1415
	[SerializeField]
	public bool animated;

	// Token: 0x04000588 RID: 1416
	[SerializeField]
	public bool disabledDuringSpecialOrginization;

	// Token: 0x04000589 RID: 1417
	public static List<DigitalInputSelectOnButton> all = new List<DigitalInputSelectOnButton>();

	// Token: 0x0400058A RID: 1418
	private bool disabledButton;

	// Token: 0x0400058B RID: 1419
	[SerializeField]
	private GameObject createdButton;

	// Token: 0x0400058C RID: 1420
	[SerializeField]
	private Transform buttonPosition;

	// Token: 0x0400058D RID: 1421
	private SpriteRenderer spriteRenderer;

	// Token: 0x0400058E RID: 1422
	private int sortingOrder;

	// Token: 0x0400058F RID: 1423
	private float escapeTime = 0.25f;

	// Token: 0x0200031E RID: 798
	public enum Type
	{
		// Token: 0x0400126E RID: 4718
		UI,
		// Token: 0x0400126F RID: 4719
		Sprite
	}
}
