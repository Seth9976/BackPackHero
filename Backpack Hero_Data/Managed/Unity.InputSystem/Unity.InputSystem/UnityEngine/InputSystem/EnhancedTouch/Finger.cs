using System;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.EnhancedTouch
{
	// Token: 0x02000097 RID: 151
	public class Finger
	{
		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000BC0 RID: 3008 RVA: 0x0003F0BB File Offset: 0x0003D2BB
		public Touchscreen screen { get; }

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000BC1 RID: 3009 RVA: 0x0003F0C3 File Offset: 0x0003D2C3
		public int index { get; }

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000BC2 RID: 3010 RVA: 0x0003F0CC File Offset: 0x0003D2CC
		public bool isActive
		{
			get
			{
				return this.currentTouch.valid;
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000BC3 RID: 3011 RVA: 0x0003F0E8 File Offset: 0x0003D2E8
		public Vector2 screenPosition
		{
			get
			{
				Touch lastTouch = this.lastTouch;
				if (!lastTouch.valid)
				{
					return default(Vector2);
				}
				return lastTouch.screenPosition;
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000BC4 RID: 3012 RVA: 0x0003F118 File Offset: 0x0003D318
		public Touch lastTouch
		{
			get
			{
				int count = this.m_StateHistory.Count;
				if (count == 0)
				{
					return default(Touch);
				}
				return new Touch(this, this.m_StateHistory[count - 1]);
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000BC5 RID: 3013 RVA: 0x0003F154 File Offset: 0x0003D354
		public Touch currentTouch
		{
			get
			{
				Touch lastTouch = this.lastTouch;
				if (!lastTouch.valid)
				{
					return default(Touch);
				}
				if (lastTouch.isInProgress)
				{
					return lastTouch;
				}
				if (lastTouch.updateStepCount == InputUpdate.s_UpdateStepCount)
				{
					return lastTouch;
				}
				return default(Touch);
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000BC6 RID: 3014 RVA: 0x0003F19F File Offset: 0x0003D39F
		public TouchHistory touchHistory
		{
			get
			{
				return new TouchHistory(this, this.m_StateHistory, -1, -1);
			}
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x0003F1B0 File Offset: 0x0003D3B0
		internal unsafe Finger(Touchscreen screen, int index, InputUpdateType updateMask)
		{
			this.screen = screen;
			this.index = index;
			this.m_StateHistory = new InputStateHistory<TouchState>(screen.touches[index])
			{
				historyDepth = Touch.maxHistoryLengthPerFinger,
				extraMemoryPerRecord = UnsafeUtility.SizeOf<Touch.ExtraDataPerTouchState>(),
				onRecordAdded = new Action<InputStateHistory.Record>(this.OnTouchRecorded),
				onShouldRecordStateChange = new Func<InputControl, double, InputEventPtr, bool>(Finger.ShouldRecordTouch),
				updateMask = updateMask
			};
			this.m_StateHistory.StartRecording();
			if (screen.touches[index].isInProgress)
			{
				this.m_StateHistory.RecordStateChange(screen.touches[index], *screen.touches[index].value, -1.0);
			}
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x0003F28C File Offset: 0x0003D48C
		private unsafe static bool ShouldRecordTouch(InputControl control, double time, InputEventPtr eventPtr)
		{
			if (!eventPtr.valid)
			{
				return false;
			}
			FourCC type = eventPtr.type;
			if (type != 1398030676 && type != 1145852993)
			{
				return false;
			}
			TouchState* ptr = (TouchState*)((byte*)control.currentStatePtr + control.stateBlock.byteOffset);
			return !ptr->isTapRelease;
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x0003F2F4 File Offset: 0x0003D4F4
		private unsafe void OnTouchRecorded(InputStateHistory.Record record)
		{
			int recordIndex = record.recordIndex;
			InputStateHistory.RecordHeader* recordUnchecked = this.m_StateHistory.GetRecordUnchecked(recordIndex);
			TouchState* statePtrWithoutControlIndex = (TouchState*)recordUnchecked->statePtrWithoutControlIndex;
			statePtrWithoutControlIndex->updateStepCount = InputUpdate.s_UpdateStepCount;
			Touch.s_GlobalState.playerState.haveBuiltActiveTouches = false;
			Touch.ExtraDataPerTouchState* ptr = (Touch.ExtraDataPerTouchState*)(recordUnchecked + this.m_StateHistory.bytesPerRecord / sizeof(InputStateHistory.RecordHeader) - UnsafeUtility.SizeOf<Touch.ExtraDataPerTouchState>() / sizeof(InputStateHistory.RecordHeader));
			ref Touch.ExtraDataPerTouchState ptr2 = ref *ptr;
			uint num = Touch.s_GlobalState.playerState.lastId + 1U;
			Touch.s_GlobalState.playerState.lastId = num;
			ptr2.uniqueId = num;
			ptr->accumulatedDelta = statePtrWithoutControlIndex->delta;
			if (statePtrWithoutControlIndex->phase != TouchPhase.Began)
			{
				if (recordIndex != this.m_StateHistory.m_HeadIndex)
				{
					int num2 = ((recordIndex == 0) ? (this.m_StateHistory.historyDepth - 1) : (recordIndex - 1));
					TouchState* statePtrWithoutControlIndex2 = (TouchState*)this.m_StateHistory.GetRecordUnchecked(num2)->statePtrWithoutControlIndex;
					statePtrWithoutControlIndex->delta -= statePtrWithoutControlIndex2->delta;
					statePtrWithoutControlIndex->beganInSameFrame = statePtrWithoutControlIndex2->beganInSameFrame && statePtrWithoutControlIndex2->updateStepCount == statePtrWithoutControlIndex->updateStepCount;
				}
			}
			else
			{
				statePtrWithoutControlIndex->beganInSameFrame = true;
			}
			switch (statePtrWithoutControlIndex->phase)
			{
			case TouchPhase.Began:
				DelegateHelpers.InvokeCallbacksSafe<Finger>(ref Touch.s_GlobalState.onFingerDown, this, "Touch.onFingerDown", null);
				return;
			case TouchPhase.Moved:
				DelegateHelpers.InvokeCallbacksSafe<Finger>(ref Touch.s_GlobalState.onFingerMove, this, "Touch.onFingerMove", null);
				return;
			case TouchPhase.Ended:
			case TouchPhase.Canceled:
				DelegateHelpers.InvokeCallbacksSafe<Finger>(ref Touch.s_GlobalState.onFingerUp, this, "Touch.onFingerUp", null);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x0003F464 File Offset: 0x0003D664
		private unsafe Touch FindTouch(uint uniqueId)
		{
			foreach (InputStateHistory<TouchState>.Record record in this.m_StateHistory)
			{
				if (((Touch.ExtraDataPerTouchState*)record.GetUnsafeExtraMemoryPtrUnchecked())->uniqueId == uniqueId)
				{
					return new Touch(this, record);
				}
			}
			return default(Touch);
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x0003F4D0 File Offset: 0x0003D6D0
		internal unsafe TouchHistory GetTouchHistory(Touch touch)
		{
			InputStateHistory<TouchState>.Record touchRecord = touch.m_TouchRecord;
			if (touchRecord.owner != this.m_StateHistory)
			{
				touch = this.FindTouch(touch.uniqueId);
				if (!touch.valid)
				{
					return default(TouchHistory);
				}
			}
			int touchId = touch.touchId;
			int num = touch.m_TouchRecord.index;
			int num2 = 0;
			if (touch.phase != TouchPhase.Began)
			{
				InputStateHistory<TouchState>.Record record = touch.m_TouchRecord.previous;
				while (record.valid)
				{
					TouchState* unsafeMemoryPtr = (TouchState*)record.GetUnsafeMemoryPtr();
					if (unsafeMemoryPtr->touchId != touchId)
					{
						break;
					}
					num2++;
					if (unsafeMemoryPtr->phase == TouchPhase.Began)
					{
						break;
					}
					record = record.previous;
				}
			}
			if (num2 == 0)
			{
				return default(TouchHistory);
			}
			num--;
			return new TouchHistory(this, this.m_StateHistory, num, num2);
		}

		// Token: 0x04000435 RID: 1077
		internal readonly InputStateHistory<TouchState> m_StateHistory;
	}
}
