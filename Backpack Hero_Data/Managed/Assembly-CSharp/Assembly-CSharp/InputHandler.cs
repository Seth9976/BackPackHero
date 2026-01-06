using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000060 RID: 96
public class InputHandler : MonoBehaviour
{
	// Token: 0x060001B9 RID: 441 RVA: 0x0000AD63 File Offset: 0x00008F63
	private void OnEnable()
	{
		if (!InputHandler.inputHandlers.Contains(this))
		{
			InputHandler.inputHandlers.Add(this);
		}
		this.UpdateIcon();
	}

	// Token: 0x060001BA RID: 442 RVA: 0x0000AD84 File Offset: 0x00008F84
	private void OnDisable()
	{
		if (InputHandler.inputHandlers.Contains(this))
		{
			InputHandler.inputHandlers.Remove(this);
		}
		if (this.controllerIcon)
		{
			Object.Destroy(this.controllerIcon.gameObject);
		}
		if (!SceneLoader.main || SceneLoader.main.IsLoading())
		{
			return;
		}
		this.ConsiderDisabledInputHandlers();
	}

	// Token: 0x060001BB RID: 443 RVA: 0x0000ADE8 File Offset: 0x00008FE8
	private void AddToStoredInputHandlers()
	{
		InputHandler.StoredInputHandler storedInputHandler = new InputHandler.StoredInputHandler();
		storedInputHandler.inputHandler = this;
		storedInputHandler.key = this.key;
		storedInputHandler.pressType = this.pressType;
		InputHandler.storedInputHandlers.Remove(storedInputHandler);
		InputHandler.storedInputHandlers.Insert(0, storedInputHandler);
		this.disabled = false;
	}

	// Token: 0x060001BC RID: 444 RVA: 0x0000AE3C File Offset: 0x0000903C
	private void CleanInputHandlerList()
	{
		for (int i = 0; i < InputHandler.storedInputHandlers.Count; i++)
		{
			if (!InputHandler.storedInputHandlers[i].inputHandler || !InputHandler.storedInputHandlers[i].inputHandler.gameObject || !InputHandler.storedInputHandlers[i].inputHandler.gameObject.activeInHierarchy)
			{
				InputHandler.storedInputHandlers.RemoveAt(i);
				i--;
			}
		}
	}

	// Token: 0x060001BD RID: 445 RVA: 0x0000AEBC File Offset: 0x000090BC
	private void CheckForConflictingInputHandlers()
	{
		this.CleanInputHandlerList();
		foreach (InputHandler.StoredInputHandler storedInputHandler in InputHandler.storedInputHandlers)
		{
			if (!(storedInputHandler.inputHandler == this) && storedInputHandler.key == this.key && storedInputHandler.pressType == this.pressType)
			{
				storedInputHandler.inputHandler.disabled = true;
				if (storedInputHandler.inputHandler.controllerIcon)
				{
					Object.Destroy(storedInputHandler.inputHandler.controllerIcon.gameObject);
				}
			}
		}
	}

	// Token: 0x060001BE RID: 446 RVA: 0x0000AF6C File Offset: 0x0000916C
	private void ConsiderDisabledInputHandlers()
	{
		InputHandler inputHandler = null;
		this.CleanInputHandlerList();
		for (int i = InputHandler.storedInputHandlers.Count - 1; i >= 0; i--)
		{
			if (!(InputHandler.storedInputHandlers[i].inputHandler == this) && InputHandler.storedInputHandlers[i].key == this.key && InputHandler.storedInputHandlers[i].pressType == this.pressType && InputHandler.storedInputHandlers[i].inputHandler.setupKey == InputHandler.Key.None)
			{
				inputHandler = InputHandler.storedInputHandlers[i].inputHandler;
			}
		}
		if (inputHandler)
		{
			inputHandler.disabled = false;
			inputHandler.UpdateIcon();
		}
	}

	// Token: 0x060001BF RID: 447 RVA: 0x0000B01F File Offset: 0x0000921F
	public void LaunchCustomEvent()
	{
		UnityEvent unityEvent = this.customEvent;
		if (unityEvent == null)
		{
			return;
		}
		unityEvent.Invoke();
	}

	// Token: 0x060001C0 RID: 448 RVA: 0x0000B031 File Offset: 0x00009231
	public void SetKey(InputHandler.Key key)
	{
		this.key = key;
	}

	// Token: 0x060001C1 RID: 449 RVA: 0x0000B03A File Offset: 0x0000923A
	public void ChangeKeys(InputHandler.Key key, List<InputHandler.Key> alternateKeys)
	{
		this.key = key;
		this.alternateKeys = alternateKeys;
		if (this.controllerIcon && base.enabled)
		{
			this.UpdateIcon();
		}
	}

