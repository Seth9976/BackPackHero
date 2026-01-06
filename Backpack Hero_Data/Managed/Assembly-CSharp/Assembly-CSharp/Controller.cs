using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

// Token: 0x0200005F RID: 95
public class Controller : IInputActionCollection2, IInputActionCollection, IEnumerable<InputAction>, IEnumerable, IDisposable
{
	// Token: 0x17000001 RID: 1
	// (get) Token: 0x060001A8 RID: 424 RVA: 0x0000AB12 File Offset: 0x00008D12
	public InputActionAsset asset { get; }

	// Token: 0x060001A9 RID: 425 RVA: 0x0000AB1C File Offset: 0x00008D1C
	public Controller()
	{
		this.asset = InputActionAsset.FromJson("{\n    \"name\": \"Controller\",\n    \"maps\": [\n        {\n            \"name\": \"Gameplay\",\n            \"id\": \"fb6e3d88-0dd0-43f8-bf5b-cfffb362a05c\",\n            \"actions\": [\n                {\n                    \"name\": \"Confirm\",\n                    \"type\": \"Button\",\n                    \"id\": \"b0b37e45-bc37-4082-b560-74737c1bf730\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"Cancel\",\n                    \"type\": \"Button\",\n                    \"id\": \"a06ccc29-c227-4016-97f3-969cc8f5af0c\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"Move Cursor Free\",\n                    \"type\": \"Value\",\n                    \"id\": \"598a85e7-9b49-4ce0-a0cf-7aac46d4e5c9\",\n                    \"expectedControlType\": \"\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": true\n                },\n                {\n                    \"name\": \"Move Cursor Lock\",\n                    \"type\": \"Value\",\n                    \"id\": \"20a5b946-ef74-427e-92aa-e913359d0bae\",\n                    \"expectedControlType\": \"\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": true\n                },\n                {\n                    \"name\": \"RotateRight\",\n                    \"type\": \"Button\",\n                    \"id\": \"18aaafda-c608-4e65-93d7-dd9f362ac365\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"RotateLeft\",\n                    \"type\": \"Button\",\n                    \"id\": \"04926a2b-9b73-44d0-96b3-d6ca0e9becc6\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"Select\",\n                    \"type\": \"Button\",\n                    \"id\": \"faac7b5f-32f6-4288-8244-d529260dbbe1\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"ContextMenu\",\n                    \"type\": \"Button\",\n                    \"id\": \"600c45d9-20ea-44a0-a629-71bd72fae93c\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"Pause Menu\",\n                    \"type\": \"Button\",\n                    \"id\": \"63f4bc92-8611-425c-8bd9-00317e16e810\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"ContextualAction\",\n                    \"type\": \"Button\",\n                    \"id\": \"898970ff-993e-4833-b64b-6e5d7aa7d562\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"ActionButtons\",\n                    \"type\": \"Button\",\n                    \"id\": \"237ae8d9-a038-4308-bff9-3d849ce39487\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"CenterOnBackpack\",\n                    \"type\": \"Button\",\n                    \"id\": \"be0a67b6-5218-4e36-9208-ccba3a400650\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"leftBumper\",\n                    \"type\": \"Button\",\n                    \"id\": \"7455504b-ea4a-4a70-ad80-01d5b1424ac1\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"rightBumper\",\n                    \"type\": \"Button\",\n                    \"id\": \"e18ad1d5-ffef-4372-8825-ec1cdabf58b6\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                }\n            ],\n            \"bindings\": [\n                {\n                    \"name\": \"\",\n                    \"id\": \"c0b9f6f9-0e66-4a84-b1b1-79532d27b532\",\n                    \"path\": \"<Gamepad>/rightStick\",\n                    \"interactions\": \"\",\n                    \"processors\": \"StickDeadzone\",\n                    \"groups\": \"\",\n                    \"action\": \"Move Cursor Free\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"18626a74-8d47-4592-a8e1-39c0c76a1f79\",\n                    \"path\": \"<Gamepad>/leftStick\",\n                    \"interactions\": \"\",\n                    \"processors\": \"StickDeadzone\",\n                    \"groups\": \"\",\n                    \"action\": \"Move Cursor Lock\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"2D Vector\",\n                    \"id\": \"d2696d7e-7098-47e2-a6b2-030fa7ce6911\",\n                    \"path\": \"2DVector\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Move Cursor Lock\",\n                    \"isComposite\": true,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"up\",\n                    \"id\": \"cc383d79-0c35-4882-aa2e-f482acdb7694\",\n                    \"path\": \"<Gamepad>/dpad/up\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Move Cursor Lock\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"down\",\n                    \"id\": \"747b2495-ceaa-4751-8ec5-4593e64b4635\",\n                    \"path\": \"<Gamepad>/dpad/down\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Move Cursor Lock\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"left\",\n                    \"id\": \"fe31f7c9-c22c-4fec-a081-82fac39a3d21\",\n                    \"path\": \"<Gamepad>/dpad/left\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Move Cursor Lock\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"right\",\n                    \"id\": \"2987d29d-876d-49fe-a461-31a6256bc9c3\",\n                    \"path\": \"<Gamepad>/dpad/right\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Move Cursor Lock\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"7cb75f5a-da74-482d-9c84-93e341643a63\",\n                    \"path\": \"<Gamepad>/rightTrigger\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"RotateRight\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"828da108-44d2-49cb-a5d3-871b25095219\",\n                    \"path\": \"<Gamepad>/select\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Select\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"3759800a-8091-4f8a-8aed-b17557f22737\",\n                    \"path\": \"<DualShockGamepad>/touchpadButton\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Select\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"cc4e06ec-aee4-4ff5-b802-dbe61dffb4b4\",\n                    \"path\": \"<Gamepad>/buttonWest\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"ContextMenu\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"199d0d0e-5eb6-4399-9629-f514065b7d7e\",\n                    \"path\": \"<Gamepad>/start\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Pause Menu\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"9fd7fa32-baf5-46de-abbe-dd897d322e7d\",\n                    \"path\": \"<Gamepad>/buttonNorth\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"ContextualAction\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"34a659f8-2aeb-4eeb-af10-57e1e6897827\",\n                    \"path\": \"<Gamepad>/leftTrigger\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"RotateLeft\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"3d059c10-5a35-4ca4-aa39-801d7feba80f\",\n                    \"path\": \"<Gamepad>/leftStickPress\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"ActionButtons\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"2a898fbf-f78f-44c1-8fbc-bc00baba8ba3\",\n                    \"path\": \"<Gamepad>/rightStickPress\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"CenterOnBackpack\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"9c0f8e06-70ff-4d8e-adc3-ab607d416448\",\n                    \"path\": \"*/{Submit}\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Confirm\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"bda68ea8-713f-49f5-8053-4f1b7f736fa6\",\n                    \"path\": \"*/{Cancel}\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Cancel\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"9904c1c9-648c-4615-9c17-e606fc6de4d4\",\n                    \"path\": \"<Gamepad>/leftShoulder\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"leftBumper\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"65cc2191-070a-4839-99ca-a88179cd25e6\",\n                    \"path\": \"<Gamepad>/rightShoulder\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"rightBumper\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                }\n            ]\n        }\n    ],\n    \"controlSchemes\": []\n}");
		this.m_Gameplay = this.asset.FindActionMap("Gameplay", true);
		this.m_Gameplay_Confirm = this.m_Gameplay.FindAction("Confirm", true);
		this.m_Gameplay_Cancel = this.m_Gameplay.FindAction("Cancel", true);
		this.m_Gameplay_MoveCursorFree = this.m_Gameplay.FindAction("Move Cursor Free", true);
		this.m_Gameplay_MoveCursorLock = this.m_Gameplay.FindAction("Move Cursor Lock", true);
		this.m_Gameplay_RotateRight = this.m_Gameplay.FindAction("RotateRight", true);
		this.m_Gameplay_RotateLeft = this.m_Gameplay.FindAction("RotateLeft", true);
		this.m_Gameplay_Select = this.m_Gameplay.FindAction("Select", true);
		this.m_Gameplay_ContextMenu = this.m_Gameplay.FindAction("ContextMenu", true);
		this.m_Gameplay_PauseMenu = this.m_Gameplay.FindAction("Pause Menu", true);
		this.m_Gameplay_ContextualAction = this.m_Gameplay.FindAction("ContextualAction", true);
		this.m_Gameplay_ActionButtons = this.m_Gameplay.FindAction("ActionButtons", true);
		this.m_Gameplay_CenterOnBackpack = this.m_Gameplay.FindAction("CenterOnBackpack", true);
		this.m_Gameplay_leftBumper = this.m_Gameplay.FindAction("leftBumper", true);
		this.m_Gameplay_rightBumper = this.m_Gameplay.FindAction("rightBumper", true);
	}

	// Token: 0x060001AA RID: 426 RVA: 0x0000ACA3 File Offset: 0x00008EA3
	public void Dispose()
	{
		Object.Destroy(this.asset);
	}

	// Token: 0x17000002 RID: 2
	// (get) Token: 0x060001AB RID: 427 RVA: 0x0000ACB0 File Offset: 0x00008EB0
	// (set) Token: 0x060001AC RID: 428 RVA: 0x0000ACBD File Offset: 0x00008EBD
	public InputBinding? bindingMask
	{
		get
		{
			return this.asset.bindingMask;
		}
		set
		{
			this.asset.bindingMask = value;
		}
	}

	// Token: 0x17000003 RID: 3
	// (get) Token: 0x060001AD RID: 429 RVA: 0x0000ACCB File Offset: 0x00008ECB
	// (set) Token: 0x060001AE RID: 430 RVA: 0x0000ACD8 File Offset: 0x00008ED8
	public ReadOnlyArray<InputDevice>? devices
	{
		get
		{
			return this.asset.devices;
		}
		set
		{
			this.asset.devices = value;
		}
	}

	// Token: 0x17000004 RID: 4
	// (get) Token: 0x060001AF RID: 431 RVA: 0x0000ACE6 File Offset: 0x00008EE6
	public ReadOnlyArray<InputControlScheme> controlSchemes
	{
		get
		{
			return this.asset.controlSchemes;
		}
	}

	// Token: 0x060001B0 RID: 432 RVA: 0x0000ACF3 File Offset: 0x00008EF3
	public bool Contains(InputAction action)
	{
		return this.asset.Contains(action);
	}

	// Token: 0x060001B1 RID: 433 RVA: 0x0000AD01 File Offset: 0x00008F01
	public IEnumerator<InputAction> GetEnumerator()
	{
		return this.asset.GetEnumerator();
	}

	// Token: 0x060001B2 RID: 434 RVA: 0x0000AD0E File Offset: 0x00008F0E
	IEnumerator IEnumerable.GetEnumerator()
	{
		return this.GetEnumerator();
	}

	// Token: 0x060001B3 RID: 435 RVA: 0x0000AD16 File Offset: 0x00008F16
	public void Enable()
	{
		this.asset.Enable();
	}

	// Token: 0x060001B4 RID: 436 RVA: 0x0000AD23 File Offset: 0x00008F23
	public void Disable()
	{
		this.asset.Disable();
	}

	// Token: 0x17000005 RID: 5
	// (get) Token: 0x060001B5 RID: 437 RVA: 0x0000AD30 File Offset: 0x00008F30
	public IEnumerable<InputBinding> bindings
	{
		get
		{
			return this.asset.bindings;
		}
	}

	// Token: 0x060001B6 RID: 438 RVA: 0x0000AD3D File Offset: 0x00008F3D
	public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
	{
		return this.asset.FindAction(actionNameOrId, throwIfNotFound);
	}

	// Token: 0x060001B7 RID: 439 RVA: 0x0000AD4C File Offset: 0x00008F4C
	public int FindBinding(InputBinding bindingMask, out InputAction action)
	{
		return this.asset.FindBinding(bindingMask, out action);
	}

	// Token: 0x17000006 RID: 6
	// (get) Token: 0x060001B8 RID: 440 RVA: 0x0000AD5B File Offset: 0x00008F5B
	public Controller.GameplayActions Gameplay
	{
		get
		{
			return new Controller.GameplayActions(this);
		}
	}

	// Token: 0x0400011D RID: 285
	private readonly InputActionMap m_Gameplay;

	// Token: 0x0400011E RID: 286
	private List<Controller.IGameplayActions> m_GameplayActionsCallbackInterfaces = new List<Controller.IGameplayActions>();

	// Token: 0x0400011F RID: 287
	private readonly InputAction m_Gameplay_Confirm;

	// Token: 0x04000120 RID: 288
	private readonly InputAction m_Gameplay_Cancel;

	// Token: 0x04000121 RID: 289
	private readonly InputAction m_Gameplay_MoveCursorFree;

	// Token: 0x04000122 RID: 290
	private readonly InputAction m_Gameplay_MoveCursorLock;

	// Token: 0x04000123 RID: 291
	private readonly InputAction m_Gameplay_RotateRight;

	// Token: 0x04000124 RID: 292
	private readonly InputAction m_Gameplay_RotateLeft;

	// Token: 0x04000125 RID: 293
	private readonly InputAction m_Gameplay_Select;

	// Token: 0x04000126 RID: 294
	private readonly InputAction m_Gameplay_ContextMenu;

	// Token: 0x04000127 RID: 295
	private readonly InputAction m_Gameplay_PauseMenu;

	// Token: 0x04000128 RID: 296
	private readonly InputAction m_Gameplay_ContextualAction;

	// Token: 0x04000129 RID: 297
	private readonly InputAction m_Gameplay_ActionButtons;

	// Token: 0x0400012A RID: 298
	private readonly InputAction m_Gameplay_CenterOnBackpack;

	// Token: 0x0400012B RID: 299
	private readonly InputAction m_Gameplay_leftBumper;

	// Token: 0x0400012C RID: 300
	private readonly InputAction m_Gameplay_rightBumper;

	// Token: 0x0200026F RID: 623
	public struct GameplayActions
	{
		// Token: 0x0600132F RID: 4911 RVA: 0x000AF3A4 File Offset: 0x000AD5A4
		public GameplayActions(Controller wrapper)
		{
			this.m_Wrapper = wrapper;
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06001330 RID: 4912 RVA: 0x000AF3AD File Offset: 0x000AD5AD
		public InputAction Confirm
		{
			get
			{
				return this.m_Wrapper.m_Gameplay_Confirm;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06001331 RID: 4913 RVA: 0x000AF3BA File Offset: 0x000AD5BA
		public InputAction Cancel
		{
			get
			{
				return this.m_Wrapper.m_Gameplay_Cancel;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06001332 RID: 4914 RVA: 0x000AF3C7 File Offset: 0x000AD5C7
		public InputAction MoveCursorFree
		{
			get
			{
				return this.m_Wrapper.m_Gameplay_MoveCursorFree;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06001333 RID: 4915 RVA: 0x000AF3D4 File Offset: 0x000AD5D4
		public InputAction MoveCursorLock
		{
			get
			{
				return this.m_Wrapper.m_Gameplay_MoveCursorLock;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06001334 RID: 4916 RVA: 0x000AF3E1 File Offset: 0x000AD5E1
		public InputAction RotateRight
		{
			get
			{
				return this.m_Wrapper.m_Gameplay_RotateRight;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06001335 RID: 4917 RVA: 0x000AF3EE File Offset: 0x000AD5EE
		public InputAction RotateLeft
		{
			get
			{
				return this.m_Wrapper.m_Gameplay_RotateLeft;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06001336 RID: 4918 RVA: 0x000AF3FB File Offset: 0x000AD5FB
		public InputAction Select
		{
			get
			{
				return this.m_Wrapper.m_Gameplay_Select;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06001337 RID: 4919 RVA: 0x000AF408 File Offset: 0x000AD608
		public InputAction ContextMenu
		{
			get
			{
				return this.m_Wrapper.m_Gameplay_ContextMenu;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06001338 RID: 4920 RVA: 0x000AF415 File Offset: 0x000AD615
		public InputAction PauseMenu
		{
			get
			{
				return this.m_Wrapper.m_Gameplay_PauseMenu;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06001339 RID: 4921 RVA: 0x000AF422 File Offset: 0x000AD622
		public InputAction ContextualAction
		{
			get
			{
				return this.m_Wrapper.m_Gameplay_ContextualAction;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600133A RID: 4922 RVA: 0x000AF42F File Offset: 0x000AD62F
		public InputAction ActionButtons
		{
			get
			{
				return this.m_Wrapper.m_Gameplay_ActionButtons;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600133B RID: 4923 RVA: 0x000AF43C File Offset: 0x000AD63C
		public InputAction CenterOnBackpack
		{
			get
			{
				return this.m_Wrapper.m_Gameplay_CenterOnBackpack;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600133C RID: 4924 RVA: 0x000AF449 File Offset: 0x000AD649
		public InputAction leftBumper
		{
			get
			{
				return this.m_Wrapper.m_Gameplay_leftBumper;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600133D RID: 4925 RVA: 0x000AF456 File Offset: 0x000AD656
		public InputAction rightBumper
		{
			get
			{
				return this.m_Wrapper.m_Gameplay_rightBumper;
			}
		}

		// Token: 0x0600133E RID: 4926 RVA: 0x000AF463 File Offset: 0x000AD663
		public InputActionMap Get()
		{
			return this.m_Wrapper.m_Gameplay;
		}

		// Token: 0x0600133F RID: 4927 RVA: 0x000AF470 File Offset: 0x000AD670
		public void Enable()
		{
			this.Get().Enable();
		}

		// Token: 0x06001340 RID: 4928 RVA: 0x000AF47D File Offset: 0x000AD67D
		public void Disable()
		{
			this.Get().Disable();
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06001341 RID: 4929 RVA: 0x000AF48A File Offset: 0x000AD68A
		public bool enabled
		{
			get
			{
				return this.Get().enabled;
			}
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x000AF497 File Offset: 0x000AD697
		public static implicit operator InputActionMap(Controller.GameplayActions set)
		{
			return set.Get();
		}

		// Token: 0x06001343 RID: 4931 RVA: 0x000AF4A0 File Offset: 0x000AD6A0
		public void AddCallbacks(Controller.IGameplayActions instance)
		{
			if (instance == null || this.m_Wrapper.m_GameplayActionsCallbackInterfaces.Contains(instance))
			{
				return;
			}
			this.m_Wrapper.m_GameplayActionsCallbackInterfaces.Add(instance);
			this.Confirm.started += instance.OnConfirm;
			this.Confirm.performed += instance.OnConfirm;
			this.Confirm.canceled += instance.OnConfirm;
			this.Cancel.started += instance.OnCancel;
			this.Cancel.performed += instance.OnCancel;
			this.Cancel.canceled += instance.OnCancel;
			this.MoveCursorFree.started += instance.OnMoveCursorFree;
			this.MoveCursorFree.performed += instance.OnMoveCursorFree;
			this.MoveCursorFree.canceled += instance.OnMoveCursorFree;
			this.MoveCursorLock.started += instance.OnMoveCursorLock;
			this.MoveCursorLock.performed += instance.OnMoveCursorLock;
			this.MoveCursorLock.canceled += instance.OnMoveCursorLock;
			this.RotateRight.started += instance.OnRotateRight;
			this.RotateRight.performed += instance.OnRotateRight;
			this.RotateRight.canceled += instance.OnRotateRight;
			this.RotateLeft.started += instance.OnRotateLeft;
			this.RotateLeft.performed += instance.OnRotateLeft;
			this.RotateLeft.canceled += instance.OnRotateLeft;
			this.Select.started += instance.OnSelect;
			this.Select.performed += instance.OnSelect;
			this.Select.canceled += instance.OnSelect;
			this.ContextMenu.started += instance.OnContextMenu;
			this.ContextMenu.performed += instance.OnContextMenu;
			this.ContextMenu.canceled += instance.OnContextMenu;
			this.PauseMenu.started += instance.OnPauseMenu;
			this.PauseMenu.performed += instance.OnPauseMenu;
			this.PauseMenu.canceled += instance.OnPauseMenu;
			this.ContextualAction.started += instance.OnContextualAction;
			this.ContextualAction.performed += instance.OnContextualAction;
			this.ContextualAction.canceled += instance.OnContextualAction;
			this.ActionButtons.started += instance.OnActionButtons;
			this.ActionButtons.performed += instance.OnActionButtons;
			this.ActionButtons.canceled += instance.OnActionButtons;
			this.CenterOnBackpack.started += instance.OnCenterOnBackpack;
			this.CenterOnBackpack.performed += instance.OnCenterOnBackpack;
			this.CenterOnBackpack.canceled += instance.OnCenterOnBackpack;
			this.leftBumper.started += instance.OnLeftBumper;
			this.leftBumper.performed += instance.OnLeftBumper;
			this.leftBumper.canceled += instance.OnLeftBumper;
			this.rightBumper.started += instance.OnRightBumper;
			this.rightBumper.performed += instance.OnRightBumper;
			this.rightBumper.canceled += instance.OnRightBumper;
		}

		// Token: 0x06001344 RID: 4932 RVA: 0x000AF8C8 File Offset: 0x000ADAC8
		private void UnregisterCallbacks(Controller.IGameplayActions instance)
		{
			this.Confirm.started -= instance.OnConfirm;
			this.Confirm.performed -= instance.OnConfirm;
			this.Confirm.canceled -= instance.OnConfirm;
			this.Cancel.started -= instance.OnCancel;
			this.Cancel.performed -= instance.OnCancel;
			this.Cancel.canceled -= instance.OnCancel;
			this.MoveCursorFree.started -= instance.OnMoveCursorFree;
			this.MoveCursorFree.performed -= instance.OnMoveCursorFree;
			this.MoveCursorFree.canceled -= instance.OnMoveCursorFree;
			this.MoveCursorLock.started -= instance.OnMoveCursorLock;
			this.MoveCursorLock.performed -= instance.OnMoveCursorLock;
			this.MoveCursorLock.canceled -= instance.OnMoveCursorLock;
			this.RotateRight.started -= instance.OnRotateRight;
			this.RotateRight.performed -= instance.OnRotateRight;
			this.RotateRight.canceled -= instance.OnRotateRight;
			this.RotateLeft.started -= instance.OnRotateLeft;
			this.RotateLeft.performed -= instance.OnRotateLeft;
			this.RotateLeft.canceled -= instance.OnRotateLeft;
			this.Select.started -= instance.OnSelect;
			this.Select.performed -= instance.OnSelect;
			this.Select.canceled -= instance.OnSelect;
			this.ContextMenu.started -= instance.OnContextMenu;
			this.ContextMenu.performed -= instance.OnContextMenu;
			this.ContextMenu.canceled -= instance.OnContextMenu;
			this.PauseMenu.started -= instance.OnPauseMenu;
			this.PauseMenu.performed -= instance.OnPauseMenu;
			this.PauseMenu.canceled -= instance.OnPauseMenu;
			this.ContextualAction.started -= instance.OnContextualAction;
			this.ContextualAction.performed -= instance.OnContextualAction;
			this.ContextualAction.canceled -= instance.OnContextualAction;
			this.ActionButtons.started -= instance.OnActionButtons;
			this.ActionButtons.performed -= instance.OnActionButtons;
			this.ActionButtons.canceled -= instance.OnActionButtons;
			this.CenterOnBackpack.started -= instance.OnCenterOnBackpack;
			this.CenterOnBackpack.performed -= instance.OnCenterOnBackpack;
			this.CenterOnBackpack.canceled -= instance.OnCenterOnBackpack;
			this.leftBumper.started -= instance.OnLeftBumper;
			this.leftBumper.performed -= instance.OnLeftBumper;
			this.leftBumper.canceled -= instance.OnLeftBumper;
			this.rightBumper.started -= instance.OnRightBumper;
			this.rightBumper.performed -= instance.OnRightBumper;
			this.rightBumper.canceled -= instance.OnRightBumper;
		}

		// Token: 0x06001345 RID: 4933 RVA: 0x000AFCC5 File Offset: 0x000ADEC5
		public void RemoveCallbacks(Controller.IGameplayActions instance)
		{
			if (this.m_Wrapper.m_GameplayActionsCallbackInterfaces.Remove(instance))
			{
				this.UnregisterCallbacks(instance);
			}
		}

		// Token: 0x06001346 RID: 4934 RVA: 0x000AFCE4 File Offset: 0x000ADEE4
		public void SetCallbacks(Controller.IGameplayActions instance)
		{
			foreach (Controller.IGameplayActions gameplayActions in this.m_Wrapper.m_GameplayActionsCallbackInterfaces)
			{
				this.UnregisterCallbacks(gameplayActions);
			}
			this.m_Wrapper.m_GameplayActionsCallbackInterfaces.Clear();
			this.AddCallbacks(instance);
		}

		// Token: 0x04000F2C RID: 3884
		private Controller m_Wrapper;
	}

	// Token: 0x02000270 RID: 624
	public interface IGameplayActions
	{
		// Token: 0x06001347 RID: 4935
		void OnConfirm(InputAction.CallbackContext context);

		// Token: 0x06001348 RID: 4936
		void OnCancel(InputAction.CallbackContext context);

		// Token: 0x06001349 RID: 4937
		void OnMoveCursorFree(InputAction.CallbackContext context);

		// Token: 0x0600134A RID: 4938
		void OnMoveCursorLock(InputAction.CallbackContext context);

		// Token: 0x0600134B RID: 4939
		void OnRotateRight(InputAction.CallbackContext context);

		// Token: 0x0600134C RID: 4940
		void OnRotateLeft(InputAction.CallbackContext context);

		// Token: 0x0600134D RID: 4941
		void OnSelect(InputAction.CallbackContext context);

		// Token: 0x0600134E RID: 4942
		void OnContextMenu(InputAction.CallbackContext context);

		// Token: 0x0600134F RID: 4943
		void OnPauseMenu(InputAction.CallbackContext context);

		// Token: 0x06001350 RID: 4944
		void OnContextualAction(InputAction.CallbackContext context);

		// Token: 0x06001351 RID: 4945
		void OnActionButtons(InputAction.CallbackContext context);

		// Token: 0x06001352 RID: 4946
		void OnCenterOnBackpack(InputAction.CallbackContext context);

		// Token: 0x06001353 RID: 4947
		void OnLeftBumper(InputAction.CallbackContext context);

		// Token: 0x06001354 RID: 4948
		void OnRightBumper(InputAction.CallbackContext context);
	}
}
