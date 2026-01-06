using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000053 RID: 83
	[InputControlLayout(stateType = typeof(TouchscreenState), isGenericTypeOfDevice = true)]
	public class Touchscreen : Pointer, IInputStateCallbackReceiver, IEventMerger, ICustomDeviceReset
	{
		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000808 RID: 2056 RVA: 0x0002C9B5 File Offset: 0x0002ABB5
		// (set) Token: 0x06000809 RID: 2057 RVA: 0x0002C9BD File Offset: 0x0002ABBD
		public TouchControl primaryTouch { get; protected set; }

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x0600080A RID: 2058 RVA: 0x0002C9C6 File Offset: 0x0002ABC6
		// (set) Token: 0x0600080B RID: 2059 RVA: 0x0002C9CE File Offset: 0x0002ABCE
		public ReadOnlyArray<TouchControl> touches { get; protected set; }

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x0600080C RID: 2060 RVA: 0x0002C9D7 File Offset: 0x0002ABD7
		// (set) Token: 0x0600080D RID: 2061 RVA: 0x0002C9E4 File Offset: 0x0002ABE4
		protected TouchControl[] touchControlArray
		{
			get
			{
				return this.touches.m_Array;
			}
			set
			{
				this.touches = new ReadOnlyArray<TouchControl>(value);
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x0600080E RID: 2062 RVA: 0x0002C9F2 File Offset: 0x0002ABF2
		// (set) Token: 0x0600080F RID: 2063 RVA: 0x0002C9F9 File Offset: 0x0002ABF9
		public new static Touchscreen current { get; internal set; }

		// Token: 0x06000810 RID: 2064 RVA: 0x0002CA01 File Offset: 0x0002AC01
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			Touchscreen.current = this;
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x0002CA0F File Offset: 0x0002AC0F
		protected override void OnRemoved()
		{
			base.OnRemoved();
			if (Touchscreen.current == this)
			{
				Touchscreen.current = null;
			}
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x0002CA28 File Offset: 0x0002AC28
		protected override void FinishSetup()
		{
			base.FinishSetup();
			this.primaryTouch = base.GetChildControl<TouchControl>("primaryTouch");
			base.displayIndex = this.primaryTouch.displayIndex;
			int num = 0;
			using (ReadOnlyArray<InputControl>.Enumerator enumerator = base.children.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current is TouchControl)
					{
						num++;
					}
				}
			}
			if (num >= 1)
			{
				num--;
			}
			TouchControl[] array = new TouchControl[num];
			int num2 = 0;
			foreach (InputControl inputControl in base.children)
			{
				if (inputControl != this.primaryTouch)
				{
					TouchControl touchControl = inputControl as TouchControl;
					if (touchControl != null)
					{
						array[num2++] = touchControl;
					}
				}
			}
			this.touches = new ReadOnlyArray<TouchControl>(array);
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x0002CB2C File Offset: 0x0002AD2C
		protected new unsafe void OnNextUpdate()
		{
			void* currentStatePtr = base.currentStatePtr;
			TouchState* ptr = (TouchState*)((byte*)((byte*)currentStatePtr + base.stateBlock.byteOffset) + 56);
			int i = 0;
			while (i < this.touches.Count)
			{
				if (ptr->delta != default(Vector2))
				{
					InputState.Change<Vector2>(this.touches[i].delta, Vector2.zero, InputUpdateType.None, default(InputEventPtr));
				}
				if (ptr->tapCount > 0 && InputState.currentTime >= ptr->startTime + (double)Touchscreen.s_TapTime + (double)Touchscreen.s_TapDelayTime)
				{
					InputState.Change<byte>(this.touches[i].tapCount, 0, InputUpdateType.None, default(InputEventPtr));
				}
				i++;
				ptr++;
			}
			TouchState* ptr2 = (TouchState*)((byte*)currentStatePtr + base.stateBlock.byteOffset);
			if (ptr2->delta != default(Vector2))
			{
				InputState.Change<Vector2>(this.primaryTouch.delta, Vector2.zero, InputUpdateType.None, default(InputEventPtr));
			}
			if (ptr2->tapCount > 0 && InputState.currentTime >= ptr2->startTime + (double)Touchscreen.s_TapTime + (double)Touchscreen.s_TapDelayTime)
			{
				InputState.Change<byte>(this.primaryTouch.tapCount, 0, InputUpdateType.None, default(InputEventPtr));
			}
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x0002CC98 File Offset: 0x0002AE98
		protected new unsafe void OnStateEvent(InputEventPtr eventPtr)
		{
			if (eventPtr.type == 1145852993)
			{
				return;
			}
			StateEvent* ptr = StateEvent.FromUnchecked(eventPtr);
			if (ptr->stateFormat != TouchState.Format)
			{
				InputState.Change(this, eventPtr, InputUpdateType.None);
				return;
			}
			void* currentStatePtr = base.currentStatePtr;
			TouchState* ptr2 = (TouchState*)((byte*)currentStatePtr + this.touches[0].stateBlock.byteOffset);
			TouchState* ptr3 = (TouchState*)((byte*)currentStatePtr + this.primaryTouch.stateBlock.byteOffset);
			int count = this.touches.Count;
			TouchState touchState;
			if (ptr->stateSizeInBytes == 56U)
			{
				touchState = *(TouchState*)ptr->state;
			}
			else
			{
				touchState = default(TouchState);
				UnsafeUtility.MemCpy(UnsafeUtility.AddressOf<TouchState>(ref touchState), ptr->state, (long)((ulong)ptr->stateSizeInBytes));
			}
			touchState.tapCount = 0;
			touchState.isTapPress = false;
			touchState.isTapRelease = false;
			touchState.updateStepCount = InputUpdate.s_UpdateStepCount;
			if (touchState.phase != TouchPhase.Began)
			{
				int touchId = touchState.touchId;
				int i = 0;
				while (i < count)
				{
					if (ptr2[i].touchId == touchId)
					{
						bool isPrimaryTouch = ptr2[i].isPrimaryTouch;
						touchState.isPrimaryTouch = isPrimaryTouch;
						if (touchState.delta == default(Vector2))
						{
							touchState.delta = touchState.position - ptr2[i].position;
						}
						touchState.delta += ptr2[i].delta;
						touchState.startTime = ptr2[i].startTime;
						touchState.startPosition = ptr2[i].startPosition;
						bool flag = touchState.isNoneEndedOrCanceled && eventPtr.time - touchState.startTime <= (double)Touchscreen.s_TapTime && (touchState.position - touchState.startPosition).sqrMagnitude <= Touchscreen.s_TapRadiusSquared;
						if (flag)
						{
							touchState.tapCount = ptr2[i].tapCount + 1;
						}
						else
						{
							touchState.tapCount = ptr2[i].tapCount;
						}
						if (isPrimaryTouch)
						{
							if (touchState.isNoneEndedOrCanceled)
							{
								touchState.isPrimaryTouch = false;
								bool flag2 = false;
								for (int j = 0; j < count; j++)
								{
									if (j != i && ptr2[j].isInProgress)
									{
										flag2 = true;
										break;
									}
								}
								if (!flag2)
								{
									if (flag)
									{
										Touchscreen.TriggerTap(this.primaryTouch, ref touchState, eventPtr);
									}
									else
									{
										InputState.Change<TouchState>(this.primaryTouch, ref touchState, InputUpdateType.None, eventPtr);
									}
								}
								else
								{
									TouchState touchState2 = touchState;
									touchState2.phase = TouchPhase.Moved;
									touchState2.isOrphanedPrimaryTouch = true;
									InputState.Change<TouchState>(this.primaryTouch, ref touchState2, InputUpdateType.None, eventPtr);
								}
							}
							else
							{
								InputState.Change<TouchState>(this.primaryTouch, ref touchState, InputUpdateType.None, eventPtr);
							}
						}
						else if (touchState.isNoneEndedOrCanceled && ptr3->isOrphanedPrimaryTouch)
						{
							bool flag3 = false;
							for (int k = 0; k < count; k++)
							{
								if (k != i && ptr2[k].isInProgress)
								{
									flag3 = true;
									break;
								}
							}
							if (!flag3)
							{
								ptr3->isOrphanedPrimaryTouch = false;
								InputState.Change<byte>(this.primaryTouch.phase, 3, InputUpdateType.None, default(InputEventPtr));
							}
						}
						if (flag)
						{
							Touchscreen.TriggerTap(this.touches[i], ref touchState, eventPtr);
							return;
						}
						InputState.Change<TouchState>(this.touches[i], ref touchState, InputUpdateType.None, eventPtr);
						return;
					}
					else
					{
						i++;
					}
				}
				return;
			}
			int l = 0;
			while (l < count)
			{
				if (ptr2->isNoneEndedOrCanceled)
				{
					touchState.delta = Vector2.zero;
					touchState.startTime = eventPtr.time;
					touchState.startPosition = touchState.position;
					touchState.isPrimaryTouch = false;
					touchState.isOrphanedPrimaryTouch = false;
					touchState.isTap = false;
					touchState.tapCount = ptr2->tapCount;
					if (ptr3->isNoneEndedOrCanceled)
					{
						touchState.isPrimaryTouch = true;
						InputState.Change<TouchState>(this.primaryTouch, ref touchState, InputUpdateType.None, eventPtr);
					}
					InputState.Change<TouchState>(this.touches[l], ref touchState, InputUpdateType.None, eventPtr);
					return;
				}
				l++;
				ptr2++;
			}
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x0002D10A File Offset: 0x0002B30A
		void IInputStateCallbackReceiver.OnNextUpdate()
		{
			this.OnNextUpdate();
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x0002D112 File Offset: 0x0002B312
		void IInputStateCallbackReceiver.OnStateEvent(InputEventPtr eventPtr)
		{
			this.OnStateEvent(eventPtr);
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x0002D11C File Offset: 0x0002B31C
		unsafe bool IInputStateCallbackReceiver.GetStateOffsetForEvent(InputControl control, InputEventPtr eventPtr, ref uint offset)
		{
			if (!eventPtr.IsA<StateEvent>())
			{
				return false;
			}
			StateEvent* ptr = StateEvent.FromUnchecked(eventPtr);
			if (ptr->stateFormat != TouchState.Format)
			{
				return false;
			}
			if (control == null)
			{
				TouchState* ptr2 = (TouchState*)((byte*)base.currentStatePtr + this.touches[0].stateBlock.byteOffset);
				TouchState* state = (TouchState*)ptr->state;
				int touchId = state->touchId;
				TouchPhase phase = state->phase;
				int count = this.touches.Count;
				for (int i = 0; i < count; i++)
				{
					TouchState* ptr3 = ptr2 + i;
					if (ptr3->touchId == touchId || (!ptr3->isInProgress && phase.IsActive()))
					{
						offset = this.primaryTouch.m_StateBlock.byteOffset + this.primaryTouch.m_StateBlock.alignedSizeInBytes - this.m_StateBlock.byteOffset + (uint)(i * UnsafeUtility.SizeOf<TouchState>());
						return true;
					}
				}
				return false;
			}
			TouchControl touchControl = control.FindInParentChain<TouchControl>();
			if (touchControl == null || touchControl.parent != this)
			{
				return false;
			}
			if (touchControl != this.primaryTouch)
			{
				return false;
			}
			offset = touchControl.stateBlock.byteOffset - this.m_StateBlock.byteOffset;
			return true;
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x0002D25C File Offset: 0x0002B45C
		unsafe void ICustomDeviceReset.Reset()
		{
			void* currentStatePtr = base.currentStatePtr;
			using (NativeArray<byte> nativeArray = new NativeArray<byte>(StateEvent.GetEventSizeWithPayload<TouchState>(), Allocator.Temp, NativeArrayOptions.ClearMemory))
			{
				StateEvent* unsafePtr = (StateEvent*)nativeArray.GetUnsafePtr<byte>();
				unsafePtr->baseEvent = new InputEvent(1398030676, nativeArray.Length, base.deviceId, -1.0);
				TouchState* ptr = (TouchState*)((byte*)currentStatePtr + this.primaryTouch.stateBlock.byteOffset);
				if (ptr->phase.IsActive())
				{
					UnsafeUtility.MemCpy(unsafePtr->state, (void*)ptr, (long)UnsafeUtility.SizeOf<TouchState>());
					((TouchState*)unsafePtr->state)->phase = TouchPhase.Canceled;
					InputState.Change<TouchPhase>(this.primaryTouch.phase, TouchPhase.Canceled, InputUpdateType.None, new InputEventPtr((InputEvent*)unsafePtr));
				}
				TouchState* ptr2 = (TouchState*)((byte*)currentStatePtr + this.touches[0].stateBlock.byteOffset);
				int count = this.touches.Count;
				for (int i = 0; i < count; i++)
				{
					if (ptr2[i].phase.IsActive())
					{
						UnsafeUtility.MemCpy(unsafePtr->state, (void*)(ptr2 + i), (long)UnsafeUtility.SizeOf<TouchState>());
						((TouchState*)unsafePtr->state)->phase = TouchPhase.Canceled;
						InputState.Change<TouchPhase>(this.touches[i].phase, TouchPhase.Canceled, InputUpdateType.None, new InputEventPtr((InputEvent*)unsafePtr));
					}
				}
			}
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x0002D3E4 File Offset: 0x0002B5E4
		internal unsafe static bool MergeForward(InputEventPtr currentEventPtr, InputEventPtr nextEventPtr)
		{
			if (currentEventPtr.type != 1398030676 || nextEventPtr.type != 1398030676)
			{
				return false;
			}
			StateEvent* ptr = StateEvent.FromUnchecked(currentEventPtr);
			StateEvent* ptr2 = StateEvent.FromUnchecked(nextEventPtr);
			if (ptr->stateFormat != TouchState.Format || ptr2->stateFormat != TouchState.Format)
			{
				return false;
			}
			TouchState* state = (TouchState*)ptr->state;
			TouchState* state2 = (TouchState*)ptr2->state;
			if (state->touchId != state2->touchId || state->phaseId != state2->phaseId || state->flags != state2->flags)
			{
				return false;
			}
			state2->delta += state->delta;
			return true;
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x0002D4AE File Offset: 0x0002B6AE
		bool IEventMerger.MergeForward(InputEventPtr currentEventPtr, InputEventPtr nextEventPtr)
		{
			return Touchscreen.MergeForward(currentEventPtr, nextEventPtr);
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x0002D4B7 File Offset: 0x0002B6B7
		private static void TriggerTap(TouchControl control, ref TouchState state, InputEventPtr eventPtr)
		{
			state.isTapPress = true;
			state.isTapRelease = false;
			InputState.Change<TouchState>(control, ref state, InputUpdateType.None, eventPtr);
			state.isTapPress = false;
			state.isTapRelease = true;
			InputState.Change<TouchState>(control, ref state, InputUpdateType.None, eventPtr);
			state.isTapRelease = false;
		}

		// Token: 0x04000257 RID: 599
		internal static float s_TapTime;

		// Token: 0x04000258 RID: 600
		internal static float s_TapDelayTime;

		// Token: 0x04000259 RID: 601
		internal static float s_TapRadiusSquared;
	}
}
