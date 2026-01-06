using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x02000123 RID: 291
	public sealed class InputActionTrace : IEnumerable<InputActionTrace.ActionEventPtr>, IEnumerable, IDisposable
	{
		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x0600102A RID: 4138 RVA: 0x0004D335 File Offset: 0x0004B535
		public InputEventBuffer buffer
		{
			get
			{
				return this.m_EventBuffer;
			}
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x0600102B RID: 4139 RVA: 0x0004D33D File Offset: 0x0004B53D
		public int count
		{
			get
			{
				return this.m_EventBuffer.eventCount;
			}
		}

		// Token: 0x0600102C RID: 4140 RVA: 0x0004D34A File Offset: 0x0004B54A
		public InputActionTrace()
		{
		}

		// Token: 0x0600102D RID: 4141 RVA: 0x0004D352 File Offset: 0x0004B552
		public InputActionTrace(InputAction action)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			this.SubscribeTo(action);
		}

		// Token: 0x0600102E RID: 4142 RVA: 0x0004D36F File Offset: 0x0004B56F
		public InputActionTrace(InputActionMap actionMap)
		{
			if (actionMap == null)
			{
				throw new ArgumentNullException("actionMap");
			}
			this.SubscribeTo(actionMap);
		}

		// Token: 0x0600102F RID: 4143 RVA: 0x0004D38C File Offset: 0x0004B58C
		public void SubscribeToAll()
		{
			if (this.m_SubscribedToAll)
			{
				return;
			}
			this.HookOnActionChange();
			this.m_SubscribedToAll = true;
			while (this.m_SubscribedActions.length > 0)
			{
				this.UnsubscribeFrom(this.m_SubscribedActions[this.m_SubscribedActions.length - 1]);
			}
			while (this.m_SubscribedActionMaps.length > 0)
			{
				this.UnsubscribeFrom(this.m_SubscribedActionMaps[this.m_SubscribedActionMaps.length - 1]);
			}
		}

		// Token: 0x06001030 RID: 4144 RVA: 0x0004D40C File Offset: 0x0004B60C
		public void UnsubscribeFromAll()
		{
			if (this.count == 0)
			{
				this.UnhookOnActionChange();
			}
			this.m_SubscribedToAll = false;
			while (this.m_SubscribedActions.length > 0)
			{
				this.UnsubscribeFrom(this.m_SubscribedActions[this.m_SubscribedActions.length - 1]);
			}
			while (this.m_SubscribedActionMaps.length > 0)
			{
				this.UnsubscribeFrom(this.m_SubscribedActionMaps[this.m_SubscribedActionMaps.length - 1]);
			}
		}

		// Token: 0x06001031 RID: 4145 RVA: 0x0004D48C File Offset: 0x0004B68C
		public void SubscribeTo(InputAction action)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			if (this.m_CallbackDelegate == null)
			{
				this.m_CallbackDelegate = new Action<InputAction.CallbackContext>(this.RecordAction);
			}
			action.performed += this.m_CallbackDelegate;
			action.started += this.m_CallbackDelegate;
			action.canceled += this.m_CallbackDelegate;
			this.m_SubscribedActions.AppendWithCapacity(action, 10);
		}

		// Token: 0x06001032 RID: 4146 RVA: 0x0004D4F4 File Offset: 0x0004B6F4
		public void SubscribeTo(InputActionMap actionMap)
		{
			if (actionMap == null)
			{
				throw new ArgumentNullException("actionMap");
			}
			if (this.m_CallbackDelegate == null)
			{
				this.m_CallbackDelegate = new Action<InputAction.CallbackContext>(this.RecordAction);
			}
			actionMap.actionTriggered += this.m_CallbackDelegate;
			this.m_SubscribedActionMaps.AppendWithCapacity(actionMap, 10);
		}

		// Token: 0x06001033 RID: 4147 RVA: 0x0004D544 File Offset: 0x0004B744
		public void UnsubscribeFrom(InputAction action)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			if (this.m_CallbackDelegate == null)
			{
				return;
			}
			action.performed -= this.m_CallbackDelegate;
			action.started -= this.m_CallbackDelegate;
			action.canceled -= this.m_CallbackDelegate;
			int num = this.m_SubscribedActions.IndexOfReference(action);
			if (num != -1)
			{
				this.m_SubscribedActions.RemoveAtWithCapacity(num);
			}
		}

		// Token: 0x06001034 RID: 4148 RVA: 0x0004D5AC File Offset: 0x0004B7AC
		public void UnsubscribeFrom(InputActionMap actionMap)
		{
			if (actionMap == null)
			{
				throw new ArgumentNullException("actionMap");
			}
			if (this.m_CallbackDelegate == null)
			{
				return;
			}
			actionMap.actionTriggered -= this.m_CallbackDelegate;
			int num = this.m_SubscribedActionMaps.IndexOfReference(actionMap);
			if (num != -1)
			{
				this.m_SubscribedActionMaps.RemoveAtWithCapacity(num);
			}
		}

		// Token: 0x06001035 RID: 4149 RVA: 0x0004D5FC File Offset: 0x0004B7FC
		public unsafe void RecordAction(InputAction.CallbackContext context)
		{
			int num = this.m_ActionMapStates.IndexOfReference(context.m_State);
			if (num == -1)
			{
				num = this.m_ActionMapStates.AppendWithCapacity(context.m_State, 10);
			}
			this.HookOnActionChange();
			int valueSizeInBytes = context.valueSizeInBytes;
			ActionEvent* ptr = (ActionEvent*)this.m_EventBuffer.AllocateEvent(ActionEvent.GetEventSizeWithValueSize(valueSizeInBytes), 2048, Allocator.Persistent);
			ref InputActionState.TriggerState ptr2 = ref context.m_State.actionStates[context.m_ActionIndex];
			ptr->baseEvent.type = ActionEvent.Type;
			ptr->baseEvent.time = ptr2.time;
			ptr->stateIndex = num;
			ptr->controlIndex = ptr2.controlIndex;
			ptr->bindingIndex = ptr2.bindingIndex;
			ptr->interactionIndex = ptr2.interactionIndex;
			ptr->startTime = ptr2.startTime;
			ptr->phase = ptr2.phase;
			byte* valueData = ptr->valueData;
			context.ReadValue((void*)valueData, valueSizeInBytes);
		}

		// Token: 0x06001036 RID: 4150 RVA: 0x0004D6EA File Offset: 0x0004B8EA
		public void Clear()
		{
			this.m_EventBuffer.Reset();
			this.m_ActionMapStates.ClearWithCapacity();
		}

		// Token: 0x06001037 RID: 4151 RVA: 0x0004D704 File Offset: 0x0004B904
		~InputActionTrace()
		{
			this.DisposeInternal();
		}

		// Token: 0x06001038 RID: 4152 RVA: 0x0004D730 File Offset: 0x0004B930
		public override string ToString()
		{
			if (this.count == 0)
			{
				return "[]";
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('[');
			bool flag = true;
			foreach (InputActionTrace.ActionEventPtr actionEventPtr in this)
			{
				if (!flag)
				{
					stringBuilder.Append(",\n");
				}
				stringBuilder.Append(actionEventPtr.ToString());
				flag = false;
			}
			stringBuilder.Append(']');
			return stringBuilder.ToString();
		}

		// Token: 0x06001039 RID: 4153 RVA: 0x0004D7C4 File Offset: 0x0004B9C4
		public void Dispose()
		{
			this.UnsubscribeFromAll();
			this.DisposeInternal();
		}

		// Token: 0x0600103A RID: 4154 RVA: 0x0004D7D4 File Offset: 0x0004B9D4
		private void DisposeInternal()
		{
			for (int i = 0; i < this.m_ActionMapStateClones.length; i++)
			{
				this.m_ActionMapStateClones[i].Dispose();
			}
			this.m_EventBuffer.Dispose();
			this.m_ActionMapStates.Clear();
			this.m_ActionMapStateClones.Clear();
			if (this.m_ActionChangeDelegate != null)
			{
				InputSystem.onActionChange -= this.m_ActionChangeDelegate;
				this.m_ActionChangeDelegate = null;
			}
		}

		// Token: 0x0600103B RID: 4155 RVA: 0x0004D843 File Offset: 0x0004BA43
		public IEnumerator<InputActionTrace.ActionEventPtr> GetEnumerator()
		{
			return new InputActionTrace.Enumerator(this);
		}

		// Token: 0x0600103C RID: 4156 RVA: 0x0004D850 File Offset: 0x0004BA50
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600103D RID: 4157 RVA: 0x0004D858 File Offset: 0x0004BA58
		private void HookOnActionChange()
		{
			if (this.m_OnActionChangeHooked)
			{
				return;
			}
			if (this.m_ActionChangeDelegate == null)
			{
				this.m_ActionChangeDelegate = new Action<object, InputActionChange>(this.OnActionChange);
			}
			InputSystem.onActionChange += this.m_ActionChangeDelegate;
			this.m_OnActionChangeHooked = true;
		}

		// Token: 0x0600103E RID: 4158 RVA: 0x0004D88F File Offset: 0x0004BA8F
		private void UnhookOnActionChange()
		{
			if (!this.m_OnActionChangeHooked)
			{
				return;
			}
			InputSystem.onActionChange -= this.m_ActionChangeDelegate;
			this.m_OnActionChangeHooked = false;
		}

		// Token: 0x0600103F RID: 4159 RVA: 0x0004D8AC File Offset: 0x0004BAAC
		private void OnActionChange(object actionOrMapOrAsset, InputActionChange change)
		{
			if (this.m_SubscribedToAll && change - InputActionChange.ActionStarted <= 2)
			{
				InputAction inputAction = (InputAction)actionOrMapOrAsset;
				int actionIndexInState = inputAction.m_ActionIndexInState;
				InputActionState state = inputAction.m_ActionMap.m_State;
				InputAction.CallbackContext callbackContext = new InputAction.CallbackContext
				{
					m_State = state,
					m_ActionIndex = actionIndexInState
				};
				this.RecordAction(callbackContext);
				return;
			}
			if (change != InputActionChange.BoundControlsAboutToChange)
			{
				return;
			}
			InputAction inputAction2 = actionOrMapOrAsset as InputAction;
			if (inputAction2 != null)
			{
				this.CloneActionStateBeforeBindingsChange(inputAction2.m_ActionMap);
				return;
			}
			InputActionMap inputActionMap = actionOrMapOrAsset as InputActionMap;
			if (inputActionMap != null)
			{
				this.CloneActionStateBeforeBindingsChange(inputActionMap);
				return;
			}
			InputActionAsset inputActionAsset = actionOrMapOrAsset as InputActionAsset;
			if (inputActionAsset != null)
			{
				foreach (InputActionMap inputActionMap2 in inputActionAsset.actionMaps)
				{
					this.CloneActionStateBeforeBindingsChange(inputActionMap2);
				}
			}
		}

		// Token: 0x06001040 RID: 4160 RVA: 0x0004D990 File Offset: 0x0004BB90
		private void CloneActionStateBeforeBindingsChange(InputActionMap actionMap)
		{
			InputActionState state = actionMap.m_State;
			if (state == null)
			{
				return;
			}
			int num = this.m_ActionMapStates.IndexOfReference(state);
			if (num == -1)
			{
				return;
			}
			InputActionState inputActionState = state.Clone();
			this.m_ActionMapStateClones.Append(inputActionState);
			this.m_ActionMapStates[num] = inputActionState;
		}

		// Token: 0x040006A8 RID: 1704
		private bool m_SubscribedToAll;

		// Token: 0x040006A9 RID: 1705
		private bool m_OnActionChangeHooked;

		// Token: 0x040006AA RID: 1706
		private InlinedArray<InputAction> m_SubscribedActions;

		// Token: 0x040006AB RID: 1707
		private InlinedArray<InputActionMap> m_SubscribedActionMaps;

		// Token: 0x040006AC RID: 1708
		private InputEventBuffer m_EventBuffer;

		// Token: 0x040006AD RID: 1709
		private InlinedArray<InputActionState> m_ActionMapStates;

		// Token: 0x040006AE RID: 1710
		private InlinedArray<InputActionState> m_ActionMapStateClones;

		// Token: 0x040006AF RID: 1711
		private Action<InputAction.CallbackContext> m_CallbackDelegate;

		// Token: 0x040006B0 RID: 1712
		private Action<object, InputActionChange> m_ActionChangeDelegate;

		// Token: 0x02000233 RID: 563
		public struct ActionEventPtr
		{
			// Token: 0x170005CD RID: 1485
			// (get) Token: 0x06001573 RID: 5491 RVA: 0x00062694 File Offset: 0x00060894
			public unsafe InputAction action
			{
				get
				{
					return this.m_State.GetActionOrNull(this.m_Ptr->bindingIndex);
				}
			}

			// Token: 0x170005CE RID: 1486
			// (get) Token: 0x06001574 RID: 5492 RVA: 0x000626AC File Offset: 0x000608AC
			public unsafe InputActionPhase phase
			{
				get
				{
					return this.m_Ptr->phase;
				}
			}

			// Token: 0x170005CF RID: 1487
			// (get) Token: 0x06001575 RID: 5493 RVA: 0x000626B9 File Offset: 0x000608B9
			public unsafe InputControl control
			{
				get
				{
					return this.m_State.controls[this.m_Ptr->controlIndex];
				}
			}

			// Token: 0x170005D0 RID: 1488
			// (get) Token: 0x06001576 RID: 5494 RVA: 0x000626D4 File Offset: 0x000608D4
			public unsafe IInputInteraction interaction
			{
				get
				{
					int interactionIndex = this.m_Ptr->interactionIndex;
					if (interactionIndex == -1)
					{
						return null;
					}
					return this.m_State.interactions[interactionIndex];
				}
			}

			// Token: 0x170005D1 RID: 1489
			// (get) Token: 0x06001577 RID: 5495 RVA: 0x00062700 File Offset: 0x00060900
			public unsafe double time
			{
				get
				{
					return this.m_Ptr->baseEvent.time;
				}
			}

			// Token: 0x170005D2 RID: 1490
			// (get) Token: 0x06001578 RID: 5496 RVA: 0x00062712 File Offset: 0x00060912
			public unsafe double startTime
			{
				get
				{
					return this.m_Ptr->startTime;
				}
			}

			// Token: 0x170005D3 RID: 1491
			// (get) Token: 0x06001579 RID: 5497 RVA: 0x0006271F File Offset: 0x0006091F
			public double duration
			{
				get
				{
					return this.time - this.startTime;
				}
			}

			// Token: 0x170005D4 RID: 1492
			// (get) Token: 0x0600157A RID: 5498 RVA: 0x0006272E File Offset: 0x0006092E
			public unsafe int valueSizeInBytes
			{
				get
				{
					return this.m_Ptr->valueSizeInBytes;
				}
			}

			// Token: 0x0600157B RID: 5499 RVA: 0x0006273C File Offset: 0x0006093C
			public unsafe object ReadValueAsObject()
			{
				if (this.m_Ptr == null)
				{
					throw new InvalidOperationException("ActionEventPtr is invalid");
				}
				byte* valueData = this.m_Ptr->valueData;
				int bindingIndex = this.m_Ptr->bindingIndex;
				if (!this.m_State.bindingStates[bindingIndex].isPartOfComposite)
				{
					int valueSizeInBytes = this.m_Ptr->valueSizeInBytes;
					return this.control.ReadValueFromBufferAsObject((void*)valueData, valueSizeInBytes);
				}
				int compositeOrCompositeBindingIndex = this.m_State.bindingStates[bindingIndex].compositeOrCompositeBindingIndex;
				int compositeOrCompositeBindingIndex2 = this.m_State.bindingStates[compositeOrCompositeBindingIndex].compositeOrCompositeBindingIndex;
				InputBindingComposite inputBindingComposite = this.m_State.composites[compositeOrCompositeBindingIndex2];
				Type valueType = inputBindingComposite.valueType;
				if (valueType == null)
				{
					throw new InvalidOperationException(string.Format("Cannot read value from Composite '{0}' which does not have a valueType set", inputBindingComposite));
				}
				return Marshal.PtrToStructure(new IntPtr((void*)valueData), valueType);
			}

			// Token: 0x0600157C RID: 5500 RVA: 0x00062828 File Offset: 0x00060A28
			public unsafe void ReadValue(void* buffer, int bufferSize)
			{
				int valueSizeInBytes = this.m_Ptr->valueSizeInBytes;
				if (bufferSize < valueSizeInBytes)
				{
					throw new ArgumentException(string.Format("Expected buffer of at least {0} bytes but got buffer of just {1} bytes instead", valueSizeInBytes, bufferSize), "bufferSize");
				}
				UnsafeUtility.MemCpy(buffer, (void*)this.m_Ptr->valueData, (long)valueSizeInBytes);
			}

			// Token: 0x0600157D RID: 5501 RVA: 0x0006287C File Offset: 0x00060A7C
			public unsafe TValue ReadValue<TValue>() where TValue : struct
			{
				int valueSizeInBytes = this.m_Ptr->valueSizeInBytes;
				if (UnsafeUtility.SizeOf<TValue>() != valueSizeInBytes)
				{
					throw new InvalidOperationException(string.Format("Cannot read a value of type '{0}' with size {1} from event on action '{2}' with value size {3}", new object[]
					{
						typeof(TValue).Name,
						UnsafeUtility.SizeOf<TValue>(),
						this.action,
						valueSizeInBytes
					}));
				}
				TValue tvalue = new TValue();
				UnsafeUtility.MemCpy(UnsafeUtility.AddressOf<TValue>(ref tvalue), (void*)this.m_Ptr->valueData, (long)valueSizeInBytes);
				return tvalue;
			}

			// Token: 0x0600157E RID: 5502 RVA: 0x00062904 File Offset: 0x00060B04
			public override string ToString()
			{
				if (this.m_Ptr == null)
				{
					return "<null>";
				}
				string text = ((this.action.actionMap != null) ? (this.action.actionMap.name + "/" + this.action.name) : this.action.name);
				return string.Format("{{ action={0} phase={1} time={2} control={3} value={4} interaction={5} duration={6} }}", new object[]
				{
					text,
					this.phase,
					this.time,
					this.control,
					this.ReadValueAsObject(),
					this.interaction,
					this.duration
				});
			}

			// Token: 0x04000BEA RID: 3050
			internal InputActionState m_State;

			// Token: 0x04000BEB RID: 3051
			internal unsafe ActionEvent* m_Ptr;
		}

		// Token: 0x02000234 RID: 564
		private struct Enumerator : IEnumerator<InputActionTrace.ActionEventPtr>, IEnumerator, IDisposable
		{
			// Token: 0x0600157F RID: 5503 RVA: 0x000629BC File Offset: 0x00060BBC
			public unsafe Enumerator(InputActionTrace trace)
			{
				this.m_Trace = trace;
				this.m_Buffer = (ActionEvent*)trace.m_EventBuffer.bufferPtr.data;
				this.m_EventCount = trace.m_EventBuffer.eventCount;
				this.m_CurrentEvent = null;
				this.m_CurrentIndex = 0;
			}

			// Token: 0x06001580 RID: 5504 RVA: 0x00062A0C File Offset: 0x00060C0C
			public unsafe bool MoveNext()
			{
				if (this.m_CurrentIndex == this.m_EventCount)
				{
					return false;
				}
				if (this.m_CurrentEvent == null)
				{
					this.m_CurrentEvent = this.m_Buffer;
					return this.m_CurrentEvent != null;
				}
				this.m_CurrentIndex++;
				if (this.m_CurrentIndex == this.m_EventCount)
				{
					return false;
				}
				this.m_CurrentEvent = (ActionEvent*)InputEvent.GetNextInMemory((InputEvent*)this.m_CurrentEvent);
				return true;
			}

			// Token: 0x06001581 RID: 5505 RVA: 0x00062A7D File Offset: 0x00060C7D
			public void Reset()
			{
				this.m_CurrentEvent = null;
				this.m_CurrentIndex = 0;
			}

			// Token: 0x06001582 RID: 5506 RVA: 0x00062A8E File Offset: 0x00060C8E
			public void Dispose()
			{
			}

			// Token: 0x170005D5 RID: 1493
			// (get) Token: 0x06001583 RID: 5507 RVA: 0x00062A90 File Offset: 0x00060C90
			public unsafe InputActionTrace.ActionEventPtr Current
			{
				get
				{
					InputActionState inputActionState = this.m_Trace.m_ActionMapStates[this.m_CurrentEvent->stateIndex];
					return new InputActionTrace.ActionEventPtr
					{
						m_State = inputActionState,
						m_Ptr = this.m_CurrentEvent
					};
				}
			}

			// Token: 0x170005D6 RID: 1494
			// (get) Token: 0x06001584 RID: 5508 RVA: 0x00062AD7 File Offset: 0x00060CD7
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x04000BEC RID: 3052
			private readonly InputActionTrace m_Trace;

			// Token: 0x04000BED RID: 3053
			private unsafe readonly ActionEvent* m_Buffer;

			// Token: 0x04000BEE RID: 3054
			private readonly int m_EventCount;

			// Token: 0x04000BEF RID: 3055
			private unsafe ActionEvent* m_CurrentEvent;

			// Token: 0x04000BF0 RID: 3056
			private int m_CurrentIndex;
		}
	}
}
