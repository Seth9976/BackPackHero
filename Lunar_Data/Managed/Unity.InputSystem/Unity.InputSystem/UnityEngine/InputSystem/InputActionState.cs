using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000023 RID: 35
	internal class InputActionState : IInputStateChangeMonitor, ICloneable, IDisposable
	{
		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000229 RID: 553 RVA: 0x00007E10 File Offset: 0x00006010
		public int totalCompositeCount
		{
			get
			{
				return this.memory.compositeCount;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600022A RID: 554 RVA: 0x00007E1D File Offset: 0x0000601D
		public int totalMapCount
		{
			get
			{
				return this.memory.mapCount;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600022B RID: 555 RVA: 0x00007E2A File Offset: 0x0000602A
		public int totalActionCount
		{
			get
			{
				return this.memory.actionCount;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600022C RID: 556 RVA: 0x00007E37 File Offset: 0x00006037
		public int totalBindingCount
		{
			get
			{
				return this.memory.bindingCount;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600022D RID: 557 RVA: 0x00007E44 File Offset: 0x00006044
		public int totalInteractionCount
		{
			get
			{
				return this.memory.interactionCount;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600022E RID: 558 RVA: 0x00007E51 File Offset: 0x00006051
		public int totalControlCount
		{
			get
			{
				return this.memory.controlCount;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600022F RID: 559 RVA: 0x00007E5E File Offset: 0x0000605E
		public unsafe InputActionState.ActionMapIndices* mapIndices
		{
			get
			{
				return this.memory.mapIndices;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000230 RID: 560 RVA: 0x00007E6B File Offset: 0x0000606B
		public unsafe InputActionState.TriggerState* actionStates
		{
			get
			{
				return this.memory.actionStates;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000231 RID: 561 RVA: 0x00007E78 File Offset: 0x00006078
		public unsafe InputActionState.BindingState* bindingStates
		{
			get
			{
				return this.memory.bindingStates;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000232 RID: 562 RVA: 0x00007E85 File Offset: 0x00006085
		public unsafe InputActionState.InteractionState* interactionStates
		{
			get
			{
				return this.memory.interactionStates;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000233 RID: 563 RVA: 0x00007E92 File Offset: 0x00006092
		public unsafe int* controlIndexToBindingIndex
		{
			get
			{
				return this.memory.controlIndexToBindingIndex;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000234 RID: 564 RVA: 0x00007E9F File Offset: 0x0000609F
		public unsafe ushort* controlGroupingAndComplexity
		{
			get
			{
				return this.memory.controlGroupingAndComplexity;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000235 RID: 565 RVA: 0x00007EAC File Offset: 0x000060AC
		public unsafe float* controlMagnitudes
		{
			get
			{
				return this.memory.controlMagnitudes;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000236 RID: 566 RVA: 0x00007EB9 File Offset: 0x000060B9
		public unsafe uint* enabledControls
		{
			get
			{
				return (uint*)this.memory.enabledControls;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000237 RID: 567 RVA: 0x00007EC6 File Offset: 0x000060C6
		public bool isProcessingControlStateChange
		{
			get
			{
				return this.m_InProcessControlStateChange;
			}
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00007ECE File Offset: 0x000060CE
		public void Initialize(InputBindingResolver resolver)
		{
			this.ClaimDataFrom(resolver);
			this.AddToGlobalList();
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00007EE0 File Offset: 0x000060E0
		private unsafe void ComputeControlGroupingIfNecessary()
		{
			if (this.memory.controlGroupingInitialized)
			{
				return;
			}
			bool flag = !InputSystem.settings.shortcutKeysConsumeInput;
			uint num = 1U;
			for (int i = 0; i < this.totalControlCount; i++)
			{
				InputControl inputControl = this.controls[i];
				int num2 = this.controlIndexToBindingIndex[i];
				ref InputActionState.BindingState ptr = ref this.bindingStates[num2];
				int num3 = 1;
				if (ptr.isPartOfComposite && !flag)
				{
					int compositeOrCompositeBindingIndex = ptr.compositeOrCompositeBindingIndex;
					for (int j = compositeOrCompositeBindingIndex + 1; j < this.totalBindingCount; j++)
					{
						ref InputActionState.BindingState ptr2 = ref this.bindingStates[j];
						if (!ptr2.isPartOfComposite || ptr2.compositeOrCompositeBindingIndex != compositeOrCompositeBindingIndex)
						{
							break;
						}
						num3++;
					}
				}
				this.controlGroupingAndComplexity[i * 2 + 1] = (ushort)num3;
				if (this.controlGroupingAndComplexity[i * 2] == 0)
				{
					if (!flag)
					{
						for (int k = 0; k < this.totalControlCount; k++)
						{
							InputControl inputControl2 = this.controls[k];
							if (inputControl == inputControl2)
							{
								this.controlGroupingAndComplexity[k * 2] = (ushort)num;
							}
						}
					}
					this.controlGroupingAndComplexity[i * 2] = (ushort)num;
					num += 1U;
				}
			}
			this.memory.controlGroupingInitialized = true;
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00008028 File Offset: 0x00006228
		public void ClaimDataFrom(InputBindingResolver resolver)
		{
			this.totalProcessorCount = resolver.totalProcessorCount;
			this.maps = resolver.maps;
			this.interactions = resolver.interactions;
			this.processors = resolver.processors;
			this.composites = resolver.composites;
			this.controls = resolver.controls;
			this.memory = resolver.memory;
			resolver.memory = default(InputActionState.UnmanagedMemory);
			this.ComputeControlGroupingIfNecessary();
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000809C File Offset: 0x0000629C
		~InputActionState()
		{
			this.Destroy(true);
		}

		// Token: 0x0600023C RID: 572 RVA: 0x000080CC File Offset: 0x000062CC
		public void Dispose()
		{
			this.Destroy(false);
		}

		// Token: 0x0600023D RID: 573 RVA: 0x000080D8 File Offset: 0x000062D8
		private unsafe void Destroy(bool isFinalizing = false)
		{
			if (!isFinalizing)
			{
				for (int i = 0; i < this.totalMapCount; i++)
				{
					InputActionMap inputActionMap = this.maps[i];
					if (inputActionMap.enabled)
					{
						this.DisableControls(i, this.mapIndices[i].controlStartIndex, this.mapIndices[i].controlCount);
					}
					if (inputActionMap.m_Asset != null)
					{
						inputActionMap.m_Asset.m_SharedStateForAllMaps = null;
					}
					inputActionMap.m_State = null;
					inputActionMap.m_MapIndexInState = -1;
					inputActionMap.m_EnabledActionsCount = 0;
					InputAction[] actions = inputActionMap.m_Actions;
					if (actions != null)
					{
						for (int j = 0; j < actions.Length; j++)
						{
							actions[j].m_ActionIndexInState = -1;
						}
					}
				}
				this.RemoveMapFromGlobalList();
			}
			this.memory.Dispose();
		}

		// Token: 0x0600023E RID: 574 RVA: 0x000081A8 File Offset: 0x000063A8
		public InputActionState Clone()
		{
			return new InputActionState
			{
				maps = ArrayHelpers.Copy<InputActionMap>(this.maps),
				controls = ArrayHelpers.Copy<InputControl>(this.controls),
				interactions = ArrayHelpers.Copy<IInputInteraction>(this.interactions),
				processors = ArrayHelpers.Copy<InputProcessor>(this.processors),
				composites = ArrayHelpers.Copy<InputBindingComposite>(this.composites),
				totalProcessorCount = this.totalProcessorCount,
				memory = this.memory.Clone()
			};
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000822C File Offset: 0x0000642C
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00008234 File Offset: 0x00006434
		private bool IsUsingDevice(InputDevice device)
		{
			bool flag = false;
			for (int i = 0; i < this.totalMapCount; i++)
			{
				ReadOnlyArray<InputDevice>? devices = this.maps[i].devices;
				if (devices == null)
				{
					flag = true;
				}
				else if (devices.Value.Contains(device))
				{
					return true;
				}
			}
			if (!flag)
			{
				return false;
			}
			for (int j = 0; j < this.totalControlCount; j++)
			{
				if (this.controls[j].device == device)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000241 RID: 577 RVA: 0x000082B0 File Offset: 0x000064B0
		private bool CanUseDevice(InputDevice device)
		{
			bool flag = false;
			for (int i = 0; i < this.totalMapCount; i++)
			{
				ReadOnlyArray<InputDevice>? devices = this.maps[i].devices;
				if (devices == null)
				{
					flag = true;
				}
				else if (devices.Value.Contains(device))
				{
					return true;
				}
			}
			if (!flag)
			{
				return false;
			}
			for (int j = 0; j < this.totalMapCount; j++)
			{
				InputBinding[] bindings = this.maps[j].m_Bindings;
				if (bindings != null)
				{
					int num = bindings.Length;
					for (int k = 0; k < num; k++)
					{
						if (InputControlPath.TryFindControl(device, bindings[k].effectivePath, 0) != null)
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000835C File Offset: 0x0000655C
		public bool HasEnabledActions()
		{
			for (int i = 0; i < this.totalMapCount; i++)
			{
				if (this.maps[i].enabled)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000838C File Offset: 0x0000658C
		private unsafe void FinishBindingCompositeSetups()
		{
			for (int i = 0; i < this.totalBindingCount; i++)
			{
				ref InputActionState.BindingState ptr = ref this.bindingStates[i];
				if (ptr.isComposite && ptr.compositeOrCompositeBindingIndex != -1)
				{
					InputBindingComposite inputBindingComposite = this.composites[ptr.compositeOrCompositeBindingIndex];
					InputBindingCompositeContext inputBindingCompositeContext = new InputBindingCompositeContext
					{
						m_State = this,
						m_BindingIndex = i
					};
					inputBindingComposite.CallFinishSetup(ref inputBindingCompositeContext);
				}
			}
		}

		// Token: 0x06000244 RID: 580 RVA: 0x000083FC File Offset: 0x000065FC
		internal unsafe void PrepareForBindingReResolution(bool needFullResolve, ref InputControlList<InputControl> activeControls, ref bool hasEnabledActions)
		{
			bool flag = false;
			for (int i = 0; i < this.totalMapCount; i++)
			{
				InputActionMap inputActionMap = this.maps[i];
				if (inputActionMap.enabled)
				{
					hasEnabledActions = true;
					if (needFullResolve)
					{
						this.DisableAllActions(inputActionMap);
					}
					else
					{
						foreach (InputAction inputAction in inputActionMap.actions)
						{
							if (inputAction.phase.IsInProgress())
							{
								if (inputAction.ActiveControlIsValid(inputAction.activeControl))
								{
									if (!flag)
									{
										activeControls = new InputControlList<InputControl>(Allocator.Temp, 0);
										activeControls.Resize(this.totalControlCount);
										flag = true;
									}
									ref InputActionState.TriggerState ptr = ref this.actionStates[inputAction.m_ActionIndexInState];
									int num = ptr.controlIndex;
									activeControls[num] = this.controls[num];
									InputActionState.BindingState bindingState = this.bindingStates[ptr.bindingIndex];
									for (int j = 0; j < bindingState.interactionCount; j++)
									{
										int num2 = bindingState.interactionStartIndex + j;
										if (this.interactionStates[num2].phase.IsInProgress())
										{
											num = this.interactionStates[num2].triggerControlIndex;
											if (inputAction.ActiveControlIsValid(this.controls[num]))
											{
												activeControls[num] = this.controls[num];
											}
											else
											{
												this.ResetInteractionState(num2);
											}
										}
									}
								}
								else
								{
									this.ResetActionState(inputAction.m_ActionIndexInState, InputActionPhase.Waiting, false);
								}
							}
						}
						this.DisableControls(inputActionMap);
					}
				}
				inputActionMap.ClearCachedActionData(!needFullResolve);
			}
			this.NotifyListenersOfActionChange(InputActionChange.BoundControlsAboutToChange);
		}

		// Token: 0x06000245 RID: 581 RVA: 0x000085DC File Offset: 0x000067DC
		public void FinishBindingResolution(bool hasEnabledActions, InputActionState.UnmanagedMemory oldMemory, InputControlList<InputControl> activeControls, bool isFullResolve)
		{
			this.FinishBindingCompositeSetups();
			if (hasEnabledActions)
			{
				this.RestoreActionStatesAfterReResolvingBindings(oldMemory, activeControls, isFullResolve);
				return;
			}
			this.NotifyListenersOfActionChange(InputActionChange.BoundControlsChanged);
		}

		// Token: 0x06000246 RID: 582 RVA: 0x000085FC File Offset: 0x000067FC
		private unsafe void RestoreActionStatesAfterReResolvingBindings(InputActionState.UnmanagedMemory oldState, InputControlList<InputControl> activeControls, bool isFullResolve)
		{
			for (int i = 0; i < this.totalActionCount; i++)
			{
				ref InputActionState.TriggerState ptr = ref oldState.actionStates[i];
				ref InputActionState.TriggerState ptr2 = ref this.actionStates[i];
				ptr2.lastCanceledInUpdate = ptr.lastCanceledInUpdate;
				ptr2.lastPerformedInUpdate = ptr.lastPerformedInUpdate;
				ptr2.pressedInUpdate = ptr.pressedInUpdate;
				ptr2.releasedInUpdate = ptr.releasedInUpdate;
				ptr2.startTime = ptr.startTime;
				if (ptr.phase != InputActionPhase.Disabled)
				{
					ptr2.phase = InputActionPhase.Waiting;
					if (isFullResolve)
					{
						this.maps[ptr2.mapIndex].m_EnabledActionsCount++;
					}
				}
			}
			for (int j = 0; j < this.totalBindingCount; j++)
			{
				ref InputActionState.BindingState ptr3 = ref this.memory.bindingStates[j];
				if (!ptr3.isPartOfComposite)
				{
					if (ptr3.isComposite)
					{
						int compositeOrCompositeBindingIndex = ptr3.compositeOrCompositeBindingIndex;
						this.memory.compositeMagnitudes[compositeOrCompositeBindingIndex] = oldState.compositeMagnitudes[compositeOrCompositeBindingIndex];
					}
					int actionIndex = ptr3.actionIndex;
					if (actionIndex != -1)
					{
						ref InputActionState.TriggerState ptr4 = ref this.actionStates[actionIndex];
						if (!ptr4.isDisabled)
						{
							ptr3.initialStateCheckPending = ptr3.wantsInitialStateCheck;
							this.EnableControls(ptr3.mapIndex, ptr3.controlStartIndex, ptr3.controlCount);
							if (!isFullResolve)
							{
								ref InputActionState.BindingState ptr5 = ref this.memory.bindingStates[j];
								ptr3.triggerEventIdForComposite = ptr5.triggerEventIdForComposite;
								ref InputActionState.TriggerState ptr6 = ref oldState.actionStates[actionIndex];
								if (j == ptr6.bindingIndex && ptr6.phase.IsInProgress() && activeControls.Count > 0 && activeControls[ptr6.controlIndex] != null)
								{
									InputControl inputControl = activeControls[ptr6.controlIndex];
									int num = this.FindControlIndexOnBinding(j, inputControl);
									if (num != -1)
									{
										ptr4.phase = ptr6.phase;
										ptr4.controlIndex = num;
										ptr4.magnitude = ptr6.magnitude;
										ptr4.interactionIndex = ptr6.interactionIndex;
										this.memory.controlMagnitudes[num] = ptr6.magnitude;
									}
									for (int k = 0; k < ptr3.interactionCount; k++)
									{
										ref InputActionState.InteractionState ptr7 = ref oldState.interactionStates[ptr5.interactionStartIndex + k];
										if (ptr7.phase.IsInProgress())
										{
											inputControl = activeControls[ptr7.triggerControlIndex];
											if (inputControl != null)
											{
												num = this.FindControlIndexOnBinding(j, inputControl);
												ref InputActionState.InteractionState ptr8 = ref this.interactionStates[ptr3.interactionStartIndex + k];
												ptr8.phase = ptr7.phase;
												ptr8.performedTime = ptr7.performedTime;
												ptr8.startTime = ptr7.startTime;
												ptr8.triggerControlIndex = num;
												if (ptr7.isTimerRunning)
												{
													InputActionState.TriggerState triggerState = new InputActionState.TriggerState
													{
														mapIndex = ptr3.mapIndex,
														controlIndex = num,
														bindingIndex = j,
														time = ptr7.timerStartTime,
														interactionIndex = ptr3.interactionStartIndex + k
													};
													this.StartTimeout(ptr7.timerDuration, ref triggerState);
													ptr8.totalTimeoutCompletionDone = ptr7.totalTimeoutCompletionDone;
													ptr8.totalTimeoutCompletionTimeRemaining = ptr7.totalTimeoutCompletionTimeRemaining;
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			this.HookOnBeforeUpdate();
			this.NotifyListenersOfActionChange(InputActionChange.BoundControlsChanged);
			if (isFullResolve && InputActionState.s_GlobalState.onActionChange.length > 0)
			{
				for (int l = 0; l < this.totalMapCount; l++)
				{
					InputActionMap inputActionMap = this.maps[l];
					if (inputActionMap.m_SingletonAction == null && inputActionMap.m_EnabledActionsCount == inputActionMap.m_Actions.LengthSafe<InputAction>())
					{
						InputActionState.NotifyListenersOfActionChange(InputActionChange.ActionMapEnabled, inputActionMap);
					}
					else
					{
						foreach (InputAction inputAction in inputActionMap.actions)
						{
							if (inputAction.enabled)
							{
								InputActionState.NotifyListenersOfActionChange(InputActionChange.ActionEnabled, inputAction);
							}
						}
					}
				}
			}
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00008A5C File Offset: 0x00006C5C
		private unsafe bool IsActiveControl(int bindingIndex, int controlIndex)
		{
			ref InputActionState.BindingState ptr = ref this.bindingStates[bindingIndex];
			int actionIndex = ptr.actionIndex;
			if (actionIndex == -1)
			{
				return false;
			}
			if (this.actionStates[actionIndex].controlIndex == controlIndex)
			{
				return true;
			}
			for (int i = 0; i < ptr.interactionCount; i++)
			{
				if (this.interactionStates[this.bindingStates->interactionStartIndex + i].triggerControlIndex == controlIndex)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00008ADC File Offset: 0x00006CDC
		private unsafe int FindControlIndexOnBinding(int bindingIndex, InputControl control)
		{
			int controlStartIndex = this.bindingStates[bindingIndex].controlStartIndex;
			int controlCount = this.bindingStates[bindingIndex].controlCount;
			for (int i = 0; i < controlCount; i++)
			{
				if (control == this.controls[controlStartIndex + i])
				{
					return controlStartIndex + i;
				}
			}
			return -1;
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00008B34 File Offset: 0x00006D34
		private unsafe void ResetActionStatesDrivenBy(InputDevice device)
		{
			using (InputActionRebindingExtensions.DeferBindingResolution())
			{
				for (int i = 0; i < this.totalActionCount; i++)
				{
					InputActionState.TriggerState* ptr = this.actionStates + i;
					if (ptr->phase != InputActionPhase.Waiting && ptr->phase != InputActionPhase.Disabled)
					{
						if (ptr->isPassThrough)
						{
							if (!this.IsActionBoundToControlFromDevice(device, i))
							{
								goto IL_0065;
							}
						}
						else
						{
							int controlIndex = ptr->controlIndex;
							if (controlIndex == -1 || this.controls[controlIndex].device != device)
							{
								goto IL_0065;
							}
						}
						this.ResetActionState(i, InputActionPhase.Waiting, false);
					}
					IL_0065:;
				}
			}
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00008BD0 File Offset: 0x00006DD0
		private unsafe bool IsActionBoundToControlFromDevice(InputDevice device, int actionIndex)
		{
			bool flag = false;
			ushort num;
			ushort actionBindingStartIndexAndCount = this.GetActionBindingStartIndexAndCount(actionIndex, out num);
			for (int i = 0; i < (int)num; i++)
			{
				ushort num2 = this.memory.actionBindingIndices[(int)actionBindingStartIndexAndCount + i];
				int controlCount = this.bindingStates[num2].controlCount;
				int controlStartIndex = this.bindingStates[num2].controlStartIndex;
				for (int j = 0; j < controlCount; j++)
				{
					if (this.controls[controlStartIndex + j].device == device)
					{
						flag = true;
						break;
					}
				}
			}
			return flag;
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00008C64 File Offset: 0x00006E64
		public unsafe void ResetActionState(int actionIndex, InputActionPhase toPhase = InputActionPhase.Waiting, bool hardReset = false)
		{
			InputActionState.TriggerState* ptr = this.actionStates + actionIndex;
			if (ptr->phase != InputActionPhase.Waiting && ptr->phase != InputActionPhase.Disabled)
			{
				ptr->time = InputState.currentTime;
				if (ptr->interactionIndex != -1)
				{
					int bindingIndex = ptr->bindingIndex;
					if (bindingIndex != -1)
					{
						int mapIndex = ptr->mapIndex;
						int interactionCount = this.bindingStates[bindingIndex].interactionCount;
						int interactionStartIndex = this.bindingStates[bindingIndex].interactionStartIndex;
						for (int i = 0; i < interactionCount; i++)
						{
							int num = interactionStartIndex + i;
							this.ResetInteractionStateAndCancelIfNecessary(mapIndex, bindingIndex, num);
						}
					}
				}
				else if (ptr->phase != InputActionPhase.Canceled)
				{
					this.ChangePhaseOfAction(InputActionPhase.Canceled, ref this.actionStates[actionIndex], InputActionPhase.Waiting);
				}
			}
			ptr->phase = toPhase;
			ptr->controlIndex = -1;
			ptr->bindingIndex = (int)this.memory.actionBindingIndices[this.memory.actionBindingIndicesAndCounts[actionIndex]];
			ptr->interactionIndex = -1;
			ptr->startTime = 0.0;
			ptr->time = 0.0;
			ptr->hasMultipleConcurrentActuations = false;
			ptr->inProcessing = false;
			ptr->isPressed = false;
			if (hardReset)
			{
				ptr->lastCanceledInUpdate = 0U;
				ptr->lastPerformedInUpdate = 0U;
				ptr->pressedInUpdate = 0U;
				ptr->releasedInUpdate = 0U;
			}
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00008DC0 File Offset: 0x00006FC0
		public unsafe ref InputActionState.TriggerState FetchActionState(InputAction action)
		{
			return ref this.actionStates[action.m_ActionIndexInState];
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00008DD7 File Offset: 0x00006FD7
		public unsafe InputActionState.ActionMapIndices FetchMapIndices(InputActionMap map)
		{
			return this.mapIndices[map.m_MapIndexInState];
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00008DF4 File Offset: 0x00006FF4
		public unsafe void EnableAllActions(InputActionMap map)
		{
			this.EnableControls(map);
			int mapIndexInState = map.m_MapIndexInState;
			int actionCount = this.mapIndices[mapIndexInState].actionCount;
			int actionStartIndex = this.mapIndices[mapIndexInState].actionStartIndex;
			for (int i = 0; i < actionCount; i++)
			{
				int num = actionStartIndex + i;
				InputActionState.TriggerState* ptr = this.actionStates + num;
				if (ptr->isDisabled)
				{
					ptr->phase = InputActionPhase.Waiting;
				}
				ptr->inProcessing = false;
			}
			map.m_EnabledActionsCount = actionCount;
			this.HookOnBeforeUpdate();
			if (map.m_SingletonAction != null)
			{
				InputActionState.NotifyListenersOfActionChange(InputActionChange.ActionEnabled, map.m_SingletonAction);
				return;
			}
			InputActionState.NotifyListenersOfActionChange(InputActionChange.ActionMapEnabled, map);
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00008EA4 File Offset: 0x000070A4
		private unsafe void EnableControls(InputActionMap map)
		{
			int mapIndexInState = map.m_MapIndexInState;
			int controlCount = this.mapIndices[mapIndexInState].controlCount;
			int controlStartIndex = this.mapIndices[mapIndexInState].controlStartIndex;
			if (controlCount > 0)
			{
				this.EnableControls(mapIndexInState, controlStartIndex, controlCount);
			}
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00008EF4 File Offset: 0x000070F4
		public unsafe void EnableSingleAction(InputAction action)
		{
			this.EnableControls(action);
			int actionIndexInState = action.m_ActionIndexInState;
			this.actionStates[actionIndexInState].phase = InputActionPhase.Waiting;
			action.m_ActionMap.m_EnabledActionsCount++;
			this.HookOnBeforeUpdate();
			InputActionState.NotifyListenersOfActionChange(InputActionChange.ActionEnabled, action);
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00008F48 File Offset: 0x00007148
		private unsafe void EnableControls(InputAction action)
		{
			int actionIndexInState = action.m_ActionIndexInState;
			int mapIndexInState = action.m_ActionMap.m_MapIndexInState;
			int bindingStartIndex = this.mapIndices[mapIndexInState].bindingStartIndex;
			int bindingCount = this.mapIndices[mapIndexInState].bindingCount;
			InputActionState.BindingState* bindingStates = this.memory.bindingStates;
			for (int i = 0; i < bindingCount; i++)
			{
				int num = bindingStartIndex + i;
				InputActionState.BindingState* ptr = bindingStates + num;
				if (ptr->actionIndex == actionIndexInState && !ptr->isPartOfComposite)
				{
					int controlCount = ptr->controlCount;
					if (controlCount != 0)
					{
						this.EnableControls(mapIndexInState, ptr->controlStartIndex, controlCount);
					}
				}
			}
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00008FF8 File Offset: 0x000071F8
		public unsafe void DisableAllActions(InputActionMap map)
		{
			this.DisableControls(map);
			int mapIndexInState = map.m_MapIndexInState;
			int actionStartIndex = this.mapIndices[mapIndexInState].actionStartIndex;
			int actionCount = this.mapIndices[mapIndexInState].actionCount;
			bool flag = map.m_EnabledActionsCount == actionCount;
			for (int i = 0; i < actionCount; i++)
			{
				int num = actionStartIndex + i;
				if (this.actionStates[num].phase != InputActionPhase.Disabled)
				{
					this.ResetActionState(num, InputActionPhase.Disabled, false);
					if (!flag)
					{
						InputActionState.NotifyListenersOfActionChange(InputActionChange.ActionDisabled, map.m_Actions[i]);
					}
				}
			}
			map.m_EnabledActionsCount = 0;
			if (map.m_SingletonAction != null)
			{
				InputActionState.NotifyListenersOfActionChange(InputActionChange.ActionDisabled, map.m_SingletonAction);
				return;
			}
			if (flag)
			{
				InputActionState.NotifyListenersOfActionChange(InputActionChange.ActionMapDisabled, map);
			}
		}

		// Token: 0x06000253 RID: 595 RVA: 0x000090BC File Offset: 0x000072BC
		public unsafe void DisableControls(InputActionMap map)
		{
			int mapIndexInState = map.m_MapIndexInState;
			int controlCount = this.mapIndices[mapIndexInState].controlCount;
			int controlStartIndex = this.mapIndices[mapIndexInState].controlStartIndex;
			if (controlCount > 0)
			{
				this.DisableControls(mapIndexInState, controlStartIndex, controlCount);
			}
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00009109 File Offset: 0x00007309
		public void DisableSingleAction(InputAction action)
		{
			this.DisableControls(action);
			this.ResetActionState(action.m_ActionIndexInState, InputActionPhase.Disabled, false);
			action.m_ActionMap.m_EnabledActionsCount--;
			InputActionState.NotifyListenersOfActionChange(InputActionChange.ActionDisabled, action);
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000913C File Offset: 0x0000733C
		private unsafe void DisableControls(InputAction action)
		{
			int actionIndexInState = action.m_ActionIndexInState;
			int mapIndexInState = action.m_ActionMap.m_MapIndexInState;
			int bindingStartIndex = this.mapIndices[mapIndexInState].bindingStartIndex;
			int bindingCount = this.mapIndices[mapIndexInState].bindingCount;
			InputActionState.BindingState* bindingStates = this.memory.bindingStates;
			for (int i = 0; i < bindingCount; i++)
			{
				int num = bindingStartIndex + i;
				InputActionState.BindingState* ptr = bindingStates + num;
				if (ptr->actionIndex == actionIndexInState && !ptr->isPartOfComposite)
				{
					int controlCount = ptr->controlCount;
					if (controlCount != 0)
					{
						this.DisableControls(mapIndexInState, ptr->controlStartIndex, controlCount);
					}
				}
			}
		}

		// Token: 0x06000256 RID: 598 RVA: 0x000091EC File Offset: 0x000073EC
		private unsafe void EnableControls(int mapIndex, int controlStartIndex, int numControls)
		{
			InputManager s_Manager = InputSystem.s_Manager;
			for (int i = 0; i < numControls; i++)
			{
				int num = controlStartIndex + i;
				if (!this.IsControlEnabled(num))
				{
					int num2 = this.controlIndexToBindingIndex[num];
					long num3 = this.ToCombinedMapAndControlAndBindingIndex(mapIndex, num, num2);
					InputActionState.BindingState* ptr = this.bindingStates + num2;
					if (ptr->wantsInitialStateCheck)
					{
						this.SetInitialStateCheckPending(ptr, true);
					}
					s_Manager.AddStateChangeMonitor(this.controls[num], this, num3, (uint)this.controlGroupingAndComplexity[num * 2]);
					this.SetControlEnabled(num, true);
				}
			}
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000927C File Offset: 0x0000747C
		private unsafe void DisableControls(int mapIndex, int controlStartIndex, int numControls)
		{
			InputManager s_Manager = InputSystem.s_Manager;
			for (int i = 0; i < numControls; i++)
			{
				int num = controlStartIndex + i;
				if (this.IsControlEnabled(num))
				{
					int num2 = this.controlIndexToBindingIndex[num];
					long num3 = this.ToCombinedMapAndControlAndBindingIndex(mapIndex, num, num2);
					InputActionState.BindingState* ptr = this.bindingStates + num2;
					if (ptr->wantsInitialStateCheck)
					{
						this.SetInitialStateCheckPending(ptr, false);
					}
					s_Manager.RemoveStateChangeMonitor(this.controls[num], this, num3);
					this.SetControlEnabled(num, false);
				}
			}
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00009300 File Offset: 0x00007500
		public unsafe void SetInitialStateCheckPending(int actionIndex, bool value = true)
		{
			int mapIndex = this.actionStates[actionIndex].mapIndex;
			int bindingStartIndex = this.mapIndices[mapIndex].bindingStartIndex;
			int bindingCount = this.mapIndices[mapIndex].bindingCount;
			for (int i = 0; i < bindingCount; i++)
			{
				ref InputActionState.BindingState ptr = ref this.bindingStates[bindingStartIndex + i];
				if (ptr.actionIndex == actionIndex && !ptr.isPartOfComposite)
				{
					ptr.initialStateCheckPending = value;
				}
			}
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000938C File Offset: 0x0000758C
		private unsafe void SetInitialStateCheckPending(InputActionState.BindingState* bindingStatePtr, bool value)
		{
			if (bindingStatePtr->isPartOfComposite)
			{
				int compositeOrCompositeBindingIndex = bindingStatePtr->compositeOrCompositeBindingIndex;
				this.bindingStates[compositeOrCompositeBindingIndex].initialStateCheckPending = value;
				return;
			}
			bindingStatePtr->initialStateCheckPending = value;
		}

		// Token: 0x0600025A RID: 602 RVA: 0x000093C8 File Offset: 0x000075C8
		private unsafe bool IsControlEnabled(int controlIndex)
		{
			int num = controlIndex / 32;
			uint num2 = 1U << controlIndex % 32;
			return (this.enabledControls[num] & num2) > 0U;
		}

		// Token: 0x0600025B RID: 603 RVA: 0x000093F8 File Offset: 0x000075F8
		private unsafe void SetControlEnabled(int controlIndex, bool state)
		{
			int num = controlIndex / 32;
			uint num2 = 1U << controlIndex % 32;
			if (state)
			{
				this.enabledControls[num] |= num2;
				return;
			}
			this.enabledControls[num] &= ~num2;
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00009439 File Offset: 0x00007639
		private void HookOnBeforeUpdate()
		{
			if (this.m_OnBeforeUpdateHooked)
			{
				return;
			}
			if (this.m_OnBeforeUpdateDelegate == null)
			{
				this.m_OnBeforeUpdateDelegate = new Action(this.OnBeforeInitialUpdate);
			}
			InputSystem.s_Manager.onBeforeUpdate += this.m_OnBeforeUpdateDelegate;
			this.m_OnBeforeUpdateHooked = true;
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00009475 File Offset: 0x00007675
		private void UnhookOnBeforeUpdate()
		{
			if (!this.m_OnBeforeUpdateHooked)
			{
				return;
			}
			InputSystem.s_Manager.onBeforeUpdate -= this.m_OnBeforeUpdateDelegate;
			this.m_OnBeforeUpdateHooked = false;
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00009498 File Offset: 0x00007698
		private unsafe void OnBeforeInitialUpdate()
		{
			if (InputState.currentUpdateType == InputUpdateType.BeforeRender)
			{
				return;
			}
			this.UnhookOnBeforeUpdate();
			double currentTime = InputState.currentTime;
			InputManager s_Manager = InputSystem.s_Manager;
			for (int i = 0; i < this.totalBindingCount; i++)
			{
				ref InputActionState.BindingState ptr = ref this.bindingStates[i];
				if (ptr.initialStateCheckPending)
				{
					ptr.initialStateCheckPending = false;
					int controlStartIndex = ptr.controlStartIndex;
					int controlCount = ptr.controlCount;
					bool isComposite = ptr.isComposite;
					bool flag = false;
					for (int j = 0; j < controlCount; j++)
					{
						int num = controlStartIndex + j;
						InputControl inputControl = this.controls[num];
						if (!this.IsActiveControl(i, num) && !inputControl.CheckStateIsAtDefault())
						{
							if (inputControl.IsValueConsideredPressed(inputControl.magnitude) && (ptr.pressTime == 0.0 || ptr.pressTime > currentTime))
							{
								ptr.pressTime = currentTime;
							}
							if (!isComposite || !flag)
							{
								s_Manager.SignalStateChangeMonitor(inputControl, this);
								flag = true;
							}
						}
					}
				}
			}
			s_Manager.FireStateChangeNotifications();
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00009598 File Offset: 0x00007798
		void IInputStateChangeMonitor.NotifyControlStateChanged(InputControl control, double time, InputEventPtr eventPtr, long mapControlAndBindingIndex)
		{
			int num;
			int num2;
			int num3;
			this.SplitUpMapAndControlAndBindingIndex(mapControlAndBindingIndex, out num, out num2, out num3);
			this.ProcessControlStateChange(num, num2, num3, time, eventPtr);
		}

		// Token: 0x06000260 RID: 608 RVA: 0x000095C0 File Offset: 0x000077C0
		void IInputStateChangeMonitor.NotifyTimerExpired(InputControl control, double time, long mapControlAndBindingIndex, int interactionIndex)
		{
			int num;
			int num2;
			int num3;
			this.SplitUpMapAndControlAndBindingIndex(mapControlAndBindingIndex, out num, out num2, out num3);
			this.ProcessTimeout(time, num, num2, num3, interactionIndex);
		}

		// Token: 0x06000261 RID: 609 RVA: 0x000095E8 File Offset: 0x000077E8
		private unsafe long ToCombinedMapAndControlAndBindingIndex(int mapIndex, int controlIndex, int bindingIndex)
		{
			ushort num = this.controlGroupingAndComplexity[controlIndex * 2 + 1];
			return (long)controlIndex | ((long)bindingIndex << 24) | ((long)mapIndex << 40) | (long)((long)((ulong)num) << 48);
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000961A File Offset: 0x0000781A
		private void SplitUpMapAndControlAndBindingIndex(long mapControlAndBindingIndex, out int mapIndex, out int controlIndex, out int bindingIndex)
		{
			controlIndex = (int)(mapControlAndBindingIndex & 16777215L);
			bindingIndex = (int)((mapControlAndBindingIndex >> 24) & 65535L);
			mapIndex = (int)((mapControlAndBindingIndex >> 40) & 255L);
		}

		// Token: 0x06000263 RID: 611 RVA: 0x00009644 File Offset: 0x00007844
		internal static int GetComplexityFromMonitorIndex(long mapControlAndBindingIndex)
		{
			return (int)((mapControlAndBindingIndex >> 48) & 255L);
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00009654 File Offset: 0x00007854
		private unsafe void ProcessControlStateChange(int mapIndex, int controlIndex, int bindingIndex, double time, InputEventPtr eventPtr)
		{
			using (InputActionRebindingExtensions.DeferBindingResolution())
			{
				this.m_InProcessControlStateChange = true;
				this.m_CurrentlyProcessingThisEvent = eventPtr;
				try
				{
					InputActionState.BindingState* ptr = this.bindingStates + bindingIndex;
					int actionIndex = ptr->actionIndex;
					InputActionState.TriggerState triggerState = new InputActionState.TriggerState
					{
						mapIndex = mapIndex,
						controlIndex = controlIndex,
						bindingIndex = bindingIndex,
						interactionIndex = -1,
						time = time,
						startTime = time,
						isPassThrough = (actionIndex != -1 && this.actionStates[actionIndex].isPassThrough),
						isButton = (actionIndex != -1 && this.actionStates[actionIndex].isButton)
					};
					if (this.m_OnBeforeUpdateHooked)
					{
						ptr->initialStateCheckPending = false;
					}
					InputControl inputControl = this.controls[controlIndex];
					triggerState.magnitude = (inputControl.CheckStateIsAtDefault() ? 0f : inputControl.magnitude);
					this.controlMagnitudes[controlIndex] = triggerState.magnitude;
					if (inputControl.IsValueConsideredPressed(triggerState.magnitude) && (ptr->pressTime == 0.0 || ptr->pressTime > triggerState.time))
					{
						ptr->pressTime = triggerState.time;
					}
					bool flag = false;
					if (ptr->isPartOfComposite)
					{
						int compositeOrCompositeBindingIndex = ptr->compositeOrCompositeBindingIndex;
						InputActionState.BindingState* ptr2 = this.bindingStates + compositeOrCompositeBindingIndex;
						if (InputActionState.ShouldIgnoreInputOnCompositeBinding(ptr2, eventPtr))
						{
							return;
						}
						int compositeOrCompositeBindingIndex2 = this.bindingStates[compositeOrCompositeBindingIndex].compositeOrCompositeBindingIndex;
						InputBindingCompositeContext inputBindingCompositeContext = new InputBindingCompositeContext
						{
							m_State = this,
							m_BindingIndex = compositeOrCompositeBindingIndex
						};
						triggerState.magnitude = this.composites[compositeOrCompositeBindingIndex2].EvaluateMagnitude(ref inputBindingCompositeContext);
						this.memory.compositeMagnitudes[compositeOrCompositeBindingIndex2] = triggerState.magnitude;
						int interactionCount = ptr2->interactionCount;
						if (interactionCount > 0)
						{
							flag = true;
							this.ProcessInteractions(ref triggerState, ptr2->interactionStartIndex, interactionCount);
						}
					}
					bool flag2 = this.IsConflictingInput(ref triggerState, actionIndex);
					ptr = this.bindingStates + triggerState.bindingIndex;
					if (!flag2)
					{
						this.ProcessButtonState(ref triggerState, actionIndex, ptr);
					}
					int interactionCount2 = ptr->interactionCount;
					if (interactionCount2 > 0 && !ptr->isPartOfComposite)
					{
						this.ProcessInteractions(ref triggerState, ptr->interactionStartIndex, interactionCount2);
					}
					else if (!flag && !flag2)
					{
						this.ProcessDefaultInteraction(ref triggerState, actionIndex);
					}
				}
				finally
				{
					this.m_InProcessControlStateChange = false;
					this.m_CurrentlyProcessingThisEvent = default(InputEventPtr);
				}
			}
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000991C File Offset: 0x00007B1C
		private unsafe void ProcessButtonState(ref InputActionState.TriggerState trigger, int actionIndex, InputActionState.BindingState* bindingStatePtr)
		{
			InputControl inputControl = this.controls[trigger.controlIndex];
			float num = (inputControl.isButton ? ((ButtonControl)inputControl).pressPointOrDefault : ButtonControl.s_GlobalDefaultButtonPressPoint);
			if (this.controlMagnitudes[trigger.controlIndex] <= num * ButtonControl.s_GlobalDefaultButtonReleaseThreshold)
			{
				bindingStatePtr->pressTime = 0.0;
			}
			float magnitude = trigger.magnitude;
			InputActionState.TriggerState* ptr = this.actionStates + actionIndex;
			if (!ptr->isPressed && magnitude >= num)
			{
				ptr->pressedInUpdate = InputUpdate.s_UpdateStepCount;
				ptr->isPressed = true;
				return;
			}
			if (ptr->isPressed)
			{
				float num2 = num * ButtonControl.s_GlobalDefaultButtonReleaseThreshold;
				if (magnitude <= num2)
				{
					ptr->releasedInUpdate = InputUpdate.s_UpdateStepCount;
					ptr->isPressed = false;
				}
			}
		}

		// Token: 0x06000266 RID: 614 RVA: 0x000099DC File Offset: 0x00007BDC
		private unsafe static bool ShouldIgnoreInputOnCompositeBinding(InputActionState.BindingState* binding, InputEvent* eventPtr)
		{
			if (eventPtr == null)
			{
				return false;
			}
			int eventId = eventPtr->eventId;
			if (eventId != 0 && binding->triggerEventIdForComposite == eventId)
			{
				return true;
			}
			binding->triggerEventIdForComposite = eventId;
			return false;
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00009A10 File Offset: 0x00007C10
		private unsafe bool IsConflictingInput(ref InputActionState.TriggerState trigger, int actionIndex)
		{
			InputActionState.TriggerState* ptr = this.actionStates + actionIndex;
			if (!ptr->mayNeedConflictResolution)
			{
				return false;
			}
			int num = trigger.controlIndex;
			if (this.bindingStates[trigger.bindingIndex].isPartOfComposite)
			{
				int compositeOrCompositeBindingIndex = this.bindingStates[trigger.bindingIndex].compositeOrCompositeBindingIndex;
				num = this.bindingStates[compositeOrCompositeBindingIndex].controlStartIndex;
			}
			int num2 = ptr->controlIndex;
			if (this.bindingStates[ptr->bindingIndex].isPartOfComposite)
			{
				int compositeOrCompositeBindingIndex2 = this.bindingStates[ptr->bindingIndex].compositeOrCompositeBindingIndex;
				num2 = this.bindingStates[compositeOrCompositeBindingIndex2].controlStartIndex;
			}
			if (num2 == -1)
			{
				ptr->magnitude = trigger.magnitude;
				return false;
			}
			bool flag = num == num2 || this.controls[num] == this.controls[num2];
			if (trigger.magnitude > ptr->magnitude)
			{
				if (trigger.magnitude > 0f && !flag && ptr->magnitude > 0f)
				{
					ptr->hasMultipleConcurrentActuations = true;
				}
				ptr->magnitude = trigger.magnitude;
				return false;
			}
			if (trigger.magnitude < ptr->magnitude)
			{
				if (!flag)
				{
					if (trigger.magnitude > 0f)
					{
						ptr->hasMultipleConcurrentActuations = true;
					}
					return true;
				}
				if (!ptr->hasMultipleConcurrentActuations)
				{
					ptr->magnitude = trigger.magnitude;
					return false;
				}
				ushort num3;
				ushort actionBindingStartIndexAndCount = this.GetActionBindingStartIndexAndCount(actionIndex, out num3);
				float num4 = trigger.magnitude;
				int num5 = -1;
				int num6 = -1;
				int num7 = 0;
				for (int i = 0; i < (int)num3; i++)
				{
					ushort num8 = this.memory.actionBindingIndices[(int)actionBindingStartIndexAndCount + i];
					InputActionState.BindingState* ptr2 = this.memory.bindingStates + num8;
					if (ptr2->isComposite)
					{
						int controlStartIndex = ptr2->controlStartIndex;
						int compositeOrCompositeBindingIndex3 = ptr2->compositeOrCompositeBindingIndex;
						float num9 = this.memory.compositeMagnitudes[compositeOrCompositeBindingIndex3];
						if (num9 > 0f)
						{
							num7++;
						}
						if (num9 > num4)
						{
							num5 = controlStartIndex;
							num6 = this.controlIndexToBindingIndex[controlStartIndex];
							num4 = num9;
						}
					}
					else if (!ptr2->isPartOfComposite)
					{
						for (int j = 0; j < ptr2->controlCount; j++)
						{
							int num10 = ptr2->controlStartIndex + j;
							float num11 = this.memory.controlMagnitudes[num10];
							if (num11 > 0f)
							{
								num7++;
							}
							if (num11 > num4)
							{
								num5 = num10;
								num6 = (int)num8;
								num4 = num11;
							}
						}
					}
				}
				if (num7 <= 1)
				{
					ptr->hasMultipleConcurrentActuations = false;
				}
				if (num5 != -1)
				{
					trigger.controlIndex = num5;
					trigger.bindingIndex = num6;
					trigger.magnitude = num4;
					if (ptr->bindingIndex != num6)
					{
						if (ptr->interactionIndex != -1)
						{
							this.ResetInteractionState(ptr->interactionIndex);
						}
						InputActionState.BindingState* ptr3 = this.bindingStates + num6;
						int interactionCount = ptr3->interactionCount;
						int interactionStartIndex = ptr3->interactionStartIndex;
						for (int k = 0; k < interactionCount; k++)
						{
							if (this.interactionStates[interactionStartIndex + k].phase.IsInProgress())
							{
								ptr->interactionIndex = interactionStartIndex + k;
								trigger.interactionIndex = interactionStartIndex + k;
								break;
							}
						}
					}
					ptr->controlIndex = num5;
					ptr->bindingIndex = num6;
					ptr->magnitude = num4;
					return false;
				}
			}
			if (!flag && Mathf.Approximately(trigger.magnitude, ptr->magnitude))
			{
				if (trigger.magnitude > 0f)
				{
					ptr->hasMultipleConcurrentActuations = true;
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000268 RID: 616 RVA: 0x00009DAD File Offset: 0x00007FAD
		private unsafe ushort GetActionBindingStartIndexAndCount(int actionIndex, out ushort bindingCount)
		{
			bindingCount = this.memory.actionBindingIndicesAndCounts[actionIndex * 2 + 1];
			return this.memory.actionBindingIndicesAndCounts[actionIndex * 2];
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00009DDC File Offset: 0x00007FDC
		private unsafe void ProcessDefaultInteraction(ref InputActionState.TriggerState trigger, int actionIndex)
		{
			InputActionState.TriggerState* ptr = this.actionStates + actionIndex;
			switch (ptr->phase)
			{
			case InputActionPhase.Waiting:
				if (trigger.isPassThrough)
				{
					this.ChangePhaseOfAction(InputActionPhase.Performed, ref trigger, InputActionPhase.Waiting);
					return;
				}
				if (trigger.isButton)
				{
					float magnitude = trigger.magnitude;
					if (magnitude > 0f)
					{
						this.ChangePhaseOfAction(InputActionPhase.Started, ref trigger, InputActionPhase.Waiting);
					}
					ButtonControl buttonControl = this.controls[trigger.controlIndex] as ButtonControl;
					float num = ((buttonControl != null) ? buttonControl.pressPointOrDefault : ButtonControl.s_GlobalDefaultButtonPressPoint);
					if (magnitude >= num)
					{
						this.ChangePhaseOfAction(InputActionPhase.Performed, ref trigger, InputActionPhase.Performed);
						return;
					}
				}
				else if (InputActionState.IsActuated(ref trigger, 0f))
				{
					this.ChangePhaseOfAction(InputActionPhase.Started, ref trigger, InputActionPhase.Waiting);
					this.ChangePhaseOfAction(InputActionPhase.Performed, ref trigger, InputActionPhase.Started);
					return;
				}
				break;
			case InputActionPhase.Started:
				if (ptr->isButton)
				{
					float magnitude2 = trigger.magnitude;
					ButtonControl buttonControl2 = this.controls[trigger.controlIndex] as ButtonControl;
					float num2 = ((buttonControl2 != null) ? buttonControl2.pressPointOrDefault : ButtonControl.s_GlobalDefaultButtonPressPoint);
					if (magnitude2 >= num2)
					{
						this.ChangePhaseOfAction(InputActionPhase.Performed, ref trigger, InputActionPhase.Performed);
						return;
					}
					if (Mathf.Approximately(magnitude2, 0f))
					{
						this.ChangePhaseOfAction(InputActionPhase.Canceled, ref trigger, InputActionPhase.Waiting);
						return;
					}
				}
				else
				{
					if (!InputActionState.IsActuated(ref trigger, 0f))
					{
						this.ChangePhaseOfAction(InputActionPhase.Canceled, ref trigger, InputActionPhase.Waiting);
						return;
					}
					this.ChangePhaseOfAction(InputActionPhase.Performed, ref trigger, InputActionPhase.Started);
					return;
				}
				break;
			case InputActionPhase.Performed:
				if (ptr->isButton)
				{
					float magnitude3 = trigger.magnitude;
					ButtonControl buttonControl3 = this.controls[trigger.controlIndex] as ButtonControl;
					float num3 = ((buttonControl3 != null) ? buttonControl3.pressPointOrDefault : ButtonControl.s_GlobalDefaultButtonPressPoint);
					if (Mathf.Approximately(0f, magnitude3))
					{
						this.ChangePhaseOfAction(InputActionPhase.Canceled, ref trigger, InputActionPhase.Waiting);
						return;
					}
					float num4 = num3 * ButtonControl.s_GlobalDefaultButtonReleaseThreshold;
					if (magnitude3 <= num4)
					{
						this.ChangePhaseOfAction(InputActionPhase.Started, ref trigger, InputActionPhase.Waiting);
						return;
					}
				}
				else if (ptr->isPassThrough)
				{
					this.ChangePhaseOfAction(InputActionPhase.Performed, ref trigger, InputActionPhase.Performed);
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00009FAC File Offset: 0x000081AC
		private unsafe void ProcessInteractions(ref InputActionState.TriggerState trigger, int interactionStartIndex, int interactionCount)
		{
			InputInteractionContext inputInteractionContext = new InputInteractionContext
			{
				m_State = this,
				m_TriggerState = trigger
			};
			for (int i = 0; i < interactionCount; i++)
			{
				int num = interactionStartIndex + i;
				InputActionState.InteractionState interactionState = this.interactionStates[num];
				IInputInteraction inputInteraction = this.interactions[num];
				inputInteractionContext.m_TriggerState.phase = interactionState.phase;
				inputInteractionContext.m_TriggerState.startTime = interactionState.startTime;
				inputInteractionContext.m_TriggerState.interactionIndex = num;
				inputInteraction.Process(ref inputInteractionContext);
			}
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000A044 File Offset: 0x00008244
		private unsafe void ProcessTimeout(double time, int mapIndex, int controlIndex, int bindingIndex, int interactionIndex)
		{
			ref InputActionState.InteractionState ptr = ref this.interactionStates[interactionIndex];
			InputInteractionContext inputInteractionContext = new InputInteractionContext
			{
				m_State = this,
				m_TriggerState = new InputActionState.TriggerState
				{
					phase = ptr.phase,
					time = time,
					mapIndex = mapIndex,
					controlIndex = controlIndex,
					bindingIndex = bindingIndex,
					interactionIndex = interactionIndex,
					startTime = ptr.startTime
				},
				timerHasExpired = true
			};
			ptr.isTimerRunning = false;
			ptr.totalTimeoutCompletionTimeRemaining = Mathf.Max(ptr.totalTimeoutCompletionTimeRemaining - ptr.timerDuration, 0f);
			ptr.timerDuration = 0f;
			this.interactions[interactionIndex].Process(ref inputInteractionContext);
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000A110 File Offset: 0x00008310
		internal unsafe void SetTotalTimeoutCompletionTime(float seconds, ref InputActionState.TriggerState trigger)
		{
			InputActionState.InteractionState* ptr = this.interactionStates + trigger.interactionIndex;
			ptr->totalTimeoutCompletionDone = 0f;
			ptr->totalTimeoutCompletionTimeRemaining = seconds;
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000A138 File Offset: 0x00008338
		internal unsafe void StartTimeout(float seconds, ref InputActionState.TriggerState trigger)
		{
			InputManager s_Manager = InputSystem.s_Manager;
			double time = trigger.time;
			InputControl inputControl = this.controls[trigger.controlIndex];
			int interactionIndex = trigger.interactionIndex;
			long num = this.ToCombinedMapAndControlAndBindingIndex(trigger.mapIndex, trigger.controlIndex, trigger.bindingIndex);
			InputActionState.InteractionState* ptr = this.interactionStates + interactionIndex;
			if (ptr->isTimerRunning)
			{
				this.StopTimeout(interactionIndex);
			}
			s_Manager.AddStateChangeMonitorTimeout(inputControl, this, time + (double)seconds, num, interactionIndex);
			ptr->isTimerRunning = true;
			ptr->timerStartTime = time;
			ptr->timerDuration = seconds;
			ptr->timerMonitorIndex = num;
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000A1CC File Offset: 0x000083CC
		private unsafe void StopTimeout(int interactionIndex)
		{
			ref InputActionState.InteractionState ptr = ref this.interactionStates[interactionIndex];
			InputSystem.s_Manager.RemoveStateChangeMonitorTimeout(this, ptr.timerMonitorIndex, interactionIndex);
			ptr.isTimerRunning = false;
			ptr.totalTimeoutCompletionDone += ptr.timerDuration;
			ptr.totalTimeoutCompletionTimeRemaining = Mathf.Max(ptr.totalTimeoutCompletionTimeRemaining - ptr.timerDuration, 0f);
			ptr.timerDuration = 0f;
			ptr.timerStartTime = 0.0;
			ptr.timerMonitorIndex = 0L;
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000A258 File Offset: 0x00008458
		internal unsafe void ChangePhaseOfInteraction(InputActionPhase newPhase, ref InputActionState.TriggerState trigger, InputActionPhase phaseAfterPerformed = InputActionPhase.Waiting, bool processNextInteractionOnCancel = true)
		{
			int interactionIndex = trigger.interactionIndex;
			int bindingIndex = trigger.bindingIndex;
			InputActionPhase inputActionPhase = InputActionPhase.Waiting;
			if (newPhase == InputActionPhase.Performed)
			{
				inputActionPhase = phaseAfterPerformed;
			}
			ref InputActionState.InteractionState ptr = ref this.interactionStates[interactionIndex];
			if (ptr.isTimerRunning)
			{
				this.StopTimeout(trigger.interactionIndex);
			}
			ptr.phase = newPhase;
			ptr.triggerControlIndex = trigger.controlIndex;
			ptr.startTime = trigger.startTime;
			if (newPhase == InputActionPhase.Performed)
			{
				ptr.performedTime = trigger.time;
			}
			int actionIndex = this.bindingStates[bindingIndex].actionIndex;
			if (actionIndex != -1)
			{
				if (this.actionStates[actionIndex].phase == InputActionPhase.Waiting)
				{
					if (!this.ChangePhaseOfAction(newPhase, ref trigger, inputActionPhase))
					{
						return;
					}
				}
				else if (newPhase == InputActionPhase.Canceled && this.actionStates[actionIndex].interactionIndex == trigger.interactionIndex)
				{
					if (!this.ChangePhaseOfAction(newPhase, ref trigger, InputActionPhase.Waiting))
					{
						return;
					}
					if (!processNextInteractionOnCancel)
					{
						return;
					}
					int interactionStartIndex = this.bindingStates[bindingIndex].interactionStartIndex;
					int interactionCount = this.bindingStates[bindingIndex].interactionCount;
					int i = 0;
					while (i < interactionCount)
					{
						int num = interactionStartIndex + i;
						if (num != trigger.interactionIndex && (this.interactionStates[num].phase == InputActionPhase.Started || this.interactionStates[num].phase == InputActionPhase.Performed))
						{
							double startTime = this.interactionStates[num].startTime;
							InputActionState.TriggerState triggerState = new InputActionState.TriggerState
							{
								phase = InputActionPhase.Started,
								controlIndex = this.interactionStates[num].triggerControlIndex,
								bindingIndex = trigger.bindingIndex,
								interactionIndex = num,
								mapIndex = trigger.mapIndex,
								time = startTime,
								startTime = startTime
							};
							if (!this.ChangePhaseOfAction(InputActionPhase.Started, ref triggerState, InputActionPhase.Waiting))
							{
								return;
							}
							if (this.interactionStates[num].phase != InputActionPhase.Performed)
							{
								break;
							}
							triggerState = new InputActionState.TriggerState
							{
								phase = InputActionPhase.Performed,
								controlIndex = this.interactionStates[num].triggerControlIndex,
								bindingIndex = trigger.bindingIndex,
								interactionIndex = num,
								mapIndex = trigger.mapIndex,
								time = this.interactionStates[num].performedTime,
								startTime = startTime
							};
							if (!this.ChangePhaseOfAction(InputActionPhase.Performed, ref triggerState, InputActionPhase.Waiting))
							{
								return;
							}
							break;
						}
						else
						{
							i++;
						}
					}
				}
				else if (this.actionStates[actionIndex].interactionIndex == trigger.interactionIndex)
				{
					if (!this.ChangePhaseOfAction(newPhase, ref trigger, inputActionPhase))
					{
						return;
					}
					if (newPhase == InputActionPhase.Performed)
					{
						int interactionStartIndex2 = this.bindingStates[bindingIndex].interactionStartIndex;
						int interactionCount2 = this.bindingStates[bindingIndex].interactionCount;
						for (int j = 0; j < interactionCount2; j++)
						{
							int num2 = interactionStartIndex2 + j;
							if (num2 != trigger.interactionIndex)
							{
								this.ResetInteractionState(num2);
							}
						}
					}
				}
			}
			if (newPhase != InputActionPhase.Performed || this.actionStates[actionIndex].interactionIndex == trigger.interactionIndex)
			{
				if (newPhase == InputActionPhase.Performed && phaseAfterPerformed != InputActionPhase.Waiting)
				{
					ptr.phase = phaseAfterPerformed;
					return;
				}
				if (newPhase == InputActionPhase.Performed || newPhase == InputActionPhase.Canceled)
				{
					this.ResetInteractionState(trigger.interactionIndex);
				}
			}
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000A5E0 File Offset: 0x000087E0
		private unsafe bool ChangePhaseOfAction(InputActionPhase newPhase, ref InputActionState.TriggerState trigger, InputActionPhase phaseAfterPerformedOrCanceled = InputActionPhase.Waiting)
		{
			int actionIndex = this.bindingStates[trigger.bindingIndex].actionIndex;
			if (actionIndex == -1)
			{
				return true;
			}
			InputActionState.TriggerState* ptr = this.actionStates + actionIndex;
			if (ptr->isDisabled)
			{
				return true;
			}
			ptr->inProcessing = true;
			try
			{
				if (ptr->isPassThrough && trigger.interactionIndex == -1)
				{
					this.ChangePhaseOfActionInternal(actionIndex, ptr, newPhase, ref trigger);
					if (!ptr->inProcessing)
					{
						return false;
					}
				}
				else if (newPhase == InputActionPhase.Performed && ptr->phase == InputActionPhase.Waiting)
				{
					this.ChangePhaseOfActionInternal(actionIndex, ptr, InputActionPhase.Started, ref trigger);
					if (!ptr->inProcessing)
					{
						return false;
					}
					this.ChangePhaseOfActionInternal(actionIndex, ptr, newPhase, ref trigger);
					if (!ptr->inProcessing)
					{
						return false;
					}
					if (phaseAfterPerformedOrCanceled == InputActionPhase.Waiting)
					{
						this.ChangePhaseOfActionInternal(actionIndex, ptr, InputActionPhase.Canceled, ref trigger);
					}
					if (!ptr->inProcessing)
					{
						return false;
					}
					ptr->phase = phaseAfterPerformedOrCanceled;
				}
				else if (ptr->phase != newPhase || newPhase == InputActionPhase.Performed)
				{
					this.ChangePhaseOfActionInternal(actionIndex, ptr, newPhase, ref trigger);
					if (!ptr->inProcessing)
					{
						return false;
					}
					if (newPhase == InputActionPhase.Performed || newPhase == InputActionPhase.Canceled)
					{
						ptr->phase = phaseAfterPerformedOrCanceled;
					}
				}
			}
			finally
			{
				ptr->inProcessing = false;
			}
			if (ptr->phase == InputActionPhase.Waiting)
			{
				ptr->controlIndex = -1;
				ptr->flags &= ~InputActionState.TriggerState.Flags.HaveMagnitude;
			}
			return true;
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000A730 File Offset: 0x00008930
		private unsafe void ChangePhaseOfActionInternal(int actionIndex, InputActionState.TriggerState* actionState, InputActionPhase newPhase, ref InputActionState.TriggerState trigger)
		{
			InputActionState.TriggerState triggerState = trigger;
			triggerState.flags = actionState->flags;
			if (newPhase != InputActionPhase.Canceled)
			{
				triggerState.magnitude = trigger.magnitude;
			}
			else
			{
				triggerState.magnitude = 0f;
			}
			triggerState.phase = newPhase;
			if (newPhase == InputActionPhase.Performed)
			{
				triggerState.lastPerformedInUpdate = InputUpdate.s_UpdateStepCount;
				triggerState.lastCanceledInUpdate = actionState->lastCanceledInUpdate;
				if (this.controlGroupingAndComplexity[trigger.controlIndex * 2 + 1] > 1 && this.m_CurrentlyProcessingThisEvent.valid)
				{
					this.m_CurrentlyProcessingThisEvent.handled = true;
				}
			}
			else if (newPhase == InputActionPhase.Canceled)
			{
				triggerState.lastCanceledInUpdate = InputUpdate.s_UpdateStepCount;
				triggerState.lastPerformedInUpdate = actionState->lastPerformedInUpdate;
			}
			else
			{
				triggerState.lastPerformedInUpdate = actionState->lastPerformedInUpdate;
				triggerState.lastCanceledInUpdate = actionState->lastCanceledInUpdate;
			}
			triggerState.pressedInUpdate = actionState->pressedInUpdate;
			triggerState.releasedInUpdate = actionState->releasedInUpdate;
			if (newPhase == InputActionPhase.Started)
			{
				triggerState.startTime = triggerState.time;
			}
			*actionState = triggerState;
			InputActionMap inputActionMap = this.maps[trigger.mapIndex];
			InputAction inputAction = inputActionMap.m_Actions[actionIndex - this.mapIndices[trigger.mapIndex].actionStartIndex];
			trigger.phase = newPhase;
			switch (newPhase)
			{
			case InputActionPhase.Started:
				this.CallActionListeners(actionIndex, inputActionMap, newPhase, ref inputAction.m_OnStarted, "started");
				return;
			case InputActionPhase.Performed:
				this.CallActionListeners(actionIndex, inputActionMap, newPhase, ref inputAction.m_OnPerformed, "performed");
				return;
			case InputActionPhase.Canceled:
				this.CallActionListeners(actionIndex, inputActionMap, newPhase, ref inputAction.m_OnCanceled, "canceled");
				return;
			default:
				return;
			}
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000A8C8 File Offset: 0x00008AC8
		private void CallActionListeners(int actionIndex, InputActionMap actionMap, InputActionPhase phase, ref CallbackArray<Action<InputAction.CallbackContext>> listeners, string callbackName)
		{
			CallbackArray<Action<InputAction.CallbackContext>> actionCallbacks = actionMap.m_ActionCallbacks;
			if (listeners.length == 0 && actionCallbacks.length == 0 && InputActionState.s_GlobalState.onActionChange.length == 0)
			{
				return;
			}
			InputAction.CallbackContext callbackContext = new InputAction.CallbackContext
			{
				m_State = this,
				m_ActionIndex = actionIndex
			};
			InputAction action = callbackContext.action;
			if (InputActionState.s_GlobalState.onActionChange.length > 0)
			{
				InputActionChange inputActionChange;
				switch (phase)
				{
				case InputActionPhase.Started:
					inputActionChange = InputActionChange.ActionStarted;
					break;
				case InputActionPhase.Performed:
					inputActionChange = InputActionChange.ActionPerformed;
					break;
				case InputActionPhase.Canceled:
					inputActionChange = InputActionChange.ActionCanceled;
					break;
				default:
					return;
				}
				DelegateHelpers.InvokeCallbacksSafe<object, InputActionChange>(ref InputActionState.s_GlobalState.onActionChange, action, inputActionChange, "InputSystem.onActionChange", null);
			}
			DelegateHelpers.InvokeCallbacksSafe<InputAction.CallbackContext>(ref listeners, callbackContext, callbackName, action);
			DelegateHelpers.InvokeCallbacksSafe<InputAction.CallbackContext>(ref actionCallbacks, callbackContext, callbackName, actionMap);
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000A984 File Offset: 0x00008B84
		private object GetActionOrNoneString(ref InputActionState.TriggerState trigger)
		{
			InputAction actionOrNull = this.GetActionOrNull(ref trigger);
			if (actionOrNull == null)
			{
				return "<none>";
			}
			return actionOrNull;
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000A9A4 File Offset: 0x00008BA4
		internal unsafe InputAction GetActionOrNull(int bindingIndex)
		{
			int actionIndex = this.bindingStates[bindingIndex].actionIndex;
			if (actionIndex == -1)
			{
				return null;
			}
			int mapIndex = this.bindingStates[bindingIndex].mapIndex;
			int actionStartIndex = this.mapIndices[mapIndex].actionStartIndex;
			return this.maps[mapIndex].m_Actions[actionIndex - actionStartIndex];
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000AA0C File Offset: 0x00008C0C
		internal unsafe InputAction GetActionOrNull(ref InputActionState.TriggerState trigger)
		{
			int actionIndex = this.bindingStates[trigger.bindingIndex].actionIndex;
			if (actionIndex == -1)
			{
				return null;
			}
			int actionStartIndex = this.mapIndices[trigger.mapIndex].actionStartIndex;
			return this.maps[trigger.mapIndex].m_Actions[actionIndex - actionStartIndex];
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000AA6B File Offset: 0x00008C6B
		internal InputControl GetControl(ref InputActionState.TriggerState trigger)
		{
			return this.controls[trigger.controlIndex];
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000AA7A File Offset: 0x00008C7A
		private IInputInteraction GetInteractionOrNull(ref InputActionState.TriggerState trigger)
		{
			if (trigger.interactionIndex == -1)
			{
				return null;
			}
			return this.interactions[trigger.interactionIndex];
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000AA94 File Offset: 0x00008C94
		internal unsafe int GetBindingIndexInMap(int bindingIndex)
		{
			int mapIndex = this.bindingStates[bindingIndex].mapIndex;
			int bindingStartIndex = this.mapIndices[mapIndex].bindingStartIndex;
			return bindingIndex - bindingStartIndex;
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000AAD0 File Offset: 0x00008CD0
		internal unsafe int GetBindingIndexInState(int mapIndex, int bindingIndexInMap)
		{
			return this.mapIndices[mapIndex].bindingStartIndex + bindingIndexInMap;
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000AAE9 File Offset: 0x00008CE9
		internal unsafe ref InputActionState.BindingState GetBindingState(int bindingIndex)
		{
			return ref this.bindingStates[bindingIndex];
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000AAFC File Offset: 0x00008CFC
		internal unsafe ref InputBinding GetBinding(int bindingIndex)
		{
			int mapIndex = this.bindingStates[bindingIndex].mapIndex;
			int bindingStartIndex = this.mapIndices[mapIndex].bindingStartIndex;
			return ref this.maps[mapIndex].m_Bindings[bindingIndex - bindingStartIndex];
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000AB4C File Offset: 0x00008D4C
		internal unsafe InputActionMap GetActionMap(int bindingIndex)
		{
			int mapIndex = this.bindingStates[bindingIndex].mapIndex;
			return this.maps[mapIndex];
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000AB78 File Offset: 0x00008D78
		private unsafe void ResetInteractionStateAndCancelIfNecessary(int mapIndex, int bindingIndex, int interactionIndex)
		{
			int actionIndex = this.bindingStates[bindingIndex].actionIndex;
			if (this.actionStates[actionIndex].interactionIndex == interactionIndex)
			{
				InputActionPhase phase = this.interactionStates[interactionIndex].phase;
				if (phase - InputActionPhase.Started <= 1)
				{
					this.ChangePhaseOfInteraction(InputActionPhase.Canceled, ref this.actionStates[actionIndex], InputActionPhase.Waiting, false);
				}
				this.actionStates[actionIndex].interactionIndex = -1;
			}
			this.ResetInteractionState(interactionIndex);
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000AC08 File Offset: 0x00008E08
		private unsafe void ResetInteractionState(int interactionIndex)
		{
			this.interactions[interactionIndex].Reset();
			if (this.interactionStates[interactionIndex].isTimerRunning)
			{
				this.StopTimeout(interactionIndex);
			}
			this.interactionStates[interactionIndex] = new InputActionState.InteractionState
			{
				phase = InputActionPhase.Waiting,
				triggerControlIndex = -1
			};
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000AC70 File Offset: 0x00008E70
		internal unsafe int GetValueSizeInBytes(int bindingIndex, int controlIndex)
		{
			if (this.bindingStates[bindingIndex].isPartOfComposite)
			{
				int compositeOrCompositeBindingIndex = this.bindingStates[bindingIndex].compositeOrCompositeBindingIndex;
				int compositeOrCompositeBindingIndex2 = this.bindingStates[compositeOrCompositeBindingIndex].compositeOrCompositeBindingIndex;
				return this.composites[compositeOrCompositeBindingIndex2].valueSizeInBytes;
			}
			return this.controls[controlIndex].valueSizeInBytes;
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000ACDC File Offset: 0x00008EDC
		internal unsafe Type GetValueType(int bindingIndex, int controlIndex)
		{
			if (this.bindingStates[bindingIndex].isPartOfComposite)
			{
				int compositeOrCompositeBindingIndex = this.bindingStates[bindingIndex].compositeOrCompositeBindingIndex;
				int compositeOrCompositeBindingIndex2 = this.bindingStates[compositeOrCompositeBindingIndex].compositeOrCompositeBindingIndex;
				return this.composites[compositeOrCompositeBindingIndex2].valueType;
			}
			return this.controls[controlIndex].valueType;
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000AD48 File Offset: 0x00008F48
		internal static bool IsActuated(ref InputActionState.TriggerState trigger, float threshold = 0f)
		{
			float magnitude = trigger.magnitude;
			if (magnitude < 0f)
			{
				return true;
			}
			if (Mathf.Approximately(threshold, 0f))
			{
				return magnitude > 0f;
			}
			return magnitude >= threshold;
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000AD84 File Offset: 0x00008F84
		internal unsafe void ReadValue(int bindingIndex, int controlIndex, void* buffer, int bufferSize, bool ignoreComposites = false)
		{
			InputControl inputControl = null;
			if (!ignoreComposites && this.bindingStates[bindingIndex].isPartOfComposite)
			{
				int compositeOrCompositeBindingIndex = this.bindingStates[bindingIndex].compositeOrCompositeBindingIndex;
				int compositeOrCompositeBindingIndex2 = this.bindingStates[compositeOrCompositeBindingIndex].compositeOrCompositeBindingIndex;
				InputBindingComposite inputBindingComposite = this.composites[compositeOrCompositeBindingIndex2];
				InputBindingCompositeContext inputBindingCompositeContext = new InputBindingCompositeContext
				{
					m_State = this,
					m_BindingIndex = compositeOrCompositeBindingIndex
				};
				inputBindingComposite.ReadValue(ref inputBindingCompositeContext, buffer, bufferSize);
				bindingIndex = compositeOrCompositeBindingIndex;
			}
			else
			{
				inputControl = this.controls[controlIndex];
				inputControl.ReadValueIntoBuffer(buffer, bufferSize);
			}
			int processorCount = this.bindingStates[bindingIndex].processorCount;
			if (processorCount > 0)
			{
				int processorStartIndex = this.bindingStates[bindingIndex].processorStartIndex;
				for (int i = 0; i < processorCount; i++)
				{
					this.processors[processorStartIndex + i].Process(buffer, bufferSize, inputControl);
				}
			}
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000AE78 File Offset: 0x00009078
		internal unsafe TValue ReadValue<TValue>(int bindingIndex, int controlIndex, bool ignoreComposites = false) where TValue : struct
		{
			TValue tvalue = default(TValue);
			InputControl<TValue> inputControl = null;
			if (!ignoreComposites && this.bindingStates[bindingIndex].isPartOfComposite)
			{
				int compositeOrCompositeBindingIndex = this.bindingStates[bindingIndex].compositeOrCompositeBindingIndex;
				int compositeOrCompositeBindingIndex2 = this.bindingStates[compositeOrCompositeBindingIndex].compositeOrCompositeBindingIndex;
				InputBindingComposite inputBindingComposite = this.composites[compositeOrCompositeBindingIndex2];
				InputBindingCompositeContext inputBindingCompositeContext = new InputBindingCompositeContext
				{
					m_State = this,
					m_BindingIndex = compositeOrCompositeBindingIndex
				};
				InputBindingComposite<TValue> inputBindingComposite2 = inputBindingComposite as InputBindingComposite<TValue>;
				if (inputBindingComposite2 == null)
				{
					Type valueType = inputBindingComposite.valueType;
					if (!valueType.IsAssignableFrom(typeof(TValue)))
					{
						throw new InvalidOperationException(string.Format("Cannot read value of type '{0}' from composite '{1}' bound to action '{2}' (composite is a '{3}' with value type '{4}')", new object[]
						{
							typeof(TValue).Name,
							inputBindingComposite,
							this.GetActionOrNull(bindingIndex),
							compositeOrCompositeBindingIndex2.GetType().Name,
							valueType.GetNiceTypeName()
						}));
					}
					inputBindingComposite.ReadValue(ref inputBindingCompositeContext, UnsafeUtility.AddressOf<TValue>(ref tvalue), UnsafeUtility.SizeOf<TValue>());
				}
				else
				{
					tvalue = inputBindingComposite2.ReadValue(ref inputBindingCompositeContext);
				}
				bindingIndex = compositeOrCompositeBindingIndex;
			}
			else if (controlIndex != -1)
			{
				InputControl inputControl2 = this.controls[controlIndex];
				inputControl = inputControl2 as InputControl<TValue>;
				if (inputControl == null)
				{
					throw new InvalidOperationException(string.Format("Cannot read value of type '{0}' from control '{1}' bound to action '{2}' (control is a '{3}' with value type '{4}')", new object[]
					{
						typeof(TValue).GetNiceTypeName(),
						inputControl2.path,
						this.GetActionOrNull(bindingIndex),
						inputControl2.GetType().Name,
						inputControl2.valueType.GetNiceTypeName()
					}));
				}
				tvalue = *inputControl.value;
			}
			return this.ApplyProcessors<TValue>(bindingIndex, tvalue, inputControl);
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000B030 File Offset: 0x00009230
		internal unsafe TValue ApplyProcessors<TValue>(int bindingIndex, TValue value, InputControl<TValue> controlOfType = null) where TValue : struct
		{
			int processorCount = this.bindingStates[bindingIndex].processorCount;
			if (processorCount > 0)
			{
				int processorStartIndex = this.bindingStates[bindingIndex].processorStartIndex;
				for (int i = 0; i < processorCount; i++)
				{
					InputProcessor<TValue> inputProcessor = this.processors[processorStartIndex + i] as InputProcessor<TValue>;
					if (inputProcessor != null)
					{
						value = inputProcessor.Process(value, controlOfType);
					}
				}
			}
			return value;
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000B098 File Offset: 0x00009298
		public unsafe float EvaluateCompositePartMagnitude(int bindingIndex, int partNumber)
		{
			int num = bindingIndex + 1;
			float num2 = float.MinValue;
			int num3 = num;
			while (num3 < this.totalBindingCount && this.bindingStates[num3].isPartOfComposite)
			{
				if (this.bindingStates[num3].partIndex == partNumber)
				{
					int controlCount = this.bindingStates[num3].controlCount;
					int controlStartIndex = this.bindingStates[num3].controlStartIndex;
					for (int i = 0; i < controlCount; i++)
					{
						num2 = Mathf.Max(this.controls[controlStartIndex + i].magnitude, num2);
					}
				}
				num3++;
			}
			return num2;
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000B144 File Offset: 0x00009344
		internal unsafe double GetCompositePartPressTime(int bindingIndex, int partNumber)
		{
			int num = bindingIndex + 1;
			double num2 = double.MaxValue;
			int num3 = num;
			while (num3 < this.totalBindingCount && this.bindingStates[num3].isPartOfComposite)
			{
				ref InputActionState.BindingState ptr = ref this.bindingStates[num3];
				if (ptr.partIndex == partNumber && ptr.pressTime != 0.0 && ptr.pressTime < num2)
				{
					num2 = ptr.pressTime;
				}
				num3++;
			}
			if (num2 == 1.7976931348623157E+308)
			{
				return -1.0;
			}
			return num2;
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000B1D8 File Offset: 0x000093D8
		internal unsafe TValue ReadCompositePartValue<TValue, TComparer>(int bindingIndex, int partNumber, bool* buttonValuePtr, out int controlIndex, TComparer comparer = default(TComparer)) where TValue : struct where TComparer : IComparer<TValue>
		{
			TValue tvalue = default(TValue);
			int num = bindingIndex + 1;
			bool flag = true;
			controlIndex = -1;
			int num2 = num;
			while (num2 < this.totalBindingCount && this.bindingStates[num2].isPartOfComposite)
			{
				if (this.bindingStates[num2].partIndex == partNumber)
				{
					int controlCount = this.bindingStates[num2].controlCount;
					int controlStartIndex = this.bindingStates[num2].controlStartIndex;
					for (int i = 0; i < controlCount; i++)
					{
						int num3 = controlStartIndex + i;
						TValue tvalue2 = this.ReadValue<TValue>(num2, num3, true);
						if (flag)
						{
							tvalue = tvalue2;
							controlIndex = num3;
							flag = false;
						}
						else if (comparer.Compare(tvalue2, tvalue) > 0)
						{
							tvalue = tvalue2;
							controlIndex = num3;
						}
						if (buttonValuePtr != null && controlIndex == num3)
						{
							InputControl inputControl = this.controls[num3];
							ButtonControl buttonControl = inputControl as ButtonControl;
							if (buttonControl != null)
							{
								*buttonValuePtr = buttonControl.isPressed;
							}
							else if (inputControl is InputControl<float>)
							{
								void* ptr = UnsafeUtility.AddressOf<TValue>(ref tvalue2);
								*buttonValuePtr = *(float*)ptr >= ButtonControl.s_GlobalDefaultButtonPressPoint;
							}
						}
					}
				}
				num2++;
			}
			return tvalue;
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000B310 File Offset: 0x00009510
		internal unsafe bool ReadCompositePartValue(int bindingIndex, int partNumber, void* buffer, int bufferSize)
		{
			int num = bindingIndex + 1;
			float num2 = float.MinValue;
			int num3 = num;
			while (num3 < this.totalBindingCount && this.bindingStates[num3].isPartOfComposite)
			{
				if (this.bindingStates[num3].partIndex == partNumber)
				{
					int controlCount = this.bindingStates[num3].controlCount;
					int controlStartIndex = this.bindingStates[num3].controlStartIndex;
					for (int i = 0; i < controlCount; i++)
					{
						int num4 = controlStartIndex + i;
						float magnitude = this.controls[num4].magnitude;
						if (magnitude >= num2)
						{
							this.ReadValue(num3, num4, buffer, bufferSize, true);
							num2 = magnitude;
						}
					}
				}
				num3++;
			}
			return num2 > float.MinValue;
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000B3DC File Offset: 0x000095DC
		internal unsafe object ReadCompositePartValueAsObject(int bindingIndex, int partNumber)
		{
			int num = bindingIndex + 1;
			float num2 = float.MinValue;
			object obj = null;
			int num3 = num;
			while (num3 < this.totalBindingCount && this.bindingStates[num3].isPartOfComposite)
			{
				if (this.bindingStates[num3].partIndex == partNumber)
				{
					int controlCount = this.bindingStates[num3].controlCount;
					int controlStartIndex = this.bindingStates[num3].controlStartIndex;
					for (int i = 0; i < controlCount; i++)
					{
						int num4 = controlStartIndex + i;
						float magnitude = this.controls[num4].magnitude;
						if (magnitude >= num2)
						{
							obj = this.ReadValueAsObject(num3, num4, true);
							num2 = magnitude;
						}
					}
				}
				num3++;
			}
			return obj;
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000B4A4 File Offset: 0x000096A4
		internal unsafe object ReadValueAsObject(int bindingIndex, int controlIndex, bool ignoreComposites = false)
		{
			InputControl inputControl = null;
			object obj = null;
			if (!ignoreComposites && this.bindingStates[bindingIndex].isPartOfComposite)
			{
				int compositeOrCompositeBindingIndex = this.bindingStates[bindingIndex].compositeOrCompositeBindingIndex;
				int compositeOrCompositeBindingIndex2 = this.bindingStates[compositeOrCompositeBindingIndex].compositeOrCompositeBindingIndex;
				InputBindingComposite inputBindingComposite = this.composites[compositeOrCompositeBindingIndex2];
				InputBindingCompositeContext inputBindingCompositeContext = new InputBindingCompositeContext
				{
					m_State = this,
					m_BindingIndex = compositeOrCompositeBindingIndex
				};
				obj = inputBindingComposite.ReadValueAsObject(ref inputBindingCompositeContext);
				bindingIndex = compositeOrCompositeBindingIndex;
			}
			else if (controlIndex != -1)
			{
				inputControl = this.controls[controlIndex];
				obj = inputControl.ReadValueAsObject();
			}
			if (obj != null)
			{
				int processorCount = this.bindingStates[bindingIndex].processorCount;
				if (processorCount > 0)
				{
					int processorStartIndex = this.bindingStates[bindingIndex].processorStartIndex;
					for (int i = 0; i < processorCount; i++)
					{
						obj = this.processors[processorStartIndex + i].ProcessAsObject(obj, inputControl);
					}
				}
			}
			return obj;
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000B59C File Offset: 0x0000979C
		internal unsafe bool ReadValueAsButton(int bindingIndex, int controlIndex)
		{
			ButtonControl buttonControl = null;
			if (!this.bindingStates[bindingIndex].isPartOfComposite)
			{
				buttonControl = this.controls[controlIndex] as ButtonControl;
			}
			float num = this.ReadValue<float>(bindingIndex, controlIndex, false);
			if (buttonControl != null)
			{
				return num >= buttonControl.pressPointOrDefault;
			}
			return num >= ButtonControl.s_GlobalDefaultButtonPressPoint;
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000B5F8 File Offset: 0x000097F8
		internal static ISavedState SaveAndResetState()
		{
			ISavedState savedState = new SavedStructState<InputActionState.GlobalState>(ref InputActionState.s_GlobalState, delegate(ref InputActionState.GlobalState state)
			{
				InputActionState.s_GlobalState = state;
			}, delegate
			{
				InputActionState.ResetGlobals();
			});
			InputActionState.s_GlobalState = default(InputActionState.GlobalState);
			return savedState;
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000B658 File Offset: 0x00009858
		private void AddToGlobalList()
		{
			InputActionState.CompactGlobalList();
			GCHandle gchandle = GCHandle.Alloc(this, GCHandleType.Weak);
			InputActionState.s_GlobalState.globalList.AppendWithCapacity(gchandle, 10);
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000B688 File Offset: 0x00009888
		private void RemoveMapFromGlobalList()
		{
			int length = InputActionState.s_GlobalState.globalList.length;
			for (int i = 0; i < length; i++)
			{
				if (InputActionState.s_GlobalState.globalList[i].Target == this)
				{
					InputActionState.s_GlobalState.globalList[i].Free();
					InputActionState.s_GlobalState.globalList.RemoveAtByMovingTailWithCapacity(i);
					return;
				}
			}
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000B6F8 File Offset: 0x000098F8
		private static void CompactGlobalList()
		{
			int length = InputActionState.s_GlobalState.globalList.length;
			int num = 0;
			for (int i = 0; i < length; i++)
			{
				GCHandle gchandle = InputActionState.s_GlobalState.globalList[i];
				if (gchandle.IsAllocated && gchandle.Target != null)
				{
					if (num != i)
					{
						InputActionState.s_GlobalState.globalList[num] = gchandle;
					}
					num++;
				}
				else
				{
					if (gchandle.IsAllocated)
					{
						InputActionState.s_GlobalState.globalList[i].Free();
					}
					InputActionState.s_GlobalState.globalList[i] = default(GCHandle);
				}
			}
			InputActionState.s_GlobalState.globalList.length = num;
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000B7B0 File Offset: 0x000099B0
		internal void NotifyListenersOfActionChange(InputActionChange change)
		{
			for (int i = 0; i < this.totalMapCount; i++)
			{
				InputActionMap inputActionMap = this.maps[i];
				if (inputActionMap.m_SingletonAction != null)
				{
					InputActionState.NotifyListenersOfActionChange(change, inputActionMap.m_SingletonAction);
				}
				else
				{
					if (!(inputActionMap.m_Asset == null))
					{
						InputActionState.NotifyListenersOfActionChange(change, inputActionMap.m_Asset);
						return;
					}
					InputActionState.NotifyListenersOfActionChange(change, inputActionMap);
				}
			}
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000B811 File Offset: 0x00009A11
		internal static void NotifyListenersOfActionChange(InputActionChange change, object actionOrMapOrAsset)
		{
			DelegateHelpers.InvokeCallbacksSafe<object, InputActionChange>(ref InputActionState.s_GlobalState.onActionChange, actionOrMapOrAsset, change, "onActionChange", null);
			if (change == InputActionChange.BoundControlsChanged)
			{
				DelegateHelpers.InvokeCallbacksSafe<object>(ref InputActionState.s_GlobalState.onActionControlsChanged, actionOrMapOrAsset, "onActionControlsChange", null);
			}
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000B844 File Offset: 0x00009A44
		private static void ResetGlobals()
		{
			InputActionState.DestroyAllActionMapStates();
			for (int i = 0; i < InputActionState.s_GlobalState.globalList.length; i++)
			{
				if (InputActionState.s_GlobalState.globalList[i].IsAllocated)
				{
					InputActionState.s_GlobalState.globalList[i].Free();
				}
			}
			InputActionState.s_GlobalState.globalList.length = 0;
			InputActionState.s_GlobalState.onActionChange.Clear();
			InputActionState.s_GlobalState.onActionControlsChanged.Clear();
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000B8D0 File Offset: 0x00009AD0
		internal unsafe static int FindAllEnabledActions(List<InputAction> result)
		{
			int num = 0;
			int length = InputActionState.s_GlobalState.globalList.length;
			for (int i = 0; i < length; i++)
			{
				GCHandle gchandle = InputActionState.s_GlobalState.globalList[i];
				if (gchandle.IsAllocated)
				{
					InputActionState inputActionState = (InputActionState)gchandle.Target;
					if (inputActionState != null)
					{
						int totalMapCount = inputActionState.totalMapCount;
						InputActionMap[] array = inputActionState.maps;
						for (int j = 0; j < totalMapCount; j++)
						{
							InputActionMap inputActionMap = array[j];
							if (inputActionMap.enabled)
							{
								InputAction[] actions = inputActionMap.m_Actions;
								int num2 = actions.Length;
								if (inputActionMap.m_EnabledActionsCount == num2)
								{
									result.AddRange(actions);
									num += num2;
								}
								else
								{
									int actionStartIndex = inputActionState.mapIndices[inputActionMap.m_MapIndexInState].actionStartIndex;
									for (int k = 0; k < num2; k++)
									{
										if (inputActionState.actionStates[actionStartIndex + k].phase != InputActionPhase.Disabled)
										{
											result.Add(actions[k]);
											num++;
										}
									}
								}
							}
						}
					}
				}
			}
			return num;
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000B9F4 File Offset: 0x00009BF4
		internal static void OnDeviceChange(InputDevice device, InputDeviceChange change)
		{
			for (int i = 0; i < InputActionState.s_GlobalState.globalList.length; i++)
			{
				GCHandle gchandle = InputActionState.s_GlobalState.globalList[i];
				if (!gchandle.IsAllocated || gchandle.Target == null)
				{
					if (gchandle.IsAllocated)
					{
						InputActionState.s_GlobalState.globalList[i].Free();
					}
					InputActionState.s_GlobalState.globalList.RemoveAtWithCapacity(i);
					i--;
				}
				else
				{
					InputActionState inputActionState = (InputActionState)gchandle.Target;
					bool flag = true;
					switch (change)
					{
					case InputDeviceChange.Added:
						if (!inputActionState.CanUseDevice(device))
						{
							goto IL_0155;
						}
						flag = false;
						break;
					case InputDeviceChange.Removed:
					{
						if (!inputActionState.IsUsingDevice(device))
						{
							goto IL_0155;
						}
						for (int j = 0; j < inputActionState.totalMapCount; j++)
						{
							InputActionMap inputActionMap = inputActionState.maps[j];
							inputActionMap.m_Devices.Remove(device);
							InputActionAsset asset = inputActionMap.asset;
							if (asset != null)
							{
								asset.m_Devices.Remove(device);
							}
						}
						flag = false;
						break;
					}
					case InputDeviceChange.UsageChanged:
					case InputDeviceChange.ConfigurationChanged:
						if (!inputActionState.IsUsingDevice(device) && !inputActionState.CanUseDevice(device))
						{
							goto IL_0155;
						}
						break;
					case InputDeviceChange.SoftReset:
					case InputDeviceChange.HardReset:
						if (inputActionState.IsUsingDevice(device))
						{
							inputActionState.ResetActionStatesDrivenBy(device);
							goto IL_0155;
						}
						goto IL_0155;
					}
					int num = 0;
					while (num < inputActionState.totalMapCount && !inputActionState.maps[num].LazyResolveBindings(flag))
					{
						num++;
					}
				}
				IL_0155:;
			}
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000BB70 File Offset: 0x00009D70
		internal static void DeferredResolutionOfBindings()
		{
			InputActionMap.s_DeferBindingResolution++;
			try
			{
				for (int i = 0; i < InputActionState.s_GlobalState.globalList.length; i++)
				{
					GCHandle gchandle = InputActionState.s_GlobalState.globalList[i];
					if (!gchandle.IsAllocated || gchandle.Target == null)
					{
						if (gchandle.IsAllocated)
						{
							InputActionState.s_GlobalState.globalList[i].Free();
						}
						InputActionState.s_GlobalState.globalList.RemoveAtWithCapacity(i);
						i--;
					}
					else
					{
						InputActionState inputActionState = (InputActionState)gchandle.Target;
						for (int j = 0; j < inputActionState.totalMapCount; j++)
						{
							inputActionState.maps[j].ResolveBindingsIfNecessary();
						}
					}
				}
			}
			finally
			{
				InputActionMap.s_DeferBindingResolution--;
			}
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000BC54 File Offset: 0x00009E54
		internal static void DisableAllActions()
		{
			for (int i = 0; i < InputActionState.s_GlobalState.globalList.length; i++)
			{
				GCHandle gchandle = InputActionState.s_GlobalState.globalList[i];
				if (gchandle.IsAllocated && gchandle.Target != null)
				{
					InputActionState inputActionState = (InputActionState)gchandle.Target;
					int totalMapCount = inputActionState.totalMapCount;
					InputActionMap[] array = inputActionState.maps;
					for (int j = 0; j < totalMapCount; j++)
					{
						array[j].Disable();
					}
				}
			}
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000BCD0 File Offset: 0x00009ED0
		internal static void DestroyAllActionMapStates()
		{
			while (InputActionState.s_GlobalState.globalList.length > 0)
			{
				int num = InputActionState.s_GlobalState.globalList.length - 1;
				GCHandle gchandle = InputActionState.s_GlobalState.globalList[num];
				if (!gchandle.IsAllocated || gchandle.Target == null)
				{
					if (gchandle.IsAllocated)
					{
						InputActionState.s_GlobalState.globalList[num].Free();
					}
					InputActionState.s_GlobalState.globalList.RemoveAtWithCapacity(num);
				}
				else
				{
					((InputActionState)gchandle.Target).Destroy(false);
				}
			}
		}

		// Token: 0x040000C4 RID: 196
		public const int kInvalidIndex = -1;

		// Token: 0x040000C5 RID: 197
		public InputActionMap[] maps;

		// Token: 0x040000C6 RID: 198
		public InputControl[] controls;

		// Token: 0x040000C7 RID: 199
		public IInputInteraction[] interactions;

		// Token: 0x040000C8 RID: 200
		public InputProcessor[] processors;

		// Token: 0x040000C9 RID: 201
		public InputBindingComposite[] composites;

		// Token: 0x040000CA RID: 202
		public int totalProcessorCount;

		// Token: 0x040000CB RID: 203
		public InputActionState.UnmanagedMemory memory;

		// Token: 0x040000CC RID: 204
		private bool m_OnBeforeUpdateHooked;

		// Token: 0x040000CD RID: 205
		private bool m_OnAfterUpdateHooked;

		// Token: 0x040000CE RID: 206
		private bool m_InProcessControlStateChange;

		// Token: 0x040000CF RID: 207
		private InputEventPtr m_CurrentlyProcessingThisEvent;

		// Token: 0x040000D0 RID: 208
		private Action m_OnBeforeUpdateDelegate;

		// Token: 0x040000D1 RID: 209
		private Action m_OnAfterUpdateDelegate;

		// Token: 0x040000D2 RID: 210
		internal static InputActionState.GlobalState s_GlobalState;

		// Token: 0x0200016C RID: 364
		[StructLayout(LayoutKind.Explicit, Size = 48)]
		internal struct InteractionState
		{
			// Token: 0x170004F2 RID: 1266
			// (get) Token: 0x0600129B RID: 4763 RVA: 0x00057CC6 File Offset: 0x00055EC6
			// (set) Token: 0x0600129C RID: 4764 RVA: 0x00057CDD File Offset: 0x00055EDD
			public int triggerControlIndex
			{
				get
				{
					if (this.m_TriggerControlIndex == 65535)
					{
						return -1;
					}
					return (int)this.m_TriggerControlIndex;
				}
				set
				{
					if (value == -1)
					{
						this.m_TriggerControlIndex = ushort.MaxValue;
						return;
					}
					if (value < 0 || value >= 65535)
					{
						throw new NotSupportedException("More than ushort.MaxValue-1 controls in a single InputActionState");
					}
					this.m_TriggerControlIndex = (ushort)value;
				}
			}

			// Token: 0x170004F3 RID: 1267
			// (get) Token: 0x0600129D RID: 4765 RVA: 0x00057D0E File Offset: 0x00055F0E
			// (set) Token: 0x0600129E RID: 4766 RVA: 0x00057D16 File Offset: 0x00055F16
			public double startTime
			{
				get
				{
					return this.m_StartTime;
				}
				set
				{
					this.m_StartTime = value;
				}
			}

			// Token: 0x170004F4 RID: 1268
			// (get) Token: 0x0600129F RID: 4767 RVA: 0x00057D1F File Offset: 0x00055F1F
			// (set) Token: 0x060012A0 RID: 4768 RVA: 0x00057D27 File Offset: 0x00055F27
			public double performedTime
			{
				get
				{
					return this.m_PerformedTime;
				}
				set
				{
					this.m_PerformedTime = value;
				}
			}

			// Token: 0x170004F5 RID: 1269
			// (get) Token: 0x060012A1 RID: 4769 RVA: 0x00057D30 File Offset: 0x00055F30
			// (set) Token: 0x060012A2 RID: 4770 RVA: 0x00057D38 File Offset: 0x00055F38
			public double timerStartTime
			{
				get
				{
					return this.m_TimerStartTime;
				}
				set
				{
					this.m_TimerStartTime = value;
				}
			}

			// Token: 0x170004F6 RID: 1270
			// (get) Token: 0x060012A3 RID: 4771 RVA: 0x00057D41 File Offset: 0x00055F41
			// (set) Token: 0x060012A4 RID: 4772 RVA: 0x00057D49 File Offset: 0x00055F49
			public float timerDuration
			{
				get
				{
					return this.m_TimerDuration;
				}
				set
				{
					this.m_TimerDuration = value;
				}
			}

			// Token: 0x170004F7 RID: 1271
			// (get) Token: 0x060012A5 RID: 4773 RVA: 0x00057D52 File Offset: 0x00055F52
			// (set) Token: 0x060012A6 RID: 4774 RVA: 0x00057D5A File Offset: 0x00055F5A
			public float totalTimeoutCompletionDone
			{
				get
				{
					return this.m_TotalTimeoutCompletionTimeDone;
				}
				set
				{
					this.m_TotalTimeoutCompletionTimeDone = value;
				}
			}

			// Token: 0x170004F8 RID: 1272
			// (get) Token: 0x060012A7 RID: 4775 RVA: 0x00057D63 File Offset: 0x00055F63
			// (set) Token: 0x060012A8 RID: 4776 RVA: 0x00057D6B File Offset: 0x00055F6B
			public float totalTimeoutCompletionTimeRemaining
			{
				get
				{
					return this.m_TotalTimeoutCompletionTimeRemaining;
				}
				set
				{
					this.m_TotalTimeoutCompletionTimeRemaining = value;
				}
			}

			// Token: 0x170004F9 RID: 1273
			// (get) Token: 0x060012A9 RID: 4777 RVA: 0x00057D74 File Offset: 0x00055F74
			// (set) Token: 0x060012AA RID: 4778 RVA: 0x00057D7C File Offset: 0x00055F7C
			public long timerMonitorIndex
			{
				get
				{
					return this.m_TimerMonitorIndex;
				}
				set
				{
					this.m_TimerMonitorIndex = value;
				}
			}

			// Token: 0x170004FA RID: 1274
			// (get) Token: 0x060012AB RID: 4779 RVA: 0x00057D85 File Offset: 0x00055F85
			// (set) Token: 0x060012AC RID: 4780 RVA: 0x00057D94 File Offset: 0x00055F94
			public bool isTimerRunning
			{
				get
				{
					return (this.m_Flags & 1) == 1;
				}
				set
				{
					if (value)
					{
						this.m_Flags |= 1;
						return;
					}
					InputActionState.InteractionState.Flags flags = ~InputActionState.InteractionState.Flags.TimerRunning;
					this.m_Flags &= (byte)flags;
				}
			}

			// Token: 0x170004FB RID: 1275
			// (get) Token: 0x060012AD RID: 4781 RVA: 0x00057DC7 File Offset: 0x00055FC7
			// (set) Token: 0x060012AE RID: 4782 RVA: 0x00057DCF File Offset: 0x00055FCF
			public InputActionPhase phase
			{
				get
				{
					return (InputActionPhase)this.m_Phase;
				}
				set
				{
					this.m_Phase = (byte)value;
				}
			}

			// Token: 0x040007B3 RID: 1971
			[FieldOffset(0)]
			private ushort m_TriggerControlIndex;

			// Token: 0x040007B4 RID: 1972
			[FieldOffset(2)]
			private byte m_Phase;

			// Token: 0x040007B5 RID: 1973
			[FieldOffset(3)]
			private byte m_Flags;

			// Token: 0x040007B6 RID: 1974
			[FieldOffset(4)]
			private float m_TimerDuration;

			// Token: 0x040007B7 RID: 1975
			[FieldOffset(8)]
			private double m_StartTime;

			// Token: 0x040007B8 RID: 1976
			[FieldOffset(16)]
			private double m_TimerStartTime;

			// Token: 0x040007B9 RID: 1977
			[FieldOffset(24)]
			private double m_PerformedTime;

			// Token: 0x040007BA RID: 1978
			[FieldOffset(32)]
			private float m_TotalTimeoutCompletionTimeDone;

			// Token: 0x040007BB RID: 1979
			[FieldOffset(36)]
			private float m_TotalTimeoutCompletionTimeRemaining;

			// Token: 0x040007BC RID: 1980
			[FieldOffset(40)]
			private long m_TimerMonitorIndex;

			// Token: 0x0200025A RID: 602
			[Flags]
			private enum Flags
			{
				// Token: 0x04000C53 RID: 3155
				TimerRunning = 1
			}
		}

		// Token: 0x0200016D RID: 365
		[StructLayout(LayoutKind.Explicit, Size = 32)]
		internal struct BindingState
		{
			// Token: 0x170004FC RID: 1276
			// (get) Token: 0x060012AF RID: 4783 RVA: 0x00057DD9 File Offset: 0x00055FD9
			// (set) Token: 0x060012B0 RID: 4784 RVA: 0x00057DE4 File Offset: 0x00055FE4
			public int controlStartIndex
			{
				get
				{
					return (int)this.m_ControlStartIndex;
				}
				set
				{
					if (value >= 65535)
					{
						throw new NotSupportedException("Total control count in state cannot exceed byte.MaxValue=" + ushort.MaxValue.ToString());
					}
					this.m_ControlStartIndex = (ushort)value;
				}
			}

			// Token: 0x170004FD RID: 1277
			// (get) Token: 0x060012B1 RID: 4785 RVA: 0x00057E1E File Offset: 0x0005601E
			// (set) Token: 0x060012B2 RID: 4786 RVA: 0x00057E28 File Offset: 0x00056028
			public int controlCount
			{
				get
				{
					return (int)this.m_ControlCount;
				}
				set
				{
					if (value >= 255)
					{
						throw new NotSupportedException("Control count per binding cannot exceed byte.MaxValue=" + byte.MaxValue.ToString());
					}
					this.m_ControlCount = (byte)value;
				}
			}

			// Token: 0x170004FE RID: 1278
			// (get) Token: 0x060012B3 RID: 4787 RVA: 0x00057E62 File Offset: 0x00056062
			// (set) Token: 0x060012B4 RID: 4788 RVA: 0x00057E7C File Offset: 0x0005607C
			public int interactionStartIndex
			{
				get
				{
					if (this.m_InteractionStartIndex == 65535)
					{
						return -1;
					}
					return (int)this.m_InteractionStartIndex;
				}
				set
				{
					if (value == -1)
					{
						this.m_InteractionStartIndex = ushort.MaxValue;
						return;
					}
					if (value >= 65535)
					{
						throw new NotSupportedException("Interaction count cannot exceed ushort.MaxValue=" + ushort.MaxValue.ToString());
					}
					this.m_InteractionStartIndex = (ushort)value;
				}
			}

			// Token: 0x170004FF RID: 1279
			// (get) Token: 0x060012B5 RID: 4789 RVA: 0x00057EC6 File Offset: 0x000560C6
			// (set) Token: 0x060012B6 RID: 4790 RVA: 0x00057ED0 File Offset: 0x000560D0
			public int interactionCount
			{
				get
				{
					return (int)this.m_InteractionCount;
				}
				set
				{
					if (value >= 255)
					{
						throw new NotSupportedException("Interaction count per binding cannot exceed byte.MaxValue=" + byte.MaxValue.ToString());
					}
					this.m_InteractionCount = (byte)value;
				}
			}

			// Token: 0x17000500 RID: 1280
			// (get) Token: 0x060012B7 RID: 4791 RVA: 0x00057F0A File Offset: 0x0005610A
			// (set) Token: 0x060012B8 RID: 4792 RVA: 0x00057F24 File Offset: 0x00056124
			public int processorStartIndex
			{
				get
				{
					if (this.m_ProcessorStartIndex == 65535)
					{
						return -1;
					}
					return (int)this.m_ProcessorStartIndex;
				}
				set
				{
					if (value == -1)
					{
						this.m_ProcessorStartIndex = ushort.MaxValue;
						return;
					}
					if (value >= 65535)
					{
						throw new NotSupportedException("Processor count cannot exceed ushort.MaxValue=" + ushort.MaxValue.ToString());
					}
					this.m_ProcessorStartIndex = (ushort)value;
				}
			}

			// Token: 0x17000501 RID: 1281
			// (get) Token: 0x060012B9 RID: 4793 RVA: 0x00057F6E File Offset: 0x0005616E
			// (set) Token: 0x060012BA RID: 4794 RVA: 0x00057F78 File Offset: 0x00056178
			public int processorCount
			{
				get
				{
					return (int)this.m_ProcessorCount;
				}
				set
				{
					if (value >= 255)
					{
						throw new NotSupportedException("Processor count per binding cannot exceed byte.MaxValue=" + byte.MaxValue.ToString());
					}
					this.m_ProcessorCount = (byte)value;
				}
			}

			// Token: 0x17000502 RID: 1282
			// (get) Token: 0x060012BB RID: 4795 RVA: 0x00057FB2 File Offset: 0x000561B2
			// (set) Token: 0x060012BC RID: 4796 RVA: 0x00057FCC File Offset: 0x000561CC
			public int actionIndex
			{
				get
				{
					if (this.m_ActionIndex == 65535)
					{
						return -1;
					}
					return (int)this.m_ActionIndex;
				}
				set
				{
					if (value == -1)
					{
						this.m_ActionIndex = ushort.MaxValue;
						return;
					}
					if (value >= 65535)
					{
						throw new NotSupportedException("Action count cannot exceed ushort.MaxValue=" + ushort.MaxValue.ToString());
					}
					this.m_ActionIndex = (ushort)value;
				}
			}

			// Token: 0x17000503 RID: 1283
			// (get) Token: 0x060012BD RID: 4797 RVA: 0x00058016 File Offset: 0x00056216
			// (set) Token: 0x060012BE RID: 4798 RVA: 0x00058020 File Offset: 0x00056220
			public int mapIndex
			{
				get
				{
					return (int)this.m_MapIndex;
				}
				set
				{
					if (value >= 255)
					{
						throw new NotSupportedException("Map count cannot exceed byte.MaxValue=" + byte.MaxValue.ToString());
					}
					this.m_MapIndex = (byte)value;
				}
			}

			// Token: 0x17000504 RID: 1284
			// (get) Token: 0x060012BF RID: 4799 RVA: 0x0005805A File Offset: 0x0005625A
			// (set) Token: 0x060012C0 RID: 4800 RVA: 0x00058074 File Offset: 0x00056274
			public int compositeOrCompositeBindingIndex
			{
				get
				{
					if (this.m_CompositeOrCompositeBindingIndex == 65535)
					{
						return -1;
					}
					return (int)this.m_CompositeOrCompositeBindingIndex;
				}
				set
				{
					if (value == -1)
					{
						this.m_CompositeOrCompositeBindingIndex = ushort.MaxValue;
						return;
					}
					if (value >= 65535)
					{
						throw new NotSupportedException("Composite count cannot exceed ushort.MaxValue=" + ushort.MaxValue.ToString());
					}
					this.m_CompositeOrCompositeBindingIndex = (ushort)value;
				}
			}

			// Token: 0x17000505 RID: 1285
			// (get) Token: 0x060012C1 RID: 4801 RVA: 0x000580BE File Offset: 0x000562BE
			// (set) Token: 0x060012C2 RID: 4802 RVA: 0x000580C6 File Offset: 0x000562C6
			public int triggerEventIdForComposite
			{
				get
				{
					return this.m_TriggerEventIdForComposite;
				}
				set
				{
					this.m_TriggerEventIdForComposite = value;
				}
			}

			// Token: 0x17000506 RID: 1286
			// (get) Token: 0x060012C3 RID: 4803 RVA: 0x000580CF File Offset: 0x000562CF
			// (set) Token: 0x060012C4 RID: 4804 RVA: 0x000580D7 File Offset: 0x000562D7
			public double pressTime
			{
				get
				{
					return this.m_PressTime;
				}
				set
				{
					this.m_PressTime = value;
				}
			}

			// Token: 0x17000507 RID: 1287
			// (get) Token: 0x060012C5 RID: 4805 RVA: 0x000580E0 File Offset: 0x000562E0
			// (set) Token: 0x060012C6 RID: 4806 RVA: 0x000580E8 File Offset: 0x000562E8
			public InputActionState.BindingState.Flags flags
			{
				get
				{
					return (InputActionState.BindingState.Flags)this.m_Flags;
				}
				set
				{
					this.m_Flags = (byte)value;
				}
			}

			// Token: 0x17000508 RID: 1288
			// (get) Token: 0x060012C7 RID: 4807 RVA: 0x000580F2 File Offset: 0x000562F2
			// (set) Token: 0x060012C8 RID: 4808 RVA: 0x000580FF File Offset: 0x000562FF
			public bool chainsWithNext
			{
				get
				{
					return (this.flags & InputActionState.BindingState.Flags.ChainsWithNext) == InputActionState.BindingState.Flags.ChainsWithNext;
				}
				set
				{
					if (value)
					{
						this.flags |= InputActionState.BindingState.Flags.ChainsWithNext;
						return;
					}
					this.flags &= ~InputActionState.BindingState.Flags.ChainsWithNext;
				}
			}

			// Token: 0x17000509 RID: 1289
			// (get) Token: 0x060012C9 RID: 4809 RVA: 0x00058122 File Offset: 0x00056322
			// (set) Token: 0x060012CA RID: 4810 RVA: 0x0005812F File Offset: 0x0005632F
			public bool isEndOfChain
			{
				get
				{
					return (this.flags & InputActionState.BindingState.Flags.EndOfChain) == InputActionState.BindingState.Flags.EndOfChain;
				}
				set
				{
					if (value)
					{
						this.flags |= InputActionState.BindingState.Flags.EndOfChain;
						return;
					}
					this.flags &= ~InputActionState.BindingState.Flags.EndOfChain;
				}
			}

			// Token: 0x1700050A RID: 1290
			// (get) Token: 0x060012CB RID: 4811 RVA: 0x00058152 File Offset: 0x00056352
			public bool isPartOfChain
			{
				get
				{
					return this.chainsWithNext || this.isEndOfChain;
				}
			}

			// Token: 0x1700050B RID: 1291
			// (get) Token: 0x060012CC RID: 4812 RVA: 0x00058164 File Offset: 0x00056364
			// (set) Token: 0x060012CD RID: 4813 RVA: 0x00058171 File Offset: 0x00056371
			public bool isComposite
			{
				get
				{
					return (this.flags & InputActionState.BindingState.Flags.Composite) == InputActionState.BindingState.Flags.Composite;
				}
				set
				{
					if (value)
					{
						this.flags |= InputActionState.BindingState.Flags.Composite;
						return;
					}
					this.flags &= ~InputActionState.BindingState.Flags.Composite;
				}
			}

			// Token: 0x1700050C RID: 1292
			// (get) Token: 0x060012CE RID: 4814 RVA: 0x00058194 File Offset: 0x00056394
			// (set) Token: 0x060012CF RID: 4815 RVA: 0x000581A1 File Offset: 0x000563A1
			public bool isPartOfComposite
			{
				get
				{
					return (this.flags & InputActionState.BindingState.Flags.PartOfComposite) == InputActionState.BindingState.Flags.PartOfComposite;
				}
				set
				{
					if (value)
					{
						this.flags |= InputActionState.BindingState.Flags.PartOfComposite;
						return;
					}
					this.flags &= ~InputActionState.BindingState.Flags.PartOfComposite;
				}
			}

			// Token: 0x1700050D RID: 1293
			// (get) Token: 0x060012D0 RID: 4816 RVA: 0x000581C4 File Offset: 0x000563C4
			// (set) Token: 0x060012D1 RID: 4817 RVA: 0x000581D2 File Offset: 0x000563D2
			public bool initialStateCheckPending
			{
				get
				{
					return (this.flags & InputActionState.BindingState.Flags.InitialStateCheckPending) > (InputActionState.BindingState.Flags)0;
				}
				set
				{
					if (value)
					{
						this.flags |= InputActionState.BindingState.Flags.InitialStateCheckPending;
						return;
					}
					this.flags &= ~InputActionState.BindingState.Flags.InitialStateCheckPending;
				}
			}

			// Token: 0x1700050E RID: 1294
			// (get) Token: 0x060012D2 RID: 4818 RVA: 0x000581F6 File Offset: 0x000563F6
			// (set) Token: 0x060012D3 RID: 4819 RVA: 0x00058204 File Offset: 0x00056404
			public bool wantsInitialStateCheck
			{
				get
				{
					return (this.flags & InputActionState.BindingState.Flags.WantsInitialStateCheck) > (InputActionState.BindingState.Flags)0;
				}
				set
				{
					if (value)
					{
						this.flags |= InputActionState.BindingState.Flags.WantsInitialStateCheck;
						return;
					}
					this.flags &= ~InputActionState.BindingState.Flags.WantsInitialStateCheck;
				}
			}

			// Token: 0x1700050F RID: 1295
			// (get) Token: 0x060012D4 RID: 4820 RVA: 0x00058228 File Offset: 0x00056428
			// (set) Token: 0x060012D5 RID: 4821 RVA: 0x00058230 File Offset: 0x00056430
			public int partIndex
			{
				get
				{
					return (int)this.m_PartIndex;
				}
				set
				{
					if (this.partIndex < 0)
					{
						throw new ArgumentOutOfRangeException("value", "Part index must not be negative");
					}
					if (this.partIndex > 255)
					{
						throw new InvalidOperationException("Part count must not exceed byte.MaxValue=" + byte.MaxValue.ToString());
					}
					this.m_PartIndex = (byte)value;
				}
			}

			// Token: 0x040007BD RID: 1981
			[FieldOffset(0)]
			private byte m_ControlCount;

			// Token: 0x040007BE RID: 1982
			[FieldOffset(1)]
			private byte m_InteractionCount;

			// Token: 0x040007BF RID: 1983
			[FieldOffset(2)]
			private byte m_ProcessorCount;

			// Token: 0x040007C0 RID: 1984
			[FieldOffset(3)]
			private byte m_MapIndex;

			// Token: 0x040007C1 RID: 1985
			[FieldOffset(4)]
			private byte m_Flags;

			// Token: 0x040007C2 RID: 1986
			[FieldOffset(5)]
			private byte m_PartIndex;

			// Token: 0x040007C3 RID: 1987
			[FieldOffset(6)]
			private ushort m_ActionIndex;

			// Token: 0x040007C4 RID: 1988
			[FieldOffset(8)]
			private ushort m_CompositeOrCompositeBindingIndex;

			// Token: 0x040007C5 RID: 1989
			[FieldOffset(10)]
			private ushort m_ProcessorStartIndex;

			// Token: 0x040007C6 RID: 1990
			[FieldOffset(12)]
			private ushort m_InteractionStartIndex;

			// Token: 0x040007C7 RID: 1991
			[FieldOffset(14)]
			private ushort m_ControlStartIndex;

			// Token: 0x040007C8 RID: 1992
			[FieldOffset(16)]
			private double m_PressTime;

			// Token: 0x040007C9 RID: 1993
			[FieldOffset(24)]
			private int m_TriggerEventIdForComposite;

			// Token: 0x040007CA RID: 1994
			[FieldOffset(28)]
			private int __padding;

			// Token: 0x0200025B RID: 603
			[Flags]
			public enum Flags
			{
				// Token: 0x04000C55 RID: 3157
				ChainsWithNext = 1,
				// Token: 0x04000C56 RID: 3158
				EndOfChain = 2,
				// Token: 0x04000C57 RID: 3159
				Composite = 4,
				// Token: 0x04000C58 RID: 3160
				PartOfComposite = 8,
				// Token: 0x04000C59 RID: 3161
				InitialStateCheckPending = 16,
				// Token: 0x04000C5A RID: 3162
				WantsInitialStateCheck = 32
			}
		}

		// Token: 0x0200016E RID: 366
		[StructLayout(LayoutKind.Explicit, Size = 48)]
		public struct TriggerState
		{
			// Token: 0x17000510 RID: 1296
			// (get) Token: 0x060012D6 RID: 4822 RVA: 0x00058288 File Offset: 0x00056488
			// (set) Token: 0x060012D7 RID: 4823 RVA: 0x00058290 File Offset: 0x00056490
			public InputActionPhase phase
			{
				get
				{
					return (InputActionPhase)this.m_Phase;
				}
				set
				{
					this.m_Phase = (byte)value;
				}
			}

			// Token: 0x17000511 RID: 1297
			// (get) Token: 0x060012D8 RID: 4824 RVA: 0x0005829A File Offset: 0x0005649A
			public bool isDisabled
			{
				get
				{
					return this.phase == InputActionPhase.Disabled;
				}
			}

			// Token: 0x17000512 RID: 1298
			// (get) Token: 0x060012D9 RID: 4825 RVA: 0x000582A5 File Offset: 0x000564A5
			public bool isWaiting
			{
				get
				{
					return this.phase == InputActionPhase.Waiting;
				}
			}

			// Token: 0x17000513 RID: 1299
			// (get) Token: 0x060012DA RID: 4826 RVA: 0x000582B0 File Offset: 0x000564B0
			public bool isStarted
			{
				get
				{
					return this.phase == InputActionPhase.Started;
				}
			}

			// Token: 0x17000514 RID: 1300
			// (get) Token: 0x060012DB RID: 4827 RVA: 0x000582BB File Offset: 0x000564BB
			public bool isPerformed
			{
				get
				{
					return this.phase == InputActionPhase.Performed;
				}
			}

			// Token: 0x17000515 RID: 1301
			// (get) Token: 0x060012DC RID: 4828 RVA: 0x000582C6 File Offset: 0x000564C6
			public bool isCanceled
			{
				get
				{
					return this.phase == InputActionPhase.Canceled;
				}
			}

			// Token: 0x17000516 RID: 1302
			// (get) Token: 0x060012DD RID: 4829 RVA: 0x000582D1 File Offset: 0x000564D1
			// (set) Token: 0x060012DE RID: 4830 RVA: 0x000582D9 File Offset: 0x000564D9
			public double time
			{
				get
				{
					return this.m_Time;
				}
				set
				{
					this.m_Time = value;
				}
			}

			// Token: 0x17000517 RID: 1303
			// (get) Token: 0x060012DF RID: 4831 RVA: 0x000582E2 File Offset: 0x000564E2
			// (set) Token: 0x060012E0 RID: 4832 RVA: 0x000582EA File Offset: 0x000564EA
			public double startTime
			{
				get
				{
					return this.m_StartTime;
				}
				set
				{
					this.m_StartTime = value;
				}
			}

			// Token: 0x17000518 RID: 1304
			// (get) Token: 0x060012E1 RID: 4833 RVA: 0x000582F3 File Offset: 0x000564F3
			// (set) Token: 0x060012E2 RID: 4834 RVA: 0x000582FB File Offset: 0x000564FB
			public float magnitude
			{
				get
				{
					return this.m_Magnitude;
				}
				set
				{
					this.flags |= InputActionState.TriggerState.Flags.HaveMagnitude;
					this.m_Magnitude = value;
				}
			}

			// Token: 0x17000519 RID: 1305
			// (get) Token: 0x060012E3 RID: 4835 RVA: 0x00058312 File Offset: 0x00056512
			public bool haveMagnitude
			{
				get
				{
					return (this.flags & InputActionState.TriggerState.Flags.HaveMagnitude) > (InputActionState.TriggerState.Flags)0;
				}
			}

			// Token: 0x1700051A RID: 1306
			// (get) Token: 0x060012E4 RID: 4836 RVA: 0x0005831F File Offset: 0x0005651F
			// (set) Token: 0x060012E5 RID: 4837 RVA: 0x00058327 File Offset: 0x00056527
			public int mapIndex
			{
				get
				{
					return (int)this.m_MapIndex;
				}
				set
				{
					if (value < 0 || value > 255)
					{
						throw new NotSupportedException("More than byte.MaxValue InputActionMaps in a single InputActionState");
					}
					this.m_MapIndex = (byte)value;
				}
			}

			// Token: 0x1700051B RID: 1307
			// (get) Token: 0x060012E6 RID: 4838 RVA: 0x00058348 File Offset: 0x00056548
			// (set) Token: 0x060012E7 RID: 4839 RVA: 0x0005835F File Offset: 0x0005655F
			public int controlIndex
			{
				get
				{
					if (this.m_ControlIndex == 65535)
					{
						return -1;
					}
					return (int)this.m_ControlIndex;
				}
				set
				{
					if (value == -1)
					{
						this.m_ControlIndex = ushort.MaxValue;
						return;
					}
					if (value < 0 || value >= 65535)
					{
						throw new NotSupportedException("More than ushort.MaxValue-1 controls in a single InputActionState");
					}
					this.m_ControlIndex = (ushort)value;
				}
			}

			// Token: 0x1700051C RID: 1308
			// (get) Token: 0x060012E8 RID: 4840 RVA: 0x00058390 File Offset: 0x00056590
			// (set) Token: 0x060012E9 RID: 4841 RVA: 0x00058398 File Offset: 0x00056598
			public int bindingIndex
			{
				get
				{
					return (int)this.m_BindingIndex;
				}
				set
				{
					if (value < 0 || value > 65535)
					{
						throw new NotSupportedException("More than ushort.MaxValue bindings in a single InputActionState");
					}
					this.m_BindingIndex = (ushort)value;
				}
			}

			// Token: 0x1700051D RID: 1309
			// (get) Token: 0x060012EA RID: 4842 RVA: 0x000583B9 File Offset: 0x000565B9
			// (set) Token: 0x060012EB RID: 4843 RVA: 0x000583D0 File Offset: 0x000565D0
			public int interactionIndex
			{
				get
				{
					if (this.m_InteractionIndex == 65535)
					{
						return -1;
					}
					return (int)this.m_InteractionIndex;
				}
				set
				{
					if (value == -1)
					{
						this.m_InteractionIndex = ushort.MaxValue;
						return;
					}
					if (value < 0 || value >= 65535)
					{
						throw new NotSupportedException("More than ushort.MaxValue-1 interactions in a single InputActionState");
					}
					this.m_InteractionIndex = (ushort)value;
				}
			}

			// Token: 0x1700051E RID: 1310
			// (get) Token: 0x060012EC RID: 4844 RVA: 0x00058401 File Offset: 0x00056601
			// (set) Token: 0x060012ED RID: 4845 RVA: 0x00058409 File Offset: 0x00056609
			public uint lastPerformedInUpdate
			{
				get
				{
					return this.m_LastPerformedInUpdate;
				}
				set
				{
					this.m_LastPerformedInUpdate = value;
				}
			}

			// Token: 0x1700051F RID: 1311
			// (get) Token: 0x060012EE RID: 4846 RVA: 0x00058412 File Offset: 0x00056612
			// (set) Token: 0x060012EF RID: 4847 RVA: 0x0005841A File Offset: 0x0005661A
			public uint lastCanceledInUpdate
			{
				get
				{
					return this.m_LastCanceledInUpdate;
				}
				set
				{
					this.m_LastCanceledInUpdate = value;
				}
			}

			// Token: 0x17000520 RID: 1312
			// (get) Token: 0x060012F0 RID: 4848 RVA: 0x00058423 File Offset: 0x00056623
			// (set) Token: 0x060012F1 RID: 4849 RVA: 0x0005842B File Offset: 0x0005662B
			public uint pressedInUpdate
			{
				get
				{
					return this.m_PressedInUpdate;
				}
				set
				{
					this.m_PressedInUpdate = value;
				}
			}

			// Token: 0x17000521 RID: 1313
			// (get) Token: 0x060012F2 RID: 4850 RVA: 0x00058434 File Offset: 0x00056634
			// (set) Token: 0x060012F3 RID: 4851 RVA: 0x0005843C File Offset: 0x0005663C
			public uint releasedInUpdate
			{
				get
				{
					return this.m_ReleasedInUpdate;
				}
				set
				{
					this.m_ReleasedInUpdate = value;
				}
			}

			// Token: 0x17000522 RID: 1314
			// (get) Token: 0x060012F4 RID: 4852 RVA: 0x00058445 File Offset: 0x00056645
			// (set) Token: 0x060012F5 RID: 4853 RVA: 0x00058452 File Offset: 0x00056652
			public bool isPassThrough
			{
				get
				{
					return (this.flags & InputActionState.TriggerState.Flags.PassThrough) > (InputActionState.TriggerState.Flags)0;
				}
				set
				{
					if (value)
					{
						this.flags |= InputActionState.TriggerState.Flags.PassThrough;
						return;
					}
					this.flags &= ~InputActionState.TriggerState.Flags.PassThrough;
				}
			}

			// Token: 0x17000523 RID: 1315
			// (get) Token: 0x060012F6 RID: 4854 RVA: 0x00058475 File Offset: 0x00056675
			// (set) Token: 0x060012F7 RID: 4855 RVA: 0x00058483 File Offset: 0x00056683
			public bool isButton
			{
				get
				{
					return (this.flags & InputActionState.TriggerState.Flags.Button) > (InputActionState.TriggerState.Flags)0;
				}
				set
				{
					if (value)
					{
						this.flags |= InputActionState.TriggerState.Flags.Button;
						return;
					}
					this.flags &= ~InputActionState.TriggerState.Flags.Button;
				}
			}

			// Token: 0x17000524 RID: 1316
			// (get) Token: 0x060012F8 RID: 4856 RVA: 0x000584A7 File Offset: 0x000566A7
			// (set) Token: 0x060012F9 RID: 4857 RVA: 0x000584B5 File Offset: 0x000566B5
			public bool isPressed
			{
				get
				{
					return (this.flags & InputActionState.TriggerState.Flags.Pressed) > (InputActionState.TriggerState.Flags)0;
				}
				set
				{
					if (value)
					{
						this.flags |= InputActionState.TriggerState.Flags.Pressed;
						return;
					}
					this.flags &= ~InputActionState.TriggerState.Flags.Pressed;
				}
			}

			// Token: 0x17000525 RID: 1317
			// (get) Token: 0x060012FA RID: 4858 RVA: 0x000584D9 File Offset: 0x000566D9
			// (set) Token: 0x060012FB RID: 4859 RVA: 0x000584E6 File Offset: 0x000566E6
			public bool mayNeedConflictResolution
			{
				get
				{
					return (this.flags & InputActionState.TriggerState.Flags.MayNeedConflictResolution) > (InputActionState.TriggerState.Flags)0;
				}
				set
				{
					if (value)
					{
						this.flags |= InputActionState.TriggerState.Flags.MayNeedConflictResolution;
						return;
					}
					this.flags &= ~InputActionState.TriggerState.Flags.MayNeedConflictResolution;
				}
			}

			// Token: 0x17000526 RID: 1318
			// (get) Token: 0x060012FC RID: 4860 RVA: 0x00058509 File Offset: 0x00056709
			// (set) Token: 0x060012FD RID: 4861 RVA: 0x00058516 File Offset: 0x00056716
			public bool hasMultipleConcurrentActuations
			{
				get
				{
					return (this.flags & InputActionState.TriggerState.Flags.HasMultipleConcurrentActuations) > (InputActionState.TriggerState.Flags)0;
				}
				set
				{
					if (value)
					{
						this.flags |= InputActionState.TriggerState.Flags.HasMultipleConcurrentActuations;
						return;
					}
					this.flags &= ~InputActionState.TriggerState.Flags.HasMultipleConcurrentActuations;
				}
			}

			// Token: 0x17000527 RID: 1319
			// (get) Token: 0x060012FE RID: 4862 RVA: 0x00058539 File Offset: 0x00056739
			// (set) Token: 0x060012FF RID: 4863 RVA: 0x00058547 File Offset: 0x00056747
			public bool inProcessing
			{
				get
				{
					return (this.flags & InputActionState.TriggerState.Flags.InProcessing) > (InputActionState.TriggerState.Flags)0;
				}
				set
				{
					if (value)
					{
						this.flags |= InputActionState.TriggerState.Flags.InProcessing;
						return;
					}
					this.flags &= ~InputActionState.TriggerState.Flags.InProcessing;
				}
			}

			// Token: 0x17000528 RID: 1320
			// (get) Token: 0x06001300 RID: 4864 RVA: 0x0005856B File Offset: 0x0005676B
			// (set) Token: 0x06001301 RID: 4865 RVA: 0x00058573 File Offset: 0x00056773
			public InputActionState.TriggerState.Flags flags
			{
				get
				{
					return (InputActionState.TriggerState.Flags)this.m_Flags;
				}
				set
				{
					this.m_Flags = (byte)value;
				}
			}

			// Token: 0x040007CB RID: 1995
			public const int kMaxNumMaps = 255;

			// Token: 0x040007CC RID: 1996
			public const int kMaxNumControls = 65535;

			// Token: 0x040007CD RID: 1997
			public const int kMaxNumBindings = 65535;

			// Token: 0x040007CE RID: 1998
			[FieldOffset(0)]
			private byte m_Phase;

			// Token: 0x040007CF RID: 1999
			[FieldOffset(1)]
			private byte m_Flags;

			// Token: 0x040007D0 RID: 2000
			[FieldOffset(2)]
			private byte m_MapIndex;

			// Token: 0x040007D1 RID: 2001
			[FieldOffset(4)]
			private ushort m_ControlIndex;

			// Token: 0x040007D2 RID: 2002
			[FieldOffset(8)]
			private double m_Time;

			// Token: 0x040007D3 RID: 2003
			[FieldOffset(16)]
			private double m_StartTime;

			// Token: 0x040007D4 RID: 2004
			[FieldOffset(24)]
			private ushort m_BindingIndex;

			// Token: 0x040007D5 RID: 2005
			[FieldOffset(26)]
			private ushort m_InteractionIndex;

			// Token: 0x040007D6 RID: 2006
			[FieldOffset(28)]
			private float m_Magnitude;

			// Token: 0x040007D7 RID: 2007
			[FieldOffset(32)]
			private uint m_LastPerformedInUpdate;

			// Token: 0x040007D8 RID: 2008
			[FieldOffset(36)]
			private uint m_LastCanceledInUpdate;

			// Token: 0x040007D9 RID: 2009
			[FieldOffset(40)]
			private uint m_PressedInUpdate;

			// Token: 0x040007DA RID: 2010
			[FieldOffset(44)]
			private uint m_ReleasedInUpdate;

			// Token: 0x0200025C RID: 604
			[Flags]
			public enum Flags
			{
				// Token: 0x04000C5C RID: 3164
				HaveMagnitude = 1,
				// Token: 0x04000C5D RID: 3165
				PassThrough = 2,
				// Token: 0x04000C5E RID: 3166
				MayNeedConflictResolution = 4,
				// Token: 0x04000C5F RID: 3167
				HasMultipleConcurrentActuations = 8,
				// Token: 0x04000C60 RID: 3168
				InProcessing = 16,
				// Token: 0x04000C61 RID: 3169
				Button = 32,
				// Token: 0x04000C62 RID: 3170
				Pressed = 64
			}
		}

		// Token: 0x0200016F RID: 367
		public struct ActionMapIndices
		{
			// Token: 0x040007DB RID: 2011
			public int actionStartIndex;

			// Token: 0x040007DC RID: 2012
			public int actionCount;

			// Token: 0x040007DD RID: 2013
			public int controlStartIndex;

			// Token: 0x040007DE RID: 2014
			public int controlCount;

			// Token: 0x040007DF RID: 2015
			public int bindingStartIndex;

			// Token: 0x040007E0 RID: 2016
			public int bindingCount;

			// Token: 0x040007E1 RID: 2017
			public int interactionStartIndex;

			// Token: 0x040007E2 RID: 2018
			public int interactionCount;

			// Token: 0x040007E3 RID: 2019
			public int processorStartIndex;

			// Token: 0x040007E4 RID: 2020
			public int processorCount;

			// Token: 0x040007E5 RID: 2021
			public int compositeStartIndex;

			// Token: 0x040007E6 RID: 2022
			public int compositeCount;
		}

		// Token: 0x02000170 RID: 368
		public struct UnmanagedMemory : IDisposable
		{
			// Token: 0x17000529 RID: 1321
			// (get) Token: 0x06001302 RID: 4866 RVA: 0x0005857D File Offset: 0x0005677D
			public bool isAllocated
			{
				get
				{
					return this.basePtr != null;
				}
			}

			// Token: 0x1700052A RID: 1322
			// (get) Token: 0x06001303 RID: 4867 RVA: 0x0005858C File Offset: 0x0005678C
			public unsafe int sizeInBytes
			{
				get
				{
					return this.mapCount * sizeof(InputActionState.ActionMapIndices) + this.actionCount * sizeof(InputActionState.TriggerState) + this.bindingCount * sizeof(InputActionState.BindingState) + this.interactionCount * sizeof(InputActionState.InteractionState) + this.controlCount * 4 + this.compositeCount * 4 + this.controlCount * 4 + this.controlCount * 2 * 2 + this.actionCount * 2 * 2 + this.bindingCount * 2 + (this.controlCount + 31) / 32 * 4;
				}
			}

			// Token: 0x06001304 RID: 4868 RVA: 0x0005861C File Offset: 0x0005681C
			public unsafe void Allocate(int mapCount, int actionCount, int bindingCount, int controlCount, int interactionCount, int compositeCount)
			{
				this.mapCount = mapCount;
				this.actionCount = actionCount;
				this.interactionCount = interactionCount;
				this.bindingCount = bindingCount;
				this.controlCount = controlCount;
				this.compositeCount = compositeCount;
				int sizeInBytes = this.sizeInBytes;
				byte* ptr = (byte*)UnsafeUtility.Malloc((long)sizeInBytes, 8, Allocator.Persistent);
				UnsafeUtility.MemClear((void*)ptr, (long)sizeInBytes);
				this.basePtr = (void*)ptr;
				this.actionStates = (InputActionState.TriggerState*)ptr;
				ptr += actionCount * sizeof(InputActionState.TriggerState);
				this.interactionStates = (InputActionState.InteractionState*)ptr;
				ptr += interactionCount * sizeof(InputActionState.InteractionState);
				this.bindingStates = (InputActionState.BindingState*)ptr;
				ptr += bindingCount * sizeof(InputActionState.BindingState);
				this.mapIndices = (InputActionState.ActionMapIndices*)ptr;
				ptr += mapCount * sizeof(InputActionState.ActionMapIndices);
				this.controlMagnitudes = (float*)ptr;
				ptr += controlCount * 4;
				this.compositeMagnitudes = (float*)ptr;
				ptr += compositeCount * 4;
				this.controlIndexToBindingIndex = (int*)ptr;
				ptr += controlCount * 4;
				this.controlGroupingAndComplexity = (ushort*)ptr;
				ptr += controlCount * 2 * 2;
				this.actionBindingIndicesAndCounts = (ushort*)ptr;
				ptr += actionCount * 2 * 2;
				this.actionBindingIndices = (ushort*)ptr;
				ptr += bindingCount * 2;
				this.enabledControls = (int*)ptr;
				ptr += (controlCount + 31) / 32 * 4;
			}

			// Token: 0x06001305 RID: 4869 RVA: 0x0005872C File Offset: 0x0005692C
			public void Dispose()
			{
				if (this.basePtr == null)
				{
					return;
				}
				UnsafeUtility.Free(this.basePtr, Allocator.Persistent);
				this.basePtr = null;
				this.actionStates = null;
				this.interactionStates = null;
				this.bindingStates = null;
				this.mapIndices = null;
				this.controlMagnitudes = null;
				this.compositeMagnitudes = null;
				this.controlIndexToBindingIndex = null;
				this.controlGroupingAndComplexity = null;
				this.actionBindingIndices = null;
				this.actionBindingIndicesAndCounts = null;
				this.mapCount = 0;
				this.actionCount = 0;
				this.bindingCount = 0;
				this.controlCount = 0;
				this.interactionCount = 0;
				this.compositeCount = 0;
			}

			// Token: 0x06001306 RID: 4870 RVA: 0x000587D4 File Offset: 0x000569D4
			public unsafe void CopyDataFrom(InputActionState.UnmanagedMemory memory)
			{
				UnsafeUtility.MemCpy((void*)this.mapIndices, (void*)memory.mapIndices, (long)(memory.mapCount * sizeof(InputActionState.ActionMapIndices)));
				UnsafeUtility.MemCpy((void*)this.actionStates, (void*)memory.actionStates, (long)(memory.actionCount * sizeof(InputActionState.TriggerState)));
				UnsafeUtility.MemCpy((void*)this.bindingStates, (void*)memory.bindingStates, (long)(memory.bindingCount * sizeof(InputActionState.BindingState)));
				UnsafeUtility.MemCpy((void*)this.interactionStates, (void*)memory.interactionStates, (long)(memory.interactionCount * sizeof(InputActionState.InteractionState)));
				UnsafeUtility.MemCpy((void*)this.controlMagnitudes, (void*)memory.controlMagnitudes, (long)(memory.controlCount * 4));
				UnsafeUtility.MemCpy((void*)this.compositeMagnitudes, (void*)memory.compositeMagnitudes, (long)(memory.compositeCount * 4));
				UnsafeUtility.MemCpy((void*)this.controlIndexToBindingIndex, (void*)memory.controlIndexToBindingIndex, (long)(memory.controlCount * 4));
				UnsafeUtility.MemCpy((void*)this.controlGroupingAndComplexity, (void*)memory.controlGroupingAndComplexity, (long)(memory.controlCount * 2 * 2));
				UnsafeUtility.MemCpy((void*)this.actionBindingIndicesAndCounts, (void*)memory.actionBindingIndicesAndCounts, (long)(memory.actionCount * 2 * 2));
				UnsafeUtility.MemCpy((void*)this.actionBindingIndices, (void*)memory.actionBindingIndices, (long)(memory.bindingCount * 2));
				UnsafeUtility.MemCpy((void*)this.enabledControls, (void*)memory.enabledControls, (long)((memory.controlCount + 31) / 32 * 4));
			}

			// Token: 0x06001307 RID: 4871 RVA: 0x00058920 File Offset: 0x00056B20
			public InputActionState.UnmanagedMemory Clone()
			{
				if (!this.isAllocated)
				{
					return default(InputActionState.UnmanagedMemory);
				}
				InputActionState.UnmanagedMemory unmanagedMemory = default(InputActionState.UnmanagedMemory);
				int num = this.mapCount;
				int num2 = this.actionCount;
				int num3 = this.controlCount;
				unmanagedMemory.Allocate(num, num2, this.bindingCount, num3, this.interactionCount, this.compositeCount);
				unmanagedMemory.CopyDataFrom(this);
				return unmanagedMemory;
			}

			// Token: 0x040007E7 RID: 2023
			public unsafe void* basePtr;

			// Token: 0x040007E8 RID: 2024
			public int mapCount;

			// Token: 0x040007E9 RID: 2025
			public int actionCount;

			// Token: 0x040007EA RID: 2026
			public int interactionCount;

			// Token: 0x040007EB RID: 2027
			public int bindingCount;

			// Token: 0x040007EC RID: 2028
			public int controlCount;

			// Token: 0x040007ED RID: 2029
			public int compositeCount;

			// Token: 0x040007EE RID: 2030
			public unsafe InputActionState.TriggerState* actionStates;

			// Token: 0x040007EF RID: 2031
			public unsafe InputActionState.BindingState* bindingStates;

			// Token: 0x040007F0 RID: 2032
			public unsafe InputActionState.InteractionState* interactionStates;

			// Token: 0x040007F1 RID: 2033
			public unsafe float* controlMagnitudes;

			// Token: 0x040007F2 RID: 2034
			public unsafe float* compositeMagnitudes;

			// Token: 0x040007F3 RID: 2035
			public unsafe int* enabledControls;

			// Token: 0x040007F4 RID: 2036
			public unsafe ushort* actionBindingIndicesAndCounts;

			// Token: 0x040007F5 RID: 2037
			public unsafe ushort* actionBindingIndices;

			// Token: 0x040007F6 RID: 2038
			public unsafe int* controlIndexToBindingIndex;

			// Token: 0x040007F7 RID: 2039
			public unsafe ushort* controlGroupingAndComplexity;

			// Token: 0x040007F8 RID: 2040
			public bool controlGroupingInitialized;

			// Token: 0x040007F9 RID: 2041
			public unsafe InputActionState.ActionMapIndices* mapIndices;
		}

		// Token: 0x02000171 RID: 369
		internal struct GlobalState
		{
			// Token: 0x040007FA RID: 2042
			internal InlinedArray<GCHandle> globalList;

			// Token: 0x040007FB RID: 2043
			internal CallbackArray<Action<object, InputActionChange>> onActionChange;

			// Token: 0x040007FC RID: 2044
			internal CallbackArray<Action<object>> onActionControlsChanged;
		}
	}
}
