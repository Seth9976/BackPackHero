using System;
using UnityEngine;
using UnityEngine.InputSystem;

// Token: 0x0200001E RID: 30
public class ControllerSpriteManager : MonoBehaviour
{
	// Token: 0x14000001 RID: 1
	// (add) Token: 0x060000E3 RID: 227 RVA: 0x000060C0 File Offset: 0x000042C0
	// (remove) Token: 0x060000E4 RID: 228 RVA: 0x000060F8 File Offset: 0x000042F8
	public event ControllerSpriteManager.OnSpriteSetChanged spriteSetChanged;

	// Token: 0x060000E5 RID: 229 RVA: 0x00006130 File Offset: 0x00004330
	private void OnEnable()
	{
		if (ControllerSpriteManager.instance == null)
		{
			ControllerSpriteManager.instance = this;
			Object.DontDestroyOnLoad(base.gameObject);
			this.inputActions = new InputActions();
			this.inputActions.Enable();
			foreach (InputActionMap inputActionMap in this.inputActions.asset.actionMaps)
			{
				foreach (InputAction inputAction in inputActionMap.actions)
				{
					inputAction.started += this.OnControllerEvent;
					inputAction.performed += this.OnControllerEvent;
					inputAction.canceled += this.OnControllerEvent;
				}
			}
			return;
		}
		Object.Destroy(base.gameObject);
	}

	// Token: 0x060000E6 RID: 230 RVA: 0x0000623C File Offset: 0x0000443C
	private void OnDisable()
	{
		if (ControllerSpriteManager.instance == this)
		{
			ControllerSpriteManager.instance = null;
		}
		if (this.inputActions != null)
		{
			this.inputActions.Disable();
		}
	}

	// Token: 0x060000E7 RID: 231 RVA: 0x00006264 File Offset: 0x00004464
	private void Update()
	{
		if ((Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2)) && this.isUsingController)
		{
			this.isUsingController = false;
			ControllerSpriteManager.OnSpriteSetChanged onSpriteSetChanged = this.spriteSetChanged;
			if (onSpriteSetChanged == null)
			{
				return;
			}
			onSpriteSetChanged();
		}
	}

	// Token: 0x060000E8 RID: 232 RVA: 0x000062A0 File Offset: 0x000044A0
	private void OnControllerEvent(InputAction.CallbackContext context)
	{
		if (context.control.device is Gamepad)
		{
			if (!this.isUsingController)
			{
				this.isUsingController = true;
				ControllerSpriteManager.OnSpriteSetChanged onSpriteSetChanged = this.spriteSetChanged;
				if (onSpriteSetChanged != null)
				{
					onSpriteSetChanged();
				}
			}
			if (this.autoDetect)
			{
				this.AutoDetect(context.control.device);
			}
		}
	}

	// Token: 0x060000E9 RID: 233 RVA: 0x000062FA File Offset: 0x000044FA
	public void AutoDetect(InputDevice device)
	{
		this.SwitchSpriteSet(this.GetControllerType(device));
	}

	// Token: 0x060000EA RID: 234 RVA: 0x00006309 File Offset: 0x00004509
	public void SwitchSpriteSet(ControllerSpriteManager.ControllerSpriteType set)
	{
		if (this.currentSpriteSet == set)
		{
			return;
		}
		this.currentSpriteSet = set;
		ControllerSpriteManager.OnSpriteSetChanged onSpriteSetChanged = this.spriteSetChanged;
		if (onSpriteSetChanged == null)
		{
			return;
		}
		onSpriteSetChanged();
	}

	// Token: 0x060000EB RID: 235 RVA: 0x0000632C File Offset: 0x0000452C
	public Sprite GetKeySprite(ButtonHandler.Key key)
	{
		switch (key)
		{
		case ButtonHandler.Key.Confirm:
			return this.spriteSets[this.currentSpriteSet].confirm;
		case ButtonHandler.Key.Cancel:
			return this.spriteSets[this.currentSpriteSet].cancel;
		case ButtonHandler.Key.YButton:
			return this.spriteSets[this.currentSpriteSet].alt;
		default:
			Debug.LogWarning("Unknown key sprite requested!");
			return null;
		}
	}

	// Token: 0x060000EC RID: 236 RVA: 0x000063A0 File Offset: 0x000045A0
	public ControllerSpriteManager.ControllerSpriteType GetControllerType(InputDevice device)
	{
		if (device == null)
		{
			return ControllerSpriteManager.ControllerSpriteType.Xbox;
		}
		if (device.displayName.ToLower().Contains("xbox"))
		{
			return ControllerSpriteManager.ControllerSpriteType.Xbox;
		}
		if (device.displayName.ToLower().Contains("playstation"))
		{
			return ControllerSpriteManager.ControllerSpriteType.PS;
		}
		if (device.displayName.ToLower().Contains("pro controller") || device.displayName.ToLower().Contains("joy-con"))
		{
			return ControllerSpriteManager.ControllerSpriteType.Switch;
		}
		return ControllerSpriteManager.ControllerSpriteType.Xbox;
	}

	// Token: 0x040000A5 RID: 165
	public static ControllerSpriteManager instance;

	// Token: 0x040000A6 RID: 166
	public bool isUsingController;

	// Token: 0x040000A7 RID: 167
	public bool autoDetect = true;

	// Token: 0x040000A8 RID: 168
	public ControllerSpriteManager.ControllerSpriteType currentSpriteSet;

	// Token: 0x040000A9 RID: 169
	public UDictionary<ControllerSpriteManager.ControllerSpriteType, ControllerSpriteManager.ControllerSpriteSet> spriteSets;

	// Token: 0x040000AA RID: 170
	private InputActions inputActions;

	// Token: 0x020000C8 RID: 200
	public enum ControllerSpriteType
	{
		// Token: 0x04000403 RID: 1027
		Xbox,
		// Token: 0x04000404 RID: 1028
		Switch,
		// Token: 0x04000405 RID: 1029
		PS
	}

	// Token: 0x020000C9 RID: 201
	[Serializable]
	public struct ControllerSpriteSet
	{
		// Token: 0x04000406 RID: 1030
		public Sprite confirm;

		// Token: 0x04000407 RID: 1031
		public Sprite cancel;

		// Token: 0x04000408 RID: 1032
		public Sprite viewDecks;

		// Token: 0x04000409 RID: 1033
		public Sprite alt;
	}

	// Token: 0x020000CA RID: 202
	// (Invoke) Token: 0x060004EC RID: 1260
	public delegate void OnSpriteSetChanged();
}
