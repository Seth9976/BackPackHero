using System;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.Serialization;

namespace UnityEngine.InputSystem
{
	// Token: 0x0200001A RID: 26
	[Serializable]
	public sealed class InputAction : ICloneable, IDisposable
	{
		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000130 RID: 304 RVA: 0x00003399 File Offset: 0x00001599
		public string name
		{
			get
			{
				return this.m_Name;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000131 RID: 305 RVA: 0x000033A1 File Offset: 0x000015A1
		public InputActionType type
		{
			get
			{
				return this.m_Type;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000132 RID: 306 RVA: 0x000033A9 File Offset: 0x000015A9
		public Guid id
		{
			get
			{
				this.MakeSureIdIsInPlace();
				return new Guid(this.m_Id);
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000133 RID: 307 RVA: 0x000033C0 File Offset: 0x000015C0
		internal Guid idDontGenerate
		{
			get
			{
				if (string.IsNullOrEmpty(this.m_Id))
				{
					return default(Guid);
				}
				return new Guid(this.m_Id);
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000134 RID: 308 RVA: 0x000033EF File Offset: 0x000015EF
		// (set) Token: 0x06000135 RID: 309 RVA: 0x000033F7 File Offset: 0x000015F7
		public string expectedControlType
		{
			get
			{
				return this.m_ExpectedControlType;
			}
			set
			{
				this.m_ExpectedControlType = value;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00003400 File Offset: 0x00001600
		public string processors
		{
			get
			{
				return this.m_Processors;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000137 RID: 311 RVA: 0x00003408 File Offset: 0x00001608
		public string interactions
		{
			get
			{
				return this.m_Interactions;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00003410 File Offset: 0x00001610
		public InputActionMap actionMap
		{
			get
			{
				if (!this.isSingletonAction)
				{
					return this.m_ActionMap;
				}
				return null;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00003422 File Offset: 0x00001622
		// (set) Token: 0x0600013A RID: 314 RVA: 0x0000342C File Offset: 0x0000162C
		public InputBinding? bindingMask
		{
			get
			{
				return this.m_BindingMask;
			}
			set
			{
				if (value == this.m_BindingMask)
				{
					return;
				}
				if (value != null)
				{
					InputBinding value2 = value.Value;
					value2.action = this.name;
					value = new InputBinding?(value2);
				}
				this.m_BindingMask = value;
				InputActionMap orCreateActionMap = this.GetOrCreateActionMap();
				if (orCreateActionMap.m_State != null)
				{
					orCreateActionMap.LazyResolveBindings(true);
				}
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600013B RID: 315 RVA: 0x000034BB File Offset: 0x000016BB
		public ReadOnlyArray<InputBinding> bindings
		{
			get
			{
				return this.GetOrCreateActionMap().GetBindingsForSingleAction(this);
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600013C RID: 316 RVA: 0x000034C9 File Offset: 0x000016C9
		public ReadOnlyArray<InputControl> controls
		{
			get
			{
				InputActionMap orCreateActionMap = this.GetOrCreateActionMap();
				orCreateActionMap.ResolveBindingsIfNecessary();
				return orCreateActionMap.GetControlsForSingleAction(this);
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600013D RID: 317 RVA: 0x000034E0 File Offset: 0x000016E0
		public InputActionPhase phase
		{
			get
			{
				return this.currentState.phase;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600013E RID: 318 RVA: 0x000034FB File Offset: 0x000016FB
		public bool inProgress
		{
			get
			{
				return this.phase.IsInProgress();
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00003508 File Offset: 0x00001708
		public bool enabled
		{
			get
			{
				return this.phase > InputActionPhase.Disabled;
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000140 RID: 320 RVA: 0x00003513 File Offset: 0x00001713
		// (remove) Token: 0x06000141 RID: 321 RVA: 0x00003521 File Offset: 0x00001721
		public event Action<InputAction.CallbackContext> started
		{
			add
			{
				this.m_OnStarted.AddCallback(value);
			}
			remove
			{
				this.m_OnStarted.RemoveCallback(value);
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000142 RID: 322 RVA: 0x0000352F File Offset: 0x0000172F
		// (remove) Token: 0x06000143 RID: 323 RVA: 0x0000353D File Offset: 0x0000173D
		public event Action<InputAction.CallbackContext> canceled
		{
			add
			{
				this.m_OnCanceled.AddCallback(value);
			}
			remove
			{
				this.m_OnCanceled.RemoveCallback(value);
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000144 RID: 324 RVA: 0x0000354B File Offset: 0x0000174B
		// (remove) Token: 0x06000145 RID: 325 RVA: 0x00003559 File Offset: 0x00001759
		public event Action<InputAction.CallbackContext> performed
		{
			add
			{
				this.m_OnPerformed.AddCallback(value);
			}
			remove
			{
				this.m_OnPerformed.RemoveCallback(value);
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000146 RID: 326 RVA: 0x00003567 File Offset: 0x00001767
		public bool triggered
		{
			get
			{
				return this.WasPerformedThisFrame();
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00003570 File Offset: 0x00001770
		public unsafe InputControl activeControl
		{
			get
			{
				InputActionState state = this.GetOrCreateActionMap().m_State;
				if (state != null)
				{
					int controlIndex = state.actionStates[this.m_ActionIndexInState].controlIndex;
					if (controlIndex != -1)
					{
						return state.controls[controlIndex];
					}
				}
				return null;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000148 RID: 328 RVA: 0x000035B6 File Offset: 0x000017B6
		// (set) Token: 0x06000149 RID: 329 RVA: 0x000035CD File Offset: 0x000017CD
		public bool wantsInitialStateCheck
		{
			get
			{
				return this.type == InputActionType.Value || (this.m_Flags & InputAction.ActionFlags.WantsInitialStateCheck) > (InputAction.ActionFlags)0;
			}
			set
			{
				if (value)
				{
					this.m_Flags |= InputAction.ActionFlags.WantsInitialStateCheck;
					return;
				}
				this.m_Flags &= ~InputAction.ActionFlags.WantsInitialStateCheck;
			}
		}

		// Token: 0x0600014A RID: 330 RVA: 0x000035F0 File Offset: 0x000017F0
		public InputAction()
		{
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00003600 File Offset: 0x00001800
		public InputAction(string name = null, InputActionType type = InputActionType.Value, string binding = null, string interactions = null, string processors = null, string expectedControlType = null)
		{
			this.m_Name = name;
			this.m_Type = type;
			if (!string.IsNullOrEmpty(binding))
			{
				this.m_SingletonActionBindings = new InputBinding[]
				{
					new InputBinding
					{
						path = binding,
						interactions = interactions,
						processors = processors,
						action = this.m_Name
					}
				};
				this.m_BindingsStartIndex = 0;
				this.m_BindingsCount = 1;
			}
			else
			{
				this.m_Interactions = interactions;
				this.m_Processors = processors;
			}
			this.m_ExpectedControlType = expectedControlType;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0000369B File Offset: 0x0000189B
		public void Dispose()
		{
			InputActionMap actionMap = this.m_ActionMap;
			if (actionMap == null)
			{
				return;
			}
			InputActionState state = actionMap.m_State;
			if (state == null)
			{
				return;
			}
			state.Dispose();
		}

		// Token: 0x0600014D RID: 333 RVA: 0x000036B8 File Offset: 0x000018B8
		public override string ToString()
		{
			string text;
			if (this.m_Name == null)
			{
				text = "<Unnamed>";
			}
			else if (this.m_ActionMap != null && !this.isSingletonAction && !string.IsNullOrEmpty(this.m_ActionMap.name))
			{
				text = this.m_ActionMap.name + "/" + this.m_Name;
			}
			else
			{
				text = this.m_Name;
			}
			ReadOnlyArray<InputControl> controls = this.controls;
			if (controls.Count > 0)
			{
				text += "[";
				bool flag = true;
				foreach (InputControl inputControl in controls)
				{
					if (!flag)
					{
						text += ",";
					}
					text += inputControl.path;
					flag = false;
				}
				text += "]";
			}
			return text;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x000037A4 File Offset: 0x000019A4
		public void Enable()
		{
			if (this.enabled)
			{
				return;
			}
			InputActionMap orCreateActionMap = this.GetOrCreateActionMap();
			orCreateActionMap.ResolveBindingsIfNecessary();
			orCreateActionMap.m_State.EnableSingleAction(this);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x000037C7 File Offset: 0x000019C7
		public void Disable()
		{
			if (!this.enabled)
			{
				return;
			}
			this.m_ActionMap.m_State.DisableSingleAction(this);
		}

		// Token: 0x06000150 RID: 336 RVA: 0x000037E4 File Offset: 0x000019E4
		public InputAction Clone()
		{
			return new InputAction(this.m_Name, this.m_Type, null, null, null, null)
			{
				m_SingletonActionBindings = this.bindings.ToArray(),
				m_BindingsCount = this.m_BindingsCount,
				m_ExpectedControlType = this.m_ExpectedControlType,
				m_Interactions = this.m_Interactions,
				m_Processors = this.m_Processors
			};
		}

		// Token: 0x06000151 RID: 337 RVA: 0x0000384A File Offset: 0x00001A4A
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00003854 File Offset: 0x00001A54
		public unsafe TValue ReadValue<TValue>() where TValue : struct
		{
			InputActionState state = this.GetOrCreateActionMap().m_State;
			if (state == null)
			{
				return default(TValue);
			}
			InputActionState.TriggerState* ptr = state.actionStates + this.m_ActionIndexInState;
			if (!ptr->phase.IsInProgress())
			{
				return state.ApplyProcessors<TValue>(ptr->bindingIndex, default(TValue), null);
			}
			return state.ReadValue<TValue>(ptr->bindingIndex, ptr->controlIndex, false);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x000038C8 File Offset: 0x00001AC8
		public unsafe object ReadValueAsObject()
		{
			InputActionState state = this.GetOrCreateActionMap().m_State;
			if (state == null)
			{
				return null;
			}
			InputActionState.TriggerState* ptr = state.actionStates + this.m_ActionIndexInState;
			if (ptr->phase.IsInProgress())
			{
				int controlIndex = ptr->controlIndex;
				if (controlIndex != -1)
				{
					return state.ReadValueAsObject(ptr->bindingIndex, controlIndex, false);
				}
			}
			return null;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00003925 File Offset: 0x00001B25
		public void Reset()
		{
			InputActionState state = this.GetOrCreateActionMap().m_State;
			if (state == null)
			{
				return;
			}
			state.ResetActionState(this.m_ActionIndexInState, this.enabled ? InputActionPhase.Waiting : InputActionPhase.Disabled, true);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00003950 File Offset: 0x00001B50
		public unsafe bool IsPressed()
		{
			InputActionState state = this.GetOrCreateActionMap().m_State;
			return state != null && state.actionStates[this.m_ActionIndexInState].isPressed;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0000398C File Offset: 0x00001B8C
		public unsafe bool IsInProgress()
		{
			InputActionState state = this.GetOrCreateActionMap().m_State;
			return state != null && state.actionStates[this.m_ActionIndexInState].phase.IsInProgress();
		}

		// Token: 0x06000157 RID: 343 RVA: 0x000039CC File Offset: 0x00001BCC
		public unsafe bool WasPressedThisFrame()
		{
			InputActionState state = this.GetOrCreateActionMap().m_State;
			if (state != null)
			{
				ref InputActionState.TriggerState ptr = ref state.actionStates[this.m_ActionIndexInState];
				uint s_UpdateStepCount = InputUpdate.s_UpdateStepCount;
				return ptr.pressedInUpdate == s_UpdateStepCount && s_UpdateStepCount > 0U;
			}
			return false;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00003A14 File Offset: 0x00001C14
		public unsafe bool WasReleasedThisFrame()
		{
			InputActionState state = this.GetOrCreateActionMap().m_State;
			if (state != null)
			{
				ref InputActionState.TriggerState ptr = ref state.actionStates[this.m_ActionIndexInState];
				uint s_UpdateStepCount = InputUpdate.s_UpdateStepCount;
				return ptr.releasedInUpdate == s_UpdateStepCount && s_UpdateStepCount > 0U;
			}
			return false;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00003A5C File Offset: 0x00001C5C
		public unsafe bool WasPerformedThisFrame()
		{
			InputActionState state = this.GetOrCreateActionMap().m_State;
			if (state != null)
			{
				ref InputActionState.TriggerState ptr = ref state.actionStates[this.m_ActionIndexInState];
				uint s_UpdateStepCount = InputUpdate.s_UpdateStepCount;
				return ptr.lastPerformedInUpdate == s_UpdateStepCount && s_UpdateStepCount > 0U;
			}
			return false;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00003AA4 File Offset: 0x00001CA4
		public unsafe float GetTimeoutCompletionPercentage()
		{
			InputActionState state = this.GetOrCreateActionMap().m_State;
			if (state == null)
			{
				return 0f;
			}
			ref InputActionState.TriggerState ptr = ref state.actionStates[this.m_ActionIndexInState];
			int interactionIndex = ptr.interactionIndex;
			if (interactionIndex == -1)
			{
				return (float)((ptr.phase == InputActionPhase.Performed) ? 1 : 0);
			}
			ref InputActionState.InteractionState ptr2 = ref state.interactionStates[interactionIndex];
			InputActionPhase phase = ptr2.phase;
			if (phase != InputActionPhase.Started)
			{
				if (phase != InputActionPhase.Performed)
				{
					return 0f;
				}
				return 1f;
			}
			else
			{
				float num = 0f;
				if (ptr2.isTimerRunning)
				{
					float timerDuration = ptr2.timerDuration;
					double num2 = ptr2.timerStartTime + (double)timerDuration - InputState.currentTime;
					if (num2 <= 0.0)
					{
						num = 1f;
					}
					else
					{
						num = (float)(((double)timerDuration - num2) / (double)timerDuration);
					}
				}
				if (ptr2.totalTimeoutCompletionTimeRemaining > 0f)
				{
					return (ptr2.totalTimeoutCompletionDone + num * ptr2.timerDuration) / (ptr2.totalTimeoutCompletionDone + ptr2.totalTimeoutCompletionTimeRemaining);
				}
				return num;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00003BA7 File Offset: 0x00001DA7
		internal bool isSingletonAction
		{
			get
			{
				return this.m_ActionMap == null || this.m_ActionMap.m_SingletonAction == this;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00003BC4 File Offset: 0x00001DC4
		private unsafe InputActionState.TriggerState currentState
		{
			get
			{
				if (this.m_ActionIndexInState == -1)
				{
					return default(InputActionState.TriggerState);
				}
				return *this.m_ActionMap.m_State.FetchActionState(this);
			}
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00003BFA File Offset: 0x00001DFA
		internal string MakeSureIdIsInPlace()
		{
			if (string.IsNullOrEmpty(this.m_Id))
			{
				this.GenerateId();
			}
			return this.m_Id;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00003C18 File Offset: 0x00001E18
		internal void GenerateId()
		{
			this.m_Id = Guid.NewGuid().ToString();
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00003C3E File Offset: 0x00001E3E
		internal InputActionMap GetOrCreateActionMap()
		{
			if (this.m_ActionMap == null)
			{
				this.CreateInternalActionMapForSingletonAction();
			}
			return this.m_ActionMap;
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00003C54 File Offset: 0x00001E54
		private void CreateInternalActionMapForSingletonAction()
		{
			this.m_ActionMap = new InputActionMap
			{
				m_Actions = new InputAction[] { this },
				m_SingletonAction = this,
				m_Bindings = this.m_SingletonActionBindings
			};
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00003C91 File Offset: 0x00001E91
		internal void RequestInitialStateCheckOnEnabledAction()
		{
			this.GetOrCreateActionMap().m_State.SetInitialStateCheckPending(this.m_ActionIndexInState, true);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00003CAC File Offset: 0x00001EAC
		internal bool ActiveControlIsValid(InputControl control)
		{
			if (control == null)
			{
				return false;
			}
			InputDevice device = control.device;
			if (!device.added)
			{
				return false;
			}
			ReadOnlyArray<InputDevice>? devices = this.GetOrCreateActionMap().devices;
			return devices == null || devices.Value.ContainsReference(device);
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00003CF8 File Offset: 0x00001EF8
		internal InputBinding? FindEffectiveBindingMask()
		{
			if (this.m_BindingMask != null)
			{
				return this.m_BindingMask;
			}
			InputActionMap actionMap = this.m_ActionMap;
			if (actionMap != null && actionMap.m_BindingMask != null)
			{
				return this.m_ActionMap.m_BindingMask;
			}
			InputActionMap actionMap2 = this.m_ActionMap;
			if (actionMap2 == null)
			{
				return null;
			}
			InputActionAsset asset = actionMap2.m_Asset;
			if (asset == null)
			{
				return null;
			}
			return asset.m_BindingMask;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00003D6C File Offset: 0x00001F6C
		internal int BindingIndexOnActionToBindingIndexOnMap(int indexOfBindingOnAction)
		{
			InputBinding[] bindings = this.GetOrCreateActionMap().m_Bindings;
			int num = bindings.LengthSafe<InputBinding>();
			string name = this.name;
			int num2 = -1;
			for (int i = 0; i < num; i++)
			{
				if (bindings[i].TriggersAction(this))
				{
					num2++;
					if (num2 == indexOfBindingOnAction)
					{
						return i;
					}
				}
			}
			throw new ArgumentOutOfRangeException("indexOfBindingOnAction", string.Format("Binding index {0} is out of range for action '{1}' with {2} bindings", indexOfBindingOnAction, this, num2 + 1));
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00003DE0 File Offset: 0x00001FE0
		internal int BindingIndexOnMapToBindingIndexOnAction(int indexOfBindingOnMap)
		{
			InputBinding[] bindings = this.GetOrCreateActionMap().m_Bindings;
			string name = this.name;
			int num = 0;
			for (int i = indexOfBindingOnMap - 1; i >= 0; i--)
			{
				ref InputBinding ptr = ref bindings[i];
				if (string.Compare(ptr.action, name, StringComparison.InvariantCultureIgnoreCase) == 0 || ptr.action == this.m_Id)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x04000080 RID: 128
		[Tooltip("Human readable name of the action. Must be unique within its action map (case is ignored). Can be changed without breaking references to the action.")]
		[SerializeField]
		internal string m_Name;

		// Token: 0x04000081 RID: 129
		[Tooltip("Determines how the action triggers.\n\nA Value action will start and perform when a control moves from its default value and then perform on every value change. It will cancel when controls go back to default value. Also, when enabled, a Value action will respond right away to a control's current value.\n\nA Button action will start when a button is pressed and perform when the press threshold (see 'Default Button Press Point' in settings) is reached. It will cancel when the button is going below the release threshold (see 'Button Release Threshold' in settings). Also, if a button is already pressed when the action is enabled, the button has to be released first.\n\nA Pass-Through action will not explicitly start and will never cancel. Instead, for every value change on any bound control, the action will perform.")]
		[SerializeField]
		internal InputActionType m_Type;

		// Token: 0x04000082 RID: 130
		[FormerlySerializedAs("m_ExpectedControlLayout")]
		[Tooltip("The type of control expected by the action (e.g. \"Button\" or \"Stick\"). This will limit the controls shown when setting up bindings in the UI and will also limit which controls can be bound interactively to the action.")]
		[SerializeField]
		internal string m_ExpectedControlType;

		// Token: 0x04000083 RID: 131
		[Tooltip("Unique ID of the action (GUID). Used to reference the action from bindings such that actions can be renamed without breaking references.")]
		[SerializeField]
		internal string m_Id;

		// Token: 0x04000084 RID: 132
		[SerializeField]
		internal string m_Processors;

		// Token: 0x04000085 RID: 133
		[SerializeField]
		internal string m_Interactions;

		// Token: 0x04000086 RID: 134
		[SerializeField]
		internal InputBinding[] m_SingletonActionBindings;

		// Token: 0x04000087 RID: 135
		[SerializeField]
		internal InputAction.ActionFlags m_Flags;

		// Token: 0x04000088 RID: 136
		[NonSerialized]
		internal InputBinding? m_BindingMask;

		// Token: 0x04000089 RID: 137
		[NonSerialized]
		internal int m_BindingsStartIndex;

		// Token: 0x0400008A RID: 138
		[NonSerialized]
		internal int m_BindingsCount;

		// Token: 0x0400008B RID: 139
		[NonSerialized]
		internal int m_ControlStartIndex;

		// Token: 0x0400008C RID: 140
		[NonSerialized]
		internal int m_ControlCount;

		// Token: 0x0400008D RID: 141
		[NonSerialized]
		internal int m_ActionIndexInState = -1;

		// Token: 0x0400008E RID: 142
		[NonSerialized]
		internal InputActionMap m_ActionMap;

		// Token: 0x0400008F RID: 143
		[NonSerialized]
		internal CallbackArray<Action<InputAction.CallbackContext>> m_OnStarted;

		// Token: 0x04000090 RID: 144
		[NonSerialized]
		internal CallbackArray<Action<InputAction.CallbackContext>> m_OnCanceled;

		// Token: 0x04000091 RID: 145
		[NonSerialized]
		internal CallbackArray<Action<InputAction.CallbackContext>> m_OnPerformed;

		// Token: 0x02000150 RID: 336
		[Flags]
		internal enum ActionFlags
		{
			// Token: 0x0400071F RID: 1823
			WantsInitialStateCheck = 1
		}

		// Token: 0x02000151 RID: 337
		public struct CallbackContext
		{
			// Token: 0x170004CD RID: 1229
			// (get) Token: 0x060011EE RID: 4590 RVA: 0x00054768 File Offset: 0x00052968
			private int actionIndex
			{
				get
				{
					return this.m_ActionIndex;
				}
			}

			// Token: 0x170004CE RID: 1230
			// (get) Token: 0x060011EF RID: 4591 RVA: 0x00054770 File Offset: 0x00052970
			private unsafe int bindingIndex
			{
				get
				{
					return this.m_State.actionStates[this.actionIndex].bindingIndex;
				}
			}

			// Token: 0x170004CF RID: 1231
			// (get) Token: 0x060011F0 RID: 4592 RVA: 0x00054791 File Offset: 0x00052991
			private unsafe int controlIndex
			{
				get
				{
					return this.m_State.actionStates[this.actionIndex].controlIndex;
				}
			}

			// Token: 0x170004D0 RID: 1232
			// (get) Token: 0x060011F1 RID: 4593 RVA: 0x000547B2 File Offset: 0x000529B2
			private unsafe int interactionIndex
			{
				get
				{
					return this.m_State.actionStates[this.actionIndex].interactionIndex;
				}
			}

			// Token: 0x170004D1 RID: 1233
			// (get) Token: 0x060011F2 RID: 4594 RVA: 0x000547D3 File Offset: 0x000529D3
			public unsafe InputActionPhase phase
			{
				get
				{
					if (this.m_State == null)
					{
						return InputActionPhase.Disabled;
					}
					return this.m_State.actionStates[this.actionIndex].phase;
				}
			}

			// Token: 0x170004D2 RID: 1234
			// (get) Token: 0x060011F3 RID: 4595 RVA: 0x000547FE File Offset: 0x000529FE
			public bool started
			{
				get
				{
					return this.phase == InputActionPhase.Started;
				}
			}

			// Token: 0x170004D3 RID: 1235
			// (get) Token: 0x060011F4 RID: 4596 RVA: 0x00054809 File Offset: 0x00052A09
			public bool performed
			{
				get
				{
					return this.phase == InputActionPhase.Performed;
				}
			}

			// Token: 0x170004D4 RID: 1236
			// (get) Token: 0x060011F5 RID: 4597 RVA: 0x00054814 File Offset: 0x00052A14
			public bool canceled
			{
				get
				{
					return this.phase == InputActionPhase.Canceled;
				}
			}

			// Token: 0x170004D5 RID: 1237
			// (get) Token: 0x060011F6 RID: 4598 RVA: 0x0005481F File Offset: 0x00052A1F
			public InputAction action
			{
				get
				{
					InputActionState state = this.m_State;
					if (state == null)
					{
						return null;
					}
					return state.GetActionOrNull(this.bindingIndex);
				}
			}

			// Token: 0x170004D6 RID: 1238
			// (get) Token: 0x060011F7 RID: 4599 RVA: 0x00054838 File Offset: 0x00052A38
			public InputControl control
			{
				get
				{
					InputActionState state = this.m_State;
					if (state == null)
					{
						return null;
					}
					return state.controls[this.controlIndex];
				}
			}

			// Token: 0x170004D7 RID: 1239
			// (get) Token: 0x060011F8 RID: 4600 RVA: 0x00054854 File Offset: 0x00052A54
			public IInputInteraction interaction
			{
				get
				{
					if (this.m_State == null)
					{
						return null;
					}
					int interactionIndex = this.interactionIndex;
					if (interactionIndex == -1)
					{
						return null;
					}
					return this.m_State.interactions[interactionIndex];
				}
			}

			// Token: 0x170004D8 RID: 1240
			// (get) Token: 0x060011F9 RID: 4601 RVA: 0x00054885 File Offset: 0x00052A85
			public unsafe double time
			{
				get
				{
					if (this.m_State == null)
					{
						return 0.0;
					}
					return this.m_State.actionStates[this.actionIndex].time;
				}
			}

			// Token: 0x170004D9 RID: 1241
			// (get) Token: 0x060011FA RID: 4602 RVA: 0x000548B8 File Offset: 0x00052AB8
			public unsafe double startTime
			{
				get
				{
					if (this.m_State == null)
					{
						return 0.0;
					}
					return this.m_State.actionStates[this.actionIndex].startTime;
				}
			}

			// Token: 0x170004DA RID: 1242
			// (get) Token: 0x060011FB RID: 4603 RVA: 0x000548EB File Offset: 0x00052AEB
			public double duration
			{
				get
				{
					return this.time - this.startTime;
				}
			}

			// Token: 0x170004DB RID: 1243
			// (get) Token: 0x060011FC RID: 4604 RVA: 0x000548FA File Offset: 0x00052AFA
			public Type valueType
			{
				get
				{
					InputActionState state = this.m_State;
					if (state == null)
					{
						return null;
					}
					return state.GetValueType(this.bindingIndex, this.controlIndex);
				}
			}

			// Token: 0x170004DC RID: 1244
			// (get) Token: 0x060011FD RID: 4605 RVA: 0x00054919 File Offset: 0x00052B19
			public int valueSizeInBytes
			{
				get
				{
					if (this.m_State == null)
					{
						return 0;
					}
					return this.m_State.GetValueSizeInBytes(this.bindingIndex, this.controlIndex);
				}
			}

			// Token: 0x060011FE RID: 4606 RVA: 0x0005493C File Offset: 0x00052B3C
			public unsafe void ReadValue(void* buffer, int bufferSize)
			{
				if (buffer == null)
				{
					throw new ArgumentNullException("buffer");
				}
				if (this.m_State != null && this.phase.IsInProgress())
				{
					this.m_State.ReadValue(this.bindingIndex, this.controlIndex, buffer, bufferSize, false);
					return;
				}
				int valueSizeInBytes = this.valueSizeInBytes;
				if (bufferSize < valueSizeInBytes)
				{
					throw new ArgumentException(string.Format("Expected buffer of at least {0} bytes but got buffer of only {1} bytes", valueSizeInBytes, bufferSize), "bufferSize");
				}
				UnsafeUtility.MemClear(buffer, (long)this.valueSizeInBytes);
			}

			// Token: 0x060011FF RID: 4607 RVA: 0x000549C4 File Offset: 0x00052BC4
			public TValue ReadValue<TValue>() where TValue : struct
			{
				TValue tvalue = default(TValue);
				if (this.m_State != null)
				{
					tvalue = (this.phase.IsInProgress() ? this.m_State.ReadValue<TValue>(this.bindingIndex, this.controlIndex, false) : this.m_State.ApplyProcessors<TValue>(this.bindingIndex, tvalue, null));
				}
				return tvalue;
			}

			// Token: 0x06001200 RID: 4608 RVA: 0x00054A20 File Offset: 0x00052C20
			public bool ReadValueAsButton()
			{
				bool flag = false;
				if (this.m_State != null && this.phase.IsInProgress())
				{
					flag = this.m_State.ReadValueAsButton(this.bindingIndex, this.controlIndex);
				}
				return flag;
			}

			// Token: 0x06001201 RID: 4609 RVA: 0x00054A5D File Offset: 0x00052C5D
			public object ReadValueAsObject()
			{
				if (this.m_State != null && this.phase.IsInProgress())
				{
					return this.m_State.ReadValueAsObject(this.bindingIndex, this.controlIndex, false);
				}
				return null;
			}

			// Token: 0x06001202 RID: 4610 RVA: 0x00054A90 File Offset: 0x00052C90
			public override string ToString()
			{
				return string.Format("{{ action={0} phase={1} time={2} control={3} value={4} interaction={5} }}", new object[]
				{
					this.action,
					this.phase,
					this.time,
					this.control,
					this.ReadValueAsObject(),
					this.interaction
				});
			}

			// Token: 0x04000720 RID: 1824
			internal InputActionState m_State;

			// Token: 0x04000721 RID: 1825
			internal int m_ActionIndex;
		}
	}
}
