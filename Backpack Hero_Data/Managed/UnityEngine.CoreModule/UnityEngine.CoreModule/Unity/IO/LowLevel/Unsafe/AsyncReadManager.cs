using System;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Bindings;

namespace Unity.IO.LowLevel.Unsafe
{
	// Token: 0x0200007D RID: 125
	[NativeHeader("Runtime/File/AsyncReadManagerManagedApi.h")]
	public static class AsyncReadManager
	{
		// Token: 0x060001DE RID: 478 RVA: 0x00003D48 File Offset: 0x00001F48
		[FreeFunction("AsyncReadManagerManaged::Read", IsThreadSafe = true)]
		[ThreadAndSerializationSafe]
		private unsafe static ReadHandle ReadInternal(string filename, void* cmds, uint cmdCount, string assetName, ulong typeID, AssetLoadingSubsystem subsystem)
		{
			ReadHandle readHandle;
			AsyncReadManager.ReadInternal_Injected(filename, cmds, cmdCount, assetName, typeID, subsystem, out readHandle);
			return readHandle;
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00003D68 File Offset: 0x00001F68
		public unsafe static ReadHandle Read(string filename, ReadCommand* readCmds, uint readCmdCount, string assetName = "", ulong typeID = 0UL, AssetLoadingSubsystem subsystem = AssetLoadingSubsystem.Scripts)
		{
			return AsyncReadManager.ReadInternal(filename, (void*)readCmds, readCmdCount, assetName, typeID, subsystem);
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00003D88 File Offset: 0x00001F88
		[FreeFunction("AsyncReadManagerManaged::GetFileInfo", IsThreadSafe = true)]
		[ThreadAndSerializationSafe]
		private unsafe static ReadHandle GetFileInfoInternal(string filename, void* cmd)
		{
			ReadHandle readHandle;
			AsyncReadManager.GetFileInfoInternal_Injected(filename, cmd, out readHandle);
			return readHandle;
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00003DA0 File Offset: 0x00001FA0
		public unsafe static ReadHandle GetFileInfo(string filename, FileInfoResult* result)
		{
			bool flag = result == null;
			if (flag)
			{
				throw new NullReferenceException("GetFileInfo must have a valid FileInfoResult to write into.");
			}
			return AsyncReadManager.GetFileInfoInternal(filename, (void*)result);
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00003DD0 File Offset: 0x00001FD0
		[FreeFunction("AsyncReadManagerManaged::ReadWithHandles_NativePtr", IsThreadSafe = true)]
		[ThreadAndSerializationSafe]
		private unsafe static ReadHandle ReadWithHandlesInternal_NativePtr(in FileHandle fileHandle, void* readCmdArray, JobHandle dependency)
		{
			ReadHandle readHandle;
			AsyncReadManager.ReadWithHandlesInternal_NativePtr_Injected(in fileHandle, readCmdArray, ref dependency, out readHandle);
			return readHandle;
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00003DEC File Offset: 0x00001FEC
		[ThreadAndSerializationSafe]
		[FreeFunction("AsyncReadManagerManaged::ReadWithHandles_NativeCopy", IsThreadSafe = true)]
		private unsafe static ReadHandle ReadWithHandlesInternal_NativeCopy(in FileHandle fileHandle, void* readCmdArray)
		{
			ReadHandle readHandle;
			AsyncReadManager.ReadWithHandlesInternal_NativeCopy_Injected(in fileHandle, readCmdArray, out readHandle);
			return readHandle;
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00003E04 File Offset: 0x00002004
		public unsafe static ReadHandle ReadDeferred(in FileHandle fileHandle, ReadCommandArray* readCmdArray, JobHandle dependency)
		{
			bool flag = !fileHandle.IsValid();
			if (flag)
			{
				throw new InvalidOperationException("FileHandle is invalid and may not be read from.");
			}
			return AsyncReadManager.ReadWithHandlesInternal_NativePtr(in fileHandle, (void*)readCmdArray, dependency);
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00003E38 File Offset: 0x00002038
		public static ReadHandle Read(in FileHandle fileHandle, ReadCommandArray readCmdArray)
		{
			bool flag = !fileHandle.IsValid();
			if (flag)
			{
				throw new InvalidOperationException("FileHandle is invalid and may not be read from.");
			}
			return AsyncReadManager.ReadWithHandlesInternal_NativeCopy(in fileHandle, UnsafeUtility.AddressOf<ReadCommandArray>(ref readCmdArray));
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00003E70 File Offset: 0x00002070
		[ThreadAndSerializationSafe]
		[FreeFunction("AsyncReadManagerManaged::ScheduleOpenRequest", IsThreadSafe = true)]
		private static FileHandle OpenFileAsync_Internal(string fileName)
		{
			FileHandle fileHandle;
			AsyncReadManager.OpenFileAsync_Internal_Injected(fileName, out fileHandle);
			return fileHandle;
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00003E88 File Offset: 0x00002088
		public static FileHandle OpenFileAsync(string fileName)
		{
			bool flag = fileName.Length == 0;
			if (flag)
			{
				throw new InvalidOperationException("FileName is empty");
			}
			return AsyncReadManager.OpenFileAsync_Internal(fileName);
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00003EB8 File Offset: 0x000020B8
		[FreeFunction("AsyncReadManagerManaged::ScheduleCloseRequest", IsThreadSafe = true)]
		[ThreadAndSerializationSafe]
		internal static JobHandle CloseFileAsync(in FileHandle fileHandle, JobHandle dependency)
		{
			JobHandle jobHandle;
			AsyncReadManager.CloseFileAsync_Injected(in fileHandle, ref dependency, out jobHandle);
			return jobHandle;
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00003ED0 File Offset: 0x000020D0
		[FreeFunction("AsyncReadManagerManaged::ScheduleCloseCachedFileRequest", IsThreadSafe = true)]
		[ThreadAndSerializationSafe]
		public static JobHandle CloseCachedFileAsync(string fileName, JobHandle dependency = default(JobHandle))
		{
			JobHandle jobHandle;
			AsyncReadManager.CloseCachedFileAsync_Injected(fileName, ref dependency, out jobHandle);
			return jobHandle;
		}

		// Token: 0x060001EA RID: 490
		[MethodImpl(4096)]
		private unsafe static extern void ReadInternal_Injected(string filename, void* cmds, uint cmdCount, string assetName, ulong typeID, AssetLoadingSubsystem subsystem, out ReadHandle ret);

		// Token: 0x060001EB RID: 491
		[MethodImpl(4096)]
		private unsafe static extern void GetFileInfoInternal_Injected(string filename, void* cmd, out ReadHandle ret);

		// Token: 0x060001EC RID: 492
		[MethodImpl(4096)]
		private unsafe static extern void ReadWithHandlesInternal_NativePtr_Injected(in FileHandle fileHandle, void* readCmdArray, ref JobHandle dependency, out ReadHandle ret);

		// Token: 0x060001ED RID: 493
		[MethodImpl(4096)]
		private unsafe static extern void ReadWithHandlesInternal_NativeCopy_Injected(in FileHandle fileHandle, void* readCmdArray, out ReadHandle ret);

		// Token: 0x060001EE RID: 494
		[MethodImpl(4096)]
		private static extern void OpenFileAsync_Internal_Injected(string fileName, out FileHandle ret);

		// Token: 0x060001EF RID: 495
		[MethodImpl(4096)]
		private static extern void CloseFileAsync_Injected(in FileHandle fileHandle, ref JobHandle dependency, out JobHandle ret);

		// Token: 0x060001F0 RID: 496
		[MethodImpl(4096)]
		private static extern void CloseCachedFileAsync_Injected(string fileName, ref JobHandle dependency = null, out JobHandle ret);
	}
}
