using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000037 RID: 55
	public class InputDevice : InputControl
	{
		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000488 RID: 1160 RVA: 0x000130A2 File Offset: 0x000112A2
		public InputDeviceDescription description
		{
			get
			{
				return this.m_Description;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000489 RID: 1161 RVA: 0x000130AA File Offset: 0x000112AA
		public bool enabled
		{
			get
			{
				return (this.m_DeviceFlags & (InputDevice.DeviceFlags.DisabledInFrontend | InputDevice.DeviceFlags.DisabledWhileInBackground)) == (InputDevice.DeviceFlags)0 && this.QueryEnabledStateFromRuntime();
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x0600048A RID: 1162 RVA: 0x000130C4 File Offset: 0x000112C4
		public bool canRunInBackground
		{
			get
			{
				if ((this.m_DeviceFlags & InputDevice.DeviceFlags.CanRunInBackgroundHasBeenQueried) != (InputDevice.DeviceFlags)0)
				{
					return (this.m_DeviceFlags & InputDevice.DeviceFlags.CanRunInBackground) > (InputDevice.DeviceFlags)0;
				}
				QueryCanRunInBackground queryCanRunInBackground = QueryCanRunInBackground.Create();
				this.m_DeviceFlags |= InputDevice.DeviceFlags.CanRunInBackgroundHasBeenQueried;
				if (this.ExecuteCommand<QueryCanRunInBackground>(ref queryCanRunInBackground) >= 0L && queryCanRunInBackground.canRunInBackground)
				{
					this.m_DeviceFlags |= InputDevice.DeviceFlags.CanRunInBackground;
					return true;
				}
				this.m_DeviceFlags &= ~InputDevice.DeviceFlags.CanRunInBackground;
				return false;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x0600048B RID: 1163 RVA: 0x00013142 File Offset: 0x00011342
		public bool added
		{
			get
			{
				return this.m_DeviceIndex != -1;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x0600048C RID: 1164 RVA: 0x00013150 File Offset: 0x00011350
		public bool remote
		{
			get
			{
				return (this.m_DeviceFlags & InputDevice.DeviceFlags.Remote) == InputDevice.DeviceFlags.Remote;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x0600048D RID: 1165 RVA: 0x0001315D File Offset: 0x0001135D
		public bool native
		{
			get
			{
				return (this.m_DeviceFlags & InputDevice.DeviceFlags.Native) == InputDevice.DeviceFlags.Native;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x0600048E RID: 1166 RVA: 0x0001316C File Offset: 0x0001136C
		public bool updateBeforeRender
		{
			get
			{
				return (this.m_DeviceFlags & InputDevice.DeviceFlags.UpdateBeforeRender) == InputDevice.DeviceFlags.UpdateBeforeRender;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600048F RID: 1167 RVA: 0x00013179 File Offset: 0x00011379
		public int deviceId
		{
			get
			{
				return this.m_DeviceId;
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000490 RID: 1168 RVA: 0x00013181 File Offset: 0x00011381
		public double lastUpdateTime
		{
			get
			{
				return this.m_LastUpdateTimeInternal - InputRuntime.s_CurrentTimeOffsetToRealtimeSinceStartup;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000491 RID: 1169 RVA: 0x0001318F File Offset: 0x0001138F
		public bool wasUpdatedThisFrame
		{
			get
			{
				return this.m_CurrentUpdateStepCount == InputUpdate.s_UpdateStepCount;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000492 RID: 1170 RVA: 0x0001319E File Offset: 0x0001139E
		public ReadOnlyArray<InputControl> allControls
		{
			get
			{
				return new ReadOnlyArray<InputControl>(this.m_ChildrenForEachControl);
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000493 RID: 1171 RVA: 0x000131AB File Offset: 0x000113AB
		public override Type valueType
		{
			get
			{
				return typeof(byte[]);
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000494 RID: 1172 RVA: 0x000131B7 File Offset: 0x000113B7
		public override int valueSizeInBytes
		{
			get
			{
				return (int)this.m_StateBlock.alignedSizeInBytes;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000495 RID: 1173 RVA: 0x000131C4 File Offset: 0x000113C4
		[Obsolete("Use 'InputSystem.devices' instead. (UnityUpgradable) -> InputSystem.devices", false)]
		public static ReadOnlyArray<InputDevice> all
		{
			get
			{
				return InputSystem.devices;
			}
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x000131CB File Offset: 0x000113CB
		public InputDevice()
		{
			this.m_DeviceId = 0;
			this.m_ParticipantId = 0;
			this.m_DeviceIndex = -1;
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x000131E8 File Offset: 0x000113E8
		public unsafe override object ReadValueFromBufferAsObject(void* buffer, int bufferSize)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x000131F0 File Offset: 0x000113F0
		public unsafe override object ReadValueFromStateAsObject(void* statePtr)
		{
			if (this.m_DeviceIndex == -1)
			{
				return null;
			}
			uint alignedSizeInBytes = base.stateBlock.alignedSizeInBytes;
			byte[] array2;
			byte[] array = (array2 = new byte[alignedSizeInBytes]);
			byte* ptr;
			if (array == null || array2.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array2[0];
			}
			byte* ptr2 = (byte*)statePtr + this.m_StateBlock.byteOffset;
			UnsafeUtility.MemCpy((void*)ptr, (void*)ptr2, (long)((ulong)alignedSizeInBytes));
			array2 = null;
			return array;
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x00013254 File Offset: 0x00011454
		public unsafe override void ReadValueFromStateIntoBuffer(void* statePtr, void* bufferPtr, int bufferSize)
		{
			if (statePtr == null)
			{
				throw new ArgumentNullException("statePtr");
			}
			if (bufferPtr == null)
			{
				throw new ArgumentNullException("bufferPtr");
			}
			if (bufferSize < this.valueSizeInBytes)
			{
				throw new ArgumentException(string.Format("Buffer too small (expected: {0}, actual: {1}", this.valueSizeInBytes, bufferSize));
			}
			byte* ptr = (byte*)statePtr + this.m_StateBlock.byteOffset;
			UnsafeUtility.MemCpy(bufferPtr, (void*)ptr, (long)((ulong)this.m_StateBlock.alignedSizeInBytes));
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x000132D0 File Offset: 0x000114D0
		public unsafe override bool CompareValue(void* firstStatePtr, void* secondStatePtr)
		{
			if (firstStatePtr == null)
			{
				throw new ArgumentNullException("firstStatePtr");
			}
			if (secondStatePtr == null)
			{
				throw new ArgumentNullException("secondStatePtr");
			}
			void* ptr = (void*)((byte*)firstStatePtr + this.m_StateBlock.byteOffset);
			byte* ptr2 = (byte*)firstStatePtr + this.m_StateBlock.byteOffset;
			return UnsafeUtility.MemCmp(ptr, (void*)ptr2, (long)((ulong)this.m_StateBlock.alignedSizeInBytes)) == 0;
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x00013330 File Offset: 0x00011530
		internal void NotifyConfigurationChanged()
		{
			base.isConfigUpToDate = false;
			for (int i = 0; i < this.m_ChildrenForEachControl.Length; i++)
			{
				this.m_ChildrenForEachControl[i].isConfigUpToDate = false;
			}
			this.m_DeviceFlags &= ~InputDevice.DeviceFlags.DisabledStateHasBeenQueriedFromRuntime;
			this.OnConfigurationChanged();
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x0001337A File Offset: 0x0001157A
		public virtual void MakeCurrent()
		{
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x0001337C File Offset: 0x0001157C
		protected virtual void OnAdded()
		{
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x0001337E File Offset: 0x0001157E
		protected virtual void OnRemoved()
		{
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x00013380 File Offset: 0x00011580
		protected virtual void OnConfigurationChanged()
		{
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x00013384 File Offset: 0x00011584
		public unsafe long ExecuteCommand<TCommand>(ref TCommand command) where TCommand : struct, IInputDeviceCommandInfo
		{
			InputDeviceCommand* ptr = (InputDeviceCommand*)UnsafeUtility.AddressOf<TCommand>(ref command);
			InputManager s_Manager = InputSystem.s_Manager;
			s_Manager.m_DeviceCommandCallbacks.LockForChanges();
			for (int i = 0; i < s_Manager.m_DeviceCommandCallbacks.length; i++)
			{
				try
				{
					long? num = s_Manager.m_DeviceCommandCallbacks[i](this, ptr);
					if (num != null)
					{
						return num.Value;
					}
				}
				catch (Exception ex)
				{
					Debug.LogError(ex.GetType().Name + " while executing 'InputSystem.onDeviceCommand' callbacks");
					Debug.LogException(ex);
				}
			}
			s_Manager.m_DeviceCommandCallbacks.UnlockForChanges();
			return this.ExecuteCommand((InputDeviceCommand*)UnsafeUtility.AddressOf<TCommand>(ref command));
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x00013438 File Offset: 0x00011638
		protected unsafe virtual long ExecuteCommand(InputDeviceCommand* commandPtr)
		{
			return InputRuntime.s_Instance.DeviceCommand(this.deviceId, commandPtr);
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x0001344C File Offset: 0x0001164C
		internal bool QueryEnabledStateFromRuntime()
		{
			if ((this.m_DeviceFlags & InputDevice.DeviceFlags.DisabledStateHasBeenQueriedFromRuntime) == (InputDevice.DeviceFlags)0)
			{
				QueryEnabledStateCommand queryEnabledStateCommand = QueryEnabledStateCommand.Create();
				if (this.ExecuteCommand<QueryEnabledStateCommand>(ref queryEnabledStateCommand) >= 0L)
				{
					if (queryEnabledStateCommand.isEnabled)
					{
						this.m_DeviceFlags &= ~InputDevice.DeviceFlags.DisabledInRuntime;
					}
					else
					{
						this.m_DeviceFlags |= InputDevice.DeviceFlags.DisabledInRuntime;
					}
				}
				else
				{
					this.m_DeviceFlags &= ~InputDevice.DeviceFlags.DisabledInRuntime;
				}
				this.m_DeviceFlags |= InputDevice.DeviceFlags.DisabledStateHasBeenQueriedFromRuntime;
			}
			return (this.m_DeviceFlags & InputDevice.DeviceFlags.DisabledInRuntime) == (InputDevice.DeviceFlags)0;
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060004A3 RID: 1187 RVA: 0x000134D6 File Offset: 0x000116D6
		// (set) Token: 0x060004A4 RID: 1188 RVA: 0x000134E4 File Offset: 0x000116E4
		internal bool disabledInFrontend
		{
			get
			{
				return (this.m_DeviceFlags & InputDevice.DeviceFlags.DisabledInFrontend) > (InputDevice.DeviceFlags)0;
			}
			set
			{
				if (value)
				{
					this.m_DeviceFlags |= InputDevice.DeviceFlags.DisabledInFrontend;
					return;
				}
				this.m_DeviceFlags &= ~InputDevice.DeviceFlags.DisabledInFrontend;
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060004A5 RID: 1189 RVA: 0x00013508 File Offset: 0x00011708
		// (set) Token: 0x060004A6 RID: 1190 RVA: 0x00013519 File Offset: 0x00011719
		internal bool disabledInRuntime
		{
			get
			{
				return (this.m_DeviceFlags & InputDevice.DeviceFlags.DisabledInRuntime) > (InputDevice.DeviceFlags)0;
			}
			set
			{
				if (value)
				{
					this.m_DeviceFlags |= InputDevice.DeviceFlags.DisabledInRuntime;
					return;
				}
				this.m_DeviceFlags &= ~InputDevice.DeviceFlags.DisabledInRuntime;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060004A7 RID: 1191 RVA: 0x00013543 File Offset: 0x00011743
		// (set) Token: 0x060004A8 RID: 1192 RVA: 0x00013554 File Offset: 0x00011754
		internal bool disabledWhileInBackground
		{
			get
			{
				return (this.m_DeviceFlags & InputDevice.DeviceFlags.DisabledWhileInBackground) > (InputDevice.DeviceFlags)0;
			}
			set
			{
				if (value)
				{
					this.m_DeviceFlags |= InputDevice.DeviceFlags.DisabledWhileInBackground;
					return;
				}
				this.m_DeviceFlags &= ~InputDevice.DeviceFlags.DisabledWhileInBackground;
			}
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x0001357E File Offset: 0x0001177E
		internal static uint EncodeStateOffsetToControlMapEntry(uint controlIndex, uint stateOffsetInBits, uint stateSizeInBits)
		{
			return (stateOffsetInBits << 19) | (stateSizeInBits << 10) | controlIndex;
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x0001358B File Offset: 0x0001178B
		internal static void DecodeStateOffsetToControlMapEntry(uint entry, out uint controlIndex, out uint stateOffset, out uint stateSize)
		{
			controlIndex = entry & 1023U;
			stateOffset = entry >> 19;
			stateSize = (entry >> 10) & 511U;
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060004AB RID: 1195 RVA: 0x000135A8 File Offset: 0x000117A8
		// (set) Token: 0x060004AC RID: 1196 RVA: 0x000135B5 File Offset: 0x000117B5
		internal bool hasControlsWithDefaultState
		{
			get
			{
				return (this.m_DeviceFlags & InputDevice.DeviceFlags.HasControlsWithDefaultState) == InputDevice.DeviceFlags.HasControlsWithDefaultState;
			}
			set
			{
				if (value)
				{
					this.m_DeviceFlags |= InputDevice.DeviceFlags.HasControlsWithDefaultState;
					return;
				}
				this.m_DeviceFlags &= ~InputDevice.DeviceFlags.HasControlsWithDefaultState;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060004AD RID: 1197 RVA: 0x000135D8 File Offset: 0x000117D8
		// (set) Token: 0x060004AE RID: 1198 RVA: 0x000135ED File Offset: 0x000117ED
		internal bool hasDontResetControls
		{
			get
			{
				return (this.m_DeviceFlags & InputDevice.DeviceFlags.HasDontResetControls) == InputDevice.DeviceFlags.HasDontResetControls;
			}
			set
			{
				if (value)
				{
					this.m_DeviceFlags |= InputDevice.DeviceFlags.HasDontResetControls;
					return;
				}
				this.m_DeviceFlags &= ~InputDevice.DeviceFlags.HasDontResetControls;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060004AF RID: 1199 RVA: 0x00013617 File Offset: 0x00011817
		// (set) Token: 0x060004B0 RID: 1200 RVA: 0x00013624 File Offset: 0x00011824
		internal bool hasStateCallbacks
		{
			get
			{
				return (this.m_DeviceFlags & InputDevice.DeviceFlags.HasStateCallbacks) == InputDevice.DeviceFlags.HasStateCallbacks;
			}
			set
			{
				if (value)
				{
					this.m_DeviceFlags |= InputDevice.DeviceFlags.HasStateCallbacks;
					return;
				}
				this.m_DeviceFlags &= ~InputDevice.DeviceFlags.HasStateCallbacks;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060004B1 RID: 1201 RVA: 0x00013647 File Offset: 0x00011847
		// (set) Token: 0x060004B2 RID: 1202 RVA: 0x0001365C File Offset: 0x0001185C
		internal bool hasEventMerger
		{
			get
			{
				return (this.m_DeviceFlags & InputDevice.DeviceFlags.HasEventMerger) == InputDevice.DeviceFlags.HasEventMerger;
			}
			set
			{
				if (value)
				{
					this.m_DeviceFlags |= InputDevice.DeviceFlags.HasEventMerger;
					return;
				}
				this.m_DeviceFlags &= ~InputDevice.DeviceFlags.HasEventMerger;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x00013686 File Offset: 0x00011886
		// (set) Token: 0x060004B4 RID: 1204 RVA: 0x0001369B File Offset: 0x0001189B
		internal bool hasEventPreProcessor
		{
			get
			{
				return (this.m_DeviceFlags & InputDevice.DeviceFlags.HasEventPreProcessor) == InputDevice.DeviceFlags.HasEventPreProcessor;
			}
			set
			{
				if (value)
				{
					this.m_DeviceFlags |= InputDevice.DeviceFlags.HasEventPreProcessor;
					return;
				}
				this.m_DeviceFlags &= ~InputDevice.DeviceFlags.HasEventPreProcessor;
			}
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x000136C8 File Offset: 0x000118C8
		internal void AddDeviceUsage(InternedString usage)
		{
			int num = this.m_UsageToControl.LengthSafe<InputControl>() + this.m_UsageCount;
			if (this.m_UsageCount == 0)
			{
				this.m_UsageStartIndex = num;
			}
			ArrayHelpers.AppendWithCapacity<InternedString>(ref this.m_UsagesForEachControl, ref num, usage, 10);
			this.m_UsageCount++;
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x00013718 File Offset: 0x00011918
		internal void RemoveDeviceUsage(InternedString usage)
		{
			int num = this.m_UsageToControl.LengthSafe<InputControl>() + this.m_UsageCount;
			int num2 = this.m_UsagesForEachControl.IndexOfValue(usage, this.m_UsageStartIndex, num);
			if (num2 == -1)
			{
				return;
			}
			this.m_UsagesForEachControl.EraseAtWithCapacity(ref num, num2);
			this.m_UsageCount--;
			if (this.m_UsageCount == 0)
			{
				this.m_UsageStartIndex = 0;
			}
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x0001377C File Offset: 0x0001197C
		internal void ClearDeviceUsages()
		{
			for (int i = this.m_UsageStartIndex; i < this.m_UsageCount; i++)
			{
				this.m_UsagesForEachControl[i] = default(InternedString);
			}
			this.m_UsageCount = 0;
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x000137B8 File Offset: 0x000119B8
		internal bool RequestSync()
		{
			base.SetOptimizedControlDataTypeRecursively();
			RequestSyncCommand requestSyncCommand = RequestSyncCommand.Create();
			return base.device.ExecuteCommand<RequestSyncCommand>(ref requestSyncCommand) >= 0L;
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x000137E8 File Offset: 0x000119E8
		internal bool RequestReset()
		{
			base.SetOptimizedControlDataTypeRecursively();
			RequestResetCommand requestResetCommand = RequestResetCommand.Create();
			return base.device.ExecuteCommand<RequestResetCommand>(ref requestResetCommand) >= 0L;
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x00013818 File Offset: 0x00011A18
		internal bool ExecuteEnableCommand()
		{
			base.SetOptimizedControlDataTypeRecursively();
			EnableDeviceCommand enableDeviceCommand = EnableDeviceCommand.Create();
			return base.device.ExecuteCommand<EnableDeviceCommand>(ref enableDeviceCommand) >= 0L;
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x00013848 File Offset: 0x00011A48
		internal bool ExecuteDisableCommand()
		{
			DisableDeviceCommand disableDeviceCommand = DisableDeviceCommand.Create();
			return base.device.ExecuteCommand<DisableDeviceCommand>(ref disableDeviceCommand) >= 0L;
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x0001386F File Offset: 0x00011A6F
		internal void NotifyAdded()
		{
			this.OnAdded();
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x00013877 File Offset: 0x00011A77
		internal void NotifyRemoved()
		{
			this.OnRemoved();
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x00013880 File Offset: 0x00011A80
		internal static TDevice Build<TDevice>(string layoutName = null, string layoutVariants = null, InputDeviceDescription deviceDescription = default(InputDeviceDescription), bool noPrecompiledLayouts = false) where TDevice : InputDevice
		{
			InternedString internedString = new InternedString(layoutName);
			if (internedString.IsEmpty())
			{
				internedString = InputControlLayout.s_Layouts.TryFindLayoutForType(typeof(TDevice));
				if (internedString.IsEmpty())
				{
					internedString = new InternedString(typeof(TDevice).Name);
				}
			}
			InputControlLayout.Collection.PrecompiledLayout precompiledLayout;
			if (!noPrecompiledLayouts && string.IsNullOrEmpty(layoutVariants) && InputControlLayout.s_Layouts.precompiledLayouts.TryGetValue(internedString, out precompiledLayout))
			{
				return (TDevice)((object)precompiledLayout.factoryMethod());
			}
			TDevice tdevice2;
			using (InputDeviceBuilder.Ref())
			{
				InputDeviceBuilder.instance.Setup(internedString, new InternedString(layoutVariants), deviceDescription);
				InputDevice inputDevice = InputDeviceBuilder.instance.Finish();
				TDevice tdevice = inputDevice as TDevice;
				if (tdevice == null)
				{
					throw new ArgumentException(string.Concat(new string[]
					{
						"Expected device of type '",
						typeof(TDevice).Name,
						"' but got device of type '",
						inputDevice.GetType().Name,
						"' instead"
					}), "TDevice");
				}
				tdevice2 = tdevice;
			}
			return tdevice2;
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x000139B0 File Offset: 0x00011BB0
		internal unsafe void WriteChangedControlStates(byte* deviceStateBuffer, void* statePtr, uint stateSizeInBytes, uint stateOffsetInDevice)
		{
			if (this.m_ControlTreeNodes.Length == 0)
			{
				return;
			}
			if (this.m_StateBlock.sizeInBits != stateSizeInBytes * 8U)
			{
				if (this.m_ControlTreeNodes[0].leftChildIndex != -1)
				{
					this.WritePartialChangedControlStatesInternal(statePtr, stateSizeInBytes * 8U, stateOffsetInDevice * 8U, deviceStateBuffer, this.m_ControlTreeNodes[0], 0U);
					return;
				}
			}
			else if (this.m_ControlTreeNodes[0].leftChildIndex != -1)
			{
				this.WriteChangedControlStatesInternal(statePtr, stateSizeInBytes * 8U, deviceStateBuffer, this.m_ControlTreeNodes[0], 0U);
			}
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x00013A34 File Offset: 0x00011C34
		private unsafe void WritePartialChangedControlStatesInternal(void* statePtr, uint stateSizeInBits, uint stateOffsetInDeviceInBits, byte* deviceStatePtr, InputDevice.ControlBitRangeNode parentNode, uint startOffset)
		{
			InputDevice.ControlBitRangeNode controlBitRangeNode = this.m_ControlTreeNodes[(int)parentNode.leftChildIndex];
			if (Math.Max(stateOffsetInDeviceInBits, startOffset) <= Math.Min(stateOffsetInDeviceInBits + stateSizeInBits, (uint)controlBitRangeNode.endBitOffset))
			{
				int num = (int)(controlBitRangeNode.controlStartIndex + (ushort)controlBitRangeNode.controlCount);
				for (int i = (int)controlBitRangeNode.controlStartIndex; i < num; i++)
				{
					ushort num2 = this.m_ControlTreeIndices[i];
					this.m_ChildrenForEachControl[(int)num2].MarkAsStale();
				}
				if (controlBitRangeNode.leftChildIndex != -1)
				{
					this.WritePartialChangedControlStatesInternal(statePtr, stateSizeInBits, stateOffsetInDeviceInBits, deviceStatePtr, controlBitRangeNode, startOffset);
				}
			}
			InputDevice.ControlBitRangeNode controlBitRangeNode2 = this.m_ControlTreeNodes[(int)(parentNode.leftChildIndex + 1)];
			if (Math.Max(stateOffsetInDeviceInBits, (uint)controlBitRangeNode.endBitOffset) <= Math.Min(stateOffsetInDeviceInBits + stateSizeInBits, (uint)controlBitRangeNode2.endBitOffset))
			{
				int num3 = (int)(controlBitRangeNode2.controlStartIndex + (ushort)controlBitRangeNode2.controlCount);
				for (int j = (int)controlBitRangeNode2.controlStartIndex; j < num3; j++)
				{
					ushort num4 = this.m_ControlTreeIndices[j];
					this.m_ChildrenForEachControl[(int)num4].MarkAsStale();
				}
				if (controlBitRangeNode2.leftChildIndex != -1)
				{
					this.WritePartialChangedControlStatesInternal(statePtr, stateSizeInBits, stateOffsetInDeviceInBits, deviceStatePtr, controlBitRangeNode2, (uint)controlBitRangeNode.endBitOffset);
				}
			}
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x00013B44 File Offset: 0x00011D44
		private void DumpControlBitRangeNode(int nodeIndex, InputDevice.ControlBitRangeNode node, uint startOffset, uint sizeInBits, List<string> output)
		{
			List<string> list = new List<string>();
			for (int i = 0; i < (int)node.controlCount; i++)
			{
				ushort num = this.m_ControlTreeIndices[(int)node.controlStartIndex + i];
				InputControl inputControl = this.m_ChildrenForEachControl[(int)num];
				list.Add(inputControl.path);
			}
			string text = string.Join(", ", list);
			string text2 = ((node.leftChildIndex != -1) ? string.Format(" <{0}, {1}>", node.leftChildIndex, (int)(node.leftChildIndex + 1)) : "");
			output.Add(string.Format("{0} [{1}, {2}]{3}->{4}", new object[]
			{
				nodeIndex,
				startOffset,
				startOffset + sizeInBits,
				text2,
				text
			}));
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x00013C10 File Offset: 0x00011E10
		private void DumpControlTree(InputDevice.ControlBitRangeNode parentNode, uint startOffset, List<string> output)
		{
			InputDevice.ControlBitRangeNode controlBitRangeNode = this.m_ControlTreeNodes[(int)parentNode.leftChildIndex];
			InputDevice.ControlBitRangeNode controlBitRangeNode2 = this.m_ControlTreeNodes[(int)(parentNode.leftChildIndex + 1)];
			this.DumpControlBitRangeNode((int)parentNode.leftChildIndex, controlBitRangeNode, startOffset, (uint)controlBitRangeNode.endBitOffset - startOffset, output);
			this.DumpControlBitRangeNode((int)(parentNode.leftChildIndex + 1), controlBitRangeNode2, (uint)controlBitRangeNode.endBitOffset, (uint)(controlBitRangeNode2.endBitOffset - controlBitRangeNode.endBitOffset), output);
			if (controlBitRangeNode.leftChildIndex != -1)
			{
				this.DumpControlTree(controlBitRangeNode, startOffset, output);
			}
			if (controlBitRangeNode2.leftChildIndex != -1)
			{
				this.DumpControlTree(controlBitRangeNode2, (uint)controlBitRangeNode.endBitOffset, output);
			}
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x00013CA8 File Offset: 0x00011EA8
		internal string DumpControlTree()
		{
			List<string> list = new List<string>();
			this.DumpControlTree(this.m_ControlTreeNodes[0], 0U, list);
			return string.Join("\n", list);
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x00013CDC File Offset: 0x00011EDC
		private unsafe void WriteChangedControlStatesInternal(void* statePtr, uint stateSizeInBits, byte* deviceStatePtr, InputDevice.ControlBitRangeNode parentNode, uint startOffset)
		{
			InputDevice.ControlBitRangeNode controlBitRangeNode = this.m_ControlTreeNodes[(int)parentNode.leftChildIndex];
			if (InputDevice.HasDataChangedInRange(deviceStatePtr, statePtr, startOffset, (uint)controlBitRangeNode.endBitOffset - startOffset + 1U))
			{
				int num = (int)(controlBitRangeNode.controlStartIndex + (ushort)controlBitRangeNode.controlCount);
				for (int i = (int)controlBitRangeNode.controlStartIndex; i < num; i++)
				{
					ushort num2 = this.m_ControlTreeIndices[i];
					InputControl inputControl = this.m_ChildrenForEachControl[(int)num2];
					if (!inputControl.CompareState((void*)(deviceStatePtr - this.m_StateBlock.byteOffset), (void*)((byte*)statePtr - this.m_StateBlock.byteOffset), null))
					{
						inputControl.MarkAsStale();
					}
				}
				if (controlBitRangeNode.leftChildIndex != -1)
				{
					this.WriteChangedControlStatesInternal(statePtr, stateSizeInBits, deviceStatePtr, controlBitRangeNode, startOffset);
				}
			}
			InputDevice.ControlBitRangeNode controlBitRangeNode2 = this.m_ControlTreeNodes[(int)(parentNode.leftChildIndex + 1)];
			if (!InputDevice.HasDataChangedInRange(deviceStatePtr, statePtr, (uint)controlBitRangeNode.endBitOffset, (uint)(controlBitRangeNode2.endBitOffset - controlBitRangeNode.endBitOffset + 1)))
			{
				return;
			}
			int num3 = (int)(controlBitRangeNode2.controlStartIndex + (ushort)controlBitRangeNode2.controlCount);
			for (int j = (int)controlBitRangeNode2.controlStartIndex; j < num3; j++)
			{
				ushort num4 = this.m_ControlTreeIndices[j];
				InputControl inputControl2 = this.m_ChildrenForEachControl[(int)num4];
				if (!inputControl2.CompareState((void*)(deviceStatePtr - this.m_StateBlock.byteOffset), (void*)((byte*)statePtr - this.m_StateBlock.byteOffset), null))
				{
					inputControl2.MarkAsStale();
				}
			}
			if (controlBitRangeNode2.leftChildIndex != -1)
			{
				this.WriteChangedControlStatesInternal(statePtr, stateSizeInBits, deviceStatePtr, controlBitRangeNode2, (uint)controlBitRangeNode.endBitOffset);
			}
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x00013E42 File Offset: 0x00012042
		private unsafe static bool HasDataChangedInRange(byte* deviceStatePtr, void* statePtr, uint startOffset, uint sizeInBits)
		{
			if (sizeInBits == 1U)
			{
				return MemoryHelpers.ReadSingleBit((void*)deviceStatePtr, startOffset) != MemoryHelpers.ReadSingleBit(statePtr, startOffset);
			}
			return !MemoryHelpers.MemCmpBitRegion((void*)deviceStatePtr, statePtr, startOffset, sizeInBits, null);
		}

		// Token: 0x04000154 RID: 340
		public const int InvalidDeviceId = 0;

		// Token: 0x04000155 RID: 341
		internal const int kLocalParticipantId = 0;

		// Token: 0x04000156 RID: 342
		internal const int kInvalidDeviceIndex = -1;

		// Token: 0x04000157 RID: 343
		internal InputDevice.DeviceFlags m_DeviceFlags;

		// Token: 0x04000158 RID: 344
		internal int m_DeviceId;

		// Token: 0x04000159 RID: 345
		internal int m_ParticipantId;

		// Token: 0x0400015A RID: 346
		internal int m_DeviceIndex;

		// Token: 0x0400015B RID: 347
		internal InputDeviceDescription m_Description;

		// Token: 0x0400015C RID: 348
		internal double m_LastUpdateTimeInternal;

		// Token: 0x0400015D RID: 349
		internal uint m_CurrentUpdateStepCount;

		// Token: 0x0400015E RID: 350
		internal InternedString[] m_AliasesForEachControl;

		// Token: 0x0400015F RID: 351
		internal InternedString[] m_UsagesForEachControl;

		// Token: 0x04000160 RID: 352
		internal InputControl[] m_UsageToControl;

		// Token: 0x04000161 RID: 353
		internal InputControl[] m_ChildrenForEachControl;

		// Token: 0x04000162 RID: 354
		internal uint[] m_StateOffsetToControlMap;

		// Token: 0x04000163 RID: 355
		internal InputDevice.ControlBitRangeNode[] m_ControlTreeNodes;

		// Token: 0x04000164 RID: 356
		internal ushort[] m_ControlTreeIndices;

		// Token: 0x04000165 RID: 357
		internal const int kControlIndexBits = 10;

		// Token: 0x04000166 RID: 358
		internal const int kStateOffsetBits = 13;

		// Token: 0x04000167 RID: 359
		internal const int kStateSizeBits = 9;

		// Token: 0x02000191 RID: 401
		[Flags]
		[Serializable]
		internal enum DeviceFlags
		{
			// Token: 0x04000885 RID: 2181
			UpdateBeforeRender = 1,
			// Token: 0x04000886 RID: 2182
			HasStateCallbacks = 2,
			// Token: 0x04000887 RID: 2183
			HasControlsWithDefaultState = 4,
			// Token: 0x04000888 RID: 2184
			HasDontResetControls = 1024,
			// Token: 0x04000889 RID: 2185
			HasEventMerger = 8192,
			// Token: 0x0400088A RID: 2186
			HasEventPreProcessor = 16384,
			// Token: 0x0400088B RID: 2187
			Remote = 8,
			// Token: 0x0400088C RID: 2188
			Native = 16,
			// Token: 0x0400088D RID: 2189
			DisabledInFrontend = 32,
			// Token: 0x0400088E RID: 2190
			DisabledInRuntime = 128,
			// Token: 0x0400088F RID: 2191
			DisabledWhileInBackground = 256,
			// Token: 0x04000890 RID: 2192
			DisabledStateHasBeenQueriedFromRuntime = 64,
			// Token: 0x04000891 RID: 2193
			CanRunInBackground = 2048,
			// Token: 0x04000892 RID: 2194
			CanRunInBackgroundHasBeenQueried = 4096
		}

		// Token: 0x02000192 RID: 402
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		internal struct ControlBitRangeNode
		{
			// Token: 0x060013A0 RID: 5024 RVA: 0x0005AB57 File Offset: 0x00058D57
			public ControlBitRangeNode(ushort endOffset)
			{
				this.controlStartIndex = 0;
				this.controlCount = 0;
				this.endBitOffset = endOffset;
				this.leftChildIndex = -1;
			}

			// Token: 0x04000893 RID: 2195
			public ushort endBitOffset;

			// Token: 0x04000894 RID: 2196
			public short leftChildIndex;

			// Token: 0x04000895 RID: 2197
			public ushort controlStartIndex;

			// Token: 0x04000896 RID: 2198
			public byte controlCount;
		}
	}
}
