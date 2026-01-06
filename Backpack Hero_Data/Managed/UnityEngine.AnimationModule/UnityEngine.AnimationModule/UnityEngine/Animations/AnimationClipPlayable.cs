using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine.Animations
{
	// Token: 0x0200004A RID: 74
	[StaticAccessor("AnimationClipPlayableBindings", StaticAccessorType.DoubleColon)]
	[NativeHeader("Modules/Animation/Director/AnimationClipPlayable.h")]
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationClipPlayable.bindings.h")]
	[RequiredByNativeCode]
	public struct AnimationClipPlayable : IPlayable, IEquatable<AnimationClipPlayable>
	{
		// Token: 0x060002E0 RID: 736 RVA: 0x00004878 File Offset: 0x00002A78
		public static AnimationClipPlayable Create(PlayableGraph graph, AnimationClip clip)
		{
			PlayableHandle playableHandle = AnimationClipPlayable.CreateHandle(graph, clip);
			return new AnimationClipPlayable(playableHandle);
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x00004898 File Offset: 0x00002A98
		private static PlayableHandle CreateHandle(PlayableGraph graph, AnimationClip clip)
		{
			PlayableHandle @null = PlayableHandle.Null;
			bool flag = !AnimationClipPlayable.CreateHandleInternal(graph, clip, ref @null);
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

		// Token: 0x060002E2 RID: 738 RVA: 0x000048CC File Offset: 0x00002ACC
		internal AnimationClipPlayable(PlayableHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !handle.IsPlayableOfType<AnimationClipPlayable>();
				if (flag2)
				{
					throw new InvalidCastException("Can't set handle: the playable is not an AnimationClipPlayable.");
				}
			}
			this.m_Handle = handle;
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x00004908 File Offset: 0x00002B08
		public PlayableHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x00004920 File Offset: 0x00002B20
		public static implicit operator Playable(AnimationClipPlayable playable)
		{
			return new Playable(playable.GetHandle());
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x00004940 File Offset: 0x00002B40
		public static explicit operator AnimationClipPlayable(Playable playable)
		{
			return new AnimationClipPlayable(playable.GetHandle());
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x00004960 File Offset: 0x00002B60
		public bool Equals(AnimationClipPlayable other)
		{
			return this.GetHandle() == other.GetHandle();
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x00004984 File Offset: 0x00002B84
		public AnimationClip GetAnimationClip()
		{
			return AnimationClipPlayable.GetAnimationClipInternal(ref this.m_Handle);
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x000049A4 File Offset: 0x00002BA4
		public bool GetApplyFootIK()
		{
			return AnimationClipPlayable.GetApplyFootIKInternal(ref this.m_Handle);
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x000049C1 File Offset: 0x00002BC1
		public void SetApplyFootIK(bool value)
		{
			AnimationClipPlayable.SetApplyFootIKInternal(ref this.m_Handle, value);
		}

		// Token: 0x060002EA RID: 746 RVA: 0x000049D4 File Offset: 0x00002BD4
		public bool GetApplyPlayableIK()
		{
			return AnimationClipPlayable.GetApplyPlayableIKInternal(ref this.m_Handle);
		}

		// Token: 0x060002EB RID: 747 RVA: 0x000049F1 File Offset: 0x00002BF1
		public void SetApplyPlayableIK(bool value)
		{
			AnimationClipPlayable.SetApplyPlayableIKInternal(ref this.m_Handle, value);
		}

		// Token: 0x060002EC RID: 748 RVA: 0x00004A04 File Offset: 0x00002C04
		internal bool GetRemoveStartOffset()
		{
			return AnimationClipPlayable.GetRemoveStartOffsetInternal(ref this.m_Handle);
		}

		// Token: 0x060002ED RID: 749 RVA: 0x00004A21 File Offset: 0x00002C21
		internal void SetRemoveStartOffset(bool value)
		{
			AnimationClipPlayable.SetRemoveStartOffsetInternal(ref this.m_Handle, value);
		}

		// Token: 0x060002EE RID: 750 RVA: 0x00004A34 File Offset: 0x00002C34
		internal bool GetOverrideLoopTime()
		{
			return AnimationClipPlayable.GetOverrideLoopTimeInternal(ref this.m_Handle);
		}

		// Token: 0x060002EF RID: 751 RVA: 0x00004A51 File Offset: 0x00002C51
		internal void SetOverrideLoopTime(bool value)
		{
			AnimationClipPlayable.SetOverrideLoopTimeInternal(ref this.m_Handle, value);
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x00004A64 File Offset: 0x00002C64
		internal bool GetLoopTime()
		{
			return AnimationClipPlayable.GetLoopTimeInternal(ref this.m_Handle);
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x00004A81 File Offset: 0x00002C81
		internal void SetLoopTime(bool value)
		{
			AnimationClipPlayable.SetLoopTimeInternal(ref this.m_Handle, value);
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x00004A94 File Offset: 0x00002C94
		internal float GetSampleRate()
		{
			return AnimationClipPlayable.GetSampleRateInternal(ref this.m_Handle);
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x00004AB1 File Offset: 0x00002CB1
		internal void SetSampleRate(float value)
		{
			AnimationClipPlayable.SetSampleRateInternal(ref this.m_Handle, value);
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x00004AC1 File Offset: 0x00002CC1
		[NativeThrows]
		private static bool CreateHandleInternal(PlayableGraph graph, AnimationClip clip, ref PlayableHandle handle)
		{
			return AnimationClipPlayable.CreateHandleInternal_Injected(ref graph, clip, ref handle);
		}

		// Token: 0x060002F5 RID: 757
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern AnimationClip GetAnimationClipInternal(ref PlayableHandle handle);

		// Token: 0x060002F6 RID: 758
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern bool GetApplyFootIKInternal(ref PlayableHandle handle);

		// Token: 0x060002F7 RID: 759
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern void SetApplyFootIKInternal(ref PlayableHandle handle, bool value);

		// Token: 0x060002F8 RID: 760
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern bool GetApplyPlayableIKInternal(ref PlayableHandle handle);

		// Token: 0x060002F9 RID: 761
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern void SetApplyPlayableIKInternal(ref PlayableHandle handle, bool value);

		// Token: 0x060002FA RID: 762
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern bool GetRemoveStartOffsetInternal(ref PlayableHandle handle);

		// Token: 0x060002FB RID: 763
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern void SetRemoveStartOffsetInternal(ref PlayableHandle handle, bool value);

		// Token: 0x060002FC RID: 764
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern bool GetOverrideLoopTimeInternal(ref PlayableHandle handle);

		// Token: 0x060002FD RID: 765
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern void SetOverrideLoopTimeInternal(ref PlayableHandle handle, bool value);

		// Token: 0x060002FE RID: 766
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern bool GetLoopTimeInternal(ref PlayableHandle handle);

		// Token: 0x060002FF RID: 767
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern void SetLoopTimeInternal(ref PlayableHandle handle, bool value);

		// Token: 0x06000300 RID: 768
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern float GetSampleRateInternal(ref PlayableHandle handle);

		// Token: 0x06000301 RID: 769
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern void SetSampleRateInternal(ref PlayableHandle handle, float value);

		// Token: 0x06000302 RID: 770
		[MethodImpl(4096)]
		private static extern bool CreateHandleInternal_Injected(ref PlayableGraph graph, AnimationClip clip, ref PlayableHandle handle);

		// Token: 0x04000145 RID: 325
		private PlayableHandle m_Handle;
	}
}
