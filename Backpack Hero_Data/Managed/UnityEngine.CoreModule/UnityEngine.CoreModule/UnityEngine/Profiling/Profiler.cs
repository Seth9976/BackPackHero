using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Profiling;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Profiling
{
	// Token: 0x02000277 RID: 631
	[MovedFrom("UnityEngine")]
	[UsedByNativeCode]
	[NativeHeader("Runtime/Profiler/Profiler.h")]
	[NativeHeader("Runtime/Profiler/ScriptBindings/Profiler.bindings.h")]
	[NativeHeader("Runtime/ScriptingBackend/ScriptingApi.h")]
	[NativeHeader("Runtime/Allocator/MemoryManager.h")]
	[NativeHeader("Runtime/Utilities/MemoryUtilities.h")]
	public sealed class Profiler
	{
		// Token: 0x06001B5D RID: 7005 RVA: 0x00008C2F File Offset: 0x00006E2F
		private Profiler()
		{
		}

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x06001B5E RID: 7006
		public static extern bool supported
		{
			[NativeMethod(Name = "profiler_is_available", IsFreeFunction = true)]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x06001B5F RID: 7007
		// (set) Token: 0x06001B60 RID: 7008
		[StaticAccessor("ProfilerBindings", StaticAccessorType.DoubleColon)]
		public static extern string logFile
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x06001B61 RID: 7009
		// (set) Token: 0x06001B62 RID: 7010
		public static extern bool enableBinaryLog
		{
			[NativeMethod(Name = "ProfilerBindings::IsBinaryLogEnabled", IsFreeFunction = true)]
			[MethodImpl(4096)]
			get;
			[NativeMethod(Name = "ProfilerBindings::SetBinaryLogEnabled", IsFreeFunction = true)]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x06001B63 RID: 7011
		// (set) Token: 0x06001B64 RID: 7012
		public static extern int maxUsedMemory
		{
			[NativeMethod(Name = "ProfilerBindings::GetMaxUsedMemory", IsFreeFunction = true)]
			[MethodImpl(4096)]
			get;
			[NativeMethod(Name = "ProfilerBindings::SetMaxUsedMemory", IsFreeFunction = true)]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x06001B65 RID: 7013
		// (set) Token: 0x06001B66 RID: 7014
		public static extern bool enabled
		{
			[NativeConditional("ENABLE_PROFILER")]
			[NativeMethod(Name = "profiler_is_enabled", IsFreeFunction = true, IsThreadSafe = true)]
			[MethodImpl(4096)]
			get;
			[NativeMethod(Name = "ProfilerBindings::SetProfilerEnabled", IsFreeFunction = true)]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x06001B67 RID: 7015
		// (set) Token: 0x06001B68 RID: 7016
		public static extern bool enableAllocationCallstacks
		{
			[NativeMethod(Name = "ProfilerBindings::IsAllocationCallstackCaptureEnabled", IsFreeFunction = true)]
			[MethodImpl(4096)]
			get;
			[NativeMethod(Name = "ProfilerBindings::SetAllocationCallstackCaptureEnabled", IsFreeFunction = true)]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06001B69 RID: 7017
		[FreeFunction("ProfilerBindings::profiler_set_area_enabled")]
		[Conditional("ENABLE_PROFILER")]
		[MethodImpl(4096)]
		public static extern void SetAreaEnabled(ProfilerArea area, bool enabled);

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x06001B6A RID: 7018 RVA: 0x0002BD10 File Offset: 0x00029F10
		public static int areaCount
		{
			get
			{
				return Enum.GetNames(typeof(ProfilerArea)).Length;
			}
		}

		// Token: 0x06001B6B RID: 7019
		[NativeConditional("ENABLE_PROFILER")]
		[FreeFunction("ProfilerBindings::profiler_is_area_enabled")]
		[MethodImpl(4096)]
		public static extern bool GetAreaEnabled(ProfilerArea area);

		// Token: 0x06001B6C RID: 7020 RVA: 0x0002BD34 File Offset: 0x00029F34
		[Conditional("UNITY_EDITOR")]
		public static void AddFramesFromFile(string file)
		{
			bool flag = string.IsNullOrEmpty(file);
			if (flag)
			{
				Debug.LogError("AddFramesFromFile: Invalid or empty path");
			}
			else
			{
				Profiler.AddFramesFromFile_Internal(file, true);
			}
		}

		// Token: 0x06001B6D RID: 7021
		[NativeConditional("ENABLE_PROFILER && UNITY_EDITOR")]
		[NativeMethod(Name = "LoadFromFile")]
		[StaticAccessor("profiling::GetProfilerSessionPtr()", StaticAccessorType.Arrow)]
		[NativeHeader("Modules/ProfilerEditor/Public/ProfilerSession.h")]
		[MethodImpl(4096)]
		private static extern void AddFramesFromFile_Internal(string file, bool keepExistingFrames);

		// Token: 0x06001B6E RID: 7022 RVA: 0x0002BD64 File Offset: 0x00029F64
		[Conditional("ENABLE_PROFILER")]
		public static void BeginThreadProfiling(string threadGroupName, string threadName)
		{
			bool flag = string.IsNullOrEmpty(threadGroupName);
			if (flag)
			{
				throw new ArgumentException("Argument should be a valid string", "threadGroupName");
			}
			bool flag2 = string.IsNullOrEmpty(threadName);
			if (flag2)
			{
				throw new ArgumentException("Argument should be a valid string", "threadName");
			}
			Profiler.BeginThreadProfilingInternal(threadGroupName, threadName);
		}

		// Token: 0x06001B6F RID: 7023
		[NativeMethod(Name = "ProfilerBindings::BeginThreadProfiling", IsFreeFunction = true, IsThreadSafe = true)]
		[NativeConditional("ENABLE_PROFILER")]
		[MethodImpl(4096)]
		private static extern void BeginThreadProfilingInternal(string threadGroupName, string threadName);

		// Token: 0x06001B70 RID: 7024 RVA: 0x00004557 File Offset: 0x00002757
		[NativeConditional("ENABLE_PROFILER")]
		public static void EndThreadProfiling()
		{
		}

		// Token: 0x06001B71 RID: 7025 RVA: 0x0002BDAE File Offset: 0x00029FAE
		[Conditional("ENABLE_PROFILER")]
		[MethodImpl(256)]
		public static void BeginSample(string name)
		{
			Profiler.ValidateArguments(name);
			Profiler.BeginSampleImpl(name, null);
		}

		// Token: 0x06001B72 RID: 7026 RVA: 0x0002BDC0 File Offset: 0x00029FC0
		[Conditional("ENABLE_PROFILER")]
		[MethodImpl(256)]
		public static void BeginSample(string name, Object targetObject)
		{
			Profiler.ValidateArguments(name);
			Profiler.BeginSampleImpl(name, targetObject);
		}

		// Token: 0x06001B73 RID: 7027 RVA: 0x0002BDD4 File Offset: 0x00029FD4
		[MethodImpl(256)]
		private static void ValidateArguments(string name)
		{
			bool flag = string.IsNullOrEmpty(name);
			if (flag)
			{
				throw new ArgumentException("Argument should be a valid string.", "name");
			}
		}

		// Token: 0x06001B74 RID: 7028
		[NativeMethod(Name = "ProfilerBindings::BeginSample", IsFreeFunction = true, IsThreadSafe = true)]
		[MethodImpl(4096)]
		private static extern void BeginSampleImpl(string name, Object targetObject);

		// Token: 0x06001B75 RID: 7029
		[NativeMethod(Name = "ProfilerBindings::EndSample", IsFreeFunction = true, IsThreadSafe = true)]
		[Conditional("ENABLE_PROFILER")]
		[MethodImpl(4096)]
		public static extern void EndSample();

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x06001B76 RID: 7030 RVA: 0x0002BE00 File Offset: 0x0002A000
		// (set) Token: 0x06001B77 RID: 7031 RVA: 0x00004557 File Offset: 0x00002757
		[Obsolete("maxNumberOfSamplesPerFrame has been depricated. Use maxUsedMemory instead")]
		public static int maxNumberOfSamplesPerFrame
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x06001B78 RID: 7032 RVA: 0x0002BE14 File Offset: 0x0002A014
		[Obsolete("usedHeapSize has been deprecated since it is limited to 4GB. Please use usedHeapSizeLong instead.")]
		public static uint usedHeapSize
		{
			get
			{
				return (uint)Profiler.usedHeapSizeLong;
			}
		}

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x06001B79 RID: 7033
		public static extern long usedHeapSizeLong
		{
			[NativeMethod(Name = "GetUsedHeapSize", IsFreeFunction = true)]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06001B7A RID: 7034 RVA: 0x0002BE2C File Offset: 0x0002A02C
		[Obsolete("GetRuntimeMemorySize has been deprecated since it is limited to 2GB. Please use GetRuntimeMemorySizeLong() instead.")]
		public static int GetRuntimeMemorySize(Object o)
		{
			return (int)Profiler.GetRuntimeMemorySizeLong(o);
		}

		// Token: 0x06001B7B RID: 7035
		[NativeMethod(Name = "ProfilerBindings::GetRuntimeMemorySizeLong", IsFreeFunction = true)]
		[MethodImpl(4096)]
		public static extern long GetRuntimeMemorySizeLong([NotNull("ArgumentNullException")] Object o);

		// Token: 0x06001B7C RID: 7036 RVA: 0x0002BE48 File Offset: 0x0002A048
		[Obsolete("GetMonoHeapSize has been deprecated since it is limited to 4GB. Please use GetMonoHeapSizeLong() instead.")]
		public static uint GetMonoHeapSize()
		{
			return (uint)Profiler.GetMonoHeapSizeLong();
		}

		// Token: 0x06001B7D RID: 7037
		[NativeMethod(Name = "scripting_gc_get_heap_size", IsFreeFunction = true)]
		[MethodImpl(4096)]
		public static extern long GetMonoHeapSizeLong();

		// Token: 0x06001B7E RID: 7038 RVA: 0x0002BE60 File Offset: 0x0002A060
		[Obsolete("GetMonoUsedSize has been deprecated since it is limited to 4GB. Please use GetMonoUsedSizeLong() instead.")]
		public static uint GetMonoUsedSize()
		{
			return (uint)Profiler.GetMonoUsedSizeLong();
		}

		// Token: 0x06001B7F RID: 7039
		[NativeMethod(Name = "scripting_gc_get_used_size", IsFreeFunction = true)]
		[MethodImpl(4096)]
		public static extern long GetMonoUsedSizeLong();

		// Token: 0x06001B80 RID: 7040
		[StaticAccessor("GetMemoryManager()", StaticAccessorType.Dot)]
		[NativeConditional("ENABLE_MEMORY_MANAGER")]
		[MethodImpl(4096)]
		public static extern bool SetTempAllocatorRequestedSize(uint size);

		// Token: 0x06001B81 RID: 7041
		[StaticAccessor("GetMemoryManager()", StaticAccessorType.Dot)]
		[NativeConditional("ENABLE_MEMORY_MANAGER")]
		[MethodImpl(4096)]
		public static extern uint GetTempAllocatorSize();

		// Token: 0x06001B82 RID: 7042 RVA: 0x0002BE78 File Offset: 0x0002A078
		[Obsolete("GetTotalAllocatedMemory has been deprecated since it is limited to 4GB. Please use GetTotalAllocatedMemoryLong() instead.")]
		public static uint GetTotalAllocatedMemory()
		{
			return (uint)Profiler.GetTotalAllocatedMemoryLong();
		}

		// Token: 0x06001B83 RID: 7043
		[NativeMethod(Name = "GetTotalAllocatedMemory")]
		[NativeConditional("ENABLE_MEMORY_MANAGER")]
		[StaticAccessor("GetMemoryManager()", StaticAccessorType.Dot)]
		[MethodImpl(4096)]
		public static extern long GetTotalAllocatedMemoryLong();

		// Token: 0x06001B84 RID: 7044 RVA: 0x0002BE90 File Offset: 0x0002A090
		[Obsolete("GetTotalUnusedReservedMemory has been deprecated since it is limited to 4GB. Please use GetTotalUnusedReservedMemoryLong() instead.")]
		public static uint GetTotalUnusedReservedMemory()
		{
			return (uint)Profiler.GetTotalUnusedReservedMemoryLong();
		}

		// Token: 0x06001B85 RID: 7045
		[StaticAccessor("GetMemoryManager()", StaticAccessorType.Dot)]
		[NativeConditional("ENABLE_MEMORY_MANAGER")]
		[NativeMethod(Name = "GetTotalUnusedReservedMemory")]
		[MethodImpl(4096)]
		public static extern long GetTotalUnusedReservedMemoryLong();

		// Token: 0x06001B86 RID: 7046 RVA: 0x0002BEA8 File Offset: 0x0002A0A8
		[Obsolete("GetTotalReservedMemory has been deprecated since it is limited to 4GB. Please use GetTotalReservedMemoryLong() instead.")]
		public static uint GetTotalReservedMemory()
		{
			return (uint)Profiler.GetTotalReservedMemoryLong();
		}

		// Token: 0x06001B87 RID: 7047
		[NativeMethod(Name = "GetTotalReservedMemory")]
		[StaticAccessor("GetMemoryManager()", StaticAccessorType.Dot)]
		[NativeConditional("ENABLE_MEMORY_MANAGER")]
		[MethodImpl(4096)]
		public static extern long GetTotalReservedMemoryLong();

		// Token: 0x06001B88 RID: 7048 RVA: 0x0002BEC0 File Offset: 0x0002A0C0
		[NativeConditional("ENABLE_MEMORY_MANAGER")]
		public static long GetTotalFragmentationInfo(NativeArray<int> stats)
		{
			return Profiler.InternalGetTotalFragmentationInfo((IntPtr)stats.GetUnsafePtr<int>(), stats.Length);
		}

		// Token: 0x06001B89 RID: 7049
		[NativeMethod(Name = "GetTotalFragmentationInfo")]
		[StaticAccessor("GetMemoryManager()", StaticAccessorType.Dot)]
		[NativeConditional("ENABLE_MEMORY_MANAGER")]
		[MethodImpl(4096)]
		private static extern long InternalGetTotalFragmentationInfo(IntPtr pStats, int count);

		// Token: 0x06001B8A RID: 7050
		[NativeMethod(Name = "GetRegisteredGFXDriverMemory")]
		[StaticAccessor("GetMemoryManager()", StaticAccessorType.Dot)]
		[NativeConditional("ENABLE_PROFILER")]
		[MethodImpl(4096)]
		public static extern long GetAllocatedMemoryForGraphicsDriver();

		// Token: 0x06001B8B RID: 7051 RVA: 0x0002BEEC File Offset: 0x0002A0EC
		[Conditional("ENABLE_PROFILER")]
		public unsafe static void EmitFrameMetaData(Guid id, int tag, Array data)
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			Type elementType = data.GetType().GetElementType();
			bool flag2 = !UnsafeUtility.IsBlittable(elementType);
			if (flag2)
			{
				throw new ArgumentException(string.Format("{0} type must be blittable", elementType));
			}
			Profiler.Internal_EmitGlobalMetaData_Array((void*)(&id), 16, tag, data, data.Length, UnsafeUtility.SizeOf(elementType), true);
		}

		// Token: 0x06001B8C RID: 7052 RVA: 0x0002BF54 File Offset: 0x0002A154
		[Conditional("ENABLE_PROFILER")]
		public unsafe static void EmitFrameMetaData<T>(Guid id, int tag, List<T> data) where T : struct
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			Type typeFromHandle = typeof(T);
			bool flag2 = !UnsafeUtility.IsBlittable(typeof(T));
			if (flag2)
			{
				throw new ArgumentException(string.Format("{0} type must be blittable", typeFromHandle));
			}
			Profiler.Internal_EmitGlobalMetaData_Array((void*)(&id), 16, tag, NoAllocHelpers.ExtractArrayFromList(data), data.Count, UnsafeUtility.SizeOf(typeFromHandle), true);
		}

		// Token: 0x06001B8D RID: 7053 RVA: 0x0002BFC6 File Offset: 0x0002A1C6
		[Conditional("ENABLE_PROFILER")]
		public unsafe static void EmitFrameMetaData<T>(Guid id, int tag, NativeArray<T> data) where T : struct
		{
			Profiler.Internal_EmitGlobalMetaData_Native((void*)(&id), 16, tag, (IntPtr)data.GetUnsafeReadOnlyPtr<T>(), data.Length, UnsafeUtility.SizeOf<T>(), true);
		}

		// Token: 0x06001B8E RID: 7054 RVA: 0x0002BFF0 File Offset: 0x0002A1F0
		[Conditional("ENABLE_PROFILER")]
		public unsafe static void EmitSessionMetaData(Guid id, int tag, Array data)
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			Type elementType = data.GetType().GetElementType();
			bool flag2 = !UnsafeUtility.IsBlittable(elementType);
			if (flag2)
			{
				throw new ArgumentException(string.Format("{0} type must be blittable", elementType));
			}
			Profiler.Internal_EmitGlobalMetaData_Array((void*)(&id), 16, tag, data, data.Length, UnsafeUtility.SizeOf(elementType), false);
		}

		// Token: 0x06001B8F RID: 7055 RVA: 0x0002C058 File Offset: 0x0002A258
		[Conditional("ENABLE_PROFILER")]
		public unsafe static void EmitSessionMetaData<T>(Guid id, int tag, List<T> data) where T : struct
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			Type typeFromHandle = typeof(T);
			bool flag2 = !UnsafeUtility.IsBlittable(typeof(T));
			if (flag2)
			{
				throw new ArgumentException(string.Format("{0} type must be blittable", typeFromHandle));
			}
			Profiler.Internal_EmitGlobalMetaData_Array((void*)(&id), 16, tag, NoAllocHelpers.ExtractArrayFromList(data), data.Count, UnsafeUtility.SizeOf(typeFromHandle), false);
		}

		// Token: 0x06001B90 RID: 7056 RVA: 0x0002C0CA File Offset: 0x0002A2CA
		[Conditional("ENABLE_PROFILER")]
		public unsafe static void EmitSessionMetaData<T>(Guid id, int tag, NativeArray<T> data) where T : struct
		{
			Profiler.Internal_EmitGlobalMetaData_Native((void*)(&id), 16, tag, (IntPtr)data.GetUnsafeReadOnlyPtr<T>(), data.Length, UnsafeUtility.SizeOf<T>(), false);
		}

		// Token: 0x06001B91 RID: 7057
		[NativeMethod(Name = "ProfilerBindings::Internal_EmitGlobalMetaData_Array", IsFreeFunction = true, IsThreadSafe = true)]
		[NativeConditional("ENABLE_PROFILER")]
		[MethodImpl(4096)]
		private unsafe static extern void Internal_EmitGlobalMetaData_Array(void* id, int idLen, int tag, Array data, int count, int elementSize, bool frameData);

		// Token: 0x06001B92 RID: 7058
		[NativeMethod(Name = "ProfilerBindings::Internal_EmitGlobalMetaData_Native", IsFreeFunction = true, IsThreadSafe = true)]
		[NativeConditional("ENABLE_PROFILER")]
		[MethodImpl(4096)]
		private unsafe static extern void Internal_EmitGlobalMetaData_Native(void* id, int idLen, int tag, IntPtr data, int count, int elementSize, bool frameData);

		// Token: 0x06001B93 RID: 7059 RVA: 0x0002C0F4 File Offset: 0x0002A2F4
		[Conditional("ENABLE_PROFILER")]
		public static void SetCategoryEnabled(ProfilerCategory category, bool enabled)
		{
			bool flag = category == ProfilerCategory.Any;
			if (flag)
			{
				throw new ArgumentException("Argument should be a valid category", "category");
			}
			Profiler.Internal_SetCategoryEnabled(category, enabled);
		}

		// Token: 0x06001B94 RID: 7060 RVA: 0x0002C138 File Offset: 0x0002A338
		public static bool IsCategoryEnabled(ProfilerCategory category)
		{
			bool flag = category == ProfilerCategory.Any;
			if (flag)
			{
				throw new ArgumentException("Argument should be a valid category", "category");
			}
			return Profiler.Internal_IsCategoryEnabled(category);
		}

		// Token: 0x06001B95 RID: 7061
		[StaticAccessor("profiling::GetProfilerManagerPtr()", StaticAccessorType.Arrow)]
		[NativeConditional("ENABLE_PROFILER")]
		[NativeHeader("Runtime/Profiler/ProfilerManager.h")]
		[NativeMethod(Name = "GetCategoriesCount")]
		[MethodImpl(4096)]
		public static extern uint GetCategoriesCount();

		// Token: 0x06001B96 RID: 7062 RVA: 0x0002C17C File Offset: 0x0002A37C
		[Conditional("ENABLE_PROFILER")]
		public static void GetAllCategories(ProfilerCategory[] categories)
		{
			int num = 0;
			while ((long)num < Math.Min((long)((ulong)Profiler.GetCategoriesCount()), (long)categories.Length))
			{
				categories[num] = new ProfilerCategory((ushort)num);
				num++;
			}
		}

		// Token: 0x06001B97 RID: 7063 RVA: 0x0002C1B8 File Offset: 0x0002A3B8
		[Conditional("ENABLE_PROFILER")]
		public static void GetAllCategories(NativeArray<ProfilerCategory> categories)
		{
			int num = 0;
			while ((long)num < Math.Min((long)((ulong)Profiler.GetCategoriesCount()), (long)categories.Length))
			{
				categories[num] = new ProfilerCategory((ushort)num);
				num++;
			}
		}

		// Token: 0x06001B98 RID: 7064
		[NativeMethod(Name = "profiler_set_category_enable", IsFreeFunction = true, IsThreadSafe = true)]
		[NativeConditional("ENABLE_PROFILER")]
		[MethodImpl(4096)]
		private static extern void Internal_SetCategoryEnabled(ushort categoryId, bool enabled);

		// Token: 0x06001B99 RID: 7065
		[NativeConditional("ENABLE_PROFILER")]
		[NativeMethod(Name = "profiler_is_category_enabled", IsFreeFunction = true, IsThreadSafe = true)]
		[MethodImpl(4096)]
		private static extern bool Internal_IsCategoryEnabled(ushort categoryId);

		// Token: 0x04000905 RID: 2309
		internal const uint invalidProfilerArea = 4294967295U;
	}
}
