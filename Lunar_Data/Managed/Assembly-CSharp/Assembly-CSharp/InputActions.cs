using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

// Token: 0x0200001F RID: 31
public class InputActions : IInputActionCollection2, IInputActionCollection, IEnumerable<InputAction>, IEnumerable, IDisposable
{
	// Token: 0x17000001 RID: 1
	// (get) Token: 0x060000EE RID: 238 RVA: 0x00006424 File Offset: 0x00004624
	public InputActionAsset asset { get; }

	// Token: 0x060000EF RID: 239 RVA: 0x0000642C File Offset: 0x0000462C
	public InputActions()
	{
		this.asset = InputActionAsset.FromJson("{\n    \"name\": \"InputActions\",\n    \"maps\": [\n        {\n            \"name\": \"Default\",\n            \"id\": \"fdcd371a-b902-4413-8ac9-be265d549d24\",\n            \"actions\": [\n                {\n                    \"name\": \"Confirm\",\n                    \"type\": \"Button\",\n                    \"id\": \"ba578a84-a1fc-41a4-ad91-2c6b27a2b031\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"Cancel\",\n                    \"type\": \"Button\",\n                    \"id\": \"d2b2b09d-18a6-497c-92dd-222abe6939fa\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"Movement\",\n                    \"type\": \"Value\",\n                    \"id\": \"e0205518-d144-49c0-bfb9-c0b05dd6b28e\",\n                    \"expectedControlType\": \"Analog\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": true\n                },\n                {\n                    \"name\": \"Pause\",\n                    \"type\": \"Button\",\n                    \"id\": \"c825848f-1af1-4069-8562-62b89ffef064\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"ViewDecks\",\n                    \"type\": \"Button\",\n                    \"id\": \"e30eb283-ced2-406e-99e3-b14bc6656c34\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"YButton\",\n                    \"type\": \"Button\",\n                    \"id\": \"2c78c1ba-5b58-43bc-a554-9cdcd819b524\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                }\n            ],\n            \"bindings\": [\n                {\n                    \"name\": \"\",\n                    \"id\": \"566d38bf-82e0-4ad1-8a3e-6321ec6f7e12\",\n                    \"path\": \"<Gamepad>/buttonSouth\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Controller\",\n                    \"action\": \"Confirm\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"9cf78304-d3fd-44f7-be2f-19e79bdb2353\",\n                    \"path\": \"<Keyboard>/x\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Controller;Switch\",\n                    \"action\": \"Confirm\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"2b4e373c-93ac-409d-8757-74bbabdf2506\",\n                    \"path\": \"<Keyboard>/space\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Confirm\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"029d126f-e738-4483-b74f-ddb2cf27fefe\",\n                    \"path\": \"<Keyboard>/enter\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Confirm\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"fb14c8f6-6d6a-48b1-a213-1378311a6450\",\n                    \"path\": \"<Gamepad>/leftStick\",\n                    \"interactions\": \"\",\n                    \"processors\": \"AxisDeadzone\",\n                    \"groups\": \"Controller;Switch\",\n                    \"action\": \"Movement\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"2D Vector\",\n                    \"id\": \"d99617b2-85d5-44db-a713-6a2d93be0af7\",\n                    \"path\": \"2DVector\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Movement\",\n                    \"isComposite\": true,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"up\",\n                    \"id\": \"61c215b5-d51c-4aa4-94ed-6749c7c33392\",\n                    \"path\": \"<Keyboard>/upArrow\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Controller;Switch\",\n                    \"action\": \"Movement\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"down\",\n                    \"id\": \"5a912ccb-114f-4186-8a12-9b594c0c2db9\",\n                    \"path\": \"<Keyboard>/downArrow\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Controller;Switch\",\n                    \"action\": \"Movement\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"left\",\n                    \"id\": \"58ad39cf-5606-489c-9f15-5c4c4162fd93\",\n                    \"path\": \"<Keyboard>/leftArrow\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Controller;Switch\",\n                    \"action\": \"Movement\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"right\",\n                    \"id\": \"179a2c80-ab9e-4870-b267-23f0f9c99a8d\",\n                    \"path\": \"<Keyboard>/rightArrow\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Controller;Switch\",\n                    \"action\": \"Movement\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"2D Vector\",\n                    \"id\": \"45b28a2c-b8d4-4c77-888c-abcb9352a090\",\n                    \"path\": \"2DVector\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Movement\",\n                    \"isComposite\": true,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"up\",\n                    \"id\": \"95d367b8-e045-4c0f-bd4f-40e4b8fabc8d\",\n                    \"path\": \"<Keyboard>/w\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Controller;Switch\",\n                    \"action\": \"Movement\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"down\",\n                    \"id\": \"7622d906-ddee-4dfd-9685-889138de89de\",\n                    \"path\": \"<Keyboard>/s\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Controller;Switch\",\n                    \"action\": \"Movement\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"left\",\n                    \"id\": \"5f9d6d18-bba2-4744-9841-dc40b59236a0\",\n                    \"path\": \"<Keyboard>/a\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Controller;Switch\",\n                    \"action\": \"Movement\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"right\",\n                    \"id\": \"e2076e04-f8d9-4016-aca0-c7dee58e2839\",\n                    \"path\": \"<Keyboard>/d\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Controller;Switch\",\n                    \"action\": \"Movement\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"5e8dc1d0-3fb8-4aef-a289-feb068ec0b5b\",\n                    \"path\": \"<Keyboard>/c\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Controller;Switch\",\n                    \"action\": \"Cancel\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"571e35fe-2efc-473b-a2bc-599014c00029\",\n                    \"path\": \"<Keyboard>/z\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Controller;Switch\",\n                    \"action\": \"Cancel\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"d6ba547c-7701-47a5-a3a8-99f26eaf6137\",\n                    \"path\": \"<Keyboard>/slash\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Controller;Switch\",\n                    \"action\": \"Cancel\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"be8c79a8-6d50-49e3-85a4-9bf917ba90d3\",\n                    \"path\": \"<Gamepad>/buttonEast\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Controller\",\n                    \"action\": \"Cancel\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"a06e0bc2-bde4-44ce-b0ed-031cf997f1cf\",\n                    \"path\": \"<Keyboard>/escape\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Pause\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"8aa2a550-fbe1-412e-9439-54f0f13ce7c2\",\n                    \"path\": \"<Gamepad>/start\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Pause\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"6ea7986b-e1fc-46ba-b351-b829d35a3570\",\n                    \"path\": \"<Keyboard>/tab\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"ViewDecks\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"9f34d9bb-9149-44e1-a27f-e74c3f427ac7\",\n                    \"path\": \"<Gamepad>/leftShoulder\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"ViewDecks\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"1d8b089e-2903-455b-ba70-f92423bc7dbc\",\n                    \"path\": \"<Gamepad>/buttonNorth\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Controller;Switch\",\n                    \"action\": \"YButton\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                }\n            ]\n        },\n        {\n            \"name\": \"Switch\",\n            \"id\": \"94a4198a-9955-4e98-8000-eefd04190ccd\",\n            \"actions\": [\n                {\n                    \"name\": \"Confirm\",\n                    \"type\": \"Button\",\n                    \"id\": \"3782425d-4742-4547-90a8-7027b73325c7\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"Cancel\",\n                    \"type\": \"Button\",\n                    \"id\": \"e234d260-9293-449c-8313-694d53744416\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"Movement\",\n                    \"type\": \"Value\",\n                    \"id\": \"9218d25b-bb11-4bb8-81ff-716eb7f35713\",\n                    \"expectedControlType\": \"Analog\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": true\n                },\n                {\n                    \"name\": \"Pause\",\n                    \"type\": \"Button\",\n                    \"id\": \"06d9f7fb-c4f6-4110-8c9b-9205cc53369f\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"ViewDecks\",\n                    \"type\": \"Button\",\n                    \"id\": \"772fc572-dd24-4754-96b9-82d0ba4e70c8\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"YButton\",\n                    \"type\": \"Button\",\n                    \"id\": \"806b8106-3a3d-490d-9717-0ae97e9c4bb9\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                }\n            ],\n            \"bindings\": [\n                {\n                    \"name\": \"\",\n                    \"id\": \"704c62e7-cbcb-44db-b5d9-33a00d26e481\",\n                    \"path\": \"<Gamepad>/buttonEast\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Switch\",\n                    \"action\": \"Confirm\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"90fd1151-a0d0-4e08-b091-bd1ad4411c6a\",\n                    \"path\": \"<Keyboard>/x\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Controller;Switch\",\n                    \"action\": \"Confirm\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"7d07f97c-6ad4-4288-8049-dd7e7e6c707f\",\n                    \"path\": \"<Gamepad>/leftStick\",\n                    \"interactions\": \"\",\n                    \"processors\": \"AxisDeadzone\",\n                    \"groups\": \"Controller;Switch\",\n                    \"action\": \"Movement\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"2D Vector\",\n                    \"id\": \"86730b12-2e8b-45ce-a33a-4bd205e33632\",\n                    \"path\": \"2DVector\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Movement\",\n                    \"isComposite\": true,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"up\",\n                    \"id\": \"0fd43cdb-7f46-4985-8147-12390bc982ae\",\n                    \"path\": \"<Keyboard>/upArrow\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Controller;Switch\",\n                    \"action\": \"Movement\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"down\",\n                    \"id\": \"4ce8aa16-0e75-4cd9-a47d-896a2eb3b849\",\n                    \"path\": \"<Keyboard>/downArrow\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Controller;Switch\",\n                    \"action\": \"Movement\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"left\",\n                    \"id\": \"5c1679f3-b20b-4e91-91f0-5fc6c33de6d4\",\n                    \"path\": \"<Keyboard>/leftArrow\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Controller;Switch\",\n                    \"action\": \"Movement\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"right\",\n                    \"id\": \"d21ac28d-912d-4b64-b0f9-395505200355\",\n                    \"path\": \"<Keyboard>/rightArrow\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Controller;Switch\",\n                    \"action\": \"Movement\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"2D Vector\",\n                    \"id\": \"937b67ff-5951-4cf3-bdac-f986085cfd0a\",\n                    \"path\": \"2DVector\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Movement\",\n                    \"isComposite\": true,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"up\",\n                    \"id\": \"8136bfd8-47b7-4842-9014-811edb7c8644\",\n                    \"path\": \"<Keyboard>/w\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Controller;Switch\",\n                    \"action\": \"Movement\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"down\",\n                    \"id\": \"ad7e63be-7b92-4702-aafe-65b6b2e112cd\",\n                    \"path\": \"<Keyboard>/s\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Controller;Switch\",\n                    \"action\": \"Movement\",\n                    \"isComposite\": false,\n        [...string is too long...]");
		this.m_Default = this.asset.FindActionMap("Default", true);
		this.m_Default_Confirm = this.m_Default.FindAction("Confirm", true);
		this.m_Default_Cancel = this.m_Default.FindAction("Cancel", true);
		this.m_Default_Movement = this.m_Default.FindAction("Movement", true);
		this.m_Default_Pause = this.m_Default.FindAction("Pause", true);
		this.m_Default_ViewDecks = this.m_Default.FindAction("ViewDecks", true);
		this.m_Default_YButton = this.m_Default.FindAction("YButton", true);
		this.m_Switch = this.asset.FindActionMap("Switch", true);
		this.m_Switch_Confirm = this.m_Switch.FindAction("Confirm", true);
		this.m_Switch_Cancel = this.m_Switch.FindAction("Cancel", true);
		this.m_Switch_Movement = this.m_Switch.FindAction("Movement", true);
		this.m_Switch_Pause = this.m_Switch.FindAction("Pause", true);
		this.m_Switch_ViewDecks = this.m_Switch.FindAction("ViewDecks", true);
		this.m_Switch_YButton = this.m_Switch.FindAction("YButton", true);
	}

