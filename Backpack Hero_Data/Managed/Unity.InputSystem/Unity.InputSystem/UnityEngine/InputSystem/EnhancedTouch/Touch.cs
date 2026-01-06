using System;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.EnhancedTouch
{
	// Token: 0x02000098 RID: 152
	public struct Touch : IEquatable<Touch>
	{
		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000BCC RID: 3020 RVA: 0x0003F59A File Offset: 0x0003D79A
		public bool valid
		{
			get
			{
				return this.m_TouchRecord.valid;
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000BCD RID: 3021 RVA: 0x0003F5A7 File Offset: 0x0003D7A7
		public Finger finger
		{
			get
			{
				return this.m_Finger;
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000BCE RID: 3022 RVA: 0x0003F5AF File Offset: 0x0003D7AF
		public TouchPhase phase
		{
			get
			{
				return this.state.phase;
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000BCF RID: 3023 RVA: 0x0003F5BC File Offset: 0x0003D7BC
		public bool began
		{
			get
			{
				return this.phase == TouchPhase.Began;
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000BD0 RID: 3024 RVA: 0x0003F5C7 File Offset: 0x0003D7C7
		public bool inProgress
		{
			get
			{
				return this.phase == TouchPhase.Moved || this.phase == TouchPhase.Stationary || this.phase == TouchPhase.Began;
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000BD1 RID: 3025 RVA: 0x0003F5E6 File Offset: 0x0003D7E6
		public bool ended
		{
			get
			{
				return this.phase == TouchPhase.Ended || this.phase == TouchPhase.Canceled;
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000BD2 RID: 3026 RVA: 0x0003F5FC File Offset: 0x0003D7FC
		public int touchId
		{
			get
			{
				return this.state.touchId;
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000BD3 RID: 3027 RVA: 0x0003F609 File Offset: 0x0003D809
		public float pressure
		{
			get
			{
				return this.state.pressure;
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000BD4 RID: 3028 RVA: 0x0003F616 File Offset: 0x0003D816
		public Vector2 radius
		{
			get
			{
				return this.state.radius;
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000BD5 RID: 3029 RVA: 0x0003F623 File Offset: 0x0003D823
		public double startTime
		{
			get
			{
				return this.state.startTime;
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000BD6 RID: 3030 RVA: 0x0003F630 File Offset: 0x0003D830
		public double time
		{
			get
			{
				return this.m_TouchRecord.time;
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000BD7 RID: 3031 RVA: 0x0003F63D File Offset: 0x0003D83D
		public Touchscreen screen
		{
			get
			{
				return this.finger.screen;
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000BD8 RID: 3032 RVA: 0x0003F64A File Offset: 0x0003D84A
		public Vector2 screenPosition
		{
			get
			{
				return this.state.position;
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000BD9 RID: 3033 RVA: 0x0003F657 File Offset: 0x0003D857
		public Vector2 startScreenPosition
		{
			get
			{
				return this.state.startPosition;
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000BDA RID: 3034 RVA: 0x0003F664 File Offset: 0x0003D864
		public Vector2 delta
		{
			get
			{
				return this.state.delta;
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000BDB RID: 3035 RVA: 0x0003F671 File Offset: 0x0003D871
		public int tapCount
		{
			get
			{
				return (int)this.state.tapCount;
			}
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000BDC RID: 3036 RVA: 0x0003F67E File Offset: 0x0003D87E
		public bool isTap
		{
			get
			{
				return this.state.isTap;
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000BDD RID: 3037 RVA: 0x0003F68B File Offset: 0x0003D88B
		public int displayIndex
		{
			get
			{
				return (int)this.state.displayIndex;
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000BDE RID: 3038 RVA: 0x0003F698 File Offset: 0x0003D898
		public bool isInProgress
		{
			get
			{
				TouchPhase phase = this.phase;
				return phase - TouchPhase.Began <= 1 || phase == TouchPhase.Stationary;
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000BDF RID: 3039 RVA: 0x0003F6B9 File Offset: 0x0003D8B9
		internal uint updateStepCount
		{
			get
			{
				return this.state.updateStepCount;
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000BE0 RID: 3040 RVA: 0x0003F6C6 File Offset: 0x0003D8C6
		internal uint uniqueId
		{
			get
			{
				return this.extraData.uniqueId;
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000BE1 RID: 3041 RVA: 0x0003F6D3 File Offset: 0x0003D8D3
		private unsafe ref TouchState state
		{
			get
			{
				return ref *(TouchState*)this.m_TouchRecord.GetUnsafeMemoryPtr();
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000BE2 RID: 3042 RVA: 0x0003F6E0 File Offset: 0x0003D8E0
		private unsafe ref Touch.ExtraDataPerTouchState extraData
		{
			get
			{
				return ref *(Touch.ExtraDataPerTouchState*)this.m_TouchRecord.GetUnsafeExtraMemoryPtr();
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000BE3 RID: 3043 RVA: 0x0003F6ED File Offset: 0x0003D8ED
		public TouchHistory history
		{
			get
			{
				if (!this.valid)
				{
					throw new InvalidOperationException("Touch is invalid");
				}
				return this.finger.GetTouchHistory(this);
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000BE4 RID: 3044 RVA: 0x0003F713 File Offset: 0x0003D913
		public static ReadOnlyArray<Touch> activeTouches
		{
			get
			{
				Touch.s_GlobalState.playerState.UpdateActiveTouches();
				return new ReadOnlyArray<Touch>(Touch.s_GlobalState.playerState.activeTouches, 0, Touch.s_GlobalState.playerState.activeTouchCount);
			}
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000BE5 RID: 3045 RVA: 0x0003F748 File Offset: 0x0003D948
		public static ReadOnlyArray<Finger> fingers
		{
			get
			{
				return new ReadOnlyArray<Finger>(Touch.s_GlobalState.playerState.fingers, 0, Touch.s_GlobalState.playerState.totalFingerCount);
			}
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000BE6 RID: 3046 RVA: 0x0003F76E File Offset: 0x0003D96E
		public static ReadOnlyArray<Finger> activeFingers
		{
			get
			{
				Touch.s_GlobalState.playerState.UpdateActiveFingers();
				return new ReadOnlyArray<Finger>(Touch.s_GlobalState.playerState.activeFingers, 0, Touch.s_GlobalState.playerState.activeFingerCount);
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000BE7 RID: 3047 RVA: 0x0003F7A3 File Offset: 0x0003D9A3
		public static IEnumerable<Touchscreen> screens
		{
			get
			{
				return Touch.s_GlobalState.touchscreens;
			}
		}

		// Token: 0x14000021 RID: 33
		// (add) Token: 0x06000BE8 RID: 3048 RVA: 0x0003F7B4 File Offset: 0x0003D9B4
		// (remove) Token: 0x06000BE9 RID: 3049 RVA: 0x0003F7D4 File Offset: 0x0003D9D4
		public static event Action<Finger> onFingerDown
		{
			add
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				Touch.s_GlobalState.onFingerDown.AddCallback(value);
			}
			remove
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				Touch.s_GlobalState.onFingerDown.RemoveCallback(value);
			}
		}

		// Token: 0x14000022 RID: 34
		// (add) Token: 0x06000BEA RID: 3050 RVA: 0x0003F7F4 File Offset: 0x0003D9F4
		// (remove) Token: 0x06000BEB RID: 3051 RVA: 0x0003F814 File Offset: 0x0003DA14
		public static event Action<Finger> onFingerUp
		{
			add
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				Touch.s_GlobalState.onFingerUp.AddCallback(value);
			}
			remove
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				Touch.s_GlobalState.onFingerUp.RemoveCallback(value);
			}
		}

		// Token: 0x14000023 RID: 35
		// (add) Token: 0x06000BEC RID: 3052 RVA: 0x0003F834 File Offset: 0x0003DA34
		// (remove) Token: 0x06000BED RID: 3053 RVA: 0x0003F854 File Offset: 0x0003DA54
		public static event Action<Finger> onFingerMove
		{
			add
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				Touch.s_GlobalState.onFingerMove.AddCallback(value);
			}
			remove
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				Touch.s_GlobalState.onFingerMove.RemoveCallback(value);
			}
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000BEE RID: 3054 RVA: 0x0003F874 File Offset: 0x0003DA74
		public static int maxHistoryLengthPerFinger
		{
			get
			{
				return Touch.s_GlobalState.historyLengthPerFinger;
			}
		}

		// Token: 0x06000BEF RID: 3055 RVA: 0x0003F880 File Offset: 0x0003DA80
		internal Touch(Finger finger, InputStateHistory<TouchState>.Record touchRecord)
		{
			this.m_Finger = finger;
			this.m_TouchRecord = touchRecord;
		}

		// Token: 0x06000BF0 RID: 3056 RVA: 0x0003F890 File Offset: 0x0003DA90
		public override string ToString()
		{
			if (!this.valid)
			{
				return "<None>";
			}
			return string.Format("{{id={0} finger={1} phase={2} position={3} delta={4} time={5}}}", new object[]
			{
				this.touchId,
				this.finger.index,
				this.phase,
				this.screenPosition,
				this.delta,
				this.time
			});
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x0003F914 File Offset: 0x0003DB14
		public bool Equals(Touch other)
		{
			return object.Equals(this.m_Finger, other.m_Finger) && this.m_TouchRecord.Equals(other.m_TouchRecord);
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x0003F93C File Offset: 0x0003DB3C
		public override bool Equals(object obj)
		{
			if (obj is Touch)
			{
				Touch touch = (Touch)obj;
				return this.Equals(touch);
			}
			return false;
		}

		// Token: 0x06000BF3 RID: 3059 RVA: 0x0003F961 File Offset: 0x0003DB61
		public override int GetHashCode()
		{
			return (((this.m_Finger != null) ? this.m_Finger.GetHashCode() : 0) * 397) ^ this.m_TouchRecord.GetHashCode();
		}

		// Token: 0x06000BF4 RID: 3060 RVA: 0x0003F991 File Offset: 0x0003DB91
		internal static void AddTouchscreen(Touchscreen screen)
		{
			Touch.s_GlobalState.touchscreens.AppendWithCapacity(screen, 5);
			Touch.s_GlobalState.playerState.AddFingers(screen);
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x0003F9B8 File Offset: 0x0003DBB8
		internal static void RemoveTouchscreen(Touchscreen screen)
		{
			int num = Touch.s_GlobalState.touchscreens.IndexOfReference(screen);
			Touch.s_GlobalState.touchscreens.RemoveAtWithCapacity(num);
			Touch.s_GlobalState.playerState.RemoveFingers(screen);
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x0003F9F6 File Offset: 0x0003DBF6
		internal static void BeginUpdate()
		{
			if (Touch.s_GlobalState.playerState.haveActiveTouchesNeedingRefreshNextUpdate)
			{
				Touch.s_GlobalState.playerState.haveBuiltActiveTouches = false;
			}
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x0003FA1C File Offset: 0x0003DC1C
		private static Touch.GlobalState CreateGlobalState()
		{
			return new Touch.GlobalState
			{
				historyLengthPerFinger = 64
			};
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x0003FA3C File Offset: 0x0003DC3C
		internal static ISavedState SaveAndResetState()
		{
			ISavedState savedState = new SavedStructState<Touch.GlobalState>(ref Touch.s_GlobalState, delegate(ref Touch.GlobalState state)
			{
				Touch.s_GlobalState = state;
			}, delegate
			{
			});
			Touch.s_GlobalState = Touch.CreateGlobalState();
			return savedState;
		}

		// Token: 0x04000436 RID: 1078
		private readonly Finger m_Finger;

		// Token: 0x04000437 RID: 1079
		internal InputStateHistory<TouchState>.Record m_TouchRecord;

		// Token: 0x04000438 RID: 1080
		internal static Touch.GlobalState s_GlobalState = Touch.CreateGlobalState();

		// Token: 0x020001EB RID: 491
		internal struct GlobalState
		{
			// Token: 0x04000ABD RID: 2749
			internal InlinedArray<Touchscreen> touchscreens;

			// Token: 0x04000ABE RID: 2750
			internal int historyLengthPerFinger;

			// Token: 0x04000ABF RID: 2751
			internal CallbackArray<Action<Finger>> onFingerDown;

			// Token: 0x04000AC0 RID: 2752
			internal CallbackArray<Action<Finger>> onFingerMove;

			// Token: 0x04000AC1 RID: 2753
			internal CallbackArray<Action<Finger>> onFingerUp;

			// Token: 0x04000AC2 RID: 2754
			internal Touch.FingerAndTouchState playerState;
		}

		// Token: 0x020001EC RID: 492
		internal struct FingerAndTouchState
		{
			// Token: 0x0600145A RID: 5210 RVA: 0x0005EB4C File Offset: 0x0005CD4C
			public void AddFingers(Touchscreen screen)
			{
				int count = screen.touches.Count;
				ArrayHelpers.EnsureCapacity<Finger>(ref this.fingers, this.totalFingerCount, count, 10);
				for (int i = 0; i < count; i++)
				{
					Finger finger = new Finger(screen, i, this.updateMask);
					ArrayHelpers.AppendWithCapacity<Finger>(ref this.fingers, ref this.totalFingerCount, finger, 10);
				}
			}

			// Token: 0x0600145B RID: 5211 RVA: 0x0005EBAC File Offset: 0x0005CDAC
			public void RemoveFingers(Touchscreen screen)
			{
				int count = screen.touches.Count;
				for (int i = 0; i < this.fingers.Length; i++)
				{
					if (this.fingers[i].screen == screen)
					{
						for (int j = 0; j < count; j++)
						{
							this.fingers[i + j].m_StateHistory.Dispose();
						}
						ArrayHelpers.EraseSliceWithCapacity<Finger>(ref this.fingers, ref this.totalFingerCount, i, count);
						break;
					}
				}
				this.haveBuiltActiveTouches = false;
			}

			// Token: 0x0600145C RID: 5212 RVA: 0x0005EC28 File Offset: 0x0005CE28
			public void Destroy()
			{
				for (int i = 0; i < this.totalFingerCount; i++)
				{
					this.fingers[i].m_StateHistory.Dispose();
				}
				InputStateHistory<TouchState> inputStateHistory = this.activeTouchState;
				if (inputStateHistory != null)
				{
					inputStateHistory.Dispose();
				}
				this.activeTouchState = null;
			}

			// Token: 0x0600145D RID: 5213 RVA: 0x0005EC70 File Offset: 0x0005CE70
			public void UpdateActiveFingers()
			{
				this.activeFingerCount = 0;
				for (int i = 0; i < this.totalFingerCount; i++)
				{
					Finger finger = this.fingers[i];
					if (finger.currentTouch.valid)
					{
						ArrayHelpers.AppendWithCapacity<Finger>(ref this.activeFingers, ref this.activeFingerCount, finger, 10);
					}
				}
			}

			// Token: 0x0600145E RID: 5214 RVA: 0x0005ECC4 File Offset: 0x0005CEC4
			public unsafe void UpdateActiveTouches()
			{
				if (this.haveBuiltActiveTouches)
				{
					return;
				}
				if (this.activeTouchState == null)
				{
					this.activeTouchState = new InputStateHistory<TouchState>(null)
					{
						extraMemoryPerRecord = UnsafeUtility.SizeOf<Touch.ExtraDataPerTouchState>()
					};
				}
				else
				{
					this.activeTouchState.Clear();
					this.activeTouchState.m_ControlCount = 0;
					this.activeTouchState.m_Controls.Clear<InputControl>();
				}
				this.activeTouchCount = 0;
				this.haveActiveTouchesNeedingRefreshNextUpdate = false;
				uint s_UpdateStepCount = InputUpdate.s_UpdateStepCount;
				for (int i = 0; i < this.totalFingerCount; i++)
				{
					ref Finger ptr = ref this.fingers[i];
					InputStateHistory<TouchState> stateHistory = ptr.m_StateHistory;
					int count = stateHistory.Count;
					if (count != 0)
					{
						int num = this.activeTouchCount;
						int num2 = 0;
						TouchState* ptr2 = default(TouchState*);
						int num3 = stateHistory.UserIndexToRecordIndex(count - 1);
						InputStateHistory.RecordHeader* ptr3 = stateHistory.GetRecordUnchecked(num3);
						int bytesPerRecord = stateHistory.bytesPerRecord;
						int num4 = bytesPerRecord - stateHistory.extraMemoryPerRecord;
						for (int j = 0; j < count; j++)
						{
							if (j != 0)
							{
								num3--;
								if (num3 < 0)
								{
									num3 = stateHistory.historyDepth - 1;
									ptr3 = stateHistory.GetRecordUnchecked(num3);
								}
								else
								{
									ptr3 -= bytesPerRecord / sizeof(InputStateHistory.RecordHeader);
								}
							}
							TouchState* statePtrWithoutControlIndex = (TouchState*)ptr3->statePtrWithoutControlIndex;
							bool flag = statePtrWithoutControlIndex->updateStepCount == s_UpdateStepCount;
							if (statePtrWithoutControlIndex->touchId == num2 && !statePtrWithoutControlIndex->phase.IsEndedOrCanceled())
							{
								if (flag && statePtrWithoutControlIndex->phase == TouchPhase.Began)
								{
									ptr2->phase = TouchPhase.Began;
									ptr2->position = statePtrWithoutControlIndex->position;
									ptr2->delta = default(Vector2);
									this.haveActiveTouchesNeedingRefreshNextUpdate = true;
								}
							}
							else
							{
								if (statePtrWithoutControlIndex->phase.IsEndedOrCanceled() && (!statePtrWithoutControlIndex->beganInSameFrame || statePtrWithoutControlIndex->updateStepCount != s_UpdateStepCount - 1U) && !flag)
								{
									break;
								}
								Touch.ExtraDataPerTouchState* ptr4 = (Touch.ExtraDataPerTouchState*)(ptr3 + num4 / sizeof(InputStateHistory.RecordHeader));
								int num5;
								InputStateHistory.RecordHeader* ptr5 = this.activeTouchState.AllocateRecord(out num5);
								TouchState* statePtrWithControlIndex = (TouchState*)ptr5->statePtrWithControlIndex;
								Touch.ExtraDataPerTouchState* ptr6 = (Touch.ExtraDataPerTouchState*)(ptr5 + this.activeTouchState.bytesPerRecord / sizeof(InputStateHistory.RecordHeader) - UnsafeUtility.SizeOf<Touch.ExtraDataPerTouchState>() / sizeof(InputStateHistory.RecordHeader));
								ptr5->time = ptr3->time;
								ptr5->controlIndex = ArrayHelpers.AppendWithCapacity<InputControl>(ref this.activeTouchState.m_Controls, ref this.activeTouchState.m_ControlCount, ptr.m_StateHistory.controls[0], 10);
								UnsafeUtility.MemCpy((void*)statePtrWithControlIndex, (void*)statePtrWithoutControlIndex, (long)UnsafeUtility.SizeOf<TouchState>());
								UnsafeUtility.MemCpy((void*)ptr6, (void*)ptr4, (long)UnsafeUtility.SizeOf<Touch.ExtraDataPerTouchState>());
								TouchPhase phase = statePtrWithoutControlIndex->phase;
								if ((phase == TouchPhase.Moved || phase == TouchPhase.Began) && !flag && (phase != TouchPhase.Moved || !statePtrWithoutControlIndex->beganInSameFrame || statePtrWithoutControlIndex->updateStepCount != s_UpdateStepCount - 1U))
								{
									statePtrWithControlIndex->phase = TouchPhase.Stationary;
									statePtrWithControlIndex->delta = default(Vector2);
								}
								else if (!flag && !statePtrWithoutControlIndex->beganInSameFrame)
								{
									statePtrWithControlIndex->delta = default(Vector2);
								}
								else
								{
									statePtrWithControlIndex->delta = ptr6->accumulatedDelta;
								}
								InputStateHistory<TouchState>.Record record = new InputStateHistory<TouchState>.Record(this.activeTouchState, num5, ptr5);
								Touch touch = new Touch(ptr, record);
								ArrayHelpers.InsertAtWithCapacity<Touch>(ref this.activeTouches, ref this.activeTouchCount, num, touch, 10);
								num2 = statePtrWithoutControlIndex->touchId;
								ptr2 = statePtrWithControlIndex;
								if (touch.phase != TouchPhase.Stationary)
								{
									this.haveActiveTouchesNeedingRefreshNextUpdate = true;
								}
							}
						}
					}
				}
				this.haveBuiltActiveTouches = true;
			}

			// Token: 0x04000AC3 RID: 2755
			public InputUpdateType updateMask;

			// Token: 0x04000AC4 RID: 2756
			public Finger[] fingers;

			// Token: 0x04000AC5 RID: 2757
			public Finger[] activeFingers;

			// Token: 0x04000AC6 RID: 2758
			public Touch[] activeTouches;

			// Token: 0x04000AC7 RID: 2759
			public int activeFingerCount;

			// Token: 0x04000AC8 RID: 2760
			public int activeTouchCount;

			// Token: 0x04000AC9 RID: 2761
			public int totalFingerCount;

			// Token: 0x04000ACA RID: 2762
			public uint lastId;

			// Token: 0x04000ACB RID: 2763
			public bool haveBuiltActiveTouches;

			// Token: 0x04000ACC RID: 2764
			public bool haveActiveTouchesNeedingRefreshNextUpdate;

			// Token: 0x04000ACD RID: 2765
			public InputStateHistory<TouchState> activeTouchState;
		}

		// Token: 0x020001ED RID: 493
		internal struct ExtraDataPerTouchState
		{
			// Token: 0x04000ACE RID: 2766
			public Vector2 accumulatedDelta;

			// Token: 0x04000ACF RID: 2767
			public uint uniqueId;
		}
	}
}
