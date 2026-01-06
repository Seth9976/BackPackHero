using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine.Animations
{
	// Token: 0x02000053 RID: 83
	[StaticAccessor("AnimationPosePlayableBindings", StaticAccessorType.DoubleColon)]
	[RequiredByNativeCode]
	[NativeHeader("Modules/Animation/Director/AnimationPosePlayable.h")]
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationPosePlayable.bindings.h")]
	[NativeHeader("Runtime/Director/Core/HPlayable.h")]
	internal struct AnimationPosePlayable : IPlayable, IEquatable<AnimationPosePlayable>
	{
		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060003D3 RID: 979 RVA: 0x00005A24 File Offset: 0x00003C24
		public static AnimationPosePlayable Null
		{
			get
			{
				return AnimationPosePlayable.m_NullPlayable;
			}
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x00005A3C File Offset: 0x00003C3C
		public static AnimationPosePlayable Create(PlayableGraph graph)
		{
			PlayableHandle playableHandle = AnimationPosePlayable.CreateHandle(graph);
			return new AnimationPosePlayable(playableHandle);
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x00005A5C File Offset: 0x00003C5C
		private static PlayableHandle CreateHandle(PlayableGraph graph)
		{
			PlayableHandle @null = PlayableHandle.Null;
			bool flag = !AnimationPosePlayable.CreateHandleInternal(graph, ref @null);
			PlayableHandle playableHandle;
			if (flag)
			{
				playableHandle = PlayableHandle.Null;
			}
			else
			{
				playableHandle = @null;
			}
			return playableHandle;
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x00005A8C File Offset: 0x00003C8C
		internal AnimationPosePlayable(PlayableHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !handle.IsPlayableOfType<AnimationPosePlayable>();
				if (flag2)
				{
					throw new InvalidCastException("Can't set handle: the playable is not an AnimationPosePlayable.");
				}
			}
			this.m_Handle = handle;
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x00005AC8 File Offset: 0x00003CC8
		public PlayableHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x00005AE0 File Offset: 0x00003CE0
		public static implicit operator Playable(AnimationPosePlayable playable)
		{
			return new Playable(playable.GetHandle());
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x00005B00 File Offset: 0x00003D00
		public static explicit operator AnimationPosePlayable(Playable playable)
		{
			return new AnimationPosePlayable(playable.GetHandle());
		}

		// Token: 0x060003DA RID: 986 RVA: 0x00005B20 File Offset: 0x00003D20
		public bool Equals(AnimationPosePlayable other)
		{
			return this.Equals(other.GetHandle());
		}

		// Token: 0x060003DB RID: 987 RVA: 0x00005B4C File Offset: 0x00003D4C
		public bool GetMustReadPreviousPose()
		{
			return AnimationPosePlayable.GetMustReadPreviousPoseInternal(ref this.m_Handle);
		}

		// Token: 0x060003DC RID: 988 RVA: 0x00005B69 File Offset: 0x00003D69
		public void SetMustReadPreviousPose(bool value)
		{
			AnimationPosePlayable.SetMustReadPreviousPoseInternal(ref this.m_Handle, value);
		}

		// Token: 0x060003DD RID: 989 RVA: 0x00005B7C File Offset: 0x00003D7C
		public bool GetReadDefaultPose()
		{
			return AnimationPosePlayable.GetReadDefaultPoseInternal(ref this.m_Handle);
		}

		// Token: 0x060003DE RID: 990 RVA: 0x00005B99 File Offset: 0x00003D99
		public void SetReadDefaultPose(bool value)
		{
			AnimationPosePlayable.SetReadDefaultPoseInternal(ref this.m_Handle, value);
		}

		// Token: 0x060003DF RID: 991 RVA: 0x00005BAC File Offset: 0x00003DAC
		public bool GetApplyFootIK()
		{
			return AnimationPosePlayable.GetApplyFootIKInternal(ref this.m_Handle);
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x00005BC9 File Offset: 0x00003DC9
		public void SetApplyFootIK(bool value)
		{
			AnimationPosePlayable.SetApplyFootIKInternal(ref this.m_Handle, value);
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x00005BD9 File Offset: 0x00003DD9
		[NativeThrows]
		private static bool CreateHandleInternal(PlayableGraph graph, ref PlayableHandle handle)
		{
			return AnimationPosePlayable.CreateHandleInternal_Injected(ref graph, ref handle);
		}

		// Token: 0x060003E2 RID: 994
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern bool GetMustReadPreviousPoseInternal(ref PlayableHandle handle);

		// Token: 0x060003E3 RID: 995
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern void SetMustReadPreviousPoseInternal(ref PlayableHandle handle, bool value);

		// Token: 0x060003E4 RID: 996
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern bool GetReadDefaultPoseInternal(ref PlayableHandle handle);

		// Token: 0x060003E5 RID: 997
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern void SetReadDefaultPoseInternal(ref PlayableHandle handle, bool value);

		// Token: 0x060003E6 RID: 998
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern bool GetApplyFootIKInternal(ref PlayableHandle handle);

		// Token: 0x060003E7 RID: 999
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern void SetApplyFootIKInternal(ref PlayableHandle handle, bool value);

		// Token: 0x060003E9 RID: 1001
		[MethodImpl(4096)]
		private static extern bool CreateHandleInternal_Injected(ref PlayableGraph graph, ref PlayableHandle handle);

		// Token: 0x04000150 RID: 336
		private PlayableHandle m_Handle;

		// Token: 0x04000151 RID: 337
		private static readonly AnimationPosePlayable m_NullPlayable = new AnimationPosePlayable(PlayableHandle.Null);
	}
}