	// Token: 0x060000F0 RID: 240 RVA: 0x000065B5 File Offset: 0x000047B5
	public void Dispose()
	{
		Object.Destroy(this.asset);
	}

	// Token: 0x17000002 RID: 2
	// (get) Token: 0x060000F1 RID: 241 RVA: 0x000065C2 File Offset: 0x000047C2
	// (set) Token: 0x060000F2 RID: 242 RVA: 0x000065CF File Offset: 0x000047CF
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
	// (get) Token: 0x060000F3 RID: 243 RVA: 0x000065DD File Offset: 0x000047DD
	// (set) Token: 0x060000F4 RID: 244 RVA: 0x000065EA File Offset: 0x000047EA
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
	// (get) Token: 0x060000F5 RID: 245 RVA: 0x000065F8 File Offset: 0x000047F8
	public ReadOnlyArray<InputControlScheme> controlSchemes
	{
		get
		{
			return this.asset.controlSchemes;
		}
	}

	// Token: 0x060000F6 RID: 246 RVA: 0x00006605 File Offset: 0x00004805
	public bool Contains(InputAction action)
	{
		return this.asset.Contains(action);
	}

	// Token: 0x060000F7 RID: 247 RVA: 0x00006613 File Offset: 0x00004813
	public IEnumerator<InputAction> GetEnumerator()
	{
		return this.asset.GetEnumerator();
	}

