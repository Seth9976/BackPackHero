using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000030 RID: 48
	public static class InputControlExtensions
	{
		// Token: 0x060003E2 RID: 994 RVA: 0x000104B4 File Offset: 0x0000E6B4
		public static TControl FindInParentChain<TControl>(this InputControl control) where TControl : InputControl
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			for (InputControl inputControl = control; inputControl != null; inputControl = inputControl.parent)
			{
				TControl tcontrol = inputControl as TControl;
				if (tcontrol != null)
				{
					return tcontrol;
				}
			}
			return default(TControl);
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x000104FC File Offset: 0x0000E6FC
		public static bool IsPressed(this InputControl control, float buttonPressPoint = 0f)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (Mathf.Approximately(0f, buttonPressPoint))
			{
				ButtonControl buttonControl = control as ButtonControl;
				if (buttonControl != null)
				{
					buttonPressPoint = buttonControl.pressPointOrDefault;
				}
				else
				{
					buttonPressPoint = ButtonControl.s_GlobalDefaultButtonPressPoint;
				}
			}
			return control.IsActuated(buttonPressPoint);
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x00010548 File Offset: 0x0000E748
		public static bool IsActuated(this InputControl control, float threshold = 0f)
		{
			if (control.CheckStateIsAtDefault())
			{
				return false;
			}
			float magnitude = control.magnitude;
			if (magnitude < 0f)
			{
				return Mathf.Approximately(threshold, 0f);
			}
			if (Mathf.Approximately(threshold, 0f))
			{
				return magnitude > 0f;
			}
			return magnitude >= threshold;
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0001059C File Offset: 0x0000E79C
		public static object ReadValueAsObject(this InputControl control)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			return control.ReadValueFromStateAsObject(control.currentStatePtr);
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x000105B8 File Offset: 0x0000E7B8
		public unsafe static void ReadValueIntoBuffer(this InputControl control, void* buffer, int bufferSize)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			control.ReadValueFromStateIntoBuffer(control.currentStatePtr, buffer, bufferSize);
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x000105E6 File Offset: 0x0000E7E6
		public static object ReadDefaultValueAsObject(this InputControl control)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			return control.ReadValueFromStateAsObject(control.defaultStatePtr);
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x00010604 File Offset: 0x0000E804
		public static TValue ReadValueFromEvent<TValue>(this InputControl<TValue> control, InputEventPtr inputEvent) where TValue : struct
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			TValue tvalue;
			if (!control.ReadValueFromEvent(inputEvent, out tvalue))
			{
				return default(TValue);
			}
			return tvalue;
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x00010638 File Offset: 0x0000E838
		public unsafe static bool ReadValueFromEvent<TValue>(this InputControl<TValue> control, InputEventPtr inputEvent, out TValue value) where TValue : struct
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			void* statePtrFromStateEvent = control.GetStatePtrFromStateEvent(inputEvent);
			if (statePtrFromStateEvent == null)
			{
				value = control.ReadDefaultValue();
				return false;
			}
			value = control.ReadValueFromState(statePtrFromStateEvent);
			return true;
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0001067C File Offset: 0x0000E87C
		public unsafe static object ReadValueFromEventAsObject(this InputControl control, InputEventPtr inputEvent)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			void* statePtrFromStateEvent = control.GetStatePtrFromStateEvent(inputEvent);
			if (statePtrFromStateEvent == null)
			{
				return control.ReadDefaultValueAsObject();
			}
			return control.ReadValueFromStateAsObject(statePtrFromStateEvent);
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x000106B4 File Offset: 0x0000E8B4
		public static TValue ReadUnprocessedValueFromEvent<TValue>(this InputControl<TValue> control, InputEventPtr eventPtr) where TValue : struct
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			TValue tvalue = default(TValue);
			control.ReadUnprocessedValueFromEvent(eventPtr, out tvalue);
			return tvalue;
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x000106E4 File Offset: 0x0000E8E4
		public unsafe static bool ReadUnprocessedValueFromEvent<TValue>(this InputControl<TValue> control, InputEventPtr inputEvent, out TValue value) where TValue : struct
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			void* statePtrFromStateEvent = control.GetStatePtrFromStateEvent(inputEvent);
			if (statePtrFromStateEvent == null)
			{
				value = control.ReadDefaultValue();
				return false;
			}
			value = control.ReadUnprocessedValueFromState(statePtrFromStateEvent);
			return true;
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x00010728 File Offset: 0x0000E928
		public unsafe static void WriteValueFromObjectIntoEvent(this InputControl control, InputEventPtr eventPtr, object value)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			void* statePtrFromStateEvent = control.GetStatePtrFromStateEvent(eventPtr);
			if (statePtrFromStateEvent == null)
			{
				return;
			}
			control.WriteValueFromObjectIntoState(value, statePtrFromStateEvent);
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0001075C File Offset: 0x0000E95C
		public unsafe static void WriteValueIntoState(this InputControl control, void* statePtr)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (statePtr == null)
			{
				throw new ArgumentNullException("statePtr");
			}
			int valueSizeInBytes = control.valueSizeInBytes;
			void* ptr = UnsafeUtility.Malloc((long)valueSizeInBytes, 8, Allocator.Temp);
			try
			{
				control.ReadValueFromStateIntoBuffer(control.currentStatePtr, ptr, valueSizeInBytes);
				control.WriteValueFromBufferIntoState(ptr, valueSizeInBytes, statePtr);
			}
			finally
			{
				UnsafeUtility.Free(ptr, Allocator.Temp);
			}
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x000107CC File Offset: 0x0000E9CC
		public unsafe static void WriteValueIntoState<TValue>(this InputControl control, TValue value, void* statePtr) where TValue : struct
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			InputControl<TValue> inputControl = control as InputControl<TValue>;
			if (inputControl == null)
			{
				throw new ArgumentException(string.Concat(new string[]
				{
					"Expecting control of type '",
					typeof(TValue).Name,
					"' but got '",
					control.GetType().Name,
					"'"
				}));
			}
			inputControl.WriteValueIntoState(value, statePtr);
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x00010844 File Offset: 0x0000EA44
		public unsafe static void WriteValueIntoState<TValue>(this InputControl<TValue> control, TValue value, void* statePtr) where TValue : struct
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (statePtr == null)
			{
				throw new ArgumentNullException("statePtr");
			}
			void* ptr = UnsafeUtility.AddressOf<TValue>(ref value);
			int num = UnsafeUtility.SizeOf<TValue>();
			control.WriteValueFromBufferIntoState(ptr, num, statePtr);
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x00010886 File Offset: 0x0000EA86
		public unsafe static void WriteValueIntoState<TValue>(this InputControl<TValue> control, void* statePtr) where TValue : struct
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			control.WriteValueIntoState(control.ReadValue(), statePtr);
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x000108A4 File Offset: 0x0000EAA4
		public unsafe static void WriteValueIntoState<TValue, TState>(this InputControl<TValue> control, TValue value, ref TState state) where TValue : struct where TState : struct, IInputStateTypeInfo
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			int num = UnsafeUtility.SizeOf<TState>();
			if ((ulong)(control.stateOffsetRelativeToDeviceRoot + control.m_StateBlock.alignedSizeInBytes) >= (ulong)((long)num))
			{
				throw new ArgumentException(string.Format("Control {0} with offset {1} and size of {2} bits is out of bounds for state of type {3} with size {4}", new object[]
				{
					control.path,
					control.stateOffsetRelativeToDeviceRoot,
					control.m_StateBlock.sizeInBits,
					typeof(TState).Name,
					num
				}), "state");
			}
			byte* ptr = (byte*)UnsafeUtility.AddressOf<TState>(ref state);
			control.WriteValueIntoState(value, (void*)ptr);
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x0001094C File Offset: 0x0000EB4C
		public static void WriteValueIntoEvent<TValue>(this InputControl control, TValue value, InputEventPtr eventPtr) where TValue : struct
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (!eventPtr.valid)
			{
				throw new ArgumentNullException("eventPtr");
			}
			InputControl<TValue> inputControl = control as InputControl<TValue>;
			if (inputControl == null)
			{
				throw new ArgumentException(string.Concat(new string[]
				{
					"Expecting control of type '",
					typeof(TValue).Name,
					"' but got '",
					control.GetType().Name,
					"'"
				}));
			}
			inputControl.WriteValueIntoEvent(value, eventPtr);
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x000109D8 File Offset: 0x0000EBD8
		public unsafe static void WriteValueIntoEvent<TValue>(this InputControl<TValue> control, TValue value, InputEventPtr eventPtr) where TValue : struct
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (!eventPtr.valid)
			{
				throw new ArgumentNullException("eventPtr");
			}
			void* statePtrFromStateEvent = control.GetStatePtrFromStateEvent(eventPtr);
			if (statePtrFromStateEvent == null)
			{
				return;
			}
			control.WriteValueIntoState(value, statePtrFromStateEvent);
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00010A20 File Offset: 0x0000EC20
		public unsafe static void CopyState(this InputDevice device, void* buffer, int bufferSizeInBytes)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			if (bufferSizeInBytes <= 0)
			{
				throw new ArgumentException("bufferSizeInBytes must be positive", "bufferSizeInBytes");
			}
			InputStateBlock stateBlock = device.m_StateBlock;
			long num = Math.Min((long)bufferSizeInBytes, (long)((ulong)stateBlock.alignedSizeInBytes));
			UnsafeUtility.MemCpy(buffer, (void*)((byte*)device.currentStatePtr + stateBlock.byteOffset), num);
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x00010A7C File Offset: 0x0000EC7C
		public unsafe static void CopyState<TState>(this InputDevice device, out TState state) where TState : struct, IInputStateTypeInfo
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			state = default(TState);
			if (device.stateBlock.format != state.format)
			{
				throw new ArgumentException(string.Format("Struct '{0}' has state format '{1}' which doesn't match device '{2}' with state format '{3}'", new object[]
				{
					typeof(TState).Name,
					state.format,
					device,
					device.stateBlock.format
				}), "TState");
			}
			int num = UnsafeUtility.SizeOf<TState>();
			void* ptr = UnsafeUtility.AddressOf<TState>(ref state);
			device.CopyState(ptr, num);
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x00010B2F File Offset: 0x0000ED2F
		public static bool CheckStateIsAtDefault(this InputControl control)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			return control.CheckStateIsAtDefault(control.currentStatePtr, null);
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x00010B4D File Offset: 0x0000ED4D
		public unsafe static bool CheckStateIsAtDefault(this InputControl control, void* statePtr, void* maskPtr = null)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (statePtr == null)
			{
				throw new ArgumentNullException("statePtr");
			}
			return control.CompareState(statePtr, control.defaultStatePtr, maskPtr);
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00010B7B File Offset: 0x0000ED7B
		public static bool CheckStateIsAtDefaultIgnoringNoise(this InputControl control)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			return control.CheckStateIsAtDefaultIgnoringNoise(control.currentStatePtr);
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x00010B97 File Offset: 0x0000ED97
		public unsafe static bool CheckStateIsAtDefaultIgnoringNoise(this InputControl control, void* statePtr)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (statePtr == null)
			{
				throw new ArgumentNullException("statePtr");
			}
			return control.CheckStateIsAtDefault(statePtr, InputStateBuffers.s_NoiseMaskBuffer);
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x00010BC3 File Offset: 0x0000EDC3
		public unsafe static bool CompareStateIgnoringNoise(this InputControl control, void* statePtr)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (statePtr == null)
			{
				throw new ArgumentNullException("statePtr");
			}
			return control.CompareState(control.currentStatePtr, statePtr, control.noiseMaskPtr);
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x00010BF8 File Offset: 0x0000EDF8
		public unsafe static bool CompareState(this InputControl control, void* firstStatePtr, void* secondStatePtr, void* maskPtr = null)
		{
			byte* ptr = (byte*)firstStatePtr + control.m_StateBlock.byteOffset;
			byte* ptr2 = (byte*)secondStatePtr + control.m_StateBlock.byteOffset;
			byte* ptr3 = ((maskPtr != null) ? ((byte*)maskPtr + control.m_StateBlock.byteOffset) : null);
			if (control.m_StateBlock.sizeInBits == 1U)
			{
				return (ptr3 != null && MemoryHelpers.ReadSingleBit((void*)ptr3, control.m_StateBlock.bitOffset)) || MemoryHelpers.ReadSingleBit((void*)ptr2, control.m_StateBlock.bitOffset) == MemoryHelpers.ReadSingleBit((void*)ptr, control.m_StateBlock.bitOffset);
			}
			return MemoryHelpers.MemCmpBitRegion((void*)ptr, (void*)ptr2, control.m_StateBlock.bitOffset, control.m_StateBlock.sizeInBits, (void*)ptr3);
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x00010CA3 File Offset: 0x0000EEA3
		public unsafe static bool CompareState(this InputControl control, void* statePtr, void* maskPtr = null)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (statePtr == null)
			{
				throw new ArgumentNullException("statePtr");
			}
			return control.CompareState(control.currentStatePtr, statePtr, maskPtr);
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x00010CD1 File Offset: 0x0000EED1
		public unsafe static bool HasValueChangeInState(this InputControl control, void* statePtr)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (statePtr == null)
			{
				throw new ArgumentNullException("statePtr");
			}
			return control.CompareValue(control.currentStatePtr, statePtr);
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x00010D00 File Offset: 0x0000EF00
		public unsafe static bool HasValueChangeInEvent(this InputControl control, InputEventPtr eventPtr)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (!eventPtr.valid)
			{
				throw new ArgumentNullException("eventPtr");
			}
			void* statePtrFromStateEvent = control.GetStatePtrFromStateEvent(eventPtr);
			return statePtrFromStateEvent != null && control.CompareValue(control.currentStatePtr, statePtrFromStateEvent);
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x00010D4B File Offset: 0x0000EF4B
		public unsafe static void* GetStatePtrFromStateEvent(this InputControl control, InputEventPtr eventPtr)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (!eventPtr.valid)
			{
				throw new ArgumentNullException("eventPtr");
			}
			return control.GetStatePtrFromStateEventUnchecked(eventPtr, eventPtr.type);
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x00010D80 File Offset: 0x0000EF80
		internal unsafe static void* GetStatePtrFromStateEventUnchecked(this InputControl control, InputEventPtr eventPtr, FourCC eventType)
		{
			uint num;
			FourCC fourCC;
			uint num2;
			void* ptr2;
			if (eventType == 1398030676)
			{
				StateEvent* ptr = StateEvent.FromUnchecked(eventPtr);
				num = 0U;
				fourCC = ptr->stateFormat;
				num2 = ptr->stateSizeInBytes;
				ptr2 = ptr->state;
			}
			else
			{
				if (!(eventType == 1145852993))
				{
					throw new ArgumentException(string.Format("Event must be a StateEvent or DeltaStateEvent but is a {0} instead", eventType), "eventPtr");
				}
				DeltaStateEvent* ptr3 = DeltaStateEvent.FromUnchecked(eventPtr);
				num = ptr3->stateOffset;
				fourCC = ptr3->stateFormat;
				num2 = ptr3->deltaStateSizeInBytes;
				ptr2 = ptr3->deltaState;
			}
			InputDevice device = control.device;
			if (fourCC != device.m_StateBlock.format && (!device.hasStateCallbacks || !((IInputStateCallbackReceiver)device).GetStateOffsetForEvent(control, eventPtr, ref num)))
			{
				return null;
			}
			num += device.m_StateBlock.byteOffset;
			ref InputStateBlock ptr4 = ref control.m_StateBlock;
			long num3 = (long)ptr4.effectiveByteOffset - (long)((ulong)num);
			if (num3 < 0L || num3 + (long)((ulong)ptr4.alignedSizeInBytes) > (long)((ulong)num2))
			{
				return null;
			}
			return (void*)((byte*)ptr2 - num);
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x00010E88 File Offset: 0x0000F088
		public unsafe static bool ResetToDefaultStateInEvent(this InputControl control, InputEventPtr eventPtr)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (!eventPtr.valid)
			{
				throw new ArgumentNullException("eventPtr");
			}
			FourCC type = eventPtr.type;
			if (type != 1398030676 && type != 1145852993)
			{
				throw new ArgumentException("Given event is not a StateEvent or a DeltaStateEvent", "eventPtr");
			}
			byte* statePtrFromStateEvent = (byte*)control.GetStatePtrFromStateEvent(eventPtr);
			if (statePtrFromStateEvent == null)
			{
				return false;
			}
			byte* defaultStatePtr = (byte*)control.defaultStatePtr;
			ref InputStateBlock ptr = ref control.m_StateBlock;
			uint byteOffset = ptr.byteOffset;
			MemoryHelpers.MemCpyBitRegion((void*)(statePtrFromStateEvent + byteOffset), (void*)(defaultStatePtr + byteOffset), ptr.bitOffset, ptr.sizeInBits);
			return true;
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x00010F34 File Offset: 0x0000F134
		public static void QueueValueChange<TValue>(this InputControl<TValue> control, TValue value, double time = -1.0) where TValue : struct
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			InputEventPtr inputEventPtr;
			using (StateEvent.From(control.device, out inputEventPtr, Allocator.Temp))
			{
				if (time >= 0.0)
				{
					inputEventPtr.time = time;
				}
				control.WriteValueIntoEvent(value, inputEventPtr);
				InputSystem.QueueEvent(inputEventPtr);
			}
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x00010FA0 File Offset: 0x0000F1A0
		public unsafe static void AccumulateValueInEvent(this InputControl<float> control, void* currentStatePtr, InputEventPtr newState)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			float num;
			if (!control.ReadUnprocessedValueFromEvent(newState, out num))
			{
				return;
			}
			float num2 = control.ReadUnprocessedValueFromState(currentStatePtr);
			control.WriteValueIntoEvent(num2 + num, newState);
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x00010FDC File Offset: 0x0000F1DC
		internal unsafe static void AccumulateValueInEvent(this InputControl<Vector2> control, void* currentStatePtr, InputEventPtr newState)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			Vector2 vector;
			if (!control.ReadUnprocessedValueFromEvent(newState, out vector))
			{
				return;
			}
			Vector2 vector2 = control.ReadUnprocessedValueFromState(currentStatePtr);
			control.WriteValueIntoEvent(vector2 + vector, newState);
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0001101C File Offset: 0x0000F21C
		public static void FindControlsRecursive<TControl>(this InputControl parent, IList<TControl> controls, Func<TControl, bool> predicate) where TControl : InputControl
		{
			if (parent == null)
			{
				throw new ArgumentNullException("parent");
			}
			if (controls == null)
			{
				throw new ArgumentNullException("controls");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			TControl tcontrol = parent as TControl;
			if (tcontrol != null && predicate(tcontrol))
			{
				controls.Add(tcontrol);
			}
			int count = parent.children.Count;
			for (int i = 0; i < count; i++)
			{
				parent.children[i].FindControlsRecursive(controls, predicate);
			}
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x000110AC File Offset: 0x0000F2AC
		internal static string BuildPath(this InputControl control, string deviceLayout, StringBuilder builder = null)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (string.IsNullOrEmpty(deviceLayout))
			{
				throw new ArgumentNullException("deviceLayout");
			}
			if (builder == null)
			{
				builder = new StringBuilder();
			}
			InputDevice device = control.device;
			builder.Append('<');
			builder.Append(deviceLayout.Escape("\\>", "\\>"));
			builder.Append('>');
			ReadOnlyArray<InternedString> usages = device.usages;
			for (int i = 0; i < usages.Count; i++)
			{
				builder.Append('{');
				builder.Append(usages[i].ToString().Escape("\\}", "\\}"));
				builder.Append('}');
			}
			builder.Append('/');
			string text = device.path.Replace("\\", "\\\\");
			string text2 = control.path.Replace("\\", "\\\\");
			builder.Append(text2, text.Length + 1, text2.Length - text.Length - 1);
			return builder.ToString();
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x000111CC File Offset: 0x0000F3CC
		public static InputControlExtensions.InputEventControlCollection EnumerateControls(this InputEventPtr eventPtr, InputControlExtensions.Enumerate flags, InputDevice device = null, float magnitudeThreshold = 0f)
		{
			if (!eventPtr.valid)
			{
				throw new ArgumentNullException("eventPtr", "Given event pointer must not be null");
			}
			FourCC type = eventPtr.type;
			if (type != 1398030676 && type != 1145852993)
			{
				throw new ArgumentException(string.Format("Event must be a StateEvent or DeltaStateEvent but is a {0} instead", type), "eventPtr");
			}
			if (device == null)
			{
				int deviceId = eventPtr.deviceId;
				device = InputSystem.GetDeviceById(deviceId);
				if (device == null)
				{
					throw new ArgumentException(string.Format("Cannot find device with ID {0} referenced by event", deviceId), "eventPtr");
				}
			}
			return new InputControlExtensions.InputEventControlCollection
			{
				m_Device = device,
				m_EventPtr = eventPtr,
				m_Flags = flags,
				m_MagnitudeThreshold = magnitudeThreshold
			};
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x00011293 File Offset: 0x0000F493
		public static InputControlExtensions.InputEventControlCollection EnumerateChangedControls(this InputEventPtr eventPtr, InputDevice device = null, float magnitudeThreshold = 0f)
		{
			return eventPtr.EnumerateControls(InputControlExtensions.Enumerate.IgnoreControlsInCurrentState, device, magnitudeThreshold);
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0001129E File Offset: 0x0000F49E
		public static bool HasButtonPress(this InputEventPtr eventPtr, float magnitude = -1f, bool buttonControlsOnly = true)
		{
			return eventPtr.GetFirstButtonPressOrNull(magnitude, buttonControlsOnly) != null;
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x000112AC File Offset: 0x0000F4AC
		public static InputControl GetFirstButtonPressOrNull(this InputEventPtr eventPtr, float magnitude = -1f, bool buttonControlsOnly = true)
		{
			if (eventPtr.type != 1398030676 && eventPtr.type != 1145852993)
			{
				return null;
			}
			if (magnitude < 0f)
			{
				magnitude = InputSystem.settings.defaultButtonPressPoint;
			}
			foreach (InputControl inputControl in eventPtr.EnumerateControls(InputControlExtensions.Enumerate.IgnoreControlsInDefaultState, null, magnitude))
			{
				if (!buttonControlsOnly || inputControl.isButton)
				{
					return inputControl;
				}
			}
			return null;
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x00011358 File Offset: 0x0000F558
		public static IEnumerable<InputControl> GetAllButtonPresses(this InputEventPtr eventPtr, float magnitude = -1f, bool buttonControlsOnly = true)
		{
			if (eventPtr.type != 1398030676 && eventPtr.type != 1145852993)
			{
				yield break;
			}
			if (magnitude < 0f)
			{
				magnitude = InputSystem.settings.defaultButtonPressPoint;
			}
			foreach (InputControl inputControl in eventPtr.EnumerateControls(InputControlExtensions.Enumerate.IgnoreControlsInDefaultState, null, magnitude))
			{
				if (!buttonControlsOnly || inputControl.isButton)
				{
					yield return inputControl;
				}
			}
			InputControlExtensions.InputEventControlEnumerator inputEventControlEnumerator = default(InputControlExtensions.InputEventControlEnumerator);
			yield break;
			yield break;
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x00011378 File Offset: 0x0000F578
		public static InputControlExtensions.ControlBuilder Setup(this InputControl control)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (control.isSetupFinished)
			{
				throw new InvalidOperationException(string.Format("The setup of {0} cannot be modified; control is already in use", control));
			}
			return new InputControlExtensions.ControlBuilder
			{
				control = control
			};
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x000113C0 File Offset: 0x0000F5C0
		public static InputControlExtensions.DeviceBuilder Setup(this InputDevice device, int controlCount, int usageCount, int aliasCount)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			if (device.isSetupFinished)
			{
				throw new InvalidOperationException(string.Format("The setup of {0} cannot be modified; control is already in use", device));
			}
			if (controlCount < 1)
			{
				throw new ArgumentOutOfRangeException("controlCount");
			}
			if (usageCount < 0)
			{
				throw new ArgumentOutOfRangeException("usageCount");
			}
			if (aliasCount < 0)
			{
				throw new ArgumentOutOfRangeException("aliasCount");
			}
			device.m_Device = device;
			device.m_ChildrenForEachControl = new InputControl[controlCount];
			if (usageCount > 0)
			{
				device.m_UsagesForEachControl = new InternedString[usageCount];
				device.m_UsageToControl = new InputControl[usageCount];
			}
			if (aliasCount > 0)
			{
				device.m_AliasesForEachControl = new InternedString[aliasCount];
			}
			return new InputControlExtensions.DeviceBuilder
			{
				device = device
			};
		}

		// Token: 0x02000183 RID: 387
		[Flags]
		public enum Enumerate
		{
			// Token: 0x0400083E RID: 2110
			IgnoreControlsInDefaultState = 1,
			// Token: 0x0400083F RID: 2111
			IgnoreControlsInCurrentState = 2,
			// Token: 0x04000840 RID: 2112
			IncludeSyntheticControls = 4,
			// Token: 0x04000841 RID: 2113
			IncludeNoisyControls = 8,
			// Token: 0x04000842 RID: 2114
			IncludeNonLeafControls = 16
		}

		// Token: 0x02000184 RID: 388
		public struct InputEventControlCollection : IEnumerable<InputControl>, IEnumerable
		{
			// Token: 0x1700053B RID: 1339
			// (get) Token: 0x06001343 RID: 4931 RVA: 0x0005921D File Offset: 0x0005741D
			public InputEventPtr eventPtr
			{
				get
				{
					return this.m_EventPtr;
				}
			}

			// Token: 0x06001344 RID: 4932 RVA: 0x00059225 File Offset: 0x00057425
			public InputControlExtensions.InputEventControlEnumerator GetEnumerator()
			{
				return new InputControlExtensions.InputEventControlEnumerator(this.m_EventPtr, this.m_Device, this.m_Flags, this.m_MagnitudeThreshold);
			}

			// Token: 0x06001345 RID: 4933 RVA: 0x00059244 File Offset: 0x00057444
			IEnumerator<InputControl> IEnumerable<InputControl>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06001346 RID: 4934 RVA: 0x00059251 File Offset: 0x00057451
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x04000843 RID: 2115
			internal InputDevice m_Device;

			// Token: 0x04000844 RID: 2116
			internal InputEventPtr m_EventPtr;

			// Token: 0x04000845 RID: 2117
			internal InputControlExtensions.Enumerate m_Flags;

			// Token: 0x04000846 RID: 2118
			internal float m_MagnitudeThreshold;
		}

		// Token: 0x02000185 RID: 389
		public struct InputEventControlEnumerator : IEnumerator<InputControl>, IEnumerator, IDisposable
		{
			// Token: 0x06001347 RID: 4935 RVA: 0x00059260 File Offset: 0x00057460
			internal unsafe InputEventControlEnumerator(InputEventPtr eventPtr, InputDevice device, InputControlExtensions.Enumerate flags, float magnitudeThreshold = 0f)
			{
				this.m_Device = device;
				this.m_StateOffsetToControlIndex = device.m_StateOffsetToControlMap;
				this.m_StateOffsetToControlIndexLength = this.m_StateOffsetToControlIndex.LengthSafe<uint>();
				this.m_AllControls = device.m_ChildrenForEachControl;
				this.m_EventPtr = eventPtr;
				this.m_Flags = flags;
				this.m_CurrentControl = null;
				this.m_CurrentIndexInStateOffsetToControlIndexMap = 0;
				this.m_CurrentControlStateBitOffset = 0U;
				this.m_EventState = default(byte*);
				this.m_CurrentBitOffset = 0U;
				this.m_EndBitOffset = 0U;
				this.m_MagnitudeThreshold = magnitudeThreshold;
				if ((flags & InputControlExtensions.Enumerate.IncludeNoisyControls) == (InputControlExtensions.Enumerate)0)
				{
					this.m_NoiseMask = (byte*)device.noiseMaskPtr + device.m_StateBlock.byteOffset;
				}
				else
				{
					this.m_NoiseMask = default(byte*);
				}
				if ((flags & InputControlExtensions.Enumerate.IgnoreControlsInDefaultState) != (InputControlExtensions.Enumerate)0)
				{
					this.m_DefaultState = (byte*)device.defaultStatePtr + device.m_StateBlock.byteOffset;
				}
				else
				{
					this.m_DefaultState = default(byte*);
				}
				if ((flags & InputControlExtensions.Enumerate.IgnoreControlsInCurrentState) != (InputControlExtensions.Enumerate)0)
				{
					this.m_CurrentState = (byte*)device.currentStatePtr + device.m_StateBlock.byteOffset;
				}
				else
				{
					this.m_CurrentState = default(byte*);
				}
				this.Reset();
			}

			// Token: 0x06001348 RID: 4936 RVA: 0x0005936C File Offset: 0x0005756C
			private unsafe bool CheckDefault(uint numBits)
			{
				return MemoryHelpers.MemCmpBitRegion((void*)this.m_EventState, (void*)this.m_DefaultState, this.m_CurrentBitOffset, numBits, (void*)this.m_NoiseMask);
			}

			// Token: 0x06001349 RID: 4937 RVA: 0x0005938C File Offset: 0x0005758C
			private unsafe bool CheckCurrent(uint numBits)
			{
				return MemoryHelpers.MemCmpBitRegion((void*)this.m_EventState, (void*)this.m_CurrentState, this.m_CurrentBitOffset, numBits, (void*)this.m_NoiseMask);
			}

			// Token: 0x0600134A RID: 4938 RVA: 0x000593AC File Offset: 0x000575AC
			public unsafe bool MoveNext()
			{
				if (!this.m_EventPtr.valid)
				{
					throw new ObjectDisposedException("Enumerator has already been disposed");
				}
				if (this.m_CurrentControl != null && (this.m_Flags & InputControlExtensions.Enumerate.IncludeNonLeafControls) != (InputControlExtensions.Enumerate)0)
				{
					InputControl parent = this.m_CurrentControl.parent;
					if (parent != this.m_Device)
					{
						this.m_CurrentControl = parent;
						return true;
					}
				}
				bool flag = this.m_DefaultState != null;
				bool flag2 = this.m_CurrentState != null;
				for (;;)
				{
					this.m_CurrentControl = null;
					if (flag2 || flag)
					{
						if ((this.m_CurrentBitOffset & 7U) != 0U)
						{
							uint num = (this.m_CurrentBitOffset + 8U) & 7U;
							if ((flag2 && this.CheckCurrent(num)) || (flag && this.CheckDefault(num)))
							{
								this.m_CurrentBitOffset += num;
							}
						}
						while (this.m_CurrentBitOffset < this.m_EndBitOffset)
						{
							uint num2 = this.m_CurrentBitOffset >> 3;
							byte b = this.m_EventState[num2];
							int num3 = (int)((this.m_NoiseMask != null) ? this.m_NoiseMask[num2] : byte.MaxValue);
							if (flag2 && ((int)this.m_CurrentState[num2] & num3) == ((int)b & num3))
							{
								this.m_CurrentBitOffset += 8U;
							}
							else
							{
								if (!flag || ((int)this.m_DefaultState[num2] & num3) != ((int)b & num3))
								{
									break;
								}
								this.m_CurrentBitOffset += 8U;
							}
						}
					}
					if (this.m_CurrentBitOffset >= this.m_EndBitOffset || this.m_CurrentIndexInStateOffsetToControlIndexMap >= this.m_StateOffsetToControlIndexLength)
					{
						break;
					}
					while (this.m_CurrentIndexInStateOffsetToControlIndexMap < this.m_StateOffsetToControlIndexLength)
					{
						uint num4;
						uint num5;
						uint num6;
						InputDevice.DecodeStateOffsetToControlMapEntry(this.m_StateOffsetToControlIndex[this.m_CurrentIndexInStateOffsetToControlIndexMap], out num4, out num5, out num6);
						if (num5 >= this.m_CurrentControlStateBitOffset && this.m_CurrentBitOffset < num5 + num6 - this.m_CurrentControlStateBitOffset)
						{
							if (num5 - this.m_CurrentControlStateBitOffset >= this.m_CurrentBitOffset + 8U)
							{
								this.m_CurrentBitOffset = num5 - this.m_CurrentControlStateBitOffset;
								break;
							}
							if (num5 + num6 - this.m_CurrentControlStateBitOffset <= this.m_EndBitOffset)
							{
								if ((num5 & 7U) == 0U && (num6 & 7U) == 0U)
								{
									this.m_CurrentControl = this.m_AllControls[(int)num4];
								}
								else
								{
									if ((flag2 && MemoryHelpers.MemCmpBitRegion((void*)this.m_EventState, (void*)this.m_CurrentState, num5 - this.m_CurrentControlStateBitOffset, num6, (void*)this.m_NoiseMask)) || (flag && MemoryHelpers.MemCmpBitRegion((void*)this.m_EventState, (void*)this.m_DefaultState, num5 - this.m_CurrentControlStateBitOffset, num6, (void*)this.m_NoiseMask)))
									{
										goto IL_02BF;
									}
									this.m_CurrentControl = this.m_AllControls[(int)num4];
								}
								if ((this.m_Flags & InputControlExtensions.Enumerate.IncludeNoisyControls) == (InputControlExtensions.Enumerate)0 && this.m_CurrentControl.noisy)
								{
									this.m_CurrentControl = null;
								}
								else
								{
									if ((this.m_Flags & InputControlExtensions.Enumerate.IncludeSyntheticControls) != (InputControlExtensions.Enumerate)0 || (this.m_CurrentControl.m_ControlFlags & (InputControl.ControlFlags.IsSynthetic | InputControl.ControlFlags.UsesStateFromOtherControl)) <= (InputControl.ControlFlags)0)
									{
										this.m_CurrentIndexInStateOffsetToControlIndexMap++;
										break;
									}
									this.m_CurrentControl = null;
								}
							}
						}
						IL_02BF:
						this.m_CurrentIndexInStateOffsetToControlIndexMap++;
					}
					if (this.m_CurrentControl != null)
					{
						if (this.m_MagnitudeThreshold == 0f)
						{
							return true;
						}
						byte* ptr = this.m_EventState - (this.m_CurrentControlStateBitOffset >> 3) - this.m_Device.m_StateBlock.byteOffset;
						float num7 = this.m_CurrentControl.EvaluateMagnitude((void*)ptr);
						if (num7 < 0f || num7 >= this.m_MagnitudeThreshold)
						{
							return true;
						}
					}
				}
				return false;
			}

			// Token: 0x0600134B RID: 4939 RVA: 0x000596FC File Offset: 0x000578FC
			public unsafe void Reset()
			{
				if (!this.m_EventPtr.valid)
				{
					throw new ObjectDisposedException("Enumerator has already been disposed");
				}
				FourCC type = this.m_EventPtr.type;
				FourCC fourCC;
				if (type == 1398030676)
				{
					StateEvent* ptr = StateEvent.FromUnchecked(this.m_EventPtr);
					this.m_EventState = (byte*)ptr->state;
					this.m_EndBitOffset = ptr->stateSizeInBytes * 8U;
					this.m_CurrentBitOffset = 0U;
					fourCC = ptr->stateFormat;
				}
				else
				{
					if (!(type == 1145852993))
					{
						throw new NotSupportedException(string.Format("Cannot iterate over controls in event of type '{0}'", type));
					}
					DeltaStateEvent* ptr2 = DeltaStateEvent.FromUnchecked(this.m_EventPtr);
					this.m_EventState = (byte*)ptr2->deltaState - ptr2->stateOffset;
					this.m_CurrentBitOffset = ptr2->stateOffset * 8U;
					this.m_EndBitOffset = this.m_CurrentBitOffset + ptr2->deltaStateSizeInBytes * 8U;
					fourCC = ptr2->stateFormat;
				}
				this.m_CurrentIndexInStateOffsetToControlIndexMap = 0;
				this.m_CurrentControlStateBitOffset = 0U;
				this.m_CurrentControl = null;
				if (fourCC != this.m_Device.m_StateBlock.format)
				{
					uint num = 0U;
					if (this.m_Device.hasStateCallbacks && ((IInputStateCallbackReceiver)this.m_Device).GetStateOffsetForEvent(null, this.m_EventPtr, ref num))
					{
						this.m_CurrentControlStateBitOffset = num * 8U;
						if (this.m_CurrentState != null)
						{
							this.m_CurrentState += num;
						}
						if (this.m_DefaultState != null)
						{
							this.m_DefaultState += num;
						}
						if (this.m_NoiseMask != null)
						{
							this.m_NoiseMask += num;
							return;
						}
					}
					else if (!(this.m_Device is Touchscreen) || !this.m_EventPtr.IsA<StateEvent>() || !(StateEvent.FromUnchecked(this.m_EventPtr)->stateFormat == TouchState.Format))
					{
						throw new InvalidOperationException(string.Format("{0} event with state format {1} cannot be used with device '{2}'", type, fourCC, this.m_Device));
					}
				}
			}

			// Token: 0x0600134C RID: 4940 RVA: 0x000598F5 File Offset: 0x00057AF5
			public void Dispose()
			{
				this.m_EventPtr = default(InputEventPtr);
			}

			// Token: 0x1700053C RID: 1340
			// (get) Token: 0x0600134D RID: 4941 RVA: 0x00059903 File Offset: 0x00057B03
			public InputControl Current
			{
				get
				{
					return this.m_CurrentControl;
				}
			}

			// Token: 0x1700053D RID: 1341
			// (get) Token: 0x0600134E RID: 4942 RVA: 0x0005990B File Offset: 0x00057B0B
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x04000847 RID: 2119
			private InputControlExtensions.Enumerate m_Flags;

			// Token: 0x04000848 RID: 2120
			private readonly InputDevice m_Device;

			// Token: 0x04000849 RID: 2121
			private readonly uint[] m_StateOffsetToControlIndex;

			// Token: 0x0400084A RID: 2122
			private readonly int m_StateOffsetToControlIndexLength;

			// Token: 0x0400084B RID: 2123
			private readonly InputControl[] m_AllControls;

			// Token: 0x0400084C RID: 2124
			private unsafe byte* m_DefaultState;

			// Token: 0x0400084D RID: 2125
			private unsafe byte* m_CurrentState;

			// Token: 0x0400084E RID: 2126
			private unsafe byte* m_NoiseMask;

			// Token: 0x0400084F RID: 2127
			private InputEventPtr m_EventPtr;

			// Token: 0x04000850 RID: 2128
			private InputControl m_CurrentControl;

			// Token: 0x04000851 RID: 2129
			private int m_CurrentIndexInStateOffsetToControlIndexMap;

			// Token: 0x04000852 RID: 2130
			private uint m_CurrentControlStateBitOffset;

			// Token: 0x04000853 RID: 2131
			private unsafe byte* m_EventState;

			// Token: 0x04000854 RID: 2132
			private uint m_CurrentBitOffset;

			// Token: 0x04000855 RID: 2133
			private uint m_EndBitOffset;

			// Token: 0x04000856 RID: 2134
			private float m_MagnitudeThreshold;
		}

		// Token: 0x02000186 RID: 390
		public struct ControlBuilder
		{
			// Token: 0x1700053E RID: 1342
			// (get) Token: 0x0600134F RID: 4943 RVA: 0x00059913 File Offset: 0x00057B13
			// (set) Token: 0x06001350 RID: 4944 RVA: 0x0005991B File Offset: 0x00057B1B
			public InputControl control { readonly get; internal set; }

			// Token: 0x06001351 RID: 4945 RVA: 0x00059924 File Offset: 0x00057B24
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.ControlBuilder At(InputDevice device, int index)
			{
				device.m_ChildrenForEachControl[index] = this.control;
				this.control.m_Device = device;
				return this;
			}

			// Token: 0x06001352 RID: 4946 RVA: 0x00059946 File Offset: 0x00057B46
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.ControlBuilder WithParent(InputControl parent)
			{
				this.control.m_Parent = parent;
				return this;
			}

			// Token: 0x06001353 RID: 4947 RVA: 0x0005995A File Offset: 0x00057B5A
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.ControlBuilder WithName(string name)
			{
				this.control.m_Name = new InternedString(name);
				return this;
			}

			// Token: 0x06001354 RID: 4948 RVA: 0x00059973 File Offset: 0x00057B73
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.ControlBuilder WithDisplayName(string displayName)
			{
				this.control.m_DisplayNameFromLayout = new InternedString(displayName);
				return this;
			}

			// Token: 0x06001355 RID: 4949 RVA: 0x00059991 File Offset: 0x00057B91
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.ControlBuilder WithShortDisplayName(string shortDisplayName)
			{
				this.control.m_ShortDisplayNameFromLayout = new InternedString(shortDisplayName);
				return this;
			}

			// Token: 0x06001356 RID: 4950 RVA: 0x000599AF File Offset: 0x00057BAF
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.ControlBuilder WithLayout(InternedString layout)
			{
				this.control.m_Layout = layout;
				return this;
			}

			// Token: 0x06001357 RID: 4951 RVA: 0x000599C3 File Offset: 0x00057BC3
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.ControlBuilder WithUsages(int startIndex, int count)
			{
				this.control.m_UsageStartIndex = startIndex;
				this.control.m_UsageCount = count;
				return this;
			}

			// Token: 0x06001358 RID: 4952 RVA: 0x000599E3 File Offset: 0x00057BE3
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.ControlBuilder WithAliases(int startIndex, int count)
			{
				this.control.m_AliasStartIndex = startIndex;
				this.control.m_AliasCount = count;
				return this;
			}

			// Token: 0x06001359 RID: 4953 RVA: 0x00059A03 File Offset: 0x00057C03
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.ControlBuilder WithChildren(int startIndex, int count)
			{
				this.control.m_ChildStartIndex = startIndex;
				this.control.m_ChildCount = count;
				return this;
			}

			// Token: 0x0600135A RID: 4954 RVA: 0x00059A23 File Offset: 0x00057C23
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.ControlBuilder WithStateBlock(InputStateBlock stateBlock)
			{
				this.control.m_StateBlock = stateBlock;
				return this;
			}

			// Token: 0x0600135B RID: 4955 RVA: 0x00059A37 File Offset: 0x00057C37
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.ControlBuilder WithDefaultState(PrimitiveValue value)
			{
				this.control.m_DefaultState = value;
				this.control.m_Device.hasControlsWithDefaultState = true;
				return this;
			}

			// Token: 0x0600135C RID: 4956 RVA: 0x00059A5C File Offset: 0x00057C5C
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.ControlBuilder WithMinAndMax(PrimitiveValue min, PrimitiveValue max)
			{
				this.control.m_MinValue = min;
				this.control.m_MaxValue = max;
				return this;
			}

			// Token: 0x0600135D RID: 4957 RVA: 0x00059A7C File Offset: 0x00057C7C
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.ControlBuilder WithProcessor<TProcessor, TValue>(TProcessor processor) where TProcessor : InputProcessor<TValue> where TValue : struct
			{
				((InputControl<TValue>)this.control).m_ProcessorStack.Append(processor);
				return this;
			}

			// Token: 0x0600135E RID: 4958 RVA: 0x00059AA0 File Offset: 0x00057CA0
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.ControlBuilder IsNoisy(bool value)
			{
				this.control.noisy = value;
				return this;
			}

			// Token: 0x0600135F RID: 4959 RVA: 0x00059AB4 File Offset: 0x00057CB4
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.ControlBuilder IsSynthetic(bool value)
			{
				this.control.synthetic = value;
				return this;
			}

			// Token: 0x06001360 RID: 4960 RVA: 0x00059AC8 File Offset: 0x00057CC8
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.ControlBuilder DontReset(bool value)
			{
				this.control.dontReset = value;
				if (value)
				{
					this.control.m_Device.hasDontResetControls = true;
				}
				return this;
			}

			// Token: 0x06001361 RID: 4961 RVA: 0x00059AF0 File Offset: 0x00057CF0
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.ControlBuilder IsButton(bool value)
			{
				this.control.isButton = value;
				return this;
			}

			// Token: 0x06001362 RID: 4962 RVA: 0x00059B04 File Offset: 0x00057D04
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void Finish()
			{
				this.control.isSetupFinished = true;
			}
		}

		// Token: 0x02000187 RID: 391
		public struct DeviceBuilder
		{
			// Token: 0x1700053F RID: 1343
			// (get) Token: 0x06001363 RID: 4963 RVA: 0x00059B12 File Offset: 0x00057D12
			// (set) Token: 0x06001364 RID: 4964 RVA: 0x00059B1A File Offset: 0x00057D1A
			public InputDevice device { readonly get; internal set; }

			// Token: 0x06001365 RID: 4965 RVA: 0x00059B23 File Offset: 0x00057D23
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.DeviceBuilder WithName(string name)
			{
				this.device.m_Name = new InternedString(name);
				return this;
			}

			// Token: 0x06001366 RID: 4966 RVA: 0x00059B3C File Offset: 0x00057D3C
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.DeviceBuilder WithDisplayName(string displayName)
			{
				this.device.m_DisplayNameFromLayout = new InternedString(displayName);
				return this;
			}

			// Token: 0x06001367 RID: 4967 RVA: 0x00059B5A File Offset: 0x00057D5A
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.DeviceBuilder WithShortDisplayName(string shortDisplayName)
			{
				this.device.m_ShortDisplayNameFromLayout = new InternedString(shortDisplayName);
				return this;
			}

			// Token: 0x06001368 RID: 4968 RVA: 0x00059B78 File Offset: 0x00057D78
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.DeviceBuilder WithLayout(InternedString layout)
			{
				this.device.m_Layout = layout;
				return this;
			}

			// Token: 0x06001369 RID: 4969 RVA: 0x00059B8C File Offset: 0x00057D8C
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.DeviceBuilder WithChildren(int startIndex, int count)
			{
				this.device.m_ChildStartIndex = startIndex;
				this.device.m_ChildCount = count;
				return this;
			}

			// Token: 0x0600136A RID: 4970 RVA: 0x00059BAC File Offset: 0x00057DAC
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.DeviceBuilder WithStateBlock(InputStateBlock stateBlock)
			{
				this.device.m_StateBlock = stateBlock;
				return this;
			}

			// Token: 0x0600136B RID: 4971 RVA: 0x00059BC0 File Offset: 0x00057DC0
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.DeviceBuilder IsNoisy(bool value)
			{
				this.device.noisy = value;
				return this;
			}

			// Token: 0x0600136C RID: 4972 RVA: 0x00059BD4 File Offset: 0x00057DD4
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.DeviceBuilder WithControlUsage(int controlIndex, InternedString usage, InputControl control)
			{
				this.device.m_UsagesForEachControl[controlIndex] = usage;
				this.device.m_UsageToControl[controlIndex] = control;
				return this;
			}

			// Token: 0x0600136D RID: 4973 RVA: 0x00059BFC File Offset: 0x00057DFC
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.DeviceBuilder WithControlAlias(int controlIndex, InternedString alias)
			{
				this.device.m_AliasesForEachControl[controlIndex] = alias;
				return this;
			}

			// Token: 0x0600136E RID: 4974 RVA: 0x00059C16 File Offset: 0x00057E16
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.DeviceBuilder WithStateOffsetToControlIndexMap(uint[] map)
			{
				this.device.m_StateOffsetToControlMap = map;
				return this;
			}

			// Token: 0x0600136F RID: 4975 RVA: 0x00059C2C File Offset: 0x00057E2C
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public unsafe InputControlExtensions.DeviceBuilder WithControlTree(byte[] controlTreeNodes, ushort[] controlTreeIndicies)
			{
				int num = UnsafeUtility.SizeOf<InputDevice.ControlBitRangeNode>();
				int num2 = controlTreeNodes.Length / num;
				this.device.m_ControlTreeNodes = new InputDevice.ControlBitRangeNode[num2];
				fixed (byte[] array = controlTreeNodes)
				{
					byte* ptr;
					if (controlTreeNodes == null || array.Length == 0)
					{
						ptr = null;
					}
					else
					{
						ptr = &array[0];
					}
					for (int i = 0; i < num2; i++)
					{
						this.device.m_ControlTreeNodes[i] = *(InputDevice.ControlBitRangeNode*)(ptr + i * num);
					}
				}
				this.device.m_ControlTreeIndices = controlTreeIndicies;
				return this;
			}

			// Token: 0x06001370 RID: 4976 RVA: 0x00059CAF File Offset: 0x00057EAF
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void Finish()
			{
				this.device.isSetupFinished = true;
			}
		}
	}
}
