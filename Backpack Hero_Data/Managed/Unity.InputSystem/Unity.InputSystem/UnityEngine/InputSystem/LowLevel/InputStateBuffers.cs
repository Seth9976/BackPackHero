using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000F4 RID: 244
	internal struct InputStateBuffers
	{
		// Token: 0x06000E4F RID: 3663 RVA: 0x00046380 File Offset: 0x00044580
		public InputStateBuffers.DoubleBuffers GetDoubleBuffersFor(InputUpdateType updateType)
		{
			if (updateType - InputUpdateType.Dynamic <= 1 || updateType == InputUpdateType.BeforeRender || updateType == InputUpdateType.Manual)
			{
				return this.m_PlayerStateBuffers;
			}
			throw new ArgumentException("Unrecognized InputUpdateType: " + updateType.ToString(), "updateType");
		}

		// Token: 0x06000E50 RID: 3664 RVA: 0x000463B9 File Offset: 0x000445B9
		public unsafe static void* GetFrontBufferForDevice(int deviceIndex)
		{
			return InputStateBuffers.s_CurrentBuffers.GetFrontBuffer(deviceIndex);
		}

		// Token: 0x06000E51 RID: 3665 RVA: 0x000463C6 File Offset: 0x000445C6
		public unsafe static void* GetBackBufferForDevice(int deviceIndex)
		{
			return InputStateBuffers.s_CurrentBuffers.GetBackBuffer(deviceIndex);
		}

		// Token: 0x06000E52 RID: 3666 RVA: 0x000463D3 File Offset: 0x000445D3
		public static void SwitchTo(InputStateBuffers buffers, InputUpdateType update)
		{
			InputStateBuffers.s_CurrentBuffers = buffers.GetDoubleBuffersFor(update);
		}

		// Token: 0x06000E53 RID: 3667 RVA: 0x000463E4 File Offset: 0x000445E4
		public unsafe void AllocateAll(InputDevice[] devices, int deviceCount)
		{
			this.sizePerBuffer = InputStateBuffers.ComputeSizeOfSingleStateBuffer(devices, deviceCount);
			if (this.sizePerBuffer == 0U)
			{
				return;
			}
			this.sizePerBuffer = this.sizePerBuffer.AlignToMultipleOf(4U);
			uint num = (uint)(deviceCount * sizeof(void*) * 2);
			this.totalSize = 0U;
			this.totalSize += this.sizePerBuffer * 2U;
			this.totalSize += num;
			this.totalSize += this.sizePerBuffer * 3U;
			this.m_AllBuffers = UnsafeUtility.Malloc((long)((ulong)this.totalSize), 4, Allocator.Persistent);
			UnsafeUtility.MemClear(this.m_AllBuffers, (long)((ulong)this.totalSize));
			byte* allBuffers = (byte*)this.m_AllBuffers;
			this.m_PlayerStateBuffers = InputStateBuffers.SetUpDeviceToBufferMappings(deviceCount, ref allBuffers, this.sizePerBuffer, num);
			this.defaultStateBuffer = (void*)allBuffers;
			this.noiseMaskBuffer = (void*)(allBuffers + this.sizePerBuffer);
			this.resetMaskBuffer = (void*)(allBuffers + this.sizePerBuffer * 2U);
		}

		// Token: 0x06000E54 RID: 3668 RVA: 0x000464CC File Offset: 0x000446CC
		private unsafe static InputStateBuffers.DoubleBuffers SetUpDeviceToBufferMappings(int deviceCount, ref byte* bufferPtr, uint sizePerBuffer, uint mappingTableSizePerBuffer)
		{
			byte* ptr = bufferPtr;
			byte* ptr2 = bufferPtr + sizePerBuffer;
			void** ptr3 = bufferPtr / (IntPtr)sizeof(void*) + sizePerBuffer * 2U;
			bufferPtr += (IntPtr)((UIntPtr)(sizePerBuffer * 2U + mappingTableSizePerBuffer));
			InputStateBuffers.DoubleBuffers doubleBuffers = new InputStateBuffers.DoubleBuffers
			{
				deviceToBufferMapping = ptr3
			};
			for (int i = 0; i < deviceCount; i++)
			{
				int num = i;
				doubleBuffers.SetFrontBuffer(num, (void*)ptr);
				doubleBuffers.SetBackBuffer(num, (void*)ptr2);
			}
			return doubleBuffers;
		}

		// Token: 0x06000E55 RID: 3669 RVA: 0x00046534 File Offset: 0x00044734
		public void FreeAll()
		{
			if (this.m_AllBuffers != null)
			{
				UnsafeUtility.Free(this.m_AllBuffers, Allocator.Persistent);
				this.m_AllBuffers = null;
			}
			this.m_PlayerStateBuffers = default(InputStateBuffers.DoubleBuffers);
			InputStateBuffers.s_CurrentBuffers = default(InputStateBuffers.DoubleBuffers);
			if (InputStateBuffers.s_DefaultStateBuffer == this.defaultStateBuffer)
			{
				InputStateBuffers.s_DefaultStateBuffer = null;
			}
			this.defaultStateBuffer = null;
			if (InputStateBuffers.s_NoiseMaskBuffer == this.noiseMaskBuffer)
			{
				InputStateBuffers.s_NoiseMaskBuffer = null;
			}
			if (InputStateBuffers.s_ResetMaskBuffer == this.resetMaskBuffer)
			{
				InputStateBuffers.s_ResetMaskBuffer = null;
			}
			this.noiseMaskBuffer = null;
			this.resetMaskBuffer = null;
			this.totalSize = 0U;
			this.sizePerBuffer = 0U;
		}

		// Token: 0x06000E56 RID: 3670 RVA: 0x000465D8 File Offset: 0x000447D8
		public void MigrateAll(InputDevice[] devices, int deviceCount, InputStateBuffers oldBuffers)
		{
			if (oldBuffers.totalSize > 0U)
			{
				InputStateBuffers.MigrateDoubleBuffer(this.m_PlayerStateBuffers, devices, deviceCount, oldBuffers.m_PlayerStateBuffers);
				InputStateBuffers.MigrateSingleBuffer(this.defaultStateBuffer, devices, deviceCount, oldBuffers.defaultStateBuffer);
				InputStateBuffers.MigrateSingleBuffer(this.noiseMaskBuffer, devices, deviceCount, oldBuffers.noiseMaskBuffer);
				InputStateBuffers.MigrateSingleBuffer(this.resetMaskBuffer, devices, deviceCount, oldBuffers.resetMaskBuffer);
			}
			uint num = 0U;
			for (int i = 0; i < deviceCount; i++)
			{
				InputDevice inputDevice = devices[i];
				uint byteOffset = inputDevice.m_StateBlock.byteOffset;
				if (byteOffset == 4294967295U)
				{
					inputDevice.m_StateBlock.byteOffset = 0U;
					if (num != 0U)
					{
						inputDevice.BakeOffsetIntoStateBlockRecursive(num);
					}
				}
				else
				{
					uint num2 = num - byteOffset;
					if (num2 != 0U)
					{
						inputDevice.BakeOffsetIntoStateBlockRecursive(num2);
					}
				}
				num = InputStateBuffers.NextDeviceOffset(num, inputDevice);
			}
		}

		// Token: 0x06000E57 RID: 3671 RVA: 0x00046690 File Offset: 0x00044890
		private unsafe static void MigrateDoubleBuffer(InputStateBuffers.DoubleBuffers newBuffer, InputDevice[] devices, int deviceCount, InputStateBuffers.DoubleBuffers oldBuffer)
		{
			if (!newBuffer.valid)
			{
				return;
			}
			if (!oldBuffer.valid)
			{
				return;
			}
			uint num = 0U;
			for (int i = 0; i < deviceCount; i++)
			{
				InputDevice inputDevice = devices[i];
				if (inputDevice.m_StateBlock.byteOffset == 4294967295U)
				{
					break;
				}
				int deviceIndex = inputDevice.m_DeviceIndex;
				int num2 = i;
				uint alignedSizeInBytes = inputDevice.m_StateBlock.alignedSizeInBytes;
				byte* ptr = (byte*)oldBuffer.GetFrontBuffer(deviceIndex) + inputDevice.m_StateBlock.byteOffset;
				byte* ptr2 = (byte*)oldBuffer.GetBackBuffer(deviceIndex) + inputDevice.m_StateBlock.byteOffset;
				byte* ptr3 = (byte*)newBuffer.GetFrontBuffer(num2) + num;
				void* ptr4 = (void*)((byte*)newBuffer.GetBackBuffer(num2) + num);
				UnsafeUtility.MemCpy((void*)ptr3, (void*)ptr, (long)((ulong)alignedSizeInBytes));
				UnsafeUtility.MemCpy(ptr4, (void*)ptr2, (long)((ulong)alignedSizeInBytes));
				num = InputStateBuffers.NextDeviceOffset(num, inputDevice);
			}
		}

		// Token: 0x06000E58 RID: 3672 RVA: 0x00046754 File Offset: 0x00044954
		private unsafe static void MigrateSingleBuffer(void* newBuffer, InputDevice[] devices, int deviceCount, void* oldBuffer)
		{
			uint num = 0U;
			for (int i = 0; i < deviceCount; i++)
			{
				InputDevice inputDevice = devices[i];
				if (inputDevice.m_StateBlock.byteOffset == 4294967295U)
				{
					break;
				}
				uint alignedSizeInBytes = inputDevice.m_StateBlock.alignedSizeInBytes;
				byte* ptr = (byte*)oldBuffer + inputDevice.m_StateBlock.byteOffset;
				UnsafeUtility.MemCpy((void*)((byte*)newBuffer + num), (void*)ptr, (long)((ulong)alignedSizeInBytes));
				num = InputStateBuffers.NextDeviceOffset(num, inputDevice);
			}
		}

		// Token: 0x06000E59 RID: 3673 RVA: 0x000467B4 File Offset: 0x000449B4
		private static uint ComputeSizeOfSingleStateBuffer(InputDevice[] devices, int deviceCount)
		{
			uint num = 0U;
			for (int i = 0; i < deviceCount; i++)
			{
				num = InputStateBuffers.NextDeviceOffset(num, devices[i]);
			}
			return num;
		}

		// Token: 0x06000E5A RID: 3674 RVA: 0x000467DC File Offset: 0x000449DC
		private static uint NextDeviceOffset(uint currentOffset, InputDevice device)
		{
			uint alignedSizeInBytes = device.m_StateBlock.alignedSizeInBytes;
			if (alignedSizeInBytes == 0U)
			{
				throw new ArgumentException(string.Format("Device '{0}' has a zero-size state buffer", device), "device");
			}
			return currentOffset + alignedSizeInBytes.AlignToMultipleOf(4U);
		}

		// Token: 0x040005E1 RID: 1505
		public uint sizePerBuffer;

		// Token: 0x040005E2 RID: 1506
		public uint totalSize;

		// Token: 0x040005E3 RID: 1507
		public unsafe void* defaultStateBuffer;

		// Token: 0x040005E4 RID: 1508
		public unsafe void* noiseMaskBuffer;

		// Token: 0x040005E5 RID: 1509
		public unsafe void* resetMaskBuffer;

		// Token: 0x040005E6 RID: 1510
		private unsafe void* m_AllBuffers;

		// Token: 0x040005E7 RID: 1511
		internal InputStateBuffers.DoubleBuffers m_PlayerStateBuffers;

		// Token: 0x040005E8 RID: 1512
		internal unsafe static void* s_DefaultStateBuffer;

		// Token: 0x040005E9 RID: 1513
		internal unsafe static void* s_NoiseMaskBuffer;

		// Token: 0x040005EA RID: 1514
		internal unsafe static void* s_ResetMaskBuffer;

		// Token: 0x040005EB RID: 1515
		internal static InputStateBuffers.DoubleBuffers s_CurrentBuffers;

		// Token: 0x02000217 RID: 535
		[Serializable]
		internal struct DoubleBuffers
		{
			// Token: 0x17000590 RID: 1424
			// (get) Token: 0x060014B4 RID: 5300 RVA: 0x0005FF45 File Offset: 0x0005E145
			public bool valid
			{
				get
				{
					return this.deviceToBufferMapping != null;
				}
			}

			// Token: 0x060014B5 RID: 5301 RVA: 0x0005FF54 File Offset: 0x0005E154
			public unsafe void SetFrontBuffer(int deviceIndex, void* ptr)
			{
				*(IntPtr*)(this.deviceToBufferMapping + (IntPtr)(deviceIndex * 2) * (IntPtr)sizeof(void*) / (IntPtr)sizeof(void*)) = ptr;
			}

			// Token: 0x060014B6 RID: 5302 RVA: 0x0005FF6A File Offset: 0x0005E16A
			public unsafe void SetBackBuffer(int deviceIndex, void* ptr)
			{
				*(IntPtr*)(this.deviceToBufferMapping + (IntPtr)(deviceIndex * 2 + 1) * (IntPtr)sizeof(void*) / (IntPtr)sizeof(void*)) = ptr;
			}

			// Token: 0x060014B7 RID: 5303 RVA: 0x0005FF82 File Offset: 0x0005E182
			public unsafe void* GetFrontBuffer(int deviceIndex)
			{
				return *(IntPtr*)(this.deviceToBufferMapping + (IntPtr)(deviceIndex * 2) * (IntPtr)sizeof(void*) / (IntPtr)sizeof(void*));
			}

			// Token: 0x060014B8 RID: 5304 RVA: 0x0005FF97 File Offset: 0x0005E197
			public unsafe void* GetBackBuffer(int deviceIndex)
			{
				return *(IntPtr*)(this.deviceToBufferMapping + (IntPtr)(deviceIndex * 2 + 1) * (IntPtr)sizeof(void*) / (IntPtr)sizeof(void*));
			}

			// Token: 0x060014B9 RID: 5305 RVA: 0x0005FFB0 File Offset: 0x0005E1B0
			public unsafe void SwapBuffers(int deviceIndex)
			{
				if (!this.valid)
				{
					return;
				}
				void* frontBuffer = this.GetFrontBuffer(deviceIndex);
				void* backBuffer = this.GetBackBuffer(deviceIndex);
				this.SetFrontBuffer(deviceIndex, backBuffer);
				this.SetBackBuffer(deviceIndex, frontBuffer);
			}

			// Token: 0x04000B4E RID: 2894
			public unsafe void** deviceToBufferMapping;
		}
	}
}