	// Token: 0x060000F8 RID: 248 RVA: 0x00006620 File Offset: 0x00004820
	IEnumerator IEnumerable.GetEnumerator()
	{
		return this.GetEnumerator();
	}

	// Token: 0x060000F9 RID: 249 RVA: 0x00006628 File Offset: 0x00004828
	public void Enable()
	{
		this.asset.Enable();
	}

	// Token: 0x060000FA RID: 250 RVA: 0x00006635 File Offset: 0x00004835
	public void Disable()
	{
		this.asset.Disable();
	}

	// Token: 0x17000005 RID: 5
	// (get) Token: 0x060000FB RID: 251 RVA: 0x00006642 File Offset: 0x00004842
	public IEnumerable<InputBinding> bindings
	{
		get
		{
			return this.asset.bindings;
		}
	}

	// Token: 0x060000FC RID: 252 RVA: 0x0000664F File Offset: 0x0000484F
	public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
	{
		return this.asset.FindAction(actionNameOrId, throwIfNotFound);
	}

	// Token: 0x060000FD RID: 253 RVA: 0x0000665E File Offset: 0x0000485E
	public int FindBinding(InputBinding bindingMask, out InputAction action)
	{
		return this.asset.FindBinding(bindingMask, out action);
	}

	// Token: 0x17000006 RID: 6
	// (get) Token: 0x060000FE RID: 254 RVA: 0x0000666D File Offset: 0x0000486D
	public InputActions.DefaultActions Default
	{
		get
		{
			return new InputActions.DefaultActions(this);
		}
	}

