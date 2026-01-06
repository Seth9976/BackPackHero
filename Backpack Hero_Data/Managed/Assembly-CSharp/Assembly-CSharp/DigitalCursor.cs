using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock;
using UnityEngine.InputSystem.UI;
using UnityEngine.InputSystem.XInput;
using UnityEngine.UI;

// Token: 0x020000DD RID: 221
public class DigitalCursor : MonoBehaviour
{
	// Token: 0x1700000E RID: 14
	// (get) Token: 0x06000693 RID: 1683 RVA: 0x0003FB2D File Offset: 0x0003DD2D
	public static DigitalCursor main
	{
		get
		{
			return DigitalCursor._instance;
		}
	}

	// Token: 0x1700000F RID: 15
	// (get) Token: 0x06000694 RID: 1684 RVA: 0x0003FB34 File Offset: 0x0003DD34
	// (set) Token: 0x06000695 RID: 1685 RVA: 0x0003FB3C File Offset: 0x0003DD3C
	public Vector2 moveFreeVector { get; private set; }

	// Token: 0x17000010 RID: 16
	// (get) Token: 0x06000696 RID: 1686 RVA: 0x0003FB45 File Offset: 0x0003DD45
	// (set) Token: 0x06000697 RID: 1687 RVA: 0x0003FB4D File Offset: 0x0003DD4D
	public Vector2 moveLockVector { get; private set; }

	// Token: 0x06000698 RID: 1688 RVA: 0x0003FB58 File Offset: 0x0003DD58
	private void Awake()
	{
		if (DigitalCursor._instance != null && DigitalCursor._instance != this)
		{
			Object.Destroy(base.gameObject);
			return;
		}
		DigitalCursor._instance = this;
		Object.DontDestroyOnLoad(base.gameObject);
		this.defaultInput = new DefaultInputActions();
		this.defaultInput.Enable();
		this.controller = new Controller();
		this.controller.Gameplay.Enable();
		this.controller.Gameplay.Confirm.ApplyBindingOverride(new InputBinding
		{
			overridePath = "*/<Keyboard>/enter",
			groups = "Keyboard&Mouse"
		});
		this.ResetButtons();
		this.spriteRenderer = base.GetComponent<SpriteRenderer>();
	}

	// Token: 0x06000699 RID: 1689 RVA: 0x0003FC1C File Offset: 0x0003DE1C
	private void Update()
	{
		this.MakeReferences();
		new Vector3(this.currentPosition.x, this.currentPosition.y, base.transform.position.z);
		this.LimitPosition();
		this.SetControlIcons();
		this.ToggleControlStyle();
		this.MoveCursorToMouse();
		this.MoveCursorToController();
		this.GetDirectionalInteraction();
		this.GetInteractionEvents();
	}

	// Token: 0x0600069A RID: 1690 RVA: 0x0003FC85 File Offset: 0x0003DE85
	private void MakeReferences()
	{
		if (Overworld_Manager.main && !this.followObjectCamera)
		{
			this.followObjectCamera = Camera.main.GetComponent<FollowObjectCamera>();
		}
		this.ConsiderFindingGraphicsRaycasters();
	}

	// Token: 0x0600069B RID: 1691 RVA: 0x0003FCB8 File Offset: 0x0003DEB8
	private void LimitPosition()
	{
		if (this.followObjectCamera)
		{
			Vector4 limitsOfTileMap = this.followObjectCamera.GetLimitsOfTileMap();
			base.transform.position = new Vector3(Mathf.Clamp(base.transform.position.x, limitsOfTileMap.x, limitsOfTileMap.y), Mathf.Clamp(base.transform.position.y, limitsOfTileMap.z, limitsOfTileMap.w), 0f);
		}
		if (GameManager.main && this.currentPosition.x >= this.limits.x && this.currentPosition.x <= this.limits.y && this.currentPosition.y >= this.limits.z)
		{
			float y = this.currentPosition.y;
			float w = this.limits.w;
		}
	}

	// Token: 0x0600069C RID: 1692 RVA: 0x0003FDA4 File Offset: 0x0003DFA4
	public Vector2 GetHighestVector(DigitalCursor.VectorType vectorType, float senstivity = 0f)
	{
		if (senstivity == 0f)
		{
			senstivity = this.senstivity;
		}
		Vector2 vector = Vector2.zero;
		if ((this.moveFreeVector.magnitude > vector.magnitude && vectorType == DigitalCursor.VectorType.either) || vectorType == DigitalCursor.VectorType.free)
		{
			vector = this.moveFreeVector;
		}
		if ((this.moveLockVector.magnitude > vector.magnitude && vectorType == DigitalCursor.VectorType.either) || vectorType == DigitalCursor.VectorType.locked)
		{
			vector = this.moveLockVector;
		}
		if (vector.magnitude < senstivity)
		{
			return Vector2.zero;
		}
		return vector;
	}

	// Token: 0x0600069D RID: 1693 RVA: 0x0003FE24 File Offset: 0x0003E024
	public Sprite GetSpriteForKey(string keyName)
	{
		foreach (DigitalCursor.ButtonImage buttonImage in this.choseControllerIamge.buttonImages)
		{
			if (buttonImage.name.ToLower().Trim() == keyName.Trim().ToLower())
			{
				return buttonImage.sprite;
			}
		}
		return null;
	}

	// Token: 0x0600069E RID: 1694 RVA: 0x0003FEA4 File Offset: 0x0003E0A4
	public bool IsHoveringOnStatusEffect()
	{
		return this.lastUpdateHoveringObjects.Count != 0 && !(this.lastUpdateHoveringObjects[0] == null) && this.lastUpdateHoveringObjects[0].GetComponent<StatusEffect>();
	}

	// Token: 0x0600069F RID: 1695 RVA: 0x0003FEF4 File Offset: 0x0003E0F4
	public string GetSpriteAtlasForKey(string keyName)
	{
		foreach (DigitalCursor.ButtonImage buttonImage in this.choseControllerIamge.buttonImages)
		{
			if (buttonImage.name.ToLower().Trim() == keyName.Trim().ToLower())
			{
				return "<sprite name=\"" + buttonImage.sprite.name + "\">";
			}
		}
		return "";
	}

	// Token: 0x060006A0 RID: 1696 RVA: 0x0003FF8C File Offset: 0x0003E18C
	[Obsolete]
	public void EnterFreeMoveOnly(Transform t)
	{
	}

	// Token: 0x060006A1 RID: 1697 RVA: 0x0003FF90 File Offset: 0x0003E190
	public void SetControlIcons()
	{
		if (Singleton.Instance.chosenControllerIcons == -1 && this.controlStyle == DigitalCursor.ControlStyle.controller)
		{
			this.AutoDetectController();
		}
		if (this.controlStyle == DigitalCursor.ControlStyle.cursor && this.choseControllerIamge != this.controllerImages[0])
		{
			this.choseControllerIamge = this.controllerImages[0];
			DigitalCursor.SetAllIcons();
			return;
		}
		if (this.controlStyle == DigitalCursor.ControlStyle.controller && this.choseControllerIamge != this.controllerImages[Singleton.Instance.chosenControllerIcons + 1])
		{
			this.choseControllerIamge = this.controllerImages[Singleton.Instance.chosenControllerIcons + 1];
			DigitalCursor.SetAllIcons();
		}
	}

	// Token: 0x060006A2 RID: 1698 RVA: 0x00040038 File Offset: 0x0003E238
	private void AutoDetectController()
	{
		Gamepad current = Gamepad.current;
		if (current == null)
		{
			return;
		}
		if (current is DualShockGamepad)
		{
			Singleton.Instance.chosenControllerIcons = 2;
			return;
		}
		if (current is XInputController)
		{
			Singleton.Instance.chosenControllerIcons = 1;
			return;
		}
		Singleton.Instance.chosenControllerIcons = 0;
	}

	// Token: 0x060006A3 RID: 1699 RVA: 0x00040084 File Offset: 0x0003E284
	private static void SetAllIcons()
	{
		foreach (DigitalInputSelectOnButton digitalInputSelectOnButton in DigitalInputSelectOnButton.all)
		{
			digitalInputSelectOnButton.UpdateIcon();
		}
		foreach (InputHandler inputHandler in InputHandler.inputHandlers)
		{
			if (inputHandler.enabled)
			{
				inputHandler.UpdateIcon();
			}
		}
	}