	// Token: 0x060001C2 RID: 450 RVA: 0x0000B065 File Offset: 0x00009265
	public void SetAsCancel()
	{
		this.showIcon = InputHandler.ShowIcon.ControllerOnly;
		this.key = InputHandler.Key.B;
		this.alternateKeys = new List<InputHandler.Key>();
		this.pressType = InputHandler.PressType.Hold;
	}

	// Token: 0x060001C3 RID: 451 RVA: 0x0000B087 File Offset: 0x00009287
	private void Start()
	{
		this.rectTransform = base.GetComponent<RectTransform>();
		this.UpdateIcon();
	}

	// Token: 0x060001C4 RID: 452 RVA: 0x0000B09C File Offset: 0x0000929C
	private void Update()
	{
		if (SingleUI.IsViewingPopUp() && !base.transform.IsChildOf(SingleUI.GetPopUp()))
		{
			if (this.controllerIcon)
			{
				this.controllerIcon.gameObject.SetActive(false);
			}
			return;
		}
		if (this.controllerIcon)
		{
			this.controllerIcon.gameObject.SetActive(true);
		}
		this.ReceiveInput();
	}

	// Token: 0x060001C5 RID: 453 RVA: 0x0000B108 File Offset: 0x00009308
	private void ReceiveInput()
	{
		if (this.pressType == InputHandler.PressType.Toggler)
		{
			if (DigitalCursor.main.GetInputHold(InputHandler.GetKeyName(this.key)))
			{
				if (this.controllerIcon)
				{
					Object.Destroy(this.controllerIcon.gameObject);
					InputHandler.StoredInputHandler storedInputHandler = new InputHandler.StoredInputHandler();
					storedInputHandler.inputHandler = this;
					storedInputHandler.key = this.key;
					storedInputHandler.pressType = this.pressType;
					InputHandler.storedInputHandlers.Remove(storedInputHandler);
					InputHandler.storedInputHandlers.Add(storedInputHandler);
					this.disabled = true;
					this.ConsiderDisabledInputHandlers();
					return;
				}
			}
			else if (!this.controllerIcon)
			{
				this.UpdateIcon();
			}
			return;
		}
		if (this.setupKey != InputHandler.Key.None)
		{
			if (!DigitalCursor.main.GetInputHold(InputHandler.GetKeyName(this.setupKey)))
			{
				if (this.controllerIcon)
				{
					Object.Destroy(this.controllerIcon.gameObject);
					InputHandler.StoredInputHandler storedInputHandler2 = new InputHandler.StoredInputHandler();
					storedInputHandler2.inputHandler = this;
					storedInputHandler2.key = this.key;
					storedInputHandler2.pressType = this.pressType;
					InputHandler.storedInputHandlers.Remove(storedInputHandler2);
					InputHandler.storedInputHandlers.Add(storedInputHandler2);
					this.disabled = true;
					this.ConsiderDisabledInputHandlers();
					this.controllerIcon = null;
				}
			}
			else if (!this.controllerIcon)
			{
				this.UpdateIcon();
			}
		}
		if (!this.controllerIcon)
		{
			return;
		}
		bool flag = this.setupKey == InputHandler.Key.None || DigitalCursor.main.GetInputHold(InputHandler.GetKeyName(this.setupKey));
		if (flag)
		{
			flag = DigitalCursor.main.GetInputHold(InputHandler.GetKeyName(this.key));
			if (!flag)
			{
				foreach (InputHandler.Key key in this.alternateKeys)
				{
					if (DigitalCursor.main.GetInputHold(InputHandler.GetKeyName(key)))
					{
						flag = true;
						break;
					}
				}
			}
		}
		if (flag)
		{
			this.controllerIcon.Press();
			return;
		}
		this.controllerIcon.Release();
	}

	// Token: 0x060001C6 RID: 454 RVA: 0x0000B318 File Offset: 0x00009518
	public static string GetKeyName(InputHandler.Key key)
	{
		string text = "";
		if (key == InputHandler.Key.Y)
		{
			text = "contextualAction";
		}
		else if (key == InputHandler.Key.A)
		{
			text = "confirm";
		}
		else if (key == InputHandler.Key.B)
		{
			text = "cancel";
		}
		else if (key == InputHandler.Key.X)
		{
			text = "contextmenu";
		}
		else if (key == InputHandler.Key.LeftBumper)
		{
			text = "leftBumper";
		}
		else if (key == InputHandler.Key.RightBumper)
		{
			text = "rightBumper";
		}
		else if (key == InputHandler.Key.LeftTrigger)
		{
			text = "rotateLeft";
		}
		else if (key == InputHandler.Key.RightTrigger)
		{
			text = "rotateRight";
		}
		else if (key == InputHandler.Key.Up)
		{
			text = "up";
		}
		else if (key == InputHandler.Key.Down)
		{
			text = "down";
		}
		else if (key == InputHandler.Key.Left)
		{
			text = "left";
		}
		else if (key == InputHandler.Key.Right)
		{
			text = "right";
		}
		else if (key == InputHandler.Key.Select)
		{
			text = "select";
		}
		else if (key == InputHandler.Key.Start)
		{
			text = "start";
		}
		return text;
	}

