using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.InputSystem.Composites;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Processors;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000057 RID: 87
	internal class InputManager
	{
		// Token: 0x1700020D RID: 525
		// (get) Token: 0x0600082E RID: 2094 RVA: 0x0002D623 File Offset: 0x0002B823
		public ReadOnlyArray<InputDevice> devices
		{
			get
			{
				return new ReadOnlyArray<InputDevice>(this.m_Devices, 0, this.m_DevicesCount);
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x0600082F RID: 2095 RVA: 0x0002D637 File Offset: 0x0002B837
		public TypeTable processors
		{
			get
			{
				return this.m_Processors;
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000830 RID: 2096 RVA: 0x0002D63F File Offset: 0x0002B83F
		public TypeTable interactions
		{
			get
			{
				return this.m_Interactions;
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000831 RID: 2097 RVA: 0x0002D647 File Offset: 0x0002B847
		public TypeTable composites
		{
			get
			{
				return this.m_Composites;
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000832 RID: 2098 RVA: 0x0002D650 File Offset: 0x0002B850
		public InputMetrics metrics
		{
			get
			{
				InputMetrics metrics = this.m_Metrics;
				metrics.currentNumDevices = this.m_DevicesCount;
				metrics.currentStateSizeInBytes = (int)this.m_StateBuffers.totalSize;
				metrics.currentControlCount = this.m_DevicesCount;
				for (int i = 0; i < this.m_DevicesCount; i++)
				{
					metrics.currentControlCount += this.m_Devices[i].allControls.Count;
				}
				metrics.currentLayoutCount = this.m_Layouts.layoutTypes.Count;
				metrics.currentLayoutCount += this.m_Layouts.layoutStrings.Count;
				metrics.currentLayoutCount += this.m_Layouts.layoutBuilders.Count;
				metrics.currentLayoutCount += this.m_Layouts.layoutOverrides.Count;
				return metrics;
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000833 RID: 2099 RVA: 0x0002D736 File Offset: 0x0002B936
		// (set) Token: 0x06000834 RID: 2100 RVA: 0x0002D73E File Offset: 0x0002B93E
		public InputSettings settings
		{
			get
			{
				return this.m_Settings;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (this.m_Settings == value)
				{
					return;
				}
				this.m_Settings = value;
				this.ApplySettings();
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000835 RID: 2101 RVA: 0x0002D770 File Offset: 0x0002B970
		// (set) Token: 0x06000836 RID: 2102 RVA: 0x0002D778 File Offset: 0x0002B978
		public InputUpdateType updateMask
		{
			get
			{
				return this.m_UpdateMask;
			}
			set
			{
				if (this.m_UpdateMask == value)
				{
					return;
				}
				this.m_UpdateMask = value;
				if (this.m_DevicesCount > 0)
				{
					this.ReallocateStateBuffers();
				}
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000837 RID: 2103 RVA: 0x0002D79A File Offset: 0x0002B99A
		public InputUpdateType defaultUpdateType
		{
			get
			{
				if (this.m_CurrentUpdate != InputUpdateType.None)
				{
					return this.m_CurrentUpdate;
				}
				return this.m_UpdateMask.GetUpdateTypeForPlayer();
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000838 RID: 2104 RVA: 0x0002D7B6 File Offset: 0x0002B9B6
		// (set) Token: 0x06000839 RID: 2105 RVA: 0x0002D7BE File Offset: 0x0002B9BE
		public float pollingFrequency
		{
			get
			{
				return this.m_PollingFrequency;
			}
			set
			{
				if (value <= 0f)
				{
					throw new ArgumentException("Polling frequency must be greater than zero", "value");
				}
				this.m_PollingFrequency = value;
				if (this.m_Runtime != null)
				{
					this.m_Runtime.pollingFrequency = value;
				}
			}
		}

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x0600083A RID: 2106 RVA: 0x0002D7F3 File Offset: 0x0002B9F3
		// (remove) Token: 0x0600083B RID: 2107 RVA: 0x0002D801 File Offset: 0x0002BA01
		public event Action<InputDevice, InputDeviceChange> onDeviceChange
		{
			add
			{
				this.m_DeviceChangeListeners.AddCallback(value);
			}
			remove
			{
				this.m_DeviceChangeListeners.RemoveCallback(value);
			}
		}

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x0600083C RID: 2108 RVA: 0x0002D80F File Offset: 0x0002BA0F
		// (remove) Token: 0x0600083D RID: 2109 RVA: 0x0002D81D File Offset: 0x0002BA1D
		public event Action<InputDevice, InputEventPtr> onDeviceStateChange
		{
			add
			{
				this.m_DeviceStateChangeListeners.AddCallback(value);
			}
			remove
			{
				this.m_DeviceStateChangeListeners.RemoveCallback(value);
			}
		}

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x0600083E RID: 2110 RVA: 0x0002D82B File Offset: 0x0002BA2B
		// (remove) Token: 0x0600083F RID: 2111 RVA: 0x0002D839 File Offset: 0x0002BA39
		public event InputDeviceCommandDelegate onDeviceCommand
		{
			add
			{
				this.m_DeviceCommandCallbacks.AddCallback(value);
			}
			remove
			{
				this.m_DeviceCommandCallbacks.RemoveCallback(value);
			}
		}

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x06000840 RID: 2112 RVA: 0x0002D847 File Offset: 0x0002BA47
		// (remove) Token: 0x06000841 RID: 2113 RVA: 0x0002D85B File Offset: 0x0002BA5B
		public event InputDeviceFindControlLayoutDelegate onFindControlLayoutForDevice
		{
			add
			{
				this.m_DeviceFindLayoutCallbacks.AddCallback(value);
				this.AddAvailableDevicesThatAreNowRecognized();
			}
			remove
			{
				this.m_DeviceFindLayoutCallbacks.RemoveCallback(value);
			}
		}

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x06000842 RID: 2114 RVA: 0x0002D869 File Offset: 0x0002BA69
		// (remove) Token: 0x06000843 RID: 2115 RVA: 0x0002D877 File Offset: 0x0002BA77
		public event Action<string, InputControlLayoutChange> onLayoutChange
		{
			add
			{
				this.m_LayoutChangeListeners.AddCallback(value);
			}
			remove
			{
				this.m_LayoutChangeListeners.RemoveCallback(value);
			}
		}

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x06000844 RID: 2116 RVA: 0x0002D885 File Offset: 0x0002BA85
		// (remove) Token: 0x06000845 RID: 2117 RVA: 0x0002D893 File Offset: 0x0002BA93
		public event Action<InputEventPtr, InputDevice> onEvent
		{
			add
			{
				this.m_EventListeners.AddCallback(value);
			}
			remove
			{
				this.m_EventListeners.RemoveCallback(value);
			}
		}

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x06000846 RID: 2118 RVA: 0x0002D8A1 File Offset: 0x0002BAA1
		// (remove) Token: 0x06000847 RID: 2119 RVA: 0x0002D8B5 File Offset: 0x0002BAB5
		public event Action onBeforeUpdate
		{
			add
			{
				this.InstallBeforeUpdateHookIfNecessary();
				this.m_BeforeUpdateListeners.AddCallback(value);
			}
			remove
			{
				this.m_BeforeUpdateListeners.RemoveCallback(value);
			}
		}

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x06000848 RID: 2120 RVA: 0x0002D8C3 File Offset: 0x0002BAC3
		// (remove) Token: 0x06000849 RID: 2121 RVA: 0x0002D8D1 File Offset: 0x0002BAD1
		public event Action onAfterUpdate
		{
			add
			{
				this.m_AfterUpdateListeners.AddCallback(value);
			}
			remove
			{
				this.m_AfterUpdateListeners.RemoveCallback(value);
			}
		}

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x0600084A RID: 2122 RVA: 0x0002D8DF File Offset: 0x0002BADF
		// (remove) Token: 0x0600084B RID: 2123 RVA: 0x0002D8ED File Offset: 0x0002BAED
		public event Action onSettingsChange
		{
			add
			{
				this.m_SettingsChangedListeners.AddCallback(value);
			}
			remove
			{
				this.m_SettingsChangedListeners.RemoveCallback(value);
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x0600084C RID: 2124 RVA: 0x0002D8FB File Offset: 0x0002BAFB
		public bool isProcessingEvents
		{
			get
			{
				return this.m_InputEventStream.isOpen;
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x0600084D RID: 2125 RVA: 0x0002D908 File Offset: 0x0002BB08
		private bool gameIsPlaying
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x0600084E RID: 2126 RVA: 0x0002D90B File Offset: 0x0002BB0B
		private bool gameHasFocus
		{
			get
			{
				return this.m_HasFocus || this.gameShouldGetInputRegardlessOfFocus;
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x0600084F RID: 2127 RVA: 0x0002D91D File Offset: 0x0002BB1D
		private bool gameShouldGetInputRegardlessOfFocus
		{
			get
			{
				return this.m_Settings.backgroundBehavior == InputSettings.BackgroundBehavior.IgnoreFocus;
			}
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x0002D930 File Offset: 0x0002BB30
		public void RegisterControlLayout(string name, Type type)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			bool flag = typeof(InputDevice).IsAssignableFrom(type);
			bool flag2 = typeof(InputControl).IsAssignableFrom(type);
			if (!flag && !flag2)
			{
				throw new ArgumentException(string.Concat(new string[]
				{
					"Types used as layouts have to be InputControls or InputDevices; '",
					type.Name,
					"' is a '",
					type.BaseType.Name,
					"'"
				}), "type");
			}
			InternedString internedString = new InternedString(name);
			bool flag3 = this.m_Layouts.HasLayout(internedString);
			this.m_Layouts.layoutTypes[internedString] = type;
			string text = null;
			Type type2 = type.BaseType;
			while (text == null && type2 != typeof(InputControl))
			{
				foreach (KeyValuePair<InternedString, Type> keyValuePair in this.m_Layouts.layoutTypes)
				{
					if (keyValuePair.Value == type2)
					{
						text = keyValuePair.Key;
						break;
					}
				}
				type2 = type2.BaseType;
			}
			this.PerformLayoutPostRegistration(internedString, new InlinedArray<InternedString>(new InternedString(text)), flag3, flag, false);
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x0002DAA4 File Offset: 0x0002BCA4
		public void RegisterControlLayout(string json, string name = null, bool isOverride = false)
		{
			if (string.IsNullOrEmpty(json))
			{
				throw new ArgumentNullException("json");
			}
			InternedString internedString;
			InlinedArray<InternedString> inlinedArray;
			InputDeviceMatcher inputDeviceMatcher;
			InputControlLayout.ParseHeaderFieldsFromJson(json, out internedString, out inlinedArray, out inputDeviceMatcher);
			InternedString internedString2 = new InternedString(name);
			if (internedString2.IsEmpty())
			{
				internedString2 = internedString;
				if (internedString2.IsEmpty())
				{
					throw new ArgumentException("Layout name has not been given and is not set in JSON layout", "name");
				}
			}
			if (isOverride && inlinedArray.length == 0)
			{
				throw new ArgumentException(string.Format("Layout override '{0}' must have 'extend' property mentioning layout to which to apply the overrides", internedString2), "json");
			}
			bool flag = this.m_Layouts.HasLayout(internedString2);
			if (flag && isOverride && !this.m_Layouts.layoutOverrideNames.Contains(internedString2))
			{
				throw new ArgumentException(string.Format("Failed to register layout override '{0}'", internedString2) + string.Format("since a layout named '{0}' already exist. Layout overrides must ", internedString2) + "have unique names with respect to existing layouts.");
			}
			this.m_Layouts.layoutStrings[internedString2] = json;
			if (isOverride)
			{
				this.m_Layouts.layoutOverrideNames.Add(internedString2);
				for (int i = 0; i < inlinedArray.length; i++)
				{
					InternedString internedString3 = inlinedArray[i];
					InternedString[] array;
					this.m_Layouts.layoutOverrides.TryGetValue(internedString3, out array);
					if (!flag)
					{
						ArrayHelpers.Append<InternedString>(ref array, internedString2);
					}
					this.m_Layouts.layoutOverrides[internedString3] = array;
				}
			}
			this.PerformLayoutPostRegistration(internedString2, inlinedArray, flag, false, isOverride);
			if (!inputDeviceMatcher.empty)
			{
				this.RegisterControlLayoutMatcher(internedString2, inputDeviceMatcher);
			}
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x0002DC18 File Offset: 0x0002BE18
		public void RegisterControlLayoutBuilder(Func<InputControlLayout> method, string name, string baseLayout = null)
		{
			if (method == null)
			{
				throw new ArgumentNullException("method");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			InternedString internedString = new InternedString(name);
			InternedString internedString2 = new InternedString(baseLayout);
			bool flag = this.m_Layouts.HasLayout(internedString);
			this.m_Layouts.layoutBuilders[internedString] = method;
			this.PerformLayoutPostRegistration(internedString, new InlinedArray<InternedString>(internedString2), flag, false, false);
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x0002DC88 File Offset: 0x0002BE88
		private void PerformLayoutPostRegistration(InternedString layoutName, InlinedArray<InternedString> baseLayouts, bool isReplacement, bool isKnownToBeDeviceLayout = false, bool isOverride = false)
		{
			this.m_LayoutRegistrationVersion++;
			InputControlLayout.s_CacheInstance.Clear();
			if (!isOverride && baseLayouts.length > 0)
			{
				if (baseLayouts.length > 1)
				{
					throw new NotSupportedException(string.Format("Layout '{0}' has multiple base layouts; this is only supported on layout overrides", layoutName));
				}
				InternedString internedString = baseLayouts[0];
				if (!internedString.IsEmpty())
				{
					this.m_Layouts.baseLayoutTable[layoutName] = internedString;
				}
			}
			this.m_Layouts.precompiledLayouts.Remove(layoutName);
			if (this.m_Layouts.precompiledLayouts.Count > 0)
			{
				foreach (InternedString internedString2 in this.m_Layouts.precompiledLayouts.Keys.ToArray<InternedString>())
				{
					string metadata = this.m_Layouts.precompiledLayouts[internedString2].metadata;
					if (isOverride)
					{
						for (int j = 0; j < baseLayouts.length; j++)
						{
							if (internedString2 == baseLayouts[j] || StringHelpers.CharacterSeparatedListsHaveAtLeastOneCommonElement(metadata, baseLayouts[j], ';'))
							{
								this.m_Layouts.precompiledLayouts.Remove(internedString2);
							}
						}
					}
					else if (StringHelpers.CharacterSeparatedListsHaveAtLeastOneCommonElement(metadata, layoutName, ';'))
					{
						this.m_Layouts.precompiledLayouts.Remove(internedString2);
					}
				}
			}
			if (isOverride)
			{
				for (int k = 0; k < baseLayouts.length; k++)
				{
					this.RecreateDevicesUsingLayout(baseLayouts[k], isKnownToBeDeviceLayout);
				}
			}
			else
			{
				this.RecreateDevicesUsingLayout(layoutName, isKnownToBeDeviceLayout);
			}
			InputControlLayoutChange inputControlLayoutChange = (isReplacement ? InputControlLayoutChange.Replaced : InputControlLayoutChange.Added);
			DelegateHelpers.InvokeCallbacksSafe<string, InputControlLayoutChange>(ref this.m_LayoutChangeListeners, layoutName.ToString(), inputControlLayoutChange, "InputSystem.onLayoutChange", null);
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x0002DE4C File Offset: 0x0002C04C
		public void RegisterPrecompiledLayout<TDevice>(string metadata) where TDevice : InputDevice, new()
		{
			if (metadata == null)
			{
				throw new ArgumentNullException("metadata");
			}
			Type baseType = typeof(TDevice).BaseType;
			InternedString internedString = this.FindOrRegisterDeviceLayoutForType(baseType);
			Dictionary<InternedString, InputControlLayout.Collection.PrecompiledLayout> precompiledLayouts = this.m_Layouts.precompiledLayouts;
			InternedString internedString2 = internedString;
			InputControlLayout.Collection.PrecompiledLayout precompiledLayout = default(InputControlLayout.Collection.PrecompiledLayout);
			precompiledLayout.factoryMethod = () => new TDevice();
			precompiledLayout.metadata = metadata;
			precompiledLayouts[internedString2] = precompiledLayout;
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x0002DEC8 File Offset: 0x0002C0C8
		private void RecreateDevicesUsingLayout(InternedString layout, bool isKnownToBeDeviceLayout = false)
		{
			if (this.m_DevicesCount == 0)
			{
				return;
			}
			List<InputDevice> list = null;
			for (int i = 0; i < this.m_DevicesCount; i++)
			{
				InputDevice inputDevice = this.m_Devices[i];
				bool flag;
				if (isKnownToBeDeviceLayout)
				{
					flag = this.IsControlUsingLayout(inputDevice, layout);
				}
				else
				{
					flag = this.IsControlOrChildUsingLayoutRecursive(inputDevice, layout);
				}
				if (flag)
				{
					if (list == null)
					{
						list = new List<InputDevice>();
					}
					list.Add(inputDevice);
				}
			}
			if (list == null)
			{
				return;
			}
			using (InputDeviceBuilder.Ref())
			{
				for (int j = 0; j < list.Count; j++)
				{
					InputDevice inputDevice2 = list[j];
					this.RecreateDevice(inputDevice2, inputDevice2.m_Layout);
				}
			}
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x0002DF80 File Offset: 0x0002C180
		private bool IsControlOrChildUsingLayoutRecursive(InputControl control, InternedString layout)
		{
			if (this.IsControlUsingLayout(control, layout))
			{
				return true;
			}
			ReadOnlyArray<InputControl> children = control.children;
			for (int i = 0; i < children.Count; i++)
			{
				if (this.IsControlOrChildUsingLayoutRecursive(children[i], layout))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x0002DFC8 File Offset: 0x0002C1C8
		private bool IsControlUsingLayout(InputControl control, InternedString layout)
		{
			if (control.layout == layout)
			{
				return true;
			}
			InternedString layout2 = control.m_Layout;
			while (this.m_Layouts.baseLayoutTable.TryGetValue(layout2, out layout2))
			{
				if (layout2 == layout)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x0002E010 File Offset: 0x0002C210
		public void RegisterControlLayoutMatcher(string layoutName, InputDeviceMatcher matcher)
		{
			if (string.IsNullOrEmpty(layoutName))
			{
				throw new ArgumentNullException("layoutName");
			}
			if (matcher.empty)
			{
				throw new ArgumentException("Matcher cannot be empty", "matcher");
			}
			InternedString internedString = new InternedString(layoutName);
			this.m_Layouts.AddMatcher(internedString, matcher);
			this.RecreateDevicesUsingLayoutWithInferiorMatch(matcher);
			this.AddAvailableDevicesMatchingDescription(matcher, internedString);
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x0002E070 File Offset: 0x0002C270
		public void RegisterControlLayoutMatcher(Type type, InputDeviceMatcher matcher)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (matcher.empty)
			{
				throw new ArgumentException("Matcher cannot be empty", "matcher");
			}
			InternedString internedString = this.m_Layouts.TryFindLayoutForType(type);
			if (internedString.IsEmpty())
			{
				throw new ArgumentException("Type '" + type.Name + "' has not been registered as a control layout", "type");
			}
			this.RegisterControlLayoutMatcher(internedString, matcher);
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x0002E0F0 File Offset: 0x0002C2F0
		private void RecreateDevicesUsingLayoutWithInferiorMatch(InputDeviceMatcher deviceMatcher)
		{
			if (this.m_DevicesCount == 0)
			{
				return;
			}
			using (InputDeviceBuilder.Ref())
			{
				int num = this.m_DevicesCount;
				for (int i = 0; i < num; i++)
				{
					InputDevice inputDevice = this.m_Devices[i];
					InputDeviceDescription description = inputDevice.description;
					if (!description.empty && deviceMatcher.MatchPercentage(description) > 0f)
					{
						InternedString internedString = this.TryFindMatchingControlLayout(ref description, inputDevice.deviceId);
						if (internedString != inputDevice.m_Layout)
						{
							inputDevice.m_Description = description;
							this.RecreateDevice(inputDevice, internedString);
							i--;
							num--;
						}
					}
				}
			}
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x0002E1A4 File Offset: 0x0002C3A4
		private void RecreateDevice(InputDevice oldDevice, InternedString newLayout)
		{
			this.RemoveDevice(oldDevice, true);
			InputDevice inputDevice = InputDevice.Build<InputDevice>(newLayout, oldDevice.m_Variants, oldDevice.m_Description, false);
			inputDevice.m_DeviceId = oldDevice.m_DeviceId;
			inputDevice.m_Description = oldDevice.m_Description;
			if (oldDevice.native)
			{
				inputDevice.m_DeviceFlags |= InputDevice.DeviceFlags.Native;
			}
			if (oldDevice.remote)
			{
				inputDevice.m_DeviceFlags |= InputDevice.DeviceFlags.Remote;
			}
			if (!oldDevice.enabled)
			{
				inputDevice.m_DeviceFlags |= InputDevice.DeviceFlags.DisabledStateHasBeenQueriedFromRuntime;
				inputDevice.m_DeviceFlags |= InputDevice.DeviceFlags.DisabledInFrontend;
			}
			this.AddDevice(inputDevice);
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x0002E24C File Offset: 0x0002C44C
		private void AddAvailableDevicesMatchingDescription(InputDeviceMatcher matcher, InternedString layout)
		{
			for (int i = 0; i < this.m_AvailableDeviceCount; i++)
			{
				if (!this.m_AvailableDevices[i].isRemoved)
				{
					int deviceId = this.m_AvailableDevices[i].deviceId;
					if (this.TryGetDeviceById(deviceId) == null && matcher.MatchPercentage(this.m_AvailableDevices[i].description) > 0f)
					{
						try
						{
							this.AddDevice(layout, deviceId, null, this.m_AvailableDevices[i].description, this.m_AvailableDevices[i].isNative ? InputDevice.DeviceFlags.Native : ((InputDevice.DeviceFlags)0), default(InternedString));
						}
						catch (Exception ex)
						{
							Debug.LogError(string.Format("Layout '{0}' matches existing device '{1}' but failed to instantiate: {2}", layout, this.m_AvailableDevices[i].description, ex));
							Debug.LogException(ex);
							goto IL_00E8;
						}
						EnableDeviceCommand enableDeviceCommand = EnableDeviceCommand.Create();
						this.m_Runtime.DeviceCommand(deviceId, ref enableDeviceCommand);
					}
				}
				IL_00E8:;
			}
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x0002E364 File Offset: 0x0002C564
		public void RemoveControlLayout(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			InternedString internedString = new InternedString(name);
			int i = 0;
			while (i < this.m_DevicesCount)
			{
				InputDevice inputDevice = this.m_Devices[i];
				if (this.IsControlOrChildUsingLayoutRecursive(inputDevice, internedString))
				{
					this.RemoveDevice(inputDevice, true);
				}
				else
				{
					i++;
				}
			}
			this.m_Layouts.layoutTypes.Remove(internedString);
			this.m_Layouts.layoutStrings.Remove(internedString);
			this.m_Layouts.layoutBuilders.Remove(internedString);
			this.m_Layouts.baseLayoutTable.Remove(internedString);
			this.m_LayoutRegistrationVersion++;
			DelegateHelpers.InvokeCallbacksSafe<string, InputControlLayoutChange>(ref this.m_LayoutChangeListeners, name, InputControlLayoutChange.Removed, "InputSystem.onLayoutChange", null);
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x0002E424 File Offset: 0x0002C624
		public InputControlLayout TryLoadControlLayout(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (!typeof(InputControl).IsAssignableFrom(type))
			{
				throw new ArgumentException("Type '" + type.Name + "' is not an InputControl", "type");
			}
			InternedString internedString = this.m_Layouts.TryFindLayoutForType(type);
			if (internedString.IsEmpty())
			{
				throw new ArgumentException("Type '" + type.Name + "' has not been registered as a control layout", "type");
			}
			return this.m_Layouts.TryLoadLayout(internedString, null);
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x0002E4BA File Offset: 0x0002C6BA
		public InputControlLayout TryLoadControlLayout(InternedString name)
		{
			return this.m_Layouts.TryLoadLayout(name, null);
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x0002E4CC File Offset: 0x0002C6CC
		public InternedString TryFindMatchingControlLayout(ref InputDeviceDescription deviceDescription, int deviceId = 0)
		{
			InternedString internedString = this.m_Layouts.TryFindMatchingLayout(deviceDescription);
			if (internedString.IsEmpty() && !string.IsNullOrEmpty(deviceDescription.deviceClass))
			{
				InternedString internedString2 = new InternedString(deviceDescription.deviceClass);
				Type controlTypeForLayout = this.m_Layouts.GetControlTypeForLayout(internedString2);
				if (controlTypeForLayout != null && typeof(InputDevice).IsAssignableFrom(controlTypeForLayout))
				{
					internedString = new InternedString(deviceDescription.deviceClass);
				}
			}
			if (this.m_DeviceFindLayoutCallbacks.length > 0)
			{
				if (this.m_DeviceFindExecuteCommandDelegate == null)
				{
					this.m_DeviceFindExecuteCommandDelegate = delegate(ref InputDeviceCommand commandRef)
					{
						if (this.m_DeviceFindExecuteCommandDeviceId == 0)
						{
							return -1L;
						}
						return this.m_Runtime.DeviceCommand(this.m_DeviceFindExecuteCommandDeviceId, ref commandRef);
					};
				}
				this.m_DeviceFindExecuteCommandDeviceId = deviceId;
				bool flag = false;
				this.m_DeviceFindLayoutCallbacks.LockForChanges();
				for (int i = 0; i < this.m_DeviceFindLayoutCallbacks.length; i++)
				{
					try
					{
						string text = this.m_DeviceFindLayoutCallbacks[i](ref deviceDescription, internedString, this.m_DeviceFindExecuteCommandDelegate);
						if (!string.IsNullOrEmpty(text) && !flag)
						{
							internedString = new InternedString(text);
							flag = true;
						}
					}
					catch (Exception ex)
					{
						Debug.LogError(ex.GetType().Name + " while executing 'InputSystem.onFindLayoutForDevice' callbacks");
						Debug.LogException(ex);
					}
				}
				this.m_DeviceFindLayoutCallbacks.UnlockForChanges();
			}
			return internedString;
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x0002E614 File Offset: 0x0002C814
		private InternedString FindOrRegisterDeviceLayoutForType(Type type)
		{
			InternedString internedString = this.m_Layouts.TryFindLayoutForType(type);
			if (internedString.IsEmpty() && internedString.IsEmpty())
			{
				internedString = new InternedString(type.Name);
				this.RegisterControlLayout(type.Name, type);
			}
			return internedString;
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x0002E65C File Offset: 0x0002C85C
		private bool IsDeviceLayoutMarkedAsSupportedInSettings(InternedString layoutName)
		{
			ReadOnlyArray<string> supportedDevices = this.m_Settings.supportedDevices;
			if (supportedDevices.Count == 0)
			{
				return true;
			}
			for (int i = 0; i < supportedDevices.Count; i++)
			{
				InternedString internedString = new InternedString(supportedDevices[i]);
				if (layoutName == internedString || this.m_Layouts.IsBasedOn(internedString, layoutName))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x0002E6BC File Offset: 0x0002C8BC
		public IEnumerable<string> ListControlLayouts(string basedOn = null)
		{
			if (!string.IsNullOrEmpty(basedOn))
			{
				InternedString internedBasedOn = new InternedString(basedOn);
				foreach (KeyValuePair<InternedString, Type> keyValuePair in this.m_Layouts.layoutTypes)
				{
					if (this.m_Layouts.IsBasedOn(internedBasedOn, keyValuePair.Key))
					{
						yield return keyValuePair.Key;
					}
				}
				Dictionary<InternedString, Type>.Enumerator enumerator = default(Dictionary<InternedString, Type>.Enumerator);
				foreach (KeyValuePair<InternedString, string> keyValuePair2 in this.m_Layouts.layoutStrings)
				{
					if (this.m_Layouts.IsBasedOn(internedBasedOn, keyValuePair2.Key))
					{
						yield return keyValuePair2.Key;
					}
				}
				Dictionary<InternedString, string>.Enumerator enumerator2 = default(Dictionary<InternedString, string>.Enumerator);
				foreach (KeyValuePair<InternedString, Func<InputControlLayout>> keyValuePair3 in this.m_Layouts.layoutBuilders)
				{
					if (this.m_Layouts.IsBasedOn(internedBasedOn, keyValuePair3.Key))
					{
						yield return keyValuePair3.Key;
					}
				}
				Dictionary<InternedString, Func<InputControlLayout>>.Enumerator enumerator3 = default(Dictionary<InternedString, Func<InputControlLayout>>.Enumerator);
				internedBasedOn = default(InternedString);
			}
			else
			{
				foreach (KeyValuePair<InternedString, Type> keyValuePair4 in this.m_Layouts.layoutTypes)
				{
					yield return keyValuePair4.Key;
				}
				Dictionary<InternedString, Type>.Enumerator enumerator = default(Dictionary<InternedString, Type>.Enumerator);
				foreach (KeyValuePair<InternedString, string> keyValuePair5 in this.m_Layouts.layoutStrings)
				{
					yield return keyValuePair5.Key;
				}
				Dictionary<InternedString, string>.Enumerator enumerator2 = default(Dictionary<InternedString, string>.Enumerator);
				foreach (KeyValuePair<InternedString, Func<InputControlLayout>> keyValuePair6 in this.m_Layouts.layoutBuilders)
				{
					yield return keyValuePair6.Key;
				}
				Dictionary<InternedString, Func<InputControlLayout>>.Enumerator enumerator3 = default(Dictionary<InternedString, Func<InputControlLayout>>.Enumerator);
			}
			yield break;
			yield break;
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x0002E6D4 File Offset: 0x0002C8D4
		public int GetControls<TControl>(string path, ref InputControlList<TControl> controls) where TControl : InputControl
		{
			if (string.IsNullOrEmpty(path))
			{
				return 0;
			}
			if (this.m_DevicesCount == 0)
			{
				return 0;
			}
			int devicesCount = this.m_DevicesCount;
			int num = 0;
			for (int i = 0; i < devicesCount; i++)
			{
				InputDevice inputDevice = this.m_Devices[i];
				num += InputControlPath.TryFindControls<TControl>(inputDevice, path, 0, ref controls);
			}
			return num;
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x0002E720 File Offset: 0x0002C920
		public void SetDeviceUsage(InputDevice device, InternedString usage)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			if (device.usages.Count == 1 && device.usages[0] == usage)
			{
				return;
			}
			if (device.usages.Count == 0 && usage.IsEmpty())
			{
				return;
			}
			device.ClearDeviceUsages();
			if (!usage.IsEmpty())
			{
				device.AddDeviceUsage(usage);
			}
			this.NotifyUsageChanged(device);
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x0002E79C File Offset: 0x0002C99C
		public void AddDeviceUsage(InputDevice device, InternedString usage)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			if (usage.IsEmpty())
			{
				throw new ArgumentException("Usage string cannot be empty", "usage");
			}
			if (device.usages.Contains(usage))
			{
				return;
			}
			device.AddDeviceUsage(usage);
			this.NotifyUsageChanged(device);
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x0002E7F0 File Offset: 0x0002C9F0
		public void RemoveDeviceUsage(InputDevice device, InternedString usage)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			if (usage.IsEmpty())
			{
				throw new ArgumentException("Usage string cannot be empty", "usage");
			}
			if (!device.usages.Contains(usage))
			{
				return;
			}
			device.RemoveDeviceUsage(usage);
			this.NotifyUsageChanged(device);
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x0002E841 File Offset: 0x0002CA41
		private void NotifyUsageChanged(InputDevice device)
		{
			InputActionState.OnDeviceChange(device, InputDeviceChange.UsageChanged);
			DelegateHelpers.InvokeCallbacksSafe<InputDevice, InputDeviceChange>(ref this.m_DeviceChangeListeners, device, InputDeviceChange.UsageChanged, "InputSystem.onDeviceChange", null);
			device.MakeCurrent();
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x0002E864 File Offset: 0x0002CA64
		public InputDevice AddDevice(Type type, string name = null)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			InternedString internedString = this.FindOrRegisterDeviceLayoutForType(type);
			return this.AddDevice(internedString, name, default(InternedString));
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x0002E8A4 File Offset: 0x0002CAA4
		public InputDevice AddDevice(string layout, string name = null, InternedString variants = default(InternedString))
		{
			if (string.IsNullOrEmpty(layout))
			{
				throw new ArgumentNullException("layout");
			}
			InputDevice inputDevice = InputDevice.Build<InputDevice>(layout, variants, default(InputDeviceDescription), false);
			if (!string.IsNullOrEmpty(name))
			{
				inputDevice.m_Name = new InternedString(name);
			}
			this.AddDevice(inputDevice);
			return inputDevice;
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x0002E8F8 File Offset: 0x0002CAF8
		private InputDevice AddDevice(InternedString layout, int deviceId, string deviceName = null, InputDeviceDescription deviceDescription = default(InputDeviceDescription), InputDevice.DeviceFlags deviceFlags = (InputDevice.DeviceFlags)0, InternedString variants = default(InternedString))
		{
			string text = new InternedString(layout);
			InputDeviceDescription inputDeviceDescription = deviceDescription;
			InputDevice inputDevice = InputDevice.Build<InputDevice>(text, variants, inputDeviceDescription, false);
			inputDevice.m_DeviceId = deviceId;
			inputDevice.m_Description = deviceDescription;
			inputDevice.m_DeviceFlags |= deviceFlags;
			if (!string.IsNullOrEmpty(deviceName))
			{
				inputDevice.m_Name = new InternedString(deviceName);
			}
			if (!string.IsNullOrEmpty(deviceDescription.product))
			{
				inputDevice.m_DisplayName = deviceDescription.product;
			}
			this.AddDevice(inputDevice);
			return inputDevice;
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x0002E97C File Offset: 0x0002CB7C
		public void AddDevice(InputDevice device)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			if (string.IsNullOrEmpty(device.layout))
			{
				throw new InvalidOperationException("Device has no associated layout");
			}
			if (ArrayHelpers.Contains<InputDevice>(this.m_Devices, device))
			{
				return;
			}
			this.MakeDeviceNameUnique(device);
			this.AssignUniqueDeviceId(device);
			device.m_DeviceIndex = ArrayHelpers.AppendWithCapacity<InputDevice>(ref this.m_Devices, ref this.m_DevicesCount, device, 10);
			this.m_DevicesById[device.deviceId] = device;
			device.m_StateBlock.byteOffset = uint.MaxValue;
			this.ReallocateStateBuffers();
			this.InitializeDeviceState(device);
			this.m_Metrics.maxNumDevices = Mathf.Max(this.m_DevicesCount, this.m_Metrics.maxNumDevices);
			this.m_Metrics.maxStateSizeInBytes = Mathf.Max((int)this.m_StateBuffers.totalSize, this.m_Metrics.maxStateSizeInBytes);
			for (int i = 0; i < this.m_AvailableDeviceCount; i++)
			{
				if (this.m_AvailableDevices[i].deviceId == device.deviceId)
				{
					this.m_AvailableDevices[i].isRemoved = false;
				}
			}
			if (true && !this.gameHasFocus && this.m_Settings.backgroundBehavior != InputSettings.BackgroundBehavior.IgnoreFocus && this.m_Runtime.runInBackground && device.QueryEnabledStateFromRuntime() && !this.ShouldRunDeviceInBackground(device))
			{
				this.EnableOrDisableDevice(device, false, InputManager.DeviceDisableScope.TemporaryWhilePlayerIsInBackground);
			}
			InputActionState.OnDeviceChange(device, InputDeviceChange.Added);
			IInputUpdateCallbackReceiver inputUpdateCallbackReceiver = device as IInputUpdateCallbackReceiver;
			if (inputUpdateCallbackReceiver != null)
			{
				this.onBeforeUpdate += inputUpdateCallbackReceiver.OnUpdate;
			}
			if (device is IInputStateCallbackReceiver)
			{
				this.InstallBeforeUpdateHookIfNecessary();
				device.m_DeviceFlags |= InputDevice.DeviceFlags.HasStateCallbacks;
				this.m_HaveDevicesWithStateCallbackReceivers = true;
			}
			if (device is IEventMerger)
			{
				device.hasEventMerger = true;
			}
			if (device is IEventPreProcessor)
			{
				device.hasEventPreProcessor = true;
			}
			if (device.updateBeforeRender)
			{
				this.updateMask |= InputUpdateType.BeforeRender;
			}
			device.NotifyAdded();
			device.MakeCurrent();
			DelegateHelpers.InvokeCallbacksSafe<InputDevice, InputDeviceChange>(ref this.m_DeviceChangeListeners, device, InputDeviceChange.Added, "InputSystem.onDeviceChange", null);
			if (device.enabled)
			{
				device.RequestSync();
			}
			device.SetOptimizedControlDataTypeRecursively();
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x0002EB83 File Offset: 0x0002CD83
		public InputDevice AddDevice(InputDeviceDescription description)
		{
			return this.AddDevice(description, true, null, 0, (InputDevice.DeviceFlags)0);
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x0002EB90 File Offset: 0x0002CD90
		public InputDevice AddDevice(InputDeviceDescription description, bool throwIfNoLayoutFound, string deviceName = null, int deviceId = 0, InputDevice.DeviceFlags deviceFlags = (InputDevice.DeviceFlags)0)
		{
			InternedString internedString = this.TryFindMatchingControlLayout(ref description, deviceId);
			if (!internedString.IsEmpty())
			{
				InputDevice inputDevice = this.AddDevice(internedString, deviceId, deviceName, description, deviceFlags, default(InternedString));
				inputDevice.m_Description = description;
				return inputDevice;
			}
			if (throwIfNoLayoutFound)
			{
				throw new ArgumentException(string.Format("Cannot find layout matching device description '{0}'", description), "description");
			}
			if (deviceId != 0)
			{
				DisableDeviceCommand disableDeviceCommand = DisableDeviceCommand.Create();
				this.m_Runtime.DeviceCommand(deviceId, ref disableDeviceCommand);
			}
			return null;
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x0002EC08 File Offset: 0x0002CE08
		public InputDevice AddDevice(InputDeviceDescription description, InternedString layout, string deviceName = null, int deviceId = 0, InputDevice.DeviceFlags deviceFlags = (InputDevice.DeviceFlags)0)
		{
			InputDevice inputDevice2;
			try
			{
				InputDevice inputDevice = this.AddDevice(layout, deviceId, deviceName, description, deviceFlags, default(InternedString));
				inputDevice.m_Description = description;
				inputDevice2 = inputDevice;
			}
			finally
			{
			}
			return inputDevice2;
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x0002EC48 File Offset: 0x0002CE48
		public void RemoveDevice(InputDevice device, bool keepOnListOfAvailableDevices = false)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			if (device.m_DeviceIndex == -1)
			{
				return;
			}
			this.RemoveStateChangeMonitors(device);
			int deviceIndex = device.m_DeviceIndex;
			int deviceId = device.deviceId;
			if (deviceIndex < this.m_StateChangeMonitors.LengthSafe<InputManager.StateChangeMonitorsForDevice>())
			{
				int num = this.m_StateChangeMonitors.Length;
				this.m_StateChangeMonitors.EraseAtWithCapacity(ref num, deviceIndex);
			}
			this.m_Devices.EraseAtWithCapacity(ref this.m_DevicesCount, deviceIndex);
			this.m_DevicesById.Remove(deviceId);
			if (this.m_Devices != null)
			{
				this.ReallocateStateBuffers();
			}
			else
			{
				this.m_StateBuffers.FreeAll();
			}
			for (int i = deviceIndex; i < this.m_DevicesCount; i++)
			{
				this.m_Devices[i].m_DeviceIndex--;
			}
			device.m_DeviceIndex = -1;
			int j = 0;
			while (j < this.m_AvailableDeviceCount)
			{
				if (this.m_AvailableDevices[j].deviceId == deviceId)
				{
					if (keepOnListOfAvailableDevices)
					{
						this.m_AvailableDevices[j].isRemoved = true;
						break;
					}
					this.m_AvailableDevices.EraseAtWithCapacity(ref this.m_AvailableDeviceCount, j);
					break;
				}
				else
				{
					j++;
				}
			}
			device.BakeOffsetIntoStateBlockRecursive((uint)(-(uint)((ulong)device.m_StateBlock.byteOffset)));
			InputActionState.OnDeviceChange(device, InputDeviceChange.Removed);
			IInputUpdateCallbackReceiver inputUpdateCallbackReceiver = device as IInputUpdateCallbackReceiver;
			if (inputUpdateCallbackReceiver != null)
			{
				this.onBeforeUpdate -= inputUpdateCallbackReceiver.OnUpdate;
			}
			if (device.updateBeforeRender)
			{
				bool flag = false;
				for (int k = 0; k < this.m_DevicesCount; k++)
				{
					if (this.m_Devices[k].updateBeforeRender)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					this.updateMask &= ~InputUpdateType.BeforeRender;
				}
			}
			device.NotifyRemoved();
			DelegateHelpers.InvokeCallbacksSafe<InputDevice, InputDeviceChange>(ref this.m_DeviceChangeListeners, device, InputDeviceChange.Removed, "InputSystem.onDeviceChange", null);
			InputDevice device2 = InputSystem.GetDevice(device.GetType());
			if (device2 == null)
			{
				return;
			}
			device2.MakeCurrent();
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x0002EE18 File Offset: 0x0002D018
		public void FlushDisconnectedDevices()
		{
			this.m_DisconnectedDevices.Clear(this.m_DisconnectedDevicesCount);
			this.m_DisconnectedDevicesCount = 0;
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x0002EE34 File Offset: 0x0002D034
		public unsafe void ResetDevice(InputDevice device, bool alsoResetDontResetControls = false, bool? issueResetCommand = null)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			if (!device.added)
			{
				throw new InvalidOperationException(string.Format("Device '{0}' has not been added to the system", device));
			}
			bool flag = alsoResetDontResetControls || !device.hasDontResetControls;
			InputDeviceChange inputDeviceChange = (flag ? InputDeviceChange.HardReset : InputDeviceChange.SoftReset);
			InputActionState.OnDeviceChange(device, inputDeviceChange);
			DelegateHelpers.InvokeCallbacksSafe<InputDevice, InputDeviceChange>(ref this.m_DeviceChangeListeners, device, inputDeviceChange, "onDeviceChange", null);
			if (!alsoResetDontResetControls)
			{
				ICustomDeviceReset customDeviceReset = device as ICustomDeviceReset;
				if (customDeviceReset != null)
				{
					customDeviceReset.Reset();
					goto IL_01B5;
				}
			}
			void* defaultStatePtr = device.defaultStatePtr;
			uint alignedSizeInBytes = device.stateBlock.alignedSizeInBytes;
			using (NativeArray<byte> nativeArray = new NativeArray<byte>((int)(24U + alignedSizeInBytes), Allocator.Temp, NativeArrayOptions.ClearMemory))
			{
				StateEvent* unsafePtr = (StateEvent*)nativeArray.GetUnsafePtr<byte>();
				void* state = unsafePtr->state;
				double currentTime = this.m_Runtime.currentTime;
				ref InputStateBlock ptr = ref device.m_StateBlock;
				unsafePtr->baseEvent.type = 1398030676;
				unsafePtr->baseEvent.sizeInBytes = 24U + alignedSizeInBytes;
				unsafePtr->baseEvent.time = currentTime;
				unsafePtr->baseEvent.deviceId = device.deviceId;
				unsafePtr->baseEvent.eventId = -1;
				unsafePtr->stateFormat = device.m_StateBlock.format;
				if (flag)
				{
					UnsafeUtility.MemCpy(state, (void*)((byte*)defaultStatePtr + ptr.byteOffset), (long)((ulong)alignedSizeInBytes));
				}
				else
				{
					void* currentStatePtr = device.currentStatePtr;
					void* resetMaskBuffer = this.m_StateBuffers.resetMaskBuffer;
					UnsafeUtility.MemCpy(state, (void*)((byte*)currentStatePtr + ptr.byteOffset), (long)((ulong)alignedSizeInBytes));
					MemoryHelpers.MemCpyMasked(state, (void*)((byte*)defaultStatePtr + ptr.byteOffset), (int)alignedSizeInBytes, (void*)((byte*)resetMaskBuffer + ptr.byteOffset));
				}
				this.UpdateState(device, this.defaultUpdateType, state, 0U, alignedSizeInBytes, currentTime, new InputEventPtr((InputEvent*)unsafePtr));
			}
			IL_01B5:
			bool flag2 = flag;
			if (issueResetCommand != null)
			{
				flag2 = issueResetCommand.Value;
			}
			if (flag2)
			{
				device.RequestReset();
			}
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x0002F030 File Offset: 0x0002D230
		public InputDevice TryGetDevice(string nameOrLayout)
		{
			if (string.IsNullOrEmpty(nameOrLayout))
			{
				throw new ArgumentException("Name is null or empty.", "nameOrLayout");
			}
			if (this.m_DevicesCount == 0)
			{
				return null;
			}
			string text = nameOrLayout.ToLower();
			for (int i = 0; i < this.m_DevicesCount; i++)
			{
				InputDevice inputDevice = this.m_Devices[i];
				if (inputDevice.m_Name.ToLower() == text || inputDevice.m_Layout.ToLower() == text)
				{
					return inputDevice;
				}
			}
			return null;
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x0002F0A9 File Offset: 0x0002D2A9
		public InputDevice GetDevice(string nameOrLayout)
		{
			InputDevice inputDevice = this.TryGetDevice(nameOrLayout);
			if (inputDevice == null)
			{
				throw new ArgumentException("Cannot find device with name or layout '" + nameOrLayout + "'", "nameOrLayout");
			}
			return inputDevice;
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x0002F0D0 File Offset: 0x0002D2D0
		public InputDevice TryGetDevice(Type layoutType)
		{
			InternedString internedString = this.m_Layouts.TryFindLayoutForType(layoutType);
			if (internedString.IsEmpty())
			{
				return null;
			}
			return this.TryGetDevice(internedString);
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x0002F104 File Offset: 0x0002D304
		public InputDevice TryGetDeviceById(int id)
		{
			InputDevice inputDevice;
			if (this.m_DevicesById.TryGetValue(id, out inputDevice))
			{
				return inputDevice;
			}
			return null;
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x0002F124 File Offset: 0x0002D324
		public int GetUnsupportedDevices(List<InputDeviceDescription> descriptions)
		{
			if (descriptions == null)
			{
				throw new ArgumentNullException("descriptions");
			}
			int num = 0;
			for (int i = 0; i < this.m_AvailableDeviceCount; i++)
			{
				if (this.TryGetDeviceById(this.m_AvailableDevices[i].deviceId) == null)
				{
					descriptions.Add(this.m_AvailableDevices[i].description);
					num++;
				}
			}
			return num;
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x0002F188 File Offset: 0x0002D388
		public void EnableOrDisableDevice(InputDevice device, bool enable, InputManager.DeviceDisableScope scope = InputManager.DeviceDisableScope.Everywhere)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			if (enable)
			{
				switch (scope)
				{
				case InputManager.DeviceDisableScope.Everywhere:
					device.disabledWhileInBackground = false;
					if (!device.disabledInFrontend && !device.disabledInRuntime)
					{
						return;
					}
					if (device.disabledInRuntime)
					{
						device.ExecuteEnableCommand();
						device.disabledInRuntime = false;
					}
					if (device.disabledInFrontend)
					{
						if (!device.RequestSync())
						{
							this.ResetDevice(device, false, null);
						}
						device.disabledInFrontend = false;
					}
					break;
				case InputManager.DeviceDisableScope.InFrontendOnly:
					device.disabledWhileInBackground = false;
					if (!device.disabledInFrontend && device.disabledInRuntime)
					{
						return;
					}
					if (!device.disabledInRuntime)
					{
						device.ExecuteDisableCommand();
						device.disabledInRuntime = true;
					}
					if (device.disabledInFrontend)
					{
						if (!device.RequestSync())
						{
							this.ResetDevice(device, false, null);
						}
						device.disabledInFrontend = false;
					}
					break;
				case InputManager.DeviceDisableScope.TemporaryWhilePlayerIsInBackground:
					if (device.disabledWhileInBackground)
					{
						if (device.disabledInRuntime)
						{
							device.ExecuteEnableCommand();
							device.disabledInRuntime = false;
						}
						if (!device.RequestSync())
						{
							this.ResetDevice(device, false, null);
						}
						device.disabledWhileInBackground = false;
					}
					break;
				}
			}
			else
			{
				switch (scope)
				{
				case InputManager.DeviceDisableScope.Everywhere:
					device.disabledWhileInBackground = false;
					if (device.disabledInFrontend && device.disabledInRuntime)
					{
						return;
					}
					if (!device.disabledInRuntime)
					{
						device.ExecuteDisableCommand();
						device.disabledInRuntime = true;
					}
					if (!device.disabledInFrontend)
					{
						this.ResetDevice(device, false, new bool?(false));
						device.disabledInFrontend = true;
					}
					break;
				case InputManager.DeviceDisableScope.InFrontendOnly:
					device.disabledWhileInBackground = false;
					if (!device.disabledInRuntime && device.disabledInFrontend)
					{
						return;
					}
					if (device.disabledInRuntime)
					{
						device.ExecuteEnableCommand();
						device.disabledInRuntime = false;
					}
					if (!device.disabledInFrontend)
					{
						this.ResetDevice(device, false, new bool?(false));
						device.disabledInFrontend = true;
					}
					break;
				case InputManager.DeviceDisableScope.TemporaryWhilePlayerIsInBackground:
					if (device.disabledInFrontend || device.disabledWhileInBackground)
					{
						return;
					}
					device.disabledWhileInBackground = true;
					this.ResetDevice(device, false, new bool?(false));
					device.ExecuteDisableCommand();
					device.disabledInRuntime = true;
					break;
				}
			}
			InputDeviceChange inputDeviceChange = (enable ? InputDeviceChange.Enabled : InputDeviceChange.Disabled);
			DelegateHelpers.InvokeCallbacksSafe<InputDevice, InputDeviceChange>(ref this.m_DeviceChangeListeners, device, inputDeviceChange, "InputSystem.onDeviceChange", null);
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x0002F3C8 File Offset: 0x0002D5C8
		private unsafe void QueueEvent(InputEvent* eventPtr)
		{
			if (this.m_InputEventStream.isOpen)
			{
				this.m_InputEventStream.Write(eventPtr);
				return;
			}
			this.m_Runtime.QueueEvent(eventPtr);
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x0002F3F0 File Offset: 0x0002D5F0
		public void QueueEvent(InputEventPtr ptr)
		{
			this.QueueEvent(ptr.data);
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x0002F3FF File Offset: 0x0002D5FF
		public unsafe void QueueEvent<TEvent>(ref TEvent inputEvent) where TEvent : struct, IInputEventTypeInfo
		{
			this.QueueEvent((InputEvent*)UnsafeUtility.AddressOf<TEvent>(ref inputEvent));
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x0002F40D File Offset: 0x0002D60D
		public void Update()
		{
			this.Update(this.defaultUpdateType);
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x0002F41B File Offset: 0x0002D61B
		public void Update(InputUpdateType updateType)
		{
			this.m_Runtime.Update(updateType);
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x0002F429 File Offset: 0x0002D629
		internal void Initialize(IInputRuntime runtime, InputSettings settings)
		{
			this.m_Settings = settings;
			this.InitializeData();
			this.InstallRuntime(runtime);
			this.InstallGlobals();
			this.ApplySettings();
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x0002F44C File Offset: 0x0002D64C
		internal void Destroy()
		{
			for (int i = 0; i < this.m_DevicesCount; i++)
			{
				this.m_Devices[i].NotifyRemoved();
			}
			this.m_StateBuffers.FreeAll();
			this.UninstallGlobals();
			if (this.m_Settings != null && this.m_Settings.hideFlags == HideFlags.HideAndDontSave)
			{
				Object.DestroyImmediate(this.m_Settings);
			}
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x0002F4B0 File Offset: 0x0002D6B0
		internal void InitializeData()
		{
			this.m_Layouts.Allocate();
			this.m_Processors.Initialize();
			this.m_Interactions.Initialize();
			this.m_Composites.Initialize();
			this.m_DevicesById = new Dictionary<int, InputDevice>();
			this.m_UpdateMask = InputUpdateType.Dynamic | InputUpdateType.Fixed;
			this.m_HasFocus = Application.isFocused;
			this.m_PollingFrequency = 60f;
			this.RegisterControlLayout("Axis", typeof(AxisControl));
			this.RegisterControlLayout("Button", typeof(ButtonControl));
			this.RegisterControlLayout("DiscreteButton", typeof(DiscreteButtonControl));
			this.RegisterControlLayout("Key", typeof(KeyControl));
			this.RegisterControlLayout("Analog", typeof(AxisControl));
			this.RegisterControlLayout("Integer", typeof(IntegerControl));
			this.RegisterControlLayout("Digital", typeof(IntegerControl));
			this.RegisterControlLayout("Double", typeof(DoubleControl));
			this.RegisterControlLayout("Vector2", typeof(Vector2Control));
			this.RegisterControlLayout("Vector3", typeof(Vector3Control));
			this.RegisterControlLayout("Delta", typeof(DeltaControl));
			this.RegisterControlLayout("Quaternion", typeof(QuaternionControl));
			this.RegisterControlLayout("Stick", typeof(StickControl));
			this.RegisterControlLayout("Dpad", typeof(DpadControl));
			this.RegisterControlLayout("DpadAxis", typeof(DpadControl.DpadAxisControl));
			this.RegisterControlLayout("AnyKey", typeof(AnyKeyControl));
			this.RegisterControlLayout("Touch", typeof(TouchControl));
			this.RegisterControlLayout("TouchPhase", typeof(TouchPhaseControl));
			this.RegisterControlLayout("TouchPress", typeof(TouchPressControl));
			this.RegisterControlLayout("Gamepad", typeof(Gamepad));
			this.RegisterControlLayout("Joystick", typeof(Joystick));
			this.RegisterControlLayout("Keyboard", typeof(Keyboard));
			this.RegisterControlLayout("Pointer", typeof(Pointer));
			this.RegisterControlLayout("Mouse", typeof(Mouse));
			this.RegisterControlLayout("Pen", typeof(Pen));
			this.RegisterControlLayout("Touchscreen", typeof(Touchscreen));
			this.RegisterControlLayout("Sensor", typeof(Sensor));
			this.RegisterControlLayout("Accelerometer", typeof(Accelerometer));
			this.RegisterControlLayout("Gyroscope", typeof(Gyroscope));
			this.RegisterControlLayout("GravitySensor", typeof(GravitySensor));
			this.RegisterControlLayout("AttitudeSensor", typeof(AttitudeSensor));
			this.RegisterControlLayout("LinearAccelerationSensor", typeof(LinearAccelerationSensor));
			this.RegisterControlLayout("MagneticFieldSensor", typeof(MagneticFieldSensor));
			this.RegisterControlLayout("LightSensor", typeof(LightSensor));
			this.RegisterControlLayout("PressureSensor", typeof(PressureSensor));
			this.RegisterControlLayout("HumiditySensor", typeof(HumiditySensor));
			this.RegisterControlLayout("AmbientTemperatureSensor", typeof(AmbientTemperatureSensor));
			this.RegisterControlLayout("StepCounter", typeof(StepCounter));
			this.RegisterControlLayout("TrackedDevice", typeof(TrackedDevice));
			this.RegisterPrecompiledLayout<FastKeyboard>(";AnyKey;Button;Axis;Key;DiscreteButton;Keyboard");
			this.RegisterPrecompiledLayout<FastTouchscreen>("AutoWindowSpace;Touch;Vector2;Delta;Analog;TouchPress;Button;Axis;Integer;TouchPhase;Double;Touchscreen;Pointer");
			this.RegisterPrecompiledLayout<FastMouse>("AutoWindowSpace;Vector2;Delta;Button;Axis;Digital;Integer;Mouse;Pointer");
			this.processors.AddTypeRegistration("Invert", typeof(InvertProcessor));
			this.processors.AddTypeRegistration("InvertVector2", typeof(InvertVector2Processor));
			this.processors.AddTypeRegistration("InvertVector3", typeof(InvertVector3Processor));
			this.processors.AddTypeRegistration("Clamp", typeof(ClampProcessor));
			this.processors.AddTypeRegistration("Normalize", typeof(NormalizeProcessor));
			this.processors.AddTypeRegistration("NormalizeVector2", typeof(NormalizeVector2Processor));
			this.processors.AddTypeRegistration("NormalizeVector3", typeof(NormalizeVector3Processor));
			this.processors.AddTypeRegistration("Scale", typeof(ScaleProcessor));
			this.processors.AddTypeRegistration("ScaleVector2", typeof(ScaleVector2Processor));
			this.processors.AddTypeRegistration("ScaleVector3", typeof(ScaleVector3Processor));
			this.processors.AddTypeRegistration("StickDeadzone", typeof(StickDeadzoneProcessor));
			this.processors.AddTypeRegistration("AxisDeadzone", typeof(AxisDeadzoneProcessor));
			this.processors.AddTypeRegistration("CompensateDirection", typeof(CompensateDirectionProcessor));
			this.processors.AddTypeRegistration("CompensateRotation", typeof(CompensateRotationProcessor));
			this.interactions.AddTypeRegistration("Hold", typeof(HoldInteraction));
			this.interactions.AddTypeRegistration("Tap", typeof(TapInteraction));
			this.interactions.AddTypeRegistration("SlowTap", typeof(SlowTapInteraction));
			this.interactions.AddTypeRegistration("MultiTap", typeof(MultiTapInteraction));
			this.interactions.AddTypeRegistration("Press", typeof(PressInteraction));
			this.composites.AddTypeRegistration("1DAxis", typeof(AxisComposite));
			this.composites.AddTypeRegistration("2DVector", typeof(Vector2Composite));
			this.composites.AddTypeRegistration("3DVector", typeof(Vector3Composite));
			this.composites.AddTypeRegistration("Axis", typeof(AxisComposite));
			this.composites.AddTypeRegistration("Dpad", typeof(Vector2Composite));
			this.composites.AddTypeRegistration("ButtonWithOneModifier", typeof(ButtonWithOneModifier));
			this.composites.AddTypeRegistration("ButtonWithTwoModifiers", typeof(ButtonWithTwoModifiers));
			this.composites.AddTypeRegistration("OneModifier", typeof(OneModifierComposite));
			this.composites.AddTypeRegistration("TwoModifiers", typeof(TwoModifiersComposite));
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x0002FB94 File Offset: 0x0002DD94
		internal void InstallRuntime(IInputRuntime runtime)
		{
			if (this.m_Runtime != null)
			{
				this.m_Runtime.onUpdate = null;
				this.m_Runtime.onBeforeUpdate = null;
				this.m_Runtime.onDeviceDiscovered = null;
				this.m_Runtime.onPlayerFocusChanged = null;
				this.m_Runtime.onShouldRunUpdate = null;
			}
			this.m_Runtime = runtime;
			this.m_Runtime.onUpdate = new InputUpdateDelegate(this.OnUpdate);
			this.m_Runtime.onDeviceDiscovered = new Action<int, string>(this.OnNativeDeviceDiscovered);
			this.m_Runtime.onPlayerFocusChanged = new Action<bool>(this.OnFocusChanged);
			this.m_Runtime.onShouldRunUpdate = new Func<InputUpdateType, bool>(this.ShouldRunUpdate);
			this.m_Runtime.pollingFrequency = this.pollingFrequency;
			this.m_HasFocus = this.m_Runtime.isPlayerFocused;
			if (this.m_BeforeUpdateListeners.length > 0 || this.m_HaveDevicesWithStateCallbackReceivers)
			{
				this.m_Runtime.onBeforeUpdate = new Action<InputUpdateType>(this.OnBeforeUpdate);
				this.m_NativeBeforeUpdateHooked = true;
			}
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x0002FCA0 File Offset: 0x0002DEA0
		internal void InstallGlobals()
		{
			InputControlLayout.s_Layouts = this.m_Layouts;
			InputProcessor.s_Processors = this.m_Processors;
			InputInteraction.s_Interactions = this.m_Interactions;
			InputBindingComposite.s_Composites = this.m_Composites;
			InputRuntime.s_Instance = this.m_Runtime;
			InputRuntime.s_CurrentTimeOffsetToRealtimeSinceStartup = this.m_Runtime.currentTimeOffsetToRealtimeSinceStartup;
			InputUpdate.Restore(default(InputUpdate.SerializedState));
			InputStateBuffers.SwitchTo(this.m_StateBuffers, InputUpdateType.Dynamic);
			InputStateBuffers.s_DefaultStateBuffer = this.m_StateBuffers.defaultStateBuffer;
			InputStateBuffers.s_NoiseMaskBuffer = this.m_StateBuffers.noiseMaskBuffer;
			InputStateBuffers.s_ResetMaskBuffer = this.m_StateBuffers.resetMaskBuffer;
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x0002FD40 File Offset: 0x0002DF40
		internal void UninstallGlobals()
		{
			if (InputControlLayout.s_Layouts.baseLayoutTable == this.m_Layouts.baseLayoutTable)
			{
				InputControlLayout.s_Layouts = default(InputControlLayout.Collection);
			}
			if (InputProcessor.s_Processors.table == this.m_Processors.table)
			{
				InputProcessor.s_Processors = default(TypeTable);
			}
			if (InputInteraction.s_Interactions.table == this.m_Interactions.table)
			{
				InputInteraction.s_Interactions = default(TypeTable);
			}
			if (InputBindingComposite.s_Composites.table == this.m_Composites.table)
			{
				InputBindingComposite.s_Composites = default(TypeTable);
			}
			InputControlLayout.s_CacheInstance = default(InputControlLayout.Cache);
			InputControlLayout.s_CacheInstanceRef = 0;
			if (this.m_Runtime != null)
			{
				this.m_Runtime.onUpdate = null;
				this.m_Runtime.onDeviceDiscovered = null;
				this.m_Runtime.onBeforeUpdate = null;
				this.m_Runtime.onPlayerFocusChanged = null;
				this.m_Runtime.onShouldRunUpdate = null;
				if (InputRuntime.s_Instance == this.m_Runtime)
				{
					InputRuntime.s_Instance = null;
				}
			}
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x0002FE40 File Offset: 0x0002E040
		private void MakeDeviceNameUnique(InputDevice device)
		{
			if (this.m_DevicesCount == 0)
			{
				return;
			}
			string text = StringHelpers.MakeUniqueName<InputDevice>(device.name, this.m_Devices, delegate(InputDevice x)
			{
				if (x == null)
				{
					return string.Empty;
				}
				return x.name;
			});
			if (text != device.name)
			{
				InputManager.ResetControlPathsRecursive(device);
				device.m_Name = new InternedString(text);
			}
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x0002FEA8 File Offset: 0x0002E0A8
		private static void ResetControlPathsRecursive(InputControl control)
		{
			control.m_Path = null;
			ReadOnlyArray<InputControl> children = control.children;
			int count = children.Count;
			for (int i = 0; i < count; i++)
			{
				InputManager.ResetControlPathsRecursive(children[i]);
			}
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x0002FEE4 File Offset: 0x0002E0E4
		private void AssignUniqueDeviceId(InputDevice device)
		{
			if (device.deviceId != 0)
			{
				InputDevice inputDevice = this.TryGetDeviceById(device.deviceId);
				if (inputDevice != null)
				{
					throw new InvalidOperationException(string.Format("Duplicate device ID {0} detected for devices '{1}' and '{2}'", device.deviceId, device.name, inputDevice.name));
				}
			}
			else
			{
				device.m_DeviceId = this.m_Runtime.AllocateDeviceId();
			}
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x0002FF44 File Offset: 0x0002E144
		private void ReallocateStateBuffers()
		{
			InputStateBuffers stateBuffers = this.m_StateBuffers;
			InputStateBuffers inputStateBuffers = default(InputStateBuffers);
			inputStateBuffers.AllocateAll(this.m_Devices, this.m_DevicesCount);
			inputStateBuffers.MigrateAll(this.m_Devices, this.m_DevicesCount, stateBuffers);
			stateBuffers.FreeAll();
			this.m_StateBuffers = inputStateBuffers;
			InputStateBuffers.s_DefaultStateBuffer = inputStateBuffers.defaultStateBuffer;
			InputStateBuffers.s_NoiseMaskBuffer = inputStateBuffers.noiseMaskBuffer;
			InputStateBuffers.s_ResetMaskBuffer = inputStateBuffers.resetMaskBuffer;
			InputStateBuffers.SwitchTo(this.m_StateBuffers, (InputUpdate.s_LatestUpdateType != InputUpdateType.None) ? InputUpdate.s_LatestUpdateType : this.defaultUpdateType);
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x0002FFD8 File Offset: 0x0002E1D8
		private unsafe void InitializeDefaultState(InputDevice device)
		{
			if (!device.hasControlsWithDefaultState)
			{
				return;
			}
			ReadOnlyArray<InputControl> allControls = device.allControls;
			int count = allControls.Count;
			void* defaultStateBuffer = this.m_StateBuffers.defaultStateBuffer;
			for (int i = 0; i < count; i++)
			{
				InputControl inputControl = allControls[i];
				if (inputControl.hasDefaultState)
				{
					inputControl.m_StateBlock.Write(defaultStateBuffer, inputControl.m_DefaultState);
				}
			}
			InputStateBlock stateBlock = device.m_StateBlock;
			int deviceIndex = device.m_DeviceIndex;
			if (this.m_StateBuffers.m_PlayerStateBuffers.valid)
			{
				stateBlock.CopyToFrom(this.m_StateBuffers.m_PlayerStateBuffers.GetFrontBuffer(deviceIndex), defaultStateBuffer);
				stateBlock.CopyToFrom(this.m_StateBuffers.m_PlayerStateBuffers.GetBackBuffer(deviceIndex), defaultStateBuffer);
			}
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x00030098 File Offset: 0x0002E298
		private unsafe void InitializeDeviceState(InputDevice device)
		{
			ReadOnlyArray<InputControl> allControls = device.allControls;
			int count = allControls.Count;
			void* resetMaskBuffer = this.m_StateBuffers.resetMaskBuffer;
			bool hasControlsWithDefaultState = device.hasControlsWithDefaultState;
			void* noiseMaskBuffer = this.m_StateBuffers.noiseMaskBuffer;
			MemoryHelpers.SetBitsInBuffer(noiseMaskBuffer, (int)device.stateBlock.byteOffset, 0, (int)device.stateBlock.sizeInBits, false);
			MemoryHelpers.SetBitsInBuffer(resetMaskBuffer, (int)device.stateBlock.byteOffset, 0, (int)device.stateBlock.sizeInBits, true);
			void* defaultStateBuffer = this.m_StateBuffers.defaultStateBuffer;
			for (int i = 0; i < count; i++)
			{
				InputControl inputControl = allControls[i];
				if (!inputControl.usesStateFromOtherControl)
				{
					if (!inputControl.noisy || inputControl.dontReset)
					{
						ref InputStateBlock ptr = ref inputControl.m_StateBlock;
						if (!inputControl.noisy)
						{
							MemoryHelpers.SetBitsInBuffer(noiseMaskBuffer, (int)ptr.byteOffset, (int)ptr.bitOffset, (int)ptr.sizeInBits, true);
						}
						if (inputControl.dontReset)
						{
							MemoryHelpers.SetBitsInBuffer(resetMaskBuffer, (int)ptr.byteOffset, (int)ptr.bitOffset, (int)ptr.sizeInBits, false);
						}
					}
					if (hasControlsWithDefaultState && inputControl.hasDefaultState)
					{
						inputControl.m_StateBlock.Write(defaultStateBuffer, inputControl.m_DefaultState);
					}
				}
			}
			if (hasControlsWithDefaultState)
			{
				ref InputStateBlock ptr2 = ref device.m_StateBlock;
				int deviceIndex = device.m_DeviceIndex;
				if (this.m_StateBuffers.m_PlayerStateBuffers.valid)
				{
					ptr2.CopyToFrom(this.m_StateBuffers.m_PlayerStateBuffers.GetFrontBuffer(deviceIndex), defaultStateBuffer);
					ptr2.CopyToFrom(this.m_StateBuffers.m_PlayerStateBuffers.GetBackBuffer(deviceIndex), defaultStateBuffer);
				}
			}
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x00030240 File Offset: 0x0002E440
		private void OnNativeDeviceDiscovered(int deviceId, string deviceDescriptor)
		{
			this.RestoreDevicesAfterDomainReloadIfNecessary();
			InputDevice inputDevice = this.TryMatchDisconnectedDevice(deviceDescriptor);
			InputDeviceDescription inputDeviceDescription = ((inputDevice != null) ? inputDevice.description : InputDeviceDescription.FromJson(deviceDescriptor));
			bool flag = false;
			try
			{
				if (this.m_Settings.supportedDevices.Count > 0)
				{
					InternedString internedString = ((inputDevice != null) ? inputDevice.m_Layout : this.TryFindMatchingControlLayout(ref inputDeviceDescription, deviceId));
					if (!this.IsDeviceLayoutMarkedAsSupportedInSettings(internedString))
					{
						flag = true;
						return;
					}
				}
				if (inputDevice != null)
				{
					inputDevice.m_DeviceId = deviceId;
					inputDevice.m_DeviceFlags |= InputDevice.DeviceFlags.Native;
					inputDevice.m_DeviceFlags &= ~InputDevice.DeviceFlags.DisabledInFrontend;
					inputDevice.m_DeviceFlags &= ~InputDevice.DeviceFlags.DisabledWhileInBackground;
					inputDevice.m_DeviceFlags &= ~InputDevice.DeviceFlags.DisabledStateHasBeenQueriedFromRuntime;
					this.AddDevice(inputDevice);
					DelegateHelpers.InvokeCallbacksSafe<InputDevice, InputDeviceChange>(ref this.m_DeviceChangeListeners, inputDevice, InputDeviceChange.Reconnected, "InputSystem.onDeviceChange", null);
				}
				else
				{
					this.AddDevice(inputDeviceDescription, false, null, deviceId, InputDevice.DeviceFlags.Native);
				}
			}
			catch (Exception ex)
			{
				Debug.LogError(string.Format("Could not create a device for '{0}' (exception: {1})", inputDeviceDescription, ex));
			}
			finally
			{
				ArrayHelpers.AppendWithCapacity<InputManager.AvailableDevice>(ref this.m_AvailableDevices, ref this.m_AvailableDeviceCount, new InputManager.AvailableDevice
				{
					description = inputDeviceDescription,
					deviceId = deviceId,
					isNative = true,
					isRemoved = flag
				}, 10);
			}
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x00030398 File Offset: 0x0002E598
		private InputDevice TryMatchDisconnectedDevice(string deviceDescriptor)
		{
			for (int i = 0; i < this.m_DisconnectedDevicesCount; i++)
			{
				InputDevice inputDevice = this.m_DisconnectedDevices[i];
				InputDeviceDescription description = inputDevice.description;
				if (InputDeviceDescription.ComparePropertyToDeviceDescriptor("interface", description.interfaceName, deviceDescriptor) && InputDeviceDescription.ComparePropertyToDeviceDescriptor("product", description.product, deviceDescriptor) && InputDeviceDescription.ComparePropertyToDeviceDescriptor("manufacturer", description.manufacturer, deviceDescriptor) && InputDeviceDescription.ComparePropertyToDeviceDescriptor("type", description.deviceClass, deviceDescriptor) && InputDeviceDescription.ComparePropertyToDeviceDescriptor("capabilities", description.capabilities, deviceDescriptor) && InputDeviceDescription.ComparePropertyToDeviceDescriptor("serial", description.serial, deviceDescriptor))
				{
					this.m_DisconnectedDevices.EraseAtWithCapacity(ref this.m_DisconnectedDevicesCount, i);
					return inputDevice;
				}
			}
			return null;
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x00030459 File Offset: 0x0002E659
		private void InstallBeforeUpdateHookIfNecessary()
		{
			if (this.m_NativeBeforeUpdateHooked || this.m_Runtime == null)
			{
				return;
			}
			this.m_Runtime.onBeforeUpdate = new Action<InputUpdateType>(this.OnBeforeUpdate);
			this.m_NativeBeforeUpdateHooked = true;
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x0003048A File Offset: 0x0002E68A
		private void RestoreDevicesAfterDomainReloadIfNecessary()
		{
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x0003048C File Offset: 0x0002E68C
		private void WarnAboutDevicesFailingToRecreateAfterDomainReload()
		{
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x00030490 File Offset: 0x0002E690
		private void OnBeforeUpdate(InputUpdateType updateType)
		{
			this.RestoreDevicesAfterDomainReloadIfNecessary();
			if ((updateType & this.m_UpdateMask) == InputUpdateType.None)
			{
				return;
			}
			InputStateBuffers.SwitchTo(this.m_StateBuffers, updateType);
			InputUpdate.OnBeforeUpdate(updateType);
			if (this.m_HaveDevicesWithStateCallbackReceivers && updateType != InputUpdateType.BeforeRender)
			{
				for (int i = 0; i < this.m_DevicesCount; i++)
				{
					InputDevice inputDevice = this.m_Devices[i];
					if (inputDevice.hasStateCallbacks)
					{
						((IInputStateCallbackReceiver)inputDevice).OnNextUpdate();
					}
				}
			}
			DelegateHelpers.InvokeCallbacksSafe(ref this.m_BeforeUpdateListeners, "onBeforeUpdate", null);
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x0003050C File Offset: 0x0002E70C
		internal void ApplySettings()
		{
			InputUpdateType inputUpdateType = InputUpdateType.Editor;
			if ((this.m_UpdateMask & InputUpdateType.BeforeRender) != InputUpdateType.None)
			{
				inputUpdateType |= InputUpdateType.BeforeRender;
			}
			if (this.m_Settings.updateMode == (InputSettings.UpdateMode)0)
			{
				this.m_Settings.updateMode = InputSettings.UpdateMode.ProcessEventsInDynamicUpdate;
			}
			switch (this.m_Settings.updateMode)
			{
			case InputSettings.UpdateMode.ProcessEventsInDynamicUpdate:
				inputUpdateType |= InputUpdateType.Dynamic;
				break;
			case InputSettings.UpdateMode.ProcessEventsInFixedUpdate:
				inputUpdateType |= InputUpdateType.Fixed;
				break;
			case InputSettings.UpdateMode.ProcessEventsManually:
				inputUpdateType |= InputUpdateType.Manual;
				break;
			default:
				throw new NotSupportedException("Invalid input update mode: " + this.m_Settings.updateMode.ToString());
			}
			this.updateMask = inputUpdateType;
			this.AddAvailableDevicesThatAreNowRecognized();
			if (this.settings.supportedDevices.Count > 0)
			{
				for (int i = 0; i < this.m_DevicesCount; i++)
				{
					InputDevice inputDevice = this.m_Devices[i];
					InternedString layout = inputDevice.m_Layout;
					bool flag = false;
					for (int j = 0; j < this.m_AvailableDeviceCount; j++)
					{
						if (this.m_AvailableDevices[j].deviceId == inputDevice.deviceId)
						{
							flag = true;
							break;
						}
					}
					if (flag && !this.IsDeviceLayoutMarkedAsSupportedInSettings(layout))
					{
						this.RemoveDevice(inputDevice, true);
						i--;
					}
				}
			}
			if (this.m_Settings.m_FeatureFlags != null && this.m_Settings.IsFeatureEnabled("USE_WINDOWS_GAMING_INPUT_BACKEND"))
			{
				UseWindowsGamingInputCommand useWindowsGamingInputCommand = UseWindowsGamingInputCommand.Create(true);
				if (this.ExecuteGlobalCommand<UseWindowsGamingInputCommand>(ref useWindowsGamingInputCommand) < 0L)
				{
					Debug.LogError("Could not enable Windows.Gaming.Input");
				}
			}
			Touchscreen.s_TapTime = this.settings.defaultTapTime;
			Touchscreen.s_TapDelayTime = this.settings.multiTapDelayTime;
			Touchscreen.s_TapRadiusSquared = this.settings.tapRadius * this.settings.tapRadius;
			ButtonControl.s_GlobalDefaultButtonPressPoint = Mathf.Clamp(this.settings.defaultButtonPressPoint, 0.0001f, float.MaxValue);
			ButtonControl.s_GlobalDefaultButtonReleaseThreshold = this.settings.buttonReleaseThreshold;
			foreach (InputDevice inputDevice2 in this.devices)
			{
				inputDevice2.SetOptimizedControlDataTypeRecursively();
			}
			foreach (InputDevice inputDevice3 in this.devices)
			{
				inputDevice3.MarkAsStaleRecursively();
			}
			DelegateHelpers.InvokeCallbacksSafe(ref this.m_SettingsChangedListeners, "InputSystem.onSettingsChange", null);
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x00030788 File Offset: 0x0002E988
		internal unsafe long ExecuteGlobalCommand<TCommand>(ref TCommand command) where TCommand : struct, IInputDeviceCommandInfo
		{
			InputDeviceCommand* ptr = (InputDeviceCommand*)UnsafeUtility.AddressOf<TCommand>(ref command);
			return InputRuntime.s_Instance.DeviceCommand(0, ptr);
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x000307A8 File Offset: 0x0002E9A8
		internal void AddAvailableDevicesThatAreNowRecognized()
		{
			for (int i = 0; i < this.m_AvailableDeviceCount; i++)
			{
				int deviceId = this.m_AvailableDevices[i].deviceId;
				if (this.TryGetDeviceById(deviceId) == null)
				{
					InternedString internedString = this.TryFindMatchingControlLayout(ref this.m_AvailableDevices[i].description, deviceId);
					if (this.IsDeviceLayoutMarkedAsSupportedInSettings(internedString))
					{
						if (internedString.IsEmpty())
						{
							if (deviceId != 0)
							{
								DisableDeviceCommand disableDeviceCommand = DisableDeviceCommand.Create();
								this.m_Runtime.DeviceCommand(deviceId, ref disableDeviceCommand);
							}
						}
						else
						{
							try
							{
								this.AddDevice(this.m_AvailableDevices[i].description, internedString, null, deviceId, this.m_AvailableDevices[i].isNative ? InputDevice.DeviceFlags.Native : ((InputDevice.DeviceFlags)0));
							}
							catch (Exception)
							{
							}
						}
					}
				}
			}
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x00030878 File Offset: 0x0002EA78
		private bool ShouldRunDeviceInBackground(InputDevice device)
		{
			return this.m_Settings.backgroundBehavior != InputSettings.BackgroundBehavior.ResetAndDisableAllDevices && device.canRunInBackground;
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x00030890 File Offset: 0x0002EA90
		internal void OnFocusChanged(bool focus)
		{
			bool runInBackground = this.m_Runtime.runInBackground;
			if (this.m_Settings.backgroundBehavior == InputSettings.BackgroundBehavior.IgnoreFocus && runInBackground)
			{
				this.m_HasFocus = focus;
				return;
			}
			if (!focus)
			{
				if (runInBackground)
				{
					for (int i = 0; i < this.m_DevicesCount; i++)
					{
						InputDevice inputDevice = this.m_Devices[i];
						if (inputDevice.enabled && !this.ShouldRunDeviceInBackground(inputDevice))
						{
							this.EnableOrDisableDevice(inputDevice, false, InputManager.DeviceDisableScope.TemporaryWhilePlayerIsInBackground);
							int num = this.m_Devices.IndexOfReference(inputDevice, this.m_DevicesCount);
							if (num == -1)
							{
								i--;
							}
							else
							{
								i = num;
							}
						}
					}
				}
			}
			else
			{
				for (int j = 0; j < this.m_DevicesCount; j++)
				{
					InputDevice inputDevice2 = this.m_Devices[j];
					if (inputDevice2.disabledWhileInBackground)
					{
						this.EnableOrDisableDevice(inputDevice2, true, InputManager.DeviceDisableScope.TemporaryWhilePlayerIsInBackground);
					}
					else if (inputDevice2.enabled && !runInBackground && !inputDevice2.RequestSync())
					{
						this.ResetDevice(inputDevice2, false, null);
					}
				}
			}
			this.m_HasFocus = focus;
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x00030988 File Offset: 0x0002EB88
		internal bool ShouldRunUpdate(InputUpdateType updateType)
		{
			if (updateType == InputUpdateType.None)
			{
				return true;
			}
			InputUpdateType updateMask = this.m_UpdateMask;
			return (updateType & updateMask) > InputUpdateType.None;
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x000309A8 File Offset: 0x0002EBA8
		private unsafe void OnUpdate(InputUpdateType updateType, ref InputEventBuffer eventBuffer)
		{
			if (this.m_InputEventStream.isOpen)
			{
				throw new InvalidOperationException("Already have an event buffer set! Was OnUpdate() called recursively?");
			}
			this.RestoreDevicesAfterDomainReloadIfNecessary();
			if ((updateType & this.m_UpdateMask) == InputUpdateType.None)
			{
				return;
			}
			this.WarnAboutDevicesFailingToRecreateAfterDomainReload();
			int num = this.m_Metrics.totalUpdateCount + 1;
			this.m_Metrics.totalUpdateCount = num;
			InputRuntime.s_CurrentTimeOffsetToRealtimeSinceStartup = this.m_Runtime.currentTimeOffsetToRealtimeSinceStartup;
			InputStateBuffers.SwitchTo(this.m_StateBuffers, updateType);
			this.m_CurrentUpdate = updateType;
			InputUpdate.OnUpdate(updateType);
			foreach (InputDevice inputDevice in this.devices)
			{
			}
			bool flag = updateType.IsPlayerUpdate() && this.gameIsPlaying;
			double num2 = ((updateType == InputUpdateType.Fixed) ? this.m_Runtime.currentTimeForFixedUpdate : this.m_Runtime.currentTime);
			bool flag2 = (updateType == InputUpdateType.Fixed || updateType == InputUpdateType.BeforeRender) && InputSystem.settings.updateMode == InputSettings.UpdateMode.ProcessEventsInFixedUpdate;
			bool flag3 = !this.gameHasFocus && !this.m_Runtime.runInBackground;
			if (eventBuffer.eventCount == 0 || flag3 || ((!this.gameHasFocus || this.gameShouldGetInputRegardlessOfFocus) && this.m_Settings.backgroundBehavior == InputSettings.BackgroundBehavior.ResetAndDisableAllDevices && updateType != InputUpdateType.Editor))
			{
				if (flag)
				{
					this.ProcessStateChangeMonitorTimeouts();
				}
				this.InvokeAfterUpdateCallback(updateType);
				if (flag3)
				{
					eventBuffer.Reset();
				}
				this.m_CurrentUpdate = InputUpdateType.None;
				return;
			}
			long timestamp = Stopwatch.GetTimestamp();
			double num3 = 0.0;
			try
			{
				this.m_InputEventStream = new InputEventStream(ref eventBuffer, this.m_Settings.maxQueuedEventsPerUpdate);
				uint num4 = 0U;
				InputEvent* ptr = null;
				while (this.m_InputEventStream.remainingEventCount > 0)
				{
					if (this.m_Settings.maxEventBytesPerUpdate > 0 && (ulong)num4 >= (ulong)((long)this.m_Settings.maxEventBytesPerUpdate))
					{
						Debug.LogError("Exceeded budget for maximum input event throughput per InputSystem.Update(). Discarding remaining events. Increase InputSystem.settings.maxEventBytesPerUpdate or set it to 0 to remove the limit.");
						break;
					}
					InputDevice inputDevice2 = null;
					InputEvent* ptr2 = this.m_InputEventStream.currentEventPtr;
					if (updateType == InputUpdateType.BeforeRender)
					{
						while (this.m_InputEventStream.remainingEventCount > 0)
						{
							inputDevice2 = this.TryGetDeviceById(ptr2->deviceId);
							if (inputDevice2 != null && inputDevice2.updateBeforeRender && (ptr2->type == 1398030676 || ptr2->type == 1145852993))
							{
								break;
							}
							ptr2 = this.m_InputEventStream.Advance(true);
						}
					}
					if (this.m_InputEventStream.remainingEventCount == 0)
					{
						break;
					}
					double internalTime = ptr2->internalTime;
					FourCC type = ptr2->type;
					if (flag2 && internalTime >= num2)
					{
						this.m_InputEventStream.Advance(true);
					}
					else
					{
						if (inputDevice2 == null)
						{
							inputDevice2 = this.TryGetDeviceById(ptr2->deviceId);
						}
						if (inputDevice2 == null)
						{
							this.m_InputEventStream.Advance(false);
						}
						else if (!inputDevice2.enabled && type != 1146242381 && type != 1145259591 && (inputDevice2.m_DeviceFlags & (InputDevice.DeviceFlags.DisabledInRuntime | InputDevice.DeviceFlags.DisabledWhileInBackground)) != (InputDevice.DeviceFlags)0)
						{
							this.m_InputEventStream.Advance(false);
						}
						else
						{
							if (!this.settings.disableRedundantEventsMerging && inputDevice2.hasEventMerger && ptr2 != ptr)
							{
								InputEvent* ptr3 = this.m_InputEventStream.Peek();
								if (ptr3 != null && ptr2->deviceId == ptr3->deviceId && (!flag2 || ptr3->internalTime < num2))
								{
									if (((IEventMerger)inputDevice2).MergeForward(ptr2, ptr3))
									{
										this.m_InputEventStream.Advance(false);
										continue;
									}
									ptr = ptr3;
								}
							}
							if (inputDevice2.hasEventPreProcessor && !((IEventPreProcessor)inputDevice2).PreProcessEvent(ptr2))
							{
								this.m_InputEventStream.Advance(false);
							}
							else
							{
								if (this.m_EventListeners.length > 0)
								{
									DelegateHelpers.InvokeCallbacksSafe<InputEventPtr, InputDevice>(ref this.m_EventListeners, new InputEventPtr(ptr2), inputDevice2, "InputSystem.onEvent", null);
									if (ptr2->handled)
									{
										this.m_InputEventStream.Advance(false);
										continue;
									}
								}
								if (internalTime <= num2)
								{
									num3 += num2 - internalTime;
								}
								num = this.m_Metrics.totalEventCount + 1;
								this.m_Metrics.totalEventCount = num;
								this.m_Metrics.totalEventBytes = this.m_Metrics.totalEventBytes + (int)ptr2->sizeInBytes;
								num = type;
								if (num <= 1146242381)
								{
									if (num != 1145259591)
									{
										if (num == 1145852993)
										{
											goto IL_04A5;
										}
										if (num == 1146242381)
										{
											this.RemoveDevice(inputDevice2, false);
											if (inputDevice2.native && !inputDevice2.description.empty)
											{
												ArrayHelpers.AppendWithCapacity<InputDevice>(ref this.m_DisconnectedDevices, ref this.m_DisconnectedDevicesCount, inputDevice2, 10);
												DelegateHelpers.InvokeCallbacksSafe<InputDevice, InputDeviceChange>(ref this.m_DeviceChangeListeners, inputDevice2, InputDeviceChange.Disconnected, "InputSystem.onDeviceChange", null);
											}
										}
									}
									else
									{
										inputDevice2.NotifyConfigurationChanged();
										InputActionState.OnDeviceChange(inputDevice2, InputDeviceChange.ConfigurationChanged);
										DelegateHelpers.InvokeCallbacksSafe<InputDevice, InputDeviceChange>(ref this.m_DeviceChangeListeners, inputDevice2, InputDeviceChange.ConfigurationChanged, "InputSystem.onDeviceChange", null);
									}
								}
								else if (num <= 1229800787)
								{
									if (num != 1146245972)
									{
										if (num == 1229800787)
										{
											IMECompositionEvent* ptr4 = (IMECompositionEvent*)ptr2;
											ITextInputReceiver textInputReceiver = inputDevice2 as ITextInputReceiver;
											if (textInputReceiver != null)
											{
												textInputReceiver.OnIMECompositionChanged(ptr4->compositionString);
											}
										}
									}
									else
									{
										this.ResetDevice(inputDevice2, ((DeviceResetEvent*)ptr2)->hardReset, null);
									}
								}
								else
								{
									if (num == 1398030676)
									{
										goto IL_04A5;
									}
									if (num == 1413830740)
									{
										TextEvent* ptr5 = (TextEvent*)ptr2;
										ITextInputReceiver textInputReceiver2 = inputDevice2 as ITextInputReceiver;
										if (textInputReceiver2 != null)
										{
											int num5 = ptr5->character;
											if (num5 >= 65536)
											{
												num5 -= 65536;
												int num6 = 55296 + ((num5 >> 10) & 1023);
												int num7 = 56320 + (num5 & 1023);
												textInputReceiver2.OnTextInput((char)num6);
												textInputReceiver2.OnTextInput((char)num7);
											}
											else
											{
												textInputReceiver2.OnTextInput((char)num5);
											}
										}
									}
								}
								IL_06AE:
								this.m_InputEventStream.Advance(false);
								continue;
								IL_04A5:
								InputEventPtr inputEventPtr = new InputEventPtr(ptr2);
								bool hasStateCallbacks = inputDevice2.hasStateCallbacks;
								if (internalTime < inputDevice2.m_LastUpdateTimeInternal && (!hasStateCallbacks || !(inputDevice2.stateBlock.format != inputEventPtr.stateFormat)))
								{
									goto IL_06AE;
								}
								bool flag4;
								if (hasStateCallbacks)
								{
									this.m_ShouldMakeCurrentlyUpdatingDeviceCurrent = true;
									((IInputStateCallbackReceiver)inputDevice2).OnStateEvent(inputEventPtr);
									flag4 = this.m_ShouldMakeCurrentlyUpdatingDeviceCurrent;
								}
								else
								{
									if (inputDevice2.stateBlock.format != inputEventPtr.stateFormat)
									{
										goto IL_06AE;
									}
									flag4 = this.UpdateState(inputDevice2, inputEventPtr, updateType);
								}
								num4 += inputEventPtr.sizeInBytes;
								if (inputDevice2.m_LastUpdateTimeInternal <= inputEventPtr.internalTime)
								{
									inputDevice2.m_LastUpdateTimeInternal = inputEventPtr.internalTime;
								}
								if (flag4)
								{
									inputDevice2.MakeCurrent();
									goto IL_06AE;
								}
								goto IL_06AE;
							}
						}
					}
				}
				this.m_Metrics.totalEventProcessingTime = this.m_Metrics.totalEventProcessingTime + (double)(Stopwatch.GetTimestamp() - timestamp) / (double)Stopwatch.Frequency;
				this.m_Metrics.totalEventLagTime = this.m_Metrics.totalEventLagTime + num3;
				this.m_InputEventStream.Close(ref eventBuffer);
			}
			catch (Exception)
			{
				this.m_InputEventStream.CleanUpAfterException();
				throw;
			}
			if (flag)
			{
				this.ProcessStateChangeMonitorTimeouts();
			}
			this.InvokeAfterUpdateCallback(updateType);
			this.m_CurrentUpdate = InputUpdateType.None;
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x00031120 File Offset: 0x0002F320
		private void InvokeAfterUpdateCallback(InputUpdateType updateType)
		{
			if (updateType == InputUpdateType.Editor && this.gameIsPlaying)
			{
				return;
			}
			DelegateHelpers.InvokeCallbacksSafe(ref this.m_AfterUpdateListeners, "InputSystem.onAfterUpdate", null);
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x00031140 File Offset: 0x0002F340
		internal void DontMakeCurrentlyUpdatingDeviceCurrent()
		{
			this.m_ShouldMakeCurrentlyUpdatingDeviceCurrent = false;
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x0003114C File Offset: 0x0002F34C
		internal unsafe bool UpdateState(InputDevice device, InputEvent* eventPtr, InputUpdateType updateType)
		{
			InputStateBlock stateBlock = device.m_StateBlock;
			uint num = stateBlock.sizeInBits / 8U;
			uint num2 = 0U;
			byte* ptr;
			uint num3;
			if (eventPtr->type == 1398030676)
			{
				StateEvent stateEvent = *(StateEvent*)eventPtr;
				uint stateSizeInBytes = ((StateEvent*)eventPtr)->stateSizeInBytes;
				ptr = (byte*)((StateEvent*)eventPtr)->state;
				num3 = stateSizeInBytes;
				if (num3 > num)
				{
					num3 = num;
				}
			}
			else
			{
				DeltaStateEvent deltaStateEvent = *(DeltaStateEvent*)eventPtr;
				uint deltaStateSizeInBytes = ((DeltaStateEvent*)eventPtr)->deltaStateSizeInBytes;
				ptr = (byte*)((DeltaStateEvent*)eventPtr)->deltaState;
				num2 = ((DeltaStateEvent*)eventPtr)->stateOffset;
				num3 = deltaStateSizeInBytes;
				if (num2 + num3 > num)
				{
					if (num2 >= num)
					{
						return false;
					}
					num3 = num - num2;
				}
			}
			return this.UpdateState(device, updateType, (void*)ptr, num2, num3, eventPtr->internalTime, eventPtr);
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x000311F4 File Offset: 0x0002F3F4
		internal unsafe bool UpdateState(InputDevice device, InputUpdateType updateType, void* statePtr, uint stateOffsetInDevice, uint stateSize, double internalTime, InputEventPtr eventPtr = default(InputEventPtr))
		{
			int deviceIndex = device.m_DeviceIndex;
			ref InputStateBlock ptr = ref device.m_StateBlock;
			byte* frontBufferForDevice = (byte*)InputStateBuffers.GetFrontBufferForDevice(deviceIndex);
			this.SortStateChangeMonitorsIfNecessary(deviceIndex);
			bool flag = this.ProcessStateChangeMonitors(deviceIndex, statePtr, (void*)(frontBufferForDevice + ptr.byteOffset), stateSize, stateOffsetInDevice);
			uint num = device.m_StateBlock.byteOffset + stateOffsetInDevice;
			void* ptr2 = (void*)(frontBufferForDevice + num);
			byte* ptr3 = (device.noisy ? ((byte*)InputStateBuffers.s_NoiseMaskBuffer + num) : null);
			bool flag2 = !MemoryHelpers.MemCmpBitRegion(ptr2, statePtr, 0U, stateSize * 8U, (void*)ptr3);
			bool flag3 = this.FlipBuffersForDeviceIfNecessary(device, updateType);
			this.WriteStateChange(this.m_StateBuffers.m_PlayerStateBuffers, deviceIndex, ref ptr, stateOffsetInDevice, statePtr, stateSize, flag3);
			DelegateHelpers.InvokeCallbacksSafe<InputDevice, InputEventPtr>(ref this.m_DeviceStateChangeListeners, device, eventPtr, "InputSystem.onDeviceStateChange", null);
			if (flag)
			{
				this.FireStateChangeNotifications(deviceIndex, internalTime, eventPtr);
			}
			return flag2;
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x000312BC File Offset: 0x0002F4BC
		private unsafe void WriteStateChange(InputStateBuffers.DoubleBuffers buffers, int deviceIndex, ref InputStateBlock deviceStateBlock, uint stateOffsetInDevice, void* statePtr, uint stateSizeInBytes, bool flippedBuffers)
		{
			void* frontBuffer = buffers.GetFrontBuffer(deviceIndex);
			uint num = deviceStateBlock.sizeInBits / 8U;
			if (flippedBuffers && num != stateSizeInBytes)
			{
				void* backBuffer = buffers.GetBackBuffer(deviceIndex);
				UnsafeUtility.MemCpy((void*)((byte*)frontBuffer + deviceStateBlock.byteOffset), (void*)((byte*)backBuffer + deviceStateBlock.byteOffset), (long)((ulong)num));
			}
			if (InputSettings.readValueCachingFeatureEnabled)
			{
				byte* ptr = (byte*)frontBuffer;
				if (flippedBuffers && num == stateSizeInBytes)
				{
					ptr = (byte*)buffers.GetBackBuffer(deviceIndex);
				}
				this.m_Devices[deviceIndex].WriteChangedControlStates(ptr + deviceStateBlock.byteOffset, statePtr, stateSizeInBytes, stateOffsetInDevice);
			}
			UnsafeUtility.MemCpy((void*)((byte*)((byte*)frontBuffer + deviceStateBlock.byteOffset) + stateOffsetInDevice), statePtr, (long)((ulong)stateSizeInBytes));
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x00031354 File Offset: 0x0002F554
		private bool FlipBuffersForDeviceIfNecessary(InputDevice device, InputUpdateType updateType)
		{
			if (updateType == InputUpdateType.BeforeRender)
			{
				return false;
			}
			if (device.m_CurrentUpdateStepCount != InputUpdate.s_UpdateStepCount)
			{
				this.m_StateBuffers.m_PlayerStateBuffers.SwapBuffers(device.m_DeviceIndex);
				device.m_CurrentUpdateStepCount = InputUpdate.s_UpdateStepCount;
				return true;
			}
			return false;
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x00031390 File Offset: 0x0002F590
		public void AddStateChangeMonitor(InputControl control, IInputStateChangeMonitor monitor, long monitorIndex, uint groupIndex)
		{
			int deviceIndex = control.device.m_DeviceIndex;
			if (this.m_StateChangeMonitors == null)
			{
				this.m_StateChangeMonitors = new InputManager.StateChangeMonitorsForDevice[this.m_DevicesCount];
			}
			else if (this.m_StateChangeMonitors.Length <= deviceIndex)
			{
				Array.Resize<InputManager.StateChangeMonitorsForDevice>(ref this.m_StateChangeMonitors, this.m_DevicesCount);
			}
			if (!this.isProcessingEvents && this.m_StateChangeMonitors[deviceIndex].needToCompactArrays)
			{
				this.m_StateChangeMonitors[deviceIndex].CompactArrays();
			}
			this.m_StateChangeMonitors[deviceIndex].Add(control, monitor, monitorIndex, groupIndex);
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x00031424 File Offset: 0x0002F624
		private void RemoveStateChangeMonitors(InputDevice device)
		{
			if (this.m_StateChangeMonitors == null)
			{
				return;
			}
			int deviceIndex = device.m_DeviceIndex;
			if (deviceIndex >= this.m_StateChangeMonitors.Length)
			{
				return;
			}
			this.m_StateChangeMonitors[deviceIndex].Clear();
			for (int i = 0; i < this.m_StateChangeMonitorTimeouts.length; i++)
			{
				InputControl control = this.m_StateChangeMonitorTimeouts[i].control;
				if (((control != null) ? control.device : null) == device)
				{
					this.m_StateChangeMonitorTimeouts[i] = default(InputManager.StateChangeMonitorTimeout);
				}
			}
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x000314AC File Offset: 0x0002F6AC
		public void RemoveStateChangeMonitor(InputControl control, IInputStateChangeMonitor monitor, long monitorIndex)
		{
			if (this.m_StateChangeMonitors == null)
			{
				return;
			}
			int deviceIndex = control.device.m_DeviceIndex;
			if (deviceIndex == -1)
			{
				return;
			}
			if (deviceIndex >= this.m_StateChangeMonitors.Length)
			{
				return;
			}
			this.m_StateChangeMonitors[deviceIndex].Remove(monitor, monitorIndex, this.isProcessingEvents);
			for (int i = 0; i < this.m_StateChangeMonitorTimeouts.length; i++)
			{
				if (this.m_StateChangeMonitorTimeouts[i].monitor == monitor && this.m_StateChangeMonitorTimeouts[i].monitorIndex == monitorIndex)
				{
					this.m_StateChangeMonitorTimeouts[i] = default(InputManager.StateChangeMonitorTimeout);
				}
			}
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x0003154C File Offset: 0x0002F74C
		public void AddStateChangeMonitorTimeout(InputControl control, IInputStateChangeMonitor monitor, double time, long monitorIndex, int timerIndex)
		{
			this.m_StateChangeMonitorTimeouts.Append(new InputManager.StateChangeMonitorTimeout
			{
				control = control,
				time = time,
				monitor = monitor,
				monitorIndex = monitorIndex,
				timerIndex = timerIndex
			});
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x00031598 File Offset: 0x0002F798
		public void RemoveStateChangeMonitorTimeout(IInputStateChangeMonitor monitor, long monitorIndex, int timerIndex)
		{
			int length = this.m_StateChangeMonitorTimeouts.length;
			for (int i = 0; i < length; i++)
			{
				if (this.m_StateChangeMonitorTimeouts[i].monitor == monitor && this.m_StateChangeMonitorTimeouts[i].monitorIndex == monitorIndex && this.m_StateChangeMonitorTimeouts[i].timerIndex == timerIndex)
				{
					this.m_StateChangeMonitorTimeouts[i] = default(InputManager.StateChangeMonitorTimeout);
					return;
				}
			}
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x0003160F File Offset: 0x0002F80F
		private void SortStateChangeMonitorsIfNecessary(int deviceIndex)
		{
			if (this.m_StateChangeMonitors != null && deviceIndex < this.m_StateChangeMonitors.Length && this.m_StateChangeMonitors[deviceIndex].needToUpdateOrderingOfMonitors)
			{
				this.m_StateChangeMonitors[deviceIndex].SortMonitorsByIndex();
			}
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x00031648 File Offset: 0x0002F848
		public void SignalStateChangeMonitor(InputControl control, IInputStateChangeMonitor monitor)
		{
			int deviceIndex = control.device.m_DeviceIndex;
			ref InputManager.StateChangeMonitorsForDevice ptr = ref this.m_StateChangeMonitors[deviceIndex];
			for (int i = 0; i < ptr.signalled.length; i++)
			{
				this.SortStateChangeMonitorsIfNecessary(i);
				ref InputManager.StateChangeMonitorListener ptr2 = ref ptr.listeners[i];
				if (ptr2.control == control && ptr2.monitor == monitor)
				{
					ptr.signalled.SetBit(i);
				}
			}
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x000316B8 File Offset: 0x0002F8B8
		public void FireStateChangeNotifications()
		{
			double currentTime = this.m_Runtime.currentTime;
			int num = Math.Min(this.m_StateChangeMonitors.LengthSafe<InputManager.StateChangeMonitorsForDevice>(), this.m_DevicesCount);
			for (int i = 0; i < num; i++)
			{
				this.FireStateChangeNotifications(i, currentTime, null);
			}
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x00031700 File Offset: 0x0002F900
		private unsafe bool ProcessStateChangeMonitors(int deviceIndex, void* newStateFromEvent, void* oldStateOfDevice, uint newStateSizeInBytes, uint newStateOffsetInBytes)
		{
			if (this.m_StateChangeMonitors == null)
			{
				return false;
			}
			if (deviceIndex >= this.m_StateChangeMonitors.Length)
			{
				return false;
			}
			MemoryHelpers.BitRegion[] memoryRegions = this.m_StateChangeMonitors[deviceIndex].memoryRegions;
			if (memoryRegions == null)
			{
				return false;
			}
			int num = this.m_StateChangeMonitors[deviceIndex].count;
			bool flag = false;
			DynamicBitfield signalled = this.m_StateChangeMonitors[deviceIndex].signalled;
			bool flag2 = false;
			MemoryHelpers.BitRegion bitRegion = new MemoryHelpers.BitRegion(newStateOffsetInBytes, 0U, newStateSizeInBytes * 8U);
			for (int i = 0; i < num; i++)
			{
				MemoryHelpers.BitRegion bitRegion2 = memoryRegions[i];
				if (bitRegion2.sizeInBits == 0U)
				{
					int num2 = num;
					int num3 = num;
					this.m_StateChangeMonitors[deviceIndex].listeners.EraseAtWithCapacity(ref num2, i);
					memoryRegions.EraseAtWithCapacity(ref num3, i);
					signalled.SetLength(num - 1);
					flag2 = true;
					num--;
					i--;
				}
				else
				{
					MemoryHelpers.BitRegion bitRegion3 = bitRegion.Overlap(bitRegion2);
					if (!bitRegion3.isEmpty && !MemoryHelpers.Compare(oldStateOfDevice, (void*)((byte*)newStateFromEvent - newStateOffsetInBytes), bitRegion3))
					{
						signalled.SetBit(i);
						flag2 = true;
						flag = true;
					}
				}
			}
			if (flag2)
			{
				this.m_StateChangeMonitors[deviceIndex].signalled = signalled;
			}
			this.m_StateChangeMonitors[deviceIndex].needToCompactArrays = false;
			return flag;
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x00031838 File Offset: 0x0002FA38
		internal unsafe void FireStateChangeNotifications(int deviceIndex, double internalTime, InputEvent* eventPtr)
		{
			ref DynamicBitfield ptr = ref this.m_StateChangeMonitors[deviceIndex].signalled;
			ref InputManager.StateChangeMonitorListener[] ptr2 = ref this.m_StateChangeMonitors[deviceIndex].listeners;
			double num = internalTime - InputRuntime.s_CurrentTimeOffsetToRealtimeSinceStartup;
			InputEvent inputEvent = new InputEvent(new FourCC('F', 'A', 'K', 'E'), 20, -1, internalTime);
			if (eventPtr == null)
			{
				eventPtr = (InputEvent*)UnsafeUtility.AddressOf<InputEvent>(ref inputEvent);
			}
			eventPtr->handled = false;
			for (int i = 0; i < ptr.length; i++)
			{
				if (ptr.TestBit(i))
				{
					InputManager.StateChangeMonitorListener stateChangeMonitorListener = ptr2[i];
					try
					{
						stateChangeMonitorListener.monitor.NotifyControlStateChanged(stateChangeMonitorListener.control, num, eventPtr, stateChangeMonitorListener.monitorIndex);
					}
					catch (Exception ex)
					{
						Debug.LogError(string.Format("Exception '{0}' thrown from state change monitor '{1}' on '{2}'", ex.GetType().Name, stateChangeMonitorListener.monitor.GetType().Name, stateChangeMonitorListener.control));
						Debug.LogException(ex);
					}
					if (eventPtr->handled)
					{
						uint groupIndex = ptr2[i].groupIndex;
						for (int j = i + 1; j < ptr.length; j++)
						{
							if (ptr2[j].groupIndex == groupIndex && ptr2[j].monitor == stateChangeMonitorListener.monitor)
							{
								ptr.ClearBit(j);
							}
						}
						eventPtr->handled = false;
					}
					ptr.ClearBit(i);
				}
			}
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x000319B4 File Offset: 0x0002FBB4
		private void ProcessStateChangeMonitorTimeouts()
		{
			if (this.m_StateChangeMonitorTimeouts.length == 0)
			{
				return;
			}
			double num = this.m_Runtime.currentTime - InputRuntime.s_CurrentTimeOffsetToRealtimeSinceStartup;
			int num2 = 0;
			for (int i = 0; i < this.m_StateChangeMonitorTimeouts.length; i++)
			{
				if (this.m_StateChangeMonitorTimeouts[i].control != null)
				{
					if (this.m_StateChangeMonitorTimeouts[i].time <= num)
					{
						InputManager.StateChangeMonitorTimeout stateChangeMonitorTimeout = this.m_StateChangeMonitorTimeouts[i];
						stateChangeMonitorTimeout.monitor.NotifyTimerExpired(stateChangeMonitorTimeout.control, num, stateChangeMonitorTimeout.monitorIndex, stateChangeMonitorTimeout.timerIndex);
					}
					else
					{
						if (i != num2)
						{
							this.m_StateChangeMonitorTimeouts[num2] = this.m_StateChangeMonitorTimeouts[i];
						}
						num2++;
					}
				}
			}
			this.m_StateChangeMonitorTimeouts.SetLength(num2);
		}

		// Token: 0x04000264 RID: 612
		internal int m_LayoutRegistrationVersion;

		// Token: 0x04000265 RID: 613
		private float m_PollingFrequency;

		// Token: 0x04000266 RID: 614
		internal InputControlLayout.Collection m_Layouts;

		// Token: 0x04000267 RID: 615
		private TypeTable m_Processors;

		// Token: 0x04000268 RID: 616
		private TypeTable m_Interactions;

		// Token: 0x04000269 RID: 617
		private TypeTable m_Composites;

		// Token: 0x0400026A RID: 618
		private int m_DevicesCount;

		// Token: 0x0400026B RID: 619
		private InputDevice[] m_Devices;

		// Token: 0x0400026C RID: 620
		private Dictionary<int, InputDevice> m_DevicesById;

		// Token: 0x0400026D RID: 621
		internal int m_AvailableDeviceCount;

		// Token: 0x0400026E RID: 622
		internal InputManager.AvailableDevice[] m_AvailableDevices;

		// Token: 0x0400026F RID: 623
		internal int m_DisconnectedDevicesCount;

		// Token: 0x04000270 RID: 624
		internal InputDevice[] m_DisconnectedDevices;

		// Token: 0x04000271 RID: 625
		internal InputUpdateType m_UpdateMask;

		// Token: 0x04000272 RID: 626
		private InputUpdateType m_CurrentUpdate;

		// Token: 0x04000273 RID: 627
		internal InputStateBuffers m_StateBuffers;

		// Token: 0x04000274 RID: 628
		private CallbackArray<Action<InputDevice, InputDeviceChange>> m_DeviceChangeListeners;

		// Token: 0x04000275 RID: 629
		private CallbackArray<Action<InputDevice, InputEventPtr>> m_DeviceStateChangeListeners;

		// Token: 0x04000276 RID: 630
		private CallbackArray<InputDeviceFindControlLayoutDelegate> m_DeviceFindLayoutCallbacks;

		// Token: 0x04000277 RID: 631
		internal CallbackArray<InputDeviceCommandDelegate> m_DeviceCommandCallbacks;

		// Token: 0x04000278 RID: 632
		private CallbackArray<Action<string, InputControlLayoutChange>> m_LayoutChangeListeners;

		// Token: 0x04000279 RID: 633
		private CallbackArray<Action<InputEventPtr, InputDevice>> m_EventListeners;

		// Token: 0x0400027A RID: 634
		private CallbackArray<Action> m_BeforeUpdateListeners;

		// Token: 0x0400027B RID: 635
		private CallbackArray<Action> m_AfterUpdateListeners;

		// Token: 0x0400027C RID: 636
		private CallbackArray<Action> m_SettingsChangedListeners;

		// Token: 0x0400027D RID: 637
		private bool m_NativeBeforeUpdateHooked;

		// Token: 0x0400027E RID: 638
		private bool m_HaveDevicesWithStateCallbackReceivers;

		// Token: 0x0400027F RID: 639
		private bool m_HasFocus;

		// Token: 0x04000280 RID: 640
		private InputEventStream m_InputEventStream;

		// Token: 0x04000281 RID: 641
		private InputDeviceExecuteCommandDelegate m_DeviceFindExecuteCommandDelegate;

		// Token: 0x04000282 RID: 642
		private int m_DeviceFindExecuteCommandDeviceId;

		// Token: 0x04000283 RID: 643
		internal IInputRuntime m_Runtime;

		// Token: 0x04000284 RID: 644
		internal InputMetrics m_Metrics;

		// Token: 0x04000285 RID: 645
		internal InputSettings m_Settings;

		// Token: 0x04000286 RID: 646
		private bool m_ShouldMakeCurrentlyUpdatingDeviceCurrent;

		// Token: 0x04000287 RID: 647
		internal InputManager.StateChangeMonitorsForDevice[] m_StateChangeMonitors;

		// Token: 0x04000288 RID: 648
		private InlinedArray<InputManager.StateChangeMonitorTimeout> m_StateChangeMonitorTimeouts;

		// Token: 0x020001A3 RID: 419
		internal enum DeviceDisableScope
		{
			// Token: 0x040008B3 RID: 2227
			Everywhere,
			// Token: 0x040008B4 RID: 2228
			InFrontendOnly,
			// Token: 0x040008B5 RID: 2229
			TemporaryWhilePlayerIsInBackground
		}

		// Token: 0x020001A4 RID: 420
		[Serializable]
		internal struct AvailableDevice
		{
			// Token: 0x040008B6 RID: 2230
			public InputDeviceDescription description;

			// Token: 0x040008B7 RID: 2231
			public int deviceId;

			// Token: 0x040008B8 RID: 2232
			public bool isNative;

			// Token: 0x040008B9 RID: 2233
			public bool isRemoved;
		}

		// Token: 0x020001A5 RID: 421
		private struct StateChangeMonitorTimeout
		{
			// Token: 0x040008BA RID: 2234
			public InputControl control;

			// Token: 0x040008BB RID: 2235
			public double time;

			// Token: 0x040008BC RID: 2236
			public IInputStateChangeMonitor monitor;

			// Token: 0x040008BD RID: 2237
			public long monitorIndex;

			// Token: 0x040008BE RID: 2238
			public int timerIndex;
		}

		// Token: 0x020001A6 RID: 422
		internal struct StateChangeMonitorListener
		{
			// Token: 0x040008BF RID: 2239
			public InputControl control;

			// Token: 0x040008C0 RID: 2240
			public IInputStateChangeMonitor monitor;

			// Token: 0x040008C1 RID: 2241
			public long monitorIndex;

			// Token: 0x040008C2 RID: 2242
			public uint groupIndex;
		}

		// Token: 0x020001A7 RID: 423
		internal struct StateChangeMonitorsForDevice
		{
			// Token: 0x1700054F RID: 1359
			// (get) Token: 0x060013B5 RID: 5045 RVA: 0x0005B324 File Offset: 0x00059524
			public int count
			{
				get
				{
					return this.signalled.length;
				}
			}

			// Token: 0x060013B6 RID: 5046 RVA: 0x0005B334 File Offset: 0x00059534
			public void Add(InputControl control, IInputStateChangeMonitor monitor, long monitorIndex, uint groupIndex)
			{
				int length = this.signalled.length;
				ArrayHelpers.AppendWithCapacity<InputManager.StateChangeMonitorListener>(ref this.listeners, ref length, new InputManager.StateChangeMonitorListener
				{
					monitor = monitor,
					monitorIndex = monitorIndex,
					groupIndex = groupIndex,
					control = control
				}, 10);
				ref InputStateBlock ptr = ref control.m_StateBlock;
				int length2 = this.signalled.length;
				ArrayHelpers.AppendWithCapacity<MemoryHelpers.BitRegion>(ref this.memoryRegions, ref length2, new MemoryHelpers.BitRegion(ptr.byteOffset - control.device.stateBlock.byteOffset, ptr.bitOffset, ptr.sizeInBits), 10);
				this.signalled.SetLength(this.signalled.length + 1);
				this.needToUpdateOrderingOfMonitors = true;
			}

			// Token: 0x060013B7 RID: 5047 RVA: 0x0005B3F8 File Offset: 0x000595F8
			public void Remove(IInputStateChangeMonitor monitor, long monitorIndex, bool deferRemoval)
			{
				if (this.listeners == null)
				{
					return;
				}
				int i = 0;
				while (i < this.signalled.length)
				{
					if (this.listeners[i].monitor == monitor && this.listeners[i].monitorIndex == monitorIndex)
					{
						if (deferRemoval)
						{
							this.listeners[i] = default(InputManager.StateChangeMonitorListener);
							this.memoryRegions[i] = default(MemoryHelpers.BitRegion);
							this.signalled.ClearBit(i);
							this.needToCompactArrays = true;
							return;
						}
						this.RemoveAt(i);
						return;
					}
					else
					{
						i++;
					}
				}
			}

			// Token: 0x060013B8 RID: 5048 RVA: 0x0005B48F File Offset: 0x0005968F
			public void Clear()
			{
				this.listeners.Clear(this.count);
				this.signalled.SetLength(0);
				this.needToCompactArrays = false;
			}

			// Token: 0x060013B9 RID: 5049 RVA: 0x0005B4B8 File Offset: 0x000596B8
			public void CompactArrays()
			{
				for (int i = this.count - 1; i >= 0; i--)
				{
					if (this.memoryRegions[i].sizeInBits == 0U)
					{
						this.RemoveAt(i);
					}
				}
				this.needToCompactArrays = false;
			}

			// Token: 0x060013BA RID: 5050 RVA: 0x0005B4FC File Offset: 0x000596FC
			private void RemoveAt(int i)
			{
				int count = this.count;
				int count2 = this.count;
				this.listeners.EraseAtWithCapacity(ref count, i);
				this.memoryRegions.EraseAtWithCapacity(ref count2, i);
				this.signalled.SetLength(this.count - 1);
			}

			// Token: 0x060013BB RID: 5051 RVA: 0x0005B548 File Offset: 0x00059748
			public void SortMonitorsByIndex()
			{
				for (int i = 1; i < this.signalled.length; i++)
				{
					for (int j = i; j > 0; j--)
					{
						int complexityFromMonitorIndex = InputActionState.GetComplexityFromMonitorIndex(this.listeners[j - 1].monitorIndex);
						int complexityFromMonitorIndex2 = InputActionState.GetComplexityFromMonitorIndex(this.listeners[j].monitorIndex);
						if (complexityFromMonitorIndex >= complexityFromMonitorIndex2)
						{
							break;
						}
						this.listeners.SwapElements(j, j - 1);
						this.memoryRegions.SwapElements(j, j - 1);
					}
				}
				this.needToUpdateOrderingOfMonitors = false;
			}

			// Token: 0x040008C3 RID: 2243
			public MemoryHelpers.BitRegion[] memoryRegions;

			// Token: 0x040008C4 RID: 2244
			public InputManager.StateChangeMonitorListener[] listeners;

			// Token: 0x040008C5 RID: 2245
			public DynamicBitfield signalled;

			// Token: 0x040008C6 RID: 2246
			public bool needToUpdateOrderingOfMonitors;

			// Token: 0x040008C7 RID: 2247
			public bool needToCompactArrays;
		}
	}
}
