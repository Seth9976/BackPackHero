using System;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Bindings;

namespace Unity.IO.LowLevel.Unsafe
{
	// Token: 0x0200007C RID: 124
	public struct ReadHandle : IDisposable
	{
		// Token: 0x060001C3 RID: 451 RVA: 0x00003AD8 File Offset: 0x00001CD8
		public bool IsValid()
		{
			return ReadHandle.IsReadHandleValid(this);
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00003AF8 File Offset: 0x00001CF8
		public void Dispose()
		{
			bool flag = !ReadHandle.IsReadHandleValid(this);
			if (flag)
			{
				throw new InvalidOperationException("ReadHandle.Dispose cannot be called twice on the same ReadHandle");
			}
			bool flag2 = this.Status == ReadStatus.InProgress;
			if (flag2)
			{
				throw new InvalidOperationException("ReadHandle.Dispose cannot be called until the read operation completes");
			}
			ReadHandle.ReleaseReadHandle(this);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00003B48 File Offset: 0x00001D48
		public void Cancel()
		{
			bool flag = !ReadHandle.IsReadHandleValid(this);
			if (flag)
			{
				throw new InvalidOperationException("ReadHandle.Cancel cannot be called on a disposed ReadHandle");
			}
			ReadHandle.CancelInternal(this);
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00003B7F File Offset: 0x00001D7F
		[FreeFunction("AsyncReadManagerManaged::CancelReadRequest")]
		private static void CancelInternal(ReadHandle handle)
		{
			ReadHandle.CancelInternal_Injected(ref handle);
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x00003B88 File Offset: 0x00001D88
		public JobHandle JobHandle
		{
			get
			{
				bool flag = !ReadHandle.IsReadHandleValid(this);
				if (flag)
				{
					throw new InvalidOperationException("ReadHandle.JobHandle cannot be called after the ReadHandle has been disposed");
				}
				return ReadHandle.GetJobHandle(this);
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x00003BC4 File Offset: 0x00001DC4
		public ReadStatus Status
		{
			get
			{
				bool flag = !ReadHandle.IsReadHandleValid(this);
				if (flag)
				{
					throw new InvalidOperationException("Cannot use a ReadHandle that has been disposed");
				}
				return ReadHandle.GetReadStatus(this);
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x00003C00 File Offset: 0x00001E00
		public long ReadCount
		{
			get
			{
				bool flag = !ReadHandle.IsReadHandleValid(this);
				if (flag)
				{
					throw new InvalidOperationException("Cannot use a ReadHandle that has been disposed");
				}
				return ReadHandle.GetReadCount(this);
			}
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00003C3C File Offset: 0x00001E3C
		public long GetBytesRead()
		{
			bool flag = !ReadHandle.IsReadHandleValid(this);
			if (flag)
			{
				throw new InvalidOperationException("ReadHandle.GetBytesRead cannot be called after the ReadHandle has been disposed");
			}
			return ReadHandle.GetBytesRead(this);
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00003C78 File Offset: 0x00001E78
		public long GetBytesRead(uint readCommandIndex)
		{
			bool flag = !ReadHandle.IsReadHandleValid(this);
			if (flag)
			{
				throw new InvalidOperationException("ReadHandle.GetBytesRead cannot be called after the ReadHandle has been disposed");
			}
			return ReadHandle.GetBytesReadForCommand(this, readCommandIndex);
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00003CB4 File Offset: 0x00001EB4
		public unsafe ulong* GetBytesReadArray()
		{
			bool flag = !ReadHandle.IsReadHandleValid(this);
			if (flag)
			{
				throw new InvalidOperationException("ReadHandle.GetBytesReadArray cannot be called after the ReadHandle has been disposed");
			}
			return ReadHandle.GetBytesReadArray(this);
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00003CEE File Offset: 0x00001EEE
		[ThreadAndSerializationSafe]
		[FreeFunction("AsyncReadManagerManaged::GetReadStatus", IsThreadSafe = true)]
		private static ReadStatus GetReadStatus(ReadHandle handle)
		{
			return ReadHandle.GetReadStatus_Injected(ref handle);
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00003CF7 File Offset: 0x00001EF7
		[ThreadAndSerializationSafe]
		[FreeFunction("AsyncReadManagerManaged::GetReadCount", IsThreadSafe = true)]
		private static long GetReadCount(ReadHandle handle)
		{
			return ReadHandle.GetReadCount_Injected(ref handle);
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00003D00 File Offset: 0x00001F00
		[ThreadAndSerializationSafe]
		[FreeFunction("AsyncReadManagerManaged::GetBytesRead", IsThreadSafe = true)]
		private static long GetBytesRead(ReadHandle handle)
		{
			return ReadHandle.GetBytesRead_Injected(ref handle);
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00003D09 File Offset: 0x00001F09
		[ThreadAndSerializationSafe]
		[FreeFunction("AsyncReadManagerManaged::GetBytesReadForCommand", IsThreadSafe = true)]
		private static long GetBytesReadForCommand(ReadHandle handle, uint readCommandIndex)
		{
			return ReadHandle.GetBytesReadForCommand_Injected(ref handle, readCommandIndex);
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00003D13 File Offset: 0x00001F13
		[FreeFunction("AsyncReadManagerManaged::GetBytesReadArray", IsThreadSafe = true)]
		[ThreadAndSerializationSafe]
		private unsafe static ulong* GetBytesReadArray(ReadHandle handle)
		{
			return ReadHandle.GetBytesReadArray_Injected(ref handle);
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00003D1C File Offset: 0x00001F1C
		[FreeFunction("AsyncReadManagerManaged::ReleaseReadHandle", IsThreadSafe = true)]
		[ThreadAndSerializationSafe]
		private static void ReleaseReadHandle(ReadHandle handle)
		{
			ReadHandle.ReleaseReadHandle_Injected(ref handle);
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00003D25 File Offset: 0x00001F25
		[FreeFunction("AsyncReadManagerManaged::IsReadHandleValid", IsThreadSafe = true)]
		[ThreadAndSerializationSafe]
		private static bool IsReadHandleValid(ReadHandle handle)
		{
			return ReadHandle.IsReadHandleValid_Injected(ref handle);
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00003D30 File Offset: 0x00001F30
		[ThreadAndSerializationSafe]
		[FreeFunction("AsyncReadManagerManaged::GetJobHandle", IsThreadSafe = true)]
		private static JobHandle GetJobHandle(ReadHandle handle)
		{
			JobHandle jobHandle;
			ReadHandle.GetJobHandle_Injected(ref handle, out jobHandle);
			return jobHandle;
		}

		// Token: 0x060001D5 RID: 469
		[MethodImpl(4096)]
		private static extern void CancelInternal_Injected(ref ReadHandle handle);

		// Token: 0x060001D6 RID: 470
		[MethodImpl(4096)]
		private static extern ReadStatus GetReadStatus_Injected(ref ReadHandle handle);

		// Token: 0x060001D7 RID: 471
		[MethodImpl(4096)]
		private static extern long GetReadCount_Injected(ref ReadHandle handle);

		// Token: 0x060001D8 RID: 472
		[MethodImpl(4096)]
		private static extern long GetBytesRead_Injected(ref ReadHandle handle);

		// Token: 0x060001D9 RID: 473
		[MethodImpl(4096)]
		private static extern long GetBytesReadForCommand_Injected(ref ReadHandle handle, uint readCommandIndex);

		// Token: 0x060001DA RID: 474
		[MethodImpl(4096)]
		private unsafe static extern ulong* GetBytesReadArray_Injected(ref ReadHandle handle);

		// Token: 0x060001DB RID: 475
		[MethodImpl(4096)]
		private static extern void ReleaseReadHandle_Injected(ref ReadHandle handle);

		// Token: 0x060001DC RID: 476
		[MethodImpl(4096)]
		private static extern bool IsReadHandleValid_Injected(ref ReadHandle handle);

		// Token: 0x060001DD RID: 477
		[MethodImpl(4096)]
		private static extern void GetJobHandle_Injected(ref ReadHandle handle, out JobHandle ret);

		// Token: 0x040001D0 RID: 464
		[NativeDisableUnsafePtrRestriction]
		internal IntPtr ptr;

		// Token: 0x040001D1 RID: 465
		internal int version;
	}
}
