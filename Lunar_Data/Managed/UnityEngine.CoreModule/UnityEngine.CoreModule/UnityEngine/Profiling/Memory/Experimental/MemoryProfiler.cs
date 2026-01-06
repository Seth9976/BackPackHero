using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Profiling.Experimental;
using UnityEngine.Scripting;

namespace UnityEngine.Profiling.Memory.Experimental
{
	// Token: 0x0200027E RID: 638
	[NativeHeader("Modules/Profiler/Runtime/MemorySnapshotManager.h")]
	public sealed class MemoryProfiler
	{
		// Token: 0x14000018 RID: 24
		// (add) Token: 0x06001BC1 RID: 7105 RVA: 0x0002C794 File Offset: 0x0002A994
		// (remove) Token: 0x06001BC2 RID: 7106 RVA: 0x0002C7C8 File Offset: 0x0002A9C8
		[field: DebuggerBrowsable(0)]
		private static event Action<string, bool> m_SnapshotFinished;

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x06001BC3 RID: 7107 RVA: 0x0002C7FC File Offset: 0x0002A9FC
		// (remove) Token: 0x06001BC4 RID: 7108 RVA: 0x0002C830 File Offset: 0x0002AA30
		[field: DebuggerBrowsable(0)]
		private static event Action<string, bool, DebugScreenCapture> m_SaveScreenshotToDisk;

		// Token: 0x1400001A RID: 26
		// (add) Token: 0x06001BC5 RID: 7109 RVA: 0x0002C864 File Offset: 0x0002AA64
		// (remove) Token: 0x06001BC6 RID: 7110 RVA: 0x0002C898 File Offset: 0x0002AA98
		[field: DebuggerBrowsable(0)]
		public static event Action<MetaData> createMetaData;

		// Token: 0x06001BC7 RID: 7111
		[NativeMethod("StartOperation")]
		[StaticAccessor("profiling::memory::GetMemorySnapshotManager()", StaticAccessorType.Dot)]
		[NativeConditional("ENABLE_PROFILER")]
		[MethodImpl(4096)]
		private static extern void StartOperation(uint captureFlag, bool requestScreenshot, string path, bool isRemote);

		// Token: 0x06001BC8 RID: 7112 RVA: 0x0002C8CB File Offset: 0x0002AACB
		public static void TakeSnapshot(string path, Action<string, bool> finishCallback, CaptureFlags captureFlags = CaptureFlags.ManagedObjects | CaptureFlags.NativeObjects)
		{
			MemoryProfiler.TakeSnapshot(path, finishCallback, null, captureFlags);
		}

		// Token: 0x06001BC9 RID: 7113 RVA: 0x0002C8D8 File Offset: 0x0002AAD8
		public static void TakeSnapshot(string path, Action<string, bool> finishCallback, Action<string, bool, DebugScreenCapture> screenshotCallback, CaptureFlags captureFlags = CaptureFlags.ManagedObjects | CaptureFlags.NativeObjects)
		{
			bool flag = MemoryProfiler.m_SnapshotFinished != null;
			if (flag)
			{
				Debug.LogWarning("Canceling snapshot, there is another snapshot in progress.");
				finishCallback.Invoke(path, false);
			}
			else
			{
				MemoryProfiler.m_SnapshotFinished += finishCallback;
				MemoryProfiler.m_SaveScreenshotToDisk += screenshotCallback;
				MemoryProfiler.StartOperation((uint)captureFlags, MemoryProfiler.m_SaveScreenshotToDisk != null, path, false);
			}
		}

		// Token: 0x06001BCA RID: 7114 RVA: 0x0002C92C File Offset: 0x0002AB2C
		public static void TakeTempSnapshot(Action<string, bool> finishCallback, CaptureFlags captureFlags = CaptureFlags.ManagedObjects | CaptureFlags.NativeObjects)
		{
			string[] array = Application.dataPath.Split(new char[] { '/' });
			string text = array[array.Length - 2];
			string text2 = Application.temporaryCachePath + "/" + text + ".snap";
			MemoryProfiler.TakeSnapshot(text2, finishCallback, captureFlags);
		}