	// Token: 0x060006A4 RID: 1700 RVA: 0x0004011C File Offset: 0x0003E31C
	public GameObject GetControllerImage(string name, SpriteRenderer sprite, bool showIconWithMouse)
	{
		if (this.choseControllerIamge == null)
		{
			this.choseControllerIamge = this.controllerImages[0];
		}
		if (!showIconWithMouse && this.choseControllerIamge == this.controllerImages[0])
		{
			return null;
		}
		foreach (DigitalCursor.ButtonImage buttonImage in this.choseControllerIamge.buttonImages)
		{
			if (buttonImage.name == name)
			{
				new Vector3[4];
				GameObject gameObject = Object.Instantiate<GameObject>(this.spriteButtonIconPrefab, sprite.bounds.max, Quaternion.identity, sprite.transform);
				gameObject.transform.localPosition += Vector3.left * 0.25f + Vector3.down * 0.25f + Vector3.back;
				SpriteRenderer component = gameObject.GetComponent<SpriteRenderer>();
				component.sprite = buttonImage.sprite;
				component.sortingOrder = sprite.sortingOrder;
				return gameObject;
			}
		}
		return null;
	}

	// Token: 0x060006A5 RID: 1701 RVA: 0x00040248 File Offset: 0x0003E448
	public Sprite GetSprite(string name)
	{
		if (this.choseControllerIamge == null)
		{
			this.choseControllerIamge = this.controllerImages[0];
		}
		foreach (DigitalCursor.ButtonImage buttonImage in this.choseControllerIamge.buttonImages)
		{
			if (buttonImage.name.ToLower().Trim() == name.ToLower().Trim())
			{
				return buttonImage.sprite;
			}
		}
		return null;
	}

	// Token: 0x060006A6 RID: 1702 RVA: 0x000402E4 File Offset: 0x0003E4E4
	public string GetCurrentImageNameForButtonName(string name)
	{
		if (this.choseControllerIamge == null)
		{
			this.choseControllerIamge = this.controllerImages[0];
		}
		for (int i = 0; i < this.choseControllerIamge.buttonImages.Count; i++)
		{
			if (this.choseControllerIamge.buttonImages[i].name.ToLower().Trim() == name.ToLower().Trim())
			{
				return this.choseControllerIamge.buttonImages[i].sprite.name;
			}
		}
		return "";
	}

	// Token: 0x060006A7 RID: 1703 RVA: 0x0004037C File Offset: 0x0003E57C
	public GameObject GetControllerImage(string name, RectTransform rect, bool showIconWithMouse)
	{
		if (this.choseControllerIamge == null)
		{
			this.choseControllerIamge = this.controllerImages[0];
		}
		if ((!showIconWithMouse && this.controlStyle == DigitalCursor.ControlStyle.cursor) || this.choseControllerIamge.buttonImages.Count == 0)
		{
			return null;
		}
		DigitalCursor.ButtonImage buttonImage = this.choseControllerIamge.buttonImages[0];
		foreach (DigitalCursor.ButtonImage buttonImage2 in this.choseControllerIamge.buttonImages)
		{
			if (buttonImage2.name.ToLower().Trim() == name.ToLower().Trim())
			{
				buttonImage = buttonImage2;
				break;
			}
		}
		Vector3[] array = new Vector3[4];
		GameObject gameObject = Object.Instantiate<GameObject>(this.buttonIconPrefab, array[2], Quaternion.identity);
		if (rect)
		{
			rect.GetWorldCorners(array);
			gameObject.transform.position = array[2];
		}
		gameObject.GetComponent<Image>().sprite = buttonImage.sprite;
		return gameObject;
	}

	// Token: 0x060006A8 RID: 1704 RVA: 0x00040494 File Offset: 0x0003E694
	public void ResetButtons()
	{
		this.controller.Gameplay.Enable();
		this.controller.Gameplay.MoveCursorLock.performed += delegate(InputAction.CallbackContext ctx)
		{
			this.moveLockVector = ctx.ReadValue<Vector2>();
		};
		this.controller.Gameplay.MoveCursorLock.canceled += delegate(InputAction.CallbackContext ctx)
		{
			this.moveLockVector = Vector2.zero;
		};
		this.controller.Gameplay.MoveCursorFree.performed += delegate(InputAction.CallbackContext ctx)
		{
			this.moveFreeVector = ctx.ReadValue<Vector2>();
		};
		this.controller.Gameplay.MoveCursorFree.canceled += delegate(InputAction.CallbackContext ctx)
		{
			this.moveFreeVector = Vector2.zero;
		};
		string confirmButton = "confirm";
		string cancelButton = "cancel";
		if (Singleton.Instance.reverseAandB)
		{
			confirmButton = "cancel";
			cancelButton = "confirm";
			InputSystemUIInputModule inputSystemUIInputModule = Object.FindObjectOfType<InputSystemUIInputModule>();
			InputActionReference submit = inputSystemUIInputModule.submit;
			inputSystemUIInputModule.submit = inputSystemUIInputModule.cancel;
			inputSystemUIInputModule.cancel = submit;
		}
		string contextMenu = "contextMenu";
		string contextualAction = "contextualAction";
		if (Singleton.Instance.reverseXandY)
		{
			contextMenu = "contextualAction";
			contextualAction = "contextMenu";
		}
		this.controller.Gameplay.ContextMenu.started += delegate(InputAction.CallbackContext ctx)
		{
			this.NewButton(contextMenu, DigitalCursor.InputListener.Type.press);
		};
		this.controller.Gameplay.ContextMenu.performed += delegate(InputAction.CallbackContext ctx)
		{
			this.NewButton(contextMenu, DigitalCursor.InputListener.Type.hold);
		};
		this.controller.Gameplay.ContextMenu.canceled += delegate(InputAction.CallbackContext ctx)
		{
			this.NewButton(contextMenu, DigitalCursor.InputListener.Type.release);
		};
		this.controller.Gameplay.RotateRight.started += delegate(InputAction.CallbackContext ctx)
		{
			this.NewButton("rotateRight", DigitalCursor.InputListener.Type.press);
		};
		this.controller.Gameplay.RotateRight.performed += delegate(InputAction.CallbackContext ctx)
		{
			this.NewButton("rotateRight", DigitalCursor.InputListener.Type.hold);
		};
		this.controller.Gameplay.RotateRight.canceled += delegate(InputAction.CallbackContext ctx)
		{
			this.NewButton("rotateRight", DigitalCursor.InputListener.Type.release);
		};
		this.controller.Gameplay.RotateLeft.started += delegate(InputAction.CallbackContext ctx)
		{
			this.NewButton("rotateLeft", DigitalCursor.InputListener.Type.press);
		};
		this.controller.Gameplay.RotateLeft.performed += delegate(InputAction.CallbackContext ctx)
		{
			this.NewButton("rotateLeft", DigitalCursor.InputListener.Type.hold);
		};
		this.controller.Gameplay.RotateLeft.canceled += delegate(InputAction.CallbackContext ctx)
		{
			this.NewButton("rotateLeft", DigitalCursor.InputListener.Type.release);
		};
		this.controller.Gameplay.PauseMenu.started += delegate(InputAction.CallbackContext ctx)
		{
			this.NewButton("start", DigitalCursor.InputListener.Type.press);
		};
		this.controller.Gameplay.PauseMenu.performed += delegate(InputAction.CallbackContext ctx)
		{
			this.NewButton("start", DigitalCursor.InputListener.Type.hold);
		};
		this.controller.Gameplay.PauseMenu.canceled += delegate(InputAction.CallbackContext ctx)
		{
			this.NewButton("start", DigitalCursor.InputListener.Type.release);
		};
		this.controller.Gameplay.Select.started += delegate(InputAction.CallbackContext ctx)
		{
			this.NewButton("select", DigitalCursor.InputListener.Type.press);
		};
		this.controller.Gameplay.Select.performed += delegate(InputAction.CallbackContext ctx)
		{
			this.NewButton("select", DigitalCursor.InputListener.Type.hold);
		};
		this.controller.Gameplay.Select.canceled += delegate(InputAction.CallbackContext ctx)
		{
			this.NewButton("select", DigitalCursor.InputListener.Type.release);
		};
		this.controller.Gameplay.CenterOnBackpack.started += delegate(InputAction.CallbackContext ctx)
		{
			this.NewButton("centerOnBackpack", DigitalCursor.InputListener.Type.press);
		};
		this.controller.Gameplay.ActionButtons.started += delegate(InputAction.CallbackContext ctx)
		{
			this.NewButton("ActionButtons", DigitalCursor.InputListener.Type.press);
		};
		this.controller.Gameplay.Confirm.started += delegate(InputAction.CallbackContext ctx)
		{
			this.NewButton(confirmButton, DigitalCursor.InputListener.Type.press);
		};
		this.controller.Gameplay.Confirm.performed += delegate(InputAction.CallbackContext ctx)
		{
			this.NewButton(confirmButton, DigitalCursor.InputListener.Type.hold);
		};
		this.controller.Gameplay.Confirm.canceled += delegate(InputAction.CallbackContext ctx)
		{
			this.NewButton(confirmButton, DigitalCursor.InputListener.Type.release);
		};
		this.controller.Gameplay.Cancel.started += delegate(InputAction.CallbackContext ctx)
		{
			this.NewButton(cancelButton, DigitalCursor.InputListener.Type.press);
		};
		this.controller.Gameplay.Cancel.performed += delegate(InputAction.CallbackContext ctx)
		{
			this.NewButton(cancelButton, DigitalCursor.InputListener.Type.hold);
		};
		this.controller.Gameplay.Cancel.canceled += delegate(InputAction.CallbackContext ctx)
		{
			this.NewButton(cancelButton, DigitalCursor.InputListener.Type.release);
		};
		this.controller.Gameplay.PauseMenu.started += delegate(InputAction.CallbackContext ctx)
		{
			this.NewButton("pause", DigitalCursor.InputListener.Type.press);
		};
		this.controller.Gameplay.PauseMenu.performed += delegate(InputAction.CallbackContext ctx)
		{
			this.NewButton("pause", DigitalCursor.InputListener.Type.hold);
		};
		this.controller.Gameplay.PauseMenu.canceled += delegate(InputAction.CallbackContext ctx)
		{
			this.NewButton("pause", DigitalCursor.InputListener.Type.release);
		};
		this.controller.Gameplay.leftBumper.started += delegate(InputAction.CallbackContext ctx)
		{
			this.NewButton("leftBumper", DigitalCursor.InputListener.Type.press);
		};
		this.controller.Gameplay.leftBumper.performed += delegate(InputAction.CallbackContext ctx)
		{
			this.NewButton("leftBumper", DigitalCursor.InputListener.Type.hold);
		};
		this.controller.Gameplay.leftBumper.canceled += delegate(InputAction.CallbackContext ctx)
		{
			this.NewButton("leftBumper", DigitalCursor.InputListener.Type.release);
		};
		this.controller.Gameplay.rightBumper.started += delegate(InputAction.CallbackContext ctx)
		{
			this.NewButton("rightBumper", DigitalCursor.InputListener.Type.press);
		};
		this.controller.Gameplay.rightBumper.performed += delegate(InputAction.CallbackContext ctx)
		{
			this.NewButton("rightBumper", DigitalCursor.InputListener.Type.hold);
		};
		this.controller.Gameplay.rightBumper.canceled += delegate(InputAction.CallbackContext ctx)
		{
			this.NewButton("rightBumper", DigitalCursor.InputListener.Type.release);
		};
		this.controller.Gameplay.ContextualAction.started += delegate(InputAction.CallbackContext ctx)
		{
			this.NewButton(contextualAction, DigitalCursor.InputListener.Type.press);
		};
		this.controller.Gameplay.ContextualAction.performed += delegate(InputAction.CallbackContext ctx)
		{
			this.NewButton(contextualAction, DigitalCursor.InputListener.Type.hold);
		};
		this.controller.Gameplay.ContextualAction.canceled += delegate(InputAction.CallbackContext ctx)
		{
			this.NewButton(contextualAction, DigitalCursor.InputListener.Type.release);
		};
	}

