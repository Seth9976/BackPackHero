using System;
using System.Diagnostics;
using Unity.Profiling.LowLevel;
using Unity.Profiling.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Profiling
{
	// Token: 0x0200027A RID: 634
	[NativeHeader("Runtime/Profiler/Marker.h")]
	[NativeHeader("Runtime/Profiler/ScriptBindings/Sampler.bindings.h")]
	[UsedByNativeCode]
	public sealed class CustomSampler : Sampler
	{
		// Token: 0x06001BB1 RID: 7089 RVA: 0x0002C6B5 File Offset: 0x0002A8B5
		internal CustomSampler()
		{
		}

		// Token: 0x06001BB2 RID: 7090 RVA: 0x0002C6BF File Offset: 0x0002A8BF
		internal CustomSampler(IntPtr ptr)
		{
			this.m_Ptr = ptr;
		}

		// Token: 0x06001BB3 RID: 7091 RVA: 0x0002C6D0 File Offset: 0x0002A8D0
		public static CustomSampler Create(string name, bool collectGpuData = false)
		{
			IntPtr intPtr = ProfilerUnsafeUtility.CreateMarker(name, 1, MarkerFlags.AvailabilityNonDevelopment | (collectGpuData ? MarkerFlags.SampleGPU : MarkerFlags.Default), 0);
			bool flag = intPtr == IntPtr.Zero;
			CustomSampler customSampler;
			if (flag)
			{
				customSampler = CustomSampler.s_InvalidCustomSampler;
			}
			else
			{
				customSampler = new CustomSampler(intPtr);
			}
			return customSampler;
		}

		// Token: 0x06001BB4 RID: 7092 RVA: 0x0002C715 File Offset: 0x0002A915
		[Conditional("ENABLE_PROFILER")]
		public void Begin()
		{
			ProfilerUnsafeUtility.BeginSample(this.m_Ptr);
		}

		// Token: 0x06001BB5 RID: 7093 RVA: 0x0002C724 File Offset: 0x0002A924
		[Conditional("ENABLE_PROFILER")]
		public void Begin(Object targetObject)
		{
			ProfilerUnsafeUtility.Internal_BeginWithObject(this.m_Ptr, targetObject);
		}

		// Token: 0x06001BB6 RID: 7094 RVA: 0x0002C734 File Offset: 0x0002A934
		[Conditional("ENABLE_PROFILER")]
		public void End()
		{
			ProfilerUnsafeUtility.EndSample(this.m_Ptr);
		}

		// Token: 0x0400090C RID: 2316
		internal static CustomSampler s_InvalidCustomSampler = new CustomSampler();
	}
}
