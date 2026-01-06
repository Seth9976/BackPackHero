using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Burst;
using Unity.Profiling.LowLevel;
using Unity.Profiling.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace Unity.Profiling
{
	// Token: 0x0200004B RID: 75
	[UsedByNativeCode]
	[NativeHeader("Runtime/Profiler/ScriptBindings/ProfilerRecorder.bindings.h")]
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(ProfilerRecorderDebugView))]
	public struct ProfilerRecorder : IDisposable
	{
		// Token: 0x060000DC RID: 220 RVA: 0x00002A44 File Offset: 0x00000C44
		internal ProfilerRecorder(ProfilerRecorderOptions options)
		{
			this = ProfilerRecorder.Create(default(ProfilerRecorderHandle), 0, options);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00002A68 File Offset: 0x00000C68
		public ProfilerRecorder(string statName, int capacity = 1, ProfilerRecorderOptions options = ProfilerRecorderOptions.Default)
		{
			this = new ProfilerRecorder(ProfilerCategory.Any, statName, capacity, options);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00002A7A File Offset: 0x00000C7A
		public ProfilerRecorder(string categoryName, string statName, int capacity = 1, ProfilerRecorderOptions options = ProfilerRecorderOptions.Default)
		{
			this = new ProfilerRecorder(new ProfilerCategory(categoryName), statName, capacity, options);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00002A90 File Offset: 0x00000C90
		public ProfilerRecorder(ProfilerCategory category, string statName, int capacity = 1, ProfilerRecorderOptions options = ProfilerRecorderOptions.Default)
		{
			ProfilerRecorderHandle byName = ProfilerRecorderHandle.GetByName(category, statName);
			this = ProfilerRecorder.Create(byName, capacity, options);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00002AB8 File Offset: 0x00000CB8
		public unsafe ProfilerRecorder(ProfilerCategory category, char* statName, int statNameLen, int capacity = 1, ProfilerRecorderOptions options = ProfilerRecorderOptions.Default)
		{
			ProfilerRecorderHandle byName = ProfilerRecorderHandle.GetByName(category, statName, statNameLen);
			this = ProfilerRecorder.Create(byName, capacity, options);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00002ADF File Offset: 0x00000CDF
		public ProfilerRecorder(ProfilerMarker marker, int capacity = 1, ProfilerRecorderOptions options = ProfilerRecorderOptions.Default)
		{
			this = ProfilerRecorder.Create(ProfilerRecorderHandle.Get(marker), capacity, options);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00002AF5 File Offset: 0x00000CF5
		public ProfilerRecorder(ProfilerRecorderHandle statHandle, int capacity = 1, ProfilerRecorderOptions options = ProfilerRecorderOptions.Default)
		{
			this = ProfilerRecorder.Create(statHandle, capacity, options);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00002B08 File Offset: 0x00000D08
		public unsafe static ProfilerRecorder StartNew(ProfilerCategory category, string statName, int capacity = 1, ProfilerRecorderOptions options = ProfilerRecorderOptions.Default)
		{
			char* ptr = statName;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return new ProfilerRecorder(category, ptr, statName.Length, capacity, options | ProfilerRecorderOptions.StartImmediately);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00002B3C File Offset: 0x00000D3C
		public static ProfilerRecorder StartNew(ProfilerMarker marker, int capacity = 1, ProfilerRecorderOptions options = ProfilerRecorderOptions.Default)
		{
			return new ProfilerRecorder(marker, capacity, options | ProfilerRecorderOptions.StartImmediately);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00002B58 File Offset: 0x00000D58
		internal static ProfilerRecorder StartNew()
		{
			return ProfilerRecorder.Create(default(ProfilerRecorderHandle), 0, ProfilerRecorderOptions.StartImmediately);
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00002B7A File Offset: 0x00000D7A
		public bool Valid
		{
			get
			{
				return this.handle != 0UL && ProfilerRecorder.GetValid(this);
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00002B94 File Offset: 0x00000D94
		public ProfilerMarkerDataType DataType
		{
			get
			{
				this.CheckInitializedAndThrow();
				return ProfilerRecorder.GetValueDataType(this);
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00002BB8 File Offset: 0x00000DB8
		public ProfilerMarkerDataUnit UnitType
		{
			get
			{
				this.CheckInitializedAndThrow();
				return ProfilerRecorder.GetValueUnitType(this);
			}
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00002BDC File Offset: 0x00000DDC
		public void Start()
		{
			this.CheckInitializedAndThrow();
			ProfilerRecorder.Control(this, ProfilerRecorder.ControlOptions.Start);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00002BF3 File Offset: 0x00000DF3
		public void Stop()
		{
			this.CheckInitializedAndThrow();
			ProfilerRecorder.Control(this, ProfilerRecorder.ControlOptions.Stop);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00002C0A File Offset: 0x00000E0A
		public void Reset()
		{
			this.CheckInitializedAndThrow();
			ProfilerRecorder.Control(this, ProfilerRecorder.ControlOptions.Reset);
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00002C24 File Offset: 0x00000E24
		public long CurrentValue
		{
			get
			{
				this.CheckInitializedAndThrow();
				return ProfilerRecorder.GetCurrentValue(this);
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00002C48 File Offset: 0x00000E48
		public double CurrentValueAsDouble
		{
			get
			{
				this.CheckInitializedAndThrow();
				return ProfilerRecorder.GetCurrentValueAsDouble(this);
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00002C6C File Offset: 0x00000E6C
		public long LastValue
		{
			get
			{
				this.CheckInitializedAndThrow();
				return ProfilerRecorder.GetLastValue(this);
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00002C90 File Offset: 0x00000E90
		public double LastValueAsDouble
		{
			get
			{
				this.CheckInitializedAndThrow();
				return ProfilerRecorder.GetLastValueAsDouble(this);
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00002CB4 File Offset: 0x00000EB4
		public int Capacity
		{
			get
			{
				this.CheckInitializedAndThrow();
				return ProfilerRecorder.GetCount(this, ProfilerRecorder.CountOptions.MaxCount);
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x00002CDC File Offset: 0x00000EDC
		public int Count
		{
			get
			{
				this.CheckInitializedAndThrow();
				return ProfilerRecorder.GetCount(this, ProfilerRecorder.CountOptions.Count);
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00002D04 File Offset: 0x00000F04
		public bool IsRunning
		{
			get
			{
				this.CheckInitializedAndThrow();
				return ProfilerRecorder.GetRunning(this);
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00002D28 File Offset: 0x00000F28
		public bool WrappedAround
		{
			get
			{
				this.CheckInitializedAndThrow();
				return ProfilerRecorder.GetWrapped(this);
			}
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00002D4C File Offset: 0x00000F4C
		public ProfilerRecorderSample GetSample(int index)
		{
			this.CheckInitializedAndThrow();
			return ProfilerRecorder.GetSampleInternal(this, index);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00002D74 File Offset: 0x00000F74
		public void CopyTo(List<ProfilerRecorderSample> outSamples, bool reset = false)
		{
			bool flag = outSamples == null;
			if (flag)
			{
				throw new ArgumentNullException("outSamples");
			}
			this.CheckInitializedAndThrow();
			ProfilerRecorder.CopyTo_List(this, outSamples, reset);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00002DAC File Offset: 0x00000FAC
		public unsafe int CopyTo(ProfilerRecorderSample* dest, int destSize, bool reset = false)
		{
			this.CheckInitializedWithParamsAndThrow(dest);
			return ProfilerRecorder.CopyTo_Pointer(this, dest, destSize, reset);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00002DD4 File Offset: 0x00000FD4
		public unsafe ProfilerRecorderSample[] ToArray()
		{
			this.CheckInitializedAndThrow();
			int count = this.Count;
			ProfilerRecorderSample[] array = new ProfilerRecorderSample[count];
			ProfilerRecorderSample[] array2;
			ProfilerRecorderSample* ptr;
			if ((array2 = array) == null || array2.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array2[0];
			}
			ProfilerRecorder.CopyTo_Pointer(this, ptr, count, false);
			array2 = null;
			return array;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00002E29 File Offset: 0x00001029
		internal void FilterToCurrentThread()
		{
			this.CheckInitializedAndThrow();
			ProfilerRecorder.Control(this, ProfilerRecorder.ControlOptions.SetFilterToCurrentThread);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00002E40 File Offset: 0x00001040
		internal void CollectFromAllThreads()
		{
			this.CheckInitializedAndThrow();
			ProfilerRecorder.Control(this, ProfilerRecorder.ControlOptions.SetToCollectFromAllThreads);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00002E58 File Offset: 0x00001058
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		private static ProfilerRecorder Create(ProfilerRecorderHandle statHandle, int maxSampleCount, ProfilerRecorderOptions options)
		{
			ProfilerRecorder profilerRecorder;
			ProfilerRecorder.Create_Injected(ref statHandle, maxSampleCount, options, out profilerRecorder);
			return profilerRecorder;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00002E71 File Offset: 0x00001071
		[NativeMethod(IsThreadSafe = true)]
		private static void Control(ProfilerRecorder handle, ProfilerRecorder.ControlOptions options)
		{
			ProfilerRecorder.Control_Injected(ref handle, options);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00002E7B File Offset: 0x0000107B
		[NativeMethod(IsThreadSafe = true)]
		private static ProfilerMarkerDataUnit GetValueUnitType(ProfilerRecorder handle)
		{
			return ProfilerRecorder.GetValueUnitType_Injected(ref handle);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00002E84 File Offset: 0x00001084
		[NativeMethod(IsThreadSafe = true)]
		private static ProfilerMarkerDataType GetValueDataType(ProfilerRecorder handle)
		{
			return ProfilerRecorder.GetValueDataType_Injected(ref handle);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00002E8D File Offset: 0x0000108D
		[NativeMethod(IsThreadSafe = true)]
		private static long GetCurrentValue(ProfilerRecorder handle)
		{
			return ProfilerRecorder.GetCurrentValue_Injected(ref handle);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00002E96 File Offset: 0x00001096
		[NativeMethod(IsThreadSafe = true)]
		private static double GetCurrentValueAsDouble(ProfilerRecorder handle)
		{
			return ProfilerRecorder.GetCurrentValueAsDouble_Injected(ref handle);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00002E9F File Offset: 0x0000109F
		[NativeMethod(IsThreadSafe = true)]
		private static long GetLastValue(ProfilerRecorder handle)
		{
			return ProfilerRecorder.GetLastValue_Injected(ref handle);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00002EA8 File Offset: 0x000010A8
		[NativeMethod(IsThreadSafe = true)]
		private static double GetLastValueAsDouble(ProfilerRecorder handle)
		{
			return ProfilerRecorder.GetLastValueAsDouble_Injected(ref handle);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00002EB1 File Offset: 0x000010B1
		[NativeMethod(IsThreadSafe = true)]
		private static int GetCount(ProfilerRecorder handle, ProfilerRecorder.CountOptions countOptions)
		{
			return ProfilerRecorder.GetCount_Injected(ref handle, countOptions);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00002EBB File Offset: 0x000010BB
		[NativeMethod(IsThreadSafe = true)]
		private static bool GetValid(ProfilerRecorder handle)
		{
			return ProfilerRecorder.GetValid_Injected(ref handle);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00002EC4 File Offset: 0x000010C4
		[NativeMethod(IsThreadSafe = true)]
		private static bool GetWrapped(ProfilerRecorder handle)
		{
			return ProfilerRecorder.GetWrapped_Injected(ref handle);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00002ECD File Offset: 0x000010CD
		[NativeMethod(IsThreadSafe = true)]
		private static bool GetRunning(ProfilerRecorder handle)
		{
			return ProfilerRecorder.GetRunning_Injected(ref handle);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00002ED8 File Offset: 0x000010D8
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		private static ProfilerRecorderSample GetSampleInternal(ProfilerRecorder handle, int index)
		{
			ProfilerRecorderSample profilerRecorderSample;
			ProfilerRecorder.GetSampleInternal_Injected(ref handle, index, out profilerRecorderSample);
			return profilerRecorderSample;
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00002EF0 File Offset: 0x000010F0
		[NativeMethod(IsThreadSafe = true)]
		private static void CopyTo_List(ProfilerRecorder handle, List<ProfilerRecorderSample> outSamples, bool reset)
		{
			ProfilerRecorder.CopyTo_List_Injected(ref handle, outSamples, reset);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00002EFB File Offset: 0x000010FB
		[NativeMethod(IsThreadSafe = true)]
		private unsafe static int CopyTo_Pointer(ProfilerRecorder handle, ProfilerRecorderSample* outSamples, int outSamplesSize, bool reset)
		{
			return ProfilerRecorder.CopyTo_Pointer_Injected(ref handle, outSamples, outSamplesSize, reset);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00002F08 File Offset: 0x00001108
		public void Dispose()
		{
			bool flag = this.handle == 0UL;
			if (!flag)
			{
				ProfilerRecorder.Control(this, ProfilerRecorder.ControlOptions.Release);
				this.handle = 0UL;
			}
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00002F3C File Offset: 0x0000113C
		[BurstDiscard]
		private unsafe void CheckInitializedWithParamsAndThrow(ProfilerRecorderSample* dest)
		{
			bool flag = this.handle == 0UL;
			if (flag)
			{
				throw new InvalidOperationException("ProfilerRecorder object is not initialized or has been disposed.");
			}
			bool flag2 = dest == null;
			if (flag2)
			{
				throw new ArgumentNullException("dest");
			}
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00002F78 File Offset: 0x00001178
		[BurstDiscard]
		private void CheckInitializedAndThrow()
		{
			bool flag = this.handle == 0UL;
			if (flag)
			{
				throw new InvalidOperationException("ProfilerRecorder object is not initialized or has been disposed.");
			}
		}

		// Token: 0x0600010C RID: 268
		[MethodImpl(4096)]
		private static extern void Create_Injected(ref ProfilerRecorderHandle statHandle, int maxSampleCount, ProfilerRecorderOptions options, out ProfilerRecorder ret);

		// Token: 0x0600010D RID: 269
		[MethodImpl(4096)]
		private static extern void Control_Injected(ref ProfilerRecorder handle, ProfilerRecorder.ControlOptions options);

		// Token: 0x0600010E RID: 270
		[MethodImpl(4096)]
		private static extern ProfilerMarkerDataUnit GetValueUnitType_Injected(ref ProfilerRecorder handle);

		// Token: 0x0600010F RID: 271
		[MethodImpl(4096)]
		private static extern ProfilerMarkerDataType GetValueDataType_Injected(ref ProfilerRecorder handle);

		// Token: 0x06000110 RID: 272
		[MethodImpl(4096)]
		private static extern long GetCurrentValue_Injected(ref ProfilerRecorder handle);

		// Token: 0x06000111 RID: 273
		[MethodImpl(4096)]
		private static extern double GetCurrentValueAsDouble_Injected(ref ProfilerRecorder handle);

		// Token: 0x06000112 RID: 274
		[MethodImpl(4096)]
		private static extern long GetLastValue_Injected(ref ProfilerRecorder handle);

		// Token: 0x06000113 RID: 275
		[MethodImpl(4096)]
		private static extern double GetLastValueAsDouble_Injected(ref ProfilerRecorder handle);

		// Token: 0x06000114 RID: 276
		[MethodImpl(4096)]
		private static extern int GetCount_Injected(ref ProfilerRecorder handle, ProfilerRecorder.CountOptions countOptions);

		// Token: 0x06000115 RID: 277
		[MethodImpl(4096)]
		private static extern bool GetValid_Injected(ref ProfilerRecorder handle);

		// Token: 0x06000116 RID: 278
		[MethodImpl(4096)]
		private static extern bool GetWrapped_Injected(ref ProfilerRecorder handle);

		// Token: 0x06000117 RID: 279
		[MethodImpl(4096)]
		private static extern bool GetRunning_Injected(ref ProfilerRecorder handle);

		// Token: 0x06000118 RID: 280
		[MethodImpl(4096)]
		private static extern void GetSampleInternal_Injected(ref ProfilerRecorder handle, int index, out ProfilerRecorderSample ret);

		// Token: 0x06000119 RID: 281
		[MethodImpl(4096)]
		private static extern void CopyTo_List_Injected(ref ProfilerRecorder handle, List<ProfilerRecorderSample> outSamples, bool reset);

		// Token: 0x0600011A RID: 282
		[MethodImpl(4096)]
		private unsafe static extern int CopyTo_Pointer_Injected(ref ProfilerRecorder handle, ProfilerRecorderSample* outSamples, int outSamplesSize, bool reset);

		// Token: 0x0400012E RID: 302
		internal ulong handle;

		// Token: 0x0400012F RID: 303
		internal const ProfilerRecorderOptions SharedRecorder = (ProfilerRecorderOptions)128;

		// Token: 0x0200004C RID: 76
		internal enum ControlOptions
		{
			// Token: 0x04000131 RID: 305
			Start,
			// Token: 0x04000132 RID: 306
			Stop,
			// Token: 0x04000133 RID: 307
			Reset,
			// Token: 0x04000134 RID: 308
			Release = 4,
			// Token: 0x04000135 RID: 309
			SetFilterToCurrentThread,
			// Token: 0x04000136 RID: 310
			SetToCollectFromAllThreads
		}

		// Token: 0x0200004D RID: 77
		internal enum CountOptions
		{
			// Token: 0x04000138 RID: 312
			Count,
			// Token: 0x04000139 RID: 313
			MaxCount
		}
	}
}
