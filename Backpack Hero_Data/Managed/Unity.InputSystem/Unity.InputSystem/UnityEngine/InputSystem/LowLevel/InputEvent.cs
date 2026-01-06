using System;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.InputSystem.Utilities;
using UnityEngineInternal.Input;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000DF RID: 223
	[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 20)]
	public struct InputEvent
	{
		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06000D34 RID: 3380 RVA: 0x00042835 File Offset: 0x00040A35
		// (set) Token: 0x06000D35 RID: 3381 RVA: 0x00042847 File Offset: 0x00040A47
		public FourCC type
		{
			get
			{
				return new FourCC((int)this.m_Event.type);
			}
			set
			{
				this.m_Event.type = (NativeInputEventType)value;
			}
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06000D36 RID: 3382 RVA: 0x0004285A File Offset: 0x00040A5A
		// (set) Token: 0x06000D37 RID: 3383 RVA: 0x00042868 File Offset: 0x00040A68
		public uint sizeInBytes
		{
			get
			{
				return (uint)this.m_Event.sizeInBytes;
			}
			set
			{
				if (value > 65535U)
				{
					throw new ArgumentException("Maximum event size is " + ushort.MaxValue.ToString(), "value");
				}
				this.m_Event.sizeInBytes = (ushort)value;
			}
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06000D38 RID: 3384 RVA: 0x000428AC File Offset: 0x00040AAC
		// (set) Token: 0x06000D39 RID: 3385 RVA: 0x000428C2 File Offset: 0x00040AC2
		public int eventId
		{
			get
			{
				return (int)((long)this.m_Event.eventId & 2147483647L);
			}
			set
			{
				this.m_Event.eventId = value | (int)((long)this.m_Event.eventId & (long)((ulong)int.MinValue));
			}
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06000D3A RID: 3386 RVA: 0x000428E5 File Offset: 0x00040AE5
		// (set) Token: 0x06000D3B RID: 3387 RVA: 0x000428F2 File Offset: 0x00040AF2
		public int deviceId
		{
			get
			{
				return (int)this.m_Event.deviceId;
			}
			set
			{
				this.m_Event.deviceId = (ushort)value;
			}
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06000D3C RID: 3388 RVA: 0x00042901 File Offset: 0x00040B01
		// (set) Token: 0x06000D3D RID: 3389 RVA: 0x00042914 File Offset: 0x00040B14
		public double time
		{
			get
			{
				return this.m_Event.time - InputRuntime.s_CurrentTimeOffsetToRealtimeSinceStartup;
			}
			set
			{
				this.m_Event.time = value + InputRuntime.s_CurrentTimeOffsetToRealtimeSinceStartup;
			}
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06000D3E RID: 3390 RVA: 0x00042928 File Offset: 0x00040B28
		// (set) Token: 0x06000D3F RID: 3391 RVA: 0x00042935 File Offset: 0x00040B35
		internal double internalTime
		{
			get
			{
				return this.m_Event.time;
			}
			set
			{
				this.m_Event.time = value;
			}
		}

		// Token: 0x06000D40 RID: 3392 RVA: 0x00042944 File Offset: 0x00040B44
		public InputEvent(FourCC type, int sizeInBytes, int deviceId, double time = -1.0)
		{
			if (time < 0.0)
			{
				time = InputRuntime.s_Instance.currentTime;
			}
			this.m_Event.type = (NativeInputEventType)type;
			this.m_Event.sizeInBytes = (ushort)sizeInBytes;
			this.m_Event.deviceId = (ushort)deviceId;
			this.m_Event.time = time;
			this.m_Event.eventId = 0;
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06000D41 RID: 3393 RVA: 0x000429AE File Offset: 0x00040BAE
		// (set) Token: 0x06000D42 RID: 3394 RVA: 0x000429CC File Offset: 0x00040BCC
		public bool handled
		{
			get
			{
				return ((long)this.m_Event.eventId & (long)((ulong)int.MinValue)) == (long)((ulong)int.MinValue);
			}
			set
			{
				if (value)
				{
					this.m_Event.eventId = (int)((long)this.m_Event.eventId | (long)((ulong)int.MinValue));
					return;
				}
				this.m_Event.eventId = (int)((long)this.m_Event.eventId & 2147483647L);
			}
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x00042A1C File Offset: 0x00040C1C
		public override string ToString()
		{
			return string.Format("id={0} type={1} device={2} size={3} time={4}", new object[] { this.eventId, this.type, this.deviceId, this.sizeInBytes, this.time });
		}

		// Token: 0x06000D44 RID: 3396 RVA: 0x00042A80 File Offset: 0x00040C80
		internal unsafe static InputEvent* GetNextInMemory(InputEvent* currentPtr)
		{
			uint num = currentPtr->sizeInBytes.AlignToMultipleOf(4U);
			return currentPtr + num / (uint)sizeof(InputEvent);
		}

		// Token: 0x06000D45 RID: 3397 RVA: 0x00042AA0 File Offset: 0x00040CA0
		internal unsafe static InputEvent* GetNextInMemoryChecked(InputEvent* currentPtr, ref InputEventBuffer buffer)
		{
			uint num = currentPtr->sizeInBytes.AlignToMultipleOf(4U);
			InputEvent* ptr = currentPtr + num / (uint)sizeof(InputEvent);
			if (!buffer.Contains(ptr))
			{
				throw new InvalidOperationException(string.Format("Event '{0}' is last event in given buffer with size {1}", new InputEventPtr(currentPtr), buffer.sizeInBytes));
			}
			return ptr;
		}

		// Token: 0x06000D46 RID: 3398 RVA: 0x00042AF0 File Offset: 0x00040CF0
		public unsafe static bool Equals(InputEvent* first, InputEvent* second)
		{
			return first == second || (first != null && second != null && first->m_Event.sizeInBytes == second->m_Event.sizeInBytes && UnsafeUtility.MemCmp((void*)first, (void*)second, (long)((ulong)first->m_Event.sizeInBytes)) == 0);
		}

		// Token: 0x04000562 RID: 1378
		private const uint kHandledMask = 2147483648U;

		// Token: 0x04000563 RID: 1379
		private const uint kIdMask = 2147483647U;

		// Token: 0x04000564 RID: 1380
		internal const int kBaseEventSize = 20;

		// Token: 0x04000565 RID: 1381
		public const int InvalidEventId = 0;

		// Token: 0x04000566 RID: 1382
		internal const int kAlignment = 4;

		// Token: 0x04000567 RID: 1383
		[FieldOffset(0)]
		private NativeInputEvent m_Event;
	}
}