	// Token: 0x17000007 RID: 7
	// (get) Token: 0x060000FF RID: 255 RVA: 0x00006675 File Offset: 0x00004875
	public InputActions.SwitchActions Switch
	{
		get
		{
			return new InputActions.SwitchActions(this);
		}
	}

	// Token: 0x17000008 RID: 8
	// (get) Token: 0x06000100 RID: 256 RVA: 0x00006680 File Offset: 0x00004880
	public InputControlScheme ControllerScheme
	{
		get
		{
			if (this.m_ControllerSchemeIndex == -1)
			{
				this.m_ControllerSchemeIndex = this.asset.FindControlSchemeIndex("Controller");
			}
			return this.asset.controlSchemes[this.m_ControllerSchemeIndex];
		}
	}

	// Token: 0x17000009 RID: 9
	// (get) Token: 0x06000101 RID: 257 RVA: 0x000066C8 File Offset: 0x000048C8
	public InputControlScheme SwitchScheme
	{
		get
		{
			if (this.m_SwitchSchemeIndex == -1)
			{
				this.m_SwitchSchemeIndex = this.asset.FindControlSchemeIndex("Switch");
			}
			return this.asset.controlSchemes[this.m_SwitchSchemeIndex];
		}
	}

	// Token: 0x040000AD RID: 173
	private readonly InputActionMap m_Default;

	// Token: 0x040000AE RID: 174
	private List<InputActions.IDefaultActions> m_DefaultActionsCallbackInterfaces = new List<InputActions.IDefaultActions>();

	// Token: 0x040000AF RID: 175
	private readonly InputAction m_Default_Confirm;

	// Token: 0x040000B0 RID: 176
	private readonly InputAction m_Default_Cancel;

	// Token: 0x040000B1 RID: 177
	private readonly InputAction m_Default_Movement;

	// Token: 0x040000B2 RID: 178
	private readonly InputAction m_Default_Pause;

	// Token: 0x040000B3 RID: 179
	private readonly InputAction m_Default_ViewDecks;

	// Token: 0x040000B4 RID: 180
	private readonly InputAction m_Default_YButton;

	// Token: 0x040000B5 RID: 181
	private readonly InputActionMap m_Switch;

	// Token: 0x040000B6 RID: 182
	private List<InputActions.ISwitchActions> m_SwitchActionsCallbackInterfaces = new List<InputActions.ISwitchActions>();

	// Token: 0x040000B7 RID: 183
	private readonly InputAction m_Switch_Confirm;

