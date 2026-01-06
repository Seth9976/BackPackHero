using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine.Animations
{
	// Token: 0x0200004F RID: 79
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationOffsetPlayable.bindings.h")]
	[NativeHeader("Modules/Animation/Director/AnimationOffsetPlayable.h")]
	[NativeHeader("Runtime/Director/Core/HPlayable.h")]
	[StaticAccessor("AnimationOffsetPlayableBindings", StaticAccessorType.DoubleColon)]
	[RequiredByNativeCode]
	internal struct AnimationOffsetPlayable : IPlayable, IEquatable<AnimationOffsetPlayable>
	{
		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060003A9 RID: 937 RVA: 0x000056CC File Offset: 0x000038CC
		public static AnimationOffsetPlayable Null
		{
			get
			{
				return AnimationOffsetPlayable.m_NullPlayable;
			}
		}

		// Token: 0x060003AA RID: 938 RVA: 0x000056E4 File Offset: 0x000038E4
		public static AnimationOffsetPlayable Create(PlayableGraph graph, Vector3 position, Quaternion rotation, int inputCount)
		{
			PlayableHandle playableHandle = AnimationOffsetPlayable.CreateHandle(graph, position, rotation, inputCount);
			return new AnimationOffsetPlayable(playableHandle);
		}

		// Token: 0x060003AB RID: 939 RVA: 0x00005708 File Offset: 0x00003908
		private static PlayableHandle CreateHandle(PlayableGraph graph, Vector3 position, Quaternion rotation, int inputCount)
		{
			PlayableHandle @null = PlayableHandle.Null;
			bool flag = !AnimationOffsetPlayable.CreateHandleInternal(graph, position, rotation, ref @null);
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

		// Token: 0x060003AC RID: 940 RVA: 0x00005744 File Offset: 0x00003944
		internal AnimationOffsetPlayable(PlayableHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !handle.IsPlayableOfType<AnimationOffsetPlayable>();
				if (flag2)
				{
					throw new InvalidCastException("Can't set handle: the playable is not an AnimationOffsetPlayable.");
				}
			}
			this.m_Handle = handle;
		}

		// Token: 0x060003AD RID: 941 RVA: 0x00005780 File Offset: 0x00003980
		public PlayableHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x060003AE RID: 942 RVA: 0x00005798 File Offset: 0x00003998
		public static implicit operator Playable(AnimationOffsetPlayable playable)
		{
			return new Playable(playable.GetHandle());
		}

		// Token: 0x060003AF RID: 943 RVA: 0x000057B8 File Offset: 0x000039B8
		public static explicit operator AnimationOffsetPlayable(Playable playable)
		{
			return new AnimationOffsetPlayable(playable.GetHandle());
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x000057D8 File Offset: 0x000039D8
		public bool Equals(AnimationOffsetPlayable other)
		{
			return this.Equals(other.GetHandle());
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x00005804 File Offset: 0x00003A04
		public Vector3 GetPosition()
		{
			return AnimationOffsetPlayable.GetPositionInternal(ref this.m_Handle);
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x00005821 File Offset: 0x00003A21
		public void SetPosition(Vector3 value)
		{
			AnimationOffsetPlayable.SetPositionInternal(ref this.m_Handle, value);
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x00005834 File Offset: 0x00003A34
		public Quaternion GetRotation()
		{
			return AnimationOffsetPlayable.GetRotationInternal(ref this.m_Handle);
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x00005851 File Offset: 0x00003A51
		public void SetRotation(Quaternion value)
		{
			AnimationOffsetPlayable.SetRotationInternal(ref this.m_Handle, value);
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x00005861 File Offset: 0x00003A61
		[NativeThrows]
		private static bool CreateHandleInternal(PlayableGraph graph, Vector3 position, Quaternion rotation, ref PlayableHandle handle)
		{
			return AnimationOffsetPlayable.CreateHandleInternal_Injected(ref graph, ref position, ref rotation, ref handle);
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x00005870 File Offset: 0x00003A70
		[NativeThrows]
		private static Vector3 GetPositionInternal(ref PlayableHandle handle)
		{
			Vector3 vector;
			AnimationOffsetPlayable.GetPositionInternal_Injected(ref handle, out vector);
			return vector;
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x00005886 File Offset: 0x00003A86
		[NativeThrows]
		private static void SetPositionInternal(ref PlayableHandle handle, Vector3 value)
		{
			AnimationOffsetPlayable.SetPositionInternal_Injected(ref handle, ref value);
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x00005890 File Offset: 0x00003A90
		[NativeThrows]
		private static Quaternion GetRotationInternal(ref PlayableHandle handle)
		{
			Quaternion quaternion;
			AnimationOffsetPlayable.GetRotationInternal_Injected(ref handle, out quaternion);
			return quaternion;
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x000058A6 File Offset: 0x00003AA6
		[NativeThrows]
		private static void SetRotationInternal(ref PlayableHandle handle, Quaternion value)
		{
			AnimationOffsetPlayable.SetRotationInternal_Injected(ref handle, ref value);
		}

		// Token: 0x060003BB RID: 955
		[MethodImpl(4096)]
		private static extern bool CreateHandleInternal_Injected(ref PlayableGraph graph, ref Vector3 position, ref Quaternion rotation, ref PlayableHandle handle);

		// Token: 0x060003BC RID: 956
		[MethodImpl(4096)]
		private static extern void GetPositionInternal_Injected(ref PlayableHandle handle, out Vector3 ret);

		// Token: 0x060003BD RID: 957
		[MethodImpl(4096)]
		private static extern void SetPositionInternal_Injected(ref PlayableHandle handle, ref Vector3 value);

		// Token: 0x060003BE RID: 958
		[MethodImpl(4096)]
		private static extern void GetRotationInternal_Injected(ref PlayableHandle handle, out Quaternion ret);

		// Token: 0x060003BF RID: 959
		[MethodImpl(4096)]
		private static extern void SetRotationInternal_Injected(ref PlayableHandle handle, ref Quaternion value);

		// Token: 0x0400014D RID: 333
		private PlayableHandle m_Handle;

		// Token: 0x0400014E RID: 334
		private static readonly AnimationOffsetPlayable m_NullPlayable = new AnimationOffsetPlayable(PlayableHandle.Null);
	}
}