		// Token: 0x06001BCB RID: 7115 RVA: 0x0002C978 File Offset: 0x0002AB78
		[RequiredByNativeCode]
		private static byte[] PrepareMetadata()
		{
			bool flag = MemoryProfiler.createMetaData == null;
			byte[] array;
			if (flag)
			{
				array = new byte[0];
			}
			else
			{
				MetaData metaData = new MetaData();
				MemoryProfiler.createMetaData.Invoke(metaData);
				bool flag2 = metaData.content == null;
				if (flag2)
				{
					metaData.content = "";
				}
				bool flag3 = metaData.platform == null;
				if (flag3)
				{
					metaData.platform = "";
				}
				int num = 2 * metaData.content.Length;
				int num2 = 2 * metaData.platform.Length;
				int num3 = num + num2 + 12;
				byte[] array2 = new byte[num3];
				int num4 = 0;
				num4 = MemoryProfiler.WriteIntToByteArray(array2, num4, metaData.content.Length);
				num4 = MemoryProfiler.WriteStringToByteArray(array2, num4, metaData.content);
				num4 = MemoryProfiler.WriteIntToByteArray(array2, num4, metaData.platform.Length);
				num4 = MemoryProfiler.WriteStringToByteArray(array2, num4, metaData.platform);
				array = array2;
			}
			return array;
		}

		// Token: 0x06001BCC RID: 7116 RVA: 0x0002CA6C File Offset: 0x0002AC6C
		internal unsafe static int WriteIntToByteArray(byte[] array, int offset, int value)
		{
			byte* ptr = (byte*)(&value);
			array[offset++] = *ptr;
			array[offset++] = ptr[1];
			array[offset++] = ptr[2];
			array[offset++] = ptr[3];
			return offset;
		}

		// Token: 0x06001BCD RID: 7117 RVA: 0x0002CAB4 File Offset: 0x0002ACB4
		internal unsafe static int WriteStringToByteArray(byte[] array, int offset, string value)
		{
			bool flag = value.Length != 0;
			if (flag)
			{
				fixed (string text = value)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					char* ptr2 = ptr;
					char* ptr3 = ptr + value.Length;
					while (ptr2 != ptr3)
					{
						for (int i = 0; i < 2; i++)
						{
							array[offset++] = *(byte*)(ptr2 + i / 2);
						}
						ptr2++;
					}
				}
			}
			return offset;
		}

		// Token: 0x06001BCE RID: 7118 RVA: 0x0002CB38 File Offset: 0x0002AD38
		[RequiredByNativeCode]
		private static void FinalizeSnapshot(string path, bool result)
		{
			bool flag = MemoryProfiler.m_SnapshotFinished != null;
			if (flag)
			{
				Action<string, bool> snapshotFinished = MemoryProfiler.m_SnapshotFinished;
				MemoryProfiler.m_SnapshotFinished = null;
				snapshotFinished.Invoke(path, result);
			}
		}

		// Token: 0x06001BCF RID: 7119 RVA: 0x0002CB6C File Offset: 0x0002AD6C
		[RequiredByNativeCode]
		private static void SaveScreenshotToDisk(string path, bool result, IntPtr pixelsPtr, int pixelsCount, TextureFormat format, int width, int height)
		{
			bool flag = MemoryProfiler.m_SaveScreenshotToDisk != null;
			if (flag)
			{
				Action<string, bool, DebugScreenCapture> saveScreenshotToDisk = MemoryProfiler.m_SaveScreenshotToDisk;
				MemoryProfiler.m_SaveScreenshotToDisk = null;
				DebugScreenCapture debugScreenCapture = default(DebugScreenCapture);
				if (result)
				{
					NativeArray<byte> nativeArray = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<byte>(pixelsPtr.ToPointer(), pixelsCount, Allocator.Persistent);
					debugScreenCapture.rawImageDataReference = nativeArray;
					debugScreenCapture.height = height;
					debugScreenCapture.width = width;
					debugScreenCapture.imageFormat = format;
				}
				saveScreenshotToDisk.Invoke(path, result, debugScreenCapture);
			}
		}
	}
}
