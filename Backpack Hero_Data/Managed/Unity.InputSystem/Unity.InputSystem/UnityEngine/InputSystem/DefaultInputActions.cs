using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000059 RID: 89
	public class DefaultInputActions : IInputActionCollection2, IInputActionCollection, IEnumerable<InputAction>, IEnumerable, IDisposable
	{
		// Token: 0x1700022D RID: 557
		// (get) Token: 0x060008D4 RID: 2260 RVA: 0x00031EDE File Offset: 0x000300DE
		public InputActionAsset asset { get; }

		// Token: 0x060008D5 RID: 2261 RVA: 0x00031EE8 File Offset: 0x000300E8
		public DefaultInputActions()
		{
			this.asset = InputActionAsset.FromJson("{\n    \"name\": \"DefaultInputActions\",\n    \"maps\": [\n        {\n            \"name\": \"Player\",\n            \"id\": \"df70fa95-8a34-4494-b137-73ab6b9c7d37\",\n            \"actions\": [\n                {\n                    \"name\": \"Move\",\n                    \"type\": \"Value\",\n                    \"id\": \"351f2ccd-1f9f-44bf-9bec-d62ac5c5f408\",\n                    \"expectedControlType\": \"Vector2\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": true\n                },\n                {\n                    \"name\": \"Look\",\n                    \"type\": \"Value\",\n                    \"id\": \"6b444451-8a00-4d00-a97e-f47457f736a8\",\n                    \"expectedControlType\": \"Vector2\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": true\n                },\n                {\n                    \"name\": \"Fire\",\n                    \"type\": \"Button\",\n                    \"id\": \"6c2ab1b8-8984-453a-af3d-a3c78ae1679a\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                }\n            ],\n            \"bindings\": [\n                {\n                    \"name\": \"\",\n                    \"id\": \"978bfe49-cc26-4a3d-ab7b-7d7a29327403\",\n                    \"path\": \"<Gamepad>/leftStick\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"Move\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"WASD\",\n                    \"id\": \"00ca640b-d935-4593-8157-c05846ea39b3\",\n                    \"path\": \"Dpad\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Move\",\n                    \"isComposite\": true,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"up\",\n                    \"id\": \"e2062cb9-1b15-46a2-838c-2f8d72a0bdd9\",\n                    \"path\": \"<Keyboard>/w\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse\",\n                    \"action\": \"Move\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"up\",\n                    \"id\": \"8180e8bd-4097-4f4e-ab88-4523101a6ce9\",\n                    \"path\": \"<Keyboard>/upArrow\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse\",\n                    \"action\": \"Move\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"down\",\n                    \"id\": \"320bffee-a40b-4347-ac70-c210eb8bc73a\",\n                    \"path\": \"<Keyboard>/s\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse\",\n                    \"action\": \"Move\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"down\",\n                    \"id\": \"1c5327b5-f71c-4f60-99c7-4e737386f1d1\",\n                    \"path\": \"<Keyboard>/downArrow\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse\",\n                    \"action\": \"Move\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"left\",\n                    \"id\": \"d2581a9b-1d11-4566-b27d-b92aff5fabbc\",\n                    \"path\": \"<Keyboard>/a\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse\",\n                    \"action\": \"Move\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"left\",\n                    \"id\": \"2e46982e-44cc-431b-9f0b-c11910bf467a\",\n                    \"path\": \"<Keyboard>/leftArrow\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse\",\n                    \"action\": \"Move\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"right\",\n                    \"id\": \"fcfe95b8-67b9-4526-84b5-5d0bc98d6400\",\n                    \"path\": \"<Keyboard>/d\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse\",\n                    \"action\": \"Move\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"right\",\n                    \"id\": \"77bff152-3580-4b21-b6de-dcd0c7e41164\",\n                    \"path\": \"<Keyboard>/rightArrow\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse\",\n                    \"action\": \"Move\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"1635d3fe-58b6-4ba9-a4e2-f4b964f6b5c8\",\n                    \"path\": \"<XRController>/{Primary2DAxis}\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"XR\",\n                    \"action\": \"Move\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"3ea4d645-4504-4529-b061-ab81934c3752\",\n                    \"path\": \"<Joystick>/stick\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Joystick\",\n                    \"action\": \"Move\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"c1f7a91b-d0fd-4a62-997e-7fb9b69bf235\",\n                    \"path\": \"<Gamepad>/rightStick\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"Look\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"8c8e490b-c610-4785-884f-f04217b23ca4\",\n                    \"path\": \"<Pointer>/delta\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse;Touch\",\n                    \"action\": \"Look\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"3e5f5442-8668-4b27-a940-df99bad7e831\",\n                    \"path\": \"<Joystick>/{Hatswitch}\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Joystick\",\n                    \"action\": \"Look\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"143bb1cd-cc10-4eca-a2f0-a3664166fe91\",\n                    \"path\": \"<Gamepad>/rightTrigger\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"Fire\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"05f6913d-c316-48b2-a6bb-e225f14c7960\",\n                    \"path\": \"<Mouse>/leftButton\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse\",\n                    \"action\": \"Fire\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"886e731e-7071-4ae4-95c0-e61739dad6fd\",\n                    \"path\": \"<Touchscreen>/primaryTouch/tap\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Touch\",\n                    \"action\": \"Fire\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"ee3d0cd2-254e-47a7-a8cb-bc94d9658c54\",\n                    \"path\": \"<Joystick>/trigger\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Joystick\",\n                    \"action\": \"Fire\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"8255d333-5683-4943-a58a-ccb207ff1dce\",\n                    \"path\": \"<XRController>/{PrimaryAction}\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"XR\",\n                    \"action\": \"Fire\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                }\n            ]\n        },\n        {\n            \"name\": \"UI\",\n            \"id\": \"272f6d14-89ba-496f-b7ff-215263d3219f\",\n            \"actions\": [\n                {\n                    \"name\": \"Navigate\",\n                    \"type\": \"PassThrough\",\n                    \"id\": \"c95b2375-e6d9-4b88-9c4c-c5e76515df4b\",\n                    \"expectedControlType\": \"Vector2\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"Submit\",\n                    \"type\": \"Button\",\n                    \"id\": \"7607c7b6-cd76-4816-beef-bd0341cfe950\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"Cancel\",\n                    \"type\": \"Button\",\n                    \"id\": \"15cef263-9014-4fd5-94d9-4e4a6234a6ef\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"Point\",\n                    \"type\": \"PassThrough\",\n                    \"id\": \"32b35790-4ed0-4e9a-aa41-69ac6d629449\",\n                    \"expectedControlType\": \"Vector2\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": true\n                },\n                {\n                    \"name\": \"Click\",\n                    \"type\": \"PassThrough\",\n                    \"id\": \"3c7022bf-7922-4f7c-a998-c437916075ad\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": true\n                },\n                {\n                    \"name\": \"ScrollWheel\",\n                    \"type\": \"PassThrough\",\n                    \"id\": \"0489e84a-4833-4c40-bfae-cea84b696689\",\n                    \"expectedControlType\": \"Vector2\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"MiddleClick\",\n                    \"type\": \"PassThrough\",\n                    \"id\": \"dad70c86-b58c-4b17-88ad-f5e53adf419e\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"RightClick\",\n                    \"type\": \"PassThrough\",\n                    \"id\": \"44b200b1-1557-4083-816c-b22cbdf77ddf\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"TrackedDevicePosition\",\n                    \"type\": \"PassThrough\",\n                    \"id\": \"24908448-c609-4bc3-a128-ea258674378a\",\n                    \"expectedControlType\": \"Vector3\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"TrackedDeviceOrientation\",\n                    \"type\": \"PassThrough\",\n                    \"id\": \"9caa3d8a-6b2f-4e8e-8bad-6ede561bd9be\",\n                    \"expectedControlType\": \"Quaternion\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                }\n            ],\n            \"bindings\": [\n                {\n                    \"name\": \"Gamepad\",\n                    \"id\": \"809f371f-c5e2-4e7a-83a1-d867598f40dd\",\n                    \"path\": \"2DVector\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": true,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"up\",\n                    \"id\": \"14a5d6e8-4aaf-4119-a9ef-34b8c2c548bf\",\n                    \"path\": \"<Gamepad>/leftStick/up\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"up\",\n                    \"id\": \"9144cbe6-05e1-4687-a6d7-24f99d23dd81\",\n                    \"path\": \"<Gamepad>/rightStick/up\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"down\",\n                    \"id\": \"2db08d65-c5fb-421b-983f-c71163608d67\",\n                    \"path\": \"<Gamepad>/leftStick/down\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"down\",\n                    \"id\": \"58748904-2ea9-4a80-8579-b500e6a76df8\",\n                    \"path\": \"<Gamepad>/rightStick/down\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"left\",\n                    \"id\": \"8ba04515-75aa-45de-966d-393d9bbd1c14\",\n                    \"path\": \"<Gamepad>/leftStick/left\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"left\",\n                    \"id\": \"712e721c-bdfb-4b23-a86c-a0d9fcfea921\",\n                    \"path\": \"<Gamepad>/rightStick/left\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"right\",\n                    \"id\": \"fcd248ae-a788-4676-a12e-f4d81205600b\",\n                    \"path\": \"<Gamepad>/leftStick/right\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"right\",\n                    \"id\": \"1f04d9bc-c50b-41a1-bfcc-afb75475ec20\",\n                    \"path\": \"<Gamepad>/rightStick/right\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"fb8277d4-c5cd-4663-9dc7-ee3f0b506d90\",\n                    \"path\": \"<Gamepad>/dpad\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"Joystick\",\n                    \"id\": \"e25d9774-381c-4a61-b47c-7b6b299ad9f9\",\n                    \"path\": \"2DVector\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": true,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"up\",\n                    \"id\": \"3db53b26-6601-41be-9887-63ac74e79d19\",\n                    \"path\": \"<Joystick>/stick/up\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Joystick\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"down\",\n                    \"id\": \"0cb3e13e-3d90-4178-8ae6-d9c5501d653f\",\n                    \"path\": \"<Joystick>/stick/down\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Joystick\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"left\",\n                    \"id\": \"0392d399-f6dd-4c82-8062-c1e9c0d34835\",\n                    \"path\": \"<Joystick>/stick/left\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Joystick\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n   [...string is too long...]");
			this.m_Player = this.asset.FindActionMap("Player", true);
			this.m_Player_Move = this.m_Player.FindAction("Move", true);
			this.m_Player_Look = this.m_Player.FindAction("Look", true);
			this.m_Player_Fire = this.m_Player.FindAction("Fire", true);
			this.m_UI = this.asset.FindActionMap("UI", true);
			this.m_UI_Navigate = this.m_UI.FindAction("Navigate", true);
			this.m_UI_Submit = this.m_UI.FindAction("Submit", true);
			this.m_UI_Cancel = this.m_UI.FindAction("Cancel", true);
			this.m_UI_Point = this.m_UI.FindAction("Point", true);
			this.m_UI_Click = this.m_UI.FindAction("Click", true);
			this.m_UI_ScrollWheel = this.m_UI.FindAction("ScrollWheel", true);
			this.m_UI_MiddleClick = this.m_UI.FindAction("MiddleClick", true);
			this.m_UI_RightClick = this.m_UI.FindAction("RightClick", true);
			this.m_UI_TrackedDevicePosition = this.m_UI.FindAction("TrackedDevicePosition", true);
			this.m_UI_TrackedDeviceOrientation = this.m_UI.FindAction("TrackedDeviceOrientation", true);
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x00032087 File Offset: 0x00030287
		public void Dispose()
		{
			Object.Destroy(this.asset);
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x060008D7 RID: 2263 RVA: 0x00032094 File Offset: 0x00030294
		// (set) Token: 0x060008D8 RID: 2264 RVA: 0x000320A1 File Offset: 0x000302A1
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

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x060008D9 RID: 2265 RVA: 0x000320AF File Offset: 0x000302AF
		// (set) Token: 0x060008DA RID: 2266 RVA: 0x000320BC File Offset: 0x000302BC
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

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x060008DB RID: 2267 RVA: 0x000320CA File Offset: 0x000302CA
		public ReadOnlyArray<InputControlScheme> controlSchemes
		{
			get
			{
				return this.asset.controlSchemes;
			}
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x000320D7 File Offset: 0x000302D7
		public bool Contains(InputAction action)
		{
			return this.asset.Contains(action);
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x000320E5 File Offset: 0x000302E5
		public IEnumerator<InputAction> GetEnumerator()
		{
			return this.asset.GetEnumerator();
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x000320F2 File Offset: 0x000302F2
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x000320FA File Offset: 0x000302FA
		public void Enable()
		{
			this.asset.Enable();
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x00032107 File Offset: 0x00030307
		public void Disable()
		{
			this.asset.Disable();
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x060008E1 RID: 2273 RVA: 0x00032114 File Offset: 0x00030314
		public IEnumerable<InputBinding> bindings
		{
			get
			{
				return this.asset.bindings;
			}
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x00032121 File Offset: 0x00030321
		public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
		{
			return this.asset.FindAction(actionNameOrId, throwIfNotFound);
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x00032130 File Offset: 0x00030330
		public int FindBinding(InputBinding bindingMask, out InputAction action)
		{
			return this.asset.FindBinding(bindingMask, out action);
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x060008E4 RID: 2276 RVA: 0x0003213F File Offset: 0x0003033F
		public DefaultInputActions.PlayerActions Player
		{
			get
			{
				return new DefaultInputActions.PlayerActions(this);
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x060008E5 RID: 2277 RVA: 0x00032147 File Offset: 0x00030347
		public DefaultInputActions.UIActions UI
		{
			get
			{
				return new DefaultInputActions.UIActions(this);
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x060008E6 RID: 2278 RVA: 0x00032150 File Offset: 0x00030350
		public InputControlScheme KeyboardMouseScheme
		{
			get
			{
				if (this.m_KeyboardMouseSchemeIndex == -1)
				{
					this.m_KeyboardMouseSchemeIndex = this.asset.FindControlSchemeIndex("Keyboard&Mouse");
				}
				return this.asset.controlSchemes[this.m_KeyboardMouseSchemeIndex];
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x060008E7 RID: 2279 RVA: 0x00032198 File Offset: 0x00030398
		public InputControlScheme GamepadScheme
		{
			get
			{
				if (this.m_GamepadSchemeIndex == -1)
				{
					this.m_GamepadSchemeIndex = this.asset.FindControlSchemeIndex("Gamepad");
				}
				return this.asset.controlSchemes[this.m_GamepadSchemeIndex];
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x060008E8 RID: 2280 RVA: 0x000321E0 File Offset: 0x000303E0
		public InputControlScheme TouchScheme
		{
			get
			{
				if (this.m_TouchSchemeIndex == -1)
				{
					this.m_TouchSchemeIndex = this.asset.FindControlSchemeIndex("Touch");
				}
				return this.asset.controlSchemes[this.m_TouchSchemeIndex];
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x060008E9 RID: 2281 RVA: 0x00032228 File Offset: 0x00030428
		public InputControlScheme JoystickScheme
		{
			get
			{
				if (this.m_JoystickSchemeIndex == -1)
				{
					this.m_JoystickSchemeIndex = this.asset.FindControlSchemeIndex("Joystick");
				}
				return this.asset.controlSchemes[this.m_JoystickSchemeIndex];
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x060008EA RID: 2282 RVA: 0x00032270 File Offset: 0x00030470
		public InputControlScheme XRScheme
		{
			get
			{
				if (this.m_XRSchemeIndex == -1)
				{
					this.m_XRSchemeIndex = this.asset.FindControlSchemeIndex("XR");
				}
				return this.asset.controlSchemes[this.m_XRSchemeIndex];
			}
		}

		// Token: 0x040002A1 RID: 673
		private readonly InputActionMap m_Player;

		// Token: 0x040002A2 RID: 674
		private DefaultInputActions.IPlayerActions m_PlayerActionsCallbackInterface;

		// Token: 0x040002A3 RID: 675
		private readonly InputAction m_Player_Move;

		// Token: 0x040002A4 RID: 676
		private readonly InputAction m_Player_Look;

		// Token: 0x040002A5 RID: 677
		private readonly InputAction m_Player_Fire;

		// Token: 0x040002A6 RID: 678
		private readonly InputActionMap m_UI;

		// Token: 0x040002A7 RID: 679
		private DefaultInputActions.IUIActions m_UIActionsCallbackInterface;

		// Token: 0x040002A8 RID: 680
		private readonly InputAction m_UI_Navigate;

		// Token: 0x040002A9 RID: 681
		private readonly InputAction m_UI_Submit;

		// Token: 0x040002AA RID: 682
		private readonly InputAction m_UI_Cancel;

		// Token: 0x040002AB RID: 683
		private readonly InputAction m_UI_Point;

		// Token: 0x040002AC RID: 684
		private readonly InputAction m_UI_Click;

		// Token: 0x040002AD RID: 685
		private readonly InputAction m_UI_ScrollWheel;

		// Token: 0x040002AE RID: 686
		private readonly InputAction m_UI_MiddleClick;

		// Token: 0x040002AF RID: 687
		private readonly InputAction m_UI_RightClick;

		// Token: 0x040002B0 RID: 688
		private readonly InputAction m_UI_TrackedDevicePosition;

		// Token: 0x040002B1 RID: 689
		private readonly InputAction m_UI_TrackedDeviceOrientation;

		// Token: 0x040002B2 RID: 690
		private int m_KeyboardMouseSchemeIndex = -1;

		// Token: 0x040002B3 RID: 691
		private int m_GamepadSchemeIndex = -1;

		// Token: 0x040002B4 RID: 692
		private int m_TouchSchemeIndex = -1;

		// Token: 0x040002B5 RID: 693
		private int m_JoystickSchemeIndex = -1;

		// Token: 0x040002B6 RID: 694
		private int m_XRSchemeIndex = -1;

		// Token: 0x020001AE RID: 430
		public struct PlayerActions
		{
			// Token: 0x060013D0 RID: 5072 RVA: 0x0005BBC4 File Offset: 0x00059DC4
			public PlayerActions(DefaultInputActions wrapper)
			{
				this.m_Wrapper = wrapper;
			}

			// Token: 0x17000552 RID: 1362
			// (get) Token: 0x060013D1 RID: 5073 RVA: 0x0005BBCD File Offset: 0x00059DCD
			public InputAction Move
			{
				get
				{
					return this.m_Wrapper.m_Player_Move;
				}
			}

			// Token: 0x17000553 RID: 1363
			// (get) Token: 0x060013D2 RID: 5074 RVA: 0x0005BBDA File Offset: 0x00059DDA
			public InputAction Look
			{
				get
				{
					return this.m_Wrapper.m_Player_Look;
				}
			}

			// Token: 0x17000554 RID: 1364
			// (get) Token: 0x060013D3 RID: 5075 RVA: 0x0005BBE7 File Offset: 0x00059DE7
			public InputAction Fire
			{
				get
				{
					return this.m_Wrapper.m_Player_Fire;
				}
			}

			// Token: 0x060013D4 RID: 5076 RVA: 0x0005BBF4 File Offset: 0x00059DF4
			public InputActionMap Get()
			{
				return this.m_Wrapper.m_Player;
			}

			// Token: 0x060013D5 RID: 5077 RVA: 0x0005BC01 File Offset: 0x00059E01
			public void Enable()
			{
				this.Get().Enable();
			}

			// Token: 0x060013D6 RID: 5078 RVA: 0x0005BC0E File Offset: 0x00059E0E
			public void Disable()
			{
				this.Get().Disable();
			}

			// Token: 0x17000555 RID: 1365
			// (get) Token: 0x060013D7 RID: 5079 RVA: 0x0005BC1B File Offset: 0x00059E1B
			public bool enabled
			{
				get
				{
					return this.Get().enabled;
				}
			}

			// Token: 0x060013D8 RID: 5080 RVA: 0x0005BC28 File Offset: 0x00059E28
			public static implicit operator InputActionMap(DefaultInputActions.PlayerActions set)
			{
				return set.Get();
			}

			// Token: 0x060013D9 RID: 5081 RVA: 0x0005BC34 File Offset: 0x00059E34
			public void SetCallbacks(DefaultInputActions.IPlayerActions instance)
			{
				if (this.m_Wrapper.m_PlayerActionsCallbackInterface != null)
				{
					this.Move.started -= this.m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
					this.Move.performed -= this.m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
					this.Move.canceled -= this.m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
					this.Look.started -= this.m_Wrapper.m_PlayerActionsCallbackInterface.OnLook;
					this.Look.performed -= this.m_Wrapper.m_PlayerActionsCallbackInterface.OnLook;
					this.Look.canceled -= this.m_Wrapper.m_PlayerActionsCallbackInterface.OnLook;
					this.Fire.started -= this.m_Wrapper.m_PlayerActionsCallbackInterface.OnFire;
					this.Fire.performed -= this.m_Wrapper.m_PlayerActionsCallbackInterface.OnFire;
					this.Fire.canceled -= this.m_Wrapper.m_PlayerActionsCallbackInterface.OnFire;
				}
				this.m_Wrapper.m_PlayerActionsCallbackInterface = instance;
				if (instance != null)
				{
					this.Move.started += instance.OnMove;
					this.Move.performed += instance.OnMove;
					this.Move.canceled += instance.OnMove;
					this.Look.started += instance.OnLook;
					this.Look.performed += instance.OnLook;
					this.Look.canceled += instance.OnLook;
					this.Fire.started += instance.OnFire;
					this.Fire.performed += instance.OnFire;
					this.Fire.canceled += instance.OnFire;
				}
			}

			// Token: 0x040008E2 RID: 2274
			private DefaultInputActions m_Wrapper;
		}

		// Token: 0x020001AF RID: 431
		public struct UIActions
		{
			// Token: 0x060013DA RID: 5082 RVA: 0x0005BE6D File Offset: 0x0005A06D
			public UIActions(DefaultInputActions wrapper)
			{
				this.m_Wrapper = wrapper;
			}

			// Token: 0x17000556 RID: 1366
			// (get) Token: 0x060013DB RID: 5083 RVA: 0x0005BE76 File Offset: 0x0005A076
			public InputAction Navigate
			{
				get
				{
					return this.m_Wrapper.m_UI_Navigate;
				}
			}

			// Token: 0x17000557 RID: 1367
			// (get) Token: 0x060013DC RID: 5084 RVA: 0x0005BE83 File Offset: 0x0005A083
			public InputAction Submit
			{
				get
				{
					return this.m_Wrapper.m_UI_Submit;
				}
			}

			// Token: 0x17000558 RID: 1368
			// (get) Token: 0x060013DD RID: 5085 RVA: 0x0005BE90 File Offset: 0x0005A090
			public InputAction Cancel
			{
				get
				{
					return this.m_Wrapper.m_UI_Cancel;
				}
			}

			// Token: 0x17000559 RID: 1369
			// (get) Token: 0x060013DE RID: 5086 RVA: 0x0005BE9D File Offset: 0x0005A09D
			public InputAction Point
			{
				get
				{
					return this.m_Wrapper.m_UI_Point;
				}
			}

			// Token: 0x1700055A RID: 1370
			// (get) Token: 0x060013DF RID: 5087 RVA: 0x0005BEAA File Offset: 0x0005A0AA
			public InputAction Click
			{
				get
				{
					return this.m_Wrapper.m_UI_Click;
				}
			}

			// Token: 0x1700055B RID: 1371
			// (get) Token: 0x060013E0 RID: 5088 RVA: 0x0005BEB7 File Offset: 0x0005A0B7
			public InputAction ScrollWheel
			{
				get
				{
					return this.m_Wrapper.m_UI_ScrollWheel;
				}
			}

			// Token: 0x1700055C RID: 1372
			// (get) Token: 0x060013E1 RID: 5089 RVA: 0x0005BEC4 File Offset: 0x0005A0C4
			public InputAction MiddleClick
			{
				get
				{
					return this.m_Wrapper.m_UI_MiddleClick;
				}
			}

			// Token: 0x1700055D RID: 1373
			// (get) Token: 0x060013E2 RID: 5090 RVA: 0x0005BED1 File Offset: 0x0005A0D1
			public InputAction RightClick
			{
				get
				{
					return this.m_Wrapper.m_UI_RightClick;
				}
			}

			// Token: 0x1700055E RID: 1374
			// (get) Token: 0x060013E3 RID: 5091 RVA: 0x0005BEDE File Offset: 0x0005A0DE
			public InputAction TrackedDevicePosition
			{
				get
				{
					return this.m_Wrapper.m_UI_TrackedDevicePosition;
				}
			}

			// Token: 0x1700055F RID: 1375
			// (get) Token: 0x060013E4 RID: 5092 RVA: 0x0005BEEB File Offset: 0x0005A0EB
			public InputAction TrackedDeviceOrientation
			{
				get
				{
					return this.m_Wrapper.m_UI_TrackedDeviceOrientation;
				}
			}

			// Token: 0x060013E5 RID: 5093 RVA: 0x0005BEF8 File Offset: 0x0005A0F8
			public InputActionMap Get()
			{
				return this.m_Wrapper.m_UI;
			}

			// Token: 0x060013E6 RID: 5094 RVA: 0x0005BF05 File Offset: 0x0005A105
			public void Enable()
			{
				this.Get().Enable();
			}

			// Token: 0x060013E7 RID: 5095 RVA: 0x0005BF12 File Offset: 0x0005A112
			public void Disable()
			{
				this.Get().Disable();
			}

			// Token: 0x17000560 RID: 1376
			// (get) Token: 0x060013E8 RID: 5096 RVA: 0x0005BF1F File Offset: 0x0005A11F
			public bool enabled
			{
				get
				{
					return this.Get().enabled;
				}
			}

			// Token: 0x060013E9 RID: 5097 RVA: 0x0005BF2C File Offset: 0x0005A12C
			public static implicit operator InputActionMap(DefaultInputActions.UIActions set)
			{
				return set.Get();
			}

			// Token: 0x060013EA RID: 5098 RVA: 0x0005BF38 File Offset: 0x0005A138
			public void SetCallbacks(DefaultInputActions.IUIActions instance)
			{
				if (this.m_Wrapper.m_UIActionsCallbackInterface != null)
				{
					this.Navigate.started -= this.m_Wrapper.m_UIActionsCallbackInterface.OnNavigate;
					this.Navigate.performed -= this.m_Wrapper.m_UIActionsCallbackInterface.OnNavigate;
					this.Navigate.canceled -= this.m_Wrapper.m_UIActionsCallbackInterface.OnNavigate;
					this.Submit.started -= this.m_Wrapper.m_UIActionsCallbackInterface.OnSubmit;
					this.Submit.performed -= this.m_Wrapper.m_UIActionsCallbackInterface.OnSubmit;
					this.Submit.canceled -= this.m_Wrapper.m_UIActionsCallbackInterface.OnSubmit;
					this.Cancel.started -= this.m_Wrapper.m_UIActionsCallbackInterface.OnCancel;
					this.Cancel.performed -= this.m_Wrapper.m_UIActionsCallbackInterface.OnCancel;
					this.Cancel.canceled -= this.m_Wrapper.m_UIActionsCallbackInterface.OnCancel;
					this.Point.started -= this.m_Wrapper.m_UIActionsCallbackInterface.OnPoint;
					this.Point.performed -= this.m_Wrapper.m_UIActionsCallbackInterface.OnPoint;
					this.Point.canceled -= this.m_Wrapper.m_UIActionsCallbackInterface.OnPoint;
					this.Click.started -= this.m_Wrapper.m_UIActionsCallbackInterface.OnClick;
					this.Click.performed -= this.m_Wrapper.m_UIActionsCallbackInterface.OnClick;
					this.Click.canceled -= this.m_Wrapper.m_UIActionsCallbackInterface.OnClick;
					this.ScrollWheel.started -= this.m_Wrapper.m_UIActionsCallbackInterface.OnScrollWheel;
					this.ScrollWheel.performed -= this.m_Wrapper.m_UIActionsCallbackInterface.OnScrollWheel;
					this.ScrollWheel.canceled -= this.m_Wrapper.m_UIActionsCallbackInterface.OnScrollWheel;
					this.MiddleClick.started -= this.m_Wrapper.m_UIActionsCallbackInterface.OnMiddleClick;
					this.MiddleClick.performed -= this.m_Wrapper.m_UIActionsCallbackInterface.OnMiddleClick;
					this.MiddleClick.canceled -= this.m_Wrapper.m_UIActionsCallbackInterface.OnMiddleClick;
					this.RightClick.started -= this.m_Wrapper.m_UIActionsCallbackInterface.OnRightClick;
					this.RightClick.performed -= this.m_Wrapper.m_UIActionsCallbackInterface.OnRightClick;
					this.RightClick.canceled -= this.m_Wrapper.m_UIActionsCallbackInterface.OnRightClick;
					this.TrackedDevicePosition.started -= this.m_Wrapper.m_UIActionsCallbackInterface.OnTrackedDevicePosition;
					this.TrackedDevicePosition.performed -= this.m_Wrapper.m_UIActionsCallbackInterface.OnTrackedDevicePosition;
					this.TrackedDevicePosition.canceled -= this.m_Wrapper.m_UIActionsCallbackInterface.OnTrackedDevicePosition;
					this.TrackedDeviceOrientation.started -= this.m_Wrapper.m_UIActionsCallbackInterface.OnTrackedDeviceOrientation;
					this.TrackedDeviceOrientation.performed -= this.m_Wrapper.m_UIActionsCallbackInterface.OnTrackedDeviceOrientation;
					this.TrackedDeviceOrientation.canceled -= this.m_Wrapper.m_UIActionsCallbackInterface.OnTrackedDeviceOrientation;
				}
				this.m_Wrapper.m_UIActionsCallbackInterface = instance;
				if (instance != null)
				{
					this.Navigate.started += instance.OnNavigate;
					this.Navigate.performed += instance.OnNavigate;
					this.Navigate.canceled += instance.OnNavigate;
					this.Submit.started += instance.OnSubmit;
					this.Submit.performed += instance.OnSubmit;
					this.Submit.canceled += instance.OnSubmit;
					this.Cancel.started += instance.OnCancel;
					this.Cancel.performed += instance.OnCancel;
					this.Cancel.canceled += instance.OnCancel;
					this.Point.started += instance.OnPoint;
					this.Point.performed += instance.OnPoint;
					this.Point.canceled += instance.OnPoint;
					this.Click.started += instance.OnClick;
					this.Click.performed += instance.OnClick;
					this.Click.canceled += instance.OnClick;
					this.ScrollWheel.started += instance.OnScrollWheel;
					this.ScrollWheel.performed += instance.OnScrollWheel;
					this.ScrollWheel.canceled += instance.OnScrollWheel;
					this.MiddleClick.started += instance.OnMiddleClick;
					this.MiddleClick.performed += instance.OnMiddleClick;
					this.MiddleClick.canceled += instance.OnMiddleClick;
					this.RightClick.started += instance.OnRightClick;
					this.RightClick.performed += instance.OnRightClick;
					this.RightClick.canceled += instance.OnRightClick;
					this.TrackedDevicePosition.started += instance.OnTrackedDevicePosition;
					this.TrackedDevicePosition.performed += instance.OnTrackedDevicePosition;
					this.TrackedDevicePosition.canceled += instance.OnTrackedDevicePosition;
					this.TrackedDeviceOrientation.started += instance.OnTrackedDeviceOrientation;
					this.TrackedDeviceOrientation.performed += instance.OnTrackedDeviceOrientation;
					this.TrackedDeviceOrientation.canceled += instance.OnTrackedDeviceOrientation;
				}
			}

			// Token: 0x040008E3 RID: 2275
			private DefaultInputActions m_Wrapper;
		}

		// Token: 0x020001B0 RID: 432
		public interface IPlayerActions
		{
			// Token: 0x060013EB RID: 5099
			void OnMove(InputAction.CallbackContext context);

			// Token: 0x060013EC RID: 5100
			void OnLook(InputAction.CallbackContext context);

			// Token: 0x060013ED RID: 5101
			void OnFire(InputAction.CallbackContext context);
		}

		// Token: 0x020001B1 RID: 433
		public interface IUIActions
		{
			// Token: 0x060013EE RID: 5102
			void OnNavigate(InputAction.CallbackContext context);

			// Token: 0x060013EF RID: 5103
			void OnSubmit(InputAction.CallbackContext context);

			// Token: 0x060013F0 RID: 5104
			void OnCancel(InputAction.CallbackContext context);

			// Token: 0x060013F1 RID: 5105
			void OnPoint(InputAction.CallbackContext context);

			// Token: 0x060013F2 RID: 5106
			void OnClick(InputAction.CallbackContext context);

			// Token: 0x060013F3 RID: 5107
			void OnScrollWheel(InputAction.CallbackContext context);

			// Token: 0x060013F4 RID: 5108
			void OnMiddleClick(InputAction.CallbackContext context);

			// Token: 0x060013F5 RID: 5109
			void OnRightClick(InputAction.CallbackContext context);

			// Token: 0x060013F6 RID: 5110
			void OnTrackedDevicePosition(InputAction.CallbackContext context);

			// Token: 0x060013F7 RID: 5111
			void OnTrackedDeviceOrientation(InputAction.CallbackContext context);
		}
	}
}