	// Token: 0x060001C7 RID: 455 RVA: 0x0000B3E0 File Offset: 0x000095E0
	public void UpdateIcon()
	{
		if (!DigitalCursor.main)
		{
			return;
		}
		if (!base.enabled)
		{
			return;
		}
		if (this.controllerIcon)
		{
			Object.Destroy(this.controllerIcon.gameObject);
		}
		InputHandler.Key key = this.key;
		GameObject controllerImage = DigitalCursor.main.GetControllerImage(InputHandler.GetKeyName(key), this.rectTransform, true);
		if (controllerImage)
		{
			Transform transform = null;
			GameObject gameObject = GameObject.FindGameObjectWithTag("UI Canvas");
			Canvas componentInParent = base.GetComponentInParent<Canvas>();
			if (!gameObject && !componentInParent)
			{
				return;
			}
			if (gameObject && !componentInParent)
			{
				transform = gameObject.transform;
			}
			else if (!gameObject && componentInParent)
			{
				transform = componentInParent.transform;
			}
			else if (gameObject && componentInParent)
			{
				Canvas component = gameObject.GetComponent<Canvas>();
				if (componentInParent.sortingOrder > component.sortingOrder)
				{
					transform = componentInParent.transform;
				}
				else
				{
					transform = gameObject.transform;
				}
			}
			this.controllerIcon = controllerImage.GetComponent<ControllerIcon>();
			this.controllerIcon.inputHandler = this;
			controllerImage.transform.SetParent(transform.transform);
			controllerImage.transform.localScale = Vector3.one;
			if (!this.controllerIconTransform)
			{
				if (!this.rectTransform)
				{
					this.rectTransform = base.GetComponent<RectTransform>();
				}
				this.controllerIcon.FollowTransformUpperRight(this.rectTransform, this.buttonParent);
			}
			else
			{
				this.controllerIcon.FollowTransformCenter(this.controllerIconTransform, this.buttonParent);
			}
			CanvasGroup[] componentsInParent = base.GetComponentsInParent<CanvasGroup>();
			if (componentsInParent.Length != 0)
			{
				this.controllerIcon.parentCanvasGroup = componentsInParent.ToList<CanvasGroup>();
			}
			SpriteRenderer componentInChildren = base.GetComponentInChildren<SpriteRenderer>();
			if (componentInChildren)
			{
				this.controllerIcon.spriteRenderer = componentInChildren;
			}
			if (this.pressType == InputHandler.PressType.Instant || this.pressType == InputHandler.PressType.Toggler || this.pressType == InputHandler.PressType.InstantOrHold || DigitalCursor.main.controlStyle != DigitalCursor.ControlStyle.controller)
			{
				this.controllerIcon.SetAsInstant(this.requiresHoldText);
			}
			if (this.animated == InputHandler.Animated.NotAnimated)
			{
				this.controllerIcon.GetComponent<Animator>().enabled = false;
			}
			if (InputHandler.GetKeyName(key) == "start" && (this.pressType == InputHandler.PressType.Hold || this.requiresHoldText) && Singleton.Instance.chosenControllerIcons == 2)
			{
				RectTransform component2 = this.controllerIcon.gameObject.GetComponentInChildren<TMP_Text>().GetComponent<RectTransform>();
				component2.anchorMax = new Vector2(component2.anchorMax.x, 0f);
				component2.anchorMin = new Vector2(component2.anchorMin.x, 0f);
			}
			this.AddToStoredInputHandlers();
			this.CheckForConflictingInputHandlers();
		}
	}

	// Token: 0x060001C8 RID: 456 RVA: 0x0000B688 File Offset: 0x00009888
	public void Launch()
	{
		if (this.customEvent.GetPersistentEventCount() > 0)
		{
			this.customEvent.Invoke();
			return;
		}
		Button componentInChildren = base.GetComponentInChildren<Button>();
		if (componentInChildren)
		{
			componentInChildren.onClick.Invoke();
			return;
		}
		Toggle componentInChildren2 = base.GetComponentInChildren<Toggle>();
		if (componentInChildren2)
		{
			componentInChildren2.isOn = !componentInChildren2.isOn;
			return;
		}
		CustomInputHandler componentInChildren3 = base.GetComponentInChildren<CustomInputHandler>();
		if (componentInChildren3)
		{
			componentInChildren3.OnPressStart("", true);
			return;
		}
		Selectable componentInChildren4 = base.GetComponentInChildren<Selectable>();
		if (componentInChildren4)
		{
			componentInChildren4.Select();
			return;
		}
	}

