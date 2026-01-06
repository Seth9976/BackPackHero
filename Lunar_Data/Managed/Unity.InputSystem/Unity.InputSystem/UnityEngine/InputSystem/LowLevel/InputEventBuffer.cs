using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000E0 RID: 224
	public struct InputEventBuffer : IEnumerable<InputEventPtr>, IEnumerable, IDisposable, ICloneable
	{
		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06000D44 RID: 3396 RVA: 0x00042AF7 File Offset: 0x00040CF7
		public int eventCount
		{
			get
			{
				return this.m_EventCount;
			}
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06000D45 RID: 3397 RVA: 0x00042AFF File Offset: 0x00040CFF
		public long sizeInBytes
		{
			get
			{
				return this.m_SizeInBytes;
			}
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06000D46 RID: 3398 RVA: 0x00042B07 File Offset: 0x00040D07
		public long capacityInBytes
		{
			get
			{
				if (!this.m_Buffer.IsCreated)
				{
					return 0L;
				}
				return (long)this.m_Buffer.Length;
			}
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06000D47 RID: 3399 RVA: 0x00042B25 File Offset: 0x00040D25
		public NativeArray<byte> data
		{
			get
			{
				return this.m_Buffer;
			}
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06000D48 RID: 3400 RVA: 0x00042B2D File Offset: 0x00040D2D
		public unsafe InputEventPtr bufferPtr
		{
			get
			{
				return (InputEvent*)NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<byte>(this.m_Buffer);
			}
		}

		// Token: 0x06000D49 RID: 3401 RVA: 0x00042B40 File Offset: 0x00040D40
		public unsafe InputEventBuffer(InputEvent* eventPtr, int eventCount, int sizeInBytes = -1, int capacityInBytes = -1)
		{
			this = default(InputEventBuffer);
			if (eventPtr == null && eventCount != 0)
			{
				throw new ArgumentException("eventPtr is NULL but eventCount is != 0", "eventCount");
			}
			if (capacityInBytes != 0 && capacityInBytes < sizeInBytes)
			{
				throw new ArgumentException(string.Format("capacity({0}) cannot be smaller than size({1})", capacityInBytes, sizeInBytes), "capacityInBytes");
			}
			if (eventPtr != null)
			{
				if (capacityInBytes < 0)
				{
					capacityInBytes = sizeInBytes;
				}
				this.m_Buffer = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<byte>((void*)eventPtr, (capacityInBytes > 0) ? capacityInBytes : 0, Allocator.None);
				this.m_SizeInBytes = ((sizeInBytes >= 0) ? ((long)sizeInBytes) : (-1L));
				this.m_EventCount = eventCount;
				this.m_WeOwnTheBuffer = false;
			}
		}

		// Token: 0x06000D4A RID: 3402 RVA: 0x00042BDC File Offset: 0x00040DDC
		public InputEventBuffer(NativeArray<byte> buffer, int eventCount, int sizeInBytes = -1, bool transferNativeArrayOwnership = false)
		{
			if (eventCount > 0 && !buffer.IsCreated)
			{
				throw new ArgumentException("buffer has no data but eventCount is > 0", "eventCount");
			}
			if (sizeInBytes > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("sizeInBytes");
			}
			this.m_Buffer = buffer;
			this.m_WeOwnTheBuffer = transferNativeArrayOwnership;
			this.m_SizeInBytes = (long)((sizeInBytes >= 0) ? sizeInBytes : buffer.Length);
			this.m_EventCount = eventCount;
		}

		// Token: 0x06000D4B RID: 3403 RVA: 0x00042C48 File Offset: 0x00040E48
		public unsafe void AppendEvent(InputEvent* eventPtr, int capacityIncrementInBytes = 2048, Allocator allocator = Allocator.Persistent)
		{
			if (eventPtr == null)
			{
				throw new ArgumentNullException("eventPtr");
			}
			uint sizeInBytes = eventPtr->sizeInBytes;
			UnsafeUtility.MemCpy((void*)this.AllocateEvent((int)sizeInBytes, capacityIncrementInBytes, allocator), (void*)eventPtr, (long)((ulong)sizeInBytes));
		}

		// Token: 0x06000D4C RID: 3404 RVA: 0x00042C80 File Offset: 0x00040E80
		public unsafe InputEvent* AllocateEvent(int sizeInBytes, int capacityIncrementInBytes = 2048, Allocator allocator = Allocator.Persistent)
		{
			if (sizeInBytes < 20)
			{
				throw new ArgumentException(string.Format("sizeInBytes must be >= sizeof(InputEvent) == {0} (was {1})", 20, sizeInBytes), "sizeInBytes");
			}
			int num = sizeInBytes.AlignToMultipleOf(4);
			long num2 = this.m_SizeInBytes + (long)num;
			if (this.capacityInBytes < num2)
			{
				long num3 = num2.AlignToMultipleOf((long)capacityIncrementInBytes);
				if (num3 > 2147483647L)
				{
					throw new NotImplementedException("NativeArray long support");
				}
				NativeArray<byte> nativeArray = new NativeArray<byte>((int)num3, allocator, NativeArrayOptions.ClearMemory);
				if (this.m_Buffer.IsCreated)
				{
					UnsafeUtility.MemCpy(nativeArray.GetUnsafePtr<byte>(), NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<byte>(this.m_Buffer), this.sizeInBytes);
					if (this.m_WeOwnTheBuffer)
					{
						this.m_Buffer.Dispose();
					}
				}
				this.m_Buffer = nativeArray;
				this.m_WeOwnTheBuffer = true;
			}
			InputEvent* ptr = (InputEvent*)((byte*)NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<byte>(this.m_Buffer) + this.m_SizeInBytes);
			ptr->sizeInBytes = (uint)sizeInBytes;
			this.m_SizeInBytes += (long)num;
			this.m_EventCount++;
			return ptr;
		}

		// Token: 0x06000D4D RID: 3405 RVA: 0x00042D7C File Offset: 0x00040F7C
		public unsafe bool Contains(InputEvent* eventPtr)
		{
			if (eventPtr == null)
			{
				return false;
			}
			if (this.sizeInBytes == 0L)
			{
				return false;
			}
			void* unsafeBufferPointerWithoutChecks = NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<byte>(this.data);
			return eventPtr >= (InputEvent*)unsafeBufferPointerWithoutChecks && (this.sizeInBytes == -1L || eventPtr < (InputEvent*)((byte*)unsafeBufferPointerWithoutChecks + this.sizeInBytes));
		}

		// Token: 0x06000D4E RID: 3406 RVA: 0x00042DC5 File Offset: 0x00040FC5
		public void Reset()
		{
			this.m_EventCount = 0;
			if (this.m_SizeInBytes != -1L)
			{
				this.m_SizeInBytes = 0L;
			}
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x00042DE0 File Offset: 0x00040FE0
		internal unsafe void AdvanceToNextEvent(ref InputEvent* currentReadPos, ref InputEvent* currentWritePos, ref int numEventsRetainedInBuffer, ref int numRemainingEvents, bool leaveEventInBuffer)
		{
			InputEvent* ptr = currentReadPos;
			if (numRemainingEvents > 1)
			{
				ptr = InputEvent.GetNextInMemory(currentReadPos);
			}
			if (leaveEventInBuffer)
			{
				uint sizeInBytes = currentReadPos.sizeInBytes;
				if (currentReadPos != currentWritePos)
				{
					UnsafeUtility.MemMove(currentWritePos, currentReadPos, (long)((ulong)sizeInBytes));
				}
				currentWritePos += (IntPtr)((UIntPtr)sizeInBytes.AlignToMultipleOf(4U));
				numEventsRetainedInBuffer++;
			}
			currentReadPos = ptr;
			numRemainingEvents--;
		}

		// Token: 0x06000D50 RID: 3408 RVA: 0x00042E39 File Offset: 0x00041039
		public IEnumerator<InputEventPtr> GetEnumerator()
		{
			return new InputEventBuffer.Enumerator(this);
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x00042E4B File Offset: 0x0004104B
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000D52 RID: 3410 RVA: 0x00042E53 File Offset: 0x00041053
		public void Dispose()
		{
			if (!this.m_WeOwnTheBuffer)
			{
				return;
			}
			this.m_Buffer.Dispose();
			this.m_WeOwnTheBuffer = false;
			this.m_SizeInBytes = 0L;
			this.m_EventCount = 0;
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x00042E80 File Offset: 0x00041080
		public InputEventBuffer Clone()
		{
			InputEventBuffer inputEventBuffer = default(InputEventBuffer);
			if (this.m_Buffer.IsCreated)
			{
				inputEventBuffer.m_Buffer = new NativeArray<byte>(this.m_Buffer.Length, Allocator.Persistent, NativeArrayOptions.ClearMemory);
				inputEventBuffer.m_Buffer.CopyFrom(this.m_Buffer);
				inputEventBuffer.m_WeOwnTheBuffer = true;
			}
			inputEventBuffer.m_SizeInBytes = this.m_SizeInBytes;
			inputEventBuffer.m_EventCount = this.m_EventCount;
			return inputEventBuffer;
		}

		// Token: 0x06000D54 RID: 3412 RVA: 0x00042EF0 File Offset: 0x000410F0
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x04000568 RID: 1384
		public const long BufferSizeUnknown = -1L;

		// Token: 0x04000569 RID: 1385
		private NativeArray<byte> m_Buffer;

		// Token: 0x0400056A RID: 1386
		private long m_SizeInBytes;

		// Token: 0x0400056B RID: 1387
		private int m_EventCount;

		// Token: 0x0400056C RID: 1388
		private bool m_WeOwnTheBuffer;

		// Token: 0x02000209 RID: 521
		private struct Enumerator : IEnumerator<InputEventPtr>, IEnumerator, IDisposable
		{
			// Token: 0x0600146F RID: 5231 RVA: 0x0005F198 File Offset: 0x0005D398
			public Enumerator(InputEventBuffer buffer)
			{
				this.m_Buffer = buffer.bufferPtr;
				this.m_EventCount = buffer.m_EventCount;
				this.m_CurrentEvent = null;
				this.m_CurrentIndex = 0;
			}

			// Token: 0x06001470 RID: 5232 RVA: 0x0005F1C8 File Offset: 0x0005D3C8
			public bool MoveNext()
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
				this.m_CurrentEvent = InputEvent.GetNextInMemory(this.m_CurrentEvent);
				return true;
			}

			// Token: 0x06001471 RID: 5233 RVA: 0x0005F239 File Offset: 0x0005D439
			public void Reset()
			{
				this.m_CurrentEvent = null;
				this.m_CurrentIndex = 0;
			}

			// Token: 0x06001472 RID: 5234 RVA: 0x0005F24A File Offset: 0x0005D44A
			public void Dispose()
			{
			}

			// Token: 0x17000580 RID: 1408
			// (get) Token: 0x06001473 RID: 5235 RVA: 0x0005F24C File Offset: 0x0005D44C
			public InputEventPtr Current
			{
				get
				{
					return this.m_CurrentEvent;
				}
			}

			// Token: 0x17000581 RID: 1409
			// (get) Token: 0x06001474 RID: 5236 RVA: 0x0005F259 File Offset: 0x0005D459
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x04000B24 RID: 2852
			private unsafe readonly InputEvent* m_Buffer;

			// Token: 0x04000B25 RID: 2853
			private readonly int m_EventCount;

			// Token: 0x04000B26 RID: 2854
			private unsafe InputEvent* m_CurrentEvent;

			// Token: 0x04000B27 RID: 2855
			private int m_CurrentIndex;
		}
	}
}