	// Token: 0x060006A9 RID: 1705 RVA: 0x00040ACC File Offset: 0x0003ECCC
	public bool OverUI()
	{
		if (EventSystem.current && EventSystem.current.IsPointerOverGameObject())
		{
			return true;
		}
		if (this.timeOutBeforeInteractionWithNonUIElement > 0f && this.controlStyle == DigitalCursor.ControlStyle.controller)
		{
			return true;
		}
		this.ConsiderFindingGraphicsRaycasters();
		foreach (GraphicRaycaster graphicRaycaster in this.graphicRaycasters)
		{
			if (graphicRaycaster && graphicRaycaster.enabled)
			{
				PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
				pointerEventData.position = Camera.main.WorldToScreenPoint(base.transform.position);
				List<RaycastResult> list = new List<RaycastResult>();
				graphicRaycaster.Raycast(pointerEventData, list);
				if (list.Count > 0)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x060006AA RID: 1706 RVA: 0x00040BAC File Offset: 0x0003EDAC
	public bool NoUIElements()
	{
		return (!EventSystem.current || !EventSystem.current.currentSelectedGameObject) && (this.timeOutBeforeInteractionWithNonUIElement <= 0f || this.controlStyle != DigitalCursor.ControlStyle.controller);
	}

	// Token: 0x060006AB RID: 1707 RVA: 0x00040BE6 File Offset: 0x0003EDE6
	[Obsolete]
	public void SelectPreviousUIElement()
	{
	}

	// Token: 0x060006AC RID: 1708 RVA: 0x00040BE8 File Offset: 0x0003EDE8
	[Obsolete]
	public void ClearUIElement()
	{
	}

	// Token: 0x060006AD RID: 1709 RVA: 0x00040BEA File Offset: 0x0003EDEA
	[Obsolete]
	public void SetPosition(Vector2 pos)
	{
	}

	// Token: 0x060006AE RID: 1710 RVA: 0x00040BEC File Offset: 0x0003EDEC
	public void MoveToPositionInBackpack()
	{
		if (this.controlStyle != DigitalCursor.ControlStyle.controller)
		{
			return;
		}
		this.gameWorldDestinationTransform = null;
		this.gameWorldDestinationPosition = new Vector3(0f, 2.65f, 0f);
	}

	// Token: 0x060006AF RID: 1711 RVA: 0x00040C1C File Offset: 0x0003EE1C
	public static void MoveToGameWorldElement(Transform t)
	{
		DigitalCursor main = DigitalCursor.main;
		if (!main)
		{
			return;
		}
		if (main.controlStyle != DigitalCursor.ControlStyle.controller)
		{
			return;
		}
		main.gameWorldDestinationTransform = t;
		main.gameWorldDestinationPosition = Vector3.zero;
	}

	// Token: 0x060006B0 RID: 1712 RVA: 0x00040C54 File Offset: 0x0003EE54
	[Obsolete]
	private void SelectDemandedElementAfterRemove()
	{
	}

	// Token: 0x060006B1 RID: 1713 RVA: 0x00040C56 File Offset: 0x0003EE56
	[Obsolete]
	public void SelectUIElementIfNothingSelected(GameObject i)
	{
	}

	// Token: 0x060006B2 RID: 1714 RVA: 0x00040C58 File Offset: 0x0003EE58
	[Obsolete]
	public void SelectClosestSelectableInElement(Transform trans)
	{
	}

	// Token: 0x060006B3 RID: 1715 RVA: 0x00040C5A File Offset: 0x0003EE5A
	[Obsolete]
	private IEnumerator SelectClosestSelectableInElementAfterAFrame(Transform trans)
	{
		yield return null;
		yield break;
	}

	// Token: 0x060006B4 RID: 1716 RVA: 0x00040C62 File Offset: 0x0003EE62
	[Obsolete]
	public void SelectFirstSelectableInElement(Transform trans)
	{
	}

	// Token: 0x060006B5 RID: 1717 RVA: 0x00040C64 File Offset: 0x0003EE64
	[Obsolete]
	private IEnumerator SelectFirstSelectableInElementAfterAFrame(Transform trans)
	{
		yield return null;
		yield break;
	}

	// Token: 0x060006B6 RID: 1718 RVA: 0x00040C6C File Offset: 0x0003EE6C
	[Obsolete]
	public void SelectFirstSelectableInElementInstantly(Transform trans)
	{
	}

	// Token: 0x060006B7 RID: 1719 RVA: 0x00040C6E File Offset: 0x0003EE6E
	[Obsolete]
	public void SelectUIElement(GameObject i)
	{
	}

	// Token: 0x060006B8 RID: 1720 RVA: 0x00040C70 File Offset: 0x0003EE70
	public void MoveToItem(Transform t)
	{
		this.currentPosition = t.position;
	}

	// Token: 0x060006B9 RID: 1721 RVA: 0x00040C83 File Offset: 0x0003EE83
	[Obsolete]
	public void ClearFollow()
	{
	}

	// Token: 0x060006BA RID: 1722 RVA: 0x00040C85 File Offset: 0x0003EE85
	[Obsolete]
	public void MoveInstantlyToGameElement(Transform trans)
	{
	}

	// Token: 0x060006BB RID: 1723 RVA: 0x00040C87 File Offset: 0x0003EE87
	public void SetGameWorldDestinationTransform(Transform t)
	{
		this.gameWorldDestinationTransform = t;
	}

	// Token: 0x060006BC RID: 1724 RVA: 0x00040C90 File Offset: 0x0003EE90
	[Obsolete]
	public void FollowGameElement(Transform trans, bool overrideSelectedUIGameObject = false)
	{
	}

	// Token: 0x060006BD RID: 1725 RVA: 0x00040C94 File Offset: 0x0003EE94
	public bool GetInputRelease(string inputName)
	{
		foreach (DigitalCursor.InputListener inputListener in this.inputListeners)
		{
			if (inputListener.type == DigitalCursor.InputListener.Type.release && inputName.ToLower() == "any")
			{
				return true;
			}
			if (inputListener.name == inputName.ToLower() && inputListener.type == DigitalCursor.InputListener.Type.release)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060006BE RID: 1726 RVA: 0x00040D24 File Offset: 0x0003EF24
	public bool GetInputHold(string inputName)
	{
		foreach (DigitalCursor.InputListener inputListener in this.inputListeners)
		{
			if (inputListener.type == DigitalCursor.InputListener.Type.hold && inputName.ToLower() == "any")
			{
				return true;
			}
			if (inputListener.name == inputName.ToLower() && inputListener.type == DigitalCursor.InputListener.Type.hold)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060006BF RID: 1727 RVA: 0x00040DB4 File Offset: 0x0003EFB4
	public bool GetInputDown(string inputName)
	{
		foreach (DigitalCursor.InputListener inputListener in this.inputListeners)
		{
			if (inputListener.type == DigitalCursor.InputListener.Type.press && inputName.ToLower() == "any")
			{
				return true;
			}
			if (inputListener.name == inputName.ToLower() && inputListener.type == DigitalCursor.InputListener.Type.press)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060006C0 RID: 1728 RVA: 0x00040E40 File Offset: 0x0003F040
	private void ToggleControlStyle()
	{
		if (!this.canChangeController)
		{
			return;
		}
		if ((Mouse.current != null && Mouse.current.leftButton.isPressed) || (Input.touchCount > 0 && Input.GetTouch(0).phase == global::UnityEngine.TouchPhase.Began))
		{
			if (ContextControlsDisplay.main)
			{
				ContextControlsDisplay.main.SetShow(false);
			}
			if (this.controlStyle != DigitalCursor.ControlStyle.cursor)
			{
				this.controlStyle = DigitalCursor.ControlStyle.cursor;
				OnControllerSwitchEvent.CallAllEvents();
			}
			Cursor.lockState = CursorLockMode.None;
			return;
		}
		if (this.GetInputDown("confirm"))
		{
			if (ContextControlsDisplay.main)
			{
				ContextControlsDisplay.main.SetShow(true);
			}
			if (this.controlStyle != DigitalCursor.ControlStyle.controller)
			{
				this.controlStyle = DigitalCursor.ControlStyle.controller;
				OnControllerSwitchEvent.CallAllEvents();
			}
			this.fakeController = false;
			Cursor.lockState = CursorLockMode.Locked;
		}
	}

	// Token: 0x060006C1 RID: 1729 RVA: 0x00040F00 File Offset: 0x0003F100
	public void UpdateToMousePosition()
	{
		if (Mouse.current == null)
		{
			return;
		}
		if (this.controlStyle != DigitalCursor.ControlStyle.cursor)
		{
			return;
		}
		if (!Application.isFocused)
		{
			return;
		}
		if (Input.mousePosition.x < -150f || Input.mousePosition.x > (float)Screen.width + 150f || Input.mousePosition.y < -120f || Input.mousePosition.y > (float)Screen.height + 120f)
		{
			return;
		}
		this.currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		base.transform.position = this.currentPosition;
	}

	// Token: 0x060006C2 RID: 1730 RVA: 0x00040FA9 File Offset: 0x0003F1A9
	[Obsolete]
	public void Hide()
	{
	}

	// Token: 0x060006C3 RID: 1731 RVA: 0x00040FAB File Offset: 0x0003F1AB
	[Obsolete]
	public void Show()
	{
	}

	// Token: 0x060006C4 RID: 1732 RVA: 0x00040FAD File Offset: 0x0003F1AD
	public void ToggleSprite(bool show)
	{
		if (!this.spriteRenderer)
		{
			return;
		}
		this.spriteRenderer.enabled = show;
	}

	// Token: 0x060006C5 RID: 1733 RVA: 0x00040FC9 File Offset: 0x0003F1C9
	private void MoveCursorToMouse()
	{
		if (this.controlStyle != DigitalCursor.ControlStyle.cursor)
		{
			return;
		}
		Cursor.visible = true;
		this.spriteRenderer.enabled = false;
		this.UpdateToMousePosition();
		this.gameWorldDestinationPosition = base.transform.position;
		this.gameWorldDestinationTransform = null;
	}

	// Token: 0x060006C6 RID: 1734 RVA: 0x00041004 File Offset: 0x0003F204
	public void EndInteractionWithDigitalCursorInterface()
	{
		this.digitalCursorInterface = null;
		if (this.gameWorldDestinationTransform)
		{
			this.gameWorldDestinationPosition = this.gameWorldDestinationTransform.position;
		}
		base.transform.position = this.gameWorldDestinationPosition;
	}

	// Token: 0x060006C7 RID: 1735 RVA: 0x0004103C File Offset: 0x0003F23C
	private void MoveCursorToController()
	{
		if (this.controlStyle != DigitalCursor.ControlStyle.controller)
		{
			return;
		}
		Cursor.visible = false;
		if (this.digitalCursorInterface)
		{
			this.InteractWithDigitalCursorInterface();
			return;
		}
		if (this.isLockedToDigitalCursorInterface)
		{
			this.ToggleSprite(false);
			this.isLockedToDigitalCursorInterface = false;
			return;
		}
		if (Overworld_Manager.main && !Overworld_Manager.main.IsBuildingMode() && Overworld_Purse.main)
		{
			this.ToggleSprite(false);
			base.transform.position = Overworld_Purse.main.controllerCursorPosition.position;
			this.gameWorldDestinationTransform = null;
			this.gameWorldDestinationPosition = base.transform.position;
			this.currentPosition = base.transform.position;
			return;
		}
		if (Overworld_Manager.main && Overworld_Manager.main.IsBuildingMode())
		{
			if (!this.overworldBuildMenuRectTransform)
			{
				GameObject gameObject = GameObject.FindGameObjectWithTag("Overworld_Build_Menu");
				if (gameObject)
				{
					this.overworldBuildMenuRectTransform = gameObject.GetComponent<RectTransform>();
				}
			}
			if (this.overworldBuildMenuRectTransform && this.GetInputDown("confirm"))
			{
				Vector3[] array = new Vector3[4];
				this.overworldBuildMenuRectTransform.GetWorldCorners(array);
				if (base.transform.position.x > array[0].x && base.transform.position.x < array[2].x && base.transform.position.y > array[0].y && base.transform.position.y < array[2].y)
				{
					this.digitalCursorInterface = this.overworldBuildMenuRectTransform.GetComponent<DigitalCursorInterface>();
					if (this.digitalCursorInterface)
					{
						this.digitalCursorInterface.enabled = true;
						return;
					}
				}
			}
			Vector2 vector = Vector2.zero;
			if (this.moveFreeVector.magnitude > this.stickDeadZone)
			{
				Camera.main.transform.position += this.moveFreeVector * Time.deltaTime * this.freeSpeed * Singleton.Instance.cursorSpeed;
				vector = this.moveFreeVector;
			}
			if (this.moveLockVector.magnitude > this.stickDeadZone)
			{
				vector = this.moveLockVector;
			}
			this.ToggleSprite(true);
			base.transform.position += vector * Time.deltaTime * this.freeSpeed * Singleton.Instance.cursorSpeed;
			this.gameWorldDestinationTransform = null;
			this.currentPosition = base.transform.position;
			return;
		}
		if (GameManager.main || (Overworld_Manager.main && Overworld_Manager.main.IsBuildingMode()))
		{
			this.ToggleSprite(true);
		}
		else
		{
			this.ToggleSprite(false);
		}
		if (this.moveFreeVector.magnitude > this.stickDeadZone)
		{
			this.gameWorldDestinationPosition = base.transform.position;
			this.gameWorldDestinationPosition += this.moveFreeVector * Time.deltaTime * this.freeSpeed * Singleton.Instance.cursorSpeed;
			this.gameWorldDestinationTransform = null;
		}
		else
		{
			this.ConsiderMovingInInventoryAndMap();
		}
		if (this.gameWorldDestinationTransform)
		{
			this.gameWorldDestinationPosition = this.gameWorldDestinationTransform.position;
		}
		if (GameManager.main)
		{
			this.gameWorldDestinationPosition = new Vector2(Mathf.Clamp(this.gameWorldDestinationPosition.x, this.limits.x, this.limits.y), Mathf.Clamp(this.gameWorldDestinationPosition.y, this.limits.z, this.limits.w));
		}
		Vector3 vector2 = new Vector3(this.gameWorldDestinationPosition.x, this.gameWorldDestinationPosition.y, base.transform.position.z);
		base.transform.position = Vector3.MoveTowards(base.transform.position, vector2, 50f * Time.deltaTime);
		this.currentPosition = base.transform.position;
	}

	// Token: 0x060006C8 RID: 1736 RVA: 0x000414A8 File Offset: 0x0003F6A8
	private void InteractWithDigitalCursorInterface()
	{
		this.isLockedToDigitalCursorInterface = true;
		if (this.moveFreeVector.magnitude > this.stickDeadZone)
		{
			this.isLockedToUI = false;
			this.isFree = true;
			base.transform.position += this.moveFreeVector * Time.deltaTime * this.freeSpeed * Singleton.Instance.cursorSpeed;
			this.gameWorldDestinationTransform = null;
			EventSystem.current.SetSelectedGameObject(null);
		}
		else if ((!this.isLockedToUI && this.moveLockVector.magnitude > this.stickDeadZone) || this.GetInputDown("confirm"))
		{
			this.isLockedToUI = true;
			this.isFree = false;
			this.MoveToClosestInDigitalCursorInterface();
		}
		else if (!this.isLockedToUI && !this.isFree)
		{
			this.isLockedToUI = true;
			this.isFree = false;
			EventSystem current = EventSystem.current;
			Selectable defaultSelectable = this.digitalCursorInterface.defaultSelectable;
			current.SetSelectedGameObject((defaultSelectable != null) ? defaultSelectable.gameObject : null);
		}
		if (!this.isLockedToUI || this.isFree)
		{
			return;
		}
		this.digitalCursorInterface.Interact();
		if (EventSystem.current && EventSystem.current.currentSelectedGameObject != null)
		{
			Selectable component = EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>();
			if (!component || !component.interactable || !component.enabled || !component.gameObject.activeInHierarchy)
			{
				this.MoveToClosestInDigitalCursorInterface();
				return;
			}
			Vector3[] array = new Vector3[4];
			RectTransform component2 = EventSystem.current.currentSelectedGameObject.GetComponent<RectTransform>();
			if (!component2)
			{
				return;
			}
			component2.GetWorldCorners(array);
			Vector3 vector = array[3];
			foreach (Vector3 vector2 in array)
			{
				if (vector2.x > vector.x && vector2.y < vector.y)
				{
					vector = vector2;
				}
			}
			base.transform.position = vector + Vector3.left * 0.25f + Vector3.up * 0.25f;
		}
		if (EventSystem.current.currentSelectedGameObject)
		{
			this.currentTransformSelected = EventSystem.current.currentSelectedGameObject.transform;
			return;
		}
		this.currentTransformSelected = null;
	}

	// Token: 0x060006C9 RID: 1737 RVA: 0x00041710 File Offset: 0x0003F910
	public void SelectClosestInDigitalCursorInterface()
	{
		if (!this.digitalCursorInterface)
		{
			return;
		}
		this.isLockedToUI = true;
		this.MoveToClosestInDigitalCursorInterface();
	}

	// Token: 0x060006CA RID: 1738 RVA: 0x00041730 File Offset: 0x0003F930
	private void MoveToClosestInDigitalCursorInterface()
	{
		if (!this.digitalCursorInterface)
		{
			return;
		}
		Selectable closestSelectable = this.digitalCursorInterface.GetClosestSelectable(base.transform.position);
		if (closestSelectable)
		{
			EventSystem.current.SetSelectedGameObject(closestSelectable.gameObject);
		}
	}

	// Token: 0x060006CB RID: 1739 RVA: 0x00041780 File Offset: 0x0003F980
	private void ConsiderMovingInInventoryAndMap()
	{
		if (Overworld_Manager.main)
		{
			return;
		}
		if (DraggableCombat.GetDragging())
		{
			if (this.moveLockVector.magnitude > this.senstivity && this.currentTimeOut == this.timeOut)
			{
				DraggableCombat closestNonDragging = DraggableCombat.GetClosestNonDragging(this.currentPosition, this.moveLockVector.normalized);
				if (closestNonDragging)
				{
					this.SetGameWorldDestinationTransform(null);
					this.gameWorldDestinationPosition = closestNonDragging.transform.position + this.moveLockVector.normalized * 1f;
					this.UpdateContextControls();
					return;
				}
			}
			return;
		}
		bool flag = false;
		List<GameObject> list = new List<GameObject>();
		foreach (RaycastHit2D raycastHit2D in Physics2D.RaycastAll(this.currentPosition, Vector2.zero))
		{
			CustomInputHandler component = raycastHit2D.collider.GetComponent<CustomInputHandler>();
			if (component && component.enabledInterface)
			{
				if (raycastHit2D.collider.gameObject.CompareTag("GridSquare"))
				{
					this.positionInBackpack = this.currentPosition;
				}
				if (raycastHit2D.collider.gameObject.CompareTag("GridSquare") || raycastHit2D.collider.GetComponent<DungeonRoom>())
				{
					flag = true;
					if (this.moveLockVector.magnitude > this.senstivity)
					{
						this.currentPosition = raycastHit2D.collider.transform.position;
					}
				}
				list.Add(raycastHit2D.collider.gameObject);
			}
		}
		if (GameManager.main && GameManager.main.draggingItem)
		{
			flag = true;
		}
		if (this.currentTimeOut != this.timeOut)
		{
			return;
		}
		if (this.moveLockVector.magnitude > this.senstivity)
		{
			ItemMovement.RemoveAnyItemCard();
			if (flag && this.currentTimeOut == this.timeOut)
			{
				this.followObject = null;
				if (this.moveLockVector.x > this.senstivity)
				{
					flag = this.ConsiderSingleMove(this.gameWorldDestinationPosition + Vector3.right);
				}
				else if (this.moveLockVector.x < this.senstivity * -1f)
				{
					flag = this.ConsiderSingleMove(this.gameWorldDestinationPosition + Vector3.left);
				}
				if (this.moveLockVector.y > this.senstivity)
				{
					flag = this.ConsiderSingleMove(this.gameWorldDestinationPosition + Vector3.up);
				}
				else if (this.moveLockVector.y < this.senstivity * -1f)
				{
					flag = this.ConsiderSingleMove(this.gameWorldDestinationPosition + Vector3.down);
				}
			}
			if (flag)
			{
				this.gameWorldDestinationTransform = null;
				this.UpdateContextControls();
				return;
			}
			if (this.currentTimeOut == this.timeOut)
			{
				GameObject closestItemOrGridInDirection = ItemTogglerForController.GetClosestItemOrGridInDirection(base.transform.position, this.moveLockVector.normalized, list);
				if (closestItemOrGridInDirection != null)
				{
					this.SetGameWorldDestinationTransform(closestItemOrGridInDirection.transform);
					this.UpdateContextControls();
					return;
				}
				this.followObject = null;
				foreach (RaycastHit2D raycastHit2D2 in Physics2D.RaycastAll(this.currentPosition, new Vector2((float)Mathf.RoundToInt(this.moveLockVector.x * 3f) / 3f, (float)Mathf.RoundToInt(this.moveLockVector.y * 3f) / 3f)))
				{
					if (raycastHit2D2.point.x >= this.limits.x && raycastHit2D2.point.x <= this.limits.y && raycastHit2D2.point.y >= this.limits.z && raycastHit2D2.point.y <= this.limits.w && raycastHit2D2.collider.enabled)
					{
						CustomInputHandler component2 = raycastHit2D2.collider.GetComponent<CustomInputHandler>();
						if (component2 && component2.enabledInterface && !list.Contains(raycastHit2D2.collider.gameObject))
						{
							DigitalInputSelectOnButton component3 = raycastHit2D2.collider.GetComponent<DigitalInputSelectOnButton>();
							if (!component3 || component3.isRaycastable)
							{
								this.SetGameWorldDestinationTransform(raycastHit2D2.collider.gameObject.transform);
								this.UpdateContextControls();
								return;
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x060006CC RID: 1740 RVA: 0x00041C1C File Offset: 0x0003FE1C
	public List<GameObject> ObjsHere()
	{
		List<GameObject> list = new List<GameObject>();
		foreach (RaycastHit2D raycastHit2D in Physics2D.RaycastAll(this.currentPosition, Vector2.zero))
		{
			list.Add(raycastHit2D.collider.gameObject);
		}
		return list;
	}

	// Token: 0x060006CD RID: 1741 RVA: 0x00041C6C File Offset: 0x0003FE6C
	private bool ConsiderSingleMove(Vector3 pos)
	{
		if (GameManager.main && GameManager.main.draggingItem)
		{
			this.gameWorldDestinationPosition = pos;
			return true;
		}
		foreach (RaycastHit2D raycastHit2D in Physics2D.RaycastAll(pos, Vector2.zero))
		{
			CustomInputHandler component = raycastHit2D.collider.GetComponent<CustomInputHandler>();
			if (component && component.enabledInterface && (raycastHit2D.collider.gameObject.CompareTag("GridSquare") || raycastHit2D.collider.GetComponent<DungeonRoom>()))
			{
				this.gameWorldDestinationPosition = pos;
				return true;
			}
		}
		return false;
	}

	// Token: 0x060006CE RID: 1742 RVA: 0x00041D18 File Offset: 0x0003FF18
	public GameObject GetCurrentUITop()
	{
		this.ConsiderFindingGraphicsRaycasters();
		foreach (BaseRaycaster baseRaycaster in this.graphicRaycasters)
		{
			PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
			pointerEventData.position = Camera.main.WorldToScreenPoint(base.transform.position);
			List<RaycastResult> list = new List<RaycastResult>();
			baseRaycaster.Raycast(pointerEventData, list);
			foreach (RaycastResult raycastResult in list)
			{
				CustomInputHandler component = raycastResult.gameObject.GetComponent<CustomInputHandler>();
				if (component && component.enabledInterface)
				{
					return raycastResult.gameObject;
				}
			}
		}
		return null;
	}

	// Token: 0x060006CF RID: 1743 RVA: 0x00041E10 File Offset: 0x00040010
	private void GetDirectionalInteraction()
	{
		if (this.currentTimeOut > 0f)
		{
			this.currentTimeOut -= Time.deltaTime;
		}
		if (this.moveLockVector.x > this.senstivity)
		{
			this.NewButton("right", DigitalCursor.InputListener.Type.hold);
		}
		else if (this.GetInputHold("right"))
		{
			this.NewButton("right", DigitalCursor.InputListener.Type.release);
		}
		if (this.moveLockVector.x < this.senstivity * -1f)
		{
			this.NewButton("left", DigitalCursor.InputListener.Type.hold);
		}
		else if (this.GetInputHold("left"))
		{
			this.NewButton("left", DigitalCursor.InputListener.Type.release);
		}
		if (this.moveLockVector.y > this.senstivity)
		{
			this.NewButton("up", DigitalCursor.InputListener.Type.hold);
		}
		else if (this.GetInputHold("up"))
		{
			this.NewButton("up", DigitalCursor.InputListener.Type.release);
		}
		if (this.moveLockVector.y < this.senstivity * -1f)
		{
			this.NewButton("down", DigitalCursor.InputListener.Type.hold);
		}
		else if (this.GetInputHold("down"))
		{
			this.NewButton("down", DigitalCursor.InputListener.Type.release);
		}
		if (this.currentTimeOut <= 0f)
		{
			if (this.moveLockVector.x > this.senstivity)
			{
				this.NewButton("right", DigitalCursor.InputListener.Type.press);
				this.currentTimeOut = this.timeOut;
			}
			else if (this.moveLockVector.x < this.senstivity * -1f)
			{
				this.NewButton("left", DigitalCursor.InputListener.Type.press);
				this.currentTimeOut = this.timeOut;
			}
			if (this.moveLockVector.y > this.senstivity)
			{
				this.NewButton("up", DigitalCursor.InputListener.Type.press);
				this.currentTimeOut = this.timeOut;
				return;
			}
			if (this.moveLockVector.y < this.senstivity * -1f)
			{
				this.NewButton("down", DigitalCursor.InputListener.Type.press);
				this.currentTimeOut = this.timeOut;
			}
		}
	}

	// Token: 0x060006D0 RID: 1744 RVA: 0x00041FFC File Offset: 0x000401FC
	private void GetInteractionEvents()
	{
		Vector3 position = base.transform.position;
		if (!this.currentlyLockedToUI && !this.digitalCursorInterface && this.GetInputDown("confirm"))
		{
			bool flag = true;
			foreach (GraphicRaycaster graphicRaycaster in this.graphicRaycasters)
			{
				if (!flag)
				{
					break;
				}
				PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
				pointerEventData.position = Camera.main.WorldToScreenPoint(position);
				List<RaycastResult> list = new List<RaycastResult>();
				graphicRaycaster.Raycast(pointerEventData, list);
				foreach (RaycastResult raycastResult in list)
				{
					flag = false;
					Selectable componentInChildren = raycastResult.gameObject.GetComponentInChildren<Selectable>();
					if (componentInChildren)
					{
						DigitalInputSelectOnButton component = componentInChildren.GetComponent<DigitalInputSelectOnButton>();
						if ((!component || component.isRaycastable) && (!component || !component.disabledDuringSpecialOrginization || (!GameManager.main.inSpecialReorg && GameManager.main.inventoryPhase != GameManager.InventoryPhase.choose)))
						{
							CanvasGroup componentInParent = componentInChildren.GetComponentInParent<CanvasGroup>();
							if (componentInParent && componentInParent.interactable)
							{
								this.SelectUIElementIfNothingSelected(componentInChildren.gameObject);
							}
						}
					}
				}
			}
			if (EventSystem.current.currentSelectedGameObject)
			{
				Button componentInChildren2 = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Button>();
				if (componentInChildren2 && componentInChildren2.interactable && componentInChildren2.onClick.GetPersistentEventCount() > 0)
				{
					componentInChildren2.onClick.Invoke();
					return;
				}
				Toggle componentInChildren3 = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Toggle>();
				if (componentInChildren3)
				{
					componentInChildren3.isOn = !componentInChildren3.isOn;
					return;
				}
				Dropdown componentInChildren4 = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Dropdown>();
				if (componentInChildren4)
				{
					componentInChildren4.Show();
					return;
				}
			}
		}
		List<CustomInputHandler> list2 = new List<CustomInputHandler>();
		if (this.timeOutBeforeInteractionWithNonUIElement <= 0f)
		{
			RaycastHit2D[] array = Physics2D.RaycastAll(position, Vector2.zero);
			float num = 9999999f;
			foreach (RaycastHit2D raycastHit2D in array)
			{
				if (num >= raycastHit2D.collider.transform.position.z)
				{
					List<CustomInputHandler> list3 = new List<CustomInputHandler>();
					CustomInputHandler[] componentsInChildren = raycastHit2D.collider.GetComponentsInChildren<CustomInputHandler>();
					if (componentsInChildren.Length != 0)
					{
						list3.AddRange(componentsInChildren);
					}
					CustomInputHandler[] componentsInParent = raycastHit2D.collider.GetComponentsInParent<CustomInputHandler>();
					if (componentsInParent.Length != 0)
					{
						foreach (CustomInputHandler customInputHandler in componentsInParent)
						{
							if (!list3.Contains(customInputHandler))
							{
								list3.Add(customInputHandler);
							}
						}
					}
					if (list3.Count > 0)
					{
						num = raycastHit2D.collider.transform.position.z;
						list2 = new List<CustomInputHandler>(list3);
					}
				}
			}
		}
		foreach (BaseRaycaster baseRaycaster in this.graphicRaycasters)
		{
			PointerEventData pointerEventData2 = new PointerEventData(EventSystem.current);
			pointerEventData2.position = Camera.main.WorldToScreenPoint(position);
			List<RaycastResult> list4 = new List<RaycastResult>();
			baseRaycaster.Raycast(pointerEventData2, list4);
			foreach (RaycastResult raycastResult2 in list4)
			{
				foreach (CustomInputHandler customInputHandler2 in raycastResult2.gameObject.GetComponents<CustomInputHandler>())
				{
					if (customInputHandler2 && customInputHandler2.enabledInterface && !list2.Contains(customInputHandler2))
					{
						list2.Add(customInputHandler2);
					}
				}
			}
		}
		foreach (CustomInputHandler customInputHandler3 in list2)
		{
			if (!this.lastUpdateHoveringObjects.Contains(customInputHandler3))
			{
				customInputHandler3.OnCursorStart();
			}
			customInputHandler3.OnCursorHold();
		}
		foreach (CustomInputHandler customInputHandler4 in this.lastUpdateHoveringObjects)
		{
			if (!list2.Contains(customInputHandler4) && customInputHandler4 && customInputHandler4.gameObject)
			{
				customInputHandler4.OnCursorEnd();
			}
		}
		new List<DigitalCursor.InputOnObject>();
		foreach (DigitalCursor.InputListener inputListener in new List<DigitalCursor.InputListener>(this.inputListeners))
		{
			foreach (CustomInputHandler customInputHandler5 in list2)
			{
				if (customInputHandler5 && customInputHandler5.enabledInterface)
				{
					if (inputListener.type == DigitalCursor.InputListener.Type.press)
					{
						customInputHandler5.OnPressStart(inputListener.name, false);
					}
					else if (inputListener.type == DigitalCursor.InputListener.Type.hold)
					{
						customInputHandler5.OnPressHold(inputListener.name);
					}
					else if (inputListener.type == DigitalCursor.InputListener.Type.release)
					{
						customInputHandler5.OnPressEnd(inputListener.name);
					}
				}
			}
		}
		this.timeOutBeforeInteractionWithNonUIElement -= Time.deltaTime;
		this.lastUpdateHoveringObjects = new List<CustomInputHandler>(list2);
	}

	// Token: 0x060006D1 RID: 1745 RVA: 0x00042644 File Offset: 0x00040844
	private void ConsiderFindingGraphicsRaycasters()
	{
		if (this.graphicRaycasters.Count == 0)
		{
			this.FindGraphicsRaycasters();
			return;
		}
		using (List<GraphicRaycaster>.Enumerator enumerator = this.graphicRaycasters.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (!enumerator.Current)
				{
					this.FindGraphicsRaycasters();
					break;
				}
			}
		}
	}

	// Token: 0x060006D2 RID: 1746 RVA: 0x000426B4 File Offset: 0x000408B4
	private void FindGraphicsRaycasters()
	{
		this.graphicRaycasters = Object.FindObjectsOfType<GraphicRaycaster>().ToList<GraphicRaycaster>();
		this.graphicRaycasters.Sort((GraphicRaycaster x, GraphicRaycaster y) => x.GetComponent<Canvas>().sortingOrder.CompareTo(y.GetComponent<Canvas>().sortingOrder));
		this.graphicRaycasters.Reverse();
	}

	// Token: 0x060006D3 RID: 1747 RVA: 0x00042708 File Offset: 0x00040908
	public void UpdateContextControls()
	{
		ContextControlsDisplay main = ContextControlsDisplay.main;
		LevelUpManager main2 = LevelUpManager.main;
		if (!main)
		{
			return;
		}
		if (this.controlStyle == DigitalCursor.ControlStyle.cursor)
		{
			return;
		}
		if (main2 && main2.levelingUp)
		{
			main.ClearAllControls();
			return;
		}
		if (this.digitalCursorInterface)
		{
			GameObject currentUITop = this.GetCurrentUITop();
			if (!currentUITop || !currentUITop.GetComponent<Overworld_InventoryItemButton>() || Overworld_InventoryItemButton.draggingButton)
			{
				main.ClearControl("x");
				main.ClearControl("lt");
				main.ClearControl("rt");
				main.ClearControl("y");
				main.ClearControl("a");
				main.ClearControl("b");
				return;
			}
			main.ClearControl("b");
			main.ShowControl("x", "cpExamine");
			if (Overworld_InventoryManager.currentInventories > 1)
			{
				main.ShowControl("a", "cpSwap");
				return;
			}
			main.ClearControl("a");
			return;
		}
		else
		{
			if (GameManager.main && GameManager.main.draggingItem)
			{
				if (!ContextMenuManager.main.IsViewingCard())
				{
					main.ShowControl("x", "cpExamine");
				}
				else
				{
					main.ClearControl("x");
				}
				if (GameManager.main.inventoryPhase != GameManager.InventoryPhase.locked)
				{
					main.ShowControl("a", "cpPlace");
				}
				else
				{
					main.ShowControl("a", "cpUse");
				}
				main.ShowControl("b", "cpDrop");
				main.ShowControl(new List<string> { "lt", "rt" }, "cpRotate");
				return;
			}
			if (GameFlowManager.main && GameFlowManager.main.battlePhase != GameFlowManager.BattlePhase.outOfBattle && Enemy.EnemiesAliveCount() > 1 && !GameManager.main.dead)
			{
				main.ShowControl(new List<string> { "lt", "rt" }, "cpSelect Enemy");
			}
			else
			{
				main.ClearControl("lt");
				main.ClearControl("rt");
			}
			foreach (RaycastHit2D raycastHit2D in Physics2D.RaycastAll(this.currentPosition, Vector2.zero))
			{
				if (raycastHit2D.collider.CompareTag("Item") && GameManager.main && GameManager.main.inventoryPhase != GameManager.InventoryPhase.notInteractable)
				{
					Item2 component = raycastHit2D.collider.GetComponent<Item2>();
					if (component)
					{
						if (GameManager.main.inventoryPhase == GameManager.InventoryPhase.choose)
						{
							main.ShowControl("a", "cpSelect");
						}
						else if (GameManager.main.inventoryPhase != GameManager.InventoryPhase.locked || component.CheckForStatusEffect(Item2.ItemStatusEffect.Type.canBeMovedInCombat) || component.CheckForStatusEffect(Item2.ItemStatusEffect.Type.canBeMovedInCombatButReturnsToOriginalPosition))
						{
							main.ShowControl("a", "cpPick Up");
						}
						else
						{
							main.ShowControl("a", "cpUse");
						}
						if (!ContextMenuManager.main.IsViewingCard())
						{
							main.ShowControl("x", "cpExamine");
							return;
						}
						main.ShowControl("x", "cpContext Menu");
						return;
					}
				}
				else if (raycastHit2D.collider.GetComponentInParent<DungeonRoom>())
				{
					main.ClearControl("b");
					main.ClearControl("x");
					main.ShowControl("a", "cpMove Here");
					return;
				}
			}
			main.ClearControl("a");
			main.ClearControl("b");
			main.ClearControl("x");
			main.ClearControl("y");
			return;
		}
	}

	// Token: 0x060006D4 RID: 1748 RVA: 0x00042A9C File Offset: 0x00040C9C
	private void RemoveListenersForButton(string text)
	{
		for (int i = 0; i < this.inputListeners.Count; i++)
		{
			if (this.inputListeners[i].name == text)
			{
				this.inputListeners.RemoveAt(i);
				i--;
			}
		}
	}

	// Token: 0x060006D5 RID: 1749 RVA: 0x00042AE8 File Offset: 0x00040CE8
	private void LateUpdate()
	{
		for (int i = 0; i < this.inputListeners.Count; i++)
		{
			DigitalCursor.InputListener inputListener = this.inputListeners[i];
			if (inputListener.type == DigitalCursor.InputListener.Type.release)
			{
				this.RemoveListenersForButton(inputListener.name);
				i = 0;
			}
			else if (inputListener.type == DigitalCursor.InputListener.Type.press)
			{
				this.inputListeners.RemoveAt(i);
				i--;
			}
		}
	}

	// Token: 0x060006D6 RID: 1750 RVA: 0x00042B4C File Offset: 0x00040D4C
	private void SwitchToController()
	{
		if (this.controlStyle == DigitalCursor.ControlStyle.cursor && !this.fakeController)
		{
			if (this.controlStyle != DigitalCursor.ControlStyle.controller)
			{
				this.controlStyle = DigitalCursor.ControlStyle.controller;
				OnControllerSwitchEvent.CallAllEvents();
			}
			Vector3 vector = new Vector3(0f, 0f, 0f);
			if (Mouse.current != null)
			{
				vector = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
			}
			this.currentPosition = new Vector3(vector.x, vector.y, 0f);
		}
	}

	// Token: 0x060006D7 RID: 1751 RVA: 0x00042BDC File Offset: 0x00040DDC
	public void NewButton(string keyName, DigitalCursor.InputListener.Type type)
	{
		if (type == DigitalCursor.InputListener.Type.press && this.GetInputHold(keyName))
		{
			return;
		}
		if (keyName == "confirm" || keyName == "pause")
		{
			Debug.Log("NewButton " + keyName + " " + type.ToString());
			this.SwitchToController();
		}
		foreach (DigitalCursor.InputListener inputListener in this.inputListeners)
		{
			if (inputListener.name == keyName.ToLower() && inputListener.type == type)
			{
				return;
			}
		}
		this.inputListeners.Add(new DigitalCursor.InputListener(keyName.ToLower(), type));
	}

	// Token: 0x04000547 RID: 1351
	private static DigitalCursor _instance;

	// Token: 0x04000548 RID: 1352
	[HideInInspector]
	public Vector2 currentPosition;

	// Token: 0x04000549 RID: 1353
	private Vector4 limits = new Vector4(-9.75f, 9.75f, -5.5f, 5.7f);

	// Token: 0x0400054A RID: 1354
	[Header("References")]
	private Controller controller;

	// Token: 0x0400054B RID: 1355
	private SpriteRenderer spriteRenderer;

	// Token: 0x0400054C RID: 1356
	[SerializeField]
	private GameObject buttonIconPrefab;

	// Token: 0x0400054D RID: 1357
	[SerializeField]
	private GameObject spriteButtonIconPrefab;

	// Token: 0x0400054E RID: 1358
	private List<GraphicRaycaster> graphicRaycasters = new List<GraphicRaycaster>();

	// Token: 0x0400054F RID: 1359
	[SerializeField]
	private List<CustomInputHandler> lastUpdateHoveringObjects = new List<CustomInputHandler>();

	// Token: 0x04000550 RID: 1360
	[Header(" ---- Controller Images ---- ")]
	[SerializeField]
	private List<DigitalCursor.ControllerImage> controllerImages = new List<DigitalCursor.ControllerImage>();

	// Token: 0x04000551 RID: 1361
	[Header("Universal Settings")]
	[SerializeField]
	private float senstivity = 0.1f;

	// Token: 0x04000552 RID: 1362
	[SerializeField]
	private float freeSensitivity = 0.1f;

	// Token: 0x04000553 RID: 1363
	[SerializeField]
	private float stickDeadZone = 0.2f;

	// Token: 0x04000554 RID: 1364
	[SerializeField]
	private float timeOut = 0.02f;

	// Token: 0x04000555 RID: 1365
	[SerializeField]
	private float freeSpeed = 3f;

	// Token: 0x04000556 RID: 1366
	[SerializeField]
	private float currentTimeOut;

	// Token: 0x04000557 RID: 1367
	private float timeOutBeforeInteractionWithNonUIElement;

	// Token: 0x04000558 RID: 1368
	[Header("Settings")]
	[SerializeField]
	private List<DigitalCursor.InputListener> inputListeners = new List<DigitalCursor.InputListener>();

	// Token: 0x04000559 RID: 1369
	private DefaultInputActions defaultInput;

	// Token: 0x0400055A RID: 1370
	[SerializeField]
	private List<GameObject> previouslySelectedUIElements = new List<GameObject>();

	// Token: 0x0400055B RID: 1371
	[SerializeField]
	private bool freeMoveOnly;

	// Token: 0x0400055E RID: 1374
	public Transform followObject;

	// Token: 0x0400055F RID: 1375
	public DigitalCursor.ControlStyle controlStyle;

	// Token: 0x04000560 RID: 1376
	private DigitalCursor.ControllerImage choseControllerIamge;

	// Token: 0x04000561 RID: 1377
	public bool isEscapable;

	// Token: 0x04000562 RID: 1378
	public bool currentlyLockedToUI;

	// Token: 0x04000563 RID: 1379
	public GameObject UIObjectSelected;

	// Token: 0x04000564 RID: 1380
	public GameObject worldObjectSelected;

	// Token: 0x04000565 RID: 1381
	private Vector2 positionInBackpack = Vector2.zero;

	// Token: 0x04000566 RID: 1382
	public bool isHidden;

	// Token: 0x04000567 RID: 1383
	private FollowObjectCamera followObjectCamera;

	// Token: 0x04000568 RID: 1384
	[Header("!!~~~~~~~~~~~~~~~~~~new Stored Properties~~~~~~~~~~~~~~~~~~!!")]
	[SerializeField]
	private Transform gameWorldDestinationTransform;

	// Token: 0x04000569 RID: 1385
	[SerializeField]
	private Vector3 gameWorldDestinationPosition = Vector3.zero;

	// Token: 0x0400056A RID: 1386
	public DigitalCursorInterface digitalCursorInterface;

	// Token: 0x0400056B RID: 1387
	[SerializeField]
	private bool isLockedToDigitalCursorInterface;

	// Token: 0x0400056C RID: 1388
	[SerializeField]
	private Transform currentTransformSelected;

	// Token: 0x0400056D RID: 1389
	[SerializeField]
	private bool isLockedToUI;

	// Token: 0x0400056E RID: 1390
	private bool isFree;

	// Token: 0x0400056F RID: 1391
	[HideInInspector]
	public bool canChangeController = true;

	// Token: 0x04000570 RID: 1392
	[HideInInspector]
	public bool fakeController;

	// Token: 0x04000571 RID: 1393
	private RectTransform overworldBuildMenuRectTransform;

	// Token: 0x02000314 RID: 788
	[Serializable]
	private class ControllerImage
	{
		// Token: 0x04001253 RID: 4691
		[SerializeField]
		private string controlName;

		// Token: 0x04001254 RID: 4692
		public List<DigitalCursor.ButtonImage> buttonImages = new List<DigitalCursor.ButtonImage>();
	}

	// Token: 0x02000315 RID: 789
	[Serializable]
	private struct ButtonImage
	{
		// Token: 0x04001255 RID: 4693
		public string name;

		// Token: 0x04001256 RID: 4694
		public Sprite sprite;
	}

	// Token: 0x02000316 RID: 790
	[Serializable]
	public struct InputListener
	{
		// Token: 0x060015AF RID: 5551 RVA: 0x000BC92D File Offset: 0x000BAB2D
		public InputListener(string name, DigitalCursor.InputListener.Type inputType)
		{
			this.name = name;
			this.type = inputType;
		}

		// Token: 0x04001257 RID: 4695
		public string name;

		// Token: 0x04001258 RID: 4696
		public DigitalCursor.InputListener.Type type;

		// Token: 0x0200049C RID: 1180
		public enum Type
		{
			// Token: 0x04001B06 RID: 6918
			press,
			// Token: 0x04001B07 RID: 6919
			release,
			// Token: 0x04001B08 RID: 6920
			hold
		}
	}

	// Token: 0x02000317 RID: 791
	private struct InputOnObject
	{
		// Token: 0x060015B0 RID: 5552 RVA: 0x000BC93D File Offset: 0x000BAB3D
		public InputOnObject(CustomInputHandler customInputHandler, string key)
		{
			this.customInputHandler = customInputHandler;
			this.key = key;
		}

		// Token: 0x04001259 RID: 4697
		public string key;

		// Token: 0x0400125A RID: 4698
		public CustomInputHandler customInputHandler;
	}

	// Token: 0x02000318 RID: 792
	public enum ControlStyle
	{
		// Token: 0x0400125C RID: 4700
		cursor,
		// Token: 0x0400125D RID: 4701
		controller
	}

	// Token: 0x02000319 RID: 793
	public enum VectorType
	{
		// Token: 0x0400125F RID: 4703
		either,
		// Token: 0x04001260 RID: 4704
		free,
		// Token: 0x04001261 RID: 4705
		locked
	}
}