	// Token: 0x040000B8 RID: 184
	private readonly InputAction m_Switch_Cancel;

	// Token: 0x040000B9 RID: 185
	private readonly InputAction m_Switch_Movement;

	// Token: 0x040000BA RID: 186
	private readonly InputAction m_Switch_Pause;

	// Token: 0x040000BB RID: 187
	private readonly InputAction m_Switch_ViewDecks;

	// Token: 0x040000BC RID: 188
	private readonly InputAction m_Switch_YButton;

	// Token: 0x040000BD RID: 189
	private int m_ControllerSchemeIndex = -1;

	// Token: 0x040000BE RID: 190
	private int m_SwitchSchemeIndex = -1;

	// Token: 0x020000CB RID: 203
	public struct DefaultActions
	{
		// Token: 0x060004EF RID: 1263 RVA: 0x0001785B File Offset: 0x00015A5B
		public DefaultActions(InputActions wrapper)
		{
			this.m_Wrapper = wrapper;
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060004F0 RID: 1264 RVA: 0x00017864 File Offset: 0x00015A64
		public InputAction Confirm
		{
			get
			{
				return this.m_Wrapper.m_Default_Confirm;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060004F1 RID: 1265 RVA: 0x00017871 File Offset: 0x00015A71
		public InputAction Cancel
		{
			get
			{
				return this.m_Wrapper.m_Default_Cancel;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060004F2 RID: 1266 RVA: 0x0001787E File Offset: 0x00015A7E
		public InputAction Movement
		{
			get
			{
				return this.m_Wrapper.m_Default_Movement;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060004F3 RID: 1267 RVA: 0x0001788B File Offset: 0x00015A8B
		public InputAction Pause
		{
			get
			{
				return this.m_Wrapper.m_Default_Pause;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060004F4 RID: 1268 RVA: 0x00017898 File Offset: 0x00015A98
		public InputAction ViewDecks
		{
			get
			{
				return this.m_Wrapper.m_Default_ViewDecks;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060004F5 RID: 1269 RVA: 0x000178A5 File Offset: 0x00015AA5
		public InputAction YButton
		{
			get
			{
				return this.m_Wrapper.m_Default_YButton;
			}
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x000178B2 File Offset: 0x00015AB2
		public InputActionMap Get()
		{
			return this.m_Wrapper.m_Default;
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x000178BF File Offset: 0x00015ABF
		public void Enable()
		{
			this.Get().Enable();
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x000178CC File Offset: 0x00015ACC
		public void Disable()
		{
			this.Get().Disable();
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060004F9 RID: 1273 RVA: 0x000178D9 File Offset: 0x00015AD9
		public bool enabled
		{
			get
			{
				return this.Get().enabled;
			}
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x000178E6 File Offset: 0x00015AE6
		public static implicit operator InputActionMap(InputActions.DefaultActions set)
		{
			return set.Get();
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x000178F0 File Offset: 0x00015AF0
		public void AddCallbacks(InputActions.IDefaultActions instance)
		{
			if (instance == null || this.m_Wrapper.m_DefaultActionsCallbackInterfaces.Contains(instance))
			{
				return;
			}
			this.m_Wrapper.m_DefaultActionsCallbackInterfaces.Add(instance);
			this.Confirm.started += instance.OnConfirm;
			this.Confirm.performed += instance.OnConfirm;
			this.Confirm.canceled += instance.OnConfirm;
			this.Cancel.started += instance.OnCancel;
			this.Cancel.performed += instance.OnCancel;
			this.Cancel.canceled += instance.OnCancel;
			this.Movement.started += instance.OnMovement;
			this.Movement.performed += instance.OnMovement;
			this.Movement.canceled += instance.OnMovement;
			this.Pause.started += instance.OnPause;
			this.Pause.performed += instance.OnPause;
			this.Pause.canceled += instance.OnPause;
			this.ViewDecks.started += instance.OnViewDecks;
			this.ViewDecks.performed += instance.OnViewDecks;
			this.ViewDecks.canceled += instance.OnViewDecks;
			this.YButton.started += instance.OnYButton;
			this.YButton.performed += instance.OnYButton;
			this.YButton.canceled += instance.OnYButton;
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x00017AD8 File Offset: 0x00015CD8
		private void UnregisterCallbacks(InputActions.IDefaultActions instance)
		{
			this.Confirm.started -= instance.OnConfirm;
			this.Confirm.performed -= instance.OnConfirm;
			this.Confirm.canceled -= instance.OnConfirm;
			this.Cancel.started -= instance.OnCancel;
			this.Cancel.performed -= instance.OnCancel;
			this.Cancel.canceled -= instance.OnCancel;
			this.Movement.started -= instance.OnMovement;
			this.Movement.performed -= instance.OnMovement;
			this.Movement.canceled -= instance.OnMovement;
			this.Pause.started -= instance.OnPause;
			this.Pause.performed -= instance.OnPause;
			this.Pause.canceled -= instance.OnPause;
			this.ViewDecks.started -= instance.OnViewDecks;
			this.ViewDecks.performed -= instance.OnViewDecks;
			this.ViewDecks.canceled -= instance.OnViewDecks;
			this.YButton.started -= instance.OnYButton;
			this.YButton.performed -= instance.OnYButton;
			this.YButton.canceled -= instance.OnYButton;
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x00017C95 File Offset: 0x00015E95
		public void RemoveCallbacks(InputActions.IDefaultActions instance)
		{
			if (this.m_Wrapper.m_DefaultActionsCallbackInterfaces.Remove(instance))
			{
				this.UnregisterCallbacks(instance);
			}
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x00017CB4 File Offset: 0x00015EB4
		public void SetCallbacks(InputActions.IDefaultActions instance)
		{
			foreach (InputActions.IDefaultActions defaultActions in this.m_Wrapper.m_DefaultActionsCallbackInterfaces)
			{
				this.UnregisterCallbacks(defaultActions);
			}
			this.m_Wrapper.m_DefaultActionsCallbackInterfaces.Clear();
			this.AddCallbacks(instance);
		}

		// Token: 0x0400040A RID: 1034
		private InputActions m_Wrapper;
	}

	// Token: 0x020000CC RID: 204
	public struct SwitchActions
	{
		// Token: 0x060004FF RID: 1279 RVA: 0x00017D24 File Offset: 0x00015F24
		public SwitchActions(InputActions wrapper)
		{
			this.m_Wrapper = wrapper;
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000500 RID: 1280 RVA: 0x00017D2D File Offset: 0x00015F2D
		public InputAction Confirm
		{
			get
			{
				return this.m_Wrapper.m_Switch_Confirm;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000501 RID: 1281 RVA: 0x00017D3A File Offset: 0x00015F3A
		public InputAction Cancel
		{
			get
			{
				return this.m_Wrapper.m_Switch_Cancel;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000502 RID: 1282 RVA: 0x00017D47 File Offset: 0x00015F47
		public InputAction Movement
		{
			get
			{
				return this.m_Wrapper.m_Switch_Movement;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000503 RID: 1283 RVA: 0x00017D54 File Offset: 0x00015F54
		public InputAction Pause
		{
			get
			{
				return this.m_Wrapper.m_Switch_Pause;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000504 RID: 1284 RVA: 0x00017D61 File Offset: 0x00015F61
		public InputAction ViewDecks
		{
			get
			{
				return this.m_Wrapper.m_Switch_ViewDecks;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000505 RID: 1285 RVA: 0x00017D6E File Offset: 0x00015F6E
		public InputAction YButton
		{
			get
			{
				return this.m_Wrapper.m_Switch_YButton;
			}
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x00017D7B File Offset: 0x00015F7B
		public InputActionMap Get()
		{
			return this.m_Wrapper.m_Switch;
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x00017D88 File Offset: 0x00015F88
		public void Enable()
		{
			this.Get().Enable();
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x00017D95 File Offset: 0x00015F95
		public void Disable()
		{
			this.Get().Disable();
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000509 RID: 1289 RVA: 0x00017DA2 File Offset: 0x00015FA2
		public bool enabled
		{
			get
			{
				return this.Get().enabled;
			}
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x00017DAF File Offset: 0x00015FAF
		public static implicit operator InputActionMap(InputActions.SwitchActions set)
		{
			return set.Get();
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x00017DB8 File Offset: 0x00015FB8
		public void AddCallbacks(InputActions.ISwitchActions instance)
		{
			if (instance == null || this.m_Wrapper.m_SwitchActionsCallbackInterfaces.Contains(instance))
			{
				return;
			}
			this.m_Wrapper.m_SwitchActionsCallbackInterfaces.Add(instance);
			this.Confirm.started += instance.OnConfirm;
			this.Confirm.performed += instance.OnConfirm;
			this.Confirm.canceled += instance.OnConfirm;
			this.Cancel.started += instance.OnCancel;
			this.Cancel.performed += instance.OnCancel;
			this.Cancel.canceled += instance.OnCancel;
			this.Movement.started += instance.OnMovement;
			this.Movement.performed += instance.OnMovement;
			this.Movement.canceled += instance.OnMovement;
			this.Pause.started += instance.OnPause;
			this.Pause.performed += instance.OnPause;
			this.Pause.canceled += instance.OnPause;
			this.ViewDecks.started += instance.OnViewDecks;
			this.ViewDecks.performed += instance.OnViewDecks;
			this.ViewDecks.canceled += instance.OnViewDecks;
			this.YButton.started += instance.OnYButton;
			this.YButton.performed += instance.OnYButton;
			this.YButton.canceled += instance.OnYButton;
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x00017FA0 File Offset: 0x000161A0
		private void UnregisterCallbacks(InputActions.ISwitchActions instance)
		{
			this.Confirm.started -= instance.OnConfirm;
			this.Confirm.performed -= instance.OnConfirm;
			this.Confirm.canceled -= instance.OnConfirm;
			this.Cancel.started -= instance.OnCancel;
			this.Cancel.performed -= instance.OnCancel;
			this.Cancel.canceled -= instance.OnCancel;
			this.Movement.started -= instance.OnMovement;
			this.Movement.performed -= instance.OnMovement;
			this.Movement.canceled -= instance.OnMovement;
			this.Pause.started -= instance.OnPause;
			this.Pause.performed -= instance.OnPause;
			this.Pause.canceled -= instance.OnPause;
			this.ViewDecks.started -= instance.OnViewDecks;
			this.ViewDecks.performed -= instance.OnViewDecks;
			this.ViewDecks.canceled -= instance.OnViewDecks;
			this.YButton.started -= instance.OnYButton;
			this.YButton.performed -= instance.OnYButton;
			this.YButton.canceled -= instance.OnYButton;
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x0001815D File Offset: 0x0001635D
		public void RemoveCallbacks(InputActions.ISwitchActions instance)
		{
			if (this.m_Wrapper.m_SwitchActionsCallbackInterfaces.Remove(instance))
			{
				this.UnregisterCallbacks(instance);
			}
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x0001817C File Offset: 0x0001637C
		public void SetCallbacks(InputActions.ISwitchActions instance)
		{
			foreach (InputActions.ISwitchActions switchActions in this.m_Wrapper.m_SwitchActionsCallbackInterfaces)
			{
				this.UnregisterCallbacks(switchActions);
			}
			this.m_Wrapper.m_SwitchActionsCallbackInterfaces.Clear();
			this.AddCallbacks(instance);
		}

		// Token: 0x0400040B RID: 1035
		private InputActions m_Wrapper;
	}

	// Token: 0x020000CD RID: 205
	public interface IDefaultActions
	{
		// Token: 0x0600050F RID: 1295
		void OnConfirm(InputAction.CallbackContext context);

		// Token: 0x06000510 RID: 1296
		void OnCancel(InputAction.CallbackContext context);

		// Token: 0x06000511 RID: 1297
		void OnMovement(InputAction.CallbackContext context);

		// Token: 0x06000512 RID: 1298
		void OnPause(InputAction.CallbackContext context);

		// Token: 0x06000513 RID: 1299
		void OnViewDecks(InputAction.CallbackContext context);

		// Token: 0x06000514 RID: 1300
		void OnYButton(InputAction.CallbackContext context);
	}

	// Token: 0x020000CE RID: 206
	public interface ISwitchActions
	{
		// Token: 0x06000515 RID: 1301
		void OnConfirm(InputAction.CallbackContext context);

		// Token: 0x06000516 RID: 1302
		void OnCancel(InputAction.CallbackContext context);

		// Token: 0x06000517 RID: 1303
		void OnMovement(InputAction.CallbackContext context);

		// Token: 0x06000518 RID: 1304
		void OnPause(InputAction.CallbackContext context);

		// Token: 0x06000519 RID: 1305
		void OnViewDecks(InputAction.CallbackContext context);

		// Token: 0x0600051A RID: 1306
		void OnYButton(InputAction.CallbackContext context);
	}
}
