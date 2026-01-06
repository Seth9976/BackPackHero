using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.InputSystem.DualShock;
using UnityEngine.InputSystem.Haptics;
using UnityEngine.InputSystem.HID;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Switch;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.InputSystem.XInput;
using UnityEngine.InputSystem.XR;

namespace UnityEngine.InputSystem
{
	// Token: 0x0200002C RID: 44
	public static class InputSystem
	{
		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000317 RID: 791 RVA: 0x0000E114 File Offset: 0x0000C314
		// (remove) Token: 0x06000318 RID: 792 RVA: 0x0000E158 File Offset: 0x0000C358
		public static event Action<string, InputControlLayoutChange> onLayoutChange
		{
			add
			{
				InputManager inputManager = InputSystem.s_Manager;
				lock (inputManager)
				{
					InputSystem.s_Manager.onLayoutChange += value;
				}
			}
			remove
			{
				InputManager inputManager = InputSystem.s_Manager;
				lock (inputManager)
				{
					InputSystem.s_Manager.onLayoutChange -= value;
				}
			}
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000E19C File Offset: 0x0000C39C
		public static void RegisterLayout(Type type, string name = null, InputDeviceMatcher? matches = null)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (string.IsNullOrEmpty(name))
			{
				name = type.Name;
			}
			InputSystem.s_Manager.RegisterControlLayout(name, type);
			if (matches != null)
			{
				InputSystem.s_Manager.RegisterControlLayoutMatcher(name, matches.Value);
			}
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000E1F4 File Offset: 0x0000C3F4
		public static void RegisterLayout<T>(string name = null, InputDeviceMatcher? matches = null) where T : InputControl
		{
			InputSystem.RegisterLayout(typeof(T), name, matches);
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000E207 File Offset: 0x0000C407
		public static void RegisterLayout(string json, string name = null, InputDeviceMatcher? matches = null)
		{
			InputSystem.s_Manager.RegisterControlLayout(json, name, false);
			if (matches != null)
			{
				InputSystem.s_Manager.RegisterControlLayoutMatcher(name, matches.Value);
			}
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000E231 File Offset: 0x0000C431
		public static void RegisterLayoutOverride(string json, string name = null)
		{
			InputSystem.s_Manager.RegisterControlLayout(json, name, true);
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0000E240 File Offset: 0x0000C440
		public static void RegisterLayoutMatcher(string layoutName, InputDeviceMatcher matcher)
		{
			InputSystem.s_Manager.RegisterControlLayoutMatcher(layoutName, matcher);
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0000E24E File Offset: 0x0000C44E
		public static void RegisterLayoutMatcher<TDevice>(InputDeviceMatcher matcher) where TDevice : InputDevice
		{
			InputSystem.s_Manager.RegisterControlLayoutMatcher(typeof(TDevice), matcher);
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000E268 File Offset: 0x0000C468
		public static void RegisterLayoutBuilder(Func<InputControlLayout> buildMethod, string name, string baseLayout = null, InputDeviceMatcher? matches = null)
		{
			if (buildMethod == null)
			{
				throw new ArgumentNullException("buildMethod");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			InputSystem.s_Manager.RegisterControlLayoutBuilder(buildMethod, name, baseLayout);
			if (matches != null)
			{
				InputSystem.s_Manager.RegisterControlLayoutMatcher(name, matches.Value);
			}
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000E2BE File Offset: 0x0000C4BE
		public static void RegisterPrecompiledLayout<TDevice>(string metadata) where TDevice : InputDevice, new()
		{
			InputSystem.s_Manager.RegisterPrecompiledLayout<TDevice>(metadata);
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000E2CB File Offset: 0x0000C4CB
		public static void RemoveLayout(string name)
		{
			InputSystem.s_Manager.RemoveControlLayout(name);
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000E2D8 File Offset: 0x0000C4D8
		public static string TryFindMatchingLayout(InputDeviceDescription deviceDescription)
		{
			return InputSystem.s_Manager.TryFindMatchingControlLayout(ref deviceDescription, 0);
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000E2EC File Offset: 0x0000C4EC
		public static IEnumerable<string> ListLayouts()
		{
			return InputSystem.s_Manager.ListControlLayouts(null);
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0000E2F9 File Offset: 0x0000C4F9
		public static IEnumerable<string> ListLayoutsBasedOn(string baseLayout)
		{
			if (string.IsNullOrEmpty(baseLayout))
			{
				throw new ArgumentNullException("baseLayout");
			}
			return InputSystem.s_Manager.ListControlLayouts(baseLayout);
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0000E319 File Offset: 0x0000C519
		public static InputControlLayout LoadLayout(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			return InputSystem.s_Manager.TryLoadControlLayout(new InternedString(name));
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0000E33E File Offset: 0x0000C53E
		public static InputControlLayout LoadLayout<TControl>() where TControl : InputControl
		{
			return InputSystem.s_Manager.TryLoadControlLayout(typeof(TControl));
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0000E354 File Offset: 0x0000C554
		public static string GetNameOfBaseLayout(string layoutName)
		{
			if (string.IsNullOrEmpty(layoutName))
			{
				throw new ArgumentNullException("layoutName");
			}
			InternedString internedString = new InternedString(layoutName);
			InternedString internedString2;
			if (InputControlLayout.s_Layouts.baseLayoutTable.TryGetValue(internedString, out internedString2))
			{
				return internedString2;
			}
			return null;
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0000E398 File Offset: 0x0000C598
		public static bool IsFirstLayoutBasedOnSecond(string firstLayoutName, string secondLayoutName)
		{
			if (string.IsNullOrEmpty(firstLayoutName))
			{
				throw new ArgumentNullException("firstLayoutName");
			}
			if (string.IsNullOrEmpty(secondLayoutName))
			{
				throw new ArgumentNullException("secondLayoutName");
			}
			InternedString internedString = new InternedString(firstLayoutName);
			InternedString internedString2 = new InternedString(secondLayoutName);
			return internedString == internedString2 || InputControlLayout.s_Layouts.IsBasedOn(internedString2, internedString);
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000E3F4 File Offset: 0x0000C5F4
		public static void RegisterProcessor(Type type, string name = null)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (string.IsNullOrEmpty(name))
			{
				name = type.Name;
				if (name.EndsWith("Processor"))
				{
					name = name.Substring(0, name.Length - "Processor".Length);
				}
			}
			Dictionary<InternedString, InputControlLayout.Collection.PrecompiledLayout> precompiledLayouts = InputSystem.s_Manager.m_Layouts.precompiledLayouts;
			foreach (InternedString internedString in new List<InternedString>(precompiledLayouts.Keys))
			{
				if (StringHelpers.CharacterSeparatedListsHaveAtLeastOneCommonElement(precompiledLayouts[internedString].metadata, name, ';'))
				{
					InputSystem.s_Manager.m_Layouts.precompiledLayouts.Remove(internedString);
				}
			}
			InputSystem.s_Manager.processors.AddTypeRegistration(name, type);
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000E4E0 File Offset: 0x0000C6E0
		public static void RegisterProcessor<T>(string name = null)
		{
			InputSystem.RegisterProcessor(typeof(T), name);
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000E4F4 File Offset: 0x0000C6F4
		public static Type TryGetProcessor(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			return InputSystem.s_Manager.processors.LookupTypeRegistration(name);
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0000E528 File Offset: 0x0000C728
		public static IEnumerable<string> ListProcessors()
		{
			return InputSystem.s_Manager.processors.names;
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600032D RID: 813 RVA: 0x0000E547 File Offset: 0x0000C747
		public static ReadOnlyArray<InputDevice> devices
		{
			get
			{
				return InputSystem.s_Manager.devices;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x0600032E RID: 814 RVA: 0x0000E553 File Offset: 0x0000C753
		public static ReadOnlyArray<InputDevice> disconnectedDevices
		{
			get
			{
				return new ReadOnlyArray<InputDevice>(InputSystem.s_Manager.m_DisconnectedDevices, 0, InputSystem.s_Manager.m_DisconnectedDevicesCount);
			}
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x0600032F RID: 815 RVA: 0x0000E570 File Offset: 0x0000C770
		// (remove) Token: 0x06000330 RID: 816 RVA: 0x0000E5C4 File Offset: 0x0000C7C4
		public static event Action<InputDevice, InputDeviceChange> onDeviceChange
		{
			add
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				InputManager inputManager = InputSystem.s_Manager;
				lock (inputManager)
				{
					InputSystem.s_Manager.onDeviceChange += value;
				}
			}
			remove
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				InputManager inputManager = InputSystem.s_Manager;
				lock (inputManager)
				{
					InputSystem.s_Manager.onDeviceChange -= value;
				}
			}
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000331 RID: 817 RVA: 0x0000E618 File Offset: 0x0000C818
		// (remove) Token: 0x06000332 RID: 818 RVA: 0x0000E66C File Offset: 0x0000C86C
		public static event InputDeviceCommandDelegate onDeviceCommand
		{
			add
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				InputManager inputManager = InputSystem.s_Manager;
				lock (inputManager)
				{
					InputSystem.s_Manager.onDeviceCommand += value;
				}
			}
			remove
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				InputManager inputManager = InputSystem.s_Manager;
				lock (inputManager)
				{
					InputSystem.s_Manager.onDeviceCommand -= value;
				}
			}
		}

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000333 RID: 819 RVA: 0x0000E6C0 File Offset: 0x0000C8C0
		// (remove) Token: 0x06000334 RID: 820 RVA: 0x0000E704 File Offset: 0x0000C904
		public static event InputDeviceFindControlLayoutDelegate onFindLayoutForDevice
		{
			add
			{
				InputManager inputManager = InputSystem.s_Manager;
				lock (inputManager)
				{
					InputSystem.s_Manager.onFindControlLayoutForDevice += value;
				}
			}
			remove
			{
				InputManager inputManager = InputSystem.s_Manager;
				lock (inputManager)
				{
					InputSystem.s_Manager.onFindControlLayoutForDevice -= value;
				}
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000335 RID: 821 RVA: 0x0000E748 File Offset: 0x0000C948
		// (set) Token: 0x06000336 RID: 822 RVA: 0x0000E754 File Offset: 0x0000C954
		public static float pollingFrequency
		{
			get
			{
				return InputSystem.s_Manager.pollingFrequency;
			}
			set
			{
				InputSystem.s_Manager.pollingFrequency = value;
			}
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000E761 File Offset: 0x0000C961
		public static InputDevice AddDevice(string layout, string name = null, string variants = null)
		{
			if (string.IsNullOrEmpty(layout))
			{
				throw new ArgumentNullException("layout");
			}
			return InputSystem.s_Manager.AddDevice(layout, name, new InternedString(variants));
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000E788 File Offset: 0x0000C988
		public static TDevice AddDevice<TDevice>(string name = null) where TDevice : InputDevice
		{
			InputDevice inputDevice = InputSystem.s_Manager.AddDevice(typeof(TDevice), name);
			TDevice tdevice = inputDevice as TDevice;
			if (tdevice == null)
			{
				if (inputDevice != null)
				{
					InputSystem.RemoveDevice(inputDevice);
				}
				throw new InvalidOperationException("Layout registered for type '" + typeof(TDevice).Name + "' did not produce a device of that type; layout probably has been overridden");
			}
			return tdevice;
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000E7EB File Offset: 0x0000C9EB
		public static InputDevice AddDevice(InputDeviceDescription description)
		{
			if (description.empty)
			{
				throw new ArgumentException("Description must not be empty", "description");
			}
			return InputSystem.s_Manager.AddDevice(description);
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000E811 File Offset: 0x0000CA11
		public static void AddDevice(InputDevice device)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			InputSystem.s_Manager.AddDevice(device);
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000E82C File Offset: 0x0000CA2C
		public static void RemoveDevice(InputDevice device)
		{
			InputSystem.s_Manager.RemoveDevice(device, false);
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000E83A File Offset: 0x0000CA3A
		public static void FlushDisconnectedDevices()
		{
			InputSystem.s_Manager.FlushDisconnectedDevices();
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000E846 File Offset: 0x0000CA46
		public static InputDevice GetDevice(string nameOrLayout)
		{
			return InputSystem.s_Manager.TryGetDevice(nameOrLayout);
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000E853 File Offset: 0x0000CA53
		public static TDevice GetDevice<TDevice>() where TDevice : InputDevice
		{
			return (TDevice)((object)InputSystem.GetDevice(typeof(TDevice)));
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0000E86C File Offset: 0x0000CA6C
		public static InputDevice GetDevice(Type type)
		{
			InputDevice inputDevice = null;
			double num = -1.0;
			foreach (InputDevice inputDevice2 in InputSystem.devices)
			{
				if (type.IsInstanceOfType(inputDevice2) && (inputDevice == null || inputDevice2.m_LastUpdateTimeInternal > num))
				{
					inputDevice = inputDevice2;
					num = inputDevice.m_LastUpdateTimeInternal;
				}
			}
			return inputDevice;
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0000E8EC File Offset: 0x0000CAEC
		public static TDevice GetDevice<TDevice>(InternedString usage) where TDevice : InputDevice
		{
			TDevice tdevice = default(TDevice);
			double num = -1.0;
			foreach (InputDevice inputDevice in InputSystem.devices)
			{
				TDevice tdevice2 = inputDevice as TDevice;
				if (tdevice2 != null && tdevice2.usages.Contains(usage) && (tdevice == null || tdevice2.m_LastUpdateTimeInternal > num))
				{
					tdevice = tdevice2;
					num = tdevice.m_LastUpdateTimeInternal;
				}
			}
			return tdevice;
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0000E99C File Offset: 0x0000CB9C
		public static TDevice GetDevice<TDevice>(string usage) where TDevice : InputDevice
		{
			return InputSystem.GetDevice<TDevice>(new InternedString(usage));
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0000E9A9 File Offset: 0x0000CBA9
		public static InputDevice GetDeviceById(int deviceId)
		{
			return InputSystem.s_Manager.TryGetDeviceById(deviceId);
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0000E9B6 File Offset: 0x0000CBB6
		public static List<InputDeviceDescription> GetUnsupportedDevices()
		{
			List<InputDeviceDescription> list = new List<InputDeviceDescription>();
			InputSystem.GetUnsupportedDevices(list);
			return list;
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000E9C4 File Offset: 0x0000CBC4
		public static int GetUnsupportedDevices(List<InputDeviceDescription> descriptions)
		{
			return InputSystem.s_Manager.GetUnsupportedDevices(descriptions);
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0000E9D1 File Offset: 0x0000CBD1
		public static void EnableDevice(InputDevice device)
		{
			InputSystem.s_Manager.EnableOrDisableDevice(device, true, InputManager.DeviceDisableScope.Everywhere);
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000E9E0 File Offset: 0x0000CBE0
		public static void DisableDevice(InputDevice device, bool keepSendingEvents = false)
		{
			InputSystem.s_Manager.EnableOrDisableDevice(device, false, keepSendingEvents ? InputManager.DeviceDisableScope.InFrontendOnly : InputManager.DeviceDisableScope.Everywhere);
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000E9F5 File Offset: 0x0000CBF5
		public static bool TrySyncDevice(InputDevice device)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			if (!device.added)
			{
				throw new InvalidOperationException(string.Format("Device '{0}' has not been added", device));
			}
			return device.RequestSync();
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000EA24 File Offset: 0x0000CC24
		public static void ResetDevice(InputDevice device, bool alsoResetDontResetControls = false)
		{
			InputSystem.s_Manager.ResetDevice(device, alsoResetDontResetControls, null);
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000EA46 File Offset: 0x0000CC46
		[Obsolete("Use 'ResetDevice' instead.", false)]
		public static bool TryResetDevice(InputDevice device)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			return device.RequestReset();
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000EA5C File Offset: 0x0000CC5C
		public static void PauseHaptics()
		{
			ReadOnlyArray<InputDevice> devices = InputSystem.devices;
			int count = devices.Count;
			for (int i = 0; i < count; i++)
			{
				IHaptics haptics = devices[i] as IHaptics;
				if (haptics != null)
				{
					haptics.PauseHaptics();
				}
			}
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0000EA9C File Offset: 0x0000CC9C
		public static void ResumeHaptics()
		{
			ReadOnlyArray<InputDevice> devices = InputSystem.devices;
			int count = devices.Count;
			for (int i = 0; i < count; i++)
			{
				IHaptics haptics = devices[i] as IHaptics;
				if (haptics != null)
				{
					haptics.ResumeHaptics();
				}
			}
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000EADC File Offset: 0x0000CCDC
		public static void ResetHaptics()
		{
			ReadOnlyArray<InputDevice> devices = InputSystem.devices;
			int count = devices.Count;
			for (int i = 0; i < count; i++)
			{
				IHaptics haptics = devices[i] as IHaptics;
				if (haptics != null)
				{
					haptics.ResetHaptics();
				}
			}
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000EB1A File Offset: 0x0000CD1A
		public static void SetDeviceUsage(InputDevice device, string usage)
		{
			InputSystem.SetDeviceUsage(device, new InternedString(usage));
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000EB28 File Offset: 0x0000CD28
		public static void SetDeviceUsage(InputDevice device, InternedString usage)
		{
			InputSystem.s_Manager.SetDeviceUsage(device, usage);
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0000EB36 File Offset: 0x0000CD36
		public static void AddDeviceUsage(InputDevice device, string usage)
		{
			InputSystem.s_Manager.AddDeviceUsage(device, new InternedString(usage));
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0000EB49 File Offset: 0x0000CD49
		public static void AddDeviceUsage(InputDevice device, InternedString usage)
		{
			InputSystem.s_Manager.AddDeviceUsage(device, usage);
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0000EB57 File Offset: 0x0000CD57
		public static void RemoveDeviceUsage(InputDevice device, string usage)
		{
			InputSystem.s_Manager.RemoveDeviceUsage(device, new InternedString(usage));
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0000EB6A File Offset: 0x0000CD6A
		public static void RemoveDeviceUsage(InputDevice device, InternedString usage)
		{
			InputSystem.s_Manager.RemoveDeviceUsage(device, usage);
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0000EB78 File Offset: 0x0000CD78
		public static InputControl FindControl(string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				throw new ArgumentNullException("path");
			}
			ReadOnlyArray<InputDevice> devices = InputSystem.s_Manager.devices;
			int count = devices.Count;
			for (int i = 0; i < count; i++)
			{
				InputControl inputControl = InputControlPath.TryFindControl(devices[i], path, 0);
				if (inputControl != null)
				{
					return inputControl;
				}
			}
			return null;
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000EBCD File Offset: 0x0000CDCD
		public static InputControlList<InputControl> FindControls(string path)
		{
			return InputSystem.FindControls<InputControl>(path);
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0000EBD8 File Offset: 0x0000CDD8
		public static InputControlList<TControl> FindControls<TControl>(string path) where TControl : InputControl
		{
			InputControlList<TControl> inputControlList = default(InputControlList<TControl>);
			InputSystem.FindControls<TControl>(path, ref inputControlList);
			return inputControlList;
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0000EBF7 File Offset: 0x0000CDF7
		public static int FindControls<TControl>(string path, ref InputControlList<TControl> controls) where TControl : InputControl
		{
			return InputSystem.s_Manager.GetControls<TControl>(path, ref controls);
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000357 RID: 855 RVA: 0x0000EC05 File Offset: 0x0000CE05
		internal static bool isProcessingEvents
		{
			get
			{
				return InputSystem.s_Manager.isProcessingEvents;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000358 RID: 856 RVA: 0x0000EC14 File Offset: 0x0000CE14
		// (set) Token: 0x06000359 RID: 857 RVA: 0x0000EC2A File Offset: 0x0000CE2A
		public static InputEventListener onEvent
		{
			get
			{
				return default(InputEventListener);
			}
			set
			{
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x0600035A RID: 858 RVA: 0x0000EC2C File Offset: 0x0000CE2C
		public static IObservable<InputControl> onAnyButtonPress
		{
			get
			{
				return from e in InputSystem.onEvent
					select e.GetFirstButtonPressOrNull(-1f, true) into c
					where c != null
					select c;
			}
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000EC8B File Offset: 0x0000CE8B
		public static void QueueEvent(InputEventPtr eventPtr)
		{
			if (!eventPtr.valid)
			{
				throw new ArgumentException("Received a null event pointer", "eventPtr");
			}
			InputSystem.s_Manager.QueueEvent(eventPtr);
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0000ECB1 File Offset: 0x0000CEB1
		public static void QueueEvent<TEvent>(ref TEvent inputEvent) where TEvent : struct, IInputEventTypeInfo
		{
			InputSystem.s_Manager.QueueEvent<TEvent>(ref inputEvent);
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0000ECC0 File Offset: 0x0000CEC0
		public unsafe static void QueueStateEvent<TState>(InputDevice device, TState state, double time = -1.0) where TState : struct, IInputStateTypeInfo
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			if (device.m_DeviceIndex == -1)
			{
				throw new InvalidOperationException(string.Format("Cannot queue state event for device '{0}' because device has not been added to system", device));
			}
			uint num = (uint)UnsafeUtility.SizeOf<TState>();
			if (num > 512U)
			{
				throw new ArgumentException(string.Format("Size of '{0}' exceeds maximum supported state size of {1}", typeof(TState).Name, 512), "state");
			}
			long num2 = (long)UnsafeUtility.SizeOf<StateEvent>() + (long)((ulong)num) - 1L;
			if (time < 0.0)
			{
				time = InputRuntime.s_Instance.currentTime;
			}
			else
			{
				time += InputRuntime.s_CurrentTimeOffsetToRealtimeSinceStartup;
			}
			InputSystem.StateEventBuffer stateEventBuffer;
			stateEventBuffer.stateEvent = new StateEvent
			{
				baseEvent = new InputEvent(1398030676, (int)num2, device.deviceId, time),
				stateFormat = state.format
			};
			UnsafeUtility.MemCpy((void*)(&stateEventBuffer.stateEvent.stateData.FixedElementField), UnsafeUtility.AddressOf<TState>(ref state), (long)((ulong)num));
			InputSystem.s_Manager.QueueEvent<StateEvent>(ref stateEventBuffer.stateEvent);
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0000EDD8 File Offset: 0x0000CFD8
		public unsafe static void QueueDeltaStateEvent<TDelta>(InputControl control, TDelta delta, double time = -1.0) where TDelta : struct
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (control.stateBlock.bitOffset != 0U)
			{
				throw new InvalidOperationException(string.Format("Cannot send delta state events against bitfield controls: {0}", control));
			}
			InputDevice device = control.device;
			if (device.m_DeviceIndex == -1)
			{
				throw new InvalidOperationException(string.Format("Cannot queue state event for control '{0}' on device '{1}' because device has not been added to system", control, device));
			}
			if (time < 0.0)
			{
				time = InputRuntime.s_Instance.currentTime;
			}
			else
			{
				time += InputRuntime.s_CurrentTimeOffsetToRealtimeSinceStartup;
			}
			uint num = (uint)UnsafeUtility.SizeOf<TDelta>();
			if (num > 512U)
			{
				throw new ArgumentException(string.Format("Size of state delta '{0}' exceeds maximum supported state size of {1}", typeof(TDelta).Name, 512), "delta");
			}
			if (num != control.stateBlock.alignedSizeInBytes)
			{
				throw new ArgumentException(string.Format("Size {0} of delta state of type {1} provided for control '{2}' does not match size {3} of control", new object[]
				{
					num,
					typeof(TDelta).Name,
					control,
					control.stateBlock.alignedSizeInBytes
				}), "delta");
			}
			long num2 = (long)UnsafeUtility.SizeOf<DeltaStateEvent>() + (long)((ulong)num) - 1L;
			InputSystem.DeltaStateEventBuffer deltaStateEventBuffer;
			deltaStateEventBuffer.stateEvent = new DeltaStateEvent
			{
				baseEvent = new InputEvent(1145852993, (int)num2, device.deviceId, time),
				stateFormat = device.stateBlock.format,
				stateOffset = control.m_StateBlock.byteOffset - device.m_StateBlock.byteOffset
			};
			UnsafeUtility.MemCpy((void*)(&deltaStateEventBuffer.stateEvent.stateData.FixedElementField), UnsafeUtility.AddressOf<TDelta>(ref delta), (long)((ulong)num));
			InputSystem.s_Manager.QueueEvent<DeltaStateEvent>(ref deltaStateEventBuffer.stateEvent);
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0000EF9C File Offset: 0x0000D19C
		public static void QueueConfigChangeEvent(InputDevice device, double time = -1.0)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			if (device.deviceId == 0)
			{
				throw new InvalidOperationException("Device has not been added");
			}
			if (time < 0.0)
			{
				time = InputRuntime.s_Instance.currentTime;
			}
			else
			{
				time += InputRuntime.s_CurrentTimeOffsetToRealtimeSinceStartup;
			}
			DeviceConfigurationEvent deviceConfigurationEvent = DeviceConfigurationEvent.Create(device.deviceId, time);
			InputSystem.s_Manager.QueueEvent<DeviceConfigurationEvent>(ref deviceConfigurationEvent);
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0000F008 File Offset: 0x0000D208
		public static void QueueTextEvent(InputDevice device, char character, double time = -1.0)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			if (device.deviceId == 0)
			{
				throw new InvalidOperationException("Device has not been added");
			}
			if (time < 0.0)
			{
				time = InputRuntime.s_Instance.currentTime;
			}
			else
			{
				time += InputRuntime.s_CurrentTimeOffsetToRealtimeSinceStartup;
			}
			TextEvent textEvent = TextEvent.Create(device.deviceId, character, time);
			InputSystem.s_Manager.QueueEvent<TextEvent>(ref textEvent);
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0000F073 File Offset: 0x0000D273
		public static void Update()
		{
			InputSystem.s_Manager.Update();
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000F080 File Offset: 0x0000D280
		internal static void Update(InputUpdateType updateType)
		{
			if (updateType != InputUpdateType.None && (InputSystem.s_Manager.updateMask & updateType) == InputUpdateType.None)
			{
				throw new InvalidOperationException(string.Format("'{0}' updates are not enabled; InputSystem.settings.updateMode is set to '{1}'", updateType, InputSystem.settings.updateMode));
			}
			InputSystem.s_Manager.Update(updateType);
		}

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000363 RID: 867 RVA: 0x0000F0D0 File Offset: 0x0000D2D0
		// (remove) Token: 0x06000364 RID: 868 RVA: 0x0000F114 File Offset: 0x0000D314
		public static event Action onBeforeUpdate
		{
			add
			{
				InputManager inputManager = InputSystem.s_Manager;
				lock (inputManager)
				{
					InputSystem.s_Manager.onBeforeUpdate += value;
				}
			}
			remove
			{
				InputManager inputManager = InputSystem.s_Manager;
				lock (inputManager)
				{
					InputSystem.s_Manager.onBeforeUpdate -= value;
				}
			}
		}

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06000365 RID: 869 RVA: 0x0000F158 File Offset: 0x0000D358
		// (remove) Token: 0x06000366 RID: 870 RVA: 0x0000F19C File Offset: 0x0000D39C
		public static event Action onAfterUpdate
		{
			add
			{
				InputManager inputManager = InputSystem.s_Manager;
				lock (inputManager)
				{
					InputSystem.s_Manager.onAfterUpdate += value;
				}
			}
			remove
			{
				InputManager inputManager = InputSystem.s_Manager;
				lock (inputManager)
				{
					InputSystem.s_Manager.onAfterUpdate -= value;
				}
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000367 RID: 871 RVA: 0x0000F1E0 File Offset: 0x0000D3E0
		// (set) Token: 0x06000368 RID: 872 RVA: 0x0000F1EC File Offset: 0x0000D3EC
		public static InputSettings settings
		{
			get
			{
				return InputSystem.s_Manager.settings;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (InputSystem.s_Manager.m_Settings == value)
				{
					return;
				}
				InputSystem.s_Manager.settings = value;
			}
		}

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x06000369 RID: 873 RVA: 0x0000F220 File Offset: 0x0000D420
		// (remove) Token: 0x0600036A RID: 874 RVA: 0x0000F22D File Offset: 0x0000D42D
		public static event Action onSettingsChange
		{
			add
			{
				InputSystem.s_Manager.onSettingsChange += value;
			}
			remove
			{
				InputSystem.s_Manager.onSettingsChange -= value;
			}
		}

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x0600036B RID: 875 RVA: 0x0000F23A File Offset: 0x0000D43A
		// (remove) Token: 0x0600036C RID: 876 RVA: 0x0000F25A File Offset: 0x0000D45A
		public static event Action<object, InputActionChange> onActionChange
		{
			add
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				InputActionState.s_GlobalState.onActionChange.AddCallback(value);
			}
			remove
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				InputActionState.s_GlobalState.onActionChange.RemoveCallback(value);
			}
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0000F27C File Offset: 0x0000D47C
		public static void RegisterInteraction(Type type, string name = null)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (string.IsNullOrEmpty(name))
			{
				name = type.Name;
				if (name.EndsWith("Interaction"))
				{
					name = name.Substring(0, name.Length - "Interaction".Length);
				}
			}
			InputSystem.s_Manager.interactions.AddTypeRegistration(name, type);
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0000F2E8 File Offset: 0x0000D4E8
		public static void RegisterInteraction<T>(string name = null)
		{
			InputSystem.RegisterInteraction(typeof(T), name);
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0000F2FC File Offset: 0x0000D4FC
		public static Type TryGetInteraction(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			return InputSystem.s_Manager.interactions.LookupTypeRegistration(name);
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0000F330 File Offset: 0x0000D530
		public static IEnumerable<string> ListInteractions()
		{
			return InputSystem.s_Manager.interactions.names;
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0000F350 File Offset: 0x0000D550
		public static void RegisterBindingComposite(Type type, string name)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (string.IsNullOrEmpty(name))
			{
				name = type.Name;
				if (name.EndsWith("Composite"))
				{
					name = name.Substring(0, name.Length - "Composite".Length);
				}
			}
			InputSystem.s_Manager.composites.AddTypeRegistration(name, type);
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0000F3BC File Offset: 0x0000D5BC
		public static void RegisterBindingComposite<T>(string name = null)
		{
			InputSystem.RegisterBindingComposite(typeof(T), name);
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0000F3D0 File Offset: 0x0000D5D0
		public static Type TryGetBindingComposite(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			return InputSystem.s_Manager.composites.LookupTypeRegistration(name);
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0000F403 File Offset: 0x0000D603
		public static void DisableAllEnabledActions()
		{
			InputActionState.DisableAllActions();
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0000F40A File Offset: 0x0000D60A
		public static List<InputAction> ListEnabledActions()
		{
			List<InputAction> list = new List<InputAction>();
			InputSystem.ListEnabledActions(list);
			return list;
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0000F418 File Offset: 0x0000D618
		public static int ListEnabledActions(List<InputAction> actions)
		{
			if (actions == null)
			{
				throw new ArgumentNullException("actions");
			}
			return InputActionState.FindAllEnabledActions(actions);
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000377 RID: 887 RVA: 0x0000F42E File Offset: 0x0000D62E
		public static InputRemoting remoting
		{
			get
			{
				return InputSystem.s_Remote;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000378 RID: 888 RVA: 0x0000F435 File Offset: 0x0000D635
		public static Version version
		{
			get
			{
				return new Version("1.6.1");
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000379 RID: 889 RVA: 0x0000F441 File Offset: 0x0000D641
		// (set) Token: 0x0600037A RID: 890 RVA: 0x0000F452 File Offset: 0x0000D652
		public static bool runInBackground
		{
			get
			{
				return InputSystem.s_Manager.m_Runtime.runInBackground;
			}
			set
			{
				InputSystem.s_Manager.m_Runtime.runInBackground = value;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600037B RID: 891 RVA: 0x0000F464 File Offset: 0x0000D664
		public static InputMetrics metrics
		{
			get
			{
				return InputSystem.s_Manager.metrics;
			}
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0000F470 File Offset: 0x0000D670
		static InputSystem()
		{
			InputSystem.InitializeInPlayer(null, null);
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0000F479 File Offset: 0x0000D679
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void RunInitializeInPlayer()
		{
			if (InputSystem.s_Manager == null)
			{
				InputSystem.InitializeInPlayer(null, null);
			}
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0000F489 File Offset: 0x0000D689
		internal static void EnsureInitialized()
		{
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0000F48C File Offset: 0x0000D68C
		private static void InitializeInPlayer(IInputRuntime runtime = null, InputSettings settings = null)
		{
			if (settings == null)
			{
				settings = Resources.FindObjectsOfTypeAll<InputSettings>().FirstOrDefault<InputSettings>() ?? ScriptableObject.CreateInstance<InputSettings>();
			}
			InputSystem.s_Manager = new InputManager();
			InputSystem.s_Manager.Initialize(runtime ?? NativeInputRuntime.instance, settings);
			InputSystem.PerformDefaultPluginInitialization();
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0000F4DB File Offset: 0x0000D6DB
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void RunInitialUpdate()
		{
			InputSystem.Update(InputUpdateType.None);
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0000F4E3 File Offset: 0x0000D6E3
		private static void PerformDefaultPluginInitialization()
		{
			UISupport.Initialize();
			XInputSupport.Initialize();
			DualShockSupport.Initialize();
			HIDSupport.Initialize();
			SwitchSupportHID.Initialize();
			XRSupport.Initialize();
		}

		// Token: 0x040000F9 RID: 249
		internal const string kAssemblyVersion = "1.6.1";

		// Token: 0x040000FA RID: 250
		internal const string kDocUrl = "https://docs.unity3d.com/Packages/com.unity.inputsystem@1.6";

		// Token: 0x040000FB RID: 251
		internal static InputManager s_Manager;

		// Token: 0x040000FC RID: 252
		internal static InputRemoting s_Remote;

		// Token: 0x0200017F RID: 383
		private struct StateEventBuffer
		{
			// Token: 0x0400082D RID: 2093
			public StateEvent stateEvent;

			// Token: 0x0400082E RID: 2094
			public const int kMaxSize = 512;

			// Token: 0x0400082F RID: 2095
			[FixedBuffer(typeof(byte), 511)]
			public InputSystem.StateEventBuffer.<data>e__FixedBuffer data;

			// Token: 0x02000262 RID: 610
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, Size = 511)]
			public struct <data>e__FixedBuffer
			{
				// Token: 0x04000C75 RID: 3189
				public byte FixedElementField;
			}
		}

		// Token: 0x02000180 RID: 384
		private struct DeltaStateEventBuffer
		{
			// Token: 0x04000830 RID: 2096
			public DeltaStateEvent stateEvent;

			// Token: 0x04000831 RID: 2097
			public const int kMaxSize = 512;

			// Token: 0x04000832 RID: 2098
			[FixedBuffer(typeof(byte), 511)]
			public InputSystem.DeltaStateEventBuffer.<data>e__FixedBuffer data;

			// Token: 0x02000263 RID: 611
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, Size = 511)]
			public struct <data>e__FixedBuffer
			{
				// Token: 0x04000C76 RID: 3190
				public byte FixedElementField;
			}
		}
	}
}
