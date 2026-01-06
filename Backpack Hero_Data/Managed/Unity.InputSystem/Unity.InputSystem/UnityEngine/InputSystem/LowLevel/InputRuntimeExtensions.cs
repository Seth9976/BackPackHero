using System;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000EA RID: 234
	internal static class InputRuntimeExtensions
	{
		// Token: 0x06000DE1 RID: 3553 RVA: 0x000449BC File Offset: 0x00042BBC
		public unsafe static long DeviceCommand<TCommand>(this IInputRuntime runtime, int deviceId, ref TCommand command) where TCommand : struct, IInputDeviceCommandInfo
		{
			if (runtime == null)
			{
				throw new ArgumentNullException("runtime");
			}
			return runtime.DeviceCommand(deviceId, (InputDeviceCommand*)UnsafeUtility.AddressOf<TCommand>(ref command));
		}
	}
}
