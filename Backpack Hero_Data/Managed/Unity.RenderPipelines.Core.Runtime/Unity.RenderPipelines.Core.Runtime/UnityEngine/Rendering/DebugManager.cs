using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.Rendering.UI;

namespace UnityEngine.Rendering
{
	// Token: 0x02000068 RID: 104
	public sealed class DebugManager
	{
		// Token: 0x06000338 RID: 824 RVA: 0x0000F3B8 File Offset: 0x0000D5B8
		private void RegisterActions()
		{
			this.m_DebugActions = new DebugActionDesc[9];
			this.m_DebugActionStates = new DebugActionState[9];
			this.AddAction(DebugAction.EnableDebugMenu, new DebugActionDesc
			{
				buttonAction = this.debugActionMap.FindAction("Enable Debug", false),
				repeatMode = DebugActionRepeatMode.Never
			});
			this.AddAction(DebugAction.ResetAll, new DebugActionDesc
			{
				buttonAction = this.debugActionMap.FindAction("Debug Reset", false),
				repeatMode = DebugActionRepeatMode.Never
			});
			this.AddAction(DebugAction.NextDebugPanel, new DebugActionDesc
			{
				buttonAction = this.debugActionMap.FindAction("Debug Next", false),
				repeatMode = DebugActionRepeatMode.Never
			});
			this.AddAction(DebugAction.PreviousDebugPanel, new DebugActionDesc
			{
				buttonAction = this.debugActionMap.FindAction("Debug Previous", false),
				repeatMode = DebugActionRepeatMode.Never
			});
			DebugActionDesc debugActionDesc = new DebugActionDesc();
			debugActionDesc.buttonAction = this.debugActionMap.FindAction("Debug Validate", false);
			debugActionDesc.repeatMode = DebugActionRepeatMode.Never;
			this.AddAction(DebugAction.Action, debugActionDesc);
			this.AddAction(DebugAction.MakePersistent, new DebugActionDesc
			{
				buttonAction = this.debugActionMap.FindAction("Debug Persistent", false),
				repeatMode = DebugActionRepeatMode.Never
			});
			DebugActionDesc debugActionDesc2 = new DebugActionDesc();
			debugActionDesc2.buttonAction = this.debugActionMap.FindAction("Debug Multiplier", false);
			debugActionDesc2.repeatMode = DebugActionRepeatMode.Delay;
			debugActionDesc.repeatDelay = 0f;
			this.AddAction(DebugAction.Multiplier, debugActionDesc2);
			this.AddAction(DebugAction.MoveVertical, new DebugActionDesc
			{
				buttonAction = this.debugActionMap.FindAction("Debug Vertical", false),
				repeatMode = DebugActionRepeatMode.Delay,
				repeatDelay = 0.16f
			});
			this.AddAction(DebugAction.MoveHorizontal, new DebugActionDesc
			{
				buttonAction = this.debugActionMap.FindAction("Debug Horizontal", false),
				repeatMode = DebugActionRepeatMode.Delay,
				repeatDelay = 0.16f
			});
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000F5A4 File Offset: 0x0000D7A4
		internal void EnableInputActions()
		{
			foreach (InputAction inputAction in this.debugActionMap)
			{
				inputAction.Enable();
			}
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000F5F0 File Offset: 0x0000D7F0
		private void AddAction(DebugAction action, DebugActionDesc desc)
		{
			this.m_DebugActions[(int)action] = desc;
			this.m_DebugActionStates[(int)action] = new DebugActionState();
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000F618 File Offset: 0x0000D818
		private void SampleAction(int actionIndex)
		{
			DebugActionDesc debugActionDesc = this.m_DebugActions[actionIndex];
			DebugActionState debugActionState = this.m_DebugActionStates[actionIndex];
			if (!debugActionState.runningAction && debugActionDesc.buttonAction != null)
			{
				float num = debugActionDesc.buttonAction.ReadValue<float>();
				if (!Mathf.Approximately(num, 0f))
				{
					debugActionState.TriggerWithButton(debugActionDesc.buttonAction, num);
				}
			}
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000F670 File Offset: 0x0000D870
		private void UpdateAction(int actionIndex)
		{
			DebugActionDesc debugActionDesc = this.m_DebugActions[actionIndex];
			DebugActionState debugActionState = this.m_DebugActionStates[actionIndex];
			if (debugActionState.runningAction)
			{
				debugActionState.Update(debugActionDesc);
			}
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000F6A0 File Offset: 0x0000D8A0
		internal void UpdateActions()
		{
			for (int i = 0; i < this.m_DebugActions.Length; i++)
			{
				this.UpdateAction(i);
				this.SampleAction(i);
			}
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000F6CE File Offset: 0x0000D8CE
		internal float GetAction(DebugAction action)
		{
			return this.m_DebugActionStates[(int)action].actionState;
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0000F6E0 File Offset: 0x0000D8E0
		internal bool GetActionToggleDebugMenuWithTouch()
		{
			if (!EnhancedTouchSupport.enabled)
			{
				return false;
			}
			ReadOnlyArray<Touch> activeTouches = Touch.activeTouches;
			int count = activeTouches.Count;
			TouchPhase? touchPhase = null;
			if (count == 3)
			{
				foreach (Touch touch in activeTouches)
				{
					if ((touchPhase == null || touch.phase == touchPhase.Value) && touch.tapCount == 2)
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0000F778 File Offset: 0x0000D978
		internal bool GetActionReleaseScrollTarget()
		{
			bool flag = Mouse.current != null && Mouse.current.scroll.ReadValue() != Vector2.zero;
			bool flag2 = Touchscreen.current != null;
			return flag || flag2;
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0000F7B4 File Offset: 0x0000D9B4
		private void RegisterInputs()
		{
			this.debugActionMap.AddAction("Enable Debug", InputActionType.Button, null, null, null, null, null).AddCompositeBinding("ButtonWithOneModifier", null, null).With("Modifier", "<Gamepad>/rightStickPress", null, null)
				.With("Button", "<Gamepad>/leftStickPress", null, null)
				.With("Modifier", "<Keyboard>/leftCtrl", null, null)
				.With("Button", "<Keyboard>/backspace", null, null);
			this.debugActionMap.AddAction("Debug Reset", InputActionType.Button, null, null, null, null, null).AddCompositeBinding("ButtonWithOneModifier", null, null).With("Modifier", "<Gamepad>/rightStickPress", null, null)
				.With("Button", "<Gamepad>/b", null, null)
				.With("Modifier", "<Keyboard>/leftAlt", null, null)
				.With("Button", "<Keyboard>/backspace", null, null);
			InputAction inputAction = this.debugActionMap.AddAction("Debug Next", InputActionType.Button, null, null, null, null, null);
			inputAction.AddBinding("<Keyboard>/pageDown", null, null, null);
			inputAction.AddBinding("<Gamepad>/rightShoulder", null, null, null);
			InputAction inputAction2 = this.debugActionMap.AddAction("Debug Previous", InputActionType.Button, null, null, null, null, null);
			inputAction2.AddBinding("<Keyboard>/pageUp", null, null, null);
			inputAction2.AddBinding("<Gamepad>/leftShoulder", null, null, null);
			InputAction inputAction3 = this.debugActionMap.AddAction("Debug Validate", InputActionType.Button, null, null, null, null, null);
			inputAction3.AddBinding("<Keyboard>/enter", null, null, null);
			inputAction3.AddBinding("<Gamepad>/a", null, null, null);
			InputAction inputAction4 = this.debugActionMap.AddAction("Debug Persistent", InputActionType.Button, null, null, null, null, null);
			inputAction4.AddBinding("<Keyboard>/rightShift", null, null, null);
			inputAction4.AddBinding("<Gamepad>/x", null, null, null);
			InputAction inputAction5 = this.debugActionMap.AddAction("Debug Multiplier", InputActionType.Value, null, null, null, null, null);
			inputAction5.AddBinding("<Keyboard>/leftShift", null, null, null);
			inputAction5.AddBinding("<Gamepad>/y", null, null, null);
			this.debugActionMap.AddAction("Debug Vertical", InputActionType.Value, null, null, null, null, null).AddCompositeBinding("1DAxis", null, null).With("Positive", "<Gamepad>/dpad/up", null, null)
				.With("Negative", "<Gamepad>/dpad/down", null, null)
				.With("Positive", "<Keyboard>/upArrow", null, null)
				.With("Negative", "<Keyboard>/downArrow", null, null);
			this.debugActionMap.AddAction("Debug Horizontal", InputActionType.Value, null, null, null, null, null).AddCompositeBinding("1DAxis", null, null).With("Positive", "<Gamepad>/dpad/right", null, null)
				.With("Negative", "<Gamepad>/dpad/left", null, null)
				.With("Positive", "<Keyboard>/rightArrow", null, null)
				.With("Negative", "<Keyboard>/leftArrow", null, null);
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000342 RID: 834 RVA: 0x0000FA8C File Offset: 0x0000DC8C
		public static DebugManager instance
		{
			get
			{
				return DebugManager.s_Instance.Value;
			}
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0000FA98 File Offset: 0x0000DC98
		private void UpdateReadOnlyCollection()
		{
			this.m_Panels.Sort();
			this.m_ReadOnlyPanels = this.m_Panels.AsReadOnly();
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000344 RID: 836 RVA: 0x0000FAB6 File Offset: 0x0000DCB6
		public ReadOnlyCollection<DebugUI.Panel> panels
		{
			get
			{
				if (this.m_ReadOnlyPanels == null)
				{
					this.UpdateReadOnlyCollection();
				}
				return this.m_ReadOnlyPanels;
			}
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000345 RID: 837 RVA: 0x0000FACC File Offset: 0x0000DCCC
		// (remove) Token: 0x06000346 RID: 838 RVA: 0x0000FB04 File Offset: 0x0000DD04
		public event Action<bool> onDisplayRuntimeUIChanged = delegate
		{
		};

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000347 RID: 839 RVA: 0x0000FB3C File Offset: 0x0000DD3C
		// (remove) Token: 0x06000348 RID: 840 RVA: 0x0000FB74 File Offset: 0x0000DD74
		public event Action onSetDirty = delegate
		{
		};

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000349 RID: 841 RVA: 0x0000FBAC File Offset: 0x0000DDAC
		// (remove) Token: 0x0600034A RID: 842 RVA: 0x0000FBE4 File Offset: 0x0000DDE4
		private event Action resetData;

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600034B RID: 843 RVA: 0x0000FC19 File Offset: 0x0000DE19
		public bool displayEditorUI
		{
			get
			{
				return this.m_EditorOpen;
			}
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000FC21 File Offset: 0x0000DE21
		public void ToggleEditorUI(bool open)
		{
			this.m_EditorOpen = open;
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600034D RID: 845 RVA: 0x0000FC2A File Offset: 0x0000DE2A
		// (set) Token: 0x0600034E RID: 846 RVA: 0x0000FC32 File Offset: 0x0000DE32
		public bool enableRuntimeUI
		{
			get
			{
				return this.m_EnableRuntimeUI;
			}
			set
			{
				if (value != this.m_EnableRuntimeUI)
				{
					this.m_EnableRuntimeUI = value;
					DebugUpdater.SetEnabled(value);
				}
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600034F RID: 847 RVA: 0x0000FC4A File Offset: 0x0000DE4A
		// (set) Token: 0x06000350 RID: 848 RVA: 0x0000FC68 File Offset: 0x0000DE68
		public bool displayRuntimeUI
		{
			get
			{
				return this.m_Root != null && this.m_Root.activeInHierarchy;
			}
			set
			{
				if (value)
				{
					this.m_Root = Object.Instantiate<Transform>(Resources.Load<Transform>("DebugUICanvas")).gameObject;
					this.m_Root.name = "[Debug Canvas]";
					this.m_Root.transform.localPosition = Vector3.zero;
					this.m_RootUICanvas = this.m_Root.GetComponent<DebugUIHandlerCanvas>();
					this.m_Root.SetActive(true);
				}
				else
				{
					CoreUtils.Destroy(this.m_Root);
					this.m_Root = null;
					this.m_RootUICanvas = null;
				}
				this.onDisplayRuntimeUIChanged(value);
				DebugUpdater.HandleInternalEventSystemComponents(value);
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000351 RID: 849 RVA: 0x0000FD01 File Offset: 0x0000DF01
		// (set) Token: 0x06000352 RID: 850 RVA: 0x0000FD1E File Offset: 0x0000DF1E
		public bool displayPersistentRuntimeUI
		{
			get
			{
				return this.m_RootUIPersistentCanvas != null && this.m_PersistentRoot.activeInHierarchy;
			}
			set
			{
				if (value)
				{
					this.EnsurePersistentCanvas();
					return;
				}
				CoreUtils.Destroy(this.m_PersistentRoot);
				this.m_PersistentRoot = null;
				this.m_RootUIPersistentCanvas = null;
			}
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0000FD44 File Offset: 0x0000DF44
		private DebugManager()
		{
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000FDC3 File Offset: 0x0000DFC3
		public void RefreshEditor()
		{
			this.refreshEditorRequested = true;
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0000FDCC File Offset: 0x0000DFCC
		public void Reset()
		{
			Action action = this.resetData;
			if (action != null)
			{
				action();
			}
			this.ReDrawOnScreenDebug();
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0000FDE5 File Offset: 0x0000DFE5
		public void ReDrawOnScreenDebug()
		{
			if (this.displayRuntimeUI)
			{
				DebugUIHandlerCanvas rootUICanvas = this.m_RootUICanvas;
				if (rootUICanvas == null)
				{
					return;
				}
				rootUICanvas.RequestHierarchyReset();
			}
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0000FDFF File Offset: 0x0000DFFF
		public void RegisterData(IDebugData data)
		{
			this.resetData += data.GetReset();
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0000FE0D File Offset: 0x0000E00D
		public void UnregisterData(IDebugData data)
		{
			this.resetData -= data.GetReset();
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0000FE1C File Offset: 0x0000E01C
		public int GetState()
		{
			int num = 17;
			foreach (DebugUI.Panel panel in this.m_Panels)
			{
				num = num * 23 + panel.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0000FE78 File Offset: 0x0000E078
		internal void RegisterRootCanvas(DebugUIHandlerCanvas root)
		{
			this.m_Root = root.gameObject;
			this.m_RootUICanvas = root;
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000FE8D File Offset: 0x0000E08D
		internal void ChangeSelection(DebugUIHandlerWidget widget, bool fromNext)
		{
			this.m_RootUICanvas.ChangeSelection(widget, fromNext);
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0000FE9C File Offset: 0x0000E09C
		internal void SetScrollTarget(DebugUIHandlerWidget widget)
		{
			if (this.m_RootUICanvas != null)
			{
				this.m_RootUICanvas.SetScrollTarget(widget);
			}
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0000FEB8 File Offset: 0x0000E0B8
		private void EnsurePersistentCanvas()
		{
			if (this.m_RootUIPersistentCanvas == null)
			{
				DebugUIHandlerPersistentCanvas debugUIHandlerPersistentCanvas = Object.FindObjectOfType<DebugUIHandlerPersistentCanvas>();
				if (debugUIHandlerPersistentCanvas == null)
				{
					this.m_PersistentRoot = Object.Instantiate<Transform>(Resources.Load<Transform>("DebugUIPersistentCanvas")).gameObject;
					this.m_PersistentRoot.name = "[Debug Canvas - Persistent]";
					this.m_PersistentRoot.transform.localPosition = Vector3.zero;
				}
				else
				{
					this.m_PersistentRoot = debugUIHandlerPersistentCanvas.gameObject;
				}
				this.m_RootUIPersistentCanvas = this.m_PersistentRoot.GetComponent<DebugUIHandlerPersistentCanvas>();
			}
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0000FF40 File Offset: 0x0000E140
		internal void TogglePersistent(DebugUI.Widget widget)
		{
			if (widget == null)
			{
				return;
			}
			DebugUI.Value value = widget as DebugUI.Value;
			if (value == null)
			{
				Debug.Log("Only DebugUI.Value items can be made persistent.");
				return;
			}
			this.EnsurePersistentCanvas();
			this.m_RootUIPersistentCanvas.Toggle(value);
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0000FF78 File Offset: 0x0000E178
		private void OnPanelDirty(DebugUI.Panel panel)
		{
			this.onSetDirty();
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0000FF85 File Offset: 0x0000E185
		public void RequestEditorWindowPanelIndex(int index)
		{
			this.m_RequestedPanelIndex = new int?(index);
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0000FF93 File Offset: 0x0000E193
		internal int? GetRequestedEditorWindowPanelIndex()
		{
			int? requestedPanelIndex = this.m_RequestedPanelIndex;
			this.m_RequestedPanelIndex = null;
			return requestedPanelIndex;
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000FFA8 File Offset: 0x0000E1A8
		public DebugUI.Panel GetPanel(string displayName, bool createIfNull = false, int groupIndex = 0, bool overrideIfExist = false)
		{
			DebugUI.Panel panel = null;
			foreach (DebugUI.Panel panel2 in this.m_Panels)
			{
				if (panel2.displayName == displayName)
				{
					panel = panel2;
					break;
				}
			}
			if (panel != null)
			{
				if (!overrideIfExist)
				{
					return panel;
				}
				panel.onSetDirty -= this.OnPanelDirty;
				this.RemovePanel(panel);
				panel = null;
			}
			if (createIfNull)
			{
				panel = new DebugUI.Panel
				{
					displayName = displayName,
					groupIndex = groupIndex
				};
				panel.onSetDirty += this.OnPanelDirty;
				this.m_Panels.Add(panel);
				this.UpdateReadOnlyCollection();
			}
			return panel;
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0001006C File Offset: 0x0000E26C
		public void RemovePanel(string displayName)
		{
			DebugUI.Panel panel = null;
			foreach (DebugUI.Panel panel2 in this.m_Panels)
			{
				if (panel2.displayName == displayName)
				{
					panel2.onSetDirty -= this.OnPanelDirty;
					panel = panel2;
					break;
				}
			}
			this.RemovePanel(panel);
		}

		// Token: 0x06000364 RID: 868 RVA: 0x000100E8 File Offset: 0x0000E2E8
		public void RemovePanel(DebugUI.Panel panel)
		{
			if (panel == null)
			{
				return;
			}
			this.m_Panels.Remove(panel);
			this.UpdateReadOnlyCollection();
		}

		// Token: 0x06000365 RID: 869 RVA: 0x00010104 File Offset: 0x0000E304
		public DebugUI.Widget GetItem(string queryPath)
		{
			foreach (DebugUI.Panel panel in this.m_Panels)
			{
				DebugUI.Widget item = this.GetItem(queryPath, panel);
				if (item != null)
				{
					return item;
				}
			}
			return null;
		}

		// Token: 0x06000366 RID: 870 RVA: 0x00010164 File Offset: 0x0000E364
		private DebugUI.Widget GetItem(string queryPath, DebugUI.IContainer container)
		{
			foreach (DebugUI.Widget widget in container.children)
			{
				if (widget.queryPath == queryPath)
				{
					return widget;
				}
				DebugUI.IContainer container2 = widget as DebugUI.IContainer;
				if (container2 != null)
				{
					DebugUI.Widget item = this.GetItem(queryPath, container2);
					if (item != null)
					{
						return item;
					}
				}
			}
			return null;
		}

		// Token: 0x04000217 RID: 535
		private const string kEnableDebugBtn1 = "Enable Debug Button 1";

		// Token: 0x04000218 RID: 536
		private const string kEnableDebugBtn2 = "Enable Debug Button 2";

		// Token: 0x04000219 RID: 537
		private const string kDebugPreviousBtn = "Debug Previous";

		// Token: 0x0400021A RID: 538
		private const string kDebugNextBtn = "Debug Next";

		// Token: 0x0400021B RID: 539
		private const string kValidateBtn = "Debug Validate";

		// Token: 0x0400021C RID: 540
		private const string kPersistentBtn = "Debug Persistent";

		// Token: 0x0400021D RID: 541
		private const string kDPadVertical = "Debug Vertical";

		// Token: 0x0400021E RID: 542
		private const string kDPadHorizontal = "Debug Horizontal";

		// Token: 0x0400021F RID: 543
		private const string kMultiplierBtn = "Debug Multiplier";

		// Token: 0x04000220 RID: 544
		private const string kResetBtn = "Debug Reset";

		// Token: 0x04000221 RID: 545
		private const string kEnableDebug = "Enable Debug";

		// Token: 0x04000222 RID: 546
		private DebugActionDesc[] m_DebugActions;

		// Token: 0x04000223 RID: 547
		private DebugActionState[] m_DebugActionStates;

		// Token: 0x04000224 RID: 548
		private InputActionMap debugActionMap = new InputActionMap("Debug Menu");

		// Token: 0x04000225 RID: 549
		private static readonly Lazy<DebugManager> s_Instance = new Lazy<DebugManager>(() => new DebugManager());

		// Token: 0x04000226 RID: 550
		private ReadOnlyCollection<DebugUI.Panel> m_ReadOnlyPanels;

		// Token: 0x04000227 RID: 551
		private readonly List<DebugUI.Panel> m_Panels = new List<DebugUI.Panel>();

		// Token: 0x0400022B RID: 555
		public bool refreshEditorRequested;

		// Token: 0x0400022C RID: 556
		private int? m_RequestedPanelIndex;

		// Token: 0x0400022D RID: 557
		private GameObject m_Root;

		// Token: 0x0400022E RID: 558
		private DebugUIHandlerCanvas m_RootUICanvas;

		// Token: 0x0400022F RID: 559
		private GameObject m_PersistentRoot;

		// Token: 0x04000230 RID: 560
		private DebugUIHandlerPersistentCanvas m_RootUIPersistentCanvas;

		// Token: 0x04000231 RID: 561
		private bool m_EditorOpen;

		// Token: 0x04000232 RID: 562
		private bool m_EnableRuntimeUI = true;
	}
}
