using System;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000B2 RID: 178
	[StructLayout(LayoutKind.Explicit, Size = 8)]
	public struct InputDeviceCommand : IInputDeviceCommandInfo
	{
		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000C91 RID: 3217 RVA: 0x00041734 File Offset: 0x0003F934
		public int payloadSizeInBytes
		{
			get
			{
				return this.sizeInBytes - 8;
			}
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000C92 RID: 3218 RVA: 0x00041740 File Offset: 0x0003F940
		public unsafe void* payloadPtr
		{
			get
			{
				fixed (InputDeviceCommand* ptr = &this)
				{
					void* ptr2 = (void*)ptr;
					return (void*)((byte*)ptr2 + 8);
				}
			}
		}

		// Token: 0x06000C93 RID: 3219 RVA: 0x00041755 File Offset: 0x0003F955
		public InputDeviceCommand(FourCC type, int sizeInBytes = 8)
		{
			this.type = type;
			this.sizeInBytes = sizeInBytes;
		}

		// Token: 0x06000C94 RID: 3220 RVA: 0x00041768 File Offset: 0x0003F968
		public unsafe static NativeArray<byte> AllocateNative(FourCC type, int payloadSize)
		{
			int num = payloadSize + 8;
			NativeArray<byte> nativeArray = new NativeArray<byte>(num, Allocator.Temp, NativeArrayOptions.ClearMemory);
			InputDeviceCommand* unsafePtr = (InputDeviceCommand*)nativeArray.GetUnsafePtr<byte>();
			unsafePtr->type = type;
			unsafePtr->sizeInBytes = num;
			return nativeArray;
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000C95 RID: 3221 RVA: 0x00041798 File Offset: 0x0003F998
		public FourCC typeStatic
		{
			get
			{
				return default(FourCC);
			}
		}

		// Token: 0x040004B1 RID: 1201
		internal const int kBaseCommandSize = 8;

		// Token: 0x040004B2 RID: 1202
		public const int BaseCommandSize = 8;

		// Token: 0x040004B3 RID: 1203
		public const long GenericFailure = -1L;

		// Token: 0x040004B4 RID: 1204
		public const long GenericSuccess = 1L;

		// Token: 0x040004B5 RID: 1205
		[FieldOffset(0)]
		public FourCC type;

		// Token: 0x040004B6 RID: 1206
		[FieldOffset(4)]
		public int sizeInBytes;
	}
}
