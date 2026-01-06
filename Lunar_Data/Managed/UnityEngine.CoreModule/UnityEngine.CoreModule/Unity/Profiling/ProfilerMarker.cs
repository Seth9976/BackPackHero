using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Profiling.LowLevel;
using Unity.Profiling.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Scripting;

namespace Unity.Profiling
{
	// Token: 0x02000042 RID: 66
	[UsedByNativeCode]
	public struct ProfilerMarker
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x000027B8 File Offset: 0x000009B8
		public IntPtr Handle
		{
			get
			{
				return this.m_Ptr;
			}
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000027C0 File Offset: 0x000009C0
		[MethodImpl(256)]
		public ProfilerMarker(string name)
		{
			this.m_Ptr = ProfilerUnsafeUtility.CreateMarker(name, 1, MarkerFlags.Default, 0);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000027D2 File Offset: 0x000009D2
		[MethodImpl(256)]
		public unsafe ProfilerMarker(char* name, int nameLen)
		{
			this.m_Ptr = ProfilerUnsafeUtility.CreateMarker(name, nameLen, 1, MarkerFlags.Default, 0);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x000027E5 File Offset: 0x000009E5
		[MethodImpl(256)]
		public ProfilerMarker(ProfilerCategory category, string name)
		{
			this.m_Ptr = ProfilerUnsafeUtility.CreateMarker(name, category, MarkerFlags.Default, 0);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000027FC File Offset: 0x000009FC
		[MethodImpl(256)]
		public unsafe ProfilerMarker(ProfilerCategory category, char* name, int nameLen)
		{
			this.m_Ptr = ProfilerUnsafeUtility.CreateMarker(name, nameLen, category, MarkerFlags.Default, 0);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00002814 File Offset: 0x00000A14
		[MethodImpl(256)]
		public ProfilerMarker(ProfilerCategory category, string name, MarkerFlags flags)
		{
			this.m_Ptr = ProfilerUnsafeUtility.CreateMarker(name, category, flags, 0);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x0000282B File Offset: 0x00000A2B
		[MethodImpl(256)]
		public unsafe ProfilerMarker(ProfilerCategory category, char* name, int nameLen, MarkerFlags flags)
		{
			this.m_Ptr = ProfilerUnsafeUtility.CreateMarker(name, nameLen, category, flags, 0);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00002844 File Offset: 0x00000A44
		[Pure]
		[Conditional("ENABLE_PROFILER")]
		[MethodImpl(256)]
		public void Begin()
		{
			ProfilerUnsafeUtility.BeginSample(this.m_Ptr);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00002853 File Offset: 0x00000A53
		[Conditional("ENABLE_PROFILER")]
		[MethodImpl(256)]
		public void Begin(Object contextUnityObject)
		{
			ProfilerUnsafeUtility.Internal_BeginWithObject(this.m_Ptr, contextUnityObject);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00002863 File Offset: 0x00000A63
		[Pure]
		[Conditional("ENABLE_PROFILER")]
		[MethodImpl(256)]
		public void End()
		{
			ProfilerUnsafeUtility.EndSample(this.m_Ptr);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00002872 File Offset: 0x00000A72
		[Conditional("ENABLE_PROFILER")]
		internal void GetName(ref string name)
		{
			name = ProfilerUnsafeUtility.Internal_GetName(this.m_Ptr);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00002884 File Offset: 0x00000A84
		[Pure]
		[MethodImpl(256)]
		public ProfilerMarker.AutoScope Auto()
		{
			return new ProfilerMarker.AutoScope(this.m_Ptr);
		}

		// Token: 0x0400010D RID: 269
		[NativeDisableUnsafePtrRestriction]
		[NonSerialized]
		internal readonly IntPtr m_Ptr;

		// Token: 0x02000043 RID: 67
		[UsedByNativeCode]
		public struct AutoScope : IDisposable
		{
			// Token: 0x060000D3 RID: 211 RVA: 0x000028A4 File Offset: 0x00000AA4
			[MethodImpl(256)]
			internal AutoScope(IntPtr markerPtr)
			{
				this.m_Ptr = markerPtr;
				bool flag = this.m_Ptr != IntPtr.Zero;
				if (flag)
				{
					ProfilerUnsafeUtility.BeginSample(markerPtr);
				}
			}

			// Token: 0x060000D4 RID: 212 RVA: 0x000028D4 File Offset: 0x00000AD4
			[MethodImpl(256)]
			public void Dispose()
			{
				bool flag = this.m_Ptr != IntPtr.Zero;
				if (flag)
				{
					ProfilerUnsafeUtility.EndSample(this.m_Ptr);
				}
			}

			// Token: 0x0400010E RID: 270
			[NativeDisableUnsafePtrRestriction]
			internal readonly IntPtr m_Ptr;
		}
	}
}
