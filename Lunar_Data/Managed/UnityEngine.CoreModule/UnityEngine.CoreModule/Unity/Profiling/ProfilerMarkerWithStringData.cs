using System;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Unity.Profiling.LowLevel;
using Unity.Profiling.LowLevel.Unsafe;

namespace Unity.Profiling
{
	// Token: 0x02000047 RID: 71
	internal struct ProfilerMarkerWithStringData
	{
		// Token: 0x060000D5 RID: 213 RVA: 0x00002904 File Offset: 0x00000B04
		public static ProfilerMarkerWithStringData Create(string name, string parameterName)
		{
			IntPtr intPtr = ProfilerUnsafeUtility.CreateMarker(name, 16, MarkerFlags.Default, 1);
			ProfilerUnsafeUtility.SetMarkerMetadata(intPtr, 0, parameterName, 9, 0);
			return new ProfilerMarkerWithStringData
			{
				_marker = intPtr
			};
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00002940 File Offset: 0x00000B40
		[Pure]
		[MethodImpl(256)]
		public ProfilerMarkerWithStringData.AutoScope Auto(bool enabled, Func<string> parameterValue)
		{
			ProfilerMarkerWithStringData.AutoScope autoScope;
			if (enabled)
			{
				autoScope = this.Auto(parameterValue.Invoke());
			}
			else
			{
				autoScope = new ProfilerMarkerWithStringData.AutoScope(IntPtr.Zero);
			}
			return autoScope;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00002974 File Offset: 0x00000B74
		[Pure]
		[MethodImpl(256)]
		public unsafe ProfilerMarkerWithStringData.AutoScope Auto(string value)
		{
			bool flag = value == null;
			if (flag)
			{
				throw new ArgumentNullException("value");
			}
			fixed (string text = value)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				ProfilerMarkerData profilerMarkerData = new ProfilerMarkerData
				{
					Type = 9,
					Size = (uint)(value.Length * 2 + 2)
				};
				profilerMarkerData.Ptr = (void*)ptr;
				ProfilerUnsafeUtility.BeginSampleWithMetadata(this._marker, 1, (void*)(&profilerMarkerData));
			}
			return new ProfilerMarkerWithStringData.AutoScope(this._marker);
		}

		// Token: 0x0400011F RID: 287
		private const MethodImplOptions AggressiveInlining = 256;

		// Token: 0x04000120 RID: 288
		private IntPtr _marker;

		// Token: 0x02000048 RID: 72
		public struct AutoScope : IDisposable
		{
			// Token: 0x060000D8 RID: 216 RVA: 0x000029FA File Offset: 0x00000BFA
			[MethodImpl(256)]
			internal AutoScope(IntPtr marker)
			{
				this._marker = marker;
			}

			// Token: 0x060000D9 RID: 217 RVA: 0x00002A04 File Offset: 0x00000C04
			[Pure]
			[MethodImpl(256)]
			public void Dispose()
			{
				bool flag = this._marker != IntPtr.Zero;
				if (flag)
				{
					ProfilerUnsafeUtility.EndSample(this._marker);
				}
			}

			// Token: 0x04000121 RID: 289
			private IntPtr _marker;
		}
	}
}
