using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace Unity.Profiling.LowLevel.Unsafe
{
	// Token: 0x02000052 RID: 82
	[UsedByNativeCode]
	[StructLayout(2, Size = 8)]
	public readonly struct ProfilerRecorderHandle
	{
		// Token: 0x06000124 RID: 292 RVA: 0x00003000 File Offset: 0x00001200
		internal ProfilerRecorderHandle(ulong handle)
		{
			this.handle = handle;
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000125 RID: 293 RVA: 0x0000300A File Offset: 0x0000120A
		public bool Valid
		{
			get
			{
				return this.handle != 0UL && this.handle != ulong.MaxValue;
			}
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00003024 File Offset: 0x00001224
		internal static ProfilerRecorderHandle Get(ProfilerMarker marker)
		{
			return new ProfilerRecorderHandle((ulong)marker.Handle.ToInt64());
		}

		// Token: 0x06000127 RID: 295 RVA: 0x0000304C File Offset: 0x0000124C
		internal static ProfilerRecorderHandle Get(ProfilerCategory category, string statName)
		{
			bool flag = string.IsNullOrEmpty(statName);
			if (flag)
			{
				throw new ArgumentException("String must be not null or empty", "statName");
			}
			return ProfilerRecorderHandle.GetByName(category, statName);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00003080 File Offset: 0x00001280
		public static ProfilerRecorderDescription GetDescription(ProfilerRecorderHandle handle)
		{
			bool flag = !handle.Valid;
			if (flag)
			{
				throw new ArgumentException("ProfilerRecorderHandle is not initialized or is not available", "handle");
			}
			return ProfilerRecorderHandle.GetDescriptionInternal(handle);
		}

		// Token: 0x06000129 RID: 297
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		[MethodImpl(4096)]
		public static extern void GetAvailable(List<ProfilerRecorderHandle> outRecorderHandleList);

		// Token: 0x0600012A RID: 298 RVA: 0x000030B8 File Offset: 0x000012B8
		[NativeMethod(IsThreadSafe = true)]
		internal static ProfilerRecorderHandle GetByName(ProfilerCategory category, string name)
		{
			ProfilerRecorderHandle profilerRecorderHandle;
			ProfilerRecorderHandle.GetByName_Injected(ref category, name, out profilerRecorderHandle);
			return profilerRecorderHandle;
		}

		// Token: 0x0600012B RID: 299 RVA: 0x000030D0 File Offset: 0x000012D0
		[NativeMethod(IsThreadSafe = true)]
		internal unsafe static ProfilerRecorderHandle GetByName__Unmanaged(ProfilerCategory category, byte* name, int nameLen)
		{
			ProfilerRecorderHandle profilerRecorderHandle;
			ProfilerRecorderHandle.GetByName__Unmanaged_Injected(ref category, name, nameLen, out profilerRecorderHandle);
			return profilerRecorderHandle;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x000030EC File Offset: 0x000012EC
		[MethodImpl(256)]
		internal unsafe static ProfilerRecorderHandle GetByName(ProfilerCategory category, char* name, int nameLen)
		{
			return ProfilerRecorderHandle.GetByName_Unsafe(category, name, nameLen);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00003108 File Offset: 0x00001308
		[NativeMethod(IsThreadSafe = true)]
		private unsafe static ProfilerRecorderHandle GetByName_Unsafe(ProfilerCategory category, char* name, int nameLen)
		{
			ProfilerRecorderHandle profilerRecorderHandle;
			ProfilerRecorderHandle.GetByName_Unsafe_Injected(ref category, name, nameLen, out profilerRecorderHandle);
			return profilerRecorderHandle;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00003124 File Offset: 0x00001324
		[NativeMethod(IsThreadSafe = true)]
		private static ProfilerRecorderDescription GetDescriptionInternal(ProfilerRecorderHandle handle)
		{
			ProfilerRecorderDescription profilerRecorderDescription;
			ProfilerRecorderHandle.GetDescriptionInternal_Injected(ref handle, out profilerRecorderDescription);
			return profilerRecorderDescription;
		}

		// Token: 0x0600012F RID: 303
		[MethodImpl(4096)]
		private static extern void GetByName_Injected(ref ProfilerCategory category, string name, out ProfilerRecorderHandle ret);

		// Token: 0x06000130 RID: 304
		[MethodImpl(4096)]
		private unsafe static extern void GetByName__Unmanaged_Injected(ref ProfilerCategory category, byte* name, int nameLen, out ProfilerRecorderHandle ret);

		// Token: 0x06000131 RID: 305
		[MethodImpl(4096)]
		private unsafe static extern void GetByName_Unsafe_Injected(ref ProfilerCategory category, char* name, int nameLen, out ProfilerRecorderHandle ret);

		// Token: 0x06000132 RID: 306
		[MethodImpl(4096)]
		private static extern void GetDescriptionInternal_Injected(ref ProfilerRecorderHandle handle, out ProfilerRecorderDescription ret);

		// Token: 0x04000155 RID: 341
		private const ulong k_InvalidHandle = 18446744073709551615UL;

		// Token: 0x04000156 RID: 342
		[FieldOffset(0)]
		internal readonly ulong handle;
	}
}
