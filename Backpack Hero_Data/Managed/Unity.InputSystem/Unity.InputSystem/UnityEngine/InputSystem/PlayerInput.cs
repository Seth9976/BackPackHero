using System;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.UI;
using UnityEngine.InputSystem.Users;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem
{
	// Token: 0x0200005B RID: 91
	[AddComponentMenu("Input/Player Input")]
	[DisallowMultipleComponent]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.inputsystem@1.6/manual/PlayerInput.html")]
	public class PlayerInput : MonoBehaviour
	{
		// Token: 0x1700023A RID: 570
		// (get) Token: 0x060008EF RID: 2287 RVA: 0x0003232A File Offset: 0x0003052A
		public bool inputIsActive
		{
			get
			{
				return this.m_InputActive;
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x060008F0 RID: 2288 RVA: 0x00032332 File Offset: 0x00030532
		[Obsolete("Use inputIsActive instead.")]
		public bool active
		{
			get
			{
				return this.inputIsActive;
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x060008F1 RID: 2289 RVA: 0x0003233A File Offset: 0x0003053A
		public int playerIndex
		{
			get
			{
				return this.m_PlayerIndex;
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x060008F2 RID: 2290 RVA: 0x00032342 File Offset: 0x00030542
		public int splitScreenIndex
		{
			get
			{
				return this.m_SplitScreenIndex;
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x060008F3 RID: 2291 RVA: 0x0003234A File Offset: 0x0003054A
		// (set) Token: 0x060008F4 RID: 2292 RVA: 0x00032370 File Offset: 0x00030570
		public InputActionAsset actions
		{
			get
			{
				if (!this.m_ActionsInitialized && base.gameObject.activeInHierarchy)
				{
					this.InitializeActions();
				}
				return this.m_Actions;
			}
			set
			{
				if (this.m_Actions == value)
				{
					return;
				}
				if (this.m_Actions != null)
				{
					this.m_Actions.Disable();
					if (this.m_ActionsInitialized)
					{
						this.UninitializeActions();
					}
				}
				this.m_Actions = value;
				if (this.m_Enabled)
				{
					this.ClearCaches();
					this.AssignUserAndDevices();
					this.InitializeActions();
					if (this.m_InputActive)
					{
						this.ActivateInput();
					}
				}
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x060008F5 RID: 2293 RVA: 0x000323E4 File Offset: 0x000305E4
		public string currentControlScheme
		{
			get
			{
				if (!this.m_InputUser.valid)
				{
					return null;
				}
				InputControlScheme? controlScheme = this.m_InputUser.controlScheme;
				if (controlScheme == null)
				{
					return null;
				}
				return controlScheme.GetValueOrDefault().name;
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x060008F6 RID: 2294 RVA: 0x00032426 File Offset: 0x00030626
		// (set) Token: 0x060008F7 RID: 2295 RVA: 0x0003242E File Offset: 0x0003062E
		public string defaultControlScheme
		{
			get
			{
				return this.m_DefaultControlScheme;
			}
			set
			{
				this.m_DefaultControlScheme = value;
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x060008F8 RID: 2296 RVA: 0x00032437 File Offset: 0x00030637
		// (set) Token: 0x060008F9 RID: 2297 RVA: 0x0003243F File Offset: 0x0003063F
		public bool neverAutoSwitchControlSchemes
		{
			get
			{
				return this.m_NeverAutoSwitchControlSchemes;
			}
			set
			{
				if (this.m_NeverAutoSwitchControlSchemes == value)
				{
					return;
				}
				this.m_NeverAutoSwitchControlSchemes = value;
				if (this.m_Enabled)
				{
					if (!value && !this.m_OnUnpairedDeviceUsedHooked)
					{
						this.StartListeningForUnpairedDeviceActivity();
						return;
					}
					if (value && this.m_OnUnpairedDeviceUsedHooked)
					{
						this.StopListeningForUnpairedDeviceActivity();
					}
				}
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x060008FA RID: 2298 RVA: 0x0003247D File Offset: 0x0003067D
		// (set) Token: 0x060008FB RID: 2299 RVA: 0x00032485 File Offset: 0x00030685
		public InputActionMap currentActionMap
		{
			get
			{
				return this.m_CurrentActionMap;
			}
			set
			{
				InputActionMap currentActionMap = this.m_CurrentActionMap;
				this.m_CurrentActionMap = null;
				if (currentActionMap != null)
				{
					currentActionMap.Disable();
				}
				this.m_CurrentActionMap = value;
				InputActionMap currentActionMap2 = this.m_CurrentActionMap;
				if (currentActionMap2 == null)
				{
					return;
				}
				currentActionMap2.Enable();
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x060008FC RID: 2300 RVA: 0x000324B6 File Offset: 0x000306B6
		// (set) Token: 0x060008FD RID: 2301 RVA: 0x000324BE File Offset: 0x000306BE
		public string defaultActionMap
		{
			get
			{
				return this.m_DefaultActionMap;
			}
			set
			{
				this.m_DefaultActionMap = value;
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x060008FE RID: 2302 RVA: 0x000324C7 File Offset: 0x000306C7
		// (set) Token: 0x060008FF RID: 2303 RVA: 0x000324CF File Offset: 0x000306CF
		public PlayerNotifications notificationBehavior
		{
			get
			{
				return this.m_NotificationBehavior;
			}
			set
			{
				if (this.m_NotificationBehavior == value)
				{
					return;
				}
				if (this.m_Enabled)
				{
					this.UninitializeActions();
				}
				this.m_NotificationBehavior = value;
				if (this.m_Enabled)
				{
					this.InitializeActions();
				}
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000900 RID: 2304 RVA: 0x000324FE File Offset: 0x000306FE
		// (set) Token: 0x06000901 RID: 2305 RVA: 0x0003250B File Offset: 0x0003070B
		public ReadOnlyArray<PlayerInput.ActionEvent> actionEvents
		{
			get
			{
				return this.m_ActionEvents;
			}
			set
			{
				if (this.m_Enabled)
				{
					this.UninitializeActions();
				}
				this.m_ActionEvents = value.ToArray();
				if (this.m_Enabled)
				{
					this.InitializeActions();
				}
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000902 RID: 2306 RVA: 0x00032536 File Offset: 0x00030736
		public PlayerInput.DeviceLostEvent deviceLostEvent
		{
			get
			{
				if (this.m_DeviceLostEvent == null)
				{
					this.m_DeviceLostEvent = new PlayerInput.DeviceLostEvent();
				}
				return this.m_DeviceLostEvent;
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000903 RID: 2307 RVA: 0x00032551 File Offset: 0x00030751
		public PlayerInput.DeviceRegainedEvent deviceRegainedEvent
		{
			get
			{
				if (this.m_DeviceRegainedEvent == null)
				{
					this.m_DeviceRegainedEvent = new PlayerInput.DeviceRegainedEvent();
				}
				return this.m_DeviceRegainedEvent;
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000904 RID: 2308 RVA: 0x0003256C File Offset: 0x0003076C
		public PlayerInput.ControlsChangedEvent controlsChangedEvent
		{
			get
			{
				if (this.m_ControlsChangedEvent == null)
				{
					this.m_ControlsChangedEvent = new PlayerInput.ControlsChangedEvent();
				}
				return this.m_ControlsChangedEvent;
			}
		}

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x06000905 RID: 2309 RVA: 0x00032587 File Offset: 0x00030787
		// (remove) Token: 0x06000906 RID: 2310 RVA: 0x000325A3 File Offset: 0x000307A3
		public event Action<InputAction.CallbackContext> onActionTriggered
		{
			add
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.m_ActionTriggeredCallbacks.AddCallback(value);
			}
			remove
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.m_ActionTriggeredCallbacks.RemoveCallback(value);
			}
		}

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x06000907 RID: 2311 RVA: 0x000325BF File Offset: 0x000307BF
		// (remove) Token: 0x06000908 RID: 2312 RVA: 0x000325DB File Offset: 0x000307DB
		public event Action<PlayerInput> onDeviceLost
		{
			add
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.m_DeviceLostCallbacks.AddCallback(value);
			}
			remove
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.m_DeviceLostCallbacks.RemoveCallback(value);
			}
		}

		// Token: 0x1400001A RID: 26
		// (add) Token: 0x06000909 RID: 2313 RVA: 0x000325F7 File Offset: 0x000307F7
		// (remove) Token: 0x0600090A RID: 2314 RVA: 0x00032613 File Offset: 0x00030813
		public event Action<PlayerInput> onDeviceRegained
		{
			add
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.m_DeviceRegainedCallbacks.AddCallback(value);
			}
			remove
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.m_DeviceRegainedCallbacks.RemoveCallback(value);
			}
		}

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x0600090B RID: 2315 RVA: 0x0003262F File Offset: 0x0003082F
		// (remove) Token: 0x0600090C RID: 2316 RVA: 0x0003264B File Offset: 0x0003084B
		public event Action<PlayerInput> onControlsChanged
		{
			add
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.m_ControlsChangedCallbacks.AddCallback(value);
			}
			remove
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.m_ControlsChangedCallbacks.RemoveCallback(value);
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x0600090D RID: 2317 RVA: 0x00032667 File Offset: 0x00030867
		// (set) Token: 0x0600090E RID: 2318 RVA: 0x0003266F File Offset: 0x0003086F
		public Camera camera
		{
			get
			{
				return this.m_Camera;
			}
			set
			{
				this.m_Camera = value;
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x0600090F RID: 2319 RVA: 0x00032678 File Offset: 0x00030878
		// (set) Token: 0x06000910 RID: 2320 RVA: 0x00032680 File Offset: 0x00030880
		public InputSystemUIInputModule uiInputModule
		{
			get
			{
				return this.m_UIInputModule;
			}
			set
			{
				if (this.m_UIInputModule == value)
				{
					return;
				}
				if (this.m_UIInputModule != null && this.m_UIInputModule.actionsAsset == this.m_Actions)
				{
					this.m_UIInputModule.actionsAsset = null;
				}
				this.m_UIInputModule = value;
				if (this.m_UIInputModule != null && this.m_Actions != null)
				{
					this.m_UIInputModule.actionsAsset = this.m_Actions;
				}
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000911 RID: 2321 RVA: 0x00032702 File Offset: 0x00030902
		public InputUser user
		{
			get
			{
				return this.m_InputUser;
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000912 RID: 2322 RVA: 0x0003270C File Offset: 0x0003090C
		public ReadOnlyArray<InputDevice> devices
		{
			get
			{
				if (!this.m_InputUser.valid)
				{
					return default(ReadOnlyArray<InputDevice>);
				}
				return this.m_InputUser.pairedDevices;
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000913 RID: 2323 RVA: 0x0003273C File Offset: 0x0003093C
		public bool hasMissingRequiredDevices
		{
			get
			{
				return this.user.valid && this.user.hasMissingRequiredDevices;
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000914 RID: 2324 RVA: 0x00032769 File Offset: 0x00030969
		public static ReadOnlyArray<PlayerInput> all
		{
			get
			{
				return new ReadOnlyArray<PlayerInput>(PlayerInput.s_AllActivePlayers, 0, PlayerInput.s_AllActivePlayersCount);
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000915 RID: 2325 RVA: 0x0003277B File Offset: 0x0003097B
		public static bool isSinglePlayer
		{
			get
			{
				return PlayerInput.s_AllActivePlayersCount <= 1 && (PlayerInputManager.instance == null || !PlayerInputManager.instance.joiningEnabled);
			}
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x000327A4 File Offset: 0x000309A4
		public TDevice GetDevice<TDevice>() where TDevice : InputDevice
		{
			foreach (InputDevice inputDevice in this.devices)
			{
				TDevice tdevice = inputDevice as TDevice;
				if (tdevice != null)
				{
					return tdevice;
				}
			}
			return default(TDevice);
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x00032818 File Offset: 0x00030A18
		public void ActivateInput()
		{
			this.m_InputActive = true;
			if (this.m_CurrentActionMap == null && this.m_Actions != null && !string.IsNullOrEmpty(this.m_DefaultActionMap))
			{
				this.SwitchCurrentActionMap(this.m_DefaultActionMap);
				return;
			}
			InputActionMap currentActionMap = this.m_CurrentActionMap;
			if (currentActionMap == null)
			{
				return;
			}
			currentActionMap.Enable();
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x0003286C File Offset: 0x00030A6C
		public void DeactivateInput()
		{
			InputActionMap currentActionMap = this.m_CurrentActionMap;
			if (currentActionMap != null)
			{
				currentActionMap.Disable();
			}
			this.m_InputActive = false;
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x00032886 File Offset: 0x00030A86
		[Obsolete("Use DeactivateInput instead.")]
		public void PassivateInput()
		{
			this.DeactivateInput();
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x00032890 File Offset: 0x00030A90
		public bool SwitchCurrentControlScheme(params InputDevice[] devices)
		{
			if (devices == null)
			{
				throw new ArgumentNullException("devices");
			}
			if (this.actions == null)
			{
				throw new InvalidOperationException("Must set actions on PlayerInput in order to be able to switch control schemes");
			}
			InputControlScheme? inputControlScheme = InputControlScheme.FindControlSchemeForDevices<InputDevice[], ReadOnlyArray<InputControlScheme>>(devices, this.actions.controlSchemes, null, false);
			if (inputControlScheme == null)
			{
				return false;
			}
			InputControlScheme value = inputControlScheme.Value;
			this.SwitchControlSchemeInternal(ref value, devices);
			return true;
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x000328F8 File Offset: 0x00030AF8
		public void SwitchCurrentControlScheme(string controlScheme, params InputDevice[] devices)
		{
			if (string.IsNullOrEmpty(controlScheme))
			{
				throw new ArgumentNullException("controlScheme");
			}
			if (devices == null)
			{
				throw new ArgumentNullException("devices");
			}
			InputControlScheme inputControlScheme;
			this.user.FindControlScheme(controlScheme, out inputControlScheme);
			this.SwitchControlSchemeInternal(ref inputControlScheme, devices);
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x00032940 File Offset: 0x00030B40
		public void SwitchCurrentActionMap(string mapNameOrId)
		{
			if (!this.m_Enabled)
			{
				Debug.LogError("Cannot switch to actions '" + mapNameOrId + "'; input is not enabled", this);
				return;
			}
			if (this.m_Actions == null)
			{
				Debug.LogError("Cannot switch to actions '" + mapNameOrId + "'; no actions set on PlayerInput", this);
				return;
			}
			InputActionMap inputActionMap = this.m_Actions.FindActionMap(mapNameOrId, false);
			if (inputActionMap == null)
			{
				Debug.LogError(string.Format("Cannot find action map '{0}' in actions '{1}'", mapNameOrId, this.m_Actions), this);
				return;
			}
			this.currentActionMap = inputActionMap;
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x000329C4 File Offset: 0x00030BC4
		public static PlayerInput GetPlayerByIndex(int playerIndex)
		{
			for (int i = 0; i < PlayerInput.s_AllActivePlayersCount; i++)
			{
				if (PlayerInput.s_AllActivePlayers[i].playerIndex == playerIndex)
				{
					return PlayerInput.s_AllActivePlayers[i];
				}
			}
			return null;
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x000329FC File Offset: 0x00030BFC
		public static PlayerInput FindFirstPairedToDevice(InputDevice device)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			for (int i = 0; i < PlayerInput.s_AllActivePlayersCount; i++)
			{
				if (PlayerInput.s_AllActivePlayers[i].devices.ContainsReference(device))
				{
					return PlayerInput.s_AllActivePlayers[i];
				}
			}
			return null;
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x00032A44 File Offset: 0x00030C44
		public static PlayerInput Instantiate(GameObject prefab, int playerIndex = -1, string controlScheme = null, int splitScreenIndex = -1, InputDevice pairWithDevice = null)
		{
			if (prefab == null)
			{
				throw new ArgumentNullException("prefab");
			}
			PlayerInput.s_InitPlayerIndex = playerIndex;
			PlayerInput.s_InitSplitScreenIndex = splitScreenIndex;
			PlayerInput.s_InitControlScheme = controlScheme;
			if (pairWithDevice != null)
			{
				ArrayHelpers.AppendWithCapacity<InputDevice>(ref PlayerInput.s_InitPairWithDevices, ref PlayerInput.s_InitPairWithDevicesCount, pairWithDevice, 10);
			}
			return PlayerInput.DoInstantiate(prefab);
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x00032A98 File Offset: 0x00030C98
		public static PlayerInput Instantiate(GameObject prefab, int playerIndex = -1, string controlScheme = null, int splitScreenIndex = -1, params InputDevice[] pairWithDevices)
		{
			if (prefab == null)
			{
				throw new ArgumentNullException("prefab");
			}
			PlayerInput.s_InitPlayerIndex = playerIndex;
			PlayerInput.s_InitSplitScreenIndex = splitScreenIndex;
			PlayerInput.s_InitControlScheme = controlScheme;
			if (pairWithDevices != null)
			{
				for (int i = 0; i < pairWithDevices.Length; i++)
				{
					ArrayHelpers.AppendWithCapacity<InputDevice>(ref PlayerInput.s_InitPairWithDevices, ref PlayerInput.s_InitPairWithDevicesCount, pairWithDevices[i], 10);
				}
			}
			return PlayerInput.DoInstantiate(prefab);
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x00032AFC File Offset: 0x00030CFC
		private static PlayerInput DoInstantiate(GameObject prefab)
		{
			bool flag = PlayerInput.s_DestroyIfDeviceSetupUnsuccessful;
			GameObject gameObject;
			try
			{
				gameObject = Object.Instantiate<GameObject>(prefab);
				gameObject.SetActive(true);
			}
			finally
			{
				PlayerInput.s_InitPairWithDevicesCount = 0;
				if (PlayerInput.s_InitPairWithDevices != null)
				{
					Array.Clear(PlayerInput.s_InitPairWithDevices, 0, PlayerInput.s_InitPairWithDevicesCount);
				}
				PlayerInput.s_InitControlScheme = null;
				PlayerInput.s_InitPlayerIndex = -1;
				PlayerInput.s_InitSplitScreenIndex = -1;
				PlayerInput.s_DestroyIfDeviceSetupUnsuccessful = false;
			}
			PlayerInput componentInChildren = gameObject.GetComponentInChildren<PlayerInput>();
			if (componentInChildren == null)
			{
				Object.DestroyImmediate(gameObject);
				Debug.LogError("The GameObject does not have a PlayerInput component", prefab);
				return null;
			}
			if (flag && (!componentInChildren.user.valid || componentInChildren.hasMissingRequiredDevices))
			{
				Object.DestroyImmediate(gameObject);
				return null;
			}
			return componentInChildren;
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x00032BAC File Offset: 0x00030DAC
		private void InitializeActions()
		{
			if (this.m_ActionsInitialized)
			{
				return;
			}
			if (this.m_Actions == null)
			{
				return;
			}
			for (int i = 0; i < PlayerInput.s_AllActivePlayersCount; i++)
			{
				if (PlayerInput.s_AllActivePlayers[i].m_Actions == this.m_Actions && PlayerInput.s_AllActivePlayers[i] != this)
				{
					InputActionAsset actions = this.m_Actions;
					this.m_Actions = Object.Instantiate<InputActionAsset>(this.m_Actions);
					for (int j = 0; j < actions.actionMaps.Count; j++)
					{
						for (int k = 0; k < actions.actionMaps[j].bindings.Count; k++)
						{
							this.m_Actions.actionMaps[j].ApplyBindingOverride(k, actions.actionMaps[j].bindings[k]);
						}
					}
					break;
				}
			}
			if (this.uiInputModule != null)
			{
				this.uiInputModule.actionsAsset = this.m_Actions;
			}
			switch (this.m_NotificationBehavior)
			{
			case PlayerNotifications.SendMessages:
			case PlayerNotifications.BroadcastMessages:
				this.InstallOnActionTriggeredHook();
				if (this.m_ActionMessageNames == null)
				{
					this.CacheMessageNames();
				}
				break;
			case PlayerNotifications.InvokeUnityEvents:
				if (this.m_ActionEvents != null)
				{
					foreach (PlayerInput.ActionEvent actionEvent in this.m_ActionEvents)
					{
						string actionId = actionEvent.actionId;
						if (!string.IsNullOrEmpty(actionId))
						{
							InputAction inputAction = this.m_Actions.FindAction(actionId, false);
							if (inputAction != null)
							{
								inputAction.performed += actionEvent.Invoke;
								inputAction.canceled += actionEvent.Invoke;
								inputAction.started += actionEvent.Invoke;
							}
						}
					}
				}
				break;
			case PlayerNotifications.InvokeCSharpEvents:
				this.InstallOnActionTriggeredHook();
				break;
			}
			this.m_ActionsInitialized = true;
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x00032DAC File Offset: 0x00030FAC
		private void UninitializeActions()
		{
			if (!this.m_ActionsInitialized)
			{
				return;
			}
			if (this.m_Actions == null)
			{
				return;
			}
			this.UninstallOnActionTriggeredHook();
			if (this.m_NotificationBehavior == PlayerNotifications.InvokeUnityEvents && this.m_ActionEvents != null)
			{
				foreach (PlayerInput.ActionEvent actionEvent in this.m_ActionEvents)
				{
					string actionId = actionEvent.actionId;
					if (!string.IsNullOrEmpty(actionId))
					{
						InputAction inputAction = this.m_Actions.FindAction(actionId, false);
						if (inputAction != null)
						{
							inputAction.performed -= actionEvent.Invoke;
							inputAction.canceled -= actionEvent.Invoke;
							inputAction.started -= actionEvent.Invoke;
						}
					}
				}
			}
			this.m_CurrentActionMap = null;
			this.m_ActionsInitialized = false;
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x00032E6C File Offset: 0x0003106C
		private void InstallOnActionTriggeredHook()
		{
			if (this.m_ActionTriggeredDelegate == null)
			{
				this.m_ActionTriggeredDelegate = new Action<InputAction.CallbackContext>(this.OnActionTriggered);
			}
			foreach (InputActionMap inputActionMap in this.m_Actions.actionMaps)
			{
				inputActionMap.actionTriggered += this.m_ActionTriggeredDelegate;
			}
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x00032EE4 File Offset: 0x000310E4
		private void UninstallOnActionTriggeredHook()
		{
			if (this.m_ActionTriggeredDelegate != null)
			{
				foreach (InputActionMap inputActionMap in this.m_Actions.actionMaps)
				{
					inputActionMap.actionTriggered -= this.m_ActionTriggeredDelegate;
				}
			}
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x00032F4C File Offset: 0x0003114C
		private void OnActionTriggered(InputAction.CallbackContext context)
		{
			if (!this.m_InputActive)
			{
				return;
			}
			PlayerNotifications notificationBehavior = this.m_NotificationBehavior;
			if (notificationBehavior > PlayerNotifications.BroadcastMessages)
			{
				if (notificationBehavior == PlayerNotifications.InvokeCSharpEvents)
				{
					DelegateHelpers.InvokeCallbacksSafe<InputAction.CallbackContext>(ref this.m_ActionTriggeredCallbacks, context, "PlayerInput.onActionTriggered", null);
					return;
				}
			}
			else
			{
				InputAction action = context.action;
				if (!context.performed && (!context.canceled || action.type != InputActionType.Value))
				{
					return;
				}
				if (this.m_ActionMessageNames == null)
				{
					this.CacheMessageNames();
				}
				string text = this.m_ActionMessageNames[action.m_Id];
				if (this.m_InputValueObject == null)
				{
					this.m_InputValueObject = new InputValue();
				}
				this.m_InputValueObject.m_Context = new InputAction.CallbackContext?(context);
				if (this.m_NotificationBehavior == PlayerNotifications.BroadcastMessages)
				{
					base.BroadcastMessage(text, this.m_InputValueObject, SendMessageOptions.DontRequireReceiver);
				}
				else
				{
					base.SendMessage(text, this.m_InputValueObject, SendMessageOptions.DontRequireReceiver);
				}
				this.m_InputValueObject.m_Context = null;
			}
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x00033028 File Offset: 0x00031228
		private void CacheMessageNames()
		{
			if (this.m_Actions == null)
			{
				return;
			}
			if (this.m_ActionMessageNames != null)
			{
				this.m_ActionMessageNames.Clear();
			}
			else
			{
				this.m_ActionMessageNames = new Dictionary<string, string>();
			}
			foreach (InputAction inputAction in this.m_Actions)
			{
				inputAction.MakeSureIdIsInPlace();
				string text = CSharpCodeHelpers.MakeTypeName(inputAction.name, "");
				this.m_ActionMessageNames[inputAction.m_Id] = "On" + text;
			}
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x000330D4 File Offset: 0x000312D4
		private void ClearCaches()
		{
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x000330D8 File Offset: 0x000312D8
		private void AssignUserAndDevices()
		{
			if (this.m_InputUser.valid)
			{
				this.m_InputUser.UnpairDevices();
			}
			if (!(this.m_Actions == null))
			{
				if (this.m_Actions.controlSchemes.Count > 0)
				{
					if (!string.IsNullOrEmpty(PlayerInput.s_InitControlScheme))
					{
						InputControlScheme? inputControlScheme = this.m_Actions.FindControlScheme(PlayerInput.s_InitControlScheme);
						if (inputControlScheme == null)
						{
							Debug.LogError(string.Format("No control scheme '{0}' in '{1}'", PlayerInput.s_InitControlScheme, this.m_Actions), this);
						}
						else
						{
							this.TryToActivateControlScheme(inputControlScheme.Value);
						}
					}
					else if (!string.IsNullOrEmpty(this.m_DefaultControlScheme))
					{
						InputControlScheme? inputControlScheme2 = this.m_Actions.FindControlScheme(this.m_DefaultControlScheme);
						if (inputControlScheme2 == null)
						{
							Debug.LogError(string.Format("Cannot find default control scheme '{0}' in '{1}'", this.m_DefaultControlScheme, this.m_Actions), this);
						}
						else
						{
							this.TryToActivateControlScheme(inputControlScheme2.Value);
						}
					}
					if (PlayerInput.s_InitPairWithDevicesCount > 0 && (!this.m_InputUser.valid || this.m_InputUser.controlScheme == null))
					{
						InputControlScheme? inputControlScheme3 = InputControlScheme.FindControlSchemeForDevices<ReadOnlyArray<InputDevice>, ReadOnlyArray<InputControlScheme>>(new ReadOnlyArray<InputDevice>(PlayerInput.s_InitPairWithDevices, 0, PlayerInput.s_InitPairWithDevicesCount), this.m_Actions.controlSchemes, null, true);
						if (inputControlScheme3 != null)
						{
							this.TryToActivateControlScheme(inputControlScheme3.Value);
							goto IL_029D;
						}
						goto IL_029D;
					}
					else
					{
						if ((this.m_InputUser.valid && this.m_InputUser.controlScheme != null) || !string.IsNullOrEmpty(PlayerInput.s_InitControlScheme))
						{
							goto IL_029D;
						}
						using (InputControlList<InputDevice> unpairedInputDevices = InputUser.GetUnpairedInputDevices())
						{
							InputControlScheme? inputControlScheme4 = InputControlScheme.FindControlSchemeForDevices<InputControlList<InputDevice>, ReadOnlyArray<InputControlScheme>>(unpairedInputDevices, this.m_Actions.controlSchemes, null, false);
							if (inputControlScheme4 != null)
							{
								this.TryToActivateControlScheme(inputControlScheme4.Value);
							}
							goto IL_029D;
						}
					}
				}
				if (PlayerInput.s_InitPairWithDevicesCount > 0)
				{
					for (int i = 0; i < PlayerInput.s_InitPairWithDevicesCount; i++)
					{
						this.m_InputUser = InputUser.PerformPairingWithDevice(PlayerInput.s_InitPairWithDevices[i], this.m_InputUser, InputUserPairingOptions.None);
					}
				}
				else
				{
					using (InputControlList<InputDevice> unpairedInputDevices2 = InputUser.GetUnpairedInputDevices())
					{
						for (int j = 0; j < unpairedInputDevices2.Count; j++)
						{
							InputDevice inputDevice = unpairedInputDevices2[j];
							if (this.HaveBindingForDevice(inputDevice))
							{
								this.m_InputUser = InputUser.PerformPairingWithDevice(inputDevice, this.m_InputUser, InputUserPairingOptions.None);
							}
						}
					}
				}
				IL_029D:
				if (this.m_InputUser.valid)
				{
					this.m_InputUser.AssociateActionsWithUser(this.m_Actions);
				}
				return;
			}
			if (PlayerInput.s_InitPairWithDevicesCount > 0)
			{
				for (int k = 0; k < PlayerInput.s_InitPairWithDevicesCount; k++)
				{
					this.m_InputUser = InputUser.PerformPairingWithDevice(PlayerInput.s_InitPairWithDevices[k], this.m_InputUser, InputUserPairingOptions.None);
				}
				return;
			}
			this.m_InputUser = default(InputUser);
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x000333BC File Offset: 0x000315BC
		private bool HaveBindingForDevice(InputDevice device)
		{
			if (this.m_Actions == null)
			{
				return false;
			}
			ReadOnlyArray<InputActionMap> actionMaps = this.m_Actions.actionMaps;
			for (int i = 0; i < actionMaps.Count; i++)
			{
				if (actionMaps[i].IsUsableWithDevice(device))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x0003340C File Offset: 0x0003160C
		private void UnassignUserAndDevices()
		{
			if (this.m_InputUser.valid)
			{
				this.m_InputUser.UnpairDevicesAndRemoveUser();
			}
			if (this.m_Actions != null)
			{
				this.m_Actions.devices = null;
			}
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x00033454 File Offset: 0x00031654
		private bool TryToActivateControlScheme(InputControlScheme controlScheme)
		{
			if (PlayerInput.s_InitPairWithDevicesCount > 0)
			{
				for (int i = 0; i < PlayerInput.s_InitPairWithDevicesCount; i++)
				{
					InputDevice inputDevice = PlayerInput.s_InitPairWithDevices[i];
					if (!controlScheme.SupportsDevice(inputDevice))
					{
						return false;
					}
				}
				for (int j = 0; j < PlayerInput.s_InitPairWithDevicesCount; j++)
				{
					InputDevice inputDevice2 = PlayerInput.s_InitPairWithDevices[j];
					this.m_InputUser = InputUser.PerformPairingWithDevice(inputDevice2, this.m_InputUser, InputUserPairingOptions.None);
				}
			}
			if (!this.m_InputUser.valid)
			{
				this.m_InputUser = InputUser.CreateUserWithoutPairedDevices();
			}
			this.m_InputUser.ActivateControlScheme(controlScheme).AndPairRemainingDevices();
			if (this.user.hasMissingRequiredDevices)
			{
				this.m_InputUser.ActivateControlScheme(null);
				this.m_InputUser.UnpairDevices();
				return false;
			}
			return true;
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x00033514 File Offset: 0x00031714
		private void AssignPlayerIndex()
		{
			if (PlayerInput.s_InitPlayerIndex != -1)
			{
				this.m_PlayerIndex = PlayerInput.s_InitPlayerIndex;
				return;
			}
			int num = int.MaxValue;
			int num2 = int.MinValue;
			for (int i = 0; i < PlayerInput.s_AllActivePlayersCount; i++)
			{
				int playerIndex = PlayerInput.s_AllActivePlayers[i].playerIndex;
				num = Math.Min(num, playerIndex);
				num2 = Math.Max(num2, playerIndex);
			}
			if (num != 2147483647 && num > 0)
			{
				this.m_PlayerIndex = num - 1;
				return;
			}
			if (num2 != -2147483648)
			{
				for (int j = num; j < num2; j++)
				{
					if (PlayerInput.GetPlayerByIndex(j) == null)
					{
						this.m_PlayerIndex = j;
						return;
					}
				}
				this.m_PlayerIndex = num2 + 1;
				return;
			}
			this.m_PlayerIndex = 0;
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x000335C8 File Offset: 0x000317C8
		private void OnEnable()
		{
			this.m_Enabled = true;
			using (InputActionRebindingExtensions.DeferBindingResolution())
			{
				this.AssignPlayerIndex();
				this.InitializeActions();
				this.AssignUserAndDevices();
				this.ActivateInput();
			}
			if (PlayerInput.s_InitSplitScreenIndex >= 0)
			{
				this.m_SplitScreenIndex = this.splitScreenIndex;
			}
			else
			{
				this.m_SplitScreenIndex = this.playerIndex;
			}
			ArrayHelpers.AppendWithCapacity<PlayerInput>(ref PlayerInput.s_AllActivePlayers, ref PlayerInput.s_AllActivePlayersCount, this, 10);
			for (int i = 1; i < PlayerInput.s_AllActivePlayersCount; i++)
			{
				int num = i;
				while (num > 0 && PlayerInput.s_AllActivePlayers[num - 1].playerIndex > PlayerInput.s_AllActivePlayers[num].playerIndex)
				{
					PlayerInput.s_AllActivePlayers.SwapElements(num, num - 1);
					num--;
				}
			}
			if (PlayerInput.s_AllActivePlayersCount == 1)
			{
				if (PlayerInput.s_UserChangeDelegate == null)
				{
					PlayerInput.s_UserChangeDelegate = new Action<InputUser, InputUserChange, InputDevice>(PlayerInput.OnUserChange);
				}
				InputUser.onChange += PlayerInput.s_UserChangeDelegate;
			}
			if (PlayerInput.isSinglePlayer)
			{
				if (this.m_Actions != null && this.m_Actions.controlSchemes.Count == 0)
				{
					this.StartListeningForDeviceChanges();
				}
				else if (!this.neverAutoSwitchControlSchemes)
				{
					this.StartListeningForUnpairedDeviceActivity();
				}
			}
			this.HandleControlsChanged();
			PlayerInputManager instance = PlayerInputManager.instance;
			if (instance == null)
			{
				return;
			}
			instance.NotifyPlayerJoined(this);
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x00033714 File Offset: 0x00031914
		private void StartListeningForUnpairedDeviceActivity()
		{
			if (this.m_OnUnpairedDeviceUsedHooked)
			{
				return;
			}
			if (this.m_UnpairedDeviceUsedDelegate == null)
			{
				this.m_UnpairedDeviceUsedDelegate = new Action<InputControl, InputEventPtr>(this.OnUnpairedDeviceUsed);
			}
			if (this.m_PreFilterUnpairedDeviceUsedDelegate == null)
			{
				this.m_PreFilterUnpairedDeviceUsedDelegate = new Func<InputDevice, InputEventPtr, bool>(PlayerInput.OnPreFilterUnpairedDeviceUsed);
			}
			InputUser.onUnpairedDeviceUsed += this.m_UnpairedDeviceUsedDelegate;
			InputUser.onPrefilterUnpairedDeviceActivity += this.m_PreFilterUnpairedDeviceUsedDelegate;
			InputUser.listenForUnpairedDeviceActivity++;
			this.m_OnUnpairedDeviceUsedHooked = true;
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x00033787 File Offset: 0x00031987
		private void StopListeningForUnpairedDeviceActivity()
		{
			if (!this.m_OnUnpairedDeviceUsedHooked)
			{
				return;
			}
			InputUser.onUnpairedDeviceUsed -= this.m_UnpairedDeviceUsedDelegate;
			InputUser.onPrefilterUnpairedDeviceActivity -= this.m_PreFilterUnpairedDeviceUsedDelegate;
			InputUser.listenForUnpairedDeviceActivity--;
			this.m_OnUnpairedDeviceUsedHooked = false;
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x000337BB File Offset: 0x000319BB
		private void StartListeningForDeviceChanges()
		{
			if (this.m_OnDeviceChangeHooked)
			{
				return;
			}
			if (this.m_DeviceChangeDelegate == null)
			{
				this.m_DeviceChangeDelegate = new Action<InputDevice, InputDeviceChange>(this.OnDeviceChange);
			}
			InputSystem.onDeviceChange += this.m_DeviceChangeDelegate;
			this.m_OnDeviceChangeHooked = true;
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x000337F2 File Offset: 0x000319F2
		private void StopListeningForDeviceChanges()
		{
			if (!this.m_OnDeviceChangeHooked)
			{
				return;
			}
			InputSystem.onDeviceChange -= this.m_DeviceChangeDelegate;
			this.m_OnDeviceChangeHooked = false;
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x00033810 File Offset: 0x00031A10
		private void OnDisable()
		{
			this.m_Enabled = false;
			int num = PlayerInput.s_AllActivePlayers.IndexOfReference(this, PlayerInput.s_AllActivePlayersCount);
			if (num != -1)
			{
				PlayerInput.s_AllActivePlayers.EraseAtWithCapacity(ref PlayerInput.s_AllActivePlayersCount, num);
			}
			if (PlayerInput.s_AllActivePlayersCount == 0 && PlayerInput.s_UserChangeDelegate != null)
			{
				InputUser.onChange -= PlayerInput.s_UserChangeDelegate;
			}
			this.StopListeningForUnpairedDeviceActivity();
			this.StopListeningForDeviceChanges();
			PlayerInputManager instance = PlayerInputManager.instance;
			if (instance != null)
			{
				instance.NotifyPlayerLeft(this);
			}
			using (InputActionRebindingExtensions.DeferBindingResolution())
			{
				this.DeactivateInput();
				this.UnassignUserAndDevices();
				this.UninitializeActions();
			}
			this.m_PlayerIndex = -1;
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x000338BC File Offset: 0x00031ABC
		public void DebugLogAction(InputAction.CallbackContext context)
		{
			Debug.Log(context.ToString());
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x000338D0 File Offset: 0x00031AD0
		private void HandleDeviceLost()
		{
			switch (this.m_NotificationBehavior)
			{
			case PlayerNotifications.SendMessages:
				base.SendMessage("OnDeviceLost", this, SendMessageOptions.DontRequireReceiver);
				return;
			case PlayerNotifications.BroadcastMessages:
				base.BroadcastMessage("OnDeviceLost", this, SendMessageOptions.DontRequireReceiver);
				return;
			case PlayerNotifications.InvokeUnityEvents:
			{
				PlayerInput.DeviceLostEvent deviceLostEvent = this.m_DeviceLostEvent;
				if (deviceLostEvent == null)
				{
					return;
				}
				deviceLostEvent.Invoke(this);
				return;
			}
			case PlayerNotifications.InvokeCSharpEvents:
				DelegateHelpers.InvokeCallbacksSafe<PlayerInput>(ref this.m_DeviceLostCallbacks, this, "onDeviceLost", null);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x0003393C File Offset: 0x00031B3C
		private void HandleDeviceRegained()
		{
			switch (this.m_NotificationBehavior)
			{
			case PlayerNotifications.SendMessages:
				base.SendMessage("OnDeviceRegained", this, SendMessageOptions.DontRequireReceiver);
				return;
			case PlayerNotifications.BroadcastMessages:
				base.BroadcastMessage("OnDeviceRegained", this, SendMessageOptions.DontRequireReceiver);
				return;
			case PlayerNotifications.InvokeUnityEvents:
			{
				PlayerInput.DeviceRegainedEvent deviceRegainedEvent = this.m_DeviceRegainedEvent;
				if (deviceRegainedEvent == null)
				{
					return;
				}
				deviceRegainedEvent.Invoke(this);
				return;
			}
			case PlayerNotifications.InvokeCSharpEvents:
				DelegateHelpers.InvokeCallbacksSafe<PlayerInput>(ref this.m_DeviceRegainedCallbacks, this, "onDeviceRegained", null);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x000339A8 File Offset: 0x00031BA8
		private void HandleControlsChanged()
		{
			switch (this.m_NotificationBehavior)
			{
			case PlayerNotifications.SendMessages:
				base.SendMessage("OnControlsChanged", this, SendMessageOptions.DontRequireReceiver);
				return;
			case PlayerNotifications.BroadcastMessages:
				base.BroadcastMessage("OnControlsChanged", this, SendMessageOptions.DontRequireReceiver);
				return;
			case PlayerNotifications.InvokeUnityEvents:
			{
				PlayerInput.ControlsChangedEvent controlsChangedEvent = this.m_ControlsChangedEvent;
				if (controlsChangedEvent == null)
				{
					return;
				}
				controlsChangedEvent.Invoke(this);
				return;
			}
			case PlayerNotifications.InvokeCSharpEvents:
				DelegateHelpers.InvokeCallbacksSafe<PlayerInput>(ref this.m_ControlsChangedCallbacks, this, "onControlsChanged", null);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x00033A14 File Offset: 0x00031C14
		private static void OnUserChange(InputUser user, InputUserChange change, InputDevice device)
		{
			if (change - InputUserChange.DeviceLost <= 1)
			{
				for (int i = 0; i < PlayerInput.s_AllActivePlayersCount; i++)
				{
					PlayerInput playerInput = PlayerInput.s_AllActivePlayers[i];
					if (playerInput.m_InputUser == user)
					{
						if (change == InputUserChange.DeviceLost)
						{
							playerInput.HandleDeviceLost();
						}
						else if (change == InputUserChange.DeviceRegained)
						{
							playerInput.HandleDeviceRegained();
						}
					}
				}
				return;
			}
			if (change != InputUserChange.ControlsChanged)
			{
				return;
			}
			for (int j = 0; j < PlayerInput.s_AllActivePlayersCount; j++)
			{
				PlayerInput playerInput2 = PlayerInput.s_AllActivePlayers[j];
				if (playerInput2.m_InputUser == user)
				{
					playerInput2.HandleControlsChanged();
				}
			}
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x00033A98 File Offset: 0x00031C98
		private static bool OnPreFilterUnpairedDeviceUsed(InputDevice device, InputEventPtr eventPtr)
		{
			InputActionAsset actions = PlayerInput.all[0].actions;
			return actions != null && actions.IsUsableWithDevice(device);
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x00033ACC File Offset: 0x00031CCC
		private void OnUnpairedDeviceUsed(InputControl control, InputEventPtr eventPtr)
		{
			if (!PlayerInput.isSinglePlayer || this.neverAutoSwitchControlSchemes)
			{
				return;
			}
			PlayerInput playerInput = PlayerInput.all[0];
			if (playerInput.m_Actions == null)
			{
				return;
			}
			InputDevice device = control.device;
			using (InputActionRebindingExtensions.DeferBindingResolution())
			{
				using (InputControlList<InputDevice> unpairedInputDevices = InputUser.GetUnpairedInputDevices())
				{
					if (unpairedInputDevices.Count > 1)
					{
						int num = unpairedInputDevices.IndexOf(device);
						unpairedInputDevices.SwapElements(0, num);
					}
					ReadOnlyArray<InputDevice> devices = playerInput.devices;
					for (int i = 0; i < devices.Count; i++)
					{
						unpairedInputDevices.Add(devices[i]);
					}
					InputControlScheme inputControlScheme;
					InputControlScheme.MatchResult matchResult;
					if (InputControlScheme.FindControlSchemeForDevices<InputControlList<InputDevice>, ReadOnlyArray<InputControlScheme>>(unpairedInputDevices, playerInput.m_Actions.controlSchemes, out inputControlScheme, out matchResult, device, false))
					{
						try
						{
							bool valid = playerInput.user.valid;
							if (valid)
							{
								playerInput.user.UnpairDevices();
							}
							InputControlList<InputDevice> devices2 = matchResult.devices;
							for (int j = 0; j < devices2.Count; j++)
							{
								playerInput.m_InputUser = InputUser.PerformPairingWithDevice(devices2[j], playerInput.m_InputUser, InputUserPairingOptions.None);
								if (!valid && playerInput.actions != null)
								{
									playerInput.m_InputUser.AssociateActionsWithUser(playerInput.actions);
								}
							}
							playerInput.user.ActivateControlScheme(inputControlScheme);
						}
						finally
						{
							matchResult.Dispose();
						}
					}
				}
			}
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x00033C88 File Offset: 0x00031E88
		private void OnDeviceChange(InputDevice device, InputDeviceChange change)
		{
			if (change == InputDeviceChange.Added && PlayerInput.isSinglePlayer && this.m_Actions != null && this.m_Actions.controlSchemes.Count == 0 && this.HaveBindingForDevice(device) && this.m_InputUser.valid)
			{
				InputUser.PerformPairingWithDevice(device, this.m_InputUser, InputUserPairingOptions.None);
			}
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x00033CE8 File Offset: 0x00031EE8
		private void SwitchControlSchemeInternal(ref InputControlScheme controlScheme, params InputDevice[] devices)
		{
			using (InputActionRebindingExtensions.DeferBindingResolution())
			{
				for (int i = this.user.pairedDevices.Count - 1; i >= 0; i--)
				{
					if (!devices.ContainsReference(this.user.pairedDevices[i]))
					{
						this.user.UnpairDevice(this.user.pairedDevices[i]);
					}
				}
				foreach (InputDevice inputDevice in devices)
				{
					if (!this.user.pairedDevices.ContainsReference(inputDevice))
					{
						InputUser.PerformPairingWithDevice(inputDevice, this.user, InputUserPairingOptions.None);
					}
				}
				if (this.user.controlScheme == null || !this.user.controlScheme.Value.Equals(controlScheme))
				{
					this.user.ActivateControlScheme(controlScheme);
				}
			}
		}

		// Token: 0x040002B8 RID: 696
		public const string DeviceLostMessage = "OnDeviceLost";

		// Token: 0x040002B9 RID: 697
		public const string DeviceRegainedMessage = "OnDeviceRegained";

		// Token: 0x040002BA RID: 698
		public const string ControlsChangedMessage = "OnControlsChanged";

		// Token: 0x040002BB RID: 699
		[Tooltip("Input actions associated with the player.")]
		[SerializeField]
		internal InputActionAsset m_Actions;

		// Token: 0x040002BC RID: 700
		[Tooltip("Determine how notifications should be sent when an input-related event associated with the player happens.")]
		[SerializeField]
		internal PlayerNotifications m_NotificationBehavior;

		// Token: 0x040002BD RID: 701
		[Tooltip("UI InputModule that should have it's input actions synchronized to this PlayerInput's actions.")]
		[SerializeField]
		internal InputSystemUIInputModule m_UIInputModule;

		// Token: 0x040002BE RID: 702
		[Tooltip("Event that is triggered when the PlayerInput loses a paired device (e.g. its battery runs out).")]
		[SerializeField]
		internal PlayerInput.DeviceLostEvent m_DeviceLostEvent;

		// Token: 0x040002BF RID: 703
		[SerializeField]
		internal PlayerInput.DeviceRegainedEvent m_DeviceRegainedEvent;

		// Token: 0x040002C0 RID: 704
		[SerializeField]
		internal PlayerInput.ControlsChangedEvent m_ControlsChangedEvent;

		// Token: 0x040002C1 RID: 705
		[SerializeField]
		internal PlayerInput.ActionEvent[] m_ActionEvents;

		// Token: 0x040002C2 RID: 706
		[SerializeField]
		internal bool m_NeverAutoSwitchControlSchemes;

		// Token: 0x040002C3 RID: 707
		[SerializeField]
		internal string m_DefaultControlScheme;

		// Token: 0x040002C4 RID: 708
		[SerializeField]
		internal string m_DefaultActionMap;

		// Token: 0x040002C5 RID: 709
		[SerializeField]
		internal int m_SplitScreenIndex = -1;

		// Token: 0x040002C6 RID: 710
		[Tooltip("Reference to the player's view camera. Note that this is only required when using split-screen and/or per-player UIs. Otherwise it is safe to leave this property uninitialized.")]
		[SerializeField]
		internal Camera m_Camera;

		// Token: 0x040002C7 RID: 711
		[NonSerialized]
		private InputValue m_InputValueObject;

		// Token: 0x040002C8 RID: 712
		[NonSerialized]
		internal InputActionMap m_CurrentActionMap;

		// Token: 0x040002C9 RID: 713
		[NonSerialized]
		private int m_PlayerIndex = -1;

		// Token: 0x040002CA RID: 714
		[NonSerialized]
		private bool m_InputActive;

		// Token: 0x040002CB RID: 715
		[NonSerialized]
		private bool m_Enabled;

		// Token: 0x040002CC RID: 716
		[NonSerialized]
		internal bool m_ActionsInitialized;

		// Token: 0x040002CD RID: 717
		[NonSerialized]
		private Dictionary<string, string> m_ActionMessageNames;

		// Token: 0x040002CE RID: 718
		[NonSerialized]
		private InputUser m_InputUser;

		// Token: 0x040002CF RID: 719
		[NonSerialized]
		private Action<InputAction.CallbackContext> m_ActionTriggeredDelegate;

		// Token: 0x040002D0 RID: 720
		[NonSerialized]
		private CallbackArray<Action<PlayerInput>> m_DeviceLostCallbacks;

		// Token: 0x040002D1 RID: 721
		[NonSerialized]
		private CallbackArray<Action<PlayerInput>> m_DeviceRegainedCallbacks;

		// Token: 0x040002D2 RID: 722
		[NonSerialized]
		private CallbackArray<Action<PlayerInput>> m_ControlsChangedCallbacks;

		// Token: 0x040002D3 RID: 723
		[NonSerialized]
		private CallbackArray<Action<InputAction.CallbackContext>> m_ActionTriggeredCallbacks;

		// Token: 0x040002D4 RID: 724
		[NonSerialized]
		private Action<InputControl, InputEventPtr> m_UnpairedDeviceUsedDelegate;

		// Token: 0x040002D5 RID: 725
		[NonSerialized]
		private Func<InputDevice, InputEventPtr, bool> m_PreFilterUnpairedDeviceUsedDelegate;

		// Token: 0x040002D6 RID: 726
		[NonSerialized]
		private bool m_OnUnpairedDeviceUsedHooked;

		// Token: 0x040002D7 RID: 727
		[NonSerialized]
		private Action<InputDevice, InputDeviceChange> m_DeviceChangeDelegate;

		// Token: 0x040002D8 RID: 728
		[NonSerialized]
		private bool m_OnDeviceChangeHooked;

		// Token: 0x040002D9 RID: 729
		internal static int s_AllActivePlayersCount;

		// Token: 0x040002DA RID: 730
		internal static PlayerInput[] s_AllActivePlayers;

		// Token: 0x040002DB RID: 731
		private static Action<InputUser, InputUserChange, InputDevice> s_UserChangeDelegate;

		// Token: 0x040002DC RID: 732
		private static int s_InitPairWithDevicesCount;

		// Token: 0x040002DD RID: 733
		private static InputDevice[] s_InitPairWithDevices;

		// Token: 0x040002DE RID: 734
		private static int s_InitPlayerIndex = -1;

		// Token: 0x040002DF RID: 735
		private static int s_InitSplitScreenIndex = -1;

		// Token: 0x040002E0 RID: 736
		private static string s_InitControlScheme;

		// Token: 0x040002E1 RID: 737
		internal static bool s_DestroyIfDeviceSetupUnsuccessful;

		// Token: 0x020001B2 RID: 434
		[Serializable]
		public class ActionEvent : UnityEvent<InputAction.CallbackContext>
		{
			// Token: 0x17000561 RID: 1377
			// (get) Token: 0x060013F8 RID: 5112 RVA: 0x0005C633 File Offset: 0x0005A833
			public string actionId
			{
				get
				{
					return this.m_ActionId;
				}
			}

			// Token: 0x17000562 RID: 1378
			// (get) Token: 0x060013F9 RID: 5113 RVA: 0x0005C63B File Offset: 0x0005A83B
			public string actionName
			{
				get
				{
					return this.m_ActionName;
				}
			}

			// Token: 0x060013FA RID: 5114 RVA: 0x0005C643 File Offset: 0x0005A843
			public ActionEvent()
			{
			}

			// Token: 0x060013FB RID: 5115 RVA: 0x0005C64C File Offset: 0x0005A84C
			public ActionEvent(InputAction action)
			{
				if (action == null)
				{
					throw new ArgumentNullException("action");
				}
				if (action.isSingletonAction)
				{
					throw new ArgumentException(string.Format("Action must be part of an asset (given action '{0}' is a singleton)", action));
				}
				if (action.actionMap.asset == null)
				{
					throw new ArgumentException(string.Format("Action must be part of an asset (given action '{0}' is not)", action));
				}
				this.m_ActionId = action.id.ToString();
				this.m_ActionName = action.actionMap.name + "/" + action.name;
			}

			// Token: 0x060013FC RID: 5116 RVA: 0x0005C6E5 File Offset: 0x0005A8E5
			public ActionEvent(Guid actionGUID, string name = null)
			{
				this.m_ActionId = actionGUID.ToString();
				this.m_ActionName = name;
			}

			// Token: 0x040008E4 RID: 2276
			[SerializeField]
			private string m_ActionId;

			// Token: 0x040008E5 RID: 2277
			[SerializeField]
			private string m_ActionName;
		}

		// Token: 0x020001B3 RID: 435
		[Serializable]
		public class DeviceLostEvent : UnityEvent<PlayerInput>
		{
		}

		// Token: 0x020001B4 RID: 436
		[Serializable]
		public class DeviceRegainedEvent : UnityEvent<PlayerInput>
		{
		}

		// Token: 0x020001B5 RID: 437
		[Serializable]
		public class ControlsChangedEvent : UnityEvent<PlayerInput>
		{
		}
	}
}
