using System;
using UnityEngine.Events;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Users;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem
{
	// Token: 0x0200005C RID: 92
	[AddComponentMenu("Input/Player Input Manager")]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.inputsystem@1.5/manual/PlayerInputManager.html")]
	public class PlayerInputManager : MonoBehaviour
	{
		// Token: 0x1700024F RID: 591
		// (get) Token: 0x0600093D RID: 2365 RVA: 0x00033E0C File Offset: 0x0003200C
		// (set) Token: 0x0600093E RID: 2366 RVA: 0x00033E14 File Offset: 0x00032014
		public bool splitScreen
		{
			get
			{
				return this.m_SplitScreen;
			}
			set
			{
				if (this.m_SplitScreen == value)
				{
					return;
				}
				this.m_SplitScreen = value;
				if (!this.m_SplitScreen)
				{
					using (ReadOnlyArray<PlayerInput>.Enumerator enumerator = PlayerInput.all.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							PlayerInput playerInput = enumerator.Current;
							Camera camera = playerInput.camera;
							if (camera != null)
							{
								camera.rect = new Rect(0f, 0f, 1f, 1f);
							}
						}
						return;
					}
				}
				this.UpdateSplitScreen();
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x0600093F RID: 2367 RVA: 0x00033EB0 File Offset: 0x000320B0
		public bool maintainAspectRatioInSplitScreen
		{
			get
			{
				return this.m_MaintainAspectRatioInSplitScreen;
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000940 RID: 2368 RVA: 0x00033EB8 File Offset: 0x000320B8
		public int fixedNumberOfSplitScreens
		{
			get
			{
				return this.m_FixedNumberOfSplitScreens;
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000941 RID: 2369 RVA: 0x00033EC0 File Offset: 0x000320C0
		public Rect splitScreenArea
		{
			get
			{
				return this.m_SplitScreenRect;
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000942 RID: 2370 RVA: 0x00033EC8 File Offset: 0x000320C8
		public int playerCount
		{
			get
			{
				return PlayerInput.s_AllActivePlayersCount;
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000943 RID: 2371 RVA: 0x00033ECF File Offset: 0x000320CF
		public int maxPlayerCount
		{
			get
			{
				return this.m_MaxPlayerCount;
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000944 RID: 2372 RVA: 0x00033ED7 File Offset: 0x000320D7
		public bool joiningEnabled
		{
			get
			{
				return this.m_AllowJoining;
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000945 RID: 2373 RVA: 0x00033EDF File Offset: 0x000320DF
		// (set) Token: 0x06000946 RID: 2374 RVA: 0x00033EE7 File Offset: 0x000320E7
		public PlayerJoinBehavior joinBehavior
		{
			get
			{
				return this.m_JoinBehavior;
			}
			set
			{
				if (this.m_JoinBehavior == value)
				{
					return;
				}
				bool allowJoining = this.m_AllowJoining;
				if (allowJoining)
				{
					this.DisableJoining();
				}
				this.m_JoinBehavior = value;
				if (allowJoining)
				{
					this.EnableJoining();
				}
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000947 RID: 2375 RVA: 0x00033F11 File Offset: 0x00032111
		// (set) Token: 0x06000948 RID: 2376 RVA: 0x00033F19 File Offset: 0x00032119
		public InputActionProperty joinAction
		{
			get
			{
				return this.m_JoinAction;
			}
			set
			{
				if (this.m_JoinAction == value)
				{
					return;
				}
				bool flag = this.m_AllowJoining && this.m_JoinBehavior == PlayerJoinBehavior.JoinPlayersWhenJoinActionIsTriggered;
				if (flag)
				{
					this.DisableJoining();
				}
				this.m_JoinAction = value;
				if (flag)
				{
					this.EnableJoining();
				}
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000949 RID: 2377 RVA: 0x00033F56 File Offset: 0x00032156
		// (set) Token: 0x0600094A RID: 2378 RVA: 0x00033F5E File Offset: 0x0003215E
		public PlayerNotifications notificationBehavior
		{
			get
			{
				return this.m_NotificationBehavior;
			}
			set
			{
				this.m_NotificationBehavior = value;
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x0600094B RID: 2379 RVA: 0x00033F67 File Offset: 0x00032167
		public PlayerInputManager.PlayerJoinedEvent playerJoinedEvent
		{
			get
			{
				if (this.m_PlayerJoinedEvent == null)
				{
					this.m_PlayerJoinedEvent = new PlayerInputManager.PlayerJoinedEvent();
				}
				return this.m_PlayerJoinedEvent;
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x0600094C RID: 2380 RVA: 0x00033F82 File Offset: 0x00032182
		public PlayerInputManager.PlayerLeftEvent playerLeftEvent
		{
			get
			{
				if (this.m_PlayerLeftEvent == null)
				{
					this.m_PlayerLeftEvent = new PlayerInputManager.PlayerLeftEvent();
				}
				return this.m_PlayerLeftEvent;
			}
		}

		// Token: 0x1400001C RID: 28
		// (add) Token: 0x0600094D RID: 2381 RVA: 0x00033F9D File Offset: 0x0003219D
		// (remove) Token: 0x0600094E RID: 2382 RVA: 0x00033FB9 File Offset: 0x000321B9
		public event Action<PlayerInput> onPlayerJoined
		{
			add
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.m_PlayerJoinedCallbacks.AddCallback(value);
			}
			remove
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.m_PlayerJoinedCallbacks.RemoveCallback(value);
			}
		}

		// Token: 0x1400001D RID: 29
		// (add) Token: 0x0600094F RID: 2383 RVA: 0x00033FD5 File Offset: 0x000321D5
		// (remove) Token: 0x06000950 RID: 2384 RVA: 0x00033FF1 File Offset: 0x000321F1
		public event Action<PlayerInput> onPlayerLeft
		{
			add
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.m_PlayerLeftCallbacks.AddCallback(value);
			}
			remove
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.m_PlayerLeftCallbacks.RemoveCallback(value);
			}
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000951 RID: 2385 RVA: 0x0003400D File Offset: 0x0003220D
		// (set) Token: 0x06000952 RID: 2386 RVA: 0x00034015 File Offset: 0x00032215
		public GameObject playerPrefab
		{
			get
			{
				return this.m_PlayerPrefab;
			}
			set
			{
				this.m_PlayerPrefab = value;
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000953 RID: 2387 RVA: 0x0003401E File Offset: 0x0003221E
		// (set) Token: 0x06000954 RID: 2388 RVA: 0x00034025 File Offset: 0x00032225
		public static PlayerInputManager instance { get; private set; }

		// Token: 0x06000955 RID: 2389 RVA: 0x00034030 File Offset: 0x00032230
		public void EnableJoining()
		{
			PlayerJoinBehavior joinBehavior = this.m_JoinBehavior;
			if (joinBehavior != PlayerJoinBehavior.JoinPlayersWhenButtonIsPressed)
			{
				if (joinBehavior == PlayerJoinBehavior.JoinPlayersWhenJoinActionIsTriggered)
				{
					if (this.m_JoinAction.action != null)
					{
						if (!this.m_JoinActionDelegateHooked)
						{
							if (this.m_JoinActionDelegate == null)
							{
								this.m_JoinActionDelegate = new Action<InputAction.CallbackContext>(this.JoinPlayerFromActionIfNotAlreadyJoined);
							}
							this.m_JoinAction.action.performed += this.m_JoinActionDelegate;
							this.m_JoinActionDelegateHooked = true;
						}
						this.m_JoinAction.action.Enable();
					}
					else
					{
						Debug.LogError("No join action configured on PlayerInputManager but join behavior is set to JoinPlayersWhenJoinActionIsTriggered", this);
					}
				}
			}
			else
			{
				this.ValidateInputActionAsset();
				if (!this.m_UnpairedDeviceUsedDelegateHooked)
				{
					if (this.m_UnpairedDeviceUsedDelegate == null)
					{
						this.m_UnpairedDeviceUsedDelegate = new Action<InputControl, InputEventPtr>(this.OnUnpairedDeviceUsed);
					}
					InputUser.onUnpairedDeviceUsed += this.m_UnpairedDeviceUsedDelegate;
					this.m_UnpairedDeviceUsedDelegateHooked = true;
					InputUser.listenForUnpairedDeviceActivity++;
				}
			}
			this.m_AllowJoining = true;
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x0003410C File Offset: 0x0003230C
		public void DisableJoining()
		{
			PlayerJoinBehavior joinBehavior = this.m_JoinBehavior;
			if (joinBehavior != PlayerJoinBehavior.JoinPlayersWhenButtonIsPressed)
			{
				if (joinBehavior == PlayerJoinBehavior.JoinPlayersWhenJoinActionIsTriggered)
				{
					if (this.m_JoinActionDelegateHooked)
					{
						if (this.m_JoinAction.action != null)
						{
							this.m_JoinAction.action.performed -= this.m_JoinActionDelegate;
						}
						this.m_JoinActionDelegateHooked = false;
					}
					InputAction action = this.m_JoinAction.action;
					if (action != null)
					{
						action.Disable();
					}
				}
			}
			else if (this.m_UnpairedDeviceUsedDelegateHooked)
			{
				InputUser.onUnpairedDeviceUsed -= this.m_UnpairedDeviceUsedDelegate;
				this.m_UnpairedDeviceUsedDelegateHooked = false;
				InputUser.listenForUnpairedDeviceActivity--;
			}
			this.m_AllowJoining = false;
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x000341A0 File Offset: 0x000323A0
		internal void JoinPlayerFromUI()
		{
			if (!this.CheckIfPlayerCanJoin(-1))
			{
				return;
			}
			throw new NotImplementedException();
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x000341B4 File Offset: 0x000323B4
		public void JoinPlayerFromAction(InputAction.CallbackContext context)
		{
			if (!this.CheckIfPlayerCanJoin(-1))
			{
				return;
			}
			InputDevice device = context.control.device;
			this.JoinPlayer(-1, -1, null, device);
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x000341E4 File Offset: 0x000323E4
		public void JoinPlayerFromActionIfNotAlreadyJoined(InputAction.CallbackContext context)
		{
			if (!this.CheckIfPlayerCanJoin(-1))
			{
				return;
			}
			InputDevice device = context.control.device;
			if (PlayerInput.FindFirstPairedToDevice(device) != null)
			{
				return;
			}
			this.JoinPlayer(-1, -1, null, device);
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x00034224 File Offset: 0x00032424
		public PlayerInput JoinPlayer(int playerIndex = -1, int splitScreenIndex = -1, string controlScheme = null, InputDevice pairWithDevice = null)
		{
			if (!this.CheckIfPlayerCanJoin(playerIndex))
			{
				return null;
			}
			PlayerInput.s_DestroyIfDeviceSetupUnsuccessful = true;
			return PlayerInput.Instantiate(this.m_PlayerPrefab, playerIndex, controlScheme, splitScreenIndex, pairWithDevice);
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x00034254 File Offset: 0x00032454
		public PlayerInput JoinPlayer(int playerIndex = -1, int splitScreenIndex = -1, string controlScheme = null, params InputDevice[] pairWithDevices)
		{
			if (!this.CheckIfPlayerCanJoin(playerIndex))
			{
				return null;
			}
			PlayerInput.s_DestroyIfDeviceSetupUnsuccessful = true;
			return PlayerInput.Instantiate(this.m_PlayerPrefab, playerIndex, controlScheme, splitScreenIndex, pairWithDevices);
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x0600095C RID: 2396 RVA: 0x00034284 File Offset: 0x00032484
		internal static string[] messages
		{
			get
			{
				return new string[] { "OnPlayerJoined", "OnPlayerLeft" };
			}
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x0003429C File Offset: 0x0003249C
		private bool CheckIfPlayerCanJoin(int playerIndex = -1)
		{
			if (this.m_PlayerPrefab == null)
			{
				Debug.LogError("playerPrefab must be set in order to be able to join new players", this);
				return false;
			}
			if (this.m_MaxPlayerCount >= 0 && this.playerCount >= this.m_MaxPlayerCount)
			{
				Debug.LogError("Have reached maximum player count of " + this.maxPlayerCount.ToString(), this);
				return false;
			}
			if (playerIndex != -1)
			{
				for (int i = 0; i < PlayerInput.s_AllActivePlayersCount; i++)
				{
					if (PlayerInput.s_AllActivePlayers[i].playerIndex == playerIndex)
					{
						Debug.LogError(string.Format("Player index #{0} is already taken by player {1}", playerIndex, PlayerInput.s_AllActivePlayers[i]), PlayerInput.s_AllActivePlayers[i]);
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x00034344 File Offset: 0x00032544
		private void OnUnpairedDeviceUsed(InputControl control, InputEventPtr eventPtr)
		{
			if (!this.m_AllowJoining)
			{
				return;
			}
			if (this.m_JoinBehavior == PlayerJoinBehavior.JoinPlayersWhenButtonIsPressed)
			{
				if (!(control is ButtonControl))
				{
					return;
				}
				if (!this.IsDeviceUsableWithPlayerActions(control.device))
				{
					return;
				}
				this.JoinPlayer(-1, -1, null, control.device);
			}
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x00034380 File Offset: 0x00032580
		private void OnEnable()
		{
			if (PlayerInputManager.instance == null)
			{
				PlayerInputManager.instance = this;
				if (this.joinAction.reference != null)
				{
					InputAction action = this.joinAction.action;
					Object @object;
					if (action == null)
					{
						@object = null;
					}
					else
					{
						InputActionMap actionMap = action.actionMap;
						@object = ((actionMap != null) ? actionMap.asset : null);
					}
					if (@object != null)
					{
						InputActionReference inputActionReference = InputActionReference.Create(Object.Instantiate<InputActionAsset>(this.joinAction.action.actionMap.asset).FindAction(this.joinAction.action.name, false));
						this.joinAction = new InputActionProperty(inputActionReference);
					}
				}
				for (int i = 0; i < PlayerInput.s_AllActivePlayersCount; i++)
				{
					this.NotifyPlayerJoined(PlayerInput.s_AllActivePlayers[i]);
				}
				if (this.m_AllowJoining)
				{
					this.EnableJoining();
				}
				return;
			}
			Debug.LogWarning("Multiple PlayerInputManagers in the game. There should only be one PlayerInputManager", this);
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x00034465 File Offset: 0x00032665
		private void OnDisable()
		{
			if (PlayerInputManager.instance == this)
			{
				PlayerInputManager.instance = null;
			}
			if (this.m_AllowJoining)
			{
				this.DisableJoining();
			}
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x00034488 File Offset: 0x00032688
		private void UpdateSplitScreen()
		{
			if (!this.m_SplitScreen)
			{
				return;
			}
			int num = 0;
			foreach (PlayerInput playerInput in PlayerInput.all)
			{
				if (playerInput.playerIndex >= num)
				{
					num = playerInput.playerIndex + 1;
				}
			}
			if (this.m_FixedNumberOfSplitScreens > 0)
			{
				if (this.m_FixedNumberOfSplitScreens < num)
				{
					Debug.LogWarning(string.Format("Highest playerIndex of {0} exceeds fixed number of split-screens of {1}", num, this.m_FixedNumberOfSplitScreens), this);
				}
				num = this.m_FixedNumberOfSplitScreens;
			}
			int num2 = Mathf.CeilToInt(Mathf.Sqrt((float)num));
			int num3 = num2;
			if (!this.m_MaintainAspectRatioInSplitScreen && num2 * (num2 - 1) >= num)
			{
				num3--;
			}
			foreach (PlayerInput playerInput2 in PlayerInput.all)
			{
				int splitScreenIndex = playerInput2.splitScreenIndex;
				if (splitScreenIndex >= num2 * num3)
				{
					Debug.LogError(string.Format("Split-screen index of {0} on player is out of range (have {1} screens); resetting to playerIndex", splitScreenIndex, num2 * num3), playerInput2);
					playerInput2.m_SplitScreenIndex = playerInput2.playerIndex;
				}
				Camera camera = playerInput2.camera;
				if (camera == null)
				{
					Debug.LogError("Player has no camera associated with it. Cannot set up split-screen. Point PlayerInput.camera to camera for player.", playerInput2);
				}
				else
				{
					int num4 = splitScreenIndex % num2;
					int num5 = splitScreenIndex / num2;
					Rect rect = new Rect
					{
						width = this.m_SplitScreenRect.width / (float)num2,
						height = this.m_SplitScreenRect.height / (float)num3
					};
					rect.x = this.m_SplitScreenRect.x + (float)num4 * rect.width;
					rect.y = this.m_SplitScreenRect.y + this.m_SplitScreenRect.height - (float)(num5 + 1) * rect.height;
					camera.rect = rect;
				}
			}
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x000346A8 File Offset: 0x000328A8
		private bool IsDeviceUsableWithPlayerActions(InputDevice device)
		{
			if (this.m_PlayerPrefab == null)
			{
				return true;
			}
			PlayerInput componentInChildren = this.m_PlayerPrefab.GetComponentInChildren<PlayerInput>();
			if (componentInChildren == null)
			{
				return true;
			}
			InputActionAsset actions = componentInChildren.actions;
			if (actions == null)
			{
				return true;
			}
			if (actions.controlSchemes.Count > 0)
			{
				using (InputControlList<InputDevice> unpairedInputDevices = InputUser.GetUnpairedInputDevices())
				{
					if (InputControlScheme.FindControlSchemeForDevices<InputControlList<InputDevice>, ReadOnlyArray<InputControlScheme>>(unpairedInputDevices, actions.controlSchemes, device, false) == null)
					{
						return false;
					}
				}
				return true;
			}
			using (ReadOnlyArray<InputActionMap>.Enumerator enumerator = actions.actionMaps.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsUsableWithDevice(device))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x00034798 File Offset: 0x00032998
		private void ValidateInputActionAsset()
		{
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x0003479C File Offset: 0x0003299C
		internal void NotifyPlayerJoined(PlayerInput player)
		{
			this.UpdateSplitScreen();
			switch (this.m_NotificationBehavior)
			{
			case PlayerNotifications.SendMessages:
				base.SendMessage("OnPlayerJoined", player, SendMessageOptions.DontRequireReceiver);
				return;
			case PlayerNotifications.BroadcastMessages:
				base.BroadcastMessage("OnPlayerJoined", player, SendMessageOptions.DontRequireReceiver);
				return;
			case PlayerNotifications.InvokeUnityEvents:
			{
				PlayerInputManager.PlayerJoinedEvent playerJoinedEvent = this.m_PlayerJoinedEvent;
				if (playerJoinedEvent == null)
				{
					return;
				}
				playerJoinedEvent.Invoke(player);
				return;
			}
			case PlayerNotifications.InvokeCSharpEvents:
				DelegateHelpers.InvokeCallbacksSafe<PlayerInput>(ref this.m_PlayerJoinedCallbacks, player, "onPlayerJoined", null);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x00034810 File Offset: 0x00032A10
		internal void NotifyPlayerLeft(PlayerInput player)
		{
			this.UpdateSplitScreen();
			switch (this.m_NotificationBehavior)
			{
			case PlayerNotifications.SendMessages:
				base.SendMessage("OnPlayerLeft", player, SendMessageOptions.DontRequireReceiver);
				return;
			case PlayerNotifications.BroadcastMessages:
				base.BroadcastMessage("OnPlayerLeft", player, SendMessageOptions.DontRequireReceiver);
				return;
			case PlayerNotifications.InvokeUnityEvents:
			{
				PlayerInputManager.PlayerLeftEvent playerLeftEvent = this.m_PlayerLeftEvent;
				if (playerLeftEvent == null)
				{
					return;
				}
				playerLeftEvent.Invoke(player);
				return;
			}
			case PlayerNotifications.InvokeCSharpEvents:
				DelegateHelpers.InvokeCallbacksSafe<PlayerInput>(ref this.m_PlayerLeftCallbacks, player, "onPlayerLeft", null);
				return;
			default:
				return;
			}
		}

		// Token: 0x040002E2 RID: 738
		public const string PlayerJoinedMessage = "OnPlayerJoined";

		// Token: 0x040002E3 RID: 739
		public const string PlayerLeftMessage = "OnPlayerLeft";

		// Token: 0x040002E5 RID: 741
		[SerializeField]
		internal PlayerNotifications m_NotificationBehavior;

		// Token: 0x040002E6 RID: 742
		[Tooltip("Set a limit for the maximum number of players who are able to join.")]
		[SerializeField]
		internal int m_MaxPlayerCount = -1;

		// Token: 0x040002E7 RID: 743
		[SerializeField]
		internal bool m_AllowJoining = true;

		// Token: 0x040002E8 RID: 744
		[SerializeField]
		internal PlayerJoinBehavior m_JoinBehavior;

		// Token: 0x040002E9 RID: 745
		[SerializeField]
		internal PlayerInputManager.PlayerJoinedEvent m_PlayerJoinedEvent;

		// Token: 0x040002EA RID: 746
		[SerializeField]
		internal PlayerInputManager.PlayerLeftEvent m_PlayerLeftEvent;

		// Token: 0x040002EB RID: 747
		[SerializeField]
		internal InputActionProperty m_JoinAction;

		// Token: 0x040002EC RID: 748
		[SerializeField]
		internal GameObject m_PlayerPrefab;

		// Token: 0x040002ED RID: 749
		[SerializeField]
		internal bool m_SplitScreen;

		// Token: 0x040002EE RID: 750
		[SerializeField]
		internal bool m_MaintainAspectRatioInSplitScreen;

		// Token: 0x040002EF RID: 751
		[Tooltip("Explicitly set a fixed number of screens or otherwise allow the screen to be divided automatically to best fit the number of players.")]
		[SerializeField]
		internal int m_FixedNumberOfSplitScreens = -1;

		// Token: 0x040002F0 RID: 752
		[SerializeField]
		internal Rect m_SplitScreenRect = new Rect(0f, 0f, 1f, 1f);

		// Token: 0x040002F1 RID: 753
		[NonSerialized]
		private bool m_JoinActionDelegateHooked;

		// Token: 0x040002F2 RID: 754
		[NonSerialized]
		private bool m_UnpairedDeviceUsedDelegateHooked;

		// Token: 0x040002F3 RID: 755
		[NonSerialized]
		private Action<InputAction.CallbackContext> m_JoinActionDelegate;

		// Token: 0x040002F4 RID: 756
		[NonSerialized]
		private Action<InputControl, InputEventPtr> m_UnpairedDeviceUsedDelegate;

		// Token: 0x040002F5 RID: 757
		[NonSerialized]
		private CallbackArray<Action<PlayerInput>> m_PlayerJoinedCallbacks;

		// Token: 0x040002F6 RID: 758
		[NonSerialized]
		private CallbackArray<Action<PlayerInput>> m_PlayerLeftCallbacks;

		// Token: 0x020001B6 RID: 438
		[Serializable]
		public class PlayerJoinedEvent : UnityEvent<PlayerInput>
		{
		}

		// Token: 0x020001B7 RID: 439
		[Serializable]
		public class PlayerLeftEvent : UnityEvent<PlayerInput>
		{
		}
	}
}
