using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000E3 RID: 227
	internal struct InputEventStream
	{
		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06000D7A RID: 3450 RVA: 0x0004346D File Offset: 0x0004166D
		public bool isOpen
		{
			get
			{
				return this.m_IsOpen;
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06000D7B RID: 3451 RVA: 0x00043475 File Offset: 0x00041675
		public int remainingEventCount
		{
			get
			{
				return this.m_RemainingNativeEventCount + this.m_RemainingAppendEventCount;
			}
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06000D7C RID: 3452 RVA: 0x00043484 File Offset: 0x00041684
		public int numEventsRetainedInBuffer
		{
			get
			{
				return this.m_NumEventsRetainedInBuffer;
			}
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06000D7D RID: 3453 RVA: 0x0004348C File Offset: 0x0004168C
		public unsafe InputEvent* currentEventPtr
		{
			get
			{
				if (this.m_RemainingNativeEventCount > 0)
				{
					return this.m_CurrentNativeEventReadPtr;
				}
				if (this.m_RemainingAppendEventCount <= 0)
				{
					return null;
				}
				return this.m_CurrentAppendEventReadPtr;
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06000D7E RID: 3454 RVA: 0x000434B0 File Offset: 0x000416B0
		public unsafe uint numBytesRetainedInBuffer
		{
			get
			{
				return (uint)((long)((byte*)this.m_CurrentNativeEventWritePtr - (byte*)NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<byte>(this.m_NativeBuffer.data)));
			}
		}

		// Token: 0x06000D7F RID: 3455 RVA: 0x000434D0 File Offset: 0x000416D0
		public unsafe InputEventStream(ref InputEventBuffer eventBuffer, int maxAppendedEvents)
		{
			this.m_CurrentNativeEventWritePtr = (this.m_CurrentNativeEventReadPtr = (InputEvent*)NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<byte>(eventBuffer.data));
			this.m_NativeBuffer = eventBuffer;
			this.m_RemainingNativeEventCount = this.m_NativeBuffer.eventCount;
			this.m_NumEventsRetainedInBuffer = 0;
			this.m_CurrentAppendEventReadPtr = (this.m_CurrentAppendEventWritePtr = null);
			this.m_AppendBuffer = default(InputEventBuffer);
			this.m_RemainingAppendEventCount = 0;
			this.m_MaxAppendedEvents = maxAppendedEvents;
			this.m_IsOpen = true;
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x00043550 File Offset: 0x00041750
		public unsafe void Close(ref InputEventBuffer eventBuffer)
		{
			if (this.m_NumEventsRetainedInBuffer > 0)
			{
				void* unsafeBufferPointerWithoutChecks = NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<byte>(this.m_NativeBuffer.data);
				long num = (long)((byte*)this.m_CurrentNativeEventWritePtr - (byte*)unsafeBufferPointerWithoutChecks);
				this.m_NativeBuffer = new InputEventBuffer((InputEvent*)unsafeBufferPointerWithoutChecks, this.m_NumEventsRetainedInBuffer, (int)num, (int)this.m_NativeBuffer.capacityInBytes);
			}
			else
			{
				this.m_NativeBuffer.Reset();
			}
			if (this.m_AppendBuffer.data.IsCreated)
			{
				this.m_AppendBuffer.Dispose();
			}
			eventBuffer = this.m_NativeBuffer;
			this.m_IsOpen = false;
		}

		// Token: 0x06000D81 RID: 3457 RVA: 0x000435E4 File Offset: 0x000417E4
		public void CleanUpAfterException()
		{
			if (!this.isOpen)
			{
				return;
			}
			this.m_NativeBuffer.Reset();
			if (this.m_AppendBuffer.data.IsCreated)
			{
				this.m_AppendBuffer.Dispose();
			}
			this.m_IsOpen = false;
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x0004362C File Offset: 0x0004182C
		public unsafe void Write(InputEvent* eventPtr)
		{
			if (this.m_AppendBuffer.eventCount >= this.m_MaxAppendedEvents)
			{
				Debug.LogError("Maximum number of queued events exceeded. Set the 'maxQueuedEventsPerUpdate' setting to a higher value if you need to queue more events than this. " + string.Format("Current limit is '{0}'.", this.m_MaxAppendedEvents));
				return;
			}
			bool isCreated = this.m_AppendBuffer.data.IsCreated;
			byte* data = (byte*)this.m_AppendBuffer.bufferPtr.data;
			this.m_AppendBuffer.AppendEvent(eventPtr, 2048, Allocator.Temp);
			if (!isCreated)
			{
				this.m_CurrentAppendEventWritePtr = (this.m_CurrentAppendEventReadPtr = (InputEvent*)NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<byte>(this.m_AppendBuffer.data));
			}
			else
			{
				byte* data2 = (byte*)this.m_AppendBuffer.bufferPtr.data;
				if (data != data2)
				{
					long num = (long)((byte*)this.m_CurrentAppendEventWritePtr - (byte*)data);
					long num2 = (long)((byte*)this.m_CurrentAppendEventReadPtr - (byte*)data);
					this.m_CurrentAppendEventWritePtr = (InputEvent*)(data2 + num);
					this.m_CurrentAppendEventReadPtr = (InputEvent*)(data2 + num2);
				}
			}
			this.m_RemainingAppendEventCount++;
		}

		// Token: 0x06000D83 RID: 3459 RVA: 0x00043728 File Offset: 0x00041928
		public unsafe InputEvent* Advance(bool leaveEventInBuffer)
		{
			if (this.m_RemainingNativeEventCount > 0)
			{
				this.m_NativeBuffer.AdvanceToNextEvent(ref this.m_CurrentNativeEventReadPtr, ref this.m_CurrentNativeEventWritePtr, ref this.m_NumEventsRetainedInBuffer, ref this.m_RemainingNativeEventCount, leaveEventInBuffer);
			}
			else if (this.m_RemainingAppendEventCount > 0)
			{
				int num = 0;
				this.m_AppendBuffer.AdvanceToNextEvent(ref this.m_CurrentAppendEventReadPtr, ref this.m_CurrentAppendEventWritePtr, ref num, ref this.m_RemainingAppendEventCount, false);
			}
			return this.currentEventPtr;
		}

		// Token: 0x06000D84 RID: 3460 RVA: 0x00043798 File Offset: 0x00041998
		public unsafe InputEvent* Peek()
		{
			if (this.m_RemainingNativeEventCount > 1)
			{
				return InputEvent.GetNextInMemory(this.m_CurrentNativeEventReadPtr);
			}
			if (this.m_RemainingNativeEventCount == 1)
			{
				if (this.m_RemainingAppendEventCount <= 0)
				{
					return null;
				}
				return this.m_CurrentAppendEventReadPtr;
			}
			else
			{
				if (this.m_RemainingAppendEventCount > 1)
				{
					return InputEvent.GetNextInMemory(this.m_CurrentAppendEventReadPtr);
				}
				return null;
			}
		}

		// Token: 0x0400056F RID: 1391
		private InputEventBuffer m_NativeBuffer;

		// Token: 0x04000570 RID: 1392
		private unsafe InputEvent* m_CurrentNativeEventReadPtr;

		// Token: 0x04000571 RID: 1393
		private unsafe InputEvent* m_CurrentNativeEventWritePtr;

		// Token: 0x04000572 RID: 1394
		private int m_RemainingNativeEventCount;

		// Token: 0x04000573 RID: 1395
		private readonly int m_MaxAppendedEvents;

		// Token: 0x04000574 RID: 1396
		private InputEventBuffer m_AppendBuffer;

		// Token: 0x04000575 RID: 1397
		private unsafe InputEvent* m_CurrentAppendEventReadPtr;

		// Token: 0x04000576 RID: 1398
		private unsafe InputEvent* m_CurrentAppendEventWritePtr;

		// Token: 0x04000577 RID: 1399
		private int m_RemainingAppendEventCount;

		// Token: 0x04000578 RID: 1400
		private int m_NumEventsRetainedInBuffer;

		// Token: 0x04000579 RID: 1401
		private bool m_IsOpen;
	}
}
