using System;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Animations
{
	// Token: 0x02000055 RID: 85
	[RequiredByNativeCode]
	[StaticAccessor("AnimationScriptPlayableBindings", StaticAccessorType.DoubleColon)]
	[NativeHeader("Runtime/Director/Core/HPlayableGraph.h")]
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationScriptPlayable.bindings.h")]
	[MovedFrom("UnityEngine.Experimental.Animations")]
	[NativeHeader("Runtime/Director/Core/HPlayable.h")]
	public struct AnimationScriptPlayable : IAnimationJobPlayable, IPlayable, IEquatable<AnimationScriptPlayable>
	{
		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060003F5 RID: 1013 RVA: 0x00005D44 File Offset: 0x00003F44
		public static AnimationScriptPlayable Null
		{
			get
			{
				return AnimationScriptPlayable.m_NullPlayable;
			}
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x00005D5C File Offset: 0x00003F5C
		public static AnimationScriptPlayable Create<T>(PlayableGraph graph, T jobData, int inputCount = 0) where T : struct, IAnimationJob
		{
			PlayableHandle playableHandle = AnimationScriptPlayable.CreateHandle<T>(graph, inputCount);
			AnimationScriptPlayable animationScriptPlayable = new AnimationScriptPlayable(playableHandle);
			animationScriptPlayable.SetJobData<T>(jobData);
			return animationScriptPlayable;
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x00005D88 File Offset: 0x00003F88
		private static PlayableHandle CreateHandle<T>(PlayableGraph graph, int inputCount) where T : struct, IAnimationJob
		{
			IntPtr jobReflectionData = ProcessAnimationJobStruct<T>.GetJobReflectionData();
			PlayableHandle @null = PlayableHandle.Null;
			bool flag = !AnimationScriptPlayable.CreateHandleInternal(graph, ref @null, jobReflectionData);
			PlayableHandle playableHandle;
			if (flag)
			{
				playableHandle = PlayableHandle.Null;
			}
			else
			{
				@null.SetInputCount(inputCount);
				playableHandle = @null;
			}
			return playableHandle;
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x00005DC8 File Offset: 0x00003FC8
		internal AnimationScriptPlayable(PlayableHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !handle.IsPlayableOfType<AnimationScriptPlayable>();
				if (flag2)
				{
					throw new InvalidCastException("Can't set handle: the playable is not an AnimationScriptPlayable.");
				}
			}
			this.m_Handle = handle;
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00005E04 File Offset: 0x00004004
		public PlayableHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x00005E1C File Offset: 0x0000401C
		private void CheckJobTypeValidity<T>()
		{
			Type jobType = this.GetHandle().GetJobType();
			bool flag = jobType != typeof(T);
			if (flag)
			{
				throw new ArgumentException(string.Format("Wrong type: the given job type ({0}) is different from the creation job type ({1}).", typeof(T).FullName, jobType.FullName));
			}
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x00005E74 File Offset: 0x00004074
		public unsafe T GetJobData<T>() where T : struct, IAnimationJob
		{
			this.CheckJobTypeValidity<T>();
			T t;
			UnsafeUtility.CopyPtrToStructure<T>((void*)this.GetHandle().GetJobData(), out t);
			return t;
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x00005EAC File Offset: 0x000040AC
		public unsafe void SetJobData<T>(T jobData) where T : struct, IAnimationJob
		{
			this.CheckJobTypeValidity<T>();
			UnsafeUtility.CopyStructureToPtr<T>(ref jobData, (void*)this.GetHandle().GetJobData());
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x00005EDC File Offset: 0x000040DC
		public static implicit operator Playable(AnimationScriptPlayable playable)
		{
			return new Playable(playable.GetHandle());
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x00005EFC File Offset: 0x000040FC
		public static explicit operator AnimationScriptPlayable(Playable playable)
		{
			return new AnimationScriptPlayable(playable.GetHandle());
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x00005F1C File Offset: 0x0000411C
		public bool Equals(AnimationScriptPlayable other)
		{
			return this.GetHandle() == other.GetHandle();
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x00005F40 File Offset: 0x00004140
		public void SetProcessInputs(bool value)
		{
			AnimationScriptPlayable.SetProcessInputsInternal(this.GetHandle(), value);
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x00005F50 File Offset: 0x00004150
		public bool GetProcessInputs()
		{
			return AnimationScriptPlayable.GetProcessInputsInternal(this.GetHandle());
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x00005F6D File Offset: 0x0000416D
		[NativeThrows]
		private static bool CreateHandleInternal(PlayableGraph graph, ref PlayableHandle handle, IntPtr jobReflectionData)
		{
			return AnimationScriptPlayable.CreateHandleInternal_Injected(ref graph, ref handle, jobReflectionData);
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x00005F78 File Offset: 0x00004178
		[NativeThrows]
		private static void SetProcessInputsInternal(PlayableHandle handle, bool value)
		{
			AnimationScriptPlayable.SetProcessInputsInternal_Injected(ref handle, value);
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x00005F82 File Offset: 0x00004182
		[NativeThrows]
		private static bool GetProcessInputsInternal(PlayableHandle handle)
		{
			return AnimationScriptPlayable.GetProcessInputsInternal_Injected(ref handle);
		}

		// Token: 0x06000406 RID: 1030
		[MethodImpl(4096)]
		private static extern bool CreateHandleInternal_Injected(ref PlayableGraph graph, ref PlayableHandle handle, IntPtr jobReflectionData);

		// Token: 0x06000407 RID: 1031
		[MethodImpl(4096)]
		private static extern void SetProcessInputsInternal_Injected(ref PlayableHandle handle, bool value);

		// Token: 0x06000408 RID: 1032
		[MethodImpl(4096)]
		private static extern bool GetProcessInputsInternal_Injected(ref PlayableHandle handle);

		// Token: 0x04000154 RID: 340
		private PlayableHandle m_Handle;

		// Token: 0x04000155 RID: 341
		private static readonly AnimationScriptPlayable m_NullPlayable = new AnimationScriptPlayable(PlayableHandle.Null);
	}
}
