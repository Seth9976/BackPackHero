using System;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000E2 RID: 226
	public struct InputEventPtr : IEquatable<InputEventPtr>
	{
		// Token: 0x06000D5B RID: 3419 RVA: 0x00043062 File Offset: 0x00041262
		public unsafe InputEventPtr(InputEvent* eventPtr)
		{
			this.m_EventPtr = eventPtr;
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06000D5C RID: 3420 RVA: 0x0004306B File Offset: 0x0004126B
		public bool valid
		{
			get
			{
				return this.m_EventPtr != null;
			}
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06000D5D RID: 3421 RVA: 0x0004307A File Offset: 0x0004127A
		// (set) Token: 0x06000D5E RID: 3422 RVA: 0x00043091 File Offset: 0x00041291
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

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06000D5F RID: 3423 RVA: 0x000430B2 File Offset: 0x000412B2
		// (set) Token: 0x06000D60 RID: 3424 RVA: 0x000430C9 File Offset: 0x000412C9
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

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06000D61 RID: 3425 RVA: 0x000430EC File Offset: 0x000412EC
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

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06000D62 RID: 3426 RVA: 0x00043116 File Offset: 0x00041316
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

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06000D63 RID: 3427 RVA: 0x0004312D File Offset: 0x0004132D
		// (set) Token: 0x06000D64 RID: 3428 RVA: 0x00043144 File Offset: 0x00041344
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

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06000D65 RID: 3429 RVA: 0x00043165 File Offset: 0x00041365
		// (set) Token: 0x06000D66 RID: 3430 RVA: 0x00043184 File Offset: 0x00041384
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

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06000D67 RID: 3431 RVA: 0x000431A5 File Offset: 0x000413A5
		// (set) Token: 0x06000D68 RID: 3432 RVA: 0x000431C4 File Offset: 0x000413C4
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

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06000D69 RID: 3433 RVA: 0x000431E5 File Offset: 0x000413E5
		public unsafe InputEvent* data
		{
			get
			{
				return this.m_EventPtr;
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06000D6A RID: 3434 RVA: 0x000431F0 File Offset: 0x000413F0
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

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06000D6B RID: 3435 RVA: 0x00043270 File Offset: 0x00041470
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

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06000D6C RID: 3436 RVA: 0x000432D4 File Offset: 0x000414D4
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

		// Token: 0x06000D6D RID: 3437 RVA: 0x00043320 File Offset: 0x00041520
		public unsafe bool IsA<TOtherEvent>() where TOtherEvent : struct, IInputEventTypeInfo
		{
			if (this.m_EventPtr == null)
			{
				return false;
			}
			TOtherEvent totherEvent = default(TOtherEvent);
			return this.m_EventPtr->type == totherEvent.typeStatic;
		}

		// Token: 0x06000D6E RID: 3438 RVA: 0x00043360 File Offset: 0x00041560
		public InputEventPtr Next()
		{
			if (!this.valid)
			{
				return default(InputEventPtr);
			}
			return new InputEventPtr(InputEvent.GetNextInMemory(this.m_EventPtr));
		}

		// Token: 0x06000D6F RID: 3439 RVA: 0x00043390 File Offset: 0x00041590
		public unsafe override string ToString()
		{
			if (!this.valid)
			{
				return "null";
			}
			InputEvent inputEvent = *this.m_EventPtr;
			return inputEvent.ToString();
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x000433C4 File Offset: 0x000415C4
		public unsafe InputEvent* ToPointer()
		{
			return this;
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x000433D1 File Offset: 0x000415D1
		public bool Equals(InputEventPtr other)
		{
			return this.m_EventPtr == other.m_EventPtr || InputEvent.Equals(this.m_EventPtr, other.m_EventPtr);
		}

		// Token: 0x06000D72 RID: 3442 RVA: 0x000433F4 File Offset: 0x000415F4
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

		// Token: 0x06000D73 RID: 3443 RVA: 0x0004341E File Offset: 0x0004161E
		public override int GetHashCode()
		{
			return this.m_EventPtr;
		}

		// Token: 0x06000D74 RID: 3444 RVA: 0x00043428 File Offset: 0x00041628
		public static bool operator ==(InputEventPtr left, InputEventPtr right)
		{
			return left.m_EventPtr == right.m_EventPtr;
		}

		// Token: 0x06000D75 RID: 3445 RVA: 0x00043438 File Offset: 0x00041638
		public static bool operator !=(InputEventPtr left, InputEventPtr right)
		{
			return left.m_EventPtr != right.m_EventPtr;
		}

		// Token: 0x06000D76 RID: 3446 RVA: 0x0004344B File Offset: 0x0004164B
		public unsafe static implicit operator InputEventPtr(InputEvent* eventPtr)
		{
			return new InputEventPtr(eventPtr);
		}

		// Token: 0x06000D77 RID: 3447 RVA: 0x00043453 File Offset: 0x00041653
		public unsafe static InputEventPtr From(InputEvent* eventPtr)
		{
			return new InputEventPtr(eventPtr);
		}

		// Token: 0x06000D78 RID: 3448 RVA: 0x0004345B File Offset: 0x0004165B
		public unsafe static implicit operator InputEvent*(InputEventPtr eventPtr)
		{
			return eventPtr.data;
		}

		// Token: 0x06000D79 RID: 3449 RVA: 0x00043464 File Offset: 0x00041664
		public unsafe static InputEvent* FromInputEventPtr(InputEventPtr eventPtr)
		{
			return eventPtr.data;
		}

		// Token: 0x0400056E RID: 1390
		private unsafe readonly InputEvent* m_EventPtr;
	}
}