	// Token: 0x0400012D RID: 301
	public static List<InputHandler> inputHandlers = new List<InputHandler>();

	// Token: 0x0400012E RID: 302
	private static List<InputHandler.StoredInputHandler> storedInputHandlers = new List<InputHandler.StoredInputHandler>();

	// Token: 0x0400012F RID: 303
	[Header("---------Properties---------")]
	[SerializeField]
	private UnityEvent customEvent;

	// Token: 0x04000130 RID: 304
	[SerializeField]
	public InputHandler.ShowIcon showIcon;

	// Token: 0x04000131 RID: 305
	[SerializeField]
	public InputHandler.PressType pressType;

	// Token: 0x04000132 RID: 306
	[SerializeField]
	private bool requiresHoldText;

	// Token: 0x04000133 RID: 307
	[SerializeField]
	private InputHandler.Key setupKey = InputHandler.Key.None;

	// Token: 0x04000134 RID: 308
	[SerializeField]
	private InputHandler.Key key;

	// Token: 0x04000135 RID: 309
	[SerializeField]
	private List<InputHandler.Key> alternateKeys = new List<InputHandler.Key>();

	// Token: 0x04000136 RID: 310
	[SerializeField]
	private InputHandler.Animated animated = InputHandler.Animated.NotAnimated;

	// Token: 0x04000137 RID: 311
	[Header("------Controller Icon-------")]
	[SerializeField]
	private Transform controllerIconTransform;

	// Token: 0x04000138 RID: 312
	[SerializeField]
	private RectTransform buttonParent;

	// Token: 0x04000139 RID: 313
	private bool disabled;

	// Token: 0x0400013A RID: 314
	[Header("---------Debugging---------")]
	[SerializeField]
	private ControllerIcon controllerIcon;

	// Token: 0x0400013B RID: 315
	private RectTransform rectTransform;

	// Token: 0x0400013C RID: 316
	private SpriteRenderer spriteRenderer;

	// Token: 0x02000271 RID: 625
	private class StoredInputHandler
	{
		// Token: 0x04000F2D RID: 3885
		public InputHandler inputHandler;

		// Token: 0x04000F2E RID: 3886
		public InputHandler.Key key;

		// Token: 0x04000F2F RID: 3887
		public InputHandler.PressType pressType;
	}

	// Token: 0x02000272 RID: 626
	public enum ShowIcon
	{
		// Token: 0x04000F31 RID: 3889
		Always,
		// Token: 0x04000F32 RID: 3890
		Never,
		// Token: 0x04000F33 RID: 3891
		ControllerOnly
	}

	// Token: 0x02000273 RID: 627
	public enum PressType
	{
		// Token: 0x04000F35 RID: 3893
		Instant,
		// Token: 0x04000F36 RID: 3894
		Hold,
		// Token: 0x04000F37 RID: 3895
		Toggler,
		// Token: 0x04000F38 RID: 3896
		InstantOrHold
	}

	// Token: 0x02000274 RID: 628
	public enum Key
	{
		// Token: 0x04000F3A RID: 3898
		A,
		// Token: 0x04000F3B RID: 3899
		B,
		// Token: 0x04000F3C RID: 3900
		X,
		// Token: 0x04000F3D RID: 3901
		Y,
		// Token: 0x04000F3E RID: 3902
		LeftBumper,
		// Token: 0x04000F3F RID: 3903
		RightBumper,
		// Token: 0x04000F40 RID: 3904
		LeftTrigger,
		// Token: 0x04000F41 RID: 3905
		RightTrigger,
		// Token: 0x04000F42 RID: 3906
		Up,
		// Token: 0x04000F43 RID: 3907
		Down,
		// Token: 0x04000F44 RID: 3908
		Left,
		// Token: 0x04000F45 RID: 3909
		Right,
		// Token: 0x04000F46 RID: 3910
		Start,
		// Token: 0x04000F47 RID: 3911
		Select,
		// Token: 0x04000F48 RID: 3912
		None
	}

	// Token: 0x02000275 RID: 629
	public enum Animated
	{
		// Token: 0x04000F4A RID: 3914
		Animated,
		// Token: 0x04000F4B RID: 3915
		NotAnimated
	}
}
