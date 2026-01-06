using System;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000E2 RID: 226
	public struct InputEventPtr : IEquatable<InputEventPtr>
	{
		// Token: 0x06000D58 RID: 3416 RVA: 0x0004301A File Offset: 0x0004121A
		public unsafe InputEventPtr(InputEvent* eventPtr)
		{
			this.m_EventPtr = eventPtr;
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06000D59 RID: 3417 RVA: 0x00043023 File Offset: 0x00041223
		public bool valid
		{
			get
			{
				return this.m_EventPtr != null;
			}
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06000D5A RID: 3418 RVA: 0x00043032 File Offset: 0x00041232
		// (set) Token: 0x06000D5B RID: 3419 RVA: 0x00043049 File Offset: 0x00041249
		public unsafe bool handled
		{
			get
			{
				return this.valid && this.m_EventPtr->handled;
			}
			set
			{
				if (!this.valid)
				{
					throw new InvalidOperationException("The InputEventPtr is not valid.");
				}
				this.m_EventPtr->handled = value;
			}
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06000D5C RID: 3420 RVA: 0x0004306A File Offset: 0x0004126A
		// (set) Token: 0x06000D5D RID: 3421 RVA: 0x00043081 File Offset: 0x00041281
		public unsafe int id
		{
			get
			{
				if (!this.valid)
				{
					return 0;
				}
				return this.m_EventPtr->eventId;
			}
			set
			{
				if (!this.valid)
				{
					throw new InvalidOperationException("The InputEventPtr is not valid.");
				}
				this.m_EventPtr->eventId = value;
			}
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06000D5E RID: 3422 RVA: 0x000430A4 File Offset: 0x000412A4
		public unsafe FourCC type
		{
			get
			{
				if (!this.valid)
				{
					return default(FourCC);
				}
				return this.m_EventPtr->type;
			}
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06000D5F RID: 3423 RVA: 0x000430CE File Offset: 0x000412CE
		public unsafe uint sizeInBytes
		{
			get
			{
				if (!this.valid)
				{
					return 0U;
				}
				return this.m_EventPtr->sizeInBytes;
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06000D60 RID: 3424 RVA: 0x000430E5 File Offset: 0x000412E5
		// (set) Token: 0x06000D61 RID: 3425 RVA: 0x000430FC File Offset: 0x000412FC
		public unsafe int deviceId
		{
			get
			{
				if (!this.valid)
				{
					return 0;
				}
				return this.m_EventPtr->deviceId;
			}
			set
			{
				if (!this.valid)
				{
					throw new InvalidOperationException("The InputEventPtr is not valid.");
				}
				this.m_EventPtr->deviceId = value;
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06000D62 RID: 3426 RVA: 0x0004311D File Offset: 0x0004131D
		// (set) Token: 0x06000D63 RID: 3427 RVA: 0x0004313C File Offset: 0x0004133C
		public unsafe double time
		{
			get
			{
				if (!this.valid)
				{
					return 0.0;
				}
				return this.m_EventPtr->time;
			}
			set
			{
				if (!this.valid)
				{
					throw new InvalidOperationException("The InputEventPtr is not valid.");
				}
				this.m_EventPtr->time = value;
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06000D64 RID: 3428 RVA: 0x0004315D File Offset: 0x0004135D
		// (set) Token: 0x06000D65 RID: 3429 RVA: 0x0004317C File Offset: 0x0004137C
		internal unsafe double internalTime
		{
			get
			{
				if (!this.valid)
				{
					return 0.0;
				}
				return this.m_EventPtr->internalTime;
			}
			set
			{
				if (!this.valid)
				{
					throw new InvalidOperationException("The InputEventPtr is not valid.");
				}
				this.m_EventPtr->internalTime = value;
			}
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06000D66 RID: 3430 RVA: 0x0004319D File Offset: 0x0004139D
		public unsafe InputEvent* data
		{
			get
			{
				return this.m_EventPtr;
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06000D67 RID: 3431 RVA: 0x000431A8 File Offset: 0x000413A8
		internal unsafe FourCC stateFormat
		{
			get
			{
				FourCC type = this.type;
				if (type == 1398030676)
				{
					return StateEvent.FromUnchecked(this)->stateFormat;
				}
				if (type == 1145852993)
				{
					return DeltaStateEvent.FromUnchecked(this)->stateFormat;
				}
				string text = "Event must be a StateEvent or DeltaStateEvent but is ";
				InputEventPtr inputEventPtr = this;
				throw new InvalidOperationException(text + inputEventPtr.ToString());
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06000D68 RID: 3432 RVA: 0x00043228 File Offset: 0x00041428
		internal unsafe uint stateSizeInBytes
		{
			get
			{
				if (this.IsA<StateEvent>())
				{
					return StateEvent.From(this)->stateSizeInBytes;
				}
				if (this.IsA<DeltaStateEvent>())
				{
					return DeltaStateEvent.From(this)->deltaStateSizeInBytes;
				}
				string text = "Event must be a StateEvent or DeltaStateEvent but is ";
				InputEventPtr inputEventPtr = this;
				throw new InvalidOperationException(text + inputEventPtr.ToString());
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06000D69 RID: 3433 RVA: 0x0004328C File Offset: 0x0004148C
		internal unsafe uint stateOffset
		{
			get
			{
				if (this.IsA<DeltaStateEvent>())
				{
					return DeltaStateEvent.From(this)->stateOffset;
				}
				string text = "Event must be a DeltaStateEvent but is ";
				InputEventPtr inputEventPtr = this;
				throw new InvalidOperationException(text + inputEventPtr.ToString());
			}
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x000432D8 File Offset: 0x000414D8
		public unsafe bool IsA<TOtherEvent>() where TOtherEvent : struct, IInputEventTypeInfo
		{
			if (this.m_EventPtr == null)
			{
				return false;
			}
			TOtherEvent totherEvent = default(TOtherEvent);
			return this.m_EventPtr->type == totherEvent.typeStatic;
		}

		// Token: 0x06000D6B RID: 3435 RVA: 0x00043318 File Offset: 0x00041518
		public InputEventPtr Next()
		{
			if (!this.valid)
			{
				return default(InputEventPtr);
			}
			return new InputEventPtr(InputEvent.GetNextInMemory(this.m_EventPtr));
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x00043348 File Offset: 0x00041548
		public unsafe override string ToString()
		{
			if (!this.valid)
			{
				return "null";
			}
			InputEvent inputEvent = *this.m_EventPtr;
			return inputEvent.ToString();
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x0004337C File Offset: 0x0004157C
		public unsafe InputEvent* ToPointer()
		{
			return this;
		}

		// Token: 0x06000D6E RID: 3438 RVA: 0x00043389 File Offset: 0x00041589
		public bool Equals(InputEventPtr other)
		{
			return this.m_EventPtr == other.m_EventPtr || InputEvent.Equals(this.m_EventPtr, other.m_EventPtr);
		}

		// Token: 0x06000D6F RID: 3439 RVA: 0x000433AC File Offset: 0x000415AC
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj is InputEventPtr)
			{
				InputEventPtr inputEventPtr = (InputEventPtr)obj;
				return this.Equals(inputEventPtr);
			}
			return false;
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x000433D6 File Offset: 0x000415D6
		public override int GetHashCode()
		{
			return this.m_EventPtr;
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x000433E0 File Offset: 0x000415E0
		public static bool operator ==(InputEventPtr left, InputEventPtr right)
		{
			return left.m_EventPtr == right.m_EventPtr;
		}

		// Token: 0x06000D72 RID: 3442 RVA: 0x000433F0 File Offset: 0x000415F0
		public static bool operator !=(InputEventPtr left, InputEventPtr right)
		{
			return left.m_EventPtr != right.m_EventPtr;
		}

		// Token: 0x06000D73 RID: 3443 RVA: 0x00043403 File Offset: 0x00041603
		public unsafe static implicit operator InputEventPtr(InputEvent* eventPtr)
		{
			return new InputEventPtr(eventPtr);
		}

		// Token: 0x06000D74 RID: 3444 RVA: 0x0004340B File Offset: 0x0004160B
		public unsafe static InputEventPtr From(InputEvent* eventPtr)
		{
			return new InputEventPtr(eventPtr);
		}

		// Token: 0x06000D75 RID: 3445 RVA: 0x00043413 File Offset: 0x00041613
		public unsafe static implicit operator InputEvent*(InputEventPtr eventPtr)
		{
			return eventPtr.data;
		}

		// Token: 0x06000D76 RID: 3446 RVA: 0x0004341C File Offset: 0x0004161C
		public unsafe static InputEvent* FromInputEventPtr(InputEventPtr eventPtr)
		{
			return eventPtr.data;
		}

		// Token: 0x0400056E RID: 1390
		private unsafe readonly InputEvent* m_EventPtr;
	}
}
