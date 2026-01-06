using System;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using UnityEngine.Bindings;

namespace Unity.IO.LowLevel.Unsafe
{
	// Token: 0x0200007B RID: 123
	public readonly struct FileHandle
	{
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060001BB RID: 443 RVA: 0x00003A14 File Offset: 0x00001C14
		public FileStatus Status
		{
			get
			{
				bool flag = !FileHandle.IsFileHandleValid(in this);
				if (flag)
				{
					throw new InvalidOperationException("FileHandle.Status cannot be called on a closed FileHandle");
				}
				return FileHandle.GetFileStatus_Internal(in this);
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060001BC RID: 444 RVA: 0x00003A44 File Offset: 0x00001C44
		public JobHandle JobHandle
		{
			get
			{
				bool flag = !FileHandle.IsFileHandleValid(in this);
				if (flag)
				{
					throw new InvalidOperationException("FileHandle.JobHandle cannot be called on a closed FileHandle");
				}
				return FileHandle.GetJobHandle_Internal(in this);
			}
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00003A74 File Offset: 0x00001C74
		public bool IsValid()
		{
			return FileHandle.IsFileHandleValid(in this);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00003A8C File Offset: 0x00001C8C
		public JobHandle Close(JobHandle dependency = default(JobHandle))
		{
			bool flag = !FileHandle.IsFileHandleValid(in this);
			if (flag)
			{
				throw new InvalidOperationException("FileHandle.Close cannot be called twice on the same FileHandle");
			}
			return AsyncReadManager.CloseFileAsync(in this, dependency);
		}

		// Token: 0x060001BF RID: 447
		[FreeFunction("AsyncReadManagerManaged::IsFileHandleValid")]
		[MethodImpl(4096)]
		private static extern bool IsFileHandleValid(in FileHandle handle);

		// Token: 0x060001C0 RID: 448
		[FreeFunction("AsyncReadManagerManaged::GetFileStatusFromManagedHandle")]
		[MethodImpl(4096)]
		private static extern FileStatus GetFileStatus_Internal(in FileHandle handle);

		// Token: 0x060001C1 RID: 449 RVA: 0x00003AC0 File Offset: 0x00001CC0
		[FreeFunction("AsyncReadManagerManaged::GetJobFenceFromManagedHandle")]
		private static JobHandle GetJobHandle_Internal(in FileHandle handle)
		{
			JobHandle jobHandle;
			FileHandle.GetJobHandle_Internal_Injected(in handle, out jobHandle);
			return jobHandle;
		}

		// Token: 0x060001C2 RID: 450
		[MethodImpl(4096)]
		private static extern void GetJobHandle_Internal_Injected(in FileHandle handle, out JobHandle ret);

		// Token: 0x040001CE RID: 462
		[NativeDisableUnsafePtrRestriction]
		internal readonly IntPtr fileCommandPtr;

		// Token: 0x040001CF RID: 463
		internal readonly int version;
	}
}
