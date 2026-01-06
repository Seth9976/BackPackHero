using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000E5 RID: 229
	[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 25)]
	public struct StateEvent : IInputEventTypeInfo
	{
		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06000DAE RID: 3502 RVA: 0x00044673 File Offset: 0x00042873
		public uint stateSizeInBytes
		{
			get
			{
				return this.baseEvent.sizeInBytes - 24U;
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06000DAF RID: 3503 RVA: 0x00044684 File Offset: 0x00042884
		public unsafe void* state
		{
			get
			{
				fixed (byte* ptr = &this.stateData.FixedElementField)
				{
					return (void*)ptr;
				}
			}
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x000446A0 File Offset: 0x000428A0
		public unsafe InputEventPtr ToEventPtr()
		{
			fixed (StateEvent* ptr = &this)
			{
				return new InputEventPtr((InputEvent*)ptr);
			}
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06000DB1 RID: 3505 RVA: 0x000446B6 File Offset: 0x000428B6
		public FourCC typeStatic
		{
			get
			{
				return 1398030676;
			}
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x000446C4 File Offset: 0x000428C4
		public TState GetState<TState>() where TState : struct, IInputStateTypeInfo
		{
			TState tstate = default(TState);
			if (this.stateFormat != tstate.format)
			{
				throw new InvalidOperationException(string.Format("Expected state format '{0}' but got '{1}' instead", tstate.format, this.stateFormat));
			}
			UnsafeUtility.MemCpy(UnsafeUtility.AddressOf<TState>(ref tstate), this.state, Math.Min((long)((ulong)this.stateSizeInBytes), (long)UnsafeUtility.SizeOf<TState>()));
			return tstate;
		}

		// Token: 0x06000DB3 RID: 3507 RVA: 0x00044745 File Offset: 0x00042945
		public unsafe static TState GetState<TState>(InputEventPtr ptr) where TState : struct, IInputStateTypeInfo
		{
			return StateEvent.From(ptr)->GetState<TState>();
		}

		// Token: 0x06000DB4 RID: 3508 RVA: 0x00044752 File Offset: 0x00042952
		public static int GetEventSizeWithPayload<TState>() where TState : struct
		{
			return UnsafeUtility.SizeOf<TState>() + 20 + 4;
		}

		// Token: 0x06000DB5 RID: 3509 RVA: 0x00044760 File Offset: 0x00042960
		public unsafe static StateEvent* From(InputEventPtr ptr)
		{
			if (!ptr.valid)
			{
				throw new ArgumentNullException("ptr");
			}
			if (!ptr.IsA<StateEvent>())
			{
				throw new InvalidCastException(string.Format("Cannot cast event with type '{0}' into StateEvent", ptr.type));
			}
			return StateEvent.FromUnchecked(ptr);
		}

		// Token: 0x06000DB6 RID: 3510 RVA: 0x000447AC File Offset: 0x000429AC
		internal unsafe static StateEvent* FromUnchecked(InputEventPtr ptr)
		{
			return (StateEvent*)ptr.data;
		}

		// Token: 0x06000DB7 RID: 3511 RVA: 0x000447B5 File Offset: 0x000429B5
		public static NativeArray<byte> From(InputDevice device, out InputEventPtr eventPtr, Allocator allocator = Allocator.Temp)
		{
			return StateEvent.From(device, out eventPtr, allocator, false);
		}

		// Token: 0x06000DB8 RID: 3512 RVA: 0x000447C0 File Offset: 0x000429C0
		public static NativeArray<byte> FromDefaultStateFor(InputDevice device, out InputEventPtr eventPtr, Allocator allocator = Allocator.Temp)
		{
			return StateEvent.From(device, out eventPtr, allocator, true);
		}

		// Token: 0x06000DB9 RID: 3513 RVA: 0x000447CC File Offset: 0x000429CC
		private unsafe static NativeArray<byte> From(InputDevice device, out InputEventPtr eventPtr, Allocator allocator, bool useDefaultState)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			if (!device.added)
			{
				throw new ArgumentException(string.Format("Device '{0}' has not been added to system", device), "device");
			}
			FourCC format = device.m_StateBlock.format;
			uint alignedSizeInBytes = device.m_StateBlock.alignedSizeInBytes;
			uint byteOffset = device.m_StateBlock.byteOffset;
			byte* ptr = (byte*)((useDefaultState ? device.defaultStatePtr : device.currentStatePtr) + byteOffset);
			uint num = 24U + alignedSizeInBytes;
			NativeArray<byte> nativeArray = new NativeArray<byte>((int)num.AlignToMultipleOf(4U), allocator, NativeArrayOptions.ClearMemory);
			StateEvent* unsafePtr = (StateEvent*)nativeArray.GetUnsafePtr<byte>();
			unsafePtr->baseEvent = new InputEvent(1398030676, (int)num, device.deviceId, InputRuntime.s_Instance.currentTime);
			unsafePtr->stateFormat = format;
			UnsafeUtility.MemCpy(unsafePtr->state, (void*)ptr, (long)((ulong)alignedSizeInBytes));
			eventPtr = unsafePtr->ToEventPtr();
			return nativeArray;
		}

		// Token: 0x0400058C RID: 1420
		public const int Type = 1398030676;

		// Token: 0x0400058D RID: 1421
		internal const int kStateDataSizeToSubtract = 1;

		// Token: 0x0400058E RID: 1422
		[FieldOffset(0)]
		public InputEvent baseEvent;

		// Token: 0x0400058F RID: 1423
		[FieldOffset(20)]
		public FourCC stateFormat;

		// Token: 0x04000590 RID: 1424
		[FixedBuffer(typeof(byte), 1)]
		[FieldOffset(24)]
		internal StateEvent.<stateData>e__FixedBuffer stateData;

		// Token: 0x02000210 RID: 528
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct <stateData>e__FixedBuffer
		{
			// Token: 0x04000B43 RID: 2883
			public byte FixedElementField;
		}
	}
}
