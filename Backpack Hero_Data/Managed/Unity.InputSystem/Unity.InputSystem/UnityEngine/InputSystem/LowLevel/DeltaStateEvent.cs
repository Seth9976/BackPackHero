using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000D8 RID: 216
	[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 29)]
	public struct DeltaStateEvent : IInputEventTypeInfo
	{
		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06000D1C RID: 3356 RVA: 0x00042465 File Offset: 0x00040665
		public uint deltaStateSizeInBytes
		{
			get
			{
				return this.baseEvent.sizeInBytes - 28U;
			}
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06000D1D RID: 3357 RVA: 0x00042478 File Offset: 0x00040678
		public unsafe void* deltaState
		{
			get
			{
				fixed (byte* ptr = &this.stateData.FixedElementField)
				{
					return (void*)ptr;
				}
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06000D1E RID: 3358 RVA: 0x00042493 File Offset: 0x00040693
		public FourCC typeStatic
		{
			get
			{
				return 1145852993;
			}
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x000424A0 File Offset: 0x000406A0
		public unsafe InputEventPtr ToEventPtr()
		{
			fixed (DeltaStateEvent* ptr = &this)
			{
				return new InputEventPtr((InputEvent*)ptr);
			}
		}

		// Token: 0x06000D20 RID: 3360 RVA: 0x000424B8 File Offset: 0x000406B8
		public unsafe static DeltaStateEvent* From(InputEventPtr ptr)
		{
			if (!ptr.valid)
			{
				throw new ArgumentNullException("ptr");
			}
			if (!ptr.IsA<DeltaStateEvent>())
			{
				throw new InvalidCastException(string.Format("Cannot cast event with type '{0}' into DeltaStateEvent", ptr.type));
			}
			return DeltaStateEvent.FromUnchecked(ptr);
		}

		// Token: 0x06000D21 RID: 3361 RVA: 0x00042504 File Offset: 0x00040704
		internal unsafe static DeltaStateEvent* FromUnchecked(InputEventPtr ptr)
		{
			return (DeltaStateEvent*)ptr.data;
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x00042510 File Offset: 0x00040710
		public unsafe static NativeArray<byte> From(InputControl control, out InputEventPtr eventPtr, Allocator allocator = Allocator.Temp)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			InputDevice device = control.device;
			if (!device.added)
			{
				throw new ArgumentException(string.Format("Device for control '{0}' has not been added to system", control), "control");
			}
			ref InputStateBlock ptr = ref device.m_StateBlock;
			ref InputStateBlock ptr2 = ref control.m_StateBlock;
			FourCC format = ptr.format;
			uint num;
			if (ptr2.bitOffset != 0U)
			{
				num = (ptr2.bitOffset + ptr2.sizeInBits + 7U) / 8U;
			}
			else
			{
				num = ptr2.alignedSizeInBytes;
			}
			uint byteOffset = ptr2.byteOffset;
			byte* ptr3 = (byte*)control.currentStatePtr + byteOffset;
			uint num2 = 28U + num;
			NativeArray<byte> nativeArray = new NativeArray<byte>((int)num2.AlignToMultipleOf(4U), allocator, NativeArrayOptions.ClearMemory);
			DeltaStateEvent* unsafePtr = (DeltaStateEvent*)nativeArray.GetUnsafePtr<byte>();
			unsafePtr->baseEvent = new InputEvent(1145852993, (int)num2, device.deviceId, InputRuntime.s_Instance.currentTime);
			unsafePtr->stateFormat = format;
			unsafePtr->stateOffset = ptr2.byteOffset - ptr.byteOffset;
			UnsafeUtility.MemCpy(unsafePtr->deltaState, (void*)ptr3, (long)((ulong)num));
			eventPtr = unsafePtr->ToEventPtr();
			return nativeArray;
		}

		// Token: 0x04000550 RID: 1360
		public const int Type = 1145852993;

		// Token: 0x04000551 RID: 1361
		[FieldOffset(0)]
		public InputEvent baseEvent;

		// Token: 0x04000552 RID: 1362
		[FieldOffset(20)]
		public FourCC stateFormat;

		// Token: 0x04000553 RID: 1363
		[FieldOffset(24)]
		public uint stateOffset;

		// Token: 0x04000554 RID: 1364
		[FixedBuffer(typeof(byte), 1)]
		[FieldOffset(28)]
		internal DeltaStateEvent.<stateData>e__FixedBuffer stateData;

		// Token: 0x02000206 RID: 518
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct <stateData>e__FixedBuffer
		{
			// Token: 0x04000B20 RID: 2848
			public byte FixedElementField;
		}
	}
}
